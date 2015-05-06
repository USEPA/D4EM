using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;

namespace D4EM_USGS_Seamless
{
    public partial class NLCDTable : Form
    {        
        DataTable _dt1992 = new DataTable();
        DataTable _dt2001 = new DataTable();
        DataTable _dt2006 = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string _file1992 = "";
        string _file2001 = "";
        string _file2006 = "";
        string file1 = "";
        string file2 = "";
        string file3 = "";
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;

        public NLCDTable(DataTable dt1992, DataTable dt2001, DataTable dt2006, string file1992, string file2001, string file2006, double north, double south, double east, double west)
        {
            _dt1992 = dt1992;
            _dt2001 = dt2001;
            _dt2006 = dt2006;
            _file1992 = file1992;
            _file2001 = file2001;
            _file2006 = file2006;
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            InitializeComponent();
        }

        private void LandCoverData_Load(object sender, EventArgs e)
        {
            string title = "NLCD  (North=" + String.Format("{0:0.0}", _north) + " South=" + String.Format("{0:0.0}", _south) + " East=" + String.Format("{0:0.0}", _east) + " West=" + String.Format("{0:0.0}", _west) + ")";
            this.Text = title;
            if (_dt1992 != null)
            {
                if (_dt2001 != null)
                {
                    if (_dt2006 != null)
                    {
                        populateDataGrid(_dt1992, dataGridView4);
                        label1Header.Text = "1992 LandCover";
                        dt1 = _dt1992;
                        file1 = _file1992;
                        populateDataGrid(_dt2001, dataGridView2);
                        label2Header.Text = "2001 LandCover";
                        dt2 = _dt2001;
                        file2 = _file2001;
                        populateDataGrid(_dt2006, dataGridView3);
                        label3Header.Text = "2006 LandCover";
                        dt3 = _dt2006;
                        file3 = _file2006;

                    }
                    else
                    {
                        populateDataGrid(_dt1992, dataGridView4);
                        label1Header.Text = "1992 LandCover";
                        dt1 = _dt1992;
                        file1 = _file1992;
                        populateDataGrid(_dt2001, dataGridView2);
                        label2Header.Text = "2001 LandCover";
                        dt2 = _dt2001;
                        file2 = _file2001;
                        panel3.Visible = false;
                        this.Height = this.Height - panel3.Height;
                    }
                }
                else if (_dt2006 != null)
                {
                    populateDataGrid(_dt1992, dataGridView4);
                    label1Header.Text = "1992 LandCover";
                    dt1 = _dt1992;
                    file1 = _file1992;
                    populateDataGrid(_dt2006, dataGridView2);
                    label2Header.Text = "2006 LandCover";
                    dt2 = _dt2006;
                    file2 = _file2006;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height;
                }
                else
                {
                    populateDataGrid(_dt1992, dataGridView4);
                    label1Header.Text = "1992 LandCover";
                    dt1 = _dt1992;
                    file1 = _file1992;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height - panel2.Height;
                }
            }
            else if (_dt2001 != null)
            {
                if (_dt2006 != null)
                {
                    populateDataGrid(_dt2001, dataGridView4);
                    label1Header.Text = "2001 LandCover";
                    dt1 = _dt2001;
                    file1 = _file2001;
                    populateDataGrid(_dt2006, dataGridView2);
                    label2Header.Text = "2006 LandCover";
                    dt2 = _dt2006;
                    file2 = _file2006;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height;
                }
                else
                {
                    populateDataGrid(_dt2001, dataGridView4);
                    label1Header.Text = "2001 LandCover";
                    dt1 = _dt2001;
                    file1 = _file2001;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height - panel2.Height;
                }
            }
            else if (_dt2006 != null)
            {
                populateDataGrid(_dt2006, dataGridView4);
                label1Header.Text = "2006 LandCover";
                dt1 = _dt2006;
                file1 = _file2006;
                panel2.Visible = false;
                panel3.Visible = false;
                this.Height = this.Height - panel3.Height - panel2.Height;
            }

            
        }

        private void populateDataGrid(DataTable dt, DataGridView dgv)
        {

            foreach (DataColumn dc in dt.Columns)
            {
                DataGridViewTextBoxColumn columnDataGrid = new DataGridViewTextBoxColumn();
                columnDataGrid.Name = dc.ColumnName;
                //  columnDataGridTextBox.HeaderText = fileDataField[i];
                columnDataGrid.Width = 120;
                dgv.Columns.Add(columnDataGrid);
            }
            
            foreach (DataRow dr in dt.Rows)
            {
                dgv.Rows.Add(dr.ItemArray); 
            }
            
        }

        private void writeFile(string fileName, DataTable dt)
        {
            TextWriter tw = new StreamWriter(fileName);

            int j = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (j < dt.Columns.Count)
                {
                    tw.Write(dc.ColumnName + @" \t ");
                }
                else
                {
                    tw.Write(dc.ColumnName);
                }
                j++;
            }
            tw.WriteLine();

            foreach (DataRow dr in dt.Rows)
            {
                int i = 1;
                foreach (object obj in dr.ItemArray)
                {
                    if (i < dr.ItemArray.Length)
                    {
                        tw.Write(obj.ToString() + @" \t ");
                    }
                    else
                    {
                        tw.Write(obj.ToString());
                    }
                    i++;
                }
                tw.WriteLine();
            }
            tw.Close();
        }

        private void btnNLCDwriteFile1_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file1), "TabulatedNASSData.tsv");
            writeFile(fileName, dt1);

            labelPanel1.Text = "File is located at " + fileName;
            labelPanel1.Visible = true;
            this.Cursor = StoredCursor;
        }

        private void btnNLCDWriteFile2_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file2), "TabulatedNASSData.tsv");

            writeFile(fileName, dt2);

            labelPanel2.Text = "File is located at " + fileName;
            labelPanel2.Visible = true;
            this.Cursor = StoredCursor;
        }

        private void btnNLCDWriteFile3_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file3), "TabulatedNASSData.tsv");
            writeFile(fileName, dt3);

            labelPanel3.Text = "File is located at " + fileName;
            labelPanel3.Visible = true;
            this.Cursor = StoredCursor;
        }
         

       
    }
}
