// -----------------------------------------------------------------------
// <copyright file="SimpleHeaderControl.cs" company="DotSpatial Team">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DemoMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DotSpatial.Controls.Header;
    using System.ComponentModel.Composition;
    using System.Windows.Forms;

    /// <summary>
    /// Creates a ToolStripContainer that hosts a MenuBarHeaderControl.
    /// </summary>
    [Export(typeof(IHeaderControl))]
    public class SimpleHeaderControl : DotSpatial.Controls.Header.MenuBarHeaderControl, IPartImportsSatisfiedNotification
    {
        [Import("Shell", typeof(ContainerControl))]
        private ContainerControl Shell { get; set; }

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;

        /// <summary>
        /// Initializes a new instance of the SimpleHeaderControl class.
        /// </summary>
        public SimpleHeaderControl()
        {
            
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();

            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(857, 459);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(857, 484);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";

            // place all of the controls that were on the form originally inside of our content panel.
            while (Shell.Controls.Count > 0)
            {
                foreach (Control control in Shell.Controls)
                {
                    this.toolStripContainer1.ContentPanel.Controls.Add(control);
                }
            }
            
            Shell.Controls.Add(this.toolStripContainer1);

            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();

            Initialize(toolStripContainer1);
        }

    }
}
