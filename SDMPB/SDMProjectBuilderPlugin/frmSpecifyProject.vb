Imports atcUtility

Public Class frmSpecifyProject

    Private SelectingPourPoint As Boolean = False
    Private SelectingCatchments As Boolean = False
    Private NoNCDCToken As String = "Enter NCDC Token Here"
    Private pMargin As Integer = Me.DefaultMargin.Horizontal
    Private Shared PourpointPreamble As String = "Pourpoint Watershed from NHDPlus" ' catchment containing:"
    Private pRemoveTheseLayers As New Generic.List(Of DotSpatial.Symbology.ILayer)
    Private ProjectName As String = Nothing

    Public Property SelectionLayerSpecification() As D4EM.Data.LayerSpecification
        Get
            If radioSelectCatchment.Checked Then
                Return D4EM.Data.Region.RegionTypes.catchment
            ElseIf radioSelectCounty.Checked Then
                Return D4EM.Data.Region.RegionTypes.county
            ElseIf radioSelectCurrent.Checked Then
                Return D4EM.Data.Region.RegionTypes.polygon
            ElseIf radioSelectHUC12.Checked Then
                Return D4EM.Data.Region.RegionTypes.huc12
            ElseIf radioSelectHUC8.Checked Then
                Return D4EM.Data.Region.RegionTypes.huc8
            ElseIf radioSelectBox.Checked Then
                Return D4EM.Data.Region.RegionTypes.box
            ElseIf radioSelectPourPoint.Checked Then
                Return D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed
            Else 'Should not get here, but default to box in case we do
                Return D4EM.Data.Region.RegionTypes.box
            End If
        End Get
        Set(ByVal value As D4EM.Data.LayerSpecification)
            Select Case value
                Case D4EM.Data.Region.RegionTypes.catchment : radioSelectCatchment.Checked = True
                Case D4EM.Data.Region.RegionTypes.county : radioSelectCounty.Checked = True
                Case D4EM.Data.Region.RegionTypes.polygon : radioSelectCurrent.Checked = True
                Case D4EM.Data.Region.RegionTypes.huc12 : radioSelectHUC12.Checked = True
                Case D4EM.Data.Region.RegionTypes.huc8 : radioSelectHUC8.Checked = True
                Case D4EM.Data.Region.RegionTypes.box : radioSelectBox.Checked = True
                Case D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed : radioSelectPourPoint.Checked = True
            End Select
        End Set
    End Property

    Public ReadOnly Property FlowUnits As Int16
        Get
            If rbtnFlowLinear.Checked Then
                Return 0
            ElseIf rbtnFlowLog.Checked Then
                Return 1
            Else
                Return Nothing
            End If
        End Get
    End Property




    Private Function ExistingOrSelectedRegion(ByVal aSelectionLayerSpecification As D4EM.Data.LayerSpecification,
                                              ByVal aNewKeys As Generic.List(Of String)) As D4EM.Data.Region
        'Try
        '    If params IsNot Nothing AndAlso params.Project IsNot Nothing AndAlso params.Project.Region IsNot Nothing Then
        '        Dim lExistingKeys = params.Project.Region.GetKeys(aSelectionLayerSpecification)
        '        If lExistingKeys.Count = aNewKeys.Count Then
        '            For Each lKey In lExistingKeys
        '                If Not aNewKeys.Contains(lKey) Then GoTo NoMatch
        '            Next
        '            Return params.Project.Region
        '        End If
        '    End If
        'Catch
        'End Try
