using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;

using D4EM.Data;
using Newtonsoft.Json.Linq;

namespace D4EM.Data.Source
{
    public class EPAWaters
    {
        /// <summary>Details of a pour point</summary>
        public class PourPoint
        {
            /// <summary>NHDPlus COMID of the catchment (and flowline) whose downstream end is this pour point</summary>
            public long COMID = 0;
            /// <summary>Percent of distance from downstream to upstream end of reach where water from selected point flows into stream</summary>
            public double Measure = double.NaN;
            /// <summary>Latitude of point where water from selected point flows into stream</summary>
            public double Latitude = double.NaN;
            /// <summary>Longitude of point where water from selected point flows into stream</summary>
            public double Longitude = double.NaN;
        }

        public class LayerSpecifications
        {
            public static LayerSpecification PourpointWatershed = new LayerSpecification(Name: "Pourpoint Watershed", FilePattern: "watershed.shp", Tag: "watershed", Role: LayerSpecification.Roles.SubBasin, Source: typeof(EPAWaters));
            //Commented out the version that allows these layers to replace NHDPlus because they do not currently have all the required fields
            //public static LayerSpecification Catchment = new LayerSpecification(Name: "NHDPlus Catchment Polygons", FilePattern: "catchment.shp", Tag: "catchment", Role: LayerSpecification.Roles.SubBasin, IdFieldName: "COMID", Source: typeof(EPAWaters));
            //public static LayerSpecification Flowline = new LayerSpecification(Name: "NHDPlus Flowline", FilePattern: "nhdflowline.shp", Tag: "nhdflowline", Role: LayerSpecification.Roles.Hydrography, IdFieldName: "COMID", Source: typeof(EPAWaters));
            public static LayerSpecification Catchment = new LayerSpecification(Name: "EPAWaters Catchment Polygons", FilePattern: "catchment.shp", Tag: "EPAWaterscatchment", Role: LayerSpecification.Roles.SubBasin, IdFieldName: "COMID", Source: typeof(EPAWaters));
            public static LayerSpecification Flowline = new LayerSpecification(Name: "EPAWaters Flowline", FilePattern: "nhdflowline.shp", Tag: "EPAWatersnhdflowline", Role: LayerSpecification.Roles.Hydrography, IdFieldName: "COMID", Source: typeof(EPAWaters));
        }

        # region Private Constructor
        /// <summary>
        /// Private constructor disables creating this as an object. Everything is static, so no need to create one
        /// </summary>
        private EPAWaters()
        {
        }
        #endregion

        # region URL

        private const string _PointIndexingUrl = "http://ofmpub.epa.gov/waters10/WATERS_SERVICES.PointIndexingService";
        //                                       "http://iaspub.epa.gov/WATERSWebServices/OWServices";

        private const string _DelineationUrl = "http://ofmpub.epa.gov/waters10/waters_services.navigationDelineationService";
        //                                     "http://iaspub.epa.gov/waters10/waters_services.navigationDelineationService";
        
        private const string _StreamlineUrl = "http://ofmpub.epa.gov/waters10/waters_services.upstreamDownStreamService";
        //                                    "http://iaspub.epa.gov/waters10/waters_services.upstreamDownStreamService";

        static private ProjectionInfo _defaultProjection = KnownCoordinateSystems.Geographic.World.WGS1984; 
        //                                                 KnownCoordinateSystems.Projected.World.WebMercator;


