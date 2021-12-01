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

module MBASimplifier.Engine

open FParsec
open MBASimplifier.Ast

let private ws = spaces
let private ws1 = spaces1
let private str_ws1 s = skipString s >>? ws1
let private ws_str_ws s = ws >>. skipString s .>> ws
let private betweenBrackets p = between (ws_str_ws "(") (ws_str_ws ")") p

let private pname = regexL "[a-zA-Z_][a-zA-Z_0-9]*" "name" |>> (fun name -> { Key = name })
let pvariable = pname
let pexpr, private pexprRef = createParserForwardedToRef<Expr, _> ()
let pbracketedExpr = betweenBrackets pexpr <?> "bracketed expression"
let pterm = (pint64 |>> Constant <?> "constant") <|> (pvariable |>> Variable <?> "variable")

let pfunction =
    let functionArguments = (ws1 >>? pterm) <|> pbracketedExpr
    attempt (pname .>>. functionArguments) |>> FunctionCall <?> "function call"
let passignment = 
    let binding = pipe2 (pvariable .>> ws_str_ws "=") pexpr (fun name expr -> (name, expr))
    let manyBindings = sepBy1 binding (ws_str_ws ",")
    str_ws1 "let" >>. manyBindings <?> "variable binding"
let pnegationExpr =
    let negSymbols = (many1Chars (pchar '-')) <|> (many1Chars (pchar '~')) <|> (many1Chars (pchar '!'))
    let expression = (spaces >>? pterm) <|> (spaces >>? pbracketedExpr)
    let rec build_expr s e = match (s) with 
                                | '-'::[] -> Negative e
                                | '~'::[] -> BitwiseNegation e 
                                | '!'::[] -> LogicNegation e
                                | '-'::q -> build_expr q (Negative e)
                                | '~'::q -> build_expr q (BitwiseNegation e)
                                | '!'::q -> build_expr q (LogicNegation e)
                                | [] -> failwith "pnegationExpr() - Empty buffer"
                                | c::_ -> failwith (sprintf "pnegationExpr() - Unexcepted negation character %c" c)
    pipe2 negSymbols expression (fun s e -> build_expr (s |> Seq.toList) e) <?> "negate expression"

// https://en.cppreference.com/w/c/language/operator_precedence
do pexprRef :=
    let opp = new OperatorPrecedenceParser<Expr, unit, unit> ()
    let expr = opp.ExpressionParser
    opp.TermParser <- (pfunction <|> pterm <|> pnegationExpr <|> pbracketedExpr) .>> ws

    opp.AddOperator(InfixOperator("*", ws, 3, Associativity.Left, fun x y -> Multiply (x, y)))
    opp.AddOperator(InfixOperator("/", ws, 3, Associativity.Left, fun x y -> Divide (x, y)))
    opp.AddOperator(InfixOperator("%", ws, 3, Associativity.Left, fun x y -> Modulo (x, y)))

    opp.AddOperator(InfixOperator("-", ws, 4, Associativity.Left, fun x y -> Subtract (x, y)))
    opp.AddOperator(InfixOperator("+", ws, 4, Associativity.Left, fun x y -> Add (x, y)))

    opp.AddOperator(InfixOperator(">>", ws, 5, Associativity.Left, fun x y -> BitwiseRightShift (x, y)))
    opp.AddOperator(InfixOperator("<<", ws, 5, Associativity.Left, fun x y -> BitwiseLeftShift (x, y)))

    opp.AddOperator(InfixOperator(">", ws, 6, Associativity.Left, fun x y -> GreaterThan (x, y)))
    opp.AddOperator(InfixOperator("<", ws, 6, Associativity.Left, fun x y -> LessThan (x, y)))
    opp.AddOperator(InfixOperator(">=", ws, 6, Associativity.Left, fun x y -> GreaterOrEqual (x, y)))
    opp.AddOperator(InfixOperator("<=", ws, 6, Associativity.Left, fun x y -> LessOrEqual (x, y)))

    opp.AddOperator(InfixOperator("==", ws, 7, Associativity.Left, fun x y -> Equal (x, y)))
    opp.AddOperator(InfixOperator("!=", ws, 7, Associativity.Left, fun x y -> NotEqual (x, y)))

    opp.AddOperator(InfixOperator("&", ws, 8, Associativity.Left, fun x y -> BitwiseAnd (x, y)))
    opp.AddOperator(InfixOperator("^", ws, 9, Associativity.Left, fun x y -> BitwiseXor (x, y)))
    opp.AddOperator(InfixOperator("|", ws, 10, Associativity.Left, fun x y -> BitwiseOr (x, y)))
    opp.AddOperator(InfixOperator("&&", ws, 11, Associativity.Left, fun x y -> LogicAnd (x, y)))
    opp.AddOperator(InfixOperator("||", ws, 12, Associativity.Left, fun x y -> LogicOr (x, y)))

    expr

let pcommand_eof = ((passignment |>> Assignment) <|> (pexpr |>> Single)) .>> eof