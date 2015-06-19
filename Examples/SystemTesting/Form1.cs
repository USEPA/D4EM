using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using D4EMSystemTesting;
using System.Diagnostics;
using System.Threading;

namespace D4EMSystemTesting
{
    public partial class Form1 : Form
    {
        List<string> _items = new List<string>(); 
        string ProjectFolder = Path.Combine(System.IO.Path.GetTempPath(), "D4EMSystemTesting");
        double North = 33;
        double South = 32;
        double East = -83;
        double West = -84;
        string HUC = "03070101";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            label1.Visible = true;
            label1.Text = "Running test....";
            if(Directory.Exists(ProjectFolder))
            {
                Directory.Delete(ProjectFolder, true);
            }
            Directory.CreateDirectory(ProjectFolder);
            toolStripStatusLabel1.Text = "Testing NLCD....";
            doNLCDTest();
            toolStripStatusLabel1.Text = "Testing Basins....";
            doBasinsTest();
            toolStripStatusLabel1.Text = "Testing NHDPlus....";
            doNHDPlusTest();
            toolStripStatusLabel1.Text = "Testing NWIS....";
            doNWISTest();
            toolStripStatusLabel1.Text = "Testing NRCS-Soil....";
            doNRCSSoilTest();
            toolStripStatusLabel1.Text = "Testing NCDC....";
            doNCDCTest();
            toolStripStatusLabel1.Text = "Testing Storet....";            
            doStoretTest();
            toolStripStatusLabel1.Text = "Testing WDNR....";
            doWDNRTest();
            toolStripStatusLabel1.Text = "Testing NASS....";
            doNASSTest();
            toolStripStatusLabel1.Text = "Testing NDBC....";
            doNDBCTest();
            toolStripStatusLabel1.Text = "Testing NatureServe....";
            doNatureServeTest();
            toolStripStatusLabel1.Text = "Testing NAWQA....";
            doNAWQATest();
            listBox1.DataSource = _items;
            this.Cursor = StoredCursor;
            toolStripStatusLabel1.Text = "Test completed.";
            label1.Visible = false;
            if (System.Windows.Forms.MessageBox.Show("Delete " + ProjectFolder + "?", "Delete temporary folder?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Directory.Delete(ProjectFolder, true);
            }
        }