        /// <summary>
        /// Gets the point query URL for a specified location
        /// </summary>
        /// <returns>the uri</returns>
        static private string GetPourPointURL(double Latitude, double Longitude)
        {
            // The Max Distance is set as 100km. Non-limited distance could cause timeout.
            //return _PointIndexingUrl + "&pGeometry=POINT("
            //                         + Longitude.ToString(CultureInfo.InvariantCulture) + "+"
            //                         + Latitude.ToString(CultureInfo.InvariantCulture) + ")"
            //                         + "&pGeometrySrid=8265&pReachresolution=3&pPointIndexingMethod=Distance"
            //                         + "&pPointIndexingFcodeAllow=&pPointIndexingFcodeDeny=&pPointIndexingMaxDist=100"
            //                         + "&pPointIndexingRaindropDist=100&pOutputPathFlag=&pTolerance=5";

            //pOutputPathFlag=TRUE&pReturnFlowlineGeomFlag=FULL&optCache=1344610557567&optJSONPCallback=success
            // The Max Distance is set as 100km. Non-limited distance could cause timeout.
            return _PointIndexingUrl + "?pGeometry=POINT("
                                     + Longitude.ToString(CultureInfo.InvariantCulture) + "+"
                                     + Latitude.ToString(CultureInfo.InvariantCulture) + ")"
                                     + "&pGeometryMod=WKT,SRID=8265&pResolution=3&pPointIndexingMethod=DISTANCE"
                                     + "&pPointIndexingMaxDist=100";
                                     
        }

        /// <summary>
        /// Get the query string for the given EPA Waters layer
        /// </summary>
        /// <param name="COMID">NHD COMID of starting catchment</param>
        /// <param name="qmeasure">Measure from the PointIndexing Service</param>
        /// <param name="MaxDistanceKm">Maximum distance to follow upstream (km). Suggested value is 50.</param>
        /// <param name="WhichLayer">One of the items in EPAWater.LayerSpecifications: PourpointWatershed, Catchment, or Flowline</param>
        /// <returns>Returns the query url</returns>
        static private string GetLayerURL(long COMID, int MaxDistanceKm, LayerSpecification WhichLayer)
        {
            if (WhichLayer.Tag == LayerSpecifications.PourpointWatershed.Tag)
                return _DelineationUrl + "?pNavigationType=UT&pStartComid=" + COMID.ToString()
                                       + "&pStartMeasure=&pMaxDistance=" + MaxDistanceKm.ToString()
                                       + "&pMaxTime=&pAggregationFlag=true&pOutputFlag=FEATURE&pFeatureType=CATCHMENT_TOPO"
                                       + "&optCache=1269303461090&optOutGeomFormat=GEOJSON&optJSONPCallback=success";
            else if (WhichLayer.Tag == LayerSpecifications.Catchment.Tag)
                return _DelineationUrl + "?pNavigationType=UT&pStartComid=" + COMID.ToString()
                                       + "&pStartMeasure=&pMaxDistance=" + MaxDistanceKm.ToString()
                                       + "&pMaxTime=&pAggregationFlag=false&pOutputFlag=BOTH&pFeatureType=CATCHMENT"
                                       + "&optCache=1269303461090&optOutGeomFormat=GEOJSON&optJSONPCallback=success";
            else if (WhichLayer.Tag == LayerSpecifications.Flowline.Tag)
                return _StreamlineUrl + "?pNavigationType=UT&pStartComid=" + COMID.ToString()
                                      + "&pStartMeasure=&pStopDistancekm=" + MaxDistanceKm.ToString()
                                      + "&pStopTimeOfTravel=&pFlowlinelist=true&pTraversalSummary=true"
                                      + "&optCache=1269303461090&optOutGeomFormat=GEOJSON&optJSONPCallback=success";
            else
                throw new ApplicationException("Cannot build URL for layer " + WhichLayer.Name);
        }

        #endregion

        #region Get Data

        /// <summary>
        /// Get the NHDPlus catchment pour point of the specified coordinates
        /// </summary>
        /// <param name="Latitude">Latitude of the specified coordinate</param>
        /// <param name="Longitude">Longitude of the specified coordinate</param>
        /// <returns>EPAWaters.PourPoint object with all fields populated.</returns>
        /// <remarks>Latitude and Longitude of the returned object are where water from the selected point enters the NHDPlus flow line.
        /// The pour point of the whole area is the downstream end of this NHDPlus flow line.</remarks>
        static public EPAWaters.PourPoint GetPourPoint(string CacheFolder, double Latitude, double Longitude)
        {
            string JSON = GetCachedOrDownload(CacheFolder,
                                              "PourPoint" + Latitude.ToString("#.###", CultureInfo.InvariantCulture) + ","
                                                          + Longitude.ToString("#.###", CultureInfo.InvariantCulture) + ".JSON",
                                              GetPourPointURL(Latitude, Longitude));
            return ParsePourPoint(JSON);
        }

