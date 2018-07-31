Imports System.IO
Imports System.Reflection
Imports atcUtility
Imports MapWinUtility

''' <summary>Soil properties at a specified location</summary>
''' <remarks>Requires application to have ServiceReference to USDA Soil Data Mart; See http://sdmdataaccess.nrcs.usda.gov/QueryHelp.aspx for documentation of SDM</remarks>
Public Class SoilLocation

    Public Shared SoilLayerSpecification As New LayerSpecification(
                FilePattern:="*.shp", Tag:="SSURGO", Role:=D4EM.Data.LayerSpecification.Roles.Soil, IdFieldName:="MuKey", Name:="Soil", Source:=GetType(SoilLocation))

    ''' <summary>Always split areas larger than this before attempting to download</summary>
    ''' <remarks>Failed to download a 1,317,400,000 square meter area, NRCS stated download limit is 10,100,000,000 sq meters</remarks>
    Public Shared AreaLimitPerRequestSqMeters As Double = 900000000

    ''' <summary>Stop trying to split when the area gets this small, must be some other problem</summary>
    Public Shared AreaMinimumSqMeters As Double = 1000000

    ''' <summary>Limit each shapefile to have at most this many polygons</summary>
    Public Shared MaxShapesPerFile As Integer = 10000

    ''' <summary>Latitude/longititude coordinates of a point on a soil polygon</summary>
    ''' <remarks></remarks>
    Public Class XYPair
        ''' <summary>Latitude of point</summary>
        Public Property Lat As Double 'Y
        ''' <summary>Longitude of point</summary>        
        Public Property Lng As Double 'X

        Public Sub New(ByVal aLatitude As Double, ByVal aLongitude As Double)
            Lat = aLatitude
            Lng = aLongitude
        End Sub
    End Class

    ' ''' <summary>Polygon associated with a set of soil properties</summary>
    ' ''' <remarks></remarks>
    'Public Class SoilPolygon
    '    Public Property Text As String
    '    ' ''' <summary>Point pairs which describe polygon</summary>        
    '    'Public Property Coord As New List(Of XYPair)

    '    ''' <summary>Inner or outer flag</summary>        
    '    ''' <remarks>Allows islands</remarks>
    '    Public Property Outer As Boolean = True

    '    Public Sub New(ByVal aText As String)
    '        Text = aText
    '    End Sub

    '    Public Function ToCoordinates() As Generic.List(Of DotSpatial.Topology.Coordinate)
    '        Dim lCoordinates As New Generic.List(Of DotSpatial.Topology.Coordinate)
    '        Dim lLatitude As Double
    '        Dim lLongitude As Double
    '        Dim lLonLat() As String
    '        Dim lPoints() As String = Text.Split(" ")
    '        For Each lPoint In lPoints
    '            lLonLat = lPoint.Split(",")
    '            If lLonLat.Length = 2 AndAlso
    '                Double.TryParse(lLonLat(0), lLongitude) AndAlso
    '                Double.TryParse(lLonLat(1), lLatitude) Then
    '                lCoordinates.Add(New DotSpatial.Topology.Coordinate(lLongitude, lLatitude))
    '            Else
    '                Throw New ApplicationException("Polygon coordinates not found in " & lPoint)
    '            End If
    '        Next
    '        If lCoordinates.Count > 0 Then lCoordinates.Add(lCoordinates(0)) 'Close the polygon
    '        Return lCoordinates
    '    End Function

    '    'Public Function ToShape() As DotSpatial.Data.Shape
    '    '    Dim lShape As New DotSpatial.Data.Shape(DotSpatial.Topology.FeatureType.Polygon)
    '    '    lShape.AddPart(ToCoordinates, DotSpatial.Data.CoordinateType.Regular)
    '    '    Return lShape
    '    'End Function
    'End Class

    Public Class SoilPolygons
        Implements IDisposable
        Private pDirectoryName As String

        Public Sub New()
            Me.New("")
        End Sub

        Public Sub New(ByVal ID As String)
            pDirectoryName = "S:\Temp\Soil\Polygons" & ID & "_2" & g_PathChar
            If Not IO.Directory.Exists(pDirectoryName) Then
                pDirectoryName = atcUtility.GetTemporaryFileName("Soil" & g_PathChar & "Polygons" & ID, "") & g_PathChar
            End If
            IO.Directory.CreateDirectory(pDirectoryName)
        End Sub

        Sub Dispose() Implements IDisposable.Dispose
            TryDelete(pDirectoryName)
        End Sub

        Public Sub AddInner(ByVal aTextCoordinates As String)
            Add("I" & aTextCoordinates)
        End Sub

        Public Sub AddOuter(ByVal aTextCoordinates As String)
            Add("O" & aTextCoordinates)
        End Sub

        Private Sub Add(ByVal aTextCoordinates As String)
            Dim lSpacePos = aTextCoordinates.IndexOf(" ")
            Select Case lSpacePos
                Case -1, Is > 25 : lSpacePos = 10
            End Select
            Dim lFilename As String = pDirectoryName & SafeSubstring(aTextCoordinates, 0, lSpacePos)
            If IO.File.Exists(lFilename) Then
                For Each lLine As String In LinesInFile(lFilename)
                    If lLine.Equals(aTextCoordinates) Then
                        Exit Sub
                    End If
                Next
                Logger.Dbg("Adding polygon to " & lFilename)
            End If
            IO.File.AppendAllText(lFilename, aTextCoordinates & vbCrLf)
        End Sub

        Public Function Files() As String()
            Return IO.Directory.GetFiles(pDirectoryName)
        End Function

        Public Shared Function ToCoordinates(ByVal aLine As String) As Generic.List(Of DotSpatial.Topology.Coordinate)
            Dim lInner As Boolean = False
            Select Case aLine.Substring(0, 1)
                Case "O" : lInner = False : aLine = aLine.Substring(1)
                Case "I" : lInner = True : aLine = aLine.Substring(1)
            End Select
            Dim lCoordinates As New Generic.List(Of DotSpatial.Topology.Coordinate)
            Dim lLatitude As Double
            Dim lLongitude As Double
            Dim lLonLat() As String
            Dim lPoints() As String = aLine.Split(" ")
            If lInner Then lPoints.Reverse()
            For Each lPoint In lPoints
                lLonLat = lPoint.Split(",")
                If lLonLat.Length = 2 AndAlso
                    Double.TryParse(lLonLat(0), lLongitude) AndAlso
                    Double.TryParse(lLonLat(1), lLatitude) Then
                    lCoordinates.Add(New DotSpatial.Topology.Coordinate(lLongitude, lLatitude))
                ElseIf Not String.IsNullOrWhiteSpace(lPoint) Then
                    Logger.Dbg("Polygon coordinates not found in " & lPoint)
                End If
            Next
            If lCoordinates.Count > 0 Then lCoordinates.Add(lCoordinates(0)) 'Close the polygon
            Return lCoordinates
        End Function

    End Class

    ''' <summary>Soil layer properties</summary>
    ''' <remarks>Subset of properties available in the USDA Soil Data Mart</remarks>
    Public Class SoilLayer
        ''' <summary>Depth to top of layer</summary>        
        Public Property DepthToTop As Double
        ''' <summary>Depth to bottom of layer</summary>        
        Public Property DepthToBottom As Double
        ''' <summary>Saturated Hydraulic Conductivity (micrometers/second) depth weighted</summary>        
        Public Property KSAT As Double
        ''' <summary>Hydrologic Soil Group (A, B, C or D), blank if not available</summary>        
        Public Property HSG As String
        ''' <summary>Representative slope in %</summary>        
        Public Property Slope_R As Double
        ''' <summary></summary>        
        Public Property CompPct_R As Integer
    End Class

    ''' <summary>Class to store details of each soil key</summary>
    Public Class Soil
        Implements IComparable

        Public Property MuKey As String 'SoilDataAccess MuKey, eg 124246
        Public Property AreaSymbol As String 'SoilDataAccess AreaSymbol, eg GA089
        Public Property MuSym As String 'SoilDataAccess MuSym, eg CuC
        Public Property HSG As String = "" 'Hydrologic soil group (A, B, C, D or combination A/D), blank if not available
        Public Property ObjectID As String '13936396
        Public Property NationalMuSym As String '458y
        Public Property Polygons As SoilPolygons ' New List(Of SoilPolygon) 'Polygons describing spatial extent of soil
        Public Property Components As New List(Of SoilComponent)

        'Public Property KSAT_Surface As Double 'Saturated Hydraulic Conductivity (micrometers/second) in top horizon
        'Public Property KSAT As Double 'Saturated Hydraulic Conductivity (micrometers/second) depth weighted
        'Public Property Slope_R As Double 'Representative slope in %
        Public Property Layers As List(Of SoilLayer) 'Layers associated with soil

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Return MuKey.CompareTo(obj.MuKey)
        End Function
    End Class

    Public Class SoilList
        Inherits System.Collections.Generic.List(Of Soil)

        Public Function FindSoil(ByVal aMuKey) As Soil
            For Each lUniqueSoil As Soil In Me
                If lUniqueSoil.MuKey = aMuKey Then
                    Return lUniqueSoil
                End If
            Next
            Return Nothing
        End Function
    End Class

    Public Class SoilComponent
        Public Property cokey As String = "" 'component key, 124246:177287 or 390453:484578
        Public Property HSG As String = "" 'hydrologic soil group, A B C D, blank if not available
        Public Property albedodry_r As Double = 0.0 'soil Albedo
        Public Property comppct_r As Double = 0.0 'component percent
        Public Property compname As String = "" 'component name, eg Urban land
        Public Property saversion As String = "" 'component version, 5
        Public Property saverest As String = "" 'component save time, 10/9/2008 6:19:08 AM
        Public Property IsDominant As Boolean = False
        Public Property chorizons As New List(Of SoilHorizon)
        Public Property sname As String
    End Class

    Public Class SoilHorizon 'chorizon
        Public Property chkey As String = "" 'component key, 397681
        Public Property hzdepb_r As Double = 0.0 'z
        Public Property dbovendry_r As Double = 0.0 'bd
        Public Property awc_r As Double = 0.0 'awc
        Public Property ksat_r As Double = 0.0 'ksat
        Public Property om_r As Double = 0.0 'cbd
        Public Property claytotal_r As Double = 0.0 'clay%
        Public Property silttotal_r As Double = 0.0 'silt%
        Public Property sandtotal_r As Double = 0.0 'sand%
        Public Property rockpct As Double = 0.0 'rock%
        Public Property kffact As Double = 0.0 'kffact
        Public Property ec_r As Double = 0.0 'ec
        Public Property anion_excl As Double = 0.5 'anion content
        Public Property soilcrk As Double = 0.5 'soil fissures
        Public Property texture As String = "" 'texture description
    End Class

    ''' <summary>
    ''' Get soil polygons from NRCS Soil Data Mart
    ''' Create shapefile and add to aProject.Layers.
    ''' </summary>
    ''' <param name="aProject">Project's Region determines bounding box of query</param>
    ''' <param name="aSaveFolder">Sub-folder within aProject.ProjectFolder (e.g. "NRCS_Soil") or full path of folder to save in (e.g. "C:\NRCS_Soil").
    '''  If nothing or empty string, will save in aProject.ProjectFolder.</param>
    ''' <returns>List of soils found within the bounding box of aProject.Region</returns>
    ''' <remarks>NRCS_Soil folder within aProject.CacheFolder is used to cache query results</remarks>
    Public Shared Function GetSoils(ByVal aProject As Project,
                                    ByVal aSaveFolder As String) As List(Of Soil)
        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lCacheFolder As String = IO.Path.Combine(aProject.CacheFolder, "NRCS_Soil")
        Dim lWest As Double, lSouth As Double, lEast As Double, lNorth As Double
        aProject.Region.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)

        Dim lSoils As SoilList = Nothing
        Try
            GetSoilsMultiPart(lWest, lSouth, lEast, lNorth, lCacheFolder, lSoils, 0, 0)
            Logger.Status("(LABEL MIDDLE)")

            PopulateSoils(lSoils, lCacheFolder)

            Dim lSoilsShapefilename As String = IO.Path.Combine(lSaveIn, "SSURGO.shp")
            If SaveShapefile(lSoils, lSoilsShapefilename, aProject.DesiredProjection) Then
                Dim lSoilLayer As New D4EM.Data.Layer(lSoilsShapefilename, SoilLayerSpecification, False)
                aProject.Layers.Add(lSoilLayer)
            End If
        Catch Aex As ApplicationException
            lSoils = Nothing
            Throw Aex
        Catch lEx As Exception
            Logger.Dbg("Unable to parse returned xml:" & lEx.ToString)
        End Try

        Return lSoils
    End Function

    Private Shared Sub GetSoilsMultiPart(ByVal aWest As Double, ByVal aSouth As Double, ByVal aEast As Double, ByVal aNorth As Double, ByVal aCacheFolder As String, ByRef aSoils As SoilList, ByRef aTotalArea As Double, ByRef aAreaProcessed As Double)
        If aSoils Is Nothing Then aSoils = New SoilList

        Dim lWestMeters As Double, lSouthMeters As Double, lEastMeters As Double, lNorthMeters As Double
        Dim lRegion As New D4EM.Data.Region(aNorth, aSouth, aWest, aEast, Globals.GeographicProjection)
        lRegion.GetBounds(lNorthMeters, lSouthMeters, lWestMeters, lEastMeters, Globals.AlbersProjection)
        Dim lEastWest = lEastMeters - lWestMeters
        Dim lNorthSouth = lNorthMeters - lSouthMeters
        Dim lAreaMeters = lEastWest * lNorthSouth

        If aTotalArea = 0 Then aTotalArea = lAreaMeters

        Dim lFilter As String = aWest & "," & aSouth & "," & aEast & "," & aNorth
        Dim lCacheFile As String = IO.Path.Combine(aCacheFolder, "NRCS_Soil_W,S,E,N=" & lFilter & ".xml")

        If IO.File.Exists(lCacheFile) Then
            Logger.Dbg("Using cached results in " & lCacheFile)
        Else
            Dim lDownloaded As Boolean = False
            If lAreaMeters > AreaLimitPerRequestSqMeters Then
