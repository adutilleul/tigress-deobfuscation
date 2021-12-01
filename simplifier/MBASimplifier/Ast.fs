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
          let format_binary_op (e1: Expr) (e2: Expr) op = (e1.stringize(e1.Priority < this.Priority)) + " " + op + " " + (e2.stringize(e1.Priority < this.Priority))
          let format_unary_op (e: Expr) op =  op + (e.stringize(e.Priority < this.Priority))
          match this with 
            | Constant c -> sprintf "%d" c
            | Variable e -> e.Key
            | FunctionCall(n, e) -> sprintf "%s %s" n.Key (e.stringize(e.Priority < this.Priority))

            | LogicOr(e1, e2) -> format_binary_op e1 e2 "||"
            | LogicAnd(e1, e2) -> format_binary_op e1 e2 "&&"
            | LogicNegation(e) -> format_unary_op e "!"

            | Subtract(e1, e2) -> format_binary_op e1 e2 "-"
            | Add(e1, e2) -> format_binary_op e1 e2 "+"
            | Multiply(e1, e2) -> format_binary_op e1 e2 "*"
            | Divide(e1, e2) -> format_binary_op e1 e2 "/"
            | Modulo(e1, e2) -> format_binary_op e1 e2 "%"
            | Negative(e) -> format_unary_op e "-"

            | BitwiseAnd(e1, e2) -> format_binary_op e1 e2 "&"
            | BitwiseOr(e1, e2) -> format_binary_op e1 e2 "|"
            | BitwiseXor(e1, e2) -> format_binary_op e1 e2 "^"
            | BitwiseLeftShift(e1, e2) -> format_binary_op e1 e2 "<<"
            | BitwiseRightShift(e1, e2) -> format_binary_op e1 e2 ">>"
            | Equal(e1, e2) -> format_binary_op e1 e2 "=="
            | NotEqual(e1, e2) -> format_binary_op e1 e2 "!="
            | BitwiseNegation(e) -> format_unary_op e "~"

            | GreaterOrEqual(e1, e2) -> format_binary_op e1 e2 ">="
            | GreaterThan(e1, e2) -> format_binary_op e1 e2 ">"
            | LessOrEqual(e1, e2) -> format_binary_op e1 e2 "<="
            | LessThan(e1, e2) -> format_binary_op e1 e2 "<"

      member this.stringize(parenthesesRequired) = 
        let exprStr = (this.humanize()) in 
        match this with 
            | Constant _ | Variable _ -> exprStr 
            | _ -> if parenthesesRequired then sprintf "(%s)" exprStr else exprStr   

      member this.Priority with get() : int  = 
        match this with 
            | Constant _ | Variable _ -> 1
            | LogicOr(e1, e2) -> 2
            | LogicAnd(e1, e2) -> 3
            | BitwiseOr(e1, e2) -> 4 
            | BitwiseXor(e1, e2) -> 5
            | BitwiseAnd(e1, e2) -> 6 
            | NotEqual(e1, e2) -> 7 
            | Equal(e1, e2) -> 7
            | LessOrEqual(_, _) | LessThan(_, _) | GreaterOrEqual(_, _) | GreaterThan(_, _) -> 8
            | BitwiseLeftShift(_, _)| BitwiseRightShift(_, _) -> 9
            | Add(_, _)  | Subtract(_, _) -> 10
            | Modulo(_, _)| Divide(_, _) | Multiply(_, _) -> 11
            | BitwiseNegation(_) | LogicNegation(_) | Negative(_) -> 12
            | FunctionCall(n, e) -> 13

      (* Helpers *)
      static member (~-) (e1) = Negative(e1)
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
                                  | Negative e | BitwiseNegation e | LogicNegation e -> 4 + count e
                                  | Add(e1, e2) | Multiply(e1, e2) | Subtract (e1, e2) | Divide(e1, e2) | Modulo(e1, e2)
                                  | BitwiseLeftShift(e1, e2) | BitwiseRightShift(e1, e2) | BitwiseAnd(e1, e2) | BitwiseXor(e1, e2) 
                                  | BitwiseOr(e1, e2) | Equal(e1, e2) | NotEqual(e1, e2) | GreaterOrEqual(e1, e2) | LessOrEqual(e1, e2) 
                                  | GreaterThan(e1, e2) | LessThan(e1, e2) | LogicAnd(e1, e2) | LogicAnd(e1, e2) | LogicOr(e1, e2) -> 8 + count e1 + count e2
                                  | FunctionCall(_,e) -> 20 + count e
          count this
and Statement =
    | Single of Expr
    | Assignment of VariableAssignment list

and VariableAssignment = Name * Expr