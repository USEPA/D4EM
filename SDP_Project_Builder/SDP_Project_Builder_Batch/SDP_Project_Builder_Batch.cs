using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;
using MapWinUtility;
using D4EM.Model.HE2RMES;
using D4EM.Data.DBManager;



namespace SDP_Project_Builder_Batch
{
    public class SDP_Project_Builder_Batch
    {
        private string _batchConfigFile = String.Empty;

        public SDP_Project_Builder_Batch()
        {
        }

        public SDP_Project_Builder_Batch(string batchConfigFile)
        {
            // TODO: Complete member initialization
            this._batchConfigFile = batchConfigFile;
        }
        /// <summary>
        /// The main entry point for the application.  Will be used in NoGUI mode.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            SDP_Project_Builder_Batch HEB= new SDP_Project_Builder_Batch(args[0]);
            HEB.go();
        }

        public void go()
        {
            //read batch file
            SDPBatchParameters batchParams = new SDPBatchParameters();
            if (!String.IsNullOrEmpty(_batchConfigFile))
            {
                batchParams.ReadParametersTextFile(_batchConfigFile);
                //iterate over project directories
                foreach (string curProjConfigFile in batchParams.ProjectFiles)
                {
                    //run each directory
                    doD4EMProject(curProjConfigFile);
                }
            }
           
        }

        public void doD4EMProject(string curProjConfigFile)
        {

            //create folders

            //open control file for this project
            Logger.Status("Opening project file...");
            SDPParameters parameters = new SDPParameters();
            parameters.ReadParametersTextFile(curProjConfigFile);

            //connect to DB
            Logger.Status("Connecting to database...");
            DBManager dbMgr = new DBManager("MySQL");
            Hashtable htConn = new Hashtable();
            htConn.Add("Server", parameters.Server);
            htConn.Add("Port", parameters.Port);
            htConn.Add("Username", parameters.Username);
            htConn.Add("Password", parameters.Password);
            htConn.Add("Database", parameters.DatabaseName);
            //htConn.Add("Connection Timeout", parameters.LengthOfTimeout);
            //htConn.Add("NumberOfRetries", parameters.NumberOfRetries);
            if (dbMgr.initializeConnection(htConn) == false)
            {
                Logger.Status("Failed to connect to database.");
                return;
            }

            //load AOI feature set
            //load up AOI
            IFeatureSet fsAOI = null;
            string fileName = parameters.AOIFileName;
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }
            if (!File.Exists(fileName))
            {
                return;
            }
            fsAOI = FeatureSet.OpenFile(fileName);

            //get NHD Plus 
            Logger.Status("Downloading NHDPlus...");
            string sHUCNumber;
            GetNHDPlus(parameters, fsAOI, out sHUCNumber);
            Logger.Status("NHDPlus downloaded.");
            //get NLCD
            Logger.Status("Downloading NLCD...");
            GetNLCD(parameters, fsAOI);
            Logger.Status("NLCD downloaded.");
            //get Soils
            Logger.Status("Downloading Soils...");
            GetSoils(parameters, fsAOI, sHUCNumber);
            Logger.Status("Soils downloaded.");

            //Sources first (parallel)?
            HE2RMESParameters hermesParams = new HE2RMESParameters();
            hermesParams.SourceFileName = parameters.SourceFileName;
            hermesParams.AOIFileName = parameters.AOIFileName;
            hermesParams.CacheFolder = parameters.CacheFolder;
            hermesParams.IntermediateFolder = parameters.IntermediateFolder;
            hermesParams.HUCNumber = sHUCNumber;
            hermesParams.SourceName = parameters.SourceName;
            hermesParams.DBManager = dbMgr;
            //run each source type
            foreach (string sSourceType in parameters.SourceTypes)
            {
                hermesParams.SourceType = sSourceType;
                switch (sSourceType)
                {
                    case "Aerated Tank":
                        AeratedTank oAeratedTank = new AeratedTank(hermesParams);
                        oAeratedTank.go();
                        RunScienceModules(hermesParams, parameters);
                        break;
                    case "Land Application Unit":
                        LAU oLAU = new LAU(hermesParams);
                        oLAU.go();
                        RunScienceModules(hermesParams, parameters);
                        break;
                    case "Landfill":
                        Landfill oLandfill = new Landfill(hermesParams);
                        oLandfill.go();
                        RunScienceModules(hermesParams, parameters);
                        break;
                    case "Surface Impoundment":
                        SurfaceImpoundment oSurfaceImpoundment = new SurfaceImpoundment(hermesParams);
                        oSurfaceImpoundment.go();
                        RunScienceModules(hermesParams, parameters);
                        break;
                    case "Waste Pile":
                        WastePile oWastePile = new WastePile(hermesParams);
                        oWastePile.go();
                        RunScienceModules(hermesParams, parameters);
                        break;
                    default: break;
                }                
                
            }

