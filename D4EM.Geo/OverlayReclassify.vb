Imports atcUtility
Imports MapWinUtility
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports D4EM.Data

Public Class OverlayReclassify

    Public Shared GridSlopeLayerSpecification As New LayerSpecification(Tag:="GridSlopeValue")

    Public Shared Function Overlay(ByVal aGridOutputFileName As String, _
                                   ByVal aGridSlopeValue As Layer, _
                                   ByVal aResume As Boolean, _
                                   ByVal ParamArray aLayers() As Layer) As HRUTable
        Dim lHruTable As HRUTable = Nothing
        Try
            Dim lLayers As New Generic.List(Of Layer)
            lLayers.AddRange(aLayers)

            lHruTable = Overlay(aGridOutputFileName, aGridSlopeValue, lLayers, aResume)

            For Each lLayer As Layer In lLayers
                lLayer.Close()
            Next
        Catch ex As Exception
            Logger.Dbg("OverlayProblem " & ex.Message & vbCrLf & ex.StackTrace)
            lHruTable = Nothing
        End Try

        Return lHruTable
    End Function

    ''' <summary>
    ''' Overlay the grid and/or shape layers in aLayers
    ''' For each unique combination, add an entry to Me.HruTable,
    ''' and put the index of the combination in Me.HruTable into a new grid named aGridOutputFilename
    ''' </summary>
    ''' <param name="aGridOutputFileName">Grid file name to write </param>
    ''' <param name="aGridSlopeValue">Grid containing slope values, used as a template for the resulting grid</param>
    ''' <param name="aLayers"></param>
    ''' <param name="aResume"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Overlay(ByVal aGridOutputFileName As String, _
                                   ByVal aGridSlopeValue As Layer, _
                                   ByVal aLayers As Generic.List(Of Layer), _
                          Optional ByVal aResume As Boolean = False) As HRUTable
        Dim lHruTable As HRUTable = Nothing

        Dim lDebug As Boolean = False
        Dim lResumed As Boolean = False
        Dim lSlopeRaster As DotSpatial.Data.Raster = aGridSlopeValue.AsRaster
        Dim lRasterProjection = lSlopeRaster.Projection
        Dim lLastRow As Integer = lSlopeRaster.EndRow
        Dim lLastCol As Integer = lSlopeRaster.EndColumn

        Dim lLayerMinCells As New List(Of DotSpatial.Data.RcIndex)
        Dim lLayerMaxCells As New List(Of DotSpatial.Data.RcIndex)

        Dim lGridOverlay As DotSpatial.Data.Raster = Nothing 'overlay output grid
        'Dim lGridOverlayHeader As New MapWinGIS.GridHeader ' output grid header
        Dim lGridOverlayNoDataValue As Integer = -1
        Dim lStartRow As Integer = 0

        If aResume AndAlso IO.File.Exists(aGridOutputFileName) Then
            lGridOverlay = DotSpatial.Data.Raster.Open(aGridOutputFileName)
            Dim lHruTableFilename As String = ""
            Dim lHruTableBaseFilename As String = IO.Path.ChangeExtension(aGridOutputFileName, ".table")
            For Each lFilename As String In IO.Directory.GetFiles(IO.Path.GetDirectoryName(aGridOutputFileName), _
                                                                  IO.Path.GetFileName(lHruTableBaseFilename) & "*.txt")
                Try
                    Dim lSavedRow As Integer = lFilename.Substring(lHruTableBaseFilename.Length).Replace(".txt", "")
                    If lSavedRow > lStartRow Then
                        lStartRow = lSavedRow
                        lHruTableFilename = lFilename
                    End If
                Catch
                End Try
            Next
            If IO.File.Exists(lHruTableFilename) Then
                lHruTable = New Geo.HRUTable(lHruTableFilename)
                lResumed = True
            End If
        End If

        Dim lRow As Integer
        Dim lCol As Integer
        Dim lSlopeReclassIndex As Integer
        Dim lTags As New Generic.List(Of String)
        Dim lLayerSameRaster As New Generic.List(Of Boolean)
        Dim lLayerSameProjection As New Generic.List(Of Boolean)
        Dim lLayerProjection As New Generic.List(Of DotSpatial.Projections.ProjectionInfo)
        For Each lLayer As Layer In aLayers
            If lLayer.Specification.Role = LayerSpecification.Roles.Slope Then
                lSlopeReclassIndex = lTags.Count
            End If
            lTags.Add(lLayer.Specification.Tag)
            lLayerSameRaster.Add(MatchesGrid(lLayer, lSlopeRaster))
            lLayerProjection.Add(lLayer.Projection)
            lLayerSameProjection.Add(lLayer.Projection.Equals(lRasterProjection))

            With lLayer.DataSet.Extent
                Dim lRcIndex = DotSpatial.Data.RasterExt.ProjToCell(lSlopeRaster, .MinX, .MaxY)
                If lRcIndex.IsEmpty() Then
                    lRcIndex = New DotSpatial.Data.RcIndex(0, 0)
                End If
                lLayerMinCells.Add(lRcIndex)

                lRcIndex = DotSpatial.Data.RasterExt.ProjToCell(lSlopeRaster, .MaxX, .MinY)
                If lRcIndex.IsEmpty() Then
                    lRcIndex = New DotSpatial.Data.RcIndex(lSlopeRaster.EndRow, lSlopeRaster.EndColumn)
                End If
                lLayerMaxCells.Add(lRcIndex)
            End With
        Next

        Dim lCellSizedX As Double = lSlopeRaster.CellWidth
        Dim lCellSizedY As Double = lSlopeRaster.CellHeight
        Dim lCellCountToArea As Double = (lCellSizedX * lCellSizedY) / (1000 * 1000)
        'km2
        Dim lAreaTotal As Double = (lLastCol + 1) * _
                                   (lLastRow + 1) * lCellCountToArea
        Dim lOutsideAreaSkipped As Double = 0.0
        Dim lRequiredAreaSkippedBadSlope As Double = 0.0
        Dim lBigSlopeAreaClamped As Double = 0.0
        Dim lRequiredAreaSkippedSlopeCantReclass As Double = 0.0
        Dim lTotalHruArea As Double = 0.0

        If Not lResumed Then
            lHruTable = New Geo.HRUTable(lTags)
            'lGridOverlayHeader = New MapWinGIS.GridHeader
            'lGridOverlayHeader.CopyFrom(aGridSlopeValue.Grid.Header)
            'lGridOverlayHeader.NodataValue = lGridOverlayNoDataValue
            'lGridOverlay.CreateNew(aGridOutputFileName, lGridOverlayHeader, MapWinGIS.GridDataType.ShortDataType, lGridOverlayNoDataValue, True, MapWinGIS.GridFileType.Binary)
            'lGridOverlay = New DotSpatial.Data.Raster(Of Integer)(lSlopeRaster.NumRows, lSlopeRaster.NumColumns)
            lGridOverlay = aGridSlopeValue.CreateSimilarRaster(aGridOutputFileName, GetType(Integer))
            'IRaster output = Raster.CreateRaster(outputFileName, input.DriverCode, input.NumColumns, input.NumRows, 1, input.DataType, new[] { string.Empty })
            lGridOverlay.NoDataValue = lGridOverlayNoDataValue
        End If

        Logger.Dbg("OverlayCreated " & MemUsage())

        Dim lRem As Integer
        Logger.Dbg("OverlayValuesStart " & lLastRow & " rows, " & lLastCol & " columns " & MemUsage())
        Dim lLayerIndex As Integer
        Dim lLastLayerIndex As Integer = aLayers.Count - 1
        Dim lCount As Integer = 0
        Dim lMaxCount As Integer = (lLastRow - lStartRow) * lLastCol

        For lRow = lStartRow To lLastRow
            Math.DivRem(lRow, 100, lRem)
            If lRem = 0 AndAlso lRow > lStartRow Then
                Logger.Dbg("Row " & lRow & " of " & lLastRow & " HRUCount " & lHruTable.Count & " " & MemUsage())
                lHruTable.Save(IO.Path.ChangeExtension(aGridOutputFileName, ".table" & lRow.ToString & ".txt"))
                lGridOverlay.Save()
                Logger.Dbg("GridOverlaySaved " & MemUsage())
            End If

            'TODO: look for memory leak as needed here
            'If (System.Diagnostics.Process.GetCurrentProcess.PrivateMemorySize64 / (2 ^ 20)) > 1500 Then 'bigger for win64?
            '    Logger.Dbg("TryToFreeMemory " & MemUsage())
            '    For Each lLayer As clsLayer In aLayers
            '        lLayer.Reopen()
            '    Next
            '    aGridSlopeValue.Reopen()
            'End If

            Dim lLayersThisRow As New Generic.List(Of Integer)
            For lLayerIndex = 0 To lLastLayerIndex
                If lRow >= lLayerMinCells(lLastLayerIndex).Row AndAlso lRow <= lLayerMaxCells(lLastLayerIndex).Row Then
                    lLayersThisRow.Add(lLayerIndex)
                End If
            Next

            For lCol = 0 To lLastCol
                lCount += 1
                Logger.Progress(lCount, lMaxCount)

                Dim lProjCoord As DotSpatial.Topology.Coordinate = DotSpatial.Data.RasterExt.CellToProj(lSlopeRaster, lRow, lCol)
                Dim lKey As String = ""
                Dim lKeyParts As New Generic.List(Of String)
                Dim lRequired As Boolean = False
                Dim lIncomplete As Boolean = False
                lLayerIndex = -1
                For Each lLayer As Layer In aLayers
                    lLayerIndex += 1
                    Dim lKeyPart As String = ""
                    If lLayersThisRow.Contains(lLayerIndex) AndAlso lCol >= lLayerMinCells(lLastLayerIndex).Column AndAlso lCol <= lLayerMaxCells(lLastLayerIndex).Column Then
                        'Find the part of the key for this layer at this lRow, lCol
                        Dim lUseCoord As DotSpatial.Topology.Coordinate = Nothing
                        If lLayerSameProjection(lLayerIndex) Then
                            lUseCoord = lProjCoord
                        Else
                            lUseCoord = lProjCoord.Clone  'Reproject a copy of the coordinates
                            D4EM.Geo.SpatialOperations.ProjectPoint(lUseCoord.X, lUseCoord.Y, lRasterProjection, lLayerProjection(lLayerIndex))
                        End If
                        If lLayer.IsFeatureSet() Then
                            Dim lFeatureIndex As Integer = lLayer.CoordinatesInShapefile(lUseCoord.X, lUseCoord.Y)
                            If lFeatureIndex >= 0 Then
                                Try
                                    lKeyPart = lLayer.AsFeatureSet.Features(lFeatureIndex).DataRow(lLayer.IdFieldIndex)
                                Catch exCast As System.InvalidCastException
                                    'Ignore invalid cast, this probably means value is DBNull, which means it is missing, which leaves lKeyPart correctly blank
                                Catch exShapeValue As Exception
                                    Logger.Dbg("Exception getting shape value at feature index " & lFeatureIndex & ", field " & lLayer.IdFieldIndex & " (" & exShapeValue.Message & ")")
                                End Try
                            End If
                        Else
                            Dim lLayerRow As Integer
                            Dim lLayerColumn As Integer
                            Dim lMyRaster As DotSpatial.Data.Raster = lLayer.AsRaster()
                            If lLayerSameRaster(lLayerIndex) Then
                                lLayerRow = lRow
                                lLayerColumn = lCol
                            Else
                                With DotSpatial.Data.RasterExt.ProjToCell(lMyRaster, lUseCoord)
                                    lLayerRow = .Row
                                    lLayerColumn = .Column
                                End With
                            End If
                            If lLayerRow < 0 OrElse lLayerColumn < 0 OrElse lLayerRow > lMyRaster.EndRow OrElse lLayerColumn > lMyRaster.EndColumn Then
                                'outside this grid
                            Else
                                lKeyPart = lMyRaster.Value(lLayerRow, lLayerColumn)
                                If lKeyPart = lMyRaster.NoDataValue Then
                                    lKeyPart = ""
                                End If
                            End If
                        End If
                    End If

                    If lKeyPart.Length = 0 Then 'no data for this layer at this row, col
                        lIncomplete = True
                        lKeyPart = "Missing"
                    Else
                        'If we have a good value in a required layer, keep the cell even if incomplete
                        If lLayer.IsRequired Then lRequired = True
                    End If

                    lKey &= lKeyPart & "_"
                    lKeyParts.Add(lKeyPart)
                Next
                If lIncomplete AndAlso Not lRequired Then
                    lOutsideAreaSkipped += lCellCountToArea
                    lGridOverlay.Value(lRow, lCol) = lGridOverlayNoDataValue
                Else
                    Dim lGridSlopeValue As Double = lSlopeRaster.Value(lRow, lCol)
                    If lGridSlopeValue < 0 Then
                        lRequiredAreaSkippedBadSlope += lCellCountToArea
                        lGridOverlay.Value(lRow, lCol) = lGridOverlayNoDataValue
                    ElseIf lKeyParts(lSlopeReclassIndex).Length = 0 AndAlso lGridSlopeValue > 0 Then
                        lRequiredAreaSkippedSlopeCantReclass += lCellCountToArea
                        lGridOverlay.Value(lRow, lCol) = lGridOverlayNoDataValue
                    Else
                        lTotalHruArea += lCellCountToArea
                        If lGridSlopeValue > 100 Then 'Clamp slope values to maximum of 100
                            lBigSlopeAreaClamped += lCellCountToArea
                            lGridSlopeValue = 100
                        End If
                        lKey = lKey.TrimEnd("_")
                        If lHruTable.Contains(lKey) Then
                            lGridOverlay.Value(lRow, lCol) = CDbl(lHruTable(lKey).Handle)
                            With lHruTable(lKey)
                                .CellCount += 1
                                .Area = .CellCount * lCellSizedX * lCellSizedY
                                If lGridSlopeValue < 100 Then 'Only include reasonable slope values in mean
                                    If .SlopeMean > 99 Then
                                        .SlopeMean = lGridSlopeValue
                                    Else
                                        .SlopeMean = (.SlopeMean * (.CellCount - 1) + lGridSlopeValue) / .CellCount
                                    End If
                                End If
                            End With
                        Else
                            lGridOverlay.Value(lRow, lCol) = CDbl(lHruTable.Count)
                            Dim lOverlayGridTable As New HRU(lKey, lHruTable.Count, 1, lKeyParts, _
                                                             (lCellSizedX * lCellSizedY), lGridSlopeValue)
                            lHruTable.Add(lOverlayGridTable)
                            lOverlayGridTable = Nothing
                            'If lDebug Then Logger.Dbg("AfterNewGridStructure " & MemUsage())
                        End If
                    End If
                End If
            Next
            If lDebug Then
                Logger.Dbg("Row " & lRow & _
                           " OverlayHashTableCount " & lHruTable.Count & _
                           " Complete " & MemUsage())
                Logger.Flush()
            End If
        Next
        lHruTable.Save(IO.Path.ChangeExtension(aGridOutputFileName, ".table.txt"))

        Logger.Dbg("OverlayValuesSet " & lLastRow * lLastCol & " " & MemUsage())
        Logger.Dbg("HRUCount " & lHruTable.Count)

        Logger.Dbg("Total Overlay Area     " & DoubleToString(lAreaTotal, , "#,###,##0.00"))
        Logger.Dbg("Total Area Outside     " & DoubleToString(lOutsideAreaSkipped, , "#,###,##0.00"))
        Logger.Dbg("Total Area Inside      " & DoubleToString(lAreaTotal - lOutsideAreaSkipped, , "#,###,##0.00"))
        Logger.Dbg("Total Area In HRUs     " & DoubleToString(lTotalHruArea, , "#,###,##0.00"))
        Logger.Dbg("Inside Area Not in HRU " & DoubleToString(lAreaTotal - lOutsideAreaSkipped - lTotalHruArea, , "#,###,##0.00") & " = Inside - HRUs")
        Logger.Dbg("Inside Area Not in HRU " & DoubleToString(lRequiredAreaSkippedBadSlope + lRequiredAreaSkippedSlopeCantReclass, , "#,###,##0.00") & " = Bad Slope + Can't Reclassify")
        Logger.Dbg("Area Clamped Big Slope " & DoubleToString(lBigSlopeAreaClamped, , "#,###,##0.00"))
        Logger.Dbg("Area Skipped Bad Slope " & DoubleToString(lRequiredAreaSkippedBadSlope, , "#,###,##0.00"))
        Logger.Dbg("Area Skipped Reclass   " & DoubleToString(lRequiredAreaSkippedSlopeCantReclass, , "#,###,##0.00"))

        lGridOverlay.Save()
        Logger.Dbg("GridOverlaySaved " & MemUsage())
        'TODO: write a legend file using the keys
        lGridOverlay.Close()
        Logger.Dbg("GridOverlayClosed " & MemUsage())
        lGridOverlay = Nothing
        lHruTable.ComputeTotalCellCount()
        Return lHruTable
    End Function

    ''' <summary>
    ''' Overlay land use grid and a soil shape layer
    ''' For each unique combination, add an entry to returned HruTable,
    ''' Create a new grid named aGridOutputFilename with cell values either:
    '''  the same as the original land use grid value if no soil overlaps that cell or the soil does not contain a value for aShapesOverlayField
    '''  or the original land use grid value times ten plus a number depending on the value of the soil overlay field: A=1, B=2, C=3, D=4
    ''' </summary>
    ''' <param name="aGridOutputFileName">Grid file name to write </param>
    ''' <param name="aLandUseGrid">Land use grid to be overlaid with aSoilShapes, is also used as a template for the resulting grid</param>
    ''' <param name="aSoilShapes">Soil shapefile</param>
    ''' <param name="aShapesOverlayField">Field name to use as a key in overlay. If null or empty, defaults to aSoilShapes.IdFieldIndex.</param>
    Public Shared Function OverlayLandUseSoils(ByVal aGridOutputFileName As String, _
                                               ByVal aLandUseGrid As D4EM.Data.Layer, _
                                               ByVal aSoilShapes As D4EM.Data.Layer, _
                                               ByVal aShapesOverlayField As String) As HRUTable
        Dim lHruTable As HRUTable = Nothing
        Dim lShapesOverlayFieldIndex As Integer
        If String.IsNullOrEmpty(aShapesOverlayField) Then
            lShapesOverlayFieldIndex = aSoilShapes.IdFieldIndex
        Else
            lShapesOverlayFieldIndex = aSoilShapes.FieldIndex(aShapesOverlayField)
        End If

        Dim lDebug As Boolean = False
        Dim lInputRaster As DotSpatial.Data.Raster = aLandUseGrid.AsRaster
        Dim lLastRow As Integer = lInputRaster.EndRow
        Dim lLastCol As Integer = lInputRaster.EndColumn

        Dim lFeatureSet = aSoilShapes.AsFeatureSet
        Dim lLayerMinCell As DotSpatial.Data.RcIndex
        Dim lLayerMaxCell As DotSpatial.Data.RcIndex

        Dim lGridOverlay As DotSpatial.Data.Raster = Nothing 'overlay output grid
        Dim lGridOverlayNoDataValue As Integer = -1
        Dim lStartRow As Integer = 0

        Dim lRow As Integer
        Dim lCol As Integer
        Dim lTags As New Generic.List(Of String)

        lTags.Add(aSoilShapes.Specification.Tag)

        With aSoilShapes.DataSet.Extent
            Dim lRcIndex = DotSpatial.Data.RasterExt.ProjToCell(lInputRaster, .MinX, .MaxY)
            If lRcIndex.IsEmpty() Then
                lRcIndex = New DotSpatial.Data.RcIndex(0, 0)
            End If
            lLayerMinCell = lRcIndex

            lRcIndex = DotSpatial.Data.RasterExt.ProjToCell(lInputRaster, .MaxX, .MinY)
            If lRcIndex.IsEmpty() Then
                lRcIndex = New DotSpatial.Data.RcIndex(lInputRaster.EndRow, lInputRaster.EndColumn)
            End If
            lLayerMaxCell = lRcIndex
        End With

        Dim lCellSizedX As Double = lInputRaster.CellWidth
        Dim lCellSizedY As Double = lInputRaster.CellHeight
        Dim lCellCountToArea As Double = (lCellSizedX * lCellSizedY) / (1000 * 1000)
        'km2
        Dim lAreaTotal As Double = (lLastCol + 1) * _
                                   (lLastRow + 1) * lCellCountToArea
        Dim lTotalHruArea As Double = 0.0

        lHruTable = New Geo.HRUTable(lTags)
        lGridOverlay = aLandUseGrid.CreateSimilarRaster(aGridOutputFileName, GetType(Integer))
        lGridOverlay.NoDataValue = lGridOverlayNoDataValue

        Logger.Dbg("OverlayCreated " & MemUsage())

        Dim lRem As Integer
        Logger.Dbg("OverlayValuesStart " & lLastRow & " rows, " & lLastCol & " columns " & MemUsage())
        Dim lCount As Integer = 0
        Dim lMaxCount As Integer = (lLastRow - lStartRow) * lLastCol

        For lRow = lStartRow To lLastRow
            Math.DivRem(lRow, 100, lRem)
            If lRem = 0 AndAlso lRow > lStartRow Then
                Logger.Dbg("Row " & lRow & " of " & lLastRow & " HRUCount " & lHruTable.Count & " " & MemUsage())
            End If

            If lRow >= lLayerMinCell.Row AndAlso lRow <= lLayerMaxCell.Row Then
                For lCol = 0 To lLastCol
                    lCount += 1
                    Logger.Progress(lCount, lMaxCount)

                    Dim lProjCoord As DotSpatial.Topology.Coordinate = DotSpatial.Data.RasterExt.CellToProj(lInputRaster, lRow, lCol)
                    Dim lKey As String = lInputRaster.Value(lRow, lCol)
                    Dim lKeyParts As New Generic.List(Of String)
                    lKeyParts.Add(lKey.Clone)

                    Dim lKeyPart As String = ""
                    If lCol >= lLayerMinCell.Column AndAlso lCol <= lLayerMaxCell.Column Then
                        'Find the part of the key for this layer at this lRow, lCol
                        D4EM.Geo.SpatialOperations.ProjectPoint(lProjCoord.X, lProjCoord.Y, lInputRaster.Projection, aSoilShapes.Projection)
                        Dim lFeatureIndex As Integer = aSoilShapes.CoordinatesInShapefile(lProjCoord.X, lProjCoord.Y)
                        If lFeatureIndex >= 0 Then
                            Try
                                Dim lDbObject As Object = lFeatureSet.Features(lFeatureIndex).DataRow(lShapesOverlayFieldIndex)
                                If Not IsDBNull(lDbObject) Then lKeyPart = lDbObject
                            Catch exCast As System.InvalidCastException
                                'Ignore invalid cast, this probably means value is DBNull, which means it is missing, which leaves lKeyPart correctly blank
                            Catch exShapeValue As Exception
                                Logger.Dbg("Exception getting shape value at feature index " & lFeatureIndex & ", field " & lShapesOverlayFieldIndex & " (" & exShapeValue.Message & ")")
                            End Try
                        End If
                    End If

                    Dim lGridOverlayValue As Integer = lHruTable.Count
                    Select Case lKeyPart
                        Case ""
                            lKeyPart = "Missing" : lGridOverlayValue = lInputRaster.Value(lRow, lCol)
                        Case "A", "A/B", "A/C", "A/D"
                            lKeyPart = "A" : lGridOverlayValue = 1 + 10 * lInputRaster.Value(lRow, lCol)
                        Case "B", "B/A", "B/C", "B/D"
                            lKeyPart = "B" : lGridOverlayValue = 2 + 10 * lInputRaster.Value(lRow, lCol)
                        Case "C", "C/A", "C/B", "C/D"
                            lKeyPart = "C" : lGridOverlayValue = 3 + 10 * lInputRaster.Value(lRow, lCol)
                        Case "D", "D/A", "D/B", "D/C"
                            lKeyPart = "D" : lGridOverlayValue = 4 + 10 * lInputRaster.Value(lRow, lCol)
                        Case Else
                            lGridOverlayValue = lInputRaster.Value(lRow, lCol)
                    End Select

                    lKey &= "_" & lKeyPart
                    lKeyParts.Add(lKeyPart)

                    lTotalHruArea += lCellCountToArea
                    If lHruTable.Contains(lKey) Then
                        lGridOverlay.Value(lRow, lCol) = lHruTable(lKey).Handle
                        With lHruTable(lKey)
                            .CellCount += 1
                            .Area = .CellCount * lCellSizedX * lCellSizedY
                        End With
                    Else
                        lGridOverlay.Value(lRow, lCol) = lGridOverlayValue
                        Dim lOverlayGridTable As New HRU(lKey, lGridOverlayValue, 1, lKeyParts, _
                                                         (lCellSizedX * lCellSizedY), 0)
                        lHruTable.Add(lOverlayGridTable)
                        lOverlayGridTable = Nothing
                    End If
                Next
                If lDebug Then
                    Logger.Dbg("Row " & lRow & _
                               " OverlayHashTableCount " & lHruTable.Count & _
                               " Complete " & MemUsage())
                    Logger.Flush()
                End If
            End If
        Next

        Logger.Dbg("OverlayValuesSet " & lLastRow * lLastCol & " " & MemUsage())
        Logger.Dbg("HRUCount " & lHruTable.Count)

        Logger.Dbg("Total Overlay Area     " & DoubleToString(lAreaTotal, , "#,###,##0.00"))
        Logger.Dbg("Total Area In HRUs     " & DoubleToString(lTotalHruArea, , "#,###,##0.00"))
        Logger.Dbg("Area Not in HRU " & DoubleToString(lAreaTotal - lTotalHruArea, , "#,###,##0.00"))

        lGridOverlay.Save()
        Logger.Dbg("GridOverlaySaved " & MemUsage())
        'TODO: write a legend file using the keys
        lGridOverlay.Close()
        Logger.Dbg("GridOverlayClosed " & MemUsage())
        lGridOverlay = Nothing
        lHruTable.ComputeTotalCellCount()
        lHruTable.Save(IO.Path.ChangeExtension(aGridOutputFileName, ".table.txt"))
        Return lHruTable
    End Function

    ''' <summary>
    ''' Create a new grid by resampling an original grid at each pixel of a reference grid
    ''' </summary>
    ''' <param name="aOriginalGrid">Grid to sample values from</param>
    ''' <param name="aReferenceGrid">Grid in projection and resolution desired</param>
    ''' <param name="aGridOutputFileName">Save resampled grid in this file</param>
    ''' <returns>New grid containing values from aOriginalGrid at locations from aReferenceGrid</returns>
    ''' <remarks></remarks>
    Public Shared Function Resample(ByVal aOriginalGrid As Layer, ByVal aReferenceGrid As Layer, ByVal aGridOutputFileName As String) As Layer
        Dim lOriginalRaster As DotSpatial.Data.Raster = aOriginalGrid.AsRaster
        Dim lReferenceRaster As DotSpatial.Data.Raster = aReferenceGrid.AsRaster
        Dim lLastRow As Integer = lReferenceRaster.EndRow
        Dim lLastCol As Integer = lReferenceRaster.EndColumn

        Dim lLayerMinCell As DotSpatial.Data.RcIndex
        Dim lLayerMaxCell As DotSpatial.Data.RcIndex

        Dim lResampled As DotSpatial.Data.Raster = Nothing 'overlay output grid
        Dim lNoDataValue As Double = lOriginalRaster.NoDataValue
        Dim lStartRow As Integer = 0

        Dim lRow As Integer
        Dim lCol As Integer

        With aOriginalGrid.DataSet.Extent
            lLayerMinCell = DotSpatial.Data.RasterExt.ProjToCell(lReferenceRaster, .MinX, .MaxY)
            If lLayerMinCell.IsEmpty() Then
                lLayerMinCell = New DotSpatial.Data.RcIndex(0, 0)
            End If

            lLayerMaxCell = DotSpatial.Data.RasterExt.ProjToCell(lReferenceRaster, .MaxX, .MinY)
            If lLayerMaxCell.IsEmpty() Then
                lLayerMaxCell = New DotSpatial.Data.RcIndex(lReferenceRaster.EndRow, lReferenceRaster.EndColumn)
            End If
        End With

        Dim lCellSizedX As Double = lReferenceRaster.CellWidth
        Dim lCellSizedY As Double = lReferenceRaster.CellHeight

        lResampled = aReferenceGrid.CreateSimilarRaster(aGridOutputFileName, lOriginalRaster.DataType)
        lResampled.NoDataValue = lOriginalRaster.NoDataValue



        Logger.Dbg("Resample Start " & lLastRow & " rows, " & lLastCol & " columns " & MemUsage() & " Creating " & aGridOutputFileName)

        For lRow = lStartRow To lLastRow
            'Math.DivRem(lRow, 100, lRem)
            'If lRem = 0 AndAlso lRow > lStartRow Then
            '    Logger.Dbg("Row " & lRow & " of " & lLastRow & " HRUCount " & lHruTable.Count & " " & MemUsage())
            '    lHruTable.Save(IO.Path.ChangeExtension(aGridOutputFileName, ".table" & lRow.ToString & ".txt"))
            '    lGridOverlay.Save()
            '    Logger.Dbg("GridOverlaySaved " & MemUsage())
            'End If

            If lRow >= lLayerMinCell.Row AndAlso lRow <= lLayerMaxCell.Row Then
                For lCol = 0 To lLastCol
                    If lCol >= lLayerMinCell.Column AndAlso lCol <= lLayerMaxCell.Column Then
                        Dim lProjCoord As DotSpatial.Topology.Coordinate = DotSpatial.Data.RasterExt.CellToProj(lReferenceRaster, lRow, lCol)
                        D4EM.Geo.SpatialOperations.ProjectPoint(lProjCoord.X, lProjCoord.Y, lReferenceRaster.Projection, lOriginalRaster.Projection)
                        With DotSpatial.Data.RasterExt.ProjToCell(lOriginalRaster, lProjCoord)
                            If .Row < 0 OrElse .Column < 0 OrElse .Row > lOriginalRaster.EndRow OrElse .Column > lOriginalRaster.EndColumn Then
                                'outside this grid
                                lResampled.Value(lRow, lCol) = lNoDataValue
                            Else
                                lResampled.Value(lRow, lCol) = lOriginalRaster.Value(.Row, .Column)
                            End If
                        End With
                    Else
                        lResampled.Value(lRow, lCol) = lNoDataValue
                    End If
                Next
            Else
                For lCol = 0 To lLastCol
                    lResampled.Value(lRow, lCol) = lNoDataValue
                Next
            End If
            Logger.Progress(lRow, lLastRow)
        Next
        lResampled.Save()
        Return New Layer(lResampled, aOriginalGrid.Specification)
    End Function

    ''' <summary>True if aTestLayer is a grid whose cells exactly overlap aGrid's cells</summary>
    ''' <param name="aTestLayer">layer to test</param>
    ''' <param name="aGrid">raster to compare aTestLayer with</param>
    ''' <returns>False of aTestLayer is not a grid or does not exactly overlap aGrid's cells</returns>
    Private Shared Function MatchesGrid(ByVal aTestLayer As Layer, ByVal aGrid As DotSpatial.Data.Raster) As Boolean
        If Not aTestLayer.IsFeatureSet Then
            Try
                Dim lTestRaster As DotSpatial.Data.Raster = aTestLayer.AsRaster
                Dim lTestEndCol As Integer = lTestRaster.EndColumn
                Dim lTestEndRow As Integer = lTestRaster.EndRow
                If lTestEndCol = aGrid.EndColumn AndAlso _
                    lTestEndRow = aGrid.EndRow Then
                    Dim lProjCoord = DotSpatial.Data.RasterExt.CellToProj(aGrid, 0, 0)
                    Dim lRowCol = DotSpatial.Data.RasterExt.ProjToCell(lTestRaster, lProjCoord)
                    If lRowCol.Column = 0 AndAlso lRowCol.Row = 0 Then
                        lProjCoord = DotSpatial.Data.RasterExt.CellToProj(aGrid, lTestEndRow, lTestEndCol)
                        lRowCol = DotSpatial.Data.RasterExt.ProjToCell(lTestRaster, lProjCoord)
                        Return (lRowCol.Column = lTestEndCol AndAlso lRowCol.Row = lTestEndRow)
                    End If
                End If
            Catch e As Exception
                Logger.Dbg("MatchesGrid Exception: " & e.Message)
            End Try
        End If
        Return False
    End Function

    Public Shared Sub ReportByTag(ByVal aReport As Text.StringBuilder, _
                           ByVal aCollection As atcCollection, _
                           ByVal aDisplayTags As Generic.List(Of String), _
                  Optional ByVal aDisplayFirst As Boolean = True, _
                  Optional ByVal aDisplayAll As Boolean = True, _
                  Optional ByVal aDisplayPredominant As Boolean = False)

        Dim lCountCum As Int64 = 0
        Dim lCountOut As Int64 = 0

        If aReport.Length = 0 Then
            aReport.Append("Index".PadLeft(12) & vbTab)
            For Each lDisplayTag As String In aDisplayTags
                aReport.Append(lDisplayTag.PadLeft(12) & vbTab)
            Next
            aReport.Append("Area".PadLeft(12) & vbTab _
                             & "CntCell".PadLeft(12) & vbTab _
                             & "PctTag".PadLeft(12) & vbTab _
                             & "CumPct".PadLeft(12) & vbTab _
                             & "Slope".PadLeft(12))
            aReport.AppendLine()
        End If

        If aCollection.Count > 0 Then
            If aCollection.ItemByIndex(0).GetType.Name = "atcCollection" Then
                For Each lCollection As atcCollection In aCollection
                    ReportByTag(aReport, lCollection, aDisplayTags, aDisplayFirst, aDisplayAll, aDisplayPredominant)
                Next
            Else
                For Each lHruTableOfTag As HRUTable In aCollection
                    Dim lTagCellCount As Int64 = lHruTableOfTag.TotalCellCount
                    Dim lHrusSortedByCellCount As HRUTable = lHruTableOfTag.Sort(True)
                    Dim lTagCellCum As Int64 = 0

                    If aDisplayFirst OrElse aDisplayAll Then
                        For Each lHru As HRU In lHrusSortedByCellCount
                            With lHru
                                lTagCellCum += .CellCount
                                lCountCum += .CellCount
                                lCountOut += 1

                                aReport.Append(lCountOut.ToString.PadLeft(12) & vbTab)
                                For Each lDisplayTag As String In aDisplayTags
                                    aReport.Append(lHruTableOfTag.Id(lHru, lDisplayTag).PadLeft(12) & vbTab)
                                Next

                                aReport.Append(DoubleToString(.Area, 12, "#,###,##0.").PadLeft(12) & vbTab & _
                                           .CellCount.ToString.PadLeft(12) & vbTab & _
                                           DoubleToString((100 * .CellCount) / lTagCellCount, , "#0.000", "#0.000", , 3).PadLeft(12) & vbTab & _
                                           DoubleToString((100 * lTagCellCum) / lTagCellCount, , "#0.000", "#0.000", , 3).PadLeft(12) & vbTab & _
                                           DoubleToString(.SlopeMean, , "#,##0.00").PadLeft(12))

                                If aDisplayPredominant Then
                                    For Each lDisplayTag As String In aDisplayTags
                                        Dim lPredominantValue As String = lHruTableOfTag.PredominantTagValue(lDisplayTag)
                                        If lPredominantValue <> lHruTableOfTag.Id(lHru, lDisplayTag) Then
                                            aReport.Append(vbTab & "Predominant " & lDisplayTag & " = " & lPredominantValue & " first = " & lHruTableOfTag.Id(lHru, lDisplayTag))
                                        End If
                                    Next
                                    aReport.AppendLine()
                                End If
                                aReport.AppendLine()
                                If Not aDisplayAll Then Exit For
                            End With
                        Next
                    End If

                    If aDisplayPredominant Then
                        aReport.Append("Predominant" & vbTab)
                        For Each lDisplayTag As String In aDisplayTags
                            aReport.Append(lHruTableOfTag.PredominantTagValue(lDisplayTag).PadLeft(12) & vbTab)
                        Next
                        aReport.AppendLine()
                    End If
                Next
            End If
        End If
        Logger.Dbg(aDisplayTags(0) & "Count " & aCollection.Count & " CountCum " & lCountCum)

    End Sub

    Public Shared Function UniqueValuesSummary(ByVal aHruTable As HRUTable, Optional ByVal aTag As String = Nothing) As String
        Dim lTags As Generic.List(Of String)
        If aTag Is Nothing Then
            lTags = aHruTable.Tags
        Else
            lTags = New Generic.List(Of String)
            lTags.Add(aTag)
        End If

        Dim lSB As New Text.StringBuilder
        For Each lTag As String In lTags
            lSB.AppendLine(lTag & " Summary")
            lSB.AppendLine("Key" & vbTab & "Count" & vbTab & "% of Total" & vbTab & "Cumulative %")
            Dim lTagTotals As atcUtility.atcCollection = aHruTable.CountCellsPerTagValue(lTag)
            Dim lTotalTotal As Int64 = 0
            For lIndex As Integer = lTagTotals.Count - 1 To 0 Step -1
                lTotalTotal += lTagTotals.ItemByIndex(lIndex)
            Next
            Dim lCumCount As Int64 = 0
            For lIndex As Integer = lTagTotals.Count - 1 To 0 Step -1
                Dim lCountThisTagValue As Int64 = lTagTotals.ItemByIndex(lIndex)
                lCumCount += lCountThisTagValue
                lSB.AppendLine(lTagTotals.Keys(lIndex) & vbTab & lCountThisTagValue.ToString.PadLeft(10) & vbTab _
                               & atcUtility.DoubleToString(100 * lCountThisTagValue / lTotalTotal, , "0.0", "0.0").PadLeft(5) & vbTab _
                               & atcUtility.DoubleToString(100 * lCumCount / lTotalTotal, , "0.0", "0.0").PadLeft(5))
            Next
        Next
        Return lSB.ToString
    End Function

    ''' <summary>
    ''' Simplify a collection of HRUs by dissolving those with areas below a threshold into similar HRUs
    ''' </summary>
    ''' <param name="aLayerTags">Tag names used for each field of the HRUs</param>
    ''' <param name="aSubBasinTable">Collection of clsHruTable items, one clsHruTable for each subbasin, obtainable from clsHruTable.SummarizeByTag("SubBasin")</param>
    ''' <param name="aTag">Tag of layer to dissolve, or "Area" to decide by area of HRU</param>
    ''' <param name="aIgnoreBelowFraction">Threshold as a fraction (between zero and 1) of the area of the subbasin</param>
    ''' <param name="aIgnoreBelowAbsolute">Threshold as an absolute area</param>
    ''' <param name="aGridOverlayFileName">Grid containing HRU indexes</param>
    ''' <returns>Table of HRUs from which those representing small [aTag] have been removed and remaining HRUs have been expanded to preserve total area</returns>
    ''' <remarks></remarks>
    Public Shared Function Simplify(ByVal aLayerTags As Generic.List(Of String), _
                                    ByVal aSubBasinTable As atcCollection, _
                                    ByVal aTag As String, _
                                    ByVal aIgnoreBelowFraction As Double, _
                                    ByVal aIgnoreBelowAbsolute As Double, _
                                    ByVal aGridOverlayFileName As String) As HRUTable

        Dim lHruTableSimplified As New HRUTable(aLayerTags)
        Dim lTagIsArea As Boolean = False
        If aTag = "Area" Then lTagIsArea = True

        Dim lAreaTotalBefore As Double = 0.0
        Dim lNumHRUsBefore As Integer = 0
        Dim lAreaTotalAfter As Double = 0.0
        For Each lHruTable As HRUTable In aSubBasinTable
            Dim lSubBasinAreaTotal As Double = 0.0
            Dim lTagAreaMax As Double = 0.0 'Largest area with this tag
            Dim lTagAreaMaxId As String = "" 'ID of largest area
            Dim lTagAreas As New atcCollection
            Dim lTagKey As String
            For Each lHru As HRU In lHruTable
                lNumHRUsBefore += 1
                lSubBasinAreaTotal += lHru.Area
                If lTagIsArea Then
                    lTagKey = lHru.Key
                Else
                    lTagKey = lHruTable.Id(lHru, aTag)
                End If
                lTagAreas.Increment(lTagKey, lHru.Area)
                Dim lTagAreaIndex As Integer = lTagAreas.IndexFromKey(lTagKey)
                If lTagAreas(lTagAreaIndex) > lTagAreaMax Then
                    lTagAreaMax = lTagAreas(lTagAreaIndex)
                    lTagAreaMaxId = lTagKey
                End If
            Next
            lAreaTotalBefore += lSubBasinAreaTotal

            Dim lAreaRemoved As Double = 0.0
            For Each lTagKey In lTagAreas.Keys
                Dim lTagArea As Double = lTagAreas.ItemByKey(lTagKey)
                If (lTagArea / lSubBasinAreaTotal) < aIgnoreBelowFraction OrElse _
                    lTagArea < aIgnoreBelowAbsolute Then
                    If lTagAreaMaxId <> lTagKey Then
                        lAreaRemoved += lTagArea
                        lTagAreas.Increment(lTagKey, -lTagArea)
                    Else
                        Logger.Dbg("MustKeepSomeArea " & lTagAreaMaxId)
                    End If
                End If
            Next

            Dim lHruCountRemoved As Integer = 0
            Dim lHruCountSaved As Integer = 0
            For Each lHru As HRU In lHruTable
                If lTagIsArea Then
                    lTagKey = lHru.Key
                Else
                    lTagKey = lHruTable.Id(lHru, aTag)
                End If
                If (lTagAreas.ItemByKey(lTagKey) > 0) Then
                    lHruCountSaved += 1
                Else
                    lHruCountRemoved += 1
                End If
            Next

            Dim lAreaAdjustmentRatio As Double = lSubBasinAreaTotal / (lSubBasinAreaTotal - lAreaRemoved)

            Logger.Dbg("SubBasin " & lHruTable.Id(lHruTable.Item(0), "catchment") & _
                       " O " & lHruTable.Count & _
                       " C " & lHruCountSaved & _
                       " R " & lHruCountRemoved & _
                       " A " & lSubBasinAreaTotal & _
                       " F " & lAreaAdjustmentRatio)
            For Each lHru As HRU In lHruTable
                If lTagIsArea Then
                    lTagKey = lHru.Key
                Else
                    lTagKey = lHruTable.Id(lHru, aTag)
                End If
                If (lTagAreas.ItemByKey(lTagKey) > 0) Then
                    Dim lHruSimplified As HRU = lHru.Clone
                    With lHruSimplified
                        .Area *= lAreaAdjustmentRatio
                        lHruTableSimplified.Add(lHruSimplified)
                        lAreaTotalAfter += .Area
                    End With
                End If
            Next
        Next
        lHruTableSimplified.ComputeTotalCellCount()

        Logger.Dbg("BeforeSimplify: HruCount=" & lNumHRUsBefore & " AreaTotal=" & DoubleToString(lAreaTotalBefore / (1000 * 1000), , "#,###,##0.00"))
        Logger.Dbg("AfterSimplify:  HruCount " & lHruTableSimplified.Count & " AreaTotal=" & DoubleToString(lAreaTotalAfter / (1000 * 1000), , "#,###,##0.00"))
        Dim lSB As New System.Text.StringBuilder
        Dim lSortTags As New Generic.List(Of String)
        lSortTags.Add("catchment") 'SubBasin")
        ReportByTag(lSB, lHruTableSimplified.SummarizeByTag(lSortTags), aLayerTags)
        IO.File.WriteAllText(IO.Path.GetDirectoryName(aGridOverlayFileName) & "\HRUsRev" & aIgnoreBelowFraction.ToString & ".txt", lSB.ToString)
        Logger.Flush()

        Return lHruTableSimplified
    End Function

    Private Shared pLastPrivateMemory As Integer = 0
    Private Shared pLastGcMemory As Integer = 0
    Friend Shared Function MemUsage() As String
        System.GC.Collect()
        System.GC.WaitForPendingFinalizers()
        Dim lPrivateMemory As Integer = System.Diagnostics.Process.GetCurrentProcess.PrivateMemorySize64 / (2 ^ 20)
        Dim lGcMemory As Integer = System.GC.GetTotalMemory(True) / (2 ^ 20)
        MemUsage = "Megabytes: " & lPrivateMemory & " (" & Format((lPrivateMemory - pLastPrivateMemory), "+0;-0") & ") " _
                   & " GC: " & lGcMemory & " (" & Format((lGcMemory - pLastGcMemory), "+0;-0") & ") "
        pLastPrivateMemory = lPrivateMemory
        pLastGcMemory = lGcMemory
    End Function
End Class

''' <summary>
''' Hydrologic Response Unit test
''' </summary>
''' <remarks></remarks>
Public Class HRU
    Implements ICloneable

    Public Handle As Long
    Public Key As String
    Public CellCount As Integer
    Public Ids As New Generic.List(Of String)
    Public Area As Double
    Public SlopeMean As Double

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New HRU(Key, Handle, CellCount, Ids, Area, SlopeMean)
    End Function

    Public Sub New(ByVal aKey As String, ByVal aHandle As Integer, ByVal aCellCount As Integer, _
                   ByVal aIds As Generic.List(Of String), _
                   ByVal aArea As Double, ByVal aSlopeMean As Double)
        Key = aKey
        Handle = aHandle
        CellCount = aCellCount
        Ids.AddRange(aIds)
        Area = aArea
        SlopeMean = aSlopeMean
    End Sub

    Public Sub SetKeyFromIds()
        Key = ""
        For Each lValue As String In Ids
            Key &= lValue & "_"
        Next
        Key = Key.TrimEnd("_")
    End Sub
End Class

Public Class HRUComparer
    Implements Collections.IComparer

    Public TagIndex As Integer = -1

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        If TagIndex < 0 Then
            Return x.CellCount.CompareTo(y.CellCount)
        Else
            Return x.Ids(TagIndex).CompareTo(y.Ids(TagIndex))
        End If
    End Function
End Class

Public Class HRUComparerGeneric
    Implements Collections.Generic.IComparer(Of HRU)

    Public TagIndex As Integer = -1

    Public Function Compare(ByVal x As HRU, ByVal y As HRU) As Integer Implements System.Collections.Generic.IComparer(Of HRU).Compare
        If TagIndex < 0 Then
            Return x.CellCount.CompareTo(y.CellCount)
        Else
            Return x.Ids(TagIndex).CompareTo(y.Ids(TagIndex))
        End If
    End Function
End Class

Public Class HRUTable
    Inherits KeyedCollection(Of String, HRU)
    Implements IComparable

    Public TotalCellCount As Int64 = 0

    ''' <summary>
    ''' One tag for each layer contributing to HRU key, in same order as clsHru.Key and clsHru.Ids
    ''' </summary>
    ''' <remarks></remarks>
    Public Tags As New Generic.List(Of String)

    ''' <summary>
    ''' Create empty HRU table with the given set of layer tags
    ''' </summary>
    ''' <param name="aTags">One tag for each layer contributing to HRU key, in same order as clsHru.Key and clsHru.Ids</param>
    ''' <remarks></remarks>
    Sub New(ByVal aTags As Generic.List(Of String))
        Tags.AddRange(aTags)
    End Sub

    ''' <summary>
    ''' Create HRU table by reading saved table from file
    ''' </summary>
    ''' <param name="aFilename">full path of file to read</param>
    Sub New(ByVal aFilename As String)
        Dim lFile As New IO.StreamReader(aFilename)
        Dim lTagsToRead As Integer = lFile.ReadLine().Substring("Tags ".Length)
        While lTagsToRead > 0
            Tags.Add(lFile.ReadLine)
            lTagsToRead -= 1
        End While
        TotalCellCount = 0
        Dim lHrusToRead As Integer = lFile.ReadLine().Substring("HRUs ".Length)
        While lHrusToRead > 0
            Dim lHandle As Integer = lFile.ReadLine()
            Dim lCellCount As Integer = lFile.ReadLine()
            TotalCellCount += lCellCount
            Dim lArea As Double = lFile.ReadLine()
            Dim lSlopeMean As Double = lFile.ReadLine()
            Dim lKey As String = lFile.ReadLine()
            If lKey.Contains(":") Then
                lKey = lKey.Substring(0, lKey.IndexOf(":"))
            End If
            Dim lIds As New Generic.List(Of String)
            lIds.AddRange(lKey.Split("_"))
            Me.Add(New HRU(lKey, lHandle, lCellCount, lIds, lArea, lSlopeMean))
            lHrusToRead -= 1
        End While
        lFile.Close()
    End Sub

    ''' <summary>
    ''' Get the key for the KeyedCollection base class
    ''' </summary>
    ''' <param name="aHru">item to get key of</param>
    Protected Overrides Function GetKeyForItem(ByVal aHru As HRU) As String
        Return aHru.Key
    End Function

    ''' <summary>
    ''' Return the part of the key for the given HRU corresponding to the given layer tag
    ''' </summary>
    ''' <param name="aHru">HRU whose layer value is requested</param>
    ''' <param name="aTag">Tag of layer whose value for this HRU is requested</param>
    Function Id(ByVal aHru As HRU, ByVal aTag As String) As String
        Return (aHru.Ids(Tags.IndexOf(aTag)))
    End Function

    ''' <summary>
    ''' Save contents of this table to a text file
    ''' </summary>
    ''' <param name="aFilename">Full path of file to save in</param>
    Public Sub Save(ByVal aFilename As String)
        Dim lFile As New IO.StreamWriter(aFilename)
        lFile.WriteLine("Tags " & Tags.Count)
        For Each lTag As String In Tags
            lFile.WriteLine(lTag)
        Next
        lFile.WriteLine("HRUs " & Me.Count)
        For Each lHru As HRU In Me
            With lHru
                lFile.WriteLine(.Handle)
                lFile.WriteLine(.CellCount)
                lFile.WriteLine(.Area)
                lFile.WriteLine(.SlopeMean)
                lFile.WriteLine(.Key)
            End With
        Next
        lFile.Close()
    End Sub

    Public Sub ComputeTotalCellCount()
        TotalCellCount = 0
        For Each lHru As HRU In Me
            TotalCellCount += lHru.CellCount
        Next
    End Sub

    Public Function CountCellsPerTagValue(ByVal aTag As String) As atcCollection
        Dim lTagIndex As Integer = Tags.IndexOf(aTag)
        Dim lValueTotals As New atcCollection
        For Each lHru As HRU In Me
            lValueTotals.Increment(lHru.Ids(lTagIndex), lHru.CellCount)
        Next
        lValueTotals.SortByValue()
        Return lValueTotals
    End Function

    Public Function PredominantTagValue(ByVal aTag As String) As String
        Dim lMaxIndex As Integer = -1
        Dim lMaxCellCount As Int64 = 0
        Dim lSplit As atcCollection = SplitByTag(aTag)
        Dim lSplitIndex As Integer = 0
        For Each lTable As HRUTable In lSplit
            If lTable.TotalCellCount > lMaxCellCount Then
                lMaxIndex = lSplitIndex
                lMaxCellCount = lSplit.ItemByIndex(lSplitIndex).TotalCellCount
            End If
            lSplitIndex += 1
        Next
        If lMaxIndex > -1 Then
            Return lSplit.Keys(lMaxIndex)
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="aDescending"></param>
    ''' <param name="aTag">Tag to sort by, if omitted sort by cell count</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sort(ByVal aDescending As Boolean, Optional ByVal aTag As String = "") As HRUTable
        Dim lSortedTable As New HRUTable(Tags)

        Dim lComparer As New HRUComparerGeneric
        lComparer.TagIndex = Tags.IndexOf(aTag)
        Dim lSorter As New Generic.List(Of HRU)
        lSorter.AddRange(Me.Items)
        lSorter.Sort(lComparer)

        If aDescending Then
            For lIndex As Integer = lSorter.Count - 1 To 0 Step -1
                lSortedTable.Add(lSorter(lIndex))
            Next
        Else
            For Each lHru As HRU In lSorter
                lSortedTable.Add(lHru)
            Next
        End If

        lSortedTable.TotalCellCount = TotalCellCount
        Return lSortedTable
    End Function

    ''' <summary>
    ''' Split this table into a collection of smaller tables.
    ''' One new table is created for each unique value of aTag.
    ''' All elements sharing the same value of aTag are placed in the same resulting table.
    ''' </summary>
    ''' <param name="aTag">Name of tag to split by</param>
    ''' <remarks>returned collection is keyed by the unique tag value of each table</remarks>
    Public Function SplitByTag(ByVal aTag As String) As atcCollection
        Dim lTagIndex As Integer = Tags.IndexOf(aTag)
        Dim lHruTableOfTag As HRUTable
        Dim lSplitTables As New atcCollection
        For Each lHru As HRU In Me
            Dim lId As String = lHru.Ids(lTagIndex)
            Dim lIndex As Integer = lSplitTables.IndexFromKey(lId)
            If lIndex >= 0 Then
                lHruTableOfTag = lSplitTables.ItemByIndex(lIndex)
            Else
                lHruTableOfTag = New HRUTable(Tags)
                lSplitTables.Add(lId, lHruTableOfTag)
            End If
            lHruTableOfTag.Add(lHru)
        Next
        For Each lHruTableOfTag In lSplitTables
            lHruTableOfTag.ComputeTotalCellCount()
        Next
        lSplitTables.SortByValue()
        Return lSplitTables
    End Function

    ''' <summary>
    ''' Recursively split and sort by each given tag
    ''' If aSortTags contains only one tag, returns collection of tables from SplitByTag.
    ''' If aSortTags contains more than one tag, split by the first one then return a collection of collections
    ''' from recursive calls on tags after the first one.
    ''' </summary>
    ''' <param name="aSortTags">Tags to group by</param>
    ''' <returns>collection of resulting tables or collections for each value of the first aSortTag</returns>
    ''' <remarks></remarks>
    Public Function SummarizeByTag(ByVal aSortTags As Generic.List(Of String)) As atcCollection
        Dim lCountGood As Int64 = TotalCellCount
        Dim lSplitTables As atcCollection = SplitByTag(aSortTags(0))
        Dim lResult As atcCollection

        If aSortTags.Count > 1 Then
            Dim lSortTagsRemaining As Generic.List(Of String) = aSortTags.GetRange(1, aSortTags.Count - 1)
            lResult = New atcCollection
            For lIndex As Integer = 0 To lSplitTables.Count - 1
                Dim lSplitTable As HRUTable = lSplitTables.ItemByIndex(lIndex)
                lResult.Add(lSplitTables.Keys(lIndex), lSplitTable.SummarizeByTag(lSortTagsRemaining))
            Next
        Else
            lResult = lSplitTables
        End If

        Logger.Dbg(aSortTags(0) & "Count " & lSplitTables.Count & " CountGood " & lCountGood)
        Return lResult

    End Function

    ''' <summary>
    ''' Read a comma-separated values text file of IDs to change from and to
    ''' First column must contain values to change from and values must be unique.
    ''' Second column must contain values to change to and may contain duplicates.
    ''' </summary>
    ''' <param name="aCsvFileName">Full path of file to read</param>
    ''' <param name="aOriginalIDs">return parameter containing values to change from</param>
    ''' <param name="aNewIds">return parameter containing values to change to</param>
    ''' <remarks>useful before calling Reclassify</remarks>
    Public Sub ReadReclassifyCSV(ByVal aCsvFileName As String, _
                    ByRef aOriginalIDs As Generic.List(Of String), _
                    ByRef aNewIds As Generic.List(Of String), _
                    Optional ByVal aRemoveAfter As String = "")
        aOriginalIDs = New Generic.List(Of String)
        aNewIds = New Generic.List(Of String)
        For Each lLine As String In IO.File.ReadAllLines(aCsvFileName)
            Dim lFields() As String = lLine.Split(",")
            If lFields.Length = 2 Then
                aOriginalIDs.Add(lFields(0))
                If aRemoveAfter.Length > 0 AndAlso lFields(1).Contains(aRemoveAfter) Then
                    lFields(1) = lFields(1).Remove(lFields(1).IndexOf(aRemoveAfter))
                End If
                aNewIds.Add(lFields(1))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Change values in all HRUs with tag value
    ''' aTag=[a value in aChangeFromValues]
    ''' to 
    ''' aTag=[value at same index in aChangeToValues]
    ''' </summary>
    ''' <param name="aTag"></param>
    ''' <param name="aChangeFromValues"></param>
    ''' <param name="aChangeToValues"></param>
    ''' <remarks></remarks>
    Public Sub Reclassify(ByVal aTag As String, _
                          ByVal aChangeFromValues As Generic.List(Of String), _
                          ByVal aChangeToValues As Generic.List(Of String))
        Dim lTagIndex As Integer = Tags.IndexOf(aTag)
        Dim lNumChanged As Integer = 0
        If lTagIndex < 0 Then
            Logger.Dbg("ChangeTag:TagNotFound:" & aTag)
        Else
            For Each lHru As HRU In Me
                Dim lReplacmentIndex As Integer = aChangeFromValues.IndexOf(lHru.Ids(lTagIndex))
                If lReplacmentIndex >= 0 Then
                    lHru.Ids(lTagIndex) = aChangeToValues(lReplacmentIndex)
                    lHru.SetKeyFromIds()
                    lNumChanged += 1
                End If
            Next
            If lNumChanged > 0 Then
                Logger.Dbg("Reclassify changed " & aTag & " for " & lNumChanged & " of " & Me.Count)
                EnsureUnique()
            Else
                Logger.Dbg("Reclassify made no changes to " & aTag)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Scan all existing HRUs and consolidate any with matching keys
    ''' </summary>
    ''' <remarks>
    ''' When combining two HRUs:
    ''' Slope mean is recomputed as area-weighted average
    ''' Cell count and area are sums of existing values
    ''' </remarks>
    Public Sub EnsureUnique()
        Dim lCountBefore As Integer = Me.Count
        For lCheckIndex1 As Integer = Me.Count - 1 To 0 Step -1
            Dim lHru1 As HRU = Me.Item(lCheckIndex1)
            For lCheckIndex2 As Integer = lCheckIndex1 - 1 To 0 Step -1
                If lHru1.Key = Me.Item(lCheckIndex2).Key Then
                    With Me.Item(lCheckIndex2)
                        'TODO: keep track of which indexes are mapped to which other ones so we can interpret HRU grid, .Handles.Add?
                        'Logger.Dbg("Merging duplicate HRU " & .Key)
                        .Area += lHru1.Area
                        Dim lNewCellCount As Int64 = .CellCount + lHru1.CellCount
                        .SlopeMean = (.SlopeMean * .CellCount + lHru1.SlopeMean * lHru1.CellCount) / lNewCellCount
                        .CellCount = lNewCellCount
                    End With
                    Me.RemoveAt(lCheckIndex1)
                    Exit For
                End If
            Next
        Next
        Logger.Dbg("EnsureUnique number of HRUs changed from " & lCountBefore & " to " & Me.Count)
    End Sub

    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Return TotalCellCount.CompareTo(obj.TotalCellCount)
    End Function
End Class
