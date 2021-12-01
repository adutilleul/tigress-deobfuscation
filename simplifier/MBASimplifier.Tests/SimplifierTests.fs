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

namespace MBASimplifier.Tests

open System
open NUnit.Framework

open MBASimplifier.Ast
open MBASimplifier.Implementation

[<TestFixture>]
type TestClass () =

    let mutable testState =
        let bitcount (n : int64) =
            let count2 = n - ((n >>> 1) &&& 0x5555555555555555L)
            let count4 = (count2 &&& 0x3333333333333333L) + ((count2 >>> 2) &&& 0x3333333333333333L)
            let count8 = (count4 + (count4 >>> 4)) &&& 0x0f0f0f0f0f0f0f0fL
            (count8 * 0x0101010101010101L) >>> 56 |> int
        { 
            ExpressionsCache = Map.empty
            Exploration = Set.empty
            MemoryMap = Map.ofSeq(
                seq { 
                    yield "inc", Function (fun e -> e + Constant 1)
                    yield "dec", Function (fun e -> e - Constant 1)
                    yield "popcnt", Function (fun e -> match e with Constant c -> Constant(bitcount c) | _ -> failwith "popcnt(int) is excepted")
                }
            );
            Debug = false
        }

    let assertSimplification line (exprs: Expr list) =
        let parseResult = (parseLine testState line)
        match parseResult with
            | ParseError msg -> Assert.True(false);
            | ParseSuccess statement ->
                let result = executeStatement testState statement
                match result with
                | SingleResult (state, expr') -> Assert.AreEqual(List.head exprs, expr')
                | AssignmentResult (state, exprs') -> Assert.AreEqual(exprs, exprs')

    [<Test>]
    member this.TestBitwiseOperatorsSimplification() =
        // AND Tests
        assertSimplification "(((~ a | b) + a) + 1)"                              [BitwiseAnd(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((~ a | b) - ~ a)"                                  [BitwiseAnd(Variable ({Key="a"}), Variable {Key="b"})]

        // XOR Tests
        assertSimplification "((a | b) - (a & b))"                                [BitwiseXor(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a - b) - ((a | ~ b) + (a | ~ b))) - 2)"          [BitwiseXor(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a - b) - ((a | ~ b) << 1)) - 2)"                 [BitwiseXor(Variable ({Key="a"}), Variable {Key="b"})]

        // OR Tests
        assertSimplification "(((a + b) + 1) + ((- a - 1) | (- b - 1)))"          [BitwiseOr(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a & ~ b) + b)"                                    [BitwiseOr(Variable ({Key="a"}), Variable {Key="b"})]

        // NEG Tests
        assertSimplification "(-a - 1)"                                           [BitwiseNegation(Variable ({Key="a"}))]


    [<Test>]
    member this.TestArithmeticOperatorsSimplification() =
        // ADD Tests
        assertSimplification "(((a ^ ~ b) + ((a | b) + (a | b))) + 1)"            [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a ^ ~ b) + ((a | b) << 1)) + 1)"                 [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a | b) + (a | b)) - (a ^ b))"                    [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a | b) << 1) - (a ^ b))"                         [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a - ~ b) - 1)"                                    [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a ^ b) + ((a & b) + (a & b)))"                    [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a ^ b) + ((a & b) << 1))"                         [Add(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a | b) + (a & b))"                                [Add(Variable ({Key="a"}), Variable {Key="b"})]

        // Sub Tests
        assertSimplification "(((a & ~ b) + (a & ~ b)) - (a ^ b))"                [Subtract(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "(((a & ~ b) << 1) - (a ^ b))"                       [Subtract(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a & ~ b) - (~ a & b))"                            [Subtract(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a + ~ b) + 1)"                                    [Subtract(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a ^ b) - ((~ a & b) + (~ a & b)))"                [Subtract(Variable ({Key="a"}), Variable {Key="b"})]
        assertSimplification "((a ^ b) - ((~ a & b) << 1))"                       [Subtract(Variable ({Key="a"}), Variable {Key="b"})]

        // MULT Tests
        assertSimplification "((a & b) * (a | b) + (a & ~ b) * (~ a & b))"        [Multiply(Variable ({Key="a"}), Variable {Key="b"})]

    [<Test>]
    member this.TestComplexMixedOperatorsSimplification() =
        let complexExpr1 = "(((((((a + ~ b) + 1) & ~ c) << 1) - (((a + ~ b) + 1) ^ c)) & ((a & b) * (a | b) + (a & ~ b) * (~ a & b))) * ((((((a + ~ b) + 1) & ~ c) << 1) - (((a + ~ b) + 1) ^ c)) | ((a & b) * (a | b) + (a & ~ b) * (~ a & b))) + ((((((a + ~ b) + 1) & ~ c) << 1) - (((a + ~ b) + 1) ^ c)) & ~ ((a & b) * (a | b) + (a & ~ b) * (~ a & b))) * (~ (((((a + ~ b) + 1) & ~ c) << 1) - (((a + ~ b) + 1) ^ c)) & ((a & b) * (a | b) + (a & ~ b) * (~ a & b))))"
        let complexExpr2 = "((((((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) | 5) + (((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) & 5)) & c) * (((((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) | 5) + (((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) & 5)) | c) + (((((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) | 5) + (((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) & 5)) & ~ c) * (~ ((((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) | 5) + (((((((a ^ ~ b) + ((a | b) + (a | b))) + 1) ^ ~ c) + (((((a ^ ~ b) + ((a | b) + (a | b))) + 1) | c) << 1)) + 1) & 5)) & c))"
        let complexExpr3 = "((((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) | (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) + ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & ~ (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * (~ (- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2))) & ((d | ((~ d | d) - ~ d)) - (d & ((~ d | d) - ~ d)))) * ((((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) | (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) + ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & ~ (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * (~ (- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2))) | ((d | ((~ d | d) - ~ d)) - (d & ((~ d | d) - ~ d)))) + ((((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) | (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) + ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & ~ (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * (~ (- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2))) & ~ ((d | ((~ d | d) - ~ d)) - (d & ((~ d | d) - ~ d)))) * (~ (((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) | (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) + ((- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & ~ (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2)) * (~ (- ((d & e) * (d | e) + (d & ~ e) * (~ d & e)) - 1) & (((d - (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) - ((d | ~ (((d - (((~ e | e) + e) + 1)) - ((d | ~ (((~ e | e) + e) + 1)) + (d | ~ (((~ e | e) + e) + 1)))) - 2)) << 1)) - 2))) & ((d | ((~ d | d) - ~ d)) - (d & ((~ d | d) - ~ d))))"
        let complexExpr4 = "(((~ ((e + ~ e) + 1) | (((e & ~ d) + (e & ~ d)) - (e ^ d))) - ~ ((e + ~ e) + 1)) ^ d) - ((~ ((~ ((e + ~ e) + 1) | (((e & ~ d) + (e & ~ d)) - (e ^ d))) - ~ ((e + ~ e) + 1)) & d) << 1)"
        let complexExpr5 = "((((((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e) + (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e)) - (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) ^ e)) & (((((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ~ ((((c - ~ e) - 1) & ~ d) + d)) - (~ (((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ((((c - ~ e) - 1) & ~ d) + d)))) * ((((((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e) + (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e)) - (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) ^ e)) | (((((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ~ ((((c - ~ e) - 1) & ~ d) + d)) - (~ (((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ((((c - ~ e) - 1) & ~ d) + d)))) + ((((((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e) + (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e)) - (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) ^ e)) & ~ (((((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ~ ((((c - ~ e) - 1) & ~ d) + d)) - (~ (((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ((((c - ~ e) - 1) & ~ d) + d)))) * (~ (((((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e) + (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) & ~ e)) - (((((~ c | e) - ~ c) | d) - (((~ c | e) - ~ c) & d)) ^ e)) & (((((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ~ ((((c - ~ e) - 1) & ~ d) + d)) - (~ (((((c ^ ~ c) + ((c | c) + (c | c))) + 1) & ~ ((d | d) + (d & d))) + ((d | d) + (d & d))) & ((((c - ~ e) - 1) & ~ d) + d))))"
        let complexExpr6 = "(((~ d | d) - ~ d) + ~ ((d + ~ (- d - 1)) + 1)) + 1"

        // b * (a * (a - b - c))
        assertSimplification complexExpr1                                         [Multiply(Variable {Key="b"}, Multiply(Variable {Key="a"}, Subtract (Subtract (Variable {Key="a"}, Variable {Key="b"}), Variable {Key="c"})))]

        // (5 + a + c + b) * c
        assertSimplification complexExpr2                                         [Multiply(Add (Add (Constant 5L, Variable {Key="a"}), Add (Variable {Key="c"}, Variable {Key="b"})), Variable {Key="c"})]

        // ((((~(d*e))*(d^(d^(e&e))))*(d^(d&d)))) = 0
        assertSimplification complexExpr3                                         [Constant 0]

        // ((((e-e)&(e-d))-d)) = -d
        assertSimplification complexExpr4                                         [Negative(Variable {Key="d"})]

        // ((c & e ^ d) - e) * ((2 * c | 2 * d) - (c + e | d))
        assertSimplification complexExpr5                                         [Multiply(Subtract(BitwiseXor (BitwiseAnd (Variable {Key="c"}, Variable {Key="e"}), Variable {Key="d"}), Variable {Key="e"}),Subtract(BitwiseOr(Multiply (Constant 2L, Variable {Key="c"}), Multiply (Constant 2L, Variable {Key="d"})), BitwiseOr (Add (Variable {Key="c"}, Variable {Key="e"}), Variable {Key="d"})))]

        // (((d&d)-(d-(~d))) = ~d
        assertSimplification complexExpr6                                         [BitwiseNegation(Variable {Key="d"})]

