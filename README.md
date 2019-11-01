<h1 align="center">ToyRobot</h1>

<p align="center">
<a href="https://github.com/ZaymonFC/ToyRobot/actions?query=workflow%3A%22.NET+Core%22"><img alt=".NET Core Build" src="https://github.com/ZaymonFC/ToyRobot/workflows/.NET%20Core/badge.svg"></a>
<img alt="Made with F#" src="https://img.shields.io/github/languages/top/ZaymonFC/ToyRobot?color=%23b845fc">
<img alt="License - MIT" src="https://img.shields.io/github/license/ZaymonFC/ToyRobot?color=%23FF957D">
</p><br>


A small repository for playing with:
- Domain Driven Development.
- Software Testing.
- Parser Combinators.

### Build and Run
OSX: `./start.sh`

Windows: `dotnet run --project src/ToyRobot/ToyRobot.fsproj`

### Running Tests
OSX (Watch for changes): `./test.sh`

Windows: `dotnet test` or for `expecto` output: `dotnet watch --project ./src/Test/Test.fsproj run`
### Command Reference
All commands are case-insensitive.
- `PLACE X,Y,DIRECTION`
- `MOVE`
- `LEFT`
- `RIGHT`
- `REPORT`
- `QUIT`

Command Parsing is implemented with `FParsec`.

This allows for reporting of more advanced parse errors and suggestions:
```sh
> Place 1,
"Error in Ln: 1 Col: 10
Place 1,
         ^
Note: The error occurred at the end of the input stream.
Expecting: integer number (32-bit, signed)
"
```

### Invariants
1. The robot cannot be placed outside of the specified bounds (defaults to 5x5).
2. The robot will stop reacting to input once it has reached the edge of the table.
