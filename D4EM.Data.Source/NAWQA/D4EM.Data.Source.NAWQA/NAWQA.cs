using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using DotSpatial.Projections;
using DotSpatial.Data;
using System.Data;

namespace D4EM.Data.Source
{
    public class NAWQA
    {
        public string fileName;
        public string[] counties;
        public string aProjectFolderNAWQA;
        public string aCacheFolderNAWQA;
        public string subFolder;
        public string state;
        public string _waterType;
        public string waterTypeAbbrev;
        public string _fileType;
        public string _queryType;
        public string fileExt;
        public DataTable dtAverages;

        public static string countyShapeFile = Path.GetFullPath(@"..\Bin\County\cnty.shp");

        public NAWQA(string aProjectFolder, string aCacheFolder, string waterType, string fileType, string queryType, string countyShapeFilePath)
        {
            if (countyShapeFilePath != "")
            {
                countyShapeFile = countyShapeFilePath;
            }
            aProjectFolderNAWQA = aProjectFolder;
            aCacheFolderNAWQA = aCacheFolder;
            _queryType = queryType;
            subFolder = "";
            state = "";
            if (waterType == "groundwater")
            {
                _waterType = "groundwater";
                subFolder = subFolder + "GW";
                waterTypeAbbrev = "GW";
            }
            else if (waterType == "surfacegroundwater")
            {
                _waterType = "surfacegroundwater";
                subFolder = subFolder + "GW&SW";
                waterTypeAbbrev = "GW&SW";
            }

            if (fileType == "excel")
            {
                _fileType = "excel";
                fileExt = "xls";
            }
            else if (fileType == "tab")
            {
                _fileType = "tab";
                fileExt = "tsv";
            }
            else
            {
                _fileType = fileType;
                fileExt = fileType;
            }

        }

        public NAWQA(string aProjectFolder, string aCacheFolder, string waterType, string fileType, string queryType)
        {
            aProjectFolderNAWQA = aProjectFolder;
            aCacheFolderNAWQA = aCacheFolder;
            _queryType = queryType;
            subFolder = "";
            state = "";
            if (waterType == "groundwater")
            {
                _waterType = "groundwater";
                subFolder = subFolder + "GW";
                waterTypeAbbrev = "GW";
            }
            else if (waterType == "surfacegroundwater")
            {
                _waterType = "surfacegroundwater";
                subFolder = subFolder + "GW&SW";
                waterTypeAbbrev = "GW&SW";
            }

            if (fileType == "excel")
            {
                _fileType = "excel";
                fileExt = "xls";
            }
            else if (fileType == "tab")
            {
                _fileType = "tab";
                fileExt = "tsv";
            }
            else
            {
                _fileType = fileType;
                fileExt = fileType;
            }

        }

        private string doNAWQADownload(string state, string county, string parameter, string startYear = "", string endYear = "")
        {
            string url = "http://infotrek.er.usgs.gov/nawqa_webservices/service/" + _waterType + "/" + _fileType + "/" + _queryType + "/read?" + "state=" + state + "&county=" + county;
            string fileName = "";
            subFolder = state + "(" + county + ") " + waterTypeAbbrev;
            Directory.CreateDirectory(Path.Combine(aProjectFolderNAWQA, subFolder));
            if (parameter != "")
            {
                string par_number = parameter.Substring(0, 5);
                url = url + "&parameterCode=" + par_number;
                fileName = parameter;
            }
            else
            {
                fileName = "AllParameters";
            }
            
            if ((startYear != "") && (endYear != ""))
            {
                url = url + "&waterYearStart=" + startYear + "&waterYearEnd=" + endYear;
            }

            string cacheFile = Path.Combine(aCacheFolderNAWQA, subFolder, fileName + "." + fileExt);
            string fileNameFullPath = Path.Combine(aProjectFolderNAWQA, subFolder, fileName + "." + fileExt);
            if (checkCache(cacheFile))
            {
                if (!File.Exists(fileNameFullPath))
                {
                    File.Copy(cacheFile, fileNameFullPath);
                }
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(aCacheFolderNAWQA, subFolder));
                WebClient wc = new WebClient();
                try
                {
                    wc.DownloadFile(url, cacheFile);
                }
                catch (Exception ex)
                {
                    int i = 1;
                }
                if (!File.Exists(fileNameFullPath))
                {
                    File.Copy(cacheFile, fileNameFullPath);
                }
            }

