# CESIL

From [Wikipedia](https://en.wikipedia.org/wiki/CESIL):

CESIL, or **C**omputer **E**ducation in **S**chools **I**nstruction **L**anguage, is a programming language designed to introduce pupils in British secondary schools to elementary computer programming. It is a simple language containing a total of fourteen instructions.

## Background
Computer Education in Schools (CES) was a project developed in the late 1960s and early 1970s by International Computers Limited (ICL). CESIL was developed by ICL as part of the CES project, and introduced in 1974. In those days, very few if any schools had computers, so pupils would write programs on coding sheets, which would then be transferred to punched cards or paper tape. Typically, this would be sent to run on a mainframe computer, with the output from a line printer being returned later.

## Structure
Because CESIL was not designed as an interactive language, there is no facility to input data in real time. Instead, numeric data is included as a separate section at the end of the program.

The fundamental principle of CESIL is the use of a single accumulator, which handles mathematical operations. Numeric values are stored in variables, which in CESIL are referred to as store locations. CESIL only works with integers, and results from DIVIDE operations are rounded if necessary. There is no facility for structured data such as arrays, nor for string handling, though string constants can be output by means of the PRINT instruction.

Jumps and loops can be conditional or non-conditional, and transfer operation of the program to a line with a specific label, which is identified in the first column of a coding sheet. The instruction or operation is stated in the second column, and the operand in the third column. On some coding sheets, comments and the text of the PRINT instruction would be written in a fourth column.

## Instructions
Instructions, or operations, are written in upper case and may have a single operand, which can be a store location, constant integer value or line label. Store locations and line labels are alphanumeric, up to six characters, and begin with a letter.[12] Numeric integer constants must be signed + or −, with zero being denoted as +0.[a]

### Input and output
**IN** – reads the next value from the data, and stores it in the accumulator. The error message `*** PROGRAM REQUIRES MORE DATA ***` is printed if the program tries to read beyond the end of the data provided.
**OUT** – prints the current value of the accumulator. No carriage return is printed.
**PRINT** *"text in quotes"* – prints the given text. No carriage return is printed.
**LINE** – prints a carriage return, thus starting a new line.

### Memory storage
**LOAD** *location* or **LOAD** *constant* – copies the value of the given location or constant to the accumulator.
**STORE** *location* – copies the contents of the accumulator to the given location.

### Mathematical instructions
**ADD** *location* or **ADD** *constant* – adds the value of the given location or constant to the accumulator.
**SUBTRACT** *location* or **SUBTRACT** *constant* – subtracts the value of the given location or constant from the accumulator.
**MULTIPLY** *location* or **MULTIPLY** *constant* – multiplies the accumulator by the value of the given location or constant.
**DIVIDE** *location* or **DIVIDE** *constant* – divides the accumulator by the value of the given location or constant. The result is rounded down if the result is positive, and up if the result is negative. A `*** DIVISION BY ZERO ***` error message is printed if the divisor is zero. In each case, the result of the operation is stored in the accumulator, replacing the previous value.

### Program control
**JUMP** *label* – unconditionally transfers control to location labelled.
**JINEG** *label* (Jump If NEGative) – transfers control to location labelled if the accumulator contains a negative value.
**JIZERO** *label* (Jump If ZERO) – transfers control to location labelled if the accumulator contains zero.
**HALT** – terminates the program.

### Other symbols
Three special symbols are used in CESIL at the beginnings of lines.

`%` is used to mark the end of the program and the start of data.  
`*` is used to mark the end of the data.  
`(` is used at the start of a line to indicate a comment.  

## CESIL programming tools
An emulator for CESIL, designed to run on Windows and called Visual CESIL, is available as freeware.
An interpreter for CESIL, designed to run on the Android platform and called Wyrm CESIL, is available as free to install.

## Example
The following totals the integers in the runtime data section until it encounters a negative value and prints the total.
```
        LOAD    +0
LOOP    STORE   TOTAL
        IN
        JINEG   DONE
        ADD     TOTAL
        JUMP    LOOP

DONE    PRINT   "The total is: "
        LOAD    TOTAL
        OUT
        LINE
        HALT

%
1
2
3
-1
*
```
The output of the above program would be:
```
The total is: 6
```