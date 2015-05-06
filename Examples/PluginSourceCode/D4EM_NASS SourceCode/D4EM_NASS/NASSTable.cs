using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace D4EM_NASS
{
    public partial class NASSTable : Form
    {
        DataTable _dt2008 = new DataTable();
        DataTable _dt2009 = new DataTable();
        DataTable _dt2010 = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string _file2008 = "";
        string _file2009 = "";
        string _file2010 = "";
        string file1 = "";
        string file2 = "";
        string file3 = "";        
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;
        

        public NASSTable(DataTable dt2008, DataTable dt2009, DataTable dt2010, string file2008, string file2009, string file2010, double north, double south, double east, double west)
        {
            _dt2008 = dt2008;
            _dt2009 = dt2009;
            _dt2010 = dt2010;
            _file2008 = file2008;
            _file2009 = file2009;
            _file2010 = file2010;           
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            
            InitializeComponent();
        }

        private void NASSTable_Load(object sender, EventArgs e)
        {
            
            string title = "NASS  (North=" + String.Format("{0:0.0}", _north) + " South=" + String.Format("{0:0.0}", _south) + " East=" + String.Format("{0:0.0}", _east) + " West=" + String.Format("{0:0.0}", _west) +")";
            
            this.Text = title;
            if (_dt2008 != null)
            {
                if (_dt2009 != null)
                {
                    if (_dt2010 != null)
                    {
                        populateDataGrid(_dt2008, dataGridView1);
                        label1Header.Text = "2008";
                        dt1 = _dt2008;
                        file1 = _file2008;
                        populateDataGrid(_dt2009, dataGridView2);
                        label2Header.Text = "2009";
                        dt2 = _dt2009;
                        file2 = _file2009;
                        populateDataGrid(_dt2010, dataGridView3);
                        label3Header.Text = "2010";
                        dt3 = _dt2010;
                        file3 = _file2010;
                        
                    }
                    else
                    {
                        populateDataGrid(_dt2008, dataGridView1);
                        label1Header.Text = "2008";
                        dt1 = _dt2008;
                        file1 = _file2008;
                        populateDataGrid(_dt2009, dataGridView2);
                        label2Header.Text = "2009";
                        dt2 = _dt2009;
                        file2 = _file2009;
                        panel3.Visible = false;
                        this.Height = this.Height - panel3.Height;
                    }
                }
                else if (_dt2010 != null)
                {
                    populateDataGrid(_dt2008, dataGridView1);
                    label1Header.Text = "2008";
                    dt1 = _dt2008;
                    file1 = _file2008;
                    populateDataGrid(_dt2010, dataGridView2);
                    label2Header.Text = "2010";
                    dt2 = _dt2010;
                    file2 = _file2010;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height;
                }
                else
                {
                    populateDataGrid(_dt2008, dataGridView1);
                    label1Header.Text = "2008";
                    dt1 = _dt2008;
                    file1 = _file2008;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height - panel2.Height;
                }
            }
            else if (_dt2009 != null)
            {
                if (_dt2010 != null)
                {                   
                    populateDataGrid(_dt2009, dataGridView1);
                    label1Header.Text = "2009";
                    dt1 = _dt2009;
                    file1 = _file2009;
                    populateDataGrid(_dt2010, dataGridView2);
                    label2Header.Text = "2010";
                    dt2 = _dt2010;
                    file2 = _file2010;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height;
                }
                else
                {                    
                    populateDataGrid(_dt2009, dataGridView1);
                    label1Header.Text = "2009";
                    dt1 = _dt2009;
                    file1 = _file2009;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    this.Height = this.Height - panel3.Height - panel2.Height;
                }
            }
            else if (_dt2010 != null)
            {
                populateDataGrid(_dt2010, dataGridView1);
                label1Header.Text = "2010";
                dt1 = _dt2010;
                file1 = _file2010;
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
            double totalarea = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string code = dr[0].ToString();
                string cmdty = dr[1].ToString();
                string perc = dr[2].ToString();
                string area = dr[3].ToString();
                dgv.Rows.Add(code, cmdty, perc, area);
                totalarea = Convert.ToDouble(area) + totalarea;
            }
            dgv.Rows.Add("Total", "", "", totalarea);
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

        private void btnNASSwriteFile_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file1), "TabulatedNASSData.tsv");
            writeFile(fileName, dt1);
            
            labelPanel1.Text = "File is located at " + fileName;
            labelPanel1.Visible = true;

            
            this.Cursor = StoredCursor;
        }

        private void btnNASSWriteFile2_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file2), "TabulatedNASSData.tsv");

            writeFile(fileName, dt2);

            labelPanel2.Text = "File is located at " + fileName;
            labelPanel2.Visible = true;
            this.Cursor = StoredCursor;
        }

        private void btnNASSWriteFile3_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(file3), "TabulatedNASSData.tsv");
            writeFile(fileName, dt3);
            
            labelPanel3.Text = "File is located at " + fileName;
            labelPanel3.Visible = true;
            this.Cursor = StoredCursor;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1Header_Click(object sender, EventArgs e)
        {

        }

        private void labelPanel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3Header_Click(object sender, EventArgs e)
        {

        }

        private void labelPanel3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2Header_Click(object sender, EventArgs e)
        {

        }

        private void labelPanel2_Click(object sender, EventArgs e)
        {

        }

       
    }
}
