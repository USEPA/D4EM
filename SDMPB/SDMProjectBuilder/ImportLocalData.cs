using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Projections;
using atcUtility;

namespace SDMProjectBuilder
{
    class ImportLocalData
    {
        FeatureSet _fs = null;
        private AppManager _appMgr = null;
        private IMap _map = null;

        public ImportLocalData(AppManager appMgr)
        {
            _appMgr = appMgr;
            _map = _appMgr.Map;
        }

        public FeatureSet LoadShapefileFromCSV(string fileName, string fileLocation)
        {
            string filePath = Path.Combine(fileLocation, fileName);
            DataTable dt = CSVToDataTable(filePath);
            FeatureSet fs = CreateFeatureSetFromDataTable(dt, fileName);
            return fs;
        }

        /// <summary>
        /// Assume files are comma delimeted
        /// Assume first column Longitude and second column in Latitude
        /// </summary>
        /// <param name="csvFile">CSV File to import</param>
        /// <returns>DataTable filled with csv file contents</returns>
        private DataTable CSVToDataTable(string csvFile)
        {
            DataTable dt = null;
            StreamReader sr = null;
            char delim;
            try
            {                
                delim = ',';
                string filePath = csvFile;
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

                dt = new DataTable();
                List<string> columnNames = new List<string>();
                for (int i = 0; i < columns.Length; i++)
                {
                    string colName = columns[i].Trim();
                    if (string.IsNullOrWhiteSpace(colName))
                        throw new Exception("File contains an invalid column header in column: " + i + 1);

                    if (colName.Length > 10)
                        throw new Exception("Column names must be 10 characters or less.");

                    dt.Columns.Add(colName);
                    columnNames.Add(colName);
                }
                
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(delim);
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < columns.Length; i++)                    
                        row[i] = data[i].Trim();

                    dt.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                if (dt != null)
                {
                    dt.Clear();
                    dt = null;
                }
            }

            if (sr != null)
                sr.Close();

            return dt;
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">DataTable to convert to shapefile</param>
        /// <param name="csvFileName">Filename of csv file</param>
        private FeatureSet CreateFeatureSetFromDataTable(DataTable dt, string csvFileName)
        {            
            try
            {
                if (dt == null)
                    throw new Exception("DataTable is null.");

                string xField = "Longitude";
                string yField = "Latitude";
                string fileName = csvFileName;
                fileName = Path.GetFileNameWithoutExtension(fileName);
                string shpFileName = Path.ChangeExtension(csvFileName,"shp");
                _fs = new FeatureSet(FeatureType.Point);                
                //_fs = new PointShapefile(shpFileName);                
                _fs.Name = fileName;
                _fs.Projection = _map.Projection;

                for (int i = 0; i < dt.Columns.Count; i++)
                    _fs.DataTable.Columns.Add(dt.Columns[i].ColumnName);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double x = Convert.ToDouble(dt.Rows[i][xField].ToString());
                    double y = Convert.ToDouble(dt.Rows[i][yField].ToString());
                    //Coordinate coord = new Coordinate(x, y);
                    double[] pts = { x, y };

                    //Shape shpPt = new Shape(coord);
                    Reproject.ReprojectPoints(pts, null, KnownCoordinateSystems.Geographic.World.WGS1984, _map.Projection, 0, 1);

                    DotSpatial.Topology.Point pt = new DotSpatial.Topology.Point(pts[0], pts[1]);
                    IFeature feature = _fs.AddFeature(pt);
                    for (int j = 0; j < dt.Columns.Count; j++)
                        feature.DataRow[dt.Columns[j].ColumnName] = dt.Rows[i][j].ToString();
                }

                //IMapFeatureLayer iMFL = _map.Layers.Add(_fs);
                //_map.ViewExtents = iMFL.Extent;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            return _fs;
        }

        public void SaveData(FeatureSet featureSet, string saveFileName)
        {
            try
            {
                _fs = featureSet;
                if (_fs == null)
                    throw new Exception("FeatureSet is null.");

                string fileName = saveFileName;

                for (int i = 0; i < _fs.Features.Count; i++)
                {
                    IFeature feature = _fs.Features[i];
                    double x = feature.BasicGeometry.Coordinates[0].X;
                    double y = feature.BasicGeometry.Coordinates[0].Y;

                    double[] pts = { x, y };
                    Reproject.ReprojectPoints(pts, null, _map.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);

                    feature.DataRow[0] = pts[0];
                    feature.DataRow[1] = pts[1];
                }

                _fs.SaveAs(fileName, true);


                string csvFileName = Path.ChangeExtension(fileName, "csv");
                string delim = ",";
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
            catch (Exception ex)
            {

            }
        }

        public string CreateShapefileFromTable(string aSaveAsBaseFilename, atcTable aTable, int aLatField, int aLonField, ProjectionInfo aDesiredProjection)
        {
            string lShpFileName = Path.ChangeExtension(aSaveAsBaseFilename, ".shp");
            if (File.Exists(lShpFileName))
                File.Delete(lShpFileName);            

            PointShapefile lLayer = new DotSpatial.Data.PointShapefile(lShpFileName);

            atcTableDBF lDBF = new atcTableDBF();
            int[] lFieldLengths = aTable.ComputeFieldLengths();
            lDBF.NumFields = aTable.NumFields;
            int lFieldIndex = 0;
            for (lFieldIndex = 1; lFieldIndex <= aTable.NumFields; lFieldIndex++)
            {
                lDBF.set_FieldName(lFieldIndex, aTable.get_FieldName(lFieldIndex));
                //lDBF.set_FieldLength(lFieldIndex, Math.Max(lFieldLengths(lFieldIndex), 8));
                lDBF.set_FieldLength(lFieldIndex, 10);
                lDBF.set_FieldType(lFieldIndex, "S");
            }
            lDBF.NumRecords = aTable.NumRecords;
            for (int lRecordIndex = 1; lRecordIndex <= aTable.NumRecords; lRecordIndex++)
            {
                aTable.CurrentRecord = lRecordIndex;
                lDBF.CurrentRecord = lRecordIndex;
                for (lFieldIndex = 1; lFieldIndex <= aTable.NumFields; lFieldIndex++)
                {
                    lDBF.set_Value(lFieldIndex, aTable.get_Value(lFieldIndex));
                }

                double longitude = Convert.ToDouble(aTable.get_Value(aLonField));
                double latitude = Convert.ToDouble(aTable.get_Value(aLatField));
                Coordinate lPoint = new DotSpatial.Topology.Coordinate(longitude, latitude);
                Shape lShape = new DotSpatial.Data.Shape(lPoint);
                Reproject.ReprojectPoints(lShape.Vertices, lShape.Z, KnownCoordinateSystems.Geographic.World.WGS1984, aDesiredProjection, 0, 1);
                lLayer.AddShape(lShape);
            }
            lLayer.M = null;
            lLayer.Z = null;
            lLayer.Projection = aDesiredProjection;
            lLayer.SaveAs(lShpFileName, true);
            lLayer.Close();

            lDBF.WriteFile(Path.ChangeExtension(aSaveAsBaseFilename, ".dbf"));
            return lShpFileName;
        }
    }

    
}
