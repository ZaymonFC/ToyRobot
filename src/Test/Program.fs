open Expecto

// Compose all tests into an annotated test list.
[<Tests>]
let tests =
    testList "ToyRobot" [
        Tests.Scenarios.tests
        Tests.Helpers.helperTests
        Tests.Properties.invariants
    ]
    |> Test.shuffle

[<EntryPoint>]
let main argv =

    printfn "Running tests!"

    let config =
        { defaultConfig with
            verbosity = Logging.LogLevel.Verbose
            colour = Logging.Colour256
            fsCheckMaxTests = 100
        }

    runTestsWithArgs config argv tests
