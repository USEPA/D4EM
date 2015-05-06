set ExternalData=..\..\Externals\data
IF NOT EXIST bin mkdir bin
IF NOT EXIST bin\Bin mkdir bin\Bin
IF NOT EXIST bin\Bin\County mkdir bin\Bin\County
IF NOT EXIST bin\Bin\WDNRsymbols mkdir bin\Bin\WDNRsymbols
COPY %ExternalData%\national\cnty.* bin\Bin\County
COPY %ExternalData%\miscellaneous\* bin\Bin
COPY %ExternalData%\miscellaneous\WDNRsymbols\* bin\Bin\WDNRsymbols
COPY %ExternalData%\national\huc250d3.* bin\Bin

set Externals=..\..\Externals\x86Debug
set bin=bin\x86Debug
IF NOT EXIST %bin% mkdir %bin%
XCOPY /E /I /Y /EXCLUDE:CopyExclude.txt %Externals%\GDAL %bin%
COPY %Externals%\* %bin%
COPY %Externals%\AquaTerraConsultants\* %bin%
COPY %Externals%\DotSpatial\* %bin%
COPY %Externals%\DotSpatial\Tools\* %bin%
COPY %Externals%\WorldWind\* %bin%
IF NOT EXIST %bin%\renderers mkdir %bin%\renderers
COPY %Externals%\AquaTerraConsultants\renderers\* %bin%\renderers
COPY %Externals%\GDAL\* %bin%
IF NOT EXIST %bin%\gdal-data mkdir %bin%\gdal-data
IF NOT EXIST %bin%\gdalplugins mkdir %bin%\gdalplugins
COPY %Externals%\GDAL\gdal-data\* %bin%\gdal-data
COPY %Externals%\GDAL\gdalplugins\* %bin%\gdalplugins