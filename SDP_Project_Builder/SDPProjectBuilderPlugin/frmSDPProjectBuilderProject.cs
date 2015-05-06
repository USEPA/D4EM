using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SDP_Project_Builder_Batch;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;
using DotSpatial.Symbology;
using D4EM.Data.DBManager;

namespace SDPProjectBuilderPlugin
{
    public partial class frmSDPProjectBuilderProject : Form, MapWinUtility.IProgressStatus
    {


        private const string AOI_TYPE_POLYGON = "Polygon";
        private const string AOI_TYPE_CIRCLE = "Circle";
        private const string AOI_TYPE_BUFFER = "Buffer";
        private const string AOI_TYPE_SHAPE = "Shape";

        private const string SHP_STATE_NAME_FIELD = "NAME";
        private const string SHP_STATE_ST_FIELD = "ST";
        private const string SHP_COUNTY_CNTYNAME_FIELD = "CNTYNAME";
        private const string SHP_COUNTY_FIPS_FIELD = "FIPS";
        private const string SHP_COUNTY_STATE_FIELD = "STATE";
        private const string SHP_HUC_HUC_FIELD = "HUC";

        private DataTable _dtCounties = null;
        private DataTable _dtStates = null;
        private DataTable _dtHucs = null;
        private IFeatureSet _stateFeatures = null;
        private IFeatureSet _countyFeatures = null;
        private IFeatureSet _hucFeatures = null;

        private SDPProjectBuilderPolygonDrawer _drawPolygonSource = null;
        private SDPProjectBuilderPolygonDrawer _drawPolygonAOI = null;
        private SDPProjectBuilderCircleDrawer _drawCircleAOI = null;
        private SDPProjectBuilderPolygonSelector _drawShapeAOI = null;
        private const string LAYER_NAME_SOURCE = "Source";
        private const string LAYER_NAME_AOI = "AOI";

        private DataTable _dtModuleDependencies = null;

        private string _projectFile = String.Empty;

        public string ProjectFile
        {
            get { return _projectFile; }
            set { _projectFile = value; }
        }

        public frmSDPProjectBuilderProject()
        {
            InitializeComponent();
            MapWinUtility.Logger.ProgressStatus = this;

            LoadSourceTypes();
            LoadScienceModules();

            CreateModuleDependenciesTable();
        }

        void MapWinUtility.IProgressStatus.Progress(int aCurrentPosition, int aLastPosition)
        {
            int percent = 100;
            if (aLastPosition > aCurrentPosition)
                if (aLastPosition > 100000) //multiplying large numbers by 100 could overflow, safer to divide last position by 100 when it is enough larger than 100
                    percent = aCurrentPosition / (aLastPosition / 100);
                else
                    percent = aCurrentPosition * 100 / aLastPosition;
            //appManager.ProgressHandler.Progress(String.Empty, (int)0, percent + "%");
            //Application.DoEvents();
        }

        void MapWinUtility.IProgressStatus.Status(string aStatusMessage)
        {
            if (!aStatusMessage.StartsWith("PROGRESS"))
            {
                //appManager.ProgressHandler.Progress(String.Empty, (int)0, aStatusMessage);
                UpdateStatus(aStatusMessage);
            }
            //Application.DoEvents();
        }

        public IFeatureSet GetFeatureSetSource()
        {
            IMapLayer iml = GetLayerByLegendText(LAYER_NAME_SOURCE);
            IFeatureSet fs = null;
            if (iml != null)
            {
                fs = (IFeatureSet)iml.DataSet;
            }
            return fs;           
        }

        public IFeatureSet GetFeatureSetAOI()
        {
            IMapLayer iml = GetLayerByLegendText(LAYER_NAME_AOI);
            IFeatureSet fs = null;
            if (iml != null)
            {
                fs = (IFeatureSet)iml.DataSet;
            }
            return fs;
        }

        public void CreateModuleDependenciesTable()
        {
            _dtModuleDependencies = new DataTable("ModuleDependencies");
            //add columns
            DataColumn col = new DataColumn("Module");
            col.DataType = System.Type.GetType("System.String");
            _dtModuleDependencies.Columns.Add(col); 

            col = new DataColumn("Dependency");
            col.DataType = System.Type.GetType("System.String");
            _dtModuleDependencies.Columns.Add(col); 

            //add rows
            _dtModuleDependencies.Rows.Add("Ecological Risk", "Terrestrial Food Web");
            _dtModuleDependencies.Rows.Add("Ecological Risk", "Surface Water");
            _dtModuleDependencies.Rows.Add("Ecological Risk", "Ecological Exposure");
            _dtModuleDependencies.Rows.Add("Ecological Exposure", "Terrestrial Food Web");
            _dtModuleDependencies.Rows.Add("Ecological Exposure", "Aquatic Food Web");
            _dtModuleDependencies.Rows.Add("Ecological Exposure", "Surface Water");
            _dtModuleDependencies.Rows.Add("Human Exposure", "Aquatic Food Web");
            _dtModuleDependencies.Rows.Add("Human Exposure", "Air");
            _dtModuleDependencies.Rows.Add("Human Exposure", "Watershed");
            _dtModuleDependencies.Rows.Add("Human Exposure", "Farm Food Chain");
            _dtModuleDependencies.Rows.Add("Human Risk", "Human Exposure");
            _dtModuleDependencies.Rows.Add("Terrestrial Food Web", "Air");
            _dtModuleDependencies.Rows.Add("Terrestrial Food Web", "Watershed");
            _dtModuleDependencies.Rows.Add("Aquatic Food Web", "Surface Water");
            _dtModuleDependencies.Rows.Add("Farm Food Chain", "Air");
            _dtModuleDependencies.Rows.Add("Farm Food Chain", "Surface Water");
            _dtModuleDependencies.Rows.Add("Farm Food Chain", "Watershed");
            _dtModuleDependencies.Rows.Add("Farm Food Chain", "Aquifer");
            _dtModuleDependencies.Rows.Add("Surface Water", "Air");
            _dtModuleDependencies.Rows.Add("Surface Water", "Watershed");
            _dtModuleDependencies.Rows.Add("Surface Water", "Aquifer");
            _dtModuleDependencies.Rows.Add("Aquifer", "Watershed");
            _dtModuleDependencies.Rows.Add("Aquifer", "Vadose Zone");
            _dtModuleDependencies.Rows.Add("Watershed", "Air");
        
        }

        public void ForceFullDependency()
        { 
            //get list of science modules selected
            List<string> lstSelected = new List<string>();

            foreach (object obj in lbScienceModules.SelectedItems)
            {
                string sItem = obj.ToString();
                lstSelected.Add(sItem);
            }

            //find dependencies of selected items
            List<string> lstDependencies = new List<string>();
            foreach (string s in lstSelected)
            {
                GetDependencies(s, ref lstDependencies);            
            }

            //select dependencies in list box
            foreach (string s in lstDependencies)
            {
                int index = lbScienceModules.FindStringExact(s);
                lbScienceModules.SetSelected(index, true);
            }
        
        }

