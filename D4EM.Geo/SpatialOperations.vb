Imports atcUtility
Imports MapWinUtility
Imports D4EM.Data
Imports DotSpatial.Data
Imports DotSpatial.Projections
Imports NetTopologySuite.Geometries

Public Class SpatialOperations

    Private Shared pInitialized As Boolean = Globals.Initialize
    'TODO: test 'Are projections the same?' in DotSpatial.Projections.ProjectionInfo.Equals
    'Dim lProj0 As New DotSpatial.Projections.ProjectionInfo(Geo.SpatialOperations.AlbersProjections(0))
    'Dim lProj1 As New DotSpatial.Projections.ProjectionInfo(Geo.SpatialOperations.AlbersProjections(1))
    ''Dim lProj2 As New DotSpatial.Projections.ProjectionInfo(Geo.SpatialOperations.AlbersProjections(2))
    'Logger.Dbg("Albers 0=1? " & lProj0.ToEsriString.Equals(lProj1.ToEsriString))
    ''Logger.Dbg("Albers 0=2? " & lProj0.ToEsriString.Equals(lProj2.ToEsriString))
    ''Logger.Dbg("Albers 1=2? " & lProj1.ToEsriString.Equals(lProj2.ToEsriString))

    'lProj0 = New DotSpatial.Projections.ProjectionInfo(Geo.D4EM.Data.Globals.GeographicProjection(0))
    'lProj1 = New DotSpatial.Projections.ProjectionInfo(Geo.D4EM.Data.Globals.GeographicProjection(1))
    'Logger.Dbg("Geographic 0=1? " & lProj0.ToEsriString.Equals(lProj1.ToEsriString))

    Public Shared Function MergeLayers(ByVal aLayers As Generic.List(Of Layer),
                                       ByVal aFromFolder As String,
                                       ByVal aDestinationFolder As String,
                              Optional ByVal aShapeKeys As Collections.Hashtable = Nothing) As String
        Logger.Dbg("Merging " & aLayers.Count & " layers into " & aDestinationFolder)
        Dim lReturnMessage As String = ""
        Dim lFromFolderLength As Integer = aFromFolder.TrimEnd(g_PathChar).Length + 1

        'Dim AllFilesToMove As New Collections.Specialized.NameValueCollection
        'atcUtility.AddFilesInDir(AllFilesToMove, aFromFolder, True, "*")

        For Each lLayer As Layer In aLayers
            If IO.File.Exists(lLayer.FileName) Then
                Dim lDestinationFilename As String = IO.Path.Combine(aDestinationFolder, lLayer.FileName.Substring(lFromFolderLength))
                'If atcMwGisUtility.GisUtil.MappingObjectSet AndAlso FileExists(lDestinationFilename) Then
                '    Try ' Remove destination layer from map if it is already there
                '        For lLayerIndex As Integer = atcMwGisUtility.GisUtil.NumLayers - 1 To 0 Step -1
                '            If atcMwGisUtility.GisUtil.LayerFileName(lLayerIndex).ToLower = lDestinationFilename.ToLower Then
                '                atcMwGisUtility.GisUtil.RemoveLayer(lLayerIndex)
                '                Exit For
                '            End If
                '        Next
                '    Catch 'Ignore error if layer could not be removed, means it was not on map or there is no map
                '    End Try
                'End If
                Select Case IO.Path.GetExtension(lLayer.FileName).ToLower
                    Case ".shp"
                        If Not FileExists(IO.Path.ChangeExtension(lLayer.FileName, ".shx")) Then
                            Logger.Dbg("Shape file missing shx, not merging '" & lLayer.FileName & "' into '" & lDestinationFilename & "'")
                        ElseIf Not FileExists(IO.Path.ChangeExtension(lLayer.FileName, ".dbf")) Then
                            Logger.Dbg("Shape file missing DBF, not merging '" & lLayer.FileName & "' into '" & lDestinationFilename & "'")
                        Else
                            lReturnMessage &= CopyMoveMergeLayer(lLayer, lDestinationFilename) & vbCrLf
                        End If
                    Case ".tif"
                        lReturnMessage &= CopyMoveMergeLayer(lLayer, lDestinationFilename) & vbCrLf
                End Select
            End If
        Next

        Logger.Status("")
        Return lReturnMessage
    End Function

    Private Shared Function CopyMoveMergeLayer(ByVal aFromLayer As Layer, ByVal aDestinationFilename As String) As String
        Dim lResult As String = CopyMoveMergeFile(aFromLayer.FileName, aDestinationFilename, aFromLayer.Specification.IdFieldName)
        If lResult.ToLower.Contains(aDestinationFilename.ToLower) Then
            'aFromLayer.AsFeatureSet().SaveAs(aDestinationFilename, True)
            aFromLayer.Close()
            aFromLayer.FileName = aDestinationFilename
        Else
            Logger.Dbg("Destination file name not found in results of CopyMoveMergeFile")
        End If
        Return lResult
    End Function

    Public Shared Function CopyMoveMergeFile(ByVal aFromFilename As String, ByVal aDestinationFilename As String, ByVal aKeyField As String) As String
        Dim lResult As String = ""
        If FileExists(aFromFilename) Then
            Select Case IO.Path.GetExtension(aFromFilename).ToLower
                Case ".shp"
                    ShapeMerge(aFromFilename, aDestinationFilename, aKeyField)
                    lResult &= "<add_shape>" & aDestinationFilename & "</add_shape>"
                Case ".tif"
                    If Not FileExists(aDestinationFilename) Then
                        TryMoveGroup(aFromFilename, aDestinationFilename, TifExtensions)
                        lResult &= "<add_grid>" & aDestinationFilename & "</add_grid>"
                    ElseIf TryFilesMatch(aFromFilename, aDestinationFilename) Then
                        Logger.Dbg("Identical files do not need to be merged")
                        TryDeleteGroup(aFromFilename, TifExtensions)
                    Else
                        Logger.Status("Merging grid " & IO.Path.GetFileNameWithoutExtension(aFromFilename))
                        Logger.Dbg("Merging '" & aFromFilename & "' and existing" & vbCrLf &
                                   "'" & aDestinationFilename & "'")

                        Dim lMergedFilename As String = IO.Path.ChangeExtension(aDestinationFilename, ".merged" & IO.Path.GetExtension(aDestinationFilename))
                        Dim lMergedIndex As Integer = 1
                        While IO.File.Exists(lMergedFilename)
                            lMergedIndex += 1
                            lMergedFilename = IO.Path.ChangeExtension(aDestinationFilename, ".merged" & lMergedIndex & IO.Path.GetExtension(aDestinationFilename))
                        End While

                        Dim lFirstGrid As IRaster = Raster.OpenFile(aFromFilename)

                        If lFirstGrid Is Nothing Then
                            lResult &= "<error>Could not open '" & aFromFilename & "' to merge.</error>"
                        Else
                            Dim lSecondGrid As IRaster = Raster.OpenFile(aDestinationFilename)
                            If lSecondGrid Is Nothing Then
                                lResult &= "<error>Could not open '" & aFromFilename & "' to merge.</error>"
                            Else
                                'TODO: move code for merging grids into DotSpatial.Analysis, do not reference Tools in this project
                                Dim lMergeSucceeded As Boolean = False
                                Dim lMergedGrid As New Raster
                                Dim gridMergeTool As New DotSpatial.Tools.MergeGrids
                                'Dim lCPH As New MockICancelProgressHandler(New DotSpatial.Data.ProgressMeter())
                                'gridMergeTool.Execute(lFirstGrid, lSecondGrid, lMergedGrid, lCPH)
                                gridMergeTool.Execute(lFirstGrid, lSecondGrid, lMergedGrid, Nothing)
                                lFirstGrid.Close()
                                lSecondGrid.Close()
                                If lMergedGrid IsNot Nothing Then
                                    lMergedGrid.SaveAs(lMergedFilename)
                                    lMergedGrid.Close()
                                    If FileExists(lMergedFilename) AndAlso FileLen(lMergedFilename) > 10 Then
                                        lMergeSucceeded = True
                                        Logger.Dbg("Merge Complete")
                                        If Not TryMoveGroup(lMergedFilename, aDestinationFilename, TifExtensions) Then
                                            Logger.Dbg("Could not move merged file '" & lMergedFilename & "' to '" & aDestinationFilename & "'")
                                            GoTo MoveToNewDestination
                                        Else
                                            'Since merged grid will have correct world file information inside, discard world file from original grid
                                            TryDelete(IO.Path.ChangeExtension(aDestinationFilename, "tfw"), False)
                                            TryDeleteGroup(aFromFilename, TifExtensions, False)
                                            lResult &= "<add_grid>" & aDestinationFilename & "</add_grid>"
                                        End If
                                    End If
                                End If
                                If Not lMergeSucceeded Then
                                    lResult &= "<error>Unable to merge " & IO.Path.GetFileName(aDestinationFilename) & "</error>"