NoMatch:
        Return New D4EM.Data.Region(aSelectionLayerSpecification, aNewKeys)
    End Function

    Private Function SelectedRegion() As D4EM.Data.Region
        Dim lSelectedText As String = txtSelected.Text
        If lSelectedText.Contains(":") Then
            lSelectedText = lSelectedText.Substring(lSelectedText.IndexOf(":") + 1).Trim
        End If

        Dim lSelectionLayerSpecification = SelectionLayerSpecification
        Select Case lSelectionLayerSpecification
            Case D4EM.Data.Region.RegionTypes.county
                Return ExistingOrSelectedRegion(lSelectionLayerSpecification, ParseNumericKeys(5, lSelectedText))
            Case D4EM.Data.Region.RegionTypes.polygon
                Return params.Project.Region
            Case D4EM.Data.Region.RegionTypes.catchment
                Return ExistingOrSelectedRegion(lSelectionLayerSpecification, ParseNumericKeys(0, lSelectedText))
            Case D4EM.Data.Region.RegionTypes.huc12
                Return ExistingOrSelectedRegion(lSelectionLayerSpecification, ParseNumericKeys(12, lSelectedText))
            Case D4EM.Data.Region.RegionTypes.huc8
                Return ExistingOrSelectedRegion(lSelectionLayerSpecification, ParseNumericKeys(8, txtSelected.Text))
            Case D4EM.Data.Region.RegionTypes.box
                If params.Project.Region IsNot Nothing AndAlso params.Project.Region.RegionSpecification = D4EM.Data.Region.RegionTypes.box Then
                    Return params.Project.Region 'Was already set in first step, do we want to update due to map changes now?
                Else
                    Dim lBox As New D4EM.Data.Region(g_Map.ViewExtents.MaxY, g_Map.ViewExtents.MinY, g_Map.ViewExtents.MinX, g_Map.ViewExtents.MaxX, g_Map.Projection)
                    Dim lNorth As Double, lSouth As Double, lWest As Double, lEast As Double
                    lBox.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                    Return New D4EM.Data.Region(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                End If
            Case D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed
                Return params.Project.Region
        End Select
        Return Nothing
    End Function

    Private Sub btnBuild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuild.Click
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildTop", Me.Top)
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildLeft", Me.Left)
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildWidth", Me.Width)
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildHeight", Me.Height)

        For Each lMapLayer In pRemoveTheseLayers
            Try
                g_Map.Layers.Remove(lMapLayer)
            Catch ex As Exception
                MapWinUtility.Logger.Dbg("Did not remove layer from map '" & lMapLayer.LegendText & "'")
            End Try
        Next

        g_AppManager.SerializationManager.SaveProject(g_AppManager.SerializationManager.CurrentProjectFile)

        g_AddLayers = chkAddLayers.Checked
        params.SetupHSPF = chkHSPF.Checked
        params.HspfSnowOption = Math.Max(0, comboHspfSnow.SelectedIndex)
        [Enum].TryParse(comboHspfOutputInterval.Text, params.HspfOutputInterval)
        params.HspfBacterialOption = chkMicrobes.Checked
        params.HspfChemicalOption = chkLandAppliedChemical.Checked
        params.HspfSegmentationOption = Math.Max(0, cboSegmentation.SelectedIndex)
        params.SetupSWAT = chkSWAT.Checked
        params.ReportFlowUnits = FlowUnits
        params.MinCatchmentKM2 = atxSize.ValueDouble
        params.MinFlowlineKM = atxLength.ValueDouble
        params.LandUseIgnoreBelowFraction = atxLU.ValueDouble
        params.SimulationStartYear = txtSimulationStartYear.ValueInteger
        params.SimulationEndYear = txtSimulationEndYear.ValueInteger
        params.SWATDatabaseName = lblSWAT2.Text

        If comboElevation.SelectedIndex >= 0 Then
            params.ElevationGrid = SDM_Project_Builder_Batch.SDMParameters.ElevationGridOptions(comboElevation.SelectedIndex)
        End If

        If comboDelineation.SelectedIndex >= 0 Then
            params.CatchmentsMethod = comboDelineation.Text
        End If

        Select Case SelectionLayerSpecification
            Case D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed
                'If params.Project.Region Is Nothing Then
                '    params.Project.Region = New D4EM.Data.Region( D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed, 
                'End If
                'Region already set
            Case D4EM.Data.Region.RegionTypes.box
                RemoveHandler g_Map.ViewExtentsChanged, AddressOf ViewExtentsChanged
        End Select

        Dim lProjectFileFullPath As String = txtSaveProjectAs.Text
        If String.IsNullOrWhiteSpace(lProjectFileFullPath) Then
            lProjectFileFullPath = params.NewProjectFileName()
        Else
            If Not lProjectFileFullPath.Contains(g_PathChar) Then
                lProjectFileFullPath &= g_PathChar & lProjectFileFullPath
            End If
            If Not lProjectFileFullPath.EndsWith(".mwprj") Then lProjectFileFullPath &= ".mwprj"
            If Not IO.Path.IsPathRooted(lProjectFileFullPath) Then
                lProjectFileFullPath = IO.Path.Combine(params.ProjectsPath, lProjectFileFullPath)
            End If
        End If
        params.Project.ProjectFilename = lProjectFileFullPath
        params.Project.ProjectFolder = IO.Path.GetDirectoryName(lProjectFileFullPath)
        params.ProjectsPath = IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(lProjectFileFullPath))
        params.BoundariesLatLongCsvFileName = IO.Path.Combine(params.Project.ProjectFolder, "LocalData", "BoundaryPointsLL.csv")
        params.OutputsLatLongCsvFileName = IO.Path.Combine(params.Project.ProjectFolder, "LocalData", "OutputPointsLL.csv")

        params.BasinsMetConstituents.Clear()
        If radioMetDataBASINS.Checked Then
            params.BasinsMetConstituents.AddRange("PREC ATEM SOLR CLOU WIND".Split(" "))
        End If

        If radioSoilSTATSGO.Checked Then
            params.SoilSource = SDM_Project_Builder_Batch.SDMParameters.SoilSources(0)
        Else
            params.SoilSource = SDM_Project_Builder_Batch.SDMParameters.SoilSources(1)
        End If

        params.NCDCconstituents.Clear()
        If radioMetDataNCDC.Checked Then
            params.NCDCconstituents.AddRange("AA1 TMP DEW GF1 WND".Split(" "))
            If Not String.IsNullOrEmpty(txtNCDCtoken.Text) AndAlso txtNCDCtoken.Text <> NoNCDCToken Then
                D4EM.Data.Source.NCDC.token = txtNCDCtoken.Text
            End If
            Try
                Dim lToken As String = D4EM.Data.Source.NCDC.token
                MapWinUtility.Logger.Dbg("Specified NCDC token '" & lToken & "'")
            Catch ex As Exception
                Dim lLinkStart As Integer = ex.Message.IndexOf("http:")
                If lLinkStart > 0 Then
                    Dim lWebsiteButton As String = "Visit NCDC Token Website"
                    If MapWinUtility.Logger.MsgCustom(ex.Message, "NCDC Token", "Ok", lWebsiteButton) = lWebsiteButton Then
                        OpenFile(ex.Message.Substring(lLinkStart))
                    End If
                Else
                    MapWinUtility.Logger.Msg("NCDC token must be specified to use NCDC met data" & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation, "NCDC Token")
                End If
            End Try
        End If

        params.NLDASconstituents.Clear()
        If radioMetDataNLDAS.Checked Then
            params.NLDASconstituents.Add("apcpsfc")
            params.TimeZoneShift = txtTimeZone.Text
        End If

        Me.Visible = False

        Dim NationalProjectFolder As String = g_AppManager.SerializationManager.CurrentProjectDirectory
        params.ProjectsPath = IO.Path.GetDirectoryName(NationalProjectFolder)
        IO.File.WriteAllText(IO.Path.Combine(NationalProjectFolder, PARAMETER_FILE), params.XML)

        g_Map.Layers.Clear()
        g_Map.Projection = params.DesiredProjection
        SpecifyAndCreateNewProjectsWithLayerCollectionChanged(params)
        Me.Close()
        'Exit SDM Project Builder, leaving it open causes trouble
        'System.Windows.Forms.Application.Exit()
    End Sub

    Private Function IsRegionSelected() As Boolean
        Dim lSelectedText As String = txtSelected.Text
        If lSelectedText.StartsWith(PourpointPreamble) Then
            Return True
        End If
        If lSelectedText.Contains(":") Then
            lSelectedText = lSelectedText.Substring(lSelectedText.IndexOf(":") + 1).Trim
        End If
        Select Case SelectionLayerSpecification
            Case D4EM.Data.Region.RegionTypes.catchment
                Return ParseNumericKeys(0, lSelectedText).Count > 0
            Case D4EM.Data.Region.RegionTypes.county
                Return ParseNumericKeys(5, lSelectedText).Count > 0 'Throw New ApplicationException("county Not yet implemented")
            Case D4EM.Data.Region.RegionTypes.polygon
                Return params.Project.Region IsNot Nothing ' Throw New ApplicationException("polygon Not yet implemented")
            Case D4EM.Data.Region.RegionTypes.huc12
                Return ParseNumericKeys(12, txtSelected.Text).Count > 0
            Case D4EM.Data.Region.RegionTypes.huc8
                Return ParseNumericKeys(8, txtSelected.Text).Count > 0
            Case D4EM.Data.Region.RegionTypes.box
                Return True
        End Select
        Return False
    End Function

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SelectingCatchments = False
        Dim lNewGroup As Windows.Forms.GroupBox = Nothing
        If groupSelectAreaOfInterest.Visible Then
            If Not IsRegionSelected() Then
                MapWinUtility.Logger.Msg("Area of interest must be selected before proceeding", Me.Text)
                Exit Sub
            End If
            lNewGroup = groupParameters
        ElseIf groupParameters.Visible Then
            lNewGroup = groupData
        Else 'Should not get here
            MapWinUtility.Logger.Msg("TODO: additional case needed in btnNext_Click")
            Exit Sub
        End If
        SetCurrentStep(lNewGroup)
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        SelectingCatchments = False
        Dim lNewGroup As Windows.Forms.GroupBox = Nothing
        If groupParameters.Visible Then
            lNewGroup = groupSelectAreaOfInterest
        ElseIf groupData.Visible Then
            lNewGroup = groupParameters
        Else 'Should not get here
            MapWinUtility.Logger.Msg("TODO: additional case needed in btnPrevious_Click")
            Exit Sub
        End If
        SetCurrentStep(lNewGroup)
    End Sub

    Private Sub SetCurrentStep(ByVal aGroup As Windows.Forms.GroupBox)
        If Me.Height < 400 Then Me.Height = 400
        If Me.Width < 700 Then Me.Width = 700

        btnCancel.Left = pMargin
        btnCancel.Top = Me.ClientSize.Height - btnCancel.Height - pMargin
        btnNext.Top = btnCancel.Top
        btnBuild.Top = btnCancel.Top
        btnPrevious.Top = btnCancel.Top
        btnNext.Left = Me.ClientSize.Width - btnNext.Width - pMargin
        btnBuild.Left = btnNext.Left
        btnPrevious.Left = btnNext.Left - btnPrevious.Width - pMargin

        aGroup.Left = pMargin
        aGroup.Top = pMargin
        aGroup.Width = Math.Max(Me.ClientSize.Width - pMargin * 2, 200)
        aGroup.Height = Math.Max(btnCancel.Top - pMargin * 2, 200)
        aGroup.Anchor = Windows.Forms.AnchorStyles.Bottom + Windows.Forms.AnchorStyles.Top _
                      + Windows.Forms.AnchorStyles.Left + Windows.Forms.AnchorStyles.Right
        aGroup.Visible = True

        If aGroup Is groupSelectAreaOfInterest Then
            groupCatchments.Visible = False
            groupParameters.Visible = False
            groupData.Visible = False

            btnNext.Visible = True
            btnPrevious.Visible = False
            btnBuild.Visible = False
            chkAddLayers.Visible = False

        ElseIf aGroup Is groupParameters Then
            params.Project.Region = SelectedRegion()

            groupSelectAreaOfInterest.Visible = False
            groupCatchments.Visible = False
            groupData.Visible = False

            btnNext.Visible = True
            btnPrevious.Visible = True
            btnBuild.Visible = False
            chkAddLayers.Visible = False

            btnSwatDatabase.Left = aGroup.Width - btnSwatDatabase.Width - 9
            lblSWAT2.Width = Math.Max(btnSwatDatabase.Left - lblSWAT2.Left - 9, pMargin)

        ElseIf aGroup Is groupData Then
            groupSelectAreaOfInterest.Visible = False
            groupCatchments.Visible = False
            groupParameters.Visible = False

            btnNext.Visible = False
            btnPrevious.Visible = True
            btnBuild.Visible = True
            chkAddLayers.Visible = True

            btnSaveProjectAs.Left = aGroup.Width - btnSaveProjectAs.Width - 9
            txtSaveProjectAs.Width = Math.Max(btnSaveProjectAs.Left - txtSaveProjectAs.Left - 9, pMargin)
            If radioSelectPourPoint.Checked AndAlso PourPointCOMID.Length > 0 Then
                ' txtSaveProjectAs.Text = params.NewProjectFileName(Nothing, "PourPoint" & PourPointCOMID())
                If (g_NationalProject Is Nothing) Then
                    txtSaveProjectAs.Text = params.NewProjectFileName
                Else
                    Dim mwProjName As String = g_NationalProject.ProjectFilename
                    mwProjName = System.IO.Path.ChangeExtension(mwProjName, "mwprj")
                    txtSaveProjectAs.Text = mwProjName
                End If
            Else
                If (g_NationalProject Is Nothing) Then
                    txtSaveProjectAs.Text = params.NewProjectFileName
                Else
                    Dim mwProjName As String = g_NationalProject.ProjectFilename
                    mwProjName = System.IO.Path.ChangeExtension(mwProjName, "mwprj")
                    txtSaveProjectAs.Text = mwProjName
                End If
            End If

        End If

    End Sub

    Private Sub chkHSPF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHSPF.CheckedChanged
        comboHspfSnow.Enabled = chkHSPF.Checked
    End Sub

    Private Sub chkSWAT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSWAT.CheckedChanged
        lblSWAT1.Enabled = chkSWAT.Checked
        lblSWAT2.Enabled = chkSWAT.Checked
        btnSwatDatabase.Enabled = chkSWAT.Checked
        rbtnFlowLinear.Enabled = chkSWAT.Checked
        rbtnFlowLog.Enabled = chkSWAT.Checked
        lFlowUnits.Enabled = chkSWAT.Checked
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Width = groupParameters.Width + 24 + Width - ClientRectangle.Width
        AddHandler g_Map.MouseClick, AddressOf MapMouseClick
    End Sub

    Public Sub New(sProjectName As String)
        Me.New()
        ProjectName = sProjectName
    End Sub

    Private Sub radioSelctionLayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        radioSelectHUC12.CheckedChanged, _
        radioSelectHUC8.CheckedChanged, _
        radioSelectCatchment.CheckedChanged, _
        radioSelectCounty.CheckedChanged, _
        radioSelectCurrent.CheckedChanged, _
        radioSelectPourPoint.CheckedChanged, _
        radioSelectBox.CheckedChanged

        If radioSelectBox.Checked Then
            AddHandler g_Map.ViewExtentsChanged, AddressOf ViewExtentsChanged
        Else
            RemoveHandler g_Map.ViewExtentsChanged, AddressOf ViewExtentsChanged
        End If

        UpdateSelectedFeatures()
    End Sub

    Private Function PourPointCOMID() As String
        Dim lKeys = params.Project.Region.GetKeys(D4EM.Data.Region.RegionTypes.polygon)
        If lKeys.Count > 0 Then Return lKeys(0)
        Return ""
    End Function

    Friend Sub UpdateSelectedFeatures()
        If NationalProjectIsOpen() Then
            PanelPourPoint.Visible = radioSelectPourPoint.Checked
            If radioSelectPourPoint.Checked Then
                Dim lClickInstruction As String
                If SelectingPourPoint Then
                    lClickInstruction = "Click the map " 'to select a pour point."
                Else
                    lClickInstruction = "Press the button below " 'to select a pour point."
                End If
                If radioSelectPourPoint.Checked AndAlso params.Project.Region IsNot Nothing AndAlso params.Project.Region.RegionSpecification = D4EM.Data.Region.RegionTypes.catchment AndAlso PourPointCOMID.Length > 0 Then
                    txtSelected.Text = PourpointPreamble & vbCrLf _
                                     & "COMID = " & PourPointCOMID() & vbCrLf _
                                     & lClickInstruction & "to select a different pour point."
                    'txtSelected.Text = PourpointPreamble & vbCrLf _
                    '                 & "Latitude: " & DoubleToString(params.PourPointLatitude) & vbCrLf _
                    '                 & "Longitude: " & DoubleToString(params.PourPointLongitude) & vbCrLf _
                    '                 & "Upstream: " & DoubleToString(params.PourPointMaxKm) & " km" & vbCrLf _
                    '                 & lClickInstruction & "to select a different pour point."
                Else
                    txtSelected.Text = lClickInstruction & "when ready to select a pour point."
                    g_Map.Cursor = System.Windows.Forms.Cursors.Cross
                End If

            ElseIf radioSelectBox.Checked Then
                Dim lBox As New D4EM.Data.Region(g_Map.ViewExtents.MaxY, g_Map.ViewExtents.MinY, g_Map.ViewExtents.MinX, g_Map.ViewExtents.MaxX, g_Map.Projection)
                Dim lNorth As Double, lSouth As Double, lWest As Double, lEast As Double
                lBox.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)

                Dim lArea As Double = (g_Map.ViewExtents.MaxY - g_Map.ViewExtents.MinY) * (g_Map.ViewExtents.MaxX - g_Map.ViewExtents.MinX)

                txtSelected.Text = "Selected bounding box:" & vbCrLf _
                                 & "Latitude: " & DoubleToString(lSouth, 5, , , , 4) & " to " & DoubleToString(lNorth, 5, , , , 4) & vbCrLf _
                                 & "Longitude: " & DoubleToString(lWest, 6, , , , 4) & " to " & DoubleToString(lEast, 6, , , , 4) & vbCrLf _
                                 & "Area: " & DoubleToString(lArea / 1000000) & " square km, " & DoubleToString(lArea / 2589990) & " square miles" & vbCrLf _
                                 & "Entire map extent is used as bounding box." & vbCrLf _
                                 & "Pan, Zoom, and resize the map window to select the desired area."

                'Save box parameters as geographic coordinates
                lBox = New D4EM.Data.Region(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                params.Project.Region = lBox '.XML.Replace(vbCr, "").Replace(vbLf, "")
            Else
                g_Map.Layers.SuspendEvents()
                g_Map.Cursor = System.Windows.Forms.Cursors.Default
                'Dim lLayer As DotSpatial.Symbology.ILayer = g_Map.Layers.SelectedLayer
                'Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = g_Map.Layers.SelectedLayer
                Dim lSelectionSpec = Me.SelectionLayerSpecification
                Dim lSelectionText As String = ""
                Dim lSelectionCount As Integer = 0
                Dim lFeature As DotSpatial.Data.IFeature = Nothing
                Dim lSelectionExtent As DotSpatial.Data.Extent = Nothing
                For Each lLayer In g_Map.GetAllLayers
                    Try
                        Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lLayer
                        Dim lLayerFilename As String = DotSpatialDataSetFilename(lFeatureLayer.DataSet)
                        If String.IsNullOrWhiteSpace(lLayerFilename) Then
                            Continue For
                        End If
                        Dim lSelectThisLayer As Boolean = IsSelectionLayer(lFeatureLayer)
                        Dim lLoadOtherLayer As Boolean = False
                        If Not lSelectThisLayer Then
                            If lLayerFilename.ToLower.EndsWith(D4EM.Data.National.LayerSpecifications.huc8.FilePattern) Then
                                Select Case lSelectionSpec
                                    Case D4EM.Data.Region.RegionTypes.huc12, D4EM.Data.Region.RegionTypes.catchment
                                        lLoadOtherLayer = True
                                End Select
                            End If
                        End If
                        If lSelectThisLayer OrElse lLoadOtherLayer Then
                            Dim lSelectedFeatures As Generic.List(Of DotSpatial.Data.IFeature) = lFeatureLayer.Selection.ToFeatureList
                            If lSelectedFeatures.Count > 0 Then
                                Dim lKeyFieldName As String = DBFKeyFieldName(lFeatureLayer.DataSet.Filename).ToLower
                                Dim lDescFieldName As String = DBFDescriptionFieldName(lFeatureLayer.DataSet.Filename).ToLower
                                Dim lKeyFieldIndex As Integer = -1
                                Dim lDescFieldIndex As Integer = -1
                                Dim lKey As String
                                Dim lDesc As String

                                For lField = 0 To lFeatureLayer.DataSet.DataTable.Columns.Count - 1
                                    Select Case lFeatureLayer.DataSet.DataTable.Columns(lField).ColumnName.ToLower
                                        Case lKeyFieldName : lKeyFieldIndex = lField
                                        Case lDescFieldName : lDescFieldIndex = lField
                                    End Select
                                Next

                                For Each lFeature In lFeatureLayer.Selection.ToFeatureList
                                    If lSelectionExtent Is Nothing Then
                                        lSelectionExtent = New DotSpatial.Data.Extent(lFeature.Envelope)
                                    Else
                                        lSelectionExtent.ExpandToInclude(New DotSpatial.Data.Extent(lFeature.Envelope))
                                    End If
                                    If lSelectionSpec = D4EM.Data.Region.RegionTypes.polygon Then
                                        'Selection on arbitrary layer, implemented as bounding box of all selected features
                                        'TODO: re-implement as multi-polygon and/or merged polygon of all selected features
                                    Else
                                        lKey = ""
                                        lDesc = ""
                                        If lKeyFieldIndex > -1 Then
                                            lKey = lFeature.DataRow.Item(lKeyFieldIndex)
                                            If lLoadOtherLayer Then
                                                Dim lLayerHandle = LoadLayerForHUC8(lSelectionSpec, lKey)
                                                If lLayerHandle IsNot Nothing Then
                                                    SelectLayer(lLayerHandle)
                                                    g_Map.ViewExtents = lLayerHandle.Extent
                                                End If
                                                GoTo LoadedOtherLayer
                                            End If
                                        End If

                                        If lDescFieldIndex > -1 Then
                                            lDesc = lFeature.DataRow.Item(lDescFieldIndex)
                                        End If
                                        'If Not String.IsNullOrEmpty(lKey) Then
                                        '    params.Project.Region = New D4EM.Data.Region(lSelectionSpec, lKey)
                                        '    params.SelectionLayer = lFeatureLayer.DataSet.Filename
                                        'End If
                                        If (lKey & lDesc).Length = 0 Then
                                            lSelectionText &= vbCrLf & "  " & lFeature.ToString
                                        Else
                                            lSelectionText &= vbCrLf & "  " & lKey
                                        End If
                                        If Not String.IsNullOrEmpty(lDesc) Then lSelectionText &= " : " & lDesc
                                        Debug.WriteLine("Selected: " & lSelectionText.Replace(vbCrLf, ""))
                                    End If
                                    lSelectionCount += 1
                                Next
                                If String.IsNullOrEmpty(lLayerFilename) Then
                                    lSelectionText &= vbCrLf & "  Note: layer must be saved as a file to use as project area"
                                Else
                                    'If params.SelectedKeys.Count > 0 Then
                                    '    Select Case lSelectionSpec 'Save some types as XML
                                    '        Case D4EM.Data.Region.RegionTypes.huc8,
                                    '             D4EM.Data.Region.RegionTypes.huc12,
                                    '             D4EM.Data.Region.RegionTypes.catchment,
                                    '             D4EM.Data.Region.RegionTypes.county,
                                    '             D4EM.Data.Region.RegionTypes.state
                                    '            params.Project.Region = New D4EM.Data.Region(Me.SelectionLayerSpecification, params.SelectedKeys)
                                    '            params.SelectedKeys.Clear()
                                    '    End Select
                                    'End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        MapWinUtility.Logger.Dbg("UpdateSelectedFeatures: " & ex.Message & vbCrLf & ex.StackTrace.ToString)
                    End Try
LoadedOtherLayer:
                Next
                Dim lBox As D4EM.Data.Region = Nothing
                Dim lGeographicBox As D4EM.Data.Region = Nothing 'Save box as geographic coordinates
                Dim lNorth As Double, lSouth As Double, lWest As Double, lEast As Double
                If lSelectionExtent IsNot Nothing Then
                    With lSelectionExtent
                        lBox = New D4EM.Data.Region(.MaxY, .MinY, .MinX, .MaxX, g_Map.Projection)
                        lBox.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                        lGeographicBox = New D4EM.Data.Region(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                        params.Project.Region = lGeographicBox
                    End With
                End If
                Select Case lSelectionSpec
                    Case D4EM.Data.Region.RegionTypes.huc8
                        If String.IsNullOrEmpty(lSelectionText) Then
                            lSelectionText = "Select HUC-8 on map, or enter HUC-8 here."
                        ElseIf lGeographicBox Is Nothing Then
                            params.Project.Region = New D4EM.Data.Region(lSelectionSpec, ParseNumericKeys(8, lSelectionText))
                        Else
                            params.Project.Region.SetKeys(lSelectionSpec, ParseNumericKeys(8, lSelectionText))
                            params.Project.Region.RegionSpecification = lSelectionSpec
                        End If
                    Case D4EM.Data.Region.RegionTypes.huc12, D4EM.Data.Region.RegionTypes.catchment
                        If String.IsNullOrEmpty(lSelectionText) Then
                            lSelectionText = "Select a HUC-8 to retrieve and display " & lSelectionSpec.Name & " for area selection, or enter IDs here."
                        ElseIf lGeographicBox Is Nothing Then
                            params.Project.Region = New D4EM.Data.Region(lSelectionSpec, ParseNumericKeys(0, lSelectionText))
                        Else
                            params.Project.Region.SetKeys(lSelectionSpec, ParseNumericKeys(0, lSelectionText))
                            params.Project.Region.RegionSpecification = lSelectionSpec
                        End If
                    Case D4EM.Data.Region.RegionTypes.polygon 'Selection on arbitrary layer
                        lSelectionText = ""
                        If lSelectionCount = 1 Then 'Can make region from single polygon
                            lSelectionText = "Selected one feature"
                            params.Project.Region = New D4EM.Data.Region(lFeature.ToShape, g_Map.Projection)
                        ElseIf lSelectionCount > 1 AndAlso lBox IsNot Nothing Then 'Make region a box covering the extents of all selected polygons
                            With lSelectionExtent
                                Dim lArea As Double = (.MaxY - .MinY) * (.MaxX - .MinX)
                                lSelectionText = "Features in bounding box:" & vbCrLf _
                                                 & "Latitude: " & DoubleToString(lSouth, 5, , , , 4) & " to " & DoubleToString(lNorth, 5, , , , 4) & vbCrLf _
                                                 & "Longitude: " & DoubleToString(lWest, 6, , , , 4) & " to " & DoubleToString(lEast, 6, , , , 4) & vbCrLf _
                                                 & "Area: " & DoubleToString(lArea / 1000000) & " square km, " & DoubleToString(lArea / 2589990) & " square miles"
                            End With
                        End If
                    Case D4EM.Data.Region.RegionTypes.county
                        If String.IsNullOrEmpty(lSelectionText) Then
                            lSelectionText = "Select a county on map, or enter 5-digit FIPS here."
                        ElseIf lGeographicBox Is Nothing Then
                            params.Project.Region = New D4EM.Data.Region(lSelectionSpec, ParseNumericKeys(5, lSelectionText))
                        Else
                            params.Project.Region.SetKeys(lSelectionSpec, ParseNumericKeys(5, lSelectionText))
                            params.Project.Region.RegionSpecification = lSelectionSpec
                        End If
                End Select
                If String.IsNullOrEmpty(lSelectionText) Then
                    txtSelected.Text = "Select in " & lSelectionSpec.Name & " layer on map."
                Else
                    txtSelected.Text = lSelectionCount & " Selected: " & lSelectionText
                End If
                g_Map.Layers.ResumeEvents()
            End If
        End If
    End Sub

    Private Function IsSelectionLayer(ByVal aLayer As DotSpatial.Symbology.ILayer) As Boolean
        Try
            Dim lLayer As DotSpatial.Symbology.IFeatureLayer = aLayer
            Select Case SelectionLayerSpecification
                Case D4EM.Data.Region.RegionTypes.polygon : Return True
                Case D4EM.Data.Region.RegionTypes.box : Return False
                Case Else
                    If String.IsNullOrEmpty(lLayer.DataSet.Filename) Then
                        Return False
                    End If

                    Return lLayer.DataSet.Filename.ToLower.EndsWith(SelectionLayerSpecification.FilePattern.ToLower)
            End Select
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PopulateCatchments()
        If NationalProjectIsOpen() Then
            g_Map.Layers.SuspendEvents()
            'Dim lCatcmentFeatureLayers As New Generic.List(Of DotSpatial.Symbology.IFeatureLayer)
            params.Catchments.Clear()
            Dim lNationalHuc8 As D4EM.Data.Layer = Nothing
            Dim lAllSelectedFeatures As New Generic.List(Of DotSpatial.Data.IFeature)
            Dim lSelectionExtent As DotSpatial.Data.Extent = Nothing
            For Each lMapLayer In g_Map.GetAllLayers
                Try
                    Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lMapLayer
                    Dim lFeatureSet As DotSpatial.Data.FeatureSet
                    If lFeatureLayer.DataSet.Filename.ToLower.EndsWith(D4EM.Data.National.LayerSpecifications.huc8.FilePattern) Then
                        lFeatureSet = lFeatureLayer.DataSet
                        lNationalHuc8 = D4EMLayerFindOrAdd(lFeatureSet, D4EM.Data.National.LayerSpecifications.huc8)
                    End If
                    If IsSelectionLayer(lFeatureLayer) Then
                        Dim lSelectedFeatures As Generic.List(Of DotSpatial.Data.IFeature) = lFeatureLayer.Selection.ToFeatureList
                        If lSelectedFeatures.Count > 0 Then
                            If lFeatureLayer.DataSet.Filename.EndsWith(D4EM.Data.Region.RegionTypes.catchment.FilePattern) Then
                                'lCatcmentFeatureLayers.Add(lFeatureLayer)
                                lFeatureSet = lFeatureLayer.DataSet
                                Dim lCatchmentLayer As D4EM.Data.Layer = D4EMLayerFindOrAdd(lFeatureSet, D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
                                If lCatchmentLayer.IdFieldIndex > -1 Then
                                    For Each lFeature In lFeatureLayer.Selection.ToFeatureList
                                        params.Catchments.Add(lFeature.DataRow(lCatchmentLayer.IdFieldIndex))
                                        If lSelectionExtent Is Nothing Then
                                            lSelectionExtent = New DotSpatial.Data.Extent(lFeature.Envelope)
                                        Else
                                            lSelectionExtent.ExpandToInclude(New DotSpatial.Data.Extent(lFeature.Envelope))
                                        End If
                                    Next
                                End If
                            Else
                                'Selection on arbitrary layer
                                For Each lFeature In lFeatureLayer.Selection.ToFeatureList
                                    lAllSelectedFeatures.Add(lFeature)
                                    If lSelectionExtent Is Nothing Then
                                        lSelectionExtent = New DotSpatial.Data.Extent(lFeature.Envelope)
                                    Else
                                        lSelectionExtent.ExpandToInclude(New DotSpatial.Data.Extent(lFeature.Envelope))
                                    End If
                                Next

                                'Dim path As String = "G:\temp\DotSpatialTest\"
                                'Dim f1 As String = path & "watershed.shp"
                                'Dim f2 As String = path & "huc250d3_reprojected.shp"

                                'Dim lWatershed As IMapLayer = App.Map.Layers.Add(f1)
                                'Dim lHucs As IMapLayer = App.Map.Layers.Add(f2)

                                'Dim implWatershed As IFeatureLayer = TryCast(lWatershed, IFeatureLayer)
                                'Dim iflHucs As IFeatureLayer = TryCast(lHucs, IFeatureLayer)

                                'Dim ifsWatershed As IFeatureSet = TryCast(implWatershed.DataSet, IFeatureSet)
                                'Dim ifsHucs As IFeatureSet = TryCast(lHucs.DataSet, IFeatureSet)


                                'Dim fWatershed As DotSpatial.Data.IFeature = ifsWatershed.Features(0)
                                'Dim shpWatershed As DotSpatial.Data.Shape = fWatershed.ToShape()
                                'Dim gWatershed As DotSpatial.Topology.IGeometry = shpWatershed.ToGeometry()

                                'For i As Integer = 0 To ifsHucs.ShapeIndices.Count - 1
                                '    Dim shape As DotSpatial.Data.ShapeRange = ifsHucs.ShapeIndices(i)
                                '    If shape.Intersects(shpWatershed) Then
                                '        iflHucs.[Select](i)
                                '    End If
                                'Next

                            End If
                        End If
                    End If
                Catch ex As Exception
                    MapWinUtility.Logger.Dbg("PopulateCatchments: " & ex.Message & vbCrLf & ex.StackTrace.ToString)
                End Try
            Next

            If lAllSelectedFeatures.Count > 0 AndAlso lNationalHuc8 IsNot Nothing AndAlso lNationalHuc8.IdFieldIndex > -1 Then
                'Ensure all NHDPlus catchment layers overlapping any selection are loaded
                For Each lSelectedFeature As DotSpatial.Data.IFeature In lAllSelectedFeatures
                    Dim lHUC8string As String = Nothing
                    Select Case SelectionLayerSpecification
                        Case D4EM.Data.Region.RegionTypes.huc8
                            lHUC8string = lSelectedFeature.DataRow(lNationalHuc8.IdFieldIndex)
                        Case D4EM.Data.Region.RegionTypes.huc12
                            lHUC8string = lSelectedFeature.DataRow(D4EM.Data.National.LayerSpecifications.huc12.IdFieldName).Substring(0, 8)
                            'Case D4EM.Data.Region.RegionTypes.catchment

                            'Case D4EM.Data.Region.RegionTypes.county

                            'Case D4EM.Data.Region.RegionTypes.polygon
                        Case Else
                            Dim lCentroid As DotSpatial.Topology.IPoint = lSelectedFeature.ToShape.ToGeometry.Centroid
                            Dim lHuc8Index As Integer = lNationalHuc8.CoordinatesInShapefile(lCentroid.X, lCentroid.Y)
                            If lHuc8Index >= 0 Then
                                lHUC8string = lNationalHuc8.AsFeatureSet.Features(lHuc8Index).DataRow(lNationalHuc8.IdFieldIndex)
                            End If
                    End Select

                    If Not String.IsNullOrEmpty(lHUC8string) Then
                        Dim lCatchmentMapLayer As DotSpatial.Symbology.IFeatureLayer = LoadLayerForHUC8(D4EM.Data.Region.RegionTypes.catchment, lHUC8string)
                        If lCatchmentMapLayer IsNot Nothing Then
                            'Select all catchments overlapping lSelectedFeature
                            Dim lFeatureSet As DotSpatial.Data.FeatureSet = lCatchmentMapLayer.DataSet
                            Dim lCatchmentLayer As D4EM.Data.Layer = D4EMLayerFindOrAdd(lFeatureSet, D4EM.Data.Region.RegionTypes.catchment)
                            Dim lIdFieldIndex As Integer = lCatchmentLayer.IdFieldIndex
                            If lIdFieldIndex >= 0 Then
                                If SelectionLayerSpecification = D4EM.Data.Region.RegionTypes.huc8 Then
                                    For i As Integer = lCatchmentMapLayer.DataSet.ShapeIndices.Count - 1 To 0 Step -1
                                        lCatchmentMapLayer.[Select](i)
                                        Dim lCOMID As String = lCatchmentMapLayer.DataSet.DataTable.Rows(i)(lIdFieldIndex)
                                        If Not params.Catchments.Contains(lCOMID) Then
                                            params.Catchments.Add(lCOMID)
                                        End If
                                    Next
                                Else
                                    params.Catchments = lCatchmentLayer.GetKeysOfOverlappingShapes(lSelectedFeature.ToShape, 0.04)
                                    For i As Integer = lCatchmentMapLayer.DataSet.ShapeIndices.Count - 1 To 0 Step -1
                                        If params.Catchments.Contains(lCatchmentMapLayer.DataSet.DataTable.Rows(i)(lIdFieldIndex).ToString) Then
                                            lCatchmentMapLayer.[Select](i)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            If lSelectionExtent IsNot Nothing Then
                lSelectionExtent.ExpandBy(lSelectionExtent.Width / 50)
                g_Map.ViewExtents = lSelectionExtent
            End If
            txtCatchments.Text = String.Join(vbCrLf, params.Catchments)
            g_Map.Layers.ResumeEvents()
        End If
    End Sub

    Public Sub Initialize()
        'Me.Icon = g_MapWin.ApplicationInfo.FormIcon
        Me.Text = "Build " & g_AppNameLong & " Project"

        chkHSPF.Checked = params.SetupHSPF
        comboHspfSnow.SelectedIndex = params.HspfSnowOption
        chkMicrobes.Checked = params.HspfBacterialOption
        chkLandAppliedChemical.Checked = params.HspfChemicalOption
        cboSegmentation.SelectedIndex = params.HspfSegmentationOption
        comboHspfOutputInterval.Text = [Enum].GetName(params.HspfOutputInterval.GetType, params.HspfOutputInterval)
        chkSWAT.Checked = params.SetupSWAT
        atxSize.ValueDouble = params.MinCatchmentKM2
        atxLength.ValueDouble = params.MinFlowlineKM
        atxLU.ValueDouble = params.LandUseIgnoreBelowFraction
        txtSimulationStartYear.ValueInteger = params.SimulationStartYear
        txtSimulationEndYear.ValueInteger = params.SimulationEndYear
        Select Case params.SoilSource
            Case "STATSGO" : radioSoilSTATSGO.Checked = True
            Case "SSURGO" : radioSoilSSURGO.Checked = True
        End Select
        lblSWAT2.Text = params.SWATDatabaseName

        comboElevation.Items.Clear()
        For Each lElevationOption In SDM_Project_Builder_Batch.SDMParameters.ElevationGridOptions
            comboElevation.Items.Add(lElevationOption.Name)
            If lElevationOption = params.ElevationGrid Then
                comboElevation.SelectedIndex = comboElevation.Items.Count - 1
            End If
        Next

        comboDelineation.Items.Clear()
        For Each lDelineationOption In SDM_Project_Builder_Batch.SDMParameters.CatchmentsMethods
            comboDelineation.Items.Add(lDelineationOption)
            If lDelineationOption = params.CatchmentsMethod Then
                comboDelineation.SelectedIndex = comboDelineation.Items.Count - 1
            End If
        Next
        'txtLatitude.Text = params.PourPointLatitude
        'txtLongitude.Text = params.PourPointLongitude

        txtUpstream.Text = GetSetting(g_AppNameShort, "Settings", "NumUpstreamCatchments", "1")
        'If Double.IsNaN(params.PourPointMaxKm) Then
        txtPourPointKm.Text = 20
        'Else
        'txtPourPointKm.Text = DoubleToString(params.PourPointMaxKm)
        'End If

        If params.Project.Region IsNot Nothing Then
            Dim lRegionSpec = params.Project.Region.RegionSpecification
            Select Case lRegionSpec
                Case D4EM.Data.Region.RegionTypes.huc8 : radioSelectHUC8.Checked = True : txtSelected.Text = KeysToSelectedText(params.Project.Region)
                Case D4EM.Data.Region.RegionTypes.huc12 : radioSelectHUC12.Checked = True : txtSelected.Text = KeysToSelectedText(params.Project.Region)
                Case D4EM.Data.Region.RegionTypes.county : radioSelectCounty.Checked = True : txtSelected.Text = KeysToSelectedText(params.Project.Region)
                Case D4EM.Data.Region.RegionTypes.catchment : radioSelectCatchment.Checked = True : txtSelected.Text = KeysToSelectedText(params.Project.Region)
                    'Case D4EM.Data.Region.RegionTypes.polygon : radioSelectPourPoint.Checked = True
            End Select
        End If
        If D4EM.Data.Source.NCDC.HasToken Then
            txtNCDCtoken.Text = D4EM.Data.Source.NCDC.token
        Else
            txtNCDCtoken.Text = NoNCDCToken
        End If
        chkAddLayers.Checked = g_AddLayers

        Me.Size = New System.Drawing.Size(700, 400)
        SetCurrentStep(groupSelectAreaOfInterest)
    End Sub

    Private Function KeysToSelectedText(ByVal aRegion As D4EM.Data.Region) As String
        Dim lKeys As Generic.List(Of String) = aRegion.GetKeys(aRegion.RegionSpecification)
        If lKeys IsNot Nothing AndAlso lKeys.Count > 0 Then
            Return String.Join(vbCrLf, lKeys.ToArray)
        End If
        Return txtSelected.Text
    End Function

    Private Sub btnUpstream_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpstream.Click
        Dim lMaxUpstream As Integer
        If Not Integer.TryParse(txtUpstream.Text.Trim, lMaxUpstream) Then
            MapWinUtility.Logger.Msg("Non-Integer: " & txtUpstream.Text, MsgBoxStyle.OkOnly, "Select Upstream")
        Else
            SaveSetting(g_AppNameShort, "Settings", "NumUpstreamCatchments", lMaxUpstream)
            g_Map.Layers.SuspendEvents()

            For Each lMapLayer In g_Map.GetAllLayers
                Try
                    Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lMapLayer
                    If lFeatureLayer.DataSet.Filename.EndsWith(D4EM.Data.Region.RegionTypes.catchment.FilePattern) Then
                        Dim lCatchmentsFeatureSet As DotSpatial.Data.FeatureSet = lFeatureLayer.DataSet
                        Dim lCatchmentLayer As D4EM.Data.Layer = D4EMLayerFindOrAdd(lCatchmentsFeatureSet, D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
                        'Dim lCatchmentLayer As D4EM.Data.Layer = g_NationalProject.LayerFromFileName(lFeatureLayer.DataSet.Filename)
                        'If lCatchmentLayer Is Nothing Then 'Layer was loaded directly to map, not retrieved by D4EM.Data.Source.NHDPlus
                        '    lCatchmentLayer = New D4EM.Data.Layer(lFeatureLayer.DataSet, D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
                        '    g_NationalProject.Layers.Add(lCatchmentLayer)
                        'End If
                        Dim lFlowlinesLayer = D4EMLayerFindOrAdd(lFeatureLayer.DataSet.Filename.Replace("catchment", "nhdflowline").Replace("drainage", "hydrography"), D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline)
                        Dim lPreSelected = lFeatureLayer.Selection.ToFeatureList

                        For Each lFeature In lPreSelected
                            'For Each lKey As String In ParseNumericKeys(0, txtCatchments.Text)
                            Dim lKey As String = lFeature.DataRow(lCatchmentLayer.IdFieldIndex)
                            Dim lUpstreamComIds As Generic.List(Of Long) = D4EM.Geo.NetworkOperations.FindUpstreamKeys(
                                                                                lKey,
                                                                                lFlowlinesLayer.AsFeatureSet,
                                                                                lFlowlinesLayer.IdFieldIndex,
                                                                                lFlowlinesLayer.FieldIndex("TOCOMID"), lMaxUpstream)
                            For Each lCOMID In lUpstreamComIds
                                lCatchmentsFeatureSet.SelectByAttribute("COMID=" & lCOMID)
                            Next

                            'Dim lPourpointWatershed As D4EM.Data.Region = GetNHDPlusPourpointWatershed(
                            '    lFlowlinesLayer.AsFeatureSet, lCatchmentLayer.AsFeatureSet,
                            '    , txtMaxKM.Text)

                            'Next
                        Next
                    End If
                Catch ex As Exception
                    MapWinUtility.Logger.Dbg("SelectUpstream: " & ex.Message)
                End Try
            Next
            g_Map.Layers.ResumeEvents()
        End If
    End Sub

    Private Sub ViewExtentsChanged(ByVal sender As Object, ByVal e As DotSpatial.Data.ExtentArgs)
        Try
            If Me.Visible AndAlso radioSelectBox.Checked AndAlso NationalProjectIsOpen() Then
                UpdateSelectedFeatures()
            End If
        Catch 'Ignore exceptions, only need this to work when it does
        End Try
    End Sub

    Private Sub MapMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If NationalProjectIsOpen() Then
            g_Map.Layers.SuspendEvents()
            If SelectingPourPoint AndAlso e.Button = System.Windows.Forms.MouseButtons.Left Then
                Try
                    SelectingPourPoint = False
                    btnSelectPourPoint.Visible = True
                    g_Map.Cursor = System.Windows.Forms.Cursors.WaitCursor
                    Dim lPourPointMaxKm = 0
                    If Not Double.TryParse(txtPourPointKm.Text, lPourPointMaxKm) Then
                        MapWinUtility.Logger.Dbg("Non-numeric pour point maximum km, defaulting to 20.")
                        lPourPointMaxKm = 20
                    End If

                    Dim lPointMapProjection As DotSpatial.Topology.Coordinate = g_Map.PixelToProj(New System.Drawing.Point(e.X, e.Y))
                    Dim lPourPointLatitude = lPointMapProjection.Y
                    Dim lPourPointLongitude = lPointMapProjection.X
                    D4EM.Geo.SpatialOperations.ProjectPoint(lPourPointLongitude, lPourPointLatitude, g_Map.Projection, DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984)

                    Dim pourPoint As D4EM.Data.Source.EPAWaters.PourPoint = Nothing
                    Dim lWatershedShapefileName As String
                    'See if we can find the pour point in a previously created watershed
                    'Decided this is problematic because different sized watersheds might contain the same pour point, so a large watershed starting downstream could give us the downstream catchment COMID instead of the one this point is in
                    'TODO: search NHDPlus catchments already on map instead of cached watersheds
                    'For Each lEPAWatersDir In IO.Directory.GetDirectories(IO.Path.Combine(g_NationalProject.ProjectFolder, "EPAWaters"))
                    '    For Each lWatershedShapefileName In IO.Directory.GetFiles(lEPAWatersDir, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed.FilePattern)
                    '        Dim lWatershedLayer As D4EM.Data.Layer = g_NationalProject.LayerFromFileName(lWatershedShapefileName)
                    '        If lWatershedLayer Is Nothing Then
                    '            Dim lMapLayer = MapLayer(lWatershedShapefileName)
                    '            If lMapLayer IsNot Nothing Then
                    '                lWatershedLayer = New D4EM.Data.Layer(lMapLayer.DataSet, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                    '                'lFeatureSet = DotSpatial.Data.FeatureSet.Open(lWatershedShapefileName)
                    '            End If
                    '        End If
                    '        If lWatershedLayer Is Nothing Then
                    '            lWatershedLayer = New D4EM.Data.Layer(DotSpatial.Data.FeatureSet.Open(lWatershedShapefileName), D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                    '        End If
                    '        Dim lShapeIndex As Integer = -1
                    '        If lWatershedLayer.PointInShape(lShapeIndex, lPointMapProjection.X, lPointMapProjection.Y) Then
                    '            pourPoint = New D4EM.Data.Source.EPAWaters.PourPoint()
                    '            pourPoint.COMID = lWatershedLayer.AsFeatureSet.Features(lShapeIndex).DataRow(lWatershedLayer.IdFieldIndex)
                    '            pourPoint.Latitude = params.PourPointLatitude
                    '            pourPoint.Longitude = params.PourPointLongitude
                    '        End If
                    '    Next
                    'Next
                    If pourPoint Is Nothing Then
                        Try
                            pourPoint = D4EM.Data.Source.EPAWaters.GetPourPoint(g_NationalProject.CacheFolder, lPourPointLatitude, lPourPointLongitude)
                            lPourPointLatitude = pourPoint.Latitude
                            lPourPointLongitude = pourPoint.Longitude
                        Catch
                            MapWinUtility.Logger.Dbg("Exception retrieving pour point from EPAWaters: " &
                                                     lPourPointLatitude.ToString("#.###") & "," &
                                                     lPourPointLongitude.ToString("#.###") & " " & DoubleToString(lPourPointMaxKm) & "km")
                            lPourPointLatitude = 44.094
                            lPourPointLongitude = -87.662
                            lPourPointMaxKm = 1000
                            MapWinUtility.Logger.Dbg("Defaulting to mouth of Manitowoc pour point at: " &
                                                     lPourPointLatitude.ToString("#.###") & "," &
                                                     lPourPointLongitude.ToString("#.###") & " 1000km")
                            pourPoint = D4EM.Data.Source.EPAWaters.GetPourPoint(g_NationalProject.CacheFolder, lPourPointLatitude, lPourPointLongitude)
                        End Try
                    End If

                    Dim lSaveFolder As String = IO.Path.Combine("EPAWaters", pourPoint.COMID, DoubleToString(lPourPointMaxKm) & "km")
                    lWatershedShapefileName = IO.Path.Combine(g_NationalProject.ProjectFolder, lSaveFolder, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed.FilePattern)
                    If Not IO.File.Exists(lWatershedShapefileName) Then
                        D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, lSaveFolder, pourPoint.COMID, lPourPointMaxKm, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                    End If
                    Dim lMapLayer = EnsureLayerOnMap(lWatershedShapefileName, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed.Name)
                    pRemoveTheseLayers.Add(lMapLayer)
                    params.Project.Region = New D4EM.Data.Region(New D4EM.Data.Layer(lMapLayer.DataSet, D4EM.Data.Region.RegionTypes.catchment), D4EM.Data.Region.MatchAllKeys)
                    params.Project.Region.SetKeys(D4EM.Data.Region.RegionTypes.polygon, {pourPoint.COMID})

                    Dim lCatchmentsShapefileName As String = IO.Path.Combine(g_NationalProject.ProjectFolder, lSaveFolder, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment.FilePattern)
                    Dim lCatchmentsLayer As D4EM.Data.Layer
                    If IO.File.Exists(lCatchmentsShapefileName) Then
                        lCatchmentsLayer = D4EMLayerFindOrAdd(lCatchmentsShapefileName, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment)
                    Else
                        lCatchmentsLayer = D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, lSaveFolder, pourPoint.COMID, lPourPointMaxKm, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment)
                    End If

                    'Copy COMID from catchments into params.Catchments
                    'If lCatchmentsLayer.IdFieldIndex >= 0 Then
                    '    params.SelectionLayer = lCatchmentsLayer.FileName
                    '    params.Catchments.Clear()
                    '    For Each lFeature In lCatchmentsLayer.AsFeatureSet.Features
                    '        params.Catchments.Add(lFeature.DataRow(lCatchmentsLayer.IdFieldIndex))
                    '    Next
                    'End If
                    lMapLayer = EnsureLayerOnMap(lCatchmentsShapefileName, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment.Name)
                    pRemoveTheseLayers.Add(lMapLayer)

                Catch ex As Exception

                Finally
                    g_Map.Cursor = Windows.Forms.Cursors.Default
                    UpdateSelectedFeatures()
                End Try
            ElseIf SelectingCatchments Then
                params.Catchments.Clear()
                For Each lMapLayer In g_Map.GetAllLayers
                    Try
                        Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lMapLayer
                        Dim lSelectedFeatures As Generic.List(Of DotSpatial.Data.IFeature) = lFeatureLayer.Selection.ToFeatureList
                        If lSelectedFeatures.Count > 0 Then
                            If lFeatureLayer.DataSet.Filename.EndsWith(D4EM.Data.Region.RegionTypes.catchment.FilePattern) Then
                                'lCatcmentFeatureLayers.Add(lFeatureLayer)
                                Dim lFeatureSet As DotSpatial.Data.FeatureSet = lFeatureLayer.DataSet
                                Dim lCatchmentLayer As D4EM.Data.Layer = D4EMLayerFindOrAdd(lFeatureSet, D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons)
                                If lCatchmentLayer.IdFieldIndex > -1 Then
                                    For Each lFeature In lFeatureLayer.Selection.ToFeatureList
                                        params.Catchments.Add(lFeature.DataRow(lCatchmentLayer.IdFieldIndex))
                                    Next
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        MapWinUtility.Logger.Dbg("PopulateCatchments: " & ex.Message & vbCrLf & ex.StackTrace.ToString)
                    End Try
                Next
                txtCatchments.Text = String.Join(vbCrLf, params.Catchments)
            End If
            g_Map.Layers.ResumeEvents()
        End If
    End Sub

    Private Sub btnSelectPourPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPourPoint.Click
        SelectingPourPoint = True
        g_Map.Cursor = System.Windows.Forms.Cursors.Cross
        btnSelectPourPoint.Visible = False
        txtSelected.Text = "Click pour point location on the map"
    End Sub

    Private Sub btnSaveProjectAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveProjectAs.Click
        Dim lSaveAs As New Windows.Forms.SaveFileDialog()
        With lSaveAs
            .Title = "Save new SDM Project As..."
            .FileName = txtSaveProjectAs.Text
            If String.IsNullOrEmpty(.FileName) Then .FileName = params.NewProjectFileName
            .Filter = "MapWindow Project (*.mwprj)|*.mwprj"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                params.Project.ProjectFilename = .FileName
                params.Project.ProjectFolder = IO.Path.GetDirectoryName(.FileName)
            End If
            txtSaveProjectAs.Text = params.Project.ProjectFilename
        End With
    End Sub

    Private Sub btnSwatDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwatDatabase.Click
        params.SWATDatabaseName = FindFile("Please locate SWAT2005.mdb", "SWAT2005.mdb", , , True).Replace("swat", "SWAT")
        lblSWAT2.Text = params.SWATDatabaseName
    End Sub

    'Friend pFrmMapBacteria As frmMapMicrobes
    'Private Sub btnMapMicrobes_Click(sender As System.Object, e As System.EventArgs) Handles btnMapMicrobes.Click
    '    If FormIsOpen(pFrmMapBacteria) Then
    '        pFrmMapBacteria.Activate()
    '    Else
    '        pFrmMapBacteria = New frmMapMicrobes
    '        pFrmMapBacteria.Show(Me)
    '    End If
    'End Sub

    Friend pFrmChemicalProperties As frmChemical
    Private Sub btnChemical_Click(sender As System.Object, e As System.EventArgs) Handles btnChemical.Click
        If FormIsOpen(pFrmChemicalProperties) Then
            pFrmChemicalProperties.Activate()
        Else
            pFrmChemicalProperties = New frmChemical
            pFrmChemicalProperties.SetParams()
            pFrmChemicalProperties.Show(Me)
        End If
    End Sub

End Class