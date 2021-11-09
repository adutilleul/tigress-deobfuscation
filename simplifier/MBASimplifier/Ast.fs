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


module MBASimplifier.Ast

[<StructuredFormatDisplay("{StructuredFormatDisplay}")>]
type Name = { Key : string }
    with 
        member private t.StructuredFormatDisplay = t.Key

and Expr =
    | Constant              of int64
    | Variable              of Name

    | FunctionCall          of Name * Expr

    // Arithmetic
    | Add                   of Expr * Expr
    | Multiply              of Expr * Expr
    | Subtract              of Expr * Expr
    | Divide                of Expr * Expr
    | Modulo                of Expr * Expr
    | Negative              of Expr

    // Bitwise
    | BitwiseLeftShift      of Expr * Expr
    | BitwiseRightShift     of Expr * Expr
    | BitwiseAnd            of Expr * Expr
    | BitwiseXor            of Expr * Expr
    | BitwiseOr             of Expr * Expr
    | BitwiseNegation       of Expr 

    // Relational
    | Equal                 of Expr * Expr
    | NotEqual              of Expr * Expr
    | GreaterOrEqual        of Expr * Expr
    | LessOrEqual           of Expr * Expr
    | GreaterThan           of Expr * Expr
    | LessThan              of Expr * Expr

    // Logic 
    | LogicAnd              of Expr * Expr 
    | LogicOr               of Expr * Expr
    | LogicNegation         of Expr

      member this.humanize() = 
          match this with 
            | Constant a -> sprintf "%d" a
            | Variable e -> e.Key
            | FunctionCall(n, e) -> sprintf "%s %s" n.Key (e.humanize())

            | LogicOr(e1, e2) -> sprintf "(%s || %s)" (e1.humanize()) (e2.humanize())
            | LogicAnd(e1, e2) -> sprintf "(%s && %s)" (e1.humanize()) (e2.humanize())
            | LogicNegation(e) -> sprintf "!(%s)" (e.humanize())

            | Subtract(e1, e2) -> sprintf "(%s - %s)" (e1.humanize()) (e2.humanize())
            | Add(e1, e2) -> sprintf "(%s + %s)" (e1.humanize()) (e2.humanize())
            | Multiply(e1, e2) -> sprintf "(%s * %s)" (e1.humanize()) (e2.humanize())
            | Divide(e1, e2) -> sprintf "(%s / %s)" (e1.humanize()) (e2.humanize())
            | Modulo(e1, e2) -> sprintf "(%s %% %s)" (e1.humanize()) (e2.humanize())
            | Negative(e) -> sprintf "-(%s)" (e.humanize())

            | BitwiseNegation(e) -> sprintf "~(%s)" (e.humanize())
            | BitwiseAnd(e1, e2) -> sprintf "(%s & %s)" (e1.humanize()) (e2.humanize())
            | BitwiseOr(e1, e2) -> sprintf "(%s | %s)" (e1.humanize()) (e2.humanize())
            | BitwiseXor(e1, e2) -> sprintf "(%s ^ %s)" (e1.humanize()) (e2.humanize())
            | BitwiseLeftShift(e1, e2) -> sprintf "(%s << %s)" (e1.humanize()) (e2.humanize())
            | BitwiseRightShift(e1, e2) -> sprintf "(%s >> %s)" (e1.humanize()) (e2.humanize())
            | Equal(e1, e2) -> sprintf "(%s == %s)" (e1.humanize()) (e2.humanize())
            | NotEqual(e1, e2) -> sprintf "(%s != %s)" (e1.humanize()) (e2.humanize())

            | GreaterOrEqual(e1, e2) -> sprintf "(%s >= %s)" (e1.humanize()) (e2.humanize())
            | GreaterThan(e1, e2) -> sprintf "(%s > %s)" (e1.humanize()) (e2.humanize())
            | LessOrEqual(e1, e2) -> sprintf "(%s <= %s)" (e1.humanize()) (e2.humanize())
            | LessThan(e1, e2) -> sprintf "(%s < %s)" (e1.humanize()) (e2.humanize())

      (* Helpers *)
      static member (+) (e1,e2) = Add(e1,e2)
      static member (*) (e1,e2) = Multiply(e1,e2)
      static member (/) (e1,e2) = Divide(e1,e2)
      static member (-) (e1,e2) = Subtract(e1,e2)
      static member (%) (e1,e2) = Modulo(e1,e2)
      static member (&&&) (e1,e2) = BitwiseAnd(e1,e2)
      static member (|||) (e1,e2) = BitwiseOr(e1,e2)
      static member (^^^) (e1,e2) = BitwiseXor(e1,e2)
      static member (~~~) (e1) = BitwiseNegation(e1)
      static member (===) (e1, e2) = Equal(e1, e2)
      static member (<=>) (e1, e2) = NotEqual(e1, e2)

      member this.Complexity with get() : int =
          let rec count expr = match expr with 
                                  | Constant _ -> 1
                                  | Variable _ -> 2
                                  | FunctionCall(n,e) -> 20 + count e
                                  | Negative e | BitwiseNegation e | LogicNegation e -> 4 + count e
                                  | Add(e1, e2) | Multiply(e1, e2) | Subtract (e1, e2) | Divide(e1, e2) | Modulo(e1, e2)
                                  | BitwiseLeftShift(e1, e2) | BitwiseRightShift(e1, e2) | BitwiseAnd(e1, e2) | BitwiseXor(e1, e2) 
                                  | BitwiseOr(e1, e2) | Equal(e1, e2) | NotEqual(e1, e2) | GreaterOrEqual(e1, e2) | LessOrEqual(e1, e2) 
                                  | GreaterThan(e1, e2) | LessThan(e1, e2) | LogicAnd(e1, e2) | LogicAnd(e1, e2) | LogicOr(e1, e2) -> 8 + count e1 + count e2
          count this
and Statement =
| Single of Expr
| Assignment of VariableAssignment list

and VariableAssignment = Name * Expr