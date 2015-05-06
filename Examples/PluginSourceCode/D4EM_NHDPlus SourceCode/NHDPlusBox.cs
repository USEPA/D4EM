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
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls.Docking;
using DotSpatial.Projections;
using DotSpatial.Topology;
using System.Data;

namespace D4EM_NHDPlus
{
   partial class NHDPlusBox : Form
    {
       int countFiles = 0;
       List<string> hucnums = null;
       string aProjectFolderNHDPlus = "";
       string aCacheFolderNHDPlus;
       double _lng = 0;
       double _lat = 0;
       List<string> streamIDs = new List<string>();

        public NHDPlusBox(List<string> huc8nums, double lng, double lat, List<string> streams)
        {
            hucnums = null;
            InitializeComponent();
            hucnums = huc8nums;
            _lat = lat;
            _lng = lng;

            foreach (string stream in streams)
            {
                listStreamIDs.Items.Add(stream);
            }
            for (int i = 0; i < listStreamIDs.Items.Count; i++)
            {
                listStreamIDs.SetItemChecked(i, true);
            }
            if (streams.Count > 0)
            {
                groupStreamIDs.Visible = true;
            }
        }

        private void btnFindHuc8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo);
        }

        

        private void NHDPlus_Load(object sender, EventArgs e)
        {
            txtLatitude.Text = _lat.ToString();
            txtLongitude.Text = _lng.ToString();
            txtProject.Text = @"C:\Temp\ProjectFolderEPADelination";
            txtCache.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            listHuc8NHDPlus.Items.Clear();
            foreach (string huc8 in hucnums)
            {
                listHuc8NHDPlus.Items.Add(huc8);
            }

            txtCacheFolder.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            txtProjectFolderNHDPlus.Text = "C:\\Temp\\ProjectFolderNHDPlus";
            File.Delete(@"C:\Temp\DownloadedFilePathNHDPlus");
        }

        private void btnRemoveNHDPlus_Click_1(object sender, EventArgs e)
        {
            while (listHuc8NHDPlus.SelectedItems.Count > 0)
            {
                listHuc8NHDPlus.Items.Remove(listHuc8NHDPlus.SelectedItems[0]);
            }
        }

        private void btnAddHuc8NHDPlus_Click_1(object sender, EventArgs e)
        {
            listHuc8NHDPlus.Items.Add(txtHUC8NHDPlus.Text.Trim());
        }

        private void checkedListNHDPlus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseProjectNHDPlus_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNHDPlus.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNHDPlus = this.txtProjectFolderNHDPlus.Text;
            }
        }

        private string addTifFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] tiffiles = Directory.GetFiles(aSubFolder, "*.tif", SearchOption.AllDirectories);
            foreach (string file in tiffiles)
            {
                DateTime creationTime = File.GetLastWriteTime(file);
                TimeSpan timeSpan = DateTime.Now - creationTime;
                if (timeSpan.Minutes < 2)
                {
                    text = dataType + " tif file: " + file + Environment.NewLine;
                    countFiles++;
                }
            }
            return text;
        }
        private string addShpFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
            foreach (string file in shapefiles)
            {
                DateTime creationTime = File.GetLastWriteTime(file);
                TimeSpan timeSpan = DateTime.Now - creationTime;
               // if (timeSpan.Minutes < 2)
              //  {
                    text = dataType + " shapefile: " + file + Environment.NewLine;
                    countFiles++;
               // }
            }
            return text;
        }

        private void writeLogFile(string aSubFolder, string logfilename, bool logfileExists, string dataType)
        {
            string[] xmlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
            if (xmlfiles.Length > 0)
            {
                string xmlfilename = xmlfiles[0];
                DateTime creationTime = File.GetLastWriteTime(xmlfilename);
                TimeSpan timeSpan = DateTime.Now - creationTime;                
                if ((File.Exists(xmlfilename) == true) && (timeSpan.Minutes < 2))
                {                
                    ReadXMLandWriteLog readWriteHydrologicUnitsSubBasinPolygons = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, dataType);
                }
            }
        }

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolder.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNHDPlus = this.txtCacheFolder.Text;
            }
        }

        
        private void btnGetWatershed_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            string aProjectFolder = txtProject.Text.Trim();
            string aCacheFolder = txtCache.Text.Trim();
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.AlbersProjection(); 
            D4EM.Data.Region aRegion = new D4EM.Data.Region(0, 0, 0, 0, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNHDPlus, aProjectFolderNHDPlus, aRegion, false, false);
            string latitude = txtLatitude.Text.Trim();
            string longitude = txtLongitude.Text.Trim();
            string subFolder = Path.Combine(aProjectFolder, "Lat" +latitude + ";Lng" + longitude);
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNHDPlus");

            EPAWaters.PourPoint pt = EPAWaters.GetPourPoint(aCacheFolder, Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            long comID = (long)pt.COMID;
            EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.Catchment);
            EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.Flowline);
            EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.PourpointWatershed);
            string[] shapefiles = Directory.GetFiles(subFolder, "*.shp", SearchOption.AllDirectories);
            foreach(string shapeFile in shapefiles)
            {
                fileShpTif.WriteLine(shapeFile);
            }           
         
            fileShpTif.Close();
            this.Close();
            this.Cursor = StoredCursor;
        }

       

        private void btnNHDPlusLoadDataToMap_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRunNHDplus_Click(object sender, EventArgs e)
        {
            if (listHuc8NHDPlus.Items.Count == 0)
            {
                MessageBox.Show("Please add a HUC-8 to the list");
                return;
            }

            if (checkedListNHDPlus.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select atleast one data type");
                return;
            }

            if (txtProjectFolderNHDPlus.Text == "")
            {
                MessageBox.Show("Please enter a Project Folder directory");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;



            string aProjectFolderNHDPlus = System.IO.Path.Combine(txtProjectFolderNHDPlus.Text.Trim(), "NHDPlus");
            string aCacheFolderNHDPlus = txtCacheFolder.Text.Trim();

            Double aNorth = 34;
            Double aSouth = 33;
            Double aWest = -84;
            Double aEast = -83;
            //String aHUC8 = txtHUC8NHDPlus.Text.Trim();
            string fileLocationsText = "Downloaded NHDPlus files are located in " + aProjectFolderNHDPlus + Environment.NewLine + Environment.NewLine;
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNHDPlus");
            try
            {
                //NHDPlus grids are in Albers, so it is easier to request them in their native projection
                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.AlbersProjection(); // new DotSpatial.Projections.ProjectionInfo(D4EM.Data.Globals.GeographicProjection().ToProj4String());
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNHDPlus, aProjectFolderNHDPlus, aRegion, false, false);



                D4EM.Data.LayerSpecification aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.all;
                //added 9/29
                foreach (object huc8 in listHuc8NHDPlus.Items)
                {
                    bool logfileExists = false;
                    string aHUC8 = huc8.ToString();

                    string logfilename = System.IO.Path.Combine(aProjectFolderNHDPlus, aHUC8 + "_LogFile.txt");
                    fileLocationsText = fileLocationsText + "FILE LOCATIONS for " + huc8 + ":" + Environment.NewLine;
                    fileLocationsText = fileLocationsText + "Metadata: " + logfilename + Environment.NewLine;

                    foreach (object datatype in checkedListNHDPlus.CheckedItems)
                    {
                        string dataType = datatype.ToString();
                        string aSaveFolder = aHUC8 + dataType;
                        string aSubFolder = System.IO.Path.Combine(aProjectFolderNHDPlus, aSaveFolder);

                        switch (dataType)
                        {
                            case "CatchmentGrid":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentGrid;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                break;
                            case "CatchmentPolygons":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrcatchment = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "ElevationGrid":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                break;
                            case "FlowAccumulationGrid":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowAccumulationGrid;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                break;
                            case "FlowDirectionGrid":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowDirectionGrid;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                break;
                            case "SlopeGrid":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.SlopeGrid;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                break;
                            case "Hydrography.Area":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Area;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrhydrographyarea = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "Hydrography.Flowline":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrhydrographyflowline = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "Hydrography.Line":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Line;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrhydrographyline = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "Hydrography.Point":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Point;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrhydrographypoint = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "Hydrography.Waterbody":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrhydrographywaterbody = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "HydrologicUnits.RegionPolygons":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.RegionPolygons;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                break;
                            case "HydrologicUnits.SubBasinPolygons":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubBasinPolygons;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                EPAUtility.WriteFileWithShapeFilePaths wrsubbasinpolygons = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNHDPlus, aSaveFolder);
                                break;
                            case "HydrologicUnits.SubWatershedPolygons":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubWatershedPolygons;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                break;
                            case "HydrologicUnits.WatershedPolygons":
                                aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.WatershedPolygons;
                                D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType);
                                break;
                        }

                        logfileExists = true;
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                fileShpTif.Close();
                labelNHDPlus.Visible = true;
                labelNHDPlus.Text = "Downloaded data is located in " + aProjectFolderNHDPlus;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NHDPlus File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNHDPlusLoadDataToMap.Visible = true;
                }
            }
            catch (ApplicationException ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }

            this.Cursor = StoredCursor;
            countFiles = 0;
        }

        private void btnManitowoc_Click(object sender, EventArgs e)
        {
            txtLatitude.Text = "44.088571";
            txtLongitude.Text = "-87.656136";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNHDPlus");
            try
            {
                string aProjectFolder = txtProject.Text.Trim();
                string aCacheFolder = txtCache.Text.Trim();
                string subFolder = Path.Combine(aProjectFolder, "COMIDs");

                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.AlbersProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(0, 0, 0, 0, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNHDPlus, aProjectFolderNHDPlus, aRegion, false, false);
                                
                foreach (string streamID in listStreamIDs.CheckedItems)
                {
                    subFolder = Path.Combine(aProjectFolder, "COMID " + streamID);
                    long comID = (long)Convert.ToInt32(streamID);
                    EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.Catchment);
                    EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.Flowline);
                    EPAWaters.GetLayer(aProject, subFolder, comID, 100, EPAWaters.LayerSpecifications.PourpointWatershed);
                    string[] shapefiles = Directory.GetFiles(subFolder, "*.shp", SearchOption.AllDirectories);
                    foreach (string shapeFile in shapefiles)
                    {
                        fileShpTif.WriteLine(shapeFile);
                    }
                }
                fileShpTif.Close();
                this.Close();

            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());                
            }
            this.Cursor = StoredCursor;
        }
    }
}