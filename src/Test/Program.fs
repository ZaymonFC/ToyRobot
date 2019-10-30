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

[<Tests>]
let tests =
    testList "ToyRobot" [
        helperTests
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
