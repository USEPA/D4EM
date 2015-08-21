Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports System.IO.Compression
Imports System.IO.Compression.ZipArchive
Imports System.IO.Compression.ZipFile

Public Class frmPublishMain

    Dim LocationLabels As New List(Of Label)
    Dim LocationCheckboxes As New List(Of CheckBox)
    Dim LocationTextboxes As New List(Of TextBox)

    Dim CalibrationLabels As New List(Of Label)
    Dim CalibrationCoefficients As New List(Of TextBox)
    Dim CalibrationNash As New List(Of TextBox)

    Dim AllInputFiles As New List(Of String)

    Private AllGroups As New List(Of GroupBox)
    Private TimeseriesGroupToSave As atcTimeseriesGroup

    Private Sub btnHSPF_Click(sender As Object, e As EventArgs) Handles btnHSPF.Click
        MetadataInfo.ModelType = "HSPF"
        MetadataInfo.ModelVersion = "12.2"
        OpenUCI()
    End Sub

    Private Sub btnSWAT_Click(sender As Object, e As EventArgs) Handles btnSWAT.Click
        MetadataInfo.ModelType = "SWAT"
        MetadataInfo.ModelVersion = "2005"
        OpenSWAT()
        ShowGroup(grpChooseFiles)
    End Sub

    Private Sub AddFilesToList(aFiles As IEnumerable(Of String), lst As Windows.Forms.CheckedListBox)
        For Each lFileName In aFiles
            Dim lModifiedFileName As String = lFileName
            If MetadataInfo.ModelType = "SWAT" AndAlso aFiles.Count > 20 Then
                Select Case IO.Path.GetExtension(lFileName)
                    Case ".chm", ".gw", ".hru", ".lwq", ".mgt", ".sol", ".res"
                        lModifiedFileName = IO.Path.Combine(IO.Path.GetDirectoryName(lFileName), "*" & IO.Path.GetExtension(lFileName))
                End Select
            End If
            If Not lst.Items.Contains(lModifiedFileName) Then
                lst.Items.Add(lModifiedFileName, IO.File.Exists(lFileName))
            End If
        Next
    End Sub

    Private Sub OpenUCI(Optional aUciFileName As String = "")
        'If IO.File.Exists(UCIFilename) AndAlso MsgBox("Use UCI file '" & UCIFilename & "'?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    Return True
        'End If
        lblProgress.Text = "Opening UCI File"
        ShowGroup(grpProgress)
        Dim lUCI As atcUCI.HspfUci = Nothing
        If IO.File.Exists(aUciFileName) Then
            UCIFilename = aUciFileName
        Else
            Dim lFileDialog As New Windows.Forms.OpenFileDialog()
            With lFileDialog
                .Title = "Select HSPF UCI file"
                .Filter = "UCI Files|*.uci"
                .FilterIndex = 0
                .DefaultExt = ".uci"
                .CheckFileExists = True
                .CheckPathExists = True
                .FileName = UCIFilename
                If .ShowDialog(Me) = DialogResult.OK Then
                    UCIFilename = .FileName
                End If
            End With
        End If
        ShowGroup(grpProgress)
        If UCIFile() IsNot Nothing Then
            AllInputFiles.Clear()
            AllInputFiles.Add(UCIFilename)
            AllInputFiles.AddRange(FilesInUCI(True, False, False, False, False))

            lstInputFiles.Items.Clear()
            AddFilesToList(AllInputFiles, lstInputFiles)

            lstOutputFiles.Items.Clear()
            AddFilesToList(FilesInUCI(True, True, False, False, True), lstOutputFiles)

            ShowGroup(grpChooseFiles)
        Else
            ShowGroup(grpChooseModel)
        End If
    End Sub

    Private Sub OpenSWAT(Optional aFileNameCio As String = "")
        'If IO.File.Exists(UCIFilename) AndAlso MsgBox("Use UCI file '" & UCIFilename & "'?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    Return True
        'End If
        lblProgress.Text = "Opening SWAT model"
        ShowGroup(grpProgress)
        Dim lUCI As atcUCI.HspfUci = Nothing
        If IO.File.Exists(aFileNameCio) Then
            CioFilename = aFileNameCio
        Else
            Dim lFileDialog As New Windows.Forms.OpenFileDialog()
            With lFileDialog
                .Title = "Select SWAT file.cio"
                .Filter = "CIO Files|*.cio"
                .FilterIndex = 0
                .DefaultExt = ".cio"
                .CheckFileExists = True
                .CheckPathExists = True
                .FileName = UCIFilename
                If .ShowDialog(Me) = DialogResult.OK Then
                    CioFilename = .FileName
                End If
            End With
        End If
        ShowGroup(grpProgress)
        If IO.File.Exists(CioFilename) Then
            AllInputFiles.Clear()
            AllInputFiles.AddRange(GetSwatInputFiles())

            lstInputFiles.Items.Clear()
            AddFilesToList(AllInputFiles, lstInputFiles)

            lstOutputFiles.Items.Clear()
            AddFilesToList(GetSwatTimeseriesOutputFiles(), lstOutputFiles)

            ShowGroup(grpChooseFiles)
        Else
            ShowGroup(grpChooseModel)
        End If
    End Sub

    Private Sub ShowGroup(aGroup As GroupBox)
        For Each lGroup As GroupBox In AllGroups
            lGroup.Visible = Object.ReferenceEquals(aGroup, lGroup)
        Next
        aGroup.Visible = True
        aGroup.Dock = DockStyle.Fill

        aGroup.BringToFront()
        aGroup.Refresh()
        Application.DoEvents()
        If Object.ReferenceEquals(aGroup, grpMetadata) Then
            PopulateMetadata()
            aGroup.Refresh()
            Application.DoEvents()
        End If
    End Sub

    Private Sub frmPublishMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        AllGroups.Add(grpChooseModel)
        AllGroups.Add(grpChooseFiles)
        AllGroups.Add(grpProgress)
        AllGroups.Add(grpMapLocations)
        AllGroups.Add(grpMetadata)
        ShowGroup(grpChooseModel)
    End Sub

    Private Sub btnAddInputFiles_Click(sender As Object, e As EventArgs) Handles btnAddInputFiles.Click
        Dim lFileDialog As New Windows.Forms.OpenFileDialog()
        With lFileDialog
            .Title = "Select Files to Add"
            .Filter = "All Files|*.*"
            .FilterIndex = 0
            .CheckFileExists = True
            .CheckPathExists = True
            If .ShowDialog(Me) = DialogResult.OK Then
                For Each lFileName As String In .FileNames
                    If Not AllInputFiles.Contains(lFileName) Then
                        AllInputFiles.Add(lFileName)
                    End If
                Next
                AddFilesToList(.FileNames, lstInputFiles)
            End If
        End With
    End Sub

    Private Sub btnAddOutputFiles_Click(sender As Object, e As EventArgs) Handles btnAddOutputFiles.Click
        Dim lFileDialog As New Windows.Forms.OpenFileDialog()
        With lFileDialog
            .Title = "Select Files to Add"
            .Filter = "All Files|*.*"
            .FilterIndex = 0
            .CheckFileExists = True
            .CheckPathExists = True
            If .ShowDialog(Me) = DialogResult.OK Then
                AddFilesToList(.FileNames, lstOutputFiles)
            End If
        End With
        'Dim lPath As String = My.Computer.FileSystem.SpecialDirectories. ' Environment.GetFolderPath(Environment.SpecialFolder.t)
    End Sub

    Private Sub frmPublishMain_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If grpChooseModel.Visible AndAlso e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            e.Effect = Windows.Forms.DragDropEffects.All
        End If
    End Sub

    Private Sub lstInputFiles_DragEnter(sender As Object, e As DragEventArgs) Handles lstInputFiles.DragEnter, lstOutputFiles.DragEnter
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            e.Effect = Windows.Forms.DragDropEffects.All
        End If
    End Sub

    Private Sub frmPublishMain_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop ' grpChooseModel.DragDrop
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            If grpChooseModel.Visible Then
                Dim lAllFiles As List(Of String) = GetAllDroppedFilenames(e.Data.GetData(Windows.Forms.DataFormats.FileDrop))
                If lAllFiles.Count = 1 AndAlso IO.Path.GetExtension(lAllFiles(0)).ToLowerInvariant().Equals(".uci") Then
                    MetadataInfo.ModelType = "HSPF"
                    MetadataInfo.ModelVersion = "12.2"
                    OpenUCI(lAllFiles(0))
                ElseIf lAllFiles.Count = 1 AndAlso IO.Path.GetFileName(lAllFiles(0)).ToLowerInvariant().Equals("file.cio") Then
                    MetadataInfo.ModelType = "SWAT"
                    MetadataInfo.ModelVersion = "2005"
                    OpenSWAT(lAllFiles(0))
                End If
            End If
        End If
    End Sub

    Private Sub lstFiles_DragDrop(sender As Object, e As DragEventArgs) Handles lstInputFiles.DragDrop, lstOutputFiles.DragDrop
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            Dim lAllFiles As List(Of String) = GetAllDroppedFilenames(e.Data.GetData(Windows.Forms.DataFormats.FileDrop))
            For Each lFileName As String In lAllFiles
                If Not sender.Items.Contains(lFileName) Then
                    sender.Items.Add(lFileName, True)
                End If
            Next
        End If
    End Sub

    Private Function GetAllDroppedFilenames(aDropped As IEnumerable(Of String)) As List(Of String)
        Dim lAllFiles As New List(Of String)
        For Each lFileOrDirectory As String In aDropped
            If IO.Directory.Exists(lFileOrDirectory) Then
                lAllFiles.AddRange(IO.Directory.EnumerateFiles(lFileOrDirectory, "*.*", IO.SearchOption.AllDirectories))
            Else
                lAllFiles.Add(lFileOrDirectory)
            End If
        Next
        Return lAllFiles
    End Function

    Private Function SelectedInputFiles() As List(Of String)
        Dim lSelected As New List(Of String)
        For Each lDataFileName As String In lstInputFiles.CheckedItems
            Dim lAsteriskIndex As Integer = lDataFileName.IndexOf("*")
            If lAsteriskIndex > -1 Then
                Dim lStartsWith As String = lDataFileName.Substring(0, lAsteriskIndex)
                Dim lEndsWith As String = lDataFileName.Substring(lAsteriskIndex)
                For Each lEveryFile As String In AllInputFiles
                    If lEveryFile.StartsWith(lStartsWith) AndAlso lEveryFile.EndsWith(lEndsWith) Then
                        lSelected.Add(lDataFileName)
                        Exit For
                    End If
                Next
            Else
                lSelected.Add(lDataFileName)
            End If
        Next
        Return lSelected
    End Function

    Private Sub btnChooseFilesNext_Click(sender As Object, e As EventArgs) Handles btnChooseFilesNext.Click
        If MetadataInfo.ModelRunDate < #1/1/1920# Then
            For Each lDataFileName As String In SelectedInputFiles()
                If IO.File.Exists(lDataFileName) Then
                    Dim lFileDate As Date = IO.File.GetLastWriteTime(lDataFileName)
                    If lFileDate > MetadataInfo.ModelRunDate Then
                        MetadataInfo.ModelRunDate = lFileDate
                    End If
                End If
            Next
        End If

        If lstOutputFiles.CheckedItems.Count > 0 Then
            lblProgress.Text = "Selecting Data to Upload"
            ShowGroup(grpProgress)
            For Each lDataFileName As String In lstOutputFiles.CheckedItems
                lblProgress.Text = "Opening " & lDataFileName
                ShowGroup(grpProgress)
                Select Case MetadataInfo.ModelType
                    Case "SWAT"
                        modPublishSWAT.OpenSwatOutput(lDataFileName)
                    Case Else '"HSPF"
                        atcDataManager.OpenDataSource(lDataFileName)
                End Select
            Next
            lblProgress.Text = "Selecting Output Datasets to Publish"
            Dim lAvailable As New atcTimeseriesGroup
            For Each lSource As atcDataSource In atcDataManager.DataSources
                Select Case lSource.Name
                    Case "Timeseries::WDM"
                        'Look through UCI to see which WDM datasets might be of interest
                        lAvailable.AddRange(modPublishHSPF.OutputDatasetsInWDM(lSource, True))
                    Case "Timeseries::HSPF Binary Output"
                        'Choose datasets of interest by constituent name
                        lAvailable.AddRange(modPublishHSPF.OutputDatasetsInHBN(lSource, True))
                    Case Else
                        lAvailable.AddRange(lSource.DataSets)
                End Select
            Next
            ShowGroup(grpProgress)
            TimeseriesGroupToSave = atcDataManager.UserSelectData("Select Data to Publish", , lAvailable)
            PopulateLocations()
            ShowGroup(grpMapLocations)
        Else
            ShowGroup(grpMetadata)
        End If
    End Sub

    Private Sub PopulateMetadata()

        For Each lControl As Object In grpMetadata.Controls
            If lControl.GetType().Name = "TextBox" Then
                Dim lTxt As TextBox = lControl
                lTxt.Text = GetSetting(g_AppNameShort, "Defaults", lTxt.Name)
            End If
        Next

        'Dim lConstituents As New atcCollection
        'If TimeseriesGroupToSave IsNot Nothing AndAlso TimeseriesGroupToSave.Count > 0 Then
        '    For Each lTs As atcTimeseries In TimeseriesGroupToSave
        '        lConstituents.Increment(lTs.Attributes.GetValue("Constituent"))
        '    Next
        'End If
        'TODO: save / restore metadata in registry
        'txtMetadata.Text = "Author Name (First, Middle Initial, Last): " & vbCrLf & _
        '                   "Author Email: " & vbCrLf & _
        '                   "Organization Name: " & vbCrLf & _
        '                   "Was model calibrated: " & vbCrLf & _
        '                   "Constituents used in model calibration (comma-separated): " & vbCrLf & _
        '                   "Calibration correlation coefficient values (comma-separated): " & vbCrLf & _
        '                   "Nash-Sutcliffe efficiency values (comma-separated): " & vbCrLf & _
        '                   "Date model executed: " & Format(MetadataInfo.ModelRunDate, "yyyy/MM/dd HH:mm") & vbCrLf & _
        '                   "Model Start Date: " & Format(MetadataInfo.ModelStartDate, "yyyy/MM/dd HH:mm") & vbCrLf & _
        '                   "Model End Date: " & Format(MetadataInfo.ModelEndDate, "yyyy/MM/dd HH:mm") & vbCrLf & _
        '                   "Model Name: " & MetadataInfo.PublishingModelType & vbCrLf & _
        '                   "Model version number: " & vbCrLf & _
        '                   "Model run or study description: " & vbCrLf & _
        '                   "Constituents published: " & String.Join(",", lConstituents.Keys.ToArray)

        If CalibrationLabels.Count = 0 Then
            PopulateCalibration()
        End If

    End Sub

    Private Sub PopulateLocations()
        Dim lLabel As Label
        Dim lCheckbox As CheckBox
        Dim lTextBox As TextBox
        Dim lIndex As Integer = 0
        For Each lTextBox In LocationTextboxes
            lLabel = LocationLabels.Item(lIndex)
            lCheckbox = LocationCheckboxes(lIndex)
            If lTextBox.Equals(txtLocationID1) OrElse
                lTextBox.Equals(txtLocationID2) Then
                lLabel.Visible = False
                lCheckbox.Visible = False
                lTextBox.Visible = False
            Else
                RemoveHandler lTextBox.TextChanged, AddressOf txtLocationID_TextChanged
                grpMapLocations.Controls.Remove(lLabel)
                grpMapLocations.Controls.Remove(lCheckbox)
                grpMapLocations.Controls.Remove(lTextBox)
                lLabel.Dispose()
                lCheckbox.Dispose()
                lTextBox.Dispose()
            End If
            lIndex += 1
        Next
        LocationLabels.Clear()
        LocationCheckboxes.Clear()
        LocationTextboxes.Clear()

        Dim lLabelTop As Integer = lblLocation1.Top
        Dim lCheckboxTop As Integer = chkLocation1.Top
        Dim lComboTop As Integer = txtLocationID1.Top
        For Each lLocation As String In TimeseriesGroupToSave.SortedAttributeValues("Location", "<missing>")
            Select Case LocationTextboxes.Count
                Case 0
                    lLabel = lblLocation1
                    lCheckbox = chkLocation1
                    lTextBox = txtLocationID1
                Case 1
                    lLabel = lblLocation2
                    lCheckbox = chkLocation2
                    lTextBox = txtLocationID2
                Case Else
                    lLabel = New Windows.Forms.Label()
                    grpMapLocations.Controls.Add(lLabel)
                    lLabel.Top = lLabelTop
                    lLabel.Left = lblLocation1.Left

                    lCheckbox = New Windows.Forms.CheckBox()
                    grpMapLocations.Controls.Add(lCheckbox)
                    lCheckbox.Top = lCheckboxTop
                    lCheckbox.Left = chkLocation1.Left

                    lTextBox = New Windows.Forms.TextBox()
                    grpMapLocations.Controls.Add(lTextBox)
                    lTextBox.Top = lComboTop
                    lTextBox.Left = txtLocationID1.Left
                    lTextBox.Width = txtLocationID1.Width

                    AddHandler lTextBox.TextChanged, AddressOf txtLocationID_TextChanged
            End Select
            lLabel.AutoSize = True
            lLabel.Text = lLocation
            lLabel.Visible = True
            lLabelTop += (lblLocation2.Top - lblLocation1.Top)
            lCheckboxTop += (chkLocation2.Top - chkLocation1.Top)
            lComboTop += (txtLocationID2.Top - txtLocationID1.Top)
            lTextBox.Visible = True

            'Dim lLastSelected As String = GetSetting(g_AppNameShort, "Locations", lConstituent, lConstituent).ToLower

            If Not grpMapLocations.Controls.Contains(lLabel) Then
                grpMapLocations.Controls.Add(lLabel)
            End If
            If Not grpMapLocations.Controls.Contains(lCheckbox) Then
                grpMapLocations.Controls.Add(lCheckbox)
            End If
            If Not grpMapLocations.Controls.Contains(lTextBox) Then
                grpMapLocations.Controls.Add(lTextBox)
            End If

            LocationLabels.Add(lLabel)
            LocationCheckboxes.Add(lCheckbox)
            LocationTextboxes.Add(lTextBox)
        Next
    End Sub

    Private Sub PopulateCalibration()
        Try
            Dim lLabel As Label
            Dim txtCoefficient As TextBox
            Dim txtNash As TextBox
            Dim lIndex As Integer = 0
            Dim lMaxLabelWidth As Integer = 0
            Dim lLabelTop As Integer = lblCalibration1.Top
            Dim lLabelTopDelta As Integer = lblCalibration2.Top - lblCalibration1.Top
            For Each lConstituent As String In modPublishGlobal.g_ConstituentsOfInterest
                Select Case lIndex
                    Case 0
                        lLabel = lblCalibration1
                    Case 1
                        lLabel = lblCalibration2
                    Case Else
                        lLabel = New Windows.Forms.Label()
                        lLabel.Top = lLabelTop
                        lLabel.Left = lblCalibration1.Left
                        grpMetadataCalibration.Controls.Add(lLabel)
                End Select
                lLabel.AutoSize = True
                lLabel.Text = lConstituent
                lLabel.Visible = True
                lLabelTop += lLabelTopDelta
                lMaxLabelWidth = Math.Max(lMaxLabelWidth, lLabel.Width)
                CalibrationLabels.Add(lLabel)
                lIndex += 1
            Next

            Dim lMargin As Integer = lblCalibration1.Left
            Dim ltxtCoefficientLeft As Integer = lMargin * 2 + lMaxLabelWidth
            Dim ltxtWidth As Integer = (grpMetadataCalibration.Width - ltxtCoefficientLeft - lMargin * 2) / 2
            Dim ltxtNashLeft As Integer = ltxtCoefficientLeft + ltxtWidth + lMargin
            Dim ltxtTop As Integer = txtCorrelationCoefficient1.Top
            Dim ltxtTopDelta As Integer = (txtCorrelationCoefficient2.Top - txtCorrelationCoefficient1.Top)

            lblCorrelationCoefficients.Left = ltxtCoefficientLeft
            lblNashSutcliffe.Left = ltxtNashLeft
            lIndex = 0
            For Each lConstituent As String In modPublishGlobal.g_ConstituentsOfInterest
                Select Case lIndex
                    Case 0
                        txtCoefficient = txtCorrelationCoefficient1
                        txtNash = txtNashSutcliffe1
                    Case 1
                        txtCoefficient = txtCorrelationCoefficient2
                        txtNash = txtNashSutcliffe2
                    Case Else
                        txtCoefficient = New Windows.Forms.TextBox()
                        grpMetadataCalibration.Controls.Add(txtCoefficient)

                        txtNash = New Windows.Forms.TextBox()
                        grpMetadataCalibration.Controls.Add(txtNash)
                End Select
                txtCoefficient.Top = ltxtTop
                txtCoefficient.Left = ltxtCoefficientLeft
                txtCoefficient.Width = ltxtWidth
                txtNash.Top = ltxtTop
                txtNash.Left = ltxtNashLeft
                txtNash.Width = ltxtWidth

                txtCoefficient.Visible = True
                txtNash.Visible = True

                CalibrationCoefficients.Add(txtCoefficient)
                CalibrationNash.Add(txtNash)

                ltxtTop += ltxtTopDelta
                lIndex += 1
            Next
            If ltxtTop > grpMetadataCalibration.Height Then
                Dim lGroupTop As Integer = grpMetadataCalibration.Top
                Me.Height += ltxtTop - grpMetadataCalibration.Height
                grpMetadataCalibration.Height = ltxtTop
                grpMetadataCalibration.Top = lGroupTop
                txtDescription.Height = Math.Max(txtOrganization.Height, lGroupTop - lMargin - txtDescription.Top)
            End If
        Catch ex As Exception
            Logger.Msg("Problem populating calibration interface:" & vbCrLf & ex.ToString, vbOKOnly, g_AppNameShort)
        End Try
    End Sub

    Private Sub txtLocationID_TextChanged(sender As Object, e As EventArgs) Handles txtLocationID1.TextChanged, txtLocationID2.TextChanged
        Dim lTextBox As TextBox = sender
        Dim lIndex As Integer = LocationTextboxes.IndexOf(lTextBox)
        If lIndex >= 0 Then
            LocationCheckboxes(lIndex).Checked = (lTextBox.Text.Trim.Length > 0)
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_ProgramDir = PathNameOnly(Reflection.Assembly.GetEntryAssembly.Location)
        Try
            Environment.SetEnvironmentVariable("PATH", g_ProgramDir & ";" & Environment.GetEnvironmentVariable("PATH"))
        Catch eEnv As Exception
        End Try
        If g_ProgramDir.EndsWith("bin") Then g_ProgramDir = PathNameOnly(g_ProgramDir)
        g_ProgramDir &= g_PathChar

        atcDataManager.Clear()
        With atcDataManager.DataPlugins
            .Add(New atcHspfBinOut.atcTimeseriesFileHspfBinOut)
            '.Add(New atcWdmVb.atcWDMfile)
            .Add(New atcWDM.atcDataSourceWDM)
            '.Add(New atcTimeseriesScript.atcTimeseriesScriptPlugin)
        End With

        atcTimeseriesStatistics.atcTimeseriesStatistics.InitializeShared()

        MetadataInfo.ModelType = "Unspecified"

    End Sub

    Private Function AskForZipFileNameToSaveIn() As String
        lblProgress.Text = "Saving Zip File"
        ShowGroup(grpProgress)