MoveToNewDestination:
                                    Dim lAppendNumber As Integer = 2
                                    Dim lNewDestinationFilename As String = FilenameNoExt(aDestinationFilename) & "-" & lAppendNumber & ".tif"
                                    While FileExists(lNewDestinationFilename)
                                        lAppendNumber += 1
                                        lNewDestinationFilename = FilenameNoExt(aDestinationFilename) & "-" & lAppendNumber & ".tif"
                                    End While
                                    Logger.Dbg("Adding as separate layer: " & lNewDestinationFilename)
                                    TryMoveGroup(aFromFilename, lNewDestinationFilename, TifExtensions)
                                    lResult &= "<add_grid>" & lNewDestinationFilename & "</add_grid>"
                                End If
                            End If
                        End If
                    End If
                Case ".dbf"
                    If Not FileExists(aDestinationFilename) Then
                        TryMove(aFromFilename, aDestinationFilename, True)
                    ElseIf TryFilesMatch(aFromFilename, aDestinationFilename) Then
                        Logger.Dbg("Identical files do not need to be merged")
                        TryDelete(aFromFilename)
                    Else
                        Logger.Status("Merging " & IO.Path.GetFileNameWithoutExtension(aFromFilename))
                        Dim lExisting As New atcTableDBF
                        If Not lExisting.OpenFile(aDestinationFilename) Then
                            Logger.Dbg("Could not open '" & aDestinationFilename & "' for merge")
                            GoTo UnknownFileType
                        Else
                            Dim lMergeIn As New atcTableDBF
                            If Not lMergeIn.OpenFile(aFromFilename) Then
                                Logger.Dbg("Could not open '" & aDestinationFilename & "' for merge")
                            Else
                                lExisting.Merge(lMergeIn, Nothing, 1)
                                Dim lMergedFilename As String = aDestinationFilename
                                TryDelete(aDestinationFilename)
                                If IO.File.Exists(aDestinationFilename) Then
                                    lMergedFilename = GetTemporaryFileName(FilenameNoExt(lMergedFilename) & ".merged", ".dbf")
                                    Logger.Dbg("Could not save merged DBF to " & aDestinationFilename & "' saving to '" & lMergedFilename)
                                End If
                                lExisting.WriteFile(lMergedFilename)
                            End If
                            lMergeIn.Clear()
                        End If
                        lExisting.Clear()
                    End If
                Case ".prj", ".tfw"
                    If FileExists(aDestinationFilename) Then 'Already have a version of this file.
                        Logger.Dbg("Keeping existing file " & aDestinationFilename)
                    Else
                        TryMove(aFromFilename, aDestinationFilename, True)
                    End If
                Case Else
