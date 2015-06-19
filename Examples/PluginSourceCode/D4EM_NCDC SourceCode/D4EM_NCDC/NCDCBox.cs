using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using D4EM.Data;
using D4EM.Data.Source;
using D4EM.Geo;
using System.IO;

namespace D4EM_NCDC
{
    public partial class NCDCBox : Form
    {
        string aProjectFolderNCDC;
        int countFiles = 0;

        public NCDCBox()
        {
            InitializeComponent();
        }

        Stopwatch stopwatch = new Stopwatch();

       
        private void NCDCBox_Load(object sender, EventArgs e)
        {
            txtToken.Text = "VzwVyCwzUzMKMopSqnTT";
            txtStartYear.Text = "2008";
            txtStartMonth.Text = "01";
            txtStartDay.Text = "01";
            txtEndYear.Text = "2008";
            txtEndMonth.Text = "02";
            txtEndDay.Text = "01";
            txtProjectFolderNCDC.Text = @"C:\Temp\ProjectFolderNCDC";

        }

       
        private void btnDownloadNCDCStations_Click_1(object sender, EventArgs e)
        {



            if (txtToken.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a token");
                return;
            }


            if (listStatesNCDC.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a state");
                return;
            }
            if (listStatesNCDC.CheckedItems.Count > 1)
            {
                MessageBox.Show("You can only select 1 state");
                return;
            }
            if ((stopwatch.ElapsedMilliseconds < 60000) && (stopwatch.ElapsedMilliseconds != 0))
            {
                MessageBox.Show("Please wait " + (60 - stopwatch.ElapsedMilliseconds / 1000) + " more seconds");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string state = listStatesNCDC.SelectedItem.ToString();
                string token = txtToken.Text.Trim();

                EPAUtility.NCDCSupport ncdc = new EPAUtility.NCDCSupport();
                DataTable datatable = ncdc.populateStationsTable(token, state);

                stopwatch.Restart();

                dataStationsNCDC.DataSource = datatable.DefaultView;
                
                DataTable datat = new DataTable();
                EPAUtility.NCDCSupport temp = new EPAUtility.NCDCSupport();
                datat = temp.createVariablesTable();
                
                dataVariablesNCDC.DataSource = datat.DefaultView;
                this.Cursor = StoredCursor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            this.Cursor = Cursors.WaitCursor;
            //    Thread.Sleep(60 * 1000);
            this.Cursor = StoredCursor;
        }

        private void btnDownloadforSelectedStation_Click(object sender, EventArgs e)
        {
            if (txtToken.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a token");
                return;
            }
            if (txtStartYear.Text.Trim().Length != 4)
            {
                MessageBox.Show("Please enter a valid start year.  The year must be 4 digits (i.e. 2011)");
                return;
            }
            if (txtEndYear.Text.Trim().Length != 4)
            {
                MessageBox.Show("Please enter a valid end year.  The year must be 4 digits (i.e. 2011)");
                return;
            }
            if (txtStartMonth.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid start month.  The month must be 2 digits (i.e. 04 or 11)");
                return;
            }
            if (txtEndMonth.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid end month.  The month must be 2 digits (i.e. 04 or 11)");
                return;
            }
            if (txtStartDay.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid start day.  The day must be 2 digits (i.e. 08 or 25)");
                return;
            }
            if (txtEndDay.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid end day.  The day must be 2 digits (i.e. 08 or 25)");
                return;
            }

            if (outputType.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an output type");
                return;
            }
            if (dataStationsNCDC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row from the Stations grid");
                return;
            }
            if (dataStationsNCDC.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only select 1 row from the Stations grid");
                return;
            }
            if (dataVariablesNCDC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row from the Variables grid");
                return;
            }
            if (dataVariablesNCDC.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only select 1 row from the Variables grid");
                return;
            }
            if (txtProjectFolderNCDC.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a project folder (for example:  C:\\Temp\\ProjectFolder)");
                return;
            }

            if ((stopwatch.ElapsedMilliseconds < 60000) && (stopwatch.ElapsedMilliseconds != 0))
            {
                MessageBox.Show("Please wait " + (60 - stopwatch.ElapsedMilliseconds / 1000) + " more seconds");
                return;
            }
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                aProjectFolderNCDC = txtProjectFolderNCDC.Text.Trim();

                string yearStart = txtStartYear.Text.Trim();
                string monthStart = txtStartMonth.Text.Trim();
                if (monthStart.Length == 1)
                {
                    monthStart = "0" + monthStart;
                }
                string dayStart = txtStartDay.Text.Trim();
                if (dayStart.Length == 1)
                {
                    dayStart = "0" + dayStart;
                }
                string yearEnd = txtEndYear.Text.Trim();
                string monthEnd = txtEndMonth.Text.Trim();
                if (monthEnd.Length == 1)
                {
                    monthEnd = "0" + monthEnd;
                }
                string dayEnd = txtEndDay.Text.Trim();
                if (dayEnd.Length == 1)
                {
                    dayEnd = "0" + dayEnd;
                }

                string startDate = yearStart + monthStart + dayStart;
                string endDate = yearEnd + monthEnd + dayEnd;

                int start = Convert.ToInt32(startDate);
                int end = Convert.ToInt32(endDate);

                string token = txtToken.Text.Trim();

                string stationId = dataStationsNCDC.SelectedRows[0].Cells[0].Value.ToString();
                string stationName = dataStationsNCDC.SelectedRows[0].Cells[1].Value.ToString();
                double latitude = Convert.ToDouble(dataStationsNCDC.SelectedRows[0].Cells[2].Value.ToString());
                double longitude = Convert.ToDouble(dataStationsNCDC.SelectedRows[0].Cells[3].Value.ToString());
                string variableId = dataVariablesNCDC.SelectedRows[0].Cells[0].Value.ToString();
                string variableName = dataVariablesNCDC.SelectedRows[0].Cells[1].Value.ToString();
                
                string outputtype = outputType.SelectedItem.ToString();
                string datasettype = datasetType.SelectedItem.ToString();

                EPAUtility.NCDCSupport ncdcSupport = new EPAUtility.NCDCSupport(token, aProjectFolderNCDC, stationId, stationName, variableId, variableName, datasettype, outputtype, start, end);

                ncdcSupport.WriteNCDCValuesFile();
                stopwatch.Restart();

                string aSubFolder = System.IO.Path.Combine(aProjectFolderNCDC, stationId + "_" + variableId);
                string fileLocationsText = "Downloaded NCDC files are located in " + aProjectFolderNCDC + Environment.NewLine + Environment.NewLine;
                
                string dataType = stationName + "(" + stationId + ") " + variableName + "(" + variableId + ")";
                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType) + Environment.NewLine;
                if (outputtype == "csv")
                {
                    fileLocationsText = fileLocationsText + addCSVFileLocations(aSubFolder, dataType);
                }
                if (outputtype == "xml")
                {
                    fileLocationsText = fileLocationsText + addXMLFileLocations(aSubFolder, dataType);
                }
                if (outputtype == "waterml")
                {
                    fileLocationsText = fileLocationsText + addWaterMLFileLocations(aSubFolder, dataType);
                }
                labelNCDC.Visible = true;
                labelNCDC.Text = "Downloaded Data is located in " + aProjectFolderNCDC;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NCDC File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNCDCLoadStationToMap.Visible = true;
                }
                TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathNCDC");

                if (Directory.Exists(aSubFolder))
                {
                    string[] shp = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Cursor = StoredCursor;
            countFiles = 0;
        }

        private void btnFindToken(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www7.ncdc.noaa.gov/wsregistration/ws_home.html");
        }

        private void btnBrowseNCDC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNCDC.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNCDC = this.txtProjectFolderNCDC.Text;
            }
        }
        private string addCSVFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] csvfiles = Directory.GetFiles(aSubFolder, "*.csv", SearchOption.AllDirectories);
                foreach (string file in csvfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " csvfile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addXMLFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] xmlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                foreach (string file in xmlfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " xmlfile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addWaterMLFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] watermlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                foreach (string file in watermlfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " waterml file: " + file + Environment.NewLine;
                        countFiles++;
                    }
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
                if (timeSpan.Minutes < 2)
                {
                    text = dataType + " shapefile: " + file + Environment.NewLine;
                }
            }
            return text;
        }

        private void btnNCDCLoadStationToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
