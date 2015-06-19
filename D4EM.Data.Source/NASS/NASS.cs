using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Projections;
using System.IO;

namespace D4EM.Data.Source
{
    public static class NASS
    {
        static private string pBaseCDL = "http://nassgeodata.gmu.edu/CropScape/GetCDL?";
        static private string pBaseQuickStats = "http://quickstats.nass.usda.gov/";
        static public readonly ProjectionInfo NativeProjection = Globals.AlbersProjection();
        static public readonly LayerSpecification NASS_Layer_Specification = new LayerSpecification("NASS", "NASS Crop", "*.tif", typeof(NASS), LayerSpecification.Roles.LandUse); //TODO: add NoData value to NASS_Layer_Specification

        /// <summary>
        /// Get CDL crop raster coverage from nassgeodata.gmu.edu
        /// </summary>
        /// <param name="aProject"></param>
        /// <param name="aSubFolder"></param>
        /// <param name="aLegendFile"></param>
        /// <param name="aYear"></param>
        /// <returns></returns>
        public static string getRaster(Project aProject, string aSubFolder, string aLegendFile, int aYear)
        {
            string lSaveIn = aProject.ProjectFolder;
            if (aSubFolder != null && aSubFolder.Length > 0) lSaveIn = Path.Combine(lSaveIn, aSubFolder);
            Directory.CreateDirectory(lSaveIn);
            double north = 0; double south = 0; double east = 0; double west = 0;
            aProject.Region.GetBounds(ref north, ref south, ref west, ref east, KnownCoordinateSystems.Geographic.World.WGS1984);
            string lCachefolder = Path.Combine(aProject.CacheFolder, "NASS");
            string lLayerFilename = getData(lSaveIn, lCachefolder, aLegendFile, aYear, north, south, east, west);
            if (File.Exists(lLayerFilename))
            {
                var lNewLayer = new Layer(lLayerFilename, NASS_Layer_Specification, false);
                if (!aProject.DesiredProjection.Matches(NativeProjection))
                {
                    lNewLayer.Reproject(aProject.DesiredProjection);
                }
                aProject.Layers.Add(lNewLayer);
            }
            return "<add_grid>" + lLayerFilename + "</add_grid>";
        }

        /// <summary>
        /// Get crop statistics for the counties overlapping the project area
        /// </summary>
        /// <param name="aProject">Project to get statistics for and use the cache folder of</param>
        /// <param name="aSubFolder">Sub-folder within aProject.ProjectFolder or full path of folder to save in</param>
        /// <param name="year">Year to retrieve statistics from</param>
        /// <returns>name(s) of file(s) downloaded (as XML fragment)</returns>
        public static string getStatistics(Project aProject, string aSubFolder, string year)
        {
            string results = "";
            string lSaveIn = aProject.ProjectFolder;
            if (aSubFolder != null && aSubFolder.Length > 0) lSaveIn = Path.Combine(lSaveIn, aSubFolder);
            Directory.CreateDirectory(lSaveIn);
            foreach (string lCountyFIPS in aProject.GetKeys(D4EM.Data.Region.RegionTypes.county))
            {
                string lStateName = National.StateNameFromFIPS(lCountyFIPS).ToUpper().Trim();
                string lCountyName = National.CountyNameFromFIPS(lCountyFIPS).ToUpper().Replace(" COUNTY", "").Trim();

                string lSaveAs = System.IO.Path.Combine(lSaveIn, "NASS_" + lStateName + "_" + lCountyName + "_" + year + ".txt");
                if (lStateName.Length > 0 && lCountyName.Length > 0 && !File.Exists(lSaveAs))
                {
                    string lSaveAsCache = System.IO.Path.Combine(aProject.CacheFolder, "NASS_Statistics", Path.GetFileName(lSaveAs));
                    if (!File.Exists(lSaveAsCache))
                    {
                        getStatistics(lStateName, lCountyName, year, lSaveAsCache);
                    }

                    if (File.Exists(lSaveAsCache))
                    {
                        File.Copy(lSaveAsCache, lSaveAs);
                        if (File.Exists(lSaveAsCache + ".xml"))
                        {
                            File.Copy(lSaveAsCache + ".xml", lSaveAs + ".xml");
                        }
                    }
                }
                if (File.Exists(lSaveAs))
                {
                    results += "<add_data type='NASS::Statistics'>" + lSaveAs + "</add_data>\n";
                }
            }
            return results;
        }