            string metadataFileName = Path.Combine(aProjectFolderNAWQA, subFolder, "Metadata.txt");
            writeMetaDataFile(metadataFileName, county, state, parameter, startYear, endYear, url);
            return fileNameFullPath;
        }

        private void writeMetaDataFile(string metaDataFileName, string county, string state, string parameter, string startYear, string endYear, string url)
        {
            if (!File.Exists(metaDataFileName))
            {
                TextWriter tw = new StreamWriter(metaDataFileName);
                tw.WriteLine("NAWQA Metadata for " + county + ", " + state + " (" + _waterType + ")");
                tw.WriteLine();
                tw.Write("Data for " + parameter);
                if ((startYear != "") && (endYear != ""))
                {
                    tw.Write(" in the time interval " + startYear + " to " + endYear);
                }
                tw.WriteLine(" was downloaded on " + DateTime.Now + " from " + url);
                tw.WriteLine();
                tw.Close();
            }
            else
            {
                using (StreamWriter tw = File.AppendText(metaDataFileName))
                {
                    tw.Write("Data for " + parameter);
                    if ((startYear != "") && (endYear != ""))
                    {
                        tw.Write(" in the time interval " + startYear + " to " + endYear);
                    }
                    tw.WriteLine(" was downloaded on " + DateTime.Now + " from " + url);
                    tw.WriteLine();
                    tw.Close();
                }	

            }
        }

        private bool checkCache(string cacheFile)
        {
            bool fileExists = false;
            if (File.Exists(cacheFile))
            {
                fileExists = true;
            }
            return fileExists;
        }

        public string[] getAllDataLatLong(double lat, double lng, string[] parameters, string startYear = "", string endYear = "")
        {
            string[] fileNames = new string[parameters.Length];
           
            string[] countyState = getCountyStateFromLatLong(lat, lng);
            string stateName = longStateName(countyState[1]);
            string[] countyNameFull = countyState[0].Split(' ');
            string countyName = countyNameFull[0].ToUpper();
            counties = new string[1];
            counties[0] = countyName;
            state = stateName;
            int k = 0;
            if (parameters.Length > 0)
            {
                string[,] parameterFileNames = new string[counties.Length, parameters.Length];
                string _fileName = fileName;
                
                foreach (string parameter in parameters)
                {
                
                    fileNames[k] = doNAWQADownload(stateName, countyName, parameter, startYear, endYear);
                    k++;
                }
            }
            else
            {
                fileNames[k] = doNAWQADownload(stateName, countyName, "", startYear, endYear);
                k++;
            }
            return fileNames;
        }


        public string[] getAllDataStateCounties(string state, string[] counties, string[] parameters, string startYear = "", string endYear = "")
        {
            string[] fileNames = new string[parameters.Length];
            foreach (string county in counties)
            {                
                int k = 0;
                if (parameters.Length > 0)
                {
                    foreach (string parameter in parameters)
                    {
                        fileNames[k] = fileNames[k] = doNAWQADownload(state, county, parameter, startYear, endYear);
                        k++;
                    }
                }
                else
                {
                    fileNames[k] = fileNames[k] = doNAWQADownload(state, county, "", startYear, endYear);
                    k++;
                }
            }
            return fileNames;
        }

