using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.ComponentModel;

namespace DemoMap
{
    /// <summary>
    /// Status Control
    /// </summary>
    public class SimpleStatusControl : IStatusControl, IPartImportsSatisfiedNotification
    {
        [Import("Shell", typeof(ContainerControl))]
        private ContainerControl Shell { get; set; }

        private System.Windows.Forms.StatusStrip statusStrip;
        private StatusPanel defaultStatusPanel;

        /// <summary>
        /// Initializes a new instance of the SimpleStatusControl class.
        /// </summary>
        public SimpleStatusControl()
        {

        }

        /// <summary>
        /// Adds a status panel to the status strip
        /// </summary>
        /// <param name="panel">the user-specified status panel</param>
        public void Add(StatusPanel panel)
        {
            ToolStripStatusLabel myLabel = new ToolStripStatusLabel();
            myLabel.Name = panel.Key;
            myLabel.Text = panel.Caption;
            myLabel.Width = panel.Width;

            panel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                {
                    var item = sender as StatusPanel;

                    myLabel.Text = item.Caption;
                    myLabel.Width = item.Width;
                };

            statusStrip.Items.Add(myLabel);
        }

        /// <summary>
        /// Send a progress status message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="percent"></param>
        /// <param name="message"></param>
        public void Progress(string key, int percent, string message)
        {
            defaultStatusPanel.Caption = message;
        }

        private bool _cancel;
        /// <summary>True when Cancel has been requested</summary>
        public bool Cancel
        {
            set { _cancel = value; }
            get { return _cancel; }
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            statusStrip = new StatusStrip();

            statusStrip.Location = new System.Drawing.Point(0, 285);
            statusStrip.Name = "statusStrip1";
            statusStrip.Size = new System.Drawing.Size(508, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = String.Empty;

            //adding the status strip control
            Shell.Controls.Add(this.statusStrip);

            //adding one initial status panel to the status strip control
            defaultStatusPanel = new StatusPanel();
            this.Add(defaultStatusPanel);
        }

        /// <summary>
        /// Remove a panel
        /// </summary>
        /// <param name="panel"></param>
        public void Remove(StatusPanel panel)
        {
            statusStrip.Items.RemoveByKey(panel.Key);
        }
    }
}
