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
using System.Data;

namespace D4EM_NWIS
{
    public class NWIS : Extension
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
            var form = new NWIS();
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

            const string SampleMenuKey = "NWIS";

            header.Add(new RootItem(SampleMenuKey, "NWIS"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "NWIS", myEventHandler) { Key = "keyNWIS" };
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
        ProjectionInfo proj = new ProjectionInfo();
        string huc8 = "";
        List<string> huc8nums = new List<string>();
        ISelection selectedStations = null;
        double north = 0;
        double south = 0;
        double west = 0;
        double east = 0;

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

                    ProjectionInfo source = App.Map.Projection;
                    ProjectionInfo dest = KnownCoordinateSystems.Geographic.World.WGS1984;

                    EPAUtility.BoundingBox bb = new EPAUtility.BoundingBox(HUCFeatures, source, dest);
                    north = bb.North;
                    south = bb.South;
                    east = bb.East;
                    west = bb.West;
                }
                if (fs.Name.Contains("NWIS_discharge"))
                {
                    selectedStations = fl.Selection;
                }
                    /*
                else if (String.Compare(fs.Name, "HUC12", true) == 0)
                {
                    _fsHUC12 = fs;
                    _flHUC12 = fl;
                    selectedArs = _flHUC12.Selection;
                }
                     * */
             /*   else
                {
                    MessageBox.Show("Please select a HUC first.");
                    return;
                }*/
                
            }

            if ((Math.Abs(north - south) > 4) || (Math.Abs(east - west) > 4))
            {
                MessageBox.Show("We recommend that you select a smaller area");
            }

            List<string> stations = new List<string>();
            if (selectedStations != null)
            {
                if (selectedStations.Count != 0)
                {
                    List<IFeature> stationFeatures = selectedStations.ToFeatureList();
                    int k = 0;
                    foreach (IFeature feature in stationFeatures)
                    {
                        IFeature stationFeature = stationFeatures[k];
                        string stationID = stationFeature.DataRow["StationID"].ToString();
                        if (!stationID.Contains("SHIP"))
                        {
                            stations.Add(stationID);
                        }
                        k++;
                    }
                }
            }

            NWISBox nwisbox = new NWISBox(north, south, east, west, stations);
            nwisbox.ShowDialog();
            
            string directoryname = "";
            if (File.Exists(@"C:\Temp\NWISdirectoryname"))
            {
                TextReader tr = new StreamReader(@"C:\Temp\NWISdirectoryname");
                directoryname = tr.ReadLine().ToString() + "\\NWISshapefiles";
                tr.Close();
            }
            else
            {
                directoryname = @"C:\Temp\NWISshapefiles";
            }
            File.Delete(@"C:\Temp\NWISdirectoryname");

            FeatureSet pointCoords = new FeatureSet();
            pointCoords.Projection = KnownCoordinateSystems.Geographic.World.WGS1984;
            int j = 0;
            string fileName;
            
            string downloadFilePath = @"C:\Temp\DownloadedFilePathNWIS";
            if (File.Exists(downloadFilePath) == true)
            {
                List<IFeature> HUCFeatures2 = selectedArs.ToFeatureList();
                foreach (Feature hucf in HUCFeatures2)
                {
                    bool reproject = true;
                    IFeature hucFeature = HUCFeatures2[j];
                    TextReader read = new StreamReader(downloadFilePath);
                    while ((fileName = read.ReadLine()) != null)
                    {
                        EPAUtility.StationsWithinHUC st = new EPAUtility.StationsWithinHUC(hucFeature, fileName, proj, reproject);
                        pointCoords = st.Stations;
                        pointCoords.Name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                        pointCoords.Name = pointCoords.Name + "(" + hucFeature.DataRow["CU"] + ")";
                        App.Map.Layers.Add(pointCoords);
                        reproject = false;
                    }
                    read.Close();                    
                    j++;
                }
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
