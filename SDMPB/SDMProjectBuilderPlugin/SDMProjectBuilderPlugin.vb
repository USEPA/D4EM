Imports System
Imports System.Windows.Forms
Imports System.IO

Imports DotSpatial.Controls
Imports DotSpatial.Controls.Header
Imports DotSpatial.Controls.Docking
Imports DotSpatial.Symbology


'Public Class SDMProjectBuilderPlugin
'Inherits Extension
Public Class SDMProjectBuilderPlugin


    Dim mapFrame As IMapFrame
    Dim mapCtl As Map

    Private Const NationalProjectFilename As String = "ProjectBuilder.dspx"

    'Public Overrides Sub Activate()
    Public Sub Activate(App As AppManager, CachePath As String)
        Dim ScriptEditorMenuKey As String = "kSDMProjectBuilderPlugin"

        g_AppManager = App
        g_Map = g_AppManager.Map

        'App.HeaderControl.Add(New RootItem(ScriptEditorMenuKey, "SDM Project Builder"))

        ''App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Project Builder", New EventHandler(AddressOf btnSample_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Open National Project", New EventHandler(AddressOf mnuFileOpenNationalProject_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Nav Helper", New EventHandler(AddressOf mnuNavHelper_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Build SDM Project (new)", New EventHandler(AddressOf mnuFileSpecify_Click)))
        ''App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Build SDM Project (old)", New EventHandler(AddressOf mnuFileBuild_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "New DotSpatial Project", New EventHandler(AddressOf mnuFileNewProject_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Open Project", New EventHandler(AddressOf mnuFileOpenProject_Click)))
        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Save Project", New EventHandler(AddressOf mnuFileSaveProject_Click)))
        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Save Project As...", New EventHandler(AddressOf mnuFileSaveProjectAs_Click)))

        'App.HeaderControl.Add(New SimpleActionItem(ScriptEditorMenuKey, "Print Layout", New EventHandler(AddressOf mnuFilePrintLayout_Click)))

        SDM_Project_Builder_Batch.StartStatusMonitor()

        LoadNationalProject(g_AppManager.SerializationManager.CurrentProjectFile, CachePath)

        AddHandler App.Map.SelectionChanged, AddressOf LayerSelectionChanged

        'AddHandler App.Map.Layers.LayerSelected, AddressOf LayerSelected
    End Sub

    'Public Overrides Sub Deactivate()
    '    'App.HeaderControl.RemoveAll()
    'End Sub

