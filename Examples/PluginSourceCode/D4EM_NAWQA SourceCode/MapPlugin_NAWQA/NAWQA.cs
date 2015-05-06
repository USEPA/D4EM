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
using System.Net;
using System.Text;
using System.Data;


namespace D4EM_NAWQA
{
    public class NAWQA : Extension
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
            var form = new NAWQA();
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

            const string SampleMenuKey = "NAWQA";

            header.Add(new RootItem(SampleMenuKey, "NAWQA"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "NAWQA", myEventHandler) { Key = "keyNAWQA" };
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

        private IFeatureSet _fsCounty = null;
        private IFeatureLayer _flCounty = null;
        ISelection selectedArs = null;
        string county = "";
        List<string> counties = new List<string>();
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
                if (String.Compare(fs.Name, "cnty", true) == 0)
                {
                    _fsCounty = fs;
                    _flCounty = fl;
                    selectedArs = _flCounty.Selection;
                    proj = fl.Projection;

                /*    if ((selectedArs.Count < 1 ) && (String.Compare(fs.Name, "cnty", true) == 0))
                    {
                        MessageBox.Show("Please select a County first.");
                        return;
                    }*/
                    List<IFeature> CountyFeatures = selectedArs.ToFeatureList();
                    if (CountyFeatures == null)
                        return;
                    //  IFeature HUCFeature = HUCFeatures[0];
                    int i = 0;
                    counties.Clear();
                    foreach (IFeature feature in CountyFeatures)
                    {
                        IFeature CountyFeature = CountyFeatures[i];
                        string stateName = CountyFeature.DataRow[1].ToString();
                        string countyName = CountyFeature.DataRow[2].ToString();
                        counties.Add(countyName + ", " + stateName);
                        i++;
                    }
                    ProjectionInfo source = App.Map.Projection;
                    ProjectionInfo dest = KnownCoordinateSystems.Geographic.World.WGS1984;
                }
            }


            NAWQABox NAWQAbox = new NAWQABox(counties);
            NAWQAbox.ShowDialog();

            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathNAWQA";
            
            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);
                try
                {
                    while ((fileName = read.ReadLine()) != null)
                    {

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                read.Close();
            }

            File.Delete(@"C:\Temp\DownloadedFilePathNAWQA");
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
