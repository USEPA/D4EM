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
    public class SurfaceImpoundment
    {
        HE2RMESParameters _parameters = null;
        string _sDirPathNHDPlusIntermediate = null;
        string _sFilePathNHDFlowline = null;
        string _sFilePathNHDFlowlineVAA = null;
        string _sFilePathNHDFlowlineClipped = null;
        List<string> _listReachesEnteringAOI = new List<string>();
        List<string> _listReachesExitingAOI = new List<string>();
        DataTable _dtReachesInAOI = null;
        DataTable _dtFlowlineClipped = null;
        string _sSettingID = null;
        DBManager _dbManager = null;

        public SurfaceImpoundment()
        { 
        }

        public SurfaceImpoundment(HE2RMESParameters parameters)
        {
            _parameters = parameters;

            //set directory paths
           _sDirPathNHDPlusIntermediate = _parameters.IntermediateFolder + Path.DirectorySeparatorChar + "NHDPlus" + _parameters.HUCNumber;
           _sFilePathNHDFlowline = _parameters.CacheFolder + Path.DirectorySeparatorChar
                                               + "NHDPlus" + _parameters.HUCNumber + Path.DirectorySeparatorChar
                                               + "hydrography" + Path.DirectorySeparatorChar + "NHDFlowline.shp";
           _sFilePathNHDFlowlineVAA = _parameters.CacheFolder + Path.DirectorySeparatorChar
                                              + "NHDPlus" + _parameters.HUCNumber + Path.DirectorySeparatorChar + "NHDFlowlineVAA.dbf";
           _sFilePathNHDFlowlineClipped = _sDirPathNHDPlusIntermediate + Path.DirectorySeparatorChar + "NHDFlowlineClipped.shp";

           _dbManager = _parameters.DBManager;

        }

        public Boolean go()
        {
            //create NHD directories
            if (!Directory.Exists(_sDirPathNHDPlusIntermediate))
            {
                Directory.CreateDirectory(_sDirPathNHDPlusIntermediate);           
            }

            CreateReachesInAOITable();
            
            //clip NHD Layer
            ClipNHDLayersToAOI();
            //find reaches exiting
            FindReachesEnteringAndExitingAOI();
            //delineate waterbody networks
            DelineateWBNs();

            WriteVariablesToDB();

            return true;

        }

        public void WriteVariablesToDB()
        {
            //get this DLL Directory
            string sDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //create Batch dir
            string sFilePath = Path.Combine(sDir, "Variables_SurfaceImpoundment.txt");
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
            _parameters.Log.WriteLine("*** Running Surface Impoundment ***");
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
                            switch (sVariableName)
                            {
                                case "SiteLongitude":                                    
                                    break;
                               
                                default:
                                    break;                        
                             }
                          

                        }
                    }
                    
                }
            
            }
        }      

        public void CreateReachesInAOITable()
        {
            _dtReachesInAOI = new DataTable("ReachesInAOI");
            //add columns
            DataColumn col = new DataColumn("COMID");
            col.DataType = System.Type.GetType("System.String");
            _dtReachesInAOI.Columns.Add(col);

            col = new DataColumn("Index1");
            col.DataType = System.Type.GetType("System.Int32");
            _dtReachesInAOI.Columns.Add(col);

            col = new DataColumn("Index2");
            col.DataType = System.Type.GetType("System.Int32");
            _dtReachesInAOI.Columns.Add(col);

        }

        private void DelineateWBNs()
        {
            int i = 0;

            //getAOI
            IFeatureSet fsAOI = FeatureSet.OpenFile(_parameters.AOIFileName);

            //foreach (string sCOMID in _listReachesEnteringAOI)
            //{
            //    long lCOMID = long.Parse(sCOMID);
            //    EPAWaters.GetLayer(_sDirPathNHDPlusIntermediate, _sDirPathNHDPlusIntermediate,
            //        fsAOI.Projection, lCOMID, 100, EPAWaters.LayerSpecifications.Flowline);
            
            //}

            foreach (string sCOMID in _listReachesExitingAOI)
            {
                i++;
                //if reach enters and exits, then skip it the second time
                //we did it already above
                //if (!_listReachesEnteringAOI.Contains(sCOMID))
                //{
                //    long lCOMID = long.Parse(sCOMID);
                //    EPAWaters.GetLayer(_sDirPathNHDPlusIntermediate, _sDirPathNHDPlusIntermediate, 
                //        fsAOI.Projection, lCOMID, 100, EPAWaters.LayerSpecifications.Flowline);
                //}
                GetUpstreamReaches(sCOMID, i, 0);
            
            }     
        
        }

        private bool IsReachInAOI(string sCOMID)
        {
            //open NHD flowline clipped
            if (_dtFlowlineClipped == null)
            {
                IFeatureSet fsFlowlineClipped = FeatureSet.OpenFile(_sFilePathNHDFlowlineClipped);
                _dtFlowlineClipped = fsFlowlineClipped.DataTable;
            }

            DataRow[] foundRowsAOI;
            foundRowsAOI = _dtFlowlineClipped.Select("COMID = " + sCOMID);
            if (foundRowsAOI.Length > 0)
            {
                return true;
            }
            else 
            {
                return false;            
            }
        }

        private void GetUpstreamReaches(string sCOMID, int iIndex1, int iIndex2)
        {
           
            //open NHD flowline VAA
            AttributeTable atFlowlineVAA = new AttributeTable();
            atFlowlineVAA.Open(_sFilePathNHDFlowlineVAA);
            DataTable dtFlowlineVAA = atFlowlineVAA.Table;

            //add COMID 
            if (IsReachInAOI(sCOMID))
            {
                //add to list
                _dtReachesInAOI.Rows.Add(sCOMID, iIndex1, iIndex2);

            }
            else
            {
                //if reach is not in AOI then exit
                return;
            }

            DataRow[] foundRowsFrom;
            foundRowsFrom = dtFlowlineVAA.Select("COMID = " + sCOMID);
            string sFromNode = "";
            if (foundRowsFrom.Length > 0)
            {
                sFromNode = foundRowsFrom[0]["FROMNODE"].ToString();
                if (!String.IsNullOrEmpty(sFromNode))
                {
                    //query for reaches with FROM node as TO node
                    DataRow[] foundRowsTo;
                    foundRowsTo = dtFlowlineVAA.Select("TONODE = " + sFromNode);
                    if (foundRowsTo.Length > 0)
                    {
                        foreach (DataRow row in foundRowsTo)
                        {
                            string sFoundCOMID = row["COMID"].ToString();
                            ////if a reach is not in AOI
                            //if (!IsReachInAOI(sCOMID))
                            //{
                            //    break;
                            //}
                            iIndex2++;                                                       
                            //query for its upstream reaches
                            GetUpstreamReaches(sFoundCOMID, iIndex1, iIndex2);                        
                        }
                    }                  
                }
            }
        
        }

        private bool ClipNHDLayersToAOI()
        {
            //getAOI
            IFeatureSet fsAOI = FeatureSet.OpenFile(_parameters.AOIFileName);
            //get flowlines
            
            IFeatureSet fsFL = FeatureSet.OpenFile(_sFilePathNHDFlowline);
            fsFL.Reproject(fsAOI.Projection);            

            //IFeatureSet fsFLClipped = new FeatureSet(fsFL.Select(fsAOI.Extent));
            IFeatureSet fsFLClipped = fsFL.Intersection(fsAOI, FieldJoinType.All, null);
            fsFLClipped.Projection = fsAOI.Projection;
            fsFLClipped.FeatureType = FeatureType.Line;
            
            fsFLClipped.SaveAs(_sFilePathNHDFlowlineClipped, true);
            fsAOI.Close();
            fsFL.Close();
            fsFLClipped.Close();  

            return true;
        }

        private void FindReachesEnteringAndExitingAOI()
        {
            //open NHD flowline clipped
            IFeatureSet fsFlowlineClipped = FeatureSet.OpenFile(_sFilePathNHDFlowlineClipped);
            DataTable dtFlowlineClipped = fsFlowlineClipped.DataTable;

            foreach (DataRow row in dtFlowlineClipped.Rows)
            { 
                string sCOMID = row["COMID"].ToString();

                if (IsReachEnteringAOI(sCOMID, dtFlowlineClipped))
                {
                    _listReachesEnteringAOI.Add(sCOMID);                
                }

                if (IsReachExitingAOI(sCOMID, dtFlowlineClipped))
                {
                    _listReachesExitingAOI.Add(sCOMID);                    
                }
           
            }

            
        }

        private bool IsReachExitingAOI(string sCOMID, DataTable dtAOI)
        {
            string sToNode;
            try
            {
                //open NHD flowline VAA
                AttributeTable atFlowlineVAA = new AttributeTable();
                atFlowlineVAA.Open(_sFilePathNHDFlowlineVAA);
                DataTable dtFlowlineVAA = atFlowlineVAA.Table;

                //Find the COMID in VAA
                DataRow[] foundRowsTo;
                foundRowsTo = dtFlowlineVAA.Select("COMID = " + sCOMID);
                if (foundRowsTo.Length > 0)
                {                      
                    sToNode = foundRowsTo[0]["TONODE"].ToString();
                    if (String.IsNullOrEmpty(sToNode))
                    {
                        //COMID is not found in the FlowVAA table.
                        //most likely bad data - going any further will fail
                        //exit
                        return false;
                    }
                }
                else
                {
                    return false;                        
                }

                //Again query the VAA table, return the downstream reach with the upstream TO node as its FROM node                        
                DataRow[] foundRowsFrom;
                foundRowsFrom = dtFlowlineVAA.Select("FROMNODE = " + sToNode);
                //only need to test the first downstream reach to see if outside AOI
                //get the COMID for the downstream reach
                if (foundRowsFrom.Length > 0)
                {
                    string sDownstreamCOMID = foundRowsFrom[0]["COMID"].ToString();
                    //Is this flowline inside our AOI?  If not, then our initial reach has exited the AOI
                    DataRow[] foundRowsAOI;
                    foundRowsAOI = dtAOI.Select("COMID = " + sDownstreamCOMID);
                    if (foundRowsAOI.Length > 0)
                    {
                        return false;                                
                    }
                    else
                    { 
                        return true;
                    }
                }
                else
                {
                    //No downstream reaches
                    return true;
                }                   
                
            }
            catch (Exception ex)
            {
                Logger.Status("Error determining if reach was exiting AOI.");
            }
            return true;
        }

        private bool IsReachEnteringAOI(string sCOMID, DataTable dtAOI)
        {
            string sFromNode;
            try
            {
                //open NHD flowline VAA
                AttributeTable atFlowlineVAA = new AttributeTable();
                atFlowlineVAA.Open(_sFilePathNHDFlowlineVAA);
                DataTable dtFlowlineVAA = atFlowlineVAA.Table;

                //Find the COMID in VAA
                DataRow[] foundRowsFrom;
                foundRowsFrom = dtFlowlineVAA.Select("COMID = " + sCOMID);
                if (foundRowsFrom.Length > 0)
                {
                    sFromNode = foundRowsFrom[0]["FROMNODE"].ToString();
                    if (String.IsNullOrEmpty(sFromNode))
                    {
                        //COMID is not found in the FlowVAA table.
                        //most likely bad data - going any further will fail
                        //exit
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                //Again query the VAA table, return the downstream reach with the upstream FROM node as its TO node                        
                DataRow[] foundRowsTo;
                foundRowsTo = dtFlowlineVAA.Select("TONODE = " + sFromNode);
                //only need to test the first upstream reach to see if outside AOI
                //get the COMID for the upstream reach
                if (foundRowsTo.Length > 0)
                {
                    string sUpstreamCOMID = foundRowsTo[0]["COMID"].ToString();
                    //Is this flowline inside our AOI?  If not, then our initial reach has entered the AOI
                    DataRow[] foundRowsAOI;
                    foundRowsAOI = dtAOI.Select("COMID = " + sUpstreamCOMID);
                    if (foundRowsAOI.Length > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    //No upstream reaches
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logger.Status("Error determining if reach was entering AOI.");
            }
            return true;
        }




    }
}
