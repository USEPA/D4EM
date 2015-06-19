using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls.Docking;
using DotSpatial.Projections;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace D4EM_NatureServe
{
    public class NatureServe : Extension
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
            var form = new NatureServe();
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

            const string SampleMenuKey = "NatureServe";

            header.Add(new RootItem(SampleMenuKey, "NatureServe"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "NatureServe", myEventHandler) { Key = "keyNatureServe" };
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
       

        private void myEventHandler(object sender, EventArgs e)
        {
            ProjectionInfo proj = new ProjectionInfo();
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
                }
            }
            List<IFeature> HUCFeatures = selectedArs.ToFeatureList();
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

            NatureServeBox natureservebox = new NatureServeBox(huc8nums);
            natureservebox.ShowDialog();
            
            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathNatureServe";

            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);

                while ((fileName = read.ReadLine()) != null)
                {
                    IFeatureSet fs = FeatureSet.OpenFile(fileName);
                    ProjectionInfo pi = fs.Projection;
                    fs.Reproject(proj);
                    App.Map.Layers.Add(fs);
                   
                  //  App.Map.Layers[0].Projection = DotSpatial.Projections.KnownCoordinateSystems.Projected.;

                  //  App.Map.AddLayer(fileName).Reproject(proj);
                 //   App.Map.AddLayer(fileName);
                }

                read.Close();
            }
            File.Delete(downloadFilePath);

            string downloadFilePathBirds = @"C:\Temp\DownloadedFilePathNatureServeBirds";

            if (File.Exists(downloadFilePathBirds) == true)
            {
                TextReader read = new StreamReader(downloadFilePathBirds);

                while ((fileName = read.ReadLine()) != null)
                {
                    if(!fileName.Contains("bute_lago_pl.shp"))
                    {
                        IFeatureSet fs = FeatureSet.OpenFile(fileName);
                        fs.Reproject(proj);  
                        App.Map.Layers.Add(fs);
                    }                 
                }

                read.Close();
            }
            File.Delete(downloadFilePathBirds);
                    
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
