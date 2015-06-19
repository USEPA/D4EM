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
    public class SDPProjectBuilderPolygonSelector : MapFunction
    {
        #region Private Variables

        private bool _isDragging;
        private IFeatureSet _featureSet;
        private System.Drawing.Point _startPoint;
        private Coordinate _geoStartPoint;
        private System.Drawing.Point _currentPoint;

        private readonly Pen _selectionPen;
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of SelectTool
        /// </summary>
        public SDPProjectBuilderPolygonSelector(IMap inMap)
            : base(inMap)
        {
            _selectionPen = new Pen(Color.Black);
            _selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        #endregion


        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDraw(MapDrawArgs e)
        {
            if (_isDragging) // don't draw anything unless we need to draw a select rectangle
            {
                Rectangle r = Opp.RectangleFromPoints(_startPoint, _currentPoint);
                r.Width -= 1;
                r.Height -= 1;
                e.Graphics.DrawRectangle(Pens.White, r);
                e.Graphics.DrawRectangle(_selectionPen, r);
            }
            base.OnDraw(e);
        }

        /// <summary>
        /// Handles the MouseDown 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(GeoMouseArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                _startPoint = e.Location;
                _geoStartPoint = e.GeographicLocation;
                _isDragging = true;
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handles MouseMove
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(GeoMouseArgs e)
        {
            int x = Math.Min(Math.Min(_startPoint.X, _currentPoint.X), e.X);
            int y = Math.Min(Math.Min(_startPoint.Y, _currentPoint.Y), e.Y);
            int mx = Math.Max(Math.Max(_startPoint.X, _currentPoint.X), e.X);
            int my = Math.Max(Math.Max(_startPoint.Y, _currentPoint.Y), e.Y);
            _currentPoint = e.Location;
            if (_isDragging)
            {
                Map.Invalidate(new Rectangle(x, y, mx - x, my - y));
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handles the Mouse Up situation
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(GeoMouseArgs e)
        {
            if (_isDragging == false) return;
            _currentPoint = e.Location;
            _isDragging = false;
            //Map.Invalidate(); // Get rid of the selection box
            //Application.DoEvents();
            IEnvelope env = new Envelope(_geoStartPoint.X, e.GeographicLocation.X, _geoStartPoint.Y, e.GeographicLocation.Y);
            IEnvelope tolerant = env;

            if (_startPoint.X == e.X && _startPoint.Y == e.Y)
            {
                // click selection doesn't work quite right without some tiny tolerance.
                double tol = Map.MapFrame.Extent.Width / 10000;
                env.ExpandBy(tol);
            }

            if (Math.Abs(_startPoint.X - e.X) < 8 && Math.Abs(_startPoint.Y - e.Y) < 8)
            {
                Coordinate c1 = e.Map.PixelToProj(new System.Drawing.Point(e.X - 4, e.Y - 4));
                Coordinate c2 = e.Map.PixelToProj(new System.Drawing.Point(e.X + 4, e.Y + 4));
                tolerant = new Envelope(c1, c2);
            }


            HandleSelection(tolerant, env);

            e.Map.MapFrame.Initialize();
            base.OnMouseUp(e);
        }

        private void HandleSelection(IEnvelope tolerant, IEnvelope strict)
        {
            IEnvelope region;
            Keys key = Control.ModifierKeys;
            if ((((key & Keys.Shift) == Keys.Shift) == false)
                && (((key & Keys.Control) == Keys.Control) == false))
            {
                // If they are not pressing shift, then first clear the selection before adding new members to it.              

                Map.ClearSelection(out region);

            }

            if ((key & Keys.Control) == Keys.Control)
            {
                Map.InvertSelection(tolerant, strict, DotSpatial.Symbology.SelectionMode.Intersects, out region);
            }
            else
            {

                Map.Select(tolerant, strict, DotSpatial.Symbology.SelectionMode.Intersects, out region);
            }

        }

        /// <summary>
        /// Gets or sets the featureset to modify
        /// </summary>
        public IFeatureSet FeatureSet
        {
            get {

                IFeatureLayer ifl = (IFeatureLayer)SDPProjectBuilderPlugin_GUI.Map.Layers.SelectedLayer;
                _featureSet = ifl.Selection.ToFeatureSet();                
                return _featureSet;            
            
            }
            set { _featureSet = value; }
        }

    }

}