        public string[] getAllDataCountiesFromMap(string[] counties, string[] parameters, string startYear = "", string endYear = "")
        {
            string[] fileNames = new string[parameters.Length * counties.Length];
           
            int i_county = 1;
            int k = 0;
            foreach (string countyLong in counties)
            {
                string[] values = countyLong.Split(',');
                string county = values[0].ToString().Trim();
                state = values[1].ToString().Trim();
                counties[i_county - 1] = county;
               
                i_county++;

                if (parameters.Length > 0)
                {
                    foreach (string parameter in parameters)
                    {
                        fileNames[k] = doNAWQADownload(state, county, parameter, startYear, endYear);
                        k++;
                    }
                }
                else
                {
                    fileNames[k] = doNAWQADownload(state, county, "", startYear, endYear);
                    k++;
                }
            }            
            return fileNames;
        }

        private string writeAverageAndStdDevFile(string state, string county, string[] parameters, string startYear = "", string endYear = "")
        {
            subFolder = state + "(" + county + ") " + waterTypeAbbrev;
            Directory.CreateDirectory(Path.Combine(aProjectFolderNAWQA, subFolder));
            fileName = Path.Combine(aProjectFolderNAWQA, subFolder, "Averages&StdDev.txt");
            string cacheFile = Path.Combine(aCacheFolderNAWQA, subFolder, "Averages&StdDev.txt");   
            Directory.CreateDirectory(Path.Combine(aCacheFolderNAWQA, subFolder));     
            TextWriter tw = new StreamWriter(fileName);
            foreach (string par in parameters)
            {
                string parameter = par.ToString();
                string par_number = parameter.Substring(0, 5);
                string url = "http://infotrek.er.usgs.gov/nawqa_webservices/service/" + _waterType + "/tab/" + _queryType + "/read?" + "state=" + state + "&county=" + county + "&parameterCode=" + par_number;
                if ((startYear != "") && (endYear != ""))
                {
                    url = url + "&waterYearStart=" + startYear + "&waterYearEnd=" + endYear;
                }
                string tempFileName = @"C:\Temp\tempNAWQAtsv.tsv";
                WebClient wc = new WebClient();
                wc.DownloadFile(url, tempFileName);
                string[] averageStdDev = determineAverage(tempFileName, county);
                string average = averageStdDev[0];
                string stdDev = averageStdDev[1];
                string numObs = averageStdDev[2];
                File.Delete(tempFileName);
                tw.WriteLine("County: " + county + ", " + state);
                tw.WriteLine("Parameter: " + parameter);
                tw.WriteLine("Average: " + average);
                tw.WriteLine("Standard Deviation: " + stdDev);
                tw.WriteLine("# Observations: " + numObs);
                tw.WriteLine();
                //  tw.WriteLine(county + ", " + state + "\t" + parameter + "\t" + average + "\t" + stdDev + "\t" + numObs);
                dtAverages.Rows.Add(county + ", " + state, parameter, average, stdDev, numObs);
            }
            tw.Close();
            if (!File.Exists(cacheFile))
            {
                File.Copy(fileName, cacheFile);
            }
            return fileName;
        }

        public string[] getAverageAndStandardDeviationStateCounties(string state, string[] counties, string[] parameters, string startYear = "", string endYear = "")
        {
            dtAverages = new DataTable();
            dtAverages.Columns.Add("County");
            dtAverages.Columns.Add("Parameter");
            dtAverages.Columns.Add("Average Value");
            dtAverages.Columns.Add("Standard Deviation");
            dtAverages.Columns.Add("# Observations");
            
            string[] fileNames = new string[counties.Length*parameters.Length];    
            int k = 0;
            foreach (string county in counties)
            {
                fileNames[k] = writeAverageAndStdDevFile(state, county, parameters, startYear, endYear);
                k++;                
            }           
            return fileNames;
        }

