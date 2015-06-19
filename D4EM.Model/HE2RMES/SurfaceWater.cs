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
    public class SurfaceWater
    {
        HE2RMESParameters _parameters = null;           
        string _sSettingID = null;
        DBManager _dbManager = null;

        public SurfaceWater()
        { 
        }

        public SurfaceWater(HE2RMESParameters parameters)
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
            string sFilePath = Path.Combine(sDir, "Variables_SurfaceWater.txt");
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
            _parameters.Log.WriteLine("*** Running Surface Water for " + _parameters.SourceType + " ***");
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
                                default:
                                    break;
                            }
                          

                        }
                    }
                    
                }
            
            }
        }

       

    }
}
