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
using MapWinUtility;
using DotSpatial.Analysis;

namespace D4EM_NDBC
{
    public class NDBC : Extension
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
            var form = new NDBC();
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

            const string SampleMenuKey = "NDBC";

            header.Add(new RootItem(SampleMenuKey, "NDBC"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "NDBC", myEventHandler) { Key = "keyNDBC" };
            header.Add(alphaItem);
            /* header.Add(new SimpleActionItem(SampleMenuKey, "Bravo", null));
             header.Add(new SimpleActionItem(SampleMenuKey, "Charlie", null));
             header.Add(new MenuContainerItem(SampleMenuKey, "submenu", "B"));
             header.Add(new SimpleActionItem(SampleMenuKey, "submenu", "1", null));
             SimpleActionItem item = new SimpleActionItem(SampleMenuKey, "submenu", "2", null);
             header.Add(item);
             * */
            // header.Add(new SimpleActionItem(SampleMenuKey, "submenu", "3", null));
             

            // alphaItem.Enabled = false;
            // header.Remove(item.Key);

            header.Add(new RootItem(SampleMenuKey, "NDBC"));
            SimpleActionItem betaItem = new SimpleActionItem(SampleMenuKey, "Restart", restart) { Key = "keyRestartNDBC" };
            header.Add(betaItem);
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

        private void restart(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private IFeatureSet _fsHUC8 = null;
        private IFeatureSet _fsHUC12 = null;
        private IFeatureLayer _flHUC8 = null; 
        private IFeatureLayer _flHUC12 = null;
        ISelection selectedArs = null;
        ISelection selectedStations = null;
        string huc8 = "";
        List<string> huc8nums = new List<string>();
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

                    List<IFeature> HUCFeatures = selectedArs.ToFeatureList();
                    if (HUCFeatures == null)
                        return;
                    //  IFeature HUCFeature = HUCFeatures[0];
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
                }
                if (fs.Name.Contains("NDBC"))
                {
                    selectedStations = fl.Selection;
                }
                /*  else if (String.Compare(fs.Name, "HUC12", true) == 0)
                  {
                      _fsHUC12 = fs;
                      _flHUC12 = fl;
                      selectedArs = _flHUC12.Selection;
                  }*/
                /*
            else
            {
                MessageBox.Show("Please select a HUC first.");
                return;
            }
                  */
              
                

            }

            List<string> stations = new List<string>();
            if (selectedStations != null)
            {
                if (selectedStations.Count != 0)
                {
                    List<IFeature> stationFeatures = selectedStations.ToFeatureList();
                    int j = 0;
                    foreach (IFeature feature in stationFeatures)
                    {
                        IFeature stationFeature = stationFeatures[j];
                        string stationID = stationFeature.DataRow["Station"].ToString();
                        if (!stationID.Contains("SHIP"))
                        {
                            stations.Add(stationID);
                        }
                        j++;
                    }
                }
            }
            NDBCBox NDBCbox = new NDBCBox(stations);
            NDBCbox.ShowDialog();

            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathNDBC";

            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);

                while ((fileName = read.ReadLine()) != null)
                {
                    
                    EPAUtility.PointShapeFileToFeatureSet ps = new EPAUtility.PointShapeFileToFeatureSet(fileName, proj);
                    
                    IFeatureSet fs = ps.Stations;
                    fs.Name = "NDBC:" + System.IO.Path.GetFileNameWithoutExtension(fileName);
                    App.Map.Layers.Add(fs);

                }
                read.Close();
            }
            File.Delete(@"C:\Temp\DownloadedFilePathNDBC");
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
