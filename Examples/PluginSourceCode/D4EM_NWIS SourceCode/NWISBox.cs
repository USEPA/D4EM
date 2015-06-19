using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using D4EM.Data;
using D4EM.Data.Source;
using D4EM.Geo;
using System.Collections;
using System.IO;
using System.IO.Compression;
using atcUtility;
using System.Xml;
using D4EMPlugins;
using DotSpatial.Data;
using System.Data;
using DotSpatial.Projections;

namespace D4EM_NWIS
{
    public partial class NWISBox : Form
    {
        int countFiles = 0;
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;
       
        string aProjectFolderNWIS = "";
        string aCacheFolderNWIS;
        List<string> stationIDs = new List<string>();

        public NWISBox(double north, double south, double east, double west, List<string> stationIds)
        {
            InitializeComponent();
            _north = north;
            _south = south;
            _east = east;
            _west = west;

            stationIDs = stationIds;
            foreach (string station in stationIDs)
            {
                listStationIDs.Items.Add(station);
            }
            for (int i = 0; i < listStationIDs.Items.Count; i++)
            {
                listStationIDs.SetItemChecked(i, true);
            }
            if (stationIds.Count > 0)
            {
                groupNWIShistorical.Visible = true;
            }
        }

        private void NWISBox_Load(object sender, EventArgs e)
        {
          
            txtNorthNWIS.Text = _north.ToString("#.##");
            txtSouthNWIS.Text = _south.ToString("#.##");
            txtEastNWIS.Text = _east.ToString("#.##");
            txtWestNWIS.Text = _west.ToString("#.##");            
           
            txtProjectNWIS.Text = "C:\\Temp\\ProjectFolderNWIS";
            File.Delete(@"C:\Temp\DownloadedFilePathNWIS");
           
        }

        
        private void btnRunNWIS_Click(object sender, EventArgs e)
        {
            if (listNWIS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one or more data types");
                return;
            }

            if (txtNorthNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }


            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double aNorth = Convert.ToDouble(txtNorthNWIS.Text.Trim());
            double aSouth = Convert.ToDouble(txtSouthNWIS.Text.Trim());
            double aWest = Convert.ToDouble(txtWestNWIS.Text.Trim()); ;
            double aEast = Convert.ToDouble(txtEastNWIS.Text.Trim());
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNWIS");

            string aProjectFolderNWIS = System.IO.Path.Combine(txtProjectNWIS.Text.Trim(), "NWIS");
            string aCacheFolderNWIS = @"C:\Temp\cachenwis";
            string aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, "NWISstations");

            string fileLocationsText = "Downloaded NWIS files for North = " + aNorth + ", South = " + aSouth + ", East = " + aEast + ", West = " + aWest + " are located in " + aProjectFolderNWIS + Environment.NewLine;
            fileLocationsText = fileLocationsText + Environment.NewLine;

            try
            {
                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNWIS, aProjectFolderNWIS, aRegion, false, false);

                List<string> aStationIDs = new List<string>();

               // D4EM.Data.Source.NWIS nwis = new NWIS();

                D4EM.Data.LayerSpecification aStationDataType = new LayerSpecification();

