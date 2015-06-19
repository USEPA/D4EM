using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Controls.Docking;
using DotSpatial.Symbology;
using DotSpatial.Projections;
using DotSpatial.Data;
using DotSpatial.Controls.Extensions;

namespace SDPProjectBuilderPlugin
{
    class SDPProjectBuilderPlugin : Extension
    {

        public override void Activate()
        {
            String ScriptEditorMenuKey = "kSDPProjectBuilderPlugin";
            String ScriptEditorMenuSiteSubKey = "kSDPProjectBuilderPluginSiteSub";
            String ScriptEditorMenuBatchSubKey = "kSDPProjectBuilderPluginBatchSub";

            SDPProjectBuilderPlugin_GUI.AppManager = App;
            SDPProjectBuilderPlugin_GUI.Map = (Map)App.Map;

            //root item
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(new RootItem(ScriptEditorMenuKey, "SDPProjectBuilder"));
            //sub items
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(new MenuContainerItem(ScriptEditorMenuKey, ScriptEditorMenuSiteSubKey, "Site"));
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(new MenuContainerItem(ScriptEditorMenuKey, ScriptEditorMenuBatchSubKey, "Batch"));
            //action items site
            SimpleActionItem item = new SimpleActionItem(ScriptEditorMenuKey, "New Project", new EventHandler(mnuFileNewProject_Click));
            item.MenuContainerKey = ScriptEditorMenuSiteSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

            item = new SimpleActionItem(ScriptEditorMenuKey, "Open Project", new EventHandler(mnuFileOpenProject_Click));
            item.MenuContainerKey = ScriptEditorMenuSiteSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

            item = new SimpleActionItem(ScriptEditorMenuKey, "Save Project", new EventHandler(mnuFileSaveProject_Click));
            item.MenuContainerKey = ScriptEditorMenuSiteSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

            //action items batch
            item = new SimpleActionItem(ScriptEditorMenuKey, "New Batch Project", new EventHandler(mnuFileNewBatchProject_Click));
            item.MenuContainerKey = ScriptEditorMenuBatchSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

            item = new SimpleActionItem(ScriptEditorMenuKey, "Open Batch Project", new EventHandler(mnuFileOpenBatchProject_Click));
            item.MenuContainerKey = ScriptEditorMenuBatchSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

            item = new SimpleActionItem(ScriptEditorMenuKey, "Save Batch Project", new EventHandler(mnuFileSaveBatchProject_Click));
            item.MenuContainerKey = ScriptEditorMenuBatchSubKey;
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.Add(item);

        }

        public override void Deactivate()
        {
            SDPProjectBuilderPlugin_GUI.AppManager.HeaderControl.RemoveAll();
        }

#region "Menus"

        private void mnuFileNewProject_Click(object sender, EventArgs e)
        {
            //if a project is open, prompt to continue or cancel, warn of loss of project changes
             frmSDPProjectBuilderProject frmProject = null;
             frmProject = (frmSDPProjectBuilderProject)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderProject));
             if (frmProject != null)
             {
                 if (MessageBox.Show("By starting a new project any changes to the current project will be lost.  Do you wish to continue?",
                                            "Confirm New Project", MessageBoxButtons.OKCancel) == DialogResult.OK)
                 {
                     frmProject.Close();
                     SDPProjectBuilderPlugin_GUI.Map.ClearLayers();
                     frmProject = new frmSDPProjectBuilderProject();
                     frmProject.Show();
                 }
             }
             else
             {
                 SDPProjectBuilderPlugin_GUI.Map.ClearLayers();
                 frmProject = new frmSDPProjectBuilderProject();
                 frmProject.Show();               
             }                
        }

        private void mnuFileOpenProject_Click(object sender, EventArgs e)
        {
            //if a project is open, prompt to continue or cancel, warn of loss of project changes
            frmSDPProjectBuilderProject frmProject = null;
            frmProject = (frmSDPProjectBuilderProject)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderProject));
            if (frmProject != null)
            {
                if (MessageBox.Show("By opening a project any changes to the current project will be lost.  Do you wish to continue?",
                                           "Confirm Open Project", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    frmProject.Close();
                    SDPProjectBuilderPlugin_GUI.Map.ClearLayers();
                    SDPProjectBuilderPlugin_GUI.OpenProject();
                }
            }
            else
            {
                SDPProjectBuilderPlugin_GUI.Map.ClearLayers();
                SDPProjectBuilderPlugin_GUI.OpenProject();
            }           
        }

         private void  mnuFileSaveProject_Click(object sender, EventArgs e)
         {

             frmSDPProjectBuilderProject frmProject = null;
             frmProject = (frmSDPProjectBuilderProject)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderProject));
             if (frmProject != null)
             {
                SDPProjectBuilderPlugin_GUI.SaveProject(frmProject);                
             }
         }

         private void mnuFileNewBatchProject_Click(object sender, EventArgs e)
         {
             //if a project is open, prompt to continue or cancel, warn of loss of project changes
             frmSDPProjectBuilderBatch frmBatch = null;
             frmBatch = (frmSDPProjectBuilderBatch)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderBatch));
             if (frmBatch != null)
             {
                 if (MessageBox.Show("By starting a new Batch Project any changes to the current Batch Project will be lost.  Do you wish to continue?",
                                            "Confirm New Batch Project", MessageBoxButtons.OKCancel) == DialogResult.OK)
                 {
                     frmBatch.Close();
                     frmBatch = new frmSDPProjectBuilderBatch();
                     frmBatch.Show();
                 }
             }
             else
             {
                 frmBatch = new frmSDPProjectBuilderBatch();
                 frmBatch.Show();
             }
         }

         private void mnuFileOpenBatchProject_Click(object sender, EventArgs e)
         {
             //if a Batch is open, prompt to continue or cancel, warn of loss of Batch changes
             frmSDPProjectBuilderBatch frmBatch = null;
             frmBatch = (frmSDPProjectBuilderBatch)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderBatch));
             if (frmBatch != null)
             {
                 if (MessageBox.Show("By opening a Batch Project any changes to the current Batch Project will be lost.  Do you wish to continue?",
                                            "Confirm Open Batch Project", MessageBoxButtons.OKCancel) == DialogResult.OK)
                 {
                     frmBatch.Close();
                     SDPProjectBuilderPlugin_GUI.OpenBatchProject();
                 }
             }
             else
             {
                 SDPProjectBuilderPlugin_GUI.OpenBatchProject();
             }
         }

         private void mnuFileSaveBatchProject_Click(object sender, EventArgs e)
         {

             frmSDPProjectBuilderBatch frmBatch = null;
             frmBatch = (frmSDPProjectBuilderBatch)SDPProjectBuilderPlugin_GUI.IsFormAlreadyOpen(typeof(frmSDPProjectBuilderBatch));
             if (frmBatch != null)
             {
                 SDPProjectBuilderPlugin_GUI.SaveBatchProject(frmBatch);
             }
         }

#endregion //Menus    



        



    }
}

