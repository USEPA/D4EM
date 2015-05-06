using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Xml;
using System.Data.SqlClient;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Analysis;

namespace EPAUtility
{
    public class StoretFileSupport
    {
        
        private string _xmlfile;
        private string _csvfile;
        private string _subfolder;
        public List<string> FileNames = new List<string>();

        static double _north;
        static double _south;
        static double _east;
        static double _west;
       
        /*    
         *    LocationIDTable reads the stations.csv file and creates a datatable 
         *    with a primary key and a StationID
         *    
         *    LatLongTable reads the stations.xml file and creates a datatable with a
         *    primary key (corresponding to that of the stationID table) and lat/longs
         * 
         *    CombineTables combines these two tables.  
         *    
         *    Warning:  
         *    There is a built-in assumption that the stations are listed in the same 
         *    order in the xml and csv files.  If the stations ar not listed in the 
         *    same order in the xml and csv files, then the stations will be assigned 
         *    the wrong StationID.
         * 
         * */


        
        public StoretFileSupport()
        {
        }

        public void WriteStoretFiles(string file, string subFolder, double north, double south, double east, double west)
        {
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            _subfolder = subFolder;
            _xmlfile = file + ".xml";
            _csvfile = file + ".csv";

            string shapefilename = System.IO.Path.Combine(_subfolder, "N" + _north + ";S" + _south + ";E" + _east + ";W" + _west + ".shp");           
            
            DataTable dt_ids = LocationIDTable(_csvfile);
            int rows = dt_ids.Rows.Count;

            DataTable dt_coords = LatLongTable(_xmlfile);
            int rows2 = dt_coords.Rows.Count;

            DataTable dt = CombineTables(dt_ids, dt_coords);

            FeatureSet fs = makeFeatureSet(dt);
            fs.SaveAs(shapefilename, true);

            string metadataFileName = Path.Combine(_subfolder, "Metadata N" + _north + ";S" + _south + ";E" + _east + ";W" + _west + ".txt");
            writeMetaDataFile(metadataFileName, shapefilename);

            FileNames.Add(shapefilename);
            FileNames.Add(_xmlfile);
            FileNames.Add(_csvfile);
            FileNames.Add(metadataFileName);
        }

        private DataTable CombineTables(DataTable dt_ids, DataTable dt_coords)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Longitude");
            dt.Columns.Add("Latitude");  
            dt.Columns.Add("OrganizationIdentifier");
            dt.Columns.Add("OrganizationFormalName");
            dt.Columns.Add("MonitoringLocationID");

            string[] columns = new string[dt_coords.Columns.Count];
            int k = 0;
            foreach (DataColumn dc in dt_coords.Columns)
            {
                columns[k] = dc.ColumnName.ToString();
                dt.Columns.Add(columns[k]);
                k++;
            }

            for (int i =0; i < dt_ids.Rows.Count; i++)
            {
                string organizationIdentifier = dt_ids.Rows[i][1].ToString();
                string organizationFormalName = dt_ids.Rows[i][2].ToString();
                string monitoringLocationID = dt_ids.Rows[i][3].ToString();  
              
                string lng = dt_coords.Rows[i]["LongitudeMeasure"].ToString();
                string lat = dt_coords.Rows[i]["LatitudeMeasure"].ToString();
                DataRow drow = dt.NewRow();
                drow["OrganizationIdentifier"] = organizationIdentifier;
                drow["OrganizationFormalName"] = organizationFormalName;
                drow["MonitoringLocationID"] = monitoringLocationID;
                int l = 0;
                
                foreach (string column in columns)
                {
                    string columnName = columns[l];
                    string val = dt_coords.Rows[i][columnName].ToString();
                    drow[columnName] = dt_coords.Rows[i][columnName].ToString();
                    l++;
                }
                dt.Rows.Add(drow); 
            }            
            return dt;
        }

