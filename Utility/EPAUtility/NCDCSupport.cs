using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4EM.Data.Source;
using System.IO;
using System.Data;
using DotSpatial.Data;
using DotSpatial.Projections;

namespace EPAUtility
{
    public class NCDCSupport
    {

        public string variablesTableFile = Path.GetFullPath(@"..\Bin\NCDCVariables.csv");

        private string _aProjectFolderNCDC;
        private int _startDate;
        private int _endDate;
        private DateTime startDate;
        private DateTime endDate;
        private string _token;
        private string _stationID;
        private string _stationName;
        private string _variableID;
        private string _variableName;
        private string _outputtype;
        private string _datasettype;
        double _latitude;
        double _longitude;

        public NCDCSupport()
        {
        }

        public NCDCSupport(string variablesFile)
        {
            variablesTableFile = variablesFile;
        }

        public DataTable createVariablesTable()
        {
            TextReader tr = new StreamReader(variablesTableFile);
            DataTable datat = new DataTable();
            datat.Columns.Add("VariableID");
            datat.Columns.Add("VariableName");
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                string[] varinfo = line.Split(',');
                DataRow dr = datat.NewRow();
                dr["VariableID"] = varinfo[0].ToString();
                dr["VariableName"] = varinfo[1].ToString();
                datat.Rows.Add(dr);
            }
            tr.Close();
            return datat;
        }

        public NCDCSupport(string token, string aProjectFolder, string stationID, string stationName, string variableID, string variableName, string datasettype, string outputtype, int startDate, int endDate)
        {
            _aProjectFolderNCDC = aProjectFolder;
            _startDate = startDate;
            _endDate = endDate;
            _token = token;
            _stationID = stationID;
            _stationName = stationName;
            _variableID = variableID;
            _variableName = variableName;
            _outputtype = outputtype;
            _datasettype = datasettype;
            //   _latitude = latitude;
            //   _longitude = longitude;
        }


        public DataTable populateStationsTableAndWriteShapefile(string token, string state, string folder = "")
        {
            string subFolder = "";
            if (folder == "")
            {
                subFolder = System.IO.Path.Combine(_aProjectFolderNCDC, _stationID + "_" + _variableID);
            }
            else
            {
                subFolder = folder;
            }
            Directory.CreateDirectory(subFolder);

            DataTable dt = populateStationsTable(token, state);

            string shapefile = System.IO.Path.Combine(folder, "NCDCstations(" + state + ").shp");
            writeStationsShapefile(shapefile, dt);
            return dt;
        }

        private void writeStationsShapefile(string shapefile, DataTable dt)
        {
            FeatureSet pointCoords = new FeatureSet();
            DataColumn column1 = new DataColumn("StationID");
            pointCoords.DataTable.Columns.Add(column1);
            DataColumn column2 = new DataColumn("StationName");
            pointCoords.DataTable.Columns.Add(column2);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;

            foreach (DataRow dr in dt.Rows)
            {
                string stationID = dr[0].ToString();
                string stationName = dr[1].ToString();
                double latitude = Convert.ToDouble(dr[2].ToString());
                double longitude = Convert.ToDouble(dr[3].ToString());
                DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                IFeature currentFeature = pointCoords.AddFeature(point);
                currentFeature.DataRow["StationID"] = stationID;
                currentFeature.DataRow["StationName"] = stationName;
            }

            pointCoords.SaveAs(shapefile, true);
        }


        public DataTable populateStationsTable(string token, string state)
        {
            DataTable datatable = new DataTable();

            NCDC.token = token;
            //NCDC.StateAbbreviations sa = (NCDC.StateAbbreviations)System.Enum.Parse(typeof(NCDC.StateAbbreviations), state);

            NCDC.OutputTypes ot = new NCDC.OutputTypes();
            ot = NCDC.OutputTypes.csv;

            startDate = new DateTime(1992, 11, 1);
            endDate = new DateTime(1993, 11, 1);

           // string site = NCDC.GetSites(ot, NCDC.LayerSpecifications.Daily, D4EM.Data.National.StateAbbreviationFromName(state));
            string site = NCDC.GetSites(ot, NCDC.LayerSpecifications.Daily, state);

            datatable.Columns.Add("StationID");
            datatable.Columns.Add("StationName");
            datatable.Columns.Add("Latitude");
            datatable.Columns.Add("Longitude");

            string[] sites = site.Split('\n');

            foreach (string s in sites)
            {
                if (s == "")
                {
                    continue;
                }
                string[] siteinfo = s.Split(',');
                DataRow dr = datatable.NewRow();
                dr["StationID"] = siteinfo[1].ToString();
                dr["StationName"] = siteinfo[3].ToString();
                dr["Latitude"] = siteinfo[4].ToString();
                dr["Longitude"] = siteinfo[5].ToString();
                datatable.Rows.Add(dr);
            }


            return datatable;
        }


