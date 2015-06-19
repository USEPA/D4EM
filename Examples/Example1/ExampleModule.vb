Imports MapWinUtility
Imports atcUtility
Imports D4EM.Data
Imports DotSpatial.Data
Imports DotSpatial.Projections

Module ExampleModule
    Sub BuildProject(ByVal aDesiredProjection As DotSpatial.Projections.ProjectionInfo, _
                     ByVal aCacheFolder As String, _
                     ByVal aProjectFolder As String, _
                     ByVal aHuc As String, _
                     ByVal aClip As Boolean, _
                     ByVal aMerge As Boolean, _
            Optional ByVal aGetEvenIfCached As Boolean = False, _
            Optional ByVal aCacheOnly As Boolean = False)
        Logger.StartToFile(IO.Path.Combine(aCacheFolder, "log" & g_PathChar _
                         & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & "-Example1.log"))
        Logger.Dbg("D4EM_Driver Start")

        'Dim lProjectFolder As String = "C:\Dev\D4EM\D4EM\D4EM.Data.Tests\data\03070103\"
        'Dim lRegionG As Region = New Region("03070103")
        'lRegionG.PreferredFormat = "huc8"
        'Dim aGridFilename As String = lProjectFolder & "NLCD_landcover_2001.tif"
        'Dim aClippedFilename As String = lProjectFolder & "NLCD_landcover_2001_clipped.tif"
        'Dim pProjectionStringAlbers As String = "+x_0=0 +y_0=0 +lat_0=23 +lon_0=-96 +lat_1=29.5 +lat_2=45.5 +proj=aea +ellps=GRS80 +no_defs"
        'Dim aGridProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringAlbers)
        'Dim lResultG As Boolean = lRegionG.ClipGrid(aGridFilename, aClippedFilename, aGridProjection)

        If IO.Directory.Exists(aProjectFolder) Then
            If aProjectFolder.ToLower.Contains("basins\data") Then
                'TODO: try to stop big delete, need to customize
                Logger.Dbg("DeleteExistingFolder " & aProjectFolder)
                IO.Directory.Delete(aProjectFolder, True)
            Else
                Logger.Dbg("UsingExistingFolder " & aProjectFolder)
            End If
        End If

        Dim lRegion As D4EM.Data.Region
        Select Case aHuc.Length
            Case 8 : lRegion = New D4EM.Data.Region(Region.RegionTypes.huc8, aHuc)
            Case 12 : lRegion = New D4EM.Data.Region(Region.RegionTypes.huc12, aHuc)
            Case Else
                Throw New ApplicationException("Only 8 and 12-digit HUCs are supported, '" & aHuc & "' is " & aHuc.Length & " characters long")
        End Select
        Dim lHuc8 As String = SafeSubstring(aHuc, 0, 8)

        Dim lProject As New D4EM.Data.Project(aDesiredProjection, aCacheFolder, aProjectFolder, lRegion, aClip, aMerge, aGetEvenIfCached, aCacheOnly)
        Dim lResult As String = ""

        'NWIS
        Dim lNWISfolder As String = IO.Path.Combine(lProject.ProjectFolder, "NWIS")
        Dim lNWISdischargeStationsFilename As String = IO.Path.Combine(lNWISfolder, "NWIS_Stations_discharge.rdb")
        lResult = D4EM.Data.Source.NWIS.GetStationsInRegion(lProject.Region, lNWISdischargeStationsFilename, D4EM.Data.Source.NWIS.LayerSpecifications.Discharge)
        LogLayers("After NWIS.GetStationsInRegion - Groundwater Result(" & lResult.Length & ") " & lResult, lProject)

        Dim lStations As New Generic.List(Of String)
        lStations.Add("02207335") 'TODO: could get station ID from results of above call
        lResult = D4EM.Data.Source.NWIS.GetDailyDischarge(lProject, lNWISfolder, lStations)
        LogTimeseriesSources("After NWIS.GetDailyDischarge Result(" & lResult.Length & ") " & lResult, lProject)

        lStations.Clear()
        lProject.Layers.Clear()
        lProject.TimeseriesSources.Clear()
        lResult = D4EM.Data.Source.NWIS.GetStationsInRegion(lProject.Region, lNWISdischargeStationsFilename, D4EM.Data.Source.NWIS.LayerSpecifications.Groundwater)
        LogLayers("After NWIS.GetStationsInRegion - Groundwater Result(" & lResult.Length & ") " & lResult, lProject)
        lStations.Add("")
        lResult = D4EM.Data.Source.NWIS.GetDailyGroundwater(lProject, lNWISfolder, lStations)

        lProject.Layers.Clear()
        lResult = D4EM.Data.Source.BASINS.GetBASINS(lProject, Nothing, lHuc8, D4EM.Data.Source.BASINS.BASINSDataType.core31)
        LogLayers("After GetBASINS Result(" & lResult.Length & ") " & lResult, lProject)

        lResult = D4EM.Data.Source.NHDPlus.GetNHDPlus(lProject, "NHDPlus", lHuc8, True, D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid)
        Logger.Dbg("After NHDPlus.GetNHDPlus " & lProject.Layers.Count & " layers" & vbCrLf & " Result(" & lResult.Length & ") " & lResult)

        lResult = D4EM.Data.Source.BASINS.GetBASINS(lProject, Nothing, lHuc8, D4EM.Data.Source.BASINS.BASINSDataType.lstoret)
        Logger.Dbg("After GetBASINSLStoret Result(" & lResult.Length & ") " & lResult & vbCrLf & lProject.Summary)

        Dim lFeatureSet As DotSpatial.Data.FeatureSet = lProject.GetFeatureSet("core31.cat")
        If lFeatureSet Is Nothing Then
            Logger.Dbg("FeatureSetNOTFound")
        Else
            Logger.Dbg("Found FeatureSet " & lFeatureSet.Name & " as " & lFeatureSet.FeatureType.ToString)
        End If

        Dim lBasinsStates As D4EM.Data.Layer = lProject.LayerFromTag(Data.Source.BASINS.LayerSpecifications.core31.st.Tag)
        If lBasinsStates Is Nothing Then
            Logger.Dbg("BasinsStatesNOTFound")
        Else
            Logger.Dbg("BASINS States layer: " & lBasinsStates.FileName)
        End If

        Dim lMetStations As Generic.List(Of String) = Nothing
        'If met station ID is known, lMetStations.Add("myStationId") and skip BASINS.GetMetStations
        lResult = D4EM.Data.Source.BASINS.GetMetStations(lProject, lMetStations, True)
        Logger.Dbg("After BASINS.GetMetStations Result(" & lResult.Length & ") " & lResult & vbTab & lProject.Summary & " " & lMetStations.Count & " MetStations")

        If lMetStations.Count > 1 Then 'Only retrieve data for first met station found in region
            lMetStations.RemoveRange(1, lMetStations.Count - 1)
        End If

        If lMetStations.Count > 0 Then
            'lResult = (D4EM.Data.Source.BASINS.GetMetData(lProject, lMetStations, "met.wdm"))
            Logger.Dbg("After BASINS.GetMetData Result(" & lResult.Length & ") " & lResult & vbTab & lProject.Summary & " " & lMetStations.Count & " MetStations")
        End If

        lResult = D4EM.Data.Source.NLDAS.GetLocations(lProject, "NLDAS")
        Logger.Dbg("After NLDAS.GetLocations " & lProject.Layers.Count & " layers" & vbCrLf & " Result(" & lResult.Length & ") " & lResult)

        lResult = D4EM.Data.Source.USGS_Seamless.Execute(lProject, "NLCD", D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover)
        Logger.Dbg("After USGS_Seamless.Execute " & lProject.Layers.Count & " layers" & vbCrLf & " Result(" & lResult.Length & ") " & lResult)

        'D4EM.Hydrography.CombineShortOrBraidedFlowlines()
        'D4EM.Timeseries.Aggregate(aTimeSeries, aAggreationFunction)

        'D4EM.Models.SWAT
        'D4EM.Models.HSPF
        'D4EM.Models.WASP

        'D4EM.MetaData.AddProcStep(lNWISData,Curdate & "Stored in my local cache")
        'Dim lMetaDataForm as new D4EM.MetaData.DisplayForm
        'D4EM.MetaData.View(lMyProject,lMetaDataForm)

        'now save a DotSpatial, MapWindow 4.8 or ArcGIS project file 

        Logger.Msg("LogFile " & Logger.FileName, "D4EM Example1 End")

    End Sub

    Private Sub LogLayers(ByVal aMessage As String, ByVal aProject As D4EM.Data.Project)
        Logger.Dbg(aMessage & " " & aProject.Layers.Count & " layers:")
        For Each lLayer In aProject.Layers
            Logger.Dbg(lLayer.FileName)
        Next
    End Sub

    Private Sub LogTimeseriesSources(ByVal aMessage As String, ByVal aProject As D4EM.Data.Project)
        Logger.Dbg(aMessage & " " & aProject.TimeseriesSources.Count & " timeseries sources:")
        For Each lTimeseriesSource In aProject.TimeseriesSources
            Logger.Dbg(lTimeseriesSource.Specification & " contains " & lTimeseriesSource.DataSets.Count & " datasets")
            For Each lTimeseries As atcData.atcTimeseries In lTimeseriesSource.DataSets
                Logger.Dbg(lTimeseries.Serial & " contains " & lTimeseries.numValues & " values from " & DumpDate(lTimeseries.Dates.Value(1)) & " to " & DumpDate(lTimeseries.Dates.Value(lTimeseries.numValues)))
            Next
        Next
    End Sub


End Module
