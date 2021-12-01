(*
    MIT License

    Copyright (c) 2021 Alban DUTILLEUl, Gauvain THOMAS

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*)

module MBASimplifier.Implementation

open MBASimplifier.Ast
open MBASimplifier.Engine
open FParsec.CharParsers

type Stored = 
    | Value of Expr
    | Function of (Expr -> Expr)

type State = { 
               MemoryMap : Map<string, Stored>
               Debug: bool
               // The results of intermediate simplifications are stored for performance reasons
               mutable ExpressionsCache : Map<Expr, Expr>
               mutable Exploration: Set<Expr>
             }

type ParseResult =
    | ParseSuccess of Statement
    | ParseError of string

type CommandResult = 
    | SingleResult of State * Expr
    | AssignmentResult of State * (string * Expr) list

let parseLine state line =
    let parserResult = runParserOnString pcommand_eof () "Input" line
    match parserResult with
        | Success (expr, state, pos) -> ParseSuccess expr
        | Failure (msg, err, state)  -> ParseError msg

let (|StorageSymbol|StorageUnknown|) (state, name) = 
    match (Map.tryFind name.Key state.MemoryMap) with
        | Some s -> StorageSymbol(s)
        | None _ -> StorageUnknown(name.Key)

(* Cycle detection algorithm, see https://en.wikipedia.org/wiki/Cycle_detection *)
let tortoise_and_hare (x0: 'a) (fn: 'a -> 'a) : ('a * int * int) =
  let rec find_cycle n t h = if t = h then n, t else find_cycle (n+1) (fn t) (fn (fn h)) in
  let x1 = fn x0 in
  let n, xn = find_cycle 1 x1 (fn x1) in
  let rec get_cycle_length i xi xni lam =
    if xi = xni then
      i, lam
    else
      get_cycle_length (i+1) (fn xi) (fn xni)
        (if lam = 0 && i > 0 && xni = xn then i else lam)
  in
  let mu, lam = get_cycle_length 0 x0 xn 0 in
  x1, mu, if lam = 0 then n else lam

let rec eval (state: State) (expr: Expr) : Expr =
    let expr', mu, lam = tortoise_and_hare expr (simp state)
    if lam > 1 then
        failwith (sprintf "<cycle of length %d detected while running simplications>" lam) 
    else 
        if expr' = expr then expr else eval state expr' 
and simp (state: State) (expr: Expr) : Expr =  
    let expr' = 
        match (Map.tryFind expr state.ExpressionsCache) with
            | Some e -> e
            | None _ -> 
                match (state, expr) with
                    | StoredSimplification e' -> e'
                    | BeforeAssociativity e' -> e'
                    | AssociativitySimplification e' -> e'
                    | ArithmeticSimplification e' -> e'
                    | BooleanSimplification e' -> e'
                    | BitwiseGenericSimplification e' -> e'
                    | TigressSimplification e' -> e'
                    | RelationalSimplification e' -> e'
                    | BaseSimplification e' -> e'
                    | _ -> expr
    in state.ExpressionsCache <- Map.add expr expr' state.ExpressionsCache; expr'
and (|StoredSimplification|_|) (state, expr) =
    match expr with
        | Constant c -> Some(Constant c)
        | Variable n -> Some(evalVariable state n)
        | FunctionCall (name, e) -> Some(evalFunction state name e)
        | _ -> None
and (|BeforeAssociativity|_|) (state, expr) =
    match expr with
        // ((A + ~ B) + 1) = A - B
        | Add (Add (e1, BitwiseNegation (e2)), Constant 1L) -> Some((simp state e1) - (simp state e2))
        | Add (Constant 1L, Add (e1, BitwiseNegation (e2))) -> Some((simp state e1) - (simp state e2))
        | Add (Constant 1L, Add (BitwiseNegation (e2), e1)) -> Some((simp state e1) - (simp state e2))

        // Constant folding
        | Subtract(BitwiseXor (e1, Constant c1), BitwiseNegation (Multiply (Constant 2L, BitwiseOr (e2, Constant c2)))) 
            when (e1 = e2) && (c1 < 0) && (c2 > 0) && (c2 = abs(c1) - 1L ) -> Some((simp state e1) + Constant (c2))
        | Subtract(Multiply (Constant 2L, BitwiseOr (e1, Constant c1)), BitwiseNegation (BitwiseXor (e2, Constant c2)))
            when (e1 = e2) && (c1 > 0) && (c2 < 0) && (abs(c2) = c1 + 1L) -> Some((simp state e1) + Constant (c1))
        | Subtract(Multiply (Constant 2L, BitwiseAnd (e1, Constant c1)), BitwiseXor (e2, Constant c2)) 
            when (e1 = e2) && (c1 < 0) && (c2 > 0) && (c2 = abs(c1) - 1L ) -> Some((simp state e1) - Constant (c2))
        | Subtract(Multiply (Constant 2L, BitwiseOr (e1, e2)), BitwiseNegation (BitwiseXor (e3, BitwiseNegation (e4))))
            when e1 = e3 && e2 = e4 -> Some((simp state e1) + (simp state e2))
    
        | Subtract(BitwiseXor (e1, BitwiseNegation (e2)),BitwiseNegation (Multiply (Constant 2L, BitwiseOr (e3, e4))))
            when e1 = e3 && e2 = e4 -> Some((simp state e1) + (simp state e2))

        // Multiplication simplification.
        // ((A & B) * (A | B) + (A & ~ B) * (~ A & B)) = A * B
        | Multiply(Multiply(BitwiseAnd (BitwiseNegation (e1), e2), Add(BitwiseOr (e3, e4), BitwiseAnd (e5, BitwiseNegation (e6)))), BitwiseAnd (e7, e8)) 
            when (e1 = e3 && e3 = e5 && e5 = e7) && (e2 = e4 && e4 = e6 && e6 = e8) -> Some((simp state e1) * (simp state e2))                                                                                               
        | Multiply(Multiply(BitwiseAnd (e1, Constant c1), Add (BitwiseOr (e2, Constant c2), BitwiseAnd (e3, BitwiseNegation (Constant c3)))), BitwiseAnd (BitwiseNegation (e4), Constant c4)) 
            when (e1 = e2) && (e2 = e3) && (e3 = e4) && (c1 = c2) && (c2 = c3) && (c3 = c4) -> Some((simp state e1) * (Constant c4))

        // A + B + A, A + A + B, ... = 2 * A + B
        | Add(e1, Add(e2, e3)) when e1 = e3 -> Some(Constant 2 * e1 + e2)
        | Add(e1, Add(e3, e2)) when e1 = e3 -> Some(Constant 2 * e1 + e2)
        | Add(Add(e2, e1), e3) when e1 = e3 -> Some(Constant 2 * e1 + e2)
        | Add(Add(e1, e2), e3) when e1 = e3 -> Some(Constant 2 * e1 + e2)

        // Tigress OR Simplification
        | Add(Negative (BitwiseAnd (e1, e2)), Add (e3, e4)) when e1 = e3 && e2 = e4 -> Some(BitwiseOr(simp state e1, simp state e2))

        | _ -> None
and (|AssociativitySimplification|_|) (state, expr) =
    let permute list =
      let rec inserts e = function
        | [] -> [[e]]
        | x::xs as list -> (e::list)::[for xs' in inserts e xs -> x::xs']                
      List.fold (fun accum x -> List.collect (inserts x) accum) [[]] list

    (* Managing associativity in a term rewriting system is far from trivial, so we will simply avoid cycles through a hash table. *)
    match expr with
        | Add(e1, Add(e2, e3)) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> Add(simp state e[0], simp state (Add(e[1], e[2]))))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    Add(simp state e1, simp state (Add(e2, e3)))
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | Add(Add(e1, e2), e3) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> Add(simp state (Add(e[0], e[1])), simp state e[2])) 
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    Add(simp state (Add(e1, e2)), simp state e3)
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseOr(e1, BitwiseOr(e2, e3)) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseOr(simp state e[0], simp state (BitwiseOr(e[1], e[2]))))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseOr(simp state e1, simp state (BitwiseOr(e2, e3)))
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseOr(BitwiseOr(e2, e3), e1) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseOr(simp state (BitwiseOr(e[1], e[2])), simp state e[0]))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseOr(simp state (BitwiseOr(e2, e3)), simp state e1)
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseAnd(e1, BitwiseAnd(e2, e3)) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseAnd(simp state e[0], simp state (BitwiseAnd(e[1], e[2]))))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseAnd(simp state e1, simp state (BitwiseAnd(e2, e3)))
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseAnd(BitwiseAnd(e2, e3), e1) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseAnd(simp state (BitwiseAnd(e[1], e[2])), simp state e[0]))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseAnd(simp state (BitwiseAnd(e2, e3)), simp state e1)
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseXor(e1, BitwiseXor(e2, e3)) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseXor(simp state e[0], simp state (BitwiseXor(e[1], e[2]))))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseXor(simp state e1, simp state (BitwiseXor(e2, e3)))
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | BitwiseXor(BitwiseXor(e2, e3), e1) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> BitwiseXor(simp state (BitwiseXor(e[1], e[2])), simp state e[0]))
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    BitwiseXor(simp state (BitwiseXor(e2, e3)), simp state e1)
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | Multiply(e1, Multiply(e2, e3)) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> Multiply(simp state e[0], simp state (Multiply(e[1], e[2])))) 
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    Multiply(simp state e1, simp state (Multiply(e2, e3)))
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | Multiply(Multiply(e1, e2), e3) -> 
            Some(
                let visitedExpressions = 
                    permute [e1; e2; e3] 
                    |> List.map (fun e -> Multiply(simp state (Multiply(e[0], e[1])), simp state e[2])) 
                    |> List.filter(fun e -> not (state.Exploration.Contains(e)))
                visitedExpressions |> List.iter(fun e -> state.Exploration <- state.Exploration.Add(e)) 
                if visitedExpressions = [] then 
                    Multiply(simp state (Multiply(e1, e2)), simp state e3)
                else
                    visitedExpressions |> List.minBy (fun e -> e.Complexity)
            )

        | _ -> None
