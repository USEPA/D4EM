Imports atcUtility
Imports MapWinUtility
Imports MapWinUtility.Strings
Imports D4EM.Geo.NetworkOperations


Public Module modSDM
    Friend g_AppNameRegistry As String = "FramesSDM" 'For preferences in registry
    Friend g_AppNameShort As String = "FramesSDM"
    Friend g_AppNameLong As String = "Frames SDM"

    'Private pThreadMax As Integer = 2
    'Private pThreadCount As Integer = 0

    Friend Const PARAMETER_FILE As String = "SDMParameters.txt"

    Public Sub main()
        Dim lParametersFilename As String = Command()
        If Not IO.File.Exists(lParametersFilename) Then
            Dim lOpenDialog As New Windows.Forms.OpenFileDialog()
            lOpenDialog.Title = "Select SDM Project Builder Parameter File"
            lOpenDialog.FileName = PARAMETER_FILE
            If lOpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                lParametersFilename = lOpenDialog.FileName
            End If
        End If
        If IO.File.Exists(lParametersFilename) Then
            D4EM.Data.Globals.Initialize()
            Logger.StartToFile(PathNameOnly(Reflection.Assembly.GetEntryAssembly.Location) & g_PathChar & "logs" & g_PathChar & "D4EM-" _
                                 & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & ".log")
            Run(lParametersFilename)
        End If
    End Sub

    Public Sub Run(ByVal aParametersFilename As String)
        If aParametersFilename Is Nothing Then aParametersFilename = String.Empty
        If IO.File.Exists(aParametersFilename) Then
            Dim lParameters As New SDMParameters(aParametersFilename)
            Dim lCreatedMapWindowProjectFilenames As Generic.List(Of String) = SpecifyAndCreateNewProjects(lParameters)

            ''Copy parameters file into each project folder (currently this is taken care of in DownloadDataSetupModels with aParameters.WriteParametersTextFile)
            'For Each lCreatedProjectFileName As String In lCreatedMapWindowProjectFilenames
            '    Dim lCopyTo As String = IO.Path.Combine(IO.Path.GetDirectoryName(lCreatedProjectFileName), IO.Path.GetFileName(aParametersFilename))
            '    If Not IO.File.Exists(lCopyTo) Then TryCopy(aParametersFilename, lCopyTo)
            'Next
        Else
            MsgBox("Could not find SDM Project Builder parameter file '" & aParametersFilename & "' so exiting")
        End If
    End Sub

    Friend Sub ProcessNetwork(ByVal aProject As D4EM.Data.Project,
                              ByVal aPreSelectedCatchments As Generic.List(Of String),
                              ByVal aClipCatchments As Boolean,
                              ByVal aMinCatchmentKM2 As Double,
                              ByVal aMinFlowlineKM As Double,
                              ByVal aOriginalFlowlinesLayer As D4EM.Data.Layer,
                              ByVal aOriginalCatchmentsLayer As D4EM.Data.Layer,
                              ByRef aSimplifiedFlowlinesLayer As D4EM.Data.Layer,
                              ByRef aSimplifiedCatchmentsLayer As D4EM.Data.Layer,
                              ByRef aFields As FieldIndexes,
                              ByVal aBoundariesFilename As String, _
                              ByVal aOutputsFilename As String)
        Dim lProblem As String = ""

        aSimplifiedFlowlinesLayer = Nothing
        aSimplifiedCatchmentsLayer = Nothing

        Try
            Dim lFlowLinesToUseFilename As String = IO.Path.Combine(IO.Path.GetDirectoryName(aOriginalFlowlinesLayer.FileName), "use" & IO.Path.GetFileName(aOriginalFlowlinesLayer.FileName))
            Dim lCatchmentsToUseFilename As String = IO.Path.Combine(IO.Path.GetDirectoryName(aOriginalCatchmentsLayer.FileName), "use" & IO.Path.GetFileName(aOriginalCatchmentsLayer.FileName))

            Dim lFlowLines As DotSpatial.Data.FeatureSet = aOriginalFlowlinesLayer.AsFeatureSet
            Dim lCatchments As DotSpatial.Data.FeatureSet = aOriginalCatchmentsLayer.AsFeatureSet

            aFields = New D4EM.Geo.NetworkOperations.FieldIndexes(lFlowLines, lCatchments)

            'If aPreSelectedCatchments IsNot Nothing AndAlso aPreSelectedCatchments.Count > 0 Then
            '    ClipCatchmentsPreSelected(lCatchmentsToUseFilename, lCatchments, aPreSelectedCatchments, aFields)
            'ElseIf aClipCatchments Then
            If aClipCatchments Then
                Logger.Status("CatchmentCount before Clipping " & lCatchments.Features.Count, True)
                ClipCatchmentsToShape(lCatchmentsToUseFilename, lCatchments, aProject.Region.ToShape(lCatchments.Projection).ToGeometry)
            Else
                atcUtility.TryCopyShapefile(aOriginalCatchmentsLayer.FileName, lCatchmentsToUseFilename)
            End If

            'check for channel with no contrib area - no associated catchment COMID, remove and report
            If D4EM.Geo.NetworkOperations.ClipFlowLinesToCatchments(lCatchmentsToUseFilename, lFlowLines, lFlowLinesToUseFilename, aFields) Then
                Logger.Status("Clipped Flowlines", True)
            Else
                Logger.Status("Unable to clip NHDPlus flowlines in '" & aOriginalFlowlinesLayer.FileName & "'")
            End If

            Logger.Status("CombineShortOrBraidedFlowlines with MinCatchment " & DoubleToString(aMinCatchmentKM2))
            Dim lSimplifiedFlowlinesFileName = lFlowLinesToUseFilename.Substring(0, lFlowLinesToUseFilename.Length - 4) & "NoShort.shp"
            Dim lSimplifiedCatchmentsFileName As String = lCatchmentsToUseFilename.Substring(0, lCatchmentsToUseFilename.Length - 4) & "NoShort.shp"

            CombineShortOrBraidedFlowlines(New D4EM.Data.Layer(lFlowLinesToUseFilename, aOriginalFlowlinesLayer.Specification), _
                                           New D4EM.Data.Layer(lCatchmentsToUseFilename, aOriginalCatchmentsLayer.Specification), _
                                           lSimplifiedFlowlinesFileName, _
                                           lSimplifiedCatchmentsFileName, _
                                           aMinCatchmentKM2, aMinFlowlineKM, _
                                           aFields, aBoundariesFilename, aOutputsFilename)
            If IO.File.Exists(lSimplifiedFlowlinesFileName) Then
                aSimplifiedFlowlinesLayer = New D4EM.Data.Layer(lSimplifiedFlowlinesFileName, aOriginalFlowlinesLayer.Specification)
                aProject.Layers.Remove(aOriginalFlowlinesLayer)
                aOriginalFlowlinesLayer.Close()
                aProject.Layers.Add(aSimplifiedFlowlinesLayer)
            End If

            If IO.File.Exists(lSimplifiedCatchmentsFileName) Then
                aSimplifiedCatchmentsLayer = New D4EM.Data.Layer(lSimplifiedCatchmentsFileName, aOriginalCatchmentsLayer.Specification)
                aProject.Layers.Remove(aOriginalCatchmentsLayer)
                aOriginalCatchmentsLayer.Close()
                aProject.Layers.Add(aSimplifiedCatchmentsLayer)
            End If
            Logger.Dbg("DoneSimplifyHydrography " & MemUsage())

        Catch lEx As Exception
            lProblem = "Exception " & lEx.Message & vbCrLf & lEx.StackTrace
            Logger.Dbg(lProblem)
        End Try

    End Sub

    Friend Sub ClipCatchmentsToShape(ByVal aCatchmentsToUseFileName As String,
                                     ByVal aCatchments As DotSpatial.Data.Shapefile,
                                     ByVal aShapeOfInterest As NetTopologySuite.Geometries.Geometry)
        Dim lCatchmentsToUse As New DotSpatial.Data.PolygonShapefile
        Dim lLargestFractionInsideLeftOut As Double = 0
        Dim lSmallestFractionInsideKept As Double = 1
        Dim lLargestToUse As Double = atcUtility.GetNaN
        Dim lSmallestToUse As Double = atcUtility.GetNaN
        Dim lCatchmentsDBF As New atcUtility.atcTableDBF
        lCatchmentsDBF.OpenFile(IO.Path.ChangeExtension(aCatchments.Filename, "dbf"))

        Dim lCatchmentsToUseDBF As atcUtility.atcTableDBF = lCatchmentsDBF.Cousin
        lCatchmentsToUseDBF.InitData()

        lCatchmentsToUse.Filename = aCatchmentsToUseFileName
        lCatchmentsToUse.FeatureType = aCatchments.FeatureType

        For lCatchmentShapeIndex As Integer = 0 To aCatchments.Features.Count - 1
            Dim lCatchmentFeature As DotSpatial.Data.Feature = aCatchments.Features(lCatchmentShapeIndex)
            Dim lCatchmentShape As New DotSpatial.Data.Shape(lCatchmentFeature)
            Dim lIntersection As NetTopologySuite.Geometries.Geometry = lCatchmentShape.ToGeometry.Intersection(aShapeOfInterest)
            If lIntersection IsNot Nothing AndAlso lIntersection.NumPoints > 0 Then
                'Dim lCatchmentArea As Double = DotSpatial.Data.FeatureExt.Area(lCatchmentFeature)
                Dim lCatchmentArea As Double = lCatchmentFeature.Geometry.Area
                If lCatchmentArea > 0 Then
                    Dim lIntersectionArea As Double = lIntersection.Area
                    Dim lFractionInside As Double = lIntersectionArea / lCatchmentArea
                    If lFractionInside > 0.5 Then '0.5 = 50%
                        lCatchmentsToUse.Features.Add(lCatchmentFeature)

                        lCatchmentsDBF.CurrentRecord = lCatchmentShapeIndex + 1
                        lCatchmentsToUseDBF.CurrentRecord = lCatchmentsToUseDBF.NumRecords + 1
                        lCatchmentsToUseDBF.RawRecord = lCatchmentsDBF.RawRecord

                        If lFractionInside < lSmallestFractionInsideKept Then lSmallestFractionInsideKept = lFractionInside
                        If lCatchmentArea > lLargestToUse OrElse Double.IsNaN(lLargestToUse) Then lLargestToUse = lCatchmentArea
                        If lCatchmentArea < lSmallestToUse OrElse Double.IsNaN(lSmallestToUse) Then lSmallestToUse = lCatchmentArea
                        'LogMessage(aLog,"Catchment " & lCatchmentShapeIndex & " added: " & lFractionInside.ToString("0.0%"))
                    Else
                        If lFractionInside > lLargestFractionInsideLeftOut Then lLargestFractionInsideLeftOut = lFractionInside
                        'LogMessage(aLog,"Catchment " & lCatchmentShapeIndex & " skipped: " & lFractionInside.ToString("0.0%"))
                    End If
                Else
                    'LogMessage(aLog,"Catchment " & lCatchmentShapeIndex & " skipped, area<=0: " & lCatchmentArea.ToString)
                End If
            Else
                'LogMessage(aLog,"Catchment " & lCatchmentShapeIndex & " skipped, does not intersect shape of interest")
            End If
        Next
        Logger.Status("Writing " & lCatchmentsToUse.Features.Count & " catchments inside shape of interest, leaving out " & aCatchments.Features.Count - lCatchmentsToUse.Features.Count, True)
        Logger.Dbg("Smallest catchment (" & lSmallestToUse.ToString("#,###") & ") is " & (lSmallestToUse / lLargestToUse).ToString("0.0000%") & " of area of largest (" & lLargestToUse.ToString("#,###") & ")")
        If lLargestFractionInsideLeftOut > 0.001 Then Logger.Dbg("Largest percent inside left out = " & lLargestFractionInsideLeftOut.ToString("0.0000%"))
        If lSmallestFractionInsideKept < 0.999 Then Logger.Dbg("Smallest percent inside kept = " & lSmallestFractionInsideKept.ToString("0.0000%"))
        lCatchmentsToUse.Projection = aCatchments.Projection
        lCatchmentsToUse.Save()
        lCatchmentsToUse.Close()
        lCatchmentsToUseDBF.WriteFile(IO.Path.ChangeExtension(aCatchmentsToUseFileName, "dbf"))
        TryCopy(IO.Path.ChangeExtension(aCatchments.Filename, "mwsr"),
                IO.Path.ChangeExtension(aCatchmentsToUseFileName, "mwsr"))
    End Sub

    Friend Sub ClipCatchmentsPreSelected(ByVal aCatchmentsToUseFileName As String,
                                         ByVal aCatchments As DotSpatial.Data.Shapefile,
                                         ByVal aPreSelectedCatchments As Generic.List(Of String),
                                         ByVal aFields As FieldIndexes)
        Dim lCatchmentsDBF As New atcUtility.atcTableDBF
        lCatchmentsDBF.OpenFile(IO.Path.ChangeExtension(aCatchments.Filename, "dbf"))
        Dim lCOMIDfield = lCatchmentsDBF.FieldNumber(aFields.CatchmentComId + 1) '+1 because atcDBF is 1-based, FieldIndexes is zero-based

        Dim lCatchmentsToUseDBF As atcUtility.atcTableDBF = lCatchmentsDBF.Cousin
        lCatchmentsToUseDBF.InitData()

        Dim lCatchmentsToUse As New DotSpatial.Data.PolygonShapefile
        lCatchmentsToUse.Filename = aCatchmentsToUseFileName
        lCatchmentsToUse.FeatureType = aCatchments.FeatureType

        For lCatchmentShapeIndex As Integer = 0 To aCatchments.Features.Count - 1
            lCatchmentsDBF.CurrentRecord = lCatchmentShapeIndex + 1
            If aPreSelectedCatchments.Contains(lCatchmentsDBF.Value(lCOMIDfield)) Then
                lCatchmentsToUse.Features.Add(aCatchments.Features(lCatchmentShapeIndex))
                lCatchmentsToUseDBF.CurrentRecord = lCatchmentsToUseDBF.NumRecords + 1
                lCatchmentsToUseDBF.RawRecord = lCatchmentsDBF.RawRecord
            End If
        Next
        Logger.Status("Keeping " & lCatchmentsToUse.Features.Count & " pre-selected catchments, leaving out " & aCatchments.Features.Count - lCatchmentsToUse.Features.Count, True)
        lCatchmentsToUse.Projection = aCatchments.Projection
        lCatchmentsToUse.Save()
        lCatchmentsToUse.Close()
        lCatchmentsToUseDBF.WriteFile(IO.Path.ChangeExtension(aCatchmentsToUseFileName, "dbf"))
        TryCopy(IO.Path.ChangeExtension(aCatchments.Filename, "mwsr"), _
                IO.Path.ChangeExtension(aCatchmentsToUseFileName, "mwsr"))
    End Sub

    ''' <summary>
    ''' Eliminate flowlines and catchments by combining them up or down stream to reach large enough area and/or length
    ''' Eliminate braided streams by keeping only the preferred channel
    ''' </summary>
    ''' <param name="aFlowlinesLayer">Flowlines Layer to simplify</param>
    ''' <param name="aCatchmentLayer">Catchments Layer to simplify</param>
    ''' <param name="aSimplifiedFlowlinesFileName">File name for newly created simplified flowlines</param>
    ''' <param name="aSimplifiedCatchmentFileName">File name for newly created simplified catchments</param>
    ''' <param name="aMinCatchmentKM2">Minimum area of a catchment (catchments smaller than this will be merged up or downstream)</param>
    ''' <param name="aMinLengthKM">Minimum length of a flowline (flowlines shorter than this will be merged up or downstream)</param>
    ''' <param name="aFields">Hydrologic field indexes of flowlines and catchments</param>
    ''' <param name="aBoundariesFilename">Comma-separated file of locations of upstream boundary conditions</param>
    ''' <param name="aOutputsFilename">Comma-separated file of locations of desired model outputs</param>
    ''' <returns>True indicates success</returns>
    Friend Function CombineShortOrBraidedFlowlines(ByVal aFlowlinesLayer As D4EM.Data.Layer, _
                                                   ByVal aCatchmentLayer As D4EM.Data.Layer, _
                                                   ByVal aSimplifiedFlowlinesFileName As String, _
                                                   ByVal aSimplifiedCatchmentFileName As String, _
                                                   ByVal aMinCatchmentKM2 As Double, _
                                                   ByVal aMinLengthKM As Double, _
                                                   ByVal aFields As FieldIndexes, _
                                                   ByVal aBoundariesFilename As String, _
                                                   ByVal aOutputsFilename As String) As Boolean
        Dim lSaveIntermediate As Integer = 500

        atcUtility.TryDeleteShapefile(aSimplifiedFlowlinesFileName)
        atcUtility.TryCopyShapefile(aFlowlinesLayer.FileName, aSimplifiedFlowlinesFileName)

        atcUtility.TryDeleteShapefile(aSimplifiedCatchmentFileName)
        atcUtility.TryCopyShapefile(aCatchmentLayer.FileName, aSimplifiedCatchmentFileName)

        Dim lSimplifiedFlowlines As DotSpatial.Data.FeatureSet = DotSpatial.Data.FeatureSet.Open(aSimplifiedFlowlinesFileName)
        Dim lSimplifiedCatchments As DotSpatial.Data.FeatureSet = DotSpatial.Data.FeatureSet.Open(aSimplifiedCatchmentFileName)

        'DumpFlowlinesCatchments(lSimplifiedFlowlines, lSimplifiedCatchments, aFields)

        Dim lBraidedFlowlinesCombinedCount As Integer = 0
        Dim lRemoveFlowlineWithoutCatchmentCount As Integer = 0
        Dim lFlowlinesRecord As Integer = 0
        Dim lOutletComIDs As Generic.List(Of Long) = D4EM.Geo.NetworkOperations.CheckConnectivity(lSimplifiedFlowlines, aFields)
        Dim lDontCombineComIDs As New Generic.List(Of Long)

        'DumpFlowlinesCatchments(lSimplifiedFlowlines, lSimplifiedCatchments, aFields)

        D4EM.Geo.NetworkOperations.RemoveBraidedFlowlines(aFlowlinesLayer, aCatchmentLayer, aFields, lOutletComIDs)

        'DumpFlowlinesCatchments(lSimplifiedFlowlines, lSimplifiedCatchments, aFields)

        SetBoundaryAndOutputFlags(aFlowlinesLayer, aCatchmentLayer, _
                                  lSimplifiedFlowlines, lDontCombineComIDs, _
                                  aFields, aBoundariesFilename, aOutputsFilename)

        'Logger.Status("Filling Missing Flowline CUMDRAINAG", True)
        'TODO: FillMissingFlowlineCUMDRAINAG(lSimplifiedFlowlines, lSimplifiedCatchment)

        Logger.Status("Enforcing Minimum Catchment Size", True)
        'Merge short lines and/or small catchments
        For Each lOutletComID As Long In lOutletComIDs
            D4EM.Geo.NetworkOperations.EnforceMinimumSize(lSimplifiedFlowlines, lSimplifiedCatchments,
                                                          aMinCatchmentKM2, aMinLengthKM, lOutletComID, False, aFields, lOutletComIDs, lDontCombineComIDs)
            Logger.Dbg("====AfterOutlet " & lOutletComID & " Flowlines " & lSimplifiedFlowlines.NumRows & " Catchments " & lSimplifiedCatchments.NumRows)
            DumpFlowlinesCatchments(lSimplifiedFlowlines, lSimplifiedCatchments, aFields)
        Next
        Logger.Dbg("======== EnforceMinimumSizeDone OutletCount " & lOutletComIDs.Count & " ShapeCount " & lSimplifiedFlowlines.NumRows)

        CombineMissingOutletCatchments(lSimplifiedFlowlines, lSimplifiedCatchments, aMinCatchmentKM2, aMinLengthKM, aFields, lOutletComIDs, lDontCombineComIDs)
        Logger.Dbg("Combined Missing Outlet Catchments OutletCount " & lOutletComIDs.Count & " ShapeCount " & lSimplifiedFlowlines.NumRows & " " & lSimplifiedCatchments.NumRows)

        lSimplifiedCatchments.Save()
        'TODO: Dim lSortedCatchmentsFilename As String = SortCatchmentsToMatchFlowlines(lSimplifiedFlowlines, lSimplifiedCatchment)
        lSimplifiedCatchments.Close()

        lSimplifiedFlowlines.Save()
        lSimplifiedFlowlines.Close()

        lOutletComIDs.Clear()

        'TODO:
        'If IO.File.Exists(lSortedCatchmentsFilename) Then
        '    atcUtility.TryMoveShapefile(lSortedCatchmentsFilename, lSimplifiedCatchmentFileName)
        'End If
        Return True
    End Function

    ''' <summary>
    ''' Set Boundary field in aFlowlinesLayer and aSimplifiedFlowlines for catchments containing points in aBoundariesFilename.
    ''' Set Output field in aFlowlinesLayer and aSimplifiedFlowlines for catchments containing points in aOutputsFilename and add their COMIDs to aDontCombineComIDs.
    ''' </summary>
    ''' <param name="aFlowlinesLayer">Flowlines Layer to set Boundary and Output fields in</param>
    ''' <param name="aCatchmentLayer">Catchments Layer to overlay with points to determine which flowline is associates with each point</param>
    ''' <param name="aSimplifiedFlowlines">Additional Flowlines layer to also set Boundary and Output fields in</param>
    ''' <param name="aDontCombineComIDs">List of ComIDs, add outputs to this list</param>
    ''' <param name="aFields">Field information for aFlowlinesLayer</param>
    ''' <param name="aBoundariesFilename">Lat/Long CSV file of upstream boundary locations</param>
    ''' <param name="aOutputsFilename">Lat/Long CSV file of desired model output locations such as gages</param>
    ''' <remarks></remarks>
    Private Sub SetBoundaryAndOutputFlags(ByVal aFlowlinesLayer As D4EM.Data.Layer, _
                                          ByVal aCatchmentLayer As D4EM.Data.Layer, _
                                          ByVal aSimplifiedFlowlines As DotSpatial.Data.FeatureSet, _
                                          ByVal aDontCombineComIDs As Generic.List(Of Long), _
                                          ByVal aFields As FieldIndexes, _
                                          ByVal aBoundariesFilename As String, _
                                          ByVal aOutputsFilename As String)
        Dim lBoundariesTable As atcTableDelimited = Nothing
        If FileExists(aBoundariesFilename) Then
            lBoundariesTable = New atcTableDelimited()
            lBoundariesTable.Delimiter = ","
            lBoundariesTable.OpenFile(aBoundariesFilename)
        End If

        Dim lOutputsTable As atcTableDelimited = Nothing
        If FileExists(aOutputsFilename) Then
            lOutputsTable = New atcTableDelimited()
            lOutputsTable.Delimiter = ","
            lOutputsTable.OpenFile(aOutputsFilename)
        End If

        Dim lOutputsLatitudeField As Integer = 1 ' lBoundariesOutputsDBF.FieldNumber("Latitude")
        Dim lOutputsLongitudeField As Integer = 2 ' lBoundariesOutputsDBF.FieldNumber("Longitude")

        Dim lModifiedFlowlinesLayer As Boolean = False
        Dim lModifiedSimplified As Boolean = False

        For Each lTable As atcTableDelimited In {lBoundariesTable, lOutputsTable}
            If lTable IsNot Nothing Then
                Dim lFieldName As String
                Dim lFlowlinesField As Integer
                Dim lSimplifiedFlowlinesField As Integer
                If Object.ReferenceEquals(lTable, lBoundariesTable) Then
                    lFieldName = "Boundary"
                Else
                    lFieldName = "Output"
                End If
                Dim lCurRecord As Integer = 1
                lFlowlinesField = FieldIndexes.EnsureFieldExists(aFlowlinesLayer.AsFeatureSet, lFieldName, GetType(String))
                lSimplifiedFlowlinesField = FieldIndexes.EnsureFieldExists(aSimplifiedFlowlines, lFieldName, GetType(String))
                With lTable
                    While Not .EOF
                        .CurrentRecord = lCurRecord
                        If IsNumeric(.Value(lOutputsLongitudeField)) AndAlso IsNumeric(.Value(lOutputsLatitudeField)) Then
                            Dim lX As Double = .Value(lOutputsLongitudeField)
                            Dim lY As Double = .Value(lOutputsLatitudeField)
                            D4EM.Geo.SpatialOperations.ProjectPoint(lX, lY, D4EM.Data.Globals.GeographicProjection, aFlowlinesLayer.Projection)
                            Dim lCatchmentIndex As Integer = aCatchmentLayer.CoordinatesInShapefile(lX, lY)
                            If lCatchmentIndex < 0 Then
                                Logger.Dbg(lFieldName & " point not in any catchment: lat=" & .Value(lOutputsLatitudeField) & ", long=" & .Value(lOutputsLongitudeField))
                            Else
                                Dim lComID As String = aCatchmentLayer.Feature(lCatchmentIndex).DataRow(aFields.CatchmentComId)
                                For Each lFlowline In aFlowlinesLayer.AsFeatureSet.Features
                                    If lFlowline.DataRow(aFields.FlowlinesComId) = lComID Then
                                        lFlowline.DataRow(lFlowlinesField) = "1"
                                        lModifiedFlowlinesLayer = True
                                        'If lFieldName = "Output" Then
                                        aDontCombineComIDs.Add(lComID)
                                        'End If
                                        Exit For
                                    End If
                                Next
                                'also loop through simplified flowlines, mark there as well
                                For Each lFlowline In aSimplifiedFlowlines.Features
                                    If lFlowline.DataRow(aFields.FlowlinesComId) = lComID Then
                                        lFlowline.DataRow(lSimplifiedFlowlinesField) = "1"
                                        lModifiedSimplified = True
                                        Exit For
                                    End If
                                Next
                            End If
                            lCurRecord += 1
                        End If
                    End While
                End With
            End If
        Next
        If lModifiedFlowlinesLayer Then aFlowlinesLayer.AsFeatureSet.Save()
        If lModifiedSimplified Then aSimplifiedFlowlines.Save()
    End Sub

    Friend Sub SetStatus(ByVal aStatus As String)
        Logger.Dbg(aStatus)
        'If aStatus.StartsWith("Exit") Then
        '    pThreadCount -= 1
        'End If
    End Sub

    ''' <summary>
    ''' Keep track of memory usage between calls to MemUsage
    ''' </summary>
    Private pLastPrivateMemory As Integer = 0
    Private pLastGcMemory As Integer = 0

    ''' <summary>
    ''' track memory usage for discovering memory leaks or processing limits
    ''' </summary>
    Friend Function MemUsage() As String
        System.GC.Collect()
        System.GC.WaitForPendingFinalizers()
        Dim lPrivateMemory As Integer = System.Diagnostics.Process.GetCurrentProcess.PrivateMemorySize64 / (2 ^ 20)
        Dim lGcMemory As Integer = System.GC.GetTotalMemory(True) / (2 ^ 20)
        MemUsage = "Megabytes: " & lPrivateMemory & " " & Format((lPrivateMemory - pLastPrivateMemory), "+0;-0") & " " _
                   & " GC: " & lGcMemory & " " & Format((lGcMemory - pLastGcMemory), "+0;-0")
        pLastPrivateMemory = lPrivateMemory
        pLastGcMemory = lGcMemory
    End Function

