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
using D4EM.Data.Source.NRCS_Soil;


namespace D4EM_NRCS_Soil
{
    public partial class NRCS_SoilBox : Form
    {
        public string aProjectFolderSoils { get; set; }
        public string aCacheFolderNRCSSoil { get; set; }
        public int j { get; set; } //###########8march13################

        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;

        public D4EM.Data.Project aProject = null;

        List<string> hucnums = null;

        public NRCS_SoilBox(double north, double south, double east, double west, List<string> huc8nums)
        {
            InitializeComponent();
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            hucnums = huc8nums;
        }

        private void btnBrowseSoils_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderSoils.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderSoils = this.txtProjectFolderSoils.Text;
            }
        }

        private void NRCS_SoilBox_Load(object sender, EventArgs e)
        {
            j = 0; //#########8march13############

            txtNorth.Text = _north.ToString("#.##");
            txtSouth.Text = _south.ToString("#.##");
            txtEast.Text = _east.ToString("#.##");
            txtWest.Text = _west.ToString("#.##");

            btnNRCSSOILLoadDataToMap.Visible = false;

            //txtProjectFolderSoils.Text = @"C:\Temp\ProjectFolderSoils\";
            txtProjectFolderSoils.Text = @"C:\Temp\ProjectFolderSoil\";
            aProjectFolderSoils = txtProjectFolderSoils.Text; 
            txtCache.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            aCacheFolderNRCSSoil = txtCache.Text;
           
        }

        private void btnRunNRCSSoil_Click(object sender, EventArgs e)
        {

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double dblNorth = 0;
            double dblSouth = 0;
            double dblEast = 0;
            double dblWest = 0;

            //TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathSoil");

            try
            {

                dblNorth = Convert.ToDouble(txtNorth.Text);
                dblSouth = Convert.ToDouble(txtSouth.Text);
                dblEast = Convert.ToDouble(txtEast.Text);
                dblWest = Convert.ToDouble(txtWest.Text);

                aProjectFolderSoils = txtProjectFolderSoils.Text;
                string fileLocationsText = "Downloaded Storet files are located in " + aProjectFolderSoils + Environment.NewLine + Environment.NewLine;
                fileLocationsText = fileLocationsText + "STORET FILE LOCATIONS for North = " + dblNorth + ", South = " + dblSouth + ", East = " + dblEast + ", West = " + dblWest + Environment.NewLine;
                fileLocationsText = fileLocationsText + Environment.NewLine;
                string subFolder = System.IO.Path.Combine(aProjectFolderSoils, "N" + dblNorth + ";S" + dblSouth + ";E" + dblEast + ";W" + dblWest);
                Directory.CreateDirectory(subFolder);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //TextWriter fileShpTif = new StreamWriter(aProjectFolderSoils);

            try
            {
                D4EM.Data.Region lRegion = new D4EM.Data.Region(dblNorth, dblSouth, dblWest, dblEast, D4EM.Data.Globals.GeographicProjection());
                D4EM.Data.Project lProject = new Project(D4EM.Data.Globals.AlbersProjection(),
                                                         txtCache.Text,
                                                         txtProjectFolderSoils.Text, lRegion,
                                                         false, false);
                System.Collections.Generic.List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> lSoils = D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(lProject, null);

                string coords = _west.ToString("#.##") + "," + _south.ToString("#.##") + "," + 
                                _east.ToString("#.##") + "," + _north.ToString("#.##");
                string xmlFile = aCacheFolderNRCSSoil + "\\NRCS_Soil\\" + "NRCS_Soil_W,S,E,N=" + 
                                 coords + ".xml";

                if ((lSoils == null) || (lSoils.Count == 0))
                {
                    MessageBox.Show("no soils found, soil list empty");
                    this.Cursor = StoredCursor;
                    return;
                }
                else
                {
                    string lSoilsTag = D4EM.Data.Source.NRCS_Soil.SoilLocation.SoilLayerSpecification.Tag;
                    D4EM.Data.Layer lSoilsLayer = lProject.LayerFromTag(lSoilsTag);
                    if (lSoilsLayer == null)
                    {
                        MessageBox.Show("Soils layer with tag '" + lSoilsTag +
                                        "' not found, aborting Test_GetSoils");
                    }
                    else if (!File.Exists(lSoilsLayer.FileName))
                    {
                        MessageBox.Show("Soils layer with tag '" + lSoilsTag +
                                        "' not found, aborting Test_GetSoils");
                    }
                    else
                    {
                        MessageBox.Show("Successful test of NRCS_Soil found " +
                                        lSoils.Count + " soils and created layer " +
                                        lSoilsLayer.FileName);
                        //opens saved directory in explorer
                        //System.Diagnostics.Process.Start("explorer.exe", @"/select, " + lSoilsLayer.FileName);
                    }
                }

                j = 1;
                this.Cursor = StoredCursor;
                btnNRCSSOILLoadDataToMap.Visible = true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                j = 0;
                this.Cursor = StoredCursor;
                btnNRCSSOILLoadDataToMap.Visible = false;
            }

           
        }
        private string addShpFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
            foreach (string file in shapefiles)
            {
                text = dataType + " shapefile: " + file + Environment.NewLine;
            }
            return text;
        }
        private string addCSVFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            string[] csvfiles = Directory.GetFiles(aSubFolder, "*.csv", SearchOption.AllDirectories);
            foreach (string file in csvfiles)
            {
                text = dataType + " csvfile: " + file + Environment.NewLine;
            }
            return text;
        }

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCache.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNRCSSoil = this.txtCache.Text;
            }
        }

        private void btnNRCSSOILLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