and (|ArithmeticSimplification|_|) (state, expr) =
    match expr with
        // Constants result.
        | Add (Constant l, Constant r) -> Some(Constant (l + r))
        | Subtract(Constant l, Constant r) -> Some(Constant (l - r))
        | Multiply(Constant l, Constant r) -> Some(Constant(l * r))
        | Divide(Constant l, Constant r) -> Some(Constant (l / r))
        | Modulo(Constant l, Constant r) -> Some(Constant (l % r))
        | Negative(Constant e) -> Some(Constant(-e))

        // Involution.
        // -(-(A)) = A
        | Negative(Negative(e)) -> Some(simp state e)

        // Misc
        // A + A = 2 * A
        | Add(e1, e2) when e1 = e2 -> Some(Constant 2 * simp state e1)
        // A - (B * A) = A * (1 - B), ...
        | Subtract(e1, Multiply(e2, e3)) when e1 = e3 -> Some((simp state e1) * ((Constant 1) - (simp state e2)))
        | Subtract(e1, Multiply(e3, e2)) when e1 = e3 -> Some((simp state e1) * ((Constant 1) - (simp state e2)))   
        // (A + B) - A = B, ...
        | Subtract(Add(e1, e2), e3) when e1 = e3 -> Some((simp state e2)) 
        | Subtract(Add(e2, e1), e3) when e1 = e3 -> Some((simp state e2))
        // A - (B + A) = -B, ...
        | Subtract(e1, Add(e2, e3)) when e1 = e3 -> Some(-(simp state e2))
        | Subtract(e1, Add(e3, e2)) when e1 = e3 -> Some(-(simp state e2))
        // A + (B - A) = B, ...
        | Add(e1, Subtract(e2, e3)) when e1 = e3 -> Some((simp state e2))
        | Add(e1, Subtract(e3, e2)) when e1 = e3 -> Some((Constant 2) * (simp state e1) - (simp state e2))
        | Add(Subtract(e2, e1), e3) when e1 = e3 -> Some((simp state e2))
        | Add(Subtract(e1, e2), e3) when e1 = e3 -> Some((Constant 2) * (simp state e1) - (simp state e2))
        // (A - B) - A = - B, ...
        | Subtract(e1, Subtract(e2, e3)) when e1 = e3 -> Some((Constant 2) * (simp state e1) - (simp state e2))
        | Subtract(e1, Subtract(e3, e2)) when e1 = e3 -> Some(simp state e2)
        | Subtract(Subtract(e2, e1), e3) when e1 = e3 -> Some((simp state e2) - (Constant 2) * (simp state e1))
        | Subtract(Subtract(e1, e2), e3) when e1 = e3 -> Some(-(simp state e2))
        // {1} * {2} + {1} * {3} = {1} * ({2} + {3})
        | Add(Multiply(e1, e2), Multiply(e3, e4)) when e1 = e3 -> Some((simp state e1) * ((simp state e2) + e4))
        | Add(Multiply(e2, e1), Multiply(e3, e4)) when e1 = e3 -> Some((simp state e1) * ((simp state e2) + e4))
        | Add(Multiply(e1, e2), Multiply(e4, e3)) when e1 = e3 -> Some((simp state e1) * ((simp state e2) + e4))
        | Add(Multiply(e2, e1), Multiply(e4, e3)) when e1 = e3 -> Some((simp state e1) * ((simp state e2) + e4))
        | Add(e1, Multiply(e3, Constant e2)) when e1 = e3 -> Some(Constant(1L + e2) * (simp state e1))
        | Add(e1, Multiply(Constant e2, e3)) when e1 = e3 -> Some(Constant (1L + e2) * (simp state e1))
        | Add(Multiply(e1, Constant e2), e3) when e1 = e3 -> Some(Constant (1L + e2) * (simp state e1))
        | Add(Multiply(Constant e2, e1), e3) when e1 = e3 -> Some(Constant (1L + e2) * (simp state e1))

        | Subtract(Multiply(e1, e2), Multiply(e3, e4)) when e1 = e3 -> Some(((simp state e2) - (simp state e4)) * (simp state e1))
        | Subtract(Multiply(e2, e1), Multiply(e3, e4)) when e1 = e3 -> Some(((simp state e2) - (simp state e4)) * (simp state e1))
        | Subtract(Multiply(e1, e2), Multiply(e4, e3)) when e1 = e3 -> Some(((simp state e2) - (simp state e4)) * (simp state e1))
        | Subtract(Multiply(e2, e1), Multiply(e4, e3)) when e1 = e3 -> Some(((simp state e2) - (simp state e4)) * (simp state e1))
        | Subtract(e1, Multiply(e3, Constant e2)) when e1 = e3 -> Some(Constant (1L - e2) * (simp state e1))
        | Subtract(e1, Multiply(Constant e2, e3)) when e1 = e3 -> Some(Constant (1L - e2) * (simp state e1))
        | Subtract(Multiply(e1, Constant e2), e3) when e1 = e3 -> Some(Constant (e2 - 1L) * (simp state e1))
        | Subtract(Multiply(Constant e2, e1), e3) when e1 = e3 -> Some(Constant (e2 - 1L) * (simp state e1))

        // Identity and absorbing elements
        // A - A = 0
        | Subtract(e1, e2) when e1=e2 -> Some(Constant 0)
        // 0 - A = -A
        | Subtract(Constant 0L, e1) -> Some(Negative(simp state e1))
        // -(0)=0
        | Negative(Constant(0L)) -> Some(Constant(0))
        // A - 0 = A
        | Subtract(e, Constant(0L)) -> Some(simp state e)
        // A + 0 = A
        | Add(e, Constant(0L)) -> Some(simp state e)
        // 0 + A = A
        | Add(Constant(0L), e) -> Some(simp state e)
        // A * 1 = A
        | Multiply(e, Constant(1L)) -> Some(simp state e)
        // 1 * A = A
        | Multiply(Constant(1L), e) -> Some(simp state e)
        // A * 0 = 0
        | Multiply(e, Constant(0L)) -> Some(Constant(0))
        // 0 * A = 0
        | Multiply(Constant(0L), e) -> Some(Constant(0))
        // 0 / A = 0
        | Divide(Constant(0L), e) -> Some(Constant(0))
        // A / 1
        | Divide(e, Constant(1L)) -> Some(simp state e)
        // A / A = 1
        | Divide(v1, v2) when v1 = v2 -> Some(Constant 1)
        // A % A = 0
        | Modulo(e1, e2) when e1 = e2 -> Some(Constant 0)

        | _ -> None
