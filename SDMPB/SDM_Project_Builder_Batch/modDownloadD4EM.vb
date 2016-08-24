Imports atcData
Imports atcUtility
Imports MapWinUtility
Imports MapWinUtility.Strings
Imports DotSpatial.Projections
Imports System.Collections.Specialized

Public Module modDownloadD4EM
    Private Const XMLappName As String = "SDMProjectBuilder"
    Private pStatusMonitor As MonitorProgressStatus

    Public Sub StartStatusMonitor()
        If Logger.ProgressStatus Is Nothing OrElse Not (TypeOf (Logger.ProgressStatus) Is MonitorProgressStatus) Then
            'Start running status monitor to give better progress and status indication during long-running processes
            pStatusMonitor = New MonitorProgressStatus
            If pStatusMonitor.StartMonitor(FindFile("Find Status Monitor", "StatusMonitor.exe"), , System.Diagnostics.Process.GetCurrentProcess.Id) Then
                'put our status monitor (StatusMonitor.exe) between the Logger and the default MW status monitor
                pStatusMonitor.InnerProgressStatus = Logger.ProgressStatus
                Logger.ProgressStatus = pStatusMonitor
                Logger.Status("LABEL TITLE " & g_AppNameShort & " Status")
                Logger.Status("PROGRESS TIME ON") 'Enable time-to-completion estimation
                Logger.Status("")
            Else
                pStatusMonitor.StopMonitor()
                pStatusMonitor = Nothing
            End If
        End If
    End Sub

    Public Function SpecifyAndCreateNewProjects(ByVal aParameters As SDMParameters) As Generic.List(Of String)
        StartStatusMonitor()
        Dim lCreatedProjectFilenames As New Generic.List(Of String)
        Dim lProjectIndex As Integer = 0
        For Each lProject In aParameters.Projects
            EnsureCacheFolderSet(IO.Path.Combine(aParameters.ProjectsPath, "cache"), lProject.CacheFolder)
            Logger.Progress("Creating project for " & lProject.Region.ToString, lProjectIndex, aParameters.Projects.Count)
            Using lLevel As New ProgressLevel(False)
                DownloadDataSetupModels(lProject, aParameters)
                If IO.File.Exists(lProject.ProjectFilename) Then
                    Logger.Status("Finished Building " & lProject.ProjectFilename, True)
                    lCreatedProjectFilenames.Add(lProject.ProjectFilename)
                End If
            End Using
            lProjectIndex += 1
        Next

        If lCreatedProjectFilenames.Count = 1 Then
            If Logger.MsgCustom("Finished Building Project " & lCreatedProjectFilenames(0), g_AppNameLong, "Ok", "Open Folder") = "Open Folder" Then
                OpenFile(IO.Path.GetDirectoryName(lCreatedProjectFilenames(0)))
            End If
        Else
            Logger.Msg("Finished Building " & lCreatedProjectFilenames.Count & " Projects", g_AppNameLong)
        End If

        If pStatusMonitor IsNot Nothing Then
            Logger.ProgressStatus = pStatusMonitor.InnerProgressStatus
            pStatusMonitor.StopMonitor()
        End If

        Return lCreatedProjectFilenames
    End Function


    Public Sub EnsureCacheFolderSet(ByVal aDataPath As String, ByRef aCacheFolder As String)
        If String.IsNullOrEmpty(aCacheFolder) OrElse Not IO.Directory.Exists(aCacheFolder) Then
            aCacheFolder = aDataPath.TrimEnd(g_PathChar)
            Dim lDataPos As Integer = aCacheFolder.IndexOf(g_PathChar & "data" & g_PathChar)
            If lDataPos >= 0 Then
                aCacheFolder = IO.Path.Combine(aCacheFolder.Substring(0, lDataPos), "cache")
            Else
                If IsNumeric(IO.Path.GetFileName(aCacheFolder)) Then
                    aCacheFolder = IO.Path.GetDirectoryName(aCacheFolder)
                End If
                If IO.Directory.Exists(IO.Path.Combine(IO.Path.GetDirectoryName(aCacheFolder), "cache")) Then
                    aCacheFolder = IO.Path.Combine(IO.Path.GetDirectoryName(aCacheFolder), "cache")
                Else
                    aCacheFolder = IO.Path.Combine(aCacheFolder, "cache")
                End If
            End If
        End If
    End Sub

    'Public Sub CachingSSURGOSoils(ByVal aFolder As String, ByVal aSoils As List(Of D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil))
    '    Dim lSoilFileName As String
    '    Dim lSoilAttributes As New System.Text.StringBuilder()
    '    For Each lSoil As D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil In aSoils
    '        lSoilFileName = IO.Path.Combine(aFolder, lSoil.AreaSymbol & ".csv")
    '        If IO.File.Exists(lSoilFileName) Then TryDelete(lSoilFileName)
    '        Dim lSoilFile As New IO.StreamWriter(lSoilFileName, False)
    '        lSoilFile.WriteLine("MUID,SEQN,CMPPCT,S5ID,SNAM,NLAYERS,HYDGRP,SOL_ZMX,ANION_EXCL,SOL_CRK,TEXTURE,SOL_Z1,SOL_BD1,SOL_AWC1,SOL_K1,SOL_CBN1,CLAY1,SILT1,SAND1,ROCK1,SOL_ALB1,USLE_K1,SOL_EC1,SOL_Z2,SOL_BD2,SOL_AWC2,SOL_K2,SOL_CBN2,CLAY2,SILT2,SAND2,ROCK2,SOL_ALB2,USLE_K2,SOL_EC2,SOL_Z3,SOL_BD3,SOL_AWC3,SOL_K3,SOL_CBN3,CLAY3,SILT3,SAND3,ROCK3,SOL_ALB3,USLE_K3,SOL_EC3,SOL_Z4,SOL_BD4,SOL_AWC4,SOL_K4,SOL_CBN4,CLAY4,SILT4,SAND4,ROCK4,SOL_ALB4,USLE_K4,SOL_EC4,SOL_Z5,SOL_BD5,SOL_AWC5,SOL_K5,SOL_CBN5,CLAY5,SILT5,SAND5,ROCK5,SOL_ALB5,USLE_K5,SOL_EC5,SOL_Z6,SOL_BD6,SOL_AWC6,SOL_K6,SOL_CBN6,CLAY6,SILT6,SAND6,ROCK6,SOL_ALB6,USLE_K6,SOL_EC6,SOL_Z7,SOL_BD7,SOL_AWC7,SOL_K7,SOL_CBN7,CLAY7,SILT7,SAND7,ROCK7,SOL_ALB7,USLE_K7,SOL_EC7,SOL_Z8,SOL_BD8,SOL_AWC8,SOL_K8,SOL_CBN8,CLAY8,SILT8,SAND8,ROCK8,SOL_ALB8,USLE_K8,SOL_EC8,SOL_Z9,SOL_BD9,SOL_AWC9,SOL_K9,SOL_CBN9,CLAY9,SILT9,SAND9,ROCK9,SOL_ALB9,USLE_K9,SOL_EC9,SOL_Z10,SOL_BD10,SOL_AWC10,SOL_K10,SOL_CBN10,CLAY10,SILT10,SAND10,ROCK10,SOL_ALB10,USLE_K10,SOL_EC10,COMPNAME,SSURGOVER,MUKEY,COKEY,CHKEYS")
    '        lSoilAttributes.Clear()
    '        For I As Integer = 0 To lSoil.Components.Count - 1
    '            Dim lComp = lSoil.Components.Item(I)
    '            lSoilAttributes.Append(lSoil.AreaSymbol & "," & I + 1 & "," & lComp.comppct_r & "," & "" & "," & lSoil.AreaSymbol & lSoil.MuSym & "-" & (I + 1).ToString & ",")
    '            lSoilAttributes.Append(lComp.chorizons.Count & "," & lComp.HSG & "," & lComp.chorizons.Item(lComp.chorizons.Count - 1).hzdepb_r & "," & "0.5,0.5," & lComp.chorizons.Item(0).texture & ",")
    '            For J As Integer = 0 To lComp.chorizons.Count - 1
    '                Dim lHorizon = lComp.chorizons.Item(J)
    '                With lHorizon
    '                    lSoilAttributes.Append(.hzdepb_r & "," & .dbovendry_r & "," & .awc_r & "," & .ksat_r & "," & .om_r & "," & .claytotal_r & "," & .silttotal_r & "," & .sandtotal_r & "," & .rockpct & "," & lComp.albedodry_r & "," & .kffact & "," & .ec_r & ",")
    '                End With
    '            Next 'Horizon J
    '            For Z As Integer = 1 To 10 - lComp.chorizons.Count
    '                lSoilAttributes.Append("0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & "," & "0" & ",")
    '            Next
    '            lSoilAttributes.AppendLine(lComp.compname & "," & lComp.saversion & "," & lSoil.MuKey & "," & lComp.cokey & "," & "Blank")
    '            lSoilFile.WriteLine(lSoilAttributes.ToString)
    '        Next 'Comp I
    '        lSoilFile.Flush()
    '        lSoilFile.Close()
    '    Next 'lSoil
    'End Sub

    Private Sub LoadStatisticDefinitions()
        'tell atcData how to calculate basic statistics for timeseries
        Dim lStatistics As New atcTimeseriesStatistics.atcTimeseriesStatistics
        For Each lStatisticsOperation As atcData.atcDefinedValue In lStatistics.AvailableOperations
            atcData.atcDataAttributes.AddDefinition(lStatisticsOperation.Definition)
        Next
    End Sub

    Public Sub DownloadDataSetupModels(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters)
        'KW Sept. 10, 2014 - No need for this.  Project location specified at creation.
        'If IO.File.Exists(aProject.ProjectFolder) Then
        '    Throw New ApplicationException("File exists with same name as project folder: " & aProject.ProjectFolder)
        'ElseIf IO.Directory.Exists(aProject.ProjectFolder) AndAlso
        '      (IO.Directory.GetFiles(aProject.ProjectFolder).Length > 1 OrElse
        '       IO.Directory.GetDirectories(aProject.ProjectFolder).Length > 0) Then
        '    If aParameters.OverwriteProject Then
        '        Logger.Dbg("Parameters.OverwriteProject is True, so overwriting old project in: " & aProject.ProjectFolder)
        '        IO.Directory.Delete(aProject.ProjectFolder, True)
        '    Else
        '        Logger.Dbg("Parameters.OverwriteProject is False, so leaving old project in: " & aProject.ProjectFolder)
        '        Dim lNewFolder As String = GetTemporaryFileName(aProject.ProjectFolder, "")
        '        aProject.ProjectFilename = aProject.ProjectFilename.Replace(aProject.ProjectFolder, lNewFolder)
        '        aProject.ProjectFolder = lNewFolder
        '        Logger.Dbg("New project folder is now: " & aProject.ProjectFolder)
        '    End If
        'End If
        StartStatusMonitor()
        LoadStatisticDefinitions() 'atcTimeseriesStatistics.atcTimeseriesStatistics.InitializeShared()

        If atcData.atcDataManager.DataPlugins.Count = 0 Then
            atcData.atcDataManager.DataPlugins.Add(New atcWDM.atcDataSourceWDM)
        End If

        'Record log of project creation in project folder
        Logger.StartToFile(IO.Path.Combine(aProject.ProjectFolder, "D4EM-" _
                     & Format(Now, "yyyy-MM-dd") & "at" & Format(Now, "HH-mm") & ".log"), aForceNameChange:=True)
        Logger.Status("LABEL TITLE " & g_AppNameShort & " Status")
        Logger.Dbg("SDM Parameters: " & vbCrLf & aParameters.ToString)
        IO.File.WriteAllText(IO.Path.Combine(aProject.ProjectFolder, "SDMParameters.xml"), aParameters.XML)
        If aParameters.Projects.Count > 1 Then
            IO.File.WriteAllText(IO.Path.Combine(aProject.ProjectFolder, "SDMProject.xml"), aProject.XML)
        End If

        atcData.atcDataManager.Clear()
        'TODO: synchronize atcData.atcDataManager.DataSources with aProject.TimeseriesSources        

        GetEPAWaters(aProject)

        'Create a shapefile containing the project region shape
        Dim lFeatureSet As DotSpatial.Data.FeatureSet
        lFeatureSet = New DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Polygon)
        lFeatureSet.Projection = aProject.DesiredProjection
        Dim lDescription As String = aProject.Region.RegionSpecification.ToString
        Dim lNewField As New DotSpatial.Data.Field("Region", "C", lDescription.Length + 1, 0)
        lFeatureSet.DataTable.Columns.Add(lNewField)
        lFeatureSet.AddFeature(aProject.Region.ToShape(aProject.DesiredProjection).ToGeometry()).DataRow(0) = lDescription
        lFeatureSet.UpdateExtent()
        lFeatureSet.Filename = IO.Path.Combine(aProject.ProjectFolder, "aoi.shp")
        lFeatureSet.Save()
        aProject.Layers.Add(New D4EM.Data.Layer(lFeatureSet, New D4EM.Data.LayerSpecification("aoi", "Area of Interest", "aoi.shp", Role:=D4EM.Data.LayerSpecification.Roles.OtherBoundary)))
        IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

        Dim lHUC8s As Generic.List(Of String) = aProject.Region.GetKeys(D4EM.Data.Region.RegionTypes.huc8)

        'Do not try to model the Great Lakes themselves. NHDPlus does not include them. Probably the user was selecting land next to the lake.
        For Each lGreatLakeHuc8 In {"04020300", "04060200", "04080300", "04120200", "04150200"}
            If lHUC8s.Contains(lGreatLakeHuc8) Then lHUC8s.Remove(lGreatLakeHuc8) : Logger.Dbg("Skipping Great Lake " & lGreatLakeHuc8)
        Next

        If lHUC8s.Count > 4 Then
            Throw New ApplicationException("Too many HUC-8 (" & lHUC8s.Count & ") were specified. More than 4 are unlikely to complete successfully.")
        End If

        Dim lStep As Integer = 1
        Dim lLastStep As Integer = lHUC8s.Count 'BASINS Core
        If aParameters.BasinsMetConstituents.Count > 0 Then lLastStep += 2 'Met Stations, Data
        If aParameters.CatchmentsMethod = "NHDPlus" OrElse aParameters.ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid Then lLastStep += lHUC8s.Count
        If aParameters.NCDCconstituents.Count > 0 Then lLastStep += 1
        If aParameters.NLDASconstituents.Count > 0 Then lLastStep += 1
        If aParameters.SetupHSPF Then lLastStep += 1
        If aParameters.SetupSWAT Then lLastStep += 1
        lLastStep += aParameters.NASSYears.Count
        If aParameters.NASSStatistics Then lLastStep += aParameters.NASSYears.Count 'For each year also get statistics
        'If aParameters.HavePourPoint Then lLastStep += 1
        lLastStep += 1 'Always get NLCD Land Cover
        lLastStep += 1 'Always process network
        If aParameters.ElevationGrid.Source = GetType(D4EM.Data.Source.USGS_Seamless) Then lLastStep += 1
        If aParameters.CatchmentsMethod = "TauDEM5" Then lLastStep += 1
        If aParameters.SoilSource = "SSURGO" Then lLastStep += 1

        Dim lResults As String = ""

        Dim lOriginalFlowlinesLayer As D4EM.Data.Layer
        Dim lOriginalCatchmentsLayer As D4EM.Data.Layer
        Dim lSoilsLayer As D4EM.Data.Layer = Nothing
        Dim lSoils As List(Of D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil) = Nothing

        'GetBasinsMet(aProject, aParameters, lStep, lLastStep, lResults)

        For Each lHuc8 As String In lHUC8s
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting BASINS Core Layers " & lHuc8, True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
            Using lLevel As New ProgressLevel(False)
