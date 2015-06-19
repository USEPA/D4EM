using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4EM.Data;
using System.IO;
using System.Net;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Analysis;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Drawing;

namespace D4EM.Data.Source
{
    public class WDNR
    {
        /*
         * geoCodedAddressTable has a hard-coded path.  This csv file has the lat/longs for each station
         * */
        public static string codedAddressesFile = Path.GetFullPath(@"..\Bin\WDNRGeoCodedAddresses.csv");
        
        public List<string> FileNames = new List<string>();
        public static List<string> permitNumbers = new List<string>();
        public static string HUCshapefilename = @"..\Bin\huc250d3.shp";
        double _north;
        double _south;
        double _east;
        double _west;
        string _Huc8;
        string _Huc12;

        public WDNR(string geocodedAddressFile, string huc8shapefile)
        {
            codedAddressesFile = geocodedAddressFile;
            HUCshapefilename = huc8shapefile;
        }

        public WDNR(string geocodedAddressFile)
        {
            codedAddressesFile = geocodedAddressFile;
        }
        public WDNR()
        {
        }

        public class LayerSpecifications
        {
            public static LayerSpecification Beef = new LayerSpecification(
                Tag: "Beef", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(WDNR));
            public static LayerSpecification Chickens = new LayerSpecification(
                Tag: "Chickens", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(WDNR));
            public static LayerSpecification Dairy = new LayerSpecification(
                Tag: "Dairy", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(WDNR));
            public static LayerSpecification Swine = new LayerSpecification(
                Tag: "Swine", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(WDNR));
            public static LayerSpecification Turkeys = new LayerSpecification(
                Tag: "Turkeys", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(WDNR));
        }

        public void getData(D4EM.Data.LayerSpecification animalLayer, string aProjectFolder, string aCacheFolder)
        {
            FileNames.Clear();
            FileNames = new List<string>();            
            string ext = "(Statewide)";
            permitNumbers.Clear();
            permitNumbers = new List<string>();
            string _animal = animalLayer.Tag;
            checkCacheAndWriteFiles(aProjectFolder, aCacheFolder, _animal, ext, 1);
        }


        public void getDataWithinBoundingBox(D4EM.Data.LayerSpecification animalLayer, string aProjectFolder, string aCacheFolder, double aNorth, double aSouth, double aEast, double aWest)
        {
            _north = aNorth;
            _south = aSouth;
            _east = aEast;
            _west = aWest;
            FileNames.Clear();
            FileNames = new List<string>();
            string ext = "(BoundingBox~N" + String.Format("{0:0.00}", aNorth) + ";S" + String.Format("{0:0.00}", aSouth) + ";E" + String.Format("{0:0.00}", aEast) + ";W" + String.Format("{0:0.00}", aWest) + ")";
            permitNumbers.Clear();
            permitNumbers = new List<string>();
            string _animal = animalLayer.Tag;
            checkCacheAndWriteFiles(aProjectFolder, aCacheFolder, _animal, ext, 2);
        }

        public void getDataWithinHuc8(D4EM.Data.LayerSpecification animalLayer, string aProjectFolder, string aCacheFolder, string aHuc)
        {
            _Huc8 = aHuc;
            FileNames.Clear();
            FileNames = new List<string>();
            string ext = "(HUC8~" + aHuc + ")";            
            permitNumbers.Clear();
            permitNumbers = new List<string>();
            string _animal = animalLayer.Tag;
            checkCacheAndWriteFiles(aProjectFolder, aCacheFolder, _animal, ext, 3);
        }

        public void getDataWithinHuc12(D4EM.Data.LayerSpecification animalLayer, string aProjectFolder, string aCacheFolder, string aHuc12)
        {
            _Huc12 = aHuc12;
            FileNames.Clear();
            FileNames = new List<string>(); 
            string aHuc8 = aHuc12.Substring(0, 8);
            string ext = "(HUC12~" + aHuc12 + ")";
            string aTempFolder = @"C:\Temp\TempHuc12";
            string shapefilename = System.IO.Path.Combine(aTempFolder, aHuc8, "huc12.shp");
            permitNumbers.Clear();
            permitNumbers = new List<string>();
            string _animal = animalLayer.Tag;
            checkCacheAndWriteFiles(aProjectFolder, aCacheFolder, _animal, ext, 4);
          
        }

