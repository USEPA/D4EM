Imports MapWinUtility

Public Class frmExampleBasinsCore

    Private Sub btnBuildProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuildProject.Click
        If IO.Directory.Exists(txtProjectFolder.Text) Then
            If MsgBox(txtProjectFolder.Text & " exists, delete and replace with newly downloaded data?",
                      Microsoft.VisualBasic.MsgBoxStyle.YesNo) = Microsoft.VisualBasic.MsgBoxResult.No Then
                Exit Sub
            Else
                IO.Directory.Delete(txtProjectFolder.Text, True)
            End If
        End If

        BuildProject(DotSpatial.Projections.ProjectionInfo.FromProj4String(txtProjection.Text),
                     txtCacheFolder.Text, _
                     txtProjectFolder.Text, _
                     txtHuc8.Text)

        MsgBox("BASINS core data has been downloaded to " & txtProjectFolder.Text)
    End Sub

    Public Sub BuildProject(ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo, _
                            ByVal aCacheFolder As String, _
                            ByVal aProjectFolder As String, _
                            ByVal aHuc8 As String)

        Dim lRegion As New D4EM.Data.Region(D4EM.Data.Region.RegionTypes.huc8, aHuc8)
        Dim lProject As New D4EM.Data.Project(aDesiredProjection, aCacheFolder, aProjectFolder, lRegion, False, False)
        D4EM.Data.Source.BASINS.GetBASINS(lProject, Nothing, aHuc8, D4EM.Data.Source.BASINS.LayerSpecifications.core31.all)

    End Sub

    Private Sub Example1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtProjection.Text = D4EM.Data.Globals.AlbersProjection.ToProj4String
    End Sub

End Class
