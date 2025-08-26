Imports System.IO
Imports atcUtility
Imports MapWinUtility


Public Class HSPFmodel

    'General Specs
    Private pOutputPath As String = "C:\BASINS\modelout\"
    Private pInputPath As String = "C:\BASINS\data\02060006-16\"
    Private pBaseOutputName As String = "02060006-16"    'output uci will be named this with .uci extension

    'Subbasin Specs
    Private pSubbasinLayerName As String = "Subbasins"   'name of layer as displayed on legend, or file name
    Private pSubbasinFieldName As String = "SUBBASIN"    'field containing unique integer identifier
    Private pSubbasinSlopeName As String = "SLO1"        'field containing slope of each subbasin
    Private pSubbasinSegmentName As String = "ModelSeg"          'field containing name of model segment associated 
    '                                                       with each subbasin, blank indicates all subbasins 
    '                                                       are associated with a single model segment

    'Land Use Specs
    Private pLUType As Integer = 0   '(0 - USGS GIRAS Shape, 1 - NLCD grid, 2 - Other shape, 3 - Other grid)
    Private pLandUseClassFile As String = "C:\BASINS\etc\giras.dbf"   'name of file that indicates classification scheme
    '                                                                    eg 21-25 = urban, 41-45 = cropland, etc
    Private pLandUseLayerName As String = ""   'name of land use layer as displayed on legend, or file name,
    '                                             does not need to be set for GIRAS land use type
    Private pLandUseFieldName As String = ""   'field containing land use classification code,
    '                                             only used for 'other shapefile' land use type
    Private pLUInclude() As Integer = {}       'special array to specify land use codes to be included
    '                                             for every subbasin, even if none exists in that subbasin,
    '                                             used to enforce a numbering convention

    'Met Data Specs
    Private pMetWDM As String = pInputPath & "met\met.wdm"   'name of met wdm file

    'Stream Specs
    Private pStreamLayerName As String = "Streams"     'name of layer as displayed on legend, or file name
    Private pStreamFields() As String = {"SUBBASIN", "SUBBASINR", "LEN2", "SLO2", "WID2", "DEP2", "MINEL", "MAXEL", "SNAME"}
    '                                                   array of field names containing required data for each stream

    'Point Source Specs
    Private pOutletsLayerName As String = "<none>"     'name of outlets layer from legend, or file name
    Private pPointFieldName As String = "PCSID"        'name of field within outlets layer containing unique 
    '                                                     point source identifier (like pcs station id)
    Private pPointYear As String = "1999"              'year of pcs data to use
    Private pPSRCustom As Boolean = False              'flag indicating use custom point source data, else pcs
    Private pPSRCustomFile As String = ""              'if using custom point source data, pass file name
    Private pPSRCalculate As Boolean = False           'flag indicating if distance on stream is to be calculated,
    '                                                   not implemented at this time

    Public Sub BuildHSPFInput(ByVal aProject As D4EM.Data.Project,
                              ByVal aCatchmentsLayer As D4EM.Data.Layer,
                              ByVal aFlowlinesLayer As D4EM.Data.Layer,
                              ByVal aLandUseLayer As D4EM.Data.Layer,
                              ByVal aDemGridLayer As D4EM.Data.Layer,
                              ByVal aSoilsLayer As D4EM.Data.Layer,
                              ByVal aMetWDM As atcData.atcDataSource,
                              ByVal aSimulationStartYear As Integer,
                              ByVal aSimulationEndYear As Integer,
                              ByVal aOutputInterval As HspfOutputInterval,
                              ByVal aBaseOutputName As String,
                              ByVal aWQConstituents() As String,
                              ByVal aSnowOption As Integer,
                              ByVal aBacterialOption As Boolean,
                              ByVal aChemicalOption As Boolean,
                              ByVal aChemicalName As String,
                              ByVal aChemicalMaximumSolubility As Double,
                              ByVal aChemicalPartitionCoeff() As Double,
                              ByVal aChemicalFreundlichExp() As Double,
                              ByVal aChemicalDegradationRate() As Double,
                              ByVal aSegmentationOption As Integer)

        Logger.Dbg("BuildHSPFInput: Start")
        GisUtil.MappingObject = aProject

        pInputPath = aProject.ProjectFolder
        pOutputPath = aProject.ProjectFolder
        pSubbasinLayerName = aCatchmentsLayer.FileName
        pStreamLayerName = aFlowlinesLayer.FileName
        pLandUseLayerName = aLandUseLayer.FileName
        pBaseOutputName = aBaseOutputName
        pMetWDM = aMetWDM.Specification
        ChDriveDir(pOutputPath)  'change to the directory of the current project
        Logger.Dbg(" CurDir:" & CurDir())

        'Overlay soil group with land use if soils have HSG
        If aSoilsLayer Is Nothing Then
        ElseIf aSoilsLayer.FieldIndex("HSG") < 0 Then
            Logger.Dbg("Soil layer does not have HSG field, probably is STATSGO, not overlaying with land use")
        Else
            Logger.Status("Overlay Land Use and Soil")
            Dim lHRUGridFileName As String = IO.Path.Combine(aProject.ProjectFolder, "LUSoil.tif")
            Dim lHruTableFilename As String = IO.Path.ChangeExtension(lHRUGridFileName, ".table.txt")
            Dim lHruTable As D4EM.Geo.HRUTable = D4EM.Geo.OverlayReclassify.OverlayLandUseSoils(
                lHRUGridFileName, aLandUseLayer, aSoilsLayer, "HSG")
            pLandUseLayerName = lHRUGridFileName
            aProject.Layers.Add(New D4EM.Data.Layer(lHRUGridFileName, aLandUseLayer.Specification))
        End If

        'assuming we've downloaded:
        '  NHDPlus
        '  NLCD 2001
        pLUType = 1
        pLandUseClassFile = FindFile("Land Use Reclassification File", "nlcd.dbf", "dbf")
        '  DEM
        Dim lElevationUnitsName As String = "Centimeters"
        '  Met Data
        'if Subbasin Layer does not have pSubbasinFieldName, calculate them
        If Not GisUtil.IsField(GisUtil.LayerIndex(pSubbasinLayerName), pSubbasinFieldName) Then
            GisUtil.Layers(GisUtil.LayerIndex(pSubbasinLayerName)).AssignIndexes(pSubbasinFieldName)
        End If
        ManDelinPlugIn.CalculateSubbasinParameters(pSubbasinLayerName, aDemGridLayer.FileName, lElevationUnitsName, SafeFilename(aProject.Region.ToString))

        'if streams layer does not have required fields, calculate them
        If Not GisUtil.IsField(GisUtil.LayerIndex(pStreamLayerName), pStreamFields(1)) Then
            'assign subbasin numbers to each reach segment
            Dim lMinField As Integer = 9999
            ManDelinPlugIn.CalculateReachSubbasinIds(pStreamLayerName,
                                                     pSubbasinLayerName,
                                                     lMinField)
            'add downstream subbasin ids
            ManDelinPlugIn.CalculateReachDownstreamSubbasinIds(pStreamLayerName,
                                                               lMinField)
            'calculate required reach parameters
            ManDelinPlugIn.CalculateReachParameters(pStreamLayerName,
                                                    pSubbasinLayerName,
                                                    aDemGridLayer.FileName,
                                                    lElevationUnitsName,
                                                    lMinField)
        End If

        'for now assume that the met wdm only has one station in it
        'if subbasins layer does not have model segment id field, add it
        'pSubbasinSegmentName = "ModelSeg"
        'Dim lMetLayerName As String = "C:\BASINS\data\02060006-16\met\met.shp"
        'ModelSegmentationPlugIn.AssignMetStationsByProximity(pSubbasinLayerName, lMetLayerName, False)

        'get land use data ready
        Dim lPerviousGridSource As New atcControls.atcGridSource
        SetPerviousGrid(lPerviousGridSource, pLandUseClassFile, pLUType, pLandUseLayerName, pLandUseFieldName)

        'get met data ready
        Dim lUniqueModelSegmentIds As New atcCollection
        Dim lUniqueModelSegmentNames As New atcCollection
        Dim lSubbasinsLayerIndex As Integer = GisUtil.LayerIndex(pSubbasinLayerName)
        If GisUtil.IsField(lSubbasinsLayerIndex, pSubbasinSegmentName) Then
            FindUniqueMetSegments(lUniqueModelSegmentNames, lUniqueModelSegmentIds,
                                  pSubbasinLayerName, pSubbasinFieldName, pSubbasinSegmentName, aSegmentationOption)
        Else
            'if the ModelSeg field doesn't exist (using NCDC), set it to blank
            pSubbasinSegmentName = ""
        End If

        If lUniqueModelSegmentIds.Count = 0 Then
            'when using a single met station, make sure it has an ID
            lUniqueModelSegmentIds.Add(1)
        End If

        Dim lMetStations As New atcCollection
        Dim lMetBaseDsns As New atcCollection
        Dim lMetWdmNames As New atcCollection
        BuildListofMetStationNames(lMetWdmNames, lMetStations, lMetBaseDsns, aProject.TimeseriesSources, lUniqueModelSegmentNames, aSegmentationOption)

        Dim lMetWdmIds As New atcCollection
        'Using same wdm file for all model segments
        For Each lModelSegmentID In lUniqueModelSegmentIds
            lMetWdmIds.Add(lMetWdmIds.Count, "WDM2")
        Next
        If (lMetWdmIds.Count > lMetBaseDsns.Count) And lMetBaseDsns.Count = 0 Then
            'special case for ncdc, need to set the base dsn
            lMetBaseDsns.Add(1)
        End If

        Dim lOutputPath As String = IO.Path.Combine(pOutputPath, "HSPF")
        Dim lLocalDataFolder As String = IO.Path.Combine(aProject.ProjectFolder, "LocalData")
        If IO.Directory.Exists(lLocalDataFolder) Then
            Dim lAnimalLocationsFileName As String = IO.Path.Combine(lLocalDataFolder, "AnimalLL.csv")
            If FileExists(lAnimalLocationsFileName) Then
                Try
                    Dim lAnimalsCSV As New atcTableDelimited
                    lAnimalsCSV.Delimiter = ","
                    If lAnimalsCSV.OpenFile(lAnimalLocationsFileName) Then
                        Dim lShapeFilename As String = IO.Path.ChangeExtension(lAnimalLocationsFileName, ".shp")
                        lShapeFilename = CreateShapefileFromTable(lShapeFilename, lAnimalsCSV, 1, 2, aProject.DesiredProjection)
                        If IO.File.Exists(lShapeFilename) AndAlso Not GisUtil.IsLayer(lShapeFilename) Then
                            GisUtil.AddLayer(lShapeFilename, "Animals")
                        Else
                            Logger.Msg("Animal shapefile was not created:" & vbCrLf & vbCrLf & lShapeFilename, MsgBoxStyle.OkOnly, "HSPF")
                        End If
                    Else
                        Logger.Msg("Animal location file cannot be opened:" & vbCrLf & vbCrLf & lAnimalLocationsFileName, MsgBoxStyle.OkOnly, "HSPF")
                    End If
                    Logger.Progress(1, 1) 'Clear progress
                Catch ex As Exception
                    Logger.Msg(ex.ToString, "Error opening Animal Locations " & lAnimalLocationsFileName)
                End Try
            End If
            Dim lPointSourceLocationsFileName As String = IO.Path.Combine(lLocalDataFolder, "PointSourceLL.csv")
            If FileExists(lPointSourceLocationsFileName) Then
                Try
                    'try to convert it to shapefile
                    Dim lPointSourceCSV As New atcTableDelimited
                    lPointSourceCSV.Delimiter = ","
                    If lPointSourceCSV.OpenFile(lPointSourceLocationsFileName) Then
                        Dim lShapeFilename As String = IO.Path.ChangeExtension(lPointSourceLocationsFileName, ".shp")
                        lShapeFilename = CreateShapefileFromTable(lShapeFilename, lPointSourceCSV, 1, 2, aProject.DesiredProjection)
                        If IO.File.Exists(lShapeFilename) Then
                            pOutletsLayerName = lShapeFilename
                            pPointFieldName = "PtSrcId"
                            pPSRCustomFile = IO.Path.Combine(lLocalDataFolder, "PointSourceData.csv")
                            If Not GisUtil.IsLayer(pOutletsLayerName) Then
                                'add to map
                                GisUtil.AddLayer(lShapeFilename, "Point Sources")
                                'aProject.Layers.Add(New D4EM.Data.Layer(pOutletsLayerName, New D4EM.Data.LayerSpecification("PtSrc", "Point Sources", IdFieldName:="PtSrcId"), False))
                            End If
                            If GisUtil.IsLayer(pOutletsLayerName) Then
                                If FileExists(pPSRCustomFile) Then
                                    pPSRCustom = True
                                Else
                                    'Logger.Msg("Point Source Data file does not exist:" & vbCrLf & vbCrLf & pPSRCustomFile, MsgBoxStyle.OkOnly, "HSPF")
                                End If
                            Else
                                Logger.Msg("Point Source shapefile could not be added to map:" & vbCrLf & vbCrLf & pOutletsLayerName, MsgBoxStyle.OkOnly, "HSPF")
                            End If
                        Else
                            Logger.Msg("Point Source shapefile was not created:" & vbCrLf & vbCrLf & lShapeFilename, MsgBoxStyle.OkOnly, "HSPF")
                        End If
                    Else
                        Logger.Msg("Point Source text file cannot be opened:" & vbCrLf & vbCrLf & lPointSourceLocationsFileName, MsgBoxStyle.OkOnly, "HSPF")
                    End If
                Catch ex As Exception
                    Logger.Msg("Error opening Point Source Locations " & vbCrLf & vbCrLf & lPointSourceLocationsFileName & vbCrLf & vbCrLf & ex.ToString)
                End Try
            Else
                Logger.Msg("Point Source location file not found:" & vbCrLf & vbCrLf & lPointSourceLocationsFileName, MsgBoxStyle.OkOnly, "HSPF")
            End If
        End If
        'look at shapefile attributes for reaches to have intermediate output and boundary inflows
        Dim lIntermediateLocations As New atcCollection
        Dim lUpstreamLocations As New atcCollection
        'Output field 1=intermediate output desired
        'Boundary field 1=boundary inflow desired
        UpstreamInstreamLocations(pStreamLayerName, pSubbasinFieldName, "Output",
                                  lIntermediateLocations)
        UpstreamInstreamLocations(pStreamLayerName, pSubbasinFieldName, "Boundary",
                                  lUpstreamLocations)
        'aIntermediateLocations.Add(3)  'for testing
        'aUpstreamLocations.Add(2)  'for testing
        'now start the processing
        'If PreProcessChecking(lOutputPath, pBaseOutputName, "HSPF", pLUType, pMetStations.Count, _
        '                      pSubbasinLayerName, pLandUseLayerName) Then 'early checks OK
        Logger.Status("Preparing HSPF Setup")
        If SetupHSPF(lPerviousGridSource,
                     lMetBaseDsns, lMetWdmIds,
                     lUniqueModelSegmentNames, lUniqueModelSegmentIds,
                     lOutputPath, pBaseOutputName,
                     pSubbasinLayerName, pSubbasinFieldName, pSubbasinSlopeName,
                     pStreamLayerName, pStreamFields,
                     pLUType, pLandUseLayerName, pLUInclude,
                     pOutletsLayerName, pPointFieldName, pPointYear,
                     pLandUseFieldName, pLandUseClassFile,
                     pSubbasinSegmentName,
                     pPSRCustom, pPSRCustomFile, pPSRCalculate, aSnowOption, aDemGridLayer, lElevationUnitsName,
                     aSegmentationOption) Then
            lMetWdmNames.Clear()
            lMetWdmNames.Add(pMetWDM)
            If CreateUCI(IO.Path.Combine(lOutputPath, pBaseOutputName & ".uci"),
                         lMetWdmNames, aWQConstituents, True,
                         aSimulationStartYear, aSimulationEndYear, aOutputInterval,
                         aSnowOption, aBacterialOption, aChemicalOption,
                         aChemicalName, aChemicalMaximumSolubility,
                         aChemicalPartitionCoeff, aChemicalFreundlichExp, aChemicalDegradationRate,
                         lIntermediateLocations, lUpstreamLocations) Then
                Logger.Status("Completed HSPF Setup")
                Logger.Dbg("UCIBuilder:  Created UCI file " & lOutputPath & "\" & pBaseOutputName & ".uci")
            Else
                Logger.Status("HSPF Setup Failed in CreateUCI")
            End If
        Else
            Logger.Status("HSPF Setup Failed")
        End If
        'Else
        '    Logger.Status("HSPF Setup Failed in PreProcess Checking")
        'End If
        Logger.Status("")
    End Sub

    Public Shared Function CreateShapefileFromTable(aSaveAsBaseFilename As String, aTable As atcTable, aLatField As Integer, aLonField As Integer, aDesiredProjection As DotSpatial.Projections.ProjectionInfo) As String
        Dim lShpFileName As String = IO.Path.ChangeExtension(aSaveAsBaseFilename, ".shp")
        If Not TryDeleteShapefile(lShpFileName) Then
            Logger.Msg("Could not delete existing shapefile " & lShpFileName & ", probably because it is in use.", "Create Shapefile")
            Return Nothing
        End If

        Dim lLayer = New DotSpatial.Data.PointShapefile(lShpFileName)
        lLayer.FilePath = lShpFileName
        lLayer.Save() 'DotSpatial now wants it saved before adding points
        lLayer.Close()
        lLayer = New DotSpatial.Data.PointShapefile(lShpFileName)

        Dim lDBF As New atcTableDBF
        Dim lFieldLengths() As Integer = aTable.ComputeFieldLengths()
        lDBF.NumFields = aTable.NumFields
        Dim lFieldIndex As Integer
        For lFieldIndex = 1 To aTable.NumFields
            lDBF.FieldName(lFieldIndex) = aTable.FieldName(lFieldIndex)
            lDBF.FieldLength(lFieldIndex) = Math.Max(lFieldLengths(lFieldIndex), 8)
            lDBF.FieldType(lFieldIndex) = "S"
        Next
        lDBF.NumRecords = aTable.NumRecords
        For lRecordIndex As Integer = 1 To aTable.NumRecords
            aTable.CurrentRecord = lRecordIndex
            lDBF.CurrentRecord = lRecordIndex
            For lFieldIndex = 1 To aTable.NumFields
                lDBF.Value(lFieldIndex) = aTable.Value(lFieldIndex)
            Next

            Dim lPoint As New NetTopologySuite.Geometries.Coordinate(aTable.Value(aLonField), aTable.Value(aLatField))
            Dim lShape As New DotSpatial.Data.Shape(lPoint)
            DotSpatial.Projections.Reproject.ReprojectPoints(lShape.Vertices, lShape.Z, D4EM.Data.Globals.GeographicProjection, aDesiredProjection, 0, 1)
            lLayer.AddShape(lShape)
        Next
        lLayer.M = Nothing
        lLayer.Z = Nothing
        lLayer.Projection = aDesiredProjection
        lLayer.SaveAs(lShpFileName, True)
        lLayer.Close()

        lDBF.WriteFile(IO.Path.ChangeExtension(aSaveAsBaseFilename, ".dbf"))
        Return lShpFileName
    End Function

    Public Sub UpstreamInstreamLocations(ByVal aSubbasinLayerName As String,
                                         ByVal aSubbasinFieldName As String,
                                         ByVal aLocationFieldName As String,
                                         ByRef aLocationIds As atcCollection)

        If Not String.IsNullOrEmpty(aLocationFieldName) Then
            Dim lSubbasinsLayerIndex As Integer = GisUtil.LayerIndex(aSubbasinLayerName)
            Dim lSubbasinsFieldIndex As Integer = GisUtil.FieldIndex(lSubbasinsLayerIndex, aSubbasinFieldName)

            aLocationIds = New atcCollection

            'see if we have some upstream or instream location indicators in the subbasin dbf
            If GisUtil.IsField(lSubbasinsLayerIndex, aLocationFieldName) Then
                Dim lLocationFieldIndex As Integer = GisUtil.FieldIndex(lSubbasinsLayerIndex, aLocationFieldName)
                If lLocationFieldIndex > 0 Then
                    Dim lIsInteger As Boolean = True
                    For lIndex As Integer = 1 To GisUtil.NumFeatures(lSubbasinsLayerIndex)
                        Dim lLocationString As String = GisUtil.FieldValue(lSubbasinsLayerIndex, lIndex - 1, lLocationFieldIndex)
                        Dim lSubbasinString As String = GisUtil.FieldValue(lSubbasinsLayerIndex, lIndex - 1, lSubbasinsFieldIndex)

                        If lLocationString IsNot Nothing AndAlso lLocationString.Trim = "1" Then
                            aLocationIds.Add(lSubbasinString)
                        End If
                    Next
                End If
            End If
        End If
    End Sub
End Class