TryCore:        Try
                    CheckResult(lResults, D4EM.Data.Source.BASINS.GetBASINS(aProject, Nothing, lHuc8, D4EM.Data.Source.BASINS.LayerSpecifications.core31.all))
                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TryCore Else Throw ex
                End Try
            End Using

            If aParameters.CatchmentsMethod = "NHDPlus" OrElse aParameters.ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid Then
                Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NHDPlus " & lHuc8, True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
                Using lLevel As New ProgressLevel(False)
TryNHD:             Try 'Get both hydrography and elevation or only one
                        If aParameters.CatchmentsMethod = "NHDPlus" AndAlso aParameters.ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid Then
                            CheckResult(lResults,
                                        D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, "NHDPlus", lHuc8, True,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons,
                                                                            aParameters.ElevationGrid))
                        ElseIf aParameters.CatchmentsMethod = "NHDPlus" Then
                            CheckResult(lResults,
                                        D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, "NHDPlus", lHuc8, True,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody,
                                                                            D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons))
                        ElseIf aParameters.ElevationGrid = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid Then
                            CheckResult(lResults, D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, "NHDPlus", lHuc8, True, aParameters.ElevationGrid))
                        End If
                    Catch ex As ApplicationException
                        If ex.Message = "Retry Query" Then GoTo TryNHD Else Throw ex
                    End Try
                End Using
            End If
        Next
        IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

        GetNASS(aProject, aParameters, lStep, lLastStep, lResults)

        'GetNCDC(aProject, aParameters, lStep, lLastStep, lResults)

        'GetNLDAS(aProject, aParameters, lStep, lLastStep, lResults)

        GetSeamless(aProject, aParameters, lStep, lLastStep, lResults)

        EnsureGridProjectionsMatch(aProject)

        If aParameters.CatchmentsMethod = "TauDEM5" Then
            Delineate(aProject, aParameters, lStep, lLastStep, lResults)
        End If

        If aParameters.SoilSource = "SSURGO" Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting SSURGO Soils", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
            Using lLevel As New ProgressLevel(False)
                lSoils = D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(aProject, Nothing)
            End Using
            lSoilsLayer = aProject.LayerFromTag(D4EM.Data.Source.NRCS_Soil.SoilLocation.SoilLayerSpecification.Tag)
        Else
            lSoilsLayer = aProject.LayerFromTag(D4EM.Data.Source.BASINS.LayerSpecifications.core31.statsgo.Tag)
        End If

        If lResults IsNot Nothing AndAlso lResults.Length > 0 AndAlso lResults.Contains("<success") Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Processing Network", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1

            'process the network 
            IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

            'Three variables that will be assigned in ProcessNetwork
            Dim lSimplifiedFlowlinesLayer As D4EM.Data.Layer = Nothing
            Dim lSimplifiedCatchmentsLayer As D4EM.Data.Layer = Nothing
            Dim lFields As D4EM.Geo.NetworkOperations.FieldIndexes = Nothing

            Select Case aParameters.CatchmentsMethod
                Case "TauDEM5"
                    lOriginalFlowlinesLayer = aProject.LayerFromTag(TauDEMLayerSpecifications.TauDEMStream.Tag)
                    lOriginalCatchmentsLayer = aProject.LayerFromTag(TauDEMLayerSpecifications.TauDEMWatershed.Tag)
                    If aParameters.Catchments IsNot Nothing Then aParameters.Catchments.Clear()
                Case "NHDPlus"
                    lOriginalFlowlinesLayer = aProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline.Tag)
                    'pbd -- fixing situation where multiple catchment layers may exist in the project, need to get the one that corresponds to these flowlines
                    Dim lCatchmentFileName As String = PathNameOnly(lOriginalFlowlinesLayer.FileName) & "\..\drainage\" & D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons.FilePattern
                    'lOriginalCatchmentsLayer = aProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons.Tag)
                    lOriginalCatchmentsLayer = aProject.LayerFromFileName(IO.Path.GetFullPath(lCatchmentFileName))
                Case Else
                    Throw New ApplicationException("Unknown Catchements Method " & aParameters.CatchmentsMethod)
            End Select
            If lOriginalFlowlinesLayer Is Nothing Then Throw New ApplicationException("Original Flowlines not found before ProcessNetwork")
            If lOriginalCatchmentsLayer Is Nothing Then Throw New ApplicationException("Original Catchments not found before ProcessNetwork")
            Logger.Dbg("Flowlines found: " & lOriginalFlowlinesLayer.FileName)
            Logger.Dbg("Catchments found: " & lOriginalCatchmentsLayer.FileName)
            Using lLevel As New ProgressLevel(False)
                ProcessNetwork(aProject, aParameters.Catchments, aParameters.ClipCatchments,
                               aParameters.MinCatchmentKM2, aParameters.MinFlowlineKM,
                               lOriginalFlowlinesLayer, lOriginalCatchmentsLayer,
                               lSimplifiedFlowlinesLayer, lSimplifiedCatchmentsLayer, lFields, aParameters.BoundariesLatLongCsvFileName, aParameters.OutputsLatLongCsvFileName)
            End Using
            If lSimplifiedFlowlinesLayer Is Nothing Then Throw New ApplicationException("Simplified Flowlines not found after ProcessNetwork")
            If lSimplifiedCatchmentsLayer Is Nothing Then Throw New ApplicationException("Simplified Catchments not found after ProcessNetwork")
            IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

            Dim lScenarioName As String = "SDMProject"
            If lHUC8s.Count = 1 Then
                lScenarioName = lHUC8s(0)
                If aProject.Region.RegionSpecification = D4EM.Data.National.LayerSpecifications.huc12 Then
                    Dim lHUC12s As Generic.List(Of String) = aProject.Region.GetKeys(D4EM.Data.Region.RegionTypes.huc12)
                    If lHUC12s.Count = 1 Then
                        lScenarioName = lHUC12s(0)
                    End If
                End If
            End If

            If aParameters.NLDASconstituents.Count > 0 Then
                'GetBasinsMet(aProject, aParameters, Nothing, lStep, lLastStep, lResults) 'using the line below instead results in more BASINS met data available 
                GetBasinsMet(aProject, aParameters, lSimplifiedCatchmentsLayer, lStep, lLastStep, lResults)
                GetNLDAS(aProject, aParameters, lSimplifiedCatchmentsLayer, lStep, lLastStep, lResults)
            Else
                GetBasinsMet(aProject, aParameters, lSimplifiedCatchmentsLayer, lStep, lLastStep, lResults)
            End If
            GetNCDC(aProject, aParameters, lStep, lLastStep, lResults)
            IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

            'If aParameters.SetupHSPF Then
            '    'Dim lMetWDM As atcData.atcDataSource = aProject.TimeseriesSources.Item
            '    Logger.Status("Step " & lStep & " of " & lLastStep & ": Creating HSPF input sequence", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
            '    Using lLevel As New ProgressLevel(False)
            '        Dim lHSPFModel As New D4EM.Model.HSPF.HSPFmodel
            '        lHSPFModel.BuildHSPFInput(
            '            aProject:=aProject,
            '            aCatchmentsLayer:=lSimplifiedCatchmentsLayer,
            '            aFlowlinesLayer:=lSimplifiedFlowlinesLayer,
            '            aLandUseLayer:=aProject.LayerFromTag(D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover.Tag),
            '            aDemGridLayer:=aProject.LayerFromTag(aParameters.ElevationGrid.Tag), _
            '            aSoilsLayer:=lSoilsLayer,
            '            aMetWDM:=aProject.TimeseriesSources(0),
            '            aSimulationStartYear:=aParameters.SimulationStartYear,
            '            aSimulationEndYear:=aParameters.SimulationEndYear,
            '            aOutputInterval:=aParameters.HspfOutputInterval,
            '            aBaseOutputName:=lScenarioName,
            '            aWQConstituents:=aParameters.WQConstituents.ToArray(),
            '            aSnowOption:=aParameters.HspfSnowOption,
            '            aBacterialOption:=aParameters.HspfBacterialOption,
            '            aChemicalOption:=aParameters.HspfChemicalOption,
            '            aChemicalName:=aParameters.HspfChemicalName,
            '            aChemicalMaximumSolubility:=aParameters.HspfChemicalMaximumSolubility,
            '            aChemicalPartitionCoeff:=aParameters.HspfChemicalPartitionCoeff,
            '            aChemicalFreundlichExp:=aParameters.HspfChemicalFreundlichExp,
            '            aChemicalDegradationRate:=aParameters.HspfChemicalDegradationRate)
            '    End Using
            'End If
            If aParameters.SetupHSPF Then
                'Dim lMetWDM As atcData.atcDataSource = aProject.TimeseriesSources.Item
                Logger.Status("Step " & lStep & " of " & lLastStep & ": Creating HSPF input sequence", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
                Using lLevel As New ProgressLevel(False)
                    'pbd -- fixing situation where multiple elevation layers may exist in the project, need to get the one that corresponds to these flowlines
                    Dim lElevationFileName As String = IO.Path.GetFullPath(PathNameOnly(lOriginalFlowlinesLayer.FileName) & "\..\" & D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid.FilePattern)
                    Dim lHSPFModel As New D4EM.Model.HSPF.HSPFmodel
                    lHSPFModel.BuildHSPFInput(
                        aProject:=aProject,
                        aCatchmentsLayer:=lSimplifiedCatchmentsLayer,
                        aFlowlinesLayer:=lSimplifiedFlowlinesLayer,
                        aLandUseLayer:=aProject.LayerFromRole(D4EM.Data.LayerSpecification.Roles.LandUse),
                        aDemGridLayer:=aProject.LayerFromFileName(lElevationFileName), _
                        aSoilsLayer:=lSoilsLayer,
                        aMetWDM:=aProject.TimeseriesSources(0),
                        aSimulationStartYear:=aParameters.SimulationStartYear,
                        aSimulationEndYear:=aParameters.SimulationEndYear,
                        aOutputInterval:=aParameters.HspfOutputInterval,
                        aBaseOutputName:=lScenarioName,
                        aWQConstituents:=aParameters.WQConstituents.ToArray(),
                        aSnowOption:=aParameters.HspfSnowOption,
                        aBacterialOption:=aParameters.HspfBacterialOption,
                        aChemicalOption:=aParameters.HspfChemicalOption,
                        aChemicalName:=aParameters.HspfChemicalName,
                        aChemicalMaximumSolubility:=aParameters.HspfChemicalMaximumSolubility,
                        aChemicalPartitionCoeff:=aParameters.HspfChemicalPartitionCoeff,
                        aChemicalFreundlichExp:=aParameters.HspfChemicalFreundlichExp,
                        aChemicalDegradationRate:=aParameters.HspfChemicalDegradationRate)
                End Using
            End If

            'Dim aReportFlowUnits As Short = 0
            If aParameters.SetupSWAT Then
                Logger.Status("Step " & lStep & " of " & lLastStep & ": Creating SWAT input sequence", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
                Using lLevel As New ProgressLevel(False)
                    D4EM.Model.SWAT.SWATmodel.BuildSWATInput(
                        aScenarioName:=lScenarioName,
                        aProject:=aProject,
                        aCatchmentsLayer:=lSimplifiedCatchmentsLayer,
                        aFlowlinesLayer:=lSimplifiedFlowlinesLayer,
                        aLandUseLayer:=aProject.LayerFromTag(D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2011.LandCover.Tag),
                        aDemGridLayer:=aProject.LayerFromTag(aParameters.ElevationGrid.Tag),
                        aSoilsLayer:=lSoilsLayer,
                        aSoilProperties:=lSoils,
                        aCreateArcSWATFiles:=aParameters.CreateArcSWATFiles,
                        aBuildDatabase:=aParameters.BuildDatabase,
                        aSWATDatabaseName:=aParameters.SWATDatabaseName,
                        aGeoProcess:=aParameters.GeoProcess,
                        aResume:=aParameters.ResumeOverlay,
                        aAreaIgnoreBelowFraction:=aParameters.AreaIgnoreBelowFraction,
                        aAreaIgnoreBelowAbsolute:=aParameters.AreaIgnoreBelowAbsolute,
                        aLandUseIgnoreBelowFraction:=aParameters.LandUseIgnoreBelowFraction,
                        aLandUseIgnoreBelowAbsolute:=aParameters.LandUseIgnoreBelowAbsolute,
                        aParameterShapefileName:=aParameters.ParameterShapefileName,
                        aSimulationStartYear:=aParameters.SimulationStartYear,
                        aSimulationEndYear:=aParameters.SimulationEndYear,
                        aUseMgtCropFile:=aParameters.UseMgtCropFile,
                        aOutputSummarize:=aParameters.OutputSummarize,
                        aFields:=lFields,
                        aReportFlowUnits:=aParameters.ReportFlowUnits)
                    Dim lReturnCode As Integer = -1
                    Dim lInputFilePath As String = IO.Path.Combine(aProject.ProjectFolder, "Scenarios\" & lScenarioName & "\TxtInOut")
                    If aParameters.RunModel Then
                        lReturnCode = D4EM.Model.SWAT.SWATmodel.RunModel(lInputFilePath)
                    End If
                    If aParameters.OutputSummarize AndAlso lReturnCode = 0 Then
                        Logger.Status("PostProcessModelResults")
                        D4EM.Model.SWAT.SWATmodel.SummarizeOutput(aProject.ProjectFolder, lInputFilePath, lScenarioName)
                        Logger.Status("DoneOutputSummarize " & MemUsage())
                    End If

                End Using
            End If

            For Each lLocalShapeFileName As String In IO.Directory.GetFiles(IO.Path.Combine(aProject.ProjectFolder, "LocalData"), "*.shp")
                If aProject.LayerFromFileName(lLocalShapeFileName) Is Nothing Then
                    Dim lAddLayer As New D4EM.Data.Layer(lLocalShapeFileName, New D4EM.Data.LayerSpecification(Name:=IO.Path.GetFileNameWithoutExtension(lLocalShapeFileName)))
                    lAddLayer.Reproject(aProject.DesiredProjection)
                    aProject.Layers.Add(lAddLayer)
                End If
            Next

            'Run FAMoS
            If (True) Then
                'D4EM.Model.FAMoS.FAMoSTool.BuildFAMoSInput(aProject, lSimplifiedCatchmentsLayer, lSimplifiedFlowlinesLayer)
            End If

            IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
            For Each lLayer In aProject.Layers
                lLayer.Close()
            Next
        End If
    End Sub

    Private Sub GetEPAWaters(ByVal aProject As D4EM.Data.Project) ', ByRef lStep As Integer, ByVal lLastStep As Integer)
        If aProject.Region.RegionSpecification = D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed Then
            Logger.Status("Getting EPA Waters", True)
            Using lLevel As New ProgressLevel(False)
TryEPAWaters:
                Try
                    Dim lPourKeys = aProject.Region.GetKeys(D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                    Dim lLatitude = lPourKeys(0)
                    Dim lLongitude As Double = lPourKeys(1)
                    Dim lMaxKm As Double = lPourKeys(2)
                    Dim lPourPoint = D4EM.Data.Source.EPAWaters.GetPourPoint(aProject.CacheFolder, lLatitude, lLongitude)
                    Logger.Dbg("Getting EPA Waters Pourpoint Watershed", True)
                    Dim lWatershed = D4EM.Data.Source.EPAWaters.GetLayer(aProject, "EPAWaters", lPourPoint.COMID, lMaxKm, D4EM.Data.Source.EPAWaters.LayerSpecifications.PourpointWatershed)
                    aProject.Region = New D4EM.Data.Region(lWatershed, D4EM.Data.Region.MatchAllKeys)

                    'Dim lPourPoint = D4EM.Data.Source.EPAWaters.GetPourPoint(aParameters.Project.CacheFolder,
                    '                                                         aParameters.PourPointLatitude,
                    '                                                         aParameters.PourPointLongitude)
                    Logger.Dbg("Getting EPA Waters Flowline", True)
                    Dim lFlowline = D4EM.Data.Source.EPAWaters.GetLayer(aProject, "EPAWaters", lPourPoint.COMID, lMaxKm,
                                                                        D4EM.Data.Source.EPAWaters.LayerSpecifications.Flowline)
                    Logger.Dbg("Getting EPA Waters Catchment", True)
                    Dim lCatchment = D4EM.Data.Source.EPAWaters.GetLayer(aProject, "EPAWaters", lPourPoint.COMID, lMaxKm,
                                                                         D4EM.Data.Source.EPAWaters.LayerSpecifications.Catchment)
                    IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TryEPAWaters Else Throw ex
                End Try
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Get BASINS met station(s) and data
    ''' </summary>
    ''' <param name="aProject"></param>
    ''' <param name="aParameters"></param>
    ''' <param name="aCatchmentsLayer">
    ''' If closest met station to each catchment is desired, specify this Layer.
    ''' If Nothing, then one met station will be found for entire project.</param>
    ''' <param name="lStep">Current step in overall progress</param>
    ''' <param name="lLastStep">Last step in overall progress</param>
    ''' <param name="lResults">XML results string, appended to as layers and data are added</param>
    ''' <remarks></remarks>
    Public Sub GetBasinsMet(ByVal aProject As D4EM.Data.Project, _
                            ByVal aParameters As SDMParameters, _
                            ByVal aCatchmentsLayer As D4EM.Data.Layer, _
                            ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        If aParameters.BasinsMetConstituents.Count > 0 Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting BASINS Met Stations", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
TryBasinsMetStation:
            Dim lMetStationIDs As New Generic.List(Of String)
            Try
                'Temporarily change region to specify Closest for getting a met station
                'Dim lSaveSpec As D4EM.Data.LayerSpecification = aProject.Region.RegionSpecification
                'aProject.Region.RegionSpecification = D4EM.Data.Region.RegionTypes.closest
                CheckResult(lResults, D4EM.Data.Source.BASINS.GetMetStations(aProject, lMetStationIDs, True, Nothing, aCatchmentsLayer, "closest", _
                                          New Date(aParameters.SimulationStartYear, 1, 1), _
                                          New Date(aParameters.SimulationEndYear, 12, 31)))
                'aProject.Region.RegionSpecification = lSaveSpec
                IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
            Catch ex As ApplicationException
                If ex.Message = "Retry Query" Then GoTo TryBasinsMetStation Else Throw ex
            End Try

            If lMetStationIDs.Count > 0 Then
                Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting BASINS Met Data", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
TryBasinsMetData:
                Try
                    CheckResult(lResults, D4EM.Data.Source.BASINS.GetMetData(aProject, lMetStationIDs,
                                IO.Path.Combine(aProject.ProjectFolder, "met" & g_PathChar & "met.wdm")))
                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TryBasinsMetData Else Throw ex
                End Try
            Else
                Logger.Dbg("No met stations, not getting BASINS met data") : lStep += 1
            End If
        End If
    End Sub

    Private Sub GetNCDC(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        If aParameters.NCDCconstituents.Count > 0 Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NCDC Met Data", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
            Using lLevel As New ProgressLevel(False)
                ''            Dim lNCDCStationIDs As New Generic.List(Of String)
                ''TryNCDCStation: Try
                ''                'Temporarily change region to specify Closest for getting a met station
                ''                Dim lSaveSpec As D4EM.Data.LayerSpecification = aProject.Region.RegionSpecification
                ''                aProject.Region.RegionSpecification = D4EM.Data.Region.RegionTypes.closest
                ''                CheckResult(lResults, D4EM.Data.Source.NCDC.GetSites(aProject, lNCDCStationIDs,
                ''                                                                     IO.Path.Combine(aProject.ProjectFolder, "NCDC", "NCDC_ish.shp"),
                ''                                                                     D4EM.Data.Source.NCDC.LayerSpecifications.ISH))
                ''                aProject.Region.RegionSpecification = lSaveSpec

                ''                If lNCDCStationIDs.Count > 0 Then
                ''                    For Each lNCDCStationID As String In lNCDCStationIDs
                ''                        For Each lNCDCconstituent As String In aParameters.NCDCconstituents
                ''                Next
                ''            Next
                ''    IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
                ''End If
                ''    Catch ex As ApplicationException
                ''    If ex.Message = "Retry Query" Then GoTo TryNCDCStation Else Throw ex
                ''End Try
TryNCDCvalues:
                Try
                    Dim lMetDataFolder As String = IO.Path.Combine(aProject.ProjectFolder, "met")
                    Dim lDestinationWDMfilename As String = IO.Path.Combine(lMetDataFolder, "met.wdm")
                    Dim lNumNCDCSites As Integer = 3
                    CheckResult(lResults, D4EM.Data.Source.NCDC.GetClosestSitesWithData(aProject,
                                          D4EM.Data.Source.NCDC.LayerSpecifications.ISH,
                                          aParameters.NCDCconstituents,
                                          New Date(aParameters.SimulationStartYear, 1, 1),
                                          New Date(aParameters.SimulationEndYear, 12, 31), lNumNCDCSites))

                    'Read in the NCDC data
                    Logger.Status("Reading NCDC data")
                    Dim lRawDataGroup As atcTimeseriesGroup = D4EM.Data.MetCmp.ReadData(lMetDataFolder, 1)

                    Dim lPREC As atcTimeseries = Nothing
                    Dim lATEM As atcTimeseries = Nothing
                    Dim lSOLR As atcTimeseries = Nothing
                    Dim lWIND As atcTimeseries = Nothing
                    Dim lPEVT As atcTimeseries = Nothing
                    Dim lDEWP As atcTimeseries = Nothing
                    Dim lCLOU As atcTimeseries = Nothing

                    'Process NCDC data: fill missing, generate constistuents, etc.
                    Logger.Status("Processing NCDC data")
                    D4EM.Data.MetCmp.MetDataProcess(lRawDataGroup, lMetDataFolder, lPREC, lATEM, lWIND, lSOLR, lPEVT, lDEWP, lCLOU)

                    aProject.TimeseriesSources.Add(D4EM.Model.HSPF.CreateMetWDM(lDestinationWDMfilename, lPREC, lATEM, lWIND, lSOLR, lPEVT, lDEWP, lCLOU))

                    'Dim lDestinationWDM As New atcWDM.atcDataSourceWDM
                    'If Not lDestinationWDM.Open(lDestinationWDMfilename) Then
                    '    lResults &= "<message>NCDC Meterologic: Could not open or create WDM file '" & lDestinationWDMfilename & "'</message>"
                    'Else
                    '    Dim lNCDCMetDataSources As New Generic.List(Of atcData.atcTimeseriesSource)
                    '    For Each lDataSource As atcData.atcTimeseriesSource In aProject.TimeseriesSources
                    '        If lDataSource.Name = "Timeseries::NCDC" Then
                    '            lNCDCMetDataSources.Add(lDataSource)
                    '        End If
                    '    Next
                    '    'Add NCDC data to WDM file for later processing as though it was BASINS met data
                    '    For Each lDataSource As atcData.atcTimeseriesSource In lNCDCMetDataSources
                    '        aProject.TimeseriesSources.Remove(lDataSource)
                    '        Dim lDataSet = lDataSource.DataSets(0)
                    '        Dim lConstituent As String = lDataSet.Attributes.GetValue("Constituent").ToString.ToUpper
                    '        Dim lDSN As Integer = 0
                    '        Select Case lConstituent
                    '            Case "AA1", "PREC" : lDSN = 1
                    '            Case "TMP", "ATEM" : lDSN = 3
                    '            Case "WND", "WIND" : lDSN = 4
                    '            Case "DEW", "DEWP" : lDSN = 7
                    '            Case "GF1", "SKYCOND", "CLOU" : lDSN = 8 'TODO: are these the same or do we need to generate CLOU and maybe others from GF1/SKYCOND?
                    '            Case Else
                    '        End Select
                    '        If lDSN > 0 Then
                    '            lDataSet.Attributes.SetValue("id", lDSN)
                    '            If lDestinationWDM.AddDataSet(lDataSet, atcData.atcDataSource.EnumExistAction.ExistNoAction) Then
                    '                Logger.Dbg("NCDC Meterologic: Added " & lConstituent & " from " & lDataSource.Specification & " as DSN " & lDSN)
                    '            Else
                    '                Logger.Msg("Failed to add " & lConstituent & " from " & lDataSource.Specification & " as DSN " & lDSN, "NCDC Meterologic")
                    '            End If
                    '        Else
                    '            Logger.Dbg("Skipped NCDC dataset with unknown constituent: " & lConstituent & " from " & lDataSource.Specification)
                    '        End If
                    '    Next
                    '    aProject.TimeseriesSources.Add(lDestinationWDM)
                    'End If

                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TryNCDCvalues Else Throw ex
                End Try
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Get NLDAS precipitation data and incorporate it into a met data WDM file
    ''' </summary>
    ''' <param name="aProject">aProject.ProjectFolder is used to locate met\met.wdm, aProject.Region is used to determine which NLDAS grid location to access.</param>
    ''' <param name="aParameters">aParameters.NLDASconstituents determines whether any NLDAS data was requested, aParameters.SimulationStart/End Year determine date range of NLDAS data to get.</param>
    ''' <param name="lStep">which overall step we are on for progress indication</param>
    ''' <param name="lLastStep">last overall step for progress indication</param>
    ''' <param name="lResults">XML description of success or failure is appended</param>
    ''' <remarks>
    ''' 1. Start with a complete met data WDM containing all needed constituents
    ''' 2. Identify an NLDAS grid cell in the project area
    ''' 3. Download NLDAS precipitation data for that cell into into the met WDM file
    ''' 4. Set attributes so this precipitation data will be used by the HSPF model that is built by SDM Project Builder
    ''' </remarks>
    Public Sub GetNLDAS(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, _
                        ByVal aCatchmentsLayer As D4EM.Data.Layer, _
                        ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        If aParameters.NLDASconstituents.Count > 0 Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NLDAS Met Data", True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
            Using lLevel As New ProgressLevel(False)
TryGetvalues:
                Try
                    Dim lMetDataFolder As String = IO.Path.Combine(aProject.ProjectFolder, "met")
                    Dim lDestinationWDMfilename As String = IO.Path.Combine(lMetDataFolder, "met.wdm")
                    Dim lAllNLDAScells As New Generic.List(Of D4EM.Data.Source.NLDAS.NLDASGridCoords)

                    If aCatchmentsLayer Is Nothing Then
                        Dim lAllInRegion = D4EM.Data.Source.NLDAS.GetGridCellsInRegion(aProject.Region)
                        'Use the middle cell.                        
                        lAllNLDAScells.Add(lAllInRegion(Math.Floor(lAllNLDAScells.Count / 2)))
                    Else 'Use NLDAS grid cell closest to each catchment
                        Dim lMetStationFieldIndex As Integer = D4EM.Geo.NetworkOperations.FieldIndexes.EnsureFieldExists(aCatchmentsLayer.AsFeatureSet, "ModelSeg", GetType(String))
                        Dim lNorth As Double, lSouth As Double, lEast As Double, lWest As Double

                        For Each lCatchment As DotSpatial.Data.Feature In aCatchmentsLayer.AsFeatureSet.Features
                            Dim lCatchmentID As String = lCatchment.DataRow(aCatchmentsLayer.IdFieldIndex)
                            Logger.Dbg("Finding NLDAS for catchment " & lCatchmentID)
                            Dim lCatchmentRegion As New D4EM.Data.Region(aCatchmentsLayer, lCatchmentID)
                            lCatchmentRegion.GetBounds(lNorth, lSouth, lWest, lEast, D4EM.Data.Globals.GeographicProjection)
                            Dim lCenterLat As Double = (lNorth + lSouth) / 2
                            Dim lCenterLon As Double = (lEast + lWest) / 2

                            Dim lCatchmentGridCoords As D4EM.Data.Source.NLDAS.NLDASGridCoords = D4EM.Data.Source.NLDAS.GetGridCellFromLatLon(lCenterLat, lCenterLon)
                            Dim lNeedToAdd As Boolean = True
                            For Each lSearchCoords In lAllNLDAScells
                                If lSearchCoords.Equals(lCatchmentGridCoords) Then
                                    lNeedToAdd = False
                                    Exit For
                                End If
                            Next
                            If lNeedToAdd Then
                                lAllNLDAScells.Add(lCatchmentGridCoords)
                            End If
                            lCatchment.DataRow(lMetStationFieldIndex) = lCatchmentGridCoords.ToString
                        Next
                    End If

                    CheckResult(lResults, D4EM.Data.Source.NLDAS.MakeStationShapefile(aProject, IO.Path.Combine(lMetDataFolder, "NLDAS_Grid.shp"), D4EM.Data.Source.NLDAS.LayerSpecifications.GridSquares, lAllNLDAScells))
                    CheckResult(lResults, D4EM.Data.Source.NLDAS.MakeStationShapefile(aProject, IO.Path.Combine(lMetDataFolder, "NLDAS_Grid_Center.shp"), D4EM.Data.Source.NLDAS.LayerSpecifications.GridPoints, lAllNLDAScells))

                    CheckResult(lResults, D4EM.Data.Source.NLDAS.GetParameter(aProject, lMetDataFolder,
                                          lAllNLDAScells, ,
                                          New Date(aParameters.SimulationStartYear, 1, 1, 0, 0, 0),
                                          New Date(aParameters.SimulationEndYear + 1, 1, 2, 0, 0, 0), lDestinationWDMfilename, aParameters.TimeZoneShift))
                    'precip has now been added to met WDM

                    'pbd added new code to get other NLDAS data types
                    Dim AdditionalParameters() As String = {"PEVAPsfc", "TMP2m", "UGRD10m", "VGRD10m", "DSWRFsfc", "SPFH2m"} 'these are required to produce the HSPF constituents
                    'Dim AdditionalParameters() As String = {"EVPsfc", "TMP2m", "UGRD10m", "VGRD10m", "DSWRFsfc", "SPFH2m"} 'these are required to produce the HSPF constituents
                    Dim lDataTypes() As String = AdditionalParameters
                    For Each lDataType In lDataTypes
                        lResults &= D4EM.Data.Source.NLDAS.GetParameter(aProject, lMetDataFolder,
                                          lAllNLDAScells, lDataType, New Date(aParameters.SimulationStartYear, 1, 1, 0, 0, 0),
                                          New Date(aParameters.SimulationEndYear + 1, 1, 2, 0, 0, 0), lDestinationWDMfilename, aParameters.TimeZoneShift) & vbCrLf
                    Next

                    'modify WDM so NLDAS precip will be used instead of BASINS / NCDC precip
                    'Dim lWDM = atcDataManager.DataSourceBySpecification(lDestinationWDMfilename)
                    'If lWDM IsNot Nothing Then
                    '    Dim lAllPrecip = lWDM.DataSets.FindData("Constituent", "PREC")
                    '    Dim lOriginalPrecip = lAllPrecip(0)
                    '    Dim lNLDASPrecip = lWDM.DataSets.FindData("Scenario", "NLDAS")(0)
                    '    For Each lAttributeName In {"Scenario", "Location", "Stanam"}
                    '        lNLDASPrecip.Attributes.SetValue(lAttributeName, lOriginalPrecip.Attributes.GetValue(lAttributeName))
                    '    Next
                    '    For Each lPrecip As atcTimeseries In lAllPrecip
                    '        If lPrecip.Serial <> lNLDASPrecip.Serial Then 'Make sure original datasets do not get used when building model
                    '            lPrecip.Attributes.SetValue("Scenario", "Replaced")
                    '            lPrecip.Attributes.SetValue("Constituent", "Replaced")
                    '        End If
                    '    Next
                    'End If

                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TryGetvalues Else Throw ex
                End Try
            End Using
        End If
    End Sub

    Private Sub GetNASS(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        If aParameters.NASSYears.Count > 0 Then
            For Each lYear In aParameters.NASSYears
                Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NASS raster " & lYear, True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
                Using lLevel As New ProgressLevel(False)
TryNASS:            Try
                        CheckResult(lResults, D4EM.Data.Source.NASS.getRaster(aProject, "Crop", "", CInt(lYear)))
                    Catch ex As ApplicationException
                        If ex.Message = "Retry Query" Then GoTo TryNASS Else Throw ex
                    End Try
                End Using
                If aParameters.NASSStatistics Then
                    Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NASS Statistics " & lYear, True) : lStep += 1 ', lStep, lLastStep) : lStep += 1
                    Using lLevel As New ProgressLevel(False)
TryNASStats:            Try
                            CheckResult(lResults, D4EM.Data.Source.NASS.getStatistics(aProject, "Crop", lYear))
                        Catch ex As ApplicationException
                            If ex.Message = "Retry Query" Then GoTo TryNASStats Else Throw ex
                        End Try
                    End Using
                End If
            Next
            IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
        End If
    End Sub

    Private Sub GetSeamless(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting NLCD Land Cover", True) : lStep += 1
        Using lLevel As New ProgressLevel(False)
TryNLCD:    Try
                'CheckResult(lResults, D4EM.Data.Source.USGS_Seamless.Execute(aProject, "NLCD", D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover))
                CheckResult(lResults, D4EM.Data.Source.USGS_Seamless.GetNLCD(aProject, "NLCD",
                                                                             aProject.DesiredProjection,
                                                                             aProject.ProjectFolder,
                                                                             aProject.CacheFolder,
                                                                             aProject.Region,
                                                                             True,
                                                                             D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2011.LandCover))
                'D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover))
                IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)

            Catch ex As ApplicationException
                If ex.Message = "Retry Query" Then GoTo TryNLCD Else Throw ex
            End Try
        End Using
        If aParameters.ElevationGrid.Source = GetType(D4EM.Data.Source.USGS_Seamless) Then
            Logger.Status("Step " & lStep & " of " & lLastStep & ": Getting Seamless " & aParameters.ElevationGrid.Tag, True) : lStep += 1
            Using lLevel As New ProgressLevel(False)
TrySeamlessElevation:
                Try
                    'CheckResult(lResults, D4EM.Data.Source.USGS_Seamless.Execute(aProject, "Elevation", aParameters.ElevationGrid))
                    CheckResult(lResults, D4EM.Data.Source.USGS_Seamless.GetNLCD(aProject, "Elevation",
                                                                             aProject.DesiredProjection,
                                                                             aProject.ProjectFolder,
                                                                             aProject.CacheFolder,
                                                                             aProject.Region,
                                                                             True,
                                                                             D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneArcSecond))
                    IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
                Catch ex As ApplicationException
                    If ex.Message = "Retry Query" Then GoTo TrySeamlessElevation Else Throw ex
                End Try
            End Using
        End If
    End Sub


    Private Sub EnsureGridProjectionsMatch(ByVal aProject As D4EM.Data.Project)
        Dim lReferenceGrid As D4EM.Data.Layer = Nothing
        Dim lNeedProjecting As New Generic.List(Of D4EM.Data.Layer)
        For Each lLayer As D4EM.Data.Layer In aProject.Layers
            If Not lLayer.IsFeatureSet Then
                If lLayer.Projection.ToProj4String.Equals(aProject.DesiredProjection.ToProj4String) OrElse lLayer.Projection.Matches(aProject.DesiredProjection) Then
                    If lReferenceGrid Is Nothing OrElse lLayer.Specification.Role = D4EM.Data.LayerSpecification.Roles.LandUse Then
                        'pbd -- fixing situation where multiple elevation layers may exist in the project in different HUC8s, need to get the one that corresponds to these flowlines
                        If lLayer.Specification.Role = D4EM.Data.LayerSpecification.Roles.Elevation Then
                            'may not want this one, see if it has flowlines which will indicate if this is the HUC8 of interest
                            If IO.File.Exists(IO.Path.GetFullPath(PathNameOnly(lLayer.FileName) & "\hydrography\nhdflowline.shp")) Then
                                lReferenceGrid = lLayer
                            End If
                        Else
                            lReferenceGrid = lLayer
                        End If
                    End If
                Else
                    lNeedProjecting.Add(lLayer)
                End If
            End If
        Next
        If lReferenceGrid IsNot Nothing Then
            For Each lLayer As D4EM.Data.Layer In lNeedProjecting
                aProject.Layers.Remove(lLayer)
                aProject.Layers.Add(D4EM.Geo.OverlayReclassify.Resample(lLayer, lReferenceGrid, IO.Path.ChangeExtension(lLayer.FileName, ".proj" & IO.Path.GetExtension(lLayer.FileName))))
            Next
        End If
    End Sub

#Region "TauDEM Delineation"

    Public Class TauDEMLayerSpecifications
        Public Shared TauDEMStream As New D4EM.Data.LayerSpecification(Name:="TauDEM Flowline", FilePattern:="Stream.shp", Tag:="taudemstream", Role:=D4EM.Data.LayerSpecification.Roles.Hydrography, IdFieldName:="LINKNO")
        Public Shared TauDEMWatershed As New D4EM.Data.LayerSpecification(Name:="TauDEM Watershed", FilePattern:="Watershed.shp", Tag:="taudemwatershed", Role:=D4EM.Data.LayerSpecification.Roles.SubBasin, IdFieldName:="Watershed")
    End Class

    Private Sub Delineate(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
        Logger.Status("Step " & lStep & " of " & lLastStep & ": Delineating watersheds", True) : lStep += 1
        Using lLevel As New ProgressLevel(False)
TryDelineate: Try
                Dim ProgressHandler As DotSpatial.Data.IProgressHandler = Nothing
                Dim ElevationPath As String = aProject.LayerFromRole(D4EM.Data.LayerSpecification.Roles.Elevation).FileName
                Dim lExt As String = IO.Path.GetExtension(ElevationPath)
                Dim elevUnits = MapWinGeoProc.Hydrology.ElevationUnits.centimeters
                Dim FilledPath As String = IO.Path.ChangeExtension(ElevationPath, ".Filled" & lExt)
                Dim D8Path As String = IO.Path.ChangeExtension(ElevationPath, ".D8" & lExt)
                Dim D8SlopePath As String = IO.Path.ChangeExtension(ElevationPath, ".D8Slope" & lExt)
                Dim AreaD8Path As String = IO.Path.ChangeExtension(ElevationPath, ".AreaD8" & lExt)

                Dim WatershedShapefilename As String = IO.Path.GetDirectoryName(ElevationPath) & g_PathChar & "Watershed.shp"

                Dim numProcesses As Integer = 8
                Dim ShowTaudemOutput As Boolean = False
                Dim StrahlOrdResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".StrahlOrd" & lExt)
                Dim LongestUpslopeResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".LongestUpslope" & lExt)
                Dim TotalUpslopeResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".TotalUpslope" & lExt)
                Dim StreamGridResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".StreamGrid" & lExt)
                Dim StreamOrdResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".StreamOrd" & lExt)
                Dim TreeDatResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".TreeDat.txt")
                Dim CoordDatResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".CoordDat.txt")
                Dim StreamShapeResultPath As String = IO.Path.GetDirectoryName(ElevationPath) & g_PathChar & "Stream.shp"
                Dim WatershedGridResultPath As String = IO.Path.ChangeExtension(ElevationPath, ".Watershed" & lExt)
                'TODO: determine whether we should use MinCatchmentKM2 to determine a stream threshold value
                'Dim Threshold As Integer = aParameters.MinCatchmentKM2 / 0.0009 'Note: Assumes a 30m elevation grid 
                Dim Threshold As Integer = 500
                Dim UseOutlets As Boolean = False
                Dim UseEdgeContamCheck As Boolean = False
                Dim OutletsPath As String = String.Empty ' IO.Path.ChangeExtension(ElevationPath, ".Outlets.shp")

                'runPreprocessing
                'If Not runMask() Then runFormCleanup() : Return False

                Logger.Status("Filling Pits", True)
                MapWinGeoProc.Hydrology.Fill(ElevationPath, FilledPath, ProgressHandler)

                Logger.Status("D8 Grid", True)
                MapWinGeoProc.Hydrology.D8(FilledPath, D8Path, D8SlopePath, numProcesses, ShowTaudemOutput, ProgressHandler)

                'runDelinByThresh
                Dim EdgeContCheck As Boolean = False
                Logger.Status("Area D8", True)
                If MapWinGeoProc.Hydrology.AreaD8(D8Path, Nothing, AreaD8Path, UseOutlets, EdgeContCheck, numProcesses, ShowTaudemOutput, ProgressHandler) <> 0 Then
                    Throw New ApplicationException("Unable to compute Area D8 grid")
                End If

                ' i = MapWinGeoProc.Hydrology.AreaDInf(tdbFileList.ang, tdbFileList.outletshpfile, tdbFileList.sca, tdbChoiceList.useOutlets, tdbChoiceList.EdgeContCheck, tdbChoiceList.numProcesses, tdbChoiceList.ShowTaudemOutput, myWrapper)

                'runDefineStreamGrids
                Logger.Status("Delineate Stream Grid", True)
                If MapWinGeoProc.Hydrology.DelinStreamGrids(ElevationPath, FilledPath, D8Path, D8SlopePath, AreaD8Path, OutletsPath,
                                                            StrahlOrdResultPath, LongestUpslopeResultPath, TotalUpslopeResultPath,
                                                            StreamGridResultPath, StreamOrdResultPath, TreeDatResultPath, CoordDatResultPath, StreamShapeResultPath, WatershedGridResultPath,
                                                            Threshold, UseOutlets, UseEdgeContamCheck, numProcesses, ShowTaudemOutput, ProgressHandler) <> 0 Then
                    Throw New ApplicationException("Unable to delineate stream grid")
                End If

                'runWshedToShape()
                Logger.Status("Manhattan Shapes", True)
                Dim ms As New DotSpatial.Plugins.Taudem.Port.ManhattanShapes(WatershedGridResultPath)
                Dim sf As DotSpatial.Data.FeatureSet = ms.GridToShapeManhattan("Watershed", "AREA")
                D4EM.Data.Globals.RepairAlbers(sf.Projection)
                sf.SaveAs(WatershedShapefilename, True)
                'Make sure stream shapefile has the same projection (replace bad projection file for default NED from Seamless)
                TryCopy(FilenameSetExt(WatershedShapefilename, ".prj"), FilenameSetExt(StreamShapeResultPath, ".prj"))
                If Not sf.Projection.Matches(aProject.DesiredProjection) Then 'Reproject watershed and stream shapes
                    sf.Reproject(aProject.DesiredProjection)
                    sf.SaveAs(WatershedShapefilename, True)
                    sf.Close()

                    sf = DotSpatial.Data.DataManager.DefaultDataManager.OpenFile(StreamShapeResultPath)
                    sf.Reproject(aProject.DesiredProjection)
                    sf.SaveAs(StreamShapeResultPath, True)
                    sf.Close()
                End If

                'runApplyStreamAttributes
                Logger.Status("Apply Stream Attributes", True)
                If Not MapWinGeoProc.Hydrology.ApplyStreamAttributes(StreamShapeResultPath, ElevationPath, WatershedShapefilename, elevUnits, ProgressHandler) Then
                    Throw New ApplicationException("Unable to apply stream attributes")
                End If

                'The following lines are commented out in DotSpatial.Plugins.Taudem.Port frmAutomatic_v3.runOutletsAndFinish
                'TODO: figure out whether we want/need this functionality
                'runApplyWatershedAttributes

                If MapWinGeoProc.Hydrology.ApplyWatershedLinkAttributes(WatershedShapefilename, StreamShapeResultPath, ProgressHandler) <> 0 Then
                    Throw New ApplicationException("Unable to apply watershed attributes")
                End If
                'If CalcSpecialWshedFields Then
                'If Not MapWinGeoProc.Hydrology.ApplyWatershedAreaAttributes(WatershedShapefilename, ProgressHandler) Then
                '    Throw New ApplicationException("Unable to apply watershed area attributes")
                'End If
                'If Not MapWinGeoProc.Hydrology.ApplyWatershedSlopeAttribute(WatershedGridResultPath, WatershedShapefilename, D8SlopePath, elevUnits, ProgressHandler) Then
                '    Throw New ApplicationException("Unable to apply watershed slope attributes")
                'End If
                'End If 'CalcSpecialWshedFields
                'runBuildJoinedBasins() 
                'Dim joinBasinShapeResultPath As String = Nothing
                'Logger.Status("Build Joined Basins", True)
                'MapWinGeoProc.Hydrology.BuildJoinedBasins(WatershedShapefilename, OutletsPath, joinBasinShapeResultPath, ProgressHandler)
                'runApplyJoinBasinAttributes()
                'runApplyJoinBasinAttributes = MapWinGeoProc.Hydrology.ApplyJoinBasinAreaAttributes(joinBasinShapeResultPath, elevUnits, ProgressHandler)
                'runApplyJoinBasinAttributes = MapWinGeoProc.Hydrology.ApplyWatershedSlopeAttribute(WatershedGridResultPath, joinBasinShapeResultPath, D8SlopePath, elevUnits, ProgressHandler)
                'runApplyJoinBasinAttributes = MapWinGeoProc.Hydrology.ApplyWatershedElevationAttribute(WatershedGridResultPath, joinBasinShapeResultPath, FilledPath, ProgressHandler)
                'runApplyJoinBasinAttributes = MapWinGeoProc.Hydrology.ApplyJoinBasinStreamAttributes(CoordDatResultPath, WatershedGridResultPath, joinBasinShapeResultPath, ProgressHandler)

                aProject.Layers.Add(New D4EM.Data.Layer(StreamShapeResultPath, TauDEMLayerSpecifications.TauDEMStream, False))
                aProject.Layers.Add(New D4EM.Data.Layer(WatershedShapefilename, TauDEMLayerSpecifications.TauDEMWatershed, False))

                IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ)
            Catch ex As ApplicationException
                If ex.Message = "Retry Query" Then GoTo TryDelineate Else Throw New ApplicationException("Automatic Watershed Delineation Error", ex)
            End Try
        End Using
    End Sub

#End Region

    Private Sub CheckResult(ByRef aAllResults As String, ByVal aThisResult As String)
        If aThisResult.Contains("<error>") Then
            Select Case Logger.Msg(aThisResult.Replace("<error>", "").Replace("</error>", "") & vbCrLf _
                                   & vbCrLf _
                                   & "Abort building this project" & vbCrLf _
                                   & "Retry this download" & vbCrLf _
                                   & "or Ignore the error and continue to build an incomplete project", MsgBoxStyle.AbortRetryIgnore, "Download Error")
                Case MsgBoxResult.Abort
                    Logger.Progress("", 0, 0)
                    aAllResults = Nothing
                    Throw New ApplicationException("Aborted after error")
                Case MsgBoxResult.Retry
                    Throw New ApplicationException("Retry Query")
            End Select
        Else
            aAllResults &= aThisResult
        End If
    End Sub

End Module
