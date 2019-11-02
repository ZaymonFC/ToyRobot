module Tests.Properties

open Expecto
open Expecto.Flip

open ToyRobot

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