        private static string writeOverallShapeFile(List<D4EM.Data.LayerSpecification> animalLayers, string aProjectFolder, string aSubFolderAll, string folderExt)
        {
            
            string shapefilename = System.IO.Path.Combine(aSubFolderAll, "AllAnimalLocations.shp");

            FeatureSet pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            DataColumn column = new DataColumn("ID");
            DataColumn column2 = new DataColumn("PermitNumber");
            DataColumn column3 = new DataColumn("Animal");
            DataColumn column4 = new DataColumn("CurrentUnit");
            DataColumn column5 = new DataColumn("ProjectedUnit");
            pointCoords.DataTable.Columns.Add(column);
            pointCoords.DataTable.Columns.Add(column2);
            pointCoords.DataTable.Columns.Add(column3);
            pointCoords.DataTable.Columns.Add(column4);
            pointCoords.DataTable.Columns.Add(column5);

            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;

            int pointID = 0;
            string _line;

            DataTable dt = new DataTable();
            dt.Columns.Add("PermitNumber");
            dt.Columns.Add("Animal");
            dt.Columns.Add("CurrentUnit");
            dt.Columns.Add("ProjectedUnit");

            foreach (D4EM.Data.LayerSpecification animalLayer in animalLayers)
            {
                string _animal = animalLayer.Tag;

                string aSubFolder = System.IO.Path.Combine(aProjectFolder, _animal + folderExt);
                string tableFile = System.IO.Path.Combine(aSubFolder, "Addresses.csv");
                TextReader read = new StreamReader(tableFile);
                _line = read.ReadLine();
                while ((_line = read.ReadLine()) != null)
                {
                    string[] values = _line.Split(',');
                    string permitNum = values[0];
                    string current = values[3];
                    string projected = values[4];
                    dt.Rows.Add(permitNum, _animal, current, projected);
                }
                read.Close();
            }

            TextReader tr = new StreamReader(codedAddressesFile);

            while ((_line = tr.ReadLine()) != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string num = dr[0].ToString();
                    if (_line.Contains(num))
                    {
                        string[] sites = _line.Split(',');
                        double latitude = Convert.ToDouble(sites[1]);
                        double longitude = Convert.ToDouble(sites[2]);
                        string permitnumber = sites[0];
                        DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                        DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                        IFeature currentFeature = pointCoords.AddFeature(point);
                        pointID = pointID + 1;
                        string animal = dr[1].ToString();
                        string current = dr[2].ToString();
                        string projected = dr[3].ToString();
                        currentFeature.DataRow["ID"] = pointID;
                        currentFeature.DataRow["PermitNumber"] = num;
                        currentFeature.DataRow["Animal"] = animal;
                        currentFeature.DataRow["CurrentUnit"] = current;
                        currentFeature.DataRow["ProjectedUnit"] = projected;   
                       
                    }
                }
            }
            pointCoords.SaveAs(shapefilename, true);
            tr.Close();
            return shapefilename;
        }