#Region "NASS Unit Tests"
    Private Sub UnitTestNASS_getStatistics_state_county()
        Dim lSavedFilename As String = D4EM.Data.Source.NASS.getStatistics("WISCONSIN", "BROWN", "2009", "S:\\BASINS\\cache\\NASS_Statistics\\NASS_WISCONSIN_BROWN_2009.txt")

        If Not IO.File.Exists(lSavedFilename) Then
            Throw New ApplicationException("Unit test failed for NASS.getStatistics state, county")
        End If
    End Sub

    Private Sub UnitTestNASS_getStatistics_Project()
        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc12) = "s:\basins\data\national\huc12\04030101\huc12.shp"

        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state) = "s:\basins\data\national\st.shp"

        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county) = "s:\basins\data\national\cnty.shp"

        Dim lRegion As New D4EM.Data.Region(D4EM.Data.Region.RegionTypes.huc12, "040301010605")

        Dim lProject As New D4EM.Data.Project(D4EM.Data.Globals.AlbersProjection, "S:\BASINS\cache", "S:\BASINS\data\040301010605-111\", lRegion, False, False)

        Dim lResult As String = D4EM.Data.Source.NASS.getStatistics(lProject, "Crop", "2009")

        If Not lResult.Contains("<add_data type='NASS::Statistics'>") Then
            Throw New ApplicationException("Unit test failed for NASS.getStatistics Project")
        End If
    End Sub

    Private Sub UnitTestNASS_getData_Folder()
        Dim lResult As String = D4EM.Data.Source.NASS.getData("S:\BASINS\data\040301010605-111\", "S:\BASINS\cache", "", "2009", 35, 34.8, -84, -84.2)

        If Not IO.File.Exists(lResult) Then
            Throw New ApplicationException("Unit test failed for NASS.getData Folder")
        End If
    End Sub

    Private Sub UnitTestNASS_getRaster_Project()
        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc12) = "s:\basins\data\national\huc12\04030101\huc12.shp"

        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state) = "s:\basins\data\national\st.shp"

        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county) = "s:\basins\data\national\cnty.shp"

        Dim lRegion As New D4EM.Data.Region(D4EM.Data.Region.RegionTypes.huc12, "040301010605")
        'lRegion = New D4EM.Data.Region(North, South, aWest, East, D4EM.Data.Globals.AlbersProjection)

        Dim lProject As New D4EM.Data.Project(D4EM.Data.Globals.AlbersProjection, "S:\BASINS\cache", "S:\BASINS\data\040301010605-111\", lRegion, False, False)

        Dim lResult As String = D4EM.Data.Source.NASS.getRaster(lProject, "Crop", "", "2009")

        If Not lResult.Contains("<add_data type='NASS::Statistics'>") Then
            Throw New ApplicationException("Unit test failed for NASS.getRaster Project")
        End If
    End Sub

#End Region

End Module