        public void WriteNCDCValuesFile(string folder = "")
        {
            NCDC.token = _token;
            NCDC.OutputTypes ot = new NCDC.OutputTypes();

            D4EM.Data.LayerSpecification dt = NCDC.LayerSpecifications.ISH;

            switch (_datasettype)
            {
                case "ish":
                    dt = NCDC.LayerSpecifications.ISH;
                    break;
                case "isd":
                    dt = NCDC.LayerSpecifications.ISD;
                    break;
                case "daily":
                    dt = NCDC.LayerSpecifications.Daily;
                    break;
            }

            string subFolder = "";
            if (folder == "")
            {
                subFolder = System.IO.Path.Combine(_aProjectFolderNCDC, _stationID + "_" + _variableID);
            }
            else
            {
                subFolder = folder;
            }
            Directory.CreateDirectory(subFolder);

            TextWriter tw;
            string filename;

            string values;
            switch (_outputtype)
            {
                case "csv":
                    ot = NCDC.OutputTypes.csv;
                    values = NCDC.GetValues(ot, dt, _stationID, _variableID, startDate, endDate);
                    filename = System.IO.Path.Combine(subFolder, _stationID + "_" + _variableID + ".csv");
                    tw = new StreamWriter(filename);
                    tw.WriteLine("awsId,wbanId,gmtDate,gmtTime,elemId,elemfld1,elemfld2,elemfld3,elemfld4,elemfld5,elemfld6,elemfld7,elemfld8,elemfld9,elemfld10,elemfld11,elemfld12,elemfld13,dataSrcFlag,rptType");
                    tw.WriteLine(values);
                    tw.Close();
                    break;
                case "waterml":
                    ot = NCDC.OutputTypes.waterml;
                    values = NCDC.GetValues(ot, dt, _stationID, _variableID, startDate, endDate);
                    filename = System.IO.Path.Combine(subFolder, _stationID + "_" + _variableID + "_waterml.xml");
                    tw = new StreamWriter(filename);
                    tw.WriteLine(values);
                    tw.Close();
                    break;
                case "xml":
                    ot = NCDC.OutputTypes.xml;
                    values = NCDC.GetValues(ot, dt, _stationID, _variableID, startDate, endDate);
                    filename = System.IO.Path.Combine(subFolder, _stationID + "_" + _variableID + ".xml");
                    tw = new StreamWriter(filename);
                    tw.WriteLine(values);
                    tw.Close();
                    break;
            }
            string shapefile = System.IO.Path.Combine(subFolder, _stationID + "_" + _variableID + ".shp");
            //      writeShapeFile(_latitude, _longitude, shapefile);
        }

        public void writeShapeFile(double latitude, double longitude, string shapefile)
        {
            FeatureSet pointCoords = new FeatureSet();
            DataColumn column1 = new DataColumn("StationID");
            pointCoords.DataTable.Columns.Add(column1);
            DataColumn column2 = new DataColumn("StationName");
            pointCoords.DataTable.Columns.Add(column2);
            DataColumn column3 = new DataColumn("VariableID");
            pointCoords.DataTable.Columns.Add(column3);
            DataColumn column4 = new DataColumn("VariableName");
            pointCoords.DataTable.Columns.Add(column4);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
            DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
            IFeature currentFeature = pointCoords.AddFeature(point);
            currentFeature.DataRow["StationID"] = _stationID;
            currentFeature.DataRow["StationName"] = _stationName;
            currentFeature.DataRow["VariableID"] = _variableID;
            currentFeature.DataRow["VariableName"] = _variableName;
            pointCoords.SaveAs(shapefile, true);
        }
    }
}
