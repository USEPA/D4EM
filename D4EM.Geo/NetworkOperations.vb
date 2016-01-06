Imports MapWinUtility

Public Class NetworkOperations
    ' ''' <summary>
    ' ''' Eliminate flowlines which do not have corresponding catchments
    ' ''' </summary>
    ' ''' <param name="aFlowlinesFileName">source flowlines shape file name</param>
    ' ''' <param name="aCatchmentFileName">source catchment shape file name</param>
    ' ''' <param name="aNewFlowlinesFilename">destination flowlines shape file name</param>
    ' ''' <param name="aNewCatchmentFilename">destination catchment shape file name</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function RemoveFlowlinesWithoutCatchment(ByVal aFlowlinesFileName As String, _
    '                                                       ByVal aCatchmentFileName As String, _
    '                                                       ByVal aNewFlowlinesFilename As String, _
    '                                                       ByVal aNewCatchmentFilename As String) As Boolean
    '    Dim lOutletComIds As New Generic.List(Of Long)

    '    'Dim lSimplifiedFlowlinesFileName As String = aFlowlinesFileName.Replace("flowline", "flowlineNoShort")
    '    'Dim lSimplifiedCatchmentFileName As String = aCatchmentFileName.Replace("catchment", "catchmentNoShort")

    '    Dim lSimplifiedFlowlines As DotSpatial.Data.FeatureSet = CopyAndOpenNewShapefile(aFlowlinesFileName, aNewFlowlinesFilename)
    '    If lSimplifiedFlowlines Is Nothing Then Return False
    '    Dim lSimplifiedCatchment As DotSpatial.Data.FeatureSet = CopyAndOpenNewShapefile(aCatchmentFileName, aNewCatchmentFilename)
    '    If lSimplifiedCatchment Is Nothing Then Return False

    '    Logger.Dbg("Process " & lSimplifiedFlowlines.Features.Count & " Flowlines " _
    '                                & lSimplifiedCatchment.Features.Count & " Catchments ")

    '    Dim lRemoveFlowlineWithoutCatchmentCount As Integer = 0
    '    Dim lFlowlinesRecord As Integer = 0

    '    Dim lFields As New FieldIndexes(lSimplifiedFlowlines, lSimplifiedCatchment)

    '    'Remove flowlines with no catchment
    '    While lFlowlinesRecord < lSimplifiedFlowlines.Features.Count
    '        Dim lComId As Long = -1
    '        UpdateValueIfNotNull(lComId, lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesComId))
    '        Dim lCatchmentRecord As Integer = FindRecord(lSimplifiedCatchment, lFields.CatchmentComId, lComId)
    '        If lCatchmentRecord = -1 Then 'missing catchment
    '            Logger.Dbg("RemoveFlowline " & DumpComid(lSimplifiedFlowlines, lComId, lFields) & " without catchment")
    '            If lOutletComIds.Contains(lComId) Then
    '                lOutletComIds.Remove(lComId)
    '            End If
    '            Dim lToNode As Int64 = -1
    '            UpdateValueIfNotNull(lToNode, lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesToNode))
    '            Dim lRecordCombineIndex As Integer = FindRecord(lSimplifiedFlowlines, lFields.FlowlinesFromNode, lToNode)
    '            If lToNode > 0 AndAlso lRecordCombineIndex > -1 Then
    '                Dim lToComId As Long = -1
    '                UpdateValueIfNotNull(lToComId, lSimplifiedFlowlines.Features.Item(lRecordCombineIndex).DataRow.Item(lFields.FlowlinesComId))
    '                Logger.Dbg("MergeDownWith " & DumpComid(lSimplifiedFlowlines, lToComId, lFields))
    '                CombineFlowlines(lSimplifiedFlowlines, lRecordCombineIndex, lFlowlinesRecord, True, False, lFields, lOutletComIds)
    '            Else
    '                Dim lFromNode As Int64 = -1
    '                UpdateValueIfNotNull(lFromNode, lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesFromNode))
    '                Dim lFromNodeCount As Integer = Count(lSimplifiedFlowlines, lFields.FlowlinesFromNode, lFromNode)
    '                lRecordCombineIndex = FindRecord(lSimplifiedFlowlines, lFields.FlowlinesToNode, lFromNode)
    '                If lFromNode > 0 AndAlso lRecordCombineIndex > -1 Then
    '                    Logger.Dbg("MergeUpWith " & DumpComid(lSimplifiedFlowlines, lSimplifiedFlowlines.Features.Item(lRecordCombineIndex).DataRow.Item(lFields.FlowlinesComId), lFields))
    '                    CombineFlowlines(lSimplifiedFlowlines, lRecordCombineIndex, lFlowlinesRecord, True, False, lFields, lOutletComIds)
    '                Else
    '                    Logger.Dbg("OrphanFlowLine - no connections")
    '                    lSimplifiedFlowlines.Features.RemoveAt(lFlowlinesRecord)
    '                    Logger.Dbg("Removed " & lComId & " at " & lFlowlinesRecord & " of " & lSimplifiedFlowlines.Features.Count)

    '                End If
    '            End If
    '        End If
    '        lFlowlinesRecord += 1
    '    End While
    '    Logger.Dbg("========RemovedFlowlinesWithoutCatchments " & lSimplifiedFlowlines.Features.Count & " " & lSimplifiedCatchment.Features.Count)
    '    Return True
    'End Function

    ' ''' <summary>
    ' ''' Eliminate flowlines which do not have corresponding catchments
    ' ''' </summary>
    ' ''' <param name="aFlowlinesLayer">stream flowlines</param>
    ' ''' <param name="aCatchmentLayer">catchments</param>
    ' ''' <remarks>removes flowlines that do not have a catchment with a matching key</remarks>
    'Public Shared Function RemoveFlowlinesWithoutCatchment(ByVal aFlowlinesLayer As D4EM.Data.Layer, _
    '                                                       ByVal aCatchmentLayer As D4EM.Data.Layer, _
    '                                              Optional ByVal aFields As FieldIndexes = Nothing, _
    '                                              Optional ByVal aOutletComIds As Generic.List(Of Long) = Nothing) As Boolean

    '    Dim lFlowlines As DotSpatial.Data.FeatureSet = aFlowlinesLayer.DataSet
    '    Dim lCatchments As DotSpatial.Data.FeatureSet = aCatchmentLayer.DataSet

    '    Logger.Dbg("Process " & lFlowlines.Features.Count & " Flowlines " _
    '                         & lCatchments.Features.Count & " Catchments ")

    '    If aFields Is Nothing Then aFields = New FieldIndexes(lFlowlines, lCatchments)
    '    If aOutletComIds Is Nothing Then aOutletComIds = New Generic.List(Of Long)

    '    Dim lRemoveFlowlineWithoutCatchmentCount As Integer = 0
    '    Dim lFlowlinesRecord As Integer = 0

    '    While lFlowlinesRecord < lFlowlines.Features.Count
    '        Dim lComId As Long = -1
    '        UpdateValueIfNotNull(lComId, lFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesComId))
    '        Dim lCatchmentRecord As Integer = FindRecord(lCatchments, aFields.CatchmentComId, lComId)
    '        If lCatchmentRecord = -1 Then 'missing catchment
    '            Logger.Dbg("RemoveFlowline " & DumpComid(lFlowlines, lComId, aFields) & " without catchment")
    '            If aOutletComIds.Contains(lComId) Then
    '                aOutletComIds.Remove(lComId)
    '            End If
    '            Dim lToNode As Int64 = -1
    '            UpdateValueIfNotNull(lToNode, lFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesToNode))
    '            Dim lRecordCombineIndex As Integer = FindRecord(lFlowlines, aFields.FlowlinesFromNode, lToNode)
    '            If lToNode > 0 AndAlso lRecordCombineIndex > -1 Then
    '                Dim lToComId As Long = -1
    '                UpdateValueIfNotNull(lToComId, lFlowlines.Features.Item(lRecordCombineIndex).DataRow.Item(aFields.FlowlinesComId))
    '                Logger.Dbg("MergeDownWith " & DumpComid(lFlowlines, lToComId, aFields))
    '                CombineFlowlines(lFlowlines, lRecordCombineIndex, lFlowlinesRecord, True, False, aFields, aOutletComIds)
    '            Else
    '                Dim lFromNode As Int64 = -1
    '                UpdateValueIfNotNull(lFromNode, lFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesFromNode))
    '                Dim lFromNodeCount As Integer = Count(lFlowlines, aFields.FlowlinesFromNode, lFromNode)
    '                lRecordCombineIndex = FindRecord(lFlowlines, aFields.FlowlinesToNode, lFromNode)
    '                If lFromNode > 0 AndAlso lRecordCombineIndex > -1 Then
    '                    Logger.Dbg("MergeUpWith " & DumpComid(lFlowlines, lFlowlines.Features.Item(lRecordCombineIndex).DataRow.Item(aFields.FlowlinesComId), aFields))
    '                    CombineFlowlines(lFlowlines, lRecordCombineIndex, lFlowlinesRecord, True, False, aFields, aOutletComIds)
    '                Else
    '                    Logger.Dbg("OrphanFlowLine - no connections")
    '                    lFlowlines.Features.RemoveAt(lFlowlinesRecord)
    '                    Logger.Dbg("Removed " & lComId & " at " & lFlowlinesRecord & " of " & lFlowlines.Features.Count)

    '                End If
    '            End If
    '        End If
    '        lFlowlinesRecord += 1
    '    End While
    '    Logger.Dbg("========RemovedFlowlinesWithoutCatchments " & lFlowlines.Features.Count & " " & lCatchments.Features.Count)
    '    Return True
    'End Function

    ''' <summary>
    ''' Save a new shapefile as aNewFlowlinesFilename containing the subset of flowlines from aFlowLines that have a corresponding catchment
    ''' </summary>
    ''' <param name="aCatchmentsFilename">File name of layer containing catchments that we want to keep the flowlines of</param>
    ''' <param name="aFlowLines">All flowlines we have to choose from</param>
    ''' <param name="aNewFlowlinesFilename">Save this new shapefile</param>
    ''' <returns>True on success, False on failure</returns>
    ''' <remarks>Only .dbf of catchments is opened for searching, not .shp</remarks>
    Public Shared Function ClipFlowLinesToCatchments(ByVal aCatchmentsFilename As String, _
                                                     ByVal aFlowLines As DotSpatial.Data.FeatureSet, _
                                                     ByVal aNewFlowlinesFilename As String, _
                                                     ByVal aFields As FieldIndexes) As Boolean
        Try
            Dim lFlowLinesDBF As New atcUtility.atcTableDBF
            lFlowLinesDBF.OpenFile(IO.Path.ChangeExtension(aFlowLines.Filename, "dbf"))
            'Field indexes +1 because atcDBF is 1-based, FieldIndexes is zero-based
            Dim lFlowLinesComIdField As Integer = aFields.FlowlinesComId + 1
            Dim lFlowLinesDownstreamField As Integer = aFields.FlowlinesDownstreamComId + 1

            Dim lCatchmentsDBF As New atcUtility.atcTableDBF
            lCatchmentsDBF.OpenFile(IO.Path.ChangeExtension(aCatchmentsFilename, "dbf"))
            Dim lCatchmentsComIdField As Integer = aFields.CatchmentComId + 1

            Dim lFlowLinesToUseDBF As atcUtility.atcTableDBF = lFlowLinesDBF.Cousin
            lFlowLinesToUseDBF.InitData()

            Dim lFlowLinesToUse As New DotSpatial.Data.FeatureSet(aFlowLines.FeatureType)

            Logger.Dbg("FlowlineCountInitial " & aFlowLines.Features.Count)

            Dim lReconnectIndex As Integer
            Dim lLastIndex As Integer = aFlowLines.Features.Count - 1
            For lFlowLinesShapeIndex As Integer = 0 To lLastIndex
                lFlowLinesDBF.CurrentRecord = lFlowLinesShapeIndex + 1
                Dim lComId As String = lFlowLinesDBF.Value(lFlowLinesComIdField)
                If IsNumeric(lComId) Then
                    If lCatchmentsDBF.FindFirst(lCatchmentsComIdField, lComId) Then
                        'Logger.Dbg("Found " & lComId & " addShape " & lFlowLinesToUse.Features.Count)
                        Dim lFlowLineShape As DotSpatial.Data.Feature = aFlowLines.Features.Item(lFlowLinesShapeIndex)
                        lFlowLinesToUse.AddFeature(lFlowLineShape)
                        lFlowLinesToUseDBF.CurrentRecord = lFlowLinesToUseDBF.NumRecords + 1
                        lFlowLinesToUseDBF.RawRecord = lFlowLinesDBF.RawRecord
                    Else
                        'Logger.Dbg("FlowLine " & lComId & " not added, does not have associated catchment")
                        Dim lDownstreamComID As String = lFlowLinesDBF.Value(lFlowLinesDownstreamField)
                        'Reconnect flowlines not yet reached in lFlowLinesDBF
                        For lReconnectIndex = lFlowLinesDBF.CurrentRecord + 1 To lLastIndex + 1
                            lFlowLinesDBF.CurrentRecord = lReconnectIndex
                            If lFlowLinesDBF.Value(lFlowLinesDownstreamField) = lComId Then
                                lFlowLinesDBF.Value(lFlowLinesDownstreamField) = lDownstreamComID
                            End If
                        Next
                        'Reconnect flowlines already added to lFlowLinesToUseDBF
                        For lReconnectIndex = 1 To lFlowLinesToUseDBF.NumRecords
                            lFlowLinesToUseDBF.CurrentRecord = lReconnectIndex
                            If lFlowLinesToUseDBF.Value(lFlowLinesDownstreamField) = lComId Then
                                lFlowLinesToUseDBF.Value(lFlowLinesDownstreamField) = lDownstreamComID
                            End If
                        Next
                    End If
                Else
                    Logger.Dbg("Error: Non-numeric COMID '" & lComId & "' at index " & lFlowLinesShapeIndex)
                End If
            Next
            Logger.Dbg("Writing '" & lFlowLinesToUse.Features.Count & "' flowlines, leaving out " & aFlowLines.Features.Count - lFlowLinesToUse.Features.Count)
            lFlowLinesToUse.Projection = aFlowLines.Projection
            lFlowLinesToUse.SaveAs(aNewFlowlinesFilename, True)
            lFlowLinesToUseDBF.WriteFile(IO.Path.ChangeExtension(aNewFlowlinesFilename, "dbf"))
            TryCopy(IO.Path.ChangeExtension(aFlowLines.Filename, "mwsr"), _
                    IO.Path.ChangeExtension(aNewFlowlinesFilename, "mwsr"))

            Return True
        Catch lEx As Exception
            Logger.Dbg("ClipFlowLinesToCatchments: " & lEx.ToString)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Eliminate secondary flowlines which are in the same catchment as another flowline
    ''' </summary>
    ''' <param name="aFlowlinesLayer">source flowlines shape layer</param>
    ''' <param name="aCatchmentLayer">source catchment shape layer</param>
    ''' <returns>True on success, False on failure</returns>
    ''' <remarks></remarks>
    Public Shared Function RemoveBraidedFlowlines(ByVal aFlowlinesLayer As D4EM.Data.Layer, _
                                                  ByVal aCatchmentLayer As D4EM.Data.Layer, _
                                                  ByVal aFields As FieldIndexes, _
                                                  ByVal aOutletComIDs As Generic.List(Of Long)) As Boolean
        Dim lSaveIntermediate As Integer = 500

        Dim lSimplifiedFlowlines As DotSpatial.Data.FeatureSet = aFlowlinesLayer.DataSet
        If lSimplifiedFlowlines Is Nothing Then Return False
        Dim lSimplifiedCatchment As DotSpatial.Data.FeatureSet = aCatchmentLayer.DataSet
        If lSimplifiedCatchment Is Nothing Then Return False

        Logger.Dbg("Process " & lSimplifiedFlowlines.Features.Count & " Flowlines " _
                              & lSimplifiedCatchment.Features.Count & " Catchments ")

        Dim lOldFlowlineCatchmentDelta As Integer
        Dim lBraidedFlowlinesCombinedCount As Integer = 0
        Dim lFlowlineCatchmentDelta As Integer = lSimplifiedFlowlines.Features.Count - lSimplifiedCatchment.Features.Count

        Dim lFlowlinesRecord As Integer = 0
        'Remove braided flowlines
        While lFlowlinesRecord < lSimplifiedFlowlines.Features.Count
            Dim lComId As Long = lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesComId)
            Dim lFromNode As Long = lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesFromNode)
            Dim lFromNodeCount As Integer = Count(lSimplifiedFlowlines, aFields.FlowlinesFromNode, lFromNode)
            If lFromNodeCount > 1 AndAlso lFromNode > 0 Then 'braided channel
                'Dim lDownstreamCount As Integer = Count(lSimplifiedFlowlines, lFlowlinesDownstreamComIdFieldIndex, lComId)
                Dim lDownstreamRecords As Generic.List(Of Integer) = FindRecords(lSimplifiedFlowlines, aFields.FlowlinesDownstreamComId, lComId)
                Dim lDownstreamCount As Integer = lDownstreamRecords.Count
                Logger.Dbg("BraidedChannel at " & lFlowlinesRecord & " of " & lSimplifiedFlowlines.Features.Count & " DownstreamCount " & lDownstreamCount)
                For Each lDownstreamRecord As Integer In lDownstreamRecords
                    Logger.Dbg("Downstream" & DumpComid(lSimplifiedFlowlines, lSimplifiedFlowlines.Features.Item(lDownstreamRecord).DataRow.Item(aFields.FlowlinesComId), aFields))
                Next
                If lDownstreamCount > 0 Then '
                    Logger.Dbg("PreferredChannel Keep " & lComId)
                Else ' not preferred(branch - remove)
                    Dim lCumAreaLarge As Double = 0
                    Dim lFindRecordKeep As Integer = -1
                    Dim lFindRecordRemove As Integer = -1
                    Dim lFindRecords As Generic.List(Of Integer) = FindRecords(lSimplifiedFlowlines, aFields.FlowlinesFromNode, lFromNode)
                    For Each lFindRecord As Integer In lFindRecords
                        Dim lCumArea As Double = -1
                        UpdateValueIfNotNull(lCumArea, lSimplifiedFlowlines.Features.Item(lFindRecord).DataRow.Item(aFields.FlowlinesCumDrainArea))
                        Logger.Dbg(" Found " & DumpComid(lSimplifiedFlowlines, lSimplifiedFlowlines.Features.Item(lFindRecord).DataRow.Item(aFields.FlowlinesComId), aFields))
                        If lCumAreaLarge < lCumArea Then
                            lCumAreaLarge = lCumArea
                            lFindRecordRemove = lFindRecordKeep
                            lFindRecordKeep = lFindRecord
                        Else
                            lFindRecordRemove = lFindRecord
                        End If
                    Next
                    Dim lCombineWithComId As Long = lSimplifiedFlowlines.Features.Item(lFindRecordKeep).DataRow.Item(aFields.FlowlinesComId)
                    Dim lRemoveComId As Long = lSimplifiedFlowlines.Features.Item(lFindRecordRemove).DataRow.Item(aFields.FlowlinesComId)
                    If CombineCatchments(lSimplifiedCatchment, lCombineWithComId, lRemoveComId, aFields) Then
                        CombineFlowlines(lSimplifiedFlowlines, lFindRecordKeep, lFindRecordRemove, False, False, aFields, aOutletComIDs)
                    Else
                        Logger.Dbg("Failed to merge braided catchment " & lRemoveComId & " into " & lCombineWithComId)
                    End If
                    lFlowlinesRecord -= 1
                    lBraidedFlowlinesCombinedCount += 1
                    lOldFlowlineCatchmentDelta = lFlowlineCatchmentDelta
                    lFlowlineCatchmentDelta = lSimplifiedFlowlines.Features.Count - lSimplifiedCatchment.Features.Count
                    Logger.Dbg("CountsFlowlines " & lSimplifiedFlowlines.Features.Count _
                            & " Catchments " & lSimplifiedCatchment.Features.Count _
                            & " Delta " & lFlowlineCatchmentDelta _
                            & " Removed " & lBraidedFlowlinesCombinedCount)
                End If
                If lFlowlineCatchmentDelta <> lOldFlowlineCatchmentDelta Then
                    Logger.Dbg("Error: ************* Delta Change - WHY ***********************")
                End If
                'SaveIntermediate(lSimplifiedCatchment, lSimplifiedFlowlines)
            End If
            lFlowlinesRecord += 1
        End While
        lSimplifiedFlowlines.Save()
        lSimplifiedCatchment.Save()
        Logger.Dbg("======== " & lBraidedFlowlinesCombinedCount & " Braided Flowlines Combined ========")
        Return True
    End Function

    ' ''' <summary>
    ' ''' Eliminate secondary flowlines which are in the same catchment as another flowline
    ' ''' </summary>
    ' ''' <param name="aFlowlinesFileName">source flowlines shape file name</param>
    ' ''' <param name="aCatchmentFileName">source catchment shape file name</param>
    ' ''' <param name="aNewFlowlinesFilename">destination flowlines shape file name</param>
    ' ''' <param name="aNewCatchmentFilename">destination catchment shape file name</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function RemoveBraidedFlowlines(ByVal aFlowlinesFileName As String, _
    '                                              ByVal aCatchmentFileName As String, _
    '                                              ByVal aNewFlowlinesFilename As String, _
    '                                              ByVal aNewCatchmentFilename As String) As Boolean

    '    Dim lSaveIntermediate As Integer = 500
    '    Dim lSimplifiedFlowlines As DotSpatial.Data.FeatureSet = CopyAndOpenNewShapefile(aFlowlinesFileName, aNewFlowlinesFilename)
    '    If lSimplifiedFlowlines Is Nothing Then Return False
    '    Dim lSimplifiedCatchment As DotSpatial.Data.FeatureSet = CopyAndOpenNewShapefile(aCatchmentFileName, aNewCatchmentFilename)
    '    If lSimplifiedCatchment Is Nothing Then Return False

    '    Logger.Dbg("Process " & lSimplifiedFlowlines.Features.Count & " Flowlines " _
    '                          & lSimplifiedCatchment.Features.Count & " Catchments ")

    '    Dim lFields As New FieldIndexes(lSimplifiedFlowlines, lSimplifiedCatchment)
    '    Dim lOutletComIds = CheckConnectivity(lSimplifiedFlowlines, lFields)

    '    Dim lOldFlowlineCatchmentDelta As Integer
    '    Dim lBraidedFlowlinesCombinedCount As Integer = 0
    '    Dim lFlowlineCatchmentDelta As Integer = lSimplifiedFlowlines.Features.Count - lSimplifiedCatchment.Features.Count

    '    Dim lFlowlinesRecord As Integer = 0
    '    'Remove braided flowlines
    '    While lFlowlinesRecord < lSimplifiedFlowlines.Features.Count
    '        Dim lComId As Long = lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesComId)
    '        Dim lFromNode As Long = lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesFromNode)
    '        Dim lFromNodeCount As Integer = Count(lSimplifiedFlowlines, lFields.FlowlinesFromNode, lFromNode)
    '        If lFromNodeCount > 1 AndAlso lFromNode > 0 Then 'braided channel
    '            'Dim lDownstreamCount As Integer = Count(lSimplifiedFlowlines, lFlowlinesDownstreamComIdFieldIndex, lComId)
    '            Dim lDownstreamRecords As Generic.List(Of Integer) = FindRecords(lSimplifiedFlowlines, lFields.FlowlinesDownstreamComId, lComId)
    '            Dim lDownstreamCount As Integer = lDownstreamRecords.Count
    '            Logger.Dbg("BraidedChannel at " & lFlowlinesRecord & " of " & lSimplifiedFlowlines.Features.Count & " DownstreamCount " & lDownstreamCount)
    '            For Each lDownstreamRecord As Integer In lDownstreamRecords
    '                Logger.Dbg("Downstream" & DumpComid(lSimplifiedFlowlines, lSimplifiedFlowlines.Features.Item(lFlowlinesRecord).DataRow.Item(lFields.FlowlinesComId), lFields))
    '            Next
    '            If lDownstreamCount > 0 Then '
    '                Logger.Dbg("PreferredChannel Keep " & lComId)
    '            Else ' not preferred(branch - remove)
    '                Dim lCumAreaLarge As Double = 0
    '                Dim lFindRecordKeep As Integer = -1
    '                Dim lFindRecordRemove As Integer = -1
    '                Dim lFindRecords As Generic.List(Of Integer) = FindRecords(lSimplifiedFlowlines, lFields.FlowlinesFromNode, lFromNode)
    '                For Each lFindRecord As Integer In lFindRecords
    '                    Dim lCumArea As Double = -1
    '                    UpdateValueIfNotNull(lCumArea, lSimplifiedFlowlines.Features.Item(lFindRecord).DataRow.Item(lFields.FlowlinesCumDrainArea))
    '                    Logger.Dbg(" Found " & DumpComid(lSimplifiedFlowlines, lSimplifiedFlowlines.Features.Item(lFindRecord).DataRow.Item(lFields.FlowlinesComId), lFields))
    '                    If lCumAreaLarge < lCumArea Then
    '                        lCumAreaLarge = lCumArea
    '                        lFindRecordRemove = lFindRecordKeep
    '                        lFindRecordKeep = lFindRecord
    '                    Else
    '                        lFindRecordRemove = lFindRecord
    '                    End If
    '                Next
    '                Dim lCombineWithComId As Long = lSimplifiedFlowlines.Features.Item(lFindRecordKeep).DataRow.Item(lFields.FlowlinesComId)
    '                Dim lRemoveComId As Long = lSimplifiedFlowlines.Features.Item(lFindRecordRemove).DataRow.Item(lFields.FlowlinesComId)
    '                If CombineCatchments(lSimplifiedCatchment, lCombineWithComId, lRemoveComId, lFields) Then
    '                    CombineFlowlines(lSimplifiedFlowlines, lFindRecordKeep, lFindRecordRemove, False, False, lFields, lOutletComIds)
    '                Else
    '                    Logger.Dbg("Failed to merge braided catchment " & lRemoveComId & " into " & lCombineWithComId)
    '                End If
    '                lFlowlinesRecord -= 1
    '                lBraidedFlowlinesCombinedCount += 1
    '                lOldFlowlineCatchmentDelta = lFlowlineCatchmentDelta
    '                lFlowlineCatchmentDelta = lSimplifiedFlowlines.Features.Count - lSimplifiedCatchment.Features.Count
    '                Logger.Dbg("CountsFlowlines " & lSimplifiedFlowlines.Features.Count _
    '                        & " Catchments " & lSimplifiedCatchment.Features.Count _
    '                        & " Delta " & lFlowlineCatchmentDelta _
    '                        & " Removed " & lBraidedFlowlinesCombinedCount)
    '            End If
    '            If lFlowlineCatchmentDelta <> lOldFlowlineCatchmentDelta Then
    '                Logger.Dbg("Error: ************* Delta Change - WHY ***********************")
    '            End If
    '            SaveIntermediate(lSimplifiedCatchment, lSimplifiedFlowlines)
    '        End If
    '        lFlowlinesRecord += 1
    '    End While
    '    Logger.Dbg("======== " & lBraidedFlowlinesCombinedCount & " Braided Flowlines Combined ========")
    '    Return True
    'End Function

    ' ''' <summary>
    ' ''' Eliminate flowlines which do not have corresponding catchments
    ' ''' </summary>
    ' ''' <param name="aLog">destination for log messages, Nothing to use MapWinUtility.Logger</param>
    ' ''' <param name="aFlowlinesFileName">source flowlines shape file name</param>
    ' ''' <param name="aCatchmentFileName">source catchment shape file name</param>
    ' ''' <param name="aNewFlowlinesFilename">destination flowlines shape file name</param>
    ' ''' <param name="aNewCatchmentFilename">destination catchment shape file name</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function MergeShortFlowlinesOrSmallCatchments(ByVal aLog As IO.StreamWriter, _
    '                                                            ByVal aFlowlinesFileName As String, _
    '                                                            ByVal aCatchmentFileName As String, _
    '                                                            ByVal aMinCatchmentKM2 As Double, _
    '                                                            ByVal aMinLengthKM As Double, _
    '                                                            ByVal aNewFlowlinesFilename As String, _
    '                                                            ByVal aNewCatchmentFilename As String) As Boolean

    'End Function


    ''' <summary>
    ''' Copy existing shape file to a new name and open the new one for editing
    ''' If new file already exists, it is removed first
    ''' </summary>
    ''' <param name="aOldFilename"></param>
    ''' <param name="aNewFilename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CopyAndOpenNewShapefile(ByVal aOldFilename As String, ByVal aNewFilename As String) As DotSpatial.Data.FeatureSet
        If aOldFilename.ToLower.Equals(aNewFilename.ToLower) Then
            Logger.Dbg("New shape file name was not different from old one, cannot copy '" & aNewFilename & "'")
            Return Nothing
        End If
        atcUtility.TryDeleteShapefile(aNewFilename)
        atcUtility.TryCopyShapefile(aOldFilename, aNewFilename)

        Dim lNewShapefile As DotSpatial.Data.FeatureSet
        lNewShapefile = DotSpatial.Data.FeatureSet.OpenFile(aNewFilename)
        If lNewShapefile Is Nothing Then
            Logger.Dbg("Could not open '" & aNewFilename & "'")
            Return Nothing
        Else
            Return lNewShapefile
        End If
    End Function

    ''' <summary>
    ''' Return record indexes of shapes with value in aFieldIndex matching aValue
    ''' </summary>
    ''' <param name="aShapeFile">shapes to search</param>
    ''' <param name="aFieldIndex">field index to check</param>
    ''' <param name="aValue">value of field to match</param>
    ''' <returns>list of Integer record indexes</returns>
    Public Shared Function FindRecords(ByVal aShapeFile As DotSpatial.Data.FeatureSet, ByVal aFieldIndex As Integer, ByVal aValue As Long) As Generic.List(Of Integer)
        Dim lRecordsMatching As New Generic.List(Of Integer)
        For lRecordIndex As Integer = 0 To aShapeFile.Features.Count - 1
            Try
                If CLng(aShapeFile.Features.Item(lRecordIndex).DataRow.Item(aFieldIndex)) = aValue Then 'this is the record we want
                    lRecordsMatching.Add(lRecordIndex)
                End If
            Catch 'Ignore non-numeric values
            End Try
        Next
        Return lRecordsMatching
    End Function

    'Find first matching record by field value as Long
    Public Shared Function FindRecord(ByVal aShapeFile As DotSpatial.Data.FeatureSet, ByVal aFieldIndex As Integer, ByVal aValue As Long) As Integer
        Dim lFieldValue As Long = 0 'Initial value does not matter, gets set in UpdateValueIfNotNull before being checked
        For lRecordIndex As Integer = 0 To aShapeFile.Features.Count - 1
            Try
                If UpdateValueIfNotNull(lFieldValue, aShapeFile.Features.Item(lRecordIndex).DataRow.Item(aFieldIndex)) AndAlso lFieldValue = aValue Then 'this is the record we want
                    Return lRecordIndex
                End If
            Catch 'Ignore non-numeric values
            End Try
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Combines NHDPlus flowline shapes and attributes
    ''' </summary>
    ''' <param name="aFlowlinesShapeFile">Flowline shape file</param>
    ''' <param name="aSourceBaseIndex">Record number of flowline to be kept</param>
    ''' <param name="aSourceDeletingIndex">Record number of flowline to be merged and deleted</param>
    ''' <param name="aMergeShapes">True to keep all flowline segments, False to keep only base segments</param>
    ''' <param name="aKeepCosmeticRemovedLine">True to keep a flowline in the layer even when it is removed from connectivity</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineFlowlines(ByRef aFlowlinesShapeFile As DotSpatial.Data.FeatureSet, _
                                            ByVal aSourceBaseIndex As Integer, _
                                            ByVal aSourceDeletingIndex As Integer, _
                                            ByVal aMergeShapes As Boolean, _
                                            ByVal aKeepCosmeticRemovedLine As Boolean, _
                                            ByVal aFields As FieldIndexes, _
                                            ByVal aOutletComIDs As Generic.List(Of Long)) As Boolean
        Try

            Dim lSourceBaseIndex As Integer = aSourceBaseIndex
            With aFlowlinesShapeFile
                Dim lKeepFeature As DotSpatial.Data.Feature = .Features.Item(aSourceBaseIndex)
                Dim lDeleteFeature As DotSpatial.Data.Feature = .Features.Item(aSourceDeletingIndex)
                Dim lDeletedComId As Long = lDeleteFeature.DataRow.Item(aFields.FlowlinesComId)
                Dim lBaseComId As Long = lKeepFeature.DataRow.Item(aFields.FlowlinesComId)
                Dim lBaseDownStreamComId As Long = lKeepFeature.DataRow.Item(aFields.FlowlinesDownstreamComId)
                Dim lDeletingDownstream As Boolean = False
                If lBaseDownStreamComId = lDeletedComId Then
                    Dim lNewDownstreamComId As Long = lDeleteFeature.DataRow.Item(aFields.FlowlinesDownstreamComId)
                    Logger.Dbg("ChangeDownFor " & lBaseComId & " From " & lDeletedComId & " to " & lNewDownstreamComId)
                    lKeepFeature.DataRow.Item(aFields.FlowlinesDownstreamComId) = lNewDownstreamComId
                    lDeletingDownstream = True
                End If

                Dim lMinElev As Double = 1.0E+30
                Dim lMaxElev As Double = -1.0E+30
                Dim lLength As Double = 0
                Dim lNewFieldValues As New ArrayList
                For lFieldIndex As Integer = 0 To .DataTable.Columns.Count - 1
                    Dim lFieldName As String = .DataTable.Columns(lFieldIndex).ColumnName.ToUpper
                    Try
                        Dim lValueFromBase As Object = lKeepFeature.DataRow.Item(lFieldIndex)
                        Dim lValueFromDeleting As Object = lDeleteFeature.DataRow.Item(lFieldIndex)

                        If IsDBNull(lValueFromDeleting) AndAlso IsDBNull(lValueFromBase) Then 'Both were DBNull, result is too
                            lNewFieldValues.Add(lValueFromBase)
                        Else
                            Select Case lFieldName
                                Case "COMID", "BOUNDARY", "OUTPUT"
                                    If IsDBNull(lValueFromBase) OrElse CStr(lValueFromBase).Length = 0 Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    Else
                                        lNewFieldValues.Add(lValueFromBase)
                                    End If
                                    'Case "FDATE"
                                    'Case "RESOLUTION"
                                Case "GNIS_ID", "GNIS_NAME"
                                    If IsDBNull(lValueFromBase) OrElse CStr(lValueFromBase).Length = 0 Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    Else
                                        lNewFieldValues.Add(lValueFromBase)
                                    End If
                                Case "LENGTHKM", "SHAPE_LENG", "PATHLENGTH"
                                    Dim lValue As Double = 0
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lValue = lValueFromDeleting
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lValue = lValueFromBase
                                    ElseIf aMergeShapes Then
                                        lValue = lValueFromBase + lValueFromDeleting
                                    Else
                                        lValue = lValueFromBase
                                    End If

                                    If lFieldName = "LENGTHKM" Then
                                        lLength = lValue
                                    End If

                                    lNewFieldValues.Add(lValue)

                                    'Case "FLOWDIR"
                                    'Case "FTYPE"
                                    'Case "FCODE"
                                    'Case "ENABLED"
                                Case "TOCOMID"
                                    If lDeletingDownstream Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    Else
                                        lNewFieldValues.Add(lValueFromBase)
                                    End If
                                    'Case "DIRECTION"
                                Case "TOCOMIDMEA"
                                    lNewFieldValues.Add(lValueFromBase)
                                Case "FROMNODE", "UPHYDROSEQ", "UPLEVELPAT"
                                    If lDeletingDownstream Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    End If
                                    'Case "TERMINALPA"
                                    'Case "ARBOLATESU"
                                    'Case "TERMINALFL"
                                    'Case "THINNERCOD"
                                    'Case "UPMINHYDRO"
                                    'Case "DNMINHYDRO"
                                Case "CUMDRAINAG", "CUMLENKM", _
                                     "CUMNLCD_11", "CUMNLCD_12", _
                                     "CUMNLCD_21", "CUMNLCD_22", "CUMNLCD_23", _
                                     "CUMNLCD_31", "CUMNLCD_32", "CUMNLCD_33", _
                                     "CUMNLCD_41", "CUMNLCD_42", "CUMNLCD_43", _
                                     "CUMNLCD_51", _
                                     "CUMNLCD_61", _
                                     "CUMNLCD_71", _
                                     "CUMNLCD_81", "CUMNLCD_82", "CUMNLCD_83", "CUMNLCD_84", "CUMNLCD_85", _
                                     "CUMNLCD_91", "CUMNLCD_92", _
                                     "CUMPCT_CN", "CUMPCT_MX", "CUMSUM_PCT", _
                                     "MAFLOWU", "MAFLOWV", "MAVELU", "MAVELV", _
                                     "AREAWTMAP", "AREAWTMAT", _
                                     "REACHCODE", "TONODE", "HYDROSEQ", "LEVELPATHI", "GRID_CODE", "DNLEVELPAT", "DNMINHYDRO", "DNDRAINCOU"
                                    If lDeletingDownstream Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    Else
                                        lNewFieldValues.Add(lValueFromBase)
                                    End If
                                    'If lFieldName = "CUMLENKM" Then Logger.Dbg("Merged CUMLENKM = " & lNewFieldValues(lNewFieldValues.Count - 1))
                                Case "INCRFLOWU", "LOCDRAINA"
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(lValueFromDeleting + lValueFromBase)
                                    End If
                                    'If lFieldName = "LOCDRAINA" Then Logger.Dbg("Merged LOCDRAINA = " & lNewFieldValues(lNewFieldValues.Count - 1) & " = " & lValueFromDeleting & " + " & lValueFromBase)
                                Case "MAXELEVRAW", "MAXELEVSMO"
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(Math.Max(lValueFromDeleting, lValueFromBase))
                                    End If
                                    lMaxElev = lNewFieldValues.Item(lNewFieldValues.Count - 1)
                                Case "STARTFLAG", "WBAREACOMI"
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(Math.Max(lValueFromDeleting, lValueFromBase))
                                    End If
                                Case "MINELEVRAW", "MINELEVSMO"
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(Math.Min(lValueFromDeleting, lValueFromBase))
                                    End If
                                    lMinElev = lNewFieldValues.Item(lNewFieldValues.Count - 1)
                                Case "STREAMLEVE", "STREAMORDE", "DIVERGENCE", "DELTALEVEL", "DNLEVEL"
                                    If IsDBNull(lValueFromBase) OrElse Not IsNumeric(lValueFromBase) Then
                                        lNewFieldValues.Add(lValueFromDeleting)
                                    ElseIf IsDBNull(lValueFromDeleting) OrElse Not IsNumeric(lValueFromDeleting) Then
                                        lNewFieldValues.Add(lValueFromBase)
                                    Else
                                        lNewFieldValues.Add(Math.Min(lValueFromDeleting, lValueFromBase))
                                    End If
                                Case "SLOPE"
                                    Dim lSlope As Double
                                    If lLength > 0 AndAlso lMaxElev > 0 AndAlso lMinElev < 1.0E+30 Then
                                        lSlope = (lMaxElev - lMinElev) / (lLength * 1000)
                                    Else
                                        lSlope = 0.001 'TODO: estimate this!
                                        Logger.Dbg("SlopeEstimatedAs " & lSlope & " MaxElev " & lMaxElev & " MinElev " & lMinElev & " Length " & lLength)
                                    End If
                                    lNewFieldValues.Add(lSlope)
                                Case Else
                                    'TODO: move case starting with CUMDRAINAG here? makes sense to keep the field of the one we are keeping?
                                    lNewFieldValues.Add(lValueFromBase)
                                    If Not IsDBNull(lValueFromBase) AndAlso Not IsDBNull(lValueFromDeleting) AndAlso lValueFromBase <> lValueFromDeleting Then
                                        Debug.Print("Case Else " & lFieldName & " = " & lValueFromBase & " not " & lValueFromDeleting)
                                    End If
                                    'Try
                                    '    Dim lValue1 As Object = .CellValue(lFieldIndex, aSourceBaseIndex)
                                    '    Dim lValue2 As Object = .CellValue(lFieldIndex, aSourceDeletingIndex)
                                    '    If (lValue1.ToString = lValue2.ToString) Then
                                    '        lNewFieldValues.Add(lValue1)
                                    '    ElseIf lValue1.ToString.Contains(lValue2.ToString) Then
                                    '        lNewFieldValues.Add(lValue1)
                                    '    Else
                                    '        lNewFieldValues.Add(lValue1 & ", " & lValue2)
                                    '    End If
                                    'Catch
                                    '    lNewFieldValues.Add("")
                                    'End Try
                            End Select
                        End If
                    Catch
                        Logger.Dbg("FailedToSetFieldValueFor " & lFieldIndex & " " & lFieldName.ToUpper)
                        lNewFieldValues.Add("")
                    End Try
                    'Logger.Dbg(lFieldName & ": " & aFlowlinesShapeFile.CellValue(lFieldIndex, aSourceDeletingIndex) & ", " & aFlowlinesShapeFile.CellValue(lFieldIndex, lSourceBaseIndex) & " -> " & lNewFieldValues(lNewFieldValues.Count - 1))
                Next
                If aMergeShapes Then
                    Dim lMergedFeature As DotSpatial.Data.Feature = DotSpatial.Data.FeatureExt.Union(lDeleteFeature, lKeepFeature)
                    Dim lSaveDataRow As DataRow = lKeepFeature.DataRow
                    Logger.Dbg("FlowlineMerge " & lSourceBaseIndex & " and " & aSourceDeletingIndex)
                    .Features(aSourceBaseIndex).Coordinates = lMergedFeature.Coordinates
                    '.Features.Remove(lKeepFeature)
                    '.Features.Add(lMergedFeature)
                    lKeepFeature = .Features(aSourceBaseIndex)
                Else
                    Logger.Dbg("FlowlineKeep " & lSourceBaseIndex & " Discard " & aSourceDeletingIndex)
                End If
                For lFieldIndex As Integer = 0 To .DataTable.Columns.Count - 1
                    Try
                        lKeepFeature.DataRow.Item(lFieldIndex) = lNewFieldValues(lFieldIndex)
                    Catch
                        Logger.Dbg("FailedToEditFieldValueFor " & lFieldIndex)
                    End Try
                Next
                .Features.Remove(lDeleteFeature)
                If Not aMergeShapes AndAlso aKeepCosmeticRemovedLine Then
                    Logger.Dbg("Moving cosmetic line to end of file")
                    .Features.Add(lDeleteFeature)
                    For lFieldIndex As Integer = 0 To .DataTable.Columns.Count - 1
                        Try
                            lDeleteFeature.DataRow.Item(lFieldIndex) = "0"
                        Catch
                        End Try
                    Next
                End If

                ReconnectUpstreamToDownstream(aFlowlinesShapeFile, lDeletedComId, lBaseComId, aFields)

                Logger.Dbg("Kept " & DumpComid(aFlowlinesShapeFile, lBaseComId, aFields))
            End With
            Return True
        Catch e As Exception
            Logger.Dbg("Error: CombineFlowlines: BaseIndex: " & aSourceBaseIndex & " DeletingIndex: " & aSourceDeletingIndex & " " & e.Message)
            Return False
        End Try
    End Function

    Private Shared Sub ReconnectUpstreamToDownstream(ByVal aFlowlinesShapeFile As DotSpatial.Data.FeatureSet, _
                                                     ByVal aDeletedComId As Long, _
                                                     ByVal aDownstreamComId As Long, _
                                                     ByVal aFields As FieldIndexes)
        With aFlowlinesShapeFile
            Dim lFlowlinesRecord As Integer = 0
            Dim lMaybeNullComId As Object
            While lFlowlinesRecord < .Features.Count
                Try
                    lMaybeNullComId = .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesDownstreamComId)
                    If Not IsDBNull(lMaybeNullComId) Then
                        Dim lDownstreamComId As Long = lMaybeNullComId
                        If aDeletedComId = lDownstreamComId Then
                            lMaybeNullComId = .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesComId)
                            If Not IsDBNull(lMaybeNullComId) Then
                                Dim lComId As Long = lMaybeNullComId
                                Logger.Dbg("ChangeDownFor " & lComId & " From " & aDeletedComId & " to " & aDownstreamComId & " at " & lFlowlinesRecord)
                                .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesDownstreamComId) = aDownstreamComId
                            End If
                        End If
                    End If
                Catch lEx As Exception
                    Logger.Dbg("ChangeFailedAt " & lFlowlinesRecord)
                End Try
                lFlowlinesRecord += 1
            End While
        End With
    End Sub

    Public Shared Function DumpComid(ByVal aFlowLinesShapeFile As DotSpatial.Data.FeatureSet, ByVal aComId As Long, ByVal aFields As FieldIndexes) As String
        Dim lRecordIndex As Integer = FindRecord(aFlowLinesShapeFile, aFields.FlowlinesComId, aComId)
        If lRecordIndex < 0 Then
            Return aComId & " not found******************"
        Else
            Dim lValues() As Object = aFlowLinesShapeFile.Features.Item(lRecordIndex).DataRow.ItemArray
            Dim lNumValues As Integer = lValues.Count
            With aFlowLinesShapeFile
                Dim lToNode As Int64 = -1
                Dim lFromNode As Int64 = -1
                Dim lLength As Double = -1
                Dim lCumLength As Double = -1
                Dim lCumArea As Double = -1
                Dim lLocArea As Double = -1
                Dim lToComId As Long = 0
                If aFields.FlowlinesToNode < 0 Then lToNode = -2 Else If aFields.FlowlinesToNode < lNumValues Then UpdateValueIfNotNull(lToNode, lValues(aFields.FlowlinesToNode))
                If aFields.FlowlinesFromNode < 0 Then lFromNode = -2 Else If aFields.FlowlinesFromNode < lNumValues Then UpdateValueIfNotNull(lFromNode, lValues(aFields.FlowlinesFromNode))
                If aFields.FlowlinesLength < 0 Then lLength = -2 Else If aFields.FlowlinesLength < lNumValues Then UpdateValueIfNotNull(lLength, lValues(aFields.FlowlinesLength))
                If aFields.FlowlinesCumLen < 0 Then lCumLength = -2 Else If aFields.FlowlinesCumLen < lNumValues Then UpdateValueIfNotNull(lCumLength, lValues(aFields.FlowlinesCumLen))
                If aFields.FlowlinesLocDrainArea < 0 Then lLocArea = -2 Else If aFields.FlowlinesLocDrainArea < lNumValues Then UpdateValueIfNotNull(lLocArea, lValues(aFields.FlowlinesLocDrainArea))
                If aFields.FlowlinesCumDrainArea < 0 Then lCumArea = -2 Else If aFields.FlowlinesCumDrainArea < lNumValues Then UpdateValueIfNotNull(lCumArea, lValues(aFields.FlowlinesCumDrainArea))
                If aFields.FlowlinesDownstreamComId < 0 Then lToComId = -2 Else If aFields.FlowlinesDownstreamComId < lNumValues Then UpdateValueIfNotNull(lToComId, lValues(aFields.FlowlinesDownstreamComId))
                Return aComId & "(" & lRecordIndex & "-u" & lFromNode & "-d" & lToNode & "-Len" & lLength & "-LenCum" & lCumLength & "-Area" & lLocArea & "-AreaCum" & lCumArea & "-t" & lToComId & ")"
            End With
        End If
    End Function

    Public Shared Function UpdateValueIfNotNull(ByRef aCurrentValue As Long, ByRef aNewObject As Object) As Boolean
        Try
            If aNewObject IsNot System.DBNull.Value Then
                aCurrentValue = aNewObject
                Return True
            End If
        Catch
        End Try
        Return False
    End Function

    Private Shared Function UpdateValueIfNotNull(ByRef aCurrentValue As Double, ByRef aNewObject As Object) As Boolean
        Try
            If aNewObject IsNot System.DBNull.Value Then
                aCurrentValue = aNewObject
                Return True
            End If
        Catch
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Return keys of outlets from the network
    ''' </summary>
    ''' <param name="aFlowlinesShapeFile">Flowlines of stream network</param>
    ''' <param name="aFields">Field Indexes</param>
    Public Shared Function CheckConnectivity(ByVal aFlowlinesShapeFile As DotSpatial.Data.FeatureSet, _
                                             ByVal aFields As FieldIndexes) As Generic.List(Of Long)
        Logger.Dbg("*** CheckConnectivity Begin")
        Dim lOutletComIDs As New Generic.List(Of Long)
        Dim lRemoveCount As Integer = 0
        Dim lMissingCount As Integer = 0
        Dim lLargestContribAreaComId As Long = 0
        Dim lLargestContribArea As Double = 0.0
        With aFlowlinesShapeFile
            Dim lFlowlinesRecord As Integer = .Features.Count - 1
            Dim lSkipThisRecord As Boolean = False
            While lFlowlinesRecord >= 0
                Dim lComId As Long = -1
                UpdateValueIfNotNull(lComId, .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesComId))
                Dim lContribArea As Double = -1
                If UpdateValueIfNotNull(lContribArea, .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesCumDrainArea)) AndAlso lLargestContribArea < lContribArea Then
                    lLargestContribArea = lContribArea
                    lLargestContribAreaComId = lComId
                    Logger.Dbg("NewLargestContribArea " & DumpComid(aFlowlinesShapeFile, lComId, aFields))
                End If
                Dim lDownstreamComId As Long = -1

                If UpdateValueIfNotNull(lDownstreamComId, .Features.Item(lFlowlinesRecord).DataRow.Item(aFields.FlowlinesDownstreamComId)) AndAlso lDownstreamComId > -1 Then
                    If lDownstreamComId = 0 Then
                        Logger.Dbg("ZeroDownstreamComID, add outlet " & DumpComid(aFlowlinesShapeFile, lComId, aFields))
                        lMissingCount += 1
                        If Not lOutletComIDs.Contains(lComId) Then
                            lOutletComIDs.Add(lComId)
                        End If
                    ElseIf FindRecord(aFlowlinesShapeFile, aFields.FlowlinesComId, lDownstreamComId) < 0 Then
                        Logger.Dbg("MissingDownstreamComID " & lDownstreamComId & ", add outlet " & DumpComid(aFlowlinesShapeFile, lComId, aFields))
                        lMissingCount += 1
                        If Not lOutletComIDs.Contains(lComId) Then
                            lOutletComIDs.Add(lComId)
                        End If
                    End If
                ElseIf lSkipThisRecord = False Then
                    Logger.Dbg("FlowLineRecordNotNumericDown, add outlet " & DumpComid(aFlowlinesShapeFile, lComId, aFields))
                    lMissingCount += 1
                    If Not lOutletComIDs.Contains(lComId) Then
                        lOutletComIDs.Add(lComId)
                    End If
                End If
                lFlowlinesRecord -= 1
            End While
        End With
        Logger.Dbg(" ** CheckConnectivityEnd, MissingOutletsAdded " & lMissingCount)
        If Not lOutletComIDs.Contains(lLargestContribAreaComId) Then
            Logger.Dbg(" ** AddMainChannelToOutletComIds")
            lOutletComIDs.Add(lLargestContribAreaComId)
        End If

        Return lOutletComIDs
    End Function

    Public Shared Function CombineCatchments(ByRef aCatchmentShapeFile As DotSpatial.Data.FeatureSet, _
                                             ByVal aKeptComId As Long, _
                                             ByVal aAddingComId As Long, _
                                             ByVal aFields As FieldIndexes) As Boolean
        Try
            Dim lRecordKeptIndex As Integer = FindRecord(aCatchmentShapeFile, aFields.CatchmentComId, aKeptComId)
            Dim lRecordAddingIndex As Integer = FindRecord(aCatchmentShapeFile, aFields.CatchmentComId, aAddingComId)
            Logger.Dbg("CatchmentKeep " & aKeptComId & "(" & lRecordKeptIndex & ") Merge " & aAddingComId & "(" & lRecordAddingIndex & ")")
            If lRecordAddingIndex > -1 AndAlso lRecordKeptIndex > -1 Then
                With aCatchmentShapeFile
                    Dim lKept As DotSpatial.Data.Feature = .Features.Item(lRecordKeptIndex)
                    Dim lAdding As DotSpatial.Data.Feature = .Features.Item(lRecordAddingIndex)
                    Dim lNewFieldValues As New ArrayList
                    Dim lAreaBase As Double = 0
                    Dim lAreaAdding As Double = 0
                    Dim lAreaTotal As Double = 0
                    Dim lAreaPercentTotal As Double = 0.0
                    For lFieldIndex As Integer = 0 To .DataTable.Columns.Count - 1
                        'Debug.Print("Case """ & .Field(lFieldIndex).Name.ToUpper() & """")
                        Try
                            Dim lValueFromKept As Object = lKept.DataRow.Item(lFieldIndex)
                            Dim lValueFromAdding As Object = lAdding.DataRow.Item(lFieldIndex)

                            If IsDBNull(lValueFromAdding) AndAlso IsDBNull(lValueFromKept) Then 'Both were DBNull, result is too
                                lNewFieldValues.Add(lValueFromKept)
                            Else
                                Select Case .DataTable.Columns.Item(lFieldIndex).ColumnName.ToUpper
                                    Case "COMID"
                                        If IsDBNull(lValueFromKept) Then
                                            lNewFieldValues.Add(lValueFromAdding)
                                        Else
                                            lNewFieldValues.Add(lValueFromKept)
                                        End If
                                        'If lValueFromKept.ToString = "12174002" Then
                                        '    Logger.Dbg("Combining 12174002")
                                        'End If
                                    Case "GRID_CODE" 'TODO: check this
                                        If IsDBNull(lValueFromKept) Then
                                            lNewFieldValues.Add(lValueFromAdding)
                                        Else
                                            lNewFieldValues.Add(lValueFromKept)
                                        End If
                                    Case "GRID_COUNT"
                                        If IsDBNull(lValueFromAdding) Then
                                            lNewFieldValues.Add(lValueFromKept)
                                        ElseIf IsDBNull(lValueFromKept) Then
                                            lNewFieldValues.Add(lValueFromAdding)
                                        Else
                                            lNewFieldValues.Add(lValueFromKept + lValueFromAdding)
                                        End If
                                    Case "PROD_UNIT"
                                        If IsDBNull(lValueFromKept) Then
                                            lNewFieldValues.Add(lValueFromAdding)
                                        Else
                                            lNewFieldValues.Add(lValueFromKept)
                                        End If
                                    Case "AREASQKM"
                                        If IsDBNull(lValueFromAdding) Then
                                            lAreaAdding = 0
                                        Else
                                            Try
                                                lAreaAdding = lValueFromAdding
                                            Catch
                                                lAreaAdding = 0
                                            End Try
                                        End If

                                        If IsDBNull(lValueFromKept) Then
                                            lAreaBase = 0
                                        Else
                                            Try
                                                lAreaBase = lValueFromKept
                                            Catch
                                                lAreaBase = 0
                                            End Try
                                        End If

                                        lAreaTotal = lAreaBase + lAreaAdding
                                        lNewFieldValues.Add(lAreaTotal)

                                    Case "NLCD_11", "NLCD_12", _
                                         "NLCD_21", "NLCD_22", "NLCD_23", _
                                         "NLCD_31", "NLCD_32", "NLCD_33", _
                                         "NLCD_41", "NLCD_42", "NLCD_43", _
                                         "NLCD_51", "NLCD_61", "NLCD_71", _
                                         "NLCD_81", "NLCD_82", "NLCD_83", "NLCD_84", "NLCD_85", _
                                         "NLCD_91", "NLCD_92", "PCT_CN", "PCT_MX"
                                        Dim lNewValue As Double = 0
                                        If IsDBNull(lValueFromAdding) Then
                                            lNewValue = lValueFromKept
                                        ElseIf IsDBNull(lValueFromKept) Then
                                            lNewValue = lValueFromAdding
                                        ElseIf lAreaTotal > 0 Then
                                            lNewValue = ((lValueFromKept * lAreaBase) + _
                                                         (lValueFromAdding * lAreaAdding)) / lAreaTotal
                                        Else
                                            lNewFieldValues.Add(0)
                                        End If
                                        lAreaPercentTotal += lNewValue
                                        lNewFieldValues.Add(lNewValue)
                                    Case "SUM_PCT"
                                        lNewFieldValues.Add(lAreaPercentTotal)
                                    Case "PRECIP", "TEMP"
                                        If IsDBNull(lValueFromAdding) Then
                                            lNewFieldValues.Add(lValueFromKept)
                                        ElseIf IsDBNull(lValueFromKept) Then
                                            lNewFieldValues.Add(lValueFromAdding)
                                        ElseIf lAreaTotal > 0 Then
                                            Dim lNewValue As Double = (lValueFromKept * lAreaBase) + _
                                                                      (lValueFromAdding * lAreaAdding) / lAreaTotal
                                            lNewFieldValues.Add(lNewValue)
                                        Else
                                            lNewFieldValues.Add(0)
                                        End If
                                    Case "OUTPUT", "BOUNDARY"
                                        If IsDBNull(lValueFromAdding) OrElse lValueFromAdding.ToString.Length = 0 Then
                                            lNewFieldValues.Add(lValueFromKept)
                                        Else
                                            lNewFieldValues.Add(lValueFromAdding)
                                        End If
                                    Case Else
                                        lNewFieldValues.Add("")
                                End Select
                            End If
                        Catch exField As Exception
                            Logger.Dbg("Exception setting " & .DataTable.Columns.Item(lFieldIndex).ColumnName & ": " & exField.Message)
                            lNewFieldValues.Add("")
                        End Try
                    Next

                    Dim lTargetShape As DotSpatial.Data.IFeature = Nothing
                    Try
                        lTargetShape = DotSpatial.Data.FeatureExt.Union(lKept, lAdding)
                    Catch e As Exception
                        Logger.Dbg("Error: CombineCatchments:ClipTargetShape " & e.Message)
                        Try
                            lTargetShape = DotSpatial.Data.FeatureExt.Union(lKept, lAdding)
                        Catch ex As Exception
                            Logger.Dbg("Error: CombineCatchments:MergeShapes " & ex.Message)
                        End Try
                    End Try
                    If lTargetShape IsNot Nothing Then
                        .Features.Remove(lAdding)
                        .Features.Remove(lKept)
                        .Features.Add(lTargetShape)
                    Else
                        Logger.Dbg("Shape union failed, discarding shape " & aAddingComId & " todo: merge by adding parts")
                        .Features.Remove(lAdding)
                        lTargetShape = lKept
                        'Logger.Dbg("Shape Union Failed, merging by adding parts ")
                        'Throw New ApplicationException("Shape Union Failed " & aCatchmentShapeFile.Shape(lRecordKeptIndex).LastErrorCode & " " & aCatchmentShapeFile.LastErrorCode)
                        'Dim lToPointIndex As Integer = aCatchmentShapeFile.Features.Item(lRecordKeptIndex).NumPoints
                        'For lFromPartIndex As Integer = 0 To aCatchmentShapeFile.Features.Item(lRecordAddingIndex). .NumParts - 1
                        '    Dim lLastFromPointIndex As Integer = aCatchmentShapeFile.Shape(lRecordAddingIndex).numPoints - 1
                        '    If lFromPartIndex + 1 < aCatchmentShapeFile.Shape(lRecordAddingIndex).NumParts Then
                        '        lLastFromPointIndex = aCatchmentShapeFile.Shape(lRecordAddingIndex).Part(lFromPartIndex + 1) - 1
                        '    End If
                        '    If lLastFromPointIndex >= lFromPartIndex Then
                        '        aCatchmentShapeFile.Shape(lRecordKeptIndex).InsertPart(lToPointIndex, aCatchmentShapeFile.Shape(lRecordKeptIndex).NumParts)
                        '        For lFromPointIndex As Integer = aCatchmentShapeFile.Shape(lRecordAddingIndex).Part(lFromPartIndex) To lLastFromPointIndex
                        '            aCatchmentShapeFile.Shape(lRecordKeptIndex).InsertPoint(aCatchmentShapeFile.Shape(lRecordAddingIndex).Point(lFromPointIndex), lToPointIndex)
                        '            lToPointIndex += 1
                        '        Next
                        '    End If
                        'Next
                    End If
                    For lFieldIndex As Integer = 0 To .DataTable.Columns.Count - 1
                        lTargetShape.DataRow.Item(lFieldIndex) = lNewFieldValues(lFieldIndex)
                    Next
                End With
            Else
                Logger.Dbg("SkipMerge " & lRecordKeptIndex & " " & lRecordAddingIndex)
            End If
            Return True
        Catch e As Exception
            Logger.Dbg("Error: CombineCatchments:  " & e.Message)
            Return False
        End Try
    End Function

    Public Shared Sub CombineMissingOutletCatchments(ByVal aFlowlines As DotSpatial.Data.FeatureSet, _
                                                     ByVal aCatchments As DotSpatial.Data.FeatureSet, _
                                                     ByVal aMinCatchmentKM2 As Double, _
                                                     ByVal aMinLengthKM As Double, _
                                                     ByVal aFields As FieldIndexes, _
                                                     ByVal aOutletComIDs As Generic.List(Of Long), _
                                                     ByVal aDontCombineComIDs As Generic.List(Of Long))
        Logger.Dbg("CombineMissingOutletCatchments Count " & aOutletComIDs.Count)
        Dim lCheckArea As Boolean = (aMinCatchmentKM2 > 0)
        Dim lCheckLength As Boolean = (aMinLengthKM > 0)
        Dim lMergedThese As New Generic.List(Of Long)
        Dim lOutletComID As Long
        For Each lOutletComID In aOutletComIDs
            Logger.Dbg("Process " & lOutletComID)
            Dim lFlowlineIndex As Integer = FindRecord(aFlowlines, aFields.FlowlinesComId, lOutletComID)
            'Dim lOutletComIdRecordIndex As Integer = FindRecord(aFlowlines, pFlowlinesComIdFieldIndex, lOutletComID)
            If lFlowlineIndex > -1 And aFlowlines.FeatureLookup.Count > 1 Then
                If IsTooSmall(aFlowlines, lFlowlineIndex, aMinCatchmentKM2, aMinLengthKM, lCheckArea, lCheckLength, False, aFields) Then
                    'Dim lOutletCumArea As Double = aFlowlines.CellValue(pFlowlinesCumDrainAreaIndex, lOutletComIdRecordIndex)
                    'If lOutletCumArea < aMinCatchmentKM2 Then 'TODO: is this an appropriate check?
                    'whats most likely downstream - assume flowline is upstream to downstream
                    Dim lFlowLineNumPoints As Integer = aFlowlines.Features(lFlowlineIndex).NumPoints
                    Dim lFlowlinePoint = aFlowlines.Features(lFlowlineIndex).Coordinates(lFlowLineNumPoints - 1)
                    Dim lNearestIndex As Integer = NearestNeighbor(lFlowlinePoint, aCatchments, aFields.CatchmentComId, lOutletComID)
                    If lNearestIndex < 0 Then
                        Logger.Dbg("No nearest neighbor found to merge at end of flowline: " & lFlowlinePoint.ToString)
                    Else
                        Dim lMergeWithComid As Long = aCatchments.Features(lNearestIndex).DataRow(aFields.CatchmentComId)
                        Logger.Dbg("MergeWith " & DumpComid(aFlowlines, lMergeWithComid, aFields))
                        Dim lMainFlowLineIndex As Integer = FindRecord(aFlowlines, aFields.FlowlinesComId, lMergeWithComid)
                        'dont do this if lmergewithcomid is a dontcombine
                        If Not aDontCombineComIDs.Contains(lMergeWithComid) Then
                            If CombineCatchments(aCatchments, lMergeWithComid, lOutletComID, aFields) Then
                                If lMainFlowLineIndex < 0 Then
                                    Logger.Dbg("Combined catchments, but did not find flowline index for " & aFields.FlowlinesComId)
                                Else
                                    CombineFlowlines(aFlowlines, lMainFlowLineIndex, lFlowlineIndex, False, False, aFields, aOutletComIDs)
                                End If
                                lMergedThese.Add(lOutletComID)
                            Else
                                Logger.Dbg("Failed to merge missing outlet catchment " & lOutletComID & " into " & lMergeWithComid, True)
                            End If
                        End If
                    End If
                Else
                    'LogMessage(aLog, "Big Enough " & lOutletComID & " Area " & lOutletCumArea)
                End If
            Else
                Logger.Dbg("Missing OutletComID " & lOutletComID)
            End If
        Next
        For Each lOutletComID In lMergedThese
            aOutletComIDs.Remove(lOutletComID)
        Next
        If lMergedThese.Count > 0 Then
            aFlowlines.Save()
            aCatchments.Save()
        End If
        Logger.Status("CombineMissingOutletCatchments Complete: " & aOutletComIDs.Count & " outlets remain", True)
    End Sub

    ''' <summary>
    ''' Given a point and a FeatureSet, find index of feature whose centroid is closest to this point
    ''' </summary>
    ''' <param name="aPoint"></param>
    ''' <param name="aPolygons"></param>
    ''' <returns>zero-based index of closest feature, or -1 if no feature found</returns>
    Private Shared Function NearestNeighbor(ByVal aPoint As DotSpatial.Topology.Coordinate, _
                                            ByVal aPolygons As DotSpatial.Data.FeatureSet, _
                                            ByVal aKeyColumn As Integer, _
                                            ByVal aSkipValue As String) As Integer
        Dim lNearestNeighbor As Integer = -1
        Select Case aPolygons.Features.Count
            Case 0
                lNearestNeighbor = -1
            Case 1 'With only one shape, must be that one unless it is the one we need to skip
                lNearestNeighbor = 0
                If aKeyColumn >= 0 Then
                    For Each lFeature As DotSpatial.Data.Feature In aPolygons.Features
                        If lFeature.DataRow(aKeyColumn).Value = aSkipValue Then lNearestNeighbor = -1
                    Next
                End If

            Case Else
                Dim lFeatureIndex As Integer = -1
                Dim lNearestDistance As Double = 1.0E+30
                Dim lDistances(aPolygons.Features.Count) As Double
                For Each lFeature As DotSpatial.Data.Feature In aPolygons.Features
                    lFeatureIndex += 1

                    If aKeyColumn >= 0 Then
                        If lFeature.DataRow(aKeyColumn).ToString() = aSkipValue Then Continue For
                    End If

                    Dim lCentroid As DotSpatial.Topology.Point = lFeature.ToShape.ToGeometry.Centroid
                    Dim lDistance As Double = lCentroid.Distance(aPoint)
                    If lDistance < lNearestDistance Then
                        lNearestDistance = lDistance
                        lNearestNeighbor = lFeatureIndex
                    End If
                Next
        End Select
        Return lNearestNeighbor
    End Function

    ''' <summary>
    ''' Compute how many features have the value aFieldValue in the field aFieldIndex
    ''' </summary>
    Public Shared Function Count(ByVal aShapeFile As DotSpatial.Data.FeatureSet, _
                                  ByVal aFieldIndex As Integer, _
                                  ByVal aFieldValue As Object) As Integer
        Dim lCount As Integer = 0
        For lRecordIndex As Integer = 0 To aShapeFile.Features.Count - 1
            Dim lCellValue As Object = aShapeFile.Features.Item(lRecordIndex).DataRow.Item(aFieldIndex)
            If lCellValue.ToString.Length > 0 AndAlso lCellValue = aFieldValue Then
                lCount += 1
            End If
        Next
        Return lCount
    End Function

    Public Shared Sub DumpFlowlinesCatchments(ByVal aFlowlines As DotSpatial.Data.FeatureSet, _
                                              ByVal aCatchments As DotSpatial.Data.FeatureSet, _
                                              ByVal aFields As FieldIndexes)
        Logger.Dbg("Flowlines:" & aFlowlines.Features.Count & vbTab & "Catchments:" & aCatchments.Features.Count)
        For lFeatureIndex As Integer = 0 To aFlowlines.Features.Count - 1
            With aFlowlines.Features(lFeatureIndex)
                Logger.Dbg("Line " & .DataRow(aFields.FlowlinesComId) & vbTab _
                         & "Len=" & .DataRow(aFields.FlowlinesLength) & vbTab _
                         & "CLen=" & .DataRow(aFields.FlowlinesCumLen) & vbTab _
                         & "LArea=" & .DataRow(aFields.FlowlinesLocDrainArea) & vbTab _
                         & "CArea=" & .DataRow(aFields.FlowlinesCumDrainArea))
            End With
        Next

        For lFeatureIndex As Integer = 0 To aCatchments.Features.Count - 1
            Logger.Dbg("Catchment " & aCatchments.Features(lFeatureIndex).DataRow(aFields.CatchmentComId))
        Next
    End Sub

    ''' <summary>
    ''' Combine flowlines and catchments upstream of given outlet if catchment smaller than aMinCatchmentKM2 or flowline shorter than aMinLengthKM
    ''' </summary>
    ''' <param name="aFlowlines">flowlines shapefile</param>
    ''' <param name="aCatchments">catchments shapefile</param>
    ''' <param name="aMinCatchmentKM2">Minimum area for a catchment (square kilometers)</param>
    ''' <param name="aMinLengthKM">Minimum length for a flowline (kilometers)</param>
    ''' <param name="aOutletComId">given outlet</param>
    ''' <remarks></remarks>
    Public Shared Sub EnforceMinimumSize(ByRef aFlowlines As DotSpatial.Data.FeatureSet, _
                                         ByRef aCatchments As DotSpatial.Data.FeatureSet, _
                                         ByVal aMinCatchmentKM2 As Double, _
                                         ByVal aMinLengthKM As Double, _
                                         ByVal aOutletComId As Long, _
                                         ByVal aKeepConnectingRemovedFlowLines As Boolean, _
                                         ByVal aFields As FieldIndexes, _
                                         ByVal aOutletComIDs As Generic.List(Of Long),
                                         ByVal aDontCombineComIDs As Generic.List(Of Long))
        Dim lCheckArea As Boolean = (aMinCatchmentKM2 > 0)
        Dim lCheckLength As Boolean = (aMinLengthKM > 0)
        If Not lCheckArea AndAlso Not lCheckLength Then
            Logger.Dbg("EnforceMinimumSize: no minimum area or length to enforce")
            Exit Sub
        End If
        If aDontCombineComIDs Is Nothing Then
            aDontCombineComIDs = New Generic.List(Of Long)
        End If
        Logger.Dbg("EnforceMinimumSizeEntry  " & DumpComid(aFlowlines, aOutletComId, aFields) _
                       & " Flowlines: " & aFlowlines.Features.Count _
                       & " Catchments: " & aCatchments.Features.Count)
        'DumpFlowlinesCatchments(aFlowlines, aCatchments, aFields)

        Dim lUpstreamComIds As Generic.List(Of Long) = FindUpstreamKeys(aOutletComId, aFlowlines, aFields.FlowlinesComId, aFields.FlowlinesDownstreamComId)

        Logger.Dbg("Count Upstream = " & lUpstreamComIds.Count)
        Dim lComIdUp As Long
        For Each lComIdUp In lUpstreamComIds
            Logger.Status("Enforce Minimum Size upstream from " & lComIdUp, True) ' DumpComid(aFlowlines, lComIdUp, aFields), True)
            EnforceMinimumSize(aFlowlines, aCatchments, aMinCatchmentKM2, aMinLengthKM, lComIdUp, aKeepConnectingRemovedFlowLines, aFields, aOutletComIDs, aDontCombineComIDs)
        Next

        Dim lMainUpstreamChannelCumLen As Double = 0.0
        Dim lTotalContribArea As Double = 0.0
        Dim lMainUpstreamChannelComId As Long = FindMainChannel(aFlowlines, lUpstreamComIds, lTotalContribArea, lMainUpstreamChannelCumLen, 0, aFields)

        Dim lOutletFlowlineIndex As Integer = FindRecord(aFlowlines, aFields.FlowlinesComId, aOutletComId)
        If lOutletFlowlineIndex = -1 Then
            Logger.Dbg("OutletNotInLayer " & DumpComid(aFlowlines, aOutletComId, aFields), True)
            Return
        End If

        Dim lLength As Double = 0
        UpdateValueIfNotNull(lLength, aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesCumLen))
        If lLength = 0 Then 'Cumulative length not yet set, so set it to sum of main channel cumulative + outlet length
            UpdateValueIfNotNull(lLength, aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesLength))
            If lLength <= 0 Then
                lLength = DotSpatial.Topology.LineString.FromBasicGeometry(aFlowlines.Features(lOutletFlowlineIndex).BasicGeometry).Length
                lLength *= aFlowlines.Projection.Unit.Meters / 1000 'Convert to kilometers
                Logger.Dbg("Length missing for stream " & aOutletComId & ", computed " & atcUtility.DoubleToString(lLength) & " km")
                aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesLength) = lLength
            End If
            aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesCumLen) = lLength + lMainUpstreamChannelCumLen
            Logger.Dbg("Set CumLen of " & aOutletComId & " to " & lLength & " + " & lMainUpstreamChannelCumLen & " = " & aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesCumLen))
            'Else
            '    LogMessage(aLog,"Did not Set CumLen of aOutletComId = " & lLength)
            aFlowlines.DataTable.AcceptChanges()
        End If

        Dim lContribArea As Double = 0
        UpdateValueIfNotNull(lContribArea, aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesLocDrainArea))
        If lContribArea <= 0 Then 'Local drainage area not yet set
            'see if we can set it to difference of outlet cum - main channel cum
            'Find local drainage area from FlowlinesCumDrainArea
            UpdateValueIfNotNull(lContribArea, aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesCumDrainArea))
            If lContribArea > 0 AndAlso lContribArea >= lTotalContribArea Then
                aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesLocDrainArea) = lContribArea - lTotalContribArea
            Else
                Dim lCatchmentIndex As Integer = FindRecord(aCatchments, aFields.CatchmentComId, aOutletComId)
                If lCatchmentIndex > 0 Then
                    lContribArea = DotSpatial.Topology.LineString.FromBasicGeometry(aCatchments.Features(lCatchmentIndex).BasicGeometry).Area
                    lContribArea *= aCatchments.Projection.Unit.Meters / 1000000 'Convert to square kilometers
                    Logger.Dbg("Area missing for stream " & aOutletComId & ", computed " & atcUtility.DoubleToString(lContribArea) & " square km")
                    aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesLocDrainArea) = lContribArea
                End If
            End If
            aFlowlines.DataTable.AcceptChanges()
        End If

        Dim lCumDrainAreaDown As Double = 0.0
        UpdateValueIfNotNull(lCumDrainAreaDown, aFlowlines.Features(lOutletFlowlineIndex).DataRow(aFields.FlowlinesCumDrainArea))

        If lMainUpstreamChannelComId > 0 Then
            Logger.Dbg("MainUpstream from " & aOutletComId & " is " & DumpComid(aFlowlines, lMainUpstreamChannelComId, aFields))
        ElseIf lUpstreamComIds.Count > 0 Then
            Logger.Dbg("Error: NoMainUpstream from " & aOutletComId)
        Else
            Logger.Dbg("      TopOfStream at " & aOutletComId)
        End If

        Dim lFlowlineIndex As Integer

        For Each lComIdUp In lUpstreamComIds
            'Need to update each time through, index may change while merging
            lOutletFlowlineIndex = FindRecord(aFlowlines, aFields.FlowlinesComId, aOutletComId)
            If lOutletFlowlineIndex = -1 Then
                Logger.Dbg("Error: OutletLostFromLayer " & DumpComid(aFlowlines, aOutletComId, aFields), True)
                Return
            Else
                'Find contributing area and/or length of upstream segment
                lFlowlineIndex = FindRecord(aFlowlines, aFields.FlowlinesComId, lComIdUp)
                If lFlowlineIndex < 0 Then
                    Logger.Dbg("UpstreamFlowlineNotFound: " & lComIdUp)
                Else
                    If IsTooSmall(aFlowlines, lFlowlineIndex, aMinCatchmentKM2, aMinLengthKM, lCheckArea, lCheckLength, False, aFields) Then
                        'this upstream segment is too small/short, need to merge it with something
                        Dim lMerge As Boolean = True
                        Dim lKeepBothFlowlines As Boolean = False
                        Dim lCumulativeBigEnough As Boolean = Not IsTooSmall(aFlowlines, lFlowlineIndex, aMinCatchmentKM2, aMinLengthKM, lCheckArea, lCheckLength, True, aFields)

                        If lCumulativeBigEnough Then
                            'Even though this segment is small, it has significant upstream, so merge it upstream instead of discarding
                            Dim lUpUpstream As Generic.List(Of Long) = FindUpstreamKeys(lComIdUp, aFlowlines, aFields.FlowlinesComId, aFields.FlowlinesDownstreamComId)
                            Dim lCountBigUpstream As Integer = 0
                            Dim lCountBigDontCombine As Integer = 0
                            For Each lComIdUpUp As Long In lUpUpstream
                                Dim lRecordUpUp As Integer = FindRecord(aFlowlines, aFields.FlowlinesComId, lComIdUpUp)
                                If Not IsTooSmall(aFlowlines, lRecordUpUp, aMinCatchmentKM2, aMinLengthKM, lCheckArea, lCheckLength, False, aFields) Then
                                    lCountBigUpstream += 1
                                    If aDontCombineComIDs.Contains(lComIdUpUp) Then
                                        lCountBigDontCombine += 1
                                    End If
                                End If
                            Next
                            If lCountBigUpstream = 1 AndAlso lCountBigDontCombine = 0 Then 'can merge upstream if there is only one large contributor
                                If MergeUpstream(aFlowlines, aCatchments, lUpUpstream, lComIdUp, aOutletComIDs, aFields) Then
                                    lMerge = False 'merge upstream succeeded, so do not merge downstream below
                                End If
                            End If
                        End If

                        If lMerge AndAlso Not aDontCombineComIDs.Contains(lComIdUp) Then
                            lOutletFlowlineIndex = FindRecord(aFlowlines, aFields.FlowlinesComId, aOutletComId) 'might have gotten off during MergeUpstream

                            If lComIdUp = lMainUpstreamChannelComId Then 'This upstream is on the main channel, probably want to keep the stream
                                If lCumDrainAreaDown > (aMinCatchmentKM2 / 2) Then 'only keep large enough segments even if on main channel 
                                    lKeepBothFlowlines = True
                                Else
                                    Logger.Dbg("Discarding main channel small upstream segment " & lCumDrainAreaDown & " < " & (aMinCatchmentKM2 / 2) & " " & DumpComid(aFlowlines, lMainUpstreamChannelComId, aFields))
                                End If
                            End If

                            Logger.Dbg("FlowlineMergeDownstream Keep=" & lKeepBothFlowlines & " " & DumpComid(aFlowlines, aOutletComId, aFields) & _
                                                           " with upstream " & DumpComid(aFlowlines, lComIdUp, aFields))
                            Try
                                If CombineCatchments(aCatchments, aOutletComId, lComIdUp, aFields) Then
                                    CombineFlowlines(aFlowlines, lOutletFlowlineIndex, lFlowlineIndex, lKeepBothFlowlines, lCumulativeBigEnough AndAlso aKeepConnectingRemovedFlowLines, aFields, aOutletComIDs)
                                Else
                                    Logger.Dbg("Failed to merge missing outlet catchment " & lComIdUp & " into " & aOutletComId, True)
                                End If
                                If lKeepBothFlowlines Then
                                    Logger.Dbg("AfterCombineWithMerge " & DumpComid(aFlowlines, aOutletComId, aFields))
                                Else
                                    Logger.Dbg("AfterCombineNoMerge " & DumpComid(aFlowlines, aOutletComId, aFields))
                                End If

                            Catch e As Exception
                                Logger.Dbg("Error, did not combine: " & e.Message, True)
                            End Try
                        End If
                        'SaveIntermediate(aCatchments, aFlowlines)
                    End If
                End If
            End If
        Next

        'If this is an outlet, merge upstream if not large enough
        If aOutletComIDs.Contains(aOutletComId) Then
            lFlowlineIndex = FindRecord(aFlowlines, aFields.FlowlinesComId, aOutletComId)
            lContribArea = -1
            If lCheckArea Then UpdateValueIfNotNull(lContribArea, aFlowlines.Features(lFlowlineIndex).DataRow(aFields.FlowlinesCumDrainArea))
            lLength = -1
            If lCheckLength Then UpdateValueIfNotNull(lLength, aFlowlines.Features(lFlowlineIndex).DataRow(aFields.FlowlinesLength))
            If (lCheckArea AndAlso lContribArea < aMinCatchmentKM2) OrElse (lCheckLength AndAlso lLength < aMinLengthKM) Then
                Dim lFindUpstreamKeys As Generic.List(Of Long) = FindUpstreamKeys(aOutletComId, aFlowlines, aFields.FlowlinesComId, aFields.FlowlinesDownstreamComId)
                'dont merge upstream with a dontcombine   
                If lFindUpstreamKeys.Count > 0 AndAlso Not aDontCombineComIDs.Contains(lFindUpstreamKeys(0)) Then
                    If MergeUpstream(aFlowlines, aCatchments, lFindUpstreamKeys, aOutletComId, aOutletComIDs, aFields) Then
                        Logger.Dbg("Merged small outlet upstream " & aOutletComId)
                    Else
                        Logger.Dbg("Failed to merge small outlet upstream " & aOutletComId)
                    End If
                End If
            End If
        End If

        aFlowlines.Save()
        aCatchments.Save()

        Logger.Dbg("AllDone " & DumpComid(aFlowlines, aOutletComId, aFields) & " " & aFlowlines.Features.Count & " " & aCatchments.Features.Count)

    End Sub

    ''' <summary>
    ''' Find the flowline from the list aFlowLineComIds with the largest contributing area
    ''' </summary>
    ''' <param name="aFlowlines">flowlines shapefile</param>
    ''' <param name="aFlowLineComIds">list of ComIDs to search for the main one</param>
    ''' <param name="aTotalContribArea">on return this is set to the total contributing area from all upstream channels</param>
    ''' <param name="aMainChannelCumLen">on return this is set to the cumulative length of the main channel</param>
    ''' <param name="aMainChannelIndex">on return this is set to the shape index of the main channel in aFlowlines</param>
    ''' <returns>ComID of flowline from aFlowLinesUpstreamComIds with the largest contributing drainage area</returns>
    ''' <remarks></remarks>
    Private Shared Function FindMainChannel(ByVal aFlowlines As DotSpatial.Data.FeatureSet, _
                                            ByVal aFlowLineComIds As Generic.List(Of Long), _
                                            ByRef aTotalContribArea As Double, _
                                            ByRef aMainChannelCumLen As Double, _
                                            ByRef aMainChannelIndex As Integer, _
                                            ByVal aFields As FieldIndexes) As Long
        Dim lMainLocalChannelComId As Long = 0
        Dim lFlowlineIndex As Integer
        Dim lMainChannelContribArea As Double = 0
        For Each lComId As Long In aFlowLineComIds
            Try
                lFlowlineIndex = FindRecord(aFlowlines, aFields.FlowlinesComId, lComId)
                If lFlowlineIndex < 0 Then Stop
                Dim lContribArea As Double = -2
                UpdateValueIfNotNull(lContribArea, aFlowlines.Features(lFlowlineIndex).DataRow(aFields.FlowlinesCumDrainArea))

                Dim lThisChannelCumLen As Double = -2
                UpdateValueIfNotNull(lThisChannelCumLen, aFlowlines.Features(lFlowlineIndex).DataRow(aFields.FlowlinesCumLen))
                If lContribArea <= 0 OrElse lThisChannelCumLen <= 0 Then 'Look upstream for valid values
                    Logger.Dbg("Warning: FindMainChannel " & lComId & " CumDrainArea=" & lContribArea & " CumLen=" & lThisChannelCumLen & " (both should be > 0)", True)
                End If

                'Moved most of this block into FillMissingFlowlineCUMDRAINAG
                'If lContribArea <= 0 OrElse lThisChannelCumLen <= 0 Then 'Look upstream for valid values
                '    LogMessage(aLog, "FindMainChannel " & lComId & " CumDrainArea=" & lContribArea & " CumLen=" & lThisChannelCumLen & " (both should be > 0)")
                '    Dim lUpstreamComIds As Generic.List(Of Long) = FindUpstreamComIDs(aLog, aFlowlines, lComId)
                '    If lUpstreamComIds.Count = 0 Then
                '        LogMessage(aLog, "FindMainChannel: No Upstream flowlines to search")
                '        lContribArea = Math.Max(0, lContribArea)
                '        lThisChannelCumLen = Math.Max(0, lThisChannelCumLen)
                '    Else
                '        LogMessage(aLog, "FindMainChannel: Count Upstream = " & lUpstreamComIds.Count)
                '        Dim lUpContribArea As Double = 0
                '        Dim lUpCumLen = 0
                '        Dim lUpstreamChannelIndex As Integer 'we don't need to know this number now, but need a variable to send below
                '        FindMainChannel(aLog, aFlowlines, lUpstreamComIds, lUpContribArea, lUpCumLen, lUpstreamChannelIndex)
                '        lContribArea = Math.Max(lUpContribArea, lContribArea)
                '        lThisChannelCumLen = Math.Max(lUpCumLen, lThisChannelCumLen)
                '        LogMessage(aLog, "FindMainChannel: looked upstream and found CumDrainArea=" & lContribArea & " CumLen=" & lThisChannelCumLen)
                '    End If
                'End If

                If lContribArea > 0 AndAlso lThisChannelCumLen > 0 Then
                    aTotalContribArea += lContribArea
                    'Choose upstream branch with largest contributing area as main channel
                    If lContribArea > lMainChannelContribArea Then
                        lMainChannelContribArea = lContribArea
                        UpdateValueIfNotNull(aMainChannelCumLen, lThisChannelCumLen)
                        lMainLocalChannelComId = lComId
                        aMainChannelIndex = lFlowlineIndex
                    End If
                End If
            Catch lEx As Exception
                Logger.Dbg("FindMainChannel: " & lEx.ToString, True)
            End Try
        Next
        Return lMainLocalChannelComId
    End Function

    ''' <summary>
    ''' Find the COMIDs of flowlines that flow into the given outlet COMID
    ''' </summary>
    ''' <param name="aOutletKey">Key of flowline to look upstream of (COMID in NHDPlus)</param>
    ''' <param name="aFlowlines">Shapefile of flowlines</param>
    ''' <param name="aFlowlinesKey">Key field index for aFlowlines table</param>
    ''' <param name="aFlowlinesDownstreamKey">Downstream key field index for aFlowlines table</param>
    ''' <param name="aMaxNumFlowlines">Follow upstream at most this many segments along each path. 
    '''  1=find only flowlines that empty directly into aOutletCOMID
    '''  2=also find flowlines that flow directly into those</param>
    ''' <returns></returns>
    ''' <remarks>aMaxNumFlowlinesUpstream</remarks>
    Public Shared Function FindUpstreamKeys(ByVal aOutletKey As Long,
                                            ByVal aFlowlines As DotSpatial.Data.Shapefile,
                                            ByVal aFlowlinesKey As Integer,
                                            ByVal aFlowlinesDownstreamKey As Integer,
                                   Optional ByVal aMaxNumFlowlines As Integer = 1) As Generic.List(Of Long)
        Dim lFlowLinesUpstreamComIds As New Generic.List(Of Long)
        Dim lFlowLinesUpstream As Generic.List(Of Integer) = FindRecords(aFlowlines, aFlowlinesDownstreamKey, aOutletKey)
        Dim lComId As Long
        For Each lFlowlineIndex As Integer In lFlowLinesUpstream
            Try
                lComId = -1
                If UpdateValueIfNotNull(lComId, aFlowlines.Features(lFlowlineIndex).DataRow(aFlowlinesKey)) Then
                    If lComId = aOutletKey Then
                        Logger.Dbg("Error: ComID upstream of itself: " & lComId, True)
                        Return lFlowLinesUpstreamComIds
                    Else
                        Logger.Dbg("Upstream " & lComId)
                        lFlowLinesUpstreamComIds.Add(lComId)
                        If aMaxNumFlowlines > 1 Then
                            lFlowLinesUpstreamComIds.AddRange(FindUpstreamKeys(lComId, aFlowlines, aFlowlinesKey, aFlowlinesDownstreamKey, aMaxNumFlowlines - 1))
                        End If
                    End If
                Else
                    Logger.Dbg("Error: No ComId at flowline index " & lFlowlineIndex, True)
                End If
            Catch lEx As Exception
                Logger.Dbg("FindUpstreamComIDs error at index " & lFlowlineIndex & ": " & lEx.Message & " " & lEx.StackTrace, True)
            End Try
        Next
        Return lFlowLinesUpstreamComIds
    End Function

    Private Shared Function IsTooSmall(ByVal aFlowlines As DotSpatial.Data.Shapefile, _
                                       ByVal aFlowlineIndex As Integer, _
                                       ByVal aMinCatchmentKM2 As Double, _
                                       ByVal aMinLengthKM As Double, _
                                       ByVal aCheckArea As Boolean, _
                                       ByVal aCheckLength As Boolean, _
                                       ByVal aCumulative As Boolean, _
                                       ByVal aFields As FieldIndexes) As Boolean
        Dim lField As Integer
        Dim lLocCum As String

        If aCumulative Then
            lLocCum = "Cumulative"
        Else
            lLocCum = "Local"
        End If

        If aCheckArea Then
            Dim lContribArea As Double = -1
            If aCumulative Then
                lField = aFields.FlowlinesCumDrainArea
            Else
                lField = aFields.FlowlinesLocDrainArea
            End If
            UpdateValueIfNotNull(lContribArea, aFlowlines.Features(aFlowlineIndex).DataRow(lField))
            If lContribArea < aMinCatchmentKM2 Then
                Logger.Dbg(aFlowlines.DataTable.Columns(lField).ColumnName & " area too small (" & lContribArea & " < " & aMinCatchmentKM2 & ")")
                Return True
            End If
        End If

        If aCheckLength Then
            Dim lLength As Double = -1
            If aCumulative Then
                lField = aFields.FlowlinesCumLen
            Else
                lField = aFields.FlowlinesLength
            End If
            UpdateValueIfNotNull(lLength, aFlowlines.Features(aFlowlineIndex).DataRow(lField))
            If lLength < aMinLengthKM Then
                Logger.Dbg(lLocCum & " length too short (" & lLength & " < " & aMinLengthKM & ")")
                Return True
            End If
        End If
        Return False
    End Function

    Private Shared Function MergeUpstream(ByRef aFlowlines As DotSpatial.Data.FeatureSet, _
                                          ByRef aCatchments As DotSpatial.Data.FeatureSet, _
                                          ByVal aUpstreamComIds As Generic.List(Of Long), _
                                          ByVal aOutletComId As Long,
                                          ByVal aOutletComIds As Generic.List(Of Long), _
                                          ByVal aFields As FieldIndexes) As Boolean
        Dim lSuccess As Boolean = False
        Try
            Logger.Dbg("Merging upstream of " & DumpComid(aFlowlines, aOutletComId, aFields))
            Dim lMainChannelIndex As Integer
            If aUpstreamComIds.Count < 1 Then
                Logger.Dbg("Nothing upstream to merge with")
            Else
                Dim lMainUpstreamChannelComId As Long = FindMainChannel(aFlowlines, aUpstreamComIds, 0, 0, lMainChannelIndex, aFields)
                If lMainUpstreamChannelComId > 0 Then
                    Logger.Dbg("Merging upstream with " & DumpComid(aFlowlines, lMainUpstreamChannelComId, aFields))
                    Dim lOutletFlowlineIndex As Integer = FindRecord(aFlowlines, aFields.FlowlinesComId, aOutletComId)
                    lSuccess = CombineCatchments(aCatchments, aOutletComId, lMainUpstreamChannelComId, aFields)
                    If lSuccess Then
                        lSuccess = CombineFlowlines(aFlowlines, lOutletFlowlineIndex, lMainChannelIndex, True, False, aFields, aOutletComIds)
                        If Not lSuccess Then
                            Logger.Msg("Error: merged catchments but not flowlines up " & lMainUpstreamChannelComId & "-" & aOutletComId)
                        End If
                    Else
                        Logger.Msg("Error: Could not merge flowlines up " & lMainUpstreamChannelComId & "-" & aOutletComId)
                    End If
                Else
                    Logger.Dbg("No Main Channel upstream to merge with")
                End If
            End If
        Catch e As Exception
            Logger.Dbg("Error, did not combine: " & e.Message, True)
        End Try
        Return lSuccess
    End Function

    Private Shared Sub SaveIntermediate(ByVal aCatchments As DotSpatial.Data.FeatureSet, _
                                        ByVal aFlowlines As DotSpatial.Data.FeatureSet)
        Static lCount As Integer
        Dim lResult As Integer
        lCount += 1
        Math.DivRem(lCount, 500, lResult) 'every 500 write intermediate shapes
        If lResult = 0 Then
            Logger.Dbg("SaveIntermediate " & lCount & " " & aCatchments.Features.Count)
            aFlowlines.Save()
            aCatchments.Save()
            lCount = 0
        End If
    End Sub

    Public Shared Function ObjectToDouble(ByVal aObject As Object, ByRef aDouble As Double) As Boolean
        If IsDBNull(aObject) Then
            aDouble = 0
            Return False
        Else
            aDouble = CDbl(aObject)
            Return True
        End If
    End Function

    ''' <summary>
    ''' Indexes of fields needed for performing network operations
    ''' Fields are named using NHDPlus terminology. To use non-NHDPlus files, create a custom constructor.
    ''' </summary>
    Public Class FieldIndexes
        Public FlowlinesLength As Integer
        Public FlowlinesCumLen As Integer
        Public FlowlinesCumDrainArea As Integer
        Public FlowlinesLocDrainArea As Integer
        Public FlowlinesDownstreamComId As Integer
        Public FlowlinesComId As Integer
        Public FlowlinesToNode As Integer
        Public FlowlinesFromNode As Integer
        Public CatchmentComId As Integer

        ''' <summary>
        ''' Find a field (aka column) by case-insensitive search for the field name
        ''' </summary>
        ''' <param name="aFeatureSet">Layer to search</param>
        ''' <param name="aFieldName">Name of field to search for</param>
        ''' <returns>Zero-based index of field if found, -1 if not found</returns>
        Private Shared Function FieldIndex(ByVal aFeatureSet As DotSpatial.Data.IFeatureSet, ByVal aFieldName As String, Optional ByVal aThrowException As Boolean = False) As Integer
            Dim lFieldName As String = aFieldName.ToLower
            Dim lFieldIndex As Integer = 0
            For Each lColumn As DataColumn In aFeatureSet.GetColumns
                If lColumn.ColumnName.ToLower.Equals(lFieldName) Then
                    Return lFieldIndex
                End If
                lFieldIndex += 1
            Next
            Logger.Dbg("Field " & aFieldName & " not found in " & aFeatureSet.Filename)
            If aThrowException Then
                Throw New ApplicationException("Required field " & aFieldName & " not found in " & aFeatureSet.Filename)
            End If
            Return -1
        End Function

        Public Sub New(ByVal aFlowlineShapefile As DotSpatial.Data.FeatureSet,
                       ByVal aCatchmentShapefile As DotSpatial.Data.FeatureSet)
            If FieldIndex(aFlowlineShapefile, "COMID") >= 0 Then
                SetNHDPlusFields(aFlowlineShapefile, aCatchmentShapefile)
            ElseIf FieldIndex(aFlowlineShapefile, "LINKNO") >= 0 Then 'TauDEM5
                SetTauDEM5Fields(aFlowlineShapefile, aCatchmentShapefile)
            End If
        End Sub

        Private Sub SetNHDPlusFields(ByVal aFlowlineShapefile As DotSpatial.Data.FeatureSet,
                                     ByVal aCatchmentShapefile As DotSpatial.Data.FeatureSet)
            FlowlinesLength = FieldIndex(aFlowlineShapefile, "LENGTHKM", True)
            FlowlinesCumLen = EnsureFieldExists(aFlowlineShapefile, "CUMLENKM", aFlowlineShapefile.DataTable.Columns.Item(FlowlinesLength).DataType)

            FlowlinesCumDrainArea = FieldIndex(aFlowlineShapefile, "CUMDRAINAG", True)
            FlowlinesLocDrainArea = EnsureFieldExists(aFlowlineShapefile, "LOCDRAINA", aFlowlineShapefile.DataTable.Columns.Item(FlowlinesCumDrainArea).DataType)

            FlowlinesDownstreamComId = FieldIndex(aFlowlineShapefile, "TOCOMID", True)
            FlowlinesComId = FieldIndex(aFlowlineShapefile, "COMID", True)
            FlowlinesToNode = FieldIndex(aFlowlineShapefile, "TONODE", True)
            FlowlinesFromNode = FieldIndex(aFlowlineShapefile, "FROMNODE", True)
            CatchmentComId = FieldIndex(aCatchmentShapefile, "COMID", True)
            'CatchmentArea = FieldIndex(lCatchmentShapefile, "AREASQKM")
        End Sub

        ''' <summary>
        ''' Checks for aFieldName as the name of a field in aFeatureSet.
        ''' If it already exists, the index of the field is returned,
        ''' otherwise the field is added, aFeatureSet is saved, and the new field index is returned.
        ''' </summary>
        ''' <returns>index of existing or newly created field</returns>
        ''' <remarks></remarks>
        Public Shared Function EnsureFieldExists(ByRef aFeatureSet As DotSpatial.Data.FeatureSet, _
                                                 ByVal aFieldName As String, _
                                                 aFieldType As System.Type) As Integer
            Dim lFieldIndex As Integer = FieldIndex(aFeatureSet, aFieldName)
            If lFieldIndex < 0 Then 'Need to add field
                lFieldIndex = aFeatureSet.DataTable.Columns.Count
                Dim lNewField As New System.Data.DataColumn
                With lNewField
                    .ColumnName = aFieldName
                    .DataType = aFieldType
                    '.MaxLength = lLenField.Width + 1
                    '.Precision = lLenField.Precision
                End With
                aFeatureSet.DataTable.Columns.Add(lNewField)
                aFeatureSet.Save()
                lFieldIndex = FieldIndex(aFeatureSet, aFieldName)
                If lFieldIndex < 0 Then
                    Throw New ApplicationException("EnsureFieldExists: Failed to add " & aFieldName & " to " & aFeatureSet.Filename)
                Else
                    Logger.Dbg("Added field " & lFieldIndex & " " & aFieldName)
                End If
            End If
            Return lFieldIndex
        End Function

        Private Sub SetTauDEM5Fields(ByVal aFlowlineShapefile As DotSpatial.Data.FeatureSet,
                                     ByVal aCatchmentShapefile As DotSpatial.Data.FeatureSet)
            Dim lAddedColumn As Boolean = False
            FlowlinesLength = FieldIndex(aFlowlineShapefile, "Length", True)
            FlowlinesCumLen = FieldIndex(aFlowlineShapefile, "CUMLENKM")
            If FlowlinesCumLen < 0 Then 'Need to add cumulative length field
                FlowlinesCumLen = aFlowlineShapefile.DataTable.Columns.Count
                Dim lLenField As System.Data.DataColumn = aFlowlineShapefile.DataTable.Columns.Item(FlowlinesLength)
                Dim lNewField As New System.Data.DataColumn
                With lNewField
                    .ColumnName = "CUMLENKM"
                    .DataType = lLenField.DataType
                    '.MaxLength = lLenField.Width + 1
                    '.Precision = lLenField.Precision
                End With
                aFlowlineShapefile.DataTable.Columns.Add(lNewField)
                lAddedColumn = True
                Logger.Dbg("Added field CUMLENKM")
            End If

            FlowlinesCumDrainArea = FieldIndex(aFlowlineShapefile, "US_Cont_Ar", True)
            FlowlinesLocDrainArea = FieldIndex(aFlowlineShapefile, "LOCDRAINA")
            If FlowlinesLocDrainArea < 0 Then
                FlowlinesLocDrainArea = aFlowlineShapefile.DataTable.Columns.Count
                Dim lCumField As System.Data.DataColumn = aFlowlineShapefile.DataTable.Columns.Item(FlowlinesCumDrainArea)
                Dim lNewField As New System.Data.DataColumn
                With lNewField
                    .ColumnName = "LOCDRAINA"
                    .DataType = lCumField.DataType
                    '.MaxLength = lCumField.Width
                    '.Precision = lCumField.Precision
                End With
                aFlowlineShapefile.DataTable.Columns.Add(lNewField)
                lAddedColumn = True
                Logger.Dbg("Added field LOCDRAINA")
            End If

            FlowlinesDownstreamComId = FieldIndex(aFlowlineShapefile, "DSLINKNO", True)
            FlowlinesComId = FieldIndex(aFlowlineShapefile, "LINKNO", True)
            FlowlinesToNode = FieldIndex(aFlowlineShapefile, "DSNODEID", True)
            FlowlinesFromNode = FieldIndex(aFlowlineShapefile, "USLINKNO1", True)
            CatchmentComId = 0 'FieldIndex(aCatchmentShapefile, "SUBBASIN", True)
            'CatchmentArea = FieldIndex(lCatchmentShapefile, "AREA")

            If lAddedColumn Then
                aFlowlineShapefile.Save()
            End If
        End Sub

    End Class
End Class