        public bool GetDependencies(string sModule, ref List<string> lstDependencies)
        {
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = _dtModuleDependencies.Select("Module = '" + sModule + "'");

            if (foundRows.Length > 0)
            {
                for (int i = 0; i < foundRows.Length; i++)
                {
                    //add each dependency to list of dependencies
                    string sDependency = foundRows[i]["Dependency"].ToString();
                    if (lstDependencies.Contains(sDependency) == false)
                    {
                        lstDependencies.Add(sDependency);
                    }
                    //find dependencies of each dependency                            
                    GetDependencies(sDependency, ref lstDependencies);
                    
                }

                return true;
            }
            else
            {
                return false;
            } 

        
        }

        public void LoadSourceTypes()
        {
            lbSourceTypes.Items.Add("Aerated Tank");
            lbSourceTypes.Items.Add("Land Application Unit");
            lbSourceTypes.Items.Add("Landfill");
            lbSourceTypes.Items.Add("Surface Impoundment");
            lbSourceTypes.Items.Add("Waste Pile");

        }

        public void LoadScienceModules()
        {
            lbScienceModules.Items.Add("Air");
            lbScienceModules.Items.Add("Aquatic Food Web");
            lbScienceModules.Items.Add("Aquifer");
            lbScienceModules.Items.Add("Ecological Exposure");
            lbScienceModules.Items.Add("Ecological Risk");            
            lbScienceModules.Items.Add("Farm Food Chain");
            lbScienceModules.Items.Add("Human Exposure");
            lbScienceModules.Items.Add("Human Risk");
            lbScienceModules.Items.Add("Terrestrial Food Web");
            lbScienceModules.Items.Add("Surface Water");
            lbScienceModules.Items.Add("Vadose Zone");
            lbScienceModules.Items.Add("Watershed");

        }


        public void LoadParameters(SDPParameters parameters)
        {
            txtSourceName.Text = parameters.SourceName;
            
            if (!String.IsNullOrEmpty(parameters.State))
            {            
                cboState.SelectedValue = parameters.State;
            }
            if (!String.IsNullOrEmpty(parameters.County))
            {
                cboCounty.SelectedValue = parameters.County;
            }
            if (!String.IsNullOrEmpty(parameters.HUC))
            {
                cboHUC.SelectedValue = parameters.HUC;
            }
            txtLatitude.Text = parameters.Latitude;
            txtLongitude.Text = parameters.Longitude;

            //load AOI Shape file
            if (!String.IsNullOrEmpty(parameters.AOIFileName))
            {
                LoadAndSymbolizeShapeFile(parameters.AOIFileName, LAYER_NAME_AOI);
            }

            //load Source Shape file
            if (!String.IsNullOrEmpty(parameters.SourceFileName))
            {
                LoadAndSymbolizeShapeFile(parameters.SourceFileName, LAYER_NAME_SOURCE);
                ZoomToSource();
            }

            switch (parameters.AOIType)
            {
                case AOI_TYPE_POLYGON: rbPolygon.Checked = true; break;
                case AOI_TYPE_CIRCLE: rbCircle.Checked = true; break;
                case AOI_TYPE_BUFFER: rbBuffer.Checked = true; break;
                case AOI_TYPE_SHAPE: rbShape.Checked = true; break;       
            }

            txtProjectFolder.Text = parameters.ProjectFolder;
            txtCacheFolder.Text = parameters.CacheFolder;
            txtIntermediateFolder.Text = parameters.IntermediateFolder;

            txtNHDPlusLocation.Text = parameters.NHDPlusLocation;
            txtNLCDLocation.Text = parameters.NLCDLocation;
            txtSoilsLocation.Text = parameters.SoilsLocation;
            chkDisregardCacheNHDPlus.Checked = Convert.ToBoolean(parameters.NHDPlusDisregardCache);
            chkDisregardCacheNLCD.Checked = Convert.ToBoolean(parameters.NLCDDisregardCache);
            chkDisregardCacheSoils.Checked = Convert.ToBoolean(parameters.SoilsDisregardCache);

            txtServer.Text = parameters.Server;
            txtUsername.Text = parameters.Username;
            txtPassword.Text = parameters.Password;
            txtPort.Text = parameters.Port;
            txtLengthOfTimeout.Text = parameters.LengthOfTimeout;
            txtNumberOfRetries.Text = parameters.NumberOfRetries;
            if (!String.IsNullOrEmpty(parameters.DatabaseName))
            {
                cboDatabaseName.Items.Clear();
                cboDatabaseName.Items.Add(parameters.DatabaseName);
                cboDatabaseName.SelectedIndex = 0;
            }

            chkForceFullDependency.Checked = Convert.ToBoolean(parameters.ForceFullDependency);
            foreach (string s in parameters.SourceTypes)
            {
                lbSourceTypes.SelectedItem = s;
            }
            foreach (string s in parameters.ScienceModules)
            {
                lbScienceModules.SelectedItem = s;
            }

            

        }

        public SDPParameters GetParameters()
        {
            SDPParameters parameters = new SDPParameters();
            parameters.SourceName = txtSourceName.Text.Trim();
            parameters.SourceName = txtSourceName.Text.Trim();
            if (cboState.SelectedValue != null)
            {
                parameters.State = cboState.SelectedValue.ToString();
            }
            if (cboCounty.SelectedValue != null)
            {
                parameters.County = cboCounty.SelectedValue.ToString();
            }
            if (cboHUC.SelectedValue != null)
            {
                parameters.HUC = cboHUC.SelectedValue.ToString();
            }
            parameters.Latitude = txtLatitude.Text.Trim();
            parameters.Longitude = txtLongitude.Text.Trim();

            //get source file name from map
            parameters.SourceFileName = "";
            //get aoi shapfile from map
            parameters.AOIFileName = "";

            if (rbPolygon.Checked)
            {
                parameters.AOIType = AOI_TYPE_POLYGON;
            }
            if (rbCircle.Checked)
            {
                parameters.AOIType = AOI_TYPE_CIRCLE;
            }
            if (rbBuffer.Checked)
            {
                parameters.AOIType = AOI_TYPE_BUFFER;
            }
            if (rbShape.Checked)
            {
                parameters.AOIType = AOI_TYPE_SHAPE;
            }

            parameters.ProjectFolder = txtProjectFolder.Text.Trim();
            parameters.CacheFolder = txtCacheFolder.Text.Trim();
            parameters.IntermediateFolder = txtIntermediateFolder.Text.Trim();

            parameters.NHDPlusLocation = txtNHDPlusLocation.Text.Trim();
            parameters.NLCDLocation = txtNLCDLocation.Text.Trim();
            parameters.SoilsLocation = txtSoilsLocation.Text.Trim();
            parameters.NHDPlusDisregardCache = chkDisregardCacheNHDPlus.Checked.ToString();
            parameters.NLCDDisregardCache = chkDisregardCacheNLCD.Checked.ToString();
            parameters.SoilsDisregardCache = chkDisregardCacheSoils.Checked.ToString();

            parameters.Server= txtServer.Text;
            parameters.Username= txtUsername.Text;
            parameters.Password = txtPassword.Text;
            parameters.Port = txtPort.Text;
            parameters.LengthOfTimeout = txtLengthOfTimeout.Text;
            parameters.NumberOfRetries = txtNumberOfRetries.Text;
            if (!String.IsNullOrEmpty(cboDatabaseName.Text.Trim()))
            {
                parameters.DatabaseName = cboDatabaseName.Text.Trim();
            }


            parameters.ForceFullDependency = chkForceFullDependency.Checked.ToString();
            foreach (Object li in lbSourceTypes.SelectedItems)
            {
                string s = li.ToString();
                parameters.SourceTypes.Add(s);            
            }
            foreach (Object li in lbScienceModules.SelectedItems)
            {
                string s = li.ToString();
                parameters.ScienceModules.Add(s);
            }

            return parameters;       
                
        }