UnknownFileType:
                    If FileExists(aDestinationFilename) Then 'Already have a version of this file.
                        If Not TryDelete(aDestinationFilename) Then
                            Logger.Dbg("Could not remove existing file '" & aDestinationFilename & "' to replace with new file.")
                            Return lResult
                        End If
                    Else
                        TryMove(aFromFilename, aDestinationFilename, True)
                    End If
            End Select
        End If
        Return lResult
    End Function

    'Public Shared Function SameProjection(ByVal aProj1 As String, ByVal aProj2 As String) As Boolean
    '    Dim lProjection1 As ProjectionInfo = New ProjectionInfo(aProj1)
    '    Dim lProjection2 As ProjectionInfo = New ProjectionInfo(aProj2)
    '    If lProjection1.Equals(lProjection2) Then
    '        Return True
    '    End If

    '    Dim lProj1 As String = aProj1.ToLower.Trim
    '    Dim lProj2 As String = aProj2.ToLower.Trim

    '    If lProj1.Equals(lProj2) Then
    '        Return True
    '    Else
    '        Dim lMatch1 As Boolean = False
    '        Dim lMatch2 As Boolean = False
    '        For Each lProjection As String In GeographicProjections
    '            If lProjection.ToLower.Equals(lProj1) Then lMatch1 = True
    '            If lProjection.ToLower.Equals(lProj2) Then lMatch2 = True
    '        Next

    '        If lMatch1 AndAlso lMatch2 Then
    '            Return True 'Both are Geographic
    '        Else
    '            lMatch1 = False : lMatch2 = False
    '            For Each lProjection As String In AlbersProjections
    '                If lProjection.ToLower.Equals(lProj1) Then lMatch1 = True
    '                If lProjection.ToLower.Equals(lProj2) Then lMatch2 = True
    '            Next
    '            If lMatch1 AndAlso lMatch2 Then
    '                Return True 'Both are Albers centered on US
    '            End If
    '        End If
    '    End If
    '    Return False 'We can't tell that they are the same, so we report that they are not
    'End Function

    ''' <summary>
    ''' Transform a geographic point into a new projection
    ''' </summary>
    ''' <param name="x">input: x coordinate to be changed, output: projected x coordinate</param>
    ''' <param name="y">input: y coordinate to be changed, output: projected y coordinate</param>
    ''' <param name="srcProjection">Original projection of input x, y</param>
    ''' <param name="destProjection">Desired new projection</param>
    ''' <returns>True to indicate success</returns>
    ''' <remarks>To project using proj4 strings, ProjectPoint(x, y, D4EM.Data.Globals.FromProj4(srcPrj4String), D4EM.Data.Globals.FromProj4(destPrj4String))</remarks>
    Public Shared Function ProjectPoint(ByRef x As Double, ByRef y As Double,
                                        ByVal srcProjection As DotSpatial.Projections.ProjectionInfo,
                                        ByVal destProjection As DotSpatial.Projections.ProjectionInfo) As Boolean
        Dim xy As Double() = {x, y}
        DotSpatial.Projections.Reproject.ReprojectPoints(xy, Nothing, srcProjection, destProjection, 0, 1)
        x = xy(0)
        y = xy(1)
        Return True
    End Function

    Private Shared Sub ShapeMerge(ByVal aFromFilename As String,
                                  ByVal aDestinationFilename As String,
                                  ByVal aKeyField As String)
        If FileExists(aFromFilename) Then
            If Not FileExists(aDestinationFilename) Then
                TryMoveShapefile(aFromFilename, aDestinationFilename)
                TryDelete(IO.Path.ChangeExtension(aDestinationFilename, ".sbn"))
                TryDelete(IO.Path.ChangeExtension(aDestinationFilename, ".sbx"))
            ElseIf TryFilesMatch(aFromFilename, aDestinationFilename) Then
                Logger.Dbg("Identical files do not need to be merged")
                TryDeleteShapefile(aFromFilename)
            ElseIf Not FileExists(IO.Path.ChangeExtension(aFromFilename, ".shx")) Then
                Logger.Dbg("Shape file missing shx, not merging '" & aFromFilename & "' into '" & aDestinationFilename & "'")
            ElseIf Not FileExists(IO.Path.ChangeExtension(aFromFilename, ".dbf")) Then
                Logger.Dbg("Shape file missing DBF, not merging '" & aFromFilename & "' into '" & aDestinationFilename & "'")
            Else
                Dim lShapeUtilExe As String = FindFile("Please locate ShapeUtil.exe", "\basins\etc\datadownload\ShapeUtil.exe")
                If FileExists(lShapeUtilExe) Then
                    Logger.Status("Merging shape " & IO.Path.GetFileNameWithoutExtension(aFromFilename), True)
                    'IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(aDestinationFilename)) 'Not necessary now with FileExists tested above
                    Logger.Dbg(lShapeUtilExe & " """ & aDestinationFilename & """ key=" & aKeyField & " """ & aFromFilename & """") ' """ & aProjectionFilename & """")
                    'Shell(lShapeUtilExe & " """ & aOutputFilename & """ key=" & aKeyField & " """ & aCurFilename & """ """ & aProjectionFilename & """", AppWinStyle.NormalNoFocus, True)

                    'TODO: replace use of ShapeUtil with DotSpatial equivalent after checking performance. Note that ShapeUtil is a DotNet component.
                    If aKeyField Is Nothing Then
                        Shell(lShapeUtilExe & " """ & aDestinationFilename & """ """ & aFromFilename & """", AppWinStyle.NormalNoFocus, True)
                    Else
                        Shell(lShapeUtilExe & " """ & aDestinationFilename & """ key=" & aKeyField & " """ & aFromFilename & """", AppWinStyle.NormalNoFocus, True)
                    End If
                Else
                    Logger.Dbg("Failed to find ShapeUtil.exe for merging " & aFromFilename & " into " & aDestinationFilename)
                End If
            End If
        End If
    End Sub

    Public Shared Sub ProjectAndClipGridLayers(ByVal aFolder As String,
                                               ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                               ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                               ByVal aClipRegion As Region)
        ProjectAndClipGridLayers(aFolder, aNativeProjection, aDesiredProjection, aClipRegion, "*.tif")
    End Sub

    Public Shared Sub ProjectAndClipGridLayers(ByVal aFolder As String,
                                               ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                               ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                               ByVal aClipRegion As Region,
                                               ByVal aFilter As String)
        'Project geotiff layers
        Dim lAllFilesToProject As New Collections.Specialized.NameValueCollection
        atcUtility.AddFilesInDir(lAllFilesToProject, aFolder, True, aFilter)
        Dim lLayerCount As Integer = lAllFilesToProject.Count
        If lLayerCount > 0 Then
            Dim lClipToFolder As String = ""
            If aClipRegion IsNot Nothing Then lClipToFolder = NewTempDir(IO.Path.Combine(aFolder, "Clipping"))

            Dim lLayerProgressEnd As Integer = lLayerCount
            Dim lProgressComplete As Integer = 0
            If aClipRegion IsNot Nothing Then lLayerProgressEnd *= 2
            Logger.Progress(0, lLayerProgressEnd)
            For Each lLayerFilename As String In lAllFilesToProject
                Logger.Status("Label Middle Layer " & lProgressComplete + 1 & " of " & lLayerCount)
                ProjectAndClipGridLayer(lLayerFilename, aNativeProjection, aDesiredProjection, aClipRegion, lClipToFolder)
                lProgressComplete += 1
                Logger.Progress(lProgressComplete, lLayerProgressEnd)
            Next
            If lClipToFolder.Length > 0 Then TryDelete(lClipToFolder)
            Logger.Status("")
        End If
    End Sub

    Public Shared Sub ProjectAndClipGridLayer(ByRef aGridFilename As String,
                                              ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                              ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                              ByVal aClipRegion As Region,
                                              ByVal aClipFolder As String)
        Logger.Status("Processing " & IO.Path.GetFileName(aGridFilename), True)

        Dim lProjectToFolder As String = NewTempDir(IO.Path.Combine(IO.Path.GetDirectoryName(aGridFilename), "Projecting"))
        Dim lUnClippedFilename As String = ""
        Dim lClipFilename As String = ""
        Dim lFolderLength As Integer = IO.Path.GetDirectoryName(aGridFilename).TrimEnd(g_PathChar).Length + 1
        Dim lProjectedFilename As String = IO.Path.Combine(lProjectToFolder, aGridFilename.Substring(lFolderLength))

        If aClipRegion IsNot Nothing Then
            lClipFilename = IO.Path.Combine(aClipFolder, IO.Path.GetFileName(lProjectedFilename))
            Using lLevel As New ProgressLevel(True)
                aClipRegion.ClipGrid(aGridFilename, lClipFilename, aNativeProjection)
                lUnClippedFilename = aGridFilename
                aGridFilename = lClipFilename
            End Using
        End If

        IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(lProjectedFilename))
        Dim lFirstTryTime As Date = Date.Now
        Dim lProjectedProjectionFileName As String = IO.Path.ChangeExtension(lProjectedFilename, "prj")
        If IO.Path.GetExtension(aGridFilename).ToLower.Equals(".tif") AndAlso aNativeProjection.Equals(aDesiredProjection) Then
            TryCopyGroup(aGridFilename, lProjectedFilename, TifExtensions)
            If Not IO.File.Exists(lProjectedProjectionFileName) OrElse IO.File.ReadAllText(IO.Path.ChangeExtension(lProjectedFilename, "prj")).Length = 0 Then
                IO.File.WriteAllText(lProjectedProjectionFileName, aDesiredProjection.ToEsriString)
            End If
            IO.File.WriteAllText(IO.Path.ChangeExtension(lProjectedProjectionFileName, "proj4"), aDesiredProjection.ToProj4String)
        Else
ProjectIt:
            If ProjectGrid(aNativeProjection, aDesiredProjection, aGridFilename, lProjectedFilename) Then
                Logger.Dbg("Projected " & aGridFilename.Substring(lFolderLength))
                If Not IO.File.Exists(lProjectedProjectionFileName) OrElse IO.File.ReadAllText(IO.Path.ChangeExtension(lProjectedFilename, "prj")).Length = 0 Then
                    IO.File.WriteAllText(lProjectedProjectionFileName, aDesiredProjection.ToEsriString)
                End If
                IO.File.WriteAllText(IO.Path.ChangeExtension(lProjectedProjectionFileName, "proj4"), aDesiredProjection.ToProj4String)
            Else
                'If Now.Subtract(lFirstTryTime).Seconds < 60 Then
                '    System.Threading.Thread.Sleep(10000)
                '    Logger.Status("Retrying grid projection " & aGridFilename.Substring(lFolderLength))
                '    GoTo ProjectIt
                'End If

                'Since we could not reproject, copy unprojected file into place along with native projection
                If IO.Path.GetExtension(aGridFilename).ToLower.Equals(".tif") Then
                    TryCopyGroup(aGridFilename, lProjectedFilename, TifExtensions)
                    IO.File.WriteAllText(lProjectedProjectionFileName, aNativeProjection.ToEsriString)
                    IO.File.WriteAllText(IO.Path.ChangeExtension(lProjectedProjectionFileName, "proj4"), aNativeProjection.ToProj4String)
                Else
                    'TryCopy(aGridFilename, lProjectedFilename)
                End If
            End If
        End If

        If lUnClippedFilename.Length > 0 Then
            TryDelete(lClipFilename)
            aGridFilename = lUnClippedFilename
        End If
        'Since projected grid will have correct world file information inside, discard world file from original grid
        TryDelete(IO.Path.ChangeExtension(aGridFilename, "tfw"), False)
        If TryMove(lProjectedFilename, aGridFilename) Then
            TryMove(IO.Path.ChangeExtension(lProjectedFilename, "prj"), IO.Path.ChangeExtension(aGridFilename, "prj"))
            TryMove(IO.Path.ChangeExtension(lProjectedFilename, "proj4"), IO.Path.ChangeExtension(aGridFilename, "proj4"))
            TryMove(lProjectedFilename & ".xml", aGridFilename & ".xml")
        Else
            Logger.Dbg("Could not replace grid with projected: " & aGridFilename)
        End If
        TryDelete(lProjectToFolder)
    End Sub

    ''' <summary>
    ''' Project (and optionally clip) all the shape files in aFolder or its subfolders
    ''' </summary>
    ''' <param name="aShapeFileName">shape file to process</param>
    ''' <param name="aNativeProjection">Projection of shape files before calling</param>
    ''' <param name="aDesiredProjection">New projection desired for shape files</param>
    ''' <param name="aClipRegion">area to clip to before projecting, Nothing to not clip</param>
    ''' <param name="aClipFolder">temporary folder to clip into</param>
    <CLSCompliant(False)>
    Public Shared Function ProjectAndClipShapeLayer(ByRef aShapeFileName As String,
                                                    ByVal aLayerSpecification As LayerSpecification,
                                                    ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                                    ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                                    ByVal aClipRegion As Region,
                                                    ByVal aClipFolder As String) As Layer
        Dim lShapeFile As New D4EM.Data.Layer(aShapeFileName, aLayerSpecification, False)
        Dim lUnClippedFilename As String = ""
        If aClipRegion IsNot Nothing Then
            Dim lClipFilename As String = IO.Path.Combine(aClipFolder, IO.Path.GetFileName(aShapeFileName))
            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(lClipFilename))
            Logger.Status("Clipping " & IO.Path.GetFileNameWithoutExtension(aShapeFileName), True)
            Dim lClippedShapeFile = aClipRegion.SelectShapes(lShapeFile, lClipFilename)
            If lClippedShapeFile Is Nothing Then
                Logger.Dbg("SelectShapes returned Nothing: " & aShapeFileName)
                Return Nothing
            Else
                Dim lClippedFeatureSet As DotSpatial.Data.IFeatureSet = lClippedShapeFile.AsFeatureSet
                If lClippedFeatureSet Is Nothing OrElse lClippedFeatureSet.NumRows = 0 Then
                    Logger.Dbg("Failed to clip or clipping left no features: " & aShapeFileName)
                    Return Nothing
                Else
                    lUnClippedFilename = aShapeFileName
                    aShapeFileName = lClipFilename
                    lShapeFile.Close()
                    lShapeFile = lClippedShapeFile
                End If
            End If
        End If

        If IO.File.Exists(aShapeFileName) Then
            If FileLen(aShapeFileName) < 102 Then
                IO.File.WriteAllText(IO.Path.ChangeExtension(aShapeFileName, ".prj"), aDesiredProjection.ToEsriString)
                Logger.Dbg("Empty layer assigned new projection " & aShapeFileName)
            ElseIf aNativeProjection IsNot Nothing AndAlso aNativeProjection.Matches(aDesiredProjection) Then
                'skip projecting because already in desired projection
            Else
                lShapeFile.Reproject(aDesiredProjection) 'ProjectShapefile(aNativeProjection, aDesiredProjection, aShapeFilename)
            End If
            If lUnClippedFilename.Length > 0 Then 'Move clipped to replace original
                lShapeFile.Close()
                TryDeleteShapefile(lUnClippedFilename)
                If TryMoveShapefile(aShapeFileName, lUnClippedFilename) Then
                    lShapeFile.FileName = lUnClippedFilename
                End If
            End If
        Else
            Logger.Dbg("Failed to open " & aShapeFileName) '& ": " & lUtil.ErrorMsg(lShapeFile.LastErrorCode))
        End If
        Logger.Status("")
        Return lShapeFile
    End Function

    ''' <summary>
    ''' Project (and optionally clip) all the shape files in aFolder or its subfolders
    ''' </summary>
    ''' <param name="aFolder">Folder containing shape files to process</param>
    ''' <param name="aNativeProjection">Projection of shape files before calling</param>
    ''' <param name="aDesiredProjection">New projection desired for shape files</param>
    ''' <param name="aClipRegion">Area to clip to before projecting, Nothing if clipping is not needed</param>
    Public Shared Function ProjectAndClipShapeLayers(ByVal aFolder As String,
                                                     ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                                     ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                                     ByVal aClipRegion As Region,
                                                     ByVal aLayerSpecifications As Type) As Generic.List(Of Layer)
        Dim lLayers As New Generic.List(Of Layer)
        Dim lFolderLength As Integer = aFolder.TrimEnd(g_PathChar).Length + 1
        Dim lAllFilesToProject As New Collections.Specialized.NameValueCollection
        Dim lClipToFolder As String = ""
        Dim lClipToLocalFolder As String = ""
        If aClipRegion IsNot Nothing Then lClipToFolder = NewTempDir(IO.Path.Combine(aFolder, "Clipping"))

        atcUtility.AddFilesInDir(lAllFilesToProject, aFolder, True, "*.shp")
        Dim lLayerCount As Integer = lAllFilesToProject.Count
        Dim lProgressComplete As Integer = 0
        Logger.Progress(0, lLayerCount)
        For Each lLayerFilename As String In lAllFilesToProject
            Logger.Status("Processing " & IO.Path.GetFileName(lLayerFilename))
            Logger.Dbg("Label Middle Layer " & lProgressComplete + 1 & " of " & lLayerCount)
            If lClipToFolder.Length > 0 Then
                lClipToLocalFolder = IO.Path.GetDirectoryName(lLayerFilename)
                If lClipToLocalFolder.Length > lFolderLength Then
                    lClipToLocalFolder = lClipToLocalFolder.Substring(lFolderLength)
                Else
                    lClipToLocalFolder = ""
                End If
            End If
            Using lLevel As New ProgressLevel((lLayerCount > 1), (lLayerCount = 1))
                Dim lLayerSpecfication As LayerSpecification = LayerSpecification.FromFilename(lLayerFilename, aLayerSpecifications)
                If lLayerSpecfication IsNot Nothing Then
                    Dim lProcessedLayer = ProjectAndClipShapeLayer(lLayerFilename, lLayerSpecfication, aNativeProjection, aDesiredProjection, aClipRegion, IO.Path.Combine(lClipToFolder, lClipToLocalFolder))
                    If lProcessedLayer Is Nothing Then
                        Logger.Dbg("Removing original layer that did not produce a clipped file: " & lLayerFilename)
                        TryDeleteShapefile(lLayerFilename)
                    Else
                        lLayers.Add(lProcessedLayer)
                    End If
                End If
            End Using
            lProgressComplete += 1
        Next
        If lClipToFolder.Length > 0 Then TryDelete(lClipToFolder)
        Return lLayers
    End Function

    ''' <summary>Project a grid using a new progress level</summary>
    ''' <param name="aNativeProjection">Native projection of grid (only used if it cannot be determined directly from the grid)</param>
    ''' <param name="aDesiredProjection">Projection to change grid into</param>
    ''' <param name="aLayerFilename">Path and file name of existing grid</param>
    ''' <param name="aProjectedFilename">Path and file name to save projected grid as</param>
    ''' <param name="aIncrementProgressAfter">True to increment logger progress after projecting</param>
    ''' <param name="aProgressSameLevel">True to keep same logger progress while reprojecting, False to create new logger progress level while reprojecting.</param>
    ''' <returns>True if grid was reprojected (or if projection was not needed and grid was copied), False if grid could not be reprojected</returns>
    ''' <remarks></remarks>
    Public Shared Function ProjectGrid(ByVal aNativeProjection As DotSpatial.Projections.ProjectionInfo,
                                       ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo,
                                       ByVal aLayerFilename As String,
                                       ByVal aProjectedFilename As String,
                              Optional ByVal aIncrementProgressAfter As Boolean = False,
                              Optional ByVal aProgressSameLevel As Boolean = False) As Boolean
        Dim lReturn As Boolean = False

        Globals.RepairAlbers(aNativeProjection)
        Globals.RepairAlbers(aDesiredProjection)
        '        IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(aProjectedFilename))
        '        'If IO.Path.GetExtension(aLayerFilename).ToLower.Equals(".tif") AndAlso SameProjection(aNativeProjection, aDesiredProjection) Then
        '        '    TryCopyGroup(IO.Path.GetFileNameWithoutExtension(aLayerFilename), IO.Path.GetFileNameWithoutExtension(aProjectedFilename), TifExtensions)
        '        'Else
        '        Dim lNumTries As Integer = 0
        '        Dim lFirstTryTime As Date = Date.Now
        '        Logger.Status("Projecting " & IO.Path.GetFileName(aLayerFilename))
        '        System.Threading.Thread.Sleep(5000)
        'ProjectIt:
        '        'Dim lProjectFromFolder As String = NewTempDir("ProjectFrom")
        '        'For Each lFilename As String In IO.Directory.GetFiles(IO.Path.GetDirectoryName(aLayerFilename))
        '        '    If Not TryCopy(lFilename, IO.Path.Combine(lProjectFromFolder, IO.Path.GetFileName(lFilename))) Then
        '        '        If Now.Subtract(lFirstTryTime).TotalSeconds > 120 Then
        '        '            Throw New ApplicationException("CopyGrid Failed")
        '        '        Else
        '        '            System.Threading.Thread.Sleep(10000)
        '        '            Logger.Status("Retrying grid copy " & aLayerFilename)
        '        '            GoTo ProjectIt
        '        '        End If
        '        '    End If
        '        'Next
        '        'aLayerFilename = IO.Path.Combine(lProjectFromFolder, IO.Path.GetFileName(aLayerFilename))

        '        Dim lProjectToFolder As String = NewTempDir("Projecting")
        '        Dim lProjectedFilename As String = IO.Path.Combine(lProjectToFolder, IO.Path.GetFileName(aProjectedFilename))
        '        If ProjectGrid(aNativeProjection.ToProj4String, aDesiredProjection.ToProj4String, aLayerFilename, lProjectedFilename) Then
        '            lReturn = True
        '            TryCopyGroup(lProjectedFilename, IO.Path.GetDirectoryName(aProjectedFilename), TifExtensions)

        '            Dim lProjectedProjectionFileName As String = IO.Path.ChangeExtension(aProjectedFilename, "prj")
        '            If Not IO.File.Exists(lProjectedProjectionFileName) OrElse IO.File.ReadAllText(IO.Path.ChangeExtension(aProjectedFilename, "prj")).Length = 0 Then
        '                IO.File.WriteAllText(lProjectedProjectionFileName, aDesiredProjection.ToEsriString)
        '            End If
        '            IO.File.WriteAllText(IO.Path.ChangeExtension(aProjectedFilename, "proj4"), aDesiredProjection.ToProj4String)
        '            'TODO: move this metadata handling into ProjectGrid
        '            Dim lMetadataFilename As String = aProjectedFilename & ".xml"
        '            Dim lMetadata As New Metadata(lMetadataFilename)
        '            lMetadata.AddProcessStep("Projected from '" & aNativeProjection.ToProj4String & "' to '" & aDesiredProjection.ToProj4String & "'")
        '            'TODO: lMetadata.SetBoundingBox(.Extents.xMin, .Extents.xMax, .Extents.yMax, .Extents.yMin)
        '            lMetadata.Save()
        '        Else
        '            lNumTries += 1
        '            If lNumTries > 2 AndAlso Now.Subtract(lFirstTryTime).TotalSeconds > 240 Then
        '                Throw New ApplicationException("ProjectGrid Failed, attempts=" & lNumTries)
        '            Else
        '                Logger.Status("Waiting for grid projection, attempt " & lNumTries & " for " & IO.Path.GetFileName(aLayerFilename), 1)
        '                Dim lDelaySeconds As Integer = 8 + 2 * lNumTries
        '                For lDelay As Integer = 1 To lDelaySeconds
        '                    System.Threading.Thread.Sleep(1000)
        '                    Logger.Progress(lDelay, lDelaySeconds)
        '                Next
        '                Logger.Status("Retrying projection " & aLayerFilename, 1)
        '                GoTo ProjectIt
        '            End If
        '        End If
        '        'End If

        '        'Logger.Status("Projecting " & aGridFilename.Substring(lFolderLength))
        '        'Logger.Dbg("Projecting '" & aGridFilename & "' from '" & aNativeProjection.ToProj4String & "' to '" & aDesiredProjection.ToProj4String & "' as '" & lProjectedFilename & "'")
        '        'Using lLevel As New ProgressLevel(aIncrementProgressAfter, aProgressSameLevel)
        '        '    Dim rst = DotSpatial.Data.Raster.Open(aLayerFilename)
        '        '    If rst.Projection Is Nothing OrElse Not rst.Projection.IsValid Then
        '        '        rst.Projection = aNativeProjection
        '        '        If rst.Projection Is Nothing OrElse Not rst.Projection.IsValid Then 'no project information in layer or argument, assume USGS Albers
        '        '            rst.Projection = Globals.AlbersProjection
        '        '        End If
        '        '    End If

        '        '    Globals.RepairAlbers(rst.Projection)
        '        '    Globals.RepairAlbers(aDesiredProjection)
        '        '    If rst.Projection.Matches(aDesiredProjection) Then
        '        '        rst.Copy(aProjectedFilename, True)
        '        '        rst.Close()
        '        '        lReturn = True
        '        '    ElseIf rst.CanReproject Then
        '        '        rst.Reproject(aDesiredProjection)
        '        '        rst.Projection = aDesiredProjection 'TODO: move assigning new projection into Reproject
        '        '        rst.SaveAs(aProjectedFilename)
        '        '        rst.Close()

        '        '        'TODO: move metadata handling into Reproject
        '        '        Dim lMetadataFilename As String = aProjectedFilename & ".xml"
        '        '        Dim lMetadata As New Metadata(lMetadataFilename)
        '        '        lMetadata.AddProcessStep("Projected from '" & aNativeProjection.ToProj4String & "' to '" & aDesiredProjection.ToProj4String & "'")
        '        '        'TODO: lMetadata.SetBoundingBox(.Extents.xMin, .Extents.xMax, .Extents.yMax, .Extents.yMin)
        '        '        lMetadata.Save()
        '        '        lReturn = True
        '        '    End If
        '        'End Using
        If Not lReturn Then
            Logger.Dbg("Did not project " & aLayerFilename)
        End If

        Return lReturn
    End Function

    ''' <summary>
    ''' Project a grid using a new progress level
    ''' </summary>
    Private Shared Function ProjectGrid(ByVal aNativeProjection As String,
                                       ByVal aDesiredProjection As String,
                                       ByVal aLayerFilename As String,
                                       ByVal aProjectedFilename As String,
                              Optional ByVal aTrimResult As Boolean = False,
                              Optional ByVal aIncrementProgressAfter As Boolean = False,
                              Optional ByVal aProgressSameLevel As Boolean = False) As Boolean
        Using lLevel As New ProgressLevel(aIncrementProgressAfter, aProgressSameLevel)
            Try
                'GetDefaultRenderer(aLayerFilename)
                If aLayerFilename.EndsWith("dblbnd.adf") Then
                    Logger.Status("Projecting " & IO.Path.GetFileName(IO.Path.GetFileName(aLayerFilename)))
                Else
                    Logger.Status("Projecting " & IO.Path.GetFileName(aLayerFilename))
                End If
                If aNativeProjection = " +x_0=0 +y_0=0 +lat_0=23 +lon_0=-96 +lat_1=29.5 +lat_2=45.5 +proj=aea +datum=NAD83 +no_defs" Then
                    aNativeProjection = "+proj=aea +ellps=GRS80 +lon_0=-96 +lat_0=23.0 +lat_1=29.5 +lat_2=45.5 +x_0=0 +y_0=0 +datum=NAD83 +units=m"
                End If
                If aDesiredProjection = " +x_0=0 +y_0=0 +lat_0=23 +lon_0=-96 +lat_1=29.5 +lat_2=45.5 +proj=aea +datum=NAD83 +no_defs" Then
                    aDesiredProjection = "+proj=aea +ellps=GRS80 +lon_0=-96 +lat_0=23.0 +lat_1=29.5 +lat_2=45.5 +x_0=0 +y_0=0 +datum=NAD83 +units=m"
                End If
                If aDesiredProjection = " +x_0=500000 +y_0=0 +lon_0=-75 +zone=18 +proj=utm +ellps=GRS80 +no_defs" Then
                    aDesiredProjection = "+proj=utm +zone=18 +ellps=GRS80 +units=m +no_defs "
                End If

                Logger.Dbg("MapWinGeoProc.SpatialReference.ProjectGrid: " & vbCrLf _
                         & "from " & aNativeProjection & vbCrLf & "to " & aDesiredProjection & vbCrLf _
                         & "from " & aLayerFilename & vbCrLf & "to " & aProjectedFilename & vbCrLf _
                         & "aTrimResult=" & aTrimResult)
                'IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(aProjectedFilename))
                'If Not MapWinGeoProc.SpatialReference.ProjectGrid(aNativeProjection, aDesiredProjection, _
                '                             aLayerFilename, aProjectedFilename, aTrimResult) Then
                '    Dim lLastErrorMsg As String = MapWinGeoProc.Error.GetLastErrorMsg
                '    Logger.Dbg("ProjectGrid Failed: " & lLastErrorMsg)
                Return False
                'End If
                'Return True
            Catch e As Exception
                Logger.Dbg("ProjectGrid Exception: " & e.Message & vbCrLf & e.StackTrace)
                Return False
            End Try
        End Using
    End Function

    Public Shared Sub ProjectImage(ByVal aCurrentProjection As String, ByVal aDesiredProjection As String,
                                   ByVal aSourceFilename As String, ByVal aDestinationFilename As String,
                          Optional ByVal aIncrementProgressAfter As Boolean = False,
                          Optional ByVal aProgressSameLevel As Boolean = False)
        Throw New NotImplementedException("SpatialOperations.ProjectImage not yet implemented")
        'TODO: translate the following code to DotSpatial (but no hurry since we are not using this method currently)
        'If SameProjection(aCurrentProjection, aDesiredProjection) Then
        '    TryCopyGroup(IO.Path.GetFileNameWithoutExtension(aSourceFilename), IO.Path.GetFileNameWithoutExtension(aDestinationFilename), atcUtility.TifExtensions)
        'Else
        'Using lLevel As New ProgressLevel(aIncrementProgressAfter, aProgressSameLevel)
        '    MapWinGeoProc.SpatialReference.ProjectImage(aCurrentProjection, aDesiredProjection, aSourceFilename, aDestinationFilename, New MapWinCallback)
        'End Using
    End Sub

    'Private Shared Function SelectShapesWithBoxArgs(ByVal aArgs As Xml.XmlDocument, _
    '                                                ByRef aSelectFromShapeFilename As String, _
    '                                                ByRef aSelectFromShapeProjection As String, _
    '                                                ByRef aKeyFieldName As String) As Boolean

    '    Dim lArg As Xml.XmlNode = aArgs.FirstChild
    '    aSelectFromShapeProjection = ""

    '    While Not lArg Is Nothing
    '        Dim lArgName As String = lArg.Name
    '        Dim lNameAttribute As Xml.XmlAttribute = lArg.Attributes.GetNamedItem("name")
    '        If Not lNameAttribute Is Nothing Then lArgName = lNameAttribute.Value
    '        Select Case lArgName.ToLower
    '            Case "selectfromshapefilename" : aSelectFromShapeFilename = lArg.InnerText
    '            Case "selectfromshapeprojection" : aSelectFromShapeProjection = lArg.InnerText
    '            Case "keyfield" : aKeyFieldName = lArg.InnerText
    '        End Select
    '        lArg = lArg.NextSibling
    '    End While

    '    'TODO: see if this can work as is or if it needs conversion from .prj format to proj4
    '    'If projection was not specified as an argument, see if it exists as a file
    '    'If aSelectFromShapeProjection.Length = 0 Then
    '    '    If FileExists(aSelectFromShapeFilename & ".prj") Then
    '    '        aSelectFromShapeProjection = WholeFileString(aSelectFromShapeFilename & ".prj")
    '    '    End If
    '    'End If

    '    If Not aSelectFromShapeFilename Is Nothing AndAlso _
    '       FileExists(aSelectFromShapeFilename) AndAlso _
    '       aSelectFromShapeProjection.Length > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    ''' <summary>
    ''' Returns the list of 8-digit HUCs in the given 2, 4, or 6-digit HUC
    ''' </summary>
    ''' <param name="aHUC">2, 4, or 6-digit HUC</param>
    ''' <returns>8-digit HUCs as Generic.List of String</returns>
    ''' <remarks>If aHUC is 8 digits or longer, returned list is just the one 8-digit HUC which is or which contains aHUC</remarks>
    Public Shared Function HUC8List(ByVal aHUC As String) As Generic.List(Of String)
        Dim lHUCS As New Generic.List(Of String)
        If aHUC.Length >= 8 Then
            lHUCS.Add(aHUC.Substring(0, 8))
        Else
            Dim lDBFfilename As String = FindFile("Please locate the 8-digit HUC DBF", "huc250d3.dbf")
            If Not IO.File.Exists(lDBFfilename) Then
                Throw New ApplicationException("HUC DBF not found")
            Else
                Dim lDBF As New atcTableDBF
                lDBF.OpenFile(lDBFfilename)
                Dim lHUCfield As Integer = lDBF.FieldNumber("CU")
                If lHUCfield = 0 Then
                    Throw New ApplicationException("CU field not found in HUC DBF '" & lDBFfilename & "'")
                Else
                    For lRecord As Integer = 1 To lDBF.NumRecords
                        Dim lRecordHUC As String = lDBF.Value(lHUCfield)
                        If lRecordHUC.StartsWith(aHUC) Then
                            lHUCS.Add(lRecordHUC)
                        End If
                    Next
                End If
            End If
        End If
        Return lHUCS
    End Function

    '<CLSCompliant(False)> _
    'Public Shared Function GetKeysOfShapesOverlappingBox(ByVal aArgs As Chilkat.Xml, _
    '                                                     ByRef aTop As Double, _
    '                                                     ByRef aBottom As Double, _
    '                                                     ByRef aLeft As Double, _
    '                                                     ByRef aRight As Double) As ArrayList
    '    Dim lBoxProjection As String = ""
    '    Dim lSelectFromShapeFilename As String = "" '"C:\dev\BASINS40\Data\national\huc250d3.shp"
    '    Dim lSelectFromShapeProjection As String = ""
    '    Dim lKeyFieldName As String = ""
    '    If SelectShapesWithBoxArgs(aArgs, _
    '                               aTop, _
    '                               aBottom, _
    '                               aLeft, _
    '                               aRight, _
    '                               lBoxProjection, _
    '                               lSelectFromShapeFilename, _
    '                               lSelectFromShapeProjection, _
    '                               lKeyFieldName) Then

    '        MapWinGeoProc.SpatialReference.ProjectPoint(aLeft, aTop, lBoxProjection, lSelectFromShapeProjection)
    '        MapWinGeoProc.SpatialReference.ProjectPoint(aRight, aBottom, lBoxProjection, lSelectFromShapeProjection)

    '        Return SpatialOperations.GetKeysOfOverlappingShapes(lSelectFromShapeFilename, aTop, aBottom, aLeft, aRight, lKeyFieldName)
    '    End If
    '    Return Nothing
    'End Function

    '<CLSCompliant(False)> _
    'Public Shared Function ProjectedBox(ByVal aTop As Double, _
    '                                    ByVal aBottom As Double, _
    '                                    ByVal aLeft As Double, _
    '                                    ByVal aRight As Double, _
    '                                    ByVal aBoxProjection As String, _
    '                                    ByVal aNewProjection As String) As MapWinGIS.Shape
    '    Dim lBox As New MapWinGIS.Shape
    '    Dim lPoint As MapWinGIS.Point
    '    lBox.Create(MapWinGIS.ShpfileType.SHP_POLYGON)
    '    lPoint = New MapWinGIS.Point
    '    lPoint.x = aLeft
    '    lPoint.y = aTop
    '    MapWinGeoProc.SpatialReference.ProjectPoint(lPoint.x, lPoint.y, aBoxProjection, aNewProjection)
    '    lBox.InsertPoint(lPoint, 0)

    '    lPoint = New MapWinGIS.Point
    '    lPoint.x = aRight
    '    lPoint.y = aTop
    '    MapWinGeoProc.SpatialReference.ProjectPoint(lPoint.x, lPoint.y, aBoxProjection, aNewProjection)
    '    lBox.InsertPoint(lPoint, 0)

    '    lPoint = New MapWinGIS.Point
    '    lPoint.x = aRight
    '    lPoint.y = aBottom
    '    MapWinGeoProc.SpatialReference.ProjectPoint(lPoint.x, lPoint.y, aBoxProjection, aNewProjection)
    '    lBox.InsertPoint(lPoint, 0)

    '    lPoint = New MapWinGIS.Point
    '    lPoint.x = aLeft
    '    lPoint.y = aBottom
    '    MapWinGeoProc.SpatialReference.ProjectPoint(lPoint.x, lPoint.y, aBoxProjection, aNewProjection)
    '    lBox.InsertPoint(lPoint, 0)

    '    Return lBox
    'End Function

    'Public Shared Sub ProjectBox(ByRef aTop As Double, _
    '                               ByRef aBottom As Double, _
    '                               ByRef aLeft As Double, _
    '                               ByRef aRight As Double, _
    '                               ByVal aOldProjection As String, _
    '                               ByVal aNewProjection As String)
    '    'TODO: this does not properly project the other two corners
    '    MapWinGeoProc.SpatialReference.ProjectPoint(aLeft, aTop, aOldProjection, aNewProjection)
    '    MapWinGeoProc.SpatialReference.ProjectPoint(aRight, aBottom, aOldProjection, aNewProjection)
    'End Sub

    '    ''' <summary>
    '    ''' Check for and process a new copy of concise place names from USGS topical gazetteer
    '    ''' </summary>
    '    ''' <param name="aFolder">Folder containing concise place names and created shape files</param>
    '    ''' <returns>instructions for adding any newly created layers</returns>
    '    ''' <remarks>download new place names from http://geonames.usgs.gov/domestic/download_data.htm </remarks>
    '    Public Shared Function CheckPlaceNames(ByVal aFolder As String, ByVal aProjection As DotSpatial.Projections.ProjectionInfo) As String
    '        Dim lInstructions As String = ""
    '        Dim lDownloadedFilename As String = IO.Path.Combine(aFolder, "US_CONCISE.txt")
    '        'Dim lPlacesFolder As String = IO.Path.Combine(aFolder, "places") & g_PathChar
    '        If IO.File.Exists(lDownloadedFilename) Then
    '            Logger.Status("Processing Places", True)
    '            Dim lSourceTable As New atcTableDelimited
    '            lSourceTable.Delimiter = vbTab
    '            If lSourceTable.OpenFile(lDownloadedFilename) Then
    '                With lSourceTable
    '                    Dim lNameField As Integer = .FieldNumber("Feature_Name")
    '                    If lNameField < 1 Then lNameField = .FieldNumber("Name")
    '                    Dim lClassField As Integer = .FieldNumber("Class")
    '                    Dim lLatitudeField As Integer = .FieldNumber("Source_lat_dec")
    '                    Dim lLongitudeField As Integer = .FieldNumber("Source_lon_dec")
    '                    Dim lNewDBFs As New atcCollection
    '                    Dim lNewDBF As atcTableDBF = Nothing
    '                    Dim lNewFieldNums As New ArrayList
    '                    Dim lNewFieldNames As New ArrayList
    '                    lNewFieldNames.Add("") 'Skip position 0
    '                    Dim lOldFieldNum As Integer
    '                    Dim lNewFieldNum As Integer
    '                    Dim lSkippedCivil As Integer = 0
    '                    Dim lSkippedOther As Integer = 0
    '                    For lRecord As Integer = 1 To .NumRecords
    '                        .CurrentRecord = lRecord
    '                        Dim lClass As String = .Value(lClassField)
    '                        Select Case lClass
    '                            Case "Populated Place" : lClass = "Populated"
    '                            Case "Civil"
    '                                Dim lName As String = .Value(lNameField)
    '                                If lName.StartsWith("State of") _
    '                                   OrElse lName.StartsWith("Commonwealth of ") _
    '                                   OrElse lName.EndsWith(" County") _
    '                                   OrElse lName.EndsWith(" Parish") Then
    '                                    lSkippedCivil += 1
    '                                    GoTo NextRecord
    '                                End If
    '                            Case "Cemetery", "Church", "Bend", "Trail", "School"
    '                                lSkippedOther += 1
    '                                GoTo NextRecord 'Ignore the few of these in concise file
    '                        End Select
    '                        'lNewDBF = lNewDBFs.ItemByKey(lClass)
    '                        lClass = "All"
    '                        If lNewDBF Is Nothing Then 'Create new DBF
    '                            If lNewDBFs.Count = 0 Then 'Create first DBF
    '                                lNewFieldNum = 1
    '                                For lOldFieldNum = 1 To .NumFields
    '                                    lNewFieldNames.Add(NewPlaceFieldName(.FieldName(lOldFieldNum)))
    '                                    If lNewFieldNames(lNewFieldNames.Count - 1).Length > 0 Then
    '                                        lNewFieldNums.Add(lNewFieldNum)
    '                                        lNewFieldNum += 1
    '                                    Else
    '                                        lNewFieldNums.Add(-1)
    '                                    End If
    '                                Next
    '                                Dim lOldFieldWidths() As Integer = .ComputeFieldLengths
    '                                lNewDBF = New atcTableDBF
    '                                lNewDBF.NumFields = lNewFieldNum - 1
    '                                For lOldFieldNum = 1 To .NumFields
    '                                    lNewFieldNum = lNewFieldNums(lOldFieldNum - 1)
    '                                    If lNewFieldNum > 0 Then
    '                                        lNewDBF.FieldName(lNewFieldNum) = lNewFieldNames(lOldFieldNum)
    '                                        If .FieldName(lOldFieldNum) = "Class" Then
    '                                            lNewDBF.FieldLength(lNewFieldNum) = 9 'Shortening this field
    '                                        Else
    '                                            lNewDBF.FieldLength(lNewFieldNum) = lOldFieldWidths(lOldFieldNum)
    '                                        End If
    '                                        If lNewDBF.FieldName(lNewFieldNum).EndsWith("itude") Then
    '                                            lNewDBF.FieldDecimalCount(lNewFieldNum) = 6
    '                                        End If
    '                                    End If
    '                                Next
    '                            Else 'Copy fields from first DBF
    '                                lNewDBF = lNewDBFs.ItemByIndex(0).Cousin
    '                            End If
    '                            lNewDBFs.Add(lClass, lNewDBF)
    '                        End If

    '                        lNewDBF.CurrentRecord = lNewDBF.NumRecords + 1
    '                        For lOldFieldNum = 1 To .NumFields
    '                            lNewFieldNum = lNewFieldNums(lOldFieldNum - 1)
    '                            If lNewFieldNum > 0 Then
    '                                lNewDBF.Value(lNewFieldNum) = .Value(lOldFieldNum)
    '                            End If
    '                        Next
    'NextRecord:
    '                    Next
    '                    Logger.Dbg("Skipped " & lSkippedCivil & " Civil, " & lSkippedOther & " others")

    '                    For lDBFIndex As Integer = 0 To lNewDBFs.Count - 1
    '                        lNewDBF = lNewDBFs.ItemByIndex(lDBFIndex)
    '                        If lNewDBF.NumRecords > 0 Then
    '                            'Dim lFilename As String = lPlacesFolder & "Place-" & lNewDBFs.Keys(lDBFIndex) & ".dbf"
    '                            Dim lShapeFilename As String = IO.Path.Combine(aFolder, "places.shp")
    '                            TryDeleteShapefile(lShapeFilename)
    '                            lNewDBF.WriteFile(IO.Path.ChangeExtension(lShapeFilename, ".dbf"))
    '                            ProjectShapefile(GeographicProjection, aProjection, lShapeFilename)
    '                            lInstructions &= "<add_shape>" & lShapeFilename & "</add_shape>" & vbCrLf
    '                        End If
    '                    Next
    '                End With
    '                Logger.Status("")
    '            End If

    '            'Move newly imported file so we don't try to import it again next time
    '            TryDelete(lDownloadedFilename & ".bak")
    '            TryMove(lDownloadedFilename, lDownloadedFilename & ".imported")

    '            If lInstructions.Length > 0 Then 'Add newly created layers to project
    '                lInstructions = "<success>" & lInstructions & "</success>"
    '            End If
    '        End If
    '        Return lInstructions
    '    End Function

    '    Private Shared Function NewPlaceFieldName(ByVal aOldFieldName As String) As String
    '        NewPlaceFieldName = ""
    '        Select Case aOldFieldName
    '            Case "Feature_ID" : NewPlaceFieldName = "ID"
    '            Case "Feature_Name" : NewPlaceFieldName = "Name"
    '            Case "Class" : NewPlaceFieldName = aOldFieldName
    '                'Case "ST_alpha" : NewPlaceFieldName = "State"
    '                'Case "ST_num"
    '                'Case "County"
    '                'Case "County_num" : NewPlaceFieldName = "County_num"
    '                'Case "Primary_lat_DMS", "Primary_lon_DMS"
    '            Case "Primary_lat_dec" : NewPlaceFieldName = "Latitude"
    '            Case "Primary_lon_dec" : NewPlaceFieldName = "Longitude"
    '                'Case "Source_lat_DMS", "Source_lon_DMS", "Source_lat_dec", "Source_lon_dec", "Elev(Meters)", "Map_Name"
    '                'Case "ID", "Name", "State", "County", "Latitude", "Longitude": NewPlaceFieldName = aOldFieldName
    '        End Select
    '    End Function

    'Public Sub UniqueLabels(ByVal aLabelsFilename As String)
    '    If IO.File.Exists(aLabelsFilename) Then

    '    End If
    'End Sub

    Private Shared Function TryFilesMatch(ByVal aFileName1 As String, ByVal aFileName2 As String) As Boolean
        Try
            Return FilesMatch(aFileName1, aFileName2)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ChangeGridFormat(ByVal aSourceFilename As String, ByVal aDestinationFilename As String) As Boolean
        Logger.Dbg("AboutToOpen " & aSourceFilename)
        Dim lSourceGrid = DotSpatial.Data.Raster.Open(aSourceFilename)
        Logger.Dbg("Opened " & aSourceFilename & " Min " & lSourceGrid.Minimum & " Max " & lSourceGrid.Maximum & " NoData " & lSourceGrid.NoDataValue & " Type " & lSourceGrid.DataType.ToString)
        Globals.RepairAlbers(lSourceGrid.Projection)
        lSourceGrid.SaveAs(aDestinationFilename)
        lSourceGrid.Close()
        Logger.Dbg("Saved " & aDestinationFilename)
        Return IO.File.Exists(aDestinationFilename)
        'Return MapWinGeoProc.DataManagement.ChangeGridFormat(aSourceFilename, aDestinationFilename, MapWinGIS.GridFileType.UseExtension, MapWinGIS.GridDataType.UnknownDataType, 1)
    End Function

    ''' <summary>
    ''' Use existing slope or compute from elevation
    ''' </summary>
    ''' <param name="aDemGridLayer">Elevation grid</param>
    ''' <param name="aClipRegion">Optional region to clip elevation to before computing slope</param>
    ''' <remarks></remarks>
    Public Shared Function GetSlopeGrid(ByVal aDemGridLayer As D4EM.Data.Layer,
                                        ByVal aSlopeGridFileName As String,
                               Optional ByVal aClipRegion As D4EM.Data.Region = Nothing) As D4EM.Data.Layer

        'Dim progHandler As DotSpatial.Data.MockICancelProgressHandler = Nothing

        'If Not IsNothing(DotSpatial.Data.DataManager.DefaultDataManager) Then
        '    progHandler = New MockICancelProgressHandler(DotSpatial.Data.DataManager.DefaultDataManager.ProgressHandler)
        '    'progHandler = CType(DotSpatial.Data.DataManager.DefaultDataManager.ProgressHandler, DotSpatial.Data.ICancelProgressHandler)
        'End If
        With aDemGridLayer.AsRaster
            Logger.Dbg("Slope From Elevation " & .Filename & " Min " & .Minimum & " Max " & .Maximum & " NoData " & .NoDataValue & " Type " & .DataType.ToString)
        End With
        Dim lSlopeLayer As New D4EM.Data.Layer(aSlopeGridFileName, New D4EM.Data.LayerSpecification(Role:=D4EM.Data.LayerSpecification.Roles.Slope), False)
        If IO.File.Exists(aSlopeGridFileName) Then
            Logger.Status("UsingExisting " & aSlopeGridFileName)
        Else
            Dim lClippedDEM As DotSpatial.Data.Raster
            If aClipRegion Is Nothing Then
                lClippedDEM = aDemGridLayer.DataSet
            Else
                'clip grid to extents
                Dim lDEMGridClippedFilename As String = atcUtility.GetTemporaryFileName(IO.Path.Combine(PathNameOnly(aDemGridLayer.FileName), "dem_clipped"), ".tif")
                aClipRegion.ClipGrid(aDemGridLayer.FileName, lDEMGridClippedFilename)
                lClippedDEM = DotSpatial.Data.Raster.Open(lDEMGridClippedFilename)
            End If

            Globals.RepairAlbers(lClippedDEM.Projection)

            'z factor - 0.01 = cm to m
            Dim lCalculatedSlope As DotSpatial.Data.Raster = DotSpatial.Analysis.Slope.GetSlope(lClippedDEM, 0.01, True, Nothing)
            If lCalculatedSlope Is Nothing Then
                Logger.Dbg("Unable to calculate slope in  '" & aDemGridLayer.FileName & "'")
            Else
                Logger.Dbg("Calculated Slopes " & MemUsage())
                lCalculatedSlope.Projection = lClippedDEM.Projection
                lCalculatedSlope.SaveAs(aSlopeGridFileName)
                Layer.CopyProcStepsFromCachedFile(aDemGridLayer.FileName, aSlopeGridFileName)
                Layer.AddProcessStepToFile("Computed Slope using DotSpatial.Analysis.Slope.GetSlope", aSlopeGridFileName)
                lCalculatedSlope.Close() 'Close this version because it is internally of type BgdRaster
                'lSlopeLayer.DataSet = lCalculatedSlope 'This will be read in automatically later as GeoTIFF instead of assigning here
            End If
        End If
        Return lSlopeLayer
    End Function

    Public Shared Function ValidGridValueNear(ByVal lDemGrid As DotSpatial.Data.Raster, ByVal lRowCol As DotSpatial.Data.RcIndex, ByVal aMinimumValid As Double, ByVal aMaximumValid As Double) As Double
        Dim lElevation As Double = lDemGrid.Value(lRowCol.Row, lRowCol.Column) 'centimeters
        ' This version attempts to be more efficient by skipping out-of-bounds areas, but is not yet correct
        '        If Double.IsNaN(lElevation) OrElse lElevation = lDemGrid.NoDataValue OrElse lElevation < aMinimumValid OrElse lElevation > aMaximumValid Then
        '            'Use a spiral search pattern to find a valid elevation value
        '            Dim lDirection As Integer = 1 'Start by looking one cell right, spiral counterclockwise and outward until a valid value is found
        '            Dim lRemainingDistance As Integer = 2
        '            Dim lDistance As Integer = 1
        '            Dim dx As Integer = 0
        '            Dim dy As Integer = 0
        '            Dim lSpiralRow As Integer = lRowCol.Row
        '            Dim lSpiralCol As Integer = lRowCol.Column
        '            Dim lMaxRow As Integer = lDemGrid.NumRows - 1
        '            Dim lMaxCol As Integer = lDemGrid.NumColumns - 1
        'NextSpiral:
        '            lRemainingDistance -= 1
        '            If lRemainingDistance = 0 Then
        '                Select Case lDirection
        '                    Case 0
        '                        lDistance += 1
        '                        lDirection = 3 'Turn from marching Up to Left
        '                    Case 1
        '                        lDirection = 0 'Turn from marching Right to Up
        '                    Case 2
        '                        lDistance += 1
        '                        lDirection = 1 'Turn from marching Down to Right
        '                    Case 3
        '                        lDirection = 2 'Turn from marching Left to Down
        '                End Select
        '                lRemainingDistance = lDistance
        '            End If

        '            Select Case lDirection
        '                Case 0 : dy -= 1 'March Up
        '                Case 1 : dx += 1 'March Right
        '                Case 2 : dy += 1 'March Down
        '                Case 3 : dx -= 1 'March Left
        '            End Select
        'SetRowCol:
        '            lSpiralRow = lRowCol.Row + dy
        '            lSpiralCol = lRowCol.Column + dx

        '            If lSpiralRow < 0 Then 'Reached top edge, skip scanning left across top because top would be outside grid
        '                lDistance += 1 'Spiral gets larger
        '                lDirection = 2 'Turn from marching Up to Down
        '                dx = -lDistance / 2 'move from right edge to left
        '                dy = -lRowCol.Row
        '                lRemainingDistance = lDistance + dy + 1
        '                GoTo SetRowCol
        '            ElseIf lSpiralRow > lMaxRow Then 'Reached bottom edge, skip scanning right across bottom
        '                lDistance += 1 'Spiral gets larger
        '                lDirection = 0 'Turn from marching Down to Up
        '                dx = lDistance / 2 'move from left edge to right
        '                dy = lMaxRow - lRowCol.Row
        '                GoTo SetRowCol
        '            ElseIf lSpiralCol < 0 Then 'Reached left edge, skip scanning down across left edge
        '                lDirection = 1 'Turn to marching Right
        '                dx = -lRowCol.Column 'skip to lSpiralCol=0
        '                dy = lDistance / 2
        '                lRemainingDistance = lDistance / 2 - dx + 1
        '                lDistance += 1 'Spiral gets larger
        '                GoTo SetRowCol
        '            ElseIf lSpiralCol > lMaxCol Then
        '                lDirection = 3 'Turn from marching Right to Left
        '                lRemainingDistance = lDistance - lRemainingDistance
        '                dy = 1 - lDistance
        '                GoTo SetRowCol
        '            End If

        '            Logger.Dbg("Testing  (" & lSpiralCol & ", " & lSpiralRow & ") +(" & dx & ", " & dy & ")")
        '            lElevation = lDemGrid.Value(lSpiralRow, lSpiralCol)
        '            If Not Double.IsNaN(lElevation) AndAlso lElevation <> lDemGrid.NoDataValue AndAlso lElevation >= aMinimumValid AndAlso lElevation <= aMaximumValid Then
        '                Logger.Dbg("Found valid grid value at (" & lSpiralCol & ", " & lSpiralRow & ") offset (" & dx & ", " & dy & ") from (" & lRowCol.Column & ", " & lRowCol.Row & ")")
        '                Return lElevation
        '            End If
        '            GoTo NextSpiral
        '        End If

        If Double.IsNaN(lElevation) OrElse lElevation = lDemGrid.NoDataValue OrElse lElevation < aMinimumValid OrElse lElevation > aMaximumValid Then
            'Use a spiral search pattern to find a valid elevation value
            Dim lDirection As Integer = 1
            Dim lRemainingDistance As Integer = 2
            Dim lDistance As Integer = 1
            Dim dx As Integer = 0
            Dim dy As Integer = 0
            Dim lSpiralRow As Integer = lRowCol.Row
            Dim lSpiralCol As Integer = lRowCol.Column
            Dim lMaxRow As Integer = lDemGrid.NumRows - 1
            Dim lMaxCol As Integer = lDemGrid.NumColumns - 1
