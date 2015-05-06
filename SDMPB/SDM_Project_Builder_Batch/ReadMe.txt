SDM_Project_Builder_Batch.exe is a command-line program that reads a
specification file named on the command line and generates HSPF and/or
SWAT model inputs based on the given specifications.

The solution file SDM_Project_Builder_Batch.sln is designed to compile
SDM_Project_Builder_Batch.exe into the folder bin\x86Debug. (In the
future, the solution is planned to be expanded to use other folders
for other build types.)

If you do not want to use SDM_Project_Builder_Batch as a command-line
program, use a different solution such as SDMProjectBuilder.sln which
incorporates this code in a larger system that includes a GIS
interface.

Steps to build:

1. Before opening the solution for the first time, run the batch file:
   CopyExternals.bat

   This will copy needed files from the top-level Externals folder to
   where they are needed by this solution. CopyExternals.bat should be
   run again any time a new version of files is placed in Externals.

2. Open SDM_Project_Builder_Batch.sln in Visual Studio 2010

3. Make sure SDM_Project_Builder_Batch is set as the startup project

4. Rebuild Solution

   This will compile all the D4EM libraries needed and copy them and
   the executable into bin\x86Debug.

5. In the Project Properties of SDM_Project_Builder_Batch in the Debug
   section, ensure that Command line arguments contains the path to a
   valid SDMParameters.txt file. An example of this file is included
   in the same folder with the source code.

6. Run the program in Debug mode and log messages will appear in the
   Immediate Window.
