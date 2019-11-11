module Tests.Scenarios

open Expecto
open Expecto.Flip

open FParsec

open ToyRobot

let parseCommandsFromStrings : string list -> Parser.Command list
    = fun commandStrings ->
    commandStrings
    |> List.map (
        (Parser.getCommand) >>
        (fun parseResult ->
            match parseResult with
            | Failure (f, _, _) -> failtestf "Failed to parse command from test input: %s" f
            | Success (command, _, _) -> command)
    )

let applyCommand robotStates command =
    match robotStates with
    | [] -> robotStates
    | head::_ -> REPL.commandHandler robotStates head command

let tests = testList "Scenarios" [
    testAsync "1" {
        let states =
            [
                "PLACE 0,0 NORTH"
                "MOVE"
            ]
            |> parseCommandsFromStrings
            |> List.fold applyCommand [ RobotState.zero () ]

        let expectedFinalState: RobotState =
            { RobotState.zero () with
                Position = 0, 1
                Orientation = North
            }

        states.Head |> Expect.equal "Final state should equal expected"  expectedFinalState
    }

    testAsync "2" {
        let states =
            [
                "PLACE 0,0 NORTH"
                "LEFT"
            ]
            |> parseCommandsFromStrings
            |> List.fold applyCommand [ RobotState.zero () ]

        let expectedFinalState: RobotState =
            { RobotState.zero () with
                Position = 0, 0
                Orientation = West
            }

        states.Head |> Expect.equal "Final state should equal expected"  expectedFinalState
    }

    testAsync "3" {
        let states =
            [
                "PLACE 1,2 EAST"
                "MOVE"
                "MOVE"
                "LEFT"
                "MOVE"
            ]
            |> parseCommandsFromStrings
            |> List.fold applyCommand [ RobotState.zero () ]

        let expectedFinalState: RobotState =
            { RobotState.zero () with
                Position = 3, 3
                Orientation = North
            }

        states.Head |> Expect.equal "Final state should equal expected"  expectedFinalState
    }
]
