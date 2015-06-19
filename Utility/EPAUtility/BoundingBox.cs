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


namespace EPAUtility
{
    public class BoundingBox
    {
        private double _north;
        private double _south;
        private double _east;
        private double _west;

        private double _center_long;
        private double _center_lat;
        private double _radius;

        List<string> huc8nums = new List<string>();
        string huc8 = "";

        public BoundingBox(List<IFeature> HUCFeatures, ProjectionInfo source, ProjectionInfo dest)
        {
            List<double> x_Max = new List<double>();
            List<double> y_Max = new List<double>();
            List<double> x_Min = new List<double>();
            List<double> y_Min = new List<double>();

            huc8nums.Clear();

            int i = 0;

            foreach (IFeature feature in HUCFeatures)
            {
                IFeature HUCFeature = HUCFeatures[i];
                huc8 = HUCFeature.DataRow[0].ToString();
                if (huc8.Length < 8)
                {
                    huc8 = "0" + huc8;
                }
                huc8nums.Add(huc8);

                IList<DotSpatial.Topology.Coordinate> coords = HUCFeature.BasicGeometry.Coordinates;
                List<double> x_coords = new List<double>();
                List<double> y_coords = new List<double>();

                double max_x = HUCFeature.BasicGeometry.Envelope.Maximum.X;
                double max_y = HUCFeature.BasicGeometry.Envelope.Maximum.Y;
                double min_x = HUCFeature.BasicGeometry.Envelope.Minimum.X;
                double min_y = HUCFeature.BasicGeometry.Envelope.Minimum.Y;

                x_Max.Add(max_x);
                y_Max.Add(max_y);
                x_Min.Add(min_x);
                y_Min.Add(min_y);
                i++;
            }
            double Max_X = x_Max.Max();
            double Max_Y = y_Max.Max();
            double Min_X = x_Min.Min();
            double Min_Y = y_Min.Min();

            double[] vertices = new double[8];
            var xy = new List<double>();
            var z = new List<double>();

            xy.Add(Min_X);
            xy.Add(Max_Y);
            xy.Add(Max_X);
            xy.Add(Max_Y);
            xy.Add(Min_X);
            xy.Add(Min_Y);
            xy.Add(Max_X);
            xy.Add(Min_Y);

            double[] verticesz = new double[4];

            z.Add(0.0);
            z.Add(0.0);
            z.Add(0.0);
            z.Add(0.0);
            var xyA = xy.ToArray();
            var zA = z.ToArray();

            DotSpatial.Projections.Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);

            _north = xyA[1] + .05;
            _south = xyA[5] - .05;
            _west = xyA[0] - .05;
            _east = xyA[2] + .05;

            _center_lat = (_north + _south) / 2;
            _center_long = (_east + _west) / 2;

            _radius = Math.Sqrt((_north - _center_lat) * (_north - _center_lat) + (_west - _center_long) * (_west - _center_long));
        }

        public double North
        {
            get { return _north; }
            set { _north = value; }
        }

        public double South
        {
            get { return _south; }
            set { _south = value; }
        }

        public double East
        {
            get { return _east; }
            set { _east = value; }
        }

        public double West
        {
            get { return _west; }
            set { _west = value; }
        }
        public double CenterLongitude
        {
            get { return _center_long; }
            set { _center_long = value; }
        }
        public double CenterLatitude
        {
            get { return _center_lat; }
            set { _center_lat = value; }
        }
        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

    }
}
