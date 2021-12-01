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

module MBASimplifier.UserInterface

open Spectre.Console
open RadLine

let printHeader() = 
    FigletText "MBA Simplifier"
    |> AlignableExtensions.Centered
    |> (fun p -> FigletTextExtensions.Color(p, Color.LightCyan1))
    |> AnsiConsole.Console.Write
    
let printHelp() = 
    Table()
    |> (fun p -> TableExtensions.AddColumn(p, "Name"))
    |> (fun p -> TableExtensions.AddColumn(p, "What it does"))
    |> (fun p -> TableExtensions.AddRow(p, "clear", "Resets the console to its default display"))
    |> (fun p -> TableExtensions.AddRow(p, "read {path}", "Executes all instructions in a file"))
    |> (fun p -> TableExtensions.AddRow(p, "quit", "Exit the interactive mode of the console"))
    |> (fun p -> TableExtensions.AddRow(p, "help", "Display this message"))
    |> ExpandableExtensions.Expand
    |> HasTableBorderExtensions.RoundedBorder
    |> (fun p -> HasBorderExtensions.BorderColor(p, Color(byte 0, byte 128, byte 0)))
    |> AnsiConsole.Console.Write

let colorGreen = Color(byte 0, byte 128, byte 0)
let colorRed = Color(byte 196, byte 0, byte 0)
let colorYellow = Color(byte 255, byte 255, byte 0)

let writeText (ansiConsole: IAnsiConsole) (input: string) (header: string) (color: Color)=
    printf "\n"
    input
    |> Markup.Escape
    |> Panel
    |> (fun p -> PanelExtensions.Header(p, header))
    |> ExpandableExtensions.Expand
    |> HasBoxBorderExtensions.RoundedBorder
    |> (fun p -> HasBorderExtensions.BorderColor(p, color))
    |> ansiConsole.Write

let writeLine (ansiConsole: IAnsiConsole) (input: string) =
    writeText ansiConsole input "Ok" colorGreen

let writeLineError (ansiConsole: IAnsiConsole) (input: string) =
    writeText ansiConsole input "Error" colorRed
    
let writeLineInfo (ansiConsole: IAnsiConsole) (input: string) =
    writeText ansiConsole input "Info" colorYellow

let private getWordHighlighter () =
    let rec addWord words style (highlighter : WordHighlighter) =
        match words with
        | hd::tl -> 
            highlighter.AddWord(hd, style) |> ignore
            addWord tl style highlighter
        | _ -> ()

    let highlighter = WordHighlighter ()

    let keywords = ["let"]
    let operators = ["("; ")"]

    addWord keywords (Style Color.LightSlateBlue) highlighter
    addWord operators (Style Color.Pink1) highlighter

    highlighter

let getLineEditor (ansiConsole : IAnsiConsole) =
    let lineEditor = LineEditor (ansiConsole, null)
    lineEditor.Highlighter <- getWordHighlighter ()
    lineEditor

let readLine (lineEditor : LineEditor) =
    lineEditor.ReadLine(System.Threading.CancellationToken.None).Result

let writeLineAnsi text = writeLine AnsiConsole.Console text
let writeLineErrorAnsi text = writeLineError AnsiConsole.Console text
let writeLineInfoAnsi text = writeLineInfo AnsiConsole.Console text