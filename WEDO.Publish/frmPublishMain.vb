Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports System.IO.Compression
Imports System.IO.Compression.ZipArchive
Imports System.IO.Compression.ZipFile

Public Class frmPublishMain

    Private AllGroups As New List(Of GroupBox)
    Private TimeseriesGroupToSave As atcTimeseriesGroup

    Private Sub btnHSPF_Click(sender As Object, e As EventArgs) Handles btnHSPF.Click
        MetadataInfo.PublishingModelType = "HSPF"
        OpenUCI()
    End Sub

    Private Sub btnSWAT_Click(sender As Object, e As EventArgs) Handles btnSWAT.Click
        MetadataInfo.PublishingModelType = "SWAT"
        ShowGroup(grpChooseFiles)
    End Sub

    Private Sub AddFilesToList(aFiles As IEnumerable(Of String), lst As Windows.Forms.CheckedListBox)
        For Each lFileName In aFiles
            If Not lst.Items.Contains(lFileName) Then
                lst.Items.Add(lFileName, IO.File.Exists(lFileName))
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
            lstInputFiles.Items.Clear()
            lstInputFiles.Items.Add(UCIFilename, True)
            AddFilesToList(FilesInUCI(True, False, False, False, False), lstInputFiles)

            lstOutputFiles.Items.Clear()
            AddFilesToList(FilesInUCI(True, True, False, False, True), lstOutputFiles)

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
    End Sub

    Private Sub frmPublishMain_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = Windows.Forms.DragDropEffects.None
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
    End Sub

    Private Sub grpChooseModel_DragDrop(sender As Object, e As DragEventArgs) Handles grpChooseModel.DragDrop
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            Dim lAllFiles As List(Of String) = GetAllDroppedFilenames(e.Data.GetData(Windows.Forms.DataFormats.FileDrop))
            If lAllFiles.Count = 1 AndAlso _
               IO.Path.GetExtension(lAllFiles(0)).ToLowerInvariant().Equals(".uci") Then
                OpenUCI(lAllFiles(0))
            Else
                For Each lFileName As String In lAllFiles
                    lstInputFiles.Items.Add(lFileName)
                Next
                ShowGroup(grpChooseFiles)
            End If
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

    Private Sub grpChooseModel_DragEnter(sender As Object, e As DragEventArgs) Handles grpChooseModel.DragEnter
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            e.Effect = Windows.Forms.DragDropEffects.All
        End If
    End Sub

    Private Sub btnChooseFilesNext_Click(sender As Object, e As EventArgs) Handles btnChooseFilesNext.Click
        If MetadataInfo.ModelRunDate < #1/1/1920# Then
            For Each lDataFileName As String In lstInputFiles.CheckedItems
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
                atcDataManager.OpenDataSource(lDataFileName)
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
            PopulateMetadata()
            ShowGroup(grpMetadata)
        End If
    End Sub

    Dim AssociateLabels As New Generic.List(Of Label)
    Dim AssociateCheckboxes As New Generic.List(Of CheckBox)
    Dim AssociateTextboxes As New Generic.List(Of TextBox)

    Private Sub PopulateMetadata()
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
    End Sub

    Private Sub PopulateLocations()
        Dim lLabel As Label
        Dim lCheckbox As CheckBox
        Dim lTextBox As TextBox
        Dim lIndex As Integer = 0
        For Each lTextBox In AssociateTextboxes
            lLabel = AssociateLabels.Item(lIndex)
            lCheckbox = AssociateCheckboxes(lIndex)
            If lTextBox.Equals(txtLocationID1) OrElse _
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
        AssociateLabels.Clear()
        AssociateCheckboxes.Clear()
        AssociateTextboxes.Clear()

        Dim lLabelTop As Integer = lblLocation1.Top
        Dim lCheckboxTop As Integer = chkLocation1.Top
        Dim lComboTop As Integer = txtLocationID1.Top
        For Each lLocation As String In TimeseriesGroupToSave.SortedAttributeValues("Location", "<missing>")
            Select Case AssociateTextboxes.Count
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

            AssociateLabels.Add(lLabel)
            AssociateCheckboxes.Add(lCheckbox)
            AssociateTextboxes.Add(lTextBox)
        Next
    End Sub

    Private Sub txtLocationID_TextChanged(sender As Object, e As EventArgs) Handles txtLocationID1.TextChanged, txtLocationID2.TextChanged
        Dim lTextBox As TextBox = sender
        Dim lIndex As Integer = AssociateTextboxes.IndexOf(lTextBox)
        If lIndex >= 0 Then
            AssociateCheckboxes(lIndex).Checked = (lTextBox.Text.Trim.Length > 0)
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

        MetadataInfo.PublishingModelType = "Unspecified"

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
            lMetadataWriter.WriteLine("Author First: " & txtAuthor1.Text)
            lMetadataWriter.WriteLine("Author Middle: " & txtAuthor2.Text)
            lMetadataWriter.WriteLine("Author Last: " & txtAuthor3.Text)
            lMetadataWriter.WriteLine("Organization: " & txtOrganization.Text)
            If radioModelCalibratedYes.Checked Then
                lMetadataWriter.WriteLine("Calibration Constituents: " & txtCalibrationConstituents.Text)
                lMetadataWriter.WriteLine("CorrelationCoefficients: " & txtCorrelationCoefficients.Text)
                lMetadataWriter.WriteLine("Nash-Sutcliffe: " & txtNashSutcliffe.Text)

            End If
            lMetadataWriter.WriteLine("Date model executed: " & Format(MetadataInfo.ModelRunDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model Start Date: " & Format(MetadataInfo.ModelStartDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model End Date: " & Format(MetadataInfo.ModelEndDate, "yyyy/MM/dd HH:mm"))
            lMetadataWriter.WriteLine("Model Name: " & MetadataInfo.PublishingModelType)
            lMetadataWriter.WriteLine("Model version number: ")
            lMetadataWriter.WriteLine("Description: " & txtDescription.Text.Replace(vbCr, " ").Replace(vbLf, " ").Replace("  ", " "))
            lMetadataWriter.WriteLine("Constituents: " & String.Join(",", lConstituents.Keys.ToArray))

        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim lZipFileName As String = AskForZipFileNameToSaveIn()

            Dim lLocationMap As New atcCollection
            For lIndex As Integer = 0 To AssociateTextboxes.Count - 1
                If AssociateCheckboxes(lIndex).Checked Then
                    lLocationMap.Add(AssociateLabels(lIndex).Text, AssociateTextboxes(lIndex).Text)
                End If
                'SaveSetting(g_AppNameShort, "LocationAssociations", AssociateLabels(lIndex).Text, AssociateTextboxes(lIndex).Text)
            Next
            lblProgress.Text = "Saving " & lZipFileName
            ShowGroup(grpProgress)
            barProgress.Minimum = 0
            barProgress.Value = 0
            barProgress.Maximum = lstInputFiles.CheckedItems.Count + TimeseriesGroupToSave.Count
            barProgress.Visible = True

            Dim lInputsZipFileName As String = GetTemporaryFileName(IO.Path.ChangeExtension(lZipFileName, ".inputs"), ".zip")
            Using lZipStream As IO.FileStream = New IO.FileStream(lInputsZipFileName, IO.FileMode.CreateNew)
                Using lZipArchive As ZipArchive = New ZipArchive(lZipStream, ZipArchiveMode.Create)
                    AddMetadataToZipArchive(lZipArchive)
                    For Each lAddFileName As String In lstInputFiles.CheckedItems
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
        PopulateMetadata()
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

    Private Sub radioModelCalibratedYes_CheckedChanged(sender As Object, e As EventArgs) Handles radioModelCalibratedYes.CheckedChanged
        EnableCalibration(radioModelCalibratedYes.Checked)
    End Sub

    Private Sub EnableCalibration(aEnable As Boolean)
        lblCalibrationConstituents.Enabled = aEnable
        lblCorrelationCoefficients.Enabled = aEnable
        lblNashSutcliffe.Enabled = aEnable
        txtCalibrationConstituents.Enabled = aEnable
        txtCorrelationCoefficients.Enabled = aEnable
        txtNashSutcliffe.Enabled = aEnable
    End Sub

End Class
