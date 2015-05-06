Module NLDAS_Tests

    Public Sub main()
        Test_GetNLDAS()
    End Sub

    ''' <summary>
    ''' Test the GetNLDAS method
    ''' Indirectly tests methods in D4EM.Data.Source.NLDAS
    ''' </summary>
    Public Sub Test_GetNLDAS()
        Try
            D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8) = atcUtility.FindFile("Please locate national HUC-8 shapefile", "S:\BASINS\data\national\huc250d3.shp")
            'OriginalTest Dim lRegion As New D4EM.Data.Region(38.3468989298251, 38.2836163635915, -82.1690054354632, -82.0576704259708, D4EM.Data.Globals.GeographicProjection)
            'Small1
            'Dim lRegion As New D4EM.Data.Region(47.64, 47.19, -123.64, -123.2, D4EM.Data.Globals.GeographicProjection)
            'Small2
            'Dim lRegion As New D4EM.Data.Region(43.27, 42.42, -119.18, -118.57, D4EM.Data.Globals.GeographicProjection)
            'Big1
            'Dim lRegion As New D4EM.Data.Region(41.08, 39.44, -94.74, -93.42, D4EM.Data.Globals.GeographicProjection)
            'Big2
            'Dim lRegion As New D4EM.Data.Region(38.95, 38.06, -96.26, -94.84, D4EM.Data.Globals.GeographicProjection)
            'HUC-8
            Dim lRegion As New D4EM.Data.Region(D4EM.Data.National.LayerSpecifications.huc8, "03070103")
            Dim lProjectFolder As String = IO.Path.Combine(IO.Path.GetTempPath, "D4EM_Test_GetNLDAS" & atcUtility.SafeFilename(lRegion.ToString.Replace(" ", "")))
            Dim lProject = New D4EM.Data.Project(
                           aDesiredProjection:=D4EM.Data.Globals.AlbersProjection,
                           aCacheFolder:=IO.Path.Combine(IO.Path.GetTempPath, "D4EM_Test_Cache"),
                           aProjectFolder:=lProjectFolder,
                           aRegion:=lRegion,
                           aClip:=False, aMerge:=False)
            MapWinUtility.TryDelete(lProject.ProjectFolder)
            MapWinUtility.Logger.StartToFile(IO.Path.Combine(lProject.ProjectFolder, "NLDAS_Test_" & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & ".txt"))
            lProject.ProjectFilename = IO.Path.Combine(lProjectFolder, "D4EM_Test_GetNLDAS.mwprj")
            atcData.atcDataManager.Clear()

            Dim lStep As Integer = 1
            Dim lLastStep As Integer = 2
            Dim lResults As String = ""
            Dim lParameters As New SDM_Project_Builder_Batch.SDMParameters
            With lParameters
                .BasinsMetConstituents.AddRange("PREC ATEM SOLR CLOU WIND".Split(" "))
                .NLDASconstituents.Add("apcpsfc")
                .SimulationStartYear = 2000
                .SimulationEndYear = 2001
            End With
            SDM_Project_Builder_Batch.GetBasinsMet(lProject, lParameters, Nothing, lStep, lLastStep, lResults)
            IO.File.WriteAllText(lProject.ProjectFilename, lProject.AsMWPRJ) 'Note: syncs from Project.TimeseriesSources to atcDataManager.DataSources
            SDM_Project_Builder_Batch.GetNLDAS(lProject, lParameters, Nothing, lStep, lLastStep, lResults)
            Dim lMetDataFolder As String = IO.Path.Combine(lProject.ProjectFolder, "met")
            Dim lDestinationWDMfilename As String = IO.Path.Combine(lMetDataFolder, "met.wdm")
            Dim lWDM = atcData.atcDataManager.DataSourceBySpecification(lDestinationWDMfilename)
            If lWDM Is Nothing Then
                Throw New ApplicationException("Could not open WDM file: " & lDestinationWDMfilename)
            End If
            Dim lOriginalPrecip = lWDM.DataSets.FindData("Constituent", "Replaced")
            If lOriginalPrecip.Count < 1 Then
                Throw New ApplicationException("Replaced Precip not found")
            End If
            Dim lNLDASPrecip = lWDM.DataSets.FindData("Constituent", "PREC")(0)
            If lNLDASPrecip.Attributes.GetValue("Description", "") <> "Hourly Precip in Inches" Then
                Throw New ApplicationException("Precip description does not match")
            End If
            MsgBox("NLDAS Precipitation successfully added to met WDM file")
        Catch ex As ApplicationException
            MsgBox("Failed Test_GetNLDAS: " & ex.ToString)
        End Try
    End Sub
End Module