NextPoint:
            lRemainingDistance -= 1
            If lRemainingDistance = 0 Then
                Select Case lDirection
                    Case 0 'Up
                        lDistance += 1
                        lDirection = 3 'Left
                    Case 1 'Right
                        lDirection = 0 'Up
                    Case 2 ' Down
                        lDistance += 1
                        lDirection = 1 'Right
                    Case 3 'Left
                        lDirection = 2 'Down
                End Select
                If lDistance > lMaxCol AndAlso lDistance > lMaxRow Then
                    Throw New ApplicationException("No valid values found in grid")
                End If
                lRemainingDistance = lDistance
            End If
            Select Case lDirection
                Case 0 'Up
                    dy -= 1
                Case 1 'Right
                    dx += 1
                Case 2 ' Down
                    dy += 1
                Case 3 'Left
                    dx -= 1
            End Select
            lSpiralRow = lRowCol.Row + dy
            If lSpiralRow >= 0 AndAlso lSpiralRow <= lMaxRow Then
                lSpiralCol = lRowCol.Column + dx
                If lSpiralCol >= 0 AndAlso lSpiralCol < lMaxCol Then
                    lElevation = lDemGrid.Value(lSpiralRow, lSpiralCol)
                    If (Not Double.IsNaN(lElevation) AndAlso lElevation <> lDemGrid.NoDataValue AndAlso lElevation >= aMinimumValid AndAlso lElevation <= aMaximumValid) Then
                        Logger.Dbg("Found valid grid value at (" & lSpiralCol & ", " & lSpiralRow & ") displaced (" & dx & ", " & dy & ") from (" & lRowCol.Column & ", " & lRowCol.Row & ")")
                        Return lElevation
                    End If
                End If
            End If
            GoTo NextPoint
        End If
        Return lElevation
    End Function

    'Public Shared GridShapeIDLayer As DotSpatial.Data.Raster(Of Integer) = Nothing

    ''' <summary>
    ''' Compute the mean of the grid cell values that fall within a polygon
    ''' </summary>
    ''' <param name="aGridLayer">Grid of values</param>
    ''' <param name="aShape">Shape to restrict which grid cells to include</param>
    ''' <param name="aMinGridValue">Skip grid values less than this (omit or use Double.NaN if there is no minimum grid value to include in computation)</param>
    ''' <param name="aMaxGridValue">Skip grid values greater than this (omit or use Double.NaN if there is no maximum grid value to include in computation)</param>
    ''' <returns>Mean of grid values of cells inside polygon and between aMinGridValue and aMaxGridValue</returns>
    Public Shared Function ComputeGridMeanInPolygon(ByVal aGridLayer As D4EM.Data.Layer,
                                                    ByVal aShape As DotSpatial.Data.Feature,
                                                    ByVal aShapeRange As DotSpatial.Data.ShapeRange,
                                                    ByVal aShapeID As Integer,
                                           Optional ByVal aMinGridValue As Double = Double.NaN,
                                           Optional ByVal aMaxGridValue As Double = Double.NaN) As Double
        'Given a grid and a polygon layer, find the mean grid value within the feature.

        'set input grid
        Dim lInputGrid As DotSpatial.Data.Raster = aGridLayer.DataSet

        'figure out what part of the grid overlays this polygon

        Dim lStartRow As Integer, lStartCol As Integer
        Dim lEndRow As Integer, lEndCol As Integer

        Dim env = aShape.Geometry.EnvelopeInternal

        aGridLayer.CellBounds(env.MaxX, env.MinY, env.MinX, env.MaxY, lStartRow, lStartCol, lEndRow, lEndCol)
        'With aShape..Envelope
        ' aGridLayer.CellBounds(.MinX, .MinY,
        '.MaximumX, .MaxY,
        'lStartRow, lStartCol,
        'lEndRow, lEndCol)
        'End With

        'If GridShapeIDLayer Is Nothing Then
        '    GridShapeIDLayer = DotSpatial.Data.Raster.Create(IO.Path.ChangeExtension(lInputGrid.Filename, ".ShapeRangePolygon" & IO.Path.GetExtension(lInputGrid.Filename)),
        '                                                                       lInputGrid.DriverCode, lInputGrid.NumRows, lInputGrid.NumColumns, 1, GetType(Integer), New String() {""})
        '    GridShapeIDLayer.NoDataValue = -1
        '    GridShapeIDLayer.Bounds = lInputGrid.Bounds.Copy
        '    GridShapeIDLayer.Projection = lInputGrid.Projection
        'End If

        Dim lCellcount As Integer = 0
        Dim lTooBigCellCount As Integer = 0
        Dim lTooSmallCellCount As Integer = 0
        Dim lPolygonGeometry As NetTopologySuite.Geometries.Geometry = aShape.Geometry
        Dim lXYPos As NetTopologySuite.Geometries.Coordinate
        Dim lIsMulti As Boolean = (aShapeRange.NumParts > 1)
        Dim lIntersects As Boolean = True
        Dim lSum As Double = 0
        Dim lValue As Double = 0
        For lCol As Integer = lStartCol To lEndCol
            For lRow As Integer = lStartRow To lEndRow
                lXYPos = DotSpatial.Data.RasterExt.CellToProj(lInputGrid, lRow, lCol)
                'If lIsMulti Then   'testing with the assumption that they always intersect, to speed up runtime
                '    lIntersects = lPolygonGeometry.Intersects(lXYPos.X, lXYPos.Y)
                'Else
                '    lIntersects = aShapeRange.Intersects(lXYPos)
                'End If
                'Dim lGeometryIntersects As Boolean = lPolygonGeometry.Intersects(lXYPos.X, lXYPos.Y) 'this is in the polygon we want
                'Dim lShapeRangeIntersects As Boolean = aShapeRange.Intersects(lXYPos)
                'If lGeometryIntersects <> lShapeRangeIntersects Then
                '    Logger.Dbg("GeometryIntersects=" & lGeometryIntersects & " ShapeRangeIntersects=" & lShapeRangeIntersects & " Shape " & aShapeID & " (" & aShape.DataRow(0) & ") Grid Row " & lRow & " Col " & lCol & " X= " & DoubleToString(lXYPos.X) & " Y= " & DoubleToString(lXYPos.Y))
                '    'GridShapeIDLayer.Value(lRow, lCol) = -99
                'End If
                If lIntersects Then
                    'Select Case GridShapeIDLayer.Value(lRow, lCol)
                    '    Case -99 'Preserve mismatch value in grid
                    '    Case Is > 0
                    '        Logger.Dbg("Already had grid polygon value " & GridShapeIDLayer.Value(lRow, lCol) & " when setting " & aShapeID & " at " & lRow & ", " & lCol)
                    '    Case Else
                    '        GridShapeIDLayer.Value(lRow, lCol) = aShapeID
                    'End Select
                    lValue = lInputGrid.Value(lRow, lCol)
                    If lValue > aMaxGridValue Then
                        lTooBigCellCount += 1
                    ElseIf lValue < aMinGridValue Then
                        lTooSmallCellCount += 1
                    Else
                        lSum += lValue
                        lCellcount += 1
                    End If
                End If
            Next lRow
        Next lCol

        'GridShapeIDLayer.Save()

        lInputGrid = Nothing

        Dim lMean As Double
        If lCellcount > 0 Then
            lMean = lSum / lCellcount
        Else
            lMean = -999
        End If
        Dim lMessage As String = "GridMeanInPolygon(" & aShapeID & "): " & DoubleToString(lMean) & " using " & lCellcount & " cells."
        If lTooSmallCellCount > 0 Then
            lMessage &= " Skipped " & lTooSmallCellCount & " values < " & DoubleToString(aMinGridValue)
        End If
        If lTooBigCellCount > 0 Then
            lMessage &= " Skipped " & lTooBigCellCount & " values > " & DoubleToString(aMaxGridValue)
        End If
        Logger.Dbg(lMessage)
        Return lMean
    End Function

    ''' <summary>
    ''' Executes cutting of a raster to a feature
    ''' </summary>
    ''' <param name="aRaster">The input raster.</param>
    ''' <param name="aPolygon">The input feature.</param>
    ''' <param name="aSaveAs">The Filename of output raster.</param>
    ''' <returns></returns>
    Public Shared Function ClipGrid2Feature(ByVal aRaster As IRaster, ByVal aPolygon As IFeature, ByVal aSaveAs As String) As IRaster
        If aRaster Is Nothing OrElse aPolygon Is Nothing Then
            Return Nothing
        End If

        Dim cellWidth As Double = aRaster.CellWidth
        Dim cellHeight As Double = aRaster.CellHeight
        Dim lPolyExtent = New DotSpatial.Data.Extent(aPolygon.Geometry.Envelope.EnvelopeInternal)
        Dim SharedExtent As DotSpatial.Data.Extent = aRaster.Bounds.Extent.Intersection(lPolyExtent)

        Dim lNewMinCell = DotSpatial.Data.RasterExt.ProjToCell(aRaster, SharedExtent.MinX, SharedExtent.MaxY)
        If lNewMinCell.IsEmpty Then lNewMinCell = New DotSpatial.Data.RcIndex(0, 0)
        Dim lNewMaxCell = DotSpatial.Data.RasterExt.ProjToCell(aRaster, SharedExtent.MaxX, SharedExtent.MinY)
        If lNewMaxCell.IsEmpty Then lNewMaxCell = New DotSpatial.Data.RcIndex(aRaster.EndRow, aRaster.EndColumn)

        'If lNewMinCell.Column < 0 Then
        '    lNewMinCell.Column = 0
        'ElseIf lNewMinCell.Column >= aRaster.NumColumns Then
        '    lNewMinCell.Column = aRaster.NumColumns - 1
        'End If

        'If lNewMaxCell.Column < 0 Then
        '    lNewMaxCell.Column = 0
        'ElseIf lNewMinCell.Column >= aRaster.NumColumns Then
        '    lNewMaxCell.Column = aRaster.NumColumns - 1
        'End If

        Dim lNewMinCoord = DotSpatial.Data.RasterBoundsExt.CellBottomLeftToProj(aRaster.Bounds, lNewMinCell.Row, lNewMinCell.Column)
        Dim lNewMaxCoord = DotSpatial.Data.RasterBoundsExt.CellTopRightToProj(aRaster.Bounds, lNewMaxCell.Row, lNewMaxCell.Column)

        Dim lNewNumCols As Integer = lNewMaxCell.Column - lNewMinCell.Column + 1
        Dim lNewNumRows As Integer = lNewMaxCell.Row - lNewMinCell.Row + 1

        'create output raster
        Dim output As IRaster
        output = Raster.Create(aSaveAs, aRaster.DriverCode, lNewNumRows, lNewNumCols, 1, aRaster.DataType, New String() {""})
        output.NoDataValue = aRaster.NoDataValue
        output.Bounds = New RasterBounds(lNewNumRows, lNewNumCols, New Extent(lNewMinCoord.X, lNewMinCoord.Y, lNewMinCoord.X, lNewMaxCoord.Y))
        output.Projection = aRaster.Projection
        output.NoDataValue = aRaster.NoDataValue

        Dim previous As Integer = 0

        Dim lLastNewRow As Integer = (output.Bounds.NumRows - 1)
        Dim lLastNewColumn As Integer = (output.Bounds.NumColumns - 1)
        For lNewRow As Integer = 0 To lLastNewRow
            For lNewColumn As Integer = 0 To lLastNewColumn
                Dim cellCenter As NetTopologySuite.Geometries.Coordinate = output.CellToProj(lNewRow, lNewColumn)
                Dim env As NetTopologySuite.Geometries.Envelope = New Envelope(cellCenter)
                If aPolygon.Intersects(env) Then
                    output.Value(lNewRow, lNewColumn) = aRaster.Value(lNewRow + lNewMinCell.Row, lNewColumn + lNewMinCell.Column)
                Else
                    output.Value(lNewRow, lNewColumn) = output.NoDataValue
                End If
            Next
            Logger.Progress(lNewRow, lLastNewRow)
        Next

        output.GetStatistics()
        output.Save()
        Return output
    End Function

    'Public Shared Sub SetGridProjection(ByVal aGridFilename As String, ByVal aProjection As String)
    '    Dim lGrid As New MapWinGIS.Grid

    '    If lGrid.Open(aGridFilename, MapWinGIS.GridDataType.UnknownDataType, True, MapWinGIS.GridFileType.UseExtension, Nothing) Then
    '        lGrid.AssignNewProjection(aProjection)
    '        lGrid.Close()
    '    End If
    'End Sub
End Class

'Public Class MapWinCallback
'    Implements MapWinGIS.ICallback

'    Public Sub myError(ByVal KeyOfSender As String, ByVal ErrorMsg As String) Implements MapWinGIS.ICallback.Error
'        Logger.Msg(ErrorMsg, "Error - MapWinGIS.ICallback " & KeyOfSender)
'    End Sub
'    Public Sub Progress(ByVal KeyOfSender As String, ByVal Percent As Integer, ByVal Message As String) Implements MapWinGIS.ICallback.Progress
'        Logger.Progress(Percent, 100)
'        If Not String.IsNullOrEmpty(Message) Then Logger.Status("LABEL MIDDLE " & Message)
'    End Sub
'End Class