        public string[] getAverageAndStandardDeviationCountiesFromMap(string[] counties, string[] parameters, string startYear = "", string endYear = "")
        {
            string[] fileNames = new string[counties.Length];

            int k = 0;
            dtAverages = new DataTable();
            dtAverages.Columns.Add("County");
            dtAverages.Columns.Add("Parameter");
            dtAverages.Columns.Add("Average Value");
            dtAverages.Columns.Add("Standard Deviation");
            dtAverages.Columns.Add("# Observations");

            foreach (string countyLong in counties)
            {
                string[] values = countyLong.Split(',');
                string county = values[0].ToString().Trim();
                state = values[1].ToString().Trim();

                fileNames[k] = writeAverageAndStdDevFile(state, county, parameters, startYear, endYear);
                k++; 
            }
            return fileNames;
        }


        public string getAverageAndStandardDeviationLatLong(double lat, double lng, string[] parameters, string startYear = "", string endYear = "")
        {
            string fileName = "";
            string[] countyState = getCountyStateFromLatLong(lat, lng);

            string stateName = longStateName(countyState[1]);
            string[] countyNameFull = countyState[0].Split(' ');
            string countyName = countyNameFull[0].ToUpper();
            counties = new string[1];
            counties[0] = countyName;
            state = stateName; 
         
            dtAverages = new DataTable();
            dtAverages.Columns.Add("County");
            dtAverages.Columns.Add("Parameter");
            dtAverages.Columns.Add("Average Value");
            dtAverages.Columns.Add("Standard Deviation");
            dtAverages.Columns.Add("# Observations");

            fileName = writeAverageAndStdDevFile(state, countyName, parameters, startYear, endYear);

            
            return fileName;
        }

        private static string[] getCountyStateFromLatLong(double lat, double lng)
        {
            DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(lng, lat);
            DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
            string[] countyState = new string[2];
            string countyShapeFile = @"..\Bin\County\cnty.shp";
            IFeatureSet fs = FeatureSet.OpenFile(countyShapeFile);
            ProjectionInfo proj = new ProjectionInfo();
            proj = fs.Projection;
            fs.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            int count = fs.Features.Count;
            for (int i = 0; i < count; i++)
            {
                IFeature countyFeature = fs.GetFeature(i);

                if (countyFeature.Intersects(coords) == true)
                {
                    countyState[0] = countyFeature.DataRow[2].ToString();
                    countyState[1] = countyFeature.DataRow[1].ToString();
                    break;
                }
            }
            fs.Close();
            return countyState;
        }

