module MBASimplifier.Implementation

open MBASimplifier.Ast
open MBASimplifier.Engine
open FParsec.CharParsers

type Stored = 
    | Value of Expr
    | Function of (Expr -> Expr)

type State = { MemoryMap : Map<string, Stored>;
               mutable ExpressionsCache : Map<Expr, Expr>
               Debug: bool }

type ParseResult =
    | ParseSuccess of Statement
    | ParseError of string

type CommandResult = 
    | SingleResult of State * Expr
    | AssignmentResult of State * (string * Expr) list

exception CycleDetected of Expr
exception UnexceptedSymbol of string
exception SymbolResult of Stored

let parseLine state line =
    let parserResult = runParserOnString pcommand_eof () "Input" line
    match parserResult with
        | Success (expr, state, pos) -> ParseSuccess expr
        | Failure (msg, err, state)  -> ParseError msg

let private getStored state name =
    match (Map.tryFind name.Key state.MemoryMap) with
        | Some s -> raise (SymbolResult(s)) 
        | None _ -> raise (UnexceptedSymbol(name.Key)) 

let rec eval (state: State) (expr: Expr) : Expr =
    let expr' = simp state (expr) in
      if expr' = expr then 
          expr 
      else 
          eval state expr' 
// ((~(a) | ~(b)) + (b + -(~(a))))
// (-~a + b + ~(a & b))
// ((a & ~ b) + b)
// 
and simp (state: State) (expr: Expr) : Expr =  
    let is_predicate_true pred = pred > 0L
    let is_predicate_false pred = pred <= 0L
    let is_predicate_equiv pred1 pred2 = is_predicate_true pred1 = is_predicate_true pred2
    let simp' = simp state
    let permute list =
      let rec inserts e = function
        | [] -> [[e]]
        | x::xs as list -> (e::list)::[for xs' in inserts e xs -> x::xs']                
      List.fold (fun accum x -> List.collect (inserts x) accum) [[]] list
    let repeat n fn = List.init n (fun _ -> fn) |> List.reduce (>>)
    let expr' = 
        match (Map.tryFind expr state.ExpressionsCache) with
            | Some e -> e
            | None _ -> 
                match expr with
                    | Constant c -> Constant c
                    | Variable n -> evalVariable state n
                    | FunctionCall (name, e) -> evalFunction state name e

                    (*
                    ===================================
                         /!\ ARITHMETIC /!\
                    ===================================
                    *)

                    // Constants
                    | Add (Constant l, Constant r) -> Constant (l + r)
                    | Subtract(Constant l, Constant r) -> Constant (l - r)
                    | Multiply(Constant l, Constant r) -> Constant(l * r)
                    | Divide(Constant l, Constant r) -> Constant (l / r)
                    | Modulo(Constant l, Constant r) -> Constant (l % r)
                    | Negative(Constant e) -> Constant(-e)

                    // Involution 
                    | Negative(Negative(e1)) -> simp' e1
                    // a % a = 0
                    | Modulo(e1, e2) when e1 = e2 -> Constant 0

                    // a + a
                    | Add(e1, e2) when e1 = e2 -> Constant 2 * simp' e1
                    // ca + cb = 
                    | Add(Multiply(Constant c1, e1), Multiply(Constant c2, e2)) when e1 = e2 -> Constant (c1 + c2) * simp' e1

                    // a + (a + b)

                    | Add(e1, Add(e2, e3)) -> 
                        permute [e1; e2; e3] |> List.map (fun e -> Add(simp' e[0], simp' (Add(e[1], e[2])))) |> List.minBy (fun e -> e.Complexity)

                    | Add(Add(e1, e2), e3) -> 
                        permute [e1; e2; e3] |> List.map (fun e -> Add(simp' e[0], simp' (Add(e[1], e[2])))) |> List.minBy (fun e -> e.Complexity)

                    | Multiply(e1, Multiply(e2, e3)) -> 
                        permute [e1; e2; e3] |> List.map (fun e -> Multiply(simp' e[0], simp' (Multiply(e[1], e[2])))) |> List.minBy (fun e -> e.Complexity)

                    | Multiply(Multiply(e1, e2), e3) -> 
                        permute [e1; e2; e3] |> List.map (fun e -> Multiply(simp' e[0], simp' (Multiply(e[1], e[2])))) |> List.minBy (fun e -> e.Complexity)

                    | Add(Multiply(Constant c1, e1), e2) when e1 = e2 -> Constant (c1+1L) * simp' e1
                    | Add(e2, Multiply(Constant c1, e1)) when e1 = e2 -> Constant (c1+1L) * simp' e1

                    (* Division rules *)
                    // a / a = 1
                    | Divide(Variable v1, Variable v2) when v1 = v2 -> Constant 1

                    // Identity and absorbing elements
                    | Subtract(Constant 0L, e1) -> Negative(simp' e1)
                    | Negative(Constant(0L)) -> Constant(0)
                    | Add(e, Constant(0L)) -> simp' e
                    | Subtract(e, Constant(0L)) -> simp' e
                    | Add(Constant(0L), e) -> simp' e
                    | Multiply(e, Constant(1L)) -> simp' e
                    | Multiply(Constant(1L), e) -> simp' e
                    | Multiply(e, Constant(0L)) -> Constant(0)
                    | Multiply(Constant(0L), e) -> Constant(0)
                    | Divide(Constant(0L), e) -> Constant(0)
                    | Divide(e, Constant(1L)) -> simp' e

                    (*
                    ===================================
                         /!\ Boolean /!\
                    ===================================
                    *)  

                    | LogicNegation(Constant p) -> if is_predicate_true p then Constant 0 else Constant 1
                    | LogicOr(Constant l, Constant r) -> if (is_predicate_true l || is_predicate_true r) then Constant 1 else Constant 0
                    | LogicAnd(Constant l, Constant r) -> if (is_predicate_true l && is_predicate_true r) then Constant 1 else Constant 0

                    // Identity and absorbing elements
                    | LogicOr(e1, Constant p) when is_predicate_false p -> simp' e1
                    | LogicOr(Constant p, e1) when is_predicate_false p -> simp' e1

                    | LogicAnd(e1, Constant p) when is_predicate_true p -> simp' e1
                    | LogicAnd(Constant p, e1) when is_predicate_true p -> simp' e1

                    | LogicOr(e1, Constant p) when is_predicate_true p -> Constant 1
                    | LogicOr(Constant p, e1) when is_predicate_true p -> Constant 1

                    | LogicAnd(e1, Constant p) when is_predicate_false p -> Constant 0
                    | LogicAnd(Constant p, e1) when is_predicate_false p -> Constant 0

                    // Involution 
                    | LogicNegation(LogicNegation(e1)) -> simp' e1 

                    (*
                    ===================================
                         /!\ Bitwise /!\
                    ===================================
                    *)

                    // Identity constants.
                    // A|A = A
                    | BitwiseOr(l, r) when l = r -> simp' l
                    // A|0 = A
                    | BitwiseOr(l, Constant 0L) -> simp' l
                    // A&A = A
                    | BitwiseAnd(l, r) when l = r -> simp' l
                    // A^0 = A
                    | BitwiseXor(l, Constant 0L) -> simp' l
                    // A&-1 = A
                    | BitwiseAnd(l, Constant -1L) -> simp' l
                    // A<<0 = A
                    | BitwiseLeftShift(l, Constant 0L) -> simp' l
                    // A>>0 = A
                    | BitwiseRightShift(l, Constant 0L) -> simp' l

                    // Constant result.
                    // A&0 = 0
                    | BitwiseAnd(l, Constant 0L) -> Constant 0
                    // A^A = A
                    | BitwiseXor(l, r) when l = r -> Constant 0
                    // A&~A = 0
                    | BitwiseAnd(e1, BitwiseNegation(e2)) when e1 = e2 -> Constant 0
                    // A|-1 = -1
                    | BitwiseOr(e1, Constant -1L) -> Constant -1
                    // A+(~A) = -1
                    | Add(e1, BitwiseNegation e2) when e1 = e2 -> Constant -1
                    // A^(~A) = -1
                    | BitwiseXor(e1, BitwiseNegation e2) when e1 = e2 -> Constant -1
                    // A|(~A) = -1
                    | BitwiseOr(e1, BitwiseNegation e2) when e1 = e2 -> Constant -1

                    // ~(A-1) = -A 
                    | BitwiseNegation(Subtract(e1, Constant 1L)) -> Negative(simp' e1)
                    // ~(~(A)) = A
                    | BitwiseNegation(BitwiseNegation(e)) -> simp' e

                    // NOT conversion.
                    // A^-1 = ~A
                    | BitwiseXor(e1, Constant -1L) -> BitwiseNegation(simp' e1)

                    // XOR conversion.
                    // (A|B)&(~(A&B)) = A^B
                    | BitwiseAnd(BitwiseOr(e1, e2), BitwiseNegation(BitwiseAnd(e3, e4))) 
                        when e1 = e3 && e2 = e4 -> BitwiseXor(simp' e1, simp' e2)
                    // (A|B)&((~A)|(~B)) = A^B
                    | BitwiseAnd(BitwiseOr(e1, e2), BitwiseOr(BitwiseNegation(e3), BitwiseNegation(e4))) 
                         when e1 = e3 && e2 = e4 -> BitwiseXor(simp' e1, simp' e2)
                    // (A&(~B))|((~A)&B) = A^B
                    | BitwiseOr(BitwiseAnd(e1, BitwiseNegation(e2)), BitwiseAnd(BitwiseNegation(e3), e4)) 
                             when e1 = e3 && e2 = e4 -> BitwiseXor(simp' e1, simp' e2)
                    // (~(A|B))|(A&B) = ~(A^B)
                    | BitwiseOr(BitwiseNegation(BitwiseOr(e1, e2)), BitwiseAnd(e3, e4)) 
                        when e1 = e3 && e2 = e4 -> BitwiseNegation(BitwiseXor(simp' e1, simp' e2))
                    // ((~A)&(~B))|(A&B = ~(A^B)
                    | BitwiseOr(BitwiseAnd(BitwiseNegation(e1), BitwiseNegation(e2)), BitwiseAnd(e3, e4)) 
                            when e1 = e3 && e2 = e4 -> BitwiseNegation(BitwiseXor(simp' e1, simp' e2))

                    // Simplify AND OR NOT XOR.
                    // A&(A|B) = A
                    | BitwiseAnd(e1, BitwiseOr(e2, e3)) when e1 = e2 -> simp' e1 
                    // A|(A&B) = A
                    | BitwiseOr(e1, BitwiseAnd(e2, e3)) when e1 = e2 -> simp' e1 
                    // A^(A&B) = A&~B
                    | BitwiseXor(e1, BitwiseAnd(e2, e3)) when e1 = e2 -> BitwiseAnd(simp' e1 , BitwiseNegation(simp' e3))
                    // A^(A|B) = B&~A
                    | BitwiseXor(e1, BitwiseOr(e2, e3)) when e1 = e2 -> BitwiseAnd(simp' e3 , BitwiseNegation(simp' e1))
                    

                    // // (-~a + b + ~(a & b))
                    | Add(Negative (BitwiseNegation (e1)), BitwiseNegation (BitwiseAnd (e3, e4))) when e1=e3 -> BitwiseAnd(simp' e1, BitwiseNegation(simp' e4))
                    | BitwiseOr(BitwiseNegation(e1), BitwiseNegation(e2)) -> BitwiseNegation(BitwiseAnd(e1, e2))
                    | (Add (BitwiseAnd (e1, BitwiseNegation (e2)), e3)) when e2 = e3 -> BitwiseOr(simp' e1, simp' e2)
                    | (Add (e3, BitwiseAnd (e1, BitwiseNegation (e2)))) when e2 = e3 -> BitwiseOr(simp' e1, simp' e2)

                    // -a - 1
                    | Subtract(Negative(e), Constant 1L) -> BitwiseNegation(simp' e)
                    | Subtract(a, Constant c1) ->  simp' a |> repeat (int(c1)) (fun e -> BitwiseNegation(Negative(e)))
                    | Add(a, Constant c1) -> simp' a |> repeat (int(c1)) (fun e -> Negative(BitwiseNegation(e)))
                    | (Subtract(BitwiseOr (BitwiseNegation (a), b), BitwiseNegation (c))) when a = c -> BitwiseAnd ((simp' a), simp' b)
                    | (Add(a, Negative(BitwiseNegation (BitwiseOr (BitwiseNegation (b), c))))) when a = b -> BitwiseAnd(simp' a, simp' c)
                    (*
                    ===================================
                         /!\ Relational /!\
                    ===================================
                    *)

                    // Invert comparison.
                    | BitwiseNegation(GreaterThan(l, r)) -> LessOrEqual(simp' l, simp' r)
                    | BitwiseNegation(GreaterOrEqual(l, r)) -> LessThan(simp' l, simp' r)
                    | BitwiseNegation(Equal(l, r)) -> NotEqual(simp' l, simp' r)
                    | BitwiseNegation(NotEqual(l, r)) -> Equal(simp' l, simp' r)
                    | BitwiseNegation(LessOrEqual(l, r)) -> GreaterThan(simp' l, simp' r)
                    | BitwiseNegation(LessThan(l, r)) -> GreaterOrEqual(simp' l, simp' r)

                    | Equal(Variable l, Variable r) when l = r -> Constant(1)
                    | Equal(Constant l, Constant r) -> if l = r then Constant 1 else Constant 0
                    | NotEqual(Constant l, Constant r) -> if l <> r then Constant 1 else Constant 0
                    | NotEqual(Variable l, Variable r) when l = r -> Constant(0)
                    | GreaterThan(Constant l, Constant r) -> if l > r then Constant 1 else Constant 0
                    | GreaterOrEqual(Constant l, Constant r) -> if l >= r then Constant 1 else Constant 0
                    | LessOrEqual(Constant l, Constant r) -> if l <= r then Constant 1 else Constant 0
                    | LessThan(Constant l, Constant r) -> if l < r then Constant 1 else Constant 0

                    (*
                    ===================================
                         /!\ Base /!\
                    ===================================
                    *)   
        
        
                    | Add (l, r) -> (simp' l) + (simp' r)
                    | Subtract (l, r) -> (simp' l) - (simp' r)
                    | Multiply (l, r) -> (simp' l) * (simp' r)
                    | Divide (l, r) -> (simp' l) / (simp' r)
                    | Modulo (l, r) -> (simp' l) % (simp' r)
                    | Negative e -> Negative(simp' e)
       
                    | LogicNegation(e) -> LogicNegation((simp' e))
                    | LogicAnd(l, r) -> LogicAnd((simp' l), (simp' r))
                    | LogicOr(l, r) -> LogicOr((simp' l), (simp' r))
                
                    | Equal(l, r) -> (simp' l) === (simp' r)
                    | NotEqual(l, r) -> (simp' l) <=> (simp' r)
                    | GreaterThan(l, r) -> GreaterThan((simp' l), (simp' r))
                    | GreaterOrEqual(l, r) -> GreaterOrEqual((simp' l), (simp' r))
                    | LessOrEqual(l, r) -> LessOrEqual((simp' l), (simp' r))
                    | LessThan(l, r) -> LessThan((simp' l), (simp' r))

                    | BitwiseLeftShift(Constant l, Constant r) -> Constant (l <<< int(r))
                    | BitwiseRightShift(Constant l, Constant r) -> Constant (l >>> int(r))
                    | BitwiseAnd(Constant l, Constant r) -> Constant (l &&& r)
                    | BitwiseOr(Constant l, Constant r) -> Constant (l ||| r)
                    | BitwiseXor(Constant l, Constant r) -> Constant (l ^^^ r)
                    | BitwiseNegation(Constant e) -> Constant(~~~e)

                    | BitwiseLeftShift(l, r) -> BitwiseLeftShift((simp' l), (simp' r))
                    | BitwiseRightShift(l, r) -> BitwiseRightShift((simp' l), (simp' r))
                    | BitwiseAnd(l, r) -> (simp' l) &&& (simp' r)
                    | BitwiseOr(l, r) -> (simp' l) ||| (simp' r)
                    | BitwiseXor(l, r) -> (simp' l) ^^^ (simp' r)
                    | BitwiseNegation e -> ~~~(simp' e)
    in state.ExpressionsCache <- Map.add expr expr' state.ExpressionsCache; expr'

and evalFunction state name expr =
    try 
     getStored state name
    with 
     | UnexceptedSymbol _ -> failwith (sprintf "function %s doesn't exists" name.Key)
     | SymbolResult v     -> match v with
                                | Function f -> f (eval state expr)
                                | _ -> failwith (sprintf "%s is not a function" name.Key)

and evalVariable state name =
   try 
    getStored state name
   with 
    | UnexceptedSymbol _ -> Variable name 
    | SymbolResult v     -> match v with
                            | Value f -> f
                            | _ -> failwith (sprintf "%s is not a value" name.Key)

let executeStatement state statement =
    let setMem state key value = { state with MemoryMap = Map.add key value state.MemoryMap }
    
    match statement with
    | Single expression ->
        let result = (eval state expression)
        let newState = setMem state "_" (Value result)
        SingleResult (newState, result)
        
    | Assignment expressions ->
        let applyUpdate (state, results) (name, expr) =
            let result = (eval state expr)
            let newState = setMem state name.Key (Value result)
            (newState, (name.Key, result)::results)
        
        let newState, assignments = List.fold applyUpdate (state, []) expressions
        AssignmentResult (newState, List.rev assignments)