and (|BooleanSimplification|_|) (state, expr) =
    let is_predicate_true pred = pred > 0L
    let is_predicate_false pred = pred <= 0L
    match expr with
        // Constant result.
        // !T = F, !F = T 
        | LogicNegation(Constant p) -> Some(if is_predicate_true p then Constant 0 else Constant 1)
        // T || T = T, T || F = T, ...
        | LogicOr(Constant l, Constant r) -> Some(if (is_predicate_true l || is_predicate_true r) then Constant 1 else Constant 0)
        // T && T = T, T && F = F, ...
        | LogicAnd(Constant l, Constant r) -> Some(if (is_predicate_true l && is_predicate_true r) then Constant 1 else Constant 0)

        // Identity constants.
        // A || F = A, F || A = A
        | LogicOr(e, Constant p) when is_predicate_false p -> Some(simp state e)
        | LogicOr(Constant p, e) when is_predicate_false p -> Some(simp state e)

        // A && T = A, T && A = A
        | LogicAnd(e, Constant p) when is_predicate_true p -> Some(simp state e)
        | LogicAnd(Constant p, e) when is_predicate_true p -> Some(simp state e)

        // A || T = T, T && A = T 
        | LogicOr(e, Constant p) when is_predicate_true p -> Some(Constant 1)
        | LogicOr(Constant p, e) when is_predicate_true p -> Some(Constant 1)

        // A && 0 = F, 0 && A = F 
        | LogicAnd(e, Constant p) when is_predicate_false p -> Some(Constant 0)
        | LogicAnd(Constant p, e) when is_predicate_false p -> Some(Constant 0)

        // Involution
        // !(!A)) = A
        | LogicNegation(LogicNegation(e)) -> Some(simp state e)

        | _ -> None