        /// <summary>
        /// Get the specified layer and return a featureset
        /// </summary>
        /// <param name="aProject">Optional D4EM Project containing project folder, cache, and projection information. If null, layer will not be cached or reprojected</param>
        /// <param name="SaveFolder">Sub-folder within project folder or full path of folder to save in (e.g. "EPAWaters" or "C:\data\EPAWaters"). 
        /// If null or empty, layer will be saved in aProject.ProjectFolder. If both are null or empty, layer will not be saved to a file.
        /// <param name="COMID">NHD COMID of furthest downstream catchment/reach to include</param>
        /// <param name="MaxDistanceKm">Maximum distance to follow upstream (kilometers), 100 is a suggested value</param>
        /// <param name="WhichLayer">One of the EPAWaters.LayerSpecifications to say which layer to get</param>
        /// <returns>specified layer as D4EM Layer</returns>
        /// <remarks>
        /// To retrieve based on a geographic point, use GetPourPoint(latitude, longitude).COMID as COMID parameter.
        /// Layer.AsFeatureSet() returns the underlying DotSpatial FeatureSet.
        /// </remarks>
        static public D4EM.Data.Layer GetLayer(D4EM.Data.Project aProject, string SaveFolder, long COMID, int MaxDistanceKm, LayerSpecification WhichLayer)
        {
            string lCacheFolder = null;

            if (aProject != null)
            {
                if (!string.IsNullOrEmpty(aProject.CacheFolder))
                   lCacheFolder = aProject.CacheFolder;
            }
            string JSON = GetCachedOrDownload(lCacheFolder,
                                              Path.ChangeExtension(WhichLayer.FilePattern, COMID + "," + MaxDistanceKm + "km.JSON"),
                                              GetLayerURL(COMID, MaxDistanceKm, WhichLayer));
            IFeatureSet fs;
            if (WhichLayer.Tag == LayerSpecifications.PourpointWatershed.Tag)
                fs = ParseWatershedBoundary(JSON);
            else if (WhichLayer.Tag == LayerSpecifications.Catchment.Tag)
                fs = ParseCatchment(JSON);
            else if (WhichLayer.Tag == LayerSpecifications.Flowline.Tag)
                fs = ParseFlowline(JSON);
            else
                throw new ApplicationException("Cannot build layer " + WhichLayer.Name);

            return MakeLayer(aProject, SaveFolder, fs, WhichLayer);
        }

