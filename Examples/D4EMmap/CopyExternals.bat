set bin=bin\x86Debug
IF NOT EXIST %bin% mkdir %bin%
set ExternalData=..\..\Externals\data
IF NOT EXIST bin mkdir bin
IF NOT EXIST bin\Bin mkdir bin\Bin
IF NOT EXIST bin\Bin\County mkdir bin\Bin\County
IF NOT EXIST bin\Bin\WDNRsymbols mkdir bin\Bin\WDNRsymbols
COPY %ExternalData%\national\cnty.* bin\Bin\County
COPY %ExternalData%\miscellaneous\* bin\Bin
COPY %ExternalData%\miscellaneous\WDNRsymbols\* bin\Bin\WDNRsymbols
COPY HUCshapefile(Albers)\huc250d3.* bin\Bin
COPY HUCshapefile(Albers)\huc250d3.* bin\x86Debug
COPY %ExternalData%\national\cnty.* bin\x86Debug

set Externals=..\..\Externals\x86Debug
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

IF NOT EXIST %bin%\Plugins mkdir %bin%\Plugins
COPY %Externals%\GDAL\* %bin%\Plugins
IF NOT EXIST %bin%\Plugins\gdal-data mkdir %bin%\Plugins\gdal-data
IF NOT EXIST %bin%\Plugins\gdalplugins mkdir %bin%\Plugins\gdalplugins
COPY %Externals%\GDAL\gdal-data\* %bin%\Plugins\gdal-data
COPY %Externals%\GDAL\gdalplugins\* %bin%\Plugins\gdalplugins

set appext="bin\x86Debug\Application Extensions"
IF NOT EXIST %appext% mkdir %appext%
set AppExt = %ExternalData%\miscellaneous\*
COPY "..\..\Externals\x86Debug\DotSpatial\Application Extensions\*" "bin\x86Debug\Application Extensions"