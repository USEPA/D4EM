Imports atcUtility
Imports DotSpatial.Data
Imports DotSpatial.Projections
Imports MapWinUtility
Imports SDM_Project_Builder_Batch

Public Class frmBuildNew
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        txtInstructions.Text = "To Build a New " & g_AppNameShort & " Project, " & _
           "zoom/pan to your geographic area of interest, select (highlight) it, " & _
           "and then click 'Build'.  " '& _

        txtInstructionDW.Text = "To Build a New " & g_AppNameShort & " Project, " & _
           "zoom/pan to your geographic area of interest, and select a pour point. " & _
           "You can select a HUC (highlight) it, and download flowlines to help " & _
           "locate your pour point.  Click 'Build'."
        '"If your area is outside the US, then click 'Build' " & _
        '"with no features selected to create an international project."

        AddHandler g_Map.MouseClick, AddressOf MapMouseClick

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents txtInstructions As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkHSPF As System.Windows.Forms.CheckBox
    Friend WithEvents chkSWAT As System.Windows.Forms.CheckBox
    Friend WithEvents atxSize As atcControls.atcText
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblLength As System.Windows.Forms.Label
    Friend WithEvents atxLength As atcControls.atcText
    Friend WithEvents lblLU As System.Windows.Forms.Label
    Friend WithEvents atxLU As atcControls.atcText
    Friend WithEvents cmdSet As System.Windows.Forms.Button
    Friend WithEvents lblSWAT As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblSimulationStartYear As System.Windows.Forms.Label
    Friend WithEvents txtSimulationStartYear As atcControls.atcText
    Friend WithEvents lblSimulationEndYear As System.Windows.Forms.Label
    Friend WithEvents txtSimulationEndYear As atcControls.atcText
    Friend WithEvents tabsAreaSelection As System.Windows.Forms.TabControl
    Friend WithEvents tabSelectOnMap As System.Windows.Forms.TabPage
    Friend WithEvents tabSelectList As System.Windows.Forms.TabPage
    Friend WithEvents txtHucList As System.Windows.Forms.TextBox
    Friend WithEvents lblHucList As System.Windows.Forms.Label
    Friend WithEvents tabDelineateWatershed As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoHUC12 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHUC8 As System.Windows.Forms.RadioButton
    Friend WithEvents lblProjectSize As System.Windows.Forms.Label
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents txtFind As System.Windows.Forms.TextBox
    Friend WithEvents txtInstructionDW As System.Windows.Forms.TextBox
    Friend WithEvents txtSelectedDW As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectPourPt As System.Windows.Forms.Button
    Friend WithEvents txtMaxKM As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnGetDelineatedWS As System.Windows.Forms.Button
    Friend WithEvents btnGetFlowlines As System.Windows.Forms.Button
    Friend WithEvents txtSelected As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.txtInstructions = New System.Windows.Forms.TextBox()
        Me.txtSelected = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkHSPF = New System.Windows.Forms.CheckBox()
        Me.chkSWAT = New System.Windows.Forms.CheckBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblLength = New System.Windows.Forms.Label()
        Me.lblLU = New System.Windows.Forms.Label()
        Me.cmdSet = New System.Windows.Forms.Button()
        Me.lblSWAT = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblSimulationStartYear = New System.Windows.Forms.Label()
        Me.lblSimulationEndYear = New System.Windows.Forms.Label()
        Me.tabsAreaSelection = New System.Windows.Forms.TabControl()
        Me.tabDelineateWatershed = New System.Windows.Forms.TabPage()
        Me.btnGetDelineatedWS = New System.Windows.Forms.Button()
        Me.btnGetFlowlines = New System.Windows.Forms.Button()
        Me.txtMaxKM = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLongitude = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtLatitude = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSelectPourPt = New System.Windows.Forms.Button()
        Me.txtSelectedDW = New System.Windows.Forms.TextBox()
        Me.txtInstructionDW = New System.Windows.Forms.TextBox()
        Me.tabSelectOnMap = New System.Windows.Forms.TabPage()
        Me.tabSelectList = New System.Windows.Forms.TabPage()
        Me.lblHucList = New System.Windows.Forms.Label()
        Me.txtHucList = New System.Windows.Forms.TextBox()
        Me.txtSimulationEndYear = New atcControls.atcText()
        Me.txtSimulationStartYear = New atcControls.atcText()
        Me.atxLU = New atcControls.atcText()
        Me.atxLength = New atcControls.atcText()
        Me.atxSize = New atcControls.atcText()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoHUC12 = New System.Windows.Forms.RadioButton()
        Me.rdoHUC8 = New System.Windows.Forms.RadioButton()
        Me.lblProjectSize = New System.Windows.Forms.Label()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.tabsAreaSelection.SuspendLayout()
        Me.tabDelineateWatershed.SuspendLayout()
        Me.tabSelectOnMap.SuspendLayout()
        Me.tabSelectList.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBuild
        '
        Me.btnBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuild.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuild.Location = New System.Drawing.Point(438, 412)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(80, 28)
        Me.btnBuild.TabIndex = 14
        Me.btnBuild.Text = "Build"
        '
        'txtInstructions
        '
        Me.txtInstructions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInstructions.BackColor = System.Drawing.SystemColors.Control
        Me.txtInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructions.Location = New System.Drawing.Point(-4, 0)
        Me.txtInstructions.Multiline = True
        Me.txtInstructions.Name = "txtInstructions"
        Me.txtInstructions.Size = New System.Drawing.Size(588, 62)
        Me.txtInstructions.TabIndex = 2
        Me.txtInstructions.TabStop = False
        '
        'txtSelected
        '
        Me.txtSelected.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelected.BackColor = System.Drawing.SystemColors.Menu
        Me.txtSelected.Location = New System.Drawing.Point(-1, 60)
        Me.txtSelected.Multiline = True
        Me.txtSelected.Name = "txtSelected"
        Me.txtSelected.Size = New System.Drawing.Size(584, 70)
        Me.txtSelected.TabIndex = 3
        Me.txtSelected.TabStop = False
        Me.txtSelected.Text = "Selected:"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(524, 412)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 28)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        '
        'chkHSPF
        '
        Me.chkHSPF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkHSPF.AutoSize = True
        Me.chkHSPF.Checked = True
        Me.chkHSPF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHSPF.Location = New System.Drawing.Point(12, 238)
        Me.chkHSPF.Name = "chkHSPF"
        Me.chkHSPF.Size = New System.Drawing.Size(54, 17)
        Me.chkHSPF.TabIndex = 5
        Me.chkHSPF.Text = "HSPF"
        Me.chkHSPF.UseVisualStyleBackColor = True
        '
        'chkSWAT
        '
        Me.chkSWAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSWAT.AutoSize = True
        Me.chkSWAT.Checked = True
        Me.chkSWAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSWAT.Location = New System.Drawing.Point(72, 238)
        Me.chkSWAT.Name = "chkSWAT"
        Me.chkSWAT.Size = New System.Drawing.Size(58, 17)
        Me.chkSWAT.TabIndex = 6
        Me.chkSWAT.Text = "SWAT"
        Me.chkSWAT.UseVisualStyleBackColor = True
        '
        'lblSize
        '
        Me.lblSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(69, 267)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(216, 13)
        Me.lblSize.TabIndex = 8
        Me.lblSize.Text = "Minimum Catchment Size (square kilometers)"
        '
        'lblLength
        '
        Me.lblLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLength.AutoSize = True
        Me.lblLength.Location = New System.Drawing.Point(69, 291)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(181, 13)
        Me.lblLength.TabIndex = 10
        Me.lblLength.Text = "Minimum Flowline Length (kilometers)"
        '
        'lblLU
        '
        Me.lblLU.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLU.AutoSize = True
        Me.lblLU.Location = New System.Drawing.Point(69, 315)
        Me.lblLU.Name = "lblLU"
        Me.lblLU.Size = New System.Drawing.Size(184, 13)
        Me.lblLU.TabIndex = 12
        Me.lblLU.Text = "Ignore Landuse Areas Below Fraction"
        '
        'cmdSet
        '
        Me.cmdSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSet.Location = New System.Drawing.Point(575, 386)
        Me.cmdSet.Name = "cmdSet"
        Me.cmdSet.Size = New System.Drawing.Size(29, 20)
        Me.cmdSet.TabIndex = 13
        Me.cmdSet.Text = "..."
        Me.cmdSet.UseVisualStyleBackColor = True
        '
        'lblSWAT
        '
        Me.lblSWAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSWAT.AutoSize = True
        Me.lblSWAT.Location = New System.Drawing.Point(12, 391)
        Me.lblSWAT.Name = "lblSWAT"
        Me.lblSWAT.Size = New System.Drawing.Size(107, 13)
        Me.lblSWAT.TabIndex = 14
        Me.lblSWAT.Text = "SWAT Database File"
        '
        'lblFile
        '
        Me.lblFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFile.Location = New System.Drawing.Point(125, 389)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFile.Size = New System.Drawing.Size(444, 15)
        Me.lblFile.TabIndex = 12
        Me.lblFile.Text = "SWAT2005.mdb"
        Me.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSimulationStartYear
        '
        Me.lblSimulationStartYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSimulationStartYear.AutoSize = True
        Me.lblSimulationStartYear.Location = New System.Drawing.Point(69, 339)
        Me.lblSimulationStartYear.Name = "lblSimulationStartYear"
        Me.lblSimulationStartYear.Size = New System.Drawing.Size(105, 13)
        Me.lblSimulationStartYear.TabIndex = 17
        Me.lblSimulationStartYear.Text = "Simulation Start Year"
        '
        'lblSimulationEndYear
        '
        Me.lblSimulationEndYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSimulationEndYear.AutoSize = True
        Me.lblSimulationEndYear.Location = New System.Drawing.Point(69, 363)
        Me.lblSimulationEndYear.Name = "lblSimulationEndYear"
        Me.lblSimulationEndYear.Size = New System.Drawing.Size(102, 13)
        Me.lblSimulationEndYear.TabIndex = 19
        Me.lblSimulationEndYear.Text = "Simulation End Year"
        '
        'tabsAreaSelection
        '
        Me.tabsAreaSelection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabsAreaSelection.Controls.Add(Me.tabDelineateWatershed)
        Me.tabsAreaSelection.Controls.Add(Me.tabSelectOnMap)
        Me.tabsAreaSelection.Controls.Add(Me.tabSelectList)
        Me.tabsAreaSelection.Location = New System.Drawing.Point(12, 64)
        Me.tabsAreaSelection.Name = "tabsAreaSelection"
        Me.tabsAreaSelection.SelectedIndex = 0
        Me.tabsAreaSelection.Size = New System.Drawing.Size(592, 159)
        Me.tabsAreaSelection.TabIndex = 0
        '
        'tabDelineateWatershed
        '
        Me.tabDelineateWatershed.BackColor = System.Drawing.Color.Transparent
        Me.tabDelineateWatershed.Controls.Add(Me.btnGetDelineatedWS)
        Me.tabDelineateWatershed.Controls.Add(Me.btnGetFlowlines)
        Me.tabDelineateWatershed.Controls.Add(Me.txtMaxKM)
        Me.tabDelineateWatershed.Controls.Add(Me.Label3)
        Me.tabDelineateWatershed.Controls.Add(Me.txtLongitude)
        Me.tabDelineateWatershed.Controls.Add(Me.Label2)
        Me.tabDelineateWatershed.Controls.Add(Me.txtLatitude)
        Me.tabDelineateWatershed.Controls.Add(Me.Label1)
        Me.tabDelineateWatershed.Controls.Add(Me.btnSelectPourPt)
        Me.tabDelineateWatershed.Controls.Add(Me.txtSelectedDW)
        Me.tabDelineateWatershed.Controls.Add(Me.txtInstructionDW)
        Me.tabDelineateWatershed.Location = New System.Drawing.Point(4, 22)
        Me.tabDelineateWatershed.Name = "tabDelineateWatershed"
        Me.tabDelineateWatershed.Size = New System.Drawing.Size(584, 133)
        Me.tabDelineateWatershed.TabIndex = 2
        Me.tabDelineateWatershed.Text = "Delineate Watershed"
        '
        'btnGetDelineatedWS
        '
        Me.btnGetDelineatedWS.Location = New System.Drawing.Point(195, 101)
        Me.btnGetDelineatedWS.Name = "btnGetDelineatedWS"
        Me.btnGetDelineatedWS.Size = New System.Drawing.Size(151, 24)
        Me.btnGetDelineatedWS.TabIndex = 13
        Me.btnGetDelineatedWS.Text = "Get Delineated Watershed"
        Me.btnGetDelineatedWS.UseVisualStyleBackColor = True
        '
        'btnGetFlowlines
        '
        Me.btnGetFlowlines.Location = New System.Drawing.Point(5, 66)
        Me.btnGetFlowlines.Name = "btnGetFlowlines"
        Me.btnGetFlowlines.Size = New System.Drawing.Size(88, 24)
        Me.btnGetFlowlines.TabIndex = 12
        Me.btnGetFlowlines.Text = "Get Flowlines"
        Me.btnGetFlowlines.UseVisualStyleBackColor = True
        '
        'txtMaxKM
        '
        Me.txtMaxKM.Location = New System.Drawing.Point(511, 69)
        Me.txtMaxKM.Name = "txtMaxKM"
        Me.txtMaxKM.Size = New System.Drawing.Size(67, 20)
        Me.txtMaxKM.TabIndex = 11
        Me.txtMaxKM.Text = "20"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(459, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Max KM"
        '
        'txtLongitude
        '
        Me.txtLongitude.Location = New System.Drawing.Point(358, 69)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.Size = New System.Drawing.Size(88, 20)
        Me.txtLongitude.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(327, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Long"
        '
        'txtLatitude
        '
        Me.txtLatitude.Location = New System.Drawing.Point(237, 69)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.Size = New System.Drawing.Size(88, 20)
        Me.txtLatitude.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(214, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Lat"
        '
        'btnSelectPourPt
        '
        Me.btnSelectPourPt.Location = New System.Drawing.Point(99, 66)
        Me.btnSelectPourPt.Name = "btnSelectPourPt"
        Me.btnSelectPourPt.Size = New System.Drawing.Size(109, 24)
        Me.btnSelectPourPt.TabIndex = 5
        Me.btnSelectPourPt.Text = "Select Pour Point"
        Me.btnSelectPourPt.UseVisualStyleBackColor = True
        '
        'txtSelectedDW
        '
        Me.txtSelectedDW.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectedDW.BackColor = System.Drawing.SystemColors.Menu
        Me.txtSelectedDW.Location = New System.Drawing.Point(3, 35)
        Me.txtSelectedDW.Multiline = True
        Me.txtSelectedDW.Name = "txtSelectedDW"
        Me.txtSelectedDW.Size = New System.Drawing.Size(578, 57)
        Me.txtSelectedDW.TabIndex = 4
        Me.txtSelectedDW.TabStop = False
        Me.txtSelectedDW.Text = "Selected:"
        '
        'txtInstructionDW
        '
        Me.txtInstructionDW.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInstructionDW.BackColor = System.Drawing.SystemColors.Control
        Me.txtInstructionDW.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstructionDW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructionDW.Location = New System.Drawing.Point(3, 3)
        Me.txtInstructionDW.Multiline = True
        Me.txtInstructionDW.Name = "txtInstructionDW"
        Me.txtInstructionDW.Size = New System.Drawing.Size(578, 28)
        Me.txtInstructionDW.TabIndex = 3
        Me.txtInstructionDW.TabStop = False
        '
        'tabSelectOnMap
        '
        Me.tabSelectOnMap.Controls.Add(Me.txtSelected)
        Me.tabSelectOnMap.Controls.Add(Me.txtInstructions)
        Me.tabSelectOnMap.Location = New System.Drawing.Point(4, 22)
        Me.tabSelectOnMap.Name = "tabSelectOnMap"
        Me.tabSelectOnMap.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSelectOnMap.Size = New System.Drawing.Size(584, 133)
        Me.tabSelectOnMap.TabIndex = 0
        Me.tabSelectOnMap.Text = "Select One Project Area On Map"
        Me.tabSelectOnMap.UseVisualStyleBackColor = True
        '
        'tabSelectList
        '
        Me.tabSelectList.Controls.Add(Me.lblHucList)
        Me.tabSelectList.Controls.Add(Me.txtHucList)
        Me.tabSelectList.Location = New System.Drawing.Point(4, 22)
        Me.tabSelectList.Name = "tabSelectList"
        Me.tabSelectList.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSelectList.Size = New System.Drawing.Size(584, 133)
        Me.tabSelectList.TabIndex = 1
        Me.tabSelectList.Text = "Specify List of HUCs to Build Separate Projects"
        Me.tabSelectList.UseVisualStyleBackColor = True
        '
        'lblHucList
        '
        Me.lblHucList.AutoSize = True
        Me.lblHucList.Location = New System.Drawing.Point(3, 3)
        Me.lblHucList.Name = "lblHucList"
        Me.lblHucList.Size = New System.Drawing.Size(453, 13)
        Me.lblHucList.TabIndex = 9
        Me.lblHucList.Text = "Paste or type 8 or 12-digit hydrologic unit codes separated by any delimiter or o" & _
    "n separate lines:"
        '
        'txtHucList
        '
        Me.txtHucList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHucList.Location = New System.Drawing.Point(0, 19)
        Me.txtHucList.Multiline = True
        Me.txtHucList.Name = "txtHucList"
        Me.txtHucList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHucList.Size = New System.Drawing.Size(584, 114)
        Me.txtHucList.TabIndex = 0
        '
        'txtSimulationEndYear
        '
        Me.txtSimulationEndYear.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSimulationEndYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSimulationEndYear.DataType = atcControls.atcText.ATCoDataType.ATCoInt
        Me.txtSimulationEndYear.DefaultValue = ""
        Me.txtSimulationEndYear.HardMax = -999.0R
        Me.txtSimulationEndYear.HardMin = -999.0R
        Me.txtSimulationEndYear.InsideLimitsBackground = System.Drawing.Color.White
        Me.txtSimulationEndYear.Location = New System.Drawing.Point(12, 358)
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
        Me.txtSimulationEndYear.TabIndex = 11
        Me.txtSimulationEndYear.ValueDouble = 0.0R
        Me.txtSimulationEndYear.ValueInteger = 0
        '
        'txtSimulationStartYear
        '
        Me.txtSimulationStartYear.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSimulationStartYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSimulationStartYear.DataType = atcControls.atcText.ATCoDataType.ATCoInt
        Me.txtSimulationStartYear.DefaultValue = ""
        Me.txtSimulationStartYear.HardMax = -999.0R
        Me.txtSimulationStartYear.HardMin = -999.0R
        Me.txtSimulationStartYear.InsideLimitsBackground = System.Drawing.Color.White
        Me.txtSimulationStartYear.Location = New System.Drawing.Point(12, 334)
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
        Me.txtSimulationStartYear.TabIndex = 10
        Me.txtSimulationStartYear.ValueDouble = 0.0R
        Me.txtSimulationStartYear.ValueInteger = 0
        '
        'atxLU
        '
        Me.atxLU.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLU.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.atxLU.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLU.DefaultValue = ""
        Me.atxLU.HardMax = -999.0R
        Me.atxLU.HardMin = -999.0R
        Me.atxLU.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLU.Location = New System.Drawing.Point(12, 310)
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
        Me.atxLU.TabIndex = 9
        Me.atxLU.ValueDouble = 0.0R
        Me.atxLU.ValueInteger = 0
        '
        'atxLength
        '
        Me.atxLength.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.atxLength.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLength.DefaultValue = ""
        Me.atxLength.HardMax = -999.0R
        Me.atxLength.HardMin = -999.0R
        Me.atxLength.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLength.Location = New System.Drawing.Point(12, 286)
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
        Me.atxLength.TabIndex = 8
        Me.atxLength.ValueDouble = 0.0R
        Me.atxLength.ValueInteger = 0
        '
        'atxSize
        '
        Me.atxSize.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.atxSize.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxSize.DefaultValue = ""
        Me.atxSize.HardMax = -999.0R
        Me.atxSize.HardMin = -999.0R
        Me.atxSize.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxSize.Location = New System.Drawing.Point(12, 261)
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
        Me.atxSize.TabIndex = 7
        Me.atxSize.ValueDouble = 0.0R
        Me.atxSize.ValueInteger = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rdoHUC12)
        Me.GroupBox1.Controls.Add(Me.rdoHUC8)
        Me.GroupBox1.Controls.Add(Me.lblProjectSize)
        Me.GroupBox1.Controls.Add(Me.btnFind)
        Me.GroupBox1.Controls.Add(Me.txtFind)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(584, 46)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        '
        'rdoHUC12
        '
        Me.rdoHUC12.AutoSize = True
        Me.rdoHUC12.Checked = True
        Me.rdoHUC12.Location = New System.Drawing.Point(143, 16)
        Me.rdoHUC12.Name = "rdoHUC12"
        Me.rdoHUC12.Size = New System.Drawing.Size(63, 17)
        Me.rdoHUC12.TabIndex = 28
        Me.rdoHUC12.TabStop = True
        Me.rdoHUC12.Text = "HUC-12"
        Me.rdoHUC12.UseVisualStyleBackColor = True
        '
        'rdoHUC8
        '
        Me.rdoHUC8.AutoSize = True
        Me.rdoHUC8.Location = New System.Drawing.Point(80, 15)
        Me.rdoHUC8.Name = "rdoHUC8"
        Me.rdoHUC8.Size = New System.Drawing.Size(57, 17)
        Me.rdoHUC8.TabIndex = 27
        Me.rdoHUC8.Text = "HUC-8"
        Me.rdoHUC8.UseVisualStyleBackColor = True
        '
        'lblProjectSize
        '
        Me.lblProjectSize.AutoSize = True
        Me.lblProjectSize.Location = New System.Drawing.Point(8, 17)
        Me.lblProjectSize.Name = "lblProjectSize"
        Me.lblProjectSize.Size = New System.Drawing.Size(69, 13)
        Me.lblProjectSize.TabIndex = 31
        Me.lblProjectSize.Text = "Navigate To:"
        '
        'btnFind
        '
        Me.btnFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFind.Location = New System.Drawing.Point(499, 13)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 21)
        Me.btnFind.TabIndex = 30
        Me.btnFind.Text = "Find on Map"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFind.Location = New System.Drawing.Point(211, 13)
        Me.txtFind.Multiline = True
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(279, 20)
        Me.txtFind.TabIndex = 29
        '
        'frmBuildNew
        '
        Me.AcceptButton = Me.btnBuild
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(616, 452)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblSimulationEndYear)
        Me.Controls.Add(Me.txtSimulationEndYear)
        Me.Controls.Add(Me.lblSimulationStartYear)
        Me.Controls.Add(Me.txtSimulationStartYear)
        Me.Controls.Add(Me.lblFile)
        Me.Controls.Add(Me.lblSWAT)
        Me.Controls.Add(Me.cmdSet)
        Me.Controls.Add(Me.lblLU)
        Me.Controls.Add(Me.atxLU)
        Me.Controls.Add(Me.lblLength)
        Me.Controls.Add(Me.atxLength)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.atxSize)
        Me.Controls.Add(Me.chkSWAT)
        Me.Controls.Add(Me.chkHSPF)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnBuild)
        Me.Controls.Add(Me.tabsAreaSelection)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmBuildNew"
        Me.Text = "Build Project"
        Me.TopMost = True
        Me.tabsAreaSelection.ResumeLayout(False)
        Me.tabDelineateWatershed.ResumeLayout(False)
        Me.tabDelineateWatershed.PerformLayout()
        Me.tabSelectOnMap.ResumeLayout(False)
        Me.tabSelectOnMap.PerformLayout()
        Me.tabSelectList.ResumeLayout(False)
        Me.tabSelectList.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private MouseClickLocation As New System.Drawing.Point

    Private Sub cmdBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuild.Click
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildTop", Me.Top)
        SaveSetting(g_AppNameRegistry, "Window Positions", "BuildLeft", Me.Left)

        params.SetupHSPF = chkHSPF.Checked
        params.SetupSWAT = chkSWAT.Checked
        params.MinCatchmentKM2 = atxSize.ValueDouble
        params.MinFlowlineKM = atxLength.ValueDouble
        params.LandUseIgnoreBelowFraction = atxLU.ValueDouble
        params.SimulationStartYear = txtSimulationStartYear.ValueInteger
        params.SimulationEndYear = txtSimulationEndYear.ValueInteger
        params.SWATDatabaseName = lblFile.Text
        params.CacheFolder = g_NationalProject.CacheFolder

        If tabsAreaSelection.SelectedTab Is tabSelectList Then
            params.SelectedKeys = ParseNumericKeys(12, txtHucList.Text)
            If params.SelectedKeys.Count = 0 Then
                params.SelectedKeys = ParseNumericKeys(8, txtHucList.Text)
            End If
        ElseIf tabsAreaSelection.SelectedTab Is tabDelineateWatershed Then
            params.PourPointLongitude = txtLongitude.Text
            params.PourPointLatitude = txtLatitude.Text
            params.PourPointMaxKm = txtMaxKM.Text
        ElseIf tabsAreaSelection.SelectedTab Is tabSelectOnMap Then

        End If

        Me.Visible = False

        Dim NationalProjectFolder As String = g_AppManager.SerializationManager.CurrentProjectDirectory
        params.ProjectsPath = IO.Path.GetDirectoryName(NationalProjectFolder)
        params.WriteParametersTextFile(IO.Path.Combine(NationalProjectFolder, PARAMETER_FILE))

        g_Map.Layers.Clear()
        g_Map.Projection = params.DesiredProjection
        'SDM_Project_Builder_Batch.Run(IO.Path.Combine(g_AppManager.SerializationManager.CurrentProjectDirectory, PARAMETER_FILE))
        SpecifyAndCreateNewProjectsWithLayerCollectionChanged(params)

        Me.Close()
    End Sub

    ''' <summary>
    ''' Prepend a zero if needed to make sure string has even number of characters
    ''' </summary>
    Private Function HucStringRoundLength(ByVal aHucString As String) As String
        If Math.Floor((aHucString.Length) / 2) * 2 < (aHucString.Length) Then
            Return "0" & aHucString
        Else
            Return aHucString
        End If
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Public Sub Initialize()
        'Me.Icon = g_MapWin.ApplicationInfo.FormIcon
        Me.Text = "Build " & g_AppNameLong & " Project"

        chkHSPF.Checked = params.SetupHSPF
        chkSWAT.Checked = params.SetupSWAT
        atxSize.ValueDouble = params.MinCatchmentKM2
        atxLength.ValueDouble = params.MinFlowlineKM
        atxLU.ValueDouble = params.LandUseIgnoreBelowFraction
        txtSimulationStartYear.ValueInteger = params.SimulationStartYear
        txtSimulationEndYear.ValueInteger = params.SimulationEndYear
        lblFile.Text = params.SWATDatabaseName

        txtLatitude.Text = params.PourPointLatitude
        txtLongitude.Text = params.PourPointLongitude
        txtMaxKM.Text = params.PourPointMaxKm

        If params.SelectedKeys.Count > 0 Then
            txtHucList.Text = String.Join(vbCrLf, params.SelectedKeys.ToArray())
        End If
    End Sub

    Private Sub cmdSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSet.Click
        params.SWATDatabaseName = FindFile("Please locate SWAT2005.mdb", "SWAT2005.mdb").Replace("swat", "SWAT")
        lblFile.Text = params.SWATDatabaseName
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Find()
    End Sub

    Private Sub Find()
        FindText(txtFind.Text)
    End Sub

    Public Function FindText(ByVal aText As String) As Boolean
        Dim lMatchingRecord As Integer = -1
        If aText.Length > 0 Then
            Dim lLayerHandle As DotSpatial.Symbology.ILayer = Nothing
            Dim lLoadedHuc12 As Boolean = False
            If IsNumeric(aText) Then 'Numeric search
                Select Case aText.Length
                    Case 8 : lLayerHandle = Huc8Layer()
                    Case 12
FindHuc12:
                        Dim lHuc8 As String = SafeSubstring(aText, 0, 8)

                        For Each lSearchLayer In g_Map.GetAllLayers
                            'If lSearchLayer.LegendText.ToLower.Contains(lHuc8 & g_PathChar & "huc12") Then
                            If lSearchLayer.LegendText.ToLower.Contains("huc12" & "_" & lHuc8) Then
                                lLayerHandle = lSearchLayer
                                Exit For
                            End If
                        Next

                        If lLayerHandle Is Nothing AndAlso Not lLoadedHuc12 Then
                            lLoadedHuc12 = True
                            LoadHUC12(lHuc8)
                            GoTo FindHuc12
                        End If

                        If lLayerHandle Is Nothing Then
                            lLayerHandle = Huc8Layer()
                            If lLayerHandle IsNot Nothing Then
                                aText = lHuc8
                            End If
                        End If
                    Case Else
                        'TODO: partial search for number that is not 8 or 12 digits?
                End Select
                If lLayerHandle IsNot Nothing AndAlso g_Map IsNot Nothing Then
                    Dim lDataSet As DotSpatial.Data.DataSet = lLayerHandle.DataSet
                    If Not String.IsNullOrWhiteSpace(DotSpatialDataSetFilename(lDataSet)) Then
                        lMatchingRecord = MatchingKeyRecord(DotSpatialDataSetFilename(lDataSet), aText)
                        Dim fl As DotSpatial.Symbology.IFeatureLayer
                        fl = CType(lLayerHandle, DotSpatial.Symbology.IFeatureLayer)
                        Dim sField As String = DBFKeyFieldName(fl.DataSet.Filename)
                        fl.SelectByAttribute(sField + " = " + aText)
                        fl.ZoomToSelectedFeatures()
                    End If
                    If aText.Length = 8 AndAlso Not lLoadedHuc12 AndAlso rdoHUC12.Checked Then
                        lLoadedHuc12 = True
                        LoadHUC12(aText)
                    End If
                End If
            Else
                Throw New NotImplementedException("Need filename of DotSpatial layer")
            End If
        End If
        UpdateSelectedFeatures()
        Return (lMatchingRecord >= 0)
    End Function

    Private Sub rdoHUC8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoHUC8.CheckedChanged
        If rdoHUC8.Checked Then
            chkHSPF.Checked = False
            'Remove HUC-12 layers from map
            Dim lRemoveThese As New Generic.List(Of DotSpatial.Controls.IMapLayer)

            For Each lSearchLayer In g_Map.GetAllLayers
                'KW 9/28/2011 - PointShapefile layer didn't have a Filename
                Dim fileName As String = DotSpatialDataSetFilename(lSearchLayer.DataSet)
                If fileName IsNot Nothing AndAlso fileName.ToLower.EndsWith("huc12.shp") Then
                    lRemoveThese.Add(lSearchLayer)
                End If
            Next

            For Each lLayer As DotSpatial.Controls.IMapLayer In lRemoveThese
                g_Map.Layers.Remove(lLayer)
            Next
        Else
        End If
        UpdateSelectedFeatures()
    End Sub

    'Private Sub chkHSPF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHSPF.CheckedChanged
    '    If chkHSPF.Checked AndAlso rdoHUC8.Checked Then
    '        rdoHUC12.Checked = True
    '    End If
    'End Sub

    'Private Sub txtFind_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Windows.Forms.Keys.Enter Then
    '        e.Handled = True
    '        Find()
    '    End If
    'End Sub

    'Private Sub txtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = vbCr Then
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub txtFind_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Windows.Forms.Keys.Enter Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub btnGetFlowlines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlowlines.Click
        If (params.SelectedKeys.Count > 0) Then
            'LoadNationalNHDPlus(params.SelectedKeys(0))
            LoadLayerForHUC8(D4EM.Data.Region.RegionTypes.catchment, params.SelectedKeys(0))
        End If
    End Sub

    Private Sub MapMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            'Must satisfy these three prerequisites to trig the delineation
            If ((e.Button = System.Windows.Forms.MouseButtons.Left) And (g_Map.Cursor = System.Windows.Forms.Cursors.Cross)) Then
                MouseClickLocation = New System.Drawing.Point
                MouseClickLocation.X = e.X
                MouseClickLocation.Y = e.Y

                g_Map.Cursor = System.Windows.Forms.Cursors.WaitCursor

                Dim coord As DotSpatial.Topology.Coordinate = g_Map.PixelToProj(MouseClickLocation)

                Dim xy(2) As Double
                xy(0) = coord.X
                xy(1) = coord.Y
                Dim z(1) As Double
                DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, g_NationalProject.DesiredProjection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1)

                coord.X = xy(0)
                coord.Y = xy(1)

                Dim pourPoint As D4EM.Data.Source.EPAWaters.PourPoint = D4EM.Data.Source.EPAWaters.GetPourPoint(g_NationalProject.CacheFolder, xy(1), xy(0))
                txtLatitude.Text = pourPoint.Latitude.ToString()
                txtLongitude.Text = pourPoint.Longitude.ToString()
                Dim measure As Double = pourPoint.Measure
                Dim comId As Long = pourPoint.COMID

                'Add point to the map
                xy(0) = pourPoint.Longitude
                xy(1) = pourPoint.Latitude
                Reproject.ReprojectPoints(xy, z, KnownCoordinateSystems.Geographic.World.WGS1984, g_NationalProject.DesiredProjection, 0, 1)
                coord.X = xy(0)
                coord.Y = xy(1)

                Dim point As Feature = New Feature(coord)
                Dim fsPoint As IFeatureSet = New FeatureSet(point.FeatureType)
                fsPoint.Projection = g_NationalProject.DesiredProjection
                fsPoint.Name = "PourPoint"
                fsPoint.AddFeature(point)
                g_Map.Layers.Add(fsPoint)

                Dim maxKm As Double
                If Not Double.TryParse(txtMaxKM.Text, maxKm) Then
                    maxKm = 20
                End If
                Dim watershed = D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, comId, comId, maxKm, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                g_Map.Layers.Add(watershed.AsFeatureSet())

                Dim catchments = D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, comId, comId, maxKm, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment)
                g_Map.Layers.Add(catchments.AsFeatureSet())

                'Dim frmPourPoint As frmSelectPourPoint = New frmSelectPourPoint(pourPoint.Latitude, pourPoint.Longitude, pourPoint.Measure, pourPoint.COMID)
                'If (frmPourPoint.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                '    txtLatitude.Text = frmPourPoint.Latitude
                '    txtLongitude.Text = frmPourPoint.Longitude
                'End If

            End If
        Catch ex As Exception

        Finally
            g_Map.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub btnSelectPourPt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPourPt.Click
        'g_Map.FunctionMode = DotSpatial.Controls.FunctionMode.Select
        g_Map.Cursor = System.Windows.Forms.Cursors.Cross
        'Set focus back on main form
        Me.SendToBack()
    End Sub

    Private Sub btnGetDelineatedWS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDelineatedWS.Click
        Dim comId As Long = 19416909

        Dim watershed = D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, comId, comId, 20, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
        g_Map.Layers.Add(watershed.AsFeatureSet())

        Dim catchments = D4EM.Data.Source.EPAWaters.GetLayer(g_NationalProject, comId, comId, 20, D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment)
        g_Map.Layers.Add(catchments.AsFeatureSet())
    End Sub

    Friend Sub UpdateSelectedFeatures()
        If NationalProjectIsOpen() Then

            Dim ctext As String = "Selected:" & vbCrLf & "  <none>"
            'For Each lLayer As DotSpatial.Symbology.ILayer In g_Map.GetAllLayers
            Dim lLayer As DotSpatial.Symbology.ILayer = g_Map.Layers.SelectedLayer
            'If lLayer.IsSelected Then
            Try
                Dim lFeatureLayer As DotSpatial.Symbology.IFeatureLayer = lLayer
                lFeatureLayer.DataSet.FillAttributes()
                Dim lFeatures As Generic.List(Of DotSpatial.Data.IFeature) = lFeatureLayer.Selection.ToFeatureList
                If lFeatures.Count > 0 Then
                    g_Map.Layers.SuspendEvents()

                    params.SelectedKeys.Clear()

                    ctext = lFeatures.Count & " Selected:"
                    Dim lFieldName As String = DBFKeyFieldName(lFeatureLayer.DataSet.Filename).ToLower
                    Dim lFieldDesc As String = DBFDescriptionFieldName(lFeatureLayer.DataSet.Filename).ToLower
                    Dim lNameIndex As Integer = -1
                    Dim lDescIndex As Integer = -1
                    Dim lName As String
                    Dim lDesc As String

                    For lField = 0 To lFeatureLayer.DataSet.DataTable.Columns.Count - 1
                        Select Case lFeatureLayer.DataSet.DataTable.Columns(lField).ColumnName.ToLower
                            Case lFieldName : lNameIndex = lField
                            Case lFieldDesc : lDescIndex = lField
                        End Select
                    Next

                    For Each lFeature As DotSpatial.Data.IFeature In lFeatureLayer.Selection.ToFeatureList
                        Debug.WriteLine(lFeature.DataRow.Item(lFieldName))

                        lName = ""
                        lDesc = ""
                        Dim lLoadingHUC12 As Boolean = False

                        If lNameIndex > -1 Then
                            lName = lFeature.DataRow.Item(lNameIndex)
                            If lName.Length = 8 AndAlso (lFieldName = "cu" OrElse lFieldName = "huc_8") AndAlso rdoHUC12.Checked Then
                                lLoadingHUC12 = True
                                LoadHUC12(lName) 'Make sure HUC-12 layer matching this HUC-8 layer is on the map
                            End If
                        End If
                        If Not lLoadingHUC12 Then
                            If lDescIndex > -1 Then
                                lDesc = lFeature.DataRow.Item(lDescIndex)
                            End If
                            If Not String.IsNullOrEmpty(lName) Then
                                params.SelectedKeys.Add(lName)
                                params.SelectionLayer = lFeatureLayer.DataSet.Filename
                            End If
                            If (lName & lDesc).Length = 0 Then
                                ctext &= vbCrLf & "  " & lFeature.ToString
                            Else
                                ctext &= vbCrLf & "  " & lName & " : " & lDesc
                            End If
                        End If
                    Next

                End If
            Catch ex As Exception
                MapWinUtility.Logger.Dbg("UpdateSelectedFeatures: " & ex.Message & vbCrLf & ex.StackTrace.ToString)
            End Try
            g_Map.Layers.ResumeEvents()
            'End If
            'Next
            txtSelected.Text = ctext
            txtSelectedDW.Text = ctext
        End If
    End Sub

End Class