#Region "Menus"

    'Private Sub mnuFileOpenNationalProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ofd As OpenFileDialog = New OpenFileDialog()
    '    ofd.FileName = NationalProjectFilename
    '    ofd.Multiselect = False
    '    ofd.Filter = "DotSpatial Project Files (.dspx)|*.dspx"
    '    ofd.Title = "Select SDMProjectBuilder National File"
    '    If (ofd.ShowDialog() = DialogResult.Cancel) Then
    '        Return
    '    End If
    '    LoadNationalProject(ofd.FileName)
    'End Sub

    'Private Sub mnuFileSpecify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ShowSpecifyForm()
    'End Sub

    'Private Sub mnuNavHelper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim frmNH As New frmNavHelper(App.Map)
    '    frmNH.Show()
    'End Sub

    'Private Sub mnuFileNewProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If MessageBox.Show("Save existing project", "Confirm new map", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '        g_Map.ClearLayers()
    '    End If
    'End Sub

    'Private Sub mnuFileOpenProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If g_Map.Layers.Count > 0 AndAlso MessageBox.Show("Clear existing project", "Confirm new map", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Exit Sub

    '    Dim dlg As New OpenFileDialog
    '    'dlg.Filter = DotSpatial.Controls.SerializationManager.OpenDialogFilterText

    '    If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
    '    Try
    '        g_AppManager.SerializationManager.OpenProject(dlg.FileName)
    '        g_Map.Invalidate()
    '    Catch ex As IOException
    '        MessageBox.Show("Could not open the project " + dlg.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Xml.XmlException
    '        MessageBox.Show("Failed to read the project " + dlg.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub mnuFileSaveProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim lSaveAs As String = g_AppManager.SerializationManager.CurrentProjectFile
    '    If String.IsNullOrEmpty(lSaveAs) Then
    '        Dim lSaveDialog As New Windows.Forms.SaveFileDialog
    '        With lSaveDialog
    '            .Title = "Save Project As..."
    '            .Filter = g_AppManager.SerializationManager.SaveDialogFilterFormat
    '            .FileName = g_AppManager.SerializationManager.SaveDialogFilterText
    '            If .ShowDialog = System.Windows.Forms.DialogResult.Cancel Then Exit Sub
    '            lSaveAs = .FileName
    '        End With
    '    End If
    '    If Not String.IsNullOrEmpty(lSaveAs) Then
    '        g_AppManager.SerializationManager.SaveProject(lSaveAs)
    '    End If
    'End Sub

    'Private Sub mnuFileSaveProjectAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim dlg As New SaveFileDialog
    '    'dlg.Filter = DotSpatial.Controls.SerializationManager.SaveDialogFilterText

    '    If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

    '    Try
    '        g_AppManager.SerializationManager.SaveProject(dlg.FileName)
    '    Catch ex As Xml.XmlException
    '        MessageBox.Show("Failed to save the project. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As IOException
    '        MessageBox.Show("Failed to save the project. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub mnuFilePrintLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim layoutForm As New DotSpatial.Controls.LayoutForm
    '    layoutForm.ShowDialog()
    'End Sub

    ''Private Sub toolStripMain_PrintClicked(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles toolStripMain.PrintClicked
    ''    Dim layoutForm As New DotSpatial.Controls.LayoutForm
    ''    layoutForm.ShowDialog()
    ''End Sub

#End Region 'Menus

    'Public Sub LoadNationalProject(Optional ByVal NationalProjectFullPath As String = "")
    Public Sub LoadNationalProject(Optional ByVal NationalProjectFullPath As String = "", Optional ByVal CacheFolder As String = "")

        If Not NationalProjectIsOpen() Then
            'If Not FileExists(NationalProjectFullPath) Then
            If Not File.Exists(NationalProjectFullPath) Then
                NationalProjectFullPath = atcUtility.FindFile("Open " & NationalProjectFilename,
                                                              IO.Path.Combine(CurDir, "NationalData", NationalProjectFilename))
            End If
            If File.Exists(NationalProjectFullPath) Then  'load national project
                g_AppManager.SerializationManager.OpenProject(NationalProjectFullPath)

            Else
                'Logger.Msg("Unable to find '" & NationalProjectFilename & "'", "LoadNationalProject")
                Exit Sub
            End If
        End If

        If NationalProjectIsOpen() Then

            For Each lLayer In g_Map.GetAllLayers
                Try 'Save locations of national layers in D4EM.Data.National.ShapeFilename collection
                    Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lLayer
                    Dim lLayerFilename As String = DotSpatialDataSetFilename(lFeatureLayer.DataSet)
                    For Each lNationalLayer In D4EM.Data.National.LayerSpecificationsAll
                        If lLayerFilename.ToLower.EndsWith(lNationalLayer.FilePattern) Then
                            D4EM.Data.National.ShapeFilename(lNationalLayer) = lLayerFilename
                        End If
                    Next
                Catch 'Ignore Exceptions, probably from layer not being shapefile or not having file name
                End Try
            Next

            'Select HUC-12 layer by default, or HUC-8 if no HUC-12 is on map
            Dim lHuc12Layer As DotSpatial.Symbology.ILayer = Huc12Layer()
            If lHuc12Layer IsNot Nothing Then
                SelectLayer(lHuc12Layer)
            Else
                Dim lHuc8Layer As DotSpatial.Symbology.ILayer = Huc8Layer()
                If lHuc8Layer IsNot Nothing Then
                    SelectLayer(lHuc8Layer)
                End If
            End If

            Dim CacheFullPath As String = ""
            'SDM_Project_Builder_Batch.modDownloadD4EM.EnsureCacheFolderSet(Path.GetDirectoryName(NationalProjectFullPath), CacheFullPath)
            'KW - pass in the cache folder.
            CacheFullPath = CacheFolder

            g_NationalProject = New D4EM.Data.Project(aDesiredProjection:=g_Map.Projection,
                                                      aCacheFolder:=CacheFullPath,
                                                      aProjectFolder:=(IO.Path.GetDirectoryName(NationalProjectFullPath)),
                                                      aRegion:=New D4EM.Data.Region(g_Map.ViewExtents.MaxY, g_Map.ViewExtents.MinY, g_Map.ViewExtents.MinX, g_Map.ViewExtents.MaxX, g_Map.Projection),
                                                      aClip:=False, aMerge:=False)
            params.ProjectsPath = IO.Path.GetDirectoryName(g_NationalProject.ProjectFolder)
            g_NationalProject.ProjectFilename = NationalProjectFullPath

            AddHandler g_NationalProject.Layers.CollectionChanged, AddressOf Layers_AddingNew
            Dim lParametersFilename As String = IO.Path.Combine(IO.Path.GetDirectoryName(NationalProjectFullPath), PARAMETER_FILE)
            If IO.File.Exists(lParametersFilename) Then
                params.XML = IO.File.ReadAllText(IO.Path.Combine(IO.Path.GetDirectoryName(NationalProjectFullPath), PARAMETER_FILE))
            Else
                'TODO: set defaults in params? For now we just use defaults built into SDMParameters.
            End If

            If params.Projects.Count = 0 Then
                params.Projects.Add(New D4EM.Data.Project(aDesiredProjection:=D4EM.Data.Globals.AlbersProjection,
                                                          aCacheFolder:=CacheFullPath,
                                                          aProjectFolder:=g_NationalProject.ProjectFolder,
                                                          aRegion:=Nothing, aClip:=True, aMerge:=False))
            Else
                For Each lProject In params.Projects
                    If lProject.DesiredProjection Is Nothing Then lProject.DesiredProjection = D4EM.Data.Globals.AlbersProjection
                    If Not IO.Directory.Exists(lProject.CacheFolder) Then lProject.CacheFolder = CacheFullPath
                Next
            End If
        Else
            MapWinUtility.Logger.Dbg("Unable to open national project")
        End If
    End Sub

    Function GetFilename(ByVal aLayer As DotSpatial.Controls.IMapLayer) As String
        Dim fs As DotSpatial.Data.IFeatureSet = aLayer.DataSet
        If (fs IsNot Nothing) Then ' is a vector layer
            Return fs.Filename
        End If

        Dim r As DotSpatial.Data.IRaster = aLayer.DataSet
        If r IsNot Nothing Then    ' is a raster layer.
            Return r.Filename
        End If

        Dim id As DotSpatial.Data.IImageData = aLayer.DataSet
        If id IsNot Nothing Then   ' is an image layer
            Return id.Filename
        End If
        Return ""
    End Function

    Public Sub ShowSpecifyForm()
        pFrmSpecifyProject = New frmSpecifyProject
        pFrmSpecifyProject.Show()
        pFrmSpecifyProject.Initialize()
        g_Map.FunctionMode = DotSpatial.Controls.FunctionMode.Select
    End Sub

    Private Sub LayerSelectionChanged(sender As Object, e As EventArgs)
        If FormIsOpen(pFrmSpecifyProject) Then pFrmSpecifyProject.UpdateSelectedFeatures()
    End Sub

End Class