        /// <summary>
        /// Get the specified layer and return a featureset
        /// </summary>
        /// <param name="aProject">Optional D4EM Project containing project folder, cache, and projection information. If null, layer will not be cached or reprojected</param>
        /// <param name="SaveFolder">Sub-folder within project folder or full path of folder to save in (e.g. "EPAWaters" or "C:\data\EPAWaters"). 
        /// If null or empty, layer will be saved in aProject.ProjectFolder. If both are null or empty, layer will not be saved to a file.
        /// <param name="COMID">NHD COMID of furthest downstream catchment/reach to include</param>
        /// <param name="MaxDistanceKm">Maximum distance to follow upstream (kilometers), 100 is a suggested value</param>
        /// <param name="WhichLayer">One of the EPAWaters.LayerSpecifications to say which layer to get</param>
        /// <returns>specified layer as D4EM Layer</returns>
        /// <remarks>
        /// To retrieve based on a geographic point, use GetPourPoint(latitude, longitude).COMID as COMID parameter.
        /// Layer.AsFeatureSet() returns the underlying DotSpatial FeatureSet.
        /// </remarks>
        static public D4EM.Data.Layer GetLayer(string CacheFolder, string SaveFolder, ProjectionInfo desiredProjection,
                        long COMID, int MaxDistanceKm, LayerSpecification WhichLayer)
        {
            string JSON = GetCachedOrDownload(CacheFolder,
                                              Path.ChangeExtension(WhichLayer.FilePattern, COMID + "," + MaxDistanceKm + "km.JSON"),
                                              GetLayerURL(COMID, MaxDistanceKm, WhichLayer));
            IFeatureSet fs;
            if (WhichLayer.Tag == LayerSpecifications.PourpointWatershed.Tag)
                fs = ParseWatershedBoundary(JSON);
            else if (WhichLayer.Tag == LayerSpecifications.Catchment.Tag)
                fs = ParseCatchment(JSON);
            else if (WhichLayer.Tag == LayerSpecifications.Flowline.Tag)
                fs = ParseFlowline(JSON);
            else
                throw new ApplicationException("Cannot build layer " + WhichLayer.Name);

            return MakeLayer(desiredProjection, SaveFolder, fs, WhichLayer);
        }

        #endregion

        #region Parse JSON

