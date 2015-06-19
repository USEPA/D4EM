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
using atcTimeseriesRDB;
using atcUtility;

namespace EPAUtility
{
    public class NWISFileSupport
    {
        public NWISFileSupport()
        {

        }

        public static void writeShapeFile(string aSaveAs, string aSubFolder, string aProjectFolder, string datatype)
        {
            string folder = System.IO.Path.Combine(aProjectFolder, aSubFolder);
            Directory.CreateDirectory(folder);
            FeatureSet pointCoords = new FeatureSet();
            DataColumn column = new DataColumn("StationID");
            pointCoords.DataTable.Columns.Add(column);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            

            List<string> aStationIDs = new List<string>();
            aStationIDs.Clear();
            atcTableRDB atctable = new atcTableRDB();
            atctable.Clear();
            atctable.OpenFile(aSaveAs);
            int fieldnumber = atctable.FieldNumber("site_no");
            int numrecords = atctable.NumRecords;
            int latitudeField = atctable.FieldNumber("dec_lat_va");
            int longitudeField = atctable.FieldNumber("dec_long_va");
            int agencyField = atctable.FieldNumber("agency_cd");
            DataColumn column_agency = new DataColumn("Agency");
            pointCoords.DataTable.Columns.Add(column_agency);
            int stationnameField = atctable.FieldNumber("station_nm");
            DataColumn column_stationname = new DataColumn("StationName");
            pointCoords.DataTable.Columns.Add(column_stationname);
            int sitetypeField = atctable.FieldNumber("site_tp_cd");
            DataColumn column_sitetype = new DataColumn("Site Type");
            pointCoords.DataTable.Columns.Add(column_sitetype);
            int coord_acy_cdField = atctable.FieldNumber("coord_acy_cd");
            DataColumn column_coord_acy_cd = new DataColumn("LatLong Acc"); 
            pointCoords.DataTable.Columns.Add(column_coord_acy_cd);
            int dec_coord_datum_cdField = atctable.FieldNumber("dec_coord_datum_cd");
            DataColumn column_dec_coord_datum_cd = new DataColumn("Decim datum");
            pointCoords.DataTable.Columns.Add(column_dec_coord_datum_cd);
            int district_cdField = atctable.FieldNumber("district_cd");
            DataColumn column_district_cd = new DataColumn("Dist code");
            pointCoords.DataTable.Columns.Add(column_district_cd);
            int state_cdField = atctable.FieldNumber("state_cd");
            DataColumn column_state_cd = new DataColumn("State code");
            pointCoords.DataTable.Columns.Add(column_state_cd);
            int county_cdField = atctable.FieldNumber("county_cd");
            DataColumn column_county_cd = new DataColumn("County code");
            pointCoords.DataTable.Columns.Add(column_county_cd);
            int country_cdField = atctable.FieldNumber("country_cd");
            DataColumn column_country_cd = new DataColumn("CountryCode");
            pointCoords.DataTable.Columns.Add(column_country_cd);
            int land_net_dsField = atctable.FieldNumber("land_net_ds");
            DataColumn column_land_net_ds = new DataColumn("Land net");
            pointCoords.DataTable.Columns.Add(column_land_net_ds);
            int map_nmField = atctable.FieldNumber("map_nm");
            DataColumn column_map_nm = new DataColumn("Map Name");
            pointCoords.DataTable.Columns.Add(column_map_nm);
            int map_scale_fcField = atctable.FieldNumber("map_scale_fc");
            DataColumn column_map_scale_fc = new DataColumn("Map scale");
            pointCoords.DataTable.Columns.Add(column_map_scale_fc);
            int alt_vaField = atctable.FieldNumber("alt_va");
            DataColumn column_alt_va = new DataColumn("Altitude");
            pointCoords.DataTable.Columns.Add(column_alt_va);
            int alt_meth_cdField = atctable.FieldNumber("alt_meth_cd");
            DataColumn column_alt_meth_cd = new DataColumn("Alt Method");
            pointCoords.DataTable.Columns.Add(column_alt_meth_cd);
            int alt_acy_vaField = atctable.FieldNumber("alt_acy_va");
            DataColumn column_alt_acy_va = new DataColumn("Alt Accuracy");
            pointCoords.DataTable.Columns.Add(column_alt_acy_va);
            int alt_datum_cdField = atctable.FieldNumber("alt_datum_cd");
            DataColumn column_alt_datum_cd = new DataColumn("Alt Datum");
            pointCoords.DataTable.Columns.Add(column_alt_datum_cd);
            int huc_cdField = atctable.FieldNumber("huc_cd");
            DataColumn column_huc_cd = new DataColumn("HUC-8");
            pointCoords.DataTable.Columns.Add(column_huc_cd);
            int basin_cdField = atctable.FieldNumber("basin_cd");
            DataColumn column_basin_cd = new DataColumn("Basin code");
            pointCoords.DataTable.Columns.Add(column_basin_cd);
            int topo_cdField = atctable.FieldNumber("topo_cd");
            DataColumn column_topo_cd = new DataColumn("Topogr code");
            pointCoords.DataTable.Columns.Add(column_topo_cd);
            int data_types_cdField = atctable.FieldNumber("data_types_cd");
            DataColumn column_data_types_cd = new DataColumn("Data Types");
            pointCoords.DataTable.Columns.Add(column_data_types_cd);
            int instruments_cdField = atctable.FieldNumber("instruments_cd");
            DataColumn column_instruments_cd = new DataColumn("Instruments");
            pointCoords.DataTable.Columns.Add(column_instruments_cd);
            int construction_dtField = atctable.FieldNumber("construction_dt");
            DataColumn column_construction_dt = new DataColumn("Constr Date");
            pointCoords.DataTable.Columns.Add(column_construction_dt);
            int inventory_dtField = atctable.FieldNumber("inventory_dt");
            DataColumn column_inventory_dt = new DataColumn("Invent Date");
            pointCoords.DataTable.Columns.Add(column_inventory_dt);
            int drain_area_vaField = atctable.FieldNumber("drain_area_va");
            DataColumn column_drain_area_va = new DataColumn("Drainage Area");
            pointCoords.DataTable.Columns.Add(column_drain_area_va);
            int contrib_drain_area_vaField = atctable.FieldNumber("contrib_drain_area_va");
            DataColumn column_contrib_drain_area_va = new DataColumn("Contr Dr Area");
            pointCoords.DataTable.Columns.Add(column_contrib_drain_area_va);
            int tz_cdField = atctable.FieldNumber("tz_cd");
            DataColumn column_tz_cd = new DataColumn("Time offset");
            pointCoords.DataTable.Columns.Add(column_tz_cd);
            int local_time_fgField = atctable.FieldNumber("local_time_fg");
            DataColumn column_local_time_fg = new DataColumn("LocTimeFlag");
            pointCoords.DataTable.Columns.Add(column_local_time_fg);
            int reliability_cdField = atctable.FieldNumber("reliability_cd");
            DataColumn column_reliability_cd = new DataColumn("Reliability");
            pointCoords.DataTable.Columns.Add(column_reliability_cd);
            int gw_file_cdField = atctable.FieldNumber("gw_file_cd");
            DataColumn column_gw_file_cd = new DataColumn("GW files");
            pointCoords.DataTable.Columns.Add(column_gw_file_cd);
            int nat_aqfr_cdField = atctable.FieldNumber("nat_aqfr_cd");
            DataColumn column_nat_aqfr_cd = new DataColumn("Nat Aquifer");
            pointCoords.DataTable.Columns.Add(column_nat_aqfr_cd);
            int aqfr_cdField = atctable.FieldNumber("aqfr_cd");
            DataColumn column_aqfr_cd = new DataColumn("Loc Aquifer");
            pointCoords.DataTable.Columns.Add(column_aqfr_cd);
            int aqfr_type_cdField = atctable.FieldNumber("aqfr_type_cd");
            DataColumn column_aqfr_type_cd = new DataColumn("AquiferType");
            pointCoords.DataTable.Columns.Add(column_aqfr_type_cd);
            int well_depth_vaField = atctable.FieldNumber("well_depth_va");
            DataColumn column_well_depth_va = new DataColumn("Well Depth");
            pointCoords.DataTable.Columns.Add(column_well_depth_va);
            int hole_depth_vaField = atctable.FieldNumber("hole_depth_va");
            DataColumn column_hole_depth_va = new DataColumn("Hole Depth");
            pointCoords.DataTable.Columns.Add(column_hole_depth_va);
            int depth_src_cdField = atctable.FieldNumber("depth_src_cd");
            DataColumn column_depth_src_cd = new DataColumn("Depth Data");
            pointCoords.DataTable.Columns.Add(column_depth_src_cd);
            int project_noField = atctable.FieldNumber("project_no");
            DataColumn column_project_no = new DataColumn("Project Number");
            pointCoords.DataTable.Columns.Add(column_project_no);
            int rt_bolField = atctable.FieldNumber("rt_bol");
            DataColumn column_rt_bol = new DataColumn("Real-time");
            pointCoords.DataTable.Columns.Add(column_rt_bol);
            int peak_begin_dateField = atctable.FieldNumber("peak_begin_date");
            DataColumn column_peak_begin_date = new DataColumn("StrFlow Begin");
            pointCoords.DataTable.Columns.Add(column_peak_begin_date);
            int peak_end_dateField = atctable.FieldNumber("peak_end_date");
            DataColumn column_peak_end_date = new DataColumn("StrFlow End");
            pointCoords.DataTable.Columns.Add(column_peak_end_date);
            int peak_count_nuField = atctable.FieldNumber("peak_count_nu");
            DataColumn column_peak_count_nu = new DataColumn("StrFlow Count");
            pointCoords.DataTable.Columns.Add(column_peak_count_nu);
            int qw_begin_dateField = atctable.FieldNumber("qw_begin_date");
            DataColumn column_qw_begin_date = new DataColumn("WQ Begin");
            pointCoords.DataTable.Columns.Add(column_qw_begin_date);
            int qw_end_dateField = atctable.FieldNumber("qw_end_date");
            DataColumn column_qw_end_date = new DataColumn("WQ End");
            pointCoords.DataTable.Columns.Add(column_qw_end_date);
            int qw_count_nuField = atctable.FieldNumber("qw_count_nu");
            DataColumn column_qw_count_nu = new DataColumn("WQ Count");
            pointCoords.DataTable.Columns.Add(column_qw_count_nu);
            int gw_begin_dateField = atctable.FieldNumber("gw_begin_date");
            DataColumn column_gw_begin_date = new DataColumn("FWLM Begin");
            pointCoords.DataTable.Columns.Add(column_gw_begin_date);
            int gw_end_dateField = atctable.FieldNumber("gw_end_date");
            DataColumn column_gw_end_date = new DataColumn("FWLM End");
            pointCoords.DataTable.Columns.Add(column_gw_end_date);
            int gw_count_nuField = atctable.FieldNumber("gw_count_nu");
            DataColumn column_gw_count_nu = new DataColumn("FWLM Count");
            pointCoords.DataTable.Columns.Add(column_gw_count_nu);
            int sv_begin_dateField = atctable.FieldNumber("sv_begin_date");
            DataColumn column_sv_begin_date = new DataColumn("SV Begin");
            pointCoords.DataTable.Columns.Add(column_sv_begin_date);
            int sv_end_dateField = atctable.FieldNumber("sv_end_date");
            DataColumn column_sv_end_date = new DataColumn("SV End");
            pointCoords.DataTable.Columns.Add(column_sv_end_date);
            int sv_count_nuField = atctable.FieldNumber("sv_count_nu");
            DataColumn column_sv_count_nu = new DataColumn("SV Count");
            pointCoords.DataTable.Columns.Add(column_sv_count_nu);
            /*

#  sv_count_nu     -- Site-visit data count
             * */
            atctable.MoveFirst();

            for (int i = 0; i < numrecords; i++)
            {
                string stationID = atctable.get_Value(fieldnumber);
                string agency = atctable.get_Value(agencyField);
                string stationname = atctable.get_Value(stationnameField);
                string sitetype = atctable.get_Value(sitetypeField);
                string coord_acy_cd = atctable.get_Value(coord_acy_cdField);
                string dec_coord_datum_cd = atctable.get_Value(dec_coord_datum_cdField);
                string district_cd = atctable.get_Value(district_cdField);
                string state_cd = atctable.get_Value(state_cdField);
                string county_cd = atctable.get_Value(county_cdField);
                string country_cd = atctable.get_Value(country_cdField);
                string land_net_ds = atctable.get_Value(land_net_dsField);
                string map_nm = atctable.get_Value(map_nmField);
                string map_scale_fc = atctable.get_Value(map_scale_fcField);
                string alt_va = atctable.get_Value(alt_vaField);
                string alt_meth_cd = atctable.get_Value(alt_meth_cdField);
                string alt_acy_va = atctable.get_Value(alt_acy_vaField);
                string alt_datum_cd = atctable.get_Value(alt_datum_cdField);
                string huc_cd = atctable.get_Value(huc_cdField);
                string basin_cd = atctable.get_Value(basin_cdField);
                string topo_cd = atctable.get_Value(topo_cdField);
                string data_types_cd = atctable.get_Value(data_types_cdField);
                string instruments_cd = atctable.get_Value(instruments_cdField);
                string construction_dt = atctable.get_Value(construction_dtField);
                string inventory_dt = atctable.get_Value(inventory_dtField);
                string drain_area_va = atctable.get_Value(drain_area_vaField);
                string contrib_drain_area_va = atctable.get_Value(contrib_drain_area_vaField);
                string tz_cd = atctable.get_Value(tz_cdField);
                string local_time_fg = atctable.get_Value(local_time_fgField);
                string reliability_cd = atctable.get_Value(reliability_cdField);
                string gw_file_cd = atctable.get_Value(gw_file_cdField);
                string nat_aqfr_cd = atctable.get_Value(nat_aqfr_cdField);
                string aqfr_cd = atctable.get_Value(aqfr_cdField);
                string aqfr_type_cd = atctable.get_Value(aqfr_type_cdField);
                string well_depth_va = atctable.get_Value(well_depth_vaField);
                string hole_depth_va = atctable.get_Value(hole_depth_vaField);
                string depth_src_cd = atctable.get_Value(depth_src_cdField);
                string project_no = atctable.get_Value(project_noField);
                string rt_bol = atctable.get_Value(rt_bolField);
                string peak_begin_date = atctable.get_Value(peak_begin_dateField);
                string peak_end_date = atctable.get_Value(peak_end_dateField);
                string peak_count_nu = atctable.get_Value(peak_count_nuField);
                string qw_begin_date = atctable.get_Value(qw_begin_dateField);
                string qw_end_date = atctable.get_Value(qw_end_dateField);
                string qw_count_nu = atctable.get_Value(qw_count_nuField);
                string gw_begin_date = atctable.get_Value(gw_begin_dateField);
                string gw_end_date = atctable.get_Value(gw_end_dateField);
                string gw_count_nu = atctable.get_Value(gw_count_nuField);
                string sv_begin_date = atctable.get_Value(sv_begin_dateField);
                string sv_end_date = atctable.get_Value(sv_end_dateField);
                string sv_count_nu = atctable.get_Value(sv_count_nuField);

                atctable.MoveNext();
                aStationIDs.Add(stationID);
                string lat = atctable.get_Value(latitudeField);
                string lng = atctable.get_Value(longitudeField);

                if ((lat.Length != 0) && (lng.Length != 0))
                {
                    double latitude = Convert.ToDouble(lat);
                    double longitude = Convert.ToDouble(lng);
                    DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                    DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                    IFeature currentFeature = pointCoords.AddFeature(point);
                    currentFeature.DataRow["StationID"] = stationID;
                    currentFeature.DataRow["Agency"] = agency;
                    currentFeature.DataRow["StationName"] = stationname;
                    currentFeature.DataRow["Site Type"] = sitetype;
                    currentFeature.DataRow["LatLong Acc"] = coord_acy_cd;
                    currentFeature.DataRow["Decim datum"] = dec_coord_datum_cd;
                    currentFeature.DataRow["Dist code"] = district_cd;
                    currentFeature.DataRow["State code"] = state_cd;
                    currentFeature.DataRow["County code"] = county_cd;
                    currentFeature.DataRow["CountryCode"] = country_cd;
                    currentFeature.DataRow["Land net"] = land_net_ds;
                    currentFeature.DataRow["Map name"] = map_nm;
                    currentFeature.DataRow["Map scale"] = map_scale_fc;
                    currentFeature.DataRow["Altitude"] = map_scale_fc;
                    currentFeature.DataRow["Alt Method"] = alt_meth_cd;
                    currentFeature.DataRow["Alt Accuracy"] = alt_acy_va;
                    currentFeature.DataRow["Alt Datum"] = alt_datum_cd;
                    currentFeature.DataRow["HUC-8"] = huc_cd;
                    currentFeature.DataRow["Basin code"] = basin_cd;
                    currentFeature.DataRow["Topogr code"] = topo_cd;
                    currentFeature.DataRow["Data Types"] = data_types_cd;
                    currentFeature.DataRow["Instruments"] = instruments_cd;
                    currentFeature.DataRow["Constr Date"] = construction_dt;
                    currentFeature.DataRow["Invent Date"] = inventory_dt;
                    currentFeature.DataRow["Drainage Area"] = drain_area_va;
                    currentFeature.DataRow["Contr Dr Area"] = contrib_drain_area_va;
                    currentFeature.DataRow["Time Offset"] = tz_cd;
                    currentFeature.DataRow["LocTimeFlag"] = local_time_fg;
                    currentFeature.DataRow["Reliability"] = reliability_cd;
                    currentFeature.DataRow["GW Files"] = gw_file_cd;
                    currentFeature.DataRow["Nat Aquifer"] = nat_aqfr_cd;
                    currentFeature.DataRow["Loc Aquifer"] = aqfr_cd;
                    currentFeature.DataRow["AquiferType"] = aqfr_type_cd;
                    currentFeature.DataRow["Well Depth"] = well_depth_va;
                    currentFeature.DataRow["Hole Depth"] = hole_depth_va;
                    currentFeature.DataRow["Depth Data"] = depth_src_cd;
                    currentFeature.DataRow["Project Number"] = project_no;
                    currentFeature.DataRow["Real-time"] = rt_bol;
                    currentFeature.DataRow["StrFlow Begin"] = peak_begin_date;
                    currentFeature.DataRow["StrFlow End"] = peak_end_date;
                    currentFeature.DataRow["StrFlow Count"] = peak_count_nu;
                    currentFeature.DataRow["WQ Begin"] = qw_begin_date;
                    currentFeature.DataRow["WQ End"] = qw_end_date;
                    currentFeature.DataRow["WQ Count"] = qw_count_nu;
                    currentFeature.DataRow["FWLM Begin"] = gw_begin_date;
                    currentFeature.DataRow["FWLM End"] = gw_end_date;
                    currentFeature.DataRow["FWLM Count"] = gw_count_nu;
                    currentFeature.DataRow["SV Begin"] = sv_begin_date;
                    currentFeature.DataRow["SV End"] = sv_begin_date;
                    currentFeature.DataRow["SV Count"] = sv_count_nu;
                }
            }
            string shapefile = "";
            switch (datatype)
            {
                case "Daily Discharge":
                    shapefile = System.IO.Path.Combine(folder, "NWIS_discharge.shp");
                    break;
                case "IDA Discharge":
                    shapefile = System.IO.Path.Combine(folder, "NWIS_IDA.shp");
                    break;
                case "Measurement":
                    shapefile = System.IO.Path.Combine(folder, "NWIS_measurements.shp");
                    break;
                case "Water Quality":
                    shapefile = System.IO.Path.Combine(folder, "NWIS_wq.shp");
                    break;
            }      
            pointCoords.SaveAs(shapefile, true);
        }

