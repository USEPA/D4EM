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
using DotSpatial.Analysis;
using System.Data;

namespace D4EM_WDNR
{
    public class WDNR : Extension
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
            var form = new WDNR();
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

            const string SampleMenuKey = "WDNR";

            header.Add(new RootItem(SampleMenuKey, "WDNR"));
            SimpleActionItem alphaItem = new SimpleActionItem(SampleMenuKey, "WDNR", myEventHandler) { Key = "keyWDNR" };
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
                }
                

            
            }
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
            ProjectionInfo source = App.Map.Projection;
            ProjectionInfo dest = KnownCoordinateSystems.Geographic.World.WGS1984;

            double north = 0;
            double south = 0;
            double east = 0;
            double west = 0;

            if (HUCFeatures.Count > 0)
            {

                EPAUtility.BoundingBox bb = new EPAUtility.BoundingBox(HUCFeatures, source, dest);
                north = bb.North;
                south = bb.South;
                east = bb.East;
                west = bb.West;
            }

            WDNRBox WDNRbox = new WDNRBox(huc8nums, north, south, east, west);
            WDNRbox.ShowDialog();

            string fileName;
            string downloadFilePath = @"C:\Temp\DownloadedFilePathWDNR";

            if (File.Exists(downloadFilePath) == true)
            {
                TextReader read = new StreamReader(downloadFilePath);

                while ((fileName = read.ReadLine()) != null)
                {
                  //  IFeatureSet fs = FeatureSet.OpenFile(fileName);
                    EPAUtility.PointShapeFileToFeatureSet psftfs = new EPAUtility.PointShapeFileToFeatureSet(fileName, proj);
                    IFeatureSet fs = psftfs.Stations;
                    ProjectionInfo newproj = fs.Projection;
                    fs.Reproject(proj);
                    fs.Name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    string animal = fs.Name;
          //          App.Map.Layers.Add(fs);
           
                    foreach (Feature f in fs.Features)
                    {
                        fs.FeatureLookup.Add(f.DataRow, f);
                    }

                    List<IFeature> beefFeatures = fs.SelectByAttribute("[Animal] = 'Beef'");
                    List<IFeature> swineFeatures = fs.SelectByAttribute("[Animal] = 'Swine'");
                    List<IFeature> dairyFeatures = fs.SelectByAttribute("[Animal] = 'Dairy'");
                    List<IFeature> chickenFeatures = fs.SelectByAttribute("[Animal] = 'Chickens'");
                    List<IFeature> turkeyFeatures = fs.SelectByAttribute("[Animal] = 'Turkeys'");

                    if (beefFeatures.Count > 0)
                    {
                        string beefSymbol = Path.GetFullPath(@"..\Bin\WDNRsymbols\Black-Cow-icon.png");
                        changeSymbol(fs, beefFeatures, animal, beefSymbol);
                    }
                    if (swineFeatures.Count > 0)
                    {
                        string swineSymbol = Path.GetFullPath(@"..\Bin\WDNRsymbols\pig.ico");
                        changeSymbol(fs, swineFeatures, animal, swineSymbol);
                    }
                    if (chickenFeatures.Count > 0)
                    {
                        string chickenSymbol = Path.GetFullPath(@"..\Bin\WDNRsymbols\chiken.ico");
                        changeSymbol(fs, chickenFeatures, animal, chickenSymbol);
                    }
                    if (turkeyFeatures.Count > 0)
                    {
                        string turkeySymbol = Path.GetFullPath(@"..\Bin\WDNRsymbols\turkey.png");
                        changeSymbol(fs, turkeyFeatures, animal, turkeySymbol);
                    }
                    if (dairyFeatures.Count > 0)
                    {
                        string dairySymbol = Path.GetFullPath(@"..\Bin\WDNRsymbols\Milk-icon.png");
                        changeSymbol(fs, dairyFeatures, animal, dairySymbol);
                    }
                    
                }
                read.Close();
            }
            File.Delete(downloadFilePath);            
        }

        private void changeSymbol(IFeatureSet fs, List<IFeature> animalFeatures, string animalAttribute, string picFileName)
        {
           
            FeatureSet animalFeatureSet = new FeatureSet();
           
            int counter = fs.DataTable.Columns.Count;
            string[] attributes = new string[counter];
            int i = 0;
            foreach (DataColumn dc in fs.DataTable.Columns)
            {
                animalFeatureSet.DataTable.Columns.Add(dc.ColumnName);
                attributes[i] = dc.ColumnName;
                i++;
            }

            foreach (IFeature animalFeature in animalFeatures)
            {
                foreach(IFeature fsFeature in fs.Features)
                {
                    if (fsFeature == animalFeature)
                    {
                        IFeature currentFeature = animalFeatureSet.AddFeature(animalFeature);           
                        int j = 0;
                        foreach (string attribute in attributes)
                        {
                            string column = attributes[j];
                            string value = fsFeature.DataRow[j].ToString();
                            currentFeature.DataRow[column] = value;
                            j++;
                        }
                        
                    }
                }         
            }

            animalFeatureSet.Projection = fs.Projection;
            animalFeatureSet.Reproject(proj);
            animalFeatureSet.Name = animalAttribute;
            
            DotSpatial.Symbology.PictureSymbol animalsym = new PictureSymbol();
            animalsym.ImageFilename = picFileName;
            DotSpatial.Symbology.Size2D size = new Size2D(20, 20);
            animalsym.Size = size;
            PointSymbolizer swinesymbol = new PointSymbolizer(animalsym);

            IMapFeatureLayer animalLayer = App.Map.Layers.Add(animalFeatureSet);
            animalLayer.Symbolizer = swinesymbol;
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
