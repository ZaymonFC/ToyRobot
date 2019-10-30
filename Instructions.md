Toy Robot Simulator
--------------------
Create a library that can read in commands of the following form:

PLACE X,Y,DIRECTION
MOVE
LEFT
RIGHT
REPORT

. The library allows for a simulation of a toy robot moving on a 5 x 5 square tabletop.
. There are no obstructions on the table surface.
. The robot is free to roam around the surface of the table, but must be prevented from falling to destruction. Any movement that would result in this must be prevented, however further valid movement commands must still be allowed.
. PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.
. (0,0) can be considered as the SOUTH WEST corner and (4,4) as the NORTH EAST corner.
. The first valid command to the robot is a PLACE command. After that, any sequence of commands may be issued, in any order, including another PLACE command. The library should discard all commands in the sequence until a valid PLACE command has been executed.
. The PLACE command should be discarded if it places the robot outside of the table surface.
. MOVE will move the toy robot one unit forward in the direction it is currently facing.
. LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.
. REPORT will announce the X,Y and orientation of the robot.
. A robot that is not on the table can choose to ignore the MOVE, LEFT, RIGHT and REPORT commands.
. The library should discard all invalid commands and parameters.

Example Input and Output:
a)
PLACE 0,0,NORTH
MOVE
REPORT
Output: 0,1,NORTH

b)
PLACE 0,0,NORTH
LEFT
REPORT
Output: 0,0,WEST

c)
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
Output: 3,3,NORTH

- Use your preferred language, platform and IDE to implement this solution.
- Your solution should be clean and easy to read, maintain and execute.
- There must be a way to supply the library with input data.
- Writing an interface (command prompt or otherwise) is not mandatory.
- You should provide sufficient evidence that your solution is complete by, as a minimum, indicating that it works correctly against the supplied test data.
- You should provide build scripts or instructions to build and verify the solution.
- The code should be original and you may not use any external libraries or open source code to solve this problem, but you may use external libraries or tools for building or testing purposes.
