REM Copy all needed files from Externals to where they will work for running this program

set Externals=..\..\Externals\x86Debug

IF NOT EXIST bin mkdir bin
set bin=bin\x86Debug

del /S /F /Q bin
IF NOT EXIST %bin% mkdir %bin%

COPY %Externals%\DotSpatial\* %bin%
IF EXIST %Externals%\DotSpatial\Tools COPY %Externals%\DotSpatial\Tools\* %bin%
XCOPY /E /I /Y /EXCLUDE:CopyExclude.txt %Externals%\DotSpatial\"Application Extensions" %bin%\"Application Extensions"

IF NOT EXIST %bin%\Taudem5Exe mkdir %bin%\Taudem5Exe
IF EXIST %Externals%\Taudem5Exe XCOPY %Externals%\Taudem5Exe %bin%\Taudem5Exe

set Plugins=%bin%\Plugins
IF NOT EXIST %Plugins% mkdir %Plugins%
XCOPY /E /I /Y /EXCLUDE:CopyExclude.txt %Externals%\GDAL %Plugins%\GDAL

IF NOT EXIST %Plugins%\SDMProjectBuilder mkdir %Plugins%\SDMProjectBuilder
COPY %Externals%\*.* %Plugins%\SDMProjectBuilder
COPY %Externals%\AquaTerraConsultants\* %Plugins%\SDMProjectBuilder
XCOPY /E /I /Y %Externals%\AquaTerraConsultants\renderers %Plugins%\SDMProjectBuilder\renderers
COPY %Externals%\WorldWind\* %Plugins%\SDMProjectBuilder
