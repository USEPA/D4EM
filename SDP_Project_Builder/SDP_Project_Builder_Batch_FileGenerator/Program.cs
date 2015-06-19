using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SDP_Project_Builder_Batch;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;
using System.Data;

namespace SDP_Project_Builder_Batch_FileGenerator
{
    class Program
    {
        string _sFeatureSetFilePath = "";
        string _sSiteID = "";
        string _sBatchDir = "";
        string _sShapesDir = "";
        string _sAppDir = "";
        SDPParameters _parameters = null;
        SDPBatchParameters _batchParameters = null;

        public Program()
        { 
        }

        public Program(string sFeatureSetFilePath, string sSiteID)
        {
            _sFeatureSetFilePath = sFeatureSetFilePath;
            _sSiteID = sSiteID;
        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                //arguments must be SHAPEFILE and SITEID (1 through 10000 or -1 to run all sites)
                //ex. mc_10k_nad83.shp, 45
                Console.WriteLine("Execution Failed. Invalid Arguments.");
                return;
            }

            string sFeatureSetFilePath = args[0];
            string sSiteID = args[1];

            Program p = new Program(sFeatureSetFilePath, sSiteID);
            p.BuildBatchFile();
        }

        public void BuildBatchFile()
        { 
            //start new batch file
            _batchParameters = new SDPBatchParameters();

            //get Application Directory
            _sAppDir = AppDomain.CurrentDomain.BaseDirectory;
            //create Batch dir
            _sBatchDir = Path.Combine(_sAppDir, "Batch");
            if (!Directory.Exists(_sBatchDir))
            {
                Directory.CreateDirectory(_sBatchDir);
            }
            //create shapes dir
            _sShapesDir = Path.Combine(_sBatchDir, "Shapes");
            if (!Directory.Exists(_sShapesDir))
            {
                Directory.CreateDirectory(_sShapesDir);
            }
            
            //open template project file
            string sFilePathProjTemp = Path.Combine(_sAppDir, "HE2RMESProject_Template.txt");
            _parameters = new SDP_Project_Builder_Batch.SDPParameters();
            _parameters.ReadParametersTextFile(sFilePathProjTemp);

            //open file to get projection
            string sFilePathProjection = Path.Combine(_sAppDir, "HE2RMESProject_Source.prj");
            ProjectionInfo pi = ProjectionInfo.Open(sFilePathProjection);

            //open 10K featureset
            IFeatureSet fs10K = FeatureSet.OpenFile(_sFeatureSetFilePath);
            fs10K.Reproject(pi);
            //populate feature lookup
            if (fs10K.FeatureLookup.Count == 0)
            {
                foreach (Feature f in fs10K.Features)
                {
                    fs10K.FeatureLookup.Add(f.DataRow, f);
                }
            }

            //find site to run
            if (_sSiteID != "-1")
            {
                BuildProjectFile(_sSiteID, fs10K);
            }
            else
            { 
                DataTable dt = fs10K.DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["MC_10K_"].ToString();
                    BuildProjectFile(id, fs10K);                
                }
            }
            
            //save batch file
            _batchParameters.BatchName = "Batch";
            DateTime dNow = DateTime.Now;            
            _batchParameters.BatchDescription = dNow.ToString("yyyy/MM/dd HH:mm");
            string sDateTime = dNow.ToString("yyyy_MM_dd_HHmm");
            string sFilePathBatch = Path.Combine(_sBatchDir, "Batch_" + sDateTime + ".txt");
            _batchParameters.WriteParametersTextFile(sFilePathBatch);

            Console.WriteLine("New Batch File Created: ");
        
        }

        public void BuildProjectFile(string sSiteID, IFeatureSet fs10K)
        {
            IFeature fSite;

            string sFilter = "[MC_10K_] = " + sSiteID;
            List<IFeature> fList = fs10K.SelectByAttribute(sFilter);
            if (fList.Count > 0)
            {
                fSite = fList[0];
            }
            else
            {
                return;
            }

            //open HUC 
            string sFilePathHUC = Path.Combine(_sAppDir, "huc250d3.shp");
            IFeatureSet fsHUC = FeatureSet.Open(sFilePathHUC);
            fsHUC.Reproject(fs10K.Projection);

            //create Source
            FeatureSet fsSource = new FeatureSet(FeatureType.Polygon);
            fsSource.Projection = fs10K.Projection;
            fsSource.Name = "Source_" + sSiteID;
            //fSite is point features, so buffer to make polygon
            Feature fSiteNew = (Feature)fSite.Buffer(250);
            fsSource.AddFeature(fSiteNew);

            //select huc by source extent
            List<IFeature> featuresHUC = fsHUC.Select(fsSource.Extent);
            //delete all but one HUC
            int iCount = featuresHUC.Count;
            if (iCount > 1)
            {
                //leave first huc, start at index 1
                for (int i = 1; i < iCount; i++)
                {
                    featuresHUC.RemoveAt(i);
                }
            }

            FeatureSet fsHUCClipped = new FeatureSet(FeatureType.Polygon);
            fsHUCClipped.Name = "HUC_" + sSiteID;
            fsHUCClipped.Projection = fsSource.Projection;
            Feature fHUC = (Feature)featuresHUC[0];
            fsHUCClipped.AddFeature(fHUC);
            fsHUCClipped.SaveAs(Path.Combine(_sShapesDir,  fsHUCClipped.Name + ".shp"),true);          
            
            //clip source against HUC Clipped (i.e., 1 HUC)
            IFeatureSet fsSourceClipped = null;
            fsSourceClipped = fsSource.Intersection(fsHUCClipped, FieldJoinType.All, null);
            fsSourceClipped.Name = fsSource.Name;
            fsSourceClipped.Projection = fsSource.Projection;

            //create AOI
            FeatureSet fsAOI = null;
            fsAOI = new FeatureSet(FeatureType.Polygon);
            fsAOI.Projection = fs10K.Projection;
            fsAOI.Name = "AOI_" + sSiteID;
            //buffer site new feature
            Feature fAOI = (Feature)fsSourceClipped.Features[0].Buffer(1000);
            //add to AOI feature set
            fsAOI.AddFeature(fAOI);

            //clip AOI against HUC Clipped (i.e., 1 HUC)
            IFeatureSet fsAOIClipped = null;
            fsAOIClipped = fsAOI.Intersection(fsHUCClipped, FieldJoinType.All, null);
            fsAOIClipped.Name = fsAOI.Name;
            fsAOIClipped.Projection = fsAOI.Projection;

            //save source
            string sFilePath = Path.Combine(_sShapesDir, fsSourceClipped.Name + ".shp");
            fsSourceClipped.SaveAs(sFilePath, true);
            //save to parameters
            _parameters.SourceFileName = sFilePath;          

            //save AOI
            sFilePath = Path.Combine(_sShapesDir, fsAOIClipped.Name + ".shp");
            fsAOIClipped.SaveAs(sFilePath, true);
            //save to parameters
            _parameters.AOIFileName = sFilePath;

            //save as new project file
            //pad siteid to get sourcename
            _parameters.SourceName = sSiteID.PadLeft(5,'0');
            string sFilePathProj = Path.Combine(_sBatchDir, "Project_" + sSiteID + ".txt");
            _parameters.WriteParametersTextFile(sFilePathProj);

            //add project file to batch file
            _batchParameters.ProjectFiles.Add(sFilePathProj);

        
        }
    }
}
