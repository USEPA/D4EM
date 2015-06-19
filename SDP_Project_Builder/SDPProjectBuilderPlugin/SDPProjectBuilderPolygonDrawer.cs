using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Controls.Docking;
using DotSpatial.Symbology;
using DotSpatial.Projections;
using DotSpatial.Data;
using DotSpatial.Controls.Extensions;
using DotSpatial.Topology;

namespace SDPProjectBuilderPlugin
{
    public class SDPProjectBuilderPolygonDrawer : MapFunction
    {

        private IFeatureSet _featureSet;
        private List<Coordinate> _coordinates;
        private System.Drawing.Point _mousePosition;
        private bool _standBy;
        private IMapLineLayer _tempLayer;

        public SDPProjectBuilderPolygonDrawer(IMap map) : base(map)
        {
            map.FunctionMode = FunctionMode.None;
        }

        public List<Coordinate> Coordinates
        {
            get
            {
                return _coordinates;
            }
        }
        /// <summary>
        /// Forces this function to begin collecting points for building a new shape.
        /// </summary>
        protected override void OnActivate()
        {
            _coordinates = new List<Coordinate>();
            if (_tempLayer != null)
            {
                Map.MapFrame.DrawingLayers.Remove(_tempLayer);
                Map.MapFrame.Invalidate();
                Map.Invalidate();
                _tempLayer = null;
            }
            _standBy = false;
            base.OnActivate();
        }
          /// <summary>
        /// Handles drawing of editing features
        /// </summary>
        /// <param name="e">The drawing args</param>
        protected override void OnDraw(MapDrawArgs e)
        {
            if (_standBy) return;
            if (_featureSet.FeatureType == FeatureType.Point) return;
            Pen bluePen = new Pen(Color.Blue, 2F);
            Pen redPen = new Pen(Color.Red, 3F);
            Brush redBrush = new SolidBrush(Color.Red);
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_coordinates != null)
            {
                foreach (Coordinate coord in _coordinates)
                {
                    points.Add(Map.ProjToPixel(coord));
                    Console.WriteLine(this.Name+ "wants to draw: " + coord.ToString());
                }
                if (points.Count > 1)
                {
                    if (_featureSet.FeatureType != FeatureType.MultiPoint)
                    {
                        e.Graphics.DrawLines(bluePen, points.ToArray());
                    }
                    foreach (System.Drawing.Point pt in points)
                    {
                        e.Graphics.FillRectangle(redBrush, new Rectangle(pt.X - 2, pt.Y - 2, 4, 4));
                        Console.WriteLine(this.Name + " drawing rect: " + new Rectangle(pt.X - 2, pt.Y - 2, 4, 4).ToString());
                    }
                }
                if (points.Count > 0 && _standBy == false)
                {
                    if (_featureSet.FeatureType != FeatureType.MultiPoint)
                    {
                        e.Graphics.DrawLine(redPen, points[points.Count - 1], _mousePosition);
                    }
                }
            }
            bluePen.Dispose();
            redPen.Dispose();
            redBrush.Dispose();
            base.OnDraw(e);
        }
        /// <summary>
        /// updates the auto-filling X and Y coordinates
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(GeoMouseArgs e)
        {
            if (_standBy) return;
            if (_coordinates != null && _coordinates.Count > 0)
            {
                List<System.Drawing.Point> points = new List<System.Drawing.Point>();
                foreach (Coordinate coord in _coordinates)
                {
                    points.Add(Map.ProjToPixel(coord));
                }
                Rectangle oldRect = SymbologyGlobal.GetRectangle(_mousePosition, points[points.Count - 1]);
                Rectangle newRect = SymbologyGlobal.GetRectangle(e.Location, points[points.Count - 1]);
                Rectangle invalid = Rectangle.Union(newRect, oldRect);
                invalid.Inflate(20, 20);
                Map.Invalidate(invalid);

           
            }
            _mousePosition = e.Location;
            base.OnMouseMove(e);
        }

        
        /// <summary>
        /// Handles the Mouse-Up situation
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(GeoMouseArgs e)
        {
            if (_standBy) return;
            if (_featureSet == null) return;
            // Add the current point to the featureset
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //saveDrawing();
            }
            else
            {
                if (_coordinates == null) _coordinates = new List<Coordinate>();
                _coordinates.Add(e.GeographicLocation);
                Console.WriteLine(this.Name + " adding coord: " + e.GeographicLocation);
                if (_coordinates.Count > 1)
                {
                    System.Drawing.Point p1 = Map.ProjToPixel(_coordinates[_coordinates.Count - 1]);
                    System.Drawing.Point p2 = Map.ProjToPixel(_coordinates[_coordinates.Count - 2]);
                    Rectangle invalid = SymbologyGlobal.GetRectangle(p1, p2);
                    invalid.Inflate(20, 20);
                    Map.Invalidate(invalid);
                }
               
            }
            
            base.OnMouseUp(e);
        }

        public bool saveDrawing()
        {
            if (_coordinates.Count > 2)
            {
                //clear out the old features:
                _featureSet.Features.Clear();
                Feature f = null;
                if (_featureSet.FeatureType == FeatureType.Polygon)
                {
                    Polygon pg = new Polygon(_coordinates);
                    f = new Feature(pg);
                }
                _featureSet.Features.Add(f);
                _featureSet.InvalidateVertices();
                _coordinates = new List<Coordinate>();
                return true;
            }
            else
            {
                MessageBox.Show("Drawn polygons must have at least 3 points");
                return false;
            }
        }

        /// <summary>
        /// Occurs when this function is removed.
        /// </summary>
        protected override void OnUnload()
        {
            if (this.Enabled)
            {
                _coordinates = null;
            }
            if (_tempLayer != null)
            {
                //Map.MapFrame.DrawingLayers.Remove(_tempLayer);
                Map.MapFrame.Invalidate();
                
                _tempLayer = null;
            }
            Map.Invalidate();
        }

        
        #region Properties

        /// <summary>
        /// Gets or sets the featureset to modify
        /// </summary>
        public IFeatureSet FeatureSet
        {
            get { return _featureSet; }
            set { _featureSet = value; }
        }

        
    

        #endregion















    }
}
