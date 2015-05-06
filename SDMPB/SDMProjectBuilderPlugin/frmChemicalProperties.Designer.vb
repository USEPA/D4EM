<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChemical
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblName = New System.Windows.Forms.Label()
        Me.atxName = New atcControls.atcText()
        Me.lblMaxSolubility = New System.Windows.Forms.Label()
        Me.atxMaxSolubility = New atcControls.atcText()
        Me.lblPartition = New System.Windows.Forms.Label()
        Me.lblDegradation = New System.Windows.Forms.Label()
        Me.lblSurface = New System.Windows.Forms.Label()
        Me.lblUpper = New System.Windows.Forms.Label()
        Me.lblLower = New System.Windows.Forms.Label()
        Me.lblActiveGW = New System.Windows.Forms.Label()
        Me.atxSurfacePC = New atcControls.atcText()
        Me.atxSurfaceDR = New atcControls.atcText()
        Me.atxUpperDR = New atcControls.atcText()
        Me.atxUpperPC = New atcControls.atcText()
        Me.atxLowerDR = New atcControls.atcText()
        Me.atxLowerPC = New atcControls.atcText()
        Me.atxActiveDR = New atcControls.atcText()
        Me.atxActivePC = New atcControls.atcText()
        Me.atxActiveEx = New atcControls.atcText()
        Me.atxLowerEx = New atcControls.atcText()
        Me.atxUpperEx = New atcControls.atcText()
        Me.atxSurfaceEx = New atcControls.atcText()
        Me.lblExponent = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.AutoSize = True
        Me.btnClose.Location = New System.Drawing.Point(475, 180)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 15)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(84, 13)
        Me.lblName.TabIndex = 26
        Me.lblName.Text = "Chemical Name:"
        '
        'atxName
        '
        Me.atxName.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxName.DataType = atcControls.atcText.ATCoDataType.ATCoTxt
        Me.atxName.DefaultValue = ""
        Me.atxName.HardMax = -999.0R
        Me.atxName.HardMin = -999.0R
        Me.atxName.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxName.Location = New System.Drawing.Point(102, 12)
        Me.atxName.MaxWidth = 20
        Me.atxName.Name = "atxName"
        Me.atxName.NumericFormat = "0.#####"
        Me.atxName.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxName.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxName.SelLength = 0
        Me.atxName.SelStart = 0
        Me.atxName.Size = New System.Drawing.Size(81, 22)
        Me.atxName.SoftMax = -999.0R
        Me.atxName.SoftMin = -999.0R
        Me.atxName.TabIndex = 25
        Me.atxName.ValueDouble = 0.0R
        Me.atxName.ValueInteger = 0
        '
        'lblMaxSolubility
        '
        Me.lblMaxSolubility.AutoSize = True
        Me.lblMaxSolubility.Location = New System.Drawing.Point(12, 44)
        Me.lblMaxSolubility.Name = "lblMaxSolubility"
        Me.lblMaxSolubility.Size = New System.Drawing.Size(128, 13)
        Me.lblMaxSolubility.TabIndex = 28
        Me.lblMaxSolubility.Text = "Maximum Solubility (mg/l):"
        '
        'atxMaxSolubility
        '
        Me.atxMaxSolubility.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxMaxSolubility.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxMaxSolubility.DefaultValue = ""
        Me.atxMaxSolubility.HardMax = -999.0R
        Me.atxMaxSolubility.HardMin = -999.0R
        Me.atxMaxSolubility.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxMaxSolubility.Location = New System.Drawing.Point(145, 40)
        Me.atxMaxSolubility.MaxWidth = 20
        Me.atxMaxSolubility.Name = "atxMaxSolubility"
        Me.atxMaxSolubility.NumericFormat = "0.#####"
        Me.atxMaxSolubility.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxMaxSolubility.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxMaxSolubility.SelLength = 0
        Me.atxMaxSolubility.SelStart = 0
        Me.atxMaxSolubility.Size = New System.Drawing.Size(81, 22)
        Me.atxMaxSolubility.SoftMax = -999.0R
        Me.atxMaxSolubility.SoftMin = -999.0R
        Me.atxMaxSolubility.TabIndex = 27
        Me.atxMaxSolubility.ValueDouble = 0.0R
        Me.atxMaxSolubility.ValueInteger = 0
        '
        'lblPartition
        '
        Me.lblPartition.AutoSize = True
        Me.lblPartition.Location = New System.Drawing.Point(12, 94)
        Me.lblPartition.Name = "lblPartition"
        Me.lblPartition.Size = New System.Drawing.Size(156, 13)
        Me.lblPartition.TabIndex = 29
        Me.lblPartition.Text = "Freundlich Coefficient K1 (l/kg):"
        '
        'lblDegradation
        '
        Me.lblDegradation.AutoSize = True
        Me.lblDegradation.Location = New System.Drawing.Point(12, 150)
        Me.lblDegradation.Name = "lblDegradation"
        Me.lblDegradation.Size = New System.Drawing.Size(125, 13)
        Me.lblDegradation.TabIndex = 30
        Me.lblDegradation.Text = "Degradation Rate (/day):"
        '
        'lblSurface
        '
        Me.lblSurface.AutoSize = True
        Me.lblSurface.Location = New System.Drawing.Point(174, 74)
        Me.lblSurface.Name = "lblSurface"
        Me.lblSurface.Size = New System.Drawing.Size(44, 13)
        Me.lblSurface.TabIndex = 31
        Me.lblSurface.Text = "Surface"
        '
        'lblUpper
        '
        Me.lblUpper.AutoSize = True
        Me.lblUpper.Location = New System.Drawing.Point(261, 74)
        Me.lblUpper.Name = "lblUpper"
        Me.lblUpper.Size = New System.Drawing.Size(36, 13)
        Me.lblUpper.TabIndex = 32
        Me.lblUpper.Text = "Upper"
        '
        'lblLower
        '
        Me.lblLower.AutoSize = True
        Me.lblLower.Location = New System.Drawing.Point(348, 74)
        Me.lblLower.Name = "lblLower"
        Me.lblLower.Size = New System.Drawing.Size(36, 13)
        Me.lblLower.TabIndex = 33
        Me.lblLower.Text = "Lower"
        '
        'lblActiveGW
        '
        Me.lblActiveGW.AutoSize = True
        Me.lblActiveGW.Location = New System.Drawing.Point(435, 74)
        Me.lblActiveGW.Name = "lblActiveGW"
        Me.lblActiveGW.Size = New System.Drawing.Size(101, 13)
        Me.lblActiveGW.TabIndex = 34
        Me.lblActiveGW.Text = "Active Groundwater"
        '
        'atxSurfacePC
        '
        Me.atxSurfacePC.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxSurfacePC.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxSurfacePC.DefaultValue = ""
        Me.atxSurfacePC.HardMax = -999.0R
        Me.atxSurfacePC.HardMin = 0.0R
        Me.atxSurfacePC.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxSurfacePC.Location = New System.Drawing.Point(177, 90)
        Me.atxSurfacePC.MaxWidth = 20
        Me.atxSurfacePC.Name = "atxSurfacePC"
        Me.atxSurfacePC.NumericFormat = "0.#####"
        Me.atxSurfacePC.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxSurfacePC.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxSurfacePC.SelLength = 0
        Me.atxSurfacePC.SelStart = 0
        Me.atxSurfacePC.Size = New System.Drawing.Size(81, 22)
        Me.atxSurfacePC.SoftMax = -999.0R
        Me.atxSurfacePC.SoftMin = -999.0R
        Me.atxSurfacePC.TabIndex = 35
        Me.atxSurfacePC.ValueDouble = 0.0R
        Me.atxSurfacePC.ValueInteger = 0
        '
        'atxSurfaceDR
        '
        Me.atxSurfaceDR.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxSurfaceDR.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxSurfaceDR.DefaultValue = ""
        Me.atxSurfaceDR.HardMax = 1.0R
        Me.atxSurfaceDR.HardMin = 0.0R
        Me.atxSurfaceDR.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxSurfaceDR.Location = New System.Drawing.Point(177, 146)
        Me.atxSurfaceDR.MaxWidth = 20
        Me.atxSurfaceDR.Name = "atxSurfaceDR"
        Me.atxSurfaceDR.NumericFormat = "0.#####"
        Me.atxSurfaceDR.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxSurfaceDR.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxSurfaceDR.SelLength = 0
        Me.atxSurfaceDR.SelStart = 0
        Me.atxSurfaceDR.Size = New System.Drawing.Size(81, 22)
        Me.atxSurfaceDR.SoftMax = -999.0R
        Me.atxSurfaceDR.SoftMin = -999.0R
        Me.atxSurfaceDR.TabIndex = 36
        Me.atxSurfaceDR.ValueDouble = 0.0R
        Me.atxSurfaceDR.ValueInteger = 0
        '
        'atxUpperDR
        '
        Me.atxUpperDR.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxUpperDR.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxUpperDR.DefaultValue = ""
        Me.atxUpperDR.HardMax = 1.0R
        Me.atxUpperDR.HardMin = 0.0R
        Me.atxUpperDR.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxUpperDR.Location = New System.Drawing.Point(264, 146)
        Me.atxUpperDR.MaxWidth = 20
        Me.atxUpperDR.Name = "atxUpperDR"
        Me.atxUpperDR.NumericFormat = "0.#####"
        Me.atxUpperDR.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxUpperDR.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxUpperDR.SelLength = 0
        Me.atxUpperDR.SelStart = 0
        Me.atxUpperDR.Size = New System.Drawing.Size(81, 22)
        Me.atxUpperDR.SoftMax = -999.0R
        Me.atxUpperDR.SoftMin = -999.0R
        Me.atxUpperDR.TabIndex = 38
        Me.atxUpperDR.ValueDouble = 0.0R
        Me.atxUpperDR.ValueInteger = 0
        '
        'atxUpperPC
        '
        Me.atxUpperPC.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxUpperPC.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxUpperPC.DefaultValue = ""
        Me.atxUpperPC.HardMax = -999.0R
        Me.atxUpperPC.HardMin = 0.0R
        Me.atxUpperPC.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxUpperPC.Location = New System.Drawing.Point(264, 90)
        Me.atxUpperPC.MaxWidth = 20
        Me.atxUpperPC.Name = "atxUpperPC"
        Me.atxUpperPC.NumericFormat = "0.#####"
        Me.atxUpperPC.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxUpperPC.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxUpperPC.SelLength = 0
        Me.atxUpperPC.SelStart = 0
        Me.atxUpperPC.Size = New System.Drawing.Size(81, 22)
        Me.atxUpperPC.SoftMax = -999.0R
        Me.atxUpperPC.SoftMin = -999.0R
        Me.atxUpperPC.TabIndex = 37
        Me.atxUpperPC.ValueDouble = 0.0R
        Me.atxUpperPC.ValueInteger = 0
        '
        'atxLowerDR
        '
        Me.atxLowerDR.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLowerDR.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLowerDR.DefaultValue = ""
        Me.atxLowerDR.HardMax = 1.0R
        Me.atxLowerDR.HardMin = 0.0R
        Me.atxLowerDR.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLowerDR.Location = New System.Drawing.Point(351, 146)
        Me.atxLowerDR.MaxWidth = 20
        Me.atxLowerDR.Name = "atxLowerDR"
        Me.atxLowerDR.NumericFormat = "0.#####"
        Me.atxLowerDR.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxLowerDR.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxLowerDR.SelLength = 0
        Me.atxLowerDR.SelStart = 0
        Me.atxLowerDR.Size = New System.Drawing.Size(81, 22)
        Me.atxLowerDR.SoftMax = -999.0R
        Me.atxLowerDR.SoftMin = -999.0R
        Me.atxLowerDR.TabIndex = 40
        Me.atxLowerDR.ValueDouble = 0.0R
        Me.atxLowerDR.ValueInteger = 0
        '
        'atxLowerPC
        '
        Me.atxLowerPC.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLowerPC.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLowerPC.DefaultValue = ""
        Me.atxLowerPC.HardMax = -999.0R
        Me.atxLowerPC.HardMin = 0.0R
        Me.atxLowerPC.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLowerPC.Location = New System.Drawing.Point(351, 90)
        Me.atxLowerPC.MaxWidth = 20
        Me.atxLowerPC.Name = "atxLowerPC"
        Me.atxLowerPC.NumericFormat = "0.#####"
        Me.atxLowerPC.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxLowerPC.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxLowerPC.SelLength = 0
        Me.atxLowerPC.SelStart = 0
        Me.atxLowerPC.Size = New System.Drawing.Size(81, 22)
        Me.atxLowerPC.SoftMax = -999.0R
        Me.atxLowerPC.SoftMin = -999.0R
        Me.atxLowerPC.TabIndex = 39
        Me.atxLowerPC.ValueDouble = 0.0R
        Me.atxLowerPC.ValueInteger = 0
        '
        'atxActiveDR
        '
        Me.atxActiveDR.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxActiveDR.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxActiveDR.DefaultValue = ""
        Me.atxActiveDR.HardMax = 1.0R
        Me.atxActiveDR.HardMin = 0.0R
        Me.atxActiveDR.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxActiveDR.Location = New System.Drawing.Point(438, 146)
        Me.atxActiveDR.MaxWidth = 20
        Me.atxActiveDR.Name = "atxActiveDR"
        Me.atxActiveDR.NumericFormat = "0.#####"
        Me.atxActiveDR.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxActiveDR.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxActiveDR.SelLength = 0
        Me.atxActiveDR.SelStart = 0
        Me.atxActiveDR.Size = New System.Drawing.Size(81, 22)
        Me.atxActiveDR.SoftMax = -999.0R
        Me.atxActiveDR.SoftMin = -999.0R
        Me.atxActiveDR.TabIndex = 42
        Me.atxActiveDR.ValueDouble = 0.0R
        Me.atxActiveDR.ValueInteger = 0
        '
        'atxActivePC
        '
        Me.atxActivePC.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxActivePC.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxActivePC.DefaultValue = ""
        Me.atxActivePC.HardMax = -999.0R
        Me.atxActivePC.HardMin = 0.0R
        Me.atxActivePC.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxActivePC.Location = New System.Drawing.Point(438, 90)
        Me.atxActivePC.MaxWidth = 20
        Me.atxActivePC.Name = "atxActivePC"
        Me.atxActivePC.NumericFormat = "0.#####"
        Me.atxActivePC.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxActivePC.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxActivePC.SelLength = 0
        Me.atxActivePC.SelStart = 0
        Me.atxActivePC.Size = New System.Drawing.Size(81, 22)
        Me.atxActivePC.SoftMax = -999.0R
        Me.atxActivePC.SoftMin = -999.0R
        Me.atxActivePC.TabIndex = 41
        Me.atxActivePC.ValueDouble = 0.0R
        Me.atxActivePC.ValueInteger = 0
        '
        'atxActiveEx
        '
        Me.atxActiveEx.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxActiveEx.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxActiveEx.DefaultValue = ""
        Me.atxActiveEx.HardMax = -999.0R
        Me.atxActiveEx.HardMin = 1.0R
        Me.atxActiveEx.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxActiveEx.Location = New System.Drawing.Point(438, 118)
        Me.atxActiveEx.MaxWidth = 20
        Me.atxActiveEx.Name = "atxActiveEx"
        Me.atxActiveEx.NumericFormat = "0.#####"
        Me.atxActiveEx.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxActiveEx.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxActiveEx.SelLength = 0
        Me.atxActiveEx.SelStart = 0
        Me.atxActiveEx.Size = New System.Drawing.Size(81, 22)
        Me.atxActiveEx.SoftMax = -999.0R
        Me.atxActiveEx.SoftMin = -999.0R
        Me.atxActiveEx.TabIndex = 47
        Me.atxActiveEx.ValueDouble = 1.0R
        Me.atxActiveEx.ValueInteger = 0
        '
        'atxLowerEx
        '
        Me.atxLowerEx.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxLowerEx.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxLowerEx.DefaultValue = ""
        Me.atxLowerEx.HardMax = -999.0R
        Me.atxLowerEx.HardMin = 1.0R
        Me.atxLowerEx.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxLowerEx.Location = New System.Drawing.Point(351, 118)
        Me.atxLowerEx.MaxWidth = 20
        Me.atxLowerEx.Name = "atxLowerEx"
        Me.atxLowerEx.NumericFormat = "0.#####"
        Me.atxLowerEx.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxLowerEx.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxLowerEx.SelLength = 0
        Me.atxLowerEx.SelStart = 0
        Me.atxLowerEx.Size = New System.Drawing.Size(81, 22)
        Me.atxLowerEx.SoftMax = -999.0R
        Me.atxLowerEx.SoftMin = -999.0R
        Me.atxLowerEx.TabIndex = 46
        Me.atxLowerEx.ValueDouble = 1.0R
        Me.atxLowerEx.ValueInteger = 0
        '
        'atxUpperEx
        '
        Me.atxUpperEx.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxUpperEx.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxUpperEx.DefaultValue = ""
        Me.atxUpperEx.HardMax = -999.0R
        Me.atxUpperEx.HardMin = 1.0R
        Me.atxUpperEx.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxUpperEx.Location = New System.Drawing.Point(264, 118)
        Me.atxUpperEx.MaxWidth = 20
        Me.atxUpperEx.Name = "atxUpperEx"
        Me.atxUpperEx.NumericFormat = "0.#####"
        Me.atxUpperEx.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxUpperEx.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxUpperEx.SelLength = 0
        Me.atxUpperEx.SelStart = 0
        Me.atxUpperEx.Size = New System.Drawing.Size(81, 22)
        Me.atxUpperEx.SoftMax = -999.0R
        Me.atxUpperEx.SoftMin = -999.0R
        Me.atxUpperEx.TabIndex = 45
        Me.atxUpperEx.ValueDouble = 1.0R
        Me.atxUpperEx.ValueInteger = 0
        '
        'atxSurfaceEx
        '
        Me.atxSurfaceEx.Alignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.atxSurfaceEx.DataType = atcControls.atcText.ATCoDataType.ATCoDbl
        Me.atxSurfaceEx.DefaultValue = ""
        Me.atxSurfaceEx.HardMax = -999.0R
        Me.atxSurfaceEx.HardMin = 1.0R
        Me.atxSurfaceEx.InsideLimitsBackground = System.Drawing.Color.White
        Me.atxSurfaceEx.Location = New System.Drawing.Point(177, 118)
        Me.atxSurfaceEx.MaxWidth = 20
        Me.atxSurfaceEx.Name = "atxSurfaceEx"
        Me.atxSurfaceEx.NumericFormat = "0.#####"
        Me.atxSurfaceEx.OutsideHardLimitBackground = System.Drawing.Color.Coral
        Me.atxSurfaceEx.OutsideSoftLimitBackground = System.Drawing.Color.Yellow
        Me.atxSurfaceEx.SelLength = 0
        Me.atxSurfaceEx.SelStart = 0
        Me.atxSurfaceEx.Size = New System.Drawing.Size(81, 22)
        Me.atxSurfaceEx.SoftMax = -999.0R
        Me.atxSurfaceEx.SoftMin = -999.0R
        Me.atxSurfaceEx.TabIndex = 44
        Me.atxSurfaceEx.ValueDouble = 1.0R
        Me.atxSurfaceEx.ValueInteger = 0
        '
        'lblExponent
        '
        Me.lblExponent.AutoSize = True
        Me.lblExponent.Location = New System.Drawing.Point(12, 122)
        Me.lblExponent.Name = "lblExponent"
        Me.lblExponent.Size = New System.Drawing.Size(124, 13)
        Me.lblExponent.TabIndex = 43
        Me.lblExponent.Text = "Freundlich Exponent N1:"
        '
        'frmChemical
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 214)
        Me.Controls.Add(Me.atxActiveEx)
        Me.Controls.Add(Me.atxLowerEx)
        Me.Controls.Add(Me.atxUpperEx)
        Me.Controls.Add(Me.atxSurfaceEx)
        Me.Controls.Add(Me.lblExponent)
        Me.Controls.Add(Me.atxActiveDR)
        Me.Controls.Add(Me.atxActivePC)
        Me.Controls.Add(Me.atxLowerDR)
        Me.Controls.Add(Me.atxLowerPC)
        Me.Controls.Add(Me.atxUpperDR)
        Me.Controls.Add(Me.atxUpperPC)
        Me.Controls.Add(Me.atxSurfaceDR)
        Me.Controls.Add(Me.atxSurfacePC)
        Me.Controls.Add(Me.lblActiveGW)
        Me.Controls.Add(Me.lblLower)
        Me.Controls.Add(Me.lblUpper)
        Me.Controls.Add(Me.lblSurface)
        Me.Controls.Add(Me.lblDegradation)
        Me.Controls.Add(Me.lblPartition)
        Me.Controls.Add(Me.lblMaxSolubility)
        Me.Controls.Add(Me.atxMaxSolubility)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.atxName)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmChemical"
        Me.Text = "Chemical Properties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents atxName As atcControls.atcText
    Friend WithEvents lblMaxSolubility As System.Windows.Forms.Label
    Friend WithEvents atxMaxSolubility As atcControls.atcText
    Friend WithEvents lblPartition As System.Windows.Forms.Label
    Friend WithEvents lblDegradation As System.Windows.Forms.Label
    Friend WithEvents lblSurface As System.Windows.Forms.Label
    Friend WithEvents lblUpper As System.Windows.Forms.Label
    Friend WithEvents lblLower As System.Windows.Forms.Label
    Friend WithEvents lblActiveGW As System.Windows.Forms.Label
    Friend WithEvents atxSurfacePC As atcControls.atcText
    Friend WithEvents atxSurfaceDR As atcControls.atcText
    Friend WithEvents atxUpperDR As atcControls.atcText
    Friend WithEvents atxUpperPC As atcControls.atcText
    Friend WithEvents atxLowerDR As atcControls.atcText
    Friend WithEvents atxLowerPC As atcControls.atcText
    Friend WithEvents atxActiveDR As atcControls.atcText
    Friend WithEvents atxActivePC As atcControls.atcText
    Friend WithEvents atxActiveEx As atcControls.atcText
    Friend WithEvents atxLowerEx As atcControls.atcText
    Friend WithEvents atxUpperEx As atcControls.atcText
    Friend WithEvents atxSurfaceEx As atcControls.atcText
    Friend WithEvents lblExponent As System.Windows.Forms.Label
End Class
