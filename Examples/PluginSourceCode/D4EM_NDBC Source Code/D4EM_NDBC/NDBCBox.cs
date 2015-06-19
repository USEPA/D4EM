using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.Design.DataVisualization;

namespace D4EM_NDBC
{
    public partial class NDBCBox : Form
    {
        static public string aProjectFolderNDBC;
        List<string> stationIDs = new List<string>();

        double lat = 0;
        double lng = 0;
        double radius = 0;

        public NDBCBox(List<string> stationIds)
        {
            InitializeComponent();
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
                groupNDBChistorical.Visible = true;
            }
            
        }

        private void btnRunNDBC_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            
            aProjectFolderNDBC = txtProjectFolderNDBC.Text.Trim();

            //http://www.ndbc.noaa.gov/data/latest_obs/W.rss 
            //http://www.ndbc.noaa.gov/rss/ndbc_obs_search.php?lat=X&lon=Y 

            lat = Convert.ToDouble(txtLatitudeNDBC.Text.Trim());
            lng = Convert.ToDouble(txtLongitudeNDBC.Text.Trim());
            radius = Convert.ToDouble(txtRadiusNDBC.Text.Trim());
            string aSaveFolder = "Lat" + lat + ";Lng" + lng + ";Radius" + radius;
            string aSubFolder = System.IO.Path.Combine(aProjectFolderNDBC, aSaveFolder);
            
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNDBC");
            try
            {
                D4EM.Data.Source.NDBC ndbc = new D4EM.Data.Source.NDBC(aProjectFolderNDBC, aSaveFolder, lat, lng, radius);

                List<string> fileNames = ndbc.FileNames;
                int fileCount = fileNames.Count;
                string fileLocationsText = "Downloaded NDBC files are located in " + aProjectFolderNDBC + Environment.NewLine;
                fileLocationsText = fileLocationsText + Environment.NewLine;
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
                    if (Path.GetExtension(file) == ".txt")
                    {
                        fileLocationsText = fileLocationsText + "Metadata file: " + file + Environment.NewLine;
                    }

                }
                DataTable dt = ndbc.dataTable;

                EPAUtility.WriteFileWithShapeFilePaths wr = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderNDBC, aSaveFolder);
                label1.Text = "Downloaded data is located at " + aProjectFolderNDBC;
                label1.Visible = true;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NDBC File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNDBCLoadDataToMap.Visible = true;
                }
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            fileShpTif.Close();
            
            this.Cursor = StoredCursor;
        }

        private void NDBCBox_Load(object sender, EventArgs e)
        {
            txtLatitudeNDBC.Text = "33";
            txtLongitudeNDBC.Text = "-84";
            txtRadiusNDBC.Text = "1000";            
            txtProjectFolderNDBC.Text = @"C:\Temp\ProjectFolderNDBC";
            txtNDBCyear.Text = "2011";

        }

        private void btnBrowseNDBC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNDBC.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNDBC = this.txtProjectFolderNDBC.Text;
            }
        }

        private void btnNDBCLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNDBChistoricalData_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            lat = Convert.ToDouble(txtLatitudeNDBC.Text.Trim());
            lng = Convert.ToDouble(txtLongitudeNDBC.Text.Trim());
            radius = Convert.ToDouble(txtRadiusNDBC.Text.Trim());
            aProjectFolderNDBC = txtProjectFolderNDBC.Text.Trim();
            foreach (object stationName in listStationIDs.CheckedItems)
            {
                string[] splitName = stationName.ToString().Split(' ');
                string stationID = splitName[1];
                string year = txtNDBCyear.Text.Trim();
                string aSaveFolder = "Lat" + lat + ";Lng" + lng + ";Radius" + radius + "\\Historical Data for Station + " + stationID;
                string folder = Path.Combine(aProjectFolderNDBC, aSaveFolder);
                D4EM.Data.Source.NDBC ndbc = new D4EM.Data.Source.NDBC();
                DataTable dt = ndbc.getHistoricalData(folder, stationID, year);
            //    DataTable dt = ndbc.getHistoricalData(folder, "46022", "1983");
                if (ndbc.caseColumns == 1)
                {
                    NDBCchart chart1 = new NDBCchart(dt);
                    chart1.Text = stationName.ToString();
                    chart1.Show();
                }
                else if (ndbc.caseColumns == 2)
                {
                    NDBCchart2 chart2 = new NDBCchart2(dt);
                    chart2.Text = stationName.ToString();
                    chart2.Show();
                }
            }

           
            this.Cursor = StoredCursor;
        }

        
    }
}
