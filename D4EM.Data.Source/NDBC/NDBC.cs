using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Data;
using System.Xml;
using DotSpatial.Data;
using DotSpatial.Projections;


namespace D4EM.Data.Source
{
    public class NDBC
    {
        public List<string> FileNames = new List<string>();
        DataTable dt = new DataTable();
        static double _lat = 0;
        static double _lng = 0;
        static double _radius = 0;


        public NDBC()
        {
        }

        public NDBC(string aProjectFolder, string aSaveFolder, double lat, double lng, double radius)
        {
            _lat = lat;
            _lng = lng;
            _radius = radius;
            string url = "http://www.ndbc.noaa.gov/rss/ndbc_obs_search.php?lat=" + lat + "&lon=" + lng + "&radius=" + radius;

         //   Process process = Process.Start(url);
          //  Thread.Sleep(5000);

            string tempFile = @"C:\Temp\NDBCTemp.xml";

            TextWriter tw = new StreamWriter(tempFile);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
            
            dt.Columns.Add("Station");
            dt.Columns.Add("Date/Time");
            dt.Columns.Add("Location");
            dt.Columns.Add("Wind Direction");
            dt.Columns.Add("Wind Speed");
            dt.Columns.Add("Wind Gust");
            dt.Columns.Add("Air Temperature");
            dt.Columns.Add("Water Temperature");
            dt.Columns.Add("Atmospheric Pressure");
            dt.Columns.Add("Pressure Tendency");
            dt.Columns.Add("Dew Point");
            dt.Columns.Add("Significant Wave Height");
            dt.Columns.Add("Dominant Wave Period");
            dt.Columns.Add("Average Period");
            dt.Columns.Add("Mean Wave Direction");
            dt.Columns.Add("Tide");
            dt.Columns.Add("Visibility");


            string title = "";


            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            char[] sep = new char[1];
            sep[0] = '<';
            int j = -1;
            using (XmlReader reader = XmlReader.Create(tempFile, settings))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {// Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "title":

                                title = reader.ReadString();
                                title = title.Replace(",", " ");
                                break;
                            case "description":
                                string description = reader.ReadString();
                                string[] values = description.Split(sep, 50);
                                if (values.Length < 5)
                                {
                                    break;
                                }
                                dt.Rows.Add();
                                j++;
                                dt.Rows[j]["Station"] = title;
                                string[] newvalues = new string[values.Length];
                                int i = 0;
                                foreach (string value in values)
                                {
                                    string trimmed = value.Trim();
                                    trimmed = trimmed.Replace("strong", "");
                                    trimmed = trimmed.Replace("br", "");
                                    trimmed = trimmed.Replace(">", "");
                                    trimmed = trimmed.Replace("/", "");
                                    trimmed = trimmed.Replace("\n", "");
                                    trimmed = trimmed.Replace("&#176;", "");
                                    trimmed = trimmed.Replace(",", " ");
                                    
                                    newvalues[i] = trimmed;
                                    i++;
                                }



                                bool timeAdded = false;
                                int l = 0;
                                foreach (string value in newvalues)
                                {
                                    if ((value == "") || (value == " "))
                                    {
                                        l++;
                                        continue;
                                    }
                                    if ((value != "") && (timeAdded == false))
                                    {
                                        dt.Rows[j]["Date/Time"] = newvalues[l];
                                        timeAdded = true;
                                    }
                                    else if (value.Contains("Location"))
                                    {
                                        string location = newvalues[l + 1];
                                        dt.Rows[j]["Location"] = location;
                                    }
                                    else if (value.Contains("Wind Direction"))
                                    {
                                        dt.Rows[j]["Wind Direction"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Wind Speed"))
                                    {
                                        dt.Rows[j]["Wind Speed"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Wind Gust"))
                                    {
                                        dt.Rows[j]["Wind Gust"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Air Temperature"))
                                    {
                                        dt.Rows[j]["Air Temperature"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Water Temperature"))
                                    {
                                        dt.Rows[j]["Water Temperature"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Atmospheric Pressure"))
                                    {
                                        dt.Rows[j]["Atmospheric Pressure"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Pressure Tendency"))
                                    {
                                        dt.Rows[j]["Pressure Tendency"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Dew Point"))
                                    {
                                        dt.Rows[j]["Dew Point"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Significant Wave Height"))
                                    {
                                        dt.Rows[j]["Significant Wave Height"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Dominant Wave Period"))
                                    {
                                        dt.Rows[j]["Dominant Wave Period"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Average Period"))
                                    {
                                        dt.Rows[j]["Average Period"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Mean Wave Direction"))
                                    {
                                        dt.Rows[j]["Mean Wave Direction"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Tide"))
                                    {
                                        dt.Rows[j]["Tide"] = newvalues[l + 1];
                                    }
                                    else if (value.Contains("Visibility"))
                                    {
                                        dt.Rows[j]["Visibility"] = newvalues[l + 1];
                                    }
                                    l++;
                                }

                                break;

                        }
                    }
                }
            }            
            string aSubFolder = System.IO.Path.Combine(aProjectFolder, aSaveFolder);
            Directory.CreateDirectory(aSubFolder);
            
            string shapefilename = System.IO.Path.Combine(aSubFolder, "Lat" + _lat + ";Lng" + _lng + ";Radius" + _radius + ".shp");
            
            string dbffile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapefilename) + ".dbf");
            string prjfile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapefilename) + ".prj");
            string shxfile = System.IO.Path.Combine(aSubFolder, Path.GetFileNameWithoutExtension(shapefilename) + ".shx");     
            Directory.CreateDirectory(aSubFolder);
            writeShapeFile(shapefilename);
            FileNames.Add(shapefilename);
            string tabulatedDataFileName = System.IO.Path.Combine(aSubFolder, "TabulatedNDBCData.csv");
            writeTabulatedDataFile(tabulatedDataFileName);
            FileNames.Add(tabulatedDataFileName);

            string metadataFileName = System.IO.Path.Combine(aSubFolder, "Metadata.txt");
            writeMetadataFile(metadataFileName, url, shapefilename, tabulatedDataFileName);
            FileNames.Add(metadataFileName);
        }

        private void writeShapeFile(string aShapeFileName)
        {
            
            FeatureSet pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            foreach (DataColumn dc in dt.Columns)
            {
                pointCoords.DataTable.Columns.Add(dc.ColumnName);
            }

            char[] sep = new char[2];
            sep[0] = 'N';
            sep[1] = 'W';            

            foreach (DataRow dr in dt.Rows)
            {
                string[] splitLocation = dr[2].ToString().Split(sep, 10);
                double latitude = Convert.ToDouble(splitLocation[0]);
                double longitude = -Convert.ToDouble(splitLocation[1]);
                DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                IFeature currentFeature = pointCoords.AddFeature(point);

                int i = 0;
                foreach (object value in dr.ItemArray)
                {                      
                    currentFeature.DataRow[i] = value.ToString();
                    i++;
                }

            }
            pointCoords.SaveAs(aShapeFileName, true);
        }


        private void writeTabulatedDataFile(string aFileName)
        {


            TextWriter tw = new StreamWriter(aFileName);

            int j = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (j < dt.Columns.Count)
                {
                    tw.Write(dc.ColumnName + ",");
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
                        tw.Write(obj.ToString() + ",");
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

        private static bool checkCache(string fileNameCache)
        {
            bool fileExists = false;

            if (File.Exists(fileNameCache))
            {
                fileExists = true;
            }

            return fileExists;
        }

        private static void writeMetadataFile(string metadataFileName, string url, string aShapefileName, string tabulatedFileName)
        {
            TextWriter tw = new StreamWriter(metadataFileName);

            tw.WriteLine("Download Time = " + DateTime.Now);
            tw.WriteLine("");
            tw.WriteLine("Area of Interest: ");
            tw.WriteLine("Latitude: " + _lat);
            tw.WriteLine("Longitude: " + _lng);
            tw.WriteLine("Radius: " + _radius + " (Nautical Miles)");
            tw.WriteLine("");
            tw.WriteLine("Data was read from " + url);
            tw.Write("");
            tw.WriteLine("Data was written to: ");
            tw.WriteLine(aShapefileName);
            tw.Write(tabulatedFileName);
            tw.Close();
        }

        public DataTable dataTable
        {
            get { return dt; }
            set { dt = null; }
        }

        public int caseColumns = 1;

        public DataTable getHistoricalData(string folder, string stationID, string year)
        {
            Directory.CreateDirectory(folder);

            DataTable dt = new DataTable();
            string tempFile = @"C:\Temp\NDBCTemp.tsv";
            string url = "http://www.ndbc.noaa.gov/view_text_file.php?filename=" + stationID.ToLower() +"h" + year + ".txt.gz&dir=data/historical/stdmet/";
            TextWriter tw = new StreamWriter(tempFile);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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


            string outputFile = Path.Combine(folder,stationID + " (Year "+ year + ").csv");
            TextReader tr = new StreamReader(tempFile);
            TextWriter twr = new StreamWriter(outputFile);
            string line;

            string[] items = new string[18];
            int j = 0;
            while ((line = tr.ReadLine()) != null)
            {
                string[] array = line.Split(' ');
                foreach (string item in array)
                {
                    if (item.Contains("BAR"))
                    {
                        caseColumns = 2;
                    }
                }
                int i = 0;
                if (caseColumns == 1)
                {
                    foreach (string arrayItem in array)
                    {
                        if (arrayItem != "")
                        {
                            if (j == 0)
                            {
                                items[i] = arrayItem;
                            }
                            else if (j == 1)
                            {
                                items[i] = items[i] + " (" + arrayItem + ")";
                                twr.Write(items[i] + ",");
                                dt.Columns.Add(items[i]);
                            }
                            else
                            {
                                items[i] = arrayItem;
                                twr.Write(arrayItem + ",");
                            }
                            i++;
                        }
                    }
                    if (j > 1)
                    {
                        dt.Rows.Add(items);
                    }
                    if (j != 0)
                    {
                        twr.WriteLine();
                    }
                    j++;
                }
                else if (caseColumns == 2)
                {
                    items = new string[16];
                    foreach (string arrayItem in array)
                    {
                        if (arrayItem != "")
                        {
                            
                            items[i] = arrayItem;
                            twr.Write(arrayItem + ",");
                            if (j == 0)
                            {
                                dt.Columns.Add(items[i]);
                            }
                            i++;
                        }
                    }
                    if (j > 0)
                    {
                        dt.Rows.Add(items);
                    }
                    
                    twr.WriteLine();
                    
                    j++;
                }
            }

            tr.Close();
            twr.Close();
            File.Delete(tempFile);
            return dt;
            
        }
    }
}