        /// <summary>
        /// Request data from QuickStats and return the identifier (UUID) that can be used to download the requested data
        /// </summary>
        /// <param name="aArgs">parameters of data request</param>
        /// <returns>UUID that can be used to download the requested data</returns>
        private static string getUUID(params string[] aArgs)
        {
            string URL = pBaseQuickStats + "uuid/encode?" + aArgs[0] + '=' + aArgs[1];
            for (int lArgIndex = 2; lArgIndex < aArgs.Length; lArgIndex += 2)
                URL += '&' + aArgs[lArgIndex] + '=' + aArgs[lArgIndex + 1];
            return D4EM.Data.Download.DownloadURL(URL).Replace("\"", "");
        }

        //public static string getStatistic(string commodity_desc, string short_desc, string state_name)
        //{   //http://quickstats.nass.usda.gov/uuid/encode?
        //    //commodity_desc=ALMONDS&short_desc=ALMONDS%20-%20ACRES&agg_level_desc=COUNTY&state_name=GEORGIA&index=Home&breadcrumb=commodity_desc&breadcrumb=short_desc&breadcrumb=agg_level_desc&breadcrumb=state_name
        //    uuid = getUUID("commodity_desc", commodity_desc,
        //                   "short_desc", short_desc,
        //                   "agg_level_desc", "COUNTY",
        //                   "state_name", state_name);


