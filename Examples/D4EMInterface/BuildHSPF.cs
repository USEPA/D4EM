using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using atcData;

namespace D4EMInterface
{
    //similar to modDownloadD4EM from SDMProjectBuilder solution
    class BuildHSPF
    {
        //as far as i know this just returns the created region
        public D4EM.Data.Region GetRegion(HSPFParameters aParameters)
        {
            
            int lStartPos = aParameters.SelectionLayer.IndexOf("<region>");
            D4EM.Data.Region lRegion = null;
            if (lStartPos > -1)
            {
                int lEndPos = aParameters.SelectionLayer.IndexOf("</region>", lStartPos);
                lRegion = new D4EM.Data.Region(aParameters.SelectionLayer.Substring(lStartPos, lEndPos - lStartPos + 9));
            }
            return lRegion;
        }

        //creates and returns project
        public D4EM.Data.Project CreateNewProject(D4EM.Data.Region aRegion, HSPFParameters aParameters)
        {
            string lProjectFileName = NewProjectFileName(aRegion, aParameters);
            D4EM.Data.Project lProject = new D4EM.Data.Project(aParameters.DesiredProjection,
                                                                aParameters.CacheFolder,
                                                                System.IO.Path.GetDirectoryName(lProjectFileName),
                                                                aRegion, true, false);
            lProject.ProjectFilename = lProjectFileName;
            return lProject;
        }

        //creates project file (.mwprj) and folder for huc (i think)
        //gets called by CreateNewProject method
        private string NewProjectFileName(D4EM.Data.Region aRegion, HSPFParameters aParameters)
        {
            string lProjectFileFullPath = aParameters.ProjectFileFullPath;
            string lProjectsPath;

            if (string.IsNullOrEmpty(lProjectFileFullPath))
            {
                lProjectsPath = aParameters.ProjectsPath;
                string lDefDirName = "NewProject";

                if (aRegion != null)
                {
                    List<string> lRegionKeys = aRegion.GetKeys(aRegion.RegionSpecification);
                    if (lRegionKeys.Count == 1)
                    {
                        lDefDirName = lRegionKeys[0];
                    }
                }
                lProjectFileFullPath = System.IO.Path.Combine(lProjectsPath, lDefDirName, lDefDirName + ".mwprj");
            }
            else
            {
                lProjectsPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(lProjectFileFullPath));
            }
            string lDirName = System.IO.Path.GetDirectoryName(lProjectFileFullPath);

            System.IO.Directory.CreateDirectory(lDirName);
            return lProjectFileFullPath;
        }

        public void DownloadDataSetupModels(D4EM.Data.Project aProject, HSPFParameters aParameters)
        {
            atcData.atcDataManager.Clear();
            //create a shapefile containing the project region shape
            DotSpatial.Data.FeatureSet lFeatureSet = new DotSpatial.Data.FeatureSet(DotSpatial.Topology.FeatureType.Polygon);
            lFeatureSet.Projection = aParameters.DesiredProjection;
            string lDescription = aProject.Region.RegionSpecification.ToString();
            DotSpatial.Data.Field lNewField = new DotSpatial.Data.Field("Region", Convert.ToChar("C"), 
                                                    Convert.ToByte(lDescription.Length + 1), 0);
            lFeatureSet.DataTable.Columns.Add(lNewField);
            lFeatureSet.AddFeature(aProject.Region.ToShape(aProject.DesiredProjection).ToGeometry()).DataRow[0] = lDescription;
            lFeatureSet.UpdateExtent();
            lFeatureSet.Filename = System.IO.Path.Combine(aProject.ProjectFolder, "aoi.shp");
            lFeatureSet.Save();
            aProject.Layers.Add(new D4EM.Data.Layer(lFeatureSet,
                                                    new D4EM.Data.LayerSpecification("aoi",
                                                                                     "Area of Interest",
                                                                                     "aoi.shp", null,
                                                                                     D4EM.Data.LayerSpecification.Roles.OtherBoundary,
                                                                                     null, null)));
            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());
       
