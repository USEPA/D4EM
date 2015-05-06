using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Collections.Generic;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using SDMProjectBuilder;

namespace D4EMProjectBuilder
{
    public partial class MainForm : Form, MapWinUtility.IProgressStatus
    {
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        private string D4EMMenuKey = "kD4EM_Menu_Key";

        private string _cachePath = "";
        private string _D4EMProjectBuilder = "D4EMProjectBuilder";

        SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin _sdmPlugin = null;


        public MainForm()
        {
            InitializeComponent();

            if (DesignMode) return;
            Shell = this;

            appManager.Map = map1;
            appManager.Legend = legend1;

            map1.GeoMouseMove += Map_GeoMouseMove;

            appManager.LoadExtensions();

            MapWinUtility.Logger.ProgressStatus = this;
            appManager.HeaderControl.Add(new RootItem(D4EMMenuKey, "D4EM") { SortOrder = -15 });
            // Menu items
            appManager.HeaderControl.Add(new SimpleActionItem(D4EMMenuKey, "New D4EM Project", NewProject_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Create a new D4EM project" });

            appManager.HeaderControl.Add(new SimpleActionItem(D4EMMenuKey, "Nav Helper", NavHelper_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Navigation Helper" });

            appManager.HeaderControl.Add(new SimpleActionItem(D4EMMenuKey, "Run Project Builder", RunProjectBuilder_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Run Project Builder" });

            appManager.HeaderControl.Add(new SimpleActionItem(D4EMMenuKey, "Options", ShowOptions_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Options" });

            map1.Projection = D4EM.Data.Globals.WebMercatorProjection();
        }

        /// <summary>
        /// This will create a new DotSpatial project and copy and load the files for the National dataset.
        /// Files should be the county, state, huc8 and wsa ecoregions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Please select the location of the new project.";
            sfd.Filter = "Project files (*.dspx)|*.dspx";
            sfd.CheckPathExists = true;

            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            if (string.IsNullOrWhiteSpace(sfd.FileName)) return;


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
            
            string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exePath = System.IO.Directory.GetParent(exePath).FullName;

            string searchPath = exePath;
            string srcPath = "";
            srcPath = System.IO.Path.Combine(searchPath, "NationalData");
            //do
            //{
            //    srcPath = System.IO.Path.Combine(searchPath, "national");
                
            //    if (!System.IO.Directory.Exists(srcPath))
            //    {
            //        srcPath = System.IO.Path.Combine(searchPath, "data", "national");
            //        if (!System.IO.Directory.Exists(srcPath))
            //        {
            //            srcPath = System.IO.Path.Combine(searchPath, "Externals", "data", "national");
            //        }
            //    }
            //    searchPath = System.IO.Path.GetDirectoryName(searchPath);
            //} while (!System.IO.Directory.Exists(srcPath) && searchPath != null);

            if (!System.IO.Directory.Exists(srcPath))
            {
                int lFilterIndex = -1;
                srcPath = atcUtility.modFile.FindFile("Please locate national layer national_huc250d3.shp", "national_huc250d3.shp", "", "", false, false, ref lFilterIndex);
                if (System.IO.File.Exists(srcPath))
                {
                    srcPath = System.IO.Path.GetDirectoryName(srcPath);
                }
            }

            if (System.IO.Directory.Exists(srcPath))
            {
                appManager.Map.Layers.SuspendEvents();

                //string copyNationalTo = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sfd.FileName), "national");
                string destFilePath = System.IO.Path.GetDirectoryName(sfd.FileName);
                //System.IO.Directory.CreateDirectory(copyNationalTo);
                string[] files = System.IO.Directory.GetFiles(srcPath);
                List<string> lstShapeFiles = new List<string>();
                foreach (string file in files)
                {
                    string fileNoPath = System.IO.Path.GetFileName(file);
                    string srcFilePath = System.IO.Path.Combine(srcPath, fileNoPath);
                    string destFile = System.IO.Path.Combine(destFilePath, fileNoPath);
                    System.IO.File.Copy(srcFilePath, destFile, true);

                    if (destFilePath.EndsWith(".shp"))
                        lstShapeFiles.Add(destFilePath);
                }

                //string projPath = System.IO.Path.Combine(copyNationalTo, "ProjectBuilder.dspx");
                string projPath = System.IO.Path.Combine(destFilePath, "ProjectBuilder.dspx");
                if (System.IO.File.Exists(projPath))
                {
                    appManager.SerializationManager.OpenProject(projPath);
                    //System.IO.File.Delete(projPath);
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
            }

            _sdmPlugin = new SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin();
            _sdmPlugin.Activate(appManager, _cachePath);
        }

        private void NavHelper_Click(object sender, System.EventArgs e)
        {
            SDMProjectBuilder.frmNavHelper navHelper = new frmNavHelper(appManager);
            navHelper.Show();
        }

        private void RunProjectBuilder_Click(object sender, System.EventArgs e)
        {
            //SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin sdmPlugin = new SDMProjectBuilder.SDMProjectBuilderPlugin.SDMProjectBuilderPlugin();
            //sdmPlugin.Activate(appManager, _cachePath);
            if (_sdmPlugin == null)
            {
                MessageBox.Show("Please create a new D4EM project.");
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

        private void Map_GeoMouseMove(object sender, GeoMouseArgs e)
        {
            appManager.ProgressHandler.Progress(string.Empty, 0, string.Format("X: {0}, Y: {1}", e.GeographicLocation.X, e.GeographicLocation.Y));
        }

        void MapWinUtility.IProgressStatus.Progress(int aCurrentPosition, int aLastPosition)
        {
            int percent = 100;
            if (aLastPosition > aCurrentPosition)
                if (aLastPosition > 100000) //multiplying large numbers by 100 could overflow, safer to divide last position by 100 when it is enough larger than 100
                    percent = aCurrentPosition / (aLastPosition / 100);
                else
                    percent = aCurrentPosition * 100 / aLastPosition;
            appManager.ProgressHandler.Progress(string.Empty, (int)0, percent + "%");
            Application.DoEvents();
        }

        void MapWinUtility.IProgressStatus.Status(string aStatusMessage)
        {
            if (!aStatusMessage.StartsWith("PROGRESS"))
                appManager.ProgressHandler.Progress(string.Empty, (int)0, aStatusMessage);
            Application.DoEvents();
        }

        private void navHelperToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
           
        }

        private void newD4EMProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SDMProjectBuilder.frmSpecifyProject specProj = new frmSpecifyProject();
            specProj.Initialize();
            specProj.Show();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
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
                retCachePath = System.IO.Path.Combine(localAppDataPath, _D4EMProjectBuilder);

            return retCachePath;


        }

    }
}