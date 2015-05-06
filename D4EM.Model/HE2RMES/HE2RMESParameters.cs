using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;
using System.IO;
using System.Data;
using D4EM.Data.DBManager;

namespace D4EM.Model.HE2RMES
{
    public class HE2RMESParameters
    {
        //DBManager dbMan = null;
        private string _sProjectFolder;
        private string _sCacheFolder;
        private string _sIntermediateFolder;
        private string _sAOIFileName;
        private string _sSourceFileName;
        private string _sHUCNumber;
        private string _sSourceName;
        private string _sSourceType;
        private DataTable _dtVariables = null;
        private DBManager _dbManager = null;
        private HE2RMESLog _log = null;

        public HE2RMESParameters()
        {        
        }

        public DataTable GetVariablesDataTable(string sFileName)
        {
            _dtVariables = new DataTable("Variables");
            //add columns   
            DataColumn col = new DataColumn("DataGroupName");
            col.DataType = System.Type.GetType("System.String");
            _dtVariables.Columns.Add(col);

            col = new DataColumn("VariableName");
            col.DataType = System.Type.GetType("System.String");
            _dtVariables.Columns.Add(col);

            ReadVariablesTextFile(sFileName);

            return _dtVariables;        
        }

        public string SourceFileName
        {
            get { return _sSourceFileName; }
            set { _sSourceFileName = value; }
        }

        public HE2RMESLog Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public DBManager DBManager
        {
            get { return _dbManager; }
            set { _dbManager = value; }
        }

        public string SourceName
        {
            get { return _sSourceName; }
            set { _sSourceName = value; }
        }

        public string SourceTypePrefix
        {
            get {
                switch (_sSourceType)
                {
                    case "Aerated Tank": return "AT"; 
                    case "Land Application Unit": return "LA";
                    case "Landfill": return "LF"; 
                    case "Surface Impoundment": return "SI"; 
                    case "Waste Pile": return "WP";
                    default: return "";                
                } 
            }
        
        }

        public string SourceType
        {
            get { return _sSourceType; }
            set { _sSourceType = value; }
        }

        public string ProjectFolder
        {
            get { return _sProjectFolder; }
            set { _sProjectFolder = value; }
        }

        public string CacheFolder
        {
            get { return _sCacheFolder; }
            set { _sCacheFolder = value; }
        }

        public string IntermediateFolder
        {
            get { return _sIntermediateFolder; }
            set { _sIntermediateFolder = value; }
        }

        public string AOIFileName
        {
            get { return _sAOIFileName; }
            set { _sAOIFileName = value; }
        }

        public string HUCNumber
        {
            get { return _sHUCNumber; }
            set { _sHUCNumber = value; }
        }

        private void ReadVariablesTextFile(string sFileName)
        {
            if (File.Exists(sFileName))
            {
                foreach (string line in atcUtility.modFile.LinesInFile(sFileName))
                {
                    if ((line != null) && (!String.IsNullOrEmpty(line)) && (!line.StartsWith("#")))
                    {
                        string[] items = line.Split(',');
                        if (items.Length > 1)
                        {
                            _dtVariables.Rows.Add(items[0], items[1]);                               
                        }
                        else
                        {
                            MapWinUtility.Logger.Dbg("Found " + items.Length + " but expected 2 comma-separated values in line '" + line + "' in file '" + sFileName + "'");
                        }
                    }
                }
            }
        }

    }
}