and (|BitwiseGenericSimplification|_|) (state, expr) =
    match expr with
       // Identity constants.
       // A|A = A
       | BitwiseOr(l, r) when l = r -> Some(simp state l)
       // A|0 = A
       | BitwiseOr(l, Constant 0L) -> Some(simp state l)
       | BitwiseOr(Constant 0L, e1) -> Some((simp state e1))
       // A&A = A
       | BitwiseAnd(l, r) when l = r -> Some(simp state l)
       // A^0 = A
       | BitwiseXor(l, Constant 0L) -> Some(simp state l)
       | BitwiseXor(Constant 0L, l) -> Some(simp state l)

       // A&-1 = A
       | BitwiseAnd(l, Constant -1L) -> Some(simp state l)
       | BitwiseAnd(Constant -1L, l) -> Some(simp state l)

       // A<<0 = A
       | BitwiseLeftShift(l, Constant 0L) -> Some(simp state l)
       // A>>0 = A
       | BitwiseRightShift(l, Constant 0L) -> Some(simp state l)

       // Constant result.
       // A&0 = 0
       | BitwiseAnd(l, Constant 0L) -> Some(Constant 0)
       | BitwiseAnd(Constant 0L, l) -> Some(Constant 0)
       // A^A = A
       | BitwiseXor(l, r) when l = r -> Some(Constant 0)
       // A&~A = 0
       | BitwiseAnd(BitwiseNegation(e2), e1) when e1 = e2 -> Some(Constant 0)
       | BitwiseAnd(e1, BitwiseNegation(e2)) when e1 = e2 -> Some(Constant 0)
       // A|-1 = -1
       | BitwiseOr(e1, Constant -1L) -> Some(Constant -1)
       | BitwiseOr(Constant -1L, e1) -> Some(Constant -1)
       // A+(~A) = -1
       | Add(e1, BitwiseNegation e2) when e1 = e2 -> Some(Constant -1)
       | Add(BitwiseNegation e2, e1) when e1 = e2 -> Some(Constant -1)
       // A^(~A) = -1
       | BitwiseXor(e1, BitwiseNegation e2) when e1 = e2 -> Some(Constant -1)
       | BitwiseXor(BitwiseNegation e2, e1) when e1 = e2 -> Some(Constant -1)
       // A|(~A) = -1
       | BitwiseOr(e1, BitwiseNegation e2) when e1 = e2 -> Some(Constant -1)
       | BitwiseOr(BitwiseNegation e2, e1) when e1 = e2 -> Some(Constant -1)
       // ~(~(A)) = A
       | BitwiseNegation(BitwiseNegation(e)) -> Some(simp state e)

       // NOT conversion.
       // A^-1 = ~A
       | BitwiseXor(e1, Constant -1L) -> Some(BitwiseNegation(simp state e1))
       | BitwiseXor(Constant -1L, e1) -> Some(BitwiseNegation(simp state e1))

       // Simplify shifts
       // A << C = A * (2^C)
       // Does not reduce complexity but provides a single representation for other simplifications
       | BitwiseLeftShift(e, Constant c1) -> Some(Multiply(Constant (pown 2 (int(c1))), simp state e))

       // (~A|~B) = ~(A&B)
       | BitwiseOr (BitwiseNegation (e1), BitwiseNegation (e2)) -> Some(BitwiseNegation(BitwiseAnd(simp state e1, simp state e2)))
       | _ -> None
