using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel.Composition;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using SDMPBPlugin;

using System.Reflection;
using System.ComponentModel;

namespace SDMProjectBuilder
{
    public partial class MainForm : Form, MapWinUtility.IProgressStatus
    {
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        private string SDMPBMenuKey = "kSDMPB_Menu_Key";
        private string _cachePath = "";
        private string _SDMProjectBuilder = "SDMProjectBuilder";

        SimpleActionItem saiNewProj = null;
        SimpleActionItem saiNavHelper = null;
        SimpleActionItem saiImportLocationData = null;
        SimpleActionItem saiRunProjBldr = null;

        SDMProjectBuilderPlugin _sdmPlugin = null;

        private StatusPanel xPanel;
        private StatusPanel yPanel;

        private StatusPanel selectedPanel;

        public MainForm()
        {
            InitializeComponent();

            if (DesignMode) return;
            Shell = this;
            appManager.LoadExtensions();

            AddMenuItems();

            //Use the BASINS status dialog for progress updates
            MapWinUtility.Logger.ProgressStatus = this;

            Map map = appManager.Map as Map;

            xPanel = new StatusPanel { Width = 160 };
            yPanel = new StatusPanel { Width = 160 };
            appManager.ProgressHandler.Add(xPanel);
            appManager.ProgressHandler.Add(yPanel);

            selectedPanel = new StatusPanel { Width = 180 };
            appManager.ProgressHandler.Add(selectedPanel);

            //map.GeoMouseMove += Map_GeoMouseMove;
            //appManager.Map.MapFrame.LayerSelected += MapFrame_LayerSelected;
        }

        private void Map_GeoMouseMove(object sender, GeoMouseArgs e)
        {
            //appManager.ProgressHandler.Progress(String.Empty, 0, String.Format("X: {0}, Y: {1}", e.GeographicLocation.X, e.GeographicLocation.Y));
            xPanel.Caption = String.Format("X: {0:.#####}", e.GeographicLocation.X);
            yPanel.Caption = String.Format("Y: {0:.#####}", e.GeographicLocation.Y);
        }

        private void UpdateStatus()
        {
            if (appManager.Map.Layers.SelectedLayer == null)
            {
                selectedPanel.Caption = "No selected layer";
            }
            else
            {
                var layer = appManager.Map.Layers.SelectedLayer as IMapFeatureLayer;
                if (layer != null)
                {
                    selectedPanel.Caption = String.Format("{0}: {1} feature{2} selected", layer.LegendText, layer.Selection.Count, layer.Selection.Count == 1 ? null : "s");
                }
            }
        }

        void MapFrame_LayerSelected(object sender, LayerSelectedEventArgs e)
        {
            UpdateStatus();
        }

