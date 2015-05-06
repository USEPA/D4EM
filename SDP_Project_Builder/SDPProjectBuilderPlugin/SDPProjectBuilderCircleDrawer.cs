using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class SDPProjectBuilderCircleDrawer : MapFunction
    {
        private IFeatureSet _featureSet;
        private List<Coordinate> _coordinates;
        private System.Drawing.Point _mousePosition;
        private bool _standBy;
        private IMapLineLayer _tempLayer;
        private int _radius = 50;
        private int circleRad = 50;


        public SDPProjectBuilderCircleDrawer(IMap map)
            : base(map)
        {

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

            Pen bluePen = new Pen(Color.Blue, 2F);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(bluePen, _mousePosition.X - (circleRad / 2), _mousePosition.Y - (circleRad / 2), circleRad, circleRad);
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            Pen redBrush = new Pen(Color.Red);
            foreach (Coordinate coord in _coordinates)
            {
                points.Add(Map.ProjToPixel(coord));

            }
            foreach (System.Drawing.Point pt in points)
            {
                e.Graphics.DrawEllipse(redBrush, new Rectangle(pt.X - (circleRad / 2), pt.Y - (circleRad / 2), circleRad, circleRad));
            }
            bluePen.Dispose();
            base.OnDraw(e);
        }
        /// <summary>
        /// Allows for new behavior during deactivation.
        /// </summary>
        protected override void OnDeactivate()
        {
            if (_standBy == true) return;
            // Don't completely deactivate, but rather go into standby mode
            // where we draw only the content that we have actually locked in.
            _standBy = true;

            if (_coordinates != null && _coordinates.Count > 1)
            {
                LineString ls = new LineString(_coordinates);
                FeatureSet fs = new FeatureSet(FeatureType.Line);
                fs.Features.Add(new Feature(ls));
                MapLineLayer gll = new MapLineLayer(fs);

                gll.Symbolizer.ScaleMode = ScaleMode.Symbolic;
                gll.Symbolizer.Smoothing = true;
                gll.MapFrame = Map.MapFrame;

                _tempLayer = gll;
                Map.MapFrame.DrawingLayers.Add(gll);

                Map.MapFrame.Invalidate();
                Map.Invalidate();

            }

        }
        /*
        /// <summary>
        /// Handles drawing of editing features
        /// </summary>
        /// <param name="e">The drawing args</param>
        protected override void OnDraw(MapDrawArgs e)
        {


            if (_standBy) return;
            Brush redBrush = new SolidBrush(Color.Red);
            Pen bluePen = new Pen(Color.Blue, 2F);
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            float x = _mousePosition.X;
            float y = _mousePosition.Y;
            e.Graphics.DrawEllipse(bluePen, x - (100 / 2), y - (100 / 2), 100, 100);
            foreach (Coordinate coord in _coordinates)
            {
              points.Add(Map.ProjToPixel(coord));

            }

            foreach (System.Drawing.Point pt in points)
            {
                e.Graphics.FillEllipse(redBrush, new Rectangle(pt.X - (100/2) , pt.Y - (100/2), 100, 100));
            }
        
            
            bluePen.Dispose();
            base.OnDraw(e);
 
            
              
        
        }*/
        /// <summary>

        /// updates the auto-filling X and Y coordinates
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(GeoMouseArgs e)
        {
            if (_standBy) return;
            //System.Drawing.Point pt = new System.Drawing.Point(_mousePosition.X - _radius, _mousePosition.Y - _radius);
            //Rectangle invalid = Global.GetRectangle(pt,_mousePosition);
            //invalid.Inflate(_radius, _radius);

            //Map.Invalidate(invalid);
            Map.Invalidate();
            recalcRadius(e);
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

            if (_coordinates == null) _coordinates = new List<Coordinate>();
            _coordinates.Clear();
            _coordinates.Add(e.GeographicLocation);

            base.OnMouseUp(e);
        }

        public bool saveDrawing()
        {
            Feature f = null;
            if (_featureSet.FeatureType == FeatureType.Polygon)
            {
                IGeometry g = GeometryFactory.Default.CreatePoint(_coordinates[0]);
                double dRadius = Convert.ToDouble(_radius);
                g = g.Buffer(dRadius);
                f = new Feature(g);
            }
            _featureSet.Features.Add(f);
            _featureSet.InvalidateVertices();
            _coordinates = new List<Coordinate>();
            return true;
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
                Map.MapFrame.DrawingLayers.Remove(_tempLayer);
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

        /// <summary>
        /// Gets or sets the featureset to modify
        /// </summary>
        public String Radius
        {
            //get { return _featureSet; }
            set { _radius = int.Parse(value); }
        }
        private void recalcRadius(GeoMouseArgs e)
        {
            Coordinate c1 = e.GeographicLocation;
            int dx = _radius;
            if (Map.Projection != null)
            {
                if (Map.Projection.IsLatLon)
                {
                    dx = (int)(dx * 111319.5);
                }
                else
                {
                    dx *= (int)Map.Projection.Unit.Meters;
                }
            }
            IEnvelope env = new Envelope(c1);
            env.ExpandBy(dx, 1);
            Rectangle r = Map.ProjToPixel(env.ToExtent());
            circleRad = r.Width;
        }


        #endregion

    }

}