        private DataTable LocationIDTable(string csvfile)
        {
            DataTable idTable = new DataTable();

            idTable.Columns.Add("Key");
            idTable.Columns.Add("OrganizationIdentifier");
            idTable.Columns.Add("OrganizationFormalName");
            idTable.Columns.Add("MonitoringLocationID");

            TextReader tr = new StreamReader(csvfile);
            string line;
            line = tr.ReadLine();

            int i = 0;
            while ((line = tr.ReadLine()) != null)
            {
                string[] sites = line.Split(',');
                string organizationIdentifier = sites[0];
                string organizationFormalName = sites[1];
                string monitoringLocationID = sites[2];
                idTable.Rows.Add(i, organizationIdentifier, organizationFormalName, monitoringLocationID);
                i++;
            }
            tr.Close();
            return idTable;
        }

        private DataTable LatLongTable(string xmlfile)
        {
            DataTable dtable = new DataTable();

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.ReadXml(xmlfile);
            
            DataTable dt0 = dataSet.Tables[0];
            DataTable dt1 = dataSet.Tables[1];
            DataTable dt2 = dataSet.Tables[2];
            DataTable dt3 = dataSet.Tables[3];
            DataTable dt4 = dataSet.Tables[4];
            DataTable dt5 = dataSet.Tables[5];

            string[] columns = new string[dt4.Columns.Count];
            int k = 0;
            foreach (DataColumn dc in dt4.Columns)
            {
                columns[k] = dc.ColumnName.ToString();
                k++;
            }

            DataView dv4 = new DataView(dataSet.Tables[4]);
            int j= 0;
            foreach (string column in columns)
            {
                dtable.Columns.Add(columns[j]);
                j++;
            }
           
            int i = 0;

            foreach (DataRowView dr in dv4)
            {
                string monitoringLocationGeospatialID = dv4[i]["MonitoringLocationGeospatial_Id"].ToString();
                string monitoringLocationID = dv4[i]["MonitoringLocation_Id"].ToString();
                string longitude = dv4[i]["LongitudeMeasure"].ToString();
                string latitude = dv4[i]["LatitudeMeasure"].ToString();

                int l = 0;
                string[] values = new string[dt4.Columns.Count];
                DataRow drow = dtable.NewRow();
                foreach (string column in columns)
                {
                    
                    string columnName = columns[l];
                    string val = dv4[i][columnName].ToString();
                    drow[columnName] = dv4[i][columnName].ToString();
                    
                    l++;                    
                }
                dtable.Rows.Add(drow); 
                i++;
            }
            return dtable;
        }

        private FeatureSet makeFeatureSet(DataTable dtable)
        {
            FeatureSet pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
           
            DataColumn column1 = new DataColumn("OrgID");           
            DataColumn column2 = new DataColumn("OrgName");
            DataColumn column3 = new DataColumn("LocationID");
            pointCoords.DataTable.Columns.Add(column1);
            pointCoords.DataTable.Columns.Add(column2);
            pointCoords.DataTable.Columns.Add(column3);

            string[] columns = new string[dtable.Columns.Count];
            int k = 0;
            foreach (DataColumn dc in dtable.Columns)
            {
                columns[k] = dc.ColumnName.ToString();
                k++;
            }

            for (int i = 5; i < dtable.Columns.Count; i++)
            {
                pointCoords.DataTable.Columns.Add(columns[i]);
            }
            
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            int p = 0;
            foreach(DataRow dr in dtable.Rows)
            {
                double longitude = Convert.ToDouble(dr["LongitudeMeasure"].ToString());
                double latitude = Convert.ToDouble(dr["LatitudeMeasure"].ToString());
                string orgID = dr["OrganizationIdentifier"].ToString();
                string orgname = dr["OrganizationFormalName"].ToString();
                string locationID = dr["MonitoringLocationId"].ToString();

                string[] values = new string[dtable.Columns.Count];
                for (int l = 5; l < dtable.Columns.Count; l++)
                {
                    values[l] = dtable.Rows[p][columns[l]].ToString();
                }

                DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                IFeature currentFeature = pointCoords.AddFeature(point);               
                
                currentFeature.DataRow["OrgID"] = orgID;
                currentFeature.DataRow["OrgName"] = orgname;
                currentFeature.DataRow["LocationID"] = locationID;
                for (int i = 5; i < dtable.Columns.Count; i++)
                {
                    string columnname = columns[i];
                    string value = values[i].ToString();
                    currentFeature.DataRow[columnname] = value;
                }
                p++;
            }

            return pointCoords;
        }

