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
using DotSpatial.Data;

namespace D4EM_NAWQA
{
    public partial class NAWQABox : Form
    {
        string aProjectFolderNAWQA = "";
        string aCacheFolderNAWQA = "";
        List<string> _counties = null;

        public NAWQABox(List<string> counties)
        {
            InitializeComponent();
            _counties = counties;
        }

        private void NAWQABox_Load(object sender, EventArgs e)
        {
            foreach (string countyLong in _counties)
            {
                string[] values = countyLong.Split(',');
                string state = longStateName(values[1].ToString().ToUpper().Trim());
                string county_long = values[0].ToString().ToUpper().Trim();
                string[] splitCounty = county_long.Split(' ');
                string county_short = splitCounty[0];
               listCountiesFromMap.Items.Add(county_short + ", " + state);
            }
            for(int i = 0; i < listCountiesFromMap.Items.Count; i++)
            {
                listCountiesFromMap.SetItemChecked(i, true);
            }

            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string path = Path.GetDirectoryName(filePath);   
            txtProjectFolderNAWQA.Text = @"C:\Temp\ProjectFolderNAWQA";
            txtCacheFolderNAWQA.Text = System.IO.Path.Combine(path, "Cache");
            txtNAWQAlat.Text = "35";
            txtNAWQAlng.Text = "-88";
        }

        private void btnRunNAWQA_Click(object sender, EventArgs e)
        {
            string[] counties = null;

            labelNAWQA.Visible = false;

            string aProjectFolderNAWQA = txtProjectFolderNAWQA.Text.Trim();
            aCacheFolderNAWQA = txtCacheFolderNAWQA.Text.Trim();

            string waterType = "";
            string state = "";
            string startYear = txtStartYearNAQWA.Text.Trim();
            string endYear = txtEndYearNAQWA.Text.Trim();
            for (int i = 0; i < groupNAWQAwaterType.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAwaterType.Controls[i];
                if (rb.Checked == true)
                {
                    waterType = rb.Text;
                    if (waterType == "Groundwater")
                    {
                        waterType = "groundwater";
                    }
                    else if (waterType == "Surfacewater and Groundwater")
                    {
                        waterType = "surfacegroundwater";
                    }
                }
            }
            if (waterType == "")
            {
                MessageBox.Show("Please select a water type");
                return;
            }

            string fileType = "";
            string fileExt = "";

            for (int i = 0; i < groupNAWQAfileTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAfileTypes.Controls[i];
                if (rb.Checked == true)
                {
                    fileType = rb.Text;
                    if (fileType == "excel")
                    {
                        fileExt = "xls";
                    }
                    else if (fileType == "tab")
                    {
                        fileExt = "tsv";
                    }
                    else
                    {
                        fileExt = fileType;
                    }
                }
            }
            if (fileType == "")
            {
                MessageBox.Show("Please select a file type");
                return;
            }

            string queryType = "";

            for (int i = 0; i < groupNAWQAqueryTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAqueryTypes.Controls[i];
                if (rb.Checked == true)
                {
                    queryType = rb.Text;
                }
            }
            if (queryType == "")
            {
                MessageBox.Show("Please select a query type");
                return;
            }

            if (listNAWQAparameters.SelectedItems.Count < 1)
            {
                MessageBox.Show("Please select at least one parameter");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;


            if (radioUseLatLong.Checked)
            {
                if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
                {
                    MessageBox.Show("Please enter values for Latitude and Longitude");
                    this.Cursor = StoredCursor;
                    return;
                }

                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

            }

            else if (radioUseStatesCounties.Checked)
            {
                if (listCounties.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one county");
                    this.Cursor = StoredCursor;
                    return;
                }

                foreach (object st in listNAWQAstates.SelectedItems)
                {
                    state = st.ToString();
                }

                counties = new string[listCounties.CheckedItems.Count];

                int i_county = 1;
                foreach (object co in listCounties.CheckedItems)
                {
                    string county = co.ToString();
                    counties[i_county - 1] = county;
                    i_county++;
                }
            }
            else if (radioUseCountiesFromMap.Checked)
            {
                if (listCountiesFromMap.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one county");
                    this.Cursor = StoredCursor;
                    return;
                }

                counties = new string[listCountiesFromMap.CheckedItems.Count];

                int i_county = 1;
                foreach (object co in listCountiesFromMap.CheckedItems)
                {
                    string county = co.ToString();
                    counties[i_county - 1] = county;
                    i_county++;
                }
            }
            else
            {
                counties = new string[0];
            }
            if (chkBoxWater.Checked)
            {
                if ((txtStartYearNAQWA.Text.Trim().Length != 4) && (txtEndYearNAQWA.Text.Trim().Length != 4))
                {
                    MessageBox.Show("Start Year and End Year must be 4 digits");
                    return;
                }
                startYear = txtStartYearNAQWA.Text.Trim();
                endYear = txtEndYearNAQWA.Text.Trim();
            }

            D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
            if (radioUseLatLong.Checked)
            {
                string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
                int k = 0;
                foreach (object par in listNAWQAparameters.CheckedItems)
                {
                    string parameter = par.ToString();
                    parameters[k] = parameter;
                    k++;
                }

                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

                nawqa.getAllDataLatLong(lat, lng, parameters, startYear, endYear); 



            }

            else if (radioUseStatesCounties.Checked)
            {
                string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
                int k = 0;
                foreach (object par in listNAWQAparameters.CheckedItems)
                {
                    string parameter = par.ToString();
                    parameters[k] = parameter;
                    k++;
                }

                nawqa.getAllDataStateCounties(state, counties, parameters, startYear, endYear);

            }

            else if (radioUseCountiesFromMap.Checked)
            {
                string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
                int k = 0;
                foreach (object par in listNAWQAparameters.CheckedItems)
                {
                    string parameter = par.ToString();
                    parameters[k] = parameter;
                    k++;
                }

                nawqa.getAllDataCountiesFromMap(counties, parameters, startYear, endYear);

            }

            this.Cursor = StoredCursor;

            labelNAWQA.Visible = true;
            labelNAWQA.Text = "Downloaded data is located in " + aProjectFolderNAWQA;
        }

       

        private void btnBrowseProjectFolderNAWQA_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNAWQA.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNAWQA = this.txtProjectFolderNAWQA.Text;
            }
        }