        private static string writeShapeFiles(List<string> permitNumbers, string _animal, string shapeFileName, string tableFile)
        {
            
            string shapefilename = shapeFileName;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("PermitNumber");
            dt.Columns.Add("Animal");
            dt.Columns.Add("CurrentUnit");
            dt.Columns.Add("ProjectedUnit");
            
            TextReader read = new StreamReader(tableFile);
            string line;
            line = read.ReadLine();
            while ((line = read.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                string permitNum = values[0];
                string current = values[3];
                string projected = values[4];
                dt.Rows.Add(permitNum, _animal, current, projected);
            }
            read.Close();

            FeatureSet pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            DataColumn column = new DataColumn("ID");
            DataColumn column2 = new DataColumn("PermitNumber");
            DataColumn column3 = new DataColumn("Animal");
            DataColumn column4 = new DataColumn("CurrentUnit");
            DataColumn column5 = new DataColumn("ProjectedUnit");
            pointCoords.DataTable.Columns.Add(column);
            pointCoords.DataTable.Columns.Add(column2);
            pointCoords.DataTable.Columns.Add(column3);
            pointCoords.DataTable.Columns.Add(column4);
            pointCoords.DataTable.Columns.Add(column5);

            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;



            int pointID = 0;          
            string _line;
            TextReader tr = new StreamReader(codedAddressesFile);

            while ((_line = tr.ReadLine()) != null)
            {
                foreach (string num in permitNumbers)
                {
                    if (_line.Contains(num))
                    {
                        DataRow[] dr = dt.Select("PermitNumber =" + num);
                        string animal = dr[0][1].ToString();
                        string current = dr[0][2].ToString();
                        string projected = dr[0][3].ToString();
                        
                        string[] sites = _line.Split(',');
                        double latitude = Convert.ToDouble(sites[1]);
                        double longitude = Convert.ToDouble(sites[2]);
                        string permitnumber = sites[0];
                        DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                        DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                        IFeature currentFeature = pointCoords.AddFeature(point);
                        pointID = pointID + 1;
                        currentFeature.DataRow["ID"] = pointID;
                        currentFeature.DataRow["PermitNumber"] = num;
                        currentFeature.DataRow["Animal"] = animal;
                        currentFeature.DataRow["CurrentUnit"] = current;
                        currentFeature.DataRow["ProjectedUnit"] = projected; 
                    }
                }
            }
            pointCoords.SaveAs(shapefilename, true);
            tr.Close();
            return shapefilename;
        }

        private static string writeAddressTable(List<string> permitNumbers, string _animal, string aFileName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PermitNumber");
            dt.Columns.Add("Animal");
            dt.Columns.Add("CurrentUnit");
            dt.Columns.Add("ProjectedUnit");

            foreach (string permitnum in permitNumbers)
            {
                string tempPermitFile = @"C:\Temp\PermitFile" + permitnum;
                TextWriter _tw = new StreamWriter(tempPermitFile);
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create("http://dnr.wi.gov/runoff/agriculture/cafo/permits/cafo_det.asp?AnimalChoice=" + _animal + "&Submit=Submit&PERMIT_NO=" + permitnum);
                StringBuilder _sb = new StringBuilder();
                byte[] _buf = new byte[8192];
                HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                Stream _resStream = _response.GetResponseStream();
                string _tempString = null;
                int _count = 0;
                do
                {
                    _count = _resStream.Read(_buf, 0, _buf.Length);
                    if (_count != 0)
                    {
                        _tempString = Encoding.ASCII.GetString(_buf, 0, _count);
                        _sb.Append(_tempString);
                    }
                }
                while (_count > 0);

                _tw.WriteLine(_sb);
                _tw.Close();
            }

            string newtableFile = aFileName;
            TextWriter _write = new StreamWriter(newtableFile);
            _write.Write("PermitNumber,Permittee,Address,CurrentUnit, ProjectedUnit");
            foreach (string num in permitNumbers)
            {
                _write.WriteLine();
                _write.Write(num + ",");

                TextReader _read = new StreamReader(@"C:\Temp\PermitFile" + num);

                string _line;

                char[] _sep = new char[2];
                _sep[0] = '>';
                _sep[1] = '<';

                while ((_line = _read.ReadLine()) != null)
                {
                    if (_line.Contains("Permittee") && _line.Contains("<tr>"))
                    {
                        string[] sites = _line.Split(_sep, 15);
                        string permittee = sites[12].Trim();
                        permittee = permittee.Replace("&nbsp;", "");
                        permittee = permittee.Replace(",", "");
                        _write.Write(permittee + ",");
                    }
                    else if (_line.Contains("Address") && _line.Contains("<tr>"))
                    {
                        string[] sites = _line.Split(_sep, 15);
                        string address = sites[12].Trim();
                        address = address.Replace("&nbsp;", "");
                        address = address.Replace(",", "");
                        _write.Write(address + ",");
                    }
                    else if (_line.Contains("<td><b>&nbsp;Animal Unit *&nbsp;</b></td>"))
                    {
                        string nextline = _read.ReadLine();
                        string current = nextline.Replace("&nbsp;", "");
                        current = current.Replace("<td>", "");
                        current = current.Replace("</td>", "");
                        _write.Write(current + ",");
                        nextline = _read.ReadLine();
                        string projected = nextline.Replace("&nbsp;", "");
                        projected = projected.Replace("<td>", "");
                        projected = projected.Replace("</td>", "");
                        _write.Write(projected + ",");

                    }
                }
                _read.Close();
                File.Delete(@"C:\Temp\PermitFile" + num);
            }
            _write.Close();
            return newtableFile;
        }

        private static string writePermitNumberTable(string _animal, string aFileName)
        {
            string tempFile = @"C:\Temp\WIpathogenTemp";            
            

            string tableFile = aFileName;

            TextReader read = new StreamReader(tempFile);
            TextWriter write = new StreamWriter(tableFile);

            int counter = 0;
            string line;

            char[] sep = new char[2];
            sep[0] = '>';
            sep[1] = '<';

            write.Write("Permit Number,Permittee Name,Animal,County,Region");

            while ((line = read.ReadLine()) != null)
            {
                if (line.Contains("<tr>"))
                {
                    while ((line = read.ReadLine()) != null)
                    {
                        if (line.Contains("</a>") && line.Contains("nbsp"))
                        {
                            string[] sites = line.Split(sep, 15);
                            if (sites.Length == 5)
                            {
                                write.WriteLine();
                                string permitnumber = sites[0].Trim();
                                permitNumbers.Add(permitnumber);
                                write.Write(permitnumber + ",");
                                line = read.ReadLine();
                                string permittee = line.Replace("&nbsp;", "");
                                permittee = permittee.Replace("<td>", "");
                                permittee = permittee.Replace("</td>", "");
                                permittee = permittee.Replace(",", "");
                                write.Write(permittee + ",");
                                line = read.ReadLine();
                                string animal = line.Replace("&nbsp;", "");
                                animal = animal.Replace("<td>", "");
                                animal = animal.Replace("</td>", "");
                                write.Write(animal + ",");
                                line = read.ReadLine();
                                string county = line.Replace("&nbsp;", "");
                                county = county.Replace("<td>", "");
                                county = county.Replace("</td>", "");
                                write.Write(county + ",");
                                line = read.ReadLine();
                                line = read.ReadLine();
                                string region = line.Replace("&nbsp;", "");
                                region = region.Replace("</td>", "");
                                write.Write(region + ",");
                            }

                        }
                    }
                }
                counter++;
            }
            write.Close();
            read.Close();
            File.Delete(tempFile);
            return tableFile;
        }

        private static string writePermitNumberTableWithinBoundingBox(string _animal, string aFileName, double aNorth, double aSouth, double aEast, double aWest)
        {

            TextReader tr = new StreamReader(codedAddressesFile);
            DataTable dt = new DataTable();
            dt.Columns.Add("PermitNumber");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");
            
            string _line;
            tr.ReadLine();
            while ((_line = tr.ReadLine()) != null)
            {     
                string[] sites = _line.Split(',');
                double latitude = Convert.ToDouble(sites[1]);
                double longitude = Convert.ToDouble(sites[2]);
                string permitnumber = sites[0];
                dt.Rows.Add(permitnumber, latitude, longitude);
            }
            tr.Close();

            string tempFile = @"C:\Temp\WIpathogenTemp";
            
            string tableFile = aFileName;

            TextReader read = new StreamReader(tempFile);
            TextWriter write = new StreamWriter(tableFile);

            int counter = 0;
            string line;

            char[] sep = new char[2];
            sep[0] = '>';
            sep[1] = '<';

            write.Write("Permit Number,Permittee Name,Animal,County,Region");

            while ((line = read.ReadLine()) != null)
            {
                if (line.Contains("<tr>"))
                {
                    while ((line = read.ReadLine()) != null)
                    {
                        if (line.Contains("</a>") && line.Contains("nbsp"))
                        {
                            string[] sites = line.Split(sep, 15);
                            if (sites.Length == 5)
                            { 
                                string permitnumber = sites[0].Trim();
                                DataRow[] result = dt.Select("PermitNumber = " + permitnumber);
                                if (result.Length >= 3)
                                {
                                    double latitude = Convert.ToDouble(result[0][1]);
                                    double longitude = Convert.ToDouble(result[0][2]);
                                    if ((latitude <= aNorth) && (latitude >= aSouth) && (longitude <= aEast) && (longitude >= aWest))
                                    {
                                        write.WriteLine();
                                        permitNumbers.Add(permitnumber);
                                        write.Write(permitnumber + ",");
                                        line = read.ReadLine();
                                        string permittee = line.Replace("&nbsp;", "");
                                        permittee = permittee.Replace("<td>", "");
                                        permittee = permittee.Replace("</td>", "");
                                        permittee = permittee.Replace(",", "");
                                        write.Write(permittee + ",");
                                        line = read.ReadLine();
                                        string animal = line.Replace("&nbsp;", "");
                                        animal = animal.Replace("<td>", "");
                                        animal = animal.Replace("</td>", "");
                                        write.Write(animal + ",");
                                        line = read.ReadLine();
                                        string county = line.Replace("&nbsp;", "");
                                        county = county.Replace("<td>", "");
                                        county = county.Replace("</td>", "");
                                        write.Write(county + ",");
                                        line = read.ReadLine();
                                        line = read.ReadLine();
                                        string region = line.Replace("&nbsp;", "");
                                        region = region.Replace("</td>", "");
                                        write.Write(region + ",");
                                    }
                                }
                            }
                        }
                    }
                }
                counter++;
            }
            write.Close();
            read.Close();
            File.Delete(tempFile);

            return tableFile;
        }
        
        private static string writePermitNumberTableWithinHuc(string _animal, string aFileName, string aHuc)
        {
            
            IFeature _aHucPolygon = null;
            IFeatureSet fs = FeatureSet.OpenFile(HUCshapefilename);
            int count = fs.Features.Count;
            for (int i = 0; i < count; i++)
            {
                IFeature hucFeature = fs.GetFeature(i);
                string huc = hucFeature.DataRow[2].ToString();
                if (huc == aHuc)
                {
                    _aHucPolygon = hucFeature;
                    break;
                }
            }
            ProjectionInfo proj = new ProjectionInfo();
            proj = fs.Projection;

            IFeatureSet poly = null;
            poly = new FeatureSet();
            poly.AddFeature(_aHucPolygon);
            poly.Projection = proj;
            poly.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);

            TextReader tr = new StreamReader(codedAddressesFile);
            DataTable dt = new DataTable();
            dt.Columns.Add("PermitNumber");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            string _line;
            tr.ReadLine();
            while ((_line = tr.ReadLine()) != null)
            {
                string[] sites = _line.Split(',');
                double latitude = Convert.ToDouble(sites[1]);
                double longitude = Convert.ToDouble(sites[2]);
                string permitnumber = sites[0];
                dt.Rows.Add(permitnumber, latitude, longitude);
            }
            tr.Close();

            string tempFile = @"C:\Temp\WIpathogenTemp";

            string tableFile = aFileName;

            TextReader read = new StreamReader(tempFile);
            TextWriter write = new StreamWriter(tableFile);

            int counter = 0;
            string line;

            char[] sep = new char[2];
            sep[0] = '>';
            sep[1] = '<';

            write.Write("Permit Number,Permittee Name,Animal,County,Region");

            while ((line = read.ReadLine()) != null)
            {
                if (line.Contains("<tr>"))
                {
                    while ((line = read.ReadLine()) != null)
                    {
                        if (line.Contains("</a>") && line.Contains("nbsp"))
                        {
                            string[] sites = line.Split(sep, 15);
                            if (sites.Length == 5)
                            {
                                string permitnumber = sites[0].Trim();
                                DataRow[] result = dt.Select("PermitNumber = " + permitnumber);
                                if (result.Length >= 3)
                                {
                                    double latitude = Convert.ToDouble(result[0][1]);
                                    double longitude = Convert.ToDouble(result[0][2]);
                                    DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                                    DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);

                                    if (poly.GetFeature(0).Intersects(coords) == true)
                                    {
                                        write.WriteLine();
                                        permitNumbers.Add(permitnumber);
                                        write.Write(permitnumber + ",");
                                        line = read.ReadLine();
                                        string permittee = line.Replace("&nbsp;", "");
                                        permittee = permittee.Replace("<td>", "");
                                        permittee = permittee.Replace("</td>", "");
                                        permittee = permittee.Replace(",", "");
                                        write.Write(permittee + ",");
                                        line = read.ReadLine();
                                        string animal = line.Replace("&nbsp;", "");
                                        animal = animal.Replace("<td>", "");
                                        animal = animal.Replace("</td>", "");
                                        write.Write(animal + ",");
                                        line = read.ReadLine();
                                        string county = line.Replace("&nbsp;", "");
                                        county = county.Replace("<td>", "");
                                        county = county.Replace("</td>", "");
                                        write.Write(county + ",");
                                        line = read.ReadLine();
                                        line = read.ReadLine();
                                        string region = line.Replace("&nbsp;", "");
                                        region = region.Replace("</td>", "");
                                        write.Write(region + ",");
                                    }
                                }
                            }
                        }
                    }
                }
                counter++;
            }
            write.Close();
            read.Close();
            File.Delete(tempFile);

