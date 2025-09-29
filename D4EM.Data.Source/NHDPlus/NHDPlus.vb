Imports MapWinUtility
Imports atcUtility
Imports D4EM.Data.LayerSpecification
Imports D4EM.Geo

Public Class NHDPlus

    Public Class LayerSpecifications
        Public Shared all As New LayerSpecification(Tag:="all")
        Public Class Hydrography
            Public Shared Area As New LayerSpecification(
                Name:="NHDPlus Area", FilePattern:="nhdarea.shp", Tag:="nhdarea", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
            Public Shared Flowline As New LayerSpecification(
                Name:="NHDPlus Flowline", FilePattern:="nhdflowline.shp", Tag:="nhdflowline", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
            Public Shared Line As New LayerSpecification(
                Name:="NHDPlus Line", FilePattern:="nhdline.shp", Tag:="nhdline", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
            Public Shared Point As New LayerSpecification(
                Name:="NHDPlus Point", FilePattern:="nhdpoint.shp", Tag:="nhdpoint", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
            Public Shared Waterbody As New LayerSpecification(
                Name:="NHDPlus Waterbody", FilePattern:="nhdwaterbody.shp", Tag:="nhdwaterbody", Role:=Roles.Hydrography, IdFieldName:="COMID", Source:=GetType(NHDPlus))
        End Class
        Public Class HydrologicUnits
            Public Shared SubregionPolygons As New LayerSpecification(
                Name:="NHDPlus Subregion", FilePattern:="subregion.shp", Tag:="subregion", Role:=Roles.SubBasin, Source:=Reflection.MethodInfo.GetCurrentMethod.DeclaringType)
            Public Shared RegionPolygons As New LayerSpecification(
                Name:="NHDPlus Region", FilePattern:="region.shp", Tag:="region", Role:=Roles.SubBasin, Source:=Reflection.MethodInfo.GetCurrentMethod.DeclaringType)
            Public Shared SubBasinPolygons As New LayerSpecification(
                Name:="NHDPlus HUC-8", FilePattern:="subbasin.shp", Tag:="subbasin", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
            Public Shared SubWatershedPolygons As New LayerSpecification(
                Name:="NHDPlus Subwatershed", FilePattern:="subwatershed.shp", Tag:="subwatershed", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
            Public Shared WatershedPolygons As New LayerSpecification(
                Name:="NHDPlus Watershed", FilePattern:="watershed.shp", Tag:="watershed", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
        End Class

        Public Shared CatchmentPolygons As LayerSpecification = D4EM.Data.Region.RegionTypes.catchment
        Public Shared CatchmentGrid As New LayerSpecification(
            Name:="NHDPlus Catalog Unit Grid", FilePattern:="cat.tif", Tag:="cat", Role:=Roles.SubBasin, Source:=GetType(NHDPlus))
        Public Shared ElevationGrid As New LayerSpecification(
            Name:="NHDPlus Elevation", FilePattern:="elev_cm.tif", Tag:="elev_cm", Role:=Roles.Elevation, Source:=GetType(NHDPlus))
        Public Shared SlopeGrid As New LayerSpecification(
            Name:="NHDPlus Slope Grid", FilePattern:="slope.tif", Tag:="slope", Role:=Roles.Slope, Source:=GetType(NHDPlus))
        Public Shared FlowAccumulationGrid As New LayerSpecification(
            Name:="NHDPlus Flow Accumulation Grid", FilePattern:="fac.tif", Tag:="fac", Source:=GetType(NHDPlus))
        Public Shared FlowDirectionGrid As New LayerSpecification(
            Name:="NHDPlus Flow Direction Grid", FilePattern:="fdr.tif", Tag:="fdr", Source:=GetType(NHDPlus))
    End Class

    Private Shared NativeShapeProjection As DotSpatial.Projections.ProjectionInfo = Globals.GeographicProjection
    Private Shared NativeGridProjection As DotSpatial.Projections.ProjectionInfo = Globals.AlbersProjection
    'Private Shared pBaseURL As String = "ftp://horizon-systems.com/NHDPlus/NHDPlusV1/NHDPlusExtensions/SubBasins/NHDPlus"
    'KW 10/2/2014 - above link no longing working.  Data has been moved to an Amazon EC2 site
    'Private Shared pBaseURL As String = "ftp://ec2-54-227-241-43.compute-1.amazonaws.com/NHDplus/NHDPlusV1/NHDPlusExtensions/SubBasins/NHDPlus"
    'PBD 9/16/2016 - above link no longing working.
    'Private Shared pBaseURL As String = "https://s3.amazonaws.com/nhdplus/NHDPlusV1/NHDPlusExtensions/SubBasins/NHDPlus"
    'Private Shared pBaseURL2 As String = "https://s3.amazonaws.com/nhdplus/NHDPlusV2/NHDPlusExtensions/SubBasins/NHDPlus"
    'Private Shared pBaseURLnew As String = "ftp://newftp.epa.gov/exposure/BasinsData/NHDPlus21/"
    'Private Shared pBaseURLga As String = "https://gaftp.epa.gov/Exposure/BasinsData/NHDPlus21/"
    Private Shared pBaseURL As String = "https://usgs.osn.mghpcc.org/mdmf/epa_basins/NHDPlus21/"

    ''' <summary>
    ''' Download and unpack NHDPlus data (for v2.1)
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder (e.g. "NHDPlus") or full path of folder to save in (e.g. "C:\NHDPlus").
    '''  If nothing or empty string, will save in aProject.ProjectFolder.</param>
    ''' <param name="aHUC8">8-digit hydrologic unit code to download</param>
    ''' <param name="aJoinAttributes">True to merge value-added attributes to .dbf of shape file, False to leave in separate .dbf</param>
    ''' <param name="aDataTypes">types of data to unpack: valid values are in NHDPlus.LayerSpecifications. If none specified, all data will be processed</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetNHDPlus2(ByVal aProject As D4EM.Data.Project,
                                      ByVal aSaveFolder As String,
                                      ByVal aHUC8 As String,
                                      ByVal aJoinAttributes As Boolean,
                                      ByVal ParamArray aDataTypes() As LayerSpecification) As String
        Dim lResult As String = Nothing
        Dim lVersion As Integer = 2
        lResult &= GetNHDPlusForSpecifiedVersion(aProject,
                                                 aSaveFolder,
                                                 aHUC8,
                                                 aJoinAttributes,
                                                 lVersion,
                                                 aDataTypes)
        Return lResult
    End Function

    ''' <summary>
    ''' Download and unpack NHDPlus data (for v1.0)
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder (e.g. "NHDPlus") or full path of folder to save in (e.g. "C:\NHDPlus").
    '''  If nothing or empty string, will save in aProject.ProjectFolder.</param>
    ''' <param name="aHUC8">8-digit hydrologic unit code to download</param>
    ''' <param name="aJoinAttributes">True to merge value-added attributes to .dbf of shape file, False to leave in separate .dbf</param>
    ''' <param name="aDataTypes">types of data to unpack: valid values are in NHDPlus.LayerSpecifications. If none specified, all data will be processed</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetNHDPlus(ByVal aProject As D4EM.Data.Project,
                                      ByVal aSaveFolder As String,
                                      ByVal aHUC8 As String,
                                      ByVal aJoinAttributes As Boolean,
                                      ByVal ParamArray aDataTypes() As LayerSpecification) As String
        Dim lResult As String = Nothing
        Dim lVersion As Integer = 1
        lResult &= GetNHDPlusForSpecifiedVersion(aProject,
                                                 aSaveFolder,
                                                 aHUC8,
                                                 aJoinAttributes,
                                                 lVersion,
                                                 aDataTypes)
        Return lResult
    End Function

    ''' <summary>
    ''' Download and unpack NHDPlus data, with argument to specify which version
    ''' </summary>
    ''' <param name="aProject">project to add data to</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder (e.g. "NHDPlus") or full path of folder to save in (e.g. "C:\NHDPlus").
    '''  If nothing or empty string, will save in aProject.ProjectFolder.</param>
    ''' <param name="aHUC8">8-digit hydrologic unit code to download</param>
    ''' <param name="aJoinAttributes">True to merge value-added attributes to .dbf of shape file, False to leave in separate .dbf</param>
    ''' <param name="aVersion">version number to download, supports 1 and 2</param>
    ''' <param name="aDataTypes">types of data to unpack: valid values are in NHDPlus.LayerSpecifications. If none specified, all data will be processed</param>
    ''' <returns>XML describing success or error</returns>
    Private Shared Function GetNHDPlusForSpecifiedVersion(ByVal aProject As D4EM.Data.Project,
                                                          ByVal aSaveFolder As String,
                                                          ByVal aHUC8 As String,
                                                          ByVal aJoinAttributes As Boolean,
                                                          ByVal aVersion As Integer,
                                                          ByVal ParamArray aDataTypes() As LayerSpecification) As String
        Dim lResult As String = Nothing
        Dim lSaveIn As String = aProject.ProjectFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        IO.Directory.CreateDirectory(lSaveIn)

        Dim lClipRegion As Region = Nothing
        If aProject.Clip Then lClipRegion = aProject.Region
        Dim lHUC2 As String = aHUC8.Substring(0, 2)
        Dim lBaseFilename As String = "NHDPlus" & aHUC8 & ".zip"
        Dim lZipFilename As String = IO.Path.Combine(IO.Path.Combine(aProject.CacheFolder, "NHDPlus"), lBaseFilename)
        If aVersion = 2 Then
            lZipFilename = IO.Path.Combine(IO.Path.Combine(aProject.CacheFolder, "NHDPlus2"), lBaseFilename)
        End If

Retry:
        'If the cached file already exists and either we need to get a new one anyway or it is too short to be real, then delete cached file
        If IO.File.Exists(lZipFilename) AndAlso (aProject.GetEvenIfCached OrElse FileLen(lZipFilename) < 500) Then
            While Not TryDelete(lZipFilename)
                If Logger.Msg("Unable to delete '" & lZipFilename & "'", vbRetryCancel, "Need to delete file") = MsgBoxResult.Cancel Then
                    Return ""
                End If
            End While
        End If

        If Not IO.File.Exists(lZipFilename) Then
            Dim lURL As String
            Logger.Status("Downloading NHD Plus")
            Dim lBaseURL As String = pBaseURL & lHUC2
            If aVersion = 2 Then
                lBaseURL = pBaseURL '& lHUC2
            End If
            If lHUC2 = "10" And aVersion <> 2 Then
                lURL = lBaseURL & "L/" & lBaseFilename
                If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                    lURL = lBaseURL & "U/" & lBaseFilename
                    If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                        Throw New ApplicationException("Unable to locate NHDPlus v1.0 for " & aHUC8)
                    End If
                End If
            Else
                lURL = lBaseURL & lBaseFilename
                If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                    If aVersion = 2 Then
                        lURL = pBaseURL & lBaseFilename
                        If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                            Throw New ApplicationException("Unable to locate NHDPlus v2.1 for " & aHUC8)
                        End If
                    Else
                        Throw New ApplicationException("Unable to locate NHDPlus v1.0 for " & aHUC8)
                    End If
                End If
            End If
        End If

        Dim lUnZipFolder As String = NewTempDir(IO.Path.Combine(aProject.CacheFolder, "NHDPlus_Unzip"))
        Dim lHUCUnZipFolder As String = IO.Path.Combine(lUnZipFolder, "NHDPlus" & aHUC8)
        If IO.File.Exists(lZipFilename) Then

            'Delete directory if already exists, do a check if it exists
            If IO.Directory.Exists(lHUCUnZipFolder) Then
                TryDelete(lHUCUnZipFolder)
            End If

            Logger.Status("Unzipping NHDPlus")
            Try
                Zipper.UnzipFile(lZipFilename, lUnZipFolder) 'will create lHUCCacheFolder
            Catch e As Exception
                If Logger.Msg(lZipFilename & vbCrLf _
                            & IO.File.GetLastWriteTime(lZipFilename) & vbCrLf _
                            & Format(FileLen(lZipFilename), "#,##0") & " bytes" & vbCrLf _
                            & "Delete file and retry download?", "Unable to Unzip NHDPlus") = MsgBoxResult.Yes Then
                    If TryDelete(lZipFilename, True) Then
                        GoTo Retry
                    Else
                        Logger.Msg(lZipFilename & vbCrLf _
                            & IO.File.GetLastWriteTime(lZipFilename) & vbCrLf _
                            & Format(FileLen(lZipFilename), "#,##0") & " bytes" & vbCrLf,
                            "Unable to Delete Corrupted NHDPlus download")
                    End If
                Else
                    TryDelete(lZipFilename, True)
                    Throw New ApplicationException("Unable to download NHDPlus for " & aHUC8)
                End If
            End Try
            'If IO.Directory.Exists(lHUCUnZipFolder) Then
            Dim lTempFolder As String = NewTempDir(IO.Path.Combine(aProject.CacheFolder, "NHDPlus_Temp"))
            Dim lTempHUC8folder As String = IO.Path.Combine(lTempFolder, "NHDPlus" & aHUC8)
            Dim lFromFolderLength As Integer = lUnZipFolder.TrimEnd(IO.Path.DirectorySeparatorChar).Length + 1

            Try
                Dim lESRIgridFolders As New ArrayList

                If aDataTypes IsNot Nothing Then
                    If aDataTypes.Count = 0 OrElse aDataTypes.Contains(LayerSpecifications.all) Then
                        aDataTypes = Nothing
                    End If
                End If

                'Convert/Clip/Project grids from unzipped to temp folder
                Logger.Status("Processing NHDPlus Grids")
                Dim lAllESRIgrids As New Collections.Specialized.NameValueCollection
                AddFilesInDir(lAllESRIgrids, lHUCUnZipFolder, True, "dblbnd.adf")
                Logger.Progress(0, lAllESRIgrids.Count)
                Using lLevel As New ProgressLevel(True)
                    For Each lSourceGrid As String In lAllESRIgrids
                        lESRIgridFolders.Add(IO.Path.GetDirectoryName(lSourceGrid) & IO.Path.DirectorySeparatorChar)
                        If TagInTypes(IO.Path.GetFileName(IO.Path.GetDirectoryName(lSourceGrid)).ToLower, aDataTypes) Then
                            'Dim lSavedFilename As String = 
                            ClipProjectGrid(aProject.DesiredProjection,
                                            IO.Path.GetDirectoryName(lSourceGrid),
                                            IO.Path.GetFileName(lSourceGrid),
                                            lTempHUC8folder,
                                            lClipRegion)
                            'If IO.File.Exists(lSavedFilename) Then
                            '    'Logger.Status("Clipped and projected " & IO.Path.GetFileName(lSourceGrid))
                            '    Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lSavedFilename, GetType(NHDPlus.LayerSpecifications))
                            '    If lLayerSpecfication IsNot Nothing Then
                            '        
                            '    End If
                            'End If
                            lLevel.Reset()
                        End If
                    Next
                End Using
                'also do for tif grids used in NHDPlus v2.1
                Dim lAllTifGrids As New Collections.Specialized.NameValueCollection
                AddFilesInDir(lAllTifGrids, lHUCUnZipFolder, True, "*.tif")
                Logger.Progress(0, lAllTifGrids.Count)
                Using lLevel As New ProgressLevel(True)
                    For Each lSourceGrid As String In lAllTifGrids
                        'lTifGridFolders.Add(IO.Path.GetDirectoryName(lSourceGrid) & IO.Path.DirectorySeparatorChar)
                        If TagInTypes(IO.Path.GetFileName(IO.Path.GetDirectoryName(lSourceGrid)).ToLower, aDataTypes) Then
                            ClipProjectGrid(aProject.DesiredProjection,
                                            IO.Path.GetDirectoryName(lSourceGrid),
                                            IO.Path.GetFileName(lSourceGrid),
                                            lTempHUC8folder,
                                            lClipRegion)
                            lLevel.Reset()
                            If IO.File.Exists(IO.Path.ChangeExtension(lSourceGrid, ".prj")) Then
                                TryDelete(IO.Path.ChangeExtension(lSourceGrid, ".prj"))
                            End If
                        End If
                    Next
                End Using

                'Move non-grid files from unzipped to temp
                Dim lAllFilesToMove As New Collections.Specialized.NameValueCollection
                AddFilesInDir(lAllFilesToMove, lHUCUnZipFolder, True)
                For Each lCacheFilename As String In lAllFilesToMove
                    If ShouldMoveNonGrid(lCacheFilename, lESRIgridFolders, aDataTypes) Then
                        Dim lDestination As String = IO.Path.Combine(lTempFolder, lCacheFilename.Substring(lFromFolderLength))
                        If Not IO.File.Exists(lDestination) Then
                            TryMove(lCacheFilename, lDestination)
                        Else
                            Logger.Dbg("Using existing copy of " & lDestination)
                        End If
                    End If
                Next

                'Discard empty shape files (e.g. region, subregion, subwatershed, watershed)
                For Each lShapeFilename As String In IO.Directory.GetFiles(lTempFolder, "*.shp", IO.SearchOption.AllDirectories)
                    If FileLen(lShapeFilename) < 101 Then
                        TryDeleteShapefile(lShapeFilename)
                    End If
                Next

                Logger.Status("Clipping and projecting NHDPlus shape layers")
                Dim lLayers As Generic.List(Of Layer) =
                    SpatialOperations.ProjectAndClipShapeLayers(lTempFolder, NativeShapeProjection, aProject.DesiredProjection, lClipRegion, GetType(NHDPlus.LayerSpecifications))

                If aJoinAttributes Then
                    Dim lCatchmentDBF As String = IO.Path.Combine(lTempHUC8folder, "drainage" & IO.Path.DirectorySeparatorChar & "catchment.dbf")
                    Dim lFlowlineDBF As String = IO.Path.Combine(lTempHUC8folder, "hydrography" & IO.Path.DirectorySeparatorChar & "nhdflowline.dbf")
                    Dim lNumToMerge As Integer = 0
                    If IO.File.Exists(lCatchmentDBF) Then lNumToMerge += 2
                    If IO.File.Exists(lFlowlineDBF) Then lNumToMerge += 5

                    If lNumToMerge > 0 Then
                        Logger.Status("Merging NHDPlus Value-Added Attributes")
                        Logger.Progress(0, lNumToMerge)
                        If IO.File.Exists(lCatchmentDBF) Then
                            Using lLevel As New ProgressLevel(True)
                                JoinAndSaveDbf(lCatchmentDBF, IO.Path.Combine(lTempHUC8folder, "catchmentattributesnlcd.dbf"), "COMID")
                            End Using
                            Using lLevel As New ProgressLevel(True)
                                JoinAndSaveDbf(lCatchmentDBF, IO.Path.Combine(lTempHUC8folder, "catchmentattributestempprecip.dbf"), "COMID")
                            End Using
                        End If

                        If IO.File.Exists(lFlowlineDBF) Then
                            Using lLevel As New ProgressLevel(True)
                                JoinAndSaveDbf(lFlowlineDBF, IO.Path.Combine(lTempHUC8folder, "NHDFlow.dbf"), "COMID", "FROMCOMID")
                                lLevel.Reset()
                                JoinAndSaveDbf(lFlowlineDBF, IO.Path.Combine(lTempHUC8folder, "NHDFlowlineVAA.dbf"), "COMID")
                                lLevel.Reset()
                                JoinAndSaveDbf(lFlowlineDBF, IO.Path.Combine(lTempHUC8folder, "flowlineattributesflow.dbf"), "COMID")
                                lLevel.Reset()
                                JoinAndSaveDbf(lFlowlineDBF, IO.Path.Combine(lTempHUC8folder, "flowlineattributesnlcd.dbf"), "COMID")
                                lLevel.Reset()
                                JoinAndSaveDbf(lFlowlineDBF, IO.Path.Combine(lTempHUC8folder, "flowlineattributestempprecip.dbf"), "COMID")
                            End Using
                        End If
                    End If
                End If

                'Move files from temp folder to where they belong
                Logger.Status("Moving NHDPlus files into project folder")
                Using lLevel As New ProgressLevel(True)
                    'Move files into place
                    Dim AllFilesToMove() As String = IO.Directory.GetFiles(lTempHUC8folder, "*", IO.SearchOption.AllDirectories)
                    For Each lFileName As String In AllFilesToMove
                        Dim lNewLayer As Layer = Nothing
                        For Each lSearchLayer As Layer In lLayers
                            If lSearchLayer.FileName.ToLower.Equals(lFileName.ToLower) Then lNewLayer = lSearchLayer : Exit For
                        Next
                        If lNewLayer Is Nothing Then
                            Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lFileName, GetType(NHDPlus.LayerSpecifications))
                            If lLayerSpecfication IsNot Nothing Then
                                lNewLayer = New Layer(lFileName, lLayerSpecfication, False)
                                lLayers.Add(lNewLayer)
                            End If
                        End If
                        If lNewLayer IsNot Nothing Then
                            lNewLayer.CopyProcStepsFromCachedFile(lZipFilename)
                        End If
                    Next

                    If aProject.Merge Then
                        lResult = SpatialOperations.MergeLayers(lLayers, lTempHUC8folder, lSaveIn)
                    Else
                        lResult = SpatialOperations.MergeLayers(lLayers, lTempFolder, lSaveIn)
                    End If
                    For Each lLayer As Layer In lLayers
                        aProject.Layers.Add(lLayer)
                    Next

                    Logger.Status("Moving non-layer NHDPlus files")

                    Dim lTempHUC8folderLength As Integer = lTempHUC8folder.TrimEnd(g_PathChar).Length + 1
                    For Each lFileName As String In AllFilesToMove
                        If IO.File.Exists(lFileName) Then 'Only files not already moved above
                            Dim lDestinationFilename As String = IO.Path.Combine(lSaveIn, lFileName.Substring(lTempHUC8folderLength))
                            Select Case IO.Path.GetExtension(lFileName).ToLower
                                Case ".shp", ".shx", ".spx", ".sbn", ".tif"
                                    'These should already be gone after move/merge above, try again to delete if they are still present because they could not be deleted above
                                    If IO.File.Exists(lDestinationFilename) Then
                                        TryDelete(lFileName)
                                    Else
                                        SpatialOperations.CopyMoveMergeFile(lFileName, lDestinationFilename, Nothing)
                                    End If
                                Case Else
                                    SpatialOperations.CopyMoveMergeFile(lFileName, lDestinationFilename, Nothing)
                            End Select
                        End If
                    Next

                End Using
            Catch e As Exception
                lResult = "<error>GetNHDPlus: " & e.Message & "</error>"
                Logger.Dbg(lResult, e.StackTrace)
            Finally
                If IO.Directory.Exists(lUnZipFolder) Then TryDelete(lUnZipFolder)
                If IO.Directory.Exists(lTempFolder) Then TryDelete(lTempFolder)
            End Try
        Else
            Throw New ApplicationException("NHD Plus data not found for HUC '" & aHUC8 & "' in '" & aProject.CacheFolder & "'")
        End If
        Return lResult
    End Function

    ''' <summary>
    ''' Download and unpack NHDPlus data (only for v1.0)
    ''' </summary>
    ''' <param name="aProjFolder">project folder to save data in</param>
    ''' <param name="aSaveFolder">Sub-folder within project folder (e.g. "NHDPlus") or full path of folder to save in (e.g. "C:\NHDPlus").
    '''  If nothing or empty string, will save in aProject.ProjectFolder.</param>
    ''' <param name="aHUC8">8-digit hydrologic unit code to download</param>
    ''' <param name="aJoinAttributes">True to merge value-added attributes to .dbf of shape file, False to leave in separate .dbf</param>
    ''' <param name="aDataTypes">types of data to unpack: valid values are in NHDPlus.LayerSpecifications. If none specified, all data will be processed</param>
    ''' <returns>XML describing success or error</returns>
    Public Shared Function GetNHDPlus(ByVal aProjFolder As String,
                                      ByVal shouldClip As Boolean,
                                      ByVal aSaveFolder As String,
                                      ByVal aRegionToClip As Region,
                                      ByVal getEvenIfCached As Boolean,
                                      ByVal cacheFolder As String,
                                      ByVal aHUC8 As String,
                                      ByVal aJoinAttributes As Boolean,
                                      ByVal desiredProjection As DotSpatial.Projections.ProjectionInfo,
                                      ByVal merge As Boolean,
                                      ByVal auxSaveLoc As String,
                                      ByVal ParamArray aDataTypes() As LayerSpecification) As String
        Dim lResult As String = Nothing
        Dim lSaveIn As String = aProjFolder
        If aSaveFolder IsNot Nothing AndAlso aSaveFolder.Length > 0 Then lSaveIn = IO.Path.Combine(lSaveIn, aSaveFolder)
        IO.Directory.CreateDirectory(lSaveIn)

        Dim lClipRegion As Region = Nothing
        If shouldClip Then lClipRegion = aRegionToClip
        Dim lHUC2 As String = aHUC8.Substring(0, 2)
        Dim lBaseFilename As String = "NHDPlus" & aHUC8 & ".zip"
        Dim lZipFilename As String = IO.Path.Combine(IO.Path.Combine(cacheFolder, "NHDPlus"), lBaseFilename)

Retry:
        'If the cached file already exists and either we need to get a new one anyway or it is too short to be real, then delete cached file
        If IO.File.Exists(lZipFilename) AndAlso (getEvenIfCached OrElse FileLen(lZipFilename) < 500) Then
            While Not TryDelete(lZipFilename)
                If Logger.Msg("Unable to delete '" & lZipFilename & "'", vbRetryCancel, "Need to delete file") = MsgBoxResult.Cancel Then
                    Return ""
                End If
            End While
        End If

        If Not IO.File.Exists(lZipFilename) Then
            Dim lURL As String
            Logger.Status("Downloading NHD Plus")
            Dim lBaseURL As String = pBaseURL & lHUC2
            If lHUC2 = "10" Then
                lURL = lBaseURL & "L/" & lBaseFilename
                If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                    lURL = lBaseURL & "U/" & lBaseFilename
                    If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                        Throw New ApplicationException("Unable to download NHDPlus for " & aHUC8)
                    End If
                End If
            Else
                lURL = lBaseURL & "/" & lBaseFilename
                If Not D4EM.Data.Download.DownloadURL(lURL, lZipFilename) Then
                    Throw New ApplicationException("Unable to download NHDPlus for " & aHUC8)
                End If
            End If
        End If

        Dim lUnZipFolder As String = NewTempDir(IO.Path.Combine(cacheFolder, "NHDPlus_Unzip"))
        Dim lHUCUnZipFolder As String = IO.Path.Combine(lUnZipFolder, "NHDPlus" & aHUC8)
        If IO.File.Exists(lZipFilename) Then

            'Delete directory if already exists, do a check if it exists
            If IO.Directory.Exists(lHUCUnZipFolder) Then
                TryDelete(lHUCUnZipFolder)
            End If

            Logger.Status("Unzipping NHD Plus")
            Try
                Zipper.UnzipFile(lZipFilename, lUnZipFolder) 'will create lHUCCacheFolder
                If auxSaveLoc <> Nothing And IO.Directory.Exists(auxSaveLoc) Then
                    Zipper.UnzipFile(lZipFilename, auxSaveLoc) 'will create lHUCCacheFolder
                End If
            Catch e As Exception
                If Logger.Msg(lZipFilename & vbCrLf _
                            & IO.File.GetLastWriteTime(lZipFilename) & vbCrLf _
                            & Format(FileLen(lZipFilename), "#,##0") & " bytes" & vbCrLf _
                            & "Delete file and retry download?", "Unable to Unzip NHDPlus") = MsgBoxResult.Yes Then
                    If TryDelete(lZipFilename, True) Then
                        GoTo Retry
                    Else
                        Logger.Msg(lZipFilename & vbCrLf _
                            & IO.File.GetLastWriteTime(lZipFilename) & vbCrLf _
                            & Format(FileLen(lZipFilename), "#,##0") & " bytes" & vbCrLf,
                            "Unable to Delete Corrupted NHDPlus download")
                    End If
                Else
                    TryDelete(lZipFilename, True)
                    Throw New ApplicationException("Unable to download NHDPlus for " & aHUC8)
                End If
            End Try
            'If IO.Directory.Exists(lHUCUnZipFolder) Then
            Dim lTempFolder As String = NewTempDir(IO.Path.Combine(cacheFolder, "NHDPlus_Temp"))
            Dim lTempHUC8folder As String = IO.Path.Combine(lTempFolder, "NHDPlus" & aHUC8)
            Dim lFromFolderLength As Integer = lUnZipFolder.TrimEnd(IO.Path.DirectorySeparatorChar).Length + 1

            Try
                Dim lESRIgridFolders As New ArrayList

                If aDataTypes IsNot Nothing Then
                    If aDataTypes.Count = 0 OrElse aDataTypes.Contains(LayerSpecifications.all) Then
                        aDataTypes = Nothing
                    End If
                End If

                'Convert/Clip/Project grids from unzipped to temp folder
                Dim lAllESRIgrids As New Collections.Specialized.NameValueCollection
                AddFilesInDir(lAllESRIgrids, lHUCUnZipFolder, True, "dblbnd.adf")
                Logger.Progress(0, lAllESRIgrids.Count)
                Using lLevel As New ProgressLevel(True)
                    For Each lSourceGrid As String In lAllESRIgrids
                        lESRIgridFolders.Add(IO.Path.GetDirectoryName(lSourceGrid) & IO.Path.DirectorySeparatorChar)
                        If TagInTypes(IO.Path.GetFileName(IO.Path.GetDirectoryName(lSourceGrid)).ToLower, aDataTypes) Then
                            'Dim lSavedFilename As String = 
                            ClipProjectGrid(desiredProjection,
                                            IO.Path.GetDirectoryName(lSourceGrid),
                                            IO.Path.GetFileName(lSourceGrid),
                                            lTempHUC8folder,
                                            lClipRegion)
                            'If IO.File.Exists(lSavedFilename) Then
                            '    'Logger.Status("Clipped and projected " & IO.Path.GetFileName(lSourceGrid))
                            '    Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lSavedFilename, GetType(NHDPlus.LayerSpecifications))
                            '    If lLayerSpecfication IsNot Nothing Then
                            '        
                            '    End If
                            'End If
                            lLevel.Reset()
                        End If
                    Next
                End Using

                'Move non-grid files from unzipped to temp
                Dim lAllFilesToMove As New Collections.Specialized.NameValueCollection
                AddFilesInDir(lAllFilesToMove, lHUCUnZipFolder, True)
                For Each lCacheFilename As String In lAllFilesToMove
                    If ShouldMoveNonGrid(lCacheFilename, lESRIgridFolders, aDataTypes) Then
                        Dim lDestination As String = IO.Path.Combine(lTempFolder, lCacheFilename.Substring(lFromFolderLength))
                        If Not IO.File.Exists(lDestination) Then
                            TryMove(lCacheFilename, lDestination)
                        Else
                            Logger.Dbg("Using existing copy of " & lDestination)
                        End If
                    End If
                Next

                'For Each lShapeFilename As String In IO.Directory.GetFiles(lTempFolder, "*.shp")
                '    Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lShapeFilename, GetType(NHDPlus.LayerSpecifications))
                '    If lLayerSpecfication IsNot Nothing Then
                '        Dim lLayer As New Layer(lShapeFilename, lLayerSpecfication, False)
                '        aProject.Layers.Add(lLayer)
                '        Layer.CopyProcStepsFromCachedFile(lZipFilename, lShapeFilename)
                '    End If
                'Next

                Dim lLayers As Generic.List(Of Layer) =
                    SpatialOperations.ProjectAndClipShapeLayers(lTempFolder, NativeShapeProjection, desiredProjection, lClipRegion, GetType(NHDPlus.LayerSpecifications))
                Logger.Status("Clipped and projected shape layers")

                If aJoinAttributes Then
                    Logger.Status("Merging Value-Added Attributes")
                    Logger.Progress(0, 7)
                    Dim lDbfFileName As String
                    lDbfFileName = IO.Path.Combine(lTempHUC8folder, "drainage" & IO.Path.DirectorySeparatorChar & "catchment.dbf")
                    If IO.File.Exists(lDbfFileName) Then
                        Using lLevel As New ProgressLevel(True)
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "catchmentattributesnlcd.dbf"), "COMID")
                        End Using
                        Using lLevel As New ProgressLevel(True)
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "catchmentattributestempprecip.dbf"), "COMID")
                        End Using
                    End If
                    lDbfFileName = IO.Path.Combine(lTempHUC8folder, "hydrography" & IO.Path.DirectorySeparatorChar & "nhdflowline.dbf")
                    If IO.File.Exists(lDbfFileName) Then
                        Using lLevel As New ProgressLevel(True)
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "NHDFlow.dbf"), "COMID", "FROMCOMID")
                            lLevel.Reset()
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "NHDFlowlineVAA.dbf"), "COMID")
                            lLevel.Reset()
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "flowlineattributesflow.dbf"), "COMID")
                            lLevel.Reset()
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "flowlineattributesnlcd.dbf"), "COMID")
                            lLevel.Reset()
                            JoinAndSaveDbf(lDbfFileName, IO.Path.Combine(lTempHUC8folder, "flowlineattributestempprecip.dbf"), "COMID")
                        End Using
                    End If
                End If

                'Move files from temp folder to where they belong
                Using lLevel As New ProgressLevel(True)
                    'Move files into place
                    Dim AllFilesToMove() As String = IO.Directory.GetFiles(lTempHUC8folder, "*", IO.SearchOption.AllDirectories)
                    For Each lFileName As String In AllFilesToMove
                        Dim lNewLayer As Layer = Nothing
                        For Each lSearchLayer As Layer In lLayers
                            If lSearchLayer.FileName.ToLower.Equals(lFileName.ToLower) Then lNewLayer = lSearchLayer : Exit For
                        Next
                        If lNewLayer Is Nothing Then
                            Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lFileName, GetType(NHDPlus.LayerSpecifications))
                            If lLayerSpecfication IsNot Nothing Then
                                lNewLayer = New Layer(lFileName, lLayerSpecfication, False)
                                lLayers.Add(lNewLayer)
                            End If
                        End If
                        If lNewLayer IsNot Nothing Then
                            lNewLayer.CopyProcStepsFromCachedFile(lZipFilename)
                        End If
                    Next

                    If merge Then
                        lResult = SpatialOperations.MergeLayers(lLayers, lTempHUC8folder, lSaveIn)
                    Else
                        lResult = SpatialOperations.MergeLayers(lLayers, lTempFolder, lSaveIn)
                    End If
                    'For Each lLayer As Layer In lLayers
                    '    aProject.Layers.Add(lLayer)
                    'Next

                    Logger.Status("Moving non-layer NHDPlus files")

                    Dim lTempHUC8folderLength As Integer = lTempHUC8folder.TrimEnd(g_PathChar).Length + 1
                    For Each lFileName As String In AllFilesToMove
                        If IO.File.Exists(lFileName) Then 'Only files not already moved above
                            Dim lDestinationFilename As String = IO.Path.Combine(lSaveIn, lFileName.Substring(lTempHUC8folderLength))
                            Select Case IO.Path.GetExtension(lFileName).ToLower
                                Case ".shp", ".shx", ".spx", ".sbn", ".tif"
                                    'These should already be gone after move/merge above, try again to delete if they are still present because they could not be deleted above
                                    If IO.File.Exists(lDestinationFilename) Then
                                        TryDelete(lFileName)
                                    Else
                                        SpatialOperations.CopyMoveMergeFile(lFileName, lDestinationFilename, Nothing)
                                    End If
                                Case Else
                                    SpatialOperations.CopyMoveMergeFile(lFileName, lDestinationFilename, Nothing)
                            End Select
                        End If
                    Next

                End Using
            Catch e As Exception
                lResult = "<error>GetNHDPlus: " & e.Message & "</error>"
                Logger.Dbg(lResult, e.StackTrace)
            Finally
                If IO.Directory.Exists(lUnZipFolder) Then TryDelete(lUnZipFolder)
                If IO.Directory.Exists(lTempFolder) Then TryDelete(lTempFolder)
            End Try
        Else
            Throw New ApplicationException("NHD Plus data not found for HUC '" & aHUC8 & "' in '" & cacheFolder & "'")
        End If
        Return lResult
    End Function

    Private Shared Function TagInTypes(ByVal aTag As String, ByVal aDataTypes As IEnumerable(Of LayerSpecification)) As Boolean
        If aDataTypes Is Nothing OrElse aDataTypes.Count = 0 Then
            Return True 'No types specified = get everything
        Else
            For Each lType As LayerSpecification In aDataTypes
                If lType.Tag = aTag Then Return True
            Next
        End If
    End Function

    ''' <summary>
    ''' Return True for non-grid files that should be moved
    ''' </summary>
    ''' <param name="aCacheFilename">File name to test</param>
    ''' <param name="aDataTypes">List of data types to include, Nothing = all data types</param>
    ''' <param name="aESRIgridFolders">List of grid folders to skip contents of</param>
    Private Shared Function ShouldMoveNonGrid(ByVal aCacheFilename As String,
                                              ByVal aESRIgridFolders As ArrayList,
                                              ByVal aDataTypes As IEnumerable(Of LayerSpecification)) As Boolean

        If aCacheFilename.ToLower.EndsWith(".dbf") Then
            Select Case IO.Path.GetFileNameWithoutExtension(aCacheFilename).ToLower
                Case "catchmentattributesnlcd", "catchmentattributestempprecip"
                    If ShouldMoveNonGrid(IO.Path.DirectorySeparatorChar & "drainage" & IO.Path.DirectorySeparatorChar & "catchment.shp", aESRIgridFolders, aDataTypes) Then
                        Return True
                    End If
                Case "nhdflow", "nhdflowlinevaa", "flowlineattributesflow", "flowlineattributesnlcd", "flowlineattributestempprecip"
                    If ShouldMoveNonGrid(IO.Path.DirectorySeparatorChar & "hydrography" & IO.Path.DirectorySeparatorChar & "nhdflowline.shp", aESRIgridFolders, aDataTypes) Then
                        Return True
                    End If
            End Select
        End If

        If Not aCacheFilename.Contains(IO.Path.DirectorySeparatorChar & "info" & IO.Path.DirectorySeparatorChar) _
           AndAlso Not aCacheFilename.EndsWith(".aux") _
           AndAlso Not aCacheFilename.EndsWith(".tif") _
           AndAlso TagInTypes(IO.Path.GetFileNameWithoutExtension(aCacheFilename).ToLower, aDataTypes) Then

            For Each lGridFolder As String In aESRIgridFolders
                If aCacheFilename.StartsWith(lGridFolder) Then
                    Return False
                End If
            Next

            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Add the non-duplicate fields from aAddFileName to aBaseFileName, save result in aBaseFileName
    ''' Records that contain the same value in a field in each table are joined together
    ''' </summary>
    ''' <param name="aBaseFileName">DBF with fields that will come first in joined DBF</param>
    ''' <param name="aAddFileName">DBF with fields that will be added</param>
    ''' <param name="aBaseFieldName">Name of field to match in Base</param>
    ''' <param name="aAddFieldName">Name of field to match in Add, defaults to aBaseFieldName</param>
    ''' <remarks></remarks>
    Private Shared Sub JoinAndSaveDbf(ByVal aBaseFileName As String,
                                      ByVal aAddFileName As String,
                                      ByVal aBaseFieldName As String,
                             Optional ByVal aAddFieldName As String = "")
        Logger.Status("Merging Value-Added Attributes to " & IO.Path.GetFileName(aBaseFileName) & " from " & IO.Path.GetFileName(aAddFileName))
        If Not IO.File.Exists(aBaseFileName) Then
            Logger.Dbg("JoinAndSaveDbf: Base file not found: " & aBaseFileName)
        ElseIf Not IO.File.Exists(aAddFileName) Then
            Logger.Dbg("JoinAndSaveDbf: Add file not found: " & aBaseFileName)
        Else
            If aAddFieldName.Length = 0 Then
                aAddFieldName = aBaseFieldName
            End If

            Dim lBaseDbf As New atcTableDBF
            If Not lBaseDbf.OpenFile(aBaseFileName) Then
                Throw New ApplicationException("Cannot Open Base File " & aBaseFileName)
            End If
            Dim lBaseJoinField As Integer = lBaseDbf.FieldNumber(aBaseFieldName)
            If lBaseJoinField = 0 Then
                Throw New ApplicationException("Missing Join Field " & aBaseFieldName & " on " & aBaseFileName)
            End If

            Dim lAddDbf As New atcTableDBF
            If Not lAddDbf.OpenFile(aAddFileName) Then
                Throw New ApplicationException("Cannot Open Join File " & aAddFileName)
            End If
            Dim lAddJoinField As Integer = lAddDbf.FieldNumber(aAddFieldName)
            If lAddJoinField = 0 Then
                Throw New ApplicationException("Missing Join Field " & aAddFieldName & " on " & aAddFileName)
            End If

            'build the joined file
            Dim lNewDbf As New atcTableDBF
            Dim lAddDbfUseFields As New ArrayList 'field numbers to include from lAddDbf
            Dim lOldFieldIndex As Integer
            For lOldFieldIndex = 1 To lAddDbf.NumFields
                'add fields from lAddDbf that are not the key and are not in lBaseDbf
                If lOldFieldIndex <> lAddJoinField AndAlso lBaseDbf.FieldNumber(lAddDbf.FieldName(lOldFieldIndex)) = 0 Then
                    lAddDbfUseFields.Add(lOldFieldIndex)
                End If
            Next

            lNewDbf.NumFields = lBaseDbf.NumFields + lAddDbfUseFields.Count
            Dim lNewFieldIndex As Integer = 1
            For lOldFieldIndex = 1 To lBaseDbf.NumFields
                lNewDbf.FieldType(lNewFieldIndex) = lBaseDbf.FieldType(lOldFieldIndex)
                lNewDbf.FieldLength(lNewFieldIndex) = lBaseDbf.FieldLength(lOldFieldIndex)
                lNewDbf.FieldName(lNewFieldIndex) = lBaseDbf.FieldName(lOldFieldIndex)
                lNewDbf.FieldDecimalCount(lNewFieldIndex) = lBaseDbf.FieldDecimalCount(lOldFieldIndex)
                lNewFieldIndex += 1
            Next
            For Each lOldFieldIndex In lAddDbfUseFields
                lNewDbf.FieldType(lNewFieldIndex) = lAddDbf.FieldType(lOldFieldIndex)
                lNewDbf.FieldLength(lNewFieldIndex) = lAddDbf.FieldLength(lOldFieldIndex)
                lNewDbf.FieldName(lNewFieldIndex) = lAddDbf.FieldName(lOldFieldIndex)
                lNewDbf.FieldDecimalCount(lNewFieldIndex) = lAddDbf.FieldDecimalCount(lOldFieldIndex)
                lNewFieldIndex += 1
            Next
            lNewDbf.NumRecords = lBaseDbf.NumRecords
            lNewDbf.FileName = lBaseDbf.FileName
            lNewDbf.InitData()

            'do the join

            'check to see if the file contains flowlines, then raise a flag to inspect the FLOWDIR field text
            Dim lCheckFDir As Boolean = False
            If aBaseFileName.EndsWith("nhdflowline.dbf") Then
                lCheckFDir = True
            End If

            Dim lJoinValue As String
            For lRecordIndex As Integer = 1 To lNewDbf.NumRecords
                lBaseDbf.CurrentRecord = lRecordIndex
                lNewDbf.CurrentRecord = lRecordIndex
                lNewFieldIndex = 1

                For lOldFieldIndex = 1 To lBaseDbf.NumFields
                    lNewDbf.Value(lNewFieldIndex) = lBaseDbf.Value(lOldFieldIndex)
                    lNewFieldIndex += 1
                Next
                lJoinValue = lBaseDbf.Value(lBaseJoinField)
                'skip merging unknown direction flowlines with the following IF
                If lAddDbf.FindFirst(lAddJoinField, lJoinValue) Then
                    For Each lFieldIndex As Integer In lAddDbfUseFields
                        lNewDbf.Value(lNewFieldIndex) = lAddDbf.Value(lFieldIndex)
                        lNewFieldIndex += 1
                    Next
                ElseIf lCheckFDir = False OrElse lBaseDbf.Value(8) <> "Uninitialized" Then
                    Logger.Dbg("No record found to join key " & lJoinValue)
                Else
                    'Logger.Dbg("NoFlowlineInitialized " & lJoinValue)
                End If
                Logger.Progress(lRecordIndex, lNewDbf.NumRecords)
            Next
            lBaseDbf.Clear()
            lAddDbf.Clear()
            lNewDbf.WriteFile(aBaseFileName)

            If IO.File.Exists(aAddFileName) Then
                TryDelete(aAddFileName)
            End If

            Logger.Dbg("Joined " & IO.Path.GetFileName(aAddFileName) & " to " & IO.Path.GetFileName(aBaseFileName))
        End If
    End Sub

    Private Shared Function ClipProjectGrid(ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                            ByVal aSourceDir As String,
                                            ByVal aBaseName As String,
                                            ByVal aDestDir As String,
                                            ByVal aClipRegion As Region) As String

        Dim lSourceFilename As String = IO.Path.Combine(aSourceDir, aBaseName)
        Dim lNeedProjection As Boolean = Not aDesiredProjection.Equals(NativeGridProjection)
        If lNeedProjection AndAlso lSourceFilename.Contains("fac_fdr_unit") Then
            Logger.Dbg("Reprojection would create incorrect flow direction or flow accumulation grid, skipping: " & lSourceFilename)
            Logger.Status("")
            Return ""
        End If

        Dim lTifFilename As String
        If (aBaseName.ToLower.Equals("dblbnd.adf")) Then
            lTifFilename = IO.Path.GetFileName(aSourceDir) & ".tif"
        Else
            lTifFilename = IO.Path.ChangeExtension(aBaseName, ".tif")
        End If

        'dblbnd.adf file is used when converting from ESRI grid
        'Dim lSourceFilename As String = IO.Path.Combine(aSourceDir, aBaseName & g_PathChar _
        '                                              & aBaseName & g_PathChar & "dblbnd.adf")
        Dim lDestFilename As String = IO.Path.Combine(aDestDir, lTifFilename)
        Dim lClipToFolder As String = Nothing

        IO.Directory.CreateDirectory(aDestDir)

        If aClipRegion IsNot Nothing Then 'Clip to region
            lClipToFolder = NewTempDir(lSourceFilename & "Clipping")
            Dim lClippedFilename As String = IO.Path.Combine(lClipToFolder, lTifFilename)
            Logger.Status("Clipping " & lSourceFilename & " to " & lClippedFilename, True)
            aClipRegion.ClipGrid(lSourceFilename, lClippedFilename, NativeGridProjection)
            Logger.Dbg("Clipping complete")
            lSourceFilename = lClippedFilename
            'Else 'If we did not clip, may want to change format to .tif
            '    Dim lSourceExtension As String = IO.Path.GetExtension(lSourceFilename).ToLower
            '    If Not lSourceExtension.Equals(".tif") Then
            '        Dim lConvertToTifFilename As String
            '        If lSourceExtension = ".adf" Then
            '            lConvertToTifFilename = IO.Path.GetDirectoryName(lSourceFilename) & ".tif"
            '        Else
            '            lConvertToTifFilename = IO.Path.ChangeExtension(lSourceFilename, ".tif")
            '        End If
            '        Logger.Status("Converting " & lSourceFilename & " to " & lConvertToTifFilename, True)
            '        SpatialOperations.ChangeGridFormat(lSourceFilename, lConvertToTifFilename)
            '        lSourceFilename = lConvertToTifFilename
            '    End If
        End If

        If lNeedProjection Then
            If SpatialOperations.ProjectGrid(NativeGridProjection,
                                             aDesiredProjection,
                                             lSourceFilename,
                                             lDestFilename) Then
                Logger.Status("Projected " & IO.Path.GetFileName(lDestFilename), True)
            Else
                If IO.Path.GetExtension(lSourceFilename).Equals(".tif") Then
                    'Move un-projected version into place
                    If IO.File.Exists(lSourceFilename) Then
                        Dim lMoved As Boolean = TryMoveGroup(lSourceFilename, aDestDir, TifExtensions)
                        Logger.Status("Move " & IO.Path.GetFileName(lDestFilename) & ", Success=" & lMoved, True)
                    End If
                Else 'Save as desired type
                    SpatialOperations.ChangeGridFormat(lSourceFilename, lDestFilename)
                End If
                'If projection file is not present, try saving a .prj file here.            
                Dim lProjectionFilename As String = IO.Path.ChangeExtension(lDestFilename, "prj")
                If Not IO.File.Exists(lProjectionFilename) Then
                    IO.File.WriteAllText(lProjectionFilename, NativeGridProjection.ToEsriString)
                End If
                IO.File.WriteAllText(IO.Path.ChangeExtension(lDestFilename, "proj4"), NativeGridProjection.ToProj4String)
            End If
        Else 'Already in right projection
            If IO.Path.GetExtension(lSourceFilename).Equals(".tif") Then
                'Just move into place
                If IO.File.Exists(lSourceFilename) Then
                    Dim lMoved As Boolean = TryMoveGroup(lSourceFilename, aDestDir, TifExtensions)
                    Logger.Status("Move " & IO.Path.GetFileName(lDestFilename) & ", Success=" & lMoved, True)
                End If
            Else 'Save as desired type
                SpatialOperations.ChangeGridFormat(lSourceFilename, lDestFilename)
            End If
            'If projection file is not present, try saving a .prj file here.            
            Dim lProjectionFilename As String = IO.Path.ChangeExtension(lDestFilename, "prj")
            If Not IO.File.Exists(lProjectionFilename) Then
                IO.File.WriteAllText(lProjectionFilename, aDesiredProjection.ToEsriString)
            End If
            IO.File.WriteAllText(IO.Path.ChangeExtension(lDestFilename, "proj4"), aDesiredProjection.ToProj4String)
        End If
        If lClipToFolder IsNot Nothing AndAlso IO.Directory.Exists(lClipToFolder) Then
            TryDelete(lClipToFolder, False)
        End If

        Logger.Status("")
        Return lDestFilename
    End Function
End Class
