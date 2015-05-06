Imports MapWinUtility

Public Class Example1

    Private Sub btnBuildProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuildProject.Click
        Dim lProjection As New DotSpatial.Projections.ProjectionInfo(txtProjection.Text)
        If lProjection Is Nothing Then
            If MsgBox("Could not create specified projection" & vbCrLf & "Use Albers Equal Area?", MsgBoxStyle.YesNo) = vbNo Then
                Exit Sub
            Else
                lProjection = D4EM.Data.Globals.AlbersProjection
            End If
        End If
        BuildProject(lProjection, _
                     txtCacheFolder.Text, _
                     txtProjectFolder.Text, _
                     txtHuc.Text, _
                     chkClip.Checked, _
                     chkMerge.Checked, _
                     chkGetEvenIfCached.Checked,
                     chkCacheOnly.Checked)
    End Sub

    Private Sub Example1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtProjection.Text = D4EM.Data.Globals.AlbersProjection.ToProj4String
    End Sub

    Private Sub chkCacheOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCacheOnly.CheckedChanged
        If chkCacheOnly.Checked Then
            chkClip.Checked = False
            chkMerge.Checked = False
            chkClip.Enabled = False
            chkMerge.Enabled = False
        Else
            chkClip.Enabled = True
            chkMerge.Enabled = True
        End If
    End Sub
End Class
