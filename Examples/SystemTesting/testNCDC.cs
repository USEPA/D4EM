using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using D4EM.Data.Source;
using System.Data;

namespace D4EMSystemTesting
{
    public class testNCDC
    {
        public bool testingNCDCStations(string aProjectFolder, string state, string token)
        {
            DataTable dtStations = new DataTable();
            DataTable dtVariables = new DataTable();
            bool pass = false;
            
            EPAUtility.NCDCSupport ncdc = new EPAUtility.NCDCSupport();
            try
            {
                dtStations = ncdc.populateStationsTable(token, state);
                DataTable datat = new DataTable();
                EPAUtility.NCDCSupport temp = new EPAUtility.NCDCSupport();
                dtVariables = temp.createVariablesTable();
            }
            catch (Exception ex)
            {
                
            }
            if ((dtStations.Rows.Count > 0) && (dtVariables.Rows.Count > 0))
            {
                pass = true;
            }
            else
            {
                pass = false;
            }            
            return pass;
        }
        public bool testingNCDCValues(string aProjectFolder, string stationID, string stationName, string variableID, string variableName, double latitude, double longitude, string datasetType, string outputType, string yearStart, string monthStart, string dayStart, string yearEnd, string monthEnd, string dayEnd, string token)
        {
            bool pass = false;
            string aProjectFolderNCDC = System.IO.Path.Combine(aProjectFolder, "NCDC");
            string aCacheFolderNCDC = System.IO.Path.Combine(aProjectFolderNCDC, "Cache");   

            if (monthStart.Length == 1)
            {
                monthStart = "0" + monthStart;
            }            
            if (dayStart.Length == 1)
            {
                dayStart = "0" + dayStart;
            }           
            if (monthEnd.Length == 1)
            {
                monthEnd = "0" + monthEnd;
            }            
            if (dayEnd.Length == 1)
            {
                dayEnd = "0" + dayEnd;
            }

            string startDate = yearStart + monthStart + dayStart;
            string endDate = yearEnd + monthEnd + dayEnd;

            int start = Convert.ToInt32(startDate);
            int end = Convert.ToInt32(endDate);
            try
            {
                EPAUtility.NCDCSupport ncdcSupport = new EPAUtility.NCDCSupport(token, aProjectFolderNCDC, stationID, stationName, variableID, variableName, datasetType, outputType, start, end);
                ncdcSupport.WriteNCDCValuesFile();
            }
            catch (Exception ex)
            {
            }
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNCDC, stationID + "_" + variableID);
            if (Directory.Exists(aSubFolder))
            {
                string[] filesinDirectory = Directory.GetFiles(aSubFolder);
                int numFiles = filesinDirectory.Length;
                if (numFiles >= 1)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
            }
            return pass;
        }
    }
}