        void Map_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void AddMenuItems()
        {
            appManager.HeaderControl.Add(new RootItem(SDMPBMenuKey, "SDMProjectBuilder") { SortOrder = -15 });

            saiNewProj = new SimpleActionItem(SDMPBMenuKey, "New SDM Project", NewProject_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Create a new SDM project" };
            appManager.HeaderControl.Add(saiNewProj);


            saiNavHelper = new SimpleActionItem(SDMPBMenuKey, "Nav Helper", NavHelper_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Navigation Helper" };
            saiNavHelper.Enabled = false;
            appManager.HeaderControl.Add(saiNavHelper);

            saiImportLocationData = new SimpleActionItem(SDMPBMenuKey, "Import Local Data Files", ImportFiles_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Import Local Files" };
            saiImportLocationData.Enabled = false;
            appManager.HeaderControl.Add(saiImportLocationData);

            saiRunProjBldr = new SimpleActionItem(SDMPBMenuKey, "Run Project Builder", RunProjectBuilder_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Run Project Builder" };
            saiRunProjBldr.Enabled = false;
            appManager.HeaderControl.Add(saiRunProjBldr);

            SimpleActionItem saiOptions = new SimpleActionItem(SDMPBMenuKey, "Options", ShowOptions_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Options" };
            appManager.HeaderControl.Add(saiOptions);

            //SimpleActionItem saiEvts = new SimpleActionItem(SDMPBMenuKey, "Events", ShowEvents_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Options" };
            //appManager.HeaderControl.Add(saiEvts);
        }

        /// <summary>
        /// This will create a new DotSpatial project and copy and load the files for the National dataset.
        /// Files should be the county, state, huc8 and wsa ecoregions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Click(object sender, System.EventArgs e)
        {
            //if the map is empty or if the current project is already saved, start a new project directly
            if (!appManager.SerializationManager.IsDirty || appManager.Map.Layers == null || appManager.Map.Layers.Count == 0)
            {
                appManager.SerializationManager.New();
            }
            else if (string.IsNullOrEmpty(appManager.SerializationManager.CurrentProjectFile))
            {
                //if the current project is not specified - just ask to discard changes
                if (MessageBox.Show("Clear all data and start new project?", "Discard changes?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    appManager.SerializationManager.New();
                else
                    return;
            }
            else
            {
                //the current project is specified - ask the users if they want to save changes to current project
                string saveProjectMessage = string.Format("Save changes to the current project?", System.IO.Path.GetFileName(appManager.SerializationManager.CurrentProjectFile));
                DialogResult msgBoxResult = MessageBox.Show(saveProjectMessage, "Discard changes?", MessageBoxButtons.YesNoCancel);

                if (msgBoxResult == DialogResult.Cancel)
                    return;

                if (msgBoxResult == DialogResult.Yes)
                {
                    appManager.SerializationManager.SaveProject(appManager.SerializationManager.CurrentProjectFile);
                }

                appManager.SerializationManager.New();
            }

            if (appManager.SerializationManager != null)
            {
                appManager.SerializationManager.Deserializing += SerializationManager_Deserializing;
            }

            string localDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string localSDMDataPath = System.IO.Path.Combine(localDataPath, _SDMProjectBuilder);
            if (!System.IO.Directory.Exists(localSDMDataPath))
                System.IO.Directory.CreateDirectory(localSDMDataPath);

            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.RestoreDirectory = true;
            //sfd.InitialDirectory = System.IO.Path.Combine(localDataPath, _SDMProjectBuilder);
            sfd.Title = "Specify the name and location of the new project file";
            sfd.Filter = "Project files (*.dspx)|*.dspx";
            sfd.CheckPathExists = false;
            bool foundGoodPath = false;
            string destFilePath = null;
            do
            {
                if (sfd.ShowDialog() == DialogResult.Cancel) return;
                if (string.IsNullOrWhiteSpace(sfd.FileName)) return;

                destFilePath = System.IO.Path.GetDirectoryName(sfd.FileName);
                if (System.IO.Directory.Exists(destFilePath))
                    if (System.IO.Directory.GetFileSystemEntries(destFilePath).Length == 0)
                        //If it exists and is empty, it is good
                        foundGoodPath = true;
                    else
                    {  //It exists and is not empty, ask about making a subdirectory
                        try
                        {
                            String proposedPath = atcUtility.modFile.GetNewFileName(System.IO.Path.Combine(destFilePath, System.IO.Path.GetFileNameWithoutExtension(sfd.FileName)), null);
                            String proposedFileName = System.IO.Path.Combine(proposedPath, System.IO.Path.GetFileName(sfd.FileName));
                            String answer = MapWinUtility.Logger.MsgCustomCheckbox("Folder is not empty, create new folder and save project as \n'" + proposedFileName + "' ?",
                                "Create Project", "Always create new folder", "SDMPB", "Project", "AlwaysCreateFolder", "Yes", "Cancel");
                            if (answer == "Cancel") return;
                            sfd.FileName = proposedFileName;
                            destFilePath = System.IO.Path.GetDirectoryName(proposedFileName);
                        }
                        catch
                        { }
                    }

                if (!System.IO.Directory.Exists(destFilePath))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(destFilePath);
                        foundGoodPath = true;
                    }
                    catch
                    { }
                }
            } while (!foundGoodPath);
            appManager.Map.Projection = DotSpatial.Projections.KnownCoordinateSystems.Projected.World.WebMercator;

            //Getting setup to copy files to the newly created project folder
            //First copy the base GIS files for states, counties and HUC8s
            //Next copy MSM files if they exist
            string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exePath = System.IO.Directory.GetParent(exePath).FullName;

            string searchPath = exePath;
            string srcNationalDataPath = "";
            srcNationalDataPath = System.IO.Path.Combine(searchPath, "Data", "NationalData");

            string srcMetData = "";
            srcMetData = Path.Combine(searchPath, "Data", "met");

            if (!System.IO.Directory.Exists(srcNationalDataPath))
            {
                int lFilterIndex = -1;
                srcNationalDataPath = atcUtility.modFile.FindFile("Please locate national layer national_huc250d3.shp", "national_huc250d3.shp", "", "", false, false, ref lFilterIndex);
                if (System.IO.File.Exists(srcNationalDataPath))
                {
                    srcNationalDataPath = System.IO.Path.GetDirectoryName(srcNationalDataPath);
                }
            }            

            //Copy the met.shp (and support files) into the {project}\met folder.  Having problems adding shapes to shapefiles created on the fly.
            if (Directory.Exists(srcMetData))
            {
                DirectoryInfo dir = new DirectoryInfo(srcMetData);
                string destDirName = Path.Combine(destFilePath, "met");
                if (!Directory.Exists(destDirName))
                    Directory.CreateDirectory(destDirName);
                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }
            }


            string srcPESTFiles = "";
            srcPESTFiles = Path.Combine(searchPath, "Data", "HSPF-PEST");
            if (System.IO.Directory.Exists(srcPESTFiles))
            {
                string[] files = System.IO.Directory.GetFiles(srcPESTFiles);
                List<string> lstFiles = new List<string>();
                string destDirName = Path.Combine(destFilePath, "HSPF-PEST");
                if (!Directory.Exists(destDirName))
                    Directory.CreateDirectory(destDirName);

                foreach (string file in files)
                {
                    string fileNoPath = System.IO.Path.GetFileName(file);
                    string srcFilePath = System.IO.Path.Combine(srcPESTFiles, fileNoPath);
                    string destFile = System.IO.Path.Combine(destDirName, fileNoPath);
                    System.IO.File.Copy(srcFilePath, destFile, true);

                    if (string.Compare(fileNoPath, "Input_flow.in", true) == 0)
                    {
                        StreamReader sr = new StreamReader(destFile);
                        string fileContents = sr.ReadToEnd();
                        sr.Close();
                        sr.Dispose();
                        sr = null;
                        string[] lines = fileContents.Split('\n');
                        lines[1] = destDirName;

                        StreamWriter sw = new StreamWriter(destFile);
                        foreach (string line in lines)
                            sw.WriteLine(line);
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                        sw = null;
                    }
                }
            }




            if (System.IO.Directory.Exists(srcNationalDataPath))
            {
                appManager.Map.Layers.SuspendEvents();               
                                
                string[] files = System.IO.Directory.GetFiles(srcNationalDataPath);
                List<string> lstShapeFiles = new List<string>();
                foreach (string file in files)
                {
                    string fileNoPath = System.IO.Path.GetFileName(file);
                    string srcFilePath = System.IO.Path.Combine(srcNationalDataPath, fileNoPath);
                    string destFile = System.IO.Path.Combine(destFilePath, fileNoPath);
                    System.IO.File.Copy(srcFilePath, destFile, true);

                    if (destFile.EndsWith(".shp"))
                        lstShapeFiles.Add(destFilePath);
                }

                //string projPath = System.IO.Path.Combine(copyNationalTo, "ProjectBuilder.dspx");
                string projPath = System.IO.Path.Combine(destFilePath, "ProjectBuilder.dspx");
                if (System.IO.File.Exists(projPath))
                {
                    appManager.SerializationManager.OpenProject(projPath);
                    ProjectionInfo projInfo = appManager.Map.Projection;
                }
                else
                {
                    foreach (string file in lstShapeFiles)
                        appManager.Map.AddLayer(file);
                }
                appManager.SerializationManager.SaveProject(sfd.FileName);
                //Delete the original ProjectBuilder.dspx file
                System.IO.File.Delete(projPath);
                appManager.Map.Layers.ResumeEvents();
            } // Finished copying and loading the shapefile layers

            //Copy over the MSM folder            
            searchPath = exePath;
            srcNationalDataPath = "";
            srcNationalDataPath = System.IO.Path.Combine(searchPath, "Data", "LocalData");
            
            if (Directory.Exists(srcNationalDataPath))
            {
                appManager.ProgressHandler.Progress((int)0, "Copying Local data template files.");
                string destLocalDataPath = Path.Combine(destFilePath, "LocalData");
                if (!Directory.Exists(destLocalDataPath))
                    Directory.CreateDirectory(destLocalDataPath);

                DirectoryCopy(srcNationalDataPath, destLocalDataPath, true);
                appManager.ProgressHandler.Progress((int)0, "Ready");
            }


            _sdmPlugin = new SDMProjectBuilderPlugin();
            _sdmPlugin.Activate(appManager, _cachePath);

            //Enable the menu options that require a project.
            saiNavHelper.Enabled = true;
            saiImportLocationData.Enabled = true;
            saiRunProjBldr.Enabled = true;
        }

        private void NavHelper_Click(object sender, System.EventArgs e)
        {
            SDMPBPlugin.frmNavHelper navHelper = new frmNavHelper(appManager);
            navHelper.Show();
        }

        private void RunProjectBuilder_Click(object sender, System.EventArgs e)
        {
            //SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin sdmPlugin = new SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin();
            //sdmPlugin.Activate(appManager, _cachePath);
            if (_sdmPlugin == null)
            {
                MessageBox.Show("Please create a new SDM project.");
                return;
            }
            _sdmPlugin.ShowSpecifyForm();
        }

        private void ShowOptions_Click(object sender, System.EventArgs e)
        {
            frmOptions fOptions = new frmOptions();
            fOptions.CachePath = _cachePath;
            if (fOptions.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.CachePath = fOptions.CachePath;
                _cachePath = fOptions.CachePath;
                Properties.Settings.Default.Save();
            }
        }

        private void ImportFiles_Click(object sender, System.EventArgs e)
        {
            frmEditLocalData fEditLocalData = new frmEditLocalData(appManager);
            fEditLocalData.Show();
        }

        void MapWinUtility.IProgressStatus.Progress(int aCurrentPosition, int aLastPosition)
        {
            int percent = 100;
            if (aLastPosition > aCurrentPosition)
                if (aLastPosition > 100000) //multiplying large numbers by 100 could overflow, safer to divide last position by 100 when it is enough larger than 100
                    percent = aCurrentPosition / (aLastPosition / 100);
                else
                    percent = aCurrentPosition * 100 / aLastPosition;
            appManager.ProgressHandler.Progress((int)0, percent + "%");
            Application.DoEvents();
        }

        void MapWinUtility.IProgressStatus.Status(string aStatusMessage)
        {
            if (!aStatusMessage.StartsWith("PROGRESS"))
                appManager.ProgressHandler.Progress((int)0, aStatusMessage);
            Application.DoEvents();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _cachePath = FindCachePath();
        }

        private string FindCachePath()
        {
            string retCachePath = "";
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string cachePath = Properties.Settings.Default.CachePath;
            if (!string.IsNullOrWhiteSpace(cachePath))
                retCachePath = cachePath;
            else
                retCachePath = System.IO.Path.Combine(localAppDataPath, _SDMProjectBuilder);

            return retCachePath;

        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        

        void SerializationManager_Deserializing(object sender, SerializingEventArgs e)
        {
            IMap map = appManager.Map;
            if (map != null && map.Layers != null)
            {
                map.SelectionChanged -= Map_SelectionChanged;
                map.Layers.LayerSelected -= MapFrame_LayerSelected;
                map.MapFrame.LayerSelected -= MapFrame_LayerSelected;
                //map.MapFrame.LayerRemoved -= MapFrameOnLayerRemoved;

                map.SelectionChanged += Map_SelectionChanged;
                map.Layers.LayerSelected += MapFrame_LayerSelected;
                map.MapFrame.LayerSelected += MapFrame_LayerSelected;
                //map.MapFrame.LayerRemoved += MapFrameOnLayerRemoved;
            }
        }

        
    }
}