            return tableFile;
        }

        private static void writeTempFile(string _animal)
        {
            string tempFile = @"C:\Temp\WIpathogenTemp";

            TextWriter tw = new StreamWriter(tempFile);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnr.wi.gov/runoff/agriculture/cafo/permits/cafo_ani.asp?AnimalChoice=" + _animal + "&Submit=Submit");
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);

            tw.WriteLine(sb);
            tw.Close();
        }

        private static bool checkCache(string aSaveAsCache)
        {
            bool fileExists = false;

            if (File.Exists(aSaveAsCache))
            {
                fileExists = true;
            }

            return fileExists;
        }

        private void checkCacheAndWriteFiles(string aProjectFolder, string aCacheFolder, string _animal, string ext, int type)
        {
            string aSubFolder = System.IO.Path.Combine(aProjectFolder, _animal + ext);
            Directory.CreateDirectory(aSubFolder);
            Directory.CreateDirectory(aCacheFolder);
            writeTempFile(_animal);
            string permitNumberFileCache = System.IO.Path.Combine(aCacheFolder, "PermitNumbers" + "(" + _animal + ext + ").csv");
            string permitNumberFile = System.IO.Path.Combine(aSubFolder, "PermitNumbers" + "(" + _animal + ext + ").csv");
            bool permitNumberCachefileExists = checkCache(permitNumberFileCache);
            if (type == 1)
            {
                if (permitNumberCachefileExists == false)
                {
                    writePermitNumberTable(_animal, permitNumberFileCache);

                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
                else
                {
                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
            }
            if (type == 2)
            {
                if (permitNumberCachefileExists == false)
                {
                    writePermitNumberTableWithinBoundingBox(_animal, permitNumberFileCache, _north, _south, _east, _west);

                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
                else
                {
                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
            }
            if (type == 3)
            {
                if (permitNumberCachefileExists == false)
                {
                    writePermitNumberTableWithinHuc(_animal, permitNumberFileCache, _Huc8);

                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
                else
                {
                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
            }
            if (type == 4)
            {
                if (permitNumberCachefileExists == false)
                {
                    writePermitNumberTableWithinHuc(_animal, permitNumberFileCache, _Huc12);

                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
                else
                {
                    if (File.Exists(permitNumberFile) == false)
                    {
                        File.Copy(permitNumberFileCache, permitNumberFile);
                    }
                    FileNames.Add(permitNumberFile);
                }
            }
            string addressFileCache = System.IO.Path.Combine(aCacheFolder, "Addresses" + "(" + _animal + ext + ").csv");
            string addressFile = System.IO.Path.Combine(aSubFolder, "Addresses" + "(" + _animal + ext + ").csv");
            bool addressFileExists = checkCache(addressFileCache);
            if (addressFileExists == false)
            {
                writeAddressTable(permitNumbers, _animal, addressFileCache);

                if (File.Exists(addressFile) == false)
                {
                    File.Copy(addressFileCache, addressFile);
                }
                FileNames.Add(addressFile);
            }
            else
            {
                if (File.Exists(addressFile) == false)
                {
                    File.Copy(addressFileCache, addressFile);
                }
                FileNames.Add(addressFile);
            }
            string shapeFileCache = System.IO.Path.Combine(aCacheFolder, _animal + ext + ".shp");
            string shapeFile = System.IO.Path.Combine(aSubFolder, _animal + ext + ".shp");
            string dbfFileCache = System.IO.Path.Combine(aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".dbf");
            string prjFileCache = System.IO.Path.Combine(aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".prj");
            string shxFileCache = System.IO.Path.Combine(aCacheFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".shx");
            string dbfFile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".dbf");
            string prjFile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".prj");
            string shxFile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapeFileCache) + ".shx");
            bool shapeFileExists = checkCache(shapeFileCache);
            if (addressFileExists == false)
            {
                writeShapeFiles(permitNumbers, _animal, shapeFileCache, addressFileCache);

                if (File.Exists(shapeFile) == false)
                {
                    File.Copy(shapeFileCache, shapeFile);
                    File.Copy(dbfFileCache, dbfFile);
                    File.Copy(shxFileCache, shxFile);
                    File.Copy(prjFileCache, prjFile);
                }
                FileNames.Add(shapeFile);
            }
            else
            {
                if (File.Exists(shapeFile) == false)
                {
                    File.Copy(shapeFileCache, shapeFile);
                    File.Copy(dbfFileCache, dbfFile);
                    File.Copy(shxFileCache, shxFile);
                    File.Copy(prjFileCache, prjFile);
                }
                FileNames.Add(shapeFile);
            }

            string aSubFolderAll = System.IO.Path.Combine(aProjectFolder, "AllAnimalLocations" + ext);
       //     Directory.CreateDirectory(aSubFolderAll);
            //   writeOverallShapeFile(animalLayers, aProjectFolder, aSubFolderAll, ext);
        }


    }
}