        private void frmHE2RMESProject_Load(object sender, EventArgs e)
        {
            //load map
            LoadAndSymbolizeHUC();
            LoadAndSymbolizeCounties();
            LoadAndSymbolizeStates();
            //load combo boxes
            LoadStateNames();
            //don't need to load counties, counties will be loaded
            //on cboState text changed event triggered by setting cboState.SelectedIndex
            //in LoadStateNames()
            //LoadCountyNames(); 
            LoadHUCNames();

        }

        private void LoadStateNames()
        {
            if (_stateFeatures != null)
            {
                //get data table - only first time
                if (_dtStates == null)
                {
                    _dtStates = _stateFeatures.DataTable;
                }
                //filter by state
                DataView dv = new DataView(_dtStates);
                dv.Sort = SHP_STATE_NAME_FIELD + " ASC";
                cboState.DataSource = dv;
                cboState.DisplayMember = SHP_STATE_NAME_FIELD;
                //would prefer to use shp_index as value,
                //but we need the state name to get the counties that fall within this state
                cboState.ValueMember = SHP_STATE_ST_FIELD;
                cboState.SelectedIndex = -1;
            }
           
        }

        private void LoadCountyNames()
        {
            if (_countyFeatures != null)
            {
                //get data table - only first time
                if (_dtCounties == null)
                {
                    _dtCounties = _countyFeatures.DataTable;
                }
                //filter by state
                DataView dv = new DataView(_dtCounties);
                if (String.IsNullOrEmpty(cboState.Text) == false)
                {
                    dv.RowFilter =  SHP_COUNTY_STATE_FIELD + " = '" + cboState.SelectedValue + "'";
                }
                dv.Sort = SHP_COUNTY_CNTYNAME_FIELD + " ASC";
                cboCounty.DataSource = dv;
                cboCounty.DisplayMember = SHP_COUNTY_CNTYNAME_FIELD;
                //would prefer to use shp_index as value,
                //but we need the state name to get the counties that fall within this state
                cboCounty.ValueMember = SHP_COUNTY_FIPS_FIELD;
                cboCounty.SelectedIndex = -1;
            }

        }

        private void LoadHUCNames()
        {
            if (_hucFeatures != null)
            {
                //get data table - only first time
                if (_dtHucs == null)
                {
                    _dtHucs = _hucFeatures.DataTable;
                }
                //filter by state
                DataView dv = new DataView(_dtHucs);
                dv.Sort = SHP_HUC_HUC_FIELD + " ASC";
                cboHUC.DataSource = dv;
                cboHUC.DisplayMember = SHP_HUC_HUC_FIELD;
                //would prefer to use shp_index as value,
                //but we need the state name to get the counties that fall within this state
                cboHUC.ValueMember = SHP_HUC_HUC_FIELD;
                cboHUC.SelectedIndex = -1;
            }

        }




        private bool LayerExists(IMapLayer layer)
        {
            foreach (IMapLayer l in SDPProjectBuilderPlugin_GUI.Map.Layers)
            {
                if (l.LegendText.ToLower() == layer.LegendText.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private IMapLayer   GetLayerByLegendText(string legendText)
        {
            foreach (IMapLayer l in SDPProjectBuilderPlugin_GUI.Map.Layers)
            {
                if (l.LegendText.ToLower() == legendText.ToLower())
                {
                    return l;
                }
            }
            return null;
        }

     

        private void LoadAndSymbolizeCounties()
        {
            string fileName = Directory.GetCurrentDirectory() + @"\Plugins\SDPProjectBuilder\BaseLayers\cnty.shp";

            if (File.Exists(fileName))
            {
                _countyFeatures = FeatureSet.OpenFile(fileName);

                IMapPolygonLayer impl = new MapPolygonLayer(_countyFeatures);

                impl.Reproject(KnownCoordinateSystems.Projected.World.WebMercator);
                impl.LegendText = "Counties";
                impl.Symbolizer.SetFillColor(Color.Transparent);
                impl.Symbolizer.SetOutlineWidth(1);
                impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Green);

                //load layer if not already loaded
                if (!LayerExists(impl))
                {
                    SDPProjectBuilderPlugin_GUI.Map.Layers.Add(impl);
                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = impl.Extent;
                }
            }
        }

        private void LoadAndSymbolizeStates()
        {

            string fileName = Directory.GetCurrentDirectory() + @"\Plugins\SDPProjectBuilder\BaseLayers\st.shp";

            if (File.Exists(fileName))
            {
                _stateFeatures = FeatureSet.OpenFile(fileName);

                IMapPolygonLayer impl = new MapPolygonLayer(_stateFeatures);

                impl.Reproject(KnownCoordinateSystems.Projected.World.WebMercator);
                impl.LegendText = "States";
                impl.Symbolizer.SetFillColor(Color.Transparent);
                impl.Symbolizer.SetOutlineWidth(1.5);
                impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Blue);

                //load layer if not already loaded
                if (!LayerExists(impl))
                {
                    SDPProjectBuilderPlugin_GUI.Map.Layers.Add(impl);
                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = impl.Extent;
                }
            }
        }

        private void LoadAndSymbolizeHUC()
        {
            string fileName = Directory.GetCurrentDirectory() + @"\Plugins\SDPProjectBuilder\BaseLayers\huc250d3.shp";

            if (File.Exists(fileName))
            {
                _hucFeatures = FeatureSet.OpenFile(fileName);

                IMapPolygonLayer impl = new MapPolygonLayer(_hucFeatures);

                impl.Reproject(KnownCoordinateSystems.Projected.World.WebMercator);
                impl.LegendText = "huc250d3";
                impl.Symbolizer.SetFillColor(Color.Transparent);
                impl.Symbolizer.SetOutlineWidth(.5);
                impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Black);

                //load layer if not already loaded
                if (!LayerExists(impl))
                {
                    SDPProjectBuilderPlugin_GUI.Map.Layers.Add(impl);
                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = impl.Extent;
                }
            }
        }

        private void LoadAndSymbolizeShapeFile(string fileName, string sLegendText)
        {

            if (File.Exists(fileName))
            {                
                IFeatureSet fs = FeatureSet.OpenFile(fileName);

                IMapPolygonLayer impl = new MapPolygonLayer(fs);

                impl.Reproject(KnownCoordinateSystems.Projected.World.WebMercator);
                impl.LegendText = sLegendText;
                //impl.Symbolizer.SetFillColor(Color.Transparent);
                //impl.Symbolizer.SetOutlineWidth(1.5);
                //impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Blue);

                //load layer if not already loaded
                if (!LayerExists(impl))
                {
                    SDPProjectBuilderPlugin_GUI.Map.Layers.Add(impl);
                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = impl.Extent;
                }
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("By closing this form any changes to the current project will be lost.  Do you wish to continue?",
                                           "Confirm Close", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }

        }

        

        private void btnBrowseProjectFolder_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtProjectFolder);
        }

