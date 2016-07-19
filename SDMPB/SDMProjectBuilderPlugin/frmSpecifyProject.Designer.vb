<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpecifyProject
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.groupSelectAreaOfInterest = New System.Windows.Forms.GroupBox()
        Me.PanelPourPoint = New System.Windows.Forms.Panel()
        Me.lblPourPoint = New System.Windows.Forms.Label()
        Me.btnSelectPourPoint = New System.Windows.Forms.Button()
        Me.lblPourPointKm = New System.Windows.Forms.Label()
        Me.txtPourPointKm = New System.Windows.Forms.TextBox()
        Me.panelSelctionLayer = New System.Windows.Forms.Panel()
        Me.radioSelectBox = New System.Windows.Forms.RadioButton()
        Me.radioSelectPourPoint = New System.Windows.Forms.RadioButton()
        Me.radioSelectCurrent = New System.Windows.Forms.RadioButton()
        Me.radioSelectCounty = New System.Windows.Forms.RadioButton()
        Me.radioSelectCatchment = New System.Windows.Forms.RadioButton()
        Me.lblSelectionLayer = New System.Windows.Forms.Label()
        Me.radioSelectHUC12 = New System.Windows.Forms.RadioButton()
        Me.radioSelectHUC8 = New System.Windows.Forms.RadioButton()
        Me.txtSelected = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.radioSeparate = New System.Windows.Forms.RadioButton()
        Me.radioSingle = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.groupParameters = New System.Windows.Forms.GroupBox()
        Me.lFlowUnits = New System.Windows.Forms.Label()
        Me.rbtnFlowLog = New System.Windows.Forms.RadioButton()
        Me.rbtnFlowLinear = New System.Windows.Forms.RadioButton()
        Me.lblSnow = New System.Windows.Forms.Label()
        Me.btnChemical = New System.Windows.Forms.Button()
        Me.chkLandAppliedChemical = New System.Windows.Forms.CheckBox()
        Me.btnMapMicrobes = New System.Windows.Forms.Button()
        Me.comboHspfSnow = New System.Windows.Forms.ComboBox()
        Me.lblHspfOutputInterval = New System.Windows.Forms.Label()
        Me.comboHspfOutputInterval = New System.Windows.Forms.ComboBox()
        Me.chkMicrobes = New System.Windows.Forms.CheckBox()
        Me.lblSimulationEndYear = New System.Windows.Forms.Label()
        Me.txtSimulationEndYear = New atcControls.atcText()
        Me.lblSimulationStartYear = New System.Windows.Forms.Label()
        Me.txtSimulationStartYear = New atcControls.atcText()
        Me.lblSWAT2 = New System.Windows.Forms.Label()
        Me.lblSWAT1 = New System.Windows.Forms.Label()
        Me.btnSwatDatabase = New System.Windows.Forms.Button()
        Me.lblLU = New System.Windows.Forms.Label()
        Me.atxLU = New atcControls.atcText()
        Me.lblLength = New System.Windows.Forms.Label()
        Me.atxLength = New atcControls.atcText()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.atxSize = New atcControls.atcText()
        Me.chkSWAT = New System.Windows.Forms.CheckBox()
        Me.chkHSPF = New System.Windows.Forms.CheckBox()
        Me.grpSoil = New System.Windows.Forms.GroupBox()
        Me.radioSoilSSURGO = New System.Windows.Forms.RadioButton()
        Me.radioSoilSTATSGO = New System.Windows.Forms.RadioButton()
        Me.txtSaveProjectAs = New System.Windows.Forms.TextBox()
        Me.comboDelineation = New System.Windows.Forms.ComboBox()
        Me.lblDelineation = New System.Windows.Forms.Label()
        Me.lblElevation = New System.Windows.Forms.Label()
        Me.comboElevation = New System.Windows.Forms.ComboBox()
        Me.groupMetData = New System.Windows.Forms.GroupBox()
        Me.txtTimeZone = New atcControls.atcText()
        Me.lblTimeZone = New System.Windows.Forms.Label()
        Me.radioMetDataNLDAS = New System.Windows.Forms.RadioButton()
        Me.txtNCDCtoken = New System.Windows.Forms.TextBox()
        Me.radioMetDataNCDC = New System.Windows.Forms.RadioButton()
        Me.radioMetDataBASINS = New System.Windows.Forms.RadioButton()
        Me.lblSaveProjectAs = New System.Windows.Forms.Label()
        Me.btnSaveProjectAs = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.groupCatchments = New System.Windows.Forms.GroupBox()
        Me.lblCatchments = New System.Windows.Forms.Label()
        Me.panelSelectUpstream = New System.Windows.Forms.Panel()
        Me.btnUpstream = New System.Windows.Forms.Button()
        Me.lblUpstreamUnits = New System.Windows.Forms.Label()
        Me.txtUpstream = New System.Windows.Forms.TextBox()
        Me.txtCatchments = New System.Windows.Forms.TextBox()
        Me.chkAddLayers = New System.Windows.Forms.CheckBox()
        Me.groupData = New System.Windows.Forms.GroupBox()
        Me.groupSelectAreaOfInterest.SuspendLayout()
        Me.PanelPourPoint.SuspendLayout()
        Me.panelSelctionLayer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.groupParameters.SuspendLayout()
        Me.grpSoil.SuspendLayout()
        Me.groupMetData.SuspendLayout()
        Me.groupCatchments.SuspendLayout()
        Me.panelSelectUpstream.SuspendLayout()
        Me.groupData.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupSelectAreaOfInterest
        '
        Me.groupSelectAreaOfInterest.Controls.Add(Me.PanelPourPoint)
        Me.groupSelectAreaOfInterest.Controls.Add(Me.panelSelctionLayer)
        Me.groupSelectAreaOfInterest.Controls.Add(Me.txtSelected)
        Me.groupSelectAreaOfInterest.Controls.Add(Me.Panel1)
        Me.groupSelectAreaOfInterest.Location = New System.Drawing.Point(6, 6)
        Me.groupSelectAreaOfInterest.Margin = New System.Windows.Forms.Padding(0)
        Me.groupSelectAreaOfInterest.Name = "groupSelectAreaOfInterest"
        Me.groupSelectAreaOfInterest.Size = New System.Drawing.Size(598, 224)
        Me.groupSelectAreaOfInterest.TabIndex = 0
        Me.groupSelectAreaOfInterest.TabStop = False
        Me.groupSelectAreaOfInterest.Text = "Select Area Of Interest On Map Or Enter Key(s) Below"
        '
        'PanelPourPoint
        '
        Me.PanelPourPoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelPourPoint.Controls.Add(Me.lblPourPoint)
        Me.PanelPourPoint.Controls.Add(Me.btnSelectPourPoint)
        Me.PanelPourPoint.Controls.Add(Me.lblPourPointKm)
        Me.PanelPourPoint.Controls.Add(Me.txtPourPointKm)
        Me.PanelPourPoint.Location = New System.Drawing.Point(6, 192)
        Me.PanelPourPoint.Name = "PanelPourPoint"
        Me.PanelPourPoint.Size = New System.Drawing.Size(577, 26)
        Me.PanelPourPoint.TabIndex = 9
        Me.PanelPourPoint.Visible = False
        '
        'lblPourPoint
        '
        Me.lblPourPoint.AutoSize = True
        Me.lblPourPoint.Location = New System.Drawing.Point(3, 7)
        Me.lblPourPoint.Name = "lblPourPoint"
        Me.lblPourPoint.Size = New System.Drawing.Size(102, 13)
        Me.lblPourPoint.TabIndex = 11
        Me.lblPourPoint.Text = "Maximum Upstream:"
        '
        'btnSelectPourPoint
        '
        Me.btnSelectPourPoint.Location = New System.Drawing.Point(196, 2)
        Me.btnSelectPourPoint.Name = "btnSelectPourPoint"
        Me.btnSelectPourPoint.Size = New System.Drawing.Size(153, 23)
        Me.btnSelectPourPoint.TabIndex = 11
        Me.btnSelectPourPoint.Text = "Select Pour Point On Map"
        Me.btnSelectPourPoint.UseVisualStyleBackColor = True
        '
        'lblPourPointKm
        '
        Me.lblPourPointKm.AutoSize = True
        Me.lblPourPointKm.Location = New System.Drawing.Point(159, 7)
        Me.lblPourPointKm.Name = "lblPourPointKm"
        Me.lblPourPointKm.Size = New System.Drawing.Size(21, 13)
        Me.lblPourPointKm.TabIndex = 9
        Me.lblPourPointKm.Text = "km"
        '
        'txtPourPointKm
        '
        Me.txtPourPointKm.Location = New System.Drawing.Point(116, 2)
        Me.txtPourPointKm.Name = "txtPourPointKm"
        Me.txtPourPointKm.Size = New System.Drawing.Size(37, 20)
        Me.txtPourPointKm.TabIndex = 10
        '
        'panelSelctionLayer
        '
        Me.panelSelctionLayer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectBox)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectPourPoint)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectCurrent)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectCounty)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectCatchment)
        Me.panelSelctionLayer.Controls.Add(Me.lblSelectionLayer)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectHUC12)
        Me.panelSelctionLayer.Controls.Add(Me.radioSelectHUC8)
        Me.panelSelctionLayer.Location = New System.Drawing.Point(6, 18)
        Me.panelSelctionLayer.Name = "panelSelctionLayer"
        Me.panelSelctionLayer.Size = New System.Drawing.Size(586, 23)
        Me.panelSelctionLayer.TabIndex = 3
        '
        'radioSelectBox
        '
        Me.radioSelectBox.AutoSize = True
        Me.radioSelectBox.Location = New System.Drawing.Point(540, 3)
        Me.radioSelectBox.Name = "radioSelectBox"
        Me.radioSelectBox.Size = New System.Drawing.Size(43, 17)
        Me.radioSelectBox.TabIndex = 8
        Me.radioSelectBox.TabStop = True
        Me.radioSelectBox.Text = "Box"
        Me.radioSelectBox.UseVisualStyleBackColor = True
        '
        'radioSelectPourPoint
        '
        Me.radioSelectPourPoint.AutoSize = True
        Me.radioSelectPourPoint.Location = New System.Drawing.Point(460, 3)
        Me.radioSelectPourPoint.Name = "radioSelectPourPoint"
        Me.radioSelectPourPoint.Size = New System.Drawing.Size(74, 17)
        Me.radioSelectPourPoint.TabIndex = 7
        Me.radioSelectPourPoint.TabStop = True
        Me.radioSelectPourPoint.Text = "Pour Point"
        Me.radioSelectPourPoint.UseVisualStyleBackColor = True
        '
        'radioSelectCurrent
        '
        Me.radioSelectCurrent.AutoSize = True
        Me.radioSelectCurrent.Location = New System.Drawing.Point(342, 3)
        Me.radioSelectCurrent.Name = "radioSelectCurrent"
        Me.radioSelectCurrent.Size = New System.Drawing.Size(112, 17)
        Me.radioSelectCurrent.TabIndex = 6
        Me.radioSelectCurrent.TabStop = True
        Me.radioSelectCurrent.Text = "Current Map Layer"
        Me.radioSelectCurrent.UseVisualStyleBackColor = True
        '
        'radioSelectCounty
        '
        Me.radioSelectCounty.AutoSize = True
        Me.radioSelectCounty.Location = New System.Drawing.Point(278, 3)
        Me.radioSelectCounty.Name = "radioSelectCounty"
        Me.radioSelectCounty.Size = New System.Drawing.Size(58, 17)
        Me.radioSelectCounty.TabIndex = 5
        Me.radioSelectCounty.TabStop = True
        Me.radioSelectCounty.Text = "County"
        Me.radioSelectCounty.UseVisualStyleBackColor = True
        '
        'radioSelectCatchment
        '
        Me.radioSelectCatchment.AutoSize = True
        Me.radioSelectCatchment.Location = New System.Drawing.Point(196, 3)
        Me.radioSelectCatchment.Name = "radioSelectCatchment"
        Me.radioSelectCatchment.Size = New System.Drawing.Size(76, 17)
        Me.radioSelectCatchment.TabIndex = 4
        Me.radioSelectCatchment.TabStop = True
        Me.radioSelectCatchment.Text = "Catchment"
        Me.radioSelectCatchment.UseVisualStyleBackColor = True
        '
        'lblSelectionLayer
        '
        Me.lblSelectionLayer.AutoSize = True
        Me.lblSelectionLayer.Location = New System.Drawing.Point(3, 5)
        Me.lblSelectionLayer.Name = "lblSelectionLayer"
        Me.lblSelectionLayer.Size = New System.Drawing.Size(55, 13)
        Me.lblSelectionLayer.TabIndex = 3
        Me.lblSelectionLayer.Text = "Select By:"
        '
        'radioSelectHUC12
        '
        Me.radioSelectHUC12.AutoSize = True
        Me.radioSelectHUC12.Location = New System.Drawing.Point(127, 3)
        Me.radioSelectHUC12.Name = "radioSelectHUC12"
        Me.radioSelectHUC12.Size = New System.Drawing.Size(63, 17)
        Me.radioSelectHUC12.TabIndex = 2
        Me.radioSelectHUC12.TabStop = True
        Me.radioSelectHUC12.Text = "HUC-12"
        Me.radioSelectHUC12.UseVisualStyleBackColor = True
        '
        'radioSelectHUC8
        '
        Me.radioSelectHUC8.AutoSize = True
        Me.radioSelectHUC8.Checked = True
        Me.radioSelectHUC8.Location = New System.Drawing.Point(64, 3)
        Me.radioSelectHUC8.Name = "radioSelectHUC8"
        Me.radioSelectHUC8.Size = New System.Drawing.Size(57, 17)
        Me.radioSelectHUC8.TabIndex = 1
        Me.radioSelectHUC8.TabStop = True
        Me.radioSelectHUC8.Text = "HUC-8"
        Me.radioSelectHUC8.UseVisualStyleBackColor = True
        '
        'txtSelected
        '
        Me.txtSelected.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelected.Location = New System.Drawing.Point(6, 44)
        Me.txtSelected.Multiline = True
        Me.txtSelected.Name = "txtSelected"
        Me.txtSelected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSelected.Size = New System.Drawing.Size(586, 144)
        Me.txtSelected.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.radioSeparate)
        Me.Panel1.Controls.Add(Me.radioSingle)
        Me.Panel1.Location = New System.Drawing.Point(6, 192)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(262, 26)
        Me.Panel1.TabIndex = 1
        '
        'radioSeparate
        '
        Me.radioSeparate.AutoSize = True
        Me.radioSeparate.Enabled = False
        Me.radioSeparate.Location = New System.Drawing.Point(111, 3)
        Me.radioSeparate.Name = "radioSeparate"
        Me.radioSeparate.Size = New System.Drawing.Size(147, 17)
        Me.radioSeparate.TabIndex = 2
        Me.radioSeparate.Text = "One Project Per Selection"
        Me.radioSeparate.UseVisualStyleBackColor = True
        '
        'radioSingle
        '
        Me.radioSingle.AutoSize = True
        Me.radioSingle.Checked = True
        Me.radioSingle.Enabled = False
        Me.radioSingle.Location = New System.Drawing.Point(3, 3)
        Me.radioSingle.Name = "radioSingle"
        Me.radioSingle.Size = New System.Drawing.Size(90, 17)
        Me.radioSingle.TabIndex = 1
        Me.radioSingle.TabStop = True
        Me.radioSingle.Text = "Single Project"
        Me.radioSingle.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(3, 515)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 28)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(970, 515)
        Me.btnNext.Margin = New System.Windows.Forms.Padding(0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 28)
        Me.btnNext.TabIndex = 16
        Me.btnNext.Text = "Next"
        '
        'groupParameters
        '
        Me.groupParameters.Controls.Add(Me.lFlowUnits)
        Me.groupParameters.Controls.Add(Me.rbtnFlowLog)
        Me.groupParameters.Controls.Add(Me.rbtnFlowLinear)
        Me.groupParameters.Controls.Add(Me.lblSnow)
        Me.groupParameters.Controls.Add(Me.btnChemical)
        Me.groupParameters.Controls.Add(Me.chkLandAppliedChemical)
        Me.groupParameters.Controls.Add(Me.btnMapMicrobes)
        Me.groupParameters.Controls.Add(Me.comboHspfSnow)
        Me.groupParameters.Controls.Add(Me.lblHspfOutputInterval)
        Me.groupParameters.Controls.Add(Me.comboHspfOutputInterval)
        Me.groupParameters.Controls.Add(Me.chkMicrobes)
        Me.groupParameters.Controls.Add(Me.lblSimulationEndYear)
        Me.groupParameters.Controls.Add(Me.txtSimulationEndYear)
        Me.groupParameters.Controls.Add(Me.lblSimulationStartYear)
        Me.groupParameters.Controls.Add(Me.txtSimulationStartYear)
        Me.groupParameters.Controls.Add(Me.lblSWAT2)
        Me.groupParameters.Controls.Add(Me.lblSWAT1)
        Me.groupParameters.Controls.Add(Me.btnSwatDatabase)
        Me.groupParameters.Controls.Add(Me.lblLU)
        Me.groupParameters.Controls.Add(Me.atxLU)
        Me.groupParameters.Controls.Add(Me.lblLength)
        Me.groupParameters.Controls.Add(Me.atxLength)
        Me.groupParameters.Controls.Add(Me.lblSize)
        Me.groupParameters.Controls.Add(Me.atxSize)
        Me.groupParameters.Controls.Add(Me.chkSWAT)
        Me.groupParameters.Controls.Add(Me.chkHSPF)
        Me.groupParameters.Location = New System.Drawing.Point(6, 242)
        Me.groupParameters.Margin = New System.Windows.Forms.Padding(0)
        Me.groupParameters.Name = "groupParameters"
        Me.groupParameters.Padding = New System.Windows.Forms.Padding(0)
        Me.groupParameters.Size = New System.Drawing.Size(598, 262)
        Me.groupParameters.TabIndex = 18
        Me.groupParameters.TabStop = False
        Me.groupParameters.Text = "Parameters For Model Generation"
        Me.groupParameters.Visible = False
        '
        'lFlowUnits
        '
        Me.lFlowUnits.AutoSize = True
        Me.lFlowUnits.Enabled = False
        Me.lFlowUnits.Location = New System.Drawing.Point(70, 245)
        Me.lFlowUnits.Name = "lFlowUnits"
        Me.lFlowUnits.Size = New System.Drawing.Size(91, 13)
        Me.lFlowUnits.TabIndex = 48
        Me.lFlowUnits.Text = "Report Flow Units"
        '
        'rbtnFlowLog
        '
        Me.rbtnFlowLog.AutoSize = True
        Me.rbtnFlowLog.Enabled = False
        Me.rbtnFlowLog.Location = New System.Drawing.Point(220, 242)
        Me.rbtnFlowLog.Name = "rbtnFlowLog"
        Me.rbtnFlowLog.Size = New System.Drawing.Size(43, 17)
        Me.rbtnFlowLog.TabIndex = 56
        Me.rbtnFlowLog.Text = "Log"
        Me.rbtnFlowLog.UseVisualStyleBackColor = True
        '
        'rbtnFlowLinear
        '
        Me.rbtnFlowLinear.AutoSize = True
        Me.rbtnFlowLinear.Checked = True
        Me.rbtnFlowLinear.Enabled = False
        Me.rbtnFlowLinear.Location = New System.Drawing.Point(165, 243)
        Me.rbtnFlowLinear.Name = "rbtnFlowLinear"
        Me.rbtnFlowLinear.Size = New System.Drawing.Size(54, 17)
        Me.rbtnFlowLinear.TabIndex = 55
        Me.rbtnFlowLinear.TabStop = True
        Me.rbtnFlowLinear.Text = "Linear"
        Me.rbtnFlowLinear.UseVisualStyleBackColor = True
        '
        'lblSnow
        '
        Me.lblSnow.AutoSize = True
        Me.lblSnow.Location = New System.Drawing.Point(60, 183)
        Me.lblSnow.Name = "lblSnow"
        Me.lblSnow.Size = New System.Drawing.Size(37, 13)
        Me.lblSnow.TabIndex = 54
        Me.lblSnow.Text = "Snow:"
        '
        'btnChemical
        '
        Me.btnChemical.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChemical.Location = New System.Drawing.Point(388, 178)
        Me.btnChemical.Name = "btnChemical"
        Me.btnChemical.Size = New System.Drawing.Size(204, 22)
        Me.btnChemical.TabIndex = 35
        Me.btnChemical.Text = "Chemical Properties"
        Me.btnChemical.UseVisualStyleBackColor = True
        '
        'chkLandAppliedChemical
        '
        Me.chkLandAppliedChemical.AutoSize = True
        Me.chkLandAppliedChemical.Location = New System.Drawing.Point(243, 182)
        Me.chkLandAppliedChemical.Name = "chkLandAppliedChemical"
        Me.chkLandAppliedChemical.Size = New System.Drawing.Size(134, 17)
        Me.chkLandAppliedChemical.TabIndex = 34
        Me.chkLandAppliedChemical.Text = "Land-Applied Chemical"
        Me.chkLandAppliedChemical.UseVisualStyleBackColor = True
        '
        'btnMapMicrobes
        '
        Me.btnMapMicrobes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMapMicrobes.Location = New System.Drawing.Point(388, 151)
        Me.btnMapMicrobes.Name = "btnMapMicrobes"
        Me.btnMapMicrobes.Size = New System.Drawing.Size(204, 22)
        Me.btnMapMicrobes.TabIndex = 32
        Me.btnMapMicrobes.Text = "Map Microbes / Point Sources"
        Me.btnMapMicrobes.UseVisualStyleBackColor = True
        Me.btnMapMicrobes.Visible = False
        '
        'comboHspfSnow
        '
        Me.comboHspfSnow.FormattingEnabled = True
        Me.comboHspfSnow.Items.AddRange(New Object() {"No Snow", "Energy Balance", "Degree Day"})
        Me.comboHspfSnow.Location = New System.Drawing.Point(106, 180)
        Me.comboHspfSnow.Name = "comboHspfSnow"
        Me.comboHspfSnow.Size = New System.Drawing.Size(104, 21)
        Me.comboHspfSnow.TabIndex = 33
        Me.comboHspfSnow.Text = "No Snow"
        '
        'lblHspfOutputInterval
        '
        Me.lblHspfOutputInterval.AutoSize = True
        Me.lblHspfOutputInterval.Location = New System.Drawing.Point(60, 156)
        Me.lblHspfOutputInterval.Name = "lblHspfOutputInterval"
        Me.lblHspfOutputInterval.Size = New System.Drawing.Size(80, 13)
        Me.lblHspfOutputInterval.TabIndex = 49
        Me.lblHspfOutputInterval.Text = "Output Interval:"
        '
        'comboHspfOutputInterval
        '
        Me.comboHspfOutputInterval.FormattingEnabled = True
        Me.comboHspfOutputInterval.Items.AddRange(New Object() {"Hourly", "Daily", "Monthly", "Yearly", "Never"})
        Me.comboHspfOutputInterval.Location = New System.Drawing.Point(143, 153)
        Me.comboHspfOutputInterval.Name = "comboHspfOutputInterval"
        Me.comboHspfOutputInterval.Size = New System.Drawing.Size(67, 21)
        Me.comboHspfOutputInterval.TabIndex = 30
        Me.comboHspfOutputInterval.Text = "Hourly"
        '
        'chkMicrobes
        '
        Me.chkMicrobes.AutoSize = True
        Me.chkMicrobes.Location = New System.Drawing.Point(243, 155)
        Me.chkMicrobes.Name = "chkMicrobes"
        Me.chkMicrobes.Size = New System.Drawing.Size(69, 17)
        Me.chkMicrobes.TabIndex = 31
        Me.chkMicrobes.Text = "Microbes"
        Me.chkMicrobes.UseVisualStyleBackColor = True
        '
        'lblSimulationEndYear
        '
        Me.lblSimulationEndYear.AutoSize = True
        Me.lblSimulationEndYear.Location = New System.Drawing.Point(60, 123)
        Me.lblSimulationEndYear.Name = "lblSimulationEndYear"
        Me.lblSimulationEndYear.Size = New System.Drawing.Size(102, 13)
        Me.lblSimulationEndYear.TabIndex = 34
        Me.lblSimulationEndYear.Text = "Simulation End Year"
        '
        'txtSimulationEndYear
        '
        Me.txtSimulationEndYear.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSimulationEndYear.DataType = atcControls.atcText.ATCoDataType.ATCoInt
        Me.txtSimulationEndYear.DefaultValue = ""
        Me.txtSimulationEndYear.HardMax = -999.0R
        Me.txtSimulationEndYear.HardMin = -999.0R
        Me.txtSimulationEndYear.InsideLimitsBackground = System.Drawing.Color.White
        Me.txtSimulationEndYear.Location = New System.Drawing.Point(6, 121)
        Me.txtSimulationEndYear.MaxWidth = 20
        Me.txtSimulationEndYear.Name = "txtSimulationEndYear"
        Me.txtSimulationEndYear.NumericFormat = "0.#####"
        Me.txtSimulationEndYear.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.txtSimulationEndYear.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.txtSimulationEndYear.SelLength = 0
        Me.txtSimulationEndYear.SelStart = 0
        Me.txtSimulationEndYear.Size = New System.Drawing.Size(49, 18)
        Me.txtSimulationEndYear.SoftMax = -999.0R
        Me.txtSimulationEndYear.SoftMin = -999.0R
        Me.txtSimulationEndYear.TabIndex = 28
        Me.txtSimulationEndYear.ValueDouble = 0.0R
        Me.txtSimulationEndYear.ValueInteger = 0
        '
        'lblSimulationStartYear
        '
        Me.lblSimulationStartYear.AutoSize = True
        Me.lblSimulationStartYear.Location = New System.Drawing.Point(60, 99)
        Me.lblSimulationStartYear.Name = "lblSimulationStartYear"
        Me.lblSimulationStartYear.Size = New System.Drawing.Size(105, 13)
        Me.lblSimulationStartYear.TabIndex = 33
        Me.lblSimulationStartYear.Text = "Simulation Start Year"
        '
        'txtSimulationStartYear
        '
        Me.txtSimulationStartYear.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSimulationStartYear.DataType = atcControls.atcText.ATCoDataType.ATCoInt
        Me.txtSimulationStartYear.DefaultValue = ""
        Me.txtSimulationStartYear.HardMax = -999.0R
        Me.txtSimulationStartYear.HardMin = -999.0R
        Me.txtSimulationStartYear.InsideLimitsBackground = System.Drawing.Color.White
        Me.txtSimulationStartYear.Location = New System.Drawing.Point(6, 97)
        Me.txtSimulationStartYear.MaxWidth = 20
        Me.txtSimulationStartYear.Name = "txtSimulationStartYear"
        Me.txtSimulationStartYear.NumericFormat = "0.#####"
        Me.txtSimulationStartYear.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.txtSimulationStartYear.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.txtSimulationStartYear.SelLength = 0
        Me.txtSimulationStartYear.SelStart = 0
        Me.txtSimulationStartYear.Size = New System.Drawing.Size(49, 18)
        Me.txtSimulationStartYear.SoftMax = -999.0R
        Me.txtSimulationStartYear.SoftMin = -999.0R
        Me.txtSimulationStartYear.TabIndex = 26
        Me.txtSimulationStartYear.ValueDouble = 0.0R
        Me.txtSimulationStartYear.ValueInteger = 0
        '
        'lblSWAT2
        '
        Me.lblSWAT2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSWAT2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSWAT2.Location = New System.Drawing.Point(188, 223)
        Me.lblSWAT2.Name = "lblSWAT2"
        Me.lblSWAT2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSWAT2.Size = New System.Drawing.Size(369, 15)
        Me.lblSWAT2.TabIndex = 29
        Me.lblSWAT2.Text = "SWAT2005.mdb"
        Me.lblSWAT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSWAT1
        '
        Me.lblSWAT1.AutoSize = True
        Me.lblSWAT1.Location = New System.Drawing.Point(67, 223)
        Me.lblSWAT1.Name = "lblSWAT1"
        Me.lblSWAT1.Size = New System.Drawing.Size(115, 13)
        Me.lblSWAT1.TabIndex = 32
        Me.lblSWAT1.Text = "SWAT 2005 Database"
        '
        'btnSwatDatabase
        '
        Me.btnSwatDatabase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSwatDatabase.Location = New System.Drawing.Point(563, 220)
        Me.btnSwatDatabase.Name = "btnSwatDatabase"
        Me.btnSwatDatabase.Size = New System.Drawing.Size(29, 20)
        Me.btnSwatDatabase.TabIndex = 37
        Me.btnSwatDatabase.Text = "..."
        Me.btnSwatDatabase.UseVisualStyleBackColor = True
        '
        'lblLU
        '
        Me.lblLU.AutoSize = True
        Me.lblLU.Location = New System.Drawing.Point(60, 75)
        Me.lblLU.Name = "lblLU"
        Me.lblLU.Size = New System.Drawing.Size(184, 13)
        Me.lblLU.TabIndex = 30
        Me.lblLU.Text = "Ignore Landuse Areas Below Fraction"
        '
        'atxLU
        '
        Me.atxLU.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLU.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLU.DefaultValue = ""
        Me.atxLU.HardMax = -999.0R
        Me.atxLU.HardMin = -999.0R
        Me.atxLU.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLU.Location = New System.Drawing.Point(6, 73)
        Me.atxLU.MaxWidth = 20
        Me.atxLU.Name = "atxLU"
        Me.atxLU.NumericFormat = "0.#####"
        Me.atxLU.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxLU.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxLU.SelLength = 0
        Me.atxLU.SelStart = 0
        Me.atxLU.Size = New System.Drawing.Size(49, 18)
        Me.atxLU.SoftMax = -999.0R
        Me.atxLU.SoftMin = -999.0R
        Me.atxLU.TabIndex = 25
        Me.atxLU.ValueDouble = 0.0R
        Me.atxLU.ValueInteger = 0
        '
        'lblLength
        '
        Me.lblLength.AutoSize = True
        Me.lblLength.Location = New System.Drawing.Point(60, 50)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(181, 13)
        Me.lblLength.TabIndex = 27
        Me.lblLength.Text = "Minimum Flowline Length (kilometers)"
        '
        'atxLength
        '
        Me.atxLength.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLength.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLength.DefaultValue = ""
        Me.atxLength.HardMax = -999.0R
        Me.atxLength.HardMin = -999.0R
        Me.atxLength.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLength.Location = New System.Drawing.Point(6, 49)
        Me.atxLength.MaxWidth = 20
        Me.atxLength.Name = "atxLength"
        Me.atxLength.NumericFormat = "0.#####"
        Me.atxLength.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxLength.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxLength.SelLength = 0
        Me.atxLength.SelStart = 0
        Me.atxLength.Size = New System.Drawing.Size(49, 18)
        Me.atxLength.SoftMax = -999.0R
        Me.atxLength.SoftMin = -999.0R
        Me.atxLength.TabIndex = 23
        Me.atxLength.ValueDouble = 0.0R
        Me.atxLength.ValueInteger = 0
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(60, 27)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(216, 13)
        Me.lblSize.TabIndex = 24
        Me.lblSize.Text = "Minimum Catchment Size (square kilometers)"
        '
        'atxSize
        '
        Me.atxSize.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxSize.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxSize.DefaultValue = ""
        Me.atxSize.HardMax = -999.0R
        Me.atxSize.HardMin = -999.0R
        Me.atxSize.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxSize.Location = New System.Drawing.Point(6, 23)
        Me.atxSize.MaxWidth = 20
        Me.atxSize.Name = "atxSize"
        Me.atxSize.NumericFormat = "0.#####"
        Me.atxSize.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxSize.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxSize.SelLength = 0
        Me.atxSize.SelStart = 0
        Me.atxSize.Size = New System.Drawing.Size(49, 20)
        Me.atxSize.SoftMax = -999.0R
        Me.atxSize.SoftMin = -999.0R
        Me.atxSize.TabIndex = 22
        Me.atxSize.ValueDouble = 0.0R
        Me.atxSize.ValueInteger = 0
        '
        'chkSWAT
        '
        Me.chkSWAT.AutoSize = True
        Me.chkSWAT.Checked = True
        Me.chkSWAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSWAT.Location = New System.Drawing.Point(6, 222)
        Me.chkSWAT.Name = "chkSWAT"
        Me.chkSWAT.Size = New System.Drawing.Size(58, 17)
        Me.chkSWAT.TabIndex = 36
        Me.chkSWAT.Text = "SWAT"
        Me.chkSWAT.UseVisualStyleBackColor = True
        '
        'chkHSPF
        '
        Me.chkHSPF.AutoSize = True
        Me.chkHSPF.Checked = True
        Me.chkHSPF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHSPF.Location = New System.Drawing.Point(6, 155)
        Me.chkHSPF.Name = "chkHSPF"
        Me.chkHSPF.Size = New System.Drawing.Size(54, 17)
        Me.chkHSPF.TabIndex = 29
        Me.chkHSPF.Text = "HSPF"
        Me.chkHSPF.UseVisualStyleBackColor = True
        '
        'grpSoil
        '
        Me.grpSoil.Controls.Add(Me.radioSoilSSURGO)
        Me.grpSoil.Controls.Add(Me.radioSoilSTATSGO)
        Me.grpSoil.Location = New System.Drawing.Point(6, 19)
        Me.grpSoil.Margin = New System.Windows.Forms.Padding(0)
        Me.grpSoil.Name = "grpSoil"
        Me.grpSoil.Padding = New System.Windows.Forms.Padding(0)
        Me.grpSoil.Size = New System.Drawing.Size(86, 65)
        Me.grpSoil.TabIndex = 46
        Me.grpSoil.TabStop = False
        Me.grpSoil.Text = "Soil"
        '
        'radioSoilSSURGO
        '
        Me.radioSoilSSURGO.AutoSize = True
        Me.radioSoilSSURGO.Location = New System.Drawing.Point(3, 39)
        Me.radioSoilSSURGO.Name = "radioSoilSSURGO"
        Me.radioSoilSSURGO.Size = New System.Drawing.Size(71, 17)
        Me.radioSoilSSURGO.TabIndex = 1
        Me.radioSoilSSURGO.Text = "SSURGO"
        Me.radioSoilSSURGO.UseVisualStyleBackColor = True
        '
        'radioSoilSTATSGO
        '
        Me.radioSoilSTATSGO.AutoSize = True
        Me.radioSoilSTATSGO.Checked = True
        Me.radioSoilSTATSGO.Location = New System.Drawing.Point(3, 15)
        Me.radioSoilSTATSGO.Name = "radioSoilSTATSGO"
        Me.radioSoilSTATSGO.Size = New System.Drawing.Size(76, 17)
        Me.radioSoilSTATSGO.TabIndex = 0
        Me.radioSoilSTATSGO.TabStop = True
        Me.radioSoilSTATSGO.Text = "STATSGO"
        Me.radioSoilSTATSGO.UseVisualStyleBackColor = True
        '
        'txtSaveProjectAs
        '
        Me.txtSaveProjectAs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSaveProjectAs.Location = New System.Drawing.Point(98, 197)
        Me.txtSaveProjectAs.Name = "txtSaveProjectAs"
        Me.txtSaveProjectAs.Size = New System.Drawing.Size(281, 20)
        Me.txtSaveProjectAs.TabIndex = 46
        '
        'comboDelineation
        '
        Me.comboDelineation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboDelineation.FormattingEnabled = True
        Me.comboDelineation.Location = New System.Drawing.Point(98, 143)
        Me.comboDelineation.Name = "comboDelineation"
        Me.comboDelineation.Size = New System.Drawing.Size(313, 21)
        Me.comboDelineation.TabIndex = 44
        '
        'lblDelineation
        '
        Me.lblDelineation.AutoSize = True
        Me.lblDelineation.Location = New System.Drawing.Point(6, 146)
        Me.lblDelineation.Name = "lblDelineation"
        Me.lblDelineation.Size = New System.Drawing.Size(60, 13)
        Me.lblDelineation.TabIndex = 41
        Me.lblDelineation.Text = "Delineation"
        '
        'lblElevation
        '
        Me.lblElevation.AutoSize = True
        Me.lblElevation.Location = New System.Drawing.Point(6, 119)
        Me.lblElevation.Name = "lblElevation"
        Me.lblElevation.Size = New System.Drawing.Size(51, 13)
        Me.lblElevation.TabIndex = 40
        Me.lblElevation.Text = "Elevation"
        '
        'comboElevation
        '
        Me.comboElevation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboElevation.FormattingEnabled = True
        Me.comboElevation.Location = New System.Drawing.Point(98, 116)
        Me.comboElevation.Name = "comboElevation"
        Me.comboElevation.Size = New System.Drawing.Size(313, 21)
        Me.comboElevation.TabIndex = 39
        '
        'groupMetData
        '
        Me.groupMetData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupMetData.Controls.Add(Me.txtTimeZone)
        Me.groupMetData.Controls.Add(Me.lblTimeZone)
        Me.groupMetData.Controls.Add(Me.radioMetDataNLDAS)
        Me.groupMetData.Controls.Add(Me.txtNCDCtoken)
        Me.groupMetData.Controls.Add(Me.radioMetDataNCDC)
        Me.groupMetData.Controls.Add(Me.radioMetDataBASINS)
        Me.groupMetData.Location = New System.Drawing.Point(98, 19)
        Me.groupMetData.Margin = New System.Windows.Forms.Padding(0)
        Me.groupMetData.Name = "groupMetData"
        Me.groupMetData.Padding = New System.Windows.Forms.Padding(4)
        Me.groupMetData.Size = New System.Drawing.Size(313, 92)
        Me.groupMetData.TabIndex = 38
        Me.groupMetData.TabStop = False
        Me.groupMetData.Text = "Meteorologic"
        '
        'txtTimeZone
        '
        Me.txtTimeZone.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtTimeZone.DataType = atcControls.atcText.ATCoDataType.ATCoInt
        Me.txtTimeZone.DefaultValue = ""
        Me.txtTimeZone.HardMax = -999.0R
        Me.txtTimeZone.HardMin = -999.0R
        Me.txtTimeZone.InsideLimitsBackground = System.Drawing.Color.White
        Me.txtTimeZone.Location = New System.Drawing.Point(256, 66)
        Me.txtTimeZone.MaxWidth = 20
        Me.txtTimeZone.Name = "txtTimeZone"
        Me.txtTimeZone.NumericFormat = "0.#####"
        Me.txtTimeZone.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.txtTimeZone.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.txtTimeZone.SelLength = 0
        Me.txtTimeZone.SelStart = 0
        Me.txtTimeZone.Size = New System.Drawing.Size(49, 18)
        Me.txtTimeZone.SoftMax = -999.0R
        Me.txtTimeZone.SoftMin = -999.0R
        Me.txtTimeZone.TabIndex = 42
        Me.txtTimeZone.ValueDouble = 0.0R
        Me.txtTimeZone.ValueInteger = 0
        '
        'lblTimeZone
        '
        Me.lblTimeZone.AutoSize = True
        Me.lblTimeZone.Location = New System.Drawing.Point(95, 68)
        Me.lblTimeZone.Name = "lblTimeZone"
        Me.lblTimeZone.Size = New System.Drawing.Size(155, 13)
        Me.lblTimeZone.TabIndex = 41
        Me.lblTimeZone.Text = "Project Time Zone - UTC minus"
        Me.lblTimeZone.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'radioMetDataNLDAS
        '
        Me.radioMetDataNLDAS.AutoSize = True
        Me.radioMetDataNLDAS.Location = New System.Drawing.Point(7, 66)
        Me.radioMetDataNLDAS.Name = "radioMetDataNLDAS"
        Me.radioMetDataNLDAS.Size = New System.Drawing.Size(61, 17)
        Me.radioMetDataNLDAS.TabIndex = 4
        Me.radioMetDataNLDAS.Text = "NLDAS"
        Me.radioMetDataNLDAS.UseVisualStyleBackColor = True
        '
        'txtNCDCtoken
        '
        Me.txtNCDCtoken.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNCDCtoken.Location = New System.Drawing.Point(68, 40)
        Me.txtNCDCtoken.Name = "txtNCDCtoken"
        Me.txtNCDCtoken.Size = New System.Drawing.Size(238, 20)
        Me.txtNCDCtoken.TabIndex = 3
        '
        'radioMetDataNCDC
        '
        Me.radioMetDataNCDC.AutoSize = True
        Me.radioMetDataNCDC.Location = New System.Drawing.Point(7, 43)
        Me.radioMetDataNCDC.Name = "radioMetDataNCDC"
        Me.radioMetDataNCDC.Size = New System.Drawing.Size(55, 17)
        Me.radioMetDataNCDC.TabIndex = 1
        Me.radioMetDataNCDC.Text = "NCDC"
        Me.radioMetDataNCDC.UseVisualStyleBackColor = True
        '
        'radioMetDataBASINS
        '
        Me.radioMetDataBASINS.AutoSize = True
        Me.radioMetDataBASINS.Checked = True
        Me.radioMetDataBASINS.Location = New System.Drawing.Point(7, 19)
        Me.radioMetDataBASINS.Name = "radioMetDataBASINS"
        Me.radioMetDataBASINS.Size = New System.Drawing.Size(64, 17)
        Me.radioMetDataBASINS.TabIndex = 0
        Me.radioMetDataBASINS.TabStop = True
        Me.radioMetDataBASINS.Text = "BASINS"
        Me.radioMetDataBASINS.UseVisualStyleBackColor = True
        '
        'lblSaveProjectAs
        '
        Me.lblSaveProjectAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSaveProjectAs.AutoSize = True
        Me.lblSaveProjectAs.Location = New System.Drawing.Point(6, 201)
        Me.lblSaveProjectAs.Name = "lblSaveProjectAs"
        Me.lblSaveProjectAs.Size = New System.Drawing.Size(83, 13)
        Me.lblSaveProjectAs.TabIndex = 37
        Me.lblSaveProjectAs.Text = "Save Project As"
        '
        'btnSaveProjectAs
        '
        Me.btnSaveProjectAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveProjectAs.Location = New System.Drawing.Point(382, 196)
        Me.btnSaveProjectAs.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSaveProjectAs.Name = "btnSaveProjectAs"
        Me.btnSaveProjectAs.Size = New System.Drawing.Size(29, 20)
        Me.btnSaveProjectAs.TabIndex = 47
        Me.btnSaveProjectAs.Text = "..."
        Me.btnSaveProjectAs.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevious.Location = New System.Drawing.Point(881, 515)
        Me.btnPrevious.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(80, 28)
        Me.btnPrevious.TabIndex = 18
        Me.btnPrevious.Text = "Previous"
        Me.btnPrevious.Visible = False
        '
        'btnBuild
        '
        Me.btnBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuild.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuild.Location = New System.Drawing.Point(970, 515)
        Me.btnBuild.Margin = New System.Windows.Forms.Padding(0)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(80, 28)
        Me.btnBuild.TabIndex = 19
        Me.btnBuild.Text = "Build"
        Me.btnBuild.Visible = False
        '
        'groupCatchments
        '
        Me.groupCatchments.Controls.Add(Me.lblCatchments)
        Me.groupCatchments.Controls.Add(Me.panelSelectUpstream)
        Me.groupCatchments.Controls.Add(Me.txtCatchments)
        Me.groupCatchments.Location = New System.Drawing.Point(617, 6)
        Me.groupCatchments.Margin = New System.Windows.Forms.Padding(0)
        Me.groupCatchments.Name = "groupCatchments"
        Me.groupCatchments.Size = New System.Drawing.Size(421, 224)
        Me.groupCatchments.TabIndex = 20
        Me.groupCatchments.TabStop = False
        Me.groupCatchments.Text = "Catchment Selection"
        Me.groupCatchments.Visible = False
        '
        'lblCatchments
        '
        Me.lblCatchments.AutoSize = True
        Me.lblCatchments.Location = New System.Drawing.Point(6, 24)
        Me.lblCatchments.Name = "lblCatchments"
        Me.lblCatchments.Size = New System.Drawing.Size(397, 13)
        Me.lblCatchments.TabIndex = 9
        Me.lblCatchments.Text = "Fine-tune the set of catchments to include by selecting on the map or editing bel" & _
    "ow"
        '
        'panelSelectUpstream
        '
        Me.panelSelectUpstream.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.panelSelectUpstream.Controls.Add(Me.btnUpstream)
        Me.panelSelectUpstream.Controls.Add(Me.lblUpstreamUnits)
        Me.panelSelectUpstream.Controls.Add(Me.txtUpstream)
        Me.panelSelectUpstream.Location = New System.Drawing.Point(6, 192)
        Me.panelSelectUpstream.Name = "panelSelectUpstream"
        Me.panelSelectUpstream.Size = New System.Drawing.Size(240, 26)
        Me.panelSelectUpstream.TabIndex = 8
        '
        'btnUpstream
        '
        Me.btnUpstream.Location = New System.Drawing.Point(3, 2)
        Me.btnUpstream.Name = "btnUpstream"
        Me.btnUpstream.Size = New System.Drawing.Size(107, 23)
        Me.btnUpstream.TabIndex = 10
        Me.btnUpstream.Text = "Extend Upstream"
        Me.btnUpstream.UseVisualStyleBackColor = True
        '
        'lblUpstreamUnits
        '
        Me.lblUpstreamUnits.AutoSize = True
        Me.lblUpstreamUnits.Location = New System.Drawing.Point(159, 7)
        Me.lblUpstreamUnits.Name = "lblUpstreamUnits"
        Me.lblUpstreamUnits.Size = New System.Drawing.Size(62, 13)
        Me.lblUpstreamUnits.TabIndex = 9
        Me.lblUpstreamUnits.Text = "catchments"
        '
        'txtUpstream
        '
        Me.txtUpstream.Location = New System.Drawing.Point(116, 2)
        Me.txtUpstream.Name = "txtUpstream"
        Me.txtUpstream.Size = New System.Drawing.Size(37, 20)
        Me.txtUpstream.TabIndex = 8
        '
        'txtCatchments
        '
        Me.txtCatchments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCatchments.Location = New System.Drawing.Point(6, 44)
        Me.txtCatchments.Multiline = True
        Me.txtCatchments.Name = "txtCatchments"
        Me.txtCatchments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCatchments.Size = New System.Drawing.Size(409, 142)
        Me.txtCatchments.TabIndex = 3
        '
        'chkAddLayers
        '
        Me.chkAddLayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAddLayers.AutoSize = True
        Me.chkAddLayers.Checked = True
        Me.chkAddLayers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddLayers.Location = New System.Drawing.Point(97, 522)
        Me.chkAddLayers.Name = "chkAddLayers"
        Me.chkAddLayers.Size = New System.Drawing.Size(231, 17)
        Me.chkAddLayers.TabIndex = 46
        Me.chkAddLayers.Text = "Add Layers To Map During Project Creation"
        Me.chkAddLayers.UseVisualStyleBackColor = True
        Me.chkAddLayers.Visible = False
        '
        'groupData
        '
        Me.groupData.Controls.Add(Me.grpSoil)
        Me.groupData.Controls.Add(Me.txtSaveProjectAs)
        Me.groupData.Controls.Add(Me.comboDelineation)
        Me.groupData.Controls.Add(Me.lblSaveProjectAs)
        Me.groupData.Controls.Add(Me.groupMetData)
        Me.groupData.Controls.Add(Me.btnSaveProjectAs)
        Me.groupData.Controls.Add(Me.lblDelineation)
        Me.groupData.Controls.Add(Me.comboElevation)
        Me.groupData.Controls.Add(Me.lblElevation)
        Me.groupData.Location = New System.Drawing.Point(617, 242)
        Me.groupData.Margin = New System.Windows.Forms.Padding(0)
        Me.groupData.Name = "groupData"
        Me.groupData.Size = New System.Drawing.Size(420, 223)
        Me.groupData.TabIndex = 47
        Me.groupData.TabStop = False
        Me.groupData.Text = "Data Options"
        Me.groupData.Visible = False
        '
        'frmSpecifyProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1053, 546)
        Me.Controls.Add(Me.chkAddLayers)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.groupParameters)
        Me.Controls.Add(Me.groupData)
        Me.Controls.Add(Me.groupSelectAreaOfInterest)
        Me.Controls.Add(Me.groupCatchments)
        Me.Controls.Add(Me.btnBuild)
        Me.Controls.Add(Me.btnNext)
        Me.Name = "frmSpecifyProject"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Text = "Specify Project"
        Me.groupSelectAreaOfInterest.ResumeLayout(False)
        Me.groupSelectAreaOfInterest.PerformLayout()
        Me.PanelPourPoint.ResumeLayout(False)
        Me.PanelPourPoint.PerformLayout()
        Me.panelSelctionLayer.ResumeLayout(False)
        Me.panelSelctionLayer.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.groupParameters.ResumeLayout(False)
        Me.groupParameters.PerformLayout()
        Me.grpSoil.ResumeLayout(False)
        Me.grpSoil.PerformLayout()
        Me.groupMetData.ResumeLayout(False)
        Me.groupMetData.PerformLayout()
        Me.groupCatchments.ResumeLayout(False)
        Me.groupCatchments.PerformLayout()
        Me.panelSelectUpstream.ResumeLayout(False)
        Me.panelSelectUpstream.PerformLayout()
        Me.groupData.ResumeLayout(False)
        Me.groupData.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents groupSelectAreaOfInterest As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents radioSeparate As System.Windows.Forms.RadioButton
    Friend WithEvents radioSingle As System.Windows.Forms.RadioButton
    Friend WithEvents txtSelected As System.Windows.Forms.TextBox
    Friend WithEvents panelSelctionLayer As System.Windows.Forms.Panel
    Friend WithEvents radioSelectCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents radioSelectCounty As System.Windows.Forms.RadioButton
    Friend WithEvents radioSelectCatchment As System.Windows.Forms.RadioButton
    Friend WithEvents lblSelectionLayer As System.Windows.Forms.Label
    Friend WithEvents radioSelectHUC12 As System.Windows.Forms.RadioButton
    Friend WithEvents radioSelectHUC8 As System.Windows.Forms.RadioButton
    Friend WithEvents groupParameters As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblSimulationEndYear As System.Windows.Forms.Label
    Friend WithEvents txtSimulationEndYear As atcControls.atcText
    Friend WithEvents lblSimulationStartYear As System.Windows.Forms.Label
    Friend WithEvents txtSimulationStartYear As atcControls.atcText
    Friend WithEvents lblLU As System.Windows.Forms.Label
    Friend WithEvents atxLU As atcControls.atcText
    Friend WithEvents lblLength As System.Windows.Forms.Label
    Friend WithEvents atxLength As atcControls.atcText
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents atxSize As atcControls.atcText
    Friend WithEvents chkHSPF As System.Windows.Forms.CheckBox
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents lblSWAT2 As System.Windows.Forms.Label
    Friend WithEvents lblSWAT1 As System.Windows.Forms.Label
    Friend WithEvents btnSwatDatabase As System.Windows.Forms.Button
    Friend WithEvents chkSWAT As System.Windows.Forms.CheckBox
    Friend WithEvents groupCatchments As System.Windows.Forms.GroupBox
    Friend WithEvents lblCatchments As System.Windows.Forms.Label
    Friend WithEvents panelSelectUpstream As System.Windows.Forms.Panel
    Friend WithEvents btnUpstream As System.Windows.Forms.Button
    Friend WithEvents lblUpstreamUnits As System.Windows.Forms.Label
    Friend WithEvents txtUpstream As System.Windows.Forms.TextBox
    Friend WithEvents txtCatchments As System.Windows.Forms.TextBox
    Friend WithEvents lblSaveProjectAs As System.Windows.Forms.Label
    Friend WithEvents btnSaveProjectAs As System.Windows.Forms.Button
    Friend WithEvents radioSelectPourPoint As System.Windows.Forms.RadioButton
    Friend WithEvents PanelPourPoint As System.Windows.Forms.Panel
    Friend WithEvents lblPourPoint As System.Windows.Forms.Label
    Friend WithEvents btnSelectPourPoint As System.Windows.Forms.Button
    Friend WithEvents lblPourPointKm As System.Windows.Forms.Label
    Friend WithEvents txtPourPointKm As System.Windows.Forms.TextBox
    Friend WithEvents groupMetData As System.Windows.Forms.GroupBox
    Friend WithEvents radioMetDataNCDC As System.Windows.Forms.RadioButton
    Friend WithEvents radioMetDataBASINS As System.Windows.Forms.RadioButton
    Friend WithEvents txtNCDCtoken As System.Windows.Forms.TextBox
    Friend WithEvents lblElevation As System.Windows.Forms.Label
    Friend WithEvents comboElevation As System.Windows.Forms.ComboBox
    Friend WithEvents comboDelineation As System.Windows.Forms.ComboBox
    Friend WithEvents lblDelineation As System.Windows.Forms.Label
    Friend WithEvents txtSaveProjectAs As System.Windows.Forms.TextBox
    Friend WithEvents radioSelectBox As System.Windows.Forms.RadioButton
    Friend WithEvents chkAddLayers As System.Windows.Forms.CheckBox
    Friend WithEvents grpSoil As System.Windows.Forms.GroupBox
    Friend WithEvents radioSoilSSURGO As System.Windows.Forms.RadioButton
    Friend WithEvents radioSoilSTATSGO As System.Windows.Forms.RadioButton
    Friend WithEvents groupData As System.Windows.Forms.GroupBox
    Friend WithEvents chkMicrobes As System.Windows.Forms.CheckBox
    Friend WithEvents lblHspfOutputInterval As System.Windows.Forms.Label
    Friend WithEvents comboHspfOutputInterval As System.Windows.Forms.ComboBox
    Friend WithEvents comboHspfSnow As System.Windows.Forms.ComboBox
    Friend WithEvents btnMapMicrobes As System.Windows.Forms.Button
    Friend WithEvents chkLandAppliedChemical As System.Windows.Forms.CheckBox
    Friend WithEvents btnChemical As System.Windows.Forms.Button
    Friend WithEvents lblSnow As System.Windows.Forms.Label
    Friend WithEvents lFlowUnits As System.Windows.Forms.Label
    Friend WithEvents rbtnFlowLog As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnFlowLinear As System.Windows.Forms.RadioButton
    Friend WithEvents radioMetDataNLDAS As System.Windows.Forms.RadioButton
    Friend WithEvents txtTimeZone As atcControls.atcText
    Friend WithEvents lblTimeZone As System.Windows.Forms.Label
End Class
