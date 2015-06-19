using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D4EM.Data;
using D4EM.Data.Source;
using D4EM.Geo;
using System.Collections;
using System.IO;
using System.IO.Compression;
using atcUtility;
using System.Xml;
using testingD4EM;
using System.Net;
using Ionic;
using DotSpatial.Analysis;
using DotSpatial.Controls;
using DotSpatial.Extensions;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System.Diagnostics;
using System.Threading;
using atcData;
using atcWDM;

using D4EM_NRCS_Soil; 

namespace testingNLCD
{
    public partial class Form1 : Form
    {

        D4EMInterface.HSPFParameters aParameters = new D4EMInterface.HSPFParameters();
        D4EMInterface.BuildHSPF BuildHSPF = new D4EMInterface.BuildHSPF();
        
        //public D4EM.Data.Project aProject = null;
        int countFiles = 0;
        string aProjectFolderUSGS_Seamless = "";        
        string aProjectFolderBasins = "";        
        string aProjectFolderNHDPlus = "";        
       // string aProjectFolderNLDAS = "";            
        string aProjectFolderNWIS = "";
        string aProjectFolderNatureServe = "";
        string aProjectFolderStoret = "";
        string aProjectFolderSoils = "";
        string aProjectFolderWDNR = "";
        string aProjectFolderNCDC = "";
        string aProjectFolderNASS = "";
        string aProjectFolderNDBC = "";
        string aCacheFolderUSGS_Seamless = "";
        string aCacheFolderBasins = "";
        string aCacheFolderNHDPlus = "";
     //   string aCacheFolderNLDAS = "";
        string aCacheFolderNWIS = "";
        string aCacheFolderNatureServe = "";
      //  string aCacheFolderStoret = "";
        string aCacheFolderNRCSSoil = "";
        string aCacheFolderWDNR = "";
        string aCacheFolderNCDC = "";
        string aCacheFolderNASS = "";
        string aProjectFolderNAWQA = "";
        string aCacheFolderNAWQA = ""; 

        public Form1()
        {
            InitializeComponent();
            appManager.LoadExtensions(); //added 10 March 2013 - Nick Pope
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\Temp");
            string testfile = Path.GetFullPath("test.txt");

            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath; 
            string path = Path.GetDirectoryName(filePath);    

            
            txtProjectFolderUSGS_Seamless.Text = "C:\\Temp\\ProjectFolderUSGS_Seamless";
            txtCacheFolderUSGS_Seamless.Text = System.IO.Path.Combine(path, "Cache");

            txtNorthUSGS_Seamless.Text = "34";
            txtSouthUSGS_Seamless.Text = "33";
            txtWestUSGS_Seamless.Text = "-84";
            txtEastUSGS_Seamless.Text = "-83";
            txtProjectFolderBasins.Text = "C:\\Temp\\ProjectFolderBasins";
            txtCacheBasins.Text = System.IO.Path.Combine(path, "Cache");
            txtHUC8Basins.Text = "03070101";           
            txtHUC8NHDPlus.Text = "03070101";            
            txtProjectFolderNHDPlus.Text = "C:\\Temp\\ProjectFolderNHDPlus";
            txtCacheNHDPlus.Text = System.IO.Path.Combine(path, "Cache");
            
           
            txtNorthNWIS.Text = "34";
            txtSouthNWIS.Text = "33.9";
            txtWestNWIS.Text = "-84";
            txtEastNWIS.Text = "-83.9";            
            txtProjectNWIS.Text = "C:\\Temp\\ProjectFolderNWIS";
            
            txtProjectFolderNatureServe.Text = @"C:\Temp\ProjectFolderNatureServe";
            txtCacheNatureServe.Text = System.IO.Path.Combine(path, "Cache");
            //txtLatitudeSoils.Text = "33";
            //txtLongitudeSoils.Text = "-84";
            txtNorthStoret.Text = "33";
            txtSouthStoret.Text = "32";
            txtWestStoret.Text = "-84";
            txtEastStoret.Text = "-83";
            txtProjectFolderStoret.Text = @"C:\Temp\ProjectFolderStoret";
            
            txtProjectFolderSoils.Text = @"C:\Temp\ProjectFolderSoil";
            txtCacheNRCSSoil.Text = System.IO.Path.Combine(path, "Cache");
            txtNorth.Text = "38.3468989298251";
            txtSouth.Text = "38.2836163635915";
            txtWest.Text = "-82.1690054354632";
            txtEast.Text =  "-82.0576704259708";
            //txtRadiusIncrement.Text = "1000";
            //txtRadiusInitial.Text = "1000";
            //txtRadiusMax.Text = "1000";

            txtHUC8natureServe.Text = "11020001";

            txtProjectFolderWDNR.Text = @"C:\Temp\ProjectFolderWDNR";
            txtCacheWDNR.Text = System.IO.Path.Combine(path, "Cache");
            txtNorthWDNR.Text = "44.543505";
            txtSouthWDNR.Text = "42.405047";
            txtEastWDNR.Text = "-88.703613";
            txtWestWDNR.Text = "-90.681152";
            txtHucWDNR.Text = "07070005";
            txtHuc8Huc12WDNR.Text = "07070005";

            txtToken.Text = "VzwVyCwzUzMKMopSqnTT";

            txtStartYear.Text = "2008";
            txtStartMonth.Text = "01";
            txtStartDay.Text = "01";
            txtEndYear.Text = "2008";
            txtEndMonth.Text = "02";
            txtEndDay.Text = "01";

            txtProjectFolderNCDC.Text = @"C:\Temp\ProjectFolderNCDC";
            txtCacheNCDC.Text = System.IO.Path.Combine(path, "Cache");
            txtNorthNASS.Text = "34";
            txtSouthNASS.Text = "33";
            txtEastNASS.Text = "-84";
            txtWestNASS.Text = "-85";

            txtProjectFolderNASS.Text = @"C:\Temp\ProjectFolderNASS";
            txtCacheNASS.Text = System.IO.Path.Combine(path, "Cache");
            txtLatitudeNDBC.Text = "33";
            txtLongitudeNDBC.Text = "-84";
            txtRadiusNDBC.Text = "1000";
            txtProjectFolderNDBC.Text = @"C:\Temp\ProjectFolderNDBC";

            txtProjectFolderNAWQA.Text = @"C:\Temp\ProjectFolderNAWQA";
            txtCacheFolderNAWQA.Text = System.IO.Path.Combine(path, "Cache");
            txtNAWQAlat.Text = "35";
            txtNAWQAlng.Text = "-88";

            txtNDBCStationID.Text = "PPTM2";
            txtNDBCyear.Text = "2010";


            txtHucNLDAS.Enabled = false;
            txtLatitudeNLDAS.Enabled = false;
            txtLongitudeNLDAS.Enabled = false;
            lblNLDAS.Visible = false;

        }

        private void btnRun_Click_1(object sender, EventArgs e)
        {
            bool logfileExists = false;

            string aProjectFolderUSGS_Seamless = System.IO.Path.Combine(txtProjectFolderUSGS_Seamless.Text.Trim(), "USGS-Seamless");
            string aCacheFolderUSGS_Seamless = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, "Cache");

            if (boxLayer.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please specify one or more layers");
                return;
            }
            if (txtNorthUSGS_Seamless.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthUSGS_Seamless.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestUSGS_Seamless.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastUSGS_Seamless.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }

            bool dataDownloaded = false;
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            string aSaveFolder = "";
            string fileLocationsText = "Downloaded USGS-Seamless files are located in " + aProjectFolderUSGS_Seamless + Environment.NewLine + Environment.NewLine;
            try
            {
                Double aNorth = Convert.ToDouble(txtNorthUSGS_Seamless.Text.Trim());
                Double aSouth = Convert.ToDouble(txtSouthUSGS_Seamless.Text.Trim());
                Double aWest = Convert.ToDouble(txtWestUSGS_Seamless.Text.Trim());
                Double aEast = Convert.ToDouble(txtEastUSGS_Seamless.Text.Trim());

                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderUSGS_Seamless, aProjectFolderUSGS_Seamless, aRegion, true, true);
                string logfilename = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, "_LogFile.txt");
                fileLocationsText = fileLocationsText + "Metadata: " + logfilename + Environment.NewLine;

