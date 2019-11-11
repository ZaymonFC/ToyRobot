module Tests.Properties

open Expecto
open Expecto.Flip

open ToyRobot
open ToyRobot.Parser

let invariants = testList "Invariants" [
    testProperty "commands shouldn't result in invalid state" <|
        fun (commands: Parser.Command list) ->
            let commands = commands |> List.filter (fun c -> c <> Quit)

            let initialRobotState = [RobotState.zero ()]

            let folder robotStates command =
                match robotStates with
                | [] -> robotStates
                | head::_ -> REPL.commandHandler robotStates head command

            commands
            |> List.fold folder initialRobotState
            |> List.iter (fun robot ->
                let bounds = robot.Constraint

                let x, y = robot.Position

                x < bounds.Width |> Expect.isTrue ""
                x >= 0 |> Expect.isTrue ""

                y < bounds.Height |> Expect.isTrue ""
                y >= 0 |> Expect.isTrue ""
            )
]
