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
        Me.radioOutputs = New System.Windows.Forms.RadioButton()
        Me.radioInputs = New System.Windows.Forms.RadioButton()
        Me.btnSWAT = New System.Windows.Forms.Button()
        Me.btnHSPF = New System.Windows.Forms.Button()
        Me.grpChooseFiles = New System.Windows.Forms.GroupBox()
        Me.btnAddFiles = New System.Windows.Forms.Button()
        Me.lstFiles = New System.Windows.Forms.CheckedListBox()
        Me.btnChooseFilesNext = New System.Windows.Forms.Button()
        Me.btnSaveFiles = New System.Windows.Forms.Button()
        Me.grpProgress = New System.Windows.Forms.GroupBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.barProgress = New System.Windows.Forms.ProgressBar()
        Me.grpMapLocations = New System.Windows.Forms.GroupBox()
        Me.txtLocationID2 = New System.Windows.Forms.TextBox()
        Me.txtLocationID1 = New System.Windows.Forms.TextBox()
        Me.lblLocation2 = New System.Windows.Forms.Label()
        Me.lblLocation1 = New System.Windows.Forms.Label()
        Me.lblLocationID = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.btnSaveData = New System.Windows.Forms.Button()
        Me.grpMetadata = New System.Windows.Forms.GroupBox()
        Me.txtMetadata = New System.Windows.Forms.RichTextBox()
        Me.btnNHDPlusNext = New System.Windows.Forms.Button()
        Me.grpChooseModel.SuspendLayout()
        Me.grpChooseFiles.SuspendLayout()
        Me.grpProgress.SuspendLayout()
        Me.grpMapLocations.SuspendLayout()
        Me.grpMetadata.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpChooseModel
        '
        Me.grpChooseModel.Controls.Add(Me.radioOutputs)
        Me.grpChooseModel.Controls.Add(Me.radioInputs)
        Me.grpChooseModel.Controls.Add(Me.btnSWAT)
        Me.grpChooseModel.Controls.Add(Me.btnHSPF)
        Me.grpChooseModel.Location = New System.Drawing.Point(12, 12)
        Me.grpChooseModel.Name = "grpChooseModel"
        Me.grpChooseModel.Size = New System.Drawing.Size(165, 335)
        Me.grpChooseModel.TabIndex = 0
        Me.grpChooseModel.TabStop = False
        Me.grpChooseModel.Text = "Choose Model to Publish"
        '
        'radioOutputs
        '
        Me.radioOutputs.AutoSize = True
        Me.radioOutputs.Location = New System.Drawing.Point(24, 67)
        Me.radioOutputs.Name = "radioOutputs"
        Me.radioOutputs.Size = New System.Drawing.Size(134, 17)
        Me.radioOutputs.TabIndex = 5
        Me.radioOutputs.Text = "Model Output Datasets"
        Me.radioOutputs.UseVisualStyleBackColor = True
        '
        'radioInputs
        '
        Me.radioInputs.AutoSize = True
        Me.radioInputs.Checked = True
        Me.radioInputs.Location = New System.Drawing.Point(24, 37)
        Me.radioInputs.Name = "radioInputs"
        Me.radioInputs.Size = New System.Drawing.Size(105, 17)
        Me.radioInputs.TabIndex = 4
        Me.radioInputs.TabStop = True
        Me.radioInputs.Text = "Model Input Files"
        Me.radioInputs.UseVisualStyleBackColor = True
        '
        'btnSWAT
        '
        Me.btnSWAT.Location = New System.Drawing.Point(24, 193)
        Me.btnSWAT.Name = "btnSWAT"
        Me.btnSWAT.Size = New System.Drawing.Size(116, 23)
        Me.btnSWAT.TabIndex = 2
        Me.btnSWAT.Text = "SWAT"
        Me.btnSWAT.UseVisualStyleBackColor = True
        '
        'btnHSPF
        '
        Me.btnHSPF.Location = New System.Drawing.Point(24, 142)
        Me.btnHSPF.Name = "btnHSPF"
        Me.btnHSPF.Size = New System.Drawing.Size(116, 23)
        Me.btnHSPF.TabIndex = 0
        Me.btnHSPF.Text = "HSPF"
        Me.btnHSPF.UseVisualStyleBackColor = True
        '
        'grpChooseFiles
        '
        Me.grpChooseFiles.Controls.Add(Me.btnAddFiles)
        Me.grpChooseFiles.Controls.Add(Me.lstFiles)
        Me.grpChooseFiles.Controls.Add(Me.btnChooseFilesNext)
        Me.grpChooseFiles.Location = New System.Drawing.Point(199, 12)
        Me.grpChooseFiles.Name = "grpChooseFiles"
        Me.grpChooseFiles.Size = New System.Drawing.Size(237, 335)
        Me.grpChooseFiles.TabIndex = 2
        Me.grpChooseFiles.TabStop = False
        Me.grpChooseFiles.Text = "Choose Files to Publish"
        '
        'btnAddFiles
        '
        Me.btnAddFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddFiles.AutoSize = True
        Me.btnAddFiles.Location = New System.Drawing.Point(6, 306)
        Me.btnAddFiles.Name = "btnAddFiles"
        Me.btnAddFiles.Size = New System.Drawing.Size(112, 23)
        Me.btnAddFiles.TabIndex = 2
        Me.btnAddFiles.Text = "Add More Files..."
        Me.btnAddFiles.UseVisualStyleBackColor = True
        '
        'lstFiles
        '
        Me.lstFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstFiles.CheckOnClick = True
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.IntegralHeight = False
        Me.lstFiles.Location = New System.Drawing.Point(6, 19)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.Size = New System.Drawing.Size(225, 281)
        Me.lstFiles.TabIndex = 0
        '
        'btnChooseFilesNext
        '
        Me.btnChooseFilesNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooseFilesNext.Location = New System.Drawing.Point(156, 306)
        Me.btnChooseFilesNext.Name = "btnChooseFilesNext"
        Me.btnChooseFilesNext.Size = New System.Drawing.Size(75, 23)
        Me.btnChooseFilesNext.TabIndex = 3
        Me.btnChooseFilesNext.Text = "Next"
        Me.btnChooseFilesNext.UseVisualStyleBackColor = True
        '
        'btnSaveFiles
        '
        Me.btnSaveFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveFiles.Location = New System.Drawing.Point(232, 306)
        Me.btnSaveFiles.Name = "btnSaveFiles"
        Me.btnSaveFiles.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveFiles.TabIndex = 1
        Me.btnSaveFiles.Text = "Save"
        Me.btnSaveFiles.UseVisualStyleBackColor = True
        Me.btnSaveFiles.Visible = False
        '
        'grpProgress
        '
        Me.grpProgress.Controls.Add(Me.lblProgress)
        Me.grpProgress.Controls.Add(Me.barProgress)
        Me.grpProgress.Location = New System.Drawing.Point(3, 12)
        Me.grpProgress.Name = "grpProgress"
        Me.grpProgress.Size = New System.Drawing.Size(380, 246)
        Me.grpProgress.TabIndex = 3
        Me.grpProgress.TabStop = False
        Me.grpProgress.Text = "Processing"
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
        Me.grpMapLocations.Controls.Add(Me.btnNHDPlusNext)
        Me.grpMapLocations.Controls.Add(Me.txtLocationID2)
        Me.grpMapLocations.Controls.Add(Me.txtLocationID1)
        Me.grpMapLocations.Controls.Add(Me.lblLocation2)
        Me.grpMapLocations.Controls.Add(Me.lblLocation1)
        Me.grpMapLocations.Controls.Add(Me.lblLocationID)
        Me.grpMapLocations.Controls.Add(Me.lblLocation)
        Me.grpMapLocations.Location = New System.Drawing.Point(442, 12)
        Me.grpMapLocations.Name = "grpMapLocations"
        Me.grpMapLocations.Size = New System.Drawing.Size(384, 335)
        Me.grpMapLocations.TabIndex = 4
        Me.grpMapLocations.TabStop = False
        Me.grpMapLocations.Text = "Specify NHDPlus Stream IDs"
        '
        'txtLocationID2
        '
        Me.txtLocationID2.Location = New System.Drawing.Point(264, 65)
        Me.txtLocationID2.Name = "txtLocationID2"
        Me.txtLocationID2.Size = New System.Drawing.Size(220, 20)
        Me.txtLocationID2.TabIndex = 15
        Me.txtLocationID2.Visible = False
        '
        'txtLocationID1
        '
        Me.txtLocationID1.Location = New System.Drawing.Point(264, 38)
        Me.txtLocationID1.Name = "txtLocationID1"
        Me.txtLocationID1.Size = New System.Drawing.Size(220, 20)
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
        Me.lblLocationID.Location = New System.Drawing.Point(261, 19)
        Me.lblLocationID.Name = "lblLocationID"
        Me.lblLocationID.Size = New System.Drawing.Size(118, 13)
        Me.lblLocationID.TabIndex = 9
        Me.lblLocationID.Text = "NHDPlus Stream ID"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 19)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(56, 13)
        Me.lblLocation.TabIndex = 8
        Me.lblLocation.Text = "Location"
        '
        'btnSaveData
        '
        Me.btnSaveData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveData.Location = New System.Drawing.Point(232, 306)
        Me.btnSaveData.Name = "btnSaveData"
        Me.btnSaveData.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveData.TabIndex = 1
        Me.btnSaveData.Text = "Save"
        Me.btnSaveData.UseVisualStyleBackColor = True
        '
        'grpMetadata
        '
        Me.grpMetadata.Controls.Add(Me.txtMetadata)
        Me.grpMetadata.Controls.Add(Me.btnSaveData)
        Me.grpMetadata.Controls.Add(Me.btnSaveFiles)
        Me.grpMetadata.Location = New System.Drawing.Point(648, 12)
        Me.grpMetadata.Name = "grpMetadata"
        Me.grpMetadata.Size = New System.Drawing.Size(313, 335)
        Me.grpMetadata.TabIndex = 5
        Me.grpMetadata.TabStop = False
        Me.grpMetadata.Text = "Metadata"
        '
        'txtMetadata
        '
        Me.txtMetadata.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMetadata.Location = New System.Drawing.Point(6, 19)
        Me.txtMetadata.Name = "txtMetadata"
        Me.txtMetadata.Size = New System.Drawing.Size(301, 281)
        Me.txtMetadata.TabIndex = 0
        Me.txtMetadata.Text = ""
        '
        'btnNHDPlusNext
        '
        Me.btnNHDPlusNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNHDPlusNext.Location = New System.Drawing.Point(303, 306)
        Me.btnNHDPlusNext.Name = "btnNHDPlusNext"
        Me.btnNHDPlusNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNHDPlusNext.TabIndex = 16
        Me.btnNHDPlusNext.Text = "Next"
        Me.btnNHDPlusNext.UseVisualStyleBackColor = True
        '
        'frmPublishMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 359)
        Me.Controls.Add(Me.grpMapLocations)
        Me.Controls.Add(Me.grpChooseFiles)
        Me.Controls.Add(Me.grpChooseModel)
        Me.Controls.Add(Me.grpProgress)
        Me.Controls.Add(Me.grpMetadata)
        Me.Name = "frmPublishMain"
        Me.Text = "WEDO Publish"
        Me.grpChooseModel.ResumeLayout(False)
        Me.grpChooseModel.PerformLayout()
        Me.grpChooseFiles.ResumeLayout(False)
        Me.grpChooseFiles.PerformLayout()
        Me.grpProgress.ResumeLayout(False)
        Me.grpProgress.PerformLayout()
        Me.grpMapLocations.ResumeLayout(False)
        Me.grpMapLocations.PerformLayout()
        Me.grpMetadata.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpChooseModel As System.Windows.Forms.GroupBox
    Friend WithEvents btnSWAT As System.Windows.Forms.Button
    Friend WithEvents btnHSPF As System.Windows.Forms.Button
    Friend WithEvents grpChooseFiles As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddFiles As System.Windows.Forms.Button
    Friend WithEvents btnSaveFiles As System.Windows.Forms.Button
    Friend WithEvents lstFiles As System.Windows.Forms.CheckedListBox
    Friend WithEvents grpProgress As System.Windows.Forms.GroupBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents barProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents radioOutputs As System.Windows.Forms.RadioButton
    Friend WithEvents radioInputs As System.Windows.Forms.RadioButton
    Friend WithEvents btnChooseFilesNext As System.Windows.Forms.Button
    Friend WithEvents grpMapLocations As System.Windows.Forms.GroupBox
    Friend WithEvents txtLocationID2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationID1 As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation2 As System.Windows.Forms.Label
    Friend WithEvents lblLocation1 As System.Windows.Forms.Label
    Friend WithEvents lblLocationID As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents btnSaveData As System.Windows.Forms.Button
    Friend WithEvents grpMetadata As System.Windows.Forms.GroupBox
    Friend WithEvents txtMetadata As System.Windows.Forms.RichTextBox
    Friend WithEvents btnNHDPlusNext As System.Windows.Forms.Button

End Class