and (|TigressSimplification|_|) (state, expr) =
    let repeat n fn = List.init (abs(n)) (fun _ -> fn) |> List.reduce (>>)
    match expr with
        // A-C = ~(-(...(A)))
        | Subtract(a, Constant 1L) -> 
            Some(
                [simp state a |> repeat ((int) 1) (fun e -> BitwiseNegation(Negative(e)))] 
                |> List.minBy (fun e -> e.Complexity)
            )
        // A+C = -(~(...(A)))
        | Add(a, Constant 1L) -> 
            Some(
                [simp state a |> repeat ((int) 1) (fun e -> Negative(BitwiseNegation(e))); Add(Constant 1, simp state a)] 
                |> List.minBy (fun e -> e.Complexity)
            ) 
        // C+A = -(~(...(A)))
        | Add(Constant 1L, a) -> 
            Some(
                [simp state a |> repeat ((int) 1) (fun e -> Negative(BitwiseNegation(e))); Add(Constant 1, simp state a)] 
                |> List.minBy (fun e -> e.Complexity)
            ) 

        // NEG Simplification
        // -~A + ~-B = A + B
        | Add(Negative (BitwiseNegation (e1)), BitwiseNegation (Negative (e2))) -> Some((simp state e1) + (simp state e2))
        | Add(BitwiseNegation (Negative (e2)), Negative (BitwiseNegation (e1))) -> Some((simp state e1) + (simp state e2))

        // OR Simplification.
        // ((A&~B)+B) = A|B
        | Add (BitwiseAnd (e1, BitwiseNegation (e2)), e3) when e2 = e3 -> Some(BitwiseOr(simp state e1, simp state e2))
        | Add (e3, BitwiseAnd (e1, BitwiseNegation (e2))) when e2 = e3 -> Some(BitwiseOr(simp state e1, simp state e2))
        | Add (e1, Subtract (e2, BitwiseAnd (e3, e4))) when e1=e3 && e2=e4 -> Some(BitwiseOr(simp state e1, simp state e2))

        // ((-~A) + ~(A & B)) = A&~B
        | Add(Negative (BitwiseNegation (e1)), BitwiseNegation (BitwiseAnd (e3, e4))) when e1=e3 -> Some(BitwiseAnd(simp state e1, BitwiseNegation(simp state e4)))
   
        // XOR Simplification.
        // ((A|B) - (A&B)) = A^B
        | Subtract(BitwiseOr (e1, e2), BitwiseAnd (e3, e4)) when e1=e3 && e2=e4 -> Some(BitwiseXor(simp state e1, simp state e2))
        // (((A-B) - ((A|~B) * 2))) = (A^B)+2
        | Subtract(Subtract (e1, e2), Multiply (BitwiseOr (e3, BitwiseNegation (e4)), Constant 2L)) when e1=e3 && e2=e4 -> Some(Add(BitwiseXor(simp state e1, simp state e2), Constant 2L))
        // (((A-B) - 2 * ((A|~B)))) = (A^B)+2
        | Subtract(Subtract (e1, e2), Multiply (Constant 2L, BitwiseOr (e3, BitwiseNegation (e4)))) when e1=e3 && e2=e4 -> Some(Add(BitwiseXor(simp state e1, simp state e2), Constant 2L))

        // AND simplification.
        // (~A|B) - ~A = A&B
        | Subtract(BitwiseOr (BitwiseNegation (e1), e2), BitwiseNegation (e3)) when e1 = e3 -> Some(BitwiseAnd ((simp state e1), simp state e2))
        // A + -(~(~A|B)) = A&B
        | Add(e1, Negative(BitwiseNegation (BitwiseOr (BitwiseNegation (e2), e3)))) when e1 = e2 -> Some(BitwiseAnd(simp state e1, simp state e3))     
        // -~A + ~A | B = A&B
        | Add(Negative (BitwiseNegation (e1)), BitwiseOr (BitwiseNegation (e2), e3)) when e1 = e2 -> Some((simp state e1) &&& (simp state e3))

        // Sub simplification
        // ((A ^ B) - 2 * ((~ A & B))) = A - B
        | Subtract(BitwiseXor (e1, e2),Multiply (Constant 2L, BitwiseAnd (BitwiseNegation (e3), e4))) when e1=e3 && e2=e4 -> Some((simp state e1) - (simp state e2))
        // ((A & ~ B) - (~ A & B)) = A - B
        | Subtract(BitwiseAnd (e1, BitwiseNegation (e2)), BitwiseAnd (BitwiseNegation (e3), e4)) when e1=e3 && e2=e4 -> Some((simp state e1) - (simp state e2))
        // ((2 * (A & ~ B)) - (A ^ B)) = A - B
        | Subtract(Multiply (Constant 2L, BitwiseAnd (e1, BitwiseNegation (e2))), BitwiseXor (e3, e4)) when e1=e3 && e2=e4 -> Some((simp state e1) - (simp state e2))

        // Addition simplification.
        // ~(-(A - ~B)) = A + B
        | BitwiseNegation (Negative (Subtract (e1, BitwiseNegation (e2)))) -> Some((simp state e1) + (simp state e2))
        | BitwiseNegation (Negative (Subtract (e1, Constant c1))) when c1 < 0 -> Some((simp state e1) + Constant (abs(c1) - 1L))

        // (A^B) + (A & B) * 2, (A & B) * 2 + (A^B) = A + B
        | Add(BitwiseXor (e1, e2), Multiply (Constant 2L, BitwiseAnd (e3, e4))) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))
        | Add(Multiply (Constant 2L, BitwiseAnd (e3, e4)), BitwiseXor (e1, e2)) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))
        | Add(BitwiseXor (e1, BitwiseNegation (e2)), Multiply (Constant 2L, BitwiseOr (e3, e4))) when e1=e3 && e2=e4  -> Some(((simp state e1) + (simp state e2)) - Constant 1L) 
        | Add(Multiply (Constant 2L, BitwiseOr (e3, e4)), BitwiseXor (e1, BitwiseNegation (e2))) when e1=e3 && e2=e4  -> Some(((simp state e1) + (simp state e2)) - Constant 1L) 
        | Add(Multiply (Constant 2L, BitwiseOr (e1, e2)), Negative (BitwiseNegation (BitwiseXor (e3, BitwiseNegation (e4))))) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))

        // (((A | B) << 1) - (A ^ B)) = A + B
        | Add(Negative (BitwiseNegation (BitwiseXor (e3, BitwiseNegation (e4)))), Multiply (Constant 2L, BitwiseOr (e1, e2))) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))
        | Subtract(Multiply (Constant 2L, BitwiseOr (e1, e2)), BitwiseXor (e3, e4)) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))

        // ((A | B) + (A & B)) = A + B
        | Add (BitwiseOr (e1, e2), BitwiseAnd (e3, e4)) when e1=e3 && e2=e4 -> Some((simp state e1) + (simp state e2))
        | Add(Negative (BitwiseNegation (BitwiseXor (a, Constant c1))), Multiply (Constant 2L, BitwiseOr (b, Constant c2))) 
            when a = b && c1 < 0 && c2 > 0 && (c1 = ~~~c2) -> Some((simp state a) + (Constant c2))  
        | Add(Multiply (Constant 2L, BitwiseOr (b, Constant c2)), Negative (BitwiseNegation (BitwiseXor (a, Constant c1)))) 
            when a = b && c1 < 0 && c2 > 0 && (c1 = ~~~c2) -> Some((simp state a) + (Constant c2))
        | Add(Negative(BitwiseNegation(Multiply (Constant 2L, BitwiseOr (a, Constant c2)))),BitwiseXor (b, Constant c1))
            when a = b && c1 < 0 && c2 > 0 && (c1 = ~~~c2) -> Some((simp state a) + (Constant c2))
        | Add(BitwiseXor (b, Constant c1), Negative(BitwiseNegation(Multiply (Constant 2L, BitwiseOr (a, Constant c2)))))
            when a = b && c1 < 0 && c2 > 0 && (c1 = ~~~c2) -> Some((simp state a) + (Constant c2))
        | Add(BitwiseXor (b, BitwiseNegation(e1)), Negative(BitwiseNegation(Multiply (Constant 2L, BitwiseOr (a, e2)))))
            when a = b && e1=e2 -> Some((simp state a) + simp state e1)  
        | Add(Negative(BitwiseNegation(Multiply (Constant 2L, BitwiseOr (a, e2)))), BitwiseXor (b, BitwiseNegation(e1)))
            when a = b && e1=e2 -> Some((simp state a) + simp state e1)
        | Add(Negative (BitwiseNegation (BitwiseXor (a, BitwiseNegation(e1)))), Multiply (Constant 2L, BitwiseOr (b, e2))) 
            when a = b && e1=e2 -> Some((simp state a) + simp state e1) 
        | Add(Multiply (Constant 2L, BitwiseOr (b, e1)), Negative (BitwiseNegation (BitwiseXor (a, BitwiseNegation(e2))))) 
            when a = b && e1=e2 -> Some((simp state a) + simp state e1)

        | _ -> None
