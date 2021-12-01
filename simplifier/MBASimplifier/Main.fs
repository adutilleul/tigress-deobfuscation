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

module MBASimplifier.Main

open System 
open Spectre.Console

open MBASimplifier.Ast
open MBASimplifier.Implementation
open MBASimplifier.Input
open MBASimplifier.UserInterface

let lineEditor = getLineEditor AnsiConsole.Console

let initialState =
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

let runSimplification remind state line =
    let parseResult = (parseLine state line)
    match parseResult with
    | ParseError msg -> failwith msg
    | ParseSuccess statement ->
        if remind then 
            match statement with
                | Single expression ->
                    writeLineInfoAnsi (expression.humanize())
                | Assignment expressions ->
                    for (_, expr) in expressions do 
                        writeLineInfoAnsi (expr.humanize())
        let result = executeStatement state statement
        match result with
        | SingleResult (newState, value) ->
            writeLineAnsi (value.humanize())
            writeLineInfoAnsi (sprintf "%A" value)
            newState
        | AssignmentResult (newState, assignments) ->
            for (_, value) in assignments do
               writeLineAnsi (value.humanize())
            newState

let runFile state filename =
    use reader = new System.IO.StreamReader(System.IO.File.OpenRead filename)
    let fileInput = seq {
        while not reader.EndOfStream do
            let line = reader.ReadLine().Trim()
            // Don't process comments
            if line.Length > 0 && not (line.StartsWith "//") then
                yield line 
    }
    fileInput |> Seq.fold (runSimplification true) state

let rec userInput = seq {
    while true do
        printf "> "
        let input = (readLine lineEditor).Trim()
        if input.Length > 0 then 
            yield input
}

let dispatchCommand state command = 
    let none unit = None
    match command with 
        | Exit -> none(exit 0)
        | Help -> none(printHelp())
        | ClearConsole -> none (try System.Console.Clear(); printHeader() with ex -> ())
        | ReadInput input -> Some(runSimplification false state input)
        | ReadFile filename -> Some (runFile state filename)

let userMain state input =
    try
        let command = Command.parse input 
        match dispatchCommand state command with
            | Some newState -> newState
            | None -> state
    with ex ->
        writeLineErrorAnsi ex.Message
        state

[<EntryPoint>]
let main argv =
    Console.Title <- "MBA Simplifier - Interactive"
    printHeader()
    userInput |> Seq.fold userMain initialState |> ignore
    0
