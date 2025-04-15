Imports atcUtility
Imports MapWinUtility
Imports SDM_Project_Builder_Batch

Module modSDM_GUI
    Friend g_Map As DotSpatial.Controls.Map
    'Friend pBuildFrm As frmBuildNew
    Friend pFrmSpecifyProject As frmSpecifyProject
    Friend g_NationalProject As D4EM.Data.Project
    Friend g_AppManager As DotSpatial.Controls.AppManager

    Friend g_AppNameRegistry As String = "FramesSDM" 'For preferences in registry
    Friend g_AppNameShort As String = "FramesSDM"
    Friend g_AppNameLong As String = "Frames SDM"
    Friend g_AddLayers As Boolean = False
    Friend params As New SDM_Project_Builder_Batch.SDMParameters

    Friend Const PARAMETER_FILE As String = "SDMParameters.xml"

    Friend Const NATIONAL_CNTY As String = "national_cnty"
    Friend Const NATIONAL_ST As String = "national_st"
    Friend Const NATIONAL_HUC8 As String = "national_huc250d3"

    ''' <summary>Return the name of the field containing unique keys</summary>
    ''' <param name="aDBFFileName">File name of DBF whose key field to return</param>
    Friend Function DBFKeyFieldName(ByVal aDBFFileName As String) As String
        Dim lFileNameOnly As String = IO.Path.GetFileNameWithoutExtension(aDBFFileName).ToLower
        Select Case lFileNameOnly
            Case "cat", "huc", NATIONAL_HUC8 : Return "CU"
            Case "huc12" : Return "HUC_12"
            Case NATIONAL_CNTY : Return "FIPS"
            Case NATIONAL_ST : Return "ST"
            Case "catchment" : Return "COMID"
            Case Else
                If lFileNameOnly.StartsWith("wbdhu8") Then
                    Return "HUC_8"
                End If
        End Select
        Return ""
    End Function

    Friend Function DBFDescriptionFieldName(ByVal aDBFFileName As String) As String
        Dim lFileNameOnly As String = IO.Path.GetFileNameWithoutExtension(aDBFFileName).ToLower
        Select Case lFileNameOnly
            Case "cat", "huc", NATIONAL_HUC8 : Return "catname"
            Case "huc12" : Return "HU_12_NAME"
            Case NATIONAL_CNTY : Return "cntyname"
            Case NATIONAL_ST : Return "ST"
            Case Else
                If lFileNameOnly.StartsWith("wbdhu8") Then
                    Return "SUBBASIN"
                End If
        End Select
        Return ""
    End Function

    Friend Function MatchingKeyRecord(ByVal aShapeFilename As String, ByVal aText As String) As Integer
        Dim lKeyFieldName As String = DBFKeyFieldName(aShapeFilename).ToLower
        If lKeyFieldName.Length > 0 Then
            Dim lDBF As atcTableDBF = LayerDBF(aShapeFilename)
            Dim lKeyFieldNum As Integer = lDBF.FieldNumber(lKeyFieldName)
            If lKeyFieldNum > 0 AndAlso lDBF.FindFirst(lKeyFieldNum, aText) Then
                Return lDBF.CurrentRecord - 1
            End If
        End If
        Return -1
    End Function

    'Private Function MatchingDescriptionRecord(ByVal aShapeFilename As String, ByVal aText As String) As Integer
    '    Dim lDescriptionFieldName As String = DBFDescriptionFieldName(aShapeFilename).ToLower
    '    If lDescriptionFieldName.Length > 0 Then
    '        Dim lDBF As atcTableDBF = LayerDBF(aShapeFilename)
    '        Dim lDescriptionFieldNum As Integer = lDBF.FieldNumber(lDescriptionFieldName)
    '        If lDescriptionFieldNum > 0 Then
    '            aText = aText.Trim.ToLower
    '            Dim lLastRecord As Integer = lDBF.NumRecords
    '            While lDBF.CurrentRecord <= lLastRecord
    '                If lDBF.Value(lDescriptionFieldNum).ToLower.Contains(aText) Then
    '                    Return lDBF.CurrentRecord - 1
    '                End If
    '                If lDBF.CurrentRecord < lLastRecord Then
    '                    lDBF.CurrentRecord += 1
    '                Else 'force exit since attempting to set CurrentRecord beyond NumRecords sets it back to 1
    '                    Exit While
    '                End If
    '            End While
    '        End If
    '    End If
    '    Return -1
    'End Function

    Private pLayerDBFs As New Generic.List(Of atcUtility.atcTableDBF)
    Private Function LayerDBF(ByVal aShapeFileName As String) As atcTableDBF
        Dim lDBFFileName As String = IO.Path.ChangeExtension(aShapeFileName, "dbf").ToLower
        Dim lLayer As atcTableDBF
        For Each lLayer In pLayerDBFs
            If lLayer.FileName = lDBFFileName Then
                Return lLayer
            End If
        Next
        If IO.File.Exists(lDBFFileName) Then
            lLayer = New atcTableDBF
            If lLayer.OpenFile(lDBFFileName) Then
                pLayerDBFs.Add(lLayer)
                Return lLayer
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Full path of a DotSpatial DataSet
    ''' </summary>
    Public Function DotSpatialDataSetFilename(ByVal aDataSet As DotSpatial.Data.DataSet) As String
        Dim lLayerFileName As String = Nothing
        If aDataSet Is Nothing Then
            Return Nothing
        ElseIf TypeOf (aDataSet) Is DotSpatial.Data.FeatureSet Then
            lLayerFileName = CType(aDataSet, DotSpatial.Data.FeatureSet).Filename
        ElseIf TypeOf (aDataSet) Is DotSpatial.Data.IRaster Then
            lLayerFileName = CType(aDataSet, DotSpatial.Data.IRaster).Filename
        ElseIf TypeOf (aDataSet) Is DotSpatial.Data.ImageCoverage Then
            lLayerFileName = CType(aDataSet, DotSpatial.Data.ImageCoverage).FileName
        End If
        If Not String.IsNullOrEmpty(lLayerFileName) Then
            Try
                Return IO.Path.Combine(g_AppManager.SerializationManager.CurrentProjectDirectory, lLayerFileName)
            Catch
                Return lLayerFileName
            End Try
        End If

        Return ""
        'Throw New ApplicationException("Cannot get file name for dataset '" & aDataSet.Name & "'")
    End Function

    ''' <summary>
    ''' Layer Handle of first 8-digit HUC layer found or -1 if no HUC-8 layer is on the map
    ''' </summary>
    Friend Function Huc8Layer() As DotSpatial.Symbology.ILayer
        For Each lSearchLayer In g_Map.GetAllLayers
            Dim lFileName As String = DotSpatialDataSetFilename(lSearchLayer.DataSet)
            If Not String.IsNullOrWhiteSpace(lFileName) Then
                Select Case IO.Path.GetFileName(lFileName).ToLower
                    Case "cat.shp", "huc250d3.shp", "wbdhu8.shp"
                        Return lSearchLayer
                End Select
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Layer Handle of first 12-digit HUC layer found or -1 if no HUC-12 layer is on the map
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Huc12Layer() As DotSpatial.Symbology.ILayer
        For Each lSearchLayer In g_Map.GetAllLayers
            Dim lFileName As String = DotSpatialDataSetFilename(lSearchLayer.DataSet)
            If Not String.IsNullOrWhiteSpace(lFileName) AndAlso lFileName.ToLower.Contains("huc12") Then
                Return lSearchLayer
            End If
        Next
        Return Nothing
    End Function

    Public Function D4EMLayerFindOrAdd(ByVal aFilename As String, ByVal aLayerSpecification As D4EM.Data.LayerSpecification) As D4EM.Data.Layer
        Dim lLayer = g_NationalProject.LayerFromFileName(aFilename)
        If lLayer Is Nothing Then
            lLayer = New D4EM.Data.Layer(aFilename, aLayerSpecification, False)
            g_NationalProject.Layers.Add(lLayer)
        End If
        Return lLayer
    End Function

    Public Function D4EMLayerFindOrAdd(ByVal aFeatureSet As DotSpatial.Data.FeatureSet, ByVal aLayerSpecification As D4EM.Data.LayerSpecification) As D4EM.Data.Layer
        Dim lLayer As D4EM.Data.Layer = Nothing
        If Not String.IsNullOrEmpty(aFeatureSet.Filename) Then
            lLayer = g_NationalProject.LayerFromFileName(aFeatureSet.Filename)
        End If
        If lLayer Is Nothing Then
            lLayer = New D4EM.Data.Layer(aFeatureSet, aLayerSpecification)
            g_NationalProject.Layers.Add(lLayer)
        End If
        Return lLayer
    End Function

    Friend Function MapLayer(ByVal aFileName As String) As DotSpatial.Symbology.ILayer
        For Each lSearchLayer In g_Map.GetAllLayers
            Dim lFileName As String = DotSpatialDataSetFilename(lSearchLayer.DataSet)
            If Not String.IsNullOrWhiteSpace(lFileName) AndAlso lFileName.ToLower.EndsWith(aFileName.ToLower) Then
                Return lSearchLayer
            End If
        Next
        Return Nothing
    End Function

    Friend Function EnsureLayerOnMap(ByVal lShapeFileName As String, ByVal aLegendText As String) As DotSpatial.Symbology.ILayer
        If IO.File.Exists(lShapeFileName) Then
            Dim lMapLayer As DotSpatial.Symbology.ILayer = MapLayer(lShapeFileName)
            If lMapLayer Is Nothing Then
                lMapLayer = g_Map.Layers.Add(lShapeFileName)
                'TODO: also load Flowline and Waterbody along with NHDPlus CatchmentPolygons
            End If
            If lMapLayer IsNot Nothing Then
                lMapLayer.LegendText = aLegendText
                SelectLayer(lMapLayer)
                Return lMapLayer
            End If
        End If
        Return Nothing
    End Function

    Friend Sub SelectLayer(ByVal aSelectThisLayer As DotSpatial.Symbology.ILayer)
        g_Map.Layers.SelectedLayer = aSelectThisLayer
        For Each lLayer In g_Map.GetAllLayers
            If lLayer Is aSelectThisLayer Then
                lLayer.IsSelected = True
            Else
                lLayer.IsSelected = False
                If TypeOf (lLayer) Is DotSpatial.Symbology.FeatureLayer Then
                    CType(lLayer, DotSpatial.Symbology.FeatureLayer).Selection.Clear()
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' When a new layer is added to g_NationalProject.Layers, make sure it is also on the map.
    ''' </summary>
    ''' <param name="sender">not used</param>
    ''' <param name="e">e.NewItems is a collection of D4EM.Data.Layer</param>
    ''' <remarks></remarks>
    Public Sub Layers_AddingNew(ByVal sender As Object, ByVal e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
        If g_AddLayers Then
            Select Case e.Action
                Case Specialized.NotifyCollectionChangedAction.Add
                    For Each lD4EMLayer As D4EM.Data.Layer In e.NewItems
                        Try
                            If lD4EMLayer.IsFeatureSet OrElse lD4EMLayer.AsRaster.Projection.Matches(g_Map.Projection, Nothing) Then
                                Dim lMapLayer = MapLayer(lD4EMLayer.FileName)
                                If lMapLayer Is Nothing Then
                                    With g_Map.Layers.Add(lD4EMLayer.DataSet)
                                        .LegendText = lD4EMLayer.Specification.ToString()
                                    End With
                                Else 'Make sure same instance of opened dataset is on map and in g_NationalProject.Layers
                                    lD4EMLayer.DataSet = lMapLayer.DataSet
                                End If
                                If lD4EMLayer.DataSet.Extent.Width > 0 AndAlso lD4EMLayer.DataSet.Extent.Height > 0 Then
                                    g_Map.ViewExtents = lD4EMLayer.DataSet.Extent
                                End If
                                System.Windows.Forms.Application.DoEvents()
                            End If
                        Catch ex As Exception
                            Logger.Dbg("Layers_AddingNew: " & ex.Message & " " & ex.StackTrace)
                        End Try
                    Next
            End Select
        End If
    End Sub

    Public Function SpecifyAndCreateNewProjectsWithLayerCollectionChanged(ByVal aParameters As SDMParameters) As Generic.List(Of String)
        Dim lCreatedProjectFilenames As New Generic.List(Of String)
        Dim lProjectIndex As Integer = 0
        For Each lProject In aParameters.Projects
            Logger.Status("Creating project " & lProject.Region.ToString)
            Using lLevel As New ProgressLevel(True)
                If g_AddLayers Then AddHandler lProject.Layers.CollectionChanged, AddressOf Layers_AddingNew
                DownloadDataSetupModels(lProject, aParameters)
                If g_AddLayers Then RemoveHandler lProject.Layers.CollectionChanged, AddressOf Layers_AddingNew
                If IO.File.Exists(lProject.ProjectFilename) Then
                    Logger.Status("Finished Building " & lProject.ProjectFilename, True)
                    lCreatedProjectFilenames.Add(lProject.ProjectFilename)
                End If
            End Using
            lProjectIndex += 1
            Logger.Progress(lProjectIndex, aParameters.Projects.Count)
        Next

        If lCreatedProjectFilenames.Count = 1 Then
            Select Case Logger.MsgCustom("Finished Building Project " & vbCrLf & lCreatedProjectFilenames(0), g_AppNameLong, _
                                         "Ok", "Open Folder", "Open in BASINS")
                Case "Open Folder" : OpenFile(IO.Path.GetDirectoryName(lCreatedProjectFilenames(0)))
                Case "Open in BASINS"
                    Dim lBASINS_EXE As String = atcUtility.FindFile("Please locate BASINS.exe", "BASINS.exe")
                    If IO.File.Exists(lBASINS_EXE) Then
                        LaunchProgram(lBASINS_EXE, IO.Path.GetDirectoryName(lCreatedProjectFilenames(0)), """" & lCreatedProjectFilenames(0) & """", False)
                    End If
            End Select
        Else
            Logger.Msg("Finished Building " & lCreatedProjectFilenames.Count & " Projects", g_AppNameLong)
        End If
        Return lCreatedProjectFilenames
    End Function

    Friend Sub LoadHUC12(ByVal aHUC8 As String)
        Dim lHUC12ShapeFileName As String = IO.Path.Combine(g_NationalProject.ProjectFolder, "huc12", aHUC8, "huc12.shp")
        If Not IO.File.Exists(lHUC12ShapeFileName) Then
            'download HUC-12 layer
            D4EM.Data.Source.BASINS.GetBASINS(g_NationalProject,
                                              IO.Path.GetDirectoryName(lHUC12ShapeFileName),
                                              aHUC8,
                                              D4EM.Data.Source.BASINS.LayerSpecifications.huc12)
        End If

        If IO.File.Exists(lHUC12ShapeFileName) Then
            If MapLayer(lHUC12ShapeFileName) IsNot Nothing Then
                Exit Sub 'Layer is already on the map
            Else
                Dim lHuc12Layer = g_Map.Layers.Add(lHUC12ShapeFileName)
                lHuc12Layer.LegendText = D4EM.Data.Source.BASINS.LayerSpecifications.huc12.Name & " for " & aHUC8
                If lHuc12Layer IsNot Nothing Then
                    SelectLayer(lHuc12Layer)
                    'lHuc12Layer.IsSelected = True
                    g_Map.ViewExtents = lHuc12Layer.Extent
                End If
            End If
        End If
    End Sub

    ' ''' <summary>
    ' ''' Add NHDPlus flowlines and catchments to the national map
    ' ''' </summary>
    ' ''' <param name="aHUC8">8-digit HUC to get NHDPlus layers for</param>
    ' ''' <remarks></remarks>
    'Friend Sub LoadNationalNHDPlus(ByVal aHUC8 As String)
    '    Logger.Status("Getting data for HUC " + aHUC8)

    '    Dim lCatchmentFileName As String = IO.Path.Combine(g_NationalProject.ProjectFolder, "NHDPlus", "nhdplus" & aHUC8, "drainage", "catchment.shp")
    '    Dim lFlowlineFileName As String = IO.Path.Combine(g_NationalProject.ProjectFolder, "NHDPlus", "nhdplus" & aHUC8, "hydrography", "nhdflowline.shp")

    '    If Not IO.File.Exists(lCatchmentFileName) OrElse Not IO.File.Exists(lFlowlineFileName) Then
    '        D4EM.Data.Source.NHDPlus.GetNHDPlus(g_NationalProject, "NHDPlus", aHUC8, True,
    '                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline,
    '                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody,
    '                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
    '    End If

    '    Dim lCatchmentPolygonsLayer = g_NationalProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons.Tag)
    '    If lCatchmentPolygonsLayer IsNot Nothing Then
    '        Dim lCatchmentMapLayer = g_Map.Layers.Add(lCatchmentPolygonsLayer.AsFeatureSet)
    '        If lCatchmentMapLayer IsNot Nothing Then
    '            lCatchmentMapLayer.LegendText = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons.Name & " for " & aHUC8
    '            g_Map.Layers.SelectedLayer = lCatchmentMapLayer
    '            g_Map.ViewExtents = lCatchmentMapLayer.Extent
    '        End If
    '    Else
    '        Logger.Dbg("Unable to find NHDPlus catchment file: " + lCatchmentFileName + " : " + System.Reflection.MethodInfo.GetCurrentMethod().ToString())
    '    End If

    '    Dim lFlowlineLayer = g_NationalProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline.Tag)
    '    If lFlowlineLayer IsNot Nothing Then
    '        Dim lFlowlineMapLayer = g_Map.Layers.Add(lFlowlineLayer.AsFeatureSet)
    '        If lFlowlineMapLayer IsNot Nothing Then
    '            lFlowlineMapLayer.LegendText = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline.Name & " for " & aHUC8
    '            'g_Map.Layers.SelectedLayer = lFlowlineLayer
    '            'g_Map.ViewExtents = lFlowlineLayer.Extent
    '        End If
    '    Else
    '        Logger.Dbg("Unable to find NHDPlus flowline file: " + lFlowlineFileName + " : " + System.Reflection.MethodInfo.GetCurrentMethod().ToString())
    '    End If

    '    Logger.Status("")

    'End Sub

    Friend Function LoadLayerForHUC8(ByVal aLayerToLoad As D4EM.Data.LayerSpecification, ByVal aHUC8 As String) As DotSpatial.Symbology.ILayer
        Logger.Status("Getting data for HUC " + aHUC8)
        Dim lLayerHandle As DotSpatial.Symbology.ILayer = Nothing
        Dim lShapeFileNames As New Generic.List(Of String)
        Select Case aLayerToLoad
            Case D4EM.Data.Region.RegionTypes.huc12
                lShapeFileNames.Add(IO.Path.Combine(g_NationalProject.ProjectFolder, "huc12", aHUC8, "huc12.shp"))
            Case D4EM.Data.Region.RegionTypes.catchment
                lShapeFileNames.Add(IO.Path.Combine(g_NationalProject.ProjectFolder, "NHDPlus", "nhdplus" & aHUC8, "hydrography", "nhdflowline.shp"))
                lShapeFileNames.Add(IO.Path.Combine(g_NationalProject.ProjectFolder, "NHDPlus", "nhdplus" & aHUC8, "hydrography", "nhdwaterbody.shp"))
                lShapeFileNames.Add(IO.Path.Combine(g_NationalProject.ProjectFolder, "NHDPlus", "nhdplus" & aHUC8, "drainage", "catchment.shp")) 'Listed last so its layer is the one returned
        End Select
        For Each lShapeFileName As String In lShapeFileNames
            If IO.File.Exists(lShapeFileName) Then
                lLayerHandle = MapLayer(lShapeFileName)
                If lLayerHandle IsNot Nothing Then
                    Logger.Progress(100, 100)
                    Return lLayerHandle 'Layer is already on the map
                End If
                lLayerHandle = MapLayer(lShapeFileName.Replace(g_NationalProject.ProjectFolder, ""))
                If lLayerHandle IsNot Nothing Then
                    Logger.Progress(100, 100)
                    Return lLayerHandle 'Layer is already on the map
                End If
            Else 'download layer
                Select Case aLayerToLoad
                    Case D4EM.Data.Region.RegionTypes.huc12
                        D4EM.Data.Source.BASINS.GetBASINS(g_NationalProject,
                                                          IO.Path.GetDirectoryName(lShapeFileName),
                                                          aHUC8,
                                                          D4EM.Data.Source.BASINS.LayerSpecifications.huc12)
                    Case D4EM.Data.Region.RegionTypes.catchment
                        D4EM.Data.Source.NHDPlus.GetNHDPlus(g_NationalProject, "NHDPlus", aHUC8, True,
                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline,
                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody,
                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
                End Select
            End If
            lLayerHandle = EnsureLayerOnMap(lShapeFileName, IO.Path.GetFileNameWithoutExtension(lShapeFileName) & " for " & aHUC8)
        Next
        Logger.Progress(100, 100)
        Return lLayerHandle
    End Function

    Friend Function FormIsOpen(ByVal aForm As System.Windows.Forms.Form) As Boolean
        If aForm Is Nothing Then
            Return False
        ElseIf aForm.IsDisposed Then
            aForm = Nothing
            Return False
        Else
            Return True
        End If
    End Function

    Public Function NationalProjectIsOpen() As Boolean
        Return (g_Map IsNot Nothing) _
            AndAlso (g_Map.GetAllLayers.Count > 0) _
            AndAlso g_AppManager IsNot Nothing _
            AndAlso g_AppManager.SerializationManager IsNot Nothing _
            AndAlso Not String.IsNullOrEmpty(g_AppManager.SerializationManager.CurrentProjectFile) _
            AndAlso NationalProjectLayersLoaded()
    End Function

    Public Function NationalProjectLayersLoaded() As Boolean

        Dim bReturn As Boolean
        Dim lstMapFiles As New List(Of String)
        Dim lstNationalFiles As New List(Of String)

        lstNationalFiles.Add(NATIONAL_HUC8)
        lstNationalFiles.Add(NATIONAL_ST)
        lstNationalFiles.Add(NATIONAL_CNTY)

        For Each lLayer In g_Map.GetAllLayers()
            'KW: Oct. 6, 2014 - Some additional error checking. 
            If (lLayer Is Nothing) Then Continue For
            If (lLayer.DataSet Is Nothing) Then Continue For
            If (lLayer.DataSet.Name Is Nothing) Then Continue For
            lstMapFiles.Add(lLayer.DataSet.Name.ToLower)
        Next

        If (lstMapFiles.Intersect(lstNationalFiles).Count() < 3) Then
            bReturn = False
        Else
            bReturn = True
        End If

        Return bReturn

    End Function
    ''' <summary>
    ''' Parse numeric keys out of the given text
    ''' </summary>
    ''' <param name="aKeyLength">Expected length of each key. If zero, any length is allowed.</param>
    ''' <param name="aTextToParse"></param>
    ''' <returns>list of keys</returns>
    ''' <remarks>If a key is found that is one digit too short, a zero is prepended</remarks>
    Public Function ParseNumericKeys(ByVal aKeyLength As Integer, ByVal aTextToParse As String) As Generic.List(Of String)
        Dim lFoundList As New Generic.List(Of String)
        Dim lKey As String = ""
        For lCharPos As Integer = 0 To aTextToParse.Length - 1
            If IsNumeric(aTextToParse.Substring(lCharPos, 1)) Then
                lKey &= aTextToParse.Substring(lCharPos, 1)
            Else
                If lKey.Length = aKeyLength - 1 Then lKey = "0" & lKey
                If lKey.Length > 0 Then
                    If aKeyLength = 0 OrElse lKey.Length = aKeyLength Then lFoundList.Add(lKey)
                    lKey = ""
                End If
            End If
        Next
        If lKey.Length = aKeyLength - 1 Then lKey = "0" & lKey
        If lKey.Length = aKeyLength OrElse aKeyLength = 0 Then lFoundList.Add(lKey)
        Return lFoundList
    End Function

    'Private Function SelectUpstream(ByVal aFlowlines As DotSpatial.Data.FeatureSet,
    '                                ByVal aCatchments As DotSpatial.Data.FeatureSet,
    '                                ByVal aOutletComId As Long,
    '                                ByVal aMaxKM As Integer) As D4EM.Data.Region

    '    Dim aFields As New D4EM.Geo.NetworkOperations.FieldIndexes(aFlowlines, aCatchments)
    '    Dim lWatershedShape As DotSpatial.Data.IFeature = Nothing
    '    g_Map.Layers.Item(0).Checked = True

    '    Dim lCatchmentDBF As atcTableDBF = LayerDBF(aFlowlines.Filename)
    '    If lCatchmentDBF.FindFirst(aFields.FlowlinesComId, aOutletComId) Then
    '        Dim lUpstreamComIds As Generic.List(Of Long) = D4EM.Geo.NetworkOperations.FindUpstreamKeys(aOutletComId, aFlowlines, aFields.FlowlinesComId, aFields.FlowlinesDownstreamComId)
    '        For Each lCOMID As Long In lUpstreamComIds
    '            If lCatchmentDBF.FindFirst(aFields.FlowlinesComId, lCOMID) Then
    '                Try
    '                    Dim lFeature = aCatchments.Features(lCatchmentDBF.CurrentRecord - 1)
    '                    If lWatershedShape Is Nothing Then
    '                        lWatershedShape = lFeature.Clone
    '                    Else
    '                        Dim lExpandedWatershed = DotSpatial.Data.FeatureExt.Union(lWatershedShape, lFeature)
    '                        lWatershedShape = lExpandedWatershed
    '                    End If
    '                Catch ex As Exception
    '                    Logger.Dbg("Error: CombineCatchments:DotSpatial.Data.FeatureExt.Union " & ex.Message)
    '                End Try
    '            Else
    '                Logger.Dbg("Error: CombineCatchments: COMID Not Found: " & lCOMID)
    '            End If
    '        Next
    '    End If
    '    Return New D4EM.Data.Region(lWatershedShape.ToShape(), aCatchments.Projection)
    'End Function


    ''' <summary>
    ''' Create a new region by merging the catchment polygon with the given key with upstream catchments
    ''' </summary>
    ''' <param name="aFlowlines">Each catchment has a corresponding flowline which includes needed connectivity and distance information</param>
    ''' <param name="aCatchments">Catchments available to merge into a watershed</param>
    ''' <param name="aOutletComId">ID of catchment to select upstream from</param>
    ''' <param name="aMaxKM">Maximum distance in kilometers to follow flowlines upstream</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetNHDPlusPourpointWatershed(ByVal aFlowlines As DotSpatial.Data.FeatureSet,
                                                 ByVal aCatchments As DotSpatial.Data.FeatureSet,
                                                 ByVal aOutletComId As Long,
                                                 ByVal aMaxKM As Integer) As D4EM.Data.Region

        Dim lWatershedShape As DotSpatial.Data.IFeature = Nothing
        Dim lFields As New D4EM.Geo.NetworkOperations.FieldIndexes(aFlowlines, aCatchments)

        Dim lCatchmentDBF As atcTableDBF = LayerDBF(aFlowlines.Filename)
        If lCatchmentDBF.FindFirst(lFields.FlowlinesComId, aOutletComId) Then
            lWatershedShape = aCatchments.Features(lCatchmentDBF.CurrentRecord - 1).Clone
            Dim lUpstreamComIds As Generic.List(Of Long) = D4EM.Geo.NetworkOperations.FindUpstreamKeys(aOutletComId, aFlowlines, lFields.FlowlinesComId, lFields.FlowlinesDownstreamComId)
            For Each lCOMID As Long In lUpstreamComIds
                If lCatchmentDBF.FindFirst(lFields.FlowlinesComId, lCOMID) Then
                    Try
                        Dim lExpandedWatershed = DotSpatial.Data.FeatureExt.Union(lWatershedShape, aCatchments.Features(lCatchmentDBF.CurrentRecord - 1))
                        lWatershedShape = lExpandedWatershed
                    Catch ex As Exception
                        Logger.Dbg("Error: CombineCatchments:DotSpatial.Data.FeatureExt.Union " & ex.Message)
                    End Try
                Else
                    Logger.Dbg("Error: CombineCatchments: COMID Not Found: " & lCOMID)
                End If
            Next
        End If
        Dim lRegion As New D4EM.Data.Region(lWatershedShape.ToShape(), aCatchments.Projection)
        'TODO: lRegion.SetKeys(D4EM.Data.Region.RegionTypes.catchment, lAllComIDString)
        Return lRegion
    End Function

End Module
