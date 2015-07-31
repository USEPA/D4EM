<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPublishMain
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
        Me.grpChooseModel = New System.Windows.Forms.GroupBox()
        Me.btnSWAT = New System.Windows.Forms.Button()
        Me.btnHSPF = New System.Windows.Forms.Button()
        Me.grpChooseInputFiles = New System.Windows.Forms.GroupBox()
        Me.btnAddInputFiles = New System.Windows.Forms.Button()
        Me.lstInputFiles = New System.Windows.Forms.CheckedListBox()
        Me.grpProgress = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.barProgress = New System.Windows.Forms.ProgressBar()
        Me.grpMapLocations = New System.Windows.Forms.GroupBox()
        Me.lblLocationPublish = New System.Windows.Forms.Label()
        Me.chkLocation2 = New System.Windows.Forms.CheckBox()
        Me.chkLocation1 = New System.Windows.Forms.CheckBox()
        Me.btnNHDLookup = New System.Windows.Forms.Button()
        Me.btnNHDPlusNext = New System.Windows.Forms.Button()
        Me.txtLocationID2 = New System.Windows.Forms.TextBox()
        Me.txtLocationID1 = New System.Windows.Forms.TextBox()
        Me.lblLocation2 = New System.Windows.Forms.Label()
        Me.lblLocation1 = New System.Windows.Forms.Label()
        Me.lblLocationID = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.grpMetadata = New System.Windows.Forms.GroupBox()
        Me.txtAuthorPhone = New System.Windows.Forms.TextBox()
        Me.lblAuthorPhone = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.grpMetadataCalibration = New System.Windows.Forms.GroupBox()
        Me.lblCalibration2 = New System.Windows.Forms.Label()
        Me.txtNashSutcliffe2 = New System.Windows.Forms.TextBox()
        Me.txtCorrelationCoefficient2 = New System.Windows.Forms.TextBox()
        Me.lblCalibration1 = New System.Windows.Forms.Label()
        Me.txtNashSutcliffe1 = New System.Windows.Forms.TextBox()
        Me.lblNashSutcliffe = New System.Windows.Forms.Label()
        Me.txtCorrelationCoefficient1 = New System.Windows.Forms.TextBox()
        Me.lblCorrelationCoefficients = New System.Windows.Forms.Label()
        Me.txtOrganization = New System.Windows.Forms.TextBox()
        Me.lblOrganization = New System.Windows.Forms.Label()
        Me.txtAuthorEmail = New System.Windows.Forms.TextBox()
        Me.lblAuthorEmail = New System.Windows.Forms.Label()
        Me.lblAuthor2 = New System.Windows.Forms.Label()
        Me.lblAuthor1 = New System.Windows.Forms.Label()
        Me.lblAuthor3 = New System.Windows.Forms.Label()
        Me.txtAuthor3 = New System.Windows.Forms.TextBox()
        Me.txtAuthor2 = New System.Windows.Forms.TextBox()
        Me.txtAuthor1 = New System.Windows.Forms.TextBox()
        Me.lblAuthorName = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.grpChooseFiles = New System.Windows.Forms.GroupBox()
        Me.splitChooseFiles = New System.Windows.Forms.SplitContainer()
        Me.grpChooseOutputFiles = New System.Windows.Forms.GroupBox()
        Me.btnAddOutputFiles = New System.Windows.Forms.Button()
        Me.lstOutputFiles = New System.Windows.Forms.CheckedListBox()
        Me.btnChooseFilesNext = New System.Windows.Forms.Button()
        Me.grpChooseModel.SuspendLayout()
        Me.grpChooseInputFiles.SuspendLayout()
        Me.grpProgress.SuspendLayout()
        Me.grpMapLocations.SuspendLayout()
        Me.grpMetadata.SuspendLayout()
        Me.grpMetadataCalibration.SuspendLayout()
        Me.grpChooseFiles.SuspendLayout()
        CType(Me.splitChooseFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitChooseFiles.Panel1.SuspendLayout()
        Me.splitChooseFiles.Panel2.SuspendLayout()
        Me.splitChooseFiles.SuspendLayout()
        Me.grpChooseOutputFiles.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpChooseModel
        '
        Me.grpChooseModel.Controls.Add(Me.btnSWAT)
        Me.grpChooseModel.Controls.Add(Me.btnHSPF)
        Me.grpChooseModel.Location = New System.Drawing.Point(12, 12)
        Me.grpChooseModel.Name = "grpChooseModel"
        Me.grpChooseModel.Size = New System.Drawing.Size(165, 335)
        Me.grpChooseModel.TabIndex = 0
        Me.grpChooseModel.TabStop = False
        Me.grpChooseModel.Text = "Choose Model to Publish"
        '
        'btnSWAT
        '
        Me.btnSWAT.Location = New System.Drawing.Point(9, 87)
        Me.btnSWAT.Name = "btnSWAT"
        Me.btnSWAT.Size = New System.Drawing.Size(116, 23)
        Me.btnSWAT.TabIndex = 2
        Me.btnSWAT.Text = "SWAT"
        Me.btnSWAT.UseVisualStyleBackColor = True
        '
        'btnHSPF
        '
        Me.btnHSPF.Location = New System.Drawing.Point(9, 36)
        Me.btnHSPF.Name = "btnHSPF"
        Me.btnHSPF.Size = New System.Drawing.Size(116, 23)
        Me.btnHSPF.TabIndex = 0
        Me.btnHSPF.Text = "HSPF"
        Me.btnHSPF.UseVisualStyleBackColor = True
        '
        'grpChooseInputFiles
        '
        Me.grpChooseInputFiles.Controls.Add(Me.btnAddInputFiles)
        Me.grpChooseInputFiles.Controls.Add(Me.lstInputFiles)
        Me.grpChooseInputFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpChooseInputFiles.Location = New System.Drawing.Point(0, 0)
        Me.grpChooseInputFiles.Name = "grpChooseInputFiles"
        Me.grpChooseInputFiles.Size = New System.Drawing.Size(247, 158)
        Me.grpChooseInputFiles.TabIndex = 2
        Me.grpChooseInputFiles.TabStop = False
        Me.grpChooseInputFiles.Text = "Input Files to Publish"
        '
        'btnAddInputFiles
        '
        Me.btnAddInputFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddInputFiles.AutoSize = True
        Me.btnAddInputFiles.Location = New System.Drawing.Point(6, 129)
        Me.btnAddInputFiles.Name = "btnAddInputFiles"
        Me.btnAddInputFiles.Size = New System.Drawing.Size(112, 23)
        Me.btnAddInputFiles.TabIndex = 2
        Me.btnAddInputFiles.Text = "Add Input Files..."
        Me.btnAddInputFiles.UseVisualStyleBackColor = True
        '
        'lstInputFiles
        '
        Me.lstInputFiles.AllowDrop = True
        Me.lstInputFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstInputFiles.CheckOnClick = True
        Me.lstInputFiles.FormattingEnabled = True
        Me.lstInputFiles.IntegralHeight = False
        Me.lstInputFiles.Location = New System.Drawing.Point(6, 19)
        Me.lstInputFiles.Name = "lstInputFiles"
        Me.lstInputFiles.Size = New System.Drawing.Size(235, 104)
        Me.lstInputFiles.TabIndex = 0
        '
        'grpProgress
        '
        Me.grpProgress.Controls.Add(Me.btnClose)
        Me.grpProgress.Controls.Add(Me.lblProgress)
        Me.grpProgress.Controls.Add(Me.barProgress)
        Me.grpProgress.Location = New System.Drawing.Point(3, 12)
        Me.grpProgress.Name = "grpProgress"
        Me.grpProgress.Size = New System.Drawing.Size(380, 246)
        Me.grpProgress.TabIndex = 3
        Me.grpProgress.TabStop = False
        Me.grpProgress.Text = "Processing"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(299, 217)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        Me.btnClose.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(15, 87)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(68, 13)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing..."
        '
        'barProgress
        '
        Me.barProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.barProgress.Location = New System.Drawing.Point(9, 217)
        Me.barProgress.Name = "barProgress"
        Me.barProgress.Size = New System.Drawing.Size(365, 23)
        Me.barProgress.TabIndex = 0
        Me.barProgress.Visible = False
        '
        'grpMapLocations
        '
        Me.grpMapLocations.Controls.Add(Me.lblLocationPublish)
        Me.grpMapLocations.Controls.Add(Me.chkLocation2)
        Me.grpMapLocations.Controls.Add(Me.chkLocation1)
        Me.grpMapLocations.Controls.Add(Me.btnNHDLookup)
        Me.grpMapLocations.Controls.Add(Me.btnNHDPlusNext)
        Me.grpMapLocations.Controls.Add(Me.txtLocationID2)
        Me.grpMapLocations.Controls.Add(Me.txtLocationID1)
        Me.grpMapLocations.Controls.Add(Me.lblLocation2)
        Me.grpMapLocations.Controls.Add(Me.lblLocation1)
        Me.grpMapLocations.Controls.Add(Me.lblLocationID)
        Me.grpMapLocations.Controls.Add(Me.lblLocation)
        Me.grpMapLocations.Location = New System.Drawing.Point(442, 12)
        Me.grpMapLocations.Name = "grpMapLocations"
        Me.grpMapLocations.Size = New System.Drawing.Size(495, 335)
        Me.grpMapLocations.TabIndex = 4
        Me.grpMapLocations.TabStop = False
        Me.grpMapLocations.Text = "Specify NHD Reach Code of Streams to Publish"
        '
        'lblLocationPublish
        '
        Me.lblLocationPublish.AutoSize = True
        Me.lblLocationPublish.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationPublish.Location = New System.Drawing.Point(210, 19)
        Me.lblLocationPublish.Name = "lblLocationPublish"
        Me.lblLocationPublish.Size = New System.Drawing.Size(48, 13)
        Me.lblLocationPublish.TabIndex = 20
        Me.lblLocationPublish.Text = "Publish"
        '
        'chkLocation2
        '
        Me.chkLocation2.AutoSize = True
        Me.chkLocation2.Location = New System.Drawing.Point(243, 68)
        Me.chkLocation2.Name = "chkLocation2"
        Me.chkLocation2.Size = New System.Drawing.Size(15, 14)
        Me.chkLocation2.TabIndex = 19
        Me.chkLocation2.UseVisualStyleBackColor = True
        '
        'chkLocation1
        '
        Me.chkLocation1.AutoSize = True
        Me.chkLocation1.Location = New System.Drawing.Point(243, 41)
        Me.chkLocation1.Name = "chkLocation1"
        Me.chkLocation1.Size = New System.Drawing.Size(15, 14)
        Me.chkLocation1.TabIndex = 18
        Me.chkLocation1.UseVisualStyleBackColor = True
        '
        'btnNHDLookup
        '
        Me.btnNHDLookup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNHDLookup.AutoSize = True
        Me.btnNHDLookup.Location = New System.Drawing.Point(6, 306)
        Me.btnNHDLookup.Name = "btnNHDLookup"
        Me.btnNHDLookup.Size = New System.Drawing.Size(132, 23)
        Me.btnNHDLookup.TabIndex = 17
        Me.btnNHDLookup.Text = "Find NHD Reach Codes"
        Me.btnNHDLookup.UseVisualStyleBackColor = True
        '
        'btnNHDPlusNext
        '
        Me.btnNHDPlusNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNHDPlusNext.Location = New System.Drawing.Point(414, 306)
        Me.btnNHDPlusNext.Name = "btnNHDPlusNext"
        Me.btnNHDPlusNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNHDPlusNext.TabIndex = 16
        Me.btnNHDPlusNext.Text = "Next"
        Me.btnNHDPlusNext.UseVisualStyleBackColor = True
        '
        'txtLocationID2
        '
        Me.txtLocationID2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocationID2.Location = New System.Drawing.Point(264, 65)
        Me.txtLocationID2.Name = "txtLocationID2"
        Me.txtLocationID2.Size = New System.Drawing.Size(225, 20)
        Me.txtLocationID2.TabIndex = 15
        Me.txtLocationID2.Visible = False
        '
        'txtLocationID1
        '
        Me.txtLocationID1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocationID1.Location = New System.Drawing.Point(264, 38)
        Me.txtLocationID1.Name = "txtLocationID1"
        Me.txtLocationID1.Size = New System.Drawing.Size(225, 20)
        Me.txtLocationID1.TabIndex = 14
        '
        'lblLocation2
        '
        Me.lblLocation2.AutoSize = True
        Me.lblLocation2.Location = New System.Drawing.Point(9, 68)
        Me.lblLocation2.Name = "lblLocation2"
        Me.lblLocation2.Size = New System.Drawing.Size(13, 13)
        Me.lblLocation2.TabIndex = 13
        Me.lblLocation2.Text = "2"
        Me.lblLocation2.Visible = False
        '
        'lblLocation1
        '
        Me.lblLocation1.AutoSize = True
        Me.lblLocation1.Location = New System.Drawing.Point(9, 41)
        Me.lblLocation1.Name = "lblLocation1"
        Me.lblLocation1.Size = New System.Drawing.Size(13, 13)
        Me.lblLocation1.TabIndex = 11
        Me.lblLocation1.Text = "1"
        '
        'lblLocationID
        '
        Me.lblLocationID.AutoSize = True
        Me.lblLocationID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationID.Location = New System.Drawing.Point(271, 19)
        Me.lblLocationID.Name = "lblLocationID"
        Me.lblLocationID.Size = New System.Drawing.Size(108, 13)
        Me.lblLocationID.TabIndex = 9
        Me.lblLocationID.Text = "NHD Reach Code"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 19)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(101, 13)
        Me.lblLocation.TabIndex = 8
        Me.lblLocation.Text = "Model Stream ID"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(381, 359)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grpMetadata
        '
        Me.grpMetadata.Controls.Add(Me.txtAuthorPhone)
        Me.grpMetadata.Controls.Add(Me.lblAuthorPhone)
        Me.grpMetadata.Controls.Add(Me.txtDescription)
        Me.grpMetadata.Controls.Add(Me.grpMetadataCalibration)
        Me.grpMetadata.Controls.Add(Me.txtOrganization)
        Me.grpMetadata.Controls.Add(Me.lblOrganization)
        Me.grpMetadata.Controls.Add(Me.txtAuthorEmail)
        Me.grpMetadata.Controls.Add(Me.lblAuthorEmail)
        Me.grpMetadata.Controls.Add(Me.lblAuthor2)
        Me.grpMetadata.Controls.Add(Me.lblAuthor1)
        Me.grpMetadata.Controls.Add(Me.lblAuthor3)
        Me.grpMetadata.Controls.Add(Me.txtAuthor3)
        Me.grpMetadata.Controls.Add(Me.txtAuthor2)
        Me.grpMetadata.Controls.Add(Me.txtAuthor1)
        Me.grpMetadata.Controls.Add(Me.lblAuthorName)
        Me.grpMetadata.Controls.Add(Me.btnSave)
        Me.grpMetadata.Controls.Add(Me.lblDescription)
        Me.grpMetadata.Location = New System.Drawing.Point(505, 12)
        Me.grpMetadata.Name = "grpMetadata"
        Me.grpMetadata.Size = New System.Drawing.Size(462, 388)
        Me.grpMetadata.TabIndex = 5
        Me.grpMetadata.TabStop = False
        Me.grpMetadata.Text = "Metadata"
        '
        'txtAuthorPhone
        '
        Me.txtAuthorPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAuthorPhone.Location = New System.Drawing.Point(84, 84)
        Me.txtAuthorPhone.Name = "txtAuthorPhone"
        Me.txtAuthorPhone.Size = New System.Drawing.Size(372, 20)
        Me.txtAuthorPhone.TabIndex = 5
        '
        'lblAuthorPhone
        '
        Me.lblAuthorPhone.AutoSize = True
        Me.lblAuthorPhone.Location = New System.Drawing.Point(6, 87)
        Me.lblAuthorPhone.Name = "lblAuthorPhone"
        Me.lblAuthorPhone.Size = New System.Drawing.Size(72, 13)
        Me.lblAuthorPhone.TabIndex = 19
        Me.lblAuthorPhone.Text = "Author Phone"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(84, 136)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(372, 53)
        Me.txtDescription.TabIndex = 7
        '
        'grpMetadataCalibration
        '
        Me.grpMetadataCalibration.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpMetadataCalibration.Controls.Add(Me.lblCalibration2)
        Me.grpMetadataCalibration.Controls.Add(Me.txtNashSutcliffe2)
        Me.grpMetadataCalibration.Controls.Add(Me.txtCorrelationCoefficient2)
        Me.grpMetadataCalibration.Controls.Add(Me.lblCalibration1)
        Me.grpMetadataCalibration.Controls.Add(Me.txtNashSutcliffe1)
        Me.grpMetadataCalibration.Controls.Add(Me.lblNashSutcliffe)
        Me.grpMetadataCalibration.Controls.Add(Me.txtCorrelationCoefficient1)
        Me.grpMetadataCalibration.Controls.Add(Me.lblCorrelationCoefficients)
        Me.grpMetadataCalibration.Location = New System.Drawing.Point(6, 195)
        Me.grpMetadataCalibration.Name = "grpMetadataCalibration"
        Me.grpMetadataCalibration.Size = New System.Drawing.Size(450, 158)
        Me.grpMetadataCalibration.TabIndex = 16
        Me.grpMetadataCalibration.TabStop = False
        Me.grpMetadataCalibration.Text = "Model Calibration (Optional)"
        '
        'lblCalibration2
        '
        Me.lblCalibration2.AutoSize = True
        Me.lblCalibration2.Location = New System.Drawing.Point(6, 61)
        Me.lblCalibration2.Name = "lblCalibration2"
        Me.lblCalibration2.Size = New System.Drawing.Size(66, 13)
        Me.lblCalibration2.TabIndex = 27
        Me.lblCalibration2.Text = "Constituent2"
        '
        'txtNashSutcliffe2
        '
        Me.txtNashSutcliffe2.Location = New System.Drawing.Point(263, 58)
        Me.txtNashSutcliffe2.Name = "txtNashSutcliffe2"
        Me.txtNashSutcliffe2.Size = New System.Drawing.Size(151, 20)
        Me.txtNashSutcliffe2.TabIndex = 12
        '
        'txtCorrelationCoefficient2
        '
        Me.txtCorrelationCoefficient2.Location = New System.Drawing.Point(135, 58)
        Me.txtCorrelationCoefficient2.Name = "txtCorrelationCoefficient2"
        Me.txtCorrelationCoefficient2.Size = New System.Drawing.Size(122, 20)
        Me.txtCorrelationCoefficient2.TabIndex = 11
        '
        'lblCalibration1
        '
        Me.lblCalibration1.AutoSize = True
        Me.lblCalibration1.Location = New System.Drawing.Point(6, 35)
        Me.lblCalibration1.Name = "lblCalibration1"
        Me.lblCalibration1.Size = New System.Drawing.Size(66, 13)
        Me.lblCalibration1.TabIndex = 24
        Me.lblCalibration1.Text = "Constituent1"
        '
        'txtNashSutcliffe1
        '
        Me.txtNashSutcliffe1.Location = New System.Drawing.Point(263, 32)
        Me.txtNashSutcliffe1.Name = "txtNashSutcliffe1"
        Me.txtNashSutcliffe1.Size = New System.Drawing.Size(151, 20)
        Me.txtNashSutcliffe1.TabIndex = 10
        '
        'lblNashSutcliffe
        '
        Me.lblNashSutcliffe.AutoSize = True
        Me.lblNashSutcliffe.Location = New System.Drawing.Point(272, 16)
        Me.lblNashSutcliffe.Name = "lblNashSutcliffe"
        Me.lblNashSutcliffe.Size = New System.Drawing.Size(102, 13)
        Me.lblNashSutcliffe.TabIndex = 22
        Me.lblNashSutcliffe.Text = "Nash-Sutcliffe value"
        '
        'txtCorrelationCoefficient1
        '
        Me.txtCorrelationCoefficient1.Location = New System.Drawing.Point(135, 32)
        Me.txtCorrelationCoefficient1.Name = "txtCorrelationCoefficient1"
        Me.txtCorrelationCoefficient1.Size = New System.Drawing.Size(122, 20)
        Me.txtCorrelationCoefficient1.TabIndex = 9
        '
        'lblCorrelationCoefficients
        '
        Me.lblCorrelationCoefficients.AutoSize = True
        Me.lblCorrelationCoefficients.Location = New System.Drawing.Point(141, 16)
        Me.lblCorrelationCoefficients.Name = "lblCorrelationCoefficients"
        Me.lblCorrelationCoefficients.Size = New System.Drawing.Size(110, 13)
        Me.lblCorrelationCoefficients.TabIndex = 20
        Me.lblCorrelationCoefficients.Text = "Correlation Coefficient"
        '
        'txtOrganization
        '
        Me.txtOrganization.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrganization.Location = New System.Drawing.Point(84, 110)
        Me.txtOrganization.Name = "txtOrganization"
        Me.txtOrganization.Size = New System.Drawing.Size(372, 20)
        Me.txtOrganization.TabIndex = 6
        '
        'lblOrganization
        '
        Me.lblOrganization.AutoSize = True
        Me.lblOrganization.Location = New System.Drawing.Point(6, 113)
        Me.lblOrganization.Name = "lblOrganization"
        Me.lblOrganization.Size = New System.Drawing.Size(66, 13)
        Me.lblOrganization.TabIndex = 14
        Me.lblOrganization.Text = "Organization"
        '
        'txtAuthorEmail
        '
        Me.txtAuthorEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAuthorEmail.Location = New System.Drawing.Point(84, 58)
        Me.txtAuthorEmail.Name = "txtAuthorEmail"
        Me.txtAuthorEmail.Size = New System.Drawing.Size(372, 20)
        Me.txtAuthorEmail.TabIndex = 4
        '
        'lblAuthorEmail
        '
        Me.lblAuthorEmail.AutoSize = True
        Me.lblAuthorEmail.Location = New System.Drawing.Point(6, 61)
        Me.lblAuthorEmail.Name = "lblAuthorEmail"
        Me.lblAuthorEmail.Size = New System.Drawing.Size(66, 13)
        Me.lblAuthorEmail.TabIndex = 12
        Me.lblAuthorEmail.Text = "Author Email"
        '
        'lblAuthor2
        '
        Me.lblAuthor2.AutoSize = True
        Me.lblAuthor2.Location = New System.Drawing.Point(266, 16)
        Me.lblAuthor2.Name = "lblAuthor2"
        Me.lblAuthor2.Size = New System.Drawing.Size(25, 13)
        Me.lblAuthor2.TabIndex = 11
        Me.lblAuthor2.Text = "M.I."
        '
        'lblAuthor1
        '
        Me.lblAuthor1.AutoSize = True
        Me.lblAuthor1.Location = New System.Drawing.Point(81, 16)
        Me.lblAuthor1.Name = "lblAuthor1"
        Me.lblAuthor1.Size = New System.Drawing.Size(26, 13)
        Me.lblAuthor1.TabIndex = 10
        Me.lblAuthor1.Text = "First"
        '
        'lblAuthor3
        '
        Me.lblAuthor3.AutoSize = True
        Me.lblAuthor3.Location = New System.Drawing.Point(303, 16)
        Me.lblAuthor3.Name = "lblAuthor3"
        Me.lblAuthor3.Size = New System.Drawing.Size(27, 13)
        Me.lblAuthor3.TabIndex = 9
        Me.lblAuthor3.Text = "Last"
        '
        'txtAuthor3
        '
        Me.txtAuthor3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAuthor3.Location = New System.Drawing.Point(306, 32)
        Me.txtAuthor3.Name = "txtAuthor3"
        Me.txtAuthor3.Size = New System.Drawing.Size(150, 20)
        Me.txtAuthor3.TabIndex = 3
        '
        'txtAuthor2
        '
        Me.txtAuthor2.Location = New System.Drawing.Point(269, 32)
        Me.txtAuthor2.Name = "txtAuthor2"
        Me.txtAuthor2.Size = New System.Drawing.Size(31, 20)
        Me.txtAuthor2.TabIndex = 2
        '
        'txtAuthor1
        '
        Me.txtAuthor1.Location = New System.Drawing.Point(84, 32)
        Me.txtAuthor1.Name = "txtAuthor1"
        Me.txtAuthor1.Size = New System.Drawing.Size(179, 20)
        Me.txtAuthor1.TabIndex = 1
        '
        'lblAuthorName
        '
        Me.lblAuthorName.AutoSize = True
        Me.lblAuthorName.Location = New System.Drawing.Point(6, 35)
        Me.lblAuthorName.Name = "lblAuthorName"
        Me.lblAuthorName.Size = New System.Drawing.Size(69, 13)
        Me.lblAuthorName.TabIndex = 2
        Me.lblAuthorName.Text = "Author Name"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(6, 139)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(72, 42)
        Me.lblDescription.TabIndex = 17
        Me.lblDescription.Text = "Description of run or study"
        '
        'grpChooseFiles
        '
        Me.grpChooseFiles.Controls.Add(Me.splitChooseFiles)
        Me.grpChooseFiles.Location = New System.Drawing.Point(183, 12)
        Me.grpChooseFiles.Name = "grpChooseFiles"
        Me.grpChooseFiles.Size = New System.Drawing.Size(253, 335)
        Me.grpChooseFiles.TabIndex = 6
        Me.grpChooseFiles.TabStop = False
        Me.grpChooseFiles.Text = "Choose Input and Output Files to Publish"
        '
        'splitChooseFiles
        '
        Me.splitChooseFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitChooseFiles.Location = New System.Drawing.Point(3, 16)
        Me.splitChooseFiles.Name = "splitChooseFiles"
        Me.splitChooseFiles.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitChooseFiles.Panel1
        '
        Me.splitChooseFiles.Panel1.Controls.Add(Me.grpChooseInputFiles)
        '
        'splitChooseFiles.Panel2
        '
        Me.splitChooseFiles.Panel2.Controls.Add(Me.grpChooseOutputFiles)
        Me.splitChooseFiles.Size = New System.Drawing.Size(247, 316)
        Me.splitChooseFiles.SplitterDistance = 158
        Me.splitChooseFiles.TabIndex = 4
        '
        'grpChooseOutputFiles
        '
        Me.grpChooseOutputFiles.Controls.Add(Me.btnAddOutputFiles)
        Me.grpChooseOutputFiles.Controls.Add(Me.lstOutputFiles)
        Me.grpChooseOutputFiles.Controls.Add(Me.btnChooseFilesNext)
        Me.grpChooseOutputFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpChooseOutputFiles.Location = New System.Drawing.Point(0, 0)
        Me.grpChooseOutputFiles.Name = "grpChooseOutputFiles"
        Me.grpChooseOutputFiles.Size = New System.Drawing.Size(247, 154)
        Me.grpChooseOutputFiles.TabIndex = 3
        Me.grpChooseOutputFiles.TabStop = False
        Me.grpChooseOutputFiles.Text = "Output Files to Publish Data From"
        '
        'btnAddOutputFiles
        '
        Me.btnAddOutputFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddOutputFiles.AutoSize = True
        Me.btnAddOutputFiles.Location = New System.Drawing.Point(6, 125)
        Me.btnAddOutputFiles.Name = "btnAddOutputFiles"
        Me.btnAddOutputFiles.Size = New System.Drawing.Size(112, 23)
        Me.btnAddOutputFiles.TabIndex = 2
        Me.btnAddOutputFiles.Text = "Add Output Files..."
        Me.btnAddOutputFiles.UseVisualStyleBackColor = True
        '
        'lstOutputFiles
        '
        Me.lstOutputFiles.AllowDrop = True
        Me.lstOutputFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstOutputFiles.CheckOnClick = True
        Me.lstOutputFiles.FormattingEnabled = True
        Me.lstOutputFiles.IntegralHeight = False
        Me.lstOutputFiles.Location = New System.Drawing.Point(6, 19)
        Me.lstOutputFiles.Name = "lstOutputFiles"
        Me.lstOutputFiles.Size = New System.Drawing.Size(235, 100)
        Me.lstOutputFiles.TabIndex = 0
        '
        'btnChooseFilesNext
        '
        Me.btnChooseFilesNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooseFilesNext.Location = New System.Drawing.Point(166, 125)
        Me.btnChooseFilesNext.Name = "btnChooseFilesNext"
        Me.btnChooseFilesNext.Size = New System.Drawing.Size(75, 23)
        Me.btnChooseFilesNext.TabIndex = 3
        Me.btnChooseFilesNext.Text = "Next"
        Me.btnChooseFilesNext.UseVisualStyleBackColor = True
        '
        'frmPublishMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 412)
        Me.Controls.Add(Me.grpChooseModel)
        Me.Controls.Add(Me.grpProgress)
        Me.Controls.Add(Me.grpChooseFiles)
        Me.Controls.Add(Me.grpMapLocations)
        Me.Controls.Add(Me.grpMetadata)
        Me.Name = "frmPublishMain"
        Me.Text = "WEDO Publishing Utility"
        Me.grpChooseModel.ResumeLayout(False)
        Me.grpChooseInputFiles.ResumeLayout(False)
        Me.grpChooseInputFiles.PerformLayout()
        Me.grpProgress.ResumeLayout(False)
        Me.grpProgress.PerformLayout()
        Me.grpMapLocations.ResumeLayout(False)
        Me.grpMapLocations.PerformLayout()
        Me.grpMetadata.ResumeLayout(False)
        Me.grpMetadata.PerformLayout()
        Me.grpMetadataCalibration.ResumeLayout(False)
        Me.grpMetadataCalibration.PerformLayout()
        Me.grpChooseFiles.ResumeLayout(False)
        Me.splitChooseFiles.Panel1.ResumeLayout(False)
        Me.splitChooseFiles.Panel2.ResumeLayout(False)
        CType(Me.splitChooseFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitChooseFiles.ResumeLayout(False)
        Me.grpChooseOutputFiles.ResumeLayout(False)
        Me.grpChooseOutputFiles.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpChooseModel As System.Windows.Forms.GroupBox
    Friend WithEvents btnSWAT As System.Windows.Forms.Button
    Friend WithEvents btnHSPF As System.Windows.Forms.Button
    Friend WithEvents grpChooseInputFiles As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddInputFiles As System.Windows.Forms.Button
    Friend WithEvents lstInputFiles As System.Windows.Forms.CheckedListBox
    Friend WithEvents grpProgress As System.Windows.Forms.GroupBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents barProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents grpMapLocations As System.Windows.Forms.GroupBox
    Friend WithEvents txtLocationID2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationID1 As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation2 As System.Windows.Forms.Label
    Friend WithEvents lblLocation1 As System.Windows.Forms.Label
    Friend WithEvents lblLocationID As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grpMetadata As System.Windows.Forms.GroupBox
    Friend WithEvents btnNHDPlusNext As System.Windows.Forms.Button
    Friend WithEvents btnNHDLookup As System.Windows.Forms.Button
    Friend WithEvents grpChooseFiles As System.Windows.Forms.GroupBox
    Friend WithEvents splitChooseFiles As System.Windows.Forms.SplitContainer
    Friend WithEvents grpChooseOutputFiles As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddOutputFiles As System.Windows.Forms.Button
    Friend WithEvents lstOutputFiles As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnChooseFilesNext As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblLocationPublish As System.Windows.Forms.Label
    Friend WithEvents chkLocation2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLocation1 As System.Windows.Forms.CheckBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents grpMetadataCalibration As GroupBox
    Friend WithEvents txtNashSutcliffe1 As TextBox
    Friend WithEvents lblNashSutcliffe As Label
    Friend WithEvents txtCorrelationCoefficient1 As TextBox
    Friend WithEvents lblCorrelationCoefficients As Label
    Friend WithEvents txtOrganization As TextBox
    Friend WithEvents lblOrganization As Label
    Friend WithEvents txtAuthorEmail As TextBox
    Friend WithEvents lblAuthorEmail As Label
    Friend WithEvents lblAuthor2 As Label
    Friend WithEvents lblAuthor1 As Label
    Friend WithEvents lblAuthor3 As Label
    Friend WithEvents txtAuthor3 As TextBox
    Friend WithEvents txtAuthor2 As TextBox
    Friend WithEvents txtAuthor1 As TextBox
    Friend WithEvents lblAuthorName As Label
    Friend WithEvents lblDescription As Label
    Friend WithEvents txtAuthorPhone As TextBox
    Friend WithEvents lblAuthorPhone As Label
    Friend WithEvents lblCalibration1 As Label
    Friend WithEvents lblCalibration2 As Label
    Friend WithEvents txtNashSutcliffe2 As TextBox
    Friend WithEvents txtCorrelationCoefficient2 As TextBox
End Class
