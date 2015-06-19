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
using DotSpatial.Projections;

namespace SDMPBSiteEditorPlugin
{
    public partial class ImportCSV : Form
    {
        private DataTable _dt = null;
        private IMap _map = null;
        FeatureSet _fs = null;

        public ImportCSV(IMap map)
        {
            InitializeComponent();
            _map = map ;
        }

        private void ImportCSV_Load(object sender, EventArgs e)
        {
            btnLoadToMap.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_fs == null)
            {
                MessageBox.Show("Please load a file.");
                return;
            }
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = "shp";
            sfd.Title = "Save shapefile and delimited file";
            string fileName = Path.GetFileNameWithoutExtension(txtFile.Text);
            sfd.FileName = fileName;
            DialogResult dr = sfd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.Cancel)
                return;

            
            fileName = sfd.FileName;

            for (int i = 0; i < _fs.Features.Count; i++)
            {
                IFeature feature = _fs.Features[i];
                double x = feature.BasicGeometry.Coordinates[0].X;
                double y = feature.BasicGeometry.Coordinates[0].Y;

                double[] pts = { x, y };
                Reproject.ReprojectPoints(pts, null, _map.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);

                string xField = cbXField.Text;
                string yField = cbYField.Text;

                feature.DataRow[xField] = pts[0];
                feature.DataRow[yField] = pts[1];
            }

            _fs.SaveAs(fileName, true);


            string csvFileName = Path.ChangeExtension(fileName, "csv");
            string delim = cbDelimiter.Text;
            StreamWriter sw = new StreamWriter(csvFileName);
            for (int i = 0; i < _fs.Features.Count; i++)
            {
                IFeature feature = _fs.Features[i];
                int count = feature.DataRow.ItemArray.Count();
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < count; j++)
                {
                    if (j > 0)
                        sb.Append(delim);
                    sb.Append(feature.DataRow.ItemArray[j]);
                }
                sw.WriteLine(sb.ToString());
            }

            sw.Flush();
            sw.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {            
 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.Title = "Select the delimited file containing lat/lon points to import.";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            txtFile.Text = ofd.FileName;                        
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            StreamReader sr = null;
            bool bNoErrors = true;
            string delims;
            char delim;
            try
            {
                if (_dt != null)
                {
                    _dt.Clear();
                    _dt = null;
                }
                //Bunch of error handling for inputs and file integrity
                delims = cbDelimiter.Text;
                if (string.IsNullOrWhiteSpace(delims))
                    throw new Exception("Please specify a valid delimeter.");

                char[] cdelims = delims.ToCharArray();
                delim = cdelims[0];
                string filePath = txtFile.Text;
                if (!File.Exists(filePath))
                    throw new Exception("File " + filePath + " does not exist.");

                sr = new StreamReader(filePath);
                //Read the first line to get column headers
                string line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("The first line must contain the column headers.");

                //Bunch of error checking to make sure file is valid
                string[] columns = line.Split(delim);
                if (columns == null || columns.Length < 2)
                    throw new Exception("File must contain at least latitude and longitude columns.");

                _dt = new DataTable();
                List<string> columnNames = new List<string>();
                for (int i = 0; i < columns.Length; i++)
                {
                    string colName = columns[i].Trim();
                    if (string.IsNullOrWhiteSpace(colName))
                        throw new Exception("File contains an invalid column header in column: " + i + 1);

                    if (colName.Length > 10)
                        throw new Exception("Column names must be 10 characters or less.");

                    _dt.Columns.Add(colName);
                    columnNames.Add(colName);
                }

                //Lets user pick columns for X and Y values
                cbXField.DataSource = columnNames.ToArray();
                cbYField.DataSource = columnNames.ToArray();
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(delim);
                    DataRow row = _dt.NewRow();
                    for (int i = 0; i < columns.Length; i++)                    
                        row[i] = data[i].Trim();

                    _dt.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (_dt != null)
                {
                    _dt.Clear();
                    _dt = null;
                }

                bNoErrors = false;
            }

            if (sr != null)
                sr.Close();

            btnLoadToMap.Enabled = bNoErrors;
        }

        /// <summary>
        /// This assumes the file has been opened, parsed and put into a datatable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadToMap_Click(object sender, EventArgs e)
        {
            //Not sure how this would happen
            if (_dt == null)
            {
                MessageBox.Show("Please open a file.");
                return;
            }

            try
            {

                string xField = cbXField.Text;
                string yField = cbYField.Text;
                string fileName = txtFile.Text;
                fileName = Path.GetFileNameWithoutExtension(fileName);

                _fs = new FeatureSet(FeatureType.Point);
                _fs.Name = fileName;

                for (int i = 0; i < _dt.Columns.Count; i++)
                    _fs.DataTable.Columns.Add(_dt.Columns[i].ColumnName);


                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    double x = Convert.ToDouble(_dt.Rows[i][xField].ToString());
                    double y = Convert.ToDouble(_dt.Rows[i][yField].ToString());
                    Coordinate coord = new Coordinate(x, y);

                    double[] pts = { x, y };
                    Reproject.ReprojectPoints(pts, null, KnownCoordinateSystems.Geographic.World.WGS1984, _map.Projection, 0, 1);

                    DotSpatial.Topology.Point pt = new DotSpatial.Topology.Point(pts[0], pts[1]);
                    IFeature feature = _fs.AddFeature(pt);
                    for (int j = 0; j < _dt.Columns.Count; j++)
                        feature.DataRow[_dt.Columns[j].ColumnName] = _dt.Rows[i][j].ToString();
                }

                IMapFeatureLayer iMFL = _map.Layers.Add(_fs);
                _map.ViewExtents = iMFL.Extent;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