        public void writeNWISfilesWithSpecificDataParameters(string folder, string parameter)
        {
            string parametercode = parameter.Substring(0, 5);
            DataTable dt = makeDataTableWithStationIDandName(folder, parametercode);
            string subfolder = System.IO.Path.Combine(folder, parameter);
            Directory.CreateDirectory(subfolder);
            /*
            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                foreach (DataRow dr in dt.Rows)
                {
                    string stationId = dr[0].ToString();
                    if (filename.Contains(stationId) && filename.Contains(".rdb"))
                    {
                        string newfile = System.IO.Path.Combine(subfolder, filename);
                        if (File.Exists(newfile))
                        {
                            File.Delete(newfile);
                        }
                        File.Copy(file, newfile);
                    }
                }
            }
             * */
            writeSpecificCSVFiles(folder, subfolder, parameter);

        }

        private void writeSpecificCSVFiles(string folder, string subfolder, string parameter)
        {
            string parametercode = parameter.Substring(0, 5);
            List<string> stationIDs = makeStationListWithSelectedParameters(folder, parametercode);
            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                foreach (string stationId in stationIDs)
                {
                    if (filename.Contains(stationId) && filename.Contains(".rdb"))
                    {
                        DataTable dt = convertRDBtoDataTable(file);

                        TextWriter tw = new StreamWriter(System.IO.Path.Combine(subfolder, stationId + ".csv"));
                        foreach (DataColumn dc in dt.Columns)
                        {
                            tw.Write(dc.ColumnName);
                            tw.Write(",");
                        }
                        tw.WriteLine();

                        DataRow[] selectRows = dt.Select("parm_cd = '" + parametercode +"'");

                        foreach (DataRow dr in selectRows)
                        {
                            int i = 0;
                            foreach (string value in dr.ItemArray)
                            {
                                string item = value;
                                if (i == 12 || i == 1)
                                {
                                    item = "'" + item;
                                }
                                tw.Write(item);
                                tw.Write(",");
                                i++;
                            }
                            tw.WriteLine();
                        }
                        tw.Close();

                    }
                }
            }
        }