        private void btnBrowseCacheFolder_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtCacheFolder);
        }

        private void btnBrowseIntermediateFolder_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtIntermediateFolder);
        }

        private void btnBrowseNHDPlusLocation_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtNHDPlusLocation);
        }

        private void btnBrowseNLCDLocation_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtNLCDLocation);
        }

        private void btnBrowseSoilsLocation_Click(object sender, EventArgs e)
        {
            SDPProjectBuilderPlugin_GUI.BrowseToFolder(txtSoilsLocation);
        }

        private void cboState_TextChanged(object sender, EventArgs e)
        {
            LoadCountyNames();
        }

        private void btnZoomToState_Click(object sender, EventArgs e)
        {
            //bool retBool = false;
            int index = -1;

            //if they typed in the value, then the selected index not updated.
            string st = cboState.Text;
            if (!String.IsNullOrEmpty(st))
            {
                index = cboState.FindString(st);
                if (index > -1)
                {
                    cboState.SelectedIndex = index;
                }
                else
                {
                    MessageBox.Show("Cannot find State entered.", "Zoom To State");
                    return;
                }
            }
            else 
            {
                MessageBox.Show("State cannot be empty.", "Zoom To State");
                return;
            }
            

            if (_stateFeatures != null)
            {
                //populate feature lookup
                if (_stateFeatures.FeatureLookup.Count == 0) 
                {
                    foreach (Feature f in _stateFeatures.Features)
                    {
                        _stateFeatures.FeatureLookup.Add(f.DataRow, f);
                    }
                }

                //zoom to extent
                List<IFeature> shps = _stateFeatures.SelectByAttribute(SHP_STATE_ST_FIELD + " = '" + cboState.SelectedValue + "'");
                if (shps.Count > 0)
                {
                    IEnvelope myENV = new Envelope();
                    foreach (IFeature feat in shps)
                    {
                        myENV.ExpandToInclude(feat.Envelope);
                    }

                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = myENV.ToExtent();
                    SDPProjectBuilderPlugin_GUI.Map.Refresh();
                }
                else
                {
                    return;
                }
            }


        }

        private void btnZoomToCounty_Click(object sender, EventArgs e)
        {
            //bool retBool = false;
            int index = -1;

            //if they typed in the value, then the selected index not updated.
            string cnty = cboCounty.Text;
            if (!String.IsNullOrEmpty(cnty))
            {
                index = cboCounty.FindString(cnty);
                if (index > -1)
                {
                    cboCounty.SelectedIndex = index;
                }
                else
                {
                    MessageBox.Show("Cannot find County entered.", "Zoom To County");
                    return;
                }
            }
            else 
            {
                MessageBox.Show("County cannot be empty.", "Zoom To County");
                return;
            }

            if (_countyFeatures != null)
            {
                //populate feature lookup
                if (_countyFeatures.FeatureLookup.Count == 0)
                {
                    foreach (Feature f in _countyFeatures.Features)
                    {
                        _countyFeatures.FeatureLookup.Add(f.DataRow, f);
                    }
                }

                //zoom to extent
                List<IFeature> shps = _countyFeatures.SelectByAttribute(SHP_COUNTY_FIPS_FIELD + " = '" + cboCounty.SelectedValue + "'");
                if (shps.Count > 0)
                {
                    IEnvelope myENV = new Envelope();
                    foreach (IFeature feat in shps)
                    {
                        myENV.ExpandToInclude(feat.Envelope);
                    }

                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = myENV.ToExtent();
                    SDPProjectBuilderPlugin_GUI.Map.Refresh();
                }
                else
                {
                    return;
                }
            }

        }

        private void btnZoomToHUC_Click(object sender, EventArgs e)
        {
            //bool retBool = false;
            int index = -1;

            //if they typed in the value, then the selected index not updated.
            string cnty = cboHUC.Text;
            if (!String.IsNullOrEmpty(cnty))
            {
                index = cboHUC.FindString(cnty);
                if (index > -1)
                {
                    cboHUC.SelectedIndex = index;
                }
                else
                {
                    MessageBox.Show("Cannot find HUC entered.", "Zoom To HUC");
                    return;
                }
            }
            else
            {
                MessageBox.Show("HUC cannot be empty.", "Zoom To HUC");
                return;
            }

            if (_hucFeatures != null)
            {
                //populate feature lookup
                if (_hucFeatures.FeatureLookup.Count == 0)
                {
                    foreach (Feature f in _hucFeatures.Features)
                    {
                        _hucFeatures.FeatureLookup.Add(f.DataRow, f);
                    }
                }

                //zoom to extent
                List<IFeature> shps = _hucFeatures.SelectByAttribute(SHP_HUC_HUC_FIELD + " = '" + cboHUC.SelectedValue + "'");
                if (shps.Count > 0)
                {
                    IEnvelope myENV = new Envelope();
                    foreach (IFeature feat in shps)
                    {
                        myENV.ExpandToInclude(feat.Envelope);
                    }

                    SDPProjectBuilderPlugin_GUI.Map.ViewExtents = myENV.ToExtent();
                    SDPProjectBuilderPlugin_GUI.Map.Refresh();
                }
                else
                {
                    return;
                }
            }

        }

        private void ToGeographic(ref double mercatorX_lon, ref double mercatorY_lat)
        {
            if (Math.Abs(mercatorX_lon) < 180 && Math.Abs(mercatorY_lat) < 90)
                return;

            if ((Math.Abs(mercatorX_lon) > 20037508.3427892) || (Math.Abs(mercatorY_lat) > 20037508.3427892))
                return;

            double x = mercatorX_lon;
            double y = mercatorY_lat;
            double num3 = x / 6378137.0;
            double num4 = num3 * 57.295779513082323;
            double num5 = Math.Floor((double)((num4 + 180.0) / 360.0));
            double num6 = num4 - (num5 * 360.0);
            double num7 = 1.5707963267948966 - (2.0 * Math.Atan(Math.Exp((-1.0 * y) / 6378137.0)));
            mercatorX_lon = num6;
            mercatorY_lat = num7 * 57.295779513082323;
        }

        private void ToWebMercator(ref double mercatorX_lon, ref double mercatorY_lat)
        {
            if ((Math.Abs(mercatorX_lon) > 180 || Math.Abs(mercatorY_lat) > 90))
                return;

            double num = mercatorX_lon * 0.017453292519943295;
            double x = 6378137.0 * num;
            double a = mercatorY_lat * 0.017453292519943295;

            mercatorX_lon = x;
            mercatorY_lat = 3189068.5 * Math.Log((1.0 + Math.Sin(a)) / (1.0 - Math.Sin(a)));
        }


        private void btnZoomToCoordinates_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtLatitude.Text) )
            {
                MessageBox.Show("Latitude cannot be empty.", "Zoom To Coordinates");
                return;
            }
            if (String.IsNullOrEmpty(txtLongitude.Text))
            {
                MessageBox.Show("Longitude cannot be empty.", "Zoom To Coordinates");
                return;
            }

            double lat_degrees;
            if (!Double.TryParse(txtLatitude.Text, out lat_degrees))
            {
                MessageBox.Show("Latitude must be numeric.", "Zoom To Coordinates");
                return;            
            }

            if (lat_degrees < -90.0 || lat_degrees > 90.0)
            {
                MessageBox.Show("Latitude must be between -90 and +90 degrees.", "Zoom To Coordinates");
                return;
            }

            double lon_degrees;
            if (!Double.TryParse(txtLongitude.Text, out lon_degrees))
            {
                MessageBox.Show("Longitude must be numeric.", "Zoom To Coordinates");
                return;
            }
            if (lon_degrees < -180.0 || lon_degrees > 180.0)
            {
                MessageBox.Show("Longitude must be between -180 and +180 degrees.", "Zoom To Coordinates");
                return;
            }

            ProjectionInfo destProj = SDPProjectBuilderPlugin_GUI.Map.Projection;
            string startProj = "+proj=longlat +ellps=WGS84 +no_defs";
            //_ds.Log.LogDebug("GUI", "navigating to point using destination projection: " + destProj);
            try
            {
                double metersX = lon_degrees;
                double metersY = lat_degrees;
                ToWebMercator(ref metersX, ref metersY);
                //MapWinGeoProc.SpatialReference.ProjectPoint(ref myX, ref myY, startProj, destProj);             
                DotSpatial.Topology.Point p = new DotSpatial.Topology.Point(metersX, metersY);
                Feature f = new Feature(p); //Buffer(10000)
                FeatureSet fs = new FeatureSet();
                fs.FeatureType = FeatureType.Point;
                fs.Features.Add(f);
                fs.Projection = destProj;//KnownCoordinateSystems.Geographic.World.WGS1984;
                //fs.Projection = ProjectionInfo.FromProj4String(startProj);
                //fs.Reproject(destProj);

                SDPProjectBuilderPlugin_GUI.Map.ViewExtents = fs.Extent;
                SDPProjectBuilderPlugin_GUI.Map.Refresh();
            }
            catch (Exception ex)
            {
                //_ds.Log.LogError("GUI", "Error reprojecting zoom-to location from degrees to map projection. " + ex.Message);
                return;
            }


        }

        private void btnEditSource_Click(object sender, EventArgs e)
        {
            btnEditSource.Enabled = false;
            btnCancelSource.Enabled = true;
            btnUndoPointSource.Enabled = true;
            btnSavePolygonSource.Enabled = true;

            //remove current source if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_SOURCE);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();

            //open up a drawing layer
            FeatureSet fs = null;
            fs = new FeatureSet(FeatureType.Polygon);
            fs.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;
            fs.Name = LAYER_NAME_SOURCE;

            if (_drawPolygonSource == null)
            {
                _drawPolygonSource = new SDPProjectBuilderPolygonDrawer(SDPProjectBuilderPlugin_GUI.Map);
                _drawPolygonSource.Name = "AddPolySource";
            }
            if (SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Contains(_drawPolygonSource) == false)
            {
                SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Add(_drawPolygonSource);
            }

            _drawPolygonSource.FeatureSet = fs;
            _drawPolygonSource.Activate();
            IFeatureList ifl = _drawPolygonSource.FeatureSet.Features;
            ifl.FeatureAdded += new System.EventHandler<DotSpatial.Data.FeatureEventArgs>(this.featureAdded);
            _drawPolygonSource.MouseUp += new System.EventHandler<DotSpatial.Controls.GeoMouseArgs>(this.coordAdded);
        }

        private void featureAdded(object sender, EventArgs e)
        {

            //enable the save button
            btnSavePolygonSource.Enabled = true;
            //LAYER_NAME_SOURCE = txtDesc.Text;
            addSrcLayerGivenFS(_drawPolygonSource.FeatureSet, LAYER_NAME_SOURCE);
        }

        private void coordAdded(object sender, EventArgs e)
        {
            if (_drawPolygonSource.Coordinates.Count > 0)
            {
                btnClearDrawingSource.Enabled = true;
                btnUndoPointSource.Enabled = true;
            }
            else
            {
                //btnClear.Enabled = false;
                btnUndoPointSource.Enabled = false;
            }

        }

        private void featureAddedPolyAOI(object sender, EventArgs e)
        {

            //enable the save button
            btnSavePolygonAOIPolygon.Enabled = true;
            //LAYER_NAME_SOURCE = txtDesc.Text;
            addAOILayerGivenFS(_drawPolygonAOI.FeatureSet, LAYER_NAME_AOI);
        }

        private void coordAddedAOIPolygon(object sender, EventArgs e)
        {
            if (_drawPolygonAOI.Coordinates.Count > 0)
            {
                btnClearDrawingAOIPolygon.Enabled = true;
                btnUndoPointAOIPolygon.Enabled = true;
            }
            else
            {
                btnUndoPointAOIPolygon.Enabled = false;
            }

        }

        private void featureAddedCircleAOI(object sender, EventArgs e)
        {

            //enable the save button
            btnSaveCircleAOICircle.Enabled = true;
            //LAYER_NAME_SOURCE = txtDesc.Text;
            addAOILayerGivenFS(_drawCircleAOI.FeatureSet, LAYER_NAME_AOI);
        }


        private void coordAddedAOICircle(object sender, EventArgs e)
        {

            if (_drawCircleAOI.Coordinates.Count > 0)
            {
                btnClearDrawingAOICircle.Enabled = true;
                btnUndoPointAOICircle.Enabled = true;
            }
            else
            {
                btnUndoPointAOICircle.Enabled = false;
            }


        }

        private void btnClearDrawingSource_Click(object sender, EventArgs e)
        {
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_SOURCE);
            if (layerSource != null)
            {
                if (_drawPolygonSource != null)
                {
                    //_drawPolygonAOI.FeatureSet.Features.Clear();
                    _drawPolygonSource = null;
                }
                //remove the layer
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }
            else
            {
                if (_drawPolygonSource != null)
                {
                    _drawPolygonSource.Coordinates.Clear();
                }
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnUndoPointSource_Click(object sender, EventArgs e)
        {   
            if (_drawPolygonSource.Coordinates.Count > 0)
            {
                _drawPolygonSource.Coordinates.RemoveAt(_drawPolygonSource.Coordinates.Count - 1);
            }
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnSavePolygonSource_Click(object sender, System.EventArgs e)
        {
            if (_drawPolygonSource.saveDrawing())
            {
                _drawPolygonSource.Deactivate();

                btnEditSource.Enabled = true;
                btnCancelSource.Enabled = false;
                btnSavePolygonSource.Enabled = false;
                btnUndoPointSource.Enabled = false;               
            }
        }

        private void btnClearDrawingAOIPolygon_Click(object sender, EventArgs e)
        {
            IMapLayer layerAOI = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerAOI != null)
            {
                if (_drawPolygonAOI != null)
                {
                    //_drawPolygonAOI.FeatureSet.Features.Clear();
                    _drawPolygonAOI = null;
                }
                //remove the layer
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerAOI);
            }
            else
            {
                if (_drawPolygonAOI != null)
                {
                    _drawPolygonAOI.Coordinates.Clear();
                }
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnUndoPointAOIPolygon_Click(object sender, EventArgs e)
        {
            if (_drawPolygonAOI.Coordinates.Count > 0)
            {
                _drawPolygonAOI.Coordinates.RemoveAt(_drawPolygonAOI.Coordinates.Count - 1);
            }
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnSavePolygonAOIPolygon_Click(object sender, System.EventArgs e)
        {
            if (_drawPolygonAOI.saveDrawing())
            {
                _drawPolygonAOI.Deactivate();

                btnEditAOIPolygon.Enabled = true;
                btnCancelAOIPolygon.Enabled = false;
                btnSavePolygonAOIPolygon.Enabled = false;
                btnUndoPointAOIPolygon.Enabled = false;
            }
        }


        private IMapFeatureLayer addSrcLayerGivenFS(IFeatureSet fs, String name)
        {
            //remove current source if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_SOURCE);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }

            IMapFeatureLayer layer = null;
            //fs.Reproject(_geoMap.Projection);
            layer = SDPProjectBuilderPlugin_GUI.Map.Layers.Add(fs);
            layer.LegendText = name;
            MapPolygonLayer impl = (MapPolygonLayer)layer;
            impl.Symbolizer.SetFillColor(Color.Brown);
            impl.Symbolizer.SetOutlineWidth(1);
            impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Green);
            SDPProjectBuilderPlugin_GUI.Map.Layers.SelectedLayer = layer;
            return layer;
        }

        private IMapFeatureLayer addAOILayerGivenFS(IFeatureSet fs, String name)
        {
            //remove current AOI if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }

            IMapFeatureLayer layer = null;
            //fs.Reproject(_geoMap.Projection);
            layer = SDPProjectBuilderPlugin_GUI.Map.Layers.Add(fs);
            layer.LegendText = name;
            MapPolygonLayer impl = (MapPolygonLayer)layer;
            impl.Symbolizer.SetFillColor(Color.Transparent);
            impl.Symbolizer.SetOutlineWidth(2);
            impl.Symbolizer.OutlineSymbolizer.SetFillColor(Color.Blue);
            SDPProjectBuilderPlugin_GUI.Map.Layers.SelectedLayer = layer;
            return layer;
        }

        private void ZoomToSource()
        {
            IFeatureSet fs = GetFeatureSetSource();

            if (fs != null)
            {
                //zoom to extent               
                if (fs.Features.Count > 0)
                {
                    List<IFeature> shps = new List<IFeature>();  
                    shps.Add(fs.Features[0]);
                    if (shps.Count > 0)
                    {
                        IEnvelope myENV = new Envelope();
                        foreach (IFeature feat in shps)
                        {
                            myENV.ExpandToInclude(feat.Envelope);
                        }

                        SDPProjectBuilderPlugin_GUI.Map.ViewExtents = myENV.ToExtent();
                        SDPProjectBuilderPlugin_GUI.Map.Refresh();
                    }
                }
            }
        }


        private void hideAOIControls()
        {
            //btnBufferSource.Visible = false;
            //btn_makeCircle.Visible = false;
            //lblRadius.Visible = false;
            //txtRadius.Visible = false;
            //lbl_source.Visible = false;
            //cmb_source.Visible = false;
            //lblTip.Visible = false;
            
            //lbl_select.Visible = false;
            //btn_selection.Visible = false;

            groupAOIPolygon.Visible = false;
            //btnSavePolygonAOI.Visible = false;
            //btnClearDrawingAOI.Visible = false;
            //btnUndoPointAOI.Visible = false;
            //btnEditAOI.Visible = false;
            
            groupAOICircle.Visible = false;

            groupAOIBuffer.Visible = false;

            groupAOIShape.Visible = false;

        }

        private void rbPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPolygon.Checked)
            {
                hideAOIControls();
                groupAOIPolygon.Visible = true;
                btnEditAOIPolygon.Enabled = true;
                btnCancelAOIPolygon.Enabled = false;
                btnClearDrawingAOIPolygon.Enabled = true;
                btnUndoPointAOIPolygon.Enabled = false;
                btnSavePolygonAOIPolygon.Enabled = false;
            }

            if (_drawPolygonAOI != null)
            {
                _drawPolygonAOI.Coordinates.Clear();
                _drawPolygonAOI.Deactivate();
                _drawPolygonAOI = null;
            }           

        }

        private void rbCircle_CheckedChanged(object sender, EventArgs e)
        {  
            if (rbCircle.Checked)
            {
                hideAOIControls();
                groupAOICircle.Visible = true;
                btnEditAOICircle.Enabled = true;
                btnCancelAOICircle.Enabled = false;
                btnClearDrawingAOICircle.Enabled = true;
                btnUndoPointAOICircle.Enabled = false;
                btnSaveCircleAOICircle.Enabled = false;                
            }

            if (_drawCircleAOI != null)
            {
                _drawCircleAOI.Coordinates.Clear();
                _drawCircleAOI.Deactivate();
                _drawCircleAOI = null;
            }   

        }

        private void rbBuffer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBuffer.Checked)
            {
                hideAOIControls();

                groupAOIBuffer.Visible = true;

            }
        }



        private void rbShape_CheckedChanged(object sender, EventArgs e)
        {

            if (rbShape.Checked)
            {
                hideAOIControls();
                groupAOIShape.Visible = true;
                btnEditAOIShape.Enabled = true;
                btnCancelAOIShape.Enabled = false;
                btnClearDrawingAOIShape.Enabled = true;
                btnSavePolygonAOIShape.Enabled = false;
            }

            if (_drawShapeAOI != null)
            {
                //_drawShapeAOI.Coordinates.Clear();
                _drawShapeAOI.Deactivate();
                _drawShapeAOI = null;
            }        



            
        }

        private void btnEditAOIPolygon_Click(object sender, EventArgs e)
        {
            btnEditAOIPolygon.Enabled = false;
            btnCancelAOIPolygon.Enabled = true;
            btnUndoPointAOIPolygon.Enabled = true;
            btnSavePolygonAOIPolygon.Enabled = true;

            //remove current AOI if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();

            //open up a drawing layer
            FeatureSet fs = null;
            fs = new FeatureSet(FeatureType.Polygon);
            fs.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;
            fs.Name = LAYER_NAME_AOI;

            if (_drawPolygonAOI == null)
            {
                _drawPolygonAOI = new SDPProjectBuilderPolygonDrawer(SDPProjectBuilderPlugin_GUI.Map);
                _drawPolygonAOI.Name = "AddPolyAOI";
            }
            if (SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Contains(_drawPolygonAOI) == false)
            {
                SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Add(_drawPolygonAOI);
            }

            _drawPolygonAOI.FeatureSet = fs;
            _drawPolygonAOI.Activate();
            IFeatureList ifl = _drawPolygonAOI.FeatureSet.Features;
            ifl.FeatureAdded += new System.EventHandler<DotSpatial.Data.FeatureEventArgs>(this.featureAddedPolyAOI);
            _drawPolygonAOI.MouseUp += new System.EventHandler<DotSpatial.Controls.GeoMouseArgs>(this.coordAddedAOIPolygon);
        }

        private void btnSaveCircle_Click(object sender, EventArgs e)
        {
            try
            {              
                //remove current AOI if there is one:
                IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
                if (layerSource != null)
                {
                    SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
                }
                FeatureSet fs = new FeatureSet(FeatureType.Point);
                fs.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;

                //double x = _drawCircle.Coordinates[0].X;

                //double y = _drawCircle.Coordinates[0].Y;
                ////IBasicGeometry myGeo = new MapWindow.Geometries.Point(x, y);
                //IBasicGeometry myGeo = new DotSpatial.Topology.Point(x, y);
                //fs.AddFeature(myGeo);

                //int radius;
                //radius = Convert.ToInt32(txtRadius.Text);

                //IFeatureSet circle = fs.Buffer(radius, false);
                //circle.Projection = _geoMap.Projection;
                //CommonOperations.AddAdditionalAOIFields(circle, _ds.Log, _ds);
                //CommonOperations.SaveAOIValues(layerName, null, circle.Features[0], getMethod(), _ds, status);
                //status.update("Done Interting records", 50);
                //String directory = _ds.Configuration.getParameter("AOI_DIRECTORY") + Path.DirectorySeparatorChar;

                //try
                //{
                //    if (!FileOperations.createDirs(directory, _ds.Log))
                //    {
                //        _ds.Log.Error("GUI   Unable to create AOI directory for vector data.");
                //        return;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    _ds.Log.ErrorFormat("GUI   Unable to create directory: {0}, Reason: {1}", directory, ex.Message);
                //}



                //circle.SaveAs(directory + _ds.Configuration.getParameter("AOI_FILE_NAME_WITH_EXTENSION"), true);
                //status.update("Done Saving Layer", 90);
                //IFeatureSet fsAOI = FeatureSet.OpenFile(directory + _ds.Configuration.getParameter("AOI_FILE_NAME_WITH_EXTENSION"));
                //fsAOI.Name = layerName;
                //fsAOI.Projection = _geoMap.Projection;
                //IMapFeatureLayer layer = _geoMap.Layers.Add(fsAOI);
                //symbolizeAOILayer();

                //_geoMap.Layers.SelectedLayer = layer;

                ////_geoMap.Extents = fsAOI.Envelope;
                //_geoMap.ViewExtents = fsAOI.Extent;
                //_geoMap.Invalidate();
                //status.update("Done", 100);
                //_ds.Catalog.UpdateUseCaseStatus(CommonOperations.UCNAME, _ds.Configuration.getParameter("SUCCESS_STRING"));

            }
            catch (Exception ex)
            {
                //_ds.Log.ErrorFormat("GUI   Please give an integer radius, Reason: {0}", ex.Message);
                return;
            }

        }

        private void btnEditAOICircle_Click(object sender, EventArgs e)
        {
            btnEditAOICircle.Enabled = false;
            btnCancelAOICircle.Enabled = true;
            btnUndoPointAOICircle.Enabled = true;
            btnSaveCircleAOICircle.Enabled = true;

            //check that we have a radius and name
            try
            {
                Int32.Parse(txtRadius.Text);
            }
            catch (Exception ex)
            {
                txtRadius.Text = "1000";
                btnEditAOICircle.Enabled = true;
                return;
            }

            //remove current AOI if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();

            //open up a drawing layer
            FeatureSet fs = null;
            fs = new FeatureSet(FeatureType.Polygon);
            fs.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;
            fs.Name = LAYER_NAME_AOI;


            if (_drawCircleAOI == null)
            {
                _drawCircleAOI = new SDPProjectBuilderCircleDrawer(SDPProjectBuilderPlugin_GUI.Map);
                _drawCircleAOI.Name = "AddCircle";
            }

            if (SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Contains(_drawCircleAOI) == false)
            {
                SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Add(_drawCircleAOI);
            }

            _drawCircleAOI.Radius = txtRadius.Text;
            _drawCircleAOI.Activate();
            _drawCircleAOI.FeatureSet = fs;
            IFeatureList ifl = _drawCircleAOI.FeatureSet.Features;
            ifl.FeatureAdded += new System.EventHandler<DotSpatial.Data.FeatureEventArgs>(this.featureAddedCircleAOI);
            _drawCircleAOI.MouseUp += new System.EventHandler<DotSpatial.Controls.GeoMouseArgs>(this.coordAddedAOICircle);


        }

        private void btnClearDrawingAOICircle_Click(object sender, EventArgs e)
        {

            IMapLayer layerAOI = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerAOI != null)
            {
                if (_drawCircleAOI != null)
                {
                    //_drawPolygonAOI.FeatureSet.Features.Clear();
                    _drawCircleAOI = null;
                }
                //remove the layer
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerAOI);
            }
            else
            {
                if (_drawCircleAOI != null)
                {
                    _drawCircleAOI.Coordinates.Clear();
                }
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();

        }

        private void btnUndoPointAOICircle_Click(object sender, EventArgs e)
        {
            if (_drawCircleAOI.Coordinates.Count > 0)
            {
                _drawCircleAOI.Coordinates.RemoveAt(_drawCircleAOI.Coordinates.Count - 1);
            }
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnSaveCircleAOICircle_Click(object sender, EventArgs e)
        {
            if (_drawCircleAOI.saveDrawing())
            {
                _drawCircleAOI.Deactivate();

                btnEditAOICircle.Enabled = true;
                btnCancelAOICircle.Enabled = false;
                btnSaveCircleAOICircle.Enabled = false;
                btnUndoPointAOICircle.Enabled = false;
            }
        }

        private void btnCancelSource_Click(object sender, EventArgs e)
        {
            if (_drawPolygonSource != null)
            {
                _drawPolygonSource.Coordinates.Clear();
                _drawPolygonSource.Deactivate();
                _drawPolygonSource = null;
            }

            btnEditSource.Enabled = true;
            btnCancelSource.Enabled = false;
            btnSavePolygonSource.Enabled = false;
            btnUndoPointSource.Enabled = false;

            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();

        }

        private void btnCancelAOIPolygon_Click(object sender, EventArgs e)
        {
            if (_drawPolygonAOI != null)
            {
                _drawPolygonAOI.Coordinates.Clear();
                _drawPolygonAOI.Deactivate();
                _drawPolygonAOI = null;
            }

            btnEditAOIPolygon.Enabled = true;
            btnCancelAOIPolygon.Enabled = false;
            btnSavePolygonAOIPolygon.Enabled = false;
            btnUndoPointAOIPolygon.Enabled = false;

            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();

        }

        private void btnCancelAOICircle_Click(object sender, EventArgs e)
        {
            if (_drawCircleAOI != null)
            {
                _drawCircleAOI.Coordinates.Clear();
                _drawCircleAOI.Deactivate();
                _drawCircleAOI = null;
            }

            btnEditAOICircle.Enabled = true;
            btnCancelAOICircle.Enabled = false;
            btnSaveCircleAOICircle.Enabled = false;
            btnUndoPointAOICircle.Enabled = false;

            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnSaveBufferAOIBuffer_Click(object sender, EventArgs e)
        {

            //see if we have a source to buffer
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_SOURCE);
            if (layerSource == null)
            {
                MessageBox.Show("You must first draw a Source to buffer.");
                return;
            }

            //check that we have a radius and name
            double dBuffer = 0;
            try
            {
                dBuffer = Double.Parse(txtBuffer.Text);
            }
            catch (Exception ex)
            {
                txtBuffer.Text = "1000";
                return;
            }

            //remove current AOI if there is one:
            IMapLayer layerAOI = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerAOI != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerAOI);
            }

            //open up a buffer feature set
            FeatureSet fsBuffer = null;
            fsBuffer = new FeatureSet(FeatureType.Polygon);
            fsBuffer.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;
            fsBuffer.Name = LAYER_NAME_AOI;

            //get source featureset/feature
            FeatureSet fsSource = (FeatureSet)layerSource.DataSet;
            //buffer source feature
            Feature fBuffer = (Feature)fsSource.Features[0].Buffer(dBuffer);                 
            //add to buffer feature set
            fsBuffer.AddFeature(fBuffer);
            //add AOI layer
            addAOILayerGivenFS(fsBuffer, LAYER_NAME_AOI);        
            
        }

        private void btnClearDrawingAOIBuffer_Click(object sender, EventArgs e)
        {
            IMapLayer layerAOI = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerAOI != null)
            {
                //remove the layer
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerAOI);
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnEditAOIShape_Click(object sender, EventArgs e)
        {
            //get selected layer
            IMapLayer iml = SDPProjectBuilderPlugin_GUI.Map.Layers.SelectedLayer;
            if (iml == null)
            {
                MessageBox.Show("You must select a map layer prior to selecting shapes.");
                return;
            }
            
            btnEditAOIShape.Enabled = false;
            btnCancelAOIShape.Enabled = true;
            btnSavePolygonAOIShape.Enabled = true;            

            //remove current AOI if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();

            //open up a drawing layer
            FeatureSet fs = null;
            fs = new FeatureSet(FeatureType.Polygon);
            fs.Projection = SDPProjectBuilderPlugin_GUI.Map.Projection;
            fs.Name = LAYER_NAME_AOI;

            if (_drawShapeAOI == null)
            {
                _drawShapeAOI = new SDPProjectBuilderPolygonSelector(SDPProjectBuilderPlugin_GUI.Map);
                _drawShapeAOI.Name = "AddShapeAOI";
            }
            if (SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Contains(_drawShapeAOI) == false)
            {
                SDPProjectBuilderPlugin_GUI.Map.MapFunctions.Add(_drawShapeAOI);
            }

            _drawShapeAOI.FeatureSet = fs;
            _drawShapeAOI.Activate();

        }

        private void btnCancelAOIShape_Click(object sender, EventArgs e)
        {
            if (_drawShapeAOI != null)
            {
                _drawShapeAOI.FeatureSet.Features.Clear();
                _drawShapeAOI.Deactivate();
                _drawShapeAOI = null;
            }

            btnEditAOIShape.Enabled = true;
            btnCancelAOIShape.Enabled = false;
            btnSavePolygonAOIShape.Enabled = false;

            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnClearDrawingAOIShape_Click(object sender, EventArgs e)
        {
            IMapLayer layerAOI = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerAOI != null)
            {
                if (_drawShapeAOI != null)
                {
                    _drawShapeAOI.FeatureSet.Features.Clear();
                    _drawShapeAOI = null;
                }
                //remove the layer
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerAOI);
            }
            else
            {
                if (_drawShapeAOI != null)
                {
                    _drawShapeAOI.FeatureSet.Features.Clear();
                }
            }
            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();
            SDPProjectBuilderPlugin_GUI.Map.Invalidate();
        }

        private void btnSavePolygonAOIShape_Click(object sender, EventArgs e)
        {
           //remove current AOI if there is one:
            IMapLayer layerSource = GetLayerByLegendText(LAYER_NAME_AOI);
            if (layerSource != null)
            {
                SDPProjectBuilderPlugin_GUI.Map.Layers.Remove(layerSource);
            }           

            //add AOI layer
            addAOILayerGivenFS(_drawShapeAOI.FeatureSet, LAYER_NAME_AOI);

            SDPProjectBuilderPlugin_GUI.Map.ClearSelection();

            btnEditAOIShape.Enabled = true;
            btnCancelAOIShape.Enabled = false;
            btnSavePolygonAOIShape.Enabled = false;

        }

        private void UpdateStatus(string sText)
        {
            txtRunStatus.AppendText(sText + Environment.NewLine);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtSourceName.Text.Trim()))
            {
                MessageBox.Show("You must enter a Source Name.");
                return;                
            }

            if (lbSourceTypes.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one Source Type.");
                return;
            }

            if (chkForceFullDependency.Checked)
            {
                ForceFullDependency();            
            }

            if (String.IsNullOrEmpty(_projectFile))
            {
                MessageBox.Show("You must save this project before running it.");
                return;
            }

            UpdateStatus("Starting Project...");

            SDP_Project_Builder_Batch.SDP_Project_Builder_Batch batch = new SDP_Project_Builder_Batch.SDP_Project_Builder_Batch();
            batch.doD4EMProject(_projectFile);

            UpdateStatus("Project Complete!");

            MessageBox.Show("Project Run Complete!");


        }

        private void btnGetDatabases_Click(object sender, EventArgs e)
        {
            DBManager dbMgr = new DBManager("MySQL");
            Hashtable htConn = new Hashtable();
            htConn.Add("Server", txtServer.Text.Trim());
            htConn.Add("Port", txtPort.Text.Trim());
            htConn.Add("Username", txtUsername.Text.Trim());
            htConn.Add("Password", txtPassword.Text.Trim());
            //htConn.Add("Database", parameters.DatabaseName);
            //htConn.Add("Connection Timeout", parameters.LengthOfTimeout);
            //htConn.Add("NumberOfRetries", parameters.NumberOfRetries);
            if (dbMgr.initializeConnection(htConn) == false)
            {
                return;
            }
            //get list of databases
            cboDatabaseName.SelectedIndex = -1;
            cboDatabaseName.Items.Clear();
            List<string> ls = dbMgr.GetDatabases();            
            foreach (string s in ls)
            {
                cboDatabaseName.Items.Add(s);            
            }

            MessageBox.Show("Databases Retrieved!");

        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {

            DBManager dbMgr = new DBManager("MySQL");
            Hashtable htConn = new Hashtable();
            htConn.Add("Server", txtServer.Text.Trim());
            htConn.Add("Port", txtPort.Text.Trim());
            htConn.Add("Username", txtUsername.Text.Trim());
            htConn.Add("Password", txtPassword.Text.Trim());
            if (String.IsNullOrEmpty(cboDatabaseName.Text.Trim()))
            {
                MessageBox.Show("You must select a database.");
                return;
            }
            htConn.Add("Database", cboDatabaseName.Text.Trim());
            //htConn.Add("Connection Timeout", parameters.LengthOfTimeout);
            //htConn.Add("NumberOfRetries", parameters.NumberOfRetries);
            if (dbMgr.initializeConnection(htConn) == false)
            {
                MessageBox.Show("Connection Failed!");
            }
            else
            {
                MessageBox.Show("Connection Successful!");
            }

        }

        

    }
}