        /// <summary>
        /// Get crop statistical data from quickstats.nass.usda.gov
        /// </summary>
        /// <param name="state_name">Full name of state</param>
        /// <param name="county_name">Full name of county</param>
        /// <param name="year">four-digit year</param>
        /// <param name="aSaveAs">Full path of file to save statistics in. If this file exists, it will be kept instead of downloading.</param>
        /// <returns>Full path of saved data or empty string if it could not be downloaded.</returns>
        /// <remarks>Only first 1000 lines of data are retrieved. This should be plenty for any one county-year.
        /// Caching is not handled in this method. See the other getStatistics overload for caching.</remarks>
        public static string getStatistics(string state_name, string county_name, string year, string aSaveAs)
        {   //http://quickstats.nass.usda.gov/uuid/encode?agg_level_desc=COUNTY&state_name=GEORGIA&county_name=ATKINSON&year=2009
            //http://quickstats.nass.usda.gov/grid/grid_layout/7C4B3D9F-15F6-3EF3-A2C0-7095218FF6A1
            //http://quickstats.nass.usda.gov/grid/grid_data/7C4B3D9F-15F6-3EF3-A2C0-7095218FF6A1?start=0&count=30

            state_name = state_name.ToUpper();
            //Ensure county_name is all upper case and remove the word County if it was included in the name.
            county_name = county_name.ToUpper().Replace(" COUNTY", "");
            if (!File.Exists(aSaveAs))
            {
                string uuid = getUUID("agg_level_desc", "COUNTY",
                                        "state_name", state_name,
                                        "county_name", county_name,
                                        "year", year);

                D4EM.Data.Download.DownloadURL(pBaseQuickStats + "grid/grid_data/" + uuid + "?start=0&count=1000", aSaveAs);
            }
            if (File.Exists(aSaveAs))
            {
                return aSaveAs;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retrieve CDL crop raster layer or copy it from aCacheFolder
        /// </summary>
        /// <param name="aSaveFolder">Folder to save downloaded data in</param>
        /// <param name="aCacheFolder">Search for data in NASS sub-folder of aCacheFolder and use data if found; If need to download data, save it here in addition to aSaveFolder</param>
        /// <param name="aLegendFile">Full Path of Legend file to copy into same folder with layer. (If not found, defaults to "NASSLegend.txt" in the same folder with this library.)</param>
        /// <param name="year">Year of data to search for</param>
        /// <param name="north_degrees">North boundary of area of interest in degrees (WGS1984)</param>
        /// <param name="south_degrees">South boundary of area of interest in degrees (WGS1984)</param>
        /// <param name="east_degrees">East boundary of area of interest in degrees (WGS1984)</param>
        /// <param name="west_degrees">West boundary of area of interest in degrees (WGS1984)</param>
        /// <returns>Full path of saved data or empty string if not downloaded.</returns>
        public static string getData(string aSaveFolder, string aCacheFolder, string aLegendFile, int year, 
                                     double north_degrees, double south_degrees, double east_degrees, double west_degrees)
        {
            string w_deg_str = String.Format("{0:0.00}", west_degrees);
            string n_deg_str = String.Format("{0:0.00}", north_degrees);
            string e_deg_str = String.Format("{0:0.00}", east_degrees);
            string s_deg_str = String.Format("{0:0.00}", south_degrees);
            string lFileNameOnly = "NASS" + year.ToString() + ";N" + n_deg_str + ";S" + s_deg_str + ";E" + e_deg_str + ";W" + w_deg_str + ".tif";

            Directory.CreateDirectory(aSaveFolder);
            string lSaveAs = Path.Combine(aSaveFolder, lFileNameOnly);
            if (!File.Exists(lSaveAs))
            {
                string lSaveAsCache = System.IO.Path.Combine(aCacheFolder, lFileNameOnly);
                if (!File.Exists(lSaveAsCache))
                {
                    double[] xyA = { west_degrees, north_degrees, 
                                     east_degrees, north_degrees, 
                                     west_degrees, south_degrees,
                                     east_degrees, south_degrees};

                    DotSpatial.Projections.Reproject.ReprojectPoints(xyA, null, KnownCoordinateSystems.Geographic.World.WGS1984, NativeProjection, 0, 4);

                    string w = String.Format("{0:0.00}", Math.Min(xyA[0], xyA[4]));
                    string n = String.Format("{0:0.00}", Math.Max(xyA[1], xyA[3]));
                    string e = String.Format("{0:0.00}", Math.Max(xyA[2], xyA[6]));
                    string s = String.Format("{0:0.00}", Math.Min(xyA[5], xyA[7]));

                    string url_tif = pBaseCDL + "year=" + year + "&bbox=" + w + "," + s + "," + e + "," + n;
                    D4EM.Data.Download.DownloadURL(url_tif, lSaveAsCache); //creates metadata XML file with timestamp and URL
                    //Add bounding box to metadata file
                    var lMetaData = new MapWinUtility.Metadata(lSaveAsCache + ".xml");
                    lMetaData.SetBoundingBox(w_deg_str, e_deg_str, n_deg_str, s_deg_str);
                    lMetaData.Save();
                }
                if (File.Exists(lSaveAsCache))
                {
                    File.Copy(lSaveAsCache, lSaveAs);
                    if (File.Exists(lSaveAsCache + ".xml"))
                    {
                        File.Copy(lSaveAsCache + ".xml", lSaveAs + ".xml");
                    }
                    if (!File.Exists(aLegendFile))
                    {
                        aLegendFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "NASSLegend.txt");
                    }
                    if (File.Exists(aLegendFile))
                    {
                        File.Copy(aLegendFile, Path.Combine(aSaveFolder, "NASSLegend.txt"));
                    }
                }
            }
            if (File.Exists(lSaveAs))
            {
                return lSaveAs;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