and (|RelationalSimplification|_|) (state, expr) =
    match expr with
        // Invert comparison.
        // ~(A>B) = A<=B
        | BitwiseNegation(GreaterThan(l, r)) -> Some(LessOrEqual(simp state l, simp state r))
        // ~(A>=B) = A<B
        | BitwiseNegation(GreaterOrEqual(l, r)) -> Some(LessThan(simp state l, simp state r))
        // ~(A==B) = A!=B
        | BitwiseNegation(Equal(l, r)) -> Some(NotEqual(simp state l, simp state r))
        // ~(A!=B) = A==B
        | BitwiseNegation(NotEqual(l, r)) -> Some(Equal(simp state l, simp state r))
        // ~(A<=B) = A>B
        | BitwiseNegation(LessOrEqual(l, r)) -> Some(GreaterThan(simp state l, simp state r))
        // ~(A<B) >= A>=B
        | BitwiseNegation(LessThan(l, r)) -> Some(GreaterOrEqual(simp state l, simp state r))

        // Comparison result.
        (* a == a, a == b, a != a, a != b*)
        | Equal(l, r) when l = r -> Some(Constant(1))
        | Equal(l, r) when l <> r -> Some(Constant(0))
        | NotEqual(l, r) when l = r -> Some(Constant(0))
        | NotEqual(l, r) when l <> r -> Some(Constant(1))

        // Constant result.
        | GreaterThan(Constant l, Constant r) -> Some(if l > r then Constant 1 else Constant 0)
        | GreaterOrEqual(Constant l, Constant r) -> Some(if l >= r then Constant 1 else Constant 0)
        | LessOrEqual(Constant l, Constant r) -> Some(if l <= r then Constant 1 else Constant 0)
        | LessThan(Constant l, Constant r) -> Some(if l < r then Constant 1 else Constant 0)

        | _ -> None