                foreach (object layerspec in boxLayer.CheckedItems)
                {
                    if (layerspec.ToString() != "Select All")
                    {
                        string layer = layerspec.ToString();
                        //string xmlfilename = "";
                        string aSubFolder = "";
                        LayerSpecification layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.LandCover;

                        switch (layer)
                        {
                            case "1992 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD1992.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                string st = D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /* xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_LandCover_1992.tif.xml");
                                 if (File.Exists(xmlfilename) == true)
                                 {
                                     ReadXMLandWriteLog readWrite1992LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                 }*/
                                break;
                            case "2001 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*   xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_landcover_2001.xml");
                                   if (File.Exists(xmlfilename) == true)
                                   {
                                       ReadXMLandWriteLog readWrite2001LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                   }*/
                                break;
                            case "2001 Canopy":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.Canopy;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_NLCD2001.Canopy_2001.tif.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2001Canopy = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                            case "2001 Impervious":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.Impervious;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                string[] xml2001Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder), "*.xml", SearchOption.AllDirectories);
                                xmlfilename = xml2001Impervious[1];
                               // xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_impervious_2001.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2001Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                            case "2006 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                string[] xml2006LandCover = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder), "*.xml", SearchOption.AllDirectories);
                                xmlfilename = xml2006LandCover[1];
                               // xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_landcover_2006.tif.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                            case "2006 Impervious":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.Impervious;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                string[] xml2006Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder), "*.xml", SearchOption.AllDirectories);
                                xmlfilename = xml2006Impervious[1];
                               // xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD2006.Impervious.tif.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                            case "NED 1 ArcSecond":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneArcSecond;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                string[] xml2006Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder), "*.xml", SearchOption.AllDirectories);
                                xmlfilename = xml2006Impervious[1];
                               // xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD2006.Impervious.tif.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                            case "NED 1/3 ArcSecond":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneThirdArcSecond;
                                aSaveFolder = @"NED Third ArcSecond" + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(aSubFolder))
                                {
                                    dataDownloaded = true;
                                }
                                /*
                                string[] xml2006Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder), "*.xml", SearchOption.AllDirectories);
                                xmlfilename = xml2006Impervious[1];
                               // xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD2006.Impervious.tif.xml");
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }*/
                                break;
                        }

                        logfileExists = true;
                    }
                }
                labelUSGS_Seamless.Visible = true;
                labelUSGS_Seamless.Text = "Downloaded data is located in " + aProjectFolderUSGS_Seamless;

                int fileCount = countFiles;
                if (!dataDownloaded)
                {
                    MessageBox.Show("No data downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "USGS-Seamless File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
                this.Cursor = StoredCursor;
                countFiles = 0;

            
        }
           
        private void btnBrowseProject_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderUSGS_Seamless.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderUSGS_Seamless = this.txtProjectFolderUSGS_Seamless.Text;
            }
        }

        private void btnRunBasins_Click(object sender, EventArgs e)
        {
            if (listHuc8Basins.Items.Count == 0)
            {
                MessageBox.Show("Please add a HUC-8 to the list");
                return;
            }
            if (boxBasinsDataType.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select atleast one data type");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            Double aNorth = 33;
            Double aSouth = 32;
            Double aWest = -84;
            Double aEast = -83;

            String aHUC8 = txtHUC8Basins.Text.Trim();

            string aProjectFolderBasins = System.IO.Path.Combine(txtProjectFolderBasins.Text.Trim(), "Basins");
            string aCacheFolderBasins = txtCacheBasins.Text.Trim();

            string fileLocationsText = "Downloaded BASINS files are located in " + aProjectFolderBasins + Environment.NewLine + Environment.NewLine;
            
            bool fileExists = false;
            bool logfileExists = false;
            try
            {
                foreach (object datatype in boxBasinsDataType.CheckedItems)
                {
                    string adatatype = datatype.ToString();
                    D4EM.Data.LayerSpecification dt;
                    DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                    D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);

                    D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderBasins, aProjectFolderBasins, aRegion, false, true);
                    string aSaveFolder;
                    //string xmlfilename = "";
                    string logfilename = System.IO.Path.Combine(aProjectFolderBasins, "_LogFile.txt");
                    string aSubFolder = "";

                    switch (adatatype)
                    {
                        case "core31":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.core31.all;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "census":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.Census.all;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        //case "d303":
                        //    dt = D4EM.Data.Source.BASINS.BASINSDataType.d303;
                        //    aSaveFolder = dt.Tag + "_" + aHUC8;
                        //    D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                        //    aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                        //    writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                        //    fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                        //    break;
                        case "dem":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.dem;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "DEMG":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.DEMG;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            break;
                        case "giras":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.giras;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "huc12":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.huc12;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "lstoret":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.lstoret;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "NED":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.NED;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            break;
                        case "nhd":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.nhd;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                        case "pcs3":
                            dt = D4EM.Data.Source.BASINS.LayerSpecifications.pcs3;
                            aSaveFolder = dt.Tag + "_" + aHUC8;
                            D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            break;
                    }

                    //  }
                    //  catch (ApplicationException ex)
                    // {
                    //     MessageBox.Show(ex.ToString());
                    // }
                    logfileExists = true;
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                labelBasins.Visible = true;
                labelBasins.Text = "Downloaded data is located in " + aProjectFolderBasins;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "BASINS File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            countFiles = 0;
       
        }

        private void btnBrowseProjectFolderBasins_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderBasins.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderBasins = this.txtProjectFolderBasins.Text;
            }
        }

        private void btnRunNHDplus_Click(object sender, EventArgs e)
        {
            if (listHuc8NHDPlus.Items.Count == 0)
            {
                MessageBox.Show("Please add a HUC-8 to the list");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            bool logfileExists = false;

            string aProjectFolderNHDPlus = System.IO.Path.Combine(txtProjectFolderNHDPlus.Text.Trim(), "NHDPlus");
            string aCacheFolderNHDPlus = txtCacheNHDPlus.Text.Trim();

            Double aNorth = 34;
            Double aSouth = 33;
            Double aWest = -84;
            Double aEast = -83;


            //String aHUC8 = txtHUC8NHDPlus.Text.Trim();
            
            try
            {
                //NHDPlus grids are in Albers, so it is easier to request them in their native projection
                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.AlbersProjection(); // new DotSpatial.Projections.ProjectionInfo(D4EM.Data.Globals.GeographicProjection().ToProj4String());
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNHDPlus, aProjectFolderNHDPlus, aRegion, false, false);

                D4EM.Data.LayerSpecification aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.all;

                string fileLocationsText = "Downloaded NHDPlus files are located in " + aProjectFolderNHDPlus + Environment.NewLine + Environment.NewLine;
                
                foreach (object huc8 in listHuc8NHDPlus.Items)
                {
                    string aHUC8 = huc8.ToString();         
                    string logfilename = System.IO.Path.Combine(aProjectFolderNHDPlus, aHUC8 + "_LogFile.txt");
                    fileLocationsText = fileLocationsText + "FILE LOCATIONS for " + huc8 + ":" + Environment.NewLine;
                    fileLocationsText = fileLocationsText + "Metadata: " + logfilename + Environment.NewLine;

                     foreach (object datatype in checkedListNHDPlus.CheckedItems)
                     {
                         string dataType = datatype.ToString();                         
                         string aSaveFolder = aHUC8 + " " + dataType;
                         string aSubFolder = System.IO.Path.Combine(aProjectFolderNHDPlus, aSaveFolder);
                         switch (dataType)
                         {                             
                             case "CatchmentGrid":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentGrid;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);                                
                                 break;
                             case "CatchmentPolygons":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.CatchmentPolygons;
                                 string st = D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;                                 
                             case "ElevationGrid":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                 break;
                             case "FlowAccumulationGrid":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowAccumulationGrid;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                 break;
                             case "FlowDirectionGrid":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.FlowDirectionGrid;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                 break;
                             case "SlopeGrid":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.SlopeGrid;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, dataType);
                                 break;
                             case "Hydrography.Area":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Area;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "Hydrography.Flowline":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Flowline;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "Hydrography.Line":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Line;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "Hydrography.Point":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Point;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "Hydrography.Waterbody":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.Hydrography.Waterbody;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "HydrologicUnits.RegionPolygons":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.RegionPolygons;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "HydrologicUnits.SubBasinPolygons":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubBasinPolygons;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "HydrologicUnits.SubWatershedPolygons":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.SubWatershedPolygons;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                             case "HydrologicUnits.WatershedPolygons":
                                 aDataType = D4EM.Data.Source.NHDPlus.LayerSpecifications.HydrologicUnits.WatershedPolygons;
                                 D4EM.Data.Source.NHDPlus.GetNHDPlus(aProject, aSaveFolder, aHUC8, true, aDataType);
                                 writeLogFile(aSubFolder, logfilename, logfileExists, dataType);
                                 fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, dataType, false);
                                 break;
                         }
                         logfileExists = true;
                     }
                     fileLocationsText = fileLocationsText + Environment.NewLine;
                 }
                labelNHDPlus.Visible = true;
                labelNHDPlus.Text = "Downloaded data is located in " + aProjectFolderNHDPlus;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NHDPlus File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch(ApplicationException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Cursor = StoredCursor;
            countFiles = 0;
        }

        private string addTifFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] tiffiles = Directory.GetFiles(aSubFolder, "*.tif", SearchOption.AllDirectories);
                foreach (string file in tiffiles)
                {
                    DateTime creationTime = File.GetLastAccessTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " tif file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addShpFileLocations(string aSubFolder, string dataType, bool filesExist)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {                
                    string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
                    if (filesExist == false)
                    {
                    foreach (string file in shapefiles)
                    {
                        DateTime creationTime = File.GetLastAccessTime(file);
                        TimeSpan timeSpan = DateTime.Now - creationTime;
                        if (timeSpan.Minutes < 2)
                        {
                            text = text + dataType + " shapefile: " + file + Environment.NewLine;
                            countFiles++;
                        }
                    }
                }
                else
                {
                    foreach (string file in shapefiles)
                    {
                        text = text + dataType + " shapefile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addHtmFileLocations(string aSubFolder, string dataType, bool filesExist)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] htmfiles = Directory.GetFiles(aSubFolder, "*.htm", SearchOption.AllDirectories);
                if (filesExist == false)
                {
                    foreach (string file in htmfiles)
                    {
                        DateTime creationTime = File.GetLastAccessTime(file);
                        TimeSpan timeSpan = DateTime.Now - creationTime;
                        if (timeSpan.Minutes < 2)
                        {
                            text = text + dataType + " htm file: " + file + Environment.NewLine;
                            countFiles++;
                        }
                    }
                }
                else
                {
                    foreach (string file in htmfiles)
                    {
                        text = text + dataType + " htm file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addCSVFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] csvfiles = Directory.GetFiles(aSubFolder, "*.csv", SearchOption.AllDirectories);
                foreach (string file in csvfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " csvfile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addXMLFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] xmlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                foreach (string file in xmlfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " xmlfile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addWaterMLFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] watermlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                foreach (string file in watermlfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " waterml file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addRDBFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] rdbfiles = Directory.GetFiles(aSubFolder, "*.rdb", SearchOption.AllDirectories);
                foreach (string file in rdbfiles)
                {
                    DateTime creationTime = File.GetLastAccessTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " rdb file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private void writeLogFile(string aSubFolder, string logfilename, bool logfileExists, string dataType)
        {
            if (Directory.Exists(aSubFolder))
            {
                string[] xmlfiles = Directory.GetFiles(aSubFolder, "*.xml", SearchOption.AllDirectories);
                if (xmlfiles.Length > 0)
                {
                    string xmlfilename = xmlfiles[0];
                    if (File.Exists(xmlfilename) == true)
                    {
                        ReadXMLandWriteLog readWriteHydrologicUnitsSubBasinPolygons = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, dataType);
                    }
                }
            }
        }

        private void btnBrowseProjectNHDPlus_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNHDPlus.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNHDPlus = this.txtProjectFolderNHDPlus.Text;
            }
        }
        /*
        private void btnRunNLDAS_Click(object sender, EventArgs e)
        {

            string aProjectFolderNLDAS = System.IO.Path.Combine(txtProjectFolderNLDAS.Text.Trim(), "NLDAS");
            string aCacheFolderNLDAS = System.IO.Path.Combine(aProjectFolderNLDAS, "Cache");

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            Double aNorth = Convert.ToDouble(txtNorthNLDAS.Text.Trim());
            Double aSouth = Convert.ToDouble(txtSouthNLDAS.Text.Trim());
            Double aEast = Convert.ToDouble(txtEastNLDAS.Text.Trim());
            Double aWest = Convert.ToDouble(txtWestNLDAS.Text.Trim());

            D4EM.Data.Source.NLDAS nldas = new NLDAS();
            DotSpatial.Projections.ProjectionInfo aDesiredProjection = new DotSpatial.Projections.ProjectionInfo(D4EM.Data.Globals.GeographicProjection().ToProj4String());
            D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNLDAS, aProjectFolderNLDAS, aRegion, false, false);

            System.Collections.Generic.List<D4EM.Data.Source.NLDAS.NLDASGridCoords> aCells = new List<D4EM.Data.Source.NLDAS.NLDASGridCoords>(D4EM.Data.Source.NLDAS.GetGridCellsInRegion(aRegion));
            
          //  aCells = D4EM.Data.Source.NLDAS.GetGridCellsInRegion(aRegion).ToList<D4EM.Data.Source.NLDAS.NLDASGridCoords>;
                    
            

            String aDataType = D4EM.Data.Source.NLDAS.LayerSpecifications.GridPoints.ToString();

            DateTime aStartDate = new DateTime(1990, 1, 18);
            DateTime aEndDate = new DateTime(1991, 1, 18);

         // String aWDMFilename = "test.wdm";
          string aSaveFolder = "saveFolder";
  
            nldas.GetParameter(aProject, aSaveFolder, aCells);
         //   nldas.GetParameter(aProject, aCells, aDataType, aStartDate, aEndDate, aWDMFilename);
            this.Cursor = StoredCursor;

            labelNLDAS.Visible = true;
            labelNLDAS.Text = "Downloaded data is located in " + aProjectFolderNLDAS;
        }
        */
        private void btnRunNWIS_Click(object sender, EventArgs e)
        {
            if (listNWIS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one or more data types");
                return;
            }

            if (txtNorthNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastNWIS.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double aNorth = Convert.ToDouble(txtNorthNWIS.Text.Trim());
            double aSouth = Convert.ToDouble(txtSouthNWIS.Text.Trim());
            double aWest = Convert.ToDouble(txtWestNWIS.Text.Trim()); ;
            double aEast = Convert.ToDouble(txtEastNWIS.Text.Trim());
            
            string aProjectFolderNWIS = System.IO.Path.Combine(txtProjectNWIS.Text.Trim(), "NWIS");
            aCacheFolderNWIS = @"C:\Temp\nwiscache";
            string aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, "NWISstations");
            
            string fileLocationsText = "Downloaded NWIS files for North = " + aNorth + ", South = " + aSouth + ", East = " + aEast + ", West = " + aWest + " are located in " + aProjectFolderNWIS + Environment.NewLine;
            fileLocationsText = fileLocationsText + Environment.NewLine;

            try
            {
                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
            D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
            D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNWIS, aProjectFolderNWIS, aRegion, false, false);

            List<string> stationIDs = new List<string>();

            D4EM.Data.Source.NWIS nwis = new NWIS();    

            D4EM.Data.LayerSpecification aStationDataType = new LayerSpecification();

            //string xmlfilename = "";
            string logfilename = System.IO.Path.Combine(aProjectFolderNWIS, "_LogFile.txt");
            fileLocationsText = fileLocationsText + "Metadata file: " + logfilename + Environment.NewLine;
            bool logfileExists = false;
            string subFolder = "";
            foreach (object adatatype in listNWIS.CheckedItems)
            {
                string datatype = adatatype.ToString();
                string aSaveFolder = "";
                switch (datatype)
                {
                    case "Daily Discharge":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                        stationIDs = getStationIDList(aSaveAs);                        
                        aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                        D4EM.Data.Source.NWIS.GetDailyDischarge(aProject, aSaveFolder, stationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype);                     
                        EPAUtility.NWISFileSupport nwisdd = new EPAUtility.NWISFileSupport();
                        nwisdd.convertRDBtoCSV(subFolder);
                        break;
                    case "IDA Discharge":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                        stationIDs = getStationIDList(aSaveAs);                         
                        aSaveFolder = datatype + " N"+ aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                        D4EM.Data.Source.NWIS.GetIDADischarge(aProject, aSaveFolder, stationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype);   
                        EPAUtility.NWISFileSupport nwisida = new EPAUtility.NWISFileSupport();
                        nwisida.convertRDBtoCSV(subFolder);
                        break;
                    case "Measurement":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Measurement;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                        stationIDs = getStationIDList(aSaveAs);
                        aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                        D4EM.Data.Source.NWIS.GetMeasurements(aProject, aSaveFolder, stationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                        EPAUtility.NWISFileSupport nwism = new EPAUtility.NWISFileSupport();
                        nwism.convertRDBtoCSV(subFolder);
                        break;
                    case "Water Quality":
                        aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.WaterQuality;
                        D4EM.Data.Source.NWIS.GetStationsInRegion(aRegion, aSaveAs, aStationDataType);
                        stationIDs = getStationIDList(aSaveAs);                     
                        aSaveFolder = datatype + " N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                        EPAUtility.NWISFileSupport.writeShapeFile(aSaveAs, aSaveFolder, aProjectFolderNWIS, datatype);
                        D4EM.Data.Source.NWIS.GetWQ(aProject, aSaveFolder, stationIDs);
                        subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                        fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                        fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                        writeLogFile(subFolder, logfilename, logfileExists, datatype);
                        EPAUtility.NWISFileSupport nwiswq = new EPAUtility.NWISFileSupport();
                        nwiswq.convertRDBtoCSV(subFolder);
                        foreach (object spdt in listNWISDataTypesSpecific.CheckedItems)
                        {
                            string dtype = spdt.ToString();
                            EPAUtility.NWISFileSupport nfs = new EPAUtility.NWISFileSupport();
                            nfs.writeNWISfilesWithSpecificDataParameters(subFolder, dtype);
                        }

                       
                        break;
                }
                logfileExists = true;
            }
            labelNWIS.Visible = true;
            labelNWIS.Text = "Downloaded data is located in " + aProjectFolderNWIS;
            int fileCount = countFiles;
            if (fileCount == 0)
            {
                MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(fileLocationsText, "NWIS File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            catch(ApplicationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            countFiles = 0;

            DirectoryInfo aCacheFolder = new DirectoryInfo(aCacheFolderNWIS);
            foreach (System.IO.FileInfo file in aCacheFolder.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in aCacheFolder.GetDirectories()) subDirectory.Delete(true); 
        }

        

        private List<string> getStationIDList(string aSaveAs)
        {
            List<string> aStationIDs = new List<string>();
            atcTableRDB atctable = new atcTableRDB();
            atctable.OpenFile(aSaveAs);
            int fieldnumber = atctable.FieldNumber("site_no");
            int numrecords = atctable.NumRecords;
            atctable.MoveFirst();
            for (int i = 0; i < numrecords; i++)
            {
                string stationID = atctable.get_Value(fieldnumber);
                atctable.MoveNext();
                aStationIDs.Add(stationID);
            }
            return aStationIDs;
        }
       
        private void clickHUC8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://nwis.waterdata.usgs.gov/usa/nwis/pmcodes");
            System.Diagnostics.Process.Start(sInfo);            
        }

        private void btnHUCfind_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo); 
        }

        private void btnFindHuc8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo); 
        }

        private void btnAddHuc8NHDPlus_Click(object sender, EventArgs e)
        {
            listHuc8NHDPlus.Items.Add(txtHUC8NHDPlus.Text.Trim());
        }        

        private void btnRemoveNHDPlus_Click(object sender, EventArgs e)
        {
            while (listHuc8NHDPlus.SelectedItems.Count > 0)
            {
                listHuc8NHDPlus.Items.Remove(listHuc8NHDPlus.SelectedItems[0]);
            }
        }

        private void btnAddBasins_Click(object sender, EventArgs e)
        {
            listHuc8Basins.Items.Add(txtHUC8Basins.Text.Trim());
        }

        private void btnRemoveBasins_Click(object sender, EventArgs e)
        {
            while (listHuc8Basins.SelectedItems.Count > 0)
            {
                listHuc8Basins.Items.Remove(listHuc8Basins.SelectedItems[0]);
            }
        }
  
        private void btnBrowseProjectNWIS_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectNWIS.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNWIS = this.txtProjectNWIS.Text;
            }
        }

        private void btnRunNRCSSoil_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            double dblNorth = 0;
            double dblSouth = 0;
            double dblEast = 0;
            double dblWest = 0;

            try
            {
                dblNorth = Convert.ToDouble(txtNorth.Text);
                dblSouth = Convert.ToDouble(txtSouth.Text);
                dblEast = Convert.ToDouble(txtEast.Text);
                dblWest = Convert.ToDouble(txtWest.Text);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("must be number between -67 east, -168 west, 70 north, and 25 south - " 
                                + ex.Message);
            }

            try
            {
                D4EM.Data.Region lRegion = new D4EM.Data.Region(dblNorth, dblSouth, dblWest, dblEast, D4EM.Data.Globals.GeographicProjection());
                D4EM.Data.Project lProject = new Project(D4EM.Data.Globals.AlbersProjection(),
                                                         txtCacheNRCSSoil.Text,
                                                         txtProjectFolderSoils.Text, lRegion,
                                                         false, false);
                System.Collections.Generic.List<D4EM.Data.Source.NRCS_Soil.SoilLocation.Soil> lSoils = D4EM.Data.Source.NRCS_Soil.SoilLocation.GetSoils(lProject, null);
                if ((lSoils == null) || (lSoils.Count == 0))
                {
                    MessageBox.Show("no soils found, soil list empty~");
                    return;
                }
                else
                {
                    string lSoilsTag = D4EM.Data.Source.NRCS_Soil.SoilLocation.SoilLayerSpecification.Tag;
                    D4EM.Data.Layer lSoilsLayer = lProject.LayerFromTag(lSoilsTag);
                    if (lSoilsLayer == null)
                    {
                        MessageBox.Show("Soils layer with tag '" + lSoilsTag +
                                        "' not found, aborting Test_GetSoils");
                    }
                    else if (!File.Exists(lSoilsLayer.FileName))
                    {
                        MessageBox.Show("Soils layer with tag '" + lSoilsTag +
                                        "' not found, aborting Test_GetSoils");
                    }
                    else
                    {
                        MessageBox.Show("Successful test of NRCS_Soil found " +
                                        lSoils.Count + " soils and created layer " +
                                        lSoilsLayer.FileName);
                        //opens saved directory in explorer
                        //Process.Start("explorer.exe", @"/select, " + lSoilsLayer.FileName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            this.Cursor = StoredCursor;
                    
        }      

        private void btnDowloadNWISUsingStationIds_Click(object sender, EventArgs e)
        {
            if (listNWIS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one or more data types");
                return;
            }

            if (listNWISStations.Items.Count == 0)
            {
                MessageBox.Show("Please select one or more Station IDs");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double aNorth = 0;
            double aSouth = 0;
            double aWest = 0;
            double aEast = 0;

            string aProjectFolderNWIS = System.IO.Path.Combine(txtProjectNWIS.Text.Trim(), "NWIS");
            
            string aSaveAs = System.IO.Path.Combine(aProjectFolderNWIS, "NWISstations");
            string fileLocationsText = "NWIS FILE LOCATIONS" + Environment.NewLine;
            fileLocationsText = fileLocationsText + Environment.NewLine;
            try
            {
                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderNWIS, aProjectFolderNWIS, aRegion, false, false);
                List<string> aStationIDs = new List<string>();
                D4EM.Data.Source.NWIS nwis = new NWIS();    
                D4EM.Data.LayerSpecification aStationDataType = new LayerSpecification();
                //string xmlfilename = "";
                string logfilename = System.IO.Path.Combine(aProjectFolderNWIS, "_LogFile.txt");
                fileLocationsText = fileLocationsText + "Metadata file: " + logfilename + Environment.NewLine;
                bool logfileExists = false;
                foreach (object aStationID in listNWISStations.Items)
                {
                    string stationID = aStationID.ToString();
                    aStationIDs.Add(stationID);
                }

                foreach (object adatatype in listNWIS.CheckedItems)
                {
                    string datatype = adatatype.ToString();
                    string aSaveFolder = "";
                    string subFolder = "";
                    switch (datatype)
                    {
                        case "Daily Discharge":                        
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;     
                            aSaveFolder = datatype + " StationIDs";
                            D4EM.Data.Source.NWIS.GetDailyDischarge(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype); 
                            break;
                        case "IDA Discharge":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Discharge;   
                            aSaveFolder = datatype + " StationIDs";                        
                            D4EM.Data.Source.NWIS.GetIDADischarge(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);                     
                            break;
                        case "Measurement":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.Measurement;
                            aSaveFolder = datatype + " StationIDs";
                            D4EM.Data.Source.NWIS.GetMeasurements(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);                      
                            break;
                        case "Water Quality":
                            aStationDataType = D4EM.Data.Source.NWIS.LayerSpecifications.WaterQuality;
                            aSaveFolder = datatype + " StationIDs";
                            D4EM.Data.Source.NWIS.GetWQ(aProject, aSaveFolder, aStationIDs);
                            subFolder = System.IO.Path.Combine(aProjectFolderNWIS, aSaveFolder);
                            fileLocationsText = fileLocationsText + addShpFileLocations(subFolder, datatype, false);
                            fileLocationsText = fileLocationsText + addRDBFileLocations(subFolder, datatype);
                            writeLogFile(subFolder, logfilename, logfileExists, datatype);                        
                            break;
                    }
                    logfileExists = true;
                }
            }
            catch(ApplicationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            labelNWIS.Visible = true;
            labelNWIS.Text = "Downloaded data is located in " + aProjectFolderNWIS;
            MessageBox.Show(fileLocationsText);
        }
        
        private void btnAddStations_Click(object sender, EventArgs e)
        {
            listNWISStations.Items.Add(txtNWISStation.Text.Trim());
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            while (listNWISStations.SelectedItems.Count > 0)
            {
                listNWISStations.Items.Remove(listNWISStations.SelectedItems[0]);
            }
        }

        private void btnLookUpStations_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://waterdata.usgs.gov/nwis/dv/?referred_module=sw");
            System.Diagnostics.Process.Start(sInfo);
        }


        private void btnDownloadNatureServe_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            int fileCount = 0;
            string aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();
            string aCacheFolder = txtCacheNatureServe.Text.Trim();
            string fileLocationsText = "Downloaded NatureServe files are located in " + aProjectFolderNatureServe + Environment.NewLine + Environment.NewLine;
                     
            D4EM.Data.LayerSpecification pollinator_layer = new LayerSpecification();
            bool filesExist = false;
            foreach (object pollinator in listPollinator.CheckedItems)
            {
                string _pollinator = pollinator.ToString();
                string aSubFolder = "";
                switch (_pollinator)
                {
                    case "Anna's Hummingbird (Calypte anna)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Calypte_anna;
                        filesExist = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                        fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                        fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);                           
                        break;
                    case "Eastern Tiger Swallowtail (Papilio glaucus)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Papilio_glaucus;
                        filesExist = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);                                       
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                        fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                        fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                        break;
                    case "Hermit Sphinx (Lintneria eremitus)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Lintneria_eremitus;
                        filesExist = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);      
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                        fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                        fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                        break;
                    case "Rusty-patched Bumble Bee (Bombus affinis)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Bombus_affinis;
                        filesExist = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);      
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                        fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                        fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                        break;
                    case "Southeastern Blueberry Bee (Habropoda laboriosa)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Habropoda_laboriosa;
                        filesExist = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);      
                        aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                        fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                        fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                        break;
                }
            }
            fileCount = countFiles;
            this.Cursor = StoredCursor;
            labelNatureServe.Visible = true;
            labelNatureServe.Text = "Downloaded data is located in " + aProjectFolderNatureServe;
            if (fileCount == 0)
            {
                MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(fileLocationsText, "NatureServe File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            countFiles = 0;
        }

        

        private void btnBrowseNatureServe_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNatureServe.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNHDPlus = this.txtProjectFolderNatureServe.Text;
            }
        }



        private void btnDownloadBeachGuard_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("http://publicapps.odh.ohio.gov/BeachGuardPublic/SearchResults.aspx?instatepark=False&waterbodybeach=erie");

            HttpWebResponse response = (HttpWebResponse)
            request.GetResponse();

            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            TextWriter tw = new StreamWriter(@"C:\Temp\BeachGuard.txt");
            // print out page source
            tw.WriteLine(sb.ToString());
            tw.Close();
            this.Cursor = StoredCursor;

        }

        private void btnDownloadStoret_Click(object sender, EventArgs e)
        {
            
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txtNorthStoret.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthStoret.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestStoret.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastStoret.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }
            try
            {
                int fileCount = 0;
                double nlat = Convert.ToDouble(txtNorthStoret.Text.Trim());
                double slat = Convert.ToDouble(txtSouthStoret.Text.Trim());
                double wlong = Convert.ToDouble(txtWestStoret.Text.Trim());
                double elong = Convert.ToDouble(txtEastStoret.Text.Trim());
                aProjectFolderStoret = txtProjectFolderStoret.Text.Trim();
                string fileLocationsText = "Downloaded Storet files are located in " + aProjectFolderStoret + Environment.NewLine + Environment.NewLine;
                fileLocationsText = fileLocationsText + "STORET FILE LOCATIONS for North = " + nlat + ", South = " + slat + ", East = " + elong + ", West = " + wlong + Environment.NewLine;
                fileLocationsText = fileLocationsText + Environment.NewLine;
                string bboxVal = "bBox=" + wlong + "," + slat + "," + elong + "," + nlat;

                string subFolder = System.IO.Path.Combine(aProjectFolderStoret, "N" + nlat + ";S" + slat + ";E" + elong + ";W" + wlong);

                Directory.CreateDirectory(subFolder);

                string stationsFile = System.IO.Path.Combine(subFolder, "Stations " + "N" + nlat + ";S" + slat + ";E" + elong + ";W" + wlong);
                string resultsFile = System.IO.Path.Combine(subFolder, "Results " + "N" + nlat + ";S" + slat + ";E" + elong + ";W" + wlong);

                string aParamList = bboxVal;

                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                D4EM.Data.Region aRegion = new D4EM.Data.Region(nlat, slat, wlong, elong, aDesiredProjection);

                
                bool downloadcsv = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, "csv");
                bool downloadxml = D4EM.Data.Source.Storet.GetStations(aRegion, stationsFile, aParamList, "xml");
                
                if ((downloadcsv == true) && (downloadxml == true))
                {

                    EPAUtility.StoretFileSupport storetfilesupport = new EPAUtility.StoretFileSupport();
                    storetfilesupport.WriteStoretFiles(stationsFile, subFolder, nlat, slat, elong, wlong);
                    List<string> fileNames = storetfilesupport.FileNames;
                    fileCount = fileNames.Count;
                    foreach (string file in fileNames)
                    {
                        if (Path.GetExtension(file) == ".shp")
                        {
                            fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".csv")
                        {
                            fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".xml")
                        {
                            fileLocationsText = fileLocationsText + "XML file: " + file + Environment.NewLine;
                        }
                        if (Path.GetExtension(file) == ".txt")
                        {
                            fileLocationsText = fileLocationsText + "Metadata file: " + file + Environment.NewLine;
                        }
                    }
                    string resultsext = "csv";
                    bool results = D4EM.Data.Source.Storet.GetResults(aRegion, resultsFile, aParamList, resultsext);
                    if (results == true)
                    {
                        fileLocationsText = fileLocationsText + "CSV file: " + resultsFile + "." + resultsext + Environment.NewLine;
                        
                        foreach (object datatype in listStoretDataTypes.CheckedItems)
                        {
                            string dtype = datatype.ToString();                            
                            EPAUtility.StoretFileSupport storet = new EPAUtility.StoretFileSupport();
                            storet.writeSpecificStoretFiles(resultsFile + ".csv", subFolder, dtype);
                        }
                        
                    }
                    labelStoret.Text = "Downloaded data is located in " + aProjectFolderStoret;
                    labelStoret.Visible = true;
                }
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "Storet File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
           
        }

        private void writeSpecificStoretFiles(string resultsFile, string folder, string dataType)
        {
            DataTable dt = makeStoretResultsDataTable(resultsFile);
            DataTable dtSpecific = queryStoretTable(dataType, dt);
            Directory.CreateDirectory(System.IO.Path.Combine(folder, dataType));
            TextWriter tw = new StreamWriter(System.IO.Path.Combine(folder, dataType, dataType + " Results.csv"));

            foreach (DataColumn dc in dt.Columns)
            {
                tw.Write(dc.ColumnName);
                tw.Write(",");
            }
            tw.WriteLine();

            foreach (DataRow dr in dt.Rows)
            {
                foreach (string item in dr.ItemArray)
                {
                    tw.Write(item);
                    tw.Write(",");
                }
                tw.WriteLine();
            }

            tw.Close();
        }


        private DataTable queryStoretTable(string parameter, DataTable fullTable)
        {
            DataTable dt = new DataTable();

            foreach (DataColumn dc in fullTable.Columns)
            {
                dt.Columns.Add(dc.ColumnName);
            }

            DataRow[] result = fullTable.Select("CharacteristicName = '" + parameter + "'");

            foreach (DataRow dr in result)
            {
                string[] newarray = new string[dr.ItemArray.Length];
                int i = 0;
                foreach (string item in dr.ItemArray)
                {
                    newarray[i] = item;
                    i++;
                }
                dt.Rows.Add(newarray);
            }

            return dt;
        }

        private DataTable makeStoretResultsDataTable(string resultsFile)
        {
            DataTable dt = new DataTable();
            TextReader tr = new StreamReader(resultsFile);
            string line = tr.ReadLine();
            string[] columnValues = line.Split(',');
            foreach (string columnValue in columnValues)
            {
                dt.Columns.Add(columnValue);
            }
            while ((line = tr.ReadLine()) != null)
            {
                string[] rowArray = new string[columnValues.Length];
                string[] firstSplit = line.Split('\"');
                int i = 0; //array index
                int j = 0; //keeps track of even/odds
                int k = 0; //counts number of comma corrections
                foreach (string splits in firstSplit)
                { 
                    if (j % 2 == 0)
                    {
                        string[] secondSplit = splits.Split(',');
                        foreach (string split in secondSplit)
                        {
                            
                            if (i < columnValues.Length)
                            {
                                rowArray[i] = split;                                
                            }
                            i++;
                        }
                    }
                    else
                    {
                     //   if (i < columnValues.Length)
                     //   {
                        k++;
                        i = i - k;
                            string value = splits.Replace(",", ";");
                            rowArray[i] = value;
                            i++;
                      //  }
                    }
                    j++;
                    
                }
                dt.Rows.Add(rowArray);
            }

            return dt;
        }
       

        private void btnBrowseStoret_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderStoret.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderStoret = this.txtProjectFolderStoret.Text;
            }
        }

        private void btnBrowseSoils_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderSoils.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderSoils = this.txtProjectFolderSoils.Text;
            }
        }

        private void btnDownloadFishByHUC_Click(object sender, EventArgs e)
        {            
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();
            string aHuc = txtHUC8natureServe.Text.Trim();           

            D4EM.Data.Source.NativeSpecies ns = new NativeSpecies(aProjectFolderNatureServe, aHuc);
            string tableFile = ns.csvFile;
        
            if (System.IO.File.Exists(tableFile))
            {
                System.IO.StreamReader fileReader = new StreamReader(tableFile);

                //Checking the end of file's content
                if (fileReader.Peek() != -1)
                {
                    string fileRow = fileReader.ReadLine();
                    string[] fileDataField = fileRow.Split(',');
                    int counts = fileDataField.Count();   
                    //Adding Column Header to DataGridView
                    for (int i = 0; i < counts; i++)
                    {
                        DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
                        columnDataGridTextBox.Name = fileDataField[i];
                        columnDataGridTextBox.HeaderText = fileDataField[i];
                        columnDataGridTextBox.Width = 120;
                        dataGridViewNatureServe.Columns.Add(columnDataGridTextBox);
                    } 

                    //Adding Data to DataGridView
                    while (fileReader.Peek() != -1)
                    {
                        fileRow = fileReader.ReadLine();
                        fileDataField = fileRow.Split(',');
                        dataGridViewNatureServe.Rows.Add(fileDataField);
                    }
                    fileReader.Close();
                }
            }
           
            this.Cursor = StoredCursor;
        }

        private void btnGetSpreadsheet_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string fileLocationsText = "Downloaded WDNR (STATEWIDE) files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;

                string aCacheFolderWDNR = System.IO.Path.Combine(txtCacheWDNR.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                foreach (object an in checkedListAnimals.CheckedItems)
                {
                    if (an.ToString() != "Select All")
                    {
                        string animal = an.ToString();
                        D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                        switch (animal)
                        {
                            case "Beef":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                break;
                            case "Chickens":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                break;
                            case "Dairy":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                break;
                            case "Swine":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                break;
                            case "Turkeys":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                break;
                        }
                        //  animals.Add(_animal);
                        D4EM.Data.Source.WDNR wdnr = new WDNR();
                        wdnr.getData(_animal, aProjectFolderWDNR, aCacheFolderWDNR);
                        List<string> fileNames = new List<string>();
                        fileNames.Clear();
                        fileNames = wdnr.FileNames;
                        fileCount = fileNames.Count;
                        fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                        foreach (string file in fileNames)
                        {
                            if (Path.GetExtension(file) == ".shp")
                            {
                                fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                            }
                            if (Path.GetExtension(file) == ".csv")
                            {
                                fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                            }
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                }
                labelWDNRStatewide.Visible = true;
                labelWDNRStatewide.Text = "Downloaded Data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
        }

        private void btnBrowseWDNR_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderWDNR.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderWDNR = this.txtProjectFolderWDNR.Text;
            }
        }

        Stopwatch stopwatch = new Stopwatch();

        private void btnDownloadNCDC_Click(object sender, EventArgs e)
        {
           

            if (txtToken.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a token");
                return;
            }


            if (listStatesNCDC.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a state");
                return;
            }
            if (listStatesNCDC.CheckedItems.Count > 1)
            {
                MessageBox.Show("You can only select 1 state");
                return;
            }
            if ((stopwatch.ElapsedMilliseconds < 60000) && (stopwatch.ElapsedMilliseconds != 0))
            {
                MessageBox.Show("Please wait " + (60 - stopwatch.ElapsedMilliseconds / 1000) + " more seconds");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string state = listStatesNCDC.SelectedItem.ToString();
                string token = txtToken.Text.Trim();
               // state = "ALABAMA";
                EPAUtility.NCDCSupport ncdc = new EPAUtility.NCDCSupport();
                DataTable datatable = ncdc.populateStationsTable(token, state);

                stopwatch.Restart();
                

                dataStationsNCDC.DataSource = datatable.DefaultView;
               

                DataTable datat = new DataTable();
                EPAUtility.NCDCSupport temp = new EPAUtility.NCDCSupport();
                datat = temp.createVariablesTable();

                dataVariablesNCDC.DataSource = datat.DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;                             
           MessageBox.Show("Please wait 60 seconds.  The NCDC site allows one download per minute.","",MessageBoxButtons.OK, MessageBoxIcon.Information);
           while (stopwatch.ElapsedMilliseconds < 60000)
           {
               groupBoxNCDCButtons.Visible = false;
           }
      //      Thread.Sleep(60000);
            groupBoxNCDCButtons.Visible = true;
            labelNCDC.Visible = false;
        }

        private void btnDownloadforSelectedStation_Click(object sender, EventArgs e)
        {
            if (txtToken.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a token");
                return;
            }
            if (txtStartYear.Text.Trim().Length != 4)
            {
                MessageBox.Show("Please enter a valid start year.  The year must be 4 digits (i.e. 2011)");
                return;
            }
            if (txtEndYear.Text.Trim().Length != 4)
            {
                MessageBox.Show("Please enter a valid end year.  The year must be 4 digits (i.e. 2011)");
                return;
            }
            if (txtStartMonth.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid start month.  The month must be 2 digits (i.e. 04 or 11)");
                return;
            }
            if (txtEndMonth.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid end month.  The month must be 2 digits (i.e. 04 or 11)");
                return;
            }
            if (txtStartDay.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid start day.  The day must be 2 digits (i.e. 08 or 25)");
                return;
            }
            if (txtEndDay.Text.Trim().Length != 2)
            {
                MessageBox.Show("Please enter a valid end day.  The day must be 2 digits (i.e. 08 or 25)");
                return;
            }
          
            if (outputType.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an output type");
                return;
            }
            if (dataStationsNCDC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row from the Stations grid");
                return;
            }
            if (dataStationsNCDC.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only select 1 row from the Stations grid");
                return;
            }
            if (dataVariablesNCDC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row from the Variables grid");
                return;
            }
            if (dataVariablesNCDC.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only select 1 row from the Variables grid");
                return;
            }
            if (txtProjectFolderNCDC.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a project folder (for example:  C:\\Temp\\ProjectFolder)");
                return;
            }

            if ((stopwatch.ElapsedMilliseconds < 60000) && (stopwatch.ElapsedMilliseconds != 0))
            {
                MessageBox.Show("Please wait " + (60 - stopwatch.ElapsedMilliseconds / 1000) + " more seconds");
                return;
            }
          
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                aProjectFolderNCDC = txtProjectFolderNCDC.Text.Trim();

                string yearStart = txtStartYear.Text.Trim();
                string monthStart = txtStartMonth.Text.Trim();
                if (monthStart.Length == 1)
                {
                    monthStart = "0" + monthStart;
                }
                string dayStart = txtStartDay.Text.Trim();
                if (dayStart.Length == 1)
                {
                    dayStart = "0" + dayStart;
                }
                string yearEnd = txtEndYear.Text.Trim();
                string monthEnd = txtEndMonth.Text.Trim();
                if (monthEnd.Length == 1)
                {
                    monthEnd = "0" + monthEnd;
                }
                string dayEnd = txtEndDay.Text.Trim();
                if (dayEnd.Length == 1)
                {
                    dayEnd = "0" + dayEnd;
                }

                string startDate = yearStart + monthStart + dayStart;
                string endDate = yearEnd + monthEnd + dayEnd;

                int start = Convert.ToInt32(startDate);
                int end = Convert.ToInt32(endDate);

                string token = txtToken.Text.Trim();
                
                string stationId = dataStationsNCDC.SelectedRows[0].Cells[0].Value.ToString();
                string stationName = dataStationsNCDC.SelectedRows[0].Cells[1].Value.ToString();
                string variableId = dataVariablesNCDC.SelectedRows[0].Cells[0].Value.ToString();
                string variableName = dataVariablesNCDC.SelectedRows[0].Cells[1].Value.ToString();
                double latitude = Convert.ToDouble(dataStationsNCDC.SelectedRows[0].Cells[2].Value.ToString());
                double longitude = Convert.ToDouble(dataStationsNCDC.SelectedRows[0].Cells[3].Value.ToString());

                string outputtype = outputType.SelectedItem.ToString();
                string datasettype = datasetType.SelectedItem.ToString();
                stationId = "72645599999";

                EPAUtility.NCDCSupport ncdcSupport = new EPAUtility.NCDCSupport(token, aProjectFolderNCDC, stationId, stationName, variableId, variableName, datasettype, outputtype, start, end);

                ncdcSupport.WriteNCDCValuesFile();
                stopwatch.Restart();

                string aSubFolder = System.IO.Path.Combine(aProjectFolderNCDC, stationId + "_" + variableId);
                string fileLocationsText = "Downloaded NCDC files are located in " + aProjectFolderNCDC + Environment.NewLine + Environment.NewLine;
              
                string dataType = stationName + "(" + stationId + ") " + variableName + "(" + variableId + ")";

                if (outputtype == "csv")
                {
                    fileLocationsText = fileLocationsText + addCSVFileLocations(aSubFolder, dataType);
                }
                if (outputtype == "xml")
                {
                    fileLocationsText = fileLocationsText + addXMLFileLocations(aSubFolder, dataType);
                }
                if (outputtype == "waterml")
                {
                    fileLocationsText = fileLocationsText + addWaterMLFileLocations(aSubFolder, dataType);
                }
                labelNCDC.Visible = true;
                labelNCDC.Text = "Downloaded Data is located in " + aProjectFolderNCDC;
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NCDC File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            MessageBox.Show("Please wait 60 seconds.  The NCDC site allows one download per minute.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
         
            while (stopwatch.ElapsedMilliseconds < 60000)
            {
                groupBoxNCDCButtons.Visible = false;
            }
            //   Thread.Sleep(60000);
            groupBoxNCDCButtons.Visible = true;
            labelNCDC.Visible = false;
            this.Cursor = StoredCursor;
            countFiles = 0;
        }

        private void btnFindToken(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www7.ncdc.noaa.gov/wsregistration/ws_home.html");
        }

        private void btnBrowseNCDC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNCDC.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNCDC = this.txtProjectFolderNCDC.Text;
            }
        }

        private void btnGetDataWithinBoxWDNR_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                double aNorth = Convert.ToDouble(txtNorthWDNR.Text.Trim());
                double aSouth = Convert.ToDouble(txtSouthWDNR.Text.Trim());
                double aEast = Convert.ToDouble(txtEastWDNR.Text.Trim());
                double aWest = Convert.ToDouble(txtWestWDNR.Text.Trim());
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string fileLocationsText = "Downloaded WDNR files (North = " + aNorth + ", South = " + aSouth + ", East = " + aEast + ", West = " + aWest + ")  are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;

                string aCacheFolderWDNR = System.IO.Path.Combine(txtCacheWDNR.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                foreach (object an in checkedListAnimals.CheckedItems)
                {
                    if (an.ToString() != "Select All")
                    {
                        string animal = an.ToString();
                        D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                        switch (animal)
                        {
                            case "Beef":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                break;
                            case "Chickens":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                break;
                            case "Dairy":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                break;
                            case "Swine":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                break;
                            case "Turkeys":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                break;
                        }
                        D4EM.Data.Source.WDNR wdnr = new WDNR();
                        wdnr.getDataWithinBoundingBox(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aNorth, aSouth, aEast, aWest);
                        List<string> fileNames = new List<string>();
                        fileNames.Clear();
                        fileNames = wdnr.FileNames;
                        fileCount = fileNames.Count;
                        fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                        foreach (string file in fileNames)
                        {
                            if (Path.GetExtension(file) == ".shp")
                            {
                                fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                            }
                            if (Path.GetExtension(file) == ".csv")
                            {
                                fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                            }
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                }
                labelWDNRBB.Visible = true;
                labelWDNRBB.Text = "Downloaded Data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
        }


        private void btnGetDataWithinHuc_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string aCacheFolderWDNR = System.IO.Path.Combine(txtCacheWDNR.Text.Trim(), "WDNR");
                string fileLocationsText = "Downloaded WDNR files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;
                foreach (object huc8 in listHucWDNR.Items)
                {

                    string aHuc8 = huc8.ToString();

                    fileLocationsText = fileLocationsText + "WDNR FILE LOCATIONS for " + aHuc8 + Environment.NewLine + Environment.NewLine;

                    foreach (object an in checkedListAnimals.CheckedItems)
                    {
                        if (an.ToString() != "Select All")
                        {
                        string animal = an.ToString();
                        D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                        switch (animal)
                        {
                            case "Beef":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                break;
                            case "Chickens":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                break;
                            case "Dairy":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                break;
                            case "Swine":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                break;
                            case "Turkeys":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                break;
                        }
                        animals.Add(_animal);
                        D4EM.Data.Source.WDNR wdnr = new WDNR();
                        wdnr.getDataWithinHuc8(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc8);
                        List<string> fileNames = new List<string>();
                        fileNames.Clear();
                        fileNames = wdnr.FileNames;
                        fileCount = fileNames.Count;
                        fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                        foreach (string file in fileNames)
                        {
                            if (Path.GetExtension(file) == ".shp")
                            {
                                fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                            }
                            if (Path.GetExtension(file) == ".csv")
                            {
                                fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                            }
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                labelWDNRHUC8.Visible = true;
                labelWDNRHUC8.Text = "Downloaded Data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            
        }

        private void btnAddHucWDNR_Click(object sender, EventArgs e)
        {
           listHucWDNR.Items.Add(txtHucWDNR.Text.Trim());
        }

        private void btnRemoveHucWDNR_Click(object sender, EventArgs e)
        {
            while (listHucWDNR.SelectedItems.Count > 0)
            {
                listHucWDNR.Items.Remove(listHucWDNR.SelectedItems[0]);
            }
        }

        List<string> Hucs = new List<string>();
        string aTempFolder = @"C:\Temp\TempHuc12";

        private void btnGetHuc12WithinHuc8_Click(object sender, EventArgs e)
        {
            if (listHUC8huc12.Items.Count == 0)
            {
                MessageBox.Show("Please enter at least 1 HUC-8");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;            

            try
            { 
                foreach (object huc in listHUC8huc12.Items)
                {
                    string aHUC8 = huc.ToString();
                    Hucs.Add(aHUC8);
                    string aSaveFolder = aHUC8;

                    DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                    D4EM.Data.Region aRegion = new D4EM.Data.Region(0, 0, 0, 0, aDesiredProjection);

                    D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aTempFolder, aTempFolder, aRegion, false, true);
                    D4EM.Data.LayerSpecification aDataType;
                    aDataType = BASINS.LayerSpecifications.huc12;
                    D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, aDataType);

                    string huc12ShapeFile = System.IO.Path.Combine(aTempFolder, aHUC8, "huc12.shp");
                    if (File.Exists(huc12ShapeFile))
                    {
                        IFeatureSet fs = FeatureSet.OpenFile(huc12ShapeFile);
                        int count = fs.Features.Count;

                        for (int i = 0; i < count; i++)
                        {
                            IFeature hucFeature = fs.GetFeature(i);
                            string huc12 = hucFeature.DataRow[2].ToString();
                            listHuc12WDNR.Items.Add(huc12);

                        }
                        listHuc12WDNR.Sorted = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            this.Cursor = StoredCursor;
        }

        private void btnGetDataWithinHuc12_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            if (listHuc12WDNR.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 HUC-12");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string aCacheFolderWDNR = System.IO.Path.Combine(txtCacheWDNR.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                string fileLocationsText = "Downloaded WDNR files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;
                foreach (object huc12 in listHuc12WDNR.SelectedItems)
                {
                    string aHuc12 = huc12.ToString();
                    fileLocationsText = fileLocationsText + "WDNR FILE LOCATIONS for " + aHuc12 + Environment.NewLine;

                    foreach (object an in checkedListAnimals.CheckedItems)
                    {
                        if (an.ToString() != "Select All")
                        {
                            string animal = an.ToString();
                            D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                            switch (animal)
                            {
                                case "Beef":
                                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                    break;
                                case "Chickens":
                                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                    break;
                                case "Dairy":
                                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                    break;
                                case "Swine":
                                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                    break;
                                case "Turkeys":
                                    _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                    break;
                            }
                            animals.Add(_animal);
                            D4EM.Data.Source.WDNR wdnr = new WDNR();
                            wdnr.getDataWithinHuc12(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc12);
                            List<string> fileNames = new List<string>();
                            fileNames.Clear();
                            fileNames = wdnr.FileNames;
                            fileCount = fileNames.Count;
                            fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                            foreach (string file in fileNames)
                            {
                                if (Path.GetExtension(file) == ".shp")
                                {
                                    fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                                }
                                if (Path.GetExtension(file) == ".csv")
                                {
                                    fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                                }
                            }
                            fileLocationsText = fileLocationsText + Environment.NewLine;
                        }
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                /*
                foreach (string huc in Hucs)
                {
                    string aHuc8 = huc;
                    string tempFolder = System.IO.Path.Combine(aTempFolder, aHuc8);
                    if (Directory.Exists(tempFolder))
                    {
                        string[] files = Directory.GetFileSystemEntries(tempFolder);
                        int count = files.Length;
                        for (int i = 0; i < count; i++)
                        {
                            File.Delete(files[i]);
                        }
                        Directory.Delete(tempFolder);
                    }
                    if (Directory.Exists(aTempFolder + "\\clsBASINS"))
                    {
                        string[] files = Directory.GetFileSystemEntries(aTempFolder + "\\clsBASINS");
                        int count = files.Length;
                        for (int i = 0; i < count; i++)
                        {
                            File.Delete(files[i]);
                        }
                        Directory.Delete(aTempFolder + "\\clsBASINS");
                    }
                }            
                if(Directory.Exists(aTempFolder))
                {
                    Directory.Delete(aTempFolder);
                }
                */
                labelWDNRHUC12.Visible = true;
                labelWDNRHUC12.Text = "Downloaded Data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            
        }

        private void btnAddHUC8Huc12_Click(object sender, EventArgs e)
        {
           listHUC8huc12.Items.Add(txtHuc8Huc12WDNR.Text.Trim());
        }

        private void btnRemoveHuc8Huc12_Click(object sender, EventArgs e)
        {
            while (listHUC8huc12.SelectedItems.Count > 0)
            {
                listHUC8huc12.Items.Remove(listHUC8huc12.SelectedItems[0]);
            }
        }

        private void btnNASSDownload_Click(object sender, EventArgs e)
        {
            if (txtNorthNASS.Text == "")
            {
                MessageBox.Show("Please give a value for North");
                return;
            }
            if (txtSouthNASS.Text == "")
            {
                MessageBox.Show("Please give a value for South");
                return;
            }
            if (txtWestNASS.Text == "")
            {
                MessageBox.Show("Please give a value for West");
                return;
            }
            if (txtEastNASS.Text == "")
            {
                MessageBox.Show("Please give a value for East");
                return;
            }
            if (listYearsNASS.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 year");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            
            aProjectFolderNASS = txtProjectFolderNASS.Text.Trim();
            aCacheFolderNASS = txtCacheNASS.Text.Trim();
            string fileLocationsText = "Downloaded NASS files are located in " + aProjectFolderNASS + Environment.NewLine + Environment.NewLine;
          //  int fileCount = 0;
            string nassFilename = "";
            foreach (object yr in listYearsNASS.CheckedItems)
            {
                int year = Convert.ToInt32(yr);

                double north = Convert.ToDouble(txtNorthNASS.Text.Trim());
                double south = Convert.ToDouble(txtSouthNASS.Text.Trim());
                double west = Convert.ToDouble(txtWestNASS.Text.Trim());
                double east = Convert.ToDouble(txtEastNASS.Text.Trim());

                fileLocationsText = fileLocationsText + year + " NASS FILE LOCATIONS for North = " + north + ", South = " +south + ", East = " + east + ", West = " +west + Environment.NewLine;
               
                string aSubFolder = System.IO.Path.Combine(aProjectFolderNASS, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west);
                var lProject = new Project(NASS.NativeProjection, aCacheFolderNASS, aProjectFolderNASS, new D4EM.Data.Region(north, south, west, east, KnownCoordinateSystems.Geographic.World.WGS1984), false, false);
                nassFilename = D4EM.Data.Source.NASS.getRaster(lProject, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west, "", year);
               // string nassFilename = D4EM.Data.Source.NASS.getData(aSubFolder, aCacheFolderNASS, "", year, north, south, east, west);
                fileLocationsText += "tif file: " + nassFilename + Environment.NewLine;
                fileLocationsText += "metadata file: " + nassFilename + ".xml" + Environment.NewLine;

                string countyShapeFile = Path.GetFullPath(@"..\..\..\..\Externals\data\national\cnty.shp");
                string stateShapeFile = Path.GetFullPath(@"..\..\..\..\Externals\data\national\st.shp");
                D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.county, countyShapeFile);
                D4EM.Data.National.set_ShapeFilename(D4EM.Data.National.LayerSpecifications.state, stateShapeFile);
                NASS.getStatistics(lProject, year.ToString() + ";N" + north + ";S" + south + ";E" + east + ";W" + west, year.ToString());
            }
            this.Cursor = StoredCursor;
            labelNASS.Visible = true;
            labelNASS.Text = "Downloaded Data is located in " + aProjectFolderNASS;

            
            if (nassFilename == "")
            {
                MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(fileLocationsText, "NASS File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowseNASS_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNASS.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNASS = this.txtProjectFolderNASS.Text;
            }
        }

        private void btnRunNDBC_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                aProjectFolderNDBC = txtProjectFolderNDBC.Text.Trim();

                dataGridViewNDBC.Rows.Clear();
                dataGridViewNDBC.Refresh();

                //http://www.ndbc.noaa.gov/data/latest_obs/W.rss 
                //http://www.ndbc.noaa.gov/rss/ndbc_obs_search.php?lat=X&lon=Y 

                double lat = Convert.ToDouble(txtLatitudeNDBC.Text.Trim());
                double lng = Convert.ToDouble(txtLongitudeNDBC.Text.Trim());
                double radius = Convert.ToDouble(txtRadiusNDBC.Text.Trim());
                string aSaveFolder = "Lat" + lat + ";Lng" + lng + ";Radius" + radius;
                D4EM.Data.Source.NDBC ndbc = new NDBC(aProjectFolderNDBC, aSaveFolder, lat, lng, radius);


                List<string> fileNames = ndbc.FileNames;
                int fileCount = fileNames.Count;
                string fileLocationsText = "Downloaded NDBC files are located in " + aProjectFolderNDBC + Environment.NewLine;
                fileLocationsText = fileLocationsText + Environment.NewLine;
                foreach (string file in fileNames)
                {
                    if (Path.GetExtension(file) == ".shp")
                    {
                        fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                    }
                    if (Path.GetExtension(file) == ".csv")
                    {
                        fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                    }
                    if (Path.GetExtension(file) == ".txt")
                    {
                        fileLocationsText = fileLocationsText + "Metadata file: " + file + Environment.NewLine;
                    }
                }
                DataTable dt = ndbc.dataTable;

                foreach (DataColumn dc in dt.Columns)
                {
                    DataGridViewTextBoxColumn columnDataGrid = new DataGridViewTextBoxColumn();
                    columnDataGrid.Name = dc.ColumnName;
                    //  columnDataGridTextBox.HeaderText = fileDataField[i];
                    columnDataGrid.Width = 120;
                    dataGridViewNDBC.Columns.Add(columnDataGrid);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridViewNDBC.Rows.Add(dr.ItemArray);
                }
                labelNDBC.Text = "Downloaded data is located at " + aProjectFolderNDBC;
                labelNDBC.Visible = true;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NDBC File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
        }

        private void btnBrowseNDBC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNDBC.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNDBC = this.txtProjectFolderNDBC.Text;
            }
        }

        private void btnBrowseCacheNLCD_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolderUSGS_Seamless.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderUSGS_Seamless = this.txtCacheFolderUSGS_Seamless.Text;
            }
        }

        private void btnBrowseCacheBasins_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheBasins.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderBasins = this.txtCacheBasins.Text;
            }
        }

        private void btnBrowseCacheNHDPlus_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheNHDPlus.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNHDPlus = this.txtCacheNHDPlus.Text;
            }
        }

        private void btnBrowseCacheNRCSSoil_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheNRCSSoil.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNRCSSoil = this.txtCacheNRCSSoil.Text;
            }
        }

        private void btnBrowseCacheNCDC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheNCDC.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNCDC = this.txtCacheNCDC.Text;
            }
        }

        private void btnBrowseCacheNatureServe_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheNatureServe.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNatureServe = this.txtCacheNatureServe.Text;
            }
        }


        private void btnBrowseCacheWDNR_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheWDNR.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderWDNR = this.txtCacheWDNR.Text;
            }
        }

        private void btnBrowseCacheNASS_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheNASS.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNASS = this.txtCacheNASS.Text;
            }
        }

        private void btnBrowseNDBC_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNDBC.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNDBC = this.txtProjectFolderNDBC.Text;
            }
        }

        private void listNWIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listNWIS.SelectedItem.ToString().Equals("Water Quality"))
            {                
                groupNWISspecific.Visible = true;
            }
        }

        private void btnRunNLDAS_Click(object sender, EventArgs e)
        {

            aParameters.ProjectFileFullPath = "";
            aParameters.CacheFolder = txtCacheFolderNLDAS.Text;
            aParameters.ProjectsPath = txtProjectFolderNLDAS.Text;

            //aParameters.SimulationStartYear = Convert.ToInt32(txtStartYear.Text);
            //aParameters.SimulationEndYear = Convert.ToInt32(txtEndYear.Text);

            //prompt user to select a huc folder
            this.folderBrowserDialog1.ShowNewFolderButton = false; //can't create new folder
            this.folderBrowserDialog1.SelectedPath = txtProjectFolderNLDAS.Text;
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string huc = this.folderBrowserDialog1.SelectedPath;
                huc = huc.Substring(huc.Length - 8);
                aParameters.SelectedKeys = BuildHSPF.ParseNumericKeys(8, huc); //gets huc #
                txtHucNLDAS.Text = aParameters.SelectedKeys[0];
                //getting selection layer from SDMParameters.txt in huc#\ folder
                string hucDir = txtProjectFolderNLDAS.Text + "\\" + txtHucNLDAS.Text + "\\";
                StreamReader sr = new StreamReader(hucDir + "SDMParameters.xml");
                string sParams = sr.ReadToEnd();
                int lStartPos = sParams.IndexOf("<region>");
                if (lStartPos > -1)
                {
                    int lEndPos = sParams.IndexOf("</region>", lStartPos);
                    aParameters.SelectionLayer = sParams.Substring(lStartPos, lEndPos - lStartPos + 9);
                }
                else
                {
                    MessageBox.Show("No region in SDMParameters.xml file. Try a different huc");
                }

                string startYearTag = "<SimulationStartYear>";
                //string startYearCloseTag = "</SimulationStartYear>";
                lStartPos = sParams.IndexOf(startYearTag);
                if (lStartPos > -1)
                {
                    //int lEndPos = sParams.IndexOf(startYearCloseTag, lStartPos);
                    //string startYear = sParams.Substring(lStartPos, lEndPos - lStartPos + 22);
                    string startYear = sParams.Substring(lStartPos + startYearTag.Length, 4); //assuming 4 digit date
                    aParameters.SimulationStartYear = Convert.ToInt32(startYear);
                }

                string endYearTag = "<SimulationEndYear>";
                lStartPos = sParams.IndexOf(endYearTag);
                if (lStartPos > -1)
                {
                    string endYear = sParams.Substring(lStartPos + endYearTag.Length, 4);
                    aParameters.SimulationEndYear = Convert.ToInt32(endYear);
                }
                sr.Close();

                D4EM.Data.Region lRegion = new D4EM.Data.Region(aParameters.SelectionLayer);
                D4EM.Data.Project lProject = BuildHSPF.CreateNewProject(lRegion, aParameters);
                //BuildHSPF.DownloadDataSetupModels(lProject, aParameters);

                //string aProjectFolderNLDAS = @"C:\Temp\ProjectFolderNLDAS\" + "\\" + huc + "\\met\\";
                string aProjectFolderNLDAS = aParameters.ProjectsPath + "\\" + huc + "\\met\\";
                string aCacheFolderNLDAS = System.IO.Path.Combine(aProjectFolderNLDAS, "Cache");

                aParameters.ProjectsPath = aProjectFolderNLDAS;

                Cursor StoredCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                //double latitude = Convert.ToDouble(txtLatitudeNLDAS.Text);
                //double longitude = Convert.ToDouble(txtLongitudeNLDAS.Text);

                //from frmSpecifyProject.vb (ln193), SDMProjectBuilderPlugin, SDMProjectBuilder (15May13)
                aParameters.NLDASconstituents.Clear();
                aParameters.NLDASconstituents.Add("apcpsfc");

                GetNLDAS(lProject, aParameters);

                this.Cursor = StoredCursor;

                lblNLDAS.Visible = true;
                lblNLDAS.Text = "Downloaded data is located in " + aProjectFolderNLDAS;
            }
            else
            {
                MessageBox.Show("Select a huc folder and click 'Open'");
            }
            
        }

        private void GetNLDAS(D4EM.Data.Project aProject, D4EMInterface.HSPFParameters aParameters)
        {
            if (aParameters.NLDASconstituents.Count > 0)
            {
                try
                {

                    D4EM.Data.Source.NLDAS nldas = new D4EM.Data.Source.NLDAS();

                    string lMetDataFolder = System.IO.Path.Combine(aProject.ProjectFolder, "met");
                    string lDestinationWDMfilename = System.IO.Path.Combine(lMetDataFolder, "met.wdm");
                    List<D4EM.Data.Source.NLDAS.NLDASGridCoords> lAllNLDAScells = new List<D4EM.Data.Source.NLDAS.NLDASGridCoords>();
                    lAllNLDAScells = D4EM.Data.Source.NLDAS.GetGridCellsInRegion(aProject.Region);

                    List<D4EM.Data.Source.NLDAS.NLDASGridCoords> lNLDAScellsToUse = new List<D4EM.Data.Source.NLDAS.NLDASGridCoords>();
                    lNLDAScellsToUse.Add(lAllNLDAScells[Convert.ToInt32(Math.Floor(lAllNLDAScells.Count / 2.0))]);
                    
                    NLDAS.GetParameter(aProject, lMetDataFolder, lNLDAScellsToUse, null,
                        new DateTime(aParameters.SimulationStartYear, 1, 1, 0, 0, 0),
                        new DateTime(aParameters.SimulationEndYear, 12, 31, 23, 0, 0), lDestinationWDMfilename);

                    //precip has now been added to met WDM

                    //modify WDM so NLDAS precip will be used instead of BASINS / NCDC precip
                    //atcDataSourceWDM lMetWDM = new atcDataSourceWDM();
                    //lMetWDM.Open(lDestinationWDMfilename);

                    atcDataSourceWDM lWDM = new atcDataSourceWDM();
                    atcDataManager.OpenDataSource(lWDM, lDestinationWDMfilename, null);
                    
                    if (lWDM != null)
                    {
                        atcTimeseriesGroup lAllPrecip = new atcTimeseriesGroup();
                        lAllPrecip = lWDM.DataSets.FindData("Constituent", "PREC");
                        atcTimeseries lOriginalPrecip = new atcTimeseries(lWDM);
                        lOriginalPrecip = lAllPrecip[0];
                        atcTimeseries lNLDASPrecip = new atcTimeseries(lWDM);
                        lNLDASPrecip = lWDM.DataSets.FindData("Scenario", "NLDAS")[0];

                        if (lWDM.DataSets.FindData("Scenario", "NLDAS").Count > 1)
                        {
                            //removes any extra copies of NLDAS precip
                            lAllPrecip.Remove(lWDM.DataSets.FindData("Scenario", "NLDAS"));
                            lAllPrecip.Add(lNLDASPrecip);
                        }

                        string[] lAttributeNames = new string[] { "Scenario", "Location", "Stanam" };

                        foreach (string lAttributeName in lAttributeNames)
                        {
                            lNLDASPrecip.Attributes.SetValue(lAttributeName, lOriginalPrecip.Attributes.GetValue(lAttributeName));
                        }

                        foreach (atcTimeseries lPrecip in lAllPrecip)
                        {
                            if (lPrecip.Serial != lNLDASPrecip.Serial)
                            {
                                lPrecip.Attributes.SetValue("Scenario", "Replaced");
                                lPrecip.Attributes.SetValue("Constituent", "Replaced");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message == "Retry Query")
                    {

                    }
                    else throw ex;
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnSelectAllHuc12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listHuc12WDNR.Items.Count; i++)
            {
                listHuc12WDNR.SetSelected(i, true);
            } 
        }

        private void checkedListAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListAnimals.SelectedIndex == 0)
            {
                for (int i = 1; i < checkedListAnimals.Items.Count; i++)
                {
                    checkedListAnimals.SetItemChecked(i, checkedListAnimals.GetItemChecked(0));
                }
            }
            else
            {
                if (!checkedListAnimals.GetItemChecked(checkedListAnimals.SelectedIndex))
                {
                    checkedListAnimals.SetItemChecked(0, false);
                }
            } 
        }

        private void boxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxLayer.SelectedIndex == 0)
            {
                for (int i = 1; i < boxLayer.Items.Count; i++)
                {
                    boxLayer.SetItemChecked(i, boxLayer.GetItemChecked(0));
                }
            }
            else
            {
                if (!boxLayer.GetItemChecked(boxLayer.SelectedIndex))
                {
                    boxLayer.SetItemChecked(0, false);
                }
            } 
        }

        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;

        private void btnRunNAWQA_Click(object sender, EventArgs e)
        {
             
            string[] counties = null;

            labelNAWQA.Visible = false;

            string aProjectFolderNAWQA = txtProjectFolderNAWQA.Text.Trim();
            aCacheFolderNAWQA = txtCacheFolderNAWQA.Text.Trim();

           // string subFolder = "";
          //  string fileName = "";
            string waterType = "";
            string state = "";
            string startYear = "";
            string endYear = "";
            for(int i=0;i< groupNAWQAwaterType.Controls.Count;i++)
            {
                RadioButton rb= (RadioButton)groupNAWQAwaterType.Controls[i];
                if(rb.Checked == true)
                {
                    waterType = rb.Text;
                    if (waterType == "Groundwater")
                    {
                        waterType = "groundwater";
                    }
                    else if (waterType == "Surfacewater and Groundwater")
                    {
                        waterType = "surfacegroundwater";
                    }
                }
            }
            if (waterType == "")
            {
                MessageBox.Show("Please select a water type");
                return;
            }
            //
            string fileType = "";
            string fileExt = "";

            for (int i = 0; i < groupNAWQAfileTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAfileTypes.Controls[i];
                if (rb.Checked == true)
                {
                    fileType = rb.Text;
                    if (fileType == "excel")
                    {
                        fileExt = "xls";
                    }
                    else if (fileType == "tab")
                    {
                        fileExt = "tsv";
                    }
                    else
                    {
                        fileExt = fileType;
                    }
                }
            }
            if (fileType == "")
            {
                MessageBox.Show("Please select a file type");
                return;
            }

            string queryType = "";

            for (int i = 0; i < groupNAWQAqueryTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAqueryTypes.Controls[i];
                if (rb.Checked == true)
                {
                    queryType = rb.Text;
                }
            }
            if (queryType == "")
            {
                MessageBox.Show("Please select a query type");
                return;
            }


            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
           
            
            if (radioUseLatLong.Checked)
            {
                if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
                {
                    MessageBox.Show("Please enter values for Latitude and Longitude");
                    this.Cursor = StoredCursor;
                    return;
                }

                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

            }

            else if (radioUseStatesCounties.Checked)
            {
                if (listCounties.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one county");
                    this.Cursor = StoredCursor;
                    return;
                }

                foreach (object st in listNAWQAstates.SelectedItems)
                {
                    state = st.ToString();
                }

                counties = new string[listCounties.CheckedItems.Count];

                int i_county = 1;
                foreach (object co in listCounties.CheckedItems)
                {
                    string county = co.ToString();
                    counties[i_county - 1] = county;
                    i_county++;
                }
            }
            else
            {
                counties = new string[0];
            }
            if (chkBoxWater.Checked)
            {
                if ((txtStartYearNAQWA.Text.Trim().Length != 4) && (txtEndYearNAQWA.Text.Trim().Length != 4))
                {
                    MessageBox.Show("Start Year and End Year must be 4 digits");
                    return;
                }
                startYear = txtStartYearNAQWA.Text.Trim();
                endYear = txtEndYearNAQWA.Text.Trim();
            }

            D4EM.Data.Source.NAWQA nawqa = new D4EM.Data.Source.NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);
            if (radioUseLatLong.Checked)
            {
                string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
                int k = 0;
                foreach (object par in listNAWQAparameters.CheckedItems)
                {
                    string parameter = par.ToString();
                    parameters[k] = parameter;
                    k++;
                }
                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());
                nawqa.getAllDataLatLong(lat, lng, parameters, startYear, endYear);
            }

            else if (radioUseStatesCounties.Checked)
            { 
                string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
                int k = 0;
                foreach (object par in listNAWQAparameters.CheckedItems)
                {
                    string parameter = par.ToString();
                    parameters[k] = parameter;
                    k++;
                }

                nawqa.getAllDataStateCounties(state, counties, parameters, startYear, endYear);               
               
            }

            this.Cursor = StoredCursor;

            labelNAWQA.Visible = true;
            labelNAWQA.Text = "Downloaded data is located in " + aProjectFolderNAWQA;
        }

        private string longStateName(string abb)
        {
            string longStateName = "";
            abb = abb.ToUpper();
            switch (abb)
            {
                case "AL":
                    longStateName = "ALABAMA";
                    break;
                case "AK":
                    longStateName = "ALASKA";
                    break;
                case "AZ":
                    longStateName = "ARIZONA";
                    break;
                case "AR":
                    longStateName = "ARKANSAS";
                    break;
                case "CA":
                    longStateName = "CALIFORNIA";
                    break;
                case "CO":
                    longStateName = "COLORADO";
                    break;
                case "CT":
                    longStateName = "CONNECTICUT";
                    break;
                case "DE":
                    longStateName = "DELAWARE";
                    break;
                case "DC":
                    longStateName = "DISTRICT OF COLUMBIA";
                    break;
                case "FL":
                    longStateName = "FLORIDA";
                    break;
                case "GA":
                    longStateName = "GEORGIA";
                    break;
                case "HI":
                    longStateName = "HAWAII";
                    break;
                case "ID":
                    longStateName = "IDAHO";
                    break;
                case "IL":
                    longStateName = "ILLINOIS";
                    break;
                case "IN":
                    longStateName = "INDIANA";
                    break;
                case "IA":
                    longStateName = "IOWA";
                    break;
                case "KS":
                    longStateName = "KANSAS";
                    break;
                case "KY":
                    longStateName = "KENTUCKY";
                    break;
                case "LA":
                    longStateName = "LOUISIANA";
                    break;
                case "ME":
                    longStateName = "MAINE";
                    break;
                case "MD":
                    longStateName = "MARYLAND";
                    break;
                case "MA":
                    longStateName = "MASSACHUSETTS";
                    break;
                case "MI":
                    longStateName = "MICHIGAN";
                    break;
                case "MN":
                    longStateName = "MINNESOTA";
                    break;
                case "MS":
                    longStateName = "MISSISSIPPI";
                    break;
                case "MO":
                    longStateName = "MISSOURI";
                    break;
                case "MT":
                    longStateName = "MONTANA";
                    break;
                case "NE":
                    longStateName = "NEBRASKA";
                    break;
                case "NV":
                    longStateName = "NEVADA";
                    break;
                case "NH":
                    longStateName = "NEW HAMPSHIRE";
                    break;
                case "NJ":
                    longStateName = "NEW JERSEY";
                    break;
                case "NM":
                    longStateName = "NEW MEXICO";
                    break;
                case "NY":
                    longStateName = "NEW YORK";
                    break;
                case "NC":
                    longStateName = "NORTH CAROLINA";
                    break;
                case "ND":
                    longStateName = "NORTH DAKOTA";
                    break;
                case "OH":
                    longStateName = "OHIO";
                    break;
                case "OK":
                    longStateName = "OKLAHOMA";
                    break;
                case "OR":
                    longStateName = "OREGON";
                    break;
                case "PA":
                    longStateName = "PENNSYLVANIA";
                    break;
                case "RI":
                    longStateName = "RHODE ISLAND";
                    break;
                case "SC":
                    longStateName = "SOUTH CAROLINA";
                    break;
                case "SD":
                    longStateName = "SOUTH DAKOTA";
                    break;
                case "TN":
                    longStateName = "TENNESSEE";
                    break;
                case "TX":
                    longStateName = "TEXAS";
                    break;
                case "UT":
                    longStateName = "UTAH";
                    break;
                case "VT":
                    longStateName = "VERMONT";
                    break;
                case "VA":
                    longStateName = "VIRGINIA";
                    break;
                case "WA":
                    longStateName = "WASHINGTON";
                    break;
                case "WV":
                    longStateName = "WEST VIRGINIA";
                    break;
                case "WI":
                    longStateName = "WISCONSIN";
                    break;
                case "WY":
                    longStateName = "WYOMOING";
                    break;
            }

            return longStateName;
        }

        private string[] getCountyStateFromLatLong(double lat, double lng)
        {
            DotSpatial.Topology.Coordinate coords = new DotSpatial.Topology.Coordinate(lng, lat);
            DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coords);
            string[] countyState = new string[2];
            string countyShapeFile = @"..\Bin\County\cnty.shp";
            IFeatureSet fs = FeatureSet.OpenFile(countyShapeFile);
            ProjectionInfo proj = new ProjectionInfo();
            proj = fs.Projection;
            fs.Reproject(KnownCoordinateSystems.Geographic.World.WGS1984);
            int count = fs.Features.Count;
            for (int i = 0; i < count; i++)
            {
                IFeature countyFeature = fs.GetFeature(i);

                if (countyFeature.Intersects(coords) == true)
                {
                    countyState[0] = countyFeature.DataRow[2].ToString();
                    countyState[1] = countyFeature.DataRow[1].ToString();
                    break;
                }
            }
            fs.Close();
            return countyState;
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }


        private void listNAWQAstates_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listCounties.Items.Clear();

            TextReader tr = new StreamReader(@"..\Bin\counties.txt");
            string line = tr.ReadLine();
            DataTable dt = new DataTable();
            dt.Columns.Add("State");
            dt.Columns.Add("County");
            while ((line = tr.ReadLine()) != null)
            {
                string[] values = line.Split('\t');
                string state = values[0].ToString().ToUpper().Trim();
                string county_long = values[1].ToString().ToUpper().Trim();
                string[] splitCounty = county_long.Split(' ');
                string county_short = "";
                for (int i = 0; i < splitCounty.Length - 1; i++)
                {
                    county_short = county_short + " " + splitCounty[i].ToString().ToUpper().Trim();
                }
                county_short = county_short.Trim();
                dt.Rows.Add(state, county_short);
            }
            tr.Close();

            string selectedState = listNAWQAstates.SelectedItem.ToString();

            DataRow[] result = dt.Select("State ='" + selectedState + "'");
            
            List<string> counties = new List<string>();
            foreach (DataRow row in result)
            {
                counties.Add(row[1].ToString());
            }
            counties.Sort();

            foreach (string county in counties)
            {
                listCounties.Items.Add(county);
            }
        }

        private void btnBrowseProjectFolderNAWQA_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNAWQA.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNAWQA = this.txtProjectFolderNAWQA.Text;
            }
        }

        private void btnBrowseCacheFolderNAWQA_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolderNAWQA.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderNAWQA = this.txtCacheFolderNAWQA.Text;
            }
        }

        private void listCounties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void btnDetermineCounty_Click(object sender, EventArgs e)
        {
            if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
            {
                MessageBox.Show("Please enter values for Latitude and Longitude");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
            double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

            string[] countyState = getCountyStateFromLatLong(lat, lng);

            string stateName = longStateName(countyState[1]);
            string[] countyNameFull = countyState[0].Split(' ');
            string countyName = countyNameFull[0].ToUpper();

            labelNAWQAlatLongCounty.Text = countyName + ", " + stateName;
            labelNAWQAlatLongCounty.Visible = true;

            this.Cursor = StoredCursor;
        }

        private string[] determineAverage(string originalFile, string county)
        {
            DataTable dt = new DataTable();
            TextReader tr = new StreamReader(originalFile);  //tsv
            string line = tr.ReadLine();
            string[] columnNames = line.Split('\t');
            foreach (string columnName in columnNames)
            {
                dt.Columns.Add(columnName);
            }

            while ((line = tr.ReadLine()) != null)
            {
                string[] cellValues = line.Split('\t');
                dt.Rows.Add(cellValues);
            }
            tr.Close();

            DataRow[] result = dt.Select("county = '" + county + "'");

            string[] returnValues = new string[3];

            if (result.Length == 0)
            {
                returnValues[0] = "No Data";
                returnValues[1] = "";
                returnValues[2] = "";
                return returnValues;
            }
            else
            {
                List<double> values = new List<double>();

                double stdDev = 0;
               
                double sum = 0;
                string units = "";
                for (int i = 0; i < result.Length; i++)
                {
                    double value = Convert.ToDouble(result[i]["value"].ToString());
                    units = result[i]["reportUnits"].ToString();
                    sum = sum + value;
                    values.Add(value);
                }
                double average = sum / result.Length;
                string average_string = average.ToString() + " " + units;

                IEnumerable<double> en = values;
                stdDev = CalculateStdDev(en);
                string stdDev_string = stdDev.ToString() + " " + units;
                returnValues[0] = average_string;
                returnValues[1] = stdDev_string;
                returnValues[2] = values.Count.ToString();
                return returnValues;
            }

        }

        

        private double CalculateStdDev(IEnumerable<double> values) 
        {      
            double ret = 0;   
            if (values.Count() > 0)    
            {            
                //Compute the Average            
                double avg = values.Average();      
                //Perform the Sum of (value-avg)_2_2            
                double sum = values.Sum(d => Math.Pow(d - avg, 2));      
                //Put it all together            
                ret = Math.Sqrt((sum) / (values.Count()-1));      
            }      
            return ret; 
        } 

        private void radioUseStatesCounties_CheckedChanged(object sender, EventArgs e)
        {
            groupNAWQAstatesCounties.Visible = true;
            groupNAWQAlatLong.Visible = false;
        }

        private void radioUseLatLong_CheckedChanged(object sender, EventArgs e)
        {
            groupNAWQAstatesCounties.Visible = false;
            groupNAWQAlatLong.Visible = true;
        }

        private void btnGetNAWQAaverageStdDev_Click(object sender, EventArgs e)
        {
            string startYear = "";
            string endYear = "";
            if (chkBoxWater.Checked)
            {
                if ((txtStartYearNAQWA.Text.Trim().Length != 4) && (txtEndYearNAQWA.Text.Trim().Length != 4))
                {
                    MessageBox.Show("Start Year and End Year must be 4 digits");
                    return;
                }
                startYear = txtStartYearNAQWA.Text.Trim();
                endYear = txtEndYearNAQWA.Text.Trim();
            }
            string aProjectFolderNAWQA = txtProjectFolderNAWQA.Text.Trim();
            aCacheFolderNAWQA = txtCacheFolderNAWQA.Text.Trim();
            gridShowNAWQAaverages.Columns.Clear();
         ///   string subFolder = "";
          //  string fileName = "";
            string waterType = "";
            for (int i = 0; i < groupNAWQAwaterType.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAwaterType.Controls[i];
                if (rb.Checked == true)
                {
                    waterType = rb.Text;
                    if (waterType == "Groundwater")
                    {
                        waterType = "groundwater";
                    }
                    else if (waterType == "Surfacewater and Groundwater")
                    {
                        waterType = "surfacegroundwater";
                    }
                }
            }
            if (waterType == "")
            {
                MessageBox.Show("Please select a water type");
                return;
            }

            string fileType = "";
            string fileExt = "";

            for (int i = 0; i < groupNAWQAfileTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAfileTypes.Controls[i];
                if (rb.Checked == true)
                {
                    fileType = rb.Text;
                    if (fileType == "excel")
                    {
                        fileExt = "xls";
                    }
                    else if (fileType == "tab")
                    {
                        fileExt = "tsv";
                    }
                    else
                    {
                        fileExt = fileType;
                    }
                }
            }
            if (fileType == "")
            {
                fileExt = "csv";
            }

            string queryType = "";

            for (int i = 0; i < groupNAWQAqueryTypes.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)groupNAWQAqueryTypes.Controls[i];
                if (rb.Checked == true)
                {
                    queryType = rb.Text;
                }
            }
            if (queryType == "")
            {
                MessageBox.Show("Please select a query type");
                return;
            }

            

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
         //   string averagesText = "";
            string state = "";
            gridShowNAWQAaverages.Visible = true;
            DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
            columnDataGridTextBox.Name = "County";
            columnDataGridTextBox.HeaderText = "County";
            columnDataGridTextBox.Width = 220;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox);
            DataGridViewTextBoxColumn columnDataGridTextBox3 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox3.Name = "Parameter";
            columnDataGridTextBox3.HeaderText = "Parameter";
            columnDataGridTextBox3.Width = 320;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox3);
            DataGridViewTextBoxColumn columnDataGridTextBox4 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox4.Name = "Average Value";
            columnDataGridTextBox4.HeaderText = "Average Value";
            columnDataGridTextBox4.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox4);
            DataGridViewTextBoxColumn columnDataGridTextBox5 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox5.Name = "Standard Deviation";
            columnDataGridTextBox5.HeaderText = "Standard Deviation";
            columnDataGridTextBox5.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox5);
            DataGridViewTextBoxColumn columnDataGridTextBox6 = new DataGridViewTextBoxColumn();
            columnDataGridTextBox6.Name = "# Observations";
            columnDataGridTextBox6.HeaderText = "# Observations";
            columnDataGridTextBox6.Width = 120;
            gridShowNAWQAaverages.Columns.Add(columnDataGridTextBox6);

            string[] counties = null;
            string[] parameters = new string[listNAWQAparameters.CheckedItems.Count];
            int j = 0;
            foreach (string parameter in listNAWQAparameters.CheckedItems)
            {
                parameters[j] = parameter;
                j++;
            }

            D4EM.Data.Source.NAWQA nawqa = new NAWQA(aProjectFolderNAWQA, aCacheFolderNAWQA, waterType, fileType, queryType);

            if (radioUseStatesCounties.Checked)
            {
                state = listNAWQAstates.SelectedItem.ToString();
                counties = new string[listCounties.CheckedItems.Count];
                int k = 0;
               // int i_county = 1;
                foreach (string county in listCounties.CheckedItems)
                {
                    counties[k] = county;
                    k++;
                }               
                nawqa.getAverageAndStandardDeviationStateCounties(state, counties, parameters, startYear, endYear);
            }

            else if(radioUseLatLong.Checked)
            {
                if ((txtNAWQAlat.Text == "") || (txtNAWQAlng.Text == ""))
                {
                    MessageBox.Show("Please enter values for Latitude and Longitude");
                    this.Cursor = StoredCursor;
                    return;
                }

                double lat = Convert.ToDouble(txtNAWQAlat.Text.Trim());
                double lng = Convert.ToDouble(txtNAWQAlng.Text.Trim());

                nawqa.getAverageAndStandardDeviationLatLong(lat, lng, parameters, startYear, endYear);
            }
            DataTable dt = nawqa.dtAverages;

            foreach (DataRow dr in dt.Rows)
            {
              gridShowNAWQAaverages.Rows.Add(dr.ItemArray);
            }

           // gridShowNAWQAaverages.DataSource = dt;

            labelNAWQA.Visible = true;
            labelNAWQA.Text = "Downloaded data is located in " + aProjectFolderNAWQA;
            this.Cursor = StoredCursor;
        }

        private void btnCreateHUCNativeFishSpeciesFile_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();
            string aHuc = txtHUC8natureServe.Text.Trim();
            string HUCshapefilename = @"..\Bin\huc250d3.shp";
          //  IFeature _aHucPolygon = null;
            IFeatureSet fs = FeatureSet.OpenFile(HUCshapefilename);
            int count = fs.Features.Count;

            TextWriter tw = new StreamWriter(@"C:\Temp\HUCnativeSpecies.csv");

            tw.WriteLine("HUC-8, Common Name, Scientific Name");
            for (int i = 0; i < count; i++)
            {
                IFeature hucFeature = fs.GetFeature(i);
                string huc = hucFeature.DataRow[2].ToString();
                D4EM.Data.Source.NativeSpecies ns = new NativeSpecies(aProjectFolderNatureServe, huc);
                string tableFile = ns.csvFile;

                if (System.IO.File.Exists(tableFile))
                {
                    System.IO.StreamReader fileReader = new StreamReader(tableFile);

                    //Checking the end of file's content
                    if (fileReader.Peek() != -1)
                    {
                        string fileRow = fileReader.ReadLine();
                        string[] fileDataField = fileRow.Split(',');
                        int counts = fileDataField.Count();
                        while (fileReader.Peek() != -1)
                        {
                            fileRow = fileReader.ReadLine();
                            fileDataField = fileRow.Split(',');
                            string commonName = fileDataField[1];
                            string scientificName = fileDataField[0];
                            tw.WriteLine("'" + huc + ", " + commonName + ", " + scientificName);
                        }
                        fileReader.Close();
                    }

                    File.Delete(tableFile);
                }
                
            }
            fs.Close();
            tw.Close();
            

            this.Cursor = StoredCursor;
        }

        private void btnRunHSPF_Click(object sender, EventArgs e)
        {
            //Build parameter values
            //aParameters.ProjectFileFullPath = txtProjectFolderHSPF.Text + "\\national\\ProjectBuilder.dspx";
            aParameters.ProjectFileFullPath = "";

            if (txtProjectFolderHSPF.TextLength < 1)
            {
                MessageBox.Show("Locate a project folder");
                btnBrowseProjHSPF_Click(sender, e);
            }
            else aParameters.ProjectsPath = txtProjectFolderHSPF.Text;

            if (txtCacheFolderHSPF.TextLength < 1)
            {
                MessageBox.Show("Locate a cache folder");
                btnBrowseCacheHSPF_Click(sender, e);
            }
            else aParameters.CacheFolder = txtCacheFolderHSPF.Text;

            if (txtHucNumHSPF.TextLength < 1)
            {
                MessageBox.Show("Select a huc directory");
                btnBrowseHSPF_Click(sender, e);
            }


            string hucDir = txtProjectFolderHSPF.Text + "\\" + txtHucNumHSPF.Text + "\\";
            StreamReader sr = new StreamReader(hucDir + "SDMParameters.xml");
            string sParams = sr.ReadToEnd();

            string startTag = "<region>";
            int lStartPos = sParams.IndexOf(startTag);
            string endTag = startTag.Insert(1, "/");
            if (lStartPos > -1)
            {
                int lEndPos = sParams.IndexOf(endTag, lStartPos);
                aParameters.SelectionLayer = sParams.Substring(lStartPos, lEndPos - lStartPos + endTag.Length);
            }
            else
            {
                MessageBox.Show("No region in SDMParameters.xml file. Try a different huc");
            }

            //Gets layer parameters from SDMParameters.xml
            getParam("<SimulationStartYear>", sParams);
            getParam("<SimulationEndYear>", sParams);
            getParam("<MinimumCatchmentArea>", sParams);
            getParam("<MinimumStreamLength>", sParams);
            getParam("<MinumumLandUsePercent>", sParams);
            getParam("<CatchmentsMethod>", sParams);
            getParam("<SoilSource>", sParams);

            List<string> list = new List<string>();
            string[] basins = new string[5] { "PREC", "ATEM", "SOLR", "CLOU", "WIND" };
            //append basins param names to list
            for (int i = 0; i < basins.Length; i++)
            {
                list.Add(basins[i]);
            }
            aParameters.BasinsMetConstituents = list;

            List<string> keys = new List<string>();
            keys.Add(txtHucNumHSPF.Text);
            aParameters.SelectedKeys = keys;

            sr.Close();

            D4EM.Data.Region lRegion = new D4EM.Data.Region(aParameters.SelectionLayer);
            D4EM.Data.Project lProject = BuildHSPF.CreateNewProject(lRegion, aParameters);
            BuildHSPF.DownloadDataSetupModels(lProject, aParameters);
            lblHSPFComplete.Visible = true;
            lblHSPFComplete.Text = txtHucNumHSPF.Text + ".uci created in " + txtProjectFolderHSPF.Text + "\\" + txtHucNumHSPF.Text; 
        }

        private void getParam(string startTag, string sParams)
        {
            string endTag = startTag.Insert(1, "/");
            int startPos = sParams.IndexOf(startTag);
            int endPos = sParams.IndexOf(endTag, startPos);
            if (startPos > -1)
            {
                string param = sParams.Substring(startPos + startTag.Length, endPos - endTag.Length - startPos + 1); //assuming 4 digit date
                string paramName = startTag.Substring(1, startTag.Length - 2); //remove < and >

                switch (paramName)
                {
                    case "SimulationStartYear":
                        aParameters.SimulationStartYear = Convert.ToInt32(param);
                        txtSimStartYearHSPF.Text = param;
                        break;
                    case "SimulationEndYear":
                        aParameters.SimulationEndYear = Convert.ToInt32(param);
                        txtSimEndYearHSPF.Text = param;
                        break;
                    case "MinimumCatchmentArea":
                        aParameters.MinCatchmentKM2 = Convert.ToDouble(param);
                        txtMinCatchKM2.Text = param;
                        break;
                    case "MinimumStreamLength":
                        aParameters.MinFlowlineKM = Convert.ToDouble(param);
                        txtMinFlowlineKM.Text = param;
                        break;
                    case "MinumumLandUsePercent":
                        aParameters.LandUseIgnoreBelowFraction = Convert.ToDouble(param);
                        txtLanduseIgnoreBelowFraction.Text = param;
                        break;
                    case "CatchmentsMethod":
                        aParameters.CatchmentsMethod = param;
                        break;
                    case "SoilSource":
                        aParameters.SoilSource = param;
                        break;
                }
            }
        }

        private void btnRunSWAT_Click(object sender, EventArgs e)
        {
         //   D4EM.Model.SWAT.SWATmodel.BuildSWATInput(
        }

        private void btnDownloadNASS_Click(object sender, EventArgs e)
        {

        }

        private void btnRunDelineation_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNDBChistoricalData_Click(object sender, EventArgs e)
        {
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            aProjectFolderNDBC = txtProjectFolderNDBC.Text.Trim();
            string stationID = txtNDBCStationID.Text.Trim();
            string year = txtNDBCyear.Text.Trim();
            string aSaveFolder = "Historical Data for Station " + stationID;
            string folder = Path.Combine(aProjectFolderNDBC, aSaveFolder);
            D4EM.Data.Source.NDBC ndbc = new NDBC();
            DataTable dt = ndbc.getHistoricalData(folder, stationID, year);

            foreach (DataColumn dc in dt.Columns)
            {
                DataGridViewTextBoxColumn columnDataGrid = new DataGridViewTextBoxColumn();
                columnDataGrid.Name = dc.ColumnName;
                //  columnDataGridTextBox.HeaderText = fileDataField[i];
                columnDataGrid.Width = 120;
                dataGridViewNDBC.Columns.Add(columnDataGrid);
            }
            foreach (DataRow dr in dt.Rows)
            {
                dataGridViewNDBC.Rows.Add(dr.ItemArray);
            }
            this.Cursor = StoredCursor;

        }
        private void txtNorth_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.InitialDelay = 0;
            tip.ShowAlways = true;
            tip.Show("show valid values", groupBox38); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }

        private void btnBrowseHSPF_Click(object sender, EventArgs e)
        {
            //prompt user to select a huc folder
            this.folderBrowserDialog1.ShowNewFolderButton = false; //can't create new folder
            this.folderBrowserDialog1.SelectedPath = @"C:\D4EMFromGoogleCodeSVN\D4EM_05-30-13_NLDAS\Externals\data\10010002\";
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string huc = this.folderBrowserDialog1.SelectedPath;
                huc = huc.Substring(huc.Length - 8);
                txtHucNumHSPF.Text = huc;
            }
        }

        private void btnBrowseProjHSPF_Click(object sender, EventArgs e)
        {
            //prompt user to select a huc folder
            this.folderBrowserDialog1.ShowNewFolderButton = false; //can't create new folder
            this.folderBrowserDialog1.SelectedPath = @"C:\D4EMFromGoogleCodeSVN\D4EM_05-30-13_NLDAS\Externals\data\";
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string projFold = this.folderBrowserDialog1.SelectedPath;
                txtProjectFolderHSPF.Text = projFold;
            }
            else
            {
                MessageBox.Show("Select a project folder");
            }
        }

        private void btnBrowseCacheHSPF_Click(object sender, EventArgs e)
        {
            //prompt user to select a huc folder
            this.folderBrowserDialog1.ShowNewFolderButton = false; //can't create new folder
            this.folderBrowserDialog1.SelectedPath = @"C:\D4EMFromGoogleCodeSVN\D4EM_05-30-13_NLDAS\Externals\cache\";
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string cacheFold = this.folderBrowserDialog1.SelectedPath;
                txtCacheFolderHSPF.Text = cacheFold;
            }
            else
            {
                MessageBox.Show("Select a cache folder");
            }
        }

        private void lblHSPFComplete_Click(object sender, EventArgs e)
        {

        }

    }    
}


