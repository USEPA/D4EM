Module NRCS_Soil_Tests

    Public Sub main()
        Test_GetSoils()
    End Sub

    ''' <summary>
    ''' Test the NRCS_Soil methods: GetSoils(Project) 
    ''' Indirectly tests methods: ParseSoilFeatures, SetSoilProperties, FindSoilLayerProperties, MakePolygonsLayer, PopulateSoils, RunSoilQuery
    ''' Also tests all classes within SoilLocation
    ''' </summary>
    ''' <remarks>Other methods (FindSoils, ComputeUniqueLayerProperties, FindSoilLayerProperties) are part of a different workflow used by Stormwater Calculator</remarks>
    Public Sub Test_GetSoils()
        D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8) = atcUtility.FindFile("Please locate national HUC-8 shapefile", "S:\BASINS\data\national\huc250d3.shp")
        'OriginalTest Dim lRegion As New D4EM.Data.Region(38.3468989298251, 38.2836163635915, -82.1690054354632, -82.0576704259708, D4EM.Data.Globals.GeographicProjection)
        'Small1
        'Dim lRegion As New D4EM.Data.Region(47.64, 47.19, -123.64, -123.2, D4EM.Data.Globals.GeographicProjection)
        'Small2        Dim lRegion As New D4EM.Data.Region(43.27, 42.42, -119.18, -118.57, D4EM.Data.Globals.GeographicProjection)
        'Big1
        Dim lRegion As New D4EM.Data.Region(41.08, 39.44, -94.74, -93.42, D4EM.Data.Globals.GeographicProjection)
        'Big2 Dim lRegion As New D4EM.Data.Region(38.95, 38.06, -96.26, -94.84, D4EM.Data.Globals.GeographicProjection)
        Dim lProject = New D4EM.Data.Project(
                       aDesiredProjection:=D4EM.Data.Globals.AlbersProjection,
                       aCacheFolder:=IO.Path.Combine(IO.Path.GetTempPath, "D4EM_Test_Cache"),
                       aProjectFolder:=IO.Path.Combine(IO.Path.GetTempPath, "D4EM_Test_GetSoils" & atcUtility.SafeFilename(lRegion.ToString.Replace(" ", ""))),
                       aRegion:=lRegion,
                       aClip:=False, aMerge:=False)
        MapWinUtility.TryDelete(lProject.ProjectFolder)
        MapWinUtility.Logger.StartToFile(IO.Path.Combine(lProject.ProjectFolder, "NRCS_Test_" & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & ".txt"))
        Dim lSoils = D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(lProject, Nothing)
        If lSoils Is Nothing OrElse lSoils.Count = 0 Then
            MsgBox("No soils found, aborting Test_GetSoils")
        Else
            Dim lSoilsTag As String = D4EM.Data.Source.NRCS_Soil.SoilLocation.SoilLayerSpecification.Tag
            Dim lSoilsLayer = lProject.LayerFromTag(lSoilsTag)
            If lSoilsLayer Is Nothing Then
                MsgBox("Soils layer with tag '" & lSoilsTag & "' not found, aborting Test_GetSoils")
            ElseIf Not IO.File.Exists(lSoilsLayer.FileName) Then
                MsgBox("Soils layer with tag '" & lSoilsTag & "' not found, aborting Test_GetSoils")
            Else
                Dim lNumWithHSG As Integer = 0
                For Each lSoil In lSoils
                    If Not String.IsNullOrEmpty(lSoil.HSG) Then
                        lNumWithHSG += 1
                    End If
                Next
                Dim lShowDataFolderLabel As String = "Show Data Folder"
                Dim lPressed As String = ""
                If lProject.Layers.Count = 1 Then
                    lPressed = MapWinUtility.Logger.MsgCustom("Successful test of NRCS_Soil." & vbCrLf _
                       & lNumWithHSG & " of " & lSoils.Count & " soils have HSG (" & lSoils.Count - lNumWithHSG & " do not)" & vbCrLf _
                       & "Created layer " & lSoilsLayer.FileName, "NRCS Soil Test", lShowDataFolderLabel, "Close")
                Else
                    lPressed = MapWinUtility.Logger.MsgCustom("Successful test of NRCS_Soil." & vbCrLf _
                       & lNumWithHSG & " of " & lSoils.Count & " soils have HSG (" & lSoils.Count - lNumWithHSG & " do not)" & vbCrLf _
                       & "Created " & lProject.Layers.Count & " layers.", "NRCS Soil Test", lShowDataFolderLabel, "Close")
                End If
                If lPressed = lShowDataFolderLabel Then
                    atcUtility.OpenFile(lProject.ProjectFolder)
                End If
                'MapWinUtility.Logger.Dbg("MuKey" & vbTab & "AreaSymbol" & vbTab & "MuSym" & vbTab & "HSG" & vbTab & "Polygons" & vbTab & "Components")
                'For Each lSoil In lSoils
                '    MapWinUtility.Logger.Dbg(lSoil.MuKey & vbTab & lSoil.AreaSymbol & vbTab & lSoil.MuSym & vbTab & lSoil.HSG & vbTab & lSoil.Polygons.Count & vbTab & lSoil.Components.Count)
                'Next
            End If
        End If
    End Sub
End Module
