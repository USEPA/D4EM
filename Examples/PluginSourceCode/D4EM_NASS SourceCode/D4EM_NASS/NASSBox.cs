using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DotSpatial.Projections;

namespace D4EM_NASS
{
    public partial class NASSBox : Form
    {
        string aProjectFolderNASS;
        string aCacheFolderNASS;
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;
        List<string> hucnums = null;

        public NASSBox(double north, double south, double east, double west, List<string> huc8nums)
        {
            InitializeComponent();
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            hucnums = huc8nums;
        }

        private void btnNASSDownload_Click(object sender, EventArgs e)
        {
            if (txtNorthNASS.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthNASS.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestNASS.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastNASS.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }
            if (listYearsNASS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 year");
                return;
            }
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNASS");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                
                aProjectFolderNASS = txtProjectFolderNASS.Text.Trim();
                string fileLocationsText = "Downloaded NASS files are located in " + aProjectFolderNASS + Environment.NewLine + Environment.NewLine;               
                aCacheFolderNASS = txtCache.Text.Trim();
                int fileCount = 0;
                string nassFilename = "";
                foreach (object yr in listYearsNASS.CheckedItems)
                {
                    int year = Convert.ToInt32(yr);
                    double north = Convert.ToDouble(txtNorthNASS.Text.Trim());
                    double south = Convert.ToDouble(txtSouthNASS.Text.Trim());
                    double west = Convert.ToDouble(txtWestNASS.Text.Trim());
                    double east = Convert.ToDouble(txtEastNASS.Text.Trim());

                    fileLocationsText = fileLocationsText + year + " NASS FILE LOCATIONS for North = " + north + ", South = " + south + ", East = " + east + ", West = " + west + Environment.NewLine;

                    string aSubFolder = System.IO.Path.Combine(aProjectFolderNASS, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west);
                    var lProject = new D4EM.Data.Project(D4EM.Data.Source.NASS.NativeProjection, aCacheFolderNASS, aProjectFolderNASS, new D4EM.Data.Region(north, south, west, east, KnownCoordinateSystems.Geographic.World.WGS1984), false, false);
                    nassFilename = D4EM.Data.Source.NASS.getRaster(lProject, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west, "", year);
                    //string nassFilename = D4EM.Data.Source.NASS.getData(aSubFolder, aCacheFolderNASS, "", year, north, south, east, west);
                    fileLocationsText += "tif file: " + nassFilename + Environment.NewLine;
                    fileLocationsText += "metadata file: " + nassFilename + ".xml" + Environment.NewLine;

                    string countyShapeFile = Path.GetFullPath(@"..\..\..\..\Externals\data\national\cnty.shp");
                    string stateShapeFile = Path.GetFullPath(@"..\..\..\..\Externals\data\national\st.shp");
                    D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.county, countyShapeFile);
                    D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.state, stateShapeFile);
                    D4EM.Data.Source.NASS.getStatistics(lProject, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west, year.ToString());

                    if (Directory.Exists(aSubFolder))
                    {
                        string[] tif = Directory.GetFiles(aSubFolder, "*.tif", SearchOption.AllDirectories);
                        int i = 0;
                        while (i < tif.Length)
                        {
                            if (File.Exists(tif[i]))
                            {
                                fileShpTif.WriteLine(tif[i]);
                            }
                            i++;
                        }
                    }
                }
                fileShpTif.Close();
                labelNASS.Visible = true;
                labelNASS.Text = "Downloaded data is located in " + aProjectFolderNASS;
                if (nassFilename == "")
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NASS File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNASSloadDataToMap.Visible = true;
                }
               
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            
        }

        private void btnBrowseNASS_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNASS.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNASS = this.txtProjectFolderNASS.Text;
            }
        }

        private void NASSBox_Load(object sender, EventArgs e)
        {
            txtNorthNASS.Text = _north.ToString("#.##");
            txtSouthNASS.Text = _south.ToString("#.##");
            txtEastNASS.Text = _east.ToString("#.##");
            txtWestNASS.Text = _west.ToString("#.##");
            txtCache.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            txtProjectFolderNASS.Text = @"C:\Temp\ProjectFolderNASS";
            File.Delete(@"C:\Temp\DownloadedFilePathNLCD");
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
                }
            }
            return text;
        }

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCache.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNASS = this.txtCache.Text;
            }
        }

        private void btnNASSloadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