and (|BaseSimplification|_|) (state, expr) =
    match expr with
        // Arithmetic.
        | Add (l, r) -> Some((simp state l) + (simp state r))
        | Subtract (l, r) -> Some((simp state l) - (simp state r))
        | Multiply (l, r) -> Some((simp state l) * (simp state r))
        | Divide (l, r) -> Some((simp state l) / (simp state r))
        | Modulo (l, r) -> Some((simp state l) % (simp state r))
        | Negative e -> Some(Negative(simp state e))

        // Logic.
        | LogicNegation(e) -> Some(LogicNegation((simp state e)))
        | LogicAnd(l, r) -> Some(LogicAnd((simp state l), (simp state r)))
        | LogicOr(l, r) -> Some(LogicOr((simp state l), (simp state r)))

        // Relational.
        | Equal(l, r) -> Some((simp state l) === (simp state r))
        | NotEqual(l, r) -> Some((simp state l) <=> (simp state r))
        | GreaterThan(l, r) -> Some(GreaterThan((simp state l), (simp state r)))
        | GreaterOrEqual(l, r) -> Some(GreaterOrEqual((simp state l), (simp state r)))
        | LessOrEqual(l, r) -> Some(LessOrEqual((simp state l), (simp state r)))
        | LessThan(l, r) -> Some(LessThan((simp state l), (simp state r)))

        // Bitwise.
        | BitwiseLeftShift(Constant l, Constant r) -> Some(Constant (l <<< int(r)))
        | BitwiseRightShift(Constant l, Constant r) -> Some(Constant (l >>> int(r)))
        | BitwiseAnd(Constant l, Constant r) -> Some(Constant (l &&& r))
        | BitwiseOr(Constant l, Constant r) -> Some(Constant (l ||| r))
        | BitwiseXor(Constant l, Constant r) -> Some(Constant (l ^^^ r))
        | BitwiseNegation(Constant e) -> Some(Constant(~~~e))
        | BitwiseLeftShift(l, r) -> Some(BitwiseLeftShift((simp state l), (simp state r)))
        | BitwiseRightShift(l, r) -> Some(BitwiseRightShift((simp state l), (simp state r)))
        | BitwiseAnd(l, r) -> Some((simp state l) &&& (simp state r))
        | BitwiseOr(l, r) -> Some((simp state l) ||| (simp state r))
        | BitwiseXor(l, r) -> Some((simp state l) ^^^ (simp state r))
        | BitwiseNegation e -> Some(~~~(simp state e))

        | _ -> None

