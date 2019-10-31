﻿module ToyRobot

open System

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
        // Invariant: Can't place robot out of bounds
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

module Parser =
    open FParsec

    type Command =
        | Place of (int * int) * Direction
        | Move
        | Left
        | Right
        | Report

    let pvalueLessCommands =
        [ Move; Left; Right; Report ]
        |> List.map (fun command -> command |> string |> pstringCI >>% command) |> List.reduce (<|>)

    let pdirections =
        [ North; South; East; West ] |> List.map (fun d -> d |> string |> pstringCI >>% d) |> List.reduce (<|>)

    let pcoordinates = pint32 .>> spaces .>> pchar ',' .>> spaces .>>. pint32
    let pplace = "PLACE" |> pstringCI .>> spaces >>. pcoordinates .>> spaces .>>. pdirections |>> Place

    let result = run pplace "PLACE 2, 3 NORTH"
    printf "%A" result

    let pcommand = choice [pplace; pvalueLessCommands]

    let getCommand = run pcommand

[<EntryPoint>]
let main argv =
    printfn "Hello Welcome to the ToyRobot!"

    while true do
        let input = Console.ReadLine()
        printfn "\n%A" (Parser.getCommand input)

    0 // return an integer exit code