        public void writeSpecificStoretFiles(string resultsFile, string folder, string dataType)
        {
            dataType = dataType.Replace(",", ";");
            DataTable dt = makeStoretResultsDataTable(resultsFile);
            DataTable dtSpecific = queryStoretTable(dataType, dt);
            List<string> uniqueStations = determineDistinctStationIDs(dtSpecific);

            foreach (string station in uniqueStations)
            {
                DataRow[] selectRows = dtSpecific.Select("MonitoringLocationIdentifier = '" + station + "'");
                Directory.CreateDirectory(System.IO.Path.Combine(folder, dataType));
                TextWriter tw = new StreamWriter(System.IO.Path.Combine(folder, dataType, station + " Results.csv"));

                foreach (DataColumn dc in dt.Columns)
                {
                    tw.Write(dc.ColumnName);
                    tw.Write(",");
                }
                tw.WriteLine();

                foreach (DataRow dr in selectRows)
                {
                    foreach (string item in dr.ItemArray)
                    {
                        tw.Write(item);
                        tw.Write(",");
                    }
                    tw.WriteLine();
                }

                tw.Close();
            }
        }

        private List<string> determineDistinctStationIDs(DataTable dt)
        {           
            List<string> stations = new List<string>();

            DataColumn dc = dt.Columns["MonitoringLocationIdentifier"];

            foreach (DataRow dr in dt.Rows)
            {
                string locationID = dr[dc].ToString();
                stations.Add(locationID);
            }

            stations = stations.Distinct().ToList();

            return stations;
        }


        private DataTable queryStoretTable(string parameter, DataTable fullTable)
        {
            DataTable dt = new DataTable();

            foreach (DataColumn dc in fullTable.Columns)
            {
                dt.Columns.Add(dc.ColumnName);
            }

            DataRow[] result = fullTable.Select("CharacteristicName = '" + parameter + "'");

            foreach (DataRow dr in result)
            {
                string[] newarray = new string[dr.ItemArray.Length];
                int i = 0;
                foreach (string item in dr.ItemArray)
                {
                    newarray[i] = item;
                    i++;
                }
                dt.Rows.Add(newarray);
            }

            return dt;
        }

        private DataTable makeStoretResultsDataTable(string resultsFile)
        {
            DataTable dt = new DataTable();
            TextReader tr = new StreamReader(resultsFile);
            string line = tr.ReadLine();
            string[] columnValues = line.Split(',');
            foreach (string columnValue in columnValues)
            {
                dt.Columns.Add(columnValue);
            }
            int g = 0;
            while ((line = tr.ReadLine()) != null)
            {
                try
                {
                    string[] rowArray = new string[columnValues.Length];
                    string[] firstSplit = line.Split('\"');
                    int i = 0; //array index
                    int j = 0; //keeps track of even/odds
                    int k = 0; //counts number of comma corrections
                    foreach (string splits in firstSplit)
                    {
                        if (j % 2 == 0)
                        {
                            string[] secondSplit = splits.Split(',');
                            foreach (string split in secondSplit)
                            {

                                if (i < columnValues.Length)
                                {
                                    rowArray[i] = split;
                                }
                                i++;
                            }
                        }
                        else
                        {
                            //   if (i < columnValues.Length)
                            //   {
                            k++;
                            i = i - k;
                            string value = splits.Replace(",", ";");
                            rowArray[i] = value;
                            i++;
                            //  }
                        }
                        j++;

                    }
                    dt.Rows.Add(rowArray);
                    g++;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return dt;
        }
        private void writeMetaDataFile(string metadataFileName, string shapefile)
        {
            TextWriter tw = new StreamWriter(metadataFileName);

            tw.WriteLine("Download Time = " + DateTime.Now);
            tw.WriteLine("");
            tw.WriteLine("Area of Interest:");
            tw.WriteLine("North: " + _north);
            tw.WriteLine("South: " + _south);
            tw.WriteLine("East: " + _east);
            tw.WriteLine("West: " + _west);
            tw.WriteLine("");
            tw.WriteLine("Storet data was written into files:");
            tw.WriteLine("CSV File: " + _csvfile);
            tw.WriteLine("XML File: " + _xmlfile);
            tw.WriteLine("Shapefile: " + shapefile);
            tw.Close();
        }
    }
}