            //now in depdency order
        }

        private void RunScienceModules(HE2RMESParameters hermesParams, SDPParameters parameters)
        {
            //run each science module
            foreach (string sScienceModule in parameters.ScienceModules)
            {
                switch (sScienceModule)
                {
                    case "Air": 
                        Air oAir = new Air(hermesParams);
                        oAir.go();
                        break;
                    case "Aquatic Food Web":
                        AquaticFoodWeb oAquaticFoodWeb = new AquaticFoodWeb(hermesParams);
                        oAquaticFoodWeb.go();
                        break;
                    case "Aquifer":
                        Aquifer oAquifer = new Aquifer(hermesParams);
                        oAquifer.go();
                        break;
                    case "Ecological Exposure":
                        EcologicalExposure oEcologicalExposure = new EcologicalExposure(hermesParams);
                        oEcologicalExposure.go();
                        break;
                    case "Ecological Risk":
                        EcologicalRisk oEcologicalRisk = new EcologicalRisk(hermesParams);
                        oEcologicalRisk.go();
                        break;
                    case "Farm Food Chain":
                        FarmFoodChain oFarmFoodChain = new FarmFoodChain(hermesParams);
                        oFarmFoodChain.go();
                        break;
                    case "Human Exposure":
                        HumanExposure oHumanExposure = new HumanExposure(hermesParams);
                        oHumanExposure.go();
                        break;
                    case "Human Risk":
                        HumanRisk oHumanRisk = new HumanRisk(hermesParams);
                        oHumanRisk.go();
                        break;
                    case "Terrestrial Food Web":
                        TerrestrialFoodWeb oTerrestrialFoodWeb = new TerrestrialFoodWeb(hermesParams);
                        oTerrestrialFoodWeb.go();
                        break;
                    case "Surface Water":
                        SurfaceWater oSurfaceWater = new SurfaceWater(hermesParams);
                        oSurfaceWater.go();
                        break;
                    case "Vadose Zone":
                        Vadose oVadose = new Vadose(hermesParams);
                        oVadose.go();
                        break;
                    case "Watershed":
                        Watershed oWatershed = new Watershed(hermesParams);
                        oWatershed.go();
                        break;

                    default: break;
                }
            }        
        }


        private void GetNHDPlus(SDPParameters parameters, IFeatureSet fsAOI, out string curHuc) 
        {
            IFeatureSet hucs = null;
            String pathToHuc8s = Directory.GetParent(Application.ExecutablePath).FullName + @"\Plugins\SDPProjectBuilder";
            pathToHuc8s = Path.Combine(pathToHuc8s, "BaseLayers");
            pathToHuc8s = Path.Combine(pathToHuc8s, "huc250d3.shp");
            hucs = FeatureSet.OpenFile(pathToHuc8s);
            hucs.Reproject(fsAOI.Projection);


            //intersect huc8 layer to figure out what we need
            IFeatureSet intersected = null;
            intersected = hucs.Intersection(fsAOI, FieldJoinType.All, null);
            intersected.Projection = fsAOI.Projection;

            //now the huc, only get 1 huc, the first huc
            IFeature curFeat = intersected.Features[0];

            String siteRootWorkDir = String.Empty;

            //get HUC number
            curHuc = curFeat.DataRow["CU"].ToString();

            D4EM.Data.Region curRegion = new D4EM.Data.Region(new Shape(fsAOI.Extent.ToEnvelope()), fsAOI.Projection);
            D4EM.Data.LayerSpecification[] layersToGet = new D4EM.Data.LayerSpecification[1] { D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons };

            //now the download
            D4EM.Data.Source.NHDPlus.GetNHDPlus(parameters.CacheFolder, true, parameters.IntermediateFolder,
                                            curRegion, false, parameters.CacheFolder, curHuc, true,
                                            fsAOI.Projection, true,
                                            parameters.CacheFolder, layersToGet);
        }


        private void GetNLCD(SDPParameters parameters, IFeatureSet fsAOI)
        {

            //reset curRegion to reconsCatches (rather than stie area)
            D4EM.Data.Region curRegion = new D4EM.Data.Region(new Shape(fsAOI.Extent.ToEnvelope()), fsAOI.Projection);
            //Console.WriteLine("Found huc: " + curHuc);
            D4EM.Data.Source.USGS_Seamless.GetNLCD(parameters.CacheFolder, fsAOI.Projection, "NLCD", 
                parameters.CacheFolder, curRegion, false, D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover);

        }

        private void GetSoils(SDPParameters parameters, IFeatureSet fsAOI, string curHUC)
        {
            
            //reset curRegion to reconsCatches (rather than stie area)
            D4EM.Data.Region curRegion = new D4EM.Data.Region(new Shape(fsAOI.Extent.ToEnvelope()), fsAOI.Projection);
            
            //SSURGO
            //D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(parameters.CacheFolder, 
            //                    parameters.CacheFolder, parameters.CacheFolder, curRegion, fsAOI.Projection);

            //BASINS
            D4EM.Data.Source.BASINS.GetBASINS(parameters.CacheFolder, parameters.CacheFolder, "", curHUC, 
                        D4EM.Data.Source.BASINS.LayerSpecifications.core31.statsgo, curRegion, fsAOI.Projection, true);

        }
    }
}
