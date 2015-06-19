// ********************************************************************************************************
// Product Name: TestViewer.exe
// Description:  A very basic demonstration of the controls.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Initial Developer of this Original Code is Ted Dunsford. Created during refactoring 2010.
// ********************************************************************************************************

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Symbology.Forms;

namespace DemoMap
{
    /// <summary>
    /// A Form to test the map controls.
    /// </summary>
    public partial class MainForm : Form
    {
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            map1.GeoMouseMove += Map_GeoMouseMove;

            Shell = this;
            appManager.LoadExtensions();
            appManager.CompositionContainer.ComposeParts(toolManager1);
        }

        private void Map_GeoMouseMove(object sender, GeoMouseArgs e)
        {
            // Generally this check for a null is never needed.
            // We use it here to allow the DemoMap to continue working in rare cases
            // such as when two Progresshandlers are specified.
            // In your own *extension* it is recommened that you don't worry about checking ProgressHandler != null
            if (appManager.ProgressHandler != null)
                appManager.ProgressHandler.Progress(String.Empty, 0, String.Format("X: {0}, Y: {1}", e.GeographicLocation.X, e.GeographicLocation.Y));
        }

        private void map1_Load(object sender, EventArgs e)
        {
            IFeatureSet fsHuc = FeatureSet.OpenFile(@"huc250d3.shp");
            ProjectionInfo projHuc = new ProjectionInfo();
            projHuc = fsHuc.Projection;
            //    fsHuc.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);            

            IFeatureSet fsCounty = FeatureSet.OpenFile(@"cnty.shp");
            ProjectionInfo projCounty = new ProjectionInfo();
            projCounty = fsCounty.Projection;
            // fsCounty.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            fsCounty.Reproject(projHuc);
            map1.Layers.Add(fsCounty);

            IMapFeatureLayer mFeatureLayer = map1.Layers.Add(fsCounty);
            IMapFeatureLayer mFeatureLayer2 = map1.Layers.Add(fsHuc);

            mFeatureLayer.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.CadetBlue, System.Drawing.Color.DarkBlue);
            mFeatureLayer2.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.Purple, System.Drawing.Color.DarkBlue);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IFeatureSet fsHuc = FeatureSet.OpenFile(@"huc250d3.shp");
            ProjectionInfo projHuc = new ProjectionInfo();
            projHuc = fsHuc.Projection;
        //    fsHuc.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);            

            IFeatureSet fsCounty = FeatureSet.OpenFile(@"cnty.shp");
            ProjectionInfo projCounty = new ProjectionInfo();
            projCounty = fsCounty.Projection;
           // fsCounty.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            fsCounty.Reproject(projHuc);

            IMapFeatureLayer mFeatureLayer = map1.Layers.Add(fsCounty);
            IMapFeatureLayer mFeatureLayer2 = map1.Layers.Add(fsHuc);

            mFeatureLayer.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.LightBlue, System.Drawing.Color.DarkBlue);
            mFeatureLayer2.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.CadetBlue, System.Drawing.Color.DarkBlue);


        }

       
    }
}