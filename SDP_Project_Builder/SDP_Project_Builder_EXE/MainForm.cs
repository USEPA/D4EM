// ********************************************************************************************************
// Product Name: TestViewer.exe
// Description:  A very basic demonstration of the controls.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Initial Developer of this Original Code is Ted Dunsford. Created during refactoring 2010.
// ********************************************************************************************************
using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using System.ComponentModel.Composition;


namespace DemoMap
{
    /// <summary>
    /// A Form to test the map controls.
    /// </summary>
    public partial class MainForm : Form,  MapWinUtility.IProgressStatus
    {
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            map1.GeoMouseMove += Map_GeoMouseMove;

            Shell = this;
            appManager.LoadExtensions();
            appManager.CompositionContainer.ComposeParts(toolManager1);
            MapWinUtility.Logger.ProgressStatus = this;
        }

        private void Map_GeoMouseMove(object sender, GeoMouseArgs e)
        {
            appManager.ProgressHandler.Progress(String.Empty, 0, String.Format("X: {0}, Y: {1}", e.GeographicLocation.X, e.GeographicLocation.Y));
        }

        void MapWinUtility.IProgressStatus.Progress(int aCurrentPosition, int aLastPosition)
        {
            int percent = 100;
            if (aLastPosition > aCurrentPosition)
                if (aLastPosition > 100000) //multiplying large numbers by 100 could overflow, safer to divide last position by 100 when it is enough larger than 100
                    percent = aCurrentPosition / (aLastPosition / 100);
                else
                    percent = aCurrentPosition * 100 / aLastPosition;
            appManager.ProgressHandler.Progress(String.Empty, (int)0, percent + "%");
            Application.DoEvents();
        }

        void MapWinUtility.IProgressStatus.Status(string aStatusMessage)
        {
            if (!aStatusMessage.StartsWith("PROGRESS"))
                appManager.ProgressHandler.Progress(String.Empty, (int)0, aStatusMessage);
            Application.DoEvents();
        }


    }

}