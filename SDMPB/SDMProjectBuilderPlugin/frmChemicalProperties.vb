Public Class frmChemical

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        params.HspfChemicalName = atxName.Text
        params.HspfChemicalMaximumSolubility = atxMaxSolubility.Text
        params.HspfChemicalPartitionCoeff(0) = atxSurfacePC.Text
        params.HspfChemicalPartitionCoeff(1) = atxUpperPC.Text
        params.HspfChemicalPartitionCoeff(2) = atxLowerPC.Text
        params.HspfChemicalPartitionCoeff(3) = atxActivePC.Text
        params.HspfChemicalFreundlichExp(0) = atxSurfaceEx.Text
        params.HspfChemicalFreundlichExp(1) = atxUpperEx.Text
        params.HspfChemicalFreundlichExp(2) = atxLowerEx.Text
        params.HspfChemicalFreundlichExp(3) = atxActiveEx.Text
        params.HspfChemicalDegradationRate(0) = atxSurfaceDR.Text
        params.HspfChemicalDegradationRate(1) = atxUpperDR.Text
        params.HspfChemicalDegradationRate(2) = atxLowerDR.Text
        params.HspfChemicalDegradationRate(3) = atxActiveDR.Text
        Me.Close()
    End Sub

    Friend Sub SetParams()
        atxName.Text = params.HspfChemicalName
        atxMaxSolubility.Text = params.HspfChemicalMaximumSolubility
        atxSurfacePC.Text = params.HspfChemicalPartitionCoeff(0)
        atxUpperPC.Text = params.HspfChemicalPartitionCoeff(1)
        atxLowerPC.Text = params.HspfChemicalPartitionCoeff(2)
        atxActivePC.Text = params.HspfChemicalPartitionCoeff(3)
        atxSurfaceEx.Text = params.HspfChemicalFreundlichExp(0)
        atxUpperEx.Text = params.HspfChemicalFreundlichExp(1)
        atxLowerEx.Text = params.HspfChemicalFreundlichExp(2)
        atxActiveEx.Text = params.HspfChemicalFreundlichExp(3)
        atxSurfaceDR.Text = params.HspfChemicalDegradationRate(0)
        atxUpperDR.Text = params.HspfChemicalDegradationRate(1)
        atxLowerDR.Text = params.HspfChemicalDegradationRate(2)
        atxActiveDR.Text = params.HspfChemicalDegradationRate(3)
    End Sub
End Class