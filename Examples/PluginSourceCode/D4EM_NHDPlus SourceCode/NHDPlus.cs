using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls.Docking;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DotSpatial.Projections;
using DotSpatial.Topology;

namespace D4EM_NHDPlus
{ 
    public class NHDPlus : Extension
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
            var form = new NHDPlus();
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

            const string SampleMenuKey = "kSample";

            header.Add(new RootItem(SampleMenuKey, "NHDPlus"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "NHDPlus", myEventHandler) { Key = "kA" };
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
        ISelection selectedStreams = null;
        string huc8 = "";
        List<string> huc8nums = new List<string>();
        ProjectionInfo proj = new ProjectionInfo();
        double north = 0;
        double south = 0;
        double west = 0;
        double east = 0;
        double centerLong = 0;
        double centerLat = 0;
        double radius = 0;
        
        private void myEventHandler(object sender, EventArgs e)
        {
            List<string> streams = new List<string>();
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
                    /*
                else if (String.Compare(fs.Name, "HUC12", true) == 0)
                {
                    _fsHUC12 = fs;
                    _flHUC12 = fl;
                    selectedArs = _flHUC12.Selection;
                }*/
                    /*  else
                      {
                          MessageBox.Show("Please select a HUC first.");
                          return;
                      } */
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

                    centerLong = bb.CenterLongitude;
                    centerLat = bb.CenterLatitude;
                    radius = bb.Radius * 1000;

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
                if (fs.Name.Contains("nhdflowline"))
                {
                    selectedStreams = fl.Selection;
                    if (selectedStreams != null)
                    {
                        if (selectedStreams.Count != 0)
                        {
                            List<IFeature> streamFeatures = selectedStreams.ToFeatureList();
                            int j = 0;
                            foreach (IFeature feature in streamFeatures)
                            {
                                IFeature streamFeature = streamFeatures[j];
                                string streamID = streamFeature.DataRow["COMID"].ToString();
                                if(!streams.Contains(streamID))
                                {
                                    streams.Add(streamID);
                                }
                                j++;
                            }
                        }
                    }
                }
            }

            
           
            NHDPlusBox nhdplusbox = new NHDPlusBox(huc8nums, centerLong, centerLat, streams);
            nhdplusbox.ShowDialog();

            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathNHDPlus";

            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);

                while ((fileName = read.ReadLine()) != null)
                {
                    IFeatureSet fs = FeatureSet.OpenFile(fileName);
                    fs.Reproject(proj);
                    App.Map.Layers.Add(fs);                  
                                
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
