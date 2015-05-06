using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using atcUtility;

namespace D4EMSystemTesting
{
    public class testNWIS
    {
        public bool testingNWIS(string aProjectFolder, double aNorth, double aSouth, double aEast, double aWest, string dataType)
        {
            bool pass = false;
            string aProjectFolderNWIS = System.IO.Path.Combine(aProjectFolder, "NWIS");
            string aCacheFolderNWIS = System.IO.Path.Combine(aProjectFolderNWIS, "Cache");
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNWIS, aProjectFolderNWIS, aRegion, true, true);
            string aSaveFolder = dataType + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
            string aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, "NWISstations");
            List<string> stationIDs = new List<string>();
            D4EM.Data.LayerSpecification dt = new D4EM.Data.LayerSpecification();
            try
            {               
                switch (dataType)
                {
                    case "Daily Discharge":
                        dt = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, dt);
                        stationIDs = getStationIDList(aSaveAs);
                        aSaveFolder = dataType + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, dataType);
                        D4EM.Data.Source.NWIS.GetDailyDischarge(aProject, aSaveFolder, stationIDs);
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);                        
                        break;
                    case "IDA Discharge":
                        dt = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, dt);
                        stationIDs = getStationIDList(aSaveAs);
                        aSaveFolder = dataType + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, dataType);
                        D4EM.Data.Source.NWIS.GetIDADischarge(aProject, aSaveFolder, stationIDs);
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);                        
                        break;
                    case "Measurement":
                        dt = D4EM.Data.Source.NWIS.LayerSpecifications.Measurement;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, dt);
                        stationIDs = getStationIDList(aSaveAs);
                        aSaveFolder = dataType + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, dataType);
                        D4EM.Data.Source.NWIS.GetMeasurements(aProject, aSaveFolder, stationIDs);
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);                        
                        break;
                    case "Water Quality":
                        dt = D4EM.Data.Source.NWIS.LayerSpecifications.WaterQuality;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, dt);
                        stationIDs = getStationIDList(aSaveAs);
                        aSaveFolder = dataType + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, dataType);
                        D4EM.Data.Source.NWIS.GetWQ(aProject, aSaveFolder, stationIDs);
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);                        
                        break;
                }
            }
            catch (Exception ex)
            {
            }
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
            if (Directory.Exists(aCacheFolderNWIS))
            {
                DirectoryInfo aCacheFolder = new DirectoryInfo(aCacheFolderNWIS);
                foreach (System.IO.FileInfo file in aCacheFolder.GetFiles()) file.Delete();
                foreach (System.IO.DirectoryInfo subDirectory in aCacheFolder.GetDirectories()) subDirectory.Delete(true);
            }
            return pass;
        }
        private List<string> getStationIDList(string aSaveAs)
        {
            List<string> aStationIDs = new List<string>();
            atcTableRDB atctable = new atcTableRDB();
            atctable.OpenFile(aSaveAs);
            int fieldnumber = atctable.FieldNumber("site_no");
            int numrecords = atctable.NumRecords;
            atctable.MoveFirst();
            for (int i = 0; i < numrecords; i++)
            {
                string stationID = atctable.get_Value(fieldnumber);
                atctable.MoveNext();
                aStationIDs.Add(stationID);
            }
            return aStationIDs;
        }
    }
}
