Imports System.Data
Imports DotSpatial.Controls
Imports DotSpatial.Data
Imports DotSpatial.Symbology
Imports System.Windows.Forms

Public Class frmNavHelper

    Private map As DotSpatial.Controls.Map

    Dim _flStates As IFeatureLayer = Nothing
    Dim _flHUC8s As IFeatureLayer = Nothing
    Dim _flCounties As IFeatureLayer = Nothing
    Dim _flSelectedLayer As IFeatureLayer = Nothing


    'Public Sub New(dsMap As Map)
    Public Sub New(appMgr As AppManager)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        map = appMgr.Map

    End Sub
    Private Sub frmNavHelper_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim defCursor As System.Windows.Forms.Cursor = Me.Cursor
        Try

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim layers As New List(Of ILayer)
            layers = map.GetLayers()

            Dim name As String
            For Each layer As IMapLayer In layers

                If Not (layer.DataSet Is Nothing) And Not (String.IsNullOrWhiteSpace(layer.DataSet.Name)) Then
                    name = layer.DataSet.Name
                    cbLayers.Items.Add(name)
                Else
                    Continue For
                End If
            Next

            _flStates = GetLayer(NATIONAL_ST)
            _flCounties = GetLayer(NATIONAL_CNTY)
            _flHUC8s = GetLayer(NATIONAL_HUC8)
            '_flStates = GetLayer("st")
            '_flHUC8s = GetLayer("huc250d3")
            '_flCounties = GetLayer("cnty")

            If Not (_flStates Is Nothing) Then
                Dim fs As IFeatureSet = CType(_flStates.DataSet, IFeatureSet)
                For Each dr As DataRow In fs.DataTable.Rows
                    Dim lItem As New ListItem(dr("Name").ToString(), dr("ST").ToString())
                    cbStates.Items.Add(lItem)
                Next
            End If

            If Not (_flHUC8s Is Nothing) Then
                Dim fs As IFeatureSet = CType(_flHUC8s.DataSet, IFeatureSet)
                For Each dr As DataRow In fs.DataTable.Rows
                    cbHUC8s.Items.Add(New ListItem(dr("CU").ToString(), dr("CATNAME").ToString()))
                Next
            End If

            If (g_NationalProject Is Nothing) Then
                btnGetHUC12s.Enabled = False
            Else
                btnGetHUC12s.Enabled = True
            End If


        Catch ex As Exception

        Finally
            Me.Cursor = defCursor

        End Try

    End Sub

    

    Private Function GetLayer(Name As String) As ILayer
        Dim layers As New List(Of ILayer)
        layers = map.GetLayers()

        Dim dsName As String
        For Each layer As ILayer In layers

            If Not (layer.DataSet Is Nothing) And Not (String.IsNullOrWhiteSpace(layer.DataSet.Name)) Then
                dsName = layer.DataSet.Name
                If (String.Compare(dsName, Name, True) = 0) Then

                    Return layer
                End If
            Else
                Continue For
            End If

        Next

        Return Nothing
    End Function


    Private Sub cbStates_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbStates.SelectedIndexChanged

        Dim lCounties As ILayer = GetLayer(NATIONAL_CNTY)

        If (lCounties Is Nothing) Then
            Return
        End If

        Dim lItem As ListItem = cbStates.SelectedItem
        If (IsNothing(lItem)) Then
            Return
        End If

        If (String.IsNullOrWhiteSpace(lItem.DataValue) = True) Then
            Return
        End If
        Dim stateAbbrev = lItem.DataValue

        Dim fs As IFeatureSet = CType(lCounties.DataSet, IFeatureSet)
        If (fs.AttributesPopulated = False) Then
            fs.FillAttributes()
        End If
        Dim rows() As DataRow = fs.DataTable.Select("STATE = '" & stateAbbrev & "'")

        cbCounties.Items.Clear()

        For Each dr As DataRow In rows
            cbCounties.Items.Add(New ListItem(dr("CNTYNAME").ToString(), dr("FIPS").ToString()))
        Next


    End Sub

   

    Private Sub btnStateZoom_Click(sender As System.Object, e As System.EventArgs) Handles btnStateZoom.Click

        If (_flStates Is Nothing) Then
            Return
        End If

        Dim li As ListItem = CType(cbStates.SelectedItem, ListItem)
        If (IsNothing(li)) Then
            Return
        End If

        If (String.IsNullOrWhiteSpace(li.DisplayValue) = True) Then
            Return
        End If
        _flStates.SelectByAttribute("ST = '" & li.DataValue & "'")
        _flStates.ZoomToSelectedFeatures()

    End Sub

    Private Sub btnCountyZoom_Click(sender As System.Object, e As System.EventArgs) Handles btnCountyZoom.Click
        If (_flCounties Is Nothing) Then
            Return
        End If

        Dim li As ListItem = CType(cbCounties.SelectedItem, ListItem)
        If (IsNothing(li)) Then
            Return
        End If

        If (String.IsNullOrWhiteSpace(li.DisplayValue) = True) Then
            Return
        End If

        _flCounties.SelectByAttribute("FIPS = '" & li.DataValue & "'")
        _flCounties.ZoomToSelectedFeatures()
    End Sub

    Private Sub btnHUC8Zoom_Click(sender As System.Object, e As System.EventArgs) Handles btnHUC8Zoom.Click
        If (_flHUC8s Is Nothing) Then
            Return
        End If

        Dim li As ListItem = CType(cbHUC8s.SelectedItem, ListItem)
        If (IsNothing(li)) Then
            Return
        End If

        If (String.IsNullOrWhiteSpace(li.DisplayValue) = True) Then
            Return
        End If


        _flHUC8s.SelectByAttribute("CU = '" & li.DisplayValue & "'")
        _flHUC8s.ZoomToSelectedFeatures()
    End Sub


    Private Sub btnGetHUC12s_Click(sender As System.Object, e As System.EventArgs) Handles btnGetHUC12s.Click

        If (_flHUC8s Is Nothing) Then
            Return
        End If

        If (cbHUC8s.SelectedItem Is Nothing) Then
            Return
        End If
        Dim li As ListItem = CType(cbHUC8s.SelectedItem, ListItem)


        If (chkBoxCatchment.Checked) Then
            Dim layerNHD As ILayer = modSDM_GUI.LoadLayerForHUC8(D4EM.Data.Region.RegionTypes.huc12, li.DisplayValue)
        End If
        If (chkBoxNHD.Checked) Then
            Dim layerHUC12 As ILayer = modSDM_GUI.LoadLayerForHUC8(D4EM.Data.Region.RegionTypes.catchment, li.DisplayValue)
        End If






    End Sub


    



    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()

    End Sub

    
    Private Sub btnShowAttributes_Click(sender As System.Object, e As System.EventArgs) Handles btnShowAttributes.Click

        Dim layerName As String = cbLayers.SelectedItem.ToString()
        Dim layer As IFeatureLayer = GetLayer(layerName)

        If (layer Is Nothing) Then
            Return
        End If

        Dim fs As IFeatureSet = CType(layer.DataSet, IFeatureSet)
        If (fs.AttributesPopulated = False) Then
            fs.FillAttributes()
        End If

        layer.ShowAttributes()


    End Sub
End Class