        private void doNLCDTest()
        {
            D4EM.Data.LayerSpecification dt = new D4EM.Data.LayerSpecification();
            bool testdownload;
            bool testcache;

            testNLCD nlcd = new testNLCD();
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD1992.LandCover;

            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 1992 LandCover: Passed");
            }
            else
            {
                _items.Add("NLCD 1992 LandCover: Failed");
            }
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover;
            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 2001 LandCover: Passed");
            }
            else
            {
                _items.Add("NLCD 2001 LandCover: Failed");
            }
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.Canopy;
            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 2001 Canopy: Passed");
            }
            else
            {
                _items.Add("NLCD 2001 Canopy: Failed");
            }
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.Impervious;
            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 2001 Impervious: Passed");
            }
            else
            {
                _items.Add("NLCD 2001 Impervious: Failed");
            }
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.LandCover;
            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 2006 LandCover: Passed");
            }
            else
            {
                _items.Add("NLCD 2006 LandCover: Failed");
            }
            dt = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.Impervious;
            testdownload = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            testcache = nlcd.testingNLCD(ProjectFolder, North, South, East, West, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NLCD 2006 Impervious: Passed");
            }
            else
            {
                _items.Add("NLCD 2006 Impervious: Failed");
            }
        }

        private void doBasinsTest()
        {
            D4EM.Data.LayerSpecification dt;
            bool testdownload;
            bool testcache;

            testBasins basins = new testBasins();
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.core31.all;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins core31: Passed");
            }
            else
            {
                _items.Add("Basins core31: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.Census.all;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins census: Passed");
            }
            else
            {
                _items.Add("Basins census: Failed");
            }
            //dt = D4EM.Data.Source.BASINS.LayerSpecifications.d303;
            //testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            //testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            //if ((testdownload == true) && (testcache == true))
            //{
            //    _items.Add("Basins d303: Passed");
            //}
            //else
            //{
            //    _items.Add("Basins d303: Failed");
            //}
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.dem;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins dem: Passed");
            }
            else
            {
                _items.Add("Basins dem: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.DEMG;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins DEMG: Passed");
            }
            else
            {
                _items.Add("Basins DEMG: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.giras;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins giras: Passed");
            }
            else
            {
                _items.Add("Basins giras: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.huc12;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins huc12: Passed");
            }
            else
            {
                _items.Add("Basins huc12: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.lstoret;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins lstoret: Passed");
            }
            else
            {
                _items.Add("Basins lstoret: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.NED;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins NED: Passed");
            }
            else
            {
                _items.Add("Basins NED: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.nhd;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins nhd: Passed");
            }
            else
            {
                _items.Add("Basins nhd: Failed");
            }
            dt = D4EM.Data.Source.BASINS.LayerSpecifications.pcs3;
            testdownload = basins.testingBasins(ProjectFolder, HUC, dt);
            testcache = basins.testingBasins(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("Basins pcs3: Passed");
            }
            else
            {
                _items.Add("Basins pcs3: Failed");
            }
        }
        private void doNHDPlusTest()
        {
            D4EM.Data.LayerSpecification dt = new D4EM.Data.LayerSpecification();
            bool testdownload;
            bool testcache;
            testNHDPlus nhdplus = new testNHDPlus();
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentGrid;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus CatchmentGrid: Passed");
            }
            else
            {
                _items.Add("NHDPlus CatchmentGrid: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus CatchmentPolygons: Passed");
            }
            else
            {
                _items.Add("NHDPlus CatchmentPolygons: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus ElevationGrid: Passed");
            }
            else
            {
                _items.Add("NHDPlus ElevationGrid: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowAccumulationGrid;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus FlowAccumulationGrid: Passed");
            }
            else
            {
                _items.Add("NHDPlus FlowAccumulationGrid: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowDirectionGrid;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus FlowDirectionGrid: Passed");
            }
            else
            {
                _items.Add("NHDPlus FlowDirectionGrid: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.SlopeGrid;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus SlopeGrid: Passed");
            }
            else
            {
                _items.Add("NHDPlus SlopeGrid: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Area;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus Area: Passed");
            }
            else
            {
                _items.Add("NHDPlus Area: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus Flowline: Passed");
            }
            else
            {
                _items.Add("NHDPlus Flowline: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Line;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus Line: Passed");
            }
            else
            {
                _items.Add("NHDPlus Line: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Point;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus Point: Passed");
            }
            else
            {
                _items.Add("NHDPlus Point: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus Waterbody: Passed");
            }
            else
            {
                _items.Add("NHDPlus Waterbody: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.RegionPolygons;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus RegionPolygons: Passed");
            }
            else
            {
                _items.Add("NHDPlus RegionPolygons: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubBasinPolygons;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus SubBasinPolygons: Passed");
            }
            else
            {
                _items.Add("NHDPlus SubBasinPolygons: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubWatershedPolygons;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus SubWatershedPolygons: Passed");
            }
            else
            {
                _items.Add("NHDPlus SubWatershedPolygons: Failed");
            }
            dt = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.WatershedPolygons;
            testdownload = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            testcache = nhdplus.testingNHDPlus(ProjectFolder, HUC, dt);
            if ((testdownload == true) && (testcache == true))
            {
                _items.Add("NHDPlus WatershedPolygons: Passed");
            }
            else
            {
                _items.Add("NHDPlus WatershedPolygons: Failed");
            }           
            
        }

        private void doNWISTest()
        {
            D4EM.Data.LayerSpecification dt = new D4EM.Data.LayerSpecification();
            bool testdownload;
            string dataType;

            testNWIS nwis = new testNWIS();
            dataType = "Daily Discharge";
            testdownload = nwis.testingNWIS(ProjectFolder, North, South, East, West, dataType);
            if (testdownload == true)
            {
                _items.Add("NWIS Daily Discharge: Passed");
            }
            else
            {
                _items.Add("NWIS Daily Discharge: Failed");
            } 
            dataType = "IDA Discharge";
            testdownload = nwis.testingNWIS(ProjectFolder, North, South, East, West, dataType);
            if (testdownload == true)
            {
                _items.Add("NWIS IDA Discharge: Passed");
            }
            else
            {
                _items.Add("NWIS IDA Discharge: Failed");
            }
            dataType = "Measurement";
            testdownload = nwis.testingNWIS(ProjectFolder, North, South, East, West, dataType);
            if (testdownload == true)
            {
                _items.Add("NWIS Measurement: Passed");
            }
            else
            {
                _items.Add("NWIS Measurement: Failed");
            }
            dataType = "Water Quality";
            testdownload = nwis.testingNWIS(ProjectFolder, North, South, East, West, dataType);
            if (testdownload == true)
            {
                _items.Add("NWIS Water Quality Passed");
            }
            else
            {
                _items.Add("NWIS Water Quality: Failed");
            }         
        }

        private void doNRCSSoilTest()
        {
            double aLatitude = North;
            double aLongitude = East;
            double aRadiusInitial = 1000;
            double aRadiusMax = 1000;
            double aRadiusIncrement = 1000;
            bool testdownload;
            bool testcache;

            testNRCS_Soil nrcssoil = new testNRCS_Soil();

            testdownload = nrcssoil.testingNRCSSoil(ProjectFolder, aLatitude, aLongitude, aRadiusInitial, aRadiusMax, aRadiusIncrement);
            testcache = nrcssoil.testingNRCSSoil(ProjectFolder, aLatitude, aLongitude, aRadiusInitial, aRadiusMax, aRadiusIncrement);
            _items.Add("NRCS-Soil: " + ((testdownload && testcache) ? "Passed" : "Failed"));
        }

        private void doNCDCTest()
        {
            string state = "GA";
            string token = "VzwVyCwzUzMKMopSqnTT";
            string yearStart = "2008";
            string monthStart = "01";
            string dayStart = "01";
            string yearEnd = "2008";
            string monthEnd = "02";
            string dayEnd = "01";
            string outputType = "xml";
            string datasetType = "ish";
            string stationID = "99999993830";
            string stationName = "ATLANTA NAS";
            string variableID = "GF1";
            string variableName = "Sky Condition Observation #2  ***";
            double latitude = 33.867;
            double longitude = -084.300;

            bool testStations;
            bool testValues;           

            testNCDC ncdc = new testNCDC();
            testStations = ncdc.testingNCDCStations(ProjectFolder, state, token);
            Thread.Sleep(60000);
            testValues = ncdc.testingNCDCValues(ProjectFolder, stationID, stationName, variableID, variableName, latitude, longitude, datasetType, outputType, yearStart, monthStart, dayStart, yearEnd, monthEnd, dayEnd, token);

            _items.Add("NCDC: " + ((testStations && testValues) ? "Passed" : "Failed"));
        }
        private void doStoretTest()
        {
            bool testdownload;
            bool testcache;

            testStoret storet = new testStoret();

            testdownload = storet.testingStoret(ProjectFolder, North, South, East, West);
            testcache = storet.testingStoret(ProjectFolder, North, South, East, West);
            _items.Add("Storet: " + ((testdownload && testcache) ? "Passed" : "Failed"));
        }

        private void doWDNRTest()
        {
            double north = 44.543505;
            double south = 42.405047;
            double east = -88.703613;
            double west = -90.681152;
            string huc8 = "07070005";
            string huc12 = "070700050302";
            string[] animals = {"Beef", "Chickens", "Dairy", "Swine", "Turkeys"};
            
            bool testdownload;
            bool testcache;

            testWDNR wdnr = new testWDNR();
            foreach (string animal in animals)
            {
                testdownload = wdnr.testingWDNRStateWide(ProjectFolder, animal);
                testcache = wdnr.testingWDNRStateWide(ProjectFolder, animal);
                _items.Add("WDNR Statewide " + animal + ": " + ((testdownload && testcache) ? "Passed" : "Failed"));
            }
            foreach (string animal in animals)
            {
                testdownload = wdnr.testingWDNRBoundingBox(ProjectFolder, animal, north, south, east, west);
                testcache = wdnr.testingWDNRBoundingBox(ProjectFolder, animal, north, south, east, west);
                _items.Add("WDNR BoundingBox " + animal + ": " + ((testdownload && testcache) ? "Passed" : "Failed"));
            }
            foreach (string animal in animals)
            {
                testdownload = wdnr.testingWDNRHuc8(ProjectFolder, animal, huc8);
                testcache = wdnr.testingWDNRHuc8(ProjectFolder, animal, huc8);
                _items.Add("WDNR HUC-8 " + animal + ": " + ((testdownload && testcache) ? "Passed" : "Failed"));
            }
            foreach (string animal in animals)
            {
                testdownload = wdnr.testingWDNRHuc12(ProjectFolder, animal, huc12);
                testcache = wdnr.testingWDNRHuc12(ProjectFolder, animal, huc12);
                _items.Add("WDNR HUC-12 " + animal + ": " + ((testdownload && testcache) ? "Passed" : "Failed"));
            }

        }

        private void doNASSTest()
        {
            bool testdownload;
            bool testcache;

            testNASS nass = new testNASS();

            string lProjectFolderNASS = System.IO.Path.Combine(ProjectFolder, "NASS");

            int[] years = new int[] { 2008, 2009, 2010 };
            foreach (int year in years)
            {
                testdownload = nass.testingNASS(ProjectFolder, year, North, South, East, West);
                string lSubFolder = System.IO.Path.Combine(lProjectFolderNASS, year + ";N" + North + ";S" + South + ";E" + East + ";W" + West);
                Directory.Delete(lSubFolder, true);
                testcache = nass.testingNASS(ProjectFolder, year, North, South, East, West);
                _items.Add("NASS " + year + ": " + ((testdownload && testcache) ? "Passed" : "Failed"));
            }
        }

        private void doNDBCTest()
        {
            double lat = North;
            double lng = West;
            double radius = 1000;
            testNDBC ndbc = new testNDBC();

            bool testdownload = ndbc.testingNDBC(ProjectFolder, lat, lng, radius);
            bool testcache = ndbc.testingNDBC(ProjectFolder, lat, lng, radius);
            _items.Add("NDBC: " + ((testdownload && testcache) ? "Passed" : "Failed"));
        }

        private void doNatureServeTest()
        {            
            testNatureServe natureserve = new testNatureServe();
            bool testdownload = natureserve.testingNatureServe(ProjectFolder);
            bool testcache = natureserve.testingNatureServe(ProjectFolder);
            _items.Add("NatureServe: " + ((testdownload && testcache) ? "Passed" : "Failed"));
        }

        private void doNAWQATest()
        {
            testNAWQA nawqa = new testNAWQA();
            bool testdownload = nawqa.testingNAWQA(ProjectFolder, false, false);
            bool testcache = nawqa.testingNAWQA(ProjectFolder, false, false);
            _items.Add("NAWQA Data (using States/Counties): " + ((testdownload && testcache) ? "Passed" : "Failed"));
            testdownload = nawqa.testingNAWQA(ProjectFolder, true, false);
            testcache = nawqa.testingNAWQA(ProjectFolder, true, false);
            _items.Add("NAWQA Data (using Lat/Long): " + ((testdownload && testcache) ? "Passed" : "Failed"));

            testdownload = nawqa.testingNAWQA(ProjectFolder, false, true);
            testcache = nawqa.testingNAWQA(ProjectFolder, false, true);
            _items.Add("NAWQA Avg&StdDev (using States/Counties): " + ((testdownload && testcache) ? "Passed" : "Failed"));
            testdownload = nawqa.testingNAWQA(ProjectFolder, true, true);
            testcache = nawqa.testingNAWQA(ProjectFolder, true, true);
            _items.Add("NAWQA Avg&StdDev (using Lat/Long): " + ((testdownload && testcache) ? "Passed" : "Failed"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