        private static string longStateName(string abb)
        {
            string longStateName = "";
            abb = abb.ToUpper();
            switch (abb)
            {
                case "AL":
                    longStateName = "ALABAMA";
                    break;
                case "AK":
                    longStateName = "ALASKA";
                    break;
                case "AZ":
                    longStateName = "ARIZONA";
                    break;
                case "AR":
                    longStateName = "ARKANSAS";
                    break;
                case "CA":
                    longStateName = "CALIFORNIA";
                    break;
                case "CO":
                    longStateName = "COLORADO";
                    break;
                case "CT":
                    longStateName = "CONNECTICUT";
                    break;
                case "DE":
                    longStateName = "DELAWARE";
                    break;
                case "DC":
                    longStateName = "DISTRICT OF COLUMBIA";
                    break;
                case "FL":
                    longStateName = "FLORIDA";
                    break;
                case "GA":
                    longStateName = "GEORGIA";
                    break;
                case "HI":
                    longStateName = "HAWAII";
                    break;
                case "ID":
                    longStateName = "IDAHO";
                    break;
                case "IL":
                    longStateName = "ILLINOIS";
                    break;
                case "IN":
                    longStateName = "INDIANA";
                    break;
                case "IA":
                    longStateName = "IOWA";
                    break;
                case "KS":
                    longStateName = "KANSAS";
                    break;
                case "KY":
                    longStateName = "KENTUCKY";
                    break;
                case "LA":
                    longStateName = "LOUISIANA";
                    break;
                case "ME":
                    longStateName = "MAINE";
                    break;
                case "MD":
                    longStateName = "MARYLAND";
                    break;
                case "MA":
                    longStateName = "MASSACHUSETTS";
                    break;
                case "MI":
                    longStateName = "MICHIGAN";
                    break;
                case "MN":
                    longStateName = "MINNESOTA";
                    break;
                case "MS":
                    longStateName = "MISSISSIPPI";
                    break;
                case "MO":
                    longStateName = "MISSOURI";
                    break;
                case "MT":
                    longStateName = "MONTANA";
                    break;
                case "NE":
                    longStateName = "NEBRASKA";
                    break;
                case "NV":
                    longStateName = "NEVADA";
                    break;
                case "NH":
                    longStateName = "NEW HAMPSHIRE";
                    break;
                case "NJ":
                    longStateName = "NEW JERSEY";
                    break;
                case "NM":
                    longStateName = "NEW MEXICO";
                    break;
                case "NY":
                    longStateName = "NEW YORK";
                    break;
                case "NC":
                    longStateName = "NORTH CAROLINA";
                    break;
                case "ND":
                    longStateName = "NORTH DAKOTA";
                    break;
                case "OH":
                    longStateName = "OHIO";
                    break;
                case "OK":
                    longStateName = "OKLAHOMA";
                    break;
                case "OR":
                    longStateName = "OREGON";
                    break;
                case "PA":
                    longStateName = "PENNSYLVANIA";
                    break;
                case "RI":
                    longStateName = "RHODE ISLAND";
                    break;
                case "SC":
                    longStateName = "SOUTH CAROLINA";
                    break;
                case "SD":
                    longStateName = "SOUTH DAKOTA";
                    break;
                case "TN":
                    longStateName = "TENNESSEE";
                    break;
                case "TX":
                    longStateName = "TEXAS";
                    break;
                case "UT":
                    longStateName = "UTAH";
                    break;
                case "VT":
                    longStateName = "VERMONT";
                    break;
                case "VA":
                    longStateName = "VIRGINIA";
                    break;
                case "WA":
                    longStateName = "WASHINGTON";
                    break;
                case "WV":
                    longStateName = "WEST VIRGINIA";
                    break;
                case "WI":
                    longStateName = "WISCONSIN";
                    break;
                case "WY":
                    longStateName = "WYOMING";
                    break;
            }

            return longStateName;
        }

        private string[] determineAverage(string originalFile, string county)
        {
            DataTable dt = new DataTable();
            TextReader tr = new StreamReader(originalFile);  //tsv
            string line = tr.ReadLine();
            string[] columnNames = line.Split('\t');
            foreach (string columnName in columnNames)
            {
                dt.Columns.Add(columnName);
            }

            while ((line = tr.ReadLine()) != null)
            {
                string[] cellValues = line.Split('\t');
                dt.Rows.Add(cellValues);
            }
            tr.Close();

            DataRow[] result = dt.Select("county = '" + county + "'");

            string[] returnValues = new string[3];

            if (result.Length == 0)
            {
                returnValues[0] = "No Data";
                returnValues[1] = "";
                returnValues[2] = "";
                return returnValues;
            }
            else
            {
                List<double> values = new List<double>();

                double stdDev = 0;
               
                double sum = 0;
                string units = "";
                for (int i = 0; i < result.Length; i++)
                {
                    double value = Convert.ToDouble(result[i]["value"].ToString());
                    units = result[i]["reportUnits"].ToString();
                    sum = sum + value;
                    values.Add(value);
                }
                double average = sum / result.Length;
                string average_string = average.ToString() + " " + units;

                IEnumerable<double> en = values;
                stdDev = CalculateStdDev(en);
                string stdDev_string = stdDev.ToString() + " " + units;
                returnValues[0] = average_string;
                returnValues[1] = stdDev_string;
                returnValues[2] = values.Count.ToString();
                return returnValues;
            }

        }
        
        private double CalculateStdDev(IEnumerable<double> values) 
        {      
            double ret = 0;   
            if (values.Count() > 0)    
            {               
                double avg = values.Average();              
                double sum = values.Sum(d => Math.Pow(d - avg, 2));     
                ret = Math.Sqrt((sum) / (values.Count()-1));      
            }      
            return ret; 
        } 
    }
}
