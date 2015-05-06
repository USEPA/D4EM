Imports atcControls
Imports atcUtility
Imports MapWinUtility
Imports D4EM.Model.HSPF.HSPFmodel

Public Class frmMapMicrobes

    Private pShapeFileName As String = Nothing
    Private pAnimalsFileName As String = Nothing
    Dim pAnimalsCSV As New atcTableDelimited()

    Private Sub btnOpenAnimals_Click(sender As System.Object, e As System.EventArgs) Handles btnOpen.Click
        pAnimalsFileName = FindFile("Locate AnimalsLL.txt", "AnimalsLL.txt", , , True)
        Try
            pAnimalsCSV = New atcTableDelimited
            pAnimalsCSV.Delimiter = ","
            If pAnimalsCSV.OpenFile(pAnimalsFileName) Then
                lblAnimalsFilename.Text = pAnimalsFileName
                Dim lShapeFileName As String = GetTemporaryFileName(IO.Path.GetFileNameWithoutExtension(pAnimalsFileName), ".shp")
                lShapeFileName = CreateShapefileFromTable(lShapeFileName, pAnimalsCSV, 1, 2, g_Map.Projection)
                If IO.File.Exists(lShapeFilename) Then
                    pShapeFileName = lShapeFilename
                    g_Map.Layers.Add(lShapeFilename)
                End If
            End If
        Catch ex As Exception
            Logger.MsgCustomOwned(ex.ToString, "Error opening Animals", Me, "Ok")
        End Try

        'find file PointSourceLL.txt 
        Dim lFolder As String = IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(pAnimalsFileName)) & IO.Path.DirectorySeparatorChar
        Dim lPointSourceFileName As String = FindFile("Locate PointSourceLL.txt", lFolder & "PointSourceLL.txt")
        If FileExists(lPointSourceFileName) Then
            'try to convert it to shapefile
            Dim lPointSourceCSV As New atcTableDelimited
            lPointSourceCSV.Delimiter = ","
            If lPointSourceCSV.OpenFile(lPointSourceFileName) Then
                Dim lShapeFilename As String = CreateShapefileFromTable(lPointSourceFileName, lPointSourceCSV, 1, 2, g_Map.Projection)
                If IO.File.Exists(lShapeFilename) Then
                    g_Map.Layers.Add(lShapeFilename)
                End If
            End If
        End If
        Logger.Progress(1, 1)
    End Sub

    Private Sub btnDeleteFarm_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        RemoveSelectedFarms()
    End Sub

    Private Sub RemoveSelectedFarms()
        Dim lFoundSelectedFarm As Boolean = False
        Dim lLayer = FindFarmLayer()
        If lLayer IsNot Nothing Then
            Dim lSelectedFeatures As Generic.List(Of DotSpatial.Data.IFeature) = lLayer.Selection.ToFeatureList
            If lSelectedFeatures.Count > 0 Then
                lFoundSelectedFarm = True
                Dim lPlural As String = ""
                If lSelectedFeatures.Count > 1 Then lPlural = "s"
                If Logger.MsgCustomOwned("Remove " & lSelectedFeatures.Count & " farm" & lPlural & "?", "Remove", Me, "Yes", "No") = "Yes" Then
                    lLayer.RemoveSelectedFeatures()
                    g_Map.Refresh()
                End If
            End If
        End If
        If Not lFoundSelectedFarm Then
            Logger.MsgCustomOwned("Select one or more farms on the map, then delete them.", "No Farms Selected", Me, "Ok")
        End If
    End Sub

    Private Function FindFarmLayer() As DotSpatial.Symbology.IFeatureLayer
        For Each lLayer In g_Map.GetAllLayers
            Try
                Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lLayer
                Dim lLayerFilename As String = DotSpatialDataSetFilename(lFeatureLayer.DataSet)
                If String.IsNullOrWhiteSpace(lLayerFilename) OrElse Not lLayerFilename.Equals(pShapeFileName) Then
                    Continue For
                End If
                Return lLayer
            Catch ex As Exception
                MapWinUtility.Logger.Dbg("UpdateSelectedFeatures: " & ex.Message & vbCrLf & ex.StackTrace.ToString)
            End Try
        Next
        Return Nothing
    End Function

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim lLayer = FindFarmLayer()
        If lLayer IsNot Nothing Then
            lLayer.DataSet.DataTable.AcceptChanges()
            'lLayer.DataSet.Save()
            g_Map.Layers.Remove(lLayer)
            If lLayer.DataSet IsNot Nothing Then
                lLayer.DataSet.Close()
            End If
            SaveCSV(IO.Path.ChangeExtension(pShapeFileName, ".dbf"), pAnimalsFileName)
        End If
    End Sub

    Private Sub SaveCSV(aDBFFileName As String, aCSVFileName As String)
        Dim lDBF As New atcTableDBF
        lDBF.OpenFile(aDBFFileName)
        Dim lFieldIndex As Integer

        Dim lNewStr As String = ""
        lNewStr = "Lat, Long, BeefCowCount,SwineCount,DairyCowCount,ChickenCount,HorseCount,SheepCount,OtherAgAnimalCount"
        'For lFieldIndex = 1 To lDBF.NumFields
        '    lNewStr = lNewStr & lDBF.FieldName(lFieldIndex)
        '    If lFieldIndex < lDBF.NumFields Then
        '        lNewStr = lNewStr & ","
        '    End If
        'Next
        lNewStr = lNewStr & vbCrLf

        For lRecordIndex As Integer = 1 To lDBF.NumRecords
            lDBF.CurrentRecord = lRecordIndex
            For lFieldIndex = 1 To lDBF.NumFields
                lNewStr = lNewStr & lDBF.Value(lFieldIndex)
                If lFieldIndex < lDBF.NumFields Then
                    lNewStr = lNewStr & ","
                End If
            Next
            lNewStr = lNewStr & vbCrLf
        Next
        SaveFileString(aCSVFileName, lNewStr)
        'pAnimalsCSV.WriteFile(aCSVFileName)

        'now reopen the file
        Try
            pAnimalsCSV = New atcTableDelimited
            pAnimalsCSV.Delimiter = ","
            If pAnimalsCSV.OpenFile(pAnimalsFileName) Then
                lblAnimalsFilename.Text = pAnimalsFileName
                Dim lShapeFileName As String = GetTemporaryFileName(IO.Path.GetFileNameWithoutExtension(pAnimalsFileName), ".shp")
                lShapeFileName = CreateShapefileFromTable(lShapeFileName, pAnimalsCSV, 1, 2, g_Map.Projection)
                If IO.File.Exists(lShapeFileName) Then
                    pShapeFileName = lShapeFileName
                    g_Map.Layers.Add(lShapeFileName)
                End If
            End If
            Logger.Progress(1, 1)
        Catch ex As Exception
            Logger.MsgCustomOwned(ex.ToString, "Error opening Animals", Me, "Ok")
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        Dim lLayer = FindFarmLayer()
        If lLayer IsNot Nothing Then
            lLayer.ShowAttributes()
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmMapMicrobes_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim lLayer = FindFarmLayer()
        If lLayer IsNot Nothing Then
            g_Map.Layers.Remove(lLayer)
            If lLayer.DataSet IsNot Nothing Then
                lLayer.DataSet.Close()
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        btnAdd.Enabled = False
        AddHandler g_Map.MouseClick, AddressOf AddPoint
    End Sub

    Private Sub AddPoint(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            RemoveHandler g_Map.MouseClick, AddressOf AddPoint

            Dim lLayer = FindFarmLayer()
            If lLayer Is Nothing Then
                Logger.Msg("Unable to find layer on map.", MsgBoxStyle.Critical, "Could Not Add Point")
            Else
                Dim lPointMapProjection As DotSpatial.Topology.Coordinate = g_Map.PixelToProj(New System.Drawing.Point(e.X, e.Y))

                Dim lLatitude = lPointMapProjection.Y
                Dim lLongitude = lPointMapProjection.X
                D4EM.Geo.SpatialOperations.ProjectPoint(lLongitude, lLatitude, g_Map.Projection, DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984)

                'remove old layer
                g_Map.Layers.Remove(lLayer)
                If lLayer.DataSet IsNot Nothing Then
                    lLayer.DataSet.Close()
                End If

                'try writing file
                Dim lDBF As New atcTableDBF
                lDBF.OpenFile(IO.Path.ChangeExtension(pShapeFileName, ".dbf"))
                Dim lFieldIndex As Integer
                Dim lNewStr As String = ""
                lNewStr = "Lat, Long, BeefCowCount,SwineCount,DairyCowCount,ChickenCount,HorseCount,SheepCount,OtherAgAnimalCount"
                lNewStr = lNewStr & vbCrLf
                For lRecordIndex As Integer = 1 To lDBF.NumRecords
                    lDBF.CurrentRecord = lRecordIndex
                    For lFieldIndex = 1 To lDBF.NumFields
                        lNewStr = lNewStr & lDBF.Value(lFieldIndex)
                        If lFieldIndex < lDBF.NumFields Then
                            lNewStr = lNewStr & ","
                        End If
                    Next
                    lNewStr = lNewStr & vbCrLf
                Next
                lNewStr = lNewStr & String.Format("{0:0.00000000}", lLatitude) & "," & String.Format("{0:0.00000000}", lLongitude) & ",0,0,0,0,0,0,0"
                SaveFileString(pAnimalsFileName, lNewStr)
                'and then re-read it

                Try
                    pAnimalsCSV = New atcTableDelimited
                    pAnimalsCSV.Delimiter = ","
                    If pAnimalsCSV.OpenFile(pAnimalsFileName) Then
                        lblAnimalsFilename.Text = pAnimalsFileName
                        Dim lShapeFileName As String = GetTemporaryFileName(IO.Path.GetFileNameWithoutExtension(pAnimalsFileName), ".shp")
                        lShapeFileName = CreateShapefileFromTable(lShapeFileName, pAnimalsCSV, 1, 2, g_Map.Projection)
                        If IO.File.Exists(lShapeFileName) Then
                            pShapeFileName = lShapeFileName
                            g_Map.Layers.Add(lShapeFileName)
                        End If
                    End If
                Catch ex As Exception
                    Logger.MsgCustomOwned(ex.ToString, "Error opening Animals", Me, "Ok")
                End Try

                g_Map.Layers.ResumeEvents()
                g_Map.Refresh()
            End If
            btnAdd.Enabled = True
        End If
    End Sub
End Class