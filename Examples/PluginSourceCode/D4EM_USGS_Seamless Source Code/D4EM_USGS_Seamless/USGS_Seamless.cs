using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls.Docking;
using DotSpatial.Projections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using atcData;
using atcUtility;
using MapWinUtility;
using DotSpatial.Analysis;
using System.Data;

namespace D4EM_USGS_Seamless
{
    public class USGS_Seamless : Extension
    {
        private const string UniqueKeyPluginStoredValueDate = "UniqueKey-PluginStoredValueDate";
        private const string AboutPanelKey = "kAboutPanel";
        DateTime _storedValue;

        public override void Deactivate()
        {
            App.DockManager.Remove(AboutPanelKey);
            if (App.HeaderControl != null) { App.HeaderControl.RemoveAll(); }
            base.Deactivate();
        }

        #region IMapPlugin Members

        public override void Activate()
        {
            //to test slow loading
            //System.Threading.Thread.Sleep(20000);

            AddMenuItems(App.HeaderControl);

            //code for saving plugin settings...
            App.SerializationManager.Serializing += new EventHandler<SerializingEventArgs>(manager_Serializing);
            App.SerializationManager.Deserializing += new EventHandler<SerializingEventArgs>(manager_Deserializing);

            AddDockingPane();

            base.Activate();
        }

        private void AddDockingPane()
        {
            var form = new USGS_Seamless();
            //   DockablePanel aboutPanel = new DockablePanel(AboutPanelKey, "About", form.tableLayoutPanel, DockStyle.Right);
            //  App.DockManager.Add(aboutPanel);
            //  form.okButton.Click += new EventHandler(okButton_Click);
            App.DockManager.ActivePanelChanged += new EventHandler<DockablePanelEventArgs>(DockManager_ActivePanelChanged);
        }

        void DockManager_ActivePanelChanged(object sender, DockablePanelEventArgs e)
        {
            App.HeaderControl.SelectRoot("kHome");
        }

        void okButton_Click(object sender, EventArgs e)
        {
            App.DockManager.Remove(AboutPanelKey);
        }