                string logfilename = System.IO.Path.Combine(aProjectFolderNWIS, "_LogFile.txt");
                fileLocationsText = fileLocationsText + "Metadata file: " + logfilename + Environment.NewLine;
                bool logfileExists = false;
                string subFolder = "";
                foreach (object adatatype in listNWIS.CheckedItems)
                {
                    string datatype = adatatype.ToString();
                    string aSaveFolder = "";
                    switch (datatype)
                    {
                        case "Daily Discharge":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                            aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                            aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder, "NWISstations.txt");
                            D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                            EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                            writeFilePathsFile(aProjectFolderNWIS, aSaveFolder, fileShpTif);
                            aStationIDs = getStationIDList(aSaveAs);
                            D4EM.Data.Source.NWIS.GetDailyDischarge(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);    
                            break;
                        case "IDA Discharge":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                            aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                            aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder, "NWISstations.txt");
                            D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                            EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                            writeFilePathsFile(aProjectFolderNWIS, aSaveFolder, fileShpTif);
                            aStationIDs = getStationIDList(aSaveAs);
                            D4EM.Data.Source.NWIS.GetIDADischarge(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);   
                            break;
                        case "Measurement":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Measurement;
                            aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                            aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder, "NWISstations.txt");
                            D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                            EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                            writeFilePathsFile(aProjectFolderNWIS, aSaveFolder, fileShpTif);
                            aStationIDs = getStationIDList(aSaveAs);
                            D4EM.Data.Source.NWIS.GetMeasurements(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);   
                            break;
                        case "Water Quality":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.WaterQuality;
                            aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                            aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder, "NWISstations.txt");
                            D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);                            
                            EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                            writeFilePathsFile(aProjectFolderNWIS, aSaveFolder, fileShpTif);
                            aStationIDs = getStationIDList(aSaveAs);
                            D4EM.Data.Source.NWIS.GetWQ(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);   
                            EPAUtility.NWISFileSupport nwiswq = new EPAUtility.NWISFileSupport();
                            nwiswq.convertRDBtoCSV(subFolder);
                            foreach (object spdt in listNWISDataTypesSpecific.CheckedItems)
                            {
                                string dtype = spdt.ToString();
                                EPAUtility.NWISFileSupport nfs = new EPAUtility.NWISFileSupport();
                                nfs.writeNWISfilesWithSpecificDataParameters(subFolder, dtype);
                            }
                            break;
                    }
                    logfileExists = true;
                }
                labelNWIS.Visible = true;
                labelNWIS.Text = "Downloaded data is located in " + aProjectFolderNWIS;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NWIS File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNWISLoadDataToMap.Visible = true;
                }
            }
            catch (ApplicationException ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            

            TextWriter tw = new StreamWriter(@"C:\Temp\NWISdirectoryname");
            tw.WriteLine(aProjectFolderNWIS);
            tw.Close();
            fileShpTif.Close();

            DirectoryInfo aCacheFolder = new DirectoryInfo(aCacheFolderNWIS);
            Directory.CreateDirectory(aCacheFolderNWIS);
            foreach (System.IO.FileInfo file in aCacheFolder.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in aCacheFolder.GetDirectories()) subDirectory.Delete(true); 
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
                aStationIDs.Add(stationID);
                atctable.MoveNext();
               
            }
            return aStationIDs;
        }
       
        private void writeFilePathsFile(string aProjectFolder, string aSaveFolder, TextWriter fileShpTif)
        {
            string subFolder = System.IO.Path.Combine(aProjectFolder, aSaveFolder);
            if (Directory.Exists(subFolder))
            {
                string[] shp = Directory.GetFiles(subFolder, "*.shp", SearchOption.AllDirectories);
                int i = 0;
                while (i < shp.Length)
                {
                    if (File.Exists(shp[i]))
                    {
                        fileShpTif.WriteLine(shp[i]);
                    }
                    i++;
                }
            }            
        }


        private void btnDowloadNWISUsingStationIds_Click(object sender, EventArgs e)
        {
             if (listNWIS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one or more data types");
                return;
            }

            if (listNWISStations.Items.Count == 0)
            {
                MessageBox.Show("Please select one or more Station IDs");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double aNorth = 0;
            double aSouth = 0;
            double aWest = 0;
            double aEast = 0;

            string aProjectFolderNWIS = System.IO.Path.Combine(txtProjectNWIS.Text.Trim(), "NWIS");
            string aCacheFolderNWIS = @"C:\Temp\cachenwis";
            string aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, "NWISstations");
            string fileLocationsText = "NWIS FILE LOCATIONS" + Environment.NewLine;
            fileLocationsText = fileLocationsText + Environment.NewLine;
             try
            {
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNWIS, aProjectFolderNWIS, aRegion, false, false);

                
            List<string> aStationIDs = new List<string>();
            foreach (object aStationID in listNWISStations.Items)
            {
                string stationID = aStationID.ToString();
                aStationIDs.Add(stationID);
            }
            D4EM.Data.LayerSpecification aStationDataType = new LayerSpecification();

            string logfilename = System.IO.Path.Combine(aProjectFolderNWIS, "_LogFile.txt");
            bool logfileExists = false;

            foreach (object adatatype in listNWIS.CheckedItems)
            {
                string datatype = adatatype.ToString();
                string aSaveFolder = "";
                string subFolder = "";
                switch (datatype)
                {
                    case "Daily Discharge":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge; 
                        aSaveFolder = datatype + " " + "StationIDs";
                        D4EM.Data.Source.NWIS.GetDailyDischarge(aProject, aSaveFolder, aStationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                        break;
                    case "IDA Discharge":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge; 
                        aSaveFolder = datatype + " " + "StationIDs";                        
                        D4EM.Data.Source.NWIS.GetIDADischarge(aProject, aSaveFolder, aStationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                        break;
                    case "Measurement":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Measurement;
                        aSaveFolder = datatype + " " + "StationIDs";
                        D4EM.Data.Source.NWIS.GetMeasurements(aProject, aSaveFolder, aStationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                        break;
                    case "Water Quality":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.WaterQuality;
                        aSaveFolder = datatype + " " + "StationIDs";
                        D4EM.Data.Source.NWIS.GetWQ(aProject, aSaveFolder, aStationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                        break;
                }
                logfileExists = true;
            }
            }
            catch(ApplicationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            labelNWIS.Visible = true;
            labelNWIS.Text = "Downloaded data is located in " + aProjectFolderNWIS;
            MessageBox.Show(fileLocationsText);

            DirectoryInfo aCacheFolder = new DirectoryInfo(aCacheFolderNWIS);
            foreach (System.IO.FileInfo file in aCacheFolder.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in aCacheFolder.GetDirectories()) subDirectory.Delete(true); 
        }

        private void btnAddStations_Click(object sender, EventArgs e)
        {
            listNWISStations.Items.Add(txtNWISStation.Text.Trim());
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            while (listNWISStations.SelectedItems.Count > 0)
            {
                listNWISStations.Items.Remove(listNWISStations.SelectedItems[0]);
            }
        }

        private void btnBrowseProjectNWIS_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectNWIS.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNWIS = this.txtProjectNWIS.Text;
            }
        }
        private string addShpFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
                foreach (string file in shapefiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " shapefile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addRDBFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] rdbfiles = Directory.GetFiles(aSubFolder, "*.rdb", SearchOption.AllDirectories);
                foreach (string file in rdbfiles)
                {
                    DateTime creationTime = File.GetLastAccessTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " rdb file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private void writeLogFile(string aSubFolder, string logfilename, bool logfileExists, string dataType)
        {
            if (Directory.Exists(aSubFolder))
            {
                string[] xmlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                if (xmlfiles.Length > 0)
                {
                    string xmlfilename = xmlfiles[0];
                    if (File.Exists(xmlfilename) == true)
                    {
                        ReadXMLandWriteLog readWriteHydrologicUnitsSubBasinPolygons = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, dataType);
                    }
                }
            }
        }

        private void btnNWISLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listNWIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listNWIS.SelectedItem.ToString().Equals("Water Quality"))
            {               
                groupNWISspecific.Visible = true;
            }
        }

        private void btnNWIShistoricalData_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            aProjectFolderNWIS = txtProjectNWIS.Text.Trim();
            foreach (object stationId in listStationIDs.CheckedItems)
            {
                string stationID = stationId.ToString();
                string aSaveFolder = "Historical Data for Station " + stationID;
                string folder = Path.Combine(aProjectFolderNWIS, aSaveFolder);
                EPAUtility.NWISFileSupport nwis = new EPAUtility.NWISFileSupport();
                string fullName = nwis.getStationName(aProjectFolderNWIS, stationID);
                DataTable dt = nwis.getDischargeData(aProjectFolderNWIS, stationID);

                NWISchart chart = new NWISchart(dt);
                chart.Text = stationID + " - " + fullName;
                chart.Show();
            }
            this.Cursor = StoredCursor;
        }
        
    }
}
