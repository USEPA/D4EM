using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.ComponentModel;
using DotSpatial.Projections;
using D4EM.Data;

namespace D4EM.Data.Source
{
    /// <summary>
    /// This class provides REST access to  National Climatic Data Center data
    /// Access requires a NCDC token.  Users can obtain a token at the following address:
    ///  http://www7.ncdc.noaa.gov/wsregistration/ws_home.html

    /// Info on how REST URLs are constructed can be found at:
    /// http://www7.ncdc.noaa.gov/rest/

    /// Data set types include: 
    ///   Daily
    ///   ISD - Integrated Surface Database - http://www.ncdc.noaa.gov/oa/climate/isd/index.php
    ///   ISH - Integrated Surface Hourly   - http://gcmd.nasa.gov/records/GCMD_gov.noaa.ncdc.C00532.html

    /// Site information contains
    /// DatasetID
    /// stationid
    /// name
    /// lat
    /// lon
    /// elev
    /// timezone
    /// lowdate
    /// highdate
    /// </summary>
    public static class NCDC
    {
        public class LayerSpecifications
        {
            public static LayerSpecification Daily  = new LayerSpecification(
                Tag:"daily", Role:D4EM.Data.LayerSpecification.Roles.MetStation, Source:typeof(NCDC));

            public static LayerSpecification ISD = new LayerSpecification(
                Tag: "isd", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NCDC));

            public static LayerSpecification ISH = new LayerSpecification(
                Tag: "ish", Role: D4EM.Data.LayerSpecification.Roles.MetStation, Source: typeof(NCDC));
        }

        public enum OutputTypes
        {
            xml,
            csv,
            waterml
        }

        private const string _baseUrl = @"http://www7.ncdc.noaa.gov/rest/services/";
        private const string _urlRequestToken = @"http://www7.ncdc.noaa.gov/wsregistration/ws_home.html";
        private const string _NoToken = "NONE";
        private static string _token = _NoToken;

        public static string token
        {
            get 
            {
                if (!HasToken())
                    throw new ApplicationException("NCDC Token Not Set. See " + _urlRequestToken);
                return _token;
            }
            set { _token = value; }
        }

        public static bool HasToken()
        {
            return (!(string.IsNullOrEmpty(_token) || _token.Equals(_NoToken)));
        }

