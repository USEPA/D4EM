<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Example1
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
        Me.lblHuc = New System.Windows.Forms.Label()
        Me.txtHuc = New System.Windows.Forms.TextBox()
        Me.lblProjection = New System.Windows.Forms.Label()
        Me.txtProjection = New System.Windows.Forms.TextBox()
        Me.chkClip = New System.Windows.Forms.CheckBox()
        Me.chkMerge = New System.Windows.Forms.CheckBox()
        Me.chkGetEvenIfCached = New System.Windows.Forms.CheckBox()
        Me.chkCacheOnly = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnBuildProject
        '
        Me.btnBuildProject.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBuildProject.Location = New System.Drawing.Point(15, 231)
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
        Me.txtProjectFolder.Size = New System.Drawing.Size(318, 20)
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
        Me.txtCacheFolder.Size = New System.Drawing.Size(318, 20)
        Me.txtCacheFolder.TabIndex = 3
        Me.txtCacheFolder.Text = "C:\Basins\cache"
        '
        'lblHuc
        '
        Me.lblHuc.AutoSize = True
        Me.lblHuc.Location = New System.Drawing.Point(12, 67)
        Me.lblHuc.Name = "lblHuc"
        Me.lblHuc.Size = New System.Drawing.Size(66, 13)
        Me.lblHuc.TabIndex = 6
        Me.lblHuc.Text = "HUC 8 or 12"
        '
        'txtHuc
        '
        Me.txtHuc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHuc.Location = New System.Drawing.Point(103, 64)
        Me.txtHuc.Name = "txtHuc"
        Me.txtHuc.Size = New System.Drawing.Size(318, 20)
        Me.txtHuc.TabIndex = 5
        Me.txtHuc.Text = "03070103"
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
        Me.txtProjection.Size = New System.Drawing.Size(318, 20)
        Me.txtProjection.TabIndex = 7
        '
        'chkClip
        '
        Me.chkClip.AutoSize = True
        Me.chkClip.Location = New System.Drawing.Point(103, 139)
        Me.chkClip.Name = "chkClip"
        Me.chkClip.Size = New System.Drawing.Size(164, 17)
        Me.chkClip.TabIndex = 10
        Me.chkClip.Text = "Clip Layers to Area of Interest"
        Me.chkClip.UseVisualStyleBackColor = True
        '
        'chkMerge
        '
        Me.chkMerge.AutoSize = True
        Me.chkMerge.Location = New System.Drawing.Point(103, 162)
        Me.chkMerge.Name = "chkMerge"
        Me.chkMerge.Size = New System.Drawing.Size(268, 17)
        Me.chkMerge.TabIndex = 11
        Me.chkMerge.Text = "Merge Separately Downloaded Parts of Each Layer"
        Me.chkMerge.UseVisualStyleBackColor = True
        '
        'chkGetEvenIfCached
        '
        Me.chkGetEvenIfCached.AutoSize = True
        Me.chkGetEvenIfCached.Location = New System.Drawing.Point(103, 185)
        Me.chkGetEvenIfCached.Name = "chkGetEvenIfCached"
        Me.chkGetEvenIfCached.Size = New System.Drawing.Size(195, 17)
        Me.chkGetEvenIfCached.TabIndex = 12
        Me.chkGetEvenIfCached.Text = "Retrieve New Copy Even If Cached"
        Me.chkGetEvenIfCached.UseVisualStyleBackColor = True
        '
        'chkCacheOnly
        '
        Me.chkCacheOnly.AutoSize = True
        Me.chkCacheOnly.Location = New System.Drawing.Point(103, 116)
        Me.chkCacheOnly.Name = "chkCacheOnly"
        Me.chkCacheOnly.Size = New System.Drawing.Size(226, 17)
        Me.chkCacheOnly.TabIndex = 9
        Me.chkCacheOnly.Text = "Retrieve Into Cache Only, Do Not Process"
        Me.chkCacheOnly.UseVisualStyleBackColor = True
        '
        'Example1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 266)
        Me.Controls.Add(Me.chkCacheOnly)
        Me.Controls.Add(Me.chkGetEvenIfCached)
        Me.Controls.Add(Me.chkMerge)
        Me.Controls.Add(Me.chkClip)
        Me.Controls.Add(Me.lblProjection)
        Me.Controls.Add(Me.txtProjection)
        Me.Controls.Add(Me.lblHuc)
        Me.Controls.Add(Me.txtHuc)
        Me.Controls.Add(Me.lblCacheFolder)
        Me.Controls.Add(Me.txtCacheFolder)
        Me.Controls.Add(Me.lblProjectFolder)
        Me.Controls.Add(Me.txtProjectFolder)
        Me.Controls.Add(Me.btnBuildProject)
        Me.Name = "Example1"
        Me.Text = "D4EM Example1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuildProject As System.Windows.Forms.Button
    Friend WithEvents txtProjectFolder As System.Windows.Forms.TextBox
    Friend WithEvents lblProjectFolder As System.Windows.Forms.Label
    Friend WithEvents lblCacheFolder As System.Windows.Forms.Label
    Friend WithEvents txtCacheFolder As System.Windows.Forms.TextBox
    Friend WithEvents lblHuc As System.Windows.Forms.Label
    Friend WithEvents txtHuc As System.Windows.Forms.TextBox
    Friend WithEvents lblProjection As System.Windows.Forms.Label
    Friend WithEvents txtProjection As System.Windows.Forms.TextBox
    Friend WithEvents chkClip As System.Windows.Forms.CheckBox
    Friend WithEvents chkMerge As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetEvenIfCached As System.Windows.Forms.CheckBox
    Friend WithEvents chkCacheOnly As System.Windows.Forms.CheckBox

End Class
