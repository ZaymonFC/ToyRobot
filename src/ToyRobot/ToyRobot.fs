module ToyRobot

type TurningDirection = | Left | Right

type Direction = | North | East | South | West
module Direction =
    let turn : Direction * TurningDirection -> Direction = function
        | North, Right | South, Left  -> East
        | North, Left  | South, Right -> West
        | East,  Right | West,  Left  -> South
        | East,  Left  | West,  Right -> North

type Distance = int

type Position = int * int
module Position =
    let MoveInDirection : Direction -> Distance -> Position -> Position =
        fun direction distance (x, y) ->
        match direction with
        | North -> x, y + distance
        | East  -> x + distance, y
        | South -> x, y - distance
        | West  -> x - distance, y

type Bounds = { Height: int; Width: int }

type RobotState =
    {
        Constraint: Bounds
        Position: Position
        Orientation: Direction
    }

module Helpers =
    let (|WithinBound|_|) b x = if x >= 0 && x < b then Some () else None

    let boundsCheck bounds position =
        match position with
        | WithinBound bounds.Width, WithinBound bounds.Height -> Some position
        | _ -> None

module Action =
    type Place  = RobotState -> Position -> Direction -> RobotState
    type Move   = RobotState -> RobotState
    type Turn   = RobotState -> TurningDirection -> RobotState
    type Report = RobotState -> unit

    let place : Place = fun robotState position direction ->
        // Check State Invariant - Can't place outside bounds
        match Helpers.boundsCheck robotState.Constraint position with
        | Some newPosition ->
            { robotState with
                Orientation = direction
                Position = newPosition
            }
        | _ -> robotState

    let move: Move = fun robotState ->
        robotState.Position
        |> Position.MoveInDirection robotState.Orientation 1
        |> Helpers.boundsCheck robotState.Constraint
        |> function | None -> printf "Robot is at the edge."; robotState
                    | Some newPosition -> { robotState with Position = newPosition }

    let turn: Turn = fun robotState turningDirection ->
        { robotState with Orientation = Direction.turn (robotState.Orientation, turningDirection) }

    let report: Report = fun robotState ->
        let x,y = robotState.Position
        printfn "REPORT: X Position: %d Y Position: %d Orientation %A" x y robotState.Orientation

[<EntryPoint>]
let main argv =
    // TODO: Drive the Robot
    printfn "Hello World from F#!"
    0 // return an integer exit code