and evalFunction state name expr =
    match (state, name) with 
        | StorageUnknown _ -> failwith (sprintf "function %s doesn't exist" name.Key)
        | StorageSymbol v -> match v with
                                | Function f -> f (eval state expr)
                                | _ -> failwith (sprintf "%s is not a function" name.Key)

and evalVariable state name =
    match (state, name) with 
        | StorageUnknown _ -> Variable name 
        | StorageSymbol v -> match v with
                                | Value f -> f
                                | _ -> failwith (sprintf "%s is not a value" name.Key)

let executeStatement state statement =
    let setMem state key value = { state with MemoryMap = Map.add key value state.MemoryMap }
    
    match statement with
        | Single expression ->
            let result = (eval state expression)
            state.Exploration <- Set.empty
            state.ExpressionsCache <- Map.empty
            let newState = setMem state "_" (Value result)
            SingleResult (newState, result)
        
        | Assignment expressions ->
            let applyUpdate (state, results) (name, expr) =
                let result = (eval state expr)
                state.Exploration <- Set.empty
                state.ExpressionsCache <- Map.empty
                let newState = setMem state name.Key (Value result)
                (newState, (name.Key, result)::results)
        
            let newState, assignments = List.fold applyUpdate (state, []) expressions
            AssignmentResult (newState, List.rev assignments)