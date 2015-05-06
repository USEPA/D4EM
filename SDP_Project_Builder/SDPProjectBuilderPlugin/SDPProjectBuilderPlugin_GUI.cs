using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using atcUtility;
using MapWinUtility;
using SDP_Project_Builder_Batch;
using DotSpatial.Controls;
using DotSpatial.Data;


namespace SDPProjectBuilderPlugin
{
    static class SDPProjectBuilderPlugin_GUI
    {

        private static AppManager g_AppManager;
        private static Map g_Map;
        internal static frmSDPProjectBuilderProject pfrmHE2RMESProject;       

        internal static String g_AppNameRegistry  = "FramesHE2RMES"; //For preferences in registry
        internal static String g_AppNameShort  = "FramesHE2RMES";
        internal static String g_AppNameLong  = "Frames HE2RMES";   

        internal const String PARAMETER_FILE = "HE2RMESParameters.txt";


        public static AppManager AppManager
        {
            get { return g_AppManager; }
            set { g_AppManager = value; }
        }

        public static Map Map
        {
            get { return g_Map; }
            set { g_Map = value; }
        }

        internal static Form IsFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }

            return null;
        }

        internal static void SaveProject(frmSDPProjectBuilderProject frm)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            SDPParameters parameters = frm.GetParameters();

            //save source shape file?
            IFeatureSet fsSource = null;
            fsSource = frm.GetFeatureSetSource();
            if (fsSource != null)
            {
                string sSourceFilename = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\"
                        + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                sSourceFilename = sSourceFilename + "_Source.shp";
                parameters.SourceFileName = sSourceFilename;
                fsSource.SaveAs(sSourceFilename, true);
            }           

            //save AOI shape file?
            IFeatureSet fsAOI = null;
            fsAOI = frm.GetFeatureSetAOI();
            if (fsAOI != null)
            {
                string sAOIFilename = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\"
                        + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                sAOIFilename = sAOIFilename + "_AOI.shp";
                parameters.AOIFileName = sAOIFilename;
                fsAOI.SaveAs(sAOIFilename, true);
            }

            //save params text file
            parameters.WriteParametersTextFile(saveFileDialog1.FileName);

            //set file in project form
            frm.ProjectFile = saveFileDialog1.FileName;

            MessageBox.Show("Project Saved!");
        }

        internal static void OpenProject()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else 
            {
                SDPParameters parameters = new SDP_Project_Builder_Batch.SDPParameters();        
                parameters.ReadParametersTextFile(openFileDialog1.FileName);
                frmSDPProjectBuilderProject frmProject = new frmSDPProjectBuilderProject();
                frmProject.Show();
                frmProject.LoadParameters(parameters);
                //set file in project form
                frmProject.ProjectFile = openFileDialog1.FileName;
            
            }
        }

        internal static void SaveBatchProject(frmSDPProjectBuilderBatch frm)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            SDPBatchParameters parameters = frm.GetBatchParameters();          

            ////save params text file
            parameters.WriteParametersTextFile(saveFileDialog1.FileName);

            //set file in batch form
            frm.BatchProjectFile = saveFileDialog1.FileName;

            MessageBox.Show("Batch Project Saved!");
        }

        internal static void OpenBatchProject()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                SDPBatchParameters parameters = new SDP_Project_Builder_Batch.SDPBatchParameters();
                parameters.ReadParametersTextFile(openFileDialog1.FileName);
                frmSDPProjectBuilderBatch frmBatch = new frmSDPProjectBuilderBatch();
                frmBatch.Show();
                frmBatch.LoadBatchParameters(parameters);
                frmBatch.BatchProjectFile = openFileDialog1.FileName;
            }
        }

        public static void BrowseToFolder(TextBox txtBox)
        {
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();

            if (openFolderDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                txtBox.Text = openFolderDialog1.SelectedPath;
            }

        }

        public static void BrowseToFile(TextBox txtBox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                txtBox.Text = openFileDialog1.FileName;
            }

        }





    }
}
