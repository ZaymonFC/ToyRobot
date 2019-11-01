open Expecto
open Expecto.Flip

open ToyRobot

let helperTests = testList "Helper Tests" [
    testList "Bounds Checker" [
        testAsync "should report within bounds" {
            let bounds : Bounds = { Height = 10; Width = 10;}
            let validPosition : Position = 5, 5

            Helpers.boundsCheck bounds validPosition
            |> Expect.isSome "Position should be valid to place"
        }

        testAsync "should return `None` when out of bounds" {
            let bounds : Bounds = { Height = 10; Width = 10;}
            let invalidPositions : Position list = [
                -1, 5; 10, 10
                2, 11; 4, -1
            ]

            for position in invalidPositions do
                Helpers.boundsCheck bounds position
                |> Expect.isNone "Position should be invalid to place"
        }
    ]
]

let invariants = testList "Properties and Invariants" [
    testProperty "commands shouldn't result in invalid state or exception" <|
        fun (commands: Parser.Command list) ->
            let commands = commands |> List.filter (fun c -> c <> Quit)

            let initialRobotState = [RobotState.zero ()]

            let folder robotStates command =
                match robotStates with
                | [] -> robotStates
                | head::_ -> REPL.commandHandler robotStates head command

            let finalState = commands |> List.fold folder initialRobotState |> List.head
            let bounds = finalState.Constraint

            let x, y = finalState.Position

            x < bounds.Width |> Expect.isTrue ""
            x >= 0 |> Expect.isTrue ""

            y < bounds.Height |> Expect.isTrue ""
            y >= 0 |> Expect.isTrue ""
]

[<Tests>]
let tests =
    testList "ToyRobot" [
        helperTests
        invariants
    ]

[<EntryPoint>]
let main argv =

    printfn "Running tests!"

    let config =
        { defaultConfig with
            verbosity = Logging.LogLevel.Verbose
            colour = Logging.Colour256
        }

    runTestsWithArgs config argv tests
