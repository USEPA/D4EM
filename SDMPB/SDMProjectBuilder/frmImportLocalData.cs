using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace SDMProjectBuilder
{
    public partial class frmImportLocalData : Form
    {
        private AppManager _appMgr = null;
        private DataTable _dt = null;
        private Dictionary<string, FeatureSet> _dctFeatures = new Dictionary<string, FeatureSet>();
        private string _localDataConfigFile = null;

        public frmImportLocalData(AppManager appMgr)
        {
            InitializeComponent();
            _appMgr = appMgr;
        }

        private void frmImportLocalData_Load(object sender, EventArgs e)
        {

            _dt = new DataTable();
            _dt.Columns.Add("Data Use", typeof(string));
            _dt.Columns.Add("Data File", typeof(string));

            dataGridView1.DataSource = _dt;

            DataGridViewButtonColumn btnImp = new DataGridViewButtonColumn();
            btnImp.HeaderText = "Import Data";
            btnImp.Text = "Import";
            btnImp.Name = "btnImp";
            btnImp.Width = 100;
            btnImp.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(2, btnImp);

            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            btnSave.HeaderText = "Save Data";
            btnSave.Text = "Save";
            btnSave.Name = "btnSave";
            btnSave.Width = 100;
            btnSave.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(3, btnSave);

            DataGridViewButtonColumn btnRemove = new DataGridViewButtonColumn();
            btnRemove.HeaderText = "Remove Data";
            btnRemove.Text = "Remove";
            btnRemove.Name = "btnRemove";
            btnRemove.Width = 100;
            btnRemove.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(4, btnRemove);

            LoadConfigFile();

            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 2) //Import data
            {

                string dataType = _dt.Rows[e.RowIndex][0].ToString();
                string fileName = _dt.Rows[e.RowIndex][1].ToString();
                string csvFile = ImportData(dataType);
                ImportLocalData ild = new ImportLocalData(_appMgr);
                FeatureSet fs = ild.LoadShapefileFromCSV(csvFile, fileName);
                if (_dctFeatures.ContainsKey(dataType))
                    _dctFeatures[dataType] = fs;
                else
                    _dctFeatures.Add(dataType, fs);
            }
            else if (e.ColumnIndex == 3) //Save data
            {
                string dataType = _dt.Rows[e.RowIndex][0].ToString();
                string fileName = _dt.Rows[e.RowIndex][1].ToString();
                if (_dctFeatures.ContainsKey(dataType))
                {
                    string projDir = _appMgr.SerializationManager.CurrentProjectDirectory;
                    string localDataDir = Path.Combine(projDir, "LocalData");
                    FeatureSet fs = _dctFeatures[dataType];
                    string saveName = Path.Combine(localDataDir, fs.Name);                    
                    ImportLocalData ild = new ImportLocalData(_appMgr);
                    ild.SaveData(fs, saveName);
                }
            }
            else if (e.ColumnIndex == 4) //Remove data
            {
                RemoveData();
            }
        }

        private void LoadConfigFile()
        {
            string projDir = _appMgr.SerializationManager.CurrentProjectDirectory;
            _localDataConfigFile = Path.Combine(projDir, "LocalData", "localdata.config");

            if (!File.Exists(_localDataConfigFile))
            {
                MessageBox.Show("Unable to find the file: " + _localDataConfigFile);
                return;
            }

            StreamReader sr = new StreamReader(_localDataConfigFile);
            //First line is column headers - no need to keep it
            string line = sr.ReadLine();

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] data = line.Split(",".ToCharArray());
                DataRow dr = _dt.NewRow();
                dr[0] = data[0];
                dr[1] = data[1];

                _dt.Rows.Add(dr);                                
            }           
        }

        private string ImportData(string dataType)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select file to import";
            ofd.Multiselect = false;
            ofd.Filter = "Shape Files (*.shp)|*.shp|CSV Files (*.csv)|*.csv";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return null;
            else
                return ofd.FileName;
        }

        private string SaveData(string dataType, string fileName)
        {
            return "";
        }

        private string RemoveData()
        {
            return "";
        }
    }
}
