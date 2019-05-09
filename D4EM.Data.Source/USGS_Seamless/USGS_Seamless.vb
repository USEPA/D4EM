Imports atcUtility
Imports MapWinUtility
Imports D4EM.Geo
Imports D4EM.Data.LayerSpecification
Imports System
Imports System.Net
Imports System.IO
'Imports System.MarshalByRefObject
Imports System.Drawing.Imaging
Imports System.Drawing.Bitmap
Imports System.Drawing
Imports System.Drawing.Image
Imports Newtonsoft.Json.Linq

Public Class USGS_Seamless

    'Public Class LayerSpecifications
    '    Public Class NLCD1992
    '        Public Shared LandCover As New LayerSpecification(Name:="NLCD 1992 Land Cover", Tag:="NLCD1992", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '    End Class
    '    Public Class NLCD2001
    '        Public Shared LandCover As New LayerSpecification(Name:="NLCD 2001 Land Cover", Tag:="NLCD2001.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '        Public Shared Canopy As New LayerSpecification(Name:="NLCD 2001 Canopy", Tag:="NLCD2001.Canopy", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '        Public Shared Impervious As New LayerSpecification(Name:="NLCD 2001 Impervious", Tag:="NLCD2001.Impervious", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '    End Class
    '    Public Class NLCD2006
    '        Public Shared LandCover As New LayerSpecification(Name:="NLCD 2006 Land Cover", Tag:="NLCD2006.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '        Public Shared Impervious As New LayerSpecification(Name:="NLCD 2006 Impervious", Tag:="NLCD2006.Impervious", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '    End Class
    '    Public Class NLCD2011
    '        Public Shared LandCover As New LayerSpecification(Name:="NLCD 2011 Land Cover", Tag:="NLCD2011.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
    '    End Class
    '    Public Class NED
    '        Public Shared OneArcSecond As New LayerSpecification(Name:="NED One Arc Second", Tag:="NED.OneArcSecond", Role:=Roles.Elevation, Source:=GetType(USGS_Seamless))
    '        Public Shared OneThirdArcSecond As New LayerSpecification(Name:="NED One-Third Arc Second", Tag:="NED.OneThirdArcSecond", Role:=Roles.Elevation, Source:=GetType(USGS_Seamless))
    '    End Class
    '    'TODO: others from case statement in BuildURL
    'End Class

    'Base Server Text
    'Dim NLCD_USGS_Server_Text As String = "https://landfire.cr.usgs.gov/arcgis/rest/services/NLCD/USGS_EDC_LandCover_NLCD/ImageServer/exportImage?"
    'Dim NLCD_USGS_Server_Text As String = "https://edcintl.cr.usgs.gov/geoserver/mrlc_download/wms?SERVICE=WMS&request=GetMap"
    Shared NLCD_USGS_Server_Text As String = "https://www.mrlc.gov/geoserver/mrlc_download/wms?SERVICE=WMS&request=GetMap"



    Public Class LayerSpecifications
        Public Class NLCD1992
            Public Shared LandCover As New LayerSpecification(FilePattern:="NLCD_1992_landcover.tif", Name:="NLCD 1992 Land Cover", Tag:="NLCD1992", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
        End Class
        Public Class NLCD2001
            Public Shared LandCover As New LayerSpecification(FilePattern:="NLCD_2001_landcover.tif", Name:="NLCD 2001 Land Cover", Tag:="NLCD2001.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
            Public Shared Canopy As New LayerSpecification(FilePattern:="NLCD_2001_canopy.tif", Name:="NLCD 2001 Canopy", Tag:="NLCD2001.Canopy", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
            Public Shared Impervious As New LayerSpecification(FilePattern:="NLCD_2001_impervious.tif", Name:="NLCD 2001 Impervious", Tag:="NLCD2001.Impervious", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
        End Class
        Public Class NLCD2006
            Public Shared LandCover As New LayerSpecification(FilePattern:="NLCD_2006_landcover.tif", Name:="NLCD 2006 Land Cover", Tag:="NLCD2006.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
            Public Shared Impervious As New LayerSpecification(FilePattern:="NLCD_2006_impervious.tif", Name:="NLCD 2006 Impervious", Tag:="NLCD2006.Impervious", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
        End Class
        Public Class NLCD2011
            Public Shared LandCover As New LayerSpecification(FilePattern:="NLCD_2011_landcover.tif", Name:="NLCD 2011 Land Cover", Tag:="NLCD2011.LandCover", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
            Public Shared Impervious As New LayerSpecification(FilePattern:="NLCD_2011_impervious.tif", Name:="NLCD 2011 Impervious", Tag:="NLCD2011.Impervious", Role:=Roles.LandUse, Source:=GetType(USGS_Seamless))
        End Class
        Public Class NED
            Public Shared OneArcSecond As New LayerSpecification(FilePattern:="NED_2001_OneArcSecond.tif", Name:="NED One Arc Second", Tag:="NED.OneArcSecond", Role:=Roles.Elevation, Source:=GetType(USGS_Seamless))
            Public Shared OneThirdArcSecond As New LayerSpecification(FilePattern:="NED_2001_OneThirdArcSecond.tif", Name:="NED One-Third Arc Second", Tag:="NED.OneThirdArcSecond", Role:=Roles.Elevation, Source:=GetType(USGS_Seamless))
        End Class
        'TODO: others from case statement in BuildURL
    End Class

    'Tolerance for cached data
    Private Shared CacheWithinDegreesOutside As Double = 1
    Private Shared CacheWithinDegreesInside As Double = 0.001

    'Public Function test(ByVal aProject As Project,
    '                                         ByVal aSaveFolder As String)
    '    'Dim val As Integer
    '    'val = 0
    '    'Dim rest As USGS_Seamless_Soap.ExportMapImageRequest
    '    'rest = New USGS_Seamless_Soap.ExportMapImageRequest
    '    'Dim ext As New USGS_Seamless_Soap.EnvelopeN
    '    'ext.XMax = -7087933.9106248
    '    'ext.YMax = 6960328.8817100283
    '    'ext.XMin = -14497453.9106248
    '    'ext.YMin = 2480608.8817100283
    '    'rest.MapDescription.MapArea.Extent = ext
    '    'rest.MapDescription.SpatialReference.WKID = 3857
    '    'rest.ImageDescription.ImageType.ImageReturnType = "tif"
    '    'Return (val)
    'End Function

    ''' <summary>
    ''' Download a layer from USGS Seamless Server
    ''' </summary>
    ''' <param name="aProject">Project folder, region, cache settings</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder or full path of folder to save in</param>
    ''' <param name="aDataType">Type of data to get. Valid values are in USGS_Seamless.LayerSpecifications</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function Execute(ByVal aProject As Project,
                                             ByVal aSaveFolder As String,
                                             ByVal aDataType As LayerSpecification,
                                             ByVal Optional addLayer As Boolean = False) As String
        Dim lError As String = ""
        Dim lResult As String = ""
        Dim lFinalLayerName As String = ""
        Try
            'Dim req As USGS_Seamless_Soap.GetServerInfoRequest
            'Dim res As USGS_Seamless_Soap.GetServerInfoResponse
            'Dim msp As New USGS_Seamless_Soap.MapServerPortClient("http://raster.nationalmap.gov/arcgis/rest/services/LandCover/USGS_EROS_LandCover_NLCD/MapServer")
            'Dim msi As USGS_Seamless_Soap.MapServerInfo
            'msi = msp.GetServerInfo(msp.GetDefaultMapName())
            'Console.WriteLine("Got info" + msi.DefaultMapDescription.MapArea.Extent.ToString())

            Dim lSaveIn As String = aProject.ProjectFolder
            'If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)

            Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "NLCD")
            'StartOver:
            lError = ""
            lResult = ""
            Dim lBaseFilename As String

            '        If aProject.Region Is Nothing Then
            '            lError = "Region not found"
            '        Else

            'Base Server Text
            'Dim NLCD_USGS_Server_Text As String = "https://landfire.cr.usgs.gov/arcgis/rest/services/NLCD/USGS_EDC_LandCover_NLCD/ImageServer/exportImage?"
            'Dim NLCD_USGS_Server_Text As String = "https://edcintl.cr.usgs.gov/geoserver/mrlc_download/wms?SERVICE=WMS&request=GetMap"
            Dim NLCD_USGS_Server_Text As String = "https://www.mrlc.gov/geoserver/mrlc_download/wms?SERVICE=WMS&request=GetMap"

            'Bounding box of area to retrieve
            Dim lNorth As Double = 0
            Dim lSouth As Double = 0
            Dim lWest As Double = 0
            Dim lEast As Double = 0
            Dim TempDouble As Double = 0.0

            'Convert project region bounds into projection needed for retrieval
            'aProject.Region.GetBounds(lNorth, lSouth, lWest, lEast, Globals.WebMercatorProjection)
            aProject.Region.GetBounds(lNorth, lSouth, lWest, lEast, Globals.GeographicProjection)

            If (lNorth <= lSouth) Then ' Or (lWest >= lEast) Then 'Skip test for east vs. west in case it ever crosses zero.
                Throw New ApplicationException("South is not less than North value")
            End If

            'Dim responseformat As String = "f=json"    '"f=image", "f=json"
            Dim responseformat As String = ""
            Dim BoundingBox As String = "&bbox=" + lWest.ToString() + ", " + lSouth.ToString() + ", " + lEast.ToString() + ", " + lNorth.ToString()

            'Determine the output size(rows, columns) of the image based on the area requested and assumed resolution & units of NLCD (i.e., 30 meter cells)
            'Reproject to output projection to figure out size
            aProject.Region.GetBounds(lNorth, lSouth, lWest, lEast, Globals.AlbersProjection)
            Dim XSize As Integer = (lEast - lWest) / 30              'assumes coordinate units are meters and that resolution of NLCD is 30 meters
            Dim YSize As Integer = (lNorth - lSouth) / 30

            'limiting size of output .tiff
            If XSize > 50000 Then XSize = 50000
            If XSize < 1 Then XSize = 10 ' min size
            If YSize > 50000 Then YSize = 50000
            If YSize < 1 Then YSize = 10

            'size of result in pixels (e.g., 300x400 = size=300,400; default is 400x400)
            'Dim imagesize As String = "&size=" + XSize.ToString + "," + YSize.ToString
            Dim imagesize As String = "&width=" + XSize.ToString + "&height=" + YSize.ToString

            'Dim BB_SR As String = "&bboxSR=3857"                  'Spatial Reference WKIDs: 3857 = Web Mercator, 4326 = WGS 84, 102003 = Contiguous USA albers, 7301 = USA Contiguous Albers Equal Area Conic USGS version
            Dim BB_SR As String = ""

            'Dim ImageSR As String = "&imageSR=4326"               'NOTE: The server didn't return a correct image (all zero values) when trying to use USA albers as the input coords and output projection
            Dim ImageSR As String = "&srs=EPSG:4326"

            'Dim lNativeProjection As DotSpatial.Projections.ProjectionInfo = D4EM.Data.Globals.AlbersProjection
            Dim lNativeProjection As DotSpatial.Projections.ProjectionInfo = DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984 ' Globals.GeographicProjection
            'Dim imageformat As String = "&format=tiff"            'tif = "tiff" 
            Dim imageformat As String = "&format=image/geotiff"
            'Dim pixelType As String = "&pixelType=U8"             'data has unsigned 8-bit values'
            Dim pixelType As String = "&pixelType=F64"              'data has unsigned 8-bit values'
            'Dim noData As String = "noData="                     'value considered as nodata values and rendered as transparent (Note:we are leaving this blank)
            Dim image_compression As String = "&compression=L277" 'can use No compression if desired by replacing "L277" with "None"

            Dim lDataType As String = aDataType.Tag
            Dim lYear As String = "2011"
            Dim lUSarea As String = "_conus"  '  can be blank, conus, hi, ak, or pr (for 2001 NLCD)
            Dim lLayerString As String = ""

            Select Case aDataType
                Case LayerSpecifications.NLCD1992.LandCover
                    lDataType = "landcover"
                    lYear = "1992"
                    lBaseFilename = "NLCD_LandCover_1992"
                    lLayerString = "&layers=mrlc_nlcd_1992_landcover_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2001.LandCover
                    lDataType = "landcover"
                    lYear = "2001"
                    lBaseFilename = "NLCD_" & lDataType & "_2001"
                    lLayerString = "&layers=mrlc_nlcd_2001_landcover_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2001.Canopy
                    lDataType = "canopy"
                    lYear = "2001"
                    lBaseFilename = "NLCD_" & lDataType & "_2001"
                    lLayerString = "&layers=mrlc_nlcd2011_usfs_conus_canopy_cartographic"
                Case LayerSpecifications.NLCD2001.Impervious
                    lDataType = "impervious"
                    lYear = "2001"
                    lBaseFilename = "NLCD_" & lDataType & "_2001"
                    lLayerString = "&layers=mrlc_nlcd_2001_impervious_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2006.LandCover
                    lDataType = "landcover"
                    lYear = "2006"
                    lBaseFilename = "NLCD_" & lDataType & "_2006"
                    lLayerString = "&layers=mrlc_nlcd_2006_landcover_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2006.Impervious
                    lDataType = "impervious"
                    lYear = "2006"
                    lBaseFilename = "NLCD_" & lDataType & "_2006"
                    lLayerString = "&layers=mrlc_nlcd_2006_impervious_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2011.LandCover
                    lDataType = "landcover"
                    lYear = "2011"
                    lUSarea = ""
                    lBaseFilename = "NLCD_" & lDataType & "_2011"
                    lLayerString = "&layers=mrlc_nlcd_2011_landcover_2011_edition_2014_10_10"
                Case LayerSpecifications.NLCD2011.Impervious
                    lDataType = "impervious"
                    lYear = "2011"
                    lUSarea = ""
                    lBaseFilename = "NLCD_" & lDataType & "_2011"
                    lLayerString = "&layers=mrlc_nlcd_2011_impervious_2011_edition_2014_10_10"

                    'Case LayerSpecifications.NED.OneArcSecond
                    '   lBaseFilename = "NED_1ArcSecond"
                    '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
                    'Case LayerSpecifications.NED.OneThirdArcSecond
                    '   lBaseFilename = "NED_ThirdArcSecond"
                    '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
                Case Else
                    lBaseFilename = lDataType
                    lLayerString = "&layers=" + lDataType
            End Select
            Dim mosaicRule As String = "&mosaicRule={%22where%22%3A%22Name%3D%27" + lDataType + "_" + lYear + lUSarea + "%27%22}"    ' {"where":"Name='landcover_2011'"}

            ''Create NLCD request URL string
            'Dim NLCD_GET_URL As String = NLCD_USGS_Server_Text + responseformat + BoundingBox + imagesize + BB_SR + ImageSR + imageformat + pixelType + image_compression + mosaicRule
            Dim NLCD_GET_URL As String = NLCD_USGS_Server_Text + lLayerString + responseformat + BoundingBox + imagesize + BB_SR + ImageSR + imageformat + "&version=1.1.1"
            'Console.WriteLine("{0}:{1}", 1, NLCD_GET_URL)

            'Dim MyTiffURL As String = Nothing
            lFinalLayerName = IO.Path.Combine(lSaveIn, lBaseFilename & ".tif")
            If Not TryDelete(lFinalLayerName) Then
                lFinalLayerName = GetTemporaryFileName(IO.Path.ChangeExtension(lFinalLayerName, "").TrimEnd("."), IO.Path.GetExtension(lFinalLayerName))
            End If

            'Read the json file to get the .tif URL from the "href"
            Try
                Logger.Status("Requesting " & lBaseFilename)
                D4EM.Data.Download.SetSecurityProtocol()
                D4EM.Data.Download.DownloadURL(NLCD_GET_URL, lFinalLayerName)

                'Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(NLCD_GET_URL)
                'Dim response As System.Net.WebResponse = request.GetResponse()
                'Dim responseStream As System.IO.Stream = response.GetResponseStream()

                'Dim objReader As New StreamReader(responseStream)
                'Dim sLine As String = ""
                'Dim i As Integer = 0
                'Dim json As JObject
                'Do While Not sLine Is Nothing
                '    i += 1
                '    sLine = objReader.ReadLine
                '    If Not sLine Is Nothing Then
                '        'Console.WriteLine("{0}:{1}", i, sLine)
                '        json = JObject.Parse(sLine)
                '        MyTiffURL = json.SelectToken("href")
                '        'Console.WriteLine("{0}:{1}", i, MyTiffURL)
                '        Exit Do
                '    End If
                'Loop

                'Then Download the .tif file using the URL found in the JSON response
                'If String.IsNullOrEmpty(MyTiffURL) Then
                '    Throw New ApplicationException("URL not found in reply to request: " & NLCD_GET_URL)
                'End If
                System.Threading.Thread.Sleep(500)
                For lTryNum As Integer = 1 To 3
                    'If Download.DownloadURL(MyTiffURL, lFinalLayerName) AndAlso IO.File.Exists(lFinalLayerName) Then
                    If IO.File.Exists(lFinalLayerName) Then
                        'TODO: check whether the file is really an image or just a web error message
                        'TODO: reproject to aProject.DesiredProjection
                        'SpatialOperations.ProjectAndClipGridLayer(lFinalLayerName, lNativeProjection, aProject.DesiredProjection, Nothing, Nothing)
                        lResult = "<add_grid>" & lFinalLayerName & "</add_grid>" & vbCrLf
                        IO.File.WriteAllText(IO.Path.ChangeExtension(lFinalLayerName, ".prj"), lNativeProjection.ToEsriString()) ' Globals.WebMercatorProjection.ToEsriString())
                        IO.File.WriteAllText(IO.Path.ChangeExtension(lFinalLayerName, ".proj4"), lNativeProjection.ToProj4String()) 'Globals.WebMercatorProjection.ToProj4String())
                        Exit For
                    End If
                    System.Threading.Thread.Sleep(5000)
                Next
                If Not IO.File.Exists(lFinalLayerName) Then
                    Throw New ApplicationException("Error downloading from " & NLCD_GET_URL & ", file not created: " & lFinalLayerName)
                End If
            Catch wex As System.Net.WebException
                lError = "Error downloading from " & NLCD_GET_URL & " to " & lFinalLayerName & " : " & wex.ToString
            End Try
        Catch aex As ApplicationException
            lError = aex.Message
        Catch ex2 As Exception
            lError = ex2.ToString()
        End Try

        If (addLayer) Then
            Dim layerSpec As LayerSpecification = aDataType
            Dim nlayer = New Layer(lFinalLayerName, layerSpec, False)
            aProject.Layers.Add(nlayer)
        End If

        Logger.Progress("", 0, 0)
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If

    End Function

    'sdmdp version
    '''' <summary>
    '''' Download a layer from USGS Seamless Server
    '''' </summary>
    '''' <param name="aSaveFolder"></param>
    '''' <param name="DesiredProjection"></param>
    '''' <param name="aSaveLoc"></param>
    '''' <param name="cacheFolder"></param>
    '''' <param name="aRegionToClip"></param>
    '''' <param name="useCache"></param>
    '''' <param name="aDataType"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    Public Shared Function GetNLCD(ByVal aSaveFolder As String,
                                   ByVal DesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                   ByVal aSaveLoc As String,
                                   ByVal cacheFolder As String,
                                   ByVal aRegionToClip As Region,
                                   ByVal useCache As Boolean,
                                   ByVal aDataType As LayerSpecification) As String
        Dim lSaveIn As String = aSaveLoc
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)

        Dim lCacheFolder As String = IO.Path.Combine(cacheFolder, "NLCD")
        Dim lResult As String = ""
        Dim lError As String = ""
        Dim lBaseFilename As String = ""
        Dim lNorth As Double = 0
        Dim lSouth As Double = 0
        Dim lWest As Double = 0
        Dim lEast As Double = 0
        Dim TempDouble As Double = 0.0

        If aRegionToClip Is Nothing Then
            lError = "Region not found"
        Else 'convert input dimensions to web-mercator projection
            aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, Globals.WebMercatorProjection)
        End If

        'Test reprojected coordinates for validity (assumes in Webmercator projection and in NW hemisphere)
        If Not (Double.TryParse(lNorth, TempDouble) And Double.TryParse(lSouth, TempDouble) And Double.TryParse(lWest, TempDouble) And Double.TryParse(lEast, TempDouble)) Then
            lError = "Coordinates are not valid."
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If
        If (lNorth <= lSouth) Then 'Or (lWest >= lEast) Then
            lError = "Assumes conterminous US coordinates.  West value should be less than East and South less than North value"
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If

        Dim lNSEW As String = lNorth.ToString() + "_" + lSouth.ToString() + "_" + lEast.ToString() + "_" + lWest.ToString()   'Used for cache file name

        'Build NLCD request URL (json with reference to .tiff file returned)

        'Server location
        'Dim NLCD_USGS_Server_Text As String = "https://landfire.cr.usgs.gov/arcgis/rest/services/NLCD/USGS_EDC_LandCover_NLCD/ImageServer/exportImage?"

        'format can be an image or json with reference to an image
        Dim responseformat As String = "f=json"    '"f=image", "f=json"

        'box dimensions to clip
        Dim BoundingBox As String = "&bbox=" + lWest.ToString() + ", " + lSouth.ToString() + ", " + lEast.ToString() + ", " + lNorth.ToString()

        'length and width of output image
        'Determine the output size(rows, columns) of the image based on the area requested and assumed resolution & units of NLCD (i.e., 30 meter cells)
        'Reproject to output projection to figure out size
        aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, Globals.AlbersProjection)
        Dim XSize As Integer = (lEast - lWest) / 30
        Dim YSize As Integer = (lNorth - lSouth) / 30

        'limiting size of output .tiff
        If XSize > 50000 Then XSize = 50000 'limiting size of output .tiff
        If XSize < 1 Then XSize = 10 'min size
        If YSize > 50000 Then YSize = 50000
        If YSize < 1 Then YSize = 10
        'size of result in pixels (e.g., 300x400 = size=300,400; default is 400x400)
        Dim imagesize As String = "&size=" + XSize.ToString + "," + YSize.ToString  'size of result in pixels (e.g., 300x400 = size=300,400; default is 400x400)

        'input and output projections
        Dim BB_SR As String = "&bboxSR=3857"                 'Spatial Reference WKIDs: 3857 = Web Mercator, 4326 = WGS 84, 102003 = Contiguous USA albers
        Dim ImageSR As String = "&imageSR=102003"            'NOTE: The server didn't return a correct imager (all zero values) when trying to use USA albers as the input coords and output projection

        'output file * pixel type
        Dim imageformat As String = "&format=tiff"             'tif = "tiff" 
        Dim pixelType As String = "&pixelType=U8"              'data has unsigned 8-bit values'
        'Dim noData As String = "noData="                       'value considered as nodata values and rendered as transparent (Note:we are leaving this blank)
        'compression used for output tiff file
        Dim image_compression As String = "&compression=L277"  'can be L277 or None"

        'build moscaic rule that defines which year(1992, 2001, 2006, or 2011) and type of NLCD (landcover, canopy, impervious) we want to get 
        Dim lDataType As String = aDataType.Tag
        Dim lyear As String = "2011"
        Dim lUSarea As String = "conus"  '  can be blank, conus, hi, ak, or pr (for 2001 NLCD)

        Select Case aDataType
            Case LayerSpecifications.NLCD1992.LandCover
                'lDataType = "1992"
                lDataType = "landcover"
                lyear = "1992"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_LandCover_1992"
            Case LayerSpecifications.NLCD2001.LandCover
                lDataType = "landcover"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2001.Canopy
                lDataType = "canopy"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2001.Impervious
                lDataType = "impervious"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2006.LandCover
                lDataType = "landcover"
                lyear = "2006"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2006"
            Case LayerSpecifications.NLCD2006.Impervious
                lDataType = "impervious"
                lyear = "2006"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2006"
            Case LayerSpecifications.NLCD2011.LandCover
                lDataType = "landcover"
                lyear = "2011"
                lUSarea = ""
                lBaseFilename = "NLCD_" & lDataType & "_2011"
                'Case LayerSpecifications.NED.OneArcSecond
                '   lBaseFilename = "NED_1ArcSecond"
                '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
                'Case LayerSpecifications.NED.OneThirdArcSecond
                '   lBaseFilename = "NED_ThirdArcSecond"
                '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
            Case Else
                '   lBaseFilename = lDataType
        End Select

        'Specifies which version of NLCD to request
        Dim mosaicRule As String = "&mosaicRule={%22where%22%3A%22Name%3D%27" + lDataType + "_" + lyear + lUSarea + "%27%22}"    ' {"where":"Name='landcover_2011'"}

        'combine request URL pieces into request URL
        Dim NLCD_GET_URL As String = NLCD_USGS_Server_Text + responseformat + BoundingBox + imagesize + BB_SR + ImageSR + imageformat + pixelType + image_compression + mosaicRule
        Console.WriteLine("{0}:{1}", 1, NLCD_GET_URL)                                   'request URL

        ' define output .tif file name
        Dim lFinalLayerName As String = IO.Path.Combine(lSaveIn, lBaseFilename & ".tif")

        'Set up webrequest with NLCD_GETURL and create stream with JSON result (which should include a URL to the .tiff image we want)
        Try
            D4EM.Data.Download.SetSecurityProtocol()
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(NLCD_GET_URL)
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()

            'Read the json file to get the .tif URL from the "href"
            Dim objReader As New StreamReader(responseStream)
            Dim sLine As String = ""
            Dim i As Integer = 0
            Dim json As JObject
            Dim MyTiffURL As String = Nothing
            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine                                              ' Should only need to read one line which should contain the href token plus rest of tokens
                If Not sLine Is Nothing Then
                    Console.WriteLine("{0}:{1}", i, sLine)                              'response JSON
                    json = JObject.Parse(sLine)
                    MyTiffURL = json.SelectToken("href")                                'The .tif URL should be in the href token
                    Console.WriteLine("{0}:{1}", i, MyTiffURL)                           'just the href token to the .tif image that we want
                    Exit Do
                End If
            Loop

            'Download the .tif file using the URL found in the JSON response
            If MyTiffURL Is Nothing Then
                lError = "URL for image in JSON reply is empty"
                Logger.Dbg("Error downloading from USGS Seamless", lError)
                Return "<error>" & lError & "</error>"
            End If

            If System.IO.File.Exists(lFinalLayerName) = True Then
                Try
                    IO.File.Delete(lFinalLayerName)
                Catch ex As Exception
                    lError = "Can't delete existing image file with the same name: " + lFinalLayerName
                    Logger.Dbg("Error downloading from USGS Seamless", lError)
                    Return "<error>" & lError & "</error>"
                End Try
            End If

            System.Threading.Thread.Sleep(500)
            For lTryNum As Integer = 1 To 3
                If Download.DownloadURL(MyTiffURL, lFinalLayerName) AndAlso IO.File.Exists(lFinalLayerName) Then
                    'My.Computer.Network.DownloadFile(MyTiffURL, lFinalLayerName)
                    Exit For
                End If
                System.Threading.Thread.Sleep(5000)
            Next


        Catch ex As System.Net.WebException
            Throw New ApplicationException("There was an error opening the NLCD .tiff image file. Check the URL")
        End Try

        Logger.Progress("", 0, 0)
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If
    End Function

    ''' sdmpb version
    Public Shared Function GetNLCD(ByVal aProject As Project, ByVal aSaveFolder As String,
                                   ByVal DesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                   ByVal aSaveLoc As String,
                                   ByVal cacheFolder As String,
                                   ByVal aRegionToClip As Region,
                                   ByVal useCache As Boolean,
                                   ByVal aDataType As LayerSpecification) As String
        Dim lSaveIn As String = aSaveLoc
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)

        Dim lCacheFolder As String = IO.Path.Combine(cacheFolder, "NLCD")
        Dim lResult As String = ""
        Dim lError As String = ""
        Dim lBaseFilename As String = ""
        Dim lNorth As Double = 0
        Dim lSouth As Double = 0
        Dim lWest As Double = 0
        Dim lEast As Double = 0
        Dim TempDouble As Double = 0.0

        If aRegionToClip Is Nothing Then
            lError = "Region not found"
        Else 'convert input dimensions to web-mercator projection
            aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, Globals.WebMercatorProjection)
        End If

        'Test reprojected coordinates for validity (assumes in Webmercator projection and in NW hemisphere)
        If Not (Double.TryParse(lNorth, TempDouble) And Double.TryParse(lSouth, TempDouble) And Double.TryParse(lWest, TempDouble) And Double.TryParse(lEast, TempDouble)) Then
            lError = "Coordinates are not valid."
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If
        If (lNorth <= lSouth) Or (lWest >= lEast) Then
            lError = "Assumes conterminous US coordinates.  West value should be less than East and South less than North value"
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If

        Dim lNSEW As String = lNorth.ToString() + "_" + lSouth.ToString() + "_" + lEast.ToString() + "_" + lWest.ToString()   'Used for cache file name

        'Build NLCD request URL (json with reference to .tiff file returned)

        'Server location
        'Dim NLCD_USGS_Server_Text As String = "https://landfire.cr.usgs.gov/arcgis/rest/services/NLCD/USGS_EDC_LandCover_NLCD/ImageServer/exportImage?"

        'format can be an image or json with reference to an image
        Dim responseformat As String = "f=json"    '"f=image", "f=json"

        'box dimensions to clip
        Dim BoundingBox As String = "&bbox=" + lWest.ToString() + ", " + lSouth.ToString() + ", " + lEast.ToString() + ", " + lNorth.ToString()

        'length and width of output image
        'Determine the output size(rows, columns) of the image based on the area requested and assumed resolution & units of NLCD (i.e., 30 meter cells)
        'Reproject to output projection to figure out size
        aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, Globals.AlbersProjection)
        Dim XSize As Integer = (lEast - lWest) / 30
        Dim YSize As Integer = (lNorth - lSouth) / 30

        'limiting size of output .tiff
        If XSize > 50000 Then XSize = 50000 'limiting size of output .tiff
        If XSize < 1 Then XSize = 10 'min size
        If YSize > 50000 Then YSize = 50000
        If YSize < 1 Then YSize = 10
        'size of result in pixels (e.g., 300x400 = size=300,400; default is 400x400)
        Dim imagesize As String = "&size=" + XSize.ToString + "," + YSize.ToString  'size of result in pixels (e.g., 300x400 = size=300,400; default is 400x400)

        'input and output projections
        Dim BB_SR As String = "&bboxSR=3857"                 'Spatial Reference WKIDs: 3857 = Web Mercator, 4326 = WGS 84, 102003 = Contiguous USA albers
        Dim ImageSR As String = "&imageSR=7301"            'NOTE: The server didn't return a correct imager (all zero values) when trying to use USA albers as the input coords and output projection
        'Dim ImageSR As String = "&imageSR=102008"               '102008 = Contiguous NorthAmerica  albers

        'output file * pixel type
        Dim imageformat As String = "&format=tiff"             'tif = "tiff" 
        'Dim pixelType As String = "&pixelType=U8"              'data has unsigned 8-bit values'
        Dim pixelType As String = "&pixelType=F64"              'data has unsigned 8-bit values'
        Dim noData As String = "noData="                       'value considered as nodata values and rendered as transparent (Note:we are leaving this blank)
        'compression used for output tiff file
        Dim image_compression As String = "&compression=L277"  'can be L277 or None"
        'Dim image_compression As String = "&compression=None"  'can be L277 or None"

        'build moscaic rule that defines which year(1992, 2001, 2006, or 2011) and type of NLCD (landcover, canopy, impervious) we want to get 
        Dim lDataType As String = aDataType.Tag
        Dim lyear As String = "2011"
        Dim lUSarea As String = "conus"  '  can be blank, conus, hi, ak, or pr (for 2001 NLCD)

        Select Case aDataType
            Case LayerSpecifications.NLCD1992.LandCover
                'lDataType = "1992"
                lDataType = "landcover"
                lyear = "1992"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_LandCover_1992"
            Case LayerSpecifications.NLCD2001.LandCover
                lDataType = "landcover"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2001.Canopy
                lDataType = "canopy"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2001.Impervious
                lDataType = "impervious"
                lyear = "2001"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2001"
            Case LayerSpecifications.NLCD2006.LandCover
                lDataType = "landcover"
                lyear = "2006"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2006"
            Case LayerSpecifications.NLCD2006.Impervious
                lDataType = "impervious"
                lyear = "2006"
                lUSarea = "_conus"
                lBaseFilename = "NLCD_" & lDataType & "_2006"
            Case LayerSpecifications.NLCD2011.LandCover
                lDataType = "landcover"
                lyear = "2011"
                lUSarea = ""
                lBaseFilename = "NLCD_" & lDataType & "_2011"
            'Case LayerSpecifications.NED.OneArcSecond
            '   lBaseFilename = "NED_1ArcSecond"
            '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
            'Case LayerSpecifications.NED.OneThirdArcSecond
            '   lBaseFilename = "NED_ThirdArcSecond"
            '  lNativeProjection = D4EM.Data.Globals.GeographicProjection
            Case Else
                '   lBaseFilename = lDataType
        End Select

        'Specifies which version of NLCD to request
        Dim mosaicRule As String = "&mosaicRule={%22where%22%3A%22Name%3D%27" + lDataType + "_" + lyear + lUSarea + "%27%22}"    ' {"where":"Name='landcover_2011'"}

        'combine request URL pieces into request URL
        Dim NLCD_GET_URL As String = NLCD_USGS_Server_Text + responseformat + BoundingBox + imagesize + BB_SR + ImageSR + imageformat + pixelType + image_compression + mosaicRule
        Console.WriteLine("{0}:{1}", 1, NLCD_GET_URL)                                   'request URL

        ' define output .tif file name
        'Dim lFinalLayerName As String = IO.Path.Combine(lSaveIn, lBaseFilename & ".tif")
        Dim lFinalLayerName As String = IO.Path.Combine(lSaveIn, aDataType.FilePattern)

        'Set up webrequest with NLCD_GETURL and create stream with JSON result (which should include a URL to the .tiff image we want)
        Try
            D4EM.Data.Download.SetSecurityProtocol()
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(NLCD_GET_URL)
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()

            'Read the json file to get the .tif URL from the "href"
            Dim objReader As New StreamReader(responseStream)
            Dim sLine As String = ""
            Dim i As Integer = 0
            Dim json As JObject
            Dim MyTiffURL As String = Nothing
            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine                                              ' Should only need to read one line which should contain the href token plus rest of tokens
                If Not sLine Is Nothing Then
                    Console.WriteLine("{0}:{1}", i, sLine)                              'response JSON
                    json = JObject.Parse(sLine)
                    MyTiffURL = json.SelectToken("href")                                'The .tif URL should be in the href token
                    Console.WriteLine("{0}:{1}", i, MyTiffURL)                           'just the href token to the .tif image that we want
                    Exit Do
                End If
            Loop

            'Download the .tif file using the URL found in the JSON response
            If MyTiffURL Is Nothing Then
                lError = "URL for image in JSON reply is empty"
                Logger.Dbg("Error downloading from USGS Seamless", lError)
                Return "<error>" & lError & "</error>"
            End If


            If System.IO.File.Exists(lFinalLayerName) = True Then
                Try
                    IO.File.Delete(lFinalLayerName)
                Catch ex As Exception
                    lError = "Can't delete existing image file with the same name: " + lFinalLayerName
                    Logger.Dbg("Error downloading from USGS Seamless", lError)
                    Return "<error>" & lError & "</error>"
                End Try
            End If

            System.Threading.Thread.Sleep(500)
            For lTryNum As Integer = 1 To 3
                If Download.DownloadURL(MyTiffURL, lFinalLayerName) AndAlso IO.File.Exists(lFinalLayerName) Then
                    'My.Computer.Network.DownloadFile(MyTiffURL, lFinalLayerName)
                    Exit For
                End If
                System.Threading.Thread.Sleep(5000)
            Next



            'Dim layerSpec As LayerSpecification = LayerSpecification.FromFilename(lFinalLayerName, GetType(USGS_Seamless.LayerSpecifications.NLCD2001))
            Dim layerSpec As LayerSpecification = aDataType

            Dim nlayer = New Layer(lFinalLayerName, layerSpec, False)
            aProject.Layers.Add(nlayer)

        Catch ex As System.Net.WebException
            Throw New ApplicationException("There was an error opening the NLCD .tiff image file. Check the URL")
        End Try


        Logger.Progress("", 0, 0)
        If lError.Length = 0 Then
            If lResult.Length > 0 Then
                Return "<success>" & lResult & "</success>"
            Else
                Return "<success />"
            End If
        Else
            Logger.Dbg("Error downloading from USGS Seamless", lError)
            Return "<error>" & lError & "</error>"
        End If
    End Function

    '    ''' <summary>
    '    ''' Download a layer from USGS Seamless Server
    '    ''' </summary>
    '    ''' <param name="aSaveFolder"></param>
    '    ''' <param name="DesiredProjection"></param>
    '    ''' <param name="aSaveLoc"></param>
    '    ''' <param name="cacheFolder"></param>
    '    ''' <param name="aRegionToClip"></param>
    '    ''' <param name="useCache"></param>
    '    ''' <param name="aDataType"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Shared Function GetNLCDOLD(ByVal aSaveFolder As String,
    '                                   ByVal DesiredProjection As DotSpatial.Projections.ProjectionInfo,
    '                                   ByVal aSaveLoc As String,
    '                                   ByVal cacheFolder As String,
    '                                   ByVal aRegionToClip As Region,
    '                                   ByVal useCache As Boolean,
    '                                   ByVal aDataType As LayerSpecification) As String
    '        Dim lSaveIn As String = aSaveLoc
    '        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)

    '        Dim lCacheFolder As String = IO.Path.Combine(cacheFolder, "NLCD")



    'StartOver:
    '        Dim lResult As String = ""
    '        Dim lError As String = ""
    '        Dim lBaseFilename As String

    '        If aRegionToClip Is Nothing Then
    '            lError = "Region not found"
    '        Else
    '            Dim lNorth As Double = 0
    '            Dim lSouth As Double = 0
    '            Dim lWest As Double = 0
    '            Dim lEast As Double = 0
    '            Dim lRC As String = ""

    '            aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, Globals.GeographicProjection)
    '            Dim lNSEW As String = lNorth.ToString() + "_" + lSouth.ToString() + "_" + lEast.ToString() + "_" + lWest.ToString()

    '            Dim lDataType As String = aDataType.Tag
    '            Dim lNativeProjection As DotSpatial.Projections.ProjectionInfo = D4EM.Data.Globals.AlbersProjection
    '            Select Case aDataType
    '                Case LayerSpecifications.NLCD1992.LandCover
    '                    lDataType = "1992"
    '                    lBaseFilename = "NLCD_LandCover_1992"
    '                Case LayerSpecifications.NLCD2001.LandCover
    '                    lDataType = "landcover"
    '                    lBaseFilename = "NLCD_" & lDataType & "_2001"
    '                Case LayerSpecifications.NLCD2001.Canopy
    '                    lBaseFilename = "NLCD_" & lDataType & "_2001"
    '                Case LayerSpecifications.NLCD2001.Impervious
    '                    lDataType = "impervious"
    '                    lBaseFilename = "NLCD_" & lDataType & "_2001"
    '                Case LayerSpecifications.NLCD2006.LandCover
    '                    lDataType = "landcover"
    '                    lBaseFilename = "NLCD_" & lDataType & "_2006"
    '                Case LayerSpecifications.NLCD2006.Impervious
    '                    lDataType = "impervious"
    '                    lBaseFilename = "NLCD_" & lDataType & "_2006"
    '                Case LayerSpecifications.NED.OneArcSecond
    '                    lBaseFilename = "NED_1ArcSecond"
    '                    lNativeProjection = D4EM.Data.Globals.GeographicProjection
    '                Case LayerSpecifications.NED.OneThirdArcSecond
    '                    lBaseFilename = "NED_ThirdArcSecond"
    '                    lNativeProjection = D4EM.Data.Globals.GeographicProjection
    '                Case Else
    '                    lBaseFilename = lDataType
    '            End Select

    '            Dim lFinalLayerName As String = IO.Path.Combine(lSaveIn, lBaseFilename & ".tif")

    '            If IO.File.Exists(lFinalLayerName) Then
    '                lResult &= "<message>Layer already exists: " & lFinalLayerName & vbCrLf & "Delete, move, or rename existing layer before downloading.</message>"
    '            Else
    '                Dim lCacheZipFile As String = IO.Path.Combine(lCacheFolder, "NLCD_" & lDataType & "_" & lNSEW & ".zip")
    '                Try
    '                    If IsCached(lCacheZipFile) Then
    '                        Logger.Dbg("UsingExisting1 " & lCacheZipFile)
    '                        If useCache Then
    '                            Logger.Dbg("Already Cached " & lCacheZipFile)
    '                        Else
    '                            lResult &= ProcessDownloadedZip(lSaveIn, DesiredProjection, aDataType, lCacheZipFile, lBaseFilename, lNativeProjection)
    '                        End If
    '                    Else
    '                        Dim wc As New System.Net.WebClient()
    '                        wc.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials

    '                        Dim lRequestSummary As String = wc.DownloadString(BuildURL(1, aDataType, lNorth, lSouth, lWest, lEast))

    '                        Dim lFindForm As Integer
    '                        Dim lStartSearch As Integer = 0

    '                        Do
    '                            lFindForm = lRequestSummary.IndexOf("<FORM", lStartSearch)
    '                            If lFindForm < 0 Then
    '                                If lStartSearch = 0 Then
    '                                    If Logger.Msg("Seamless data not downloaded - View error message?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '                                        Dim lErrMsgFile As String = GetTemporaryFileName("Seamless_Error", "html")
    '                                        IO.File.WriteAllText(lErrMsgFile, lRequestSummary)
    '                                        OpenFile(lErrMsgFile)
    '                                    End If
    '                                    If Logger.Msg("Retry Seamless download now?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '                                        GoTo StartOver
    '                                    End If
    '                                    Throw New ApplicationException("Could not download from Seamless.USGS" & vbCrLf & "FORM METHOD not found")
    '                                End If
    '                                Exit Do
    '                            Else
    '                                Dim lForm As String = lRequestSummary.Substring(lFindForm)
    '                                Dim lEndForm As Integer = lForm.IndexOf("</FORM>")
    '                                If lEndForm > 0 Then lForm = lForm.Substring(0, lEndForm)
    '                                lStartSearch = lFindForm + lForm.Length
    '                                Dim lFormElements() As String = lForm.Split("<")

    '                                lNorth = InputValueToDouble(lFormElements, "top")
    '                                lSouth = InputValueToDouble(lFormElements, "bot")
    '                                lWest = InputValueToDouble(lFormElements, "lft")
    '                                lEast = InputValueToDouble(lFormElements, "rgt")
    '                                lRC = InputValue(lFormElements, "RC")

    '                                lNSEW = lNorth.ToString() + "_" + lSouth.ToString() + "_" + lEast.ToString() + "_" + lWest.ToString()
    '                                lCacheZipFile = IO.Path.Combine(lCacheFolder, "NLCD_" & lDataType & "_" & lNSEW & ".zip")
    '                                If IsCached(lCacheZipFile) Then
    '                                    Logger.Dbg("UsingExisting2 " & lCacheZipFile)
    '                                Else
    '                                    For Each lResponseKey As String In wc.ResponseHeaders.Keys
    '                                        Select Case lResponseKey
    '                                            Case "Set-Cookie"
    '                                                wc.Headers.Add("Cookie", wc.ResponseHeaders("Set-Cookie"))
    '                                        End Select
    '                                    Next
    '                                    wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
    '                                    Dim lUrl2 As String = BuildURL(2, aDataType, lNorth, lSouth, lWest, lEast) & lRC
    '                                    Dim lUrl2ArgStart As Integer = lUrl2.IndexOf("?")
    '                                    Dim lPostData() As Byte = System.Text.UTF8Encoding.UTF8.GetBytes(lUrl2.Substring(lUrl2ArgStart + 1))
    '                                    Dim lStage2() As Byte = wc.UploadData(lUrl2.Substring(0, lUrl2ArgStart), lPostData)
    '                                    Dim lStage3URL As String = Nothing
    '                                    Dim contentType As String = ""
    '                                    Dim data As Byte()
    '                                    wc.Headers.Remove("Content-Type") 'No longer posting, don't need this header any more
    'StartWaiting:
    '                                    For Each lResponseKey As String In wc.ResponseHeaders.Keys
    '                                        Select Case lResponseKey
    '                                            Case "Refresh"
    '                                                Dim lValue As String = wc.ResponseHeaders("Refresh")
    '                                                Dim lUrlIndex As Integer = lValue.IndexOf("URL=")
    '                                                If lUrlIndex >= 0 Then lStage3URL = lValue.Substring(lUrlIndex + 4)
    '                                            Case "Set-Cookie"
    '                                                wc.Headers.Add("Cookie", wc.ResponseHeaders("Set-Cookie"))
    '                                        End Select
    '                                    Next
    '                                    If lStage3URL Is Nothing Then Throw New ApplicationException("Could not download from Seamless.USGS (2)No refresh URL found from stage 2)")
    '                                    Dim lRetryCountIndex As Integer = 0
    '                                    Dim lRetryCountMax As Integer = 120 ' 10 minutes
    '                                    Dim lStartNumRequests As Integer = 0
    '                                    Logger.Status("Waiting for Seamless to Process " & lDataType & " Request")
    '                                    Dim lStage As String = "Start"
    '                                    Do While lRetryCountIndex < lRetryCountMax
    '                                        lRetryCountIndex += 1
    '                                        data = wc.DownloadData(lStage3URL)
    '                                        contentType = wc.ResponseHeaders("Content-Type")
    '                                        If contentType.Contains("compressed") Then
    '                                            Logger.Status("Saving " & lDataType)
    '                                            IO.Directory.CreateDirectory(lCacheFolder)
    '                                            IO.File.WriteAllBytes(lCacheZipFile, data)
    '                                            Layer.AddProcessStepToFile("Downloaded from " & lUrl2, lCacheZipFile)
    '                                            Exit Do
    '                                        Else
    '                                            Dim page As String = System.Text.UTF8Encoding.UTF8.GetString(data)
    '                                            'Debug.Print(page)
    '                                            Dim lRequestsPos As Integer = page.IndexOf("There are ")
    '                                            If lRequestsPos > 0 Then
    '                                                Dim lNumRequests As Integer = StrFirstInt(page.Substring(lRequestsPos + 10))
    '                                                If lStartNumRequests < lNumRequests Then lStartNumRequests = lNumRequests + 1
    '                                                If lNumRequests > 0 Then
    '                                                    Logger.Status("Waiting for Seamless to Process " & lDataType & " Request.")
    '                                                    Logger.Progress("MSG3 There are " & lNumRequests & " requests ahead of you.", lStartNumRequests - lNumRequests, lStartNumRequests)
    '                                                Else
    '                                                    If lStage <> "process" Then
    '                                                        lStage = "process"
    '                                                        Logger.Status("Waiting for Seamless to Process " & lDataType & " Request.")
    '                                                        lRetryCountIndex = 1
    '                                                    End If
    '                                                    Logger.Progress(lRetryCountIndex, lRetryCountMax)
    '                                                End If
    '                                            ElseIf page.IndexOf("Extracting data") > 0 OrElse page.IndexOf("Extracting Metadata") > 0 Then
    '                                                If lStage <> "extract" Then
    '                                                    lStage = "extract"
    '                                                    Logger.Status("Seamless is extracting data")
    '                                                    lRetryCountIndex = 1
    '                                                End If
    '                                                Logger.Progress(lRetryCountIndex, lRetryCountMax)
    '                                            ElseIf page.IndexOf("has completed") > 0 OrElse page.IndexOf("Creating the archive file") > 0 Then
    '                                                If lStage <> "archive" Then
    '                                                    lStage = "archive"
    '                                                    Logger.Status("Seamless is creating the archive file")
    '                                                    lRetryCountIndex = 1
    '                                                End If
    '                                                Logger.Progress(lRetryCountIndex, lRetryCountMax)
    '                                            ElseIf page.IndexOf("error") >= 0 Then
    '                                                Throw New ApplicationException("Could not download from Seamless.USGS" & vbCrLf & page.Replace("Please wait for the data to be returned.", ""))
    '                                            ElseIf page.IndexOf("lease wait") > 0 Then
    '                                                Logger.Progress(lRetryCountIndex, lRetryCountMax)
    '                                            Else
    '                                                Dim lMessage As String = page
    '                                                If lMessage.IndexOf("<BODY>") > 0 Then
    '                                                    lMessage = lMessage.Substring(lMessage.IndexOf("<BODY>") + 6)
    '                                                End If

    '                                                If Logger.Msg(StripHTMLtags(lMessage) & vbCrLf & "Continue waiting for result?", MsgBoxStyle.YesNo, "Unexpected result from seamless server") = MsgBoxResult.No Then
    '                                                    Throw New ApplicationException("Could not download from Seamless.USGS" & vbCrLf & page)
    '                                                End If
    '                                            End If
    '                                        End If
    '                                        System.Threading.Thread.Sleep(5000)
    '                                        Logger.Status("MSG3")
    '                                    Loop
    '                                    If lRetryCountIndex >= lRetryCountMax Then
    '                                        Select Case Logger.Msg("Continue waiting for result?", MsgBoxStyle.YesNo, "Seamless server is slow to respond with results")
    '                                            Case MsgBoxResult.Yes : GoTo StartWaiting
    '                                            Case MsgBoxResult.No : Throw New ApplicationException("Could not download from Seamless.USGS" & vbCrLf & "Waiting time exceeded")
    '                                        End Select
    '                                    End If
    '                                End If
    '                                If IO.File.Exists(lCacheZipFile) Then
    '                                    If useCache Then
    '                                        Logger.Dbg("Cached " & lCacheZipFile)
    '                                    Else
    '                                        Dim lProcessResult As String = ProcessDownloadedZip(lSaveIn, DesiredProjection, aDataType, lCacheZipFile, lBaseFilename, lNativeProjection)
    '                                        'When merging tiles into one grid, we get duplicate "add_grid" results for each tile
    '                                        If Not lResult.Contains(lProcessResult) Then
    '                                            lResult &= lProcessResult
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        Loop
    '                    End If
    '                    'do not want to directly add layers from here.
    '                    'If Not useCache AndAlso IO.File.Exists(lFinalLayerName) Then
    '                    '    aProject.Layers.Add(New D4EM.Data.Layer(lFinalLayerName, aDataType, False))
    '                    'End If
    '                Catch ex As Exception
    '                    lError &= ex.Message
    '                End Try
    '            End If
    '        End If
    '        Logger.Progress("", 0, 0)
    '        If lError.Length = 0 Then
    '            If lResult.Length > 0 Then
    '                Return "<success>" & lResult & "</success>"
    '            Else
    '                Return "<success />"
    '            End If
    '        Else
    '            Logger.Dbg("Error downloading from USGS Seamless", lError)
    '            Return "<error>" & lError & "</error>"
    '        End If
    '    End Function

    Private Shared Function BuildURL(ByVal aPhase As Integer,
                                     ByVal aLayerSpecification As LayerSpecification,
                                     ByVal aNorth As Double,
                                     ByVal aSouth As Double,
                                     ByVal aWest As Double,
                                     ByVal aEast As Double) As String
        Dim lKey As String = ""
        Dim lKey2 As String = ""
        Dim lMCD As String = ""
        Dim lMSD As String = ""
        Dim lSDE As String = ""
        Dim lCS As String = "30.0" 'Cell Size
        Dim lDLPRE As String = ""
        Dim lLayerFormat1 As String = "02" 'GeoTIFF
        Dim lLayerFormat2 As String = "GeoTIFF"
        Dim lMetadataFormat1 As String = "X" 'XML
        Dim lMetadataFormat2 As String = "XML"
        Dim lArchiveFormat1 As String = "Z" 'ZIP
        Dim lArchiveFormat2 As String = "ZIP"
        Select Case aLayerSpecification
            Case LayerSpecifications.NLCD1992.LandCover  '"1992", "1992landcover", "1992 land cover"
                lKey = "L92"
                lKey2 = "L92"
                lDLPRE = ""
                lMCD = "NLCD92"
                lSDE = "NLCD.CONUS_NLCD_ALBERS"
                '1992 land cover now only has text format metadata available
                lMetadataFormat1 = "T"
                lMetadataFormat2 = "TXT"
            Case LayerSpecifications.NLCD2001.LandCover '"land cover", "landcover"
                lKey = "L01"
                lKey2 = "L01"
                lDLPRE = lKey & "_"
                lMCD = "NLCD01LANC"
                lSDE = "NLCD.NLCD_2001_LANDC" '"WEBMAP.NLCD_2001_LANDC"
            Case LayerSpecifications.NLCD2001.Canopy ' "canopy"
                lKey = "LCY"
                lKey2 = "LCY"
                lDLPRE = lKey & "_"
                lMCD = "NLCD01CANO"
                lSDE = "NLCD.NLCD_2001_CANOPY"
            Case LayerSpecifications.NLCD2001.Impervious '"impervious", "impervioussurface", "impervious surface"
                lKey = "LIS"
                lKey2 = "LIS"
                lDLPRE = lKey2 & "_"
                lMCD = "NLCD01IMPV"
                lSDE = "NLCD.NLCD_2001_IMPERV"
            Case LayerSpecifications.NLCD2006.LandCover '"land cover", "landcover"
                lKey = "L6"
                lKey2 = "L06"
                lDLPRE = lKey & "_"
                lMCD = "NLCD06LANC"
                lSDE = "NLCD.NLCD_2006_LANDC"
                lLayerFormat1 = "L02"
                lMetadataFormat1 = "H"
            Case LayerSpecifications.NLCD2006.Impervious '"impervious", "impervioussurface", "impervious surface"
                lKey = "LIS"
                lKey2 = "LIS"
                lDLPRE = lKey2 & "_"
                lMCD = "NLCD06IMPV"
                lSDE = "NLCD.NLCD_2006_IMPERV"
            Case LayerSpecifications.NED.OneArcSecond
                lKey = "NED"
                lKey2 = "NED"
                lMetadataFormat1 = "H"
                lMCD = "NED"
                lSDE = "NED.conus_ned"
                lMSD = "NED.wrld_ned_metadata"
            Case LayerSpecifications.NED.OneThirdArcSecond
                lKey = "ND3"
                lKey2 = "ND3"
                lMetadataFormat1 = "H"
                lMCD = "NED13"
                lSDE = "NED.conus_ned_13e"
                lMSD = "NED.wrld_13_metadata"
                'Case "srtm1"
                '    lKey = "SM3"
                '    lDLPRE = lKey & "_"
                '    lMCD = "SRTM1FIN"
                '    lSDE = "SRTM.C_US_1_ELEVATION_SHIFT"
                '    lMSD = "SRTM.c_national_1_elevation_meta"
                '    lCS = "2.777777778000001E-4"
                'Case "ned30"
                '    lKey = "NED"
                '    lDLPRE = lKey & "_"
                '    lMCD = "NED"
                '    lSDE = "NED.conus_ned"
                '    lMSD = "NED.CONUS_NED_METADATA"
                '    lCS = "2.777777778000001E-4"

                'http://extract.cr.usgs.gov/Website/distreq/RequestSummary.jsp?AL=
                '33.783242638646314,33.734561624075354,-85.02272072569099,-85.0549361029806&PL=NED01HZ,"
                '&CS=250&PR=0&MD=CD&PL=NED02XZ
                'http://152.61.128.141/diststatus/servlet/gov.usgs.edc.RequestStatus?zid=20090325.140903964.216027161096
                'siz=1
                '&key=NED
                '&ras=1&rsp=1&pfm=GeoTIFF&imsurl=-1&ms=-1&att=-1&lay=-1&fid=-1&dlpre=NED_
                '&lft=-84.33627900701559&rgt=-84.27197085806793&top=33.79778845116024&bot=33.73907231516455
                '&wmd=1&mur=http%3A%2F%2Fextract.cr.usgs.gov%2Fdistmeta%2Fservlet%2Fgov.usgs.edc.MetaBuilder
                '&mcd=NED
                '&mdf=XML&arc=ZIP
                '&sde=NED.conus_ned
                '&msd=NED.CONUS_NED_METADATA
                '&zun=METERS
                '&prj=0
                '&csx=2.77777777799943E-4&csy=2.77777777799943E-4
                '&bnd=&bndnm=&RC=e27a7920acbb3f82d59ea032b476ddd52f1a3530
        End Select
        Select Case aPhase
            Case 1
                Return "https://landfire.cr.usgs.gov/Website/distreq/RequestSummary.jsp?AL=" _
                     & aNorth.ToString() + "," + aSouth.ToString() + "," + aEast.ToString() + "," + aWest.ToString() _
                     & "&PL=" & lKey & lLayerFormat1 & lMetadataFormat1 & lArchiveFormat1 & "&ORIG=MRLC"
            Case 2 'https://igskmncngs086.cr.usgs.gov/diststatus/servlet/gov.usgs.edc.RequestStatus?zid=20120926.083203758.152061136002
                Return "https://landfire.cr.usgs.gov/diststatus/servlet/gov.usgs.edc.RequestStatus?siz=1" _
                     & "&key=" & lKey2 _
                     & "&ras=1&rsp=0" _
                     & "&pfm=" & lLayerFormat2 _
                     & "&imsurl=-1&ms=-1&att=-1&lay=-1&fid=-1" _
                     & "&dlpre=" & lDLPRE _
                     & "&lft=" & aWest _
                     & "&rgt=" & aEast _
                     & "&top=" & aNorth _
                     & "&bot=" & aSouth _
                     & "&wmd=1&mur=https://landfire.cr.usgs.gov/distmeta/servlet/gov.usgs.edc.MetaBuilder" _
                     & "&mcd=" & lMCD _
                     & "&mdf=" & lMetadataFormat2 _
                     & "&arc=" & lArchiveFormat2 _
                     & "&sde=" & lSDE _
                     & "&msd=" & lMSD _
                     & "&zun=&prj=0" _
                     & "&csx=" & lCS _
                     & "&csy=" & lCS _
                     & "&bnd=&bndnm=&RC=&ORIG=MRLC"

            Case 3
                Return "https://landfire.cr.usgs.gov/diststatus/servlet/gov.usgs.edc.RequestStatus"
        End Select
        Return ""
    End Function

    ''' <summary>
    ''' Changes aFilename to an existing file name if one is found whose edges are close enough to those in aFilename
    ''' "Close enough" is defined by CacheWithinDegreesInside and CacheWithinDegreesOutside
    ''' </summary>
    ''' <param name="aFilename"></param>
    ''' <returns>True if file is cached, False if not found in cache</returns>
    ''' <remarks></remarks>
    Private Shared Function IsCached(ByRef aFilename As String) As Boolean
        Try
            If IO.File.Exists(aFilename) Then
                Return True
            End If

            Dim lDirectory As String = IO.Path.GetDirectoryName(aFilename)
            If IO.Directory.Exists(lDirectory) Then
                Dim lCoords() As String = IO.Path.GetFileNameWithoutExtension(aFilename).Split("_")
                Dim lNorth As Double = Double.Parse(lCoords(2))
                Dim lSouth As Double = Double.Parse(lCoords(3))
                Dim lEast As Double = Double.Parse(lCoords(4))
                Dim lWest As Double = Double.Parse(lCoords(5))

                For Each lFilename As String In IO.Directory.GetFiles(lDirectory, lCoords(0) & "_" & lCoords(1) & "_*.zip")
                    Try
                        lCoords = IO.Path.GetFileNameWithoutExtension(lFilename).Split("_")
                        Dim lNorthDifference As Double = Double.Parse(lCoords(2)) - lNorth
                        If lNorthDifference > -CacheWithinDegreesInside AndAlso lNorthDifference < CacheWithinDegreesOutside Then
                            Dim lSouthDifference As Double = lSouth - Double.Parse(lCoords(3))
                            If lSouthDifference > -CacheWithinDegreesInside AndAlso lSouthDifference < CacheWithinDegreesOutside Then
                                Dim lEastDifference As Double = Double.Parse(lCoords(4)) - lEast
                                If lEastDifference > -CacheWithinDegreesInside AndAlso lEastDifference < CacheWithinDegreesOutside Then
                                    Dim lWestDifference As Double = lWest - Double.Parse(lCoords(5))
                                    If lWestDifference > -CacheWithinDegreesInside AndAlso lWestDifference < CacheWithinDegreesOutside Then
                                        Logger.Dbg("NLCD close enough match found, requested " & IO.Path.GetFileNameWithoutExtension(aFilename) & " found " & IO.Path.GetFileNameWithoutExtension(lFilename))
                                        aFilename = lFilename
                                        Return True
                                    End If
                                End If
                            End If
                        End If
                    Catch 'Skip trying to match file if coordinates could not be parsed from file name
                    End Try
                Next
            End If
        Catch e As Exception
            Logger.Dbg("IsCached Exception, returning False '" & e.ToString() & "' for cache file '" & aFilename & "'")
        End Try
        Return False
    End Function
    Private Shared Function ProcessDownloadedZip(ByVal aSaveFolder As String,
                                                 ByVal DesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                                 ByVal aDataType As LayerSpecification,
                                                 ByVal aCacheZipFile As String,
                                                 ByVal aBaseFilename As String,
                                                 ByVal aLayerProjection As DotSpatial.Projections.ProjectionInfo) As String
        Dim lResult As String = ""
        'Unzip files to temp folder
        Dim lUnzipFolder As String = atcUtility.NewTempDir(IO.Path.Combine(IO.Path.GetDirectoryName(aCacheZipFile), aDataType.Tag))
        'Dim fz As New ICSharpCode.SharpZipLib.Zip.FastZip
        Try
            'fz.ExtractZip(lCacheZipFile, lUnzipFolder, FastZip.Overwrite.Always, Nothing, "", "", True)
            Zipper.UnzipFile(aCacheZipFile, lUnzipFolder)
        Catch exZip As Exception 'TODO: handle bad zip files uniformly in UnzipFile
            TryMove(aCacheZipFile, aCacheZipFile & ".bad")
            Throw New Exception("Could not unzip NLCD download '" & aCacheZipFile & "': " & exZip.Message)
        End Try
        'fz = Nothing

        Dim lFilenames As New Collections.Specialized.NameValueCollection
        AddFilesInDir(lFilenames, lUnzipFolder, True, "*.tif")
        If lFilenames.Count < 1 Then
            lResult &= "<message>No tif file found in " & aDataType.Tag & " download</message>"
        Else
            Dim lTifFilename As String = lFilenames(0)
            Dim lCreatedDir As String = IO.Path.GetDirectoryName(lTifFilename)
            Dim lFilename As String

            'Rename from random-numbered downloaded file name
            For Each lFilename In IO.Directory.GetFiles(lCreatedDir, IO.Path.GetFileNameWithoutExtension(lTifFilename) & ".*")
                TryMove(lFilename, IO.Path.Combine(lCreatedDir, aBaseFilename & IO.Path.GetExtension(lFilename)))
            Next

            lTifFilename = IO.Path.Combine(lCreatedDir, aBaseFilename & ".tif")

            'Rename metadata file to match new tif filename
            Dim lTifMetadataFilename As String = lTifFilename & ".xml"
            Dim lWrongMetadataFilename As String = IO.Path.Combine(lCreatedDir, "Metadata.xml")
            If IO.File.Exists(lWrongMetadataFilename) AndAlso Not IO.File.Exists(lTifMetadataFilename) Then
                TryMove(lWrongMetadataFilename, lTifMetadataFilename)
            End If

            Dim lLayer As New Layer(lTifFilename, aDataType, False)
            lLayer.CopyProcStepsFromCachedFile(aCacheZipFile)

            'Rename other files from archive so we can tell which layer they go with
            For Each lFilename In IO.Directory.GetFiles(lCreatedDir)
                If Not IO.Path.GetFileName(lFilename).ToLower.StartsWith(aBaseFilename.ToLower) Then
                    TryMove(lFilename, IO.Path.Combine(lCreatedDir, aBaseFilename & "_" & IO.Path.GetFileName(lFilename)))
                End If
            Next

            'Project if necessary
            If DesiredProjection IsNot Nothing AndAlso Not DesiredProjection.Equals(aLayerProjection) Then
                'TODO: SpatialOperations.ProjectAndClipGridLayer(lTifFilename, aLayerProjection, aProject.DesiredProjection, Nothing, Nothing)
                Logger.Dbg("Warning: Leaving layer in native projection: " & aBaseFilename & " (" & aLayerProjection.ToProj4String.Trim & ")")
                'Downloaded projection is not in correct format, rewrite it.
                IO.File.WriteAllText(IO.Path.ChangeExtension(lLayer.FileName, ".prj"), aLayerProjection.ToEsriString())
            End If

            'Move to destination
            IO.Directory.CreateDirectory(aSaveFolder)
            'Dim lDestinationLayerFilename As String = Path.Combine(lSaveFolder, Path.GetFileName(lTifFilename))
            lResult &= SpatialOperations.MergeLayers(New Generic.List(Of Layer) From {lLayer},
                                                     lCreatedDir,
                                                     aSaveFolder)
            'If IO.File.Exists(lDestinationLayerFilename) Then 'merge with existing layer
            'Else
            '    Dim lTifWorldFilename As String = Path.ChangeExtension(lTifFilename, ".tfw")
            '    TryMove(lTifFilename, lDestinationLayerFilename)
            '    TryMove(lTifWorldFilename, Path.Combine(lSaveFolder, Path.GetFileName(lTifWorldFilename)))
            '    TryMove(lTifMetadataFilename, Path.Combine(lSaveFolder, Path.GetFileName(lTifMetadataFilename)))
            '    TryMove(IO.Path.ChangeExtension(lTifFilename, ".prj"), IO.Path.ChangeExtension(lDestinationLayerFilename, ".prj"))
            '    lResult &= "<add_grid>" + lDestinationLayerFilename + "</add_grid>"
            'End If
            TryDelete(lUnzipFolder)

            Logger.Status("")
        End If
        Return lResult
    End Function
    Private Shared Function ProcessDownloadedZip(ByVal aProject As Project,
                                                 ByVal aSaveFolder As String,
                                                 ByVal aDataType As LayerSpecification,
                                                 ByVal aCacheZipFile As String,
                                                 ByVal aBaseFilename As String,
                                                 ByVal aLayerProjection As DotSpatial.Projections.ProjectionInfo) As String
        Dim lResult As String = ""
        'Unzip files to temp folder
        Dim lUnzipFolder As String = atcUtility.NewTempDir(IO.Path.Combine(IO.Path.GetDirectoryName(aCacheZipFile), aDataType.Tag))
        'Dim fz As New ICSharpCode.SharpZipLib.Zip.FastZip
        Try
            'fz.ExtractZip(lCacheZipFile, lUnzipFolder, FastZip.Overwrite.Always, Nothing, "", "", True)
            Zipper.UnzipFile(aCacheZipFile, lUnzipFolder)
        Catch exZip As Exception 'TODO: handle bad zip files uniformly in UnzipFile
            TryMove(aCacheZipFile, aCacheZipFile & ".bad")
            Throw New Exception("Could not unzip NLCD download '" & aCacheZipFile & "': " & exZip.Message)
        End Try
        'fz = Nothing

        Dim lFilenames As New Collections.Specialized.NameValueCollection
        AddFilesInDir(lFilenames, lUnzipFolder, True, "*.tif")
        If lFilenames.Count < 1 Then
            lResult &= "<message>No tif file found in " & aDataType.Tag & " download</message>"
        Else
            Dim lTifFilename As String = lFilenames(0)
            Dim lCreatedDir As String = IO.Path.GetDirectoryName(lTifFilename)
            Dim lFilename As String

            'Rename from random-numbered downloaded file name
            For Each lFilename In IO.Directory.GetFiles(lCreatedDir, IO.Path.GetFileNameWithoutExtension(lTifFilename) & ".*")
                Dim lExt As String = IO.Path.GetExtension(lFilename)
                If lExt = ".prj" Then 'Downloaded .prj is not in correct format, discard it
                    TryDelete(lFilename)
                Else
                    TryMove(lFilename, IO.Path.Combine(lCreatedDir, aBaseFilename & lExt))
                End If
            Next

            lTifFilename = IO.Path.Combine(lCreatedDir, aBaseFilename & ".tif")

            'Rename metadata file to match new tif filename
            Dim lTifMetadataFilename As String = lTifFilename & ".xml"
            Dim lWrongMetadataFilename As String = IO.Path.Combine(lCreatedDir, "Metadata.xml")
            If IO.File.Exists(lWrongMetadataFilename) AndAlso Not IO.File.Exists(lTifMetadataFilename) Then
                TryMove(lWrongMetadataFilename, lTifMetadataFilename)
            End If

            Dim lLayer As New Layer(lTifFilename, aDataType, False)
            lLayer.CopyProcStepsFromCachedFile(aCacheZipFile)

            'Rename other files from archive so we can tell which layer they go with
            For Each lFilename In IO.Directory.GetFiles(lCreatedDir)
                If Not IO.Path.GetFileName(lFilename).ToLower.StartsWith(aBaseFilename.ToLower) Then
                    TryMove(lFilename, IO.Path.Combine(lCreatedDir, aBaseFilename & "_" & IO.Path.GetFileName(lFilename)))
                End If
            Next

            'Project if necessary
            If aProject.DesiredProjection IsNot Nothing AndAlso Not aProject.DesiredProjection.Equals(aLayerProjection) Then
                'TODO: SpatialOperations.ProjectAndClipGridLayer(lTifFilename, aLayerProjection, aProject.DesiredProjection, Nothing, Nothing)
                Logger.Dbg("Warning: Leaving layer in native projection: " & aBaseFilename & " (" & aLayerProjection.ToProj4String.Trim & ")")
            End If
            'Downloaded projection is not in correct format, rewrite it.
            IO.File.WriteAllText(IO.Path.ChangeExtension(lLayer.FileName, ".prj"), aLayerProjection.ToEsriString())

            'Move to destination
            IO.Directory.CreateDirectory(aSaveFolder)
            'Dim lDestinationLayerFilename As String = Path.Combine(lSaveFolder, Path.GetFileName(lTifFilename))
            lResult &= SpatialOperations.MergeLayers(New Generic.List(Of Layer) From {lLayer},
                                                     lCreatedDir,
                                                     aSaveFolder)
            'If IO.File.Exists(lDestinationLayerFilename) Then 'merge with existing layer
            'Else
            '    Dim lTifWorldFilename As String = Path.ChangeExtension(lTifFilename, ".tfw")
            '    TryMove(lTifFilename, lDestinationLayerFilename)
            '    TryMove(lTifWorldFilename, Path.Combine(lSaveFolder, Path.GetFileName(lTifWorldFilename)))
            '    TryMove(lTifMetadataFilename, Path.Combine(lSaveFolder, Path.GetFileName(lTifMetadataFilename)))
            '    TryMove(IO.Path.ChangeExtension(lTifFilename, ".prj"), IO.Path.ChangeExtension(lDestinationLayerFilename, ".prj"))
            '    lResult &= "<add_grid>" + lDestinationLayerFilename + "</add_grid>"
            'End If
            TryDelete(lUnzipFolder)

            Logger.Status("")
        End If
        Return lResult
    End Function

    Private Shared Function InputValue(ByVal aFormElements() As String, ByVal aName As String) As String
        Dim lSearchFor As String = """" & aName & """"
        For Each lElement As String In aFormElements
            If lElement.Contains(aName) Then
                Dim lFindValue As Integer = lElement.IndexOf("VALUE")
                If lFindValue >= 0 Then
                    Dim lFindStartQuote As Integer = lElement.IndexOf("""", lFindValue)
                    If lFindStartQuote >= 0 Then
                        Dim lFindEndQuote As Integer = lElement.IndexOf("""", lFindStartQuote + 1)
                        If lFindEndQuote >= 0 Then
                            Return lElement.Substring(lFindStartQuote + 1, lFindEndQuote - lFindStartQuote - 1)
                        End If
                    End If
                End If
            End If
        Next
        Return ""
    End Function

    Private Shared Function InputValueToDouble(ByVal aFormElements() As String, ByVal aName As String, Optional ByVal aDefault As Double = 0) As Double
        Double.TryParse(InputValue(aFormElements, aName), aDefault)
        Return aDefault
    End Function

    Private Shared Function StripHTMLtags(ByVal aOriginal As String) As String
        Dim lReturn As String = ReadableFromXML(aOriginal)
        lReturn = lReturn.Replace("If an error occurs during download, click here to retrieve the download bundle.", "")
        lReturn = lReturn.Replace("After one hour, the download bundle will be automatically deleted from our server.", "")
        Return lReturn.Trim
    End Function
End Class