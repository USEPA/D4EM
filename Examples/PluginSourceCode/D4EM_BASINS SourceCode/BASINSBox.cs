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
using D4EMPlugins;

namespace D4EM_BASINS
{
    public partial class BASINSBox : Form
    {
        int countFiles = 0;
        string aProjectFolderBasins = "";
        string aCacheFolderBasins;
        List<string> hucnums = null;

        public BASINSBox(List<string> huc8nums)
        {           
            InitializeComponent();
            hucnums = huc8nums;
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

            if (txtProjectFolderBasins.Text == "")
            {
                MessageBox.Show("Please enter a Project Folder directory");
                return;
            }

           

            //Double aNorth = 0;
            //Double aSouth = 0;
            //Double aWest = 0;
            //Double aEast = 0;

            //String aHUC8 = txtHUC8Basins.Text.Trim();
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathBasins");

            try
            {            
            string aProjectFolderBasins = System.IO.Path.Combine(txtProjectFolderBasins.Text.Trim(), "Basins");
            aCacheFolderBasins = txtCacheFolder.Text.Trim();
            string fileLocationsText = "Downloaded BASINS files are located in " + aProjectFolderBasins + Environment.NewLine + Environment.NewLine;
            bool logfileExists = false;
            bool fileExists = false;
            
            foreach (string aHUC8 in listHuc8Basins.Items)
            {
                foreach (object datatype in boxBasinsDataType.CheckedItems)
                {
                    
                        string adatatype = datatype.ToString();
                        D4EM.Data.LayerSpecification dt;
                        DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                        D4EM.Data.Region aRegion = new D4EM.Data.Region(D4EM.Data.Region.RegionTypes.huc8, aHUC8);

                        D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aCacheFolderBasins, aProjectFolderBasins, aRegion, false, true);
                        string aSaveFolder;
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
                                EPAUtility.WriteFileWithShapeFilePaths wrcore31 = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                            case "census":
                                dt = D4EM.Data.Source.BASINS.LayerSpecifications.Census.all;
                                aSaveFolder = dt.Tag + "_" + aHUC8;
                                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                                aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                                writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                                EPAUtility.WriteFileWithShapeFilePaths wrcensus = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                            //case "d303":
                            //    dt = D4EM.Data.Source.BASINS.LayerSpecifications.d303;
                            //    aSaveFolder = dt.Tag + "_" + aHUC8;
                            //    D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                            //    aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            //    writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                            //    fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                            //    EPAUtility.WriteFileWithShapeFilePaths wrd303 = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                            //    string filePathD303 = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                            //    break;
                            case "dem":
                                dt = D4EM.Data.Source.BASINS.LayerSpecifications.dem;
                                aSaveFolder = dt.Tag + "_" + aHUC8;
                                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                                aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                                writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                                EPAUtility.WriteFileWithShapeFilePaths wrdem = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
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
                                EPAUtility.WriteFileWithShapeFilePaths wrgiras = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                            case "huc12":
                                dt = D4EM.Data.Source.BASINS.LayerSpecifications.huc12;
                                aSaveFolder = dt.Tag + "_" + aHUC8;
                                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                                aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                                writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                                EPAUtility.WriteFileWithShapeFilePaths wrhuc12 = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                            case "lstoret":
                                dt = D4EM.Data.Source.BASINS.LayerSpecifications.lstoret;
                                aSaveFolder = dt.Tag + "_" + aHUC8;
                                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                                aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                                writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                                EPAUtility.WriteFileWithShapeFilePaths wrlstoret = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
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
                                EPAUtility.WriteFileWithShapeFilePaths wrnhd = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                            case "pcs3":
                                dt = D4EM.Data.Source.BASINS.LayerSpecifications.pcs3;
                                aSaveFolder = dt.Tag + "_" + aHUC8;
                                D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, dt);
                                aSubFolder = System.IO.Path.Combine(aProjectFolderBasins, aSaveFolder);
                                writeLogFile(aSubFolder, logfilename, logfileExists, adatatype);
                                fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, adatatype, fileExists);
                                EPAUtility.WriteFileWithShapeFilePaths wrpcs3 = new EPAUtility.WriteFileWithShapeFilePaths(fileShpTif, aProjectFolderBasins, aSaveFolder);
                                break;
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                logfileExists = true;
                }
            
            fileShpTif.Close();
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
                btnBasinsLoadDataToMap.Visible = true;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                fileShpTif.Close();
            }
           
            this.Cursor = StoredCursor;
            
        }

        private void BASINSBox_Load(object sender, EventArgs e)
        {
            foreach (string huc8 in hucnums)
            {               
                listHuc8Basins.Items.Add(huc8);
            }

            txtCacheFolder.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            txtProjectFolderBasins.Text = @"C:\Temp\ProjectFolderBasins";
            File.Delete(@"C:\Temp\DownloadedFilePathBasins");
        }

        private void btnRemoveBasins_Click(object sender, EventArgs e)
        {
            while (listHuc8Basins.SelectedItems.Count > 0)
            {
                listHuc8Basins.Items.Remove(listHuc8Basins.SelectedItems[0]);
            }
        }

        private void btnHUCfind_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo); 
        }

        private void btnAddBasins_Click(object sender, EventArgs e)
        {
            if (txtHUC8Basins.Text.Length > 1)
            {
                listHuc8Basins.Items.Add(txtHUC8Basins.Text.Trim());
            }
            else
            {
                MessageBox.Show("Type in a HUC number");
            }
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

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolder.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderBasins = this.txtCacheFolder.Text;
            }
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
                      //  if (timeSpan.Minutes < 2)
                      //  {
                            text = text + dataType + " shapefile: " + file + Environment.NewLine;
                            countFiles++;
                      //  }
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

        private void btnBasinsLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