        /// <summary>
        /// Get the closest sites to the given project region
        /// </summary>
        /// <param name="aProject">project to add data to</param>
        /// <param name="aSiteIDs">returned list of station IDs</param>
        /// <param name="aSiteShapeFilename">If not null or blank, also create this shape file containing the stations.</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <param name="aNumberOfStations">This many stations will be included in aStationIDs</param>
        /// <returns>XML describing success or error</returns>
        /// <remarks>Sites are sorted by distance from the center of aProject.Region</remarks>
        /// TODO: review this and BASINS and NWIS site retrieval and make all as similar as possible
        static public string GetSites(Project aProject, ref List<string> aSiteIDs, string aSiteShapeFilename, LayerSpecification datasetID)
        {
            if (!File.Exists(aSiteShapeFilename))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(aSiteShapeFilename));
                var lAllSites = AllSitesSortedByDistanceFromCenterOfProject(aProject, datasetID);
                for (int lSiteIndex = 0; lSiteIndex < 10; lSiteIndex++)
                    File.AppendAllText(aSiteShapeFilename, lAllSites.Keys[lSiteIndex].ToString() + "\n");
                //TODO: add point to shapefile, add fields to DBF instead of csv                   
            }
            if (File.Exists(aSiteShapeFilename))
            {
            //    var lNewLayer = new Layer(lLayerFilename, datasetID, false);
            //    if (!aProject.DesiredProjection.Matches(KnownCoordinateSystems.Geographic.World.WGS1984))
            //    {
            //        lNewLayer.Reproject(aProject.DesiredProjection);
            //    }
            //    aProject.Layers.Add(lNewLayer);
            }
            return "<add_shape>" + aSiteShapeFilename + "</add_shape>";
        }

        /// <summary>
        /// Get data from the closest site with all the needed data types
        /// </summary>
        /// <param name="aProject">project to add data to</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <param name="variableIds">NCDC data types to retrieve such as AA1, TMP, WND, DEW, GF1</param>
        /// <param name="start">Starting data of data to retrieve</param>
        /// <param name="end">Ending data of data to retrieve</param>
        /// <param name="numSites">Number of stations to get data for</param>
        /// <returns>XML describing success or error</returns>
        /// <remarks>Data is saved in "met" folder within aProject.ProjectFolder (code to read data and add to aProject.TimeseriesSources commented out)</remarks>
        static public string GetClosestSitesWithData(Project aProject, LayerSpecification datasetID, List<string> variableIds, DateTime start, DateTime end, int numSites)
        {
            string lMetDataDir = System.IO.Path.Combine(aProject.ProjectFolder, "met");
            System.IO.Directory.CreateDirectory(lMetDataDir);
            string lCacheDir = System.IO.Path.Combine(aProject.CacheFolder, "NCDC");
            System.IO.Directory.CreateDirectory(lCacheDir);

            string result = "";
            int foundSites = 0;
            var lAllSites = AllSitesSortedByDistanceFromCenterOfProject(aProject, datasetID);
            //Do not try again to get data from a site that already has incomplete cached data
            List<int> lIncompleteSites = new List<int>();
            //First try just searching nearest stations for cached data, then try downloading from near or farther.            
            for (int lTryDownload = 0; lTryDownload < 2; lTryDownload++)
            {
                int lLastSiteToSearch = numSites * 10;
                if (lTryDownload > 0) lLastSiteToSearch = 1000; //If we did not find what we need in the cache, expand the search to farther stations
                if (lAllSites.Count <= lLastSiteToSearch) lLastSiteToSearch = lAllSites.Count - 1;
                for (int lSiteIndex = 0; lSiteIndex <= lLastSiteToSearch; lSiteIndex++)
                {
                    if (lIncompleteSites.Contains(lSiteIndex)) continue; //Already found that this station is incomplete
                    string lStationRecord = lAllSites.Keys[lSiteIndex].ToString();
                    string lStationId = lStationRecord.Split(',')[1];
                    if (result.Contains(lStationId)) continue; //Already using this station

                    var lCacheFileNames = new List<string>();
                    int lVariablesFoundInCache = 0; //different from lCacheFileNames.Count because it is not incremented when new data is downloaded
                    try
                    {
                        foreach (string lVariableId in variableIds)
                        {
                            string lCacheFileName = System.IO.Path.Combine(lCacheDir,
                                    datasetID.Tag + '.'
                                  + lStationId + '.'
                                  + lVariableId + '.'
                                  + start.ToString("yyyy-MM-dd") + "to"
                                  + end.ToString("yyyy-MM-dd") + ".csv");

                            FileInfo f = new FileInfo(lCacheFileName);
                            if (f.Exists && f.Length > 100)
                            {
                                lCacheFileNames.Add(lCacheFileName);
                                lVariablesFoundInCache++;
                            }
                            else if (f.Exists || lVariablesFoundInCache > 0)
                            {   //Finding empty cache file or some but not all variables in the cache means that the station was found to be incomplete last time
                                if (lVariablesFoundInCache > 0) lIncompleteSites.Add(lSiteIndex);
                                break;
                            }
                            else if (lTryDownload == 0)
                                break; //skip looking for other variables in cache if any are not found
                            else 
                            {
                                string gotValues = GetValues(OutputTypes.csv, datasetID, lStationId, lVariableId, start, end);
                                File.WriteAllText(lCacheFileName, gotValues);
                                if (gotValues.Length > 100)
                                {
                                    lCacheFileNames.Add(lCacheFileName);
                                }
                                else
                                {
                                    string lShortMsg = "Did not retrieve data for " + lVariableId + " at " + lStationId + " from NCDC: '" + gotValues + "'";
                                    MapWinUtility.Logger.Dbg(lShortMsg);
                                    //result += " " + lShortMsg;
                                    break; //Since we did not get this variable, we might as well move on to the next station
                                }
                            }
                        }
                        if (lCacheFileNames.Count == variableIds.Count)
                        {
                            MapWinUtility.Logger.Status("Found NCDC site " + lStationRecord.Split(',')[3] + " " + lStationId + " with all " + variableIds.Count + " desired variables", true);
                            result += " " + lStationId;
                            foreach (string lCacheFileName in lCacheFileNames)
                            {   // open and add met data to project
                                string lDataFileName = lCacheFileName.Replace(lCacheDir, lMetDataDir);
                                if (MapWinUtility.modFile.TryCopy(lCacheFileName, lDataFileName))
                                {
                                    System.IO.File.WriteAllText(lDataFileName + ".station", lStationRecord);
                                    //var newFile = new atcTimeseriesNCDC.atcTimeseriesNCDC();
                                    //if (newFile.Open(lDataFileName))
                                    //    aProject.TimeseriesSources.Add(newFile);
                                }
                            }
                            foundSites++;
                            if (foundSites >= numSites)
                                return "<success>" + result + "</success>";
                        }
                    }
                    catch (Exception ex)
                    {
                        MapWinUtility.Logger.Dbg("Exception in NCDC.GetClosestSitesWithData: " + ex.Message);
                        continue;
                    }
                }
            }
            return "<error>" + result + "</error>";
        }

        /// <summary>
        /// Get data from the given station
        /// </summary>
        /// <param name="aProject">project to add data to</param>
        /// <param name="aStationId">NCDC station to retrieve from</param>
        /// <param name="variableIds">NCDC data types to retrieve such as AA1, TMP, WND, DEW, GF1</param>
        /// <param name="start">Starting data of data to retrieve</param>
        /// <param name="end">Ending data of data to retrieve</param>
        /// <param name="numSites">Number of stations to get data for</param>
        /// <returns>XML describing success or error</returns>
        /// <remarks>Data is saved in "met" folder within aProject.ProjectFolder (code to read data and add to aProject.TimeseriesSources commented out)</remarks>
        static public string GetDataFromStation(Project aProject, String aStationId, List<string> variableIds, DateTime start, DateTime end)
        {
            string lMetDataDir = System.IO.Path.Combine(aProject.ProjectFolder, "met");
            System.IO.Directory.CreateDirectory(lMetDataDir);
            string lCacheDir = System.IO.Path.Combine(aProject.CacheFolder, "NCDC");
            System.IO.Directory.CreateDirectory(lCacheDir);

            string result = "";
            var lCacheFileNames = new List<string>();
            foreach (string lVariableId in variableIds)
            {
                try
                {
                    string lCacheFileName = System.IO.Path.Combine(lCacheDir,
                            NCDC.LayerSpecifications.ISH.Tag + '.'
                            + aStationId + '.'
                            + lVariableId + '.'
                            + start.ToString("yyyy-MM-dd") + "to"
                            + end.ToString("yyyy-MM-dd") + ".csv");

                    FileInfo f = new FileInfo(lCacheFileName);
                    if (f.Exists && f.Length > 100)
                    {
                        lCacheFileNames.Add(lCacheFileName);
                    }
                    else
                    {
                        string gotValues = GetValues(OutputTypes.csv, NCDC.LayerSpecifications.ISH, aStationId, lVariableId, start, end);
                        if ((gotValues.Length > 100) && !gotValues.StartsWith("<"))
                        {
                            File.WriteAllText(lCacheFileName, gotValues);
                            lCacheFileNames.Add(lCacheFileName);
                        }
                        else
                        {
                            string lShortMsg = "Did not retrieve data for " + lVariableId + " at " + aStationId + " from NCDC: '" + gotValues + "'";
                            MapWinUtility.Logger.Dbg(lShortMsg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MapWinUtility.Logger.Dbg("Exception in NCDC.GetDataFromStation: " + ex.ToString());
                    continue;
                }
                //                result += " " + aStationId;
                foreach (string lCacheFileName in lCacheFileNames)
                {   // open and add met data to project
                    string lDataFileName = lCacheFileName.Replace(lCacheDir, lMetDataDir);
                    MapWinUtility.modFile.TryCopy(lCacheFileName, lDataFileName);
                }
                return "<success>" + result + "</success>";
            }           
            return "<error>" + result + "</error>";
        }

        static public bool GetAllSitesCSV(string aFileName, LayerSpecification datasetID)
        {
            if (File.Exists(aFileName))
            {
                return true;
            }
            else
            { 
                Directory.CreateDirectory(Path.GetDirectoryName(aFileName));
                MapWinUtility.Logger.Status("Downloading list of NCDC sites");
                return Download.DownloadURL(URL(OutputTypes.csv, "sites", datasetID.Tag), aFileName);
            }
        }

        static private atcUtility.atcCollection AllSitesSortedByDistanceFromCenterOfProject(Project aProject, LayerSpecification datasetID) 
        {
            double north = 0; double south = 0; double east = 0; double west = 0;
            aProject.Region.GetBounds(ref north, ref south, ref west, ref east, KnownCoordinateSystems.Geographic.World.WGS1984);

            string lCachefolder = Path.Combine(aProject.CacheFolder, "NCDC");
            string lAllSitesCSVcache = Path.Combine(lCachefolder, "All_" + datasetID.Tag + ".csv");
            if (GetAllSitesCSV(lAllSitesCSVcache, datasetID))
            {
                string[] lSiteRecord;
                double lCenterLatitude = (north + south) / 2;
                double lCenterLonitude = (east + west) / 2;
                double lSiteLatitude;
                double lSiteLonitude;
                var lAllSites = new atcUtility.atcCollection();// SortedList<double, string>();
                MapWinUtility.Logger.Status("Reading list of NCDC sites");
                foreach (string lSiteCSV in atcUtility.modFile.LinesInFile(lAllSitesCSVcache))
                {
                    lSiteRecord = lSiteCSV.Split(',');
                    if (double.TryParse(lSiteRecord[4], out lSiteLatitude)
                        && double.TryParse(lSiteRecord[5], out lSiteLonitude))
                    //&& lSiteLatitude >= west && lSiteLatitude <= east&& lSiteLonitude >= south && lSiteLonitude <= north //Limiting sites to those within the region means many projects get no sites
                    {
                        //TODO: compute distance using aProject.DesiredProjection instead of lat/long
                        lAllSites.Add(lSiteCSV, Math.Pow(lSiteLatitude - lCenterLatitude, 2) + Math.Pow(lSiteLonitude - lCenterLonitude, 2));
                    }
                }
                MapWinUtility.Logger.Status("Sorting list of NCDC sites by distance");
                lAllSites.SortByValue();
                MapWinUtility.Logger.Status("Total of " + lAllSites.Count + " NCDC sites");
                return lAllSites;
            }
            throw new ApplicationException("Could not find sites");
        }

        /// <summary>
        /// Builds NCDC URL from _baseUrl, arguments, and token
        /// </summary>
        /// <param name="outputType">xml, csv, waterml</param>
        /// <param name="args">parameters needed to build a particular REST query</param>
        /// <returns>URL</returns>
        static private string URL(OutputTypes outputType, params string[] args)
        {
            return _baseUrl + string.Join(@"/", args) + "?output=" + outputType.ToString() + "&token=" + token;
        }

        /// <summary>
        /// Private base method for getting data. Builds URL from _baseUrl, arguments, and token
        /// </summary>
        /// <param name="outputType">xml, csv, waterml</param>
        /// <param name="args">parameters needed to build a particular REST query</param>
        /// <returns>downloaded data</returns>
        /// <remarks>retries for up to five minutes in case NCDC is preventing frequent access</remarks>
        static private string GetNCDC(OutputTypes outputType, params string[] args)
        {
            string URL = _baseUrl + string.Join(@"/", args) + "?output=" + outputType.ToString() + "&token=" + token;
            DateTime startTime = DateTime.Now;
            int RetryAfterSeconds = 10;
            int attempt = 1;
            string Status = "";
TryAgain:   try
            {
                Status = "";
                System.IO.StreamReader lStream = D4EM.Data.Download.GetHTTPStreamReader(URL, 180);
                MapWinUtility.Logger.Status("Reading NCDC response");
                string gotString = lStream.ReadToEnd();
                //if (gotString.Length > 0)
                    return gotString;
                //Status = "Empty Result from NCDC, waiting " + RetryAfterSeconds + " seconds to retry.";
            }
            catch (WebException ex)
            {
                if (ex.Response != null && int.TryParse(ex.Response.Headers.Get("Retry-After"), out RetryAfterSeconds))
                    Status = "NCDC requested us to wait " + RetryAfterSeconds + " seconds after attempt #" + attempt + ".";
                else
                    Status = "Waiting " + RetryAfterSeconds + " seconds to retry NCDC download after attempt #" + attempt + ".";
            }
            if (DateTime.Now.Subtract(startTime).TotalMinutes < 5)
            {
                MapWinUtility.Logger.Status(Status, true);
                for (int WaitSecond = 0; WaitSecond <= RetryAfterSeconds; WaitSecond++)
                {
                    System.Threading.Thread.Sleep(1000);
                    MapWinUtility.Logger.Progress(WaitSecond + 1, RetryAfterSeconds);
                }
                attempt++;
                MapWinUtility.Logger.Status("Retrying NCDC request, attempt #" + attempt, true);
                goto TryAgain;
            }
            throw new ApplicationException("Unable to download NCDC " + URL);
        }

        /// <summary>
        /// Get all sites in the given layer for the whole world
        /// </summary>
        /// <param name="outputType">xml, csv, waterml</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <returns>downloaded data as string</returns>
        static public string GetAllSites(OutputTypes outputType, LayerSpecification datasetID)
        {   //http://www7.ncdc.noaa.gov/rest/services/sites/ish/?output=csv&token=[TOKEN]
            return GetNCDC(outputType, "sites", datasetID.Tag);
        }

        /// <summary>
        /// Get list of sites in a state
        /// </summary>
        /// <param name="outputType">format of returned list</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <param name="stateAbbrev">2-letter state abbreviation</param>
        /// <returns>List of sites in the given state</returns>
        static public string GetSites(OutputTypes outputType, LayerSpecification datasetID, string stateAbbrev)
        {   //http://www7.ncdc.noaa.gov/rest/services/sites/daily/stateAbbrev/ca/?output=xml&token=[TOKEN]
            return GetNCDC(outputType, "sites", datasetID.Tag,
                                 "stateAbbrev", stateAbbrev.ToUpper());
        }

        /// <summary>
        /// Get list of sites in a state that have data after a specified date
        /// </summary>
        /// <param name="outputType">format of returned list</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <param name="stateAbbrev">2-letter state abbreviation</param>
        /// <param name="start">Only retrieve sites that have data after this date</param>
        /// <returns>List of sites in the given state</returns>
        static public string GetSites(OutputTypes outputType, LayerSpecification datasetID, string stateAbbrev, DateTime start)
        {   //http://www7.ncdc.noaa.gov/rest/services/sites/daily/stateAbbrev/ga/20050101/?output=xml&token=[TOKEN]
            return GetNCDC(outputType, "sites", datasetID.Tag,
                                 "stateAbbrev", stateAbbrev.ToUpper(),
                                                start.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// Get list of sites in a state that have data between specified dates
        /// </summary>
        /// <param name="outputType">format of returned list</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <param name="stateAbbrev">2-letter state abbreviation</param>
        /// <param name="start">Only retrieve sites that have data after this date</param>
        /// <param name="end">Only retrieve sites that have data before this date</param>
        /// <returns>List of sites in the given state</returns>
        static public string GetSites(OutputTypes outputType, LayerSpecification datasetID, string stateAbbrev, DateTime start, DateTime end)
        {   //http://www7.ncdc.noaa.gov/rest/services/sites/daily/stateAbbrev/ga/20050101/?output=xml&token=[TOKEN]
            return GetNCDC(outputType, "sites", datasetID.Tag,
                                 "stateAbbrev", stateAbbrev.ToUpper(),
                                                start.ToString("yyyyMMdd"),
                                                  end.ToString("yyyyMMdd")); 
        }

        static public string GetSiteInfo(OutputTypes outputType, LayerSpecification datasetID, string stationId)
        {   //"http://www7.ncdc.noaa.gov/rest/services/siteinfo/daily/01000899999/?output=waterml&token=[TOKEN]
            return GetNCDC(outputType, "siteinfo", datasetID.Tag, stationId);
        }

        /// <summary>
        /// Get list of variables included in the specified data set
        /// </summary>
        /// <param name="outputType">format of returned list</param>
        /// <param name="datasetID">NCDC coverage to retrieve from (One of the fields of NCDC.LayerSpecifications)</param>
        /// <returns>list of variables in the specified data set</returns>
        static public string GetVariables(OutputTypes outputType, LayerSpecification datasetID)
        {   //"http://www7.ncdc.noaa.gov/rest/services/variables/ish/?output=csv&token=[TOKEN]
            return GetNCDC(outputType, "variables", datasetID.Tag);
        }

        static public string GetValues(OutputTypes outputType, LayerSpecification datasetID, string stationId, string variableId, DateTime start, DateTime end)
        {   //http://www7.ncdc.noaa.gov/rest/services/values/datasetId/stationId/variableId/{startDate}/{endDate}/
            //http://www7.ncdc.noaa.gov/rest/services/values/isd/72584523225/CIG/20080101/20080131/?output=waterml&token=[TOKEN]
            //awsId,wbanId,gmtDate,gmtTime,elemId,elemfld1,elemfld2,elemfld3,elemfld4,elemfld5,elemfld6,elemfld7,elemfld8,elemfld9,elemfld10,elemfld11,elemfld12,elemfld13,dataSrcFlag,rptType
            MapWinUtility.Logger.Status("Requesting " + variableId + " at " + stationId + " from NCDC");
            return GetNCDC(outputType, "values", datasetID.Tag, stationId, variableId, start.ToString("yyyyMMdd"), end.ToString("yyyyMMdd"));
        }
    }
}