        private void AddMenuItems(IHeaderControl header)
        {
            // add sample menu items...
            if (header == null) return;

            const string SampleMenuKey = "USGS_Seamless";

            header.Add(new RootItem(SampleMenuKey, "USGS_Seamless"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "USGS_Seamless", myEventHandler) { Key = "keyUSGS_Seamless" };
            header.Add(alphaItem);
            /* header.Add(new SimpleActionItem(SampleMenuKey, "Bravo", null));
             header.Add(new SimpleActionItem(SampleMenuKey, "Charlie", null));
             header.Add(new MenuContainerItem(SampleMenuKey, "submenu", "B"));
             header.Add(new SimpleActionItem(SampleMenuKey, "submenu", "1", null));
             SimpleActionItem item = new SimpleActionItem(SampleMenuKey, "submenu", "2", null);
             header.Add(item);
             header.Add(new SimpleActionItem(SampleMenuKey, "submenu", "3", null));
             */

            // alphaItem.Enabled = false;
            // header.Remove(item.Key);
        }

        private void manager_Deserializing(object sender, SerializingEventArgs e)
        {
            var manager = sender as SerializationManager;

            _storedValue = manager.GetCustomSetting<DateTime>(UniqueKeyPluginStoredValueDate, DateTime.Now);
        }

        private void manager_Serializing(object sender, SerializingEventArgs e)
        {
            var manager = sender as SerializationManager;

            manager.SetCustomSetting(UniqueKeyPluginStoredValueDate, _storedValue);
        }

        private IFeatureSet _fsHUC8 = null;
        private IFeatureSet _fsHUC12 = null;
        private IFeatureLayer _flHUC8 = null;
        private IFeatureLayer _flHUC12 = null;
        ISelection selectedArs = null;
        string huc8 = "";
        List<string> huc8nums = new List<string>();
        double north = 0;
        double south = 0;
        double west = 0;
        double east = 0;
        ProjectionInfo proj = new ProjectionInfo();
        private void myEventHandler(object sender, EventArgs e)
        {
            List<ILayer> layers = App.Map.GetLayers();
            foreach (ILayer layer in layers)
            {
                IFeatureLayer fl = layer as IFeatureLayer;
                if (fl == null)
                    continue;
                IFeatureSet fs = fl.DataSet;
                if (String.Compare(fs.Name, "huc250d3", true) == 0)
                {
                    _fsHUC8 = fs;
                    _flHUC8 = fl;
                    selectedArs = _flHUC8.Selection;
                    proj = fl.Projection;

                    if (selectedArs.Count < 1)
                    {
                        MessageBox.Show("Please select a HUC first.");
                        return;
                    }
                    List<IFeature> HUCFeatures = selectedArs.ToFeatureList();
                    if (HUCFeatures == null)
                        return;
                    int i = 0;
                    huc8nums.Clear();
                    foreach (IFeature feature in HUCFeatures)
                    {
                        IFeature HUCFeature = HUCFeatures[i];
                        huc8 = HUCFeature.DataRow["CU"].ToString();
                        if (huc8.Length < 8)
                        {
                            huc8 = "0" + huc8;
                        }
                        huc8nums.Add(huc8);
                        i++;
                    }

                    ProjectionInfo source = App.Map.Projection;
                    ProjectionInfo dest = KnownCoordinateSystems.Geographic.World.WGS1984;

                    EPAUtility.BoundingBox bb = new EPAUtility.BoundingBox(HUCFeatures, source, dest);
                    north = bb.North;
                    south = bb.South;
                    east = bb.East;
                    west = bb.West;
                }
            }

            if ((Math.Abs(north - south) > 4) || (Math.Abs(east - west) > 4))
            {
                MessageBox.Show("We recommend that you select a smaller area");
            }

            USGS_SeamlessBox USGS_Seamlessbox = new USGS_SeamlessBox(north, south, east, west, huc8nums);
            USGS_Seamlessbox.ShowDialog();


            List<IFeature> HucFeatures = selectedArs.ToFeatureList();
            IFeature hucFeature = HucFeatures[0];

            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathUSGS_Seamless";
            DataTable dt1992 = null;
            DataTable dt2001 = null;
            DataTable dt2006 = null;
            string file1992 = "";
            string file2001 = "";
            string file2006 = "";
            string _fileName = "";
            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);
                try
                {
                    while ((fileName = read.ReadLine()) != null)
                    {
                        if (File.Exists(fileName) == true)
                        {
                            _fileName = fileName;
                            string aProjectFolder = Directory.GetParent(fileName).FullName;

                            IRaster rl = Raster.Open(fileName);
                            rl.Projection = proj;
                            ProjectionInfo rlpi = rl.Projection;
                            ProjectionInfo project = hucFeature.ParentFeatureSet.Projection;
                            //  rl.Projection = proj;
                            //   rl.Reproject(project);
                          /*  if (Path.GetFileName(_fileName).Contains("NED"))
                            {
                                rl.Reproject(proj);
                            }*/
                            rl.Projection = proj;

                            App.Map.Layers.Add(rl);

                            /*  string newTifFile = @"C:\Temp\USGS_SeamlessRasterTest.tif";
                              ClipRaster.ClipRasterWithPolygon(hucFeature, rl, newTifFile);
                              IRaster rlnew = Raster.OpenFile(fileName);                           
                              rlnew.Projection = proj;
                              //    rl.Reproject(projraster);
                              App.Map.Layers.Add(rlnew);*/


                            IRasterBounds bounds = rl.Bounds;
                            double area = bounds.Area();
                            double conv2 = 0.000247105381;  // m^2 to acres
                            double area_acres = conv2 * area;

                            int endcolumn = rl.EndColumn;
                            //     int endrow = rl.EndRow;
                            Extent extent = rl.Extent;
                            List<double> randomvalues = rl.GetRandomValues(endcolumn);
                            //  IEnumerable<double> sample = rl.Sample;
                            //   List<double> samplelist = sample.ToList<double>();

                            if (File.Exists(@"C:\Temp\tabulate.txt"))
                            {
                                EPAUtility.TabulateNLCD tab = new EPAUtility.TabulateNLCD();
                                if (fileName.Contains("1992"))
                                {
                                    dt1992 = tab.tabulateNLCD(area_acres, rl, 1992);
                                    file1992 = fileName;
                                }
                                if (fileName.Contains("2001") && fileName.Contains("landcover"))
                                {
                                    dt2001 = tab.tabulateNLCD(area_acres, rl, 2001);
                                    file2001 = fileName;
                                }
                                if (fileName.Contains("2006") && fileName.Contains("landcover"))
                                {
                                    dt2006 = tab.tabulateNLCD(area_acres, rl, 2006);
                                    file2006 = fileName;
                                }
                                NLCDTable nlcdtable = new NLCDTable(dt1992, dt2001, dt2006, file1992, file2001, file2006, north, south, east, west);
                                if (_fileName.Contains("LandCover"))
                                {
                                    nlcdtable.Show();
                                }
                                File.Delete(@"C:\Temp\tabulate.txt");
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                read.Close();
            }

            File.Delete(downloadFilePath);
        }


        private void myButton_Click(object sender, EventArgs e)
        {
            foreach (var recentFile in DotSpatial.Data.Properties.Settings.Default.RecentFiles)
            {
                MessageBox.Show("Recent File: " + recentFile);
            }

            IMapRasterLayer[] layers = App.Map.GetRasterLayers();
            if (layers.Length == 0)
            {
                MessageBox.Show("Please add a raster layer.");
                return;
            }
            IMapRasterLayer layer = layers[0];
            layer.Symbolizer.ShadedRelief.ElevationFactor = 1;
            layer.Symbolizer.ShadedRelief.IsUsed = true;
            layer.Symbolizer.CreateHillShade();

            layer.WriteBitmap();

            //Raster myRaster = new Raster();
            //myRaster.Create(
        }

        #endregion IMapPlugin Members
    }
}