AskUser:
        Dim lSaveAsDialog As New Windows.Forms.SaveFileDialog()
        With lSaveAsDialog
            .Title = "Save in zip file"
            .Filter = "Zip Files|*.zip"
            .FilterIndex = 0
            .DefaultExt = ".zip"
            If .ShowDialog(Me) = DialogResult.OK Then
                If IO.File.Exists(.FileName) Then
                    Select Case MsgBox("File already exists, replace existing file?" & vbCrLf & .FileName, MsgBoxStyle.YesNoCancel, g_AppNameLong)
                        Case MsgBoxResult.Yes : If Not TryDelete(.FileName) Then GoTo AskUser
                        Case MsgBoxResult.No : GoTo AskUser
                        Case MsgBoxResult.Cancel : Throw New ApplicationException("Cancel")
                    End Select
                End If
                Return .FileName
            Else
                Throw New ApplicationException("Cancel")
            End If
        End With
    End Function

    'Private Sub btnSaveFiles_Click(sender As Object, e As EventArgs) Handles btnSaveFiles.Click
    '    Try
    '        Dim lZipFileName As String = AskForZipFileNameToSaveIn()
    '        barProgress.Minimum = 0
    '        barProgress.Value = 0
    '        barProgress.Maximum = lstInputFiles.CheckedItems.Count
    '        barProgress.Visible = True
    '        Using lZipArchive As ZipArchive = ZipFile.Open(lZipFileName, ZipArchiveMode.Create)
    '            AddMetadataToZipArchive(lZipArchive)
    '            For Each lAddFileName As String In lstInputFiles.CheckedItems
    '                ZipFileExtensions.CreateEntryFromFile(lZipArchive, lAddFileName, IO.Path.GetFileName(lAddFileName))
    '                barProgress.Value += 1
    '                barProgress.Refresh()
    '                Application.DoEvents()
    '            Next
    '        End Using
    '        lblProgress.Text = "Wrote " & lZipFileName
    '        barProgress.Visible = False
    '    Catch ex As Exception
    '        If ex.Message <> "Cancel" Then
    '            MsgBox("Error creating zip file: " & vbCrLf & ex.ToString)
    '        End If
    '    End Try
    'End Sub

    Private Sub AddMetadataToZipArchive(aZipArchive As ZipArchive)
        Dim lMetadataEntry As ZipArchiveEntry = aZipArchive.CreateEntry("Metadata.txt")
        Using lMetadataWriter As IO.StreamWriter = New IO.StreamWriter(lMetadataEntry.Open())

            Dim lConstituents As New atcCollection
            If TimeseriesGroupToSave IsNot Nothing AndAlso TimeseriesGroupToSave.Count > 0 Then
                For Each lTs As atcTimeseries In TimeseriesGroupToSave
                    lConstituents.Increment(lTs.Attributes.GetValue("Constituent"))
                Next
            End If
            'TODO: save metadata as defaults for next session
            lMetadataWriter.WriteLine("Author First: " & txtAuthor1.Text.Trim)
            lMetadataWriter.WriteLine("Author Middle: " & txtAuthor2.Text.Trim)
            lMetadataWriter.WriteLine("Author Last: " & txtAuthor3.Text.Trim)
            lMetadataWriter.WriteLine("Author Email: " & txtAuthorEmail.Text.Trim)
            Dim lPhoneNumber As String = ""
            For Each lCh As Char In txtAuthorPhone.Text
                If IsNumeric(lCh) Then lPhoneNumber &= lCh
            Next
            lMetadataWriter.WriteLine("Author Phone: " & lPhoneNumber)
            lMetadataWriter.WriteLine("Organization: " & txtOrganization.Text)
            lMetadataWriter.WriteLine("Date model executed: " & Format(MetadataInfo.ModelRunDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model Start Date: " & Format(MetadataInfo.ModelStartDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model End Date: " & Format(MetadataInfo.ModelEndDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model Name: " & MetadataInfo.ModelType)
            lMetadataWriter.WriteLine("Model version number: ")
            lMetadataWriter.WriteLine("Description: " & txtDescription.Text.Replace(vbCr, " ").Replace(vbLf, " ").Replace("  ", " "))
            lMetadataWriter.WriteLine("Constituents: " & String.Join(",", lConstituents.Keys.ToArray))

            Dim lCalibrationCount As Integer = Math.Min(CalibrationLabels.Count, CalibrationCoefficients.Count)
            lCalibrationCount = Math.Min(lCalibrationCount, CalibrationNash.Count)
            For lIndex As Integer = 0 To lCalibrationCount - 1
                If CalibrationCoefficients(lIndex).Text.Trim.Length > 0 Then
                    lMetadataWriter.WriteLine("Correlation Coefficient " & CalibrationLabels(lIndex).Text & ": " & CalibrationCoefficients(lIndex).Text.Trim)
                End If
                If CalibrationCoefficients(lIndex).Text.Trim.Length > 0 Then
                    lMetadataWriter.WriteLine("Nash-Sutcliffe " & CalibrationLabels(lIndex).Text & ": " & CalibrationNash(lIndex).Text.Trim)
                End If
            Next
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim lZipFileName As String = AskForZipFileNameToSaveIn()

            Dim lLocationMap As New atcCollection
            For lIndex As Integer = 0 To LocationTextboxes.Count - 1
                If LocationCheckboxes(lIndex).Checked Then
                    lLocationMap.Add(LocationLabels(lIndex).Text, LocationTextboxes(lIndex).Text)
                End If
                'SaveSetting(g_AppNameShort, "LocationAssociations", AssociateLabels(lIndex).Text, AssociateTextboxes(lIndex).Text)
            Next
            lblProgress.Text = "Saving " & lZipFileName
            ShowGroup(grpProgress)

            Dim lSaveFiles As List(Of String) = SelectedInputFiles()

            barProgress.Minimum = 0
            barProgress.Value = 0
            barProgress.Maximum = lSaveFiles.Count + TimeseriesGroupToSave.Count
            barProgress.Visible = True

            For Each lControl As Object In grpMetadata.Controls
                If lControl.GetType().Name = "TextBox" Then
                    Dim lTxt As TextBox = lControl
                    SaveSetting(g_AppNameShort, "Defaults", lTxt.Name, lTxt.Text)
                End If
            Next

            Dim lInputsZipFileName As String = GetTemporaryFileName(IO.Path.ChangeExtension(lZipFileName, ".inputs"), ".zip")
            Using lZipStream As IO.FileStream = New IO.FileStream(lInputsZipFileName, IO.FileMode.CreateNew)
                Using lZipArchive As ZipArchive = New ZipArchive(lZipStream, ZipArchiveMode.Create)
                    AddMetadataToZipArchive(lZipArchive)
                    For Each lAddFileName As String In lSaveFiles
                        ZipFileExtensions.CreateEntryFromFile(lZipArchive, lAddFileName, IO.Path.GetFileName(lAddFileName))
                        barProgress.Value += 1
                        barProgress.Refresh()
                        Application.DoEvents()
                    Next
                End Using
            End Using

            Using lZipStream As IO.FileStream = New IO.FileStream(lZipFileName, IO.FileMode.CreateNew)
                Using lZipArchive As ZipArchive = New ZipArchive(lZipStream, ZipArchiveMode.Create)
                    AddMetadataToZipArchive(lZipArchive)
                    ZipFileExtensions.CreateEntryFromFile(lZipArchive, lInputsZipFileName, "inputs.zip")
                    For Each lTs As atcTimeseries In TimeseriesGroupToSave
                        Dim lConstituent As String = lTs.Attributes.GetValue("Constituent")
                        Dim lLocation = lTs.Attributes.GetValue("Location")
                        Dim lNHDID As String = lLocationMap.ItemByKey(lLocation)
                        If lNHDID = "<none>" Then Throw New ApplicationException("Missing NHD ID for " & lLocation)
                        lTs.Attributes.SetValue("NHDReachCode", lNHDID)
                        Dim lNewEntry As ZipArchiveEntry = lZipArchive.CreateEntry(lConstituent & "-" & lNHDID & ".csv")
                        Using lEntryWriter As New IO.StreamWriter(lNewEntry.Open())
                            WriteAttributes(lTs, lEntryWriter)
                            WriteValues(lTs, lEntryWriter)
                        End Using

                        barProgress.Value += 1
                        barProgress.Refresh()
                        Application.DoEvents()
                    Next
                    lZipArchive.Dispose()
                End Using
            End Using
            TryDelete(lInputsZipFileName)
            lblProgress.Text = "Wrote " & lZipFileName
            barProgress.Visible = False
            btnClose.Visible = True
        Catch ex As Exception
            If ex.Message <> "Cancel" Then
                MsgBox("Error creating zip file: " & vbCrLf & ex.ToString)
            End If
        End Try
    End Sub

    Private Sub btnNHDPlusNext_Click(sender As Object, e As EventArgs) Handles btnNHDPlusNext.Click
        ShowGroup(grpMetadata)
    End Sub

    Private Sub btnNHDLookup_Click(sender As Object, e As EventArgs) Handles btnNHDLookup.Click
        'If MsgBox("Zoom to your area of interest by selecting a state and county (optional) from the dropdown when prompted by EnviroAtlas." & vbCrLf & _
        '       "Click on 'Supplemental Maps' tab at the top of the map." & vbCrLf & _
        '       "Un-collapse 'Biophysical Data – Vector' panel from the list of supplemental maps." & vbCrLf & _
        '       "Check 'National Hydrography Dataset (NHD) – Medium' checkbox." & vbCrLf & _
        '       "You may have to zoom in further to see the streams network.", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
        '    OpenFile("http://enviroatlas.epa.gov/enviroatlas/InteractiveMapEntrance/InteractiveMap/index.html")
        'End If
        OpenFile("http://arcg.is/1CLI2YX")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub radioModelCalibratedYes_CheckedChanged(sender As Object, e As EventArgs)
    '    EnableCalibration(radioModelCalibratedYes.Checked)
    'End Sub

    'Private Sub EnableCalibration(aEnable As Boolean)
    '    lblCalibrationConstituents.Enabled = aEnable
    '    lblCorrelationCoefficients.Enabled = aEnable
    '    lblNashSutcliffe.Enabled = aEnable
    '    txtCalibrationConstituents.Enabled = aEnable
    '    txtCorrelationCoefficient1.Enabled = aEnable
    '    txtNashSutcliffe1.Enabled = aEnable
    'End Sub

End Class
