using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using D4EM.Data;

namespace D4EM_Storet
{
    public partial class StoretBox : Form
    {
        string aProjectFolderStoret;
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;
        List<string> hucnums = null;

        public StoretBox(double north, double south, double east, double west, List<string> huc8nums)
        {
            InitializeComponent();
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            hucnums = huc8nums;
        }

        private void btnDownloadStoret_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txtNorthStoret.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthStoret.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestStoret.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastStoret.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathStoret");
            try
            {
                int fileCount = 0;
                double nlat = Convert.ToDouble(txtNorthStoret.Text.Trim());
                double slat = Convert.ToDouble(txtSouthStoret.Text.Trim());
                double wlong = Convert.ToDouble(txtWestStoret.Text.Trim());
                double elong = Convert.ToDouble(txtEastStoret.Text.Trim());
                aProjectFolderStoret = txtProjectFolderStoret.Text.Trim();
                string fileLocationsText = "Downloaded Storet files are located in " + aProjectFolderStoret + Environment.NewLine + Environment.NewLine;
                fileLocationsText = fileLocationsText + "STORET FILE LOCATIONS for North = " + nlat + ", South = " + slat + ", East = " + elong + ", West = " + wlong + Environment.NewLine;
                fileLocationsText = fileLocationsText + Environment.NewLine;

                string bboxVal = "bBox=" + wlong + "," + slat + "," + elong + "," + nlat;

                string subFolder = System.IO.Path.Combine(aProjectFolderStoret, "N" + nlat + ";S" + slat + ";E" + elong + ";W" + wlong);

                Directory.CreateDirectory(subFolder);

                string stationsFile = System.IO.Path.Combine(subFolder, "Stations");
                string resultsFile = System.IO.Path.Combine(subFolder, "Results");

                string aParamList = bboxVal;

                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(nlat, slat, wlong, elong, aDesiredProjection);

                string aFileExtXml = "xml";// "csv"; //xml, csv, tsv, xls, kml                
                bool downloadxml = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, aFileExtXml);
              //  D4EM.Data.Source.Storet.GetResults(aRegion, resultsFile, aParamList, aFileExtXml);

                string aFileExtCsv = "csv";// "csv"; //xml, csv, tsv, xls, kml    
                bool downloadcsv = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, aFileExtCsv);
               // D4EM.Data.Source.Storet.GetResults(aRegion, resultsFile, aParamList, aFileExtCsv);

                if ((downloadxml == true) && (downloadcsv == true))
                {
                    EPAUtility.StoretFileSupport storetfilesupport = new EPAUtility.StoretFileSupport();
                    storetfilesupport.WriteStoretFiles(stationsFile, subFolder, nlat, slat, elong, wlong);
                    List<string> fileNames = storetfilesupport.FileNames;
                    fileCount = fileNames.Count;
                    foreach (string file in fileNames)
                    {
                        if (Path.GetExtension(file) == ".shp")
                        {
                            fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".csv")
                        {
                            fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".xml")
                        {
                            fileLocationsText = fileLocationsText + "XML file: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".txt")
                        {
                            fileLocationsText = fileLocationsText + "Metadata file: " + file + Environment.NewLine;
                        }
                    }
                    string resultsext = "csv";
                    bool results = D4EM.Data.Source.Storet.GetResults(aRegion, resultsFile, aParamList, resultsext);
                    if (results == true)
                    {
                        fileLocationsText = fileLocationsText + resultsext + " file: " + resultsFile + "." + resultsext + Environment.NewLine;
                        foreach (object datatype in listStoretDataTypes.CheckedItems)
                        {
                            string dtype = datatype.ToString();
                            EPAUtility.StoretFileSupport storet = new EPAUtility.StoretFileSupport();
                            storet.writeSpecificStoretFiles(resultsFile + ".csv", subFolder, dtype);
                        }
                    }
                    

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
                    fileShpTif.Close();

                    labelStoret.Text = "Downloaded data is located in " + aProjectFolderStoret;
                    labelStoret.Visible = true;

                    if (fileCount == 0)
                    {
                        MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(fileLocationsText, "Storet File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnStoretLoadDataToMap.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
        }

        private void btnBrowseStoret_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderStoret.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderStoret = this.txtProjectFolderStoret.Text;
            }
        }

        private void StoretBox_Load(object sender, EventArgs e)
        {
            txtNorthStoret.Text = _north.ToString("#.##");
            txtSouthStoret.Text = _south.ToString("#.##");
            txtEastStoret.Text = _east.ToString("#.##");
            txtWestStoret.Text = _west.ToString("#.##");

            txtProjectFolderStoret.Text = @"C:\Temp\ProjectFolderStoret";
            File.Delete(@"C:\Temp\DownloadedFilePathStoret");
        }
        private string addShpFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
            foreach (string file in shapefiles)
            {
                DateTime creationTime = File.GetLastWriteTime(file);
                TimeSpan timeSpan = DateTime.Now - creationTime;
                if (timeSpan.Minutes < 2)
                {
                    text = dataType + " shapefile: " + file + Environment.NewLine;
                }
            }
            return text;
        }
        private string addCSVFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] csvfiles = Directory.GetFiles(aSubFolder, "*.csv", SearchOption.AllDirectories);
            foreach (string file in csvfiles)
            {
                DateTime creationTime = File.GetLastWriteTime(file);
                TimeSpan timeSpan = DateTime.Now - creationTime;
                if (timeSpan.Minutes < 2)
                {
                    text = dataType + " csvfile: " + file + Environment.NewLine;
                }
            }
            return text;
        }

        private void btnStoretLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