        /// <summary>
        /// Interpret JSON result of a web service call and populate a PourPoint object
        /// </summary>
        /// <returns>details of the PourPoint including Comid, Measure, Latitude, and Longitude</returns>
        static private PourPoint ParsePourPoint(string JSON)
        {
            try
            {
                PourPoint pt = new PourPoint();

                JObject mainObj = JObject.Parse(JSON);
                JToken outputObj = mainObj["output"];

                //check for error message in outputObj
                if (outputObj.Type == Newtonsoft.Json.Linq.JTokenType.Null)
                {
                    JToken statusObj = mainObj["status"];
                    string statusMessage = statusObj["status_message"].ToString();
                    throw new ArgumentException(statusMessage);
                }

                JToken coord = outputObj["end_point"] as JToken;
                if (coord.Type == Newtonsoft.Json.Linq.JTokenType.Null)
                    throw new Exception("A valid point could not be found.");

                JArray endPt = coord["coordinates"] as JArray;
                pt.Longitude = Convert.ToDouble(endPt[0].ToString(), CultureInfo.InvariantCulture);
                pt.Latitude  = Convert.ToDouble(endPt[1].ToString(), CultureInfo.InvariantCulture); 
                // Get flowline array (should just be one)
                JArray flowlinesObj = outputObj["ary_flowlines"] as JArray;

                // TODO: catch when array is empty

                JToken flowline = flowlinesObj[0];
                string comidStr = flowline["comid"].ToString();
                pt.COMID = Convert.ToInt32(comidStr, CultureInfo.InvariantCulture);
                string measureStr = flowline["fmeasure"].ToString();
                pt.Measure = Convert.ToDouble(measureStr, CultureInfo.InvariantCulture);

                //using (XmlTextReader reader = new XmlTextReader(new StringReader(JSON)))
                //{
                //    char[] semi = { ';' };
                //    while (reader.Read())
                //    {
                //        if (reader.NodeType == XmlNodeType.Element)
                //        {
                //            if (reader.Name == "comid")
                //            {
                //                pt.COMID = Convert.ToInt64(reader.ReadInnerXml(), CultureInfo.InvariantCulture);
                //            }
                //            else if (reader.Name == "fmeasure")
                //            {
                //                pt.Measure = Convert.ToDouble(reader.ReadInnerXml(), CultureInfo.InvariantCulture);
                //            }
                //            else if (reader.Name == "endPoint")
                //            {
                //                string str = reader.ReadInnerXml();
                //                foreach (string strng in str.Split(semi, 15))
                //                {
                //                    if (strng.Contains(",") && !strng.Contains("cs"))
                //                    {
                //                        string[] strngs = strng.Split(',');
                //                        pt.Latitude = Convert.ToDouble(strngs[1].Replace("&lt", "").Trim(), CultureInfo.InvariantCulture);
                //                        pt.Longitude = Convert.ToDouble(strngs[0].Trim(), CultureInfo.InvariantCulture);
                //                        break;
                //                    }
                //                }
                //                break;
                //            }
                //        }
                //    }
                //}
                if (pt.COMID > 0 && pt.Measure > 0)
                {
                    return pt;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error Parsing Pour Point", ex);
            }
            throw new ApplicationException("No point returned. Please select a different point.");
        }

        /// <summary>
        /// Interpret JSON result of a watershed delineation web service call and populate an IFeatureSet
        /// </summary>
        /// <param name="JSON">JSON string containing catchment polygon query results</param>
        /// <returns>Returns an IFeatureSet containing the delineated watershed polygon</returns>
        static private IFeatureSet ParseWatershedBoundary(string JSON)
        {
            try
            {
                int start = JSON.IndexOf("(");
                int end = JSON.IndexOf(")");
                JSON = JSON.Substring(start + 1, end - 1 - start);

                IFeatureSet polyfs = null;

                JObject mainObj = JObject.Parse(JSON);
                var polys = ParseJSONPolyList(mainObj["output"]);
                Polygon watershedPoly = null;
                foreach(Polygon poly in polys)
                    if (watershedPoly == null || poly.NumPoints > watershedPoly.NumPoints)
                        watershedPoly = poly;

                if (watershedPoly != null)
                {
                    IFeature watershedFeature = new Feature(watershedPoly);
                    polyfs = new FeatureSet(watershedFeature.FeatureType);
                    polyfs.Projection = _defaultProjection;
                    polyfs.Name = LayerSpecifications.PourpointWatershed.Name;
                    polyfs.DataTable.Columns.Add(new DataColumn("areasqkm"));
                    watershedFeature = polyfs.AddFeature(watershedFeature);
                    try { watershedFeature.DataRow["areasqkm"] = mainObj["output"]["total_areasqkm"].ToString(); }
                    catch { }                        
                    //TODO: add other attributes we can find in the JSON
                    return polyfs;
                }
                else
                {
                    throw new ApplicationException("Watershed polygon not found. Please try a different point.");
                }                
            }
            catch (NullReferenceException ex1)
            {
                throw new ApplicationException("Watershed not found. Please try a different point.", ex1);
            }
            catch (Exception ex2)
            {
                throw new ApplicationException("Error searching for watershed. " + ex2.Message, ex2);
            }
        }

        /// <summary>
        /// Interpret JSON result of a catchment web service call and populate an IFeatureSet
        /// </summary>
        /// <param name="JSON">JSON string containing catchment polygon query results</param>
        /// <returns>Returns an IFeatureSet containing the catchment polygons</returns>
        static private IFeatureSet ParseCatchment(string JSON)
        {
            try
            {
                int start = JSON.IndexOf("(");
                int end = JSON.IndexOf(")");

                JSON = JSON.Substring(start + 1, end - 1 - start);

                JObject mainObj = JObject.Parse(JSON);
                JToken outputObj = mainObj["output"];
                JToken catchments = outputObj["catchments"];
                IFeatureSet fsCatchments = null;

                foreach (JToken JSONshape in catchments)
                {
                    var currentFeature = ParseJSONPolygon(JSONshape);
                    if (currentFeature != null)
                    {
                        if (fsCatchments == null)
                        {
                            fsCatchments = new FeatureSet(currentFeature.FeatureType);
                            fsCatchments.Projection = _defaultProjection;
                            fsCatchments.Name = LayerSpecifications.Catchment.Name;
                            fsCatchments.DataTable.Columns.Add(new DataColumn("COMID"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("ReachCode"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("AREASQKM"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("FeatureID"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("NHDRegion"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("NHDVersion"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("GRID_CODE"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("GRID_COUNT"));
                            fsCatchments.DataTable.Columns.Add(new DataColumn("PROD_UNIT"));
                        }

                        //"areasqkm": 1.40933574,

                        currentFeature = fsCatchments.AddFeature(currentFeature);
                        try { currentFeature.DataRow["COMID"] = JSONshape["comid"].ToString(); } catch { };
                        try { currentFeature.DataRow["ReachCode"] = JSONshape["reachcode"].ToString(); } catch { };
                        try { currentFeature.DataRow["AREASQKM"] = JSONshape["areasqkm"].ToString(); } catch { };
                        try { currentFeature.DataRow["FeatureID"] = JSONshape["feature_id"].ToString(); } catch { };
                        try { currentFeature.DataRow["NHDRegion"] = JSONshape["nhdplus_region"].ToString(); } catch { };
                        try { currentFeature.DataRow["NHDVersion"] = JSONshape["nhdplus_version"].ToString(); } catch { };
                        try { currentFeature.DataRow["GRID_CODE"] = JSONshape["grid_code"].ToString(); } catch { };
                        try { currentFeature.DataRow["GRID_COUNT"] = JSONshape["grid_count"].ToString(); } catch { };
                        try { currentFeature.DataRow["PROD_UNIT"] = JSONshape["prod_unit"].ToString(); } catch { };
                    }
                }
                return fsCatchments;
            }
            catch (NullReferenceException ex1)
            {
                throw new ApplicationException("Watershed not found. Please try a different point.", ex1);
            }
            catch (Exception ex2)
            {
                throw new ApplicationException("Error searching for watershed.", ex2);
            }
        }

        /// <summary>
        /// Interpret JSON result of a flowline web service call and populate an IFeatureSet
        /// </summary>
        /// <param name="JSON">JSON string containing flowline query results</param>
        /// <returns>Returns an IFeatureSet containing the flow lines</returns>
        static private IFeatureSet ParseFlowline(string JSON)
        {
            try
            {
                int start = JSON.IndexOf("(");
                int end = JSON.IndexOf(")");

                JSON = JSON.Substring(start + 1, end - 1 - start);

                //Declare Json Elements
                JObject mainObj = JObject.Parse(JSON);
                JToken outputObj = mainObj["output"];
                JArray lineObj = outputObj["flowlines_traversed"] as JArray;
                JToken shapeObj;

                //Initialize feature parameters
                IFeatureSet linefs = new FeatureSet(FeatureType.Line);
                linefs.Projection = _defaultProjection;
                linefs.Name = LayerSpecifications.Flowline.Name;
                linefs.DataTable.Columns.Add(new DataColumn("COMID"));
                linefs.DataTable.Columns.Add(new DataColumn("FeatureID"));
                linefs.DataTable.Columns.Add(new DataColumn("ReachCode"));
                linefs.DataTable.Columns.Add(new DataColumn("StartCOMID"));
                //linefs.DataTable.Columns.Add(new DataColumn("FromMeas"));
                //linefs.DataTable.Columns.Add(new DataColumn("ToMeas"));
                linefs.DataTable.Columns.Add(new DataColumn("PATHLENGTH"));
                linefs.DataTable.Columns.Add(new DataColumn("TotalTime"));
                linefs.DataTable.Columns.Add(new DataColumn("HYDROSEQ"));

                //for (int i = 0; i < lineObj.Count; i++)
                foreach (JObject flowObj in lineObj)
                {
                    shapeObj = flowObj["shape"] as JObject;
                    string stype = shapeObj["type"].ToString();
                    JArray coordArray = shapeObj["coordinates"] as JArray;
                    IFeature linef = null;

                    if (stype.Trim().ToLower() == "multilinestring") //For the case GeoJSON returns a MultiLineString 
                    {
                        if (coordArray != null)
                        {
                            LineString[] lines = new LineString[coordArray.Count];
                            for (int j = 0; j < coordArray.Count; j++) //The second level
                            {
                                JArray linecoord = (JArray)coordArray[j];
                                if (linecoord != null)
                                {
                                    lines[j] = new LineString(CoordsFromJArray(linecoord));
                                }
                            }
                            linef = linefs.AddFeature(new Feature(new MultiLineString(lines)));
                        }
                    }
                    else if (stype.Trim().ToLower() == "linestring") //For the case GeoJSON returns a LineString 
                    {
                        linef = linefs.AddFeature(new Feature(FeatureType.Line, CoordsFromJArray(coordArray)));
                    }

                    if (linef != null)
                    {
                        linef.DataRow["COMID"] = flowObj["comid"].ToString();
                        linef.DataRow["FeatureID"] = flowObj["feature_id"].ToString();
                        linef.DataRow["ReachCode"] = flowObj["reachcode"].ToString();
                        linef.DataRow["PATHLENGTH"] = flowObj["totaldist"].ToString();
                        linef.DataRow["StartCOMID"] = flowObj["startcomid"].ToString();
                        //linef.DataRow["FromMeas"] = flowObj["frommeas"].ToString();
                        //linef.DataRow["ToMeas"] = flowObj["tomeas"].ToString();
                        linef.DataRow["TotalTime"] = flowObj["totaltime"].ToString();
                        linef.DataRow["HYDROSEQ"] = flowObj["hydroseq"].ToString();
                    }
                }
                return linefs;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception getting streams", ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// If CacheFolder is null or empty, simply return the results of downloading URL.
        /// If CacheFolder is not null or empty, caching is enabled:
        ///    If CacheFilename exists in EPAWaters folder within CacheFolder, its contents are returned.
        ///    If file does not yet exist, URL is downloaded to the cache, and contents are returned.
        /// </summary>
        /// <param name="CacheFolder">Top cache folder. "EPAWaters" folder within this folder will be used.</param>
        /// <param name="CacheFilename">Unique file name corresponding with URL to download</param>
        /// <param name="URL">Web address that produces the desired data</param>
        /// <returns></returns>
        static private string GetCachedOrDownload(string CacheFolder, string CacheFilename, string URL)
        {
            if (string.IsNullOrEmpty(CacheFolder))
                return Download.DownloadURL(URL);

            CacheFilename = Path.Combine(CacheFolder, "EPAWaters", CacheFilename);
            if (!File.Exists(CacheFilename))
            {
                Download.DownloadURL(URL, CacheFilename);
            }
            return File.ReadAllText(CacheFilename);
        }

        /// <summary>
        /// Make a D4EM.Data.Layer from the given IFeatureSet and LayerSpec.
        /// If the given Project is not null, add the Layer to the Project
        /// If SaveFolder and/or Project.ProjectFolder are not null, save the layer in the right folder.
        /// If Project.DesiredProjection is set, make sure layer is projected correctly.
        /// </summary>
        static private D4EM.Data.Layer MakeLayer(Project aProject, string SaveFolder, IFeatureSet fs, LayerSpecification LayerSpec)
        {
            string lSaveIn = "";
            fs.Filename = LayerSpec.FilePattern;

            D4EM.Data.Layer newLayer = new D4EM.Data.Layer(fs, LayerSpec);
            if ((aProject != null) && !string.IsNullOrEmpty(aProject.ProjectFolder))
                lSaveIn = aProject.ProjectFolder;

            if (!string.IsNullOrEmpty(SaveFolder))
                lSaveIn = Path.Combine(lSaveIn, SaveFolder);

            if (!string.IsNullOrEmpty(lSaveIn))
            {
                Directory.CreateDirectory(lSaveIn);
                newLayer.FileName = Path.Combine(lSaveIn, fs.Filename);
                fs.Filename = newLayer.FileName;
                fs.Save();
                if (aProject != null && aProject.DesiredProjection != null)
                    newLayer.Reproject(aProject.DesiredProjection);
            }

            //Adding layer with features already populated should work, but newly created shapefiles
            // have some trouble so we add by filename and let it get read fresh from disk later
            fs.Close();
            fs.Dispose();
            newLayer = new D4EM.Data.Layer(newLayer.FileName, LayerSpec, false);

            if (aProject != null)
                aProject.Layers.Add(newLayer);

            return newLayer;
        }

        /// <summary>
        /// Make a D4EM.Data.Layer from the given IFeatureSet and LayerSpec.
        /// If the given Project is not null, add the Layer to the Project
        /// If SaveFolder and/or Project.ProjectFolder are not null, save the layer in the right folder.
        /// If Project.DesiredProjection is set, make sure layer is projected correctly.
        /// </summary>
        static private D4EM.Data.Layer MakeLayer(DotSpatial.Projections.ProjectionInfo desiredProjection,
            string SaveFolder, IFeatureSet fs, LayerSpecification LayerSpec)
        {
            string lSaveIn = SaveFolder;
            fs.Filename = LayerSpec.FilePattern;

            D4EM.Data.Layer newLayer = new D4EM.Data.Layer(fs, LayerSpec);

            if (!string.IsNullOrEmpty(lSaveIn))
            {
                Directory.CreateDirectory(lSaveIn);
                newLayer.FileName = Path.Combine(lSaveIn, fs.Filename);
                fs.Filename = newLayer.FileName;
                fs.Save();
                if (desiredProjection != null)
                    newLayer.Reproject(desiredProjection);
            }

            //Adding layer with features already populated should work, but newly created shapefiles
            // have some trouble so we add by filename and let it get read fresh from disk later
            fs.Close();
            fs.Dispose();
            newLayer = new D4EM.Data.Layer(newLayer.FileName, LayerSpec, false);

            return newLayer;
        }

        

        private static IList<Coordinate> CoordsFromJArray(JArray JCoords)
        {
            IList<Coordinate> coords = new List<Coordinate>();
            foreach (JArray latlongcoord in JCoords)
            {
                coords.Add(new Coordinate(Convert.ToDouble(latlongcoord[0].ToString(), CultureInfo.InvariantCulture),
                                          Convert.ToDouble(latlongcoord[1].ToString(), CultureInfo.InvariantCulture)));
            }
            return coords;
        }

        private static IFeature ParseJSONPolygon(JToken JSONshape)
        {
            JToken shapeObj = JSONshape["shape"];
            string stype = shapeObj["type"].ToString();
            var polys = ParseJSONPolyList(JSONshape);

            if (stype.Trim().ToLower() == "polygon" && (polys.Count == 1)) //For the case GeoJSON returns a Polygon 
                return new Feature(polys[0]);

            if (polys.Count > 0)
                return new Feature(new MultiPolygon(polys.ToArray()));

            return null;
        }

        private static System.Collections.Generic.List<Polygon> ParseJSONPolyList(JToken JSONshape)
        {
            JToken shapeObj = JSONshape["shape"];
            string stype = shapeObj["type"].ToString();
            JArray coordArray = shapeObj["coordinates"] as JArray;
            var polys = new System.Collections.Generic.List<Polygon>();

            if (stype.Trim().ToLower() == "multipolygon") //For the case GeoJSON returns a MultiPolygon 
            {
                foreach (JArray Polycoord in coordArray) //The third level
                    if (Polycoord != null)
                        foreach (JArray multiRingcoord in Polycoord)
                            if (multiRingcoord != null)
                                polys.Add(new Polygon(CoordsFromJArray(multiRingcoord)));
            }
            else if (stype.Trim().ToLower() == "polygon") //For the case GeoJSON returns a Polygon 
            {
                foreach (JArray Ringcoord in coordArray)  //The second level
                    if (Ringcoord != null)
                        polys.Add(new Polygon(CoordsFromJArray(Ringcoord)));
            }
            return polys;
        }

        #endregion    
    }
}
