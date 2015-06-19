using System;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls.Docking;
using DotSpatial.Projections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DotSpatial.Analysis;
using System.Data;

namespace EPAUtility
{
    public class StationsWithinHUC
    {
        private FeatureSet pointCoords = null;
        private IFeature polygon = null;
        private IFeatureSet poly = null;

        //use this if stations are listed in a text file

        public StationsWithinHUC(string coordsfilename, IFeature hucFeature, ProjectionInfo proj, bool reproject)
        {
            IFeature polygon = null;       
            IFeatureSet poly = null;
            polygon = hucFeature;
            poly = new FeatureSet();            
            poly.AddFeature(polygon);
            if (reproject == true)
            {
                poly.Projection = proj;
                poly.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            }

            pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            
            DataColumn column = new DataColumn("ID");
            DataColumn column2 = new DataColumn("StationID");
            pointCoords.DataTable.Columns.Add(column);
            pointCoords.DataTable.Columns.Add(column2);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;

            int pointID = 1;
            string coordsFile = coordsfilename;
            string line;
            if (File.Exists(coordsFile) == true)
            {
                TextReader readCoords = new StreamReader(coordsFile);
                while ((line = readCoords.ReadLine()) != null)
                {
                    string[] latlong = line.Split(' ');
                    string stationID = latlong[0];
                    double latitude = Convert.ToDouble(latlong[1]);
                    double longitude = Convert.ToDouble(latlong[2]);

                    DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(longitude, latitude);
                    DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
                    
                    if (poly.GetFeature(0).Intersects(coords) == true)
                    {
                        IFeature currentFeature = pointCoords.AddFeature(point);
                        
                        currentFeature.DataRow["ID"] = pointID;
                        currentFeature.DataRow["StationID"] = stationID;
                        pointID = pointID + 1;
                    }
                }
            }
        }

        //use this is if stations are in a shapefile

        public StationsWithinHUC(IFeature hucFeature, string shapefileName, ProjectionInfo proj, bool reproject)
        {
            pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            polygon = hucFeature;
            poly = new FeatureSet();
            poly.AddFeature(polygon);

            if (reproject == true)
            {
                poly.Projection = proj;
                poly.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            }

            IFeatureSet fs = FeatureSet.OpenFile(shapefileName);
            fs.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);

            int counter = fs.DataTable.Columns.Count;
            string[] attributes = new string[counter];
            int i = 0;
            foreach (DataColumn dc in fs.DataTable.Columns)
            {
                attributes[i] = fs.DataTable.Columns[i].ColumnName;
                pointCoords.DataTable.Columns.Add(attributes[i]);
                i++;
            }

            foreach (Feature feature in fs.Features)
            {
               
                IList<DotSpatial.Topology.Coordinate> coords = feature.Coordinates;
                foreach (DotSpatial.Topology.Coordinate coord in coords)
                {
                    DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coord);
                    if (poly.GetFeature(0).Intersects(coord) == true)
                    {
                        IFeature currentFeature = pointCoords.AddFeature(point);

                        int j = 0;
                        foreach (string attribute in attributes)
                        {
                            currentFeature.DataRow[attributes[j]] = feature.DataRow[j].ToString();
                            j++;
                        }
                        
                    }
                }
            }
            pointCoords.Reproject(proj);
        }

        public FeatureSet Stations
        {
           get { return pointCoords; }
           set { pointCoords = null; }
        }

    }
}