        private void btnBrowseCacheFolderNAWQA_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolderNAWQA.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNAWQA = this.txtCacheFolderNAWQA.Text;
            }
        }

        private void listCounties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private string longStateName(string abb)
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

        private string[] getCountyStateFromLatLong(double lat, double lng)
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

        private void radioUseLatLong_CheckedChanged_1(object sender, EventArgs e)
        {
            groupNAWQAstatesCounties.Visible = false;
            groupNAWQAlatLong.Visible = true;
            groupNAWQAcountiesFromMap.Visible = false;
        }

        private void radioUseStatesCounties_CheckedChanged_1(object sender, EventArgs e)
        {
            groupNAWQAstatesCounties.Visible = true;
            groupNAWQAlatLong.Visible = false;
            groupNAWQAcountiesFromMap.Visible = false;
        }

        private void listNAWQAstates_SelectedIndexChanged(object sender, EventArgs e)
        {
            listCounties.Items.Clear();

            TextReader tr = new StreamReader(@"..\Bin\counties.txt");
            string line = tr.ReadLine();
            DataTable dt = new DataTable();
            dt.Columns.Add("State");
            dt.Columns.Add("County");
            while ((line = tr.ReadLine()) != null)
            {
                string[] values = line.Split('\t');
                string state = values[0].ToString().ToUpper().Trim();
                string county_long = values[1].ToString().ToUpper().Trim();
                
                string[] splitCounty = county_long.Split(' ');
                string county_short = "";
                for (int i = 0; i < splitCounty.Length-1; i++)
                {
                    county_short = county_short + " " + splitCounty[i].ToString().ToUpper().Trim();
                }
                county_short = county_short.Trim();
                dt.Rows.Add(state, county_short);
            }
            tr.Close();

            string selectedState = listNAWQAstates.SelectedItem.ToString();

            DataRow[] result = dt.Select("State ='" + selectedState + "'");
            List<string> counties = new List<string>();
            foreach (DataRow row in result)
            {
                counties.Add(row[1].ToString());
            }
            counties.Sort();

            foreach (string county in counties)
            {
                listCounties.Items.Add(county);
            }
        }

        private void btnGetNAWQAaverageStdDev_Click(object sender, EventArgs e)
        {
            if (listNAWQAparameters.SelectedItems.Count < 1)
            {
                MessageBox.Show("Please select at least one parameter");
                return;
            }
            string startYear = "";
            string endYear = "";
            string aProjectFolderNAWQA = txtProjectFolderNAWQA.Text.Trim();
            aCacheFolderNAWQA = txtCacheFolderNAWQA.Text.Trim();
            gridShowNAWQAaverages.Columns.Clear();
            string subFolder = "";
            string fileName = "";
            string waterType = "";
            for (int i = 0; i < groupNAWQAwaterType.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAwaterType.Controls[i];
                if (rb.Checked == true)
                {
                    waterType = rb.Text;
                    if (waterType == "Groundwater")
                    {
                        waterType = "groundwater";
                    }
                    else if (waterType == "Surfacewater and Groundwater")
                    {
                        waterType = "surfacegroundwater";
                    }
                }
            }
            if (waterType == "")
            {
                MessageBox.Show("Please select a water type");
                return;
            }

            string fileType = "";
            string fileExt = "";

            for (int i = 0; i < groupNAWQAfileTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAfileTypes.Controls[i];
                if (rb.Checked == true)
                {
                    fileType = rb.Text;
                    if (fileType == "excel")
                    {
                        fileExt = "xls";
                    }
                    else if (fileType == "tab")
                    {
                        fileExt = "tsv";
                    }
                    else
                    {
                        fileExt = fileType;
                    }
                }
            }
            if (fileType == "")
            {
                fileExt = "csv";
            }

            string queryType = "";

            for (int i = 0; i < groupNAWQAqueryTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAqueryTypes.Controls[i];
                if (rb.Checked == true)
                {
                    queryType = rb.Text;
                }
            }
            if (queryType == "")
            {
                MessageBox.Show("Please select a query type");
                return;
            }



            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            string averagesText = "";
            string state = "";
            gridShowNAWQAaverages.Visible = true;
            DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
            columnDataGridTextBox.Name = "County";
            columnDataGridTextBox.HeaderText = "County";
            columnDataGridTextBox.Width = 220;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox);
            DataGridViewTextBoxColumn columnDataGridTextBox3 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox3.Name = "Parameter";
            columnDataGridTextBox3.HeaderText = "Parameter";
            columnDataGridTextBox3.Width = 320;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox3);
            DataGridViewTextBoxColumn columnDataGridTextBox4 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox4.Name = "Average Value";
            columnDataGridTextBox4.HeaderText = "Average Value";
            columnDataGridTextBox4.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox4);
            DataGridViewTextBoxColumn columnDataGridTextBox5 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox5.Name = "Standard Deviation";
            columnDataGridTextBox5.HeaderText = "Standard Deviation";
            columnDataGridTextBox5.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox5);
            DataGridViewTextBoxColumn columnDataGridTextBox6 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox6.Name = "# Observations";
            columnDataGridTextBox6.HeaderText = "# Observations";
            columnDataGridTextBox6.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox6);

            string[] counties = null;
            string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
            int j = 0;
            foreach (string parameter in listNAWQAparameters.CheckedItems)
            {
                parameters[j] = parameter;
                j++;
            }
            if (chkBoxWater.Checked)
            {
                if ((txtStartYearNAQWA.Text.Trim().Length != 4) && (txtEndYearNAQWA.Text.Trim().Length != 4))
                {
                    MessageBox.Show("Start Year and End Year must be 4 digits");
                    return;
                }
                startYear = txtStartYearNAQWA.Text.Trim();
                endYear = txtEndYearNAQWA.Text.Trim();
            }
            D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);

            if (radioUseStatesCounties.Checked)
            {
                state = listNAWQAstates.SelectedItem.ToString();
                counties = new string[listCounties.CheckedItems.Count];
                int k = 0;
                int i_county = 1;
                foreach (string county in listCounties.CheckedItems)
                {
                    counties[k] = county;
                    k++;
                }
                nawqa.getAverageAndStandardDeviationStateCounties(state, counties, parameters);
            }

            else if (radioUseLatLong.Checked)
            {
                if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
                {
                    MessageBox.Show("Please enter values for Latitude and Longitude");
                    this.Cursor = StoredCursor;
                    return;
                }

                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

                nawqa.getAverageAndStandardDeviationLatLong(lat, lng, parameters);
            }
            else if (radioUseCountiesFromMap.Checked)
            {
                counties = new string[listCountiesFromMap.CheckedItems.Count];
                int k = 0;
                foreach (string county in listCountiesFromMap.CheckedItems)
                {
                    counties[k] = county;
                    k++;
                }
                nawqa.getAverageAndStandardDeviationCountiesFromMap(counties, parameters);
            }

            DataTable dt = nawqa.dtAverages;

            foreach (DataRow dr in dt.Rows)
            {
                gridShowNAWQAaverages.Rows.Add(dr.ItemArray);
            }

            // gridShowNAWQAaverages.DataSource = dt;

            labelNAWQA.Visible = true;
            labelNAWQA.Text = "Downloaded data is located in " + aProjectFolderNAWQA;
            this.Cursor = StoredCursor;
        }

        private void btnDetermineCounty_Click_1(object sender, EventArgs e)
        {
            if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
            {
                MessageBox.Show("Please enter values for Latitude and Longitude");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
            double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

            string[] countyState = getCountyStateFromLatLong(lat, lng);

            string stateName = longStateName(countyState[1]);
            string[] countyNameFull = countyState[0].Split(' ');
            string countyName = countyNameFull[0].ToUpper();

            labelNAWQAlatLongCounty.Text = countyName + ", " + stateName;
            labelNAWQAlatLongCounty.Visible = true;

            this.Cursor = StoredCursor;
        }

        private void radioUseCountiesFromMap_CheckedChanged(object sender, EventArgs e)
        {
            groupNAWQAcountiesFromMap.Visible = true;
            groupNAWQAstatesCounties.Visible = false;
            groupNAWQAlatLong.Visible = false;
        }

        private void listCountiesFromMap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
