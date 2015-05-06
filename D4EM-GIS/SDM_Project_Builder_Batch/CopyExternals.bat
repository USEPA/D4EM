set Externals=..\..\Externals\x86Debug
set bin=bin\x86Debug
IF NOT EXIST %bin% mkdir %bin%
XCOPY /E /I /Y /EXCLUDE:CopyExclude.txt %Externals%\GDAL %bin%
COPY %Externals%\* %bin%
COPY %Externals%\AquaTerraConsultants\* %bin%
COPY %Externals%\DotSpatial\* %bin%
COPY %Externals%\DotSpatial\Tools\* %bin%
REM COPY %Externals%\WorldWind\* %bin%
IF NOT EXIST %bin%\renderers mkdir %bin%\renderers
COPY %Externals%\AquaTerraConsultants\renderers\* %bin%\renderers
