module MBASimplifier.Main

open System
open MBASimplifier.Ast
open MBASimplifier.Implementation
open MBASimplifier.Input

let rec userInput = seq {
    while true do
        printf "> "
        let input = System.Console.ReadLine().Trim() 
        if input.Length > 0 then 
            yield input
}

let runSimplification state line =
    let parseResult = (parseLine state line)
    match parseResult with
    | ParseError msg -> failwith msg
    | ParseSuccess statement ->
        if state.Debug then 
            printfn "[!] Line: %s" line
            printfn "[!] Statement: %A" statement
        let result = executeStatement state statement
        match result with
        | SingleResult (newState, value) ->
            printfn "%A [human: %s] [complexity : %d]" value (value.humanize()) value.Complexity
            newState
        | AssignmentResult (newState, assignments) ->
            for (name, value) in assignments do
                printfn "[+] Res(%s): %A [human: %s] [complexity: %d]" name value (value.humanize()) value.Complexity
            newState

let runFile state filename =
    use reader = new System.IO.StreamReader(System.IO.File.OpenRead filename)
    let fileInput = seq {
        while not reader.EndOfStream do
            let line = reader.ReadLine().Trim()
            // Handle comments
            if line.Length > 0 && not (line.StartsWith "//") then
                yield line 
    }
    fileInput |> Seq.fold runSimplification state

let initialState =
    let bitcount (n : int64) =
        let count2 = n - ((n >>> 1) &&& 0x5555555555555555L)
        let count4 = (count2 &&& 0x3333333333333333L) + ((count2 >>> 2) &&& 0x3333333333333333L)
        let count8 = (count4 + (count4 >>> 4)) &&& 0x0f0f0f0f0f0f0f0fL
        (count8 * 0x0101010101010101L) >>> 56 |> int
    { 
        ExpressionsCache = Map.empty
        MemoryMap = Map.ofSeq (seq { 
            yield "inc", Function (fun e -> e + Constant 1)
            yield "dec", Function (fun e -> e - Constant 1)
            yield "popcnt", Function (fun e -> match e with Constant c -> Constant(bitcount c) | _ -> failwith "popcnt(int) is excepted")

        });
        Debug = true 
    }

let dispatchCommand state command = 
    let none unit = None
    match command with 
        | Exit -> none(exit 0)
        | Help -> none(printfn "TO:DO")
        | ReadInput input -> Some(runSimplification state input)
        | ReadFile filename -> Some (runFile state filename)


let main state input =
    try
        let command = Command.parse input 
        match dispatchCommand state command with
            | Some newState -> newState
            | None          -> state
    with ex ->
        fprintfn stderr "[-]: %s" ex.Message
        state

printfn 
    @"
     __  __ ____    _      ____  _                 _ _  __ _           
    |  \/  | __ )  / \    / ___|(_)_ __ ___  _ __ | (_)/ _(_) ___ _ __ 
    | |\/| |  _ \ / _ \   \___ \| | '_ ` _ \| '_ \| | | |_| |/ _ \ '__|
    | |  | | |_) / ___ \   ___) | | | | | | | |_) | | |  _| |  __/ |   
    |_|  |_|____/_/   \_\ |____/|_|_| |_| |_| .__/|_|_|_| |_|\___|_|   
                                             |_|   
        -> Use help command to get the documentation
    ";
userInput |> Seq.fold main initialState |> ignore