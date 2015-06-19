using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;
using DotSpatial.Projections;
using System.Data;

namespace EPAUtility
{
    public class PointShapeFileToFeatureSet
    {
        public static FeatureSet pointCoords = null;

        public PointShapeFileToFeatureSet(string shapefileName, ProjectionInfo proj)
        {
            pointCoords = new FeatureSet(DotSpatial.Topology.FeatureType.Point);
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;            

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
                    IFeature currentFeature = pointCoords.AddFeature(point);
                    
                    int j = 0;
                    foreach (string attribute in attributes)
                    {
                        currentFeature.DataRow[attributes[j]] = feature.DataRow[j].ToString();
                        j++;
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
