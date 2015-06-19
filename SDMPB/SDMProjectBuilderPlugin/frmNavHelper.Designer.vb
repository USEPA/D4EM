<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNavHelper
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShowAttributes = New System.Windows.Forms.Button()
        Me.cbLayers = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkBoxNHD = New System.Windows.Forms.CheckBox()
        Me.chkBoxCatchment = New System.Windows.Forms.CheckBox()
        Me.btnGetHUC12s = New System.Windows.Forms.Button()
        Me.btnHUC8Zoom = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbHUC8s = New System.Windows.Forms.ComboBox()
        Me.btnCountyZoom = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbCounties = New System.Windows.Forms.ComboBox()
        Me.btnStateZoom = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbStates = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnShowAttributes)
        Me.GroupBox1.Controls.Add(Me.cbLayers)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkBoxNHD)
        Me.GroupBox1.Controls.Add(Me.chkBoxCatchment)
        Me.GroupBox1.Controls.Add(Me.btnGetHUC12s)
        Me.GroupBox1.Controls.Add(Me.btnHUC8Zoom)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbHUC8s)
        Me.GroupBox1.Controls.Add(Me.btnCountyZoom)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbCounties)
        Me.GroupBox1.Controls.Add(Me.btnStateZoom)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbStates)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(419, 203)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Base Layers"
        '
        'btnShowAttributes
        '
        Me.btnShowAttributes.Location = New System.Drawing.Point(294, 173)
        Me.btnShowAttributes.Name = "btnShowAttributes"
        Me.btnShowAttributes.Size = New System.Drawing.Size(92, 23)
        Me.btnShowAttributes.TabIndex = 23
        Me.btnShowAttributes.Text = "Show Attributes"
        Me.btnShowAttributes.UseVisualStyleBackColor = True
        '
        'cbLayers
        '
        Me.cbLayers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbLayers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbLayers.FormattingEnabled = True
        Me.cbLayers.Location = New System.Drawing.Point(76, 173)
        Me.cbLayers.Name = "cbLayers"
        Me.cbLayers.Size = New System.Drawing.Size(198, 21)
        Me.cbLayers.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Layers:"
        '
        'chkBoxNHD
        '
        Me.chkBoxNHD.AutoSize = True
        Me.chkBoxNHD.Location = New System.Drawing.Point(90, 137)
        Me.chkBoxNHD.Name = "chkBoxNHD"
        Me.chkBoxNHD.Size = New System.Drawing.Size(56, 17)
        Me.chkBoxNHD.TabIndex = 20
        Me.chkBoxNHD.Text = "NHD+"
        Me.chkBoxNHD.UseVisualStyleBackColor = True
        '
        'chkBoxCatchment
        '
        Me.chkBoxCatchment.AutoSize = True
        Me.chkBoxCatchment.Location = New System.Drawing.Point(164, 137)
        Me.chkBoxCatchment.Name = "chkBoxCatchment"
        Me.chkBoxCatchment.Size = New System.Drawing.Size(69, 17)
        Me.chkBoxCatchment.TabIndex = 19
        Me.chkBoxCatchment.Text = "HUC 12s"
        Me.chkBoxCatchment.UseVisualStyleBackColor = True
        '
        'btnGetHUC12s
        '
        Me.btnGetHUC12s.Location = New System.Drawing.Point(294, 133)
        Me.btnGetHUC12s.Name = "btnGetHUC12s"
        Me.btnGetHUC12s.Size = New System.Drawing.Size(75, 23)
        Me.btnGetHUC12s.TabIndex = 18
        Me.btnGetHUC12s.Text = "Get Data"
        Me.btnGetHUC12s.UseVisualStyleBackColor = True
        '
        'btnHUC8Zoom
        '
        Me.btnHUC8Zoom.Location = New System.Drawing.Point(294, 107)
        Me.btnHUC8Zoom.Name = "btnHUC8Zoom"
        Me.btnHUC8Zoom.Size = New System.Drawing.Size(75, 23)
        Me.btnHUC8Zoom.TabIndex = 17
        Me.btnHUC8Zoom.Text = "Zoom"
        Me.btnHUC8Zoom.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "HUC 8:"
        '
        'cbHUC8s
        '
        Me.cbHUC8s.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbHUC8s.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbHUC8s.FormattingEnabled = True
        Me.cbHUC8s.Location = New System.Drawing.Point(76, 110)
        Me.cbHUC8s.Name = "cbHUC8s"
        Me.cbHUC8s.Size = New System.Drawing.Size(198, 21)
        Me.cbHUC8s.TabIndex = 15
        '
        'btnCountyZoom
        '
        Me.btnCountyZoom.Location = New System.Drawing.Point(294, 57)
        Me.btnCountyZoom.Name = "btnCountyZoom"
        Me.btnCountyZoom.Size = New System.Drawing.Size(75, 23)
        Me.btnCountyZoom.TabIndex = 14
        Me.btnCountyZoom.Text = "Zoom"
        Me.btnCountyZoom.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "County:"
        '
        'cbCounties
        '
        Me.cbCounties.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbCounties.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbCounties.FormattingEnabled = True
        Me.cbCounties.Location = New System.Drawing.Point(76, 59)
        Me.cbCounties.Name = "cbCounties"
        Me.cbCounties.Size = New System.Drawing.Size(198, 21)
        Me.cbCounties.TabIndex = 12
        '
        'btnStateZoom
        '
        Me.btnStateZoom.Location = New System.Drawing.Point(294, 30)
        Me.btnStateZoom.Name = "btnStateZoom"
        Me.btnStateZoom.Size = New System.Drawing.Size(75, 23)
        Me.btnStateZoom.TabIndex = 11
        Me.btnStateZoom.Text = "Zoom"
        Me.btnStateZoom.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "State:"
        '
        'cbStates
        '
        Me.cbStates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbStates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbStates.FormattingEnabled = True
        Me.cbStates.Location = New System.Drawing.Point(76, 32)
        Me.cbStates.Name = "cbStates"
        Me.cbStates.Size = New System.Drawing.Size(198, 21)
        Me.cbStates.TabIndex = 9
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(186, 238)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmNavHelper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 274)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmNavHelper"
        Me.Text = "Navigation Helper"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnHUC8Zoom As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbHUC8s As System.Windows.Forms.ComboBox
    Friend WithEvents btnCountyZoom As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbCounties As System.Windows.Forms.ComboBox
    Friend WithEvents btnStateZoom As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbStates As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetHUC12s As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkBoxNHD As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxCatchment As System.Windows.Forms.CheckBox
    Friend WithEvents btnShowAttributes As System.Windows.Forms.Button
    Friend WithEvents cbLayers As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
