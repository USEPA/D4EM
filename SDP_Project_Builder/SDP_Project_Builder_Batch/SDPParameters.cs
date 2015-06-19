using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using atcUtility;

namespace SDP_Project_Builder_Batch
{
    public class SDPParameters
    {
        private string sSourceName;
        private string sState;
        private string sCounty;
        private string sHUC;
        private string sLatitude;
        private string sLongitude;
        private string sSourceFileName;
        private string sAOIFileName;
        private string sAOIType;
        private string sProjectFolder;
        private string sCacheFolder;
        private string sIntermediateFolder;
        private string sNHDPlusLocation;
        private string sNLCDLocation;
        private string sSoilsLocation;
        private string sNHDPlusDisregardCache;
        private string sNLCDDisregardCache;
        private string sSoilsDisregardCache;
        private string sServer;
        private string sUsername;
        private string sPassword;
        private string sPort;
        private string sLengthOfTimeout;
        private string sNumberOfRetries;
        private string sDatabaseName;
        private string sForceFullDependency;
        private List<string> lstSourceTypes = new List<string>();
        private List<string> lstScienceModules = new List<string>();


        public SDPParameters()
        {
        }

        public SDPParameters(string sProjectFileName)
        {            
        
        }

        public string SourceName
        {
            get { return sSourceName; }
            set { sSourceName = value; }
        }

        public string State
        {
            get { return sState; }
            set { sState = value; }
        }

        public string County
        {
            get { return sCounty; }
            set { sCounty = value; }
        }

        public string HUC
        {
            get { return sHUC; }
            set { sHUC = value; }
        }

        public string Latitude
        {
            get { return sLatitude; }
            set { sLatitude = value; }
        }

        public string Longitude
        {
            get { return sLongitude; }
            set { sLongitude = value; }
        }

        public string SourceFileName
        {
            get { return sSourceFileName; }
            set { sSourceFileName = value; }
        }

        public string AOIFileName
        {
            get { return sAOIFileName; }
            set { sAOIFileName = value; }
        }

        public string AOIType
        {
            get { return sAOIType; }
            set { sAOIType = value; }
        }

        public string ProjectFolder
        {
            get { return sProjectFolder; }
            set { sProjectFolder = value; }
        }

        public string CacheFolder
        {
            get { return sCacheFolder; }
            set { sCacheFolder = value; }
        }

        public string IntermediateFolder
        {
            get { return sIntermediateFolder; }
            set { sIntermediateFolder = value; }
        }

        public string NHDPlusLocation
        {
            get { return sNHDPlusLocation; }
            set { sNHDPlusLocation = value; }
        }

        public string NLCDLocation
        {
            get { return sNLCDLocation; }
            set { sNLCDLocation = value; }
        }

        public string SoilsLocation
        {
            get { return sSoilsLocation; }
            set { sSoilsLocation = value; }
        }

        public string NHDPlusDisregardCache
        {
            get { return sNHDPlusDisregardCache; }
            set { sNHDPlusDisregardCache = value; }
        }

        public string NLCDDisregardCache
        {
            get { return sNLCDDisregardCache; }
            set { sNLCDDisregardCache = value; }
        }

        public string SoilsDisregardCache
        {
            get { return sSoilsDisregardCache; }
            set { sSoilsDisregardCache = value; }
        }

        public string Server
        {
            get { return sServer; }
            set { sServer = value; }
        }

        public string Username
        {
            get { return sUsername; }
            set { sUsername = value; }
        }

        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        public string Port
        {
            get { return sPort; }
            set { sPort = value; }
        }

        public string LengthOfTimeout
        {
            get { return sLengthOfTimeout; }
            set { sLengthOfTimeout = value; }
        }

        public string NumberOfRetries
        {
            get { return sNumberOfRetries; }
            set { sNumberOfRetries = value; }
        }

        public string DatabaseName
        {
            get { return sDatabaseName; }
            set { sDatabaseName = value; }
        }

        public string ForceFullDependency
        {
            get { return sForceFullDependency; }
            set { sForceFullDependency = value; }
        }

        public List<string> SourceTypes
        {
            get { return lstSourceTypes; }
            set { lstSourceTypes = value; }
        }

        public List<string> ScienceModules
        {
            get { return lstScienceModules; }
            set { lstScienceModules = value; }
        }

