using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using D4EM.Data;
using D4EM.Data.Source;
using D4EM.Geo;
using System.Collections;
using System.IO;
using System.IO.Compression;
using atcUtility;
using System.Xml;
using D4EMPlugins;

namespace D4EM_USGS_Seamless
{
    public partial class USGS_SeamlessBox : Form
    {
        int countFiles = 0;
        double _north = 0;
        double _south = 0;
        double _east = 0;
        double _west = 0;
        string _filename = "";
        public D4EM.Data.Project aProject = null;
        string aProjectFolderUSGS_Seamless = "";
        string aCacheFolderUSGS_Seamless;

        List<string> hucnums = null;

        public USGS_SeamlessBox(double north, double south, double east, double west, List<string> huc8nums)
        {
            InitializeComponent();
            _north = north;
            _south = south;
            _east = east;
            _west = west;
            hucnums = huc8nums;
        }

        private void btnRunUSGS_Seamless_Click(object sender, EventArgs e)
        {
            bool logfileExists = false;

            string aProjectFolderUSGS_Seamless = System.IO.Path.Combine(txtProjectFolderUSGS_Seamless.Text.Trim(), "USGS_Seamless");
            aCacheFolderUSGS_Seamless = txtCacheFolderUSGS_Seamless.Text.Trim();

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

            if (txtProjectFolderUSGS_Seamless.Text == "")
            {
                MessageBox.Show("Please enter a Project Folder directory");
                return;
            }

            TextWriter file = new StreamWriter(@"C:\Temp\DownloadedFilePathUSGS_Seamless");
            string aSaveFolder = "";
            string fileLocationsText = "Downloaded USGS_Seamless files are located in " + aProjectFolderUSGS_Seamless + Environment.NewLine + Environment.NewLine;

            bool dataDownloaded = false;

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Double aNorth = Convert.ToDouble(txtNorthUSGS_Seamless.Text.Trim());
                Double aSouth = Convert.ToDouble(txtSouthUSGS_Seamless.Text.Trim());
                Double aWest = Convert.ToDouble(txtWestUSGS_Seamless.Text.Trim());
                Double aEast = Convert.ToDouble(txtEastUSGS_Seamless.Text.Trim());

                DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                //      DotSpatial.Projections.ProjectionInfo aDesiredProjection = DotSpatial.Projections.KnownCoordinateSystems.Projected.World.WebMercator;
                D4EM.Data.Region aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, aDesiredProjection);
                aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderUSGS_Seamless, aProjectFolderUSGS_Seamless, aRegion, true, true);
                string logfilename = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, "_LogFile.txt");
                fileLocationsText = fileLocationsText + "Metadata: " + logfilename + Environment.NewLine;

                foreach (object layerspec in boxLayer.CheckedItems)
                {
                    if (layerspec.ToString() != "Select All")
                    {
                        string layer = layerspec.ToString();
                        string xmlfilename = "";
                        string aSubFolder = "";
                        LayerSpecification layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover;
                        switch (layer)
                        {
                            case "1992 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD1992.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    xmlfilename = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder, "NLCD_LandCover_1992.tif.xml");
                                    string[] tif1992LandCover = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif1992LandCover.Length > 0)
                                    {
                                        file.WriteLine(tif1992LandCover[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2001LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }        
                                 * */
                                break;

                            case "2001 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                string st = D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    xmlfilename = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder, "NLCD_landcover_2001.xml");
                                    string[] tif2001LandCover = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif2001LandCover.Length > 0)
                                    {
                                        file.WriteLine(tif2001LandCover[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite1992LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }         */
                                break;
                            case "2001 Canopy":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2001.Canopy;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    xmlfilename = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder, "NLCD_NLCD2001.Canopy_2001.tif.xml");
                                    string[] tif2001Canopy = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif2001Canopy.Length > 0)
                                    {
                                        file.WriteLine(tif2001Canopy[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
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
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    string[] tif2001Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif2001Impervious.Length > 0)
                                    {
                                        file.WriteLine(tif2001Impervious[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                //   xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_impervious_2001.xml");
                                /* if (File.Exists(xmlfilename) == true)
                                 {
                                     ReadXMLandWriteLog readWrite2001Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                 }     */
                                break;
                            case "2006 LandCover":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.LandCover;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    string[] tif2006LandCover = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif2006LandCover.Length > 0)
                                    {
                                        file.WriteLine(tif2006LandCover[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                //  xmlfilename = System.IO.Path.Combine(aProjectFolderNLCD, aSubFolder, "NLCD_landcover_2006.tif.xml");
                                /*   if (File.Exists(xmlfilename) == true)
                                   {
                                       ReadXMLandWriteLog readWrite2006LandCover = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                   }
                                  */
                                break;
                            case "2006 Impervious":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NLCD2006.Impervious;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    string[] tif2006Impervious = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tif2006Impervious.Length > 0)
                                    {
                                        file.WriteLine(tif2006Impervious[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }   
                                 * */
                                break;
                            case "NED 1 ArcSecond":
                              //  aRegion = new D4EM.Data.Region(aNorth, aSouth, aWest, aEast, D4EM.Data.Globals.AlbersProjection());
                                aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderUSGS_Seamless, aProjectFolderUSGS_Seamless, aRegion, true, true);
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneArcSecond;
                                aSaveFolder = layer + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    string[] tifNed1arcsec = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tifNed1arcsec.Length > 0)
                                    {
                                        file.WriteLine(tifNed1arcsec[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }   
                                 * */
                                break;
                            case "NED 1/3 ArcSecond":
                                layerspecification = D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneThirdArcSecond;
                                aSaveFolder = @"NED Third ArcSecond" + "_N" + aNorth.ToString() + ";S" + aSouth.ToString() + ";E" + aEast.ToString() + ";W" + aWest.ToString();
                                aSubFolder = System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSaveFolder);
                                D4EM.Data.Source.USGS_Seamless.Execute(aProject, aSaveFolder, layerspecification);
                                writeLogFile(aSubFolder, logfilename, logfileExists, layer);
                                fileLocationsText = fileLocationsText + addTifFileLocations(aSubFolder, layer);
                                if (Directory.Exists(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder)))
                                {
                                    string[] tifthirdarcsec = Directory.GetFiles(System.IO.Path.Combine(aProjectFolderUSGS_Seamless, aSubFolder), "*.tif", SearchOption.AllDirectories);
                                    if (tifthirdarcsec.Length > 0)
                                    {
                                        file.WriteLine(tifthirdarcsec[0]);
                                        dataDownloaded = true;
                                    }
                                }
                                /*
                                if (File.Exists(xmlfilename) == true)
                                {
                                    ReadXMLandWriteLog readWrite2006Impervious = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, layer);
                                }   
                                 * */
                                break;
                        }
                        logfileExists = true;
                    }

                }
                file.Close();
                int fileCount = countFiles;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NLCD File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUSGS_SeamlessLoadDataToMap.Visible = true;
                    labelUSGS_Seamless.Visible = true;
                    labelUSGS_Seamless.Text = "Downloaded data is located in " + aProjectFolderUSGS_Seamless;
                    if ((boxLayer.CheckedIndices.Contains(1) || boxLayer.CheckedIndices.Contains(2) || boxLayer.CheckedIndices.Contains(5)))
                    {
                        checkBoxTabulate.Visible = true;
                    }
                }
            }
            catch (ApplicationException ex)
            {
                file.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
                      
           
        }

        private string addTifFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] tiffiles = Directory.GetFiles(aSubFolder, "*.tif", SearchOption.AllDirectories);
                foreach (string file in tiffiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    //if (timeSpan.Minutes < 2)
                    //  {
                    text = dataType + " tif file: " + file + Environment.NewLine;
                    countFiles++;
                    //  }
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
                    DateTime creationTime = File.GetLastWriteTime(xmlfilename);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if ((File.Exists(xmlfilename) == true) && (timeSpan.Minutes < 2))
                    {
                        ReadXMLandWriteLog readWriteHydrologicUnitsSubBasinPolygons = new ReadXMLandWriteLog(xmlfilename, logfilename, logfileExists, dataType);
                    }
                }
            }
        }

        private void btnUSGS_Seamlesshuc8Download_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseProjectUSGS_Seamless_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderUSGS_Seamless.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderUSGS_Seamless = this.txtProjectFolderUSGS_Seamless.Text;
            }
        }

        private void btnBrowseCacheUSGS_Seamless_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolderUSGS_Seamless.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderUSGS_Seamless = this.txtCacheFolderUSGS_Seamless.Text;
            }
        }

        private void btnUSGS_SeamlessLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
            if (checkBoxTabulate.Checked == true)
            {
                File.Create(@"C:\Temp\tabulate.txt");
            }
        }

        private void USGS_SeamlessBox_Load(object sender, EventArgs e)
        {
            txtNorthUSGS_Seamless.Text = _north.ToString("#.##");
            txtSouthUSGS_Seamless.Text = _south.ToString("#.##");
            txtEastUSGS_Seamless.Text = _east.ToString("#.##");
            txtWestUSGS_Seamless.Text = _west.ToString("#.##");

            listUSGS_Seamlesshuc8.Items.Clear();
            foreach (string huc8 in hucnums)
            {
                listUSGS_Seamlesshuc8.Items.Add(huc8);
            }

            txtCacheFolderUSGS_Seamless.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            txtProjectFolderUSGS_Seamless.Text = "C:\\Temp\\ProjectFolderUSGS_Seamless";
            File.Delete(@"C:\Temp\DownloadedFilePathUSGS_Seamless");
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
    }
}
