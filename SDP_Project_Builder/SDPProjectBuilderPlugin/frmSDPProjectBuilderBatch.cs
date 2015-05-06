using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDP_Project_Builder_Batch;
using D4EM.Data.DBManager;

namespace SDPProjectBuilderPlugin
{
    public partial class frmSDPProjectBuilderBatch : Form
    {

        private DataTable _dtFiles = null;
        private string _batchProjectFile = String.Empty;

        public string BatchProjectFile
        {
            get { return _batchProjectFile; }
            set { _batchProjectFile = value; }
        }

        public frmSDPProjectBuilderBatch()
        {
            InitializeComponent();

            CreateFilesTable();

            ConfigureFilesGrid();
            dgvFiles.DataSource = _dtFiles;
        }

        public void ConfigureFilesGrid()
        {

            dgvFiles.MultiSelect = true;
            dgvFiles.AutoGenerateColumns = false;
            dgvFiles.AllowUserToAddRows = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = "colFilePath";
            col.HeaderText = "File Path";
            col.DataPropertyName = "FilePath";
            col.Width = 425;
            dgvFiles.Columns.Add(col);
        
        }

        public void CreateFilesTable()
        {
            _dtFiles = new DataTable("Files");
            //add columns
            DataColumn col = new DataColumn("FilePath");
            col.DataType = System.Type.GetType("System.String");
            _dtFiles.Columns.Add(col);

            //col = new DataColumn("Dependency");
            //col.DataType = System.Type.GetType("System.String");
            //_dtFiles.Columns.Add(col); 
        }

        private void btnBrowseProjectFile_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFile(txtProjectFile);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sFilePath = txtProjectFile.Text.Trim();

            if (String.IsNullOrEmpty(sFilePath))
            {
                MessageBox.Show("You must first select a file to add.");
                return;
            }

            DataRow[] foundRows;
            // Use the Select method to find all rows matching the filter.
            foundRows = _dtFiles.Select("FilePath = '" + sFilePath + "'");

            if (foundRows.Length > 0)
            {
                MessageBox.Show("This file has already been added to the batch.");
                return;
            }

            _dtFiles.Rows.Add(sFilePath);
            txtProjectFile.Text = "";
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            _dtFiles.Rows.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvFiles.SelectedRows)
            {
                dgvFiles.Rows.Remove(dr);
            } 
        }

        public SDPBatchParameters GetBatchParameters()
        {
            SDPBatchParameters parameters = new SDPBatchParameters();
            parameters.BatchName = txtBatchName.Text.Trim();
            parameters.BatchDescription = txtBatchDescription.Text.Trim();

            foreach (DataRow row in _dtFiles.Rows)
            {
                string sFilePath = row["FilePath"].ToString();
                parameters.ProjectFiles.Add(sFilePath);
            } 

            return parameters;

        }

        public void LoadBatchParameters(SDPBatchParameters parameters)
        {
            txtBatchName.Text = parameters.BatchName;
            txtBatchDescription.Text = parameters.BatchDescription;     
            
            foreach (string s in parameters.ProjectFiles)
            {
                _dtFiles.Rows.Add(s);
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_batchProjectFile))
            {
                MessageBox.Show("You must save this batch before running it.");
                return;            
            }

            SDP_Project_Builder_Batch.SDP_Project_Builder_Batch batch = new SDP_Project_Builder_Batch.SDP_Project_Builder_Batch(_batchProjectFile);
            batch.go();

            MessageBox.Show("Batch Project Run Complete!");

        }

    }
}
