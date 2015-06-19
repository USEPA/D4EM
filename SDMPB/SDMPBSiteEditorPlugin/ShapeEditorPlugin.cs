// ********************************************************************************************************
// Product Name: DotSpatial.Plugins.ShapeEditor.dll
// Description:  The data access libraries for the DotSpatial project.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Original Code is from MapWindow.dll version 6.0
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 3/26/2009 6:44:10 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//
// ********************************************************************************************************

using System;
using System.IO;
using System.Windows.Forms;

using DotSpatial.Data;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using DotSpatial.Topology;

namespace SDMPBSiteEditorPlugin
{
    /// <summary>
    /// Initializes a new instance of the ShapeEditorPlugin class.
    /// </summary>
    public class ShapeEditorPlugin : Extension
    {
        private string keyPlugin = "SDMPBPointShapeEditor";
        private ButtonHandler _myHandler;
        private AppManager _appMgr;

        /// <inheritdoc/>
        public override void Activate()
        {
            _appMgr = this.App;
            AddMenuItems();
            //_myHandler = new ButtonHandler(this.App) { Map = App.Map };
            base.Activate();
        }

        /// <inheritdoc/>
        public override void Deactivate()
        {
            if (App.HeaderControl != null) { App.HeaderControl.RemoveAll(); }

            if (_myHandler != null) { _myHandler.Dispose(); }
            base.Deactivate();
        }

        private void AddMenuItems()
        {
            _appMgr.HeaderControl.Add(new RootItem(keyPlugin, "Point CSV File") { SortOrder = 20 });

            _appMgr.HeaderControl.Add(new SimpleActionItem(keyPlugin, "Import CSV File", ImportCSV_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Import CSV File" });
            _appMgr.HeaderControl.Add(new SimpleActionItem(keyPlugin, "Import CSV File", ImportCSV_Click) { GroupCaption = HeaderControl.ApplicationMenuKey, SortOrder = 5, SmallImage = null, LargeImage = null, ToolTipText = "Create a new SDM project" });

            
        }

        private void ImportCSV_Click(object sender, System.EventArgs e)
        {
            ImportCSV icsv = new ImportCSV(_appMgr.Map);
            icsv.Show();
        }
    }
}