            List<string> lHUC8s = aProject.Region.GetKeys(D4EM.Data.Region.RegionTypes.huc8);
            //skipping first and last step stuff. i think it's only for the progress bar
            
            //string lResults = "";

            D4EM.Data.Layer lOriginalFlowlinesLayer = null;
            D4EM.Data.Layer lOriginalCatchmentsLayer = null;
            D4EM.Data.Layer lSoilsLayer = null;
            //List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> lSoils = null;
            GetBasinsMet(aProject, aParameters);
            D4EM.Data.Source.BASINS.GetBASINS(aProject, null, lHUC8s[0], D4EM.Data.Source.BASINS.LayerSpecifications.core31.all);
            D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, "NHDPlus", lHUC8s[0], true,
                                                D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline,
                                                D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody,
                                                D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons,
                                                D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid);
                                                //aParameters.ElevationGrid);

                //aProject.Layers.Add(aProject.LayerFromFileName(@"C:\D4EMFromGoogleCodeSVN\D4EMFromGoogleCodeSVN\Externals\data\14060002\NHDPlus\nhdplus14060002\drainage\catchment.shp"));
            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());

            //GetNASS(aProject, aParameters);
            GetSeamless(aProject, aParameters);
            //EnsureGridProjectionsMatch(aProject);
            lSoilsLayer = aProject.LayerFromTag(D4EM.Data.Source.BASINS.LayerSpecifications.core31.statsgo.Tag);

            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());

            lOriginalFlowlinesLayer = aProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline.Tag);

            if (aParameters.CatchmentsMethod == "NHDPlus" && aParameters.ElevationGrid == D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid)
            {
                lOriginalCatchmentsLayer = aProject.LayerFromTag(D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons.Tag);
            }

            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());

            string lScenarioName = "SDMProject";
            if (lHUC8s.Count == 1)
            {
                lScenarioName = lHUC8s[0];
            }

            D4EM.Model.HSPF.HSPFmodel lHSPFModel = new D4EM.Model.HSPF.HSPFmodel();
            lHSPFModel.BuildHSPFInput(
                       aProject,
                       lOriginalCatchmentsLayer,
                       lOriginalFlowlinesLayer,
                       aProject.LayerFromTag(D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover.Tag),
                       aProject.LayerFromTag(aParameters.ElevationGrid.Tag),
                       lSoilsLayer,
                       aProject.TimeseriesSources[0],
                       aParameters.SimulationStartYear,
                       aParameters.SimulationEndYear,
                       aParameters.HspfOutputInterval,
                       lScenarioName,
                       aParameters.WQConstituents.ToArray(),
                       aParameters.HspfSnowOption,
                       aParameters.HspfBacterialOption);

            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());
            foreach (D4EM.Data.Layer lLayer in aProject.Layers)
            {
                lLayer.Close();
            }
        }
        
        private void GetBasinsMet(D4EM.Data.Project aProject, HSPFParameters aParameters)
        {
            List<string> lMetStationIDs = new List<string>();
            //temporarily change region to specify clostst for getting a met station
            D4EM.Data.LayerSpecification lSaveSpec = aProject.Region.RegionSpecification;
            aProject.Region.RegionSpecification = D4EM.Data.Region.RegionTypes.closest;
            D4EM.Data.Source.BASINS.GetMetStations(aProject, ref lMetStationIDs, true); //should add layer to aProject
            aProject.Region.RegionSpecification = lSaveSpec;
            System.IO.File.WriteAllText(aProject.ProjectFilename, aProject.AsMWPRJ());
            D4EM.Data.Source.BASINS.GetMetData(aProject, lMetStationIDs,
                System.IO.Path.Combine(aProject.ProjectFolder, "met" + atcUtility.modFile.g_PathChar + "met.wdm"));
        }

        private void GetSeamless(D4EM.Data.Project aProject, HSPFParameters aParameters)
        {
            D4EM.Data.Source.USGS_Seamless.Execute(aProject, "NLCD", D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover);
        }

        public List<string> ParseNumericKeys(int aKeyLength, string aTextToParse)
        {
            List<string> lFoundList = new List<string>();
            string lKey = "";
            int lCharPos;
            for (lCharPos = 0; lCharPos < aTextToParse.Length; lCharPos++)
            {
                if (IsNumeric(aTextToParse.Substring(lCharPos, 1)))
                {
                    lKey += aTextToParse.Substring(lCharPos, 1);
                }
                else
                {
                    if (lKey.Length == aKeyLength - 1) lKey = "0" + lKey;
                    if (lKey.Length > 0)
                    {
                        if (aKeyLength == 0 || lKey.Length == aKeyLength) lFoundList.Add(lKey);
                        lKey = "";
                    }
                }
            }
            if (lKey.Length == aKeyLength - 1) lKey = "0" + lKey;
            if (lKey.Length == aKeyLength || aKeyLength == 0) lFoundList.Add(lKey);

            return lFoundList;
        }

        //from http://geekswithblogs.net/TJ/archive/2011/03/29/c-isnumeric.aspx
        public static Boolean IsNumeric(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }

        
//        Public Sub GetNLDAS(ByVal aProject As D4EM.Data.Project, ByVal aParameters As SDMParameters, ByRef lStep As Integer, ByVal lLastStep As Integer, ByRef lResults As String)
//        If aParameters.NLDASconstituents.Count > 0 Then
//TryGetvalues:
//                Try
//                    Dim lMetDataFolder As String = IO.Path.Combine(aProject.ProjectFolder, "met")
//                    Dim lDestinationWDMfilename As String = IO.Path.Combine(lMetDataFolder, "met.wdm")
//                    Dim lAllNLDAScells = D4EM.Data.Source.NLDAS.GetGridCellsInRegion(aProject.Region)

//                    'TODO: use all cells in region or use different cell based on subbasin, etc.
//                    'For now, we just use the middle cell.
//                    Dim lNLDAScellsToUse As New Generic.List(Of D4EM.Data.Source.NLDAS.NLDASGridCoords)
//                    lNLDAScellsToUse.Add(lAllNLDAScells(Math.Floor(lAllNLDAScells.Count / 2)))

//                    CheckResult(lResults, D4EM.Data.Source.NLDAS.GetParameter(aProject, lMetDataFolder,
//                                          lNLDAScellsToUse, ,
//                                          New Date(aParameters.SimulationStartYear, 1, 1, 0, 0, 0),
//                                          New Date(aParameters.SimulationEndYear, 12, 31, 23, 0, 0), lDestinationWDMfilename))

//                    'precip has now been added to met WDM

//                    'modify WDM so NLDAS precip will be used instead of BASINS / NCDC precip
//                    Dim lWDM = atcDataManager.DataSourceBySpecification(lDestinationWDMfilename)
//                    If lWDM IsNot Nothing Then
//                        Dim lAllPrecip = lWDM.DataSets.FindData("Constituent", "PREC")
//                        Dim lOriginalPrecip = lAllPrecip(0)
//                        Dim lNLDASPrecip = lWDM.DataSets.FindData("Scenario", "NLDAS")(0)
//                        For Each lAttributeName In {"Scenario", "Location", "Stanam"}
//                            lNLDASPrecip.Attributes.SetValue(lAttributeName, lOriginalPrecip.Attributes.GetValue(lAttributeName))
//                        Next
//                        For Each lPrecip As atcTimeseries In lAllPrecip
//                            If lPrecip.Serial <> lNLDASPrecip.Serial Then 'Make sure original datasets do not get used when building model
//                                lPrecip.Attributes.SetValue("Scenario", "Replaced")
//                                lPrecip.Attributes.SetValue("Constituent", "Replaced")
//                            End If
//                        Next
//                    End If

//                Catch ex As ApplicationException
//                    If ex.Message = "Retry Query" Then GoTo TryGetvalues Else Throw ex
//                End Try
//            End Using
//        End If
//    End Sub
       
    }
}