        private List<string> makeStationListWithSelectedParameters(string folder, string parameter)
        {
            List<string> stations = new List<string>();

            string[] rdbfiles = Directory.GetFiles(folder, "*.rdb", SearchOption.AllDirectories);
            string line;
            foreach (string file in rdbfiles)
            {
                TextReader tr = new StreamReader(file);
                while ((line = tr.ReadLine()) != null)
                {
                    if (line.Contains(parameter))
                    {
                        string station = Path.GetFileNameWithoutExtension(file);
                        station = station.Replace(".rdb", "");
                        station = station.Replace("NWIS_wq_", "");
                        if (!station.Contains("Stations"))
                        {
                            stations.Add(station);
                        }
                        break;
                    }
                }
                tr.Close();
            }
            return stations;
        }

        private DataTable makeDataTableWithStationIDandName(string folder, string parametercode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StationID");
            dt.Columns.Add("StationName");
            List<string> stationIDs = makeStationListWithSelectedParameters(folder, parametercode);
            foreach (string stationID in stationIDs)
            {
                TextReader tr = new StreamReader(System.IO.Path.Combine(folder, "NWIS_Stations_qw.rdb"));
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line.Contains(stationID))
                    {
                        string[] values = line.Split('\t');
                        dt.Rows.Add(values[1], values[2]);
                    }
                }
                tr.Close();
            }
            return dt;
        }
        
        public void convertRDBtoCSV(string folder)
        {           
            string[] rdbfiles = Directory.GetFiles(folder, "*.rdb", SearchOption.AllDirectories);
            foreach (string rdbfile in rdbfiles)
            {
                DataTable dt = convertRDBtoDataTable(rdbfile);

                string csvfile = System.IO.Path.Combine(folder, Path.GetFileNameWithoutExtension(rdbfile) + ".csv");
                TextWriter tw = new StreamWriter(csvfile);
                foreach (DataColumn dc in dt.Columns)
                {
                    tw.Write(dc.ColumnName);
                    tw.Write(",");
                }
                tw.WriteLine();

                foreach (DataRow dr in dt.Rows)
                {
                    int i = 0;
                    foreach (string value in dr.ItemArray)
                    {
                        string item = value;
                        if (i == 12 || i == 1)
                        {
                            item = "'" + item;
                        }
                        tw.Write(item);
                        tw.Write(",");

                        i++;
                    }

                    tw.WriteLine();
                }
                tw.Close();
            }
        }

        private DataTable convertRDBtoDataTable(string rdbfile)
        {
            DataTable dt = new DataTable();
            TextReader tr = new StreamReader(rdbfile);
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                if (line.Substring(0, 1).Contains("#") == false)
                {
                    string[] columnvalues = line.Split('\t');
                    foreach (string column in columnvalues)
                    {
                        dt.Columns.Add(column);
                    }
                    while ((line = tr.ReadLine()) != null)
                    {
                        string[] values = line.Split('\t');
                        dt.Rows.Add(values);
                    }
                }
            }

            tr.Close();
            return dt;
        }

        public string getStationName(string projectFolder, string stationID)
        {
            string name = "";
            string stationRDBFile = "";
            string[] directories = Directory.GetDirectories(projectFolder);
            foreach (string directory in directories)
            {
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    string[] files = Directory.GetFiles(subdirectory);
                    foreach (string file in files)
                    {
                        if (file.Contains(stationID) && file.Contains("NWIS_discharge"))
                        {
                            stationRDBFile = file;
                            break;
                        }
                    }
                }
            }
            TextReader tr = new StreamReader(stationRDBFile);
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                if (line.Contains("station_nm"))
                {
                    name = line.Replace("# station_nm", "");
                    name = name.Trim();
                    break;
                }
            }
            tr.Close();
            return name;
        }

        public DataTable getDischargeData(string projectFolder, string stationID)
        {
            string rdbFolder = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Discharge");

            string stationRDBFile = "";

            string[] directories = Directory.GetDirectories(projectFolder);
            foreach (string directory in directories)
            {
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    string[] files = Directory.GetFiles(subdirectory);
                    foreach (string file in files)
                    {
                        if (file.Contains(stationID) && file.Contains("NWIS_discharge"))
                        {
                            stationRDBFile = file;
                            rdbFolder = Path.GetDirectoryName(file);
                            break;
                        }
                    }
                }
            }          

            string outputFile = Path.Combine(rdbFolder, stationID + "_DailyDischarge.csv");
            TextReader tr = new StreamReader(stationRDBFile);
            TextWriter twr = new StreamWriter(outputFile);
            string line;

            string[] items = new string[2];
            int j = 0;
            while ((line = tr.ReadLine()) != null)
            {
                if(!line.Contains('#'))
                {
                    string[] array = line.Split('\t');
                    if (j > 1)
                    {                        
                        string date = array[2];
                        items[0] = date;
                        string value = array[3];
                        items[1] = value;
                        dt.Rows.Add(items);
                        twr.WriteLine(date + "," + value);
                    }
                    j++;
                }
            }

            tr.Close();
            twr.Close();
            return dt;

        }
    }
}
