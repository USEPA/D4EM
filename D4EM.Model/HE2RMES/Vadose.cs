using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Projections;
using MapWinUtility;
using D4EM.Data.Source;
using D4EM.Data;
using System.Reflection;
using D4EM.Data.DBManager;

namespace D4EM.Model.HE2RMES
{
    public class Vadose
    {
        HE2RMESParameters _parameters = null;           
        string _sSettingID = null;
        DBManager _dbManager = null;

        public Vadose()
        { 
        }

        public Vadose(HE2RMESParameters parameters)
        {
           _parameters = parameters;    
           _dbManager = _parameters.DBManager;

        }

        public Boolean go()
        {
            WriteVariablesToDB();

            return true;

        }

        public void WriteVariablesToDB()
        {
            //get this DLL Directory
            string sDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //create Batch dir
            string sFilePath = Path.Combine(sDir, "Variables_Vadose.txt");
            DataTable dt;
            dt = _parameters.GetVariablesDataTable(sFilePath);


            _sSettingID = _parameters.SourceTypePrefix + _parameters.SourceName;
            
            //loop through variables
            //also log missing variable      
            if (_parameters.Log == null)
            {
                string sLogFile = Path.Combine(sDir, "HE2RMES_Model.log");
                _parameters.Log = new HE2RMESLog(sLogFile);
            }
            _parameters.Log.WriteLine("*** Running Vadose Zone for " + _parameters.SourceType + " ***");
            foreach (DataRow row in dt.Rows)
            {
                string sDataGroupName = row["DataGroupName"].ToString();
                string sVariableName = row["VariableName"].ToString();
                //if variable is missing then calculate it and insert it
                if (!_dbManager.VariableExistsSite(_sSettingID, sDataGroupName, sVariableName))
                {
                    if (!_dbManager.VariableExistsRegional(sDataGroupName, sVariableName))
                    {
                        if (!_dbManager.VariableExistsNational(sDataGroupName, sVariableName))
                        {
                            _parameters.Log.WriteLine("Missing Variable: " + _sSettingID + "," + sDataGroupName + "," + sVariableName);
                            string sDataGroupVar = sDataGroupName + "," + sVariableName;
                            switch (sDataGroupVar)
                            {
                                case "Site Layout,GWClass":
                                    WriteSiteLayoutGWClass(sDataGroupName,sVariableName);
                                    break;
                                case "Site Layout,MapUID":
                                    WriteSiteLayoutMapUID(sDataGroupName, sVariableName);
                                    break;
                                case "Site Layout,NumVad": 
                                    break;
                                case "Site Layout,SoilTextureCol": 
                                    break;
                                case "Site Layout,SrcLWS": 
                                    break;
                                case "Site Layout,SrcLWSSubAreaId": 
                                    break;
                                case "Site Layout,VadALPHA": 
                                    break;
                                case "Site Layout,VadBETA": 
                                    break;
                                case "Site Layout,VadID": 
                                    break;
                                case "Site Layout,VadPH": 
                                    break;
                                case "Site Layout,VadSATK": 
                                    break;
                                case "Site Layout,VadTemp": 
                                    break;
                                case "Site Layout,VadWCR": 
                                    break;
                                case "Site Layout,VadWCS": 
                                    break;
                                case "Vadose,DISPR": 
                                    break;
                                case "Vadose,POM": 
                                    break;
                                case "Vadose,RHOB": 
                                    break;

                                default:
                                    break;
                            }
                          

                        }
                    }
                    
                }
            
            }
        }

        public void WriteSiteLayoutGWClass(string sDataGroupName, string sVariableName)
        {
            //select hydro geo by centroid of source
            //open source 
            IFeatureSet fsSource = FeatureSet.OpenFile(_parameters.SourceFileName);
            //open hydro geo
            string sHydroGeoFileName = Directory.GetCurrentDirectory() + @"\Plugins\SDPProjectBuilder\BaseLayers\HydroGeo\hydro_environments_final.shp";
            IFeatureSet fsHydroGeo = FeatureSet.OpenFile(sHydroGeoFileName);
            fsHydroGeo.Reproject(fsSource.Projection);

            Feature fSourceCenter = new Feature(fsSource.Extent.Center);
            List<IFeature> featuresHydroGeo = fsHydroGeo.Select(fSourceCenter.Envelope.ToExtent());
            //delete all but one feature
            int iCount = featuresHydroGeo.Count;
            if (iCount > 1)
            {
                //leave first feature, start at index 1
                for (int i = 1; i < iCount; i++)
                {
                    featuresHydroGeo.RemoveAt(i);
                }
            }

            string sGWClass = featuresHydroGeo[0].DataRow["HyE"].ToString();
            
            _dbManager.WriteVariableSite(_sSettingID, sDataGroupName, sVariableName,"", DBManager.CONST_DATA_TYPE_STRING, sGWClass,0);
        }

        public void WriteSiteLayoutMapUID(string sDataGroupName, string sVariableName)
        {
            //select statsgo by source
            //open source 
            IFeatureSet fsSource = FeatureSet.OpenFile(_parameters.SourceFileName);
            //open hydro geo
            string sStatsgoFileName = _parameters.CacheFolder + @"\statsgo.shp";
            IFeatureSet fsStatsgo = FeatureSet.OpenFile(sStatsgoFileName);
            fsStatsgo.Reproject(fsSource.Projection);

            List<IFeature> featuresStatsgo = fsStatsgo.Select(fsSource.Extent);
            //select feature with greatest area
            int iIndexGreatestArea = 0;
            double dGreatestArea = 0;
            for (int i=0; i < featuresStatsgo.Count; i++)
            {
                double dArea = featuresStatsgo[i].Area();
                if (dArea > dGreatestArea)
                {
                    dGreatestArea = dArea;
                    iIndexGreatestArea = i;
                }
            }            

            string sMapUID = featuresStatsgo[iIndexGreatestArea].DataRow["MUID"].ToString();
            _dbManager.WriteVariableSite(_sSettingID, sDataGroupName, sVariableName, "", DBManager.CONST_DATA_TYPE_STRING, sMapUID,1,1);
            
        }

        public void WriteSiteLayoutNumVad(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutSoilTextureCol(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutSrcLWS(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutSrcLWSSubAreaId(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadALPHA(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadBETA(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadID(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadPH(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadSATK(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadTemp(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadWCR(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteSiteLayoutVadWCS(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteVadoseDISPR(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteVadosePOM(string sDataGroupName, string sVariableName)
        {

        }

        public void WriteVadoseRHOB(string sDataGroupName, string sVariableName)
        {

        }

    }
}