        public void ReadParametersTextFile(string sFileName)
        {
            if  (File.Exists(sFileName)) 
            {
                foreach (string line in atcUtility.modFile.LinesInFile(sFileName))
                {
                    if ((line != null) && (!String.IsNullOrEmpty(line)) && (!line.StartsWith("#")))
                    {
                        string[] items = line.Split(',');
                        if (items.Length > 1)
                        {
                            switch (items[0])
                            {
                                case "SourceName":
                                    SourceName = items[1];
                                    break;
                                case "State":
                                    State = items[1];
                                    break;
                                case "County":
                                    County = items[1];
                                    break;
                                case "HUC":
                                    HUC = items[1];
                                    break;
                                case "Latitude":
                                    Latitude = items[1];
                                    break;
                                case "Longitude":
                                    Longitude = items[1];
                                    break;
                                case "SourceFileName":
                                    SourceFileName = items[1];
                                    break;
                                case "AOIFileName":
                                    AOIFileName = items[1];
                                    break;
                                case "AOIType":
                                    AOIType = items[1];
                                    break;
                                case "ProjectFolder":
                                    ProjectFolder = items[1];
                                    break;
                                case "CacheFolder":
                                    CacheFolder = items[1];
                                    break;
                                case "IntermediateFolder":
                                    IntermediateFolder = items[1];
                                    break;
                                case "NHDPlusLocation":
                                    NHDPlusLocation = items[1];
                                    break;
                                case "NLCDLocation":
                                    NLCDLocation = items[1];
                                    break;
                                case "SoilsLocation":
                                    SoilsLocation = items[1];
                                    break;
                                case "NHDPlusDisregardCache":
                                    NHDPlusDisregardCache = items[1];
                                    break;
                                case "NLCDDisregardCache":
                                    NLCDDisregardCache = items[1];
                                    break;
                                case "SoilsDisregardCache":
                                    SoilsDisregardCache = items[1];
                                    break;
                                case "Server":
                                    Server = items[1];
                                    break;
                                case "Username":
                                    Username = items[1];
                                    break;
                                case "Password":
                                    Password = items[1];
                                    break;
                                case "Port":
                                    Port = items[1];
                                    break;
                                case "LengthOfTimeout":
                                    LengthOfTimeout = items[1];
                                    break;
                                case "NumberOfRetries":
                                    NumberOfRetries = items[1];
                                    break;
                                case "DatabaseName":
                                    DatabaseName = items[1];
                                    break;
                                case "ForceFullDependency":
                                    ForceFullDependency = items[1];
                                    break;
                                case "SourceTypes":
                                    SourceTypes = new List<string>(items[1].Split('|'));
                                    break;
                                case "ScienceModules":
                                    ScienceModules = new List<string>(items[1].Split('|'));
                                    break;
                                default:
                                    MapWinUtility.Logger.Dbg("Unused line in HE2RMES parameter file: '" + line + "' in file '" + sFileName + "'");
                                    break;
                            }
                        }
                        else
                        {
                            MapWinUtility.Logger.Dbg("Found " + items.Length + " but expected 2 comma-separated values in line '" + line + "' in file '" + sFileName + "'");
                        }
                    }
                }
            }
        }

        public void WriteParametersTextFile(string sFilename)
        {
            File.WriteAllText(sFilename, ParametersToString());
        }

        private void AppendList(String VariableName, List<String> Values,  StringBuilder sb )
        {
            if ((Values != null) && (Values.Count > 0))
            {
                sb.AppendLine(VariableName + "," + String.Join("|", Values.ToArray()));
            }
        }

        public string ParametersToString()
        {

            StringBuilder sb = new StringBuilder();    

            //Dim lFilename As String
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalHuc8," & lFilename)
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalCounty," & lFilename)
            //lFilename = D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state)
            //If IO.File.Exists(lFilename) Then sb.AppendLine("NationalState," & lFilename)

            sb.AppendLine("SourceName," + SourceName);
            sb.AppendLine("State," + State);
            sb.AppendLine("County," + County);
            sb.AppendLine("HUC," + HUC);
            sb.AppendLine("Latitude," + Latitude);
            sb.AppendLine("Longitude," + Longitude);
            sb.AppendLine("SourceFileName," + SourceFileName);
            sb.AppendLine("AOIFileName," + AOIFileName);
            sb.AppendLine("AOIType," + AOIType);
            sb.AppendLine("ProjectFolder," + ProjectFolder);
            sb.AppendLine("CacheFolder," + CacheFolder);
            sb.AppendLine("IntermediateFolder," + IntermediateFolder);
            sb.AppendLine("NHDPlusLocation," + NHDPlusLocation);
            sb.AppendLine("NLCDLocation," + NLCDLocation);
            sb.AppendLine("SoilsLocation," + SoilsLocation);
            sb.AppendLine("NHDPlusDisregardCache," + NHDPlusDisregardCache);
            sb.AppendLine("NLCDDisregardCache," + NLCDDisregardCache);
            sb.AppendLine("SoilsDisregardCache," + SoilsDisregardCache);
            sb.AppendLine("Server," + Server);
            sb.AppendLine("Username," + Username);
            sb.AppendLine("Password," + Password);
            sb.AppendLine("Port," + Port);
            sb.AppendLine("LengthOfTimeout," + LengthOfTimeout);
            sb.AppendLine("NumberOfRetries," + NumberOfRetries);
            sb.AppendLine("DatabaseName," + DatabaseName);
            sb.AppendLine("ForceFullDependency," + ForceFullDependency);
            AppendList("SourceTypes", SourceTypes, sb);
            AppendList("ScienceModules", ScienceModules, sb);
       
            return sb.ToString();
        }


    }
}
