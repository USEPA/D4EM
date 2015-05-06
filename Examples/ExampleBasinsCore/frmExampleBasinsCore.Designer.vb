<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExampleBasinsCore
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
        Me.btnBuildProject = New System.Windows.Forms.Button()
        Me.txtProjectFolder = New System.Windows.Forms.TextBox()
        Me.lblProjectFolder = New System.Windows.Forms.Label()
        Me.lblCacheFolder = New System.Windows.Forms.Label()
        Me.txtCacheFolder = New System.Windows.Forms.TextBox()
        Me.lblHuc8 = New System.Windows.Forms.Label()
        Me.txtHuc8 = New System.Windows.Forms.TextBox()
        Me.lblProjection = New System.Windows.Forms.Label()
        Me.txtProjection = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnBuildProject
        '
        Me.btnBuildProject.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBuildProject.Location = New System.Drawing.Point(103, 128)
        Me.btnBuildProject.Name = "btnBuildProject"
        Me.btnBuildProject.Size = New System.Drawing.Size(75, 23)
        Me.btnBuildProject.TabIndex = 13
        Me.btnBuildProject.Text = "Build Project"
        Me.btnBuildProject.UseVisualStyleBackColor = True
        '
        'txtProjectFolder
        '
        Me.txtProjectFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjectFolder.Location = New System.Drawing.Point(103, 12)
        Me.txtProjectFolder.Name = "txtProjectFolder"
        Me.txtProjectFolder.Size = New System.Drawing.Size(275, 20)
        Me.txtProjectFolder.TabIndex = 1
        Me.txtProjectFolder.Text = "C:\Basins\data\03070103"
        '
        'lblProjectFolder
        '
        Me.lblProjectFolder.AutoSize = True
        Me.lblProjectFolder.Location = New System.Drawing.Point(12, 15)
        Me.lblProjectFolder.Name = "lblProjectFolder"
        Me.lblProjectFolder.Size = New System.Drawing.Size(72, 13)
        Me.lblProjectFolder.TabIndex = 2
        Me.lblProjectFolder.Text = "Project Folder"
        '
        'lblCacheFolder
        '
        Me.lblCacheFolder.AutoSize = True
        Me.lblCacheFolder.Location = New System.Drawing.Point(12, 41)
        Me.lblCacheFolder.Name = "lblCacheFolder"
        Me.lblCacheFolder.Size = New System.Drawing.Size(70, 13)
        Me.lblCacheFolder.TabIndex = 4
        Me.lblCacheFolder.Text = "Cache Folder"
        '
        'txtCacheFolder
        '
        Me.txtCacheFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCacheFolder.Location = New System.Drawing.Point(103, 38)
        Me.txtCacheFolder.Name = "txtCacheFolder"
        Me.txtCacheFolder.Size = New System.Drawing.Size(275, 20)
        Me.txtCacheFolder.TabIndex = 3
        Me.txtCacheFolder.Text = "C:\Basins\cache"
        '
        'lblHuc8
        '
        Me.lblHuc8.AutoSize = True
        Me.lblHuc8.Location = New System.Drawing.Point(12, 67)
        Me.lblHuc8.Name = "lblHuc8"
        Me.lblHuc8.Size = New System.Drawing.Size(39, 13)
        Me.lblHuc8.TabIndex = 6
        Me.lblHuc8.Text = "HUC 8"
        '
        'txtHuc8
        '
        Me.txtHuc8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHuc8.Location = New System.Drawing.Point(103, 64)
        Me.txtHuc8.Name = "txtHuc8"
        Me.txtHuc8.Size = New System.Drawing.Size(275, 20)
        Me.txtHuc8.TabIndex = 5
        Me.txtHuc8.Text = "03070103"
        '
        'lblProjection
        '
        Me.lblProjection.AutoSize = True
        Me.lblProjection.Location = New System.Drawing.Point(12, 93)
        Me.lblProjection.Name = "lblProjection"
        Me.lblProjection.Size = New System.Drawing.Size(54, 13)
        Me.lblProjection.TabIndex = 8
        Me.lblProjection.Text = "Projection"
        '
        'txtProjection
        '
        Me.txtProjection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjection.Location = New System.Drawing.Point(103, 90)
        Me.txtProjection.Name = "txtProjection"
        Me.txtProjection.Size = New System.Drawing.Size(275, 20)
        Me.txtProjection.TabIndex = 7
        '
        'frmExampleBasinsCore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 163)
        Me.Controls.Add(Me.lblProjection)
        Me.Controls.Add(Me.txtProjection)
        Me.Controls.Add(Me.lblHuc8)
        Me.Controls.Add(Me.txtHuc8)
        Me.Controls.Add(Me.lblCacheFolder)
        Me.Controls.Add(Me.txtCacheFolder)
        Me.Controls.Add(Me.lblProjectFolder)
        Me.Controls.Add(Me.txtProjectFolder)
        Me.Controls.Add(Me.btnBuildProject)
        Me.Name = "frmExampleBasinsCore"
        Me.Text = "D4EM BASINS Core"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuildProject As System.Windows.Forms.Button
    Friend WithEvents txtProjectFolder As System.Windows.Forms.TextBox
    Friend WithEvents lblProjectFolder As System.Windows.Forms.Label
    Friend WithEvents lblCacheFolder As System.Windows.Forms.Label
    Friend WithEvents txtCacheFolder As System.Windows.Forms.TextBox
    Friend WithEvents lblHuc8 As System.Windows.Forms.Label
    Friend WithEvents txtHuc8 As System.Windows.Forms.TextBox
    Friend WithEvents lblProjection As System.Windows.Forms.Label
    Friend WithEvents txtProjection As System.Windows.Forms.TextBox

End Class
