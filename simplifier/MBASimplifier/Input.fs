module MBASimplifier.Input

let private whitespace = [| ' '; '\t' |]

type Command =
    | Exit
    | Help
    | ReadFile of string
    | ReadInput of string

    static member parse (commandString:string) =
        let input = ReadInput commandString
        let tokens = commandString.Split(whitespace, 2, System.StringSplitOptions.RemoveEmptyEntries) |> List.ofArray
        match tokens with
            // No arguments
            | [command] ->
                match command with
                | "quit"   | "exit"  -> Exit
                | "help"   | "help"  -> Help
                | _ -> input
        
            // One argument
            | [command; arg] ->
                match command with
                | "read"  -> ReadFile arg
                | _ -> input
        
            | _ -> input