SplitIt:        If lEastWest > lNorthSouth Then
                    Dim lHalfway = (aEast + aWest) / 2
                    Logger.Dbg("Splitting " & DoubleToString(lAreaMeters, 14) & " square meter area in half east-west at " & DoubleToString(lHalfway))
                    GetSoilsMultiPart(aWest, aSouth, lHalfway, aNorth, aCacheFolder, aSoils, aTotalArea, aAreaProcessed)
                    GetSoilsMultiPart(lHalfway, aSouth, aEast, aNorth, aCacheFolder, aSoils, aTotalArea, aAreaProcessed)
                Else
                    Dim lHalfway = (aNorth + aSouth) / 2
                    Logger.Dbg("Splitting " & DoubleToString(lAreaMeters, 14) & " square meter area in half north-south at " & DoubleToString(lHalfway))
                    GetSoilsMultiPart(aWest, aSouth, aEast, lHalfway, aCacheFolder, aSoils, aTotalArea, aAreaProcessed)
                    GetSoilsMultiPart(aWest, lHalfway, aEast, aNorth, aCacheFolder, aSoils, aTotalArea, aAreaProcessed)
                End If
            Else
                Using lLevel As New ProgressLevel(True)
                    Logger.Status("Attempting soil download of " & DoubleToString(lAreaMeters, 14) & " square meters")
                    D4EM.Data.Download.SetSecurityProtocol()
                    Dim lURL As String = "http://sdmdataaccess.nrcs.usda.gov/Spatial/SDMNAD83Geographic.wfs?Service=WFS&Version=1.0.0&Request=GetFeature&Typename=MapunitPoly&BBOX=" & lFilter
                    If Not DownloadURL(lURL, lCacheFile) Then
                        If lAreaMeters > 10000000 Then 'Maybe an even smaller area will help?
                            Logger.Dbg("Unable to download soil data from " & lURL & " so splitting area and reducing area limit from " & DoubleToString(AreaLimitPerRequestSqMeters, 14) & " to " & DoubleToString(lAreaMeters / 2, 14))
                            AreaLimitPerRequestSqMeters = lAreaMeters / 2
                            GoTo SplitIt
                        End If
                        Throw New ApplicationException("Unable to download soil data from " & lURL & " to " & lCacheFile)
                    End If
                End Using
            End If
        End If

        If IO.File.Exists(lCacheFile) Then 'Either already cached or just downloaded without splitting area
            Dim lSoilCountBefore = aSoils.Count
            ParseSoilFeatures(IO.File.ReadAllText(lCacheFile), aSoils)
            If lSoilCountBefore = 0 Then
                Logger.Dbg("Found " & aSoils.Count & " soils")
            Else
                Logger.Dbg("Found " & aSoils.Count - lSoilCountBefore & " additional soils, now have " & aSoils.Count)
            End If
            aAreaProcessed += lAreaMeters
            Dim lProgress As Integer = aAreaProcessed / aTotalArea * 10000
            Dim lProgressMessage As String = "NRCS Area " & DoubleToString(aAreaProcessed, 14) & " sq meters of " & DoubleToString(aTotalArea, 14)
            Logger.Dbg(lProgressMessage & " (" & DoubleToString(lProgress / 100) & "%)")
            Logger.Progress(lProgressMessage, lProgress, 10000)
            Logger.Status("(LABEL MIDDLE " & CInt(lProgress / 100) & "%)")
        End If

    End Sub

    ''' <summary>
    ''' Get soil polygons from NRCS Soil Data Mart
    ''' Create shapefile and add to aProject.Layers.
    ''' </summary>
    ''' <param name="aProjFolder">Project Folder</param>
    ''' <param name="aSaveFolder">Sub-folder within aProjFolder (e.g. "NRCS_Soil") or full path of folder to save in (e.g. "C:\NRCS_Soil").
    '''  If nothing or empty string, will save in aProjFolder.</param>
    ''' <param name="aRegionToClip">bounding box of query</param>
    ''' <returns>List of soils found within the bounding box of aRegionToClip</returns>
    ''' <remarks>NRCS_Soil folder within aCacheFolder is used to cache query results</remarks>
    Public Shared Function GetSoils(ByVal aProjFolder As String,
                                    ByVal aCacheFolder As String,
                                    ByVal aSaveFolder As String,
                                    ByVal aRegionToClip As Region,
                                    ByVal desiredProjection As DotSpatial.Projections.ProjectionInfo) As List(Of Soil)
        Dim lSaveIn As String = aProjFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        Dim lCacheFolder As String = IO.Path.Combine(aCacheFolder, "NRCS_Soil")
        Dim lEast As Double, lWest As Double, lNorth As Double, lSouth As Double
        aRegionToClip.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)

        Dim lFilter As String = lWest & "," & lSouth & "," & lEast & "," & lNorth
        Dim lCacheFile As String = IO.Path.Combine(lCacheFolder, "NRCS_Soil_W,S,E,N=" & lFilter & ".xml")

        If IO.File.Exists(lCacheFile) Then
            Logger.Dbg("Using cached results in " & lCacheFile)
        Else
            D4EM.Data.Download.SetSecurityProtocol()
            Dim lURL As String = "http://sdmdataaccess.nrcs.usda.gov/Spatial/SDMNAD83Geographic.wfs?Service=WFS&Version=1.0.0&Request=GetFeature&Typename=MapunitPoly&BBOX=" & lFilter
            If Not DownloadURL(lURL, lCacheFile) Then
                Throw New ApplicationException("Unable to download soil data from " & lURL & " to " & lCacheFile)
            End If
        End If
        Dim lSoils As SoilList = Nothing
        Dim lXMLdoc As New Xml.XmlDocument
        Try
            ParseSoilFeatures(IO.File.ReadAllText(lCacheFile), lSoils)
            Logger.Dbg("Found " & lSoils.Count & " soils")

            PopulateSoils(lSoils, lCacheFolder)

            Dim lSoilsShapefilename As String = IO.Path.Combine(lSaveIn, "SSURGO.shp")
            If SaveShapefile(lSoils, lSoilsShapefilename, desiredProjection) Then
                'Dim lSoilLayer As New D4EM.Data.Layer(lSoilsShapefilename, SoilLayerSpecification, False)
                'aProject.Layers.Add(lSoilLayer)
            End If
        Catch lEx As Exception
            Logger.Dbg("Unable to parse returned xml:" & lEx.ToString)
        End Try

        Return lSoils
    End Function


    ' ''' <summary>Find soils at/near specified location</summary>
    ' ''' <param name="aNorthLatitude">Latitude (decimal degrees)</param>
    ' ''' <param name="aSouthLatitude">Longitude (decimal degrees)</param>
    ' ''' <param name="aWestLongitude">Initial radius used to limit search for soils</param>
    ' ''' <param name="aEastLongitude">Maximum radius to search for soils</param>
    ' ''' <returns>List of Soils</returns>
    ' ''' <remarks></remarks>
    'Public Shared Function FindSoilsByBox(ByVal aNorthLatitude As Double,
    '                                      ByVal aSouthLatitude As Double, _
    '                                      ByVal aWestLongitude As Double, _
    '                                      ByVal aEastLongitude As Double) As List(Of Soil)

    '    Dim lFoundValidHSG As Boolean = False
    '    Dim lFilter As String = aWestLongitude & "," & aSouthLatitude & "," & aEastLongitude & "," & aNorthLatitude
    '    Dim lURL As String = "http://sdmdataaccess.nrcs.usda.gov/Spatial/SDMNAD83Geographic.wfs?Service=WFS&Version=1.0.0&Request=GetFeature&Typename=MapunitPoly&BBOX=" & lFilter
    '    Dim lResultXml As String = D4EM.Data.Download.DownloadURL(lURL)
    '    Dim lSoils As SoilList = Nothing
    '    Dim lXMLdoc As New Xml.XmlDocument
    '    Try
    '        lXMLdoc.LoadXml(lResultXml)
    '        ParseSoilFeatures(lXMLdoc, lSoils)
    '        Logger.Dbg("Found " & lSoils.Count & " soils")
    '        PopulateSoils(lSoils)
    '        Return lSoils
    '    Catch lEx As Exception
    '        Logger.Dbg("Unable to parse returned xml:" & lEx.ToString & vbCrLf & lResultXml)
    '    End Try
    '    Return Nothing
    'End Function

    ''' <summary>Find soils at/near specified location</summary>
    ''' <param name="aLatitude">Latitude (decimal degrees)</param>
    ''' <param name="aLongitude">Longitude (decimal degrees)</param>
    ''' <param name="aRadiusInitial">Initial radius used to limit search for soils</param>
    ''' <param name="aRadiusMax">Maximum radius to search for soils</param>
    ''' <param name="aRadiusIncrement">Amount to increase radius if no soils found</param>
    ''' <returns>List of Soils</returns>
    ''' <remarks></remarks>
    Public Shared Function FindSoils(ByVal aLatitude As Double,
                                     ByVal aLongitude As Double,
                            Optional ByVal aRadiusInitial As Double = 1000,
                            Optional ByVal aRadiusMax As Double = 1000,
                            Optional ByVal aRadiusIncrement As Double = 1000) As List(Of Soil)

        'Dim lAssemblyName As String = Assembly.GetEntryAssembly.GetName().Name
        'Dim lConfigFileName As String = lAssemblyName & ".exe.config"
        'If Not IO.File.Exists(lConfigFileName) Then
        '    SaveFileString(lConfigFileName, GetEmbeddedFileAsString(lConfigFileName, "D4EMLite." & lConfigFileName))
        '    Logger.Msg("Building Missing Configuration File " & lConfigFileName & vbCrLf & vbCrLf & lAssemblyName & " must be restarted!", vbCritical)
        '    Throw New ApplicationException("Built Missing Configuration File.  Please Restart " & lAssemblyName)
        'End If

        Logger.Dbg("about to query soil data for key at " & aLatitude & " " & aLongitude & _
                   " " & aRadiusInitial & " " & aRadiusIncrement & " " & aRadiusMax)
        Dim lDistance As Double = aRadiusInitial
        Dim lDistanceMax As Double = aRadiusMax
        If lDistanceMax < lDistance Then
            lDistanceMax = lDistance
            Logger.Dbg("RadiusIncrement Reset to " & lDistanceMax)
        End If
        Dim lDistanceIncrement As Double = aRadiusIncrement
        If lDistanceIncrement < ((aRadiusMax - aRadiusInitial) / 4) Then
            lDistanceIncrement = (aRadiusMax - aRadiusInitial) / 4
            Logger.Dbg("RadiusIncrement Reset to " & lDistanceIncrement & " for performance")
        End If
        Dim lFoundValidHSG As Boolean = False
        Do Until lFoundValidHSG
            D4EM.Data.Download.SetSecurityProtocol()
            Dim lFilter As String = "<Filter>" _
                                  & "  <DWithin>" _
                                  & "    <PropertyName>Geometry</PropertyName>" _
                                  & "    <gml:Point>" _
                                  & "       <gml:coordinates>" & aLongitude.ToString & "," & aLatitude.ToString & "</gml:coordinates>" _
                                  & "    </gml:Point>" _
                                  & "    <Distance%20units='m'>" & lDistance.ToString & "</Distance>" _
                                  & "  </DWithin>" _
                                  & "</Filter>"""
            Dim lURL As String = "http://sdmdataaccess.nrcs.usda.gov/Spatial/SDMNAD83Geographic.wfs?Service=WFS&Version=1.0.0&Request=GetFeature&Typename=MapunitPoly&Filter=" & lFilter
            Dim lResultXml As String = D4EM.Data.Download.DownloadURL(lURL)
            Logger.Dbg("returned from soil data query at distance " & lDistance.ToString & " with xml of length " & lResultXml.Length)

            Try
                Dim lSoils As SoilList = Nothing
                ParseSoilFeatures(lResultXml, lSoils)
                Logger.Dbg("Found " & lSoils.Count & " soils")
                Dim lUniqueLayerProperties = ComputeUniqueLayerProperties(lSoils, lFoundValidHSG)
                If lFoundValidHSG Then Return lSoils
            Catch lEx As Exception
                Logger.Dbg("Unable to parse returned xml:" & lEx.ToString & vbCrLf & lResultXml)
            End Try

            If lDistance >= lDistanceMax Then
                If Not lFoundValidHSG Then
                    Logger.Dbg("Unable to find any soil/hsgs within " & lDistanceMax & "m")
                    lFoundValidHSG = True 'just set to true to get out of loop
                End If
            Else
                lDistance += lDistanceIncrement
            End If
        Loop
        Return Nothing
    End Function

    Private Shared Sub ParseSoilFeatures(ByVal aXML As String, ByRef aSoils As SoilList)
        'Ensure that all tags we will be looking for are capitalized the way we expect them to be
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "gml:featureMember", "gml:featureMember", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "ms:MUKEY", "ms:MUKEY", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "ms:MUSYM", "ms:MUSYM", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "ms:AREASYMBOL", "ms:AREASYMBOL", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "ms:NationalMuSym", "ms:NationalMuSym", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "ms:OBJECTID", "ms:OBJECTID", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        aXML = Microsoft.VisualBasic.Strings.Replace(aXML, "gml:outerBoundaryIs", "gml:outerBoundaryIs", Compare:=Microsoft.VisualBasic.CompareMethod.Text)
        Dim lXMLdoc As New Xml.XmlDocument
        lXMLdoc.LoadXml(aXML)
        If aSoils Is Nothing Then aSoils = New SoilList
        Dim lDuplicatePolygons As Integer = 0
        Dim lLastFeatureIndex As Integer = lXMLdoc.GetElementsByTagName("gml:featureMember").Count - 1
        Using lLevel As New ProgressLevel(True)
            Logger.Status("Parsing Features")
            For lFeatureIndex As Integer = 0 To lLastFeatureIndex
                Dim lXMLdocFeature As New Xml.XmlDocument
                lXMLdocFeature.LoadXml(lXMLdoc.GetElementsByTagName("gml:featureMember").Item(lFeatureIndex).InnerXml)
                Dim lLastSoilIndex As Integer = lXMLdocFeature.GetElementsByTagName("ms:MUKEY").Count - 1
                For lIndex As Integer = 0 To lLastSoilIndex
                    Dim lMuKey As String = lXMLdocFeature.GetElementsByTagName("ms:MUKEY").Item(lIndex).InnerText
                    Dim lSoil As Soil = aSoils.FindSoil(lMuKey)
                    If lSoil Is Nothing Then
                        lSoil = New Soil
                        lSoil.MuKey = lMuKey
                        lSoil.Polygons = New SoilPolygons(lMuKey)
                        Try
                            lSoil.AreaSymbol = lXMLdocFeature.GetElementsByTagName("ms:AREASYMBOL").Item(lIndex).InnerText
                            lSoil.MuSym = lXMLdocFeature.GetElementsByTagName("ms:MUSYM").Item(lIndex).InnerText
                            lSoil.NationalMuSym = lXMLdocFeature.GetElementsByTagName("ms:NationalMuSym").Item(lIndex).InnerText
                            lSoil.ObjectID = lXMLdocFeature.GetElementsByTagName("ms:OBJECTID").Item(lIndex).InnerText
                        Catch eXML As Exception
                            Logger.Dbg(eXML.Message & " setting properties of soil " & lMuKey)
                        End Try
                        aSoils.Add(lSoil)
                    End If
                    If lSoil.AreaSymbol = "W" Then
                        'Skip water polygons, they can be extremely large
                    Else
                        Dim lPolygonXML As String = lXMLdocFeature.GetElementsByTagName("gml:outerBoundaryIs").Item(lIndex).InnerText
                        Try
                            lSoil.Polygons.AddOuter(lPolygonXML)
                            For Each lInnerBoundary As Xml.XmlElement In lXMLdocFeature.GetElementsByTagName("gml:innerBoundaryIs")
                                lSoil.Polygons.AddInner(lInnerBoundary.InnerText)
                            Next
                        Catch e As Exception
                            Logger.Dbg(e.Message & " in " & lPolygonXML)
                        End Try
                    End If

                    If Logger.Canceled Then
                        Throw New System.ApplicationException("User cancelled after parsing " & lFeatureIndex & " of " & lLastFeatureIndex & " features")
                    End If
                Next
                Logger.Progress(lFeatureIndex, lLastFeatureIndex)
            Next
        End Using
        If lDuplicatePolygons > 0 Then Logger.Dbg("Skipped " & lDuplicatePolygons & " duplicate polygons while parsing " & lLastFeatureIndex + 1 & " features")
    End Sub

    Private Shared Function ComputeUniqueLayerProperties(ByVal aSoils As List(Of Soil), ByRef FoundValidHSG As Boolean) As SortedList(Of String, Soil)
        Dim lUniqueLayerProperties As New SortedList(Of String, Soil)
        For Each lSoil As Soil In aSoils
            If lUniqueLayerProperties.ContainsKey(lSoil.MuKey) Then
                Logger.Dbg("UseExistingPropertiesFor " & lSoil.MuKey)
                lSoil.Layers = lUniqueLayerProperties.Item(lSoil.MuKey).Layers
            Else
                lSoil.Layers = FindSoilLayerProperties(lSoil.MuKey, lSoil.AreaSymbol).Values.ToList
                lUniqueLayerProperties.Add(lSoil.MuKey, lSoil)
            End If
            'determine dominant component
            Dim lDomPct As Integer = 0
            For Each lSoilLayer As SoilLayer In lSoil.Layers
                If lSoilLayer.CompPct_R > lDomPct Then
                    lDomPct = lSoilLayer.CompPct_R
                End If
            Next
            Dim lDepthTotal As Double = 0.0
            Dim lKSATTotal As Double = 0.0
            For Each lSoilLayer As SoilLayer In lSoil.Layers
                If lSoilLayer.CompPct_R = lDomPct Then
                    'only use dominant component in weighted average calculation
                    Dim lDepthNow As Double = lSoilLayer.DepthToBottom - lSoilLayer.DepthToTop
                    lDepthTotal += lDepthNow
                    lKSATTotal += (lDepthNow * lSoilLayer.KSAT)
                    If lSoilLayer.DepthToTop = 0 Then
                        'lSoil.KSAT_Surface = lSoilLayer.KSAT
                    End If
                    'lSoil.Slope_R = lSoilLayer.Slope_R
                End If
                If lSoilLayer.DepthToTop = 0 Then
                    FoundValidHSG = True
                    lSoil.HSG = lSoilLayer.HSG
                End If
            Next
            If lDepthTotal > 0 Then
                'lSoil.KSAT = lKSATTotal / lDepthTotal
            Else
                'lSoil.KSAT = -999
                'lSoil.KSAT_Surface = -999
            End If
            'Logger.Dbg("area symbol '" & lSoil.Area & "', musym '" & lSoil.Symbol & "', mukey '" & lSoil.Key & "'" & " at distance " & lDistance.ToString)
        Next
        Return lUniqueLayerProperties
    End Function

    Private Shared Sub PopulateSoils(ByVal aSoils As List(Of Soil), ByVal aCacheFolder As String)
        Dim lSoap As NRCS_Service.SDMTabularServiceSoapClient = Nothing
        For Each lSoil As Soil In aSoils
            If Not String.IsNullOrEmpty(lSoil.MuKey) Then
                Dim lCacheFilename As String = IO.Path.Combine(aCacheFolder, "SSURGO_" & lSoil.MuKey & ".xml")
                Dim lSoilTable As System.Data.DataTable = Nothing
                If IO.File.Exists(lCacheFilename) Then
                    Try
                        lSoilTable = New System.Data.DataTable
                        lSoilTable.ReadXml(lCacheFilename)
                    Catch eReadTable As Exception
                        Logger.Dbg("Error: Unable to read cached file " & lCacheFilename & ": " & eReadTable.ToString)
                        lSoilTable.Dispose()
                        lSoilTable = Nothing
                        TryDelete(lCacheFilename)
                    End Try
                End If
                If lSoilTable Is Nothing Then
                    If lSoap Is Nothing Then
                        Try
                            lSoap = New NRCS_Service.SDMTabularServiceSoapClient
                        Catch ex As Exception
                            Logger.Dbg("Error: Unable to create NRCS.SDMTabularServiceSoapClient: " & ex.ToString)
                        End Try
                    End If
                    If lSoap IsNot Nothing Then
                        Dim lSystemDataSet = RunSoilQuery(lSoil.MuKey, lSoap)
                        If lSystemDataSet IsNot Nothing AndAlso lSystemDataSet.Tables.Count = 1 Then
                            lSoilTable = lSystemDataSet.Tables(0)
                            Try
                                lSoilTable.WriteXml(lCacheFilename, System.Data.XmlWriteMode.WriteSchema)
                                Layer.AddProcessStepToFile("Retrieved via NRCS.SDMTabularServiceSoapClient", lCacheFilename)
                            Catch eWriteCache As Exception
                                Logger.Dbg("Unable to write cache file " & lCacheFilename & ": " & eWriteCache.ToString)
                            End Try
                        End If
                    End If
                End If

                If lSoilTable IsNot Nothing Then
                    SetSoilProperties(lSoil, lSoilTable)
                    Dim lDomComponent As SoilComponent = Nothing
                    For Each lComponent As SoilComponent In lSoil.Components
                        If lDomComponent Is Nothing OrElse lComponent.comppct_r > lDomComponent.comppct_r Then
                            lDomComponent = lComponent
                            If Not String.IsNullOrEmpty(lComponent.HSG) Then
                                lSoil.HSG = lComponent.HSG 'Prefer HSG of dominant component if set
                            End If
                        ElseIf Not String.IsNullOrEmpty(lComponent.HSG) Then
                            If String.IsNullOrEmpty(lSoil.HSG) Then
                                lSoil.HSG = lComponent.HSG 'Use HSG of non-dominant component if dominant does not have HSG
                            ElseIf lSoil.HSG <> lComponent.HSG Then
                                'Logger.Dbg("Different HSG in components of MuKey " & lSoil.MuKey & " (" & lSoil.HSG & ", " & lComponent.HSG & ")")
                            End If
                        End If
                    Next
                    If lDomComponent IsNot Nothing Then
                        lDomComponent.IsDominant = True
                    Else
                        Logger.Dbg("No components of MuKey " & lSoil.MuKey)
                    End If
                Else
                    Logger.Dbg("No soil data found for MuKey " & lSoil.MuKey)
                End If
            End If
            If String.IsNullOrEmpty(lSoil.HSG) Then
                Logger.Dbg("No HSG for MuKey " & lSoil.MuKey)
            End If
        Next

        If lSoap IsNot Nothing Then lSoap.Close()
    End Sub

    Public Shared Function SaveShapefile(ByVal aSoils As List(Of Soil), ByVal aLayerFilename As String, ByVal aProjection As DotSpatial.Projections.ProjectionInfo) As Boolean
        MkDirPath(PathNameOnly(aLayerFilename))
        Dim lShapeFileName As String
        Dim lFeatureSet As DotSpatial.Data.FeatureSet = Nothing
        Dim lLast As Integer = aSoils.Count
        Dim lCurrent As Integer = 0
        Dim lNextProgress As Date = Now.AddSeconds(10)
        For Each lSoil In aSoils
            If lFeatureSet Is Nothing Then
                lFeatureSet = New DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Polygon)
                lFeatureSet.Filename = atcUtility.GetTemporaryFileName(IO.Path.ChangeExtension(aLayerFilename, "").TrimEnd("."), ".shp")
                Logger.Dbg("Building " & lFeatureSet.Filename)
                lShapeFileName = lFeatureSet.Filename
                lFeatureSet.Projection = Globals.GeographicProjection

                lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("MuKey", "C", 8, 0))
                lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("HSG", "C", 4, 0))
                lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("AreaSymbol", "C", 8, 0))
                lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("MuSym", "C", 8, 0))
                lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("NationalMuSym", "C", 8, 0))
            End If

            For Each lPolygonFilename As String In lSoil.Polygons.Files
                Dim lLines = IO.File.ReadAllLines(lPolygonFilename) 'As IEnumerable = LinesInFile(lPolygonFilename)
                For Each lPolygonString As String In lLines
                    If lPolygonString.StartsWith("O") Then
                        Dim lCoordinates = SoilPolygons.ToCoordinates(lPolygonString)
                        Dim lShape As New DotSpatial.Data.Shape(DotSpatial.Topology.FeatureType.Polygon)
                        lShape.AddPart(lCoordinates, DotSpatial.Data.CoordinateType.Regular)
                        With (lFeatureSet.AddFeature(lShape.ToGeometry))
                            .DataRow(0) = lSoil.MuKey
                            .DataRow(1) = lSoil.HSG
                            .DataRow(2) = lSoil.AreaSymbol
                            .DataRow(3) = lSoil.MuSym
                            .DataRow(4) = lSoil.NationalMuSym
                            '.DataRow() = lSoil.ObjectID
                        End With
                    Else
                        'TODO: make inner polygons work as holes
                    End If
                    If Logger.Canceled Then Throw New ApplicationException("Cancelled")
                Next
            Next
            lCurrent += 1
            Logger.Progress(lCurrent, lLast)
            If (Now > lNextProgress) Then
                Logger.Dbg(Format(lCurrent * 100 / lLast, "0.0") & "% complete, (" & lCurrent & " of " & lLast & " soils processed)")
                lNextProgress = Now.AddSeconds(10)
            End If
            If lFeatureSet.Features.Count > MaxShapesPerFile Then 'Keep shapefile from getting too large
                SaveShapefile(lFeatureSet, aLayerFilename, aProjection)
            End If
        Next
        Logger.Dbg(lLast & " soils processed.")
        If lLast > 0 AndAlso lFeatureSet IsNot Nothing Then
            SaveShapefile(lFeatureSet, aLayerFilename, aProjection)
            Return IO.File.Exists(aLayerFilename)
        End If
        Return False
    End Function

    Private Shared Sub SaveShapefile(ByRef aFeatureSet As DotSpatial.Data.FeatureSet, ByVal aBaseShapeFileName As String, ByVal aProjection As DotSpatial.Projections.ProjectionInfo)
        Dim lThisFileName As String = aFeatureSet.Filename.Clone
        aFeatureSet.Reproject(aProjection)
        aFeatureSet.UpdateExtent()
        aFeatureSet.Save()
        aFeatureSet.Close()
        aFeatureSet.Dispose()
        aFeatureSet = Nothing

        If lThisFileName <> aBaseShapeFileName Then
            D4EM.Geo.SpatialOperations.CopyMoveMergeFile(lThisFileName, aBaseShapeFileName, Nothing)
        End If
    End Sub

    'Private Shared Function MakePolygonsLayer(ByVal aSoils As List(Of Soil), ByVal aLayerFilename As String) As DotSpatial.Data.FeatureSet
    '    MkDirPath(PathNameOnly(aLayerFilename))
    '    Dim lFeatureSet = New DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Polygon)
    '    lFeatureSet.Projection = Globals.GeographicProjection

    '    lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("MuKey", "C", 8, 0))
    '    lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("HSG", "C", 4, 0))
    '    lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("AreaSymbol", "C", 8, 0))
    '    lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("MuSym", "C", 8, 0))
    '    lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("NationalMuSym", "C", 8, 0))
    '    'lFeatureSet.DataTable.Columns.Add(New DotSpatial.Data.Field("ObjectID", "N", 12, 0))

    '    'Dim lShape As DotSpatial.Data.Shape = Nothing
    '    Dim lGeometry As DotSpatial.Topology.Geometry
    '    For Each lSoil In aSoils
    '        Dim lShell As DotSpatial.Topology.ILinearRing = Nothing
    '        Dim lHoles As New Generic.List(Of DotSpatial.Topology.ILinearRing)
    '        For Each lPolygon In lSoil.Polygons
    '            Dim lCoordinates = lPolygon.ToCoordinates
    '            If lPolygon.Outer Then
    '                lShell = New DotSpatial.Topology.LinearRing(lCoordinates)
    '                'lShape = New DotSpatial.Data.Shape(DotSpatial.Topology.FeatureType.Polygon)
    '                'lShape.AddPart(lPolygon.ToCoordinates, DotSpatial.Data.CoordinateType.Regular)
    '                'ElseIf lShape Is Nothing Then
    '                '    Logger.Dbg("Inner shape found before outer shape")
    '            Else 'TODO: make sure this is treated as inner polygon
    '                'lShape.AddPart(lPolygon.ToCoordinates, DotSpatial.Data.CoordinateType.Regular)
    '                lHoles.Add(New DotSpatial.Topology.LinearRing(lCoordinates))
    '            End If
    '        Next
    '        If lHoles.Count > 0 Then
    '            lGeometry = New DotSpatial.Topology.Polygon(lShell, lHoles.ToArray)
    '        Else
    '            lGeometry = lShell
    '        End If
    '        With lFeatureSet.AddFeature(lGeometry)
    '            .DataRow(0) = lSoil.MuKey
    '            .DataRow(1) = lSoil.HSG
    '            .DataRow(2) = lSoil.AreaSymbol
    '            .DataRow(3) = lSoil.MuSym
    '            .DataRow(4) = lSoil.NationalMuSym
    '            '.DataRow() = lSoil.ObjectID
    '        End With
    '    Next
    '    lFeatureSet.UpdateExtent()
    '    lFeatureSet.Filename = aLayerFilename
    '    lFeatureSet.Save()
    '    Return lFeatureSet
    'End Function

    Private Enum FieldNumbers As Integer
        musym = 6
        hydgrpdcd = 9
        slope_r = 10
        comppct_r = 11
        hzdept_r = 13
        hzdepb_r = 14
        ksat_r = 15
        chkey = 16
    End Enum

    Private Shared Function RunSoilQuery(ByVal aMuKey As String, ByVal aSOAPConn As NRCS_Service.SDMTabularServiceSoapClient) As System.Data.DataSet
        Dim lQuery As String = _
            "SELECT " & vbCr & _
            "saversion, saverest," & vbCr & _
            "l.areasymbol, l.areaname, l.lkey," & vbCr & _
            "mu.musym, mu.muname, museq, mu.mukey," & vbCr & _
            "compname, comppct_r, albedodry_r, hydgrp, c.cokey, " & vbCr & _
            "hzdepb_r, dbovendry_r, awc_r, ksat_r, om_r, claytotal_r, silttotal_r, sandtotal_r, kffact, ec_r, fraggt10_r, frag3to10_r, sieveno10_r, ch.chkey, " & vbCr & _
            "texdesc, texture, " & vbCr & _
            "cht.chtgkey, texcl " & vbCr & _
            "FROM sacatalog sac" & vbCr & _
            "INNER JOIN legend l ON l.areasymbol = sac.areasymbol AND l.areatypename='Non-MLRA Soil Survey Area' " & vbCr & _
            "INNER JOIN mapunit mu ON mu.lkey = l.lkey AND mu.mukey = " & "'" & aMuKey & "'" & vbCr & _
            "LEFT OUTER JOIN component c ON c.mukey = mu.mukey" & vbCr & _
            "LEFT OUTER JOIN chorizon ch ON ch.cokey = c.cokey" & vbCr & _
            "LEFT OUTER JOIN chtexturegrp chtgrp ON chtgrp.chkey = ch.chkey " & vbCr & _
            "LEFT OUTER JOIN chtexture cht ON cht.chtgkey = chtgrp.chtgkey  " & vbCr & _
            "ORDER BY l.areasymbol, mukey, cokey, comppct_r DESC, hzdepb_r"
        Try
            D4EM.Data.Download.SetSecurityProtocol()
            Return aSOAPConn.RunQuery(lQuery)
        Catch eSoap As Exception
            Logger.Dbg("Error running SOAP query '" & lQuery & "'" & vbCrLf & eSoap.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Sub SetSoilProperties(ByRef aSoil As Soil, ByVal aDataSet As System.Data.DataTable)
        With aDataSet
            For lRow As Integer = 0 To .Rows.Count - 1
                Dim lCoKey As String = .Rows(lRow).Item("cokey")
                Dim lCompToFill As SoilComponent = Nothing
                For Each lComp As SoilComponent In aSoil.Components
                    If lComp.cokey = lCoKey Then
                        lCompToFill = lComp
                        Exit For
                    End If
                Next
                Dim lNewComponent As Boolean = False
                If lCompToFill Is Nothing Then
                    lCompToFill = New SoilComponent()
                    lNewComponent = True
                End If
                If lCompToFill.cokey = "" Then
                    lCompToFill.cokey = lCoKey
                    Double.TryParse(.Rows(lRow).Item("comppct_r"), lCompToFill.comppct_r)
                    lCompToFill.compname = .Rows(lRow).Item("compname")
                    lCompToFill.saversion = .Rows(lRow).Item("saversion")
                    lCompToFill.saverest = .Rows(lRow).Item("saverest")
                    lCompToFill.HSG = .Rows(lRow).Item("hydgrp")
                    Double.TryParse(.Rows(lRow).Item("albedodry_r"), lCompToFill.albedodry_r)
                    lCompToFill.sname = aSoil.AreaSymbol & aSoil.MuSym & "_" & lCoKey.Substring(lCoKey.IndexOf(":") + 1) 'GA089CuC_177287
                End If

                Dim chkey As String = .Rows(lRow).Item("chkey")
                Dim lHorizonToFill As SoilHorizon = Nothing
                For Each lhorizon As SoilHorizon In lCompToFill.chorizons
                    If lhorizon.chkey = chkey Then
                        lHorizonToFill = lhorizon
                        Exit For
                    End If
                Next

                Dim lNewHorizon As Boolean = False
                If lHorizonToFill Is Nothing Then
                    lHorizonToFill = New SoilHorizon()
                    lNewHorizon = True
                End If
                If lHorizonToFill.chkey = "" Then
                    If .Rows(lRow).Item("hzdepb_r").ToString.Trim() <> "" AndAlso _
                       Double.TryParse(.Rows(lRow).Item("hzdepb_r").ToString, lHorizonToFill.hzdepb_r) Then
                        lHorizonToFill.chkey = chkey
                        lHorizonToFill.hzdepb_r *= 10 'turn into mm
                        Double.TryParse(.Rows(lRow).Item("dbovendry_r"), lHorizonToFill.dbovendry_r)
                        Double.TryParse(.Rows(lRow).Item("awc_r"), lHorizonToFill.awc_r)
                        Double.TryParse(.Rows(lRow).Item("ksat_r"), lHorizonToFill.ksat_r)
                        Double.TryParse(.Rows(lRow).Item("om_r"), lHorizonToFill.om_r)
                        Double.TryParse(.Rows(lRow).Item("claytotal_r"), lHorizonToFill.claytotal_r)
                        Double.TryParse(.Rows(lRow).Item("silttotal_r"), lHorizonToFill.silttotal_r)
                        Double.TryParse(.Rows(lRow).Item("sandtotal_r"), lHorizonToFill.sandtotal_r)
                        Double.TryParse(.Rows(lRow).Item("kffact"), lHorizonToFill.kffact)
                        Double.TryParse(.Rows(lRow).Item("ec_r"), lHorizonToFill.ec_r)
                        lHorizonToFill.texture = .Rows(lRow).Item("texcl")
                        Dim lfraggt10 As Double
                        Dim lfrag3to10 As Double
                        Dim lsieve10 As Double
                        Double.TryParse(.Rows(lRow).Item("fraggt10_r"), lfraggt10)
                        Double.TryParse(.Rows(lRow).Item("frag3to10_r"), lfrag3to10)
                        Double.TryParse(.Rows(lRow).Item("sieveno10_r"), lsieve10)
                        lHorizonToFill.rockpct = (lfraggt10 + lfrag3to10 + (100 - lsieve10)) * 0.96 'TODO: needs more work here
                        If lNewHorizon Then lCompToFill.chorizons.Add(lHorizonToFill)
                    End If
                End If

                If lNewComponent Then aSoil.Components.Add(lCompToFill)
            Next 'lRow
        End With 'lSystemDataSet.Tables.Item(0)
        '    End If 'lSystemDataSet.Tables.Count > 0

        '
        'Below is the code for reading XML style response
        '
        'Dim lXMLdoc As New Xml.XmlDocument 'this is the query result from above query
        'For lTableIndex As Integer = 0 To lXMLdoc.GetElementsByTagName("Table").Count - 1
        '    Dim lXMLdocFeature As New Xml.XmlDocument
        '    lXMLdocFeature.LoadXml(lXMLdoc.GetElementsByTagName("Table").Item(lTableIndex).InnerXml)
        '    Dim lFoundNewComponent As Boolean = True
        '    Dim lCoKey As String = lXMLdocFeature.GetElementsByTagName("cokey").Item(0).InnerText
        '    For Each lComp As SoilComponent In aSoil.Components
        '        If lComp.cokey = lCoKey Then
        '            lFoundNewComponent = False
        '            Exit For
        '        End If
        '    Next
        '    If lFoundNewComponent Then
        '        Dim lNewComponent As New SoilComponent()
        '        With lNewComponent
        '            .cokey = lCoKey
        '            .comppct_r = Double.Parse(lXMLdocFeature.GetElementsByTagName("comppct_r").Item(0).InnerText)
        '            .compname = lXMLdocFeature.GetElementById("compname").InnerText
        '            .saversion = lXMLdocFeature.GetElementById("saversion").InnerText
        '            .saverest = lXMLdocFeature.GetElementById("saverest").InnerText
        '            .HSG = lXMLdocFeature.GetElementById("hydgrp").InnerText
        '            .albedodry_r = Double.Parse(lXMLdocFeature.GetElementById("albedodry_r").InnerText)
        '            .sname = aSoil.AreaSymbol & aSoil.MuSym & "_" & lCoKey.Substring(lCoKey.IndexOf(":") + 1) 'GA089CuC_177287
        '            Dim chkey As String = lXMLdocFeature.GetElementById("chkey").InnerText
        '            Dim lFoundNewHorizon As Boolean = True
        '            For Each lhorizon As SoilHorizon In .chorizons
        '                If lhorizon.chkey = chkey Then
        '                    lFoundNewHorizon = False
        '                    Exit For
        '                End If
        '            Next
        '            If lFoundNewHorizon Then
        '                Dim lNewHorizon As New SoilHorizon()
        '                lNewHorizon.chkey = chkey
        '                If lXMLdocFeature.GetElementById("hzdepb_r").InnerText.Trim() <> "" OrElse _
        '                   Double.TryParse(lXMLdocFeature.GetElementById("hzdepb_r").InnerText, lNewHorizon.hzdepb_r) Then
        '                    lNewHorizon.hzdepb_r = Double.Parse(lXMLdocFeature.GetElementById("hzdepb_r").InnerText) * 10 'turn into mm
        '                    lNewHorizon.dbovendry_r = Double.Parse(lXMLdocFeature.GetElementById("dbovendry_r").InnerText)
        '                    lNewHorizon.awc_r = Double.Parse(lXMLdocFeature.GetElementById("awc_r").InnerText)
        '                    lNewHorizon.ksat_r = Double.Parse(lXMLdocFeature.GetElementById("ksat_r").InnerText)
        '                    lNewHorizon.om_r = Double.Parse(lXMLdocFeature.GetElementById("om_r").InnerText)
        '                    lNewHorizon.claytotal_r = Double.Parse(lXMLdocFeature.GetElementById("claytotal_r").InnerText)
        '                    lNewHorizon.silttotal_r = Double.Parse(lXMLdocFeature.GetElementById("silttotal_r").InnerText)
        '                    lNewHorizon.sandtotal_r = Double.Parse(lXMLdocFeature.GetElementById("sandtotal_r").InnerText)
        '                    lNewHorizon.kffact = Double.Parse(lXMLdocFeature.GetElementById("kffact").InnerText)
        '                    lNewHorizon.ec_r = Double.Parse(lXMLdocFeature.GetElementById("ec_r").InnerText)
        '                    lNewHorizon.texture = Double.Parse(lXMLdocFeature.GetElementById("texcl").InnerText)
        '                    Dim lfraggt10 As Double = Double.Parse(lXMLdocFeature.GetElementById("fraggt10_r").InnerText)
        '                    Dim lfrag3to10 As Double = Double.Parse(lXMLdocFeature.GetElementById("frag3to10_r").InnerText)
        '                    Dim lsieve10 As Double = Double.Parse(lXMLdocFeature.GetElementById("sieveno10_r").InnerText)
        '                    lNewHorizon.rockpct = (lfraggt10 + lfrag3to10 + (100 - lsieve10)) * 0.96 'TODO: needs more work here
        '                    lNewComponent.chorizons.Add(lNewHorizon)
        '                End If

        '            End If
        '        End With 'lNewComponent
        '        aSoil.Components.Add(lNewComponent)
        '    End If 'lFoundNewComponent 
        'Next 'lTableIndex

        'If aDataSet IsNot Nothing Then aDataSet.Clear()
        'End If

    End Sub

    ''' <summary>Determines soil layer properties for specified key</summary>
    ''' <param name="aKey">Unique Key (eg '124246') from SoilDataAccess Mukey property</param>
    ''' <param name="aArea">Area Symbol (eg 'GA089') from SoilDataAccess AreaSymbol property</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FindSoilLayerProperties(ByVal aKey As String, ByVal aArea As String) As SortedList(Of String, SoilLayer)
        If aKey = Nothing OrElse aKey.Length = 0 Then
            Return Nothing
        Else 'this gets the dominant hsg for the map unit based on percentage of components
            Dim lSoilLayers As New SortedList(Of String, SoilLayer)
            Dim lQuery As String = _
                "SELECT" & vbCr & _
                "saversion, saverest," & vbCr & _
                "l.areasymbol, l.areaname, l.lkey," & vbCr & _
                "mu.musym, mu.muname, museq, mu.mukey," & vbCr & _
                "hydgrpcd, " & vbCr & _
                "compname, comppct_r, albedodry_r, hydgrp, c.cokey, " & vbCr & _
                "hzdepb_r, dbovendry_r, awc_r, ksat_r, om_r, claytotal_r, silttotal_r, sandtotal_r, kffact, ec_r, ch.chkey " & vbCr & _
                "texdesc, texture, " & vbCr & _
                "cht.chtgkey, texcl " & vbCr & _
                "FROM sacatalog sac" & vbCr & _
                "INNER JOIN legend l ON l.areasymbol = sac.areasymbol AND l.areatypename='Non-MLRA Soil Survey Area' " & vbCr & _
                "INNER JOIN mapunit mu ON mu.lkey = l.lkey AND mu.mukey = " & "'" & aKey & "'" & vbCr & _
                "LEFT OUTER JOIN component c ON c.mukey = mu.mukey" & vbCr & _
                "LEFT OUTER JOIN chorizon ch ON ch.cokey = c.cokey" & vbCr & _
                "LEFT OUTER JOIN chtexturegrp chtgrp ON chtgrp.chkey = ch.chkey " & vbCr & _
                "LEFT OUTER JOIN chtexture cht ON cht.chtgkey = chtgrp.chtgkey " & vbCr & _
                "ORDER BY l.areaname, museq, comppct_r DESC, compname, hzdepb_r"

            'Dim lURL As String = "http://sdmdataaccess.nrcs.usda.gov/QueryResults.aspx?TxtQuery=" & System.Web.HttpUtility.UrlEncode(lQuery) & "&RbgFormat=RbiIXML&BtnSubmit=Submit+Query"

            'lURL = "http://sdmdataaccess.nrcs.usda.gov/QueryResults.aspx?TxtQuery=SELECT%0D%0Asaversion%2C+saverest%2C+%0D%0Al.areasymbol%2C+l.areaname%2C+l.lkey%2C+%0D%0Amusym%2C+muname%2C+museq%2C+mu.mukey%2C+%0D%0Acomppct_r%2C+albedodry_r%2C+hydgrp%2C+compname%2C+c.cokey%2C+%0D%0Ahzdepb_r%2C+dbovendry_r%2C+awc_r%2C+ksat_r%2C+om_r%2C+claytotal_r%2C+silttotal_r%2C+sandtotal_r%2C+kffact%2C+ec_r%2C+ch.chkey%2C+%0D%0Atexdesc%2C+texture%2C+stratextsflag%2C+chtgrp.rvindicator%2C+%0D%0Acht.chtgkey%2C+texcl%0D%0AFROM+sacatalog+sac%0D%0AINNER+JOIN+legend+l+ON+l.areasymbol+%3D+sac.areasymbol+AND+l.areatypename+%3D+%27Non-MLRA+Soil+Survey+Area%27%0D%0AINNER+JOIN+mapunit+mu+ON+mu.lkey+%3D+l.lkey+AND+mu.mukey+%3D%27124275%27%0D%0ALEFT+OUTER+JOIN+component+c+ON+c.mukey+%3D+mu.mukey%0D%0ALEFT+OUTER+JOIN+chorizon+ch+ON+ch.cokey+%3D+c.cokey%0D%0ALEFT+OUTER+JOIN+chtexturegrp+chtgrp+ON+chtgrp.chkey+%3D+ch.chkey+%0D%0ALEFT+OUTER+JOIN+chtexture+cht+ON+cht.chtgkey+%3D+chtgrp.chtgkey+&RbgFormat=RbiIXML&BtnSubmit=Submit+Query"


            'http://sdmdataaccess.nrcs.usda.gov/QueryResults.aspx?TxtQuery=
            'SELECT%0D%0Asaversion%2C+saverest%2C+%0D%0Al.areasymbol%2C+l.areaname%2C+l.lkey%2C+%0D%0Amusym%2C+muname%2C+museq%2C+mu.mukey%2C+%0D%0Acomppct_r%2C+albedodry_r%2C+hydgrp%2C+compname%2C+c.cokey%2C+%0D%0Ahzdepb_r%2C+dbovendry_r%2C+awc_r%2C+ksat_r%2C+om_r%2C+claytotal_r%2C+silttotal_r%2C+sandtotal_r%2C+kffact%2C+ec_r%2C+ch.chkey%2C+%0D%0Atexdesc%2C+texture%2C+stratextsflag%2C+chtgrp.rvindicator%2C+%0D%0Acht.chtgkey%2C+texcl%0D%0A
            'FROM+sacatalog+sac%0D%0AINNER+JOIN+legend+l+ON+l.areasymbol+%3D+sac.areasymbol+AND+l.areatypename+%3D+%27Non-MLRA+Soil+Survey+Area%27%0D%0A
            'INNER+JOIN+mapunit+mu+ON+mu.lkey+%3D+l.lkey+AND+mu.mukey+%3D%27124275%27%0D%0A
            'LEFT+OUTER+JOIN+component+c+ON+c.mukey+%3D+mu.mukey%0D%0A
            'LEFT+OUTER+JOIN+chorizon+ch+ON+ch.cokey+%3D+c.cokey%0D%0A
            'LEFT+OUTER+JOIN+chtexturegrp+chtgrp+ON+chtgrp.chkey+%3D+ch.chkey+%0D%0A
            'LEFT+OUTER+JOIN+chtexture+cht+ON+cht.chtgkey+%3D+chtgrp.chtgkey+
            '&RbgFormat=RbiIXML&BtnSubmit=Submit+Query

            'http://sdmdataaccess.nrcs.usda.gov/QueryResults.aspx?TxtQuery=
            'SELECT%0dsaversion%2c+saverest%2c%0dl.areasymbol%2c+l.areaname%2c+l.lkey%2c%0dmu.musym%2c+mu.muname%2c+museq%2c+mu.mukey%2c%0dhydgrpdcd%0dcompname%2c+comppct_r%2c+albedodry_r%2c+hydgrp%2c+c.cokey%2c+%0dhzdepb_r%2c+dbovendry_r%2c+awc_r%2c+ksat_r%2c+om_r%2c+claytotal%2c+silttotal_r%2c+sandtotal_r%2c+kffact%2c+ec_r%2c+ch.chkey+%0dtexdesc%2c+texture%2c+%0dcht.chtgkey%2c+texcl+%0d
            'FROM+sacatalog+sac%0dINNER+JOIN+legend+l+ON+l.areasymbol+%3d+sac.areasymbol+AND+l.areatypename%3d%27Non-MLRA+Soil+Survey+Area%27+%0d
            'INNER+JOIN+mapunit+mu+ON+mu.lkey+%3d+l.lkey+AND+mu.mukey+%3d+%27160886%27%0d
            'LEFT+OUTER+JOIN+component+c+ON+c.mukey+%3d+mu.mukey%0d
            'LEFT+OUTER+JOIN+chorizon+ch+ON+ch.cokey+%3d+c.cokey%0d
            'LEFT+OUTER+JOIN+chtexturegrp+chtgrp+ON+chtgrp.chkey+%3d+ch.chkey+%0d
            'LEFT+OUTER+JOIN+chtexture+cht+ON+cht.chtgkey+%3d+chtgrp.chtgkey++%0d
            'ORDER+BY+l.areaname%2c+museq%2c+comppct_r+DESC%2c+compname%2c+hzdepb_r
            '&RbgFormat=RbiIXML&BtnSubmit=Submit+Query

            'Dim lResults As String = DownloadURL(lURL)

            'If lResults.StartsWith("<html>") Then
            '    Throw New ApplicationException("Unable to download XML results from NRCS")
            'End If

            Logger.Dbg("about to run query for mukey '" & aKey & "'")
            Dim lSoap As NRCS_Service.SDMTabularServiceSoapClient = Nothing
            Try
                lSoap = New NRCS_Service.SDMTabularServiceSoapClient '"SDMTabularServiceSoap") '"NRCS.SDMTabularServiceSoap")
            Catch ex As Exception
                Logger.Dbg("Unable to create NRCS.SDMTabularServiceSoapClient, " & ex.ToString)
            End Try
            Dim lSystemDataSet As System.Data.DataSet = lSoap.RunQuery(lQuery)
            lSoap.Close()
            Logger.Dbg("query complete " & lSystemDataSet.Tables.Count)


            If lSystemDataSet.Tables.Count > 0 Then
                Dim lName As String = ""
                With lSystemDataSet.Tables.Item(0)
                    For lRow As Integer = 0 To .Rows.Count - 1
                        'Logger.Dbg(.Rows(lRow).ItemArray.Count)
                        Dim lRowItemArray() As Object = .Rows(lRow).ItemArray
                        lName = lRowItemArray(FieldNumbers.musym)
                        Dim lKey As String = lRowItemArray(FieldNumbers.chkey)
                        If lRowItemArray(FieldNumbers.hydgrpdcd).length > 0 Then
                            Dim lHzdept_r As String = lRowItemArray(FieldNumbers.hzdept_r)
                            If lHzdept_r Is Nothing OrElse lHzdept_r.Length = 0 Then
                                lHzdept_r = -999
                            End If
                            Dim lHzdepb_r As String = lRowItemArray(FieldNumbers.hzdepb_r)
                            If lHzdepb_r Is Nothing OrElse lHzdepb_r.Length = 0 Then
                                lHzdepb_r = -999
                            End If
                            Dim lKsat As String = lRowItemArray(FieldNumbers.ksat_r)
                            If lKsat Is Nothing OrElse lKsat.Length = 0 Then
                                lKsat = -999
                            End If
                            Dim lLayer As New SoilLayer
                            With lLayer
                                .DepthToTop = lHzdept_r
                                .DepthToBottom = lHzdepb_r
                                .KSAT = lKsat
                                .HSG = lRowItemArray(FieldNumbers.hydgrpdcd)
                                Integer.TryParse(lRowItemArray(FieldNumbers.comppct_r), .CompPct_R)
                                Double.TryParse(lRowItemArray(FieldNumbers.slope_r), .Slope_R)
                            End With
                            If Not lSoilLayers.ContainsKey(lKey) Then
                                lSoilLayers.Add(lKey, lLayer)
                            End If
                        End If
                    Next
                End With

                If lSoilLayers.Count > 0 Then
                    Logger.Dbg("found " & lSoilLayers.Count & " layers")
                Else
                    Logger.Dbg("found no hydrologic group for soil named '" & lName & "'")
                End If
                lSystemDataSet.Tables(0).WriteXml(IO.Path.GetTempPath & "SWC\" & aKey & ".xml")
            Else
                Logger.Dbg("found no table for this key '" & aKey & "'")
            End If
            Return lSoilLayers
        End If
    End Function
End Class
