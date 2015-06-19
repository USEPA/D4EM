// -----------------------------------------------------------------------
// <copyright file="SimpleDocking.cs" company="DotSpatial Team">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System.Windows.Forms;

namespace DemoMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DotSpatial.Controls.Docking;

    /// <summary>
    ///
    /// </summary>
    public class SimpleDocking : IDockManager
    {
        private List<Form> forms = new List<Form>();

        /// <summary>
        /// Adds the specified panel.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="caption">The caption of the panel and any tab button.</param>
        /// <param name="panel">The panel.</param>
        /// <param name="dockStyle">The dock location.</param>
        public void Add(string key, string caption, Control panel, DockStyle dockStyle)
        {
            panel.Dock = DockStyle.Fill;

            var form = new Form();
            form.Controls.Add(panel);
            form.Name = key;
            form.Width = panel.Width;
            form.Height = panel.Height;
            form.Show();
            form.Activated += new EventHandler(form_Activated);
            forms.Add(form);
        }

        void form_Activated(object sender, EventArgs e)
        {
            OnActivePanelChanged(new DockablePanelEventArgs((sender as Form).Name));
        }

        /// <summary>
        /// Removes the specified panel.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            foreach (Form form in forms)
            {
                if (form.Name == key)
                {
                    form.Close();
                    forms.Remove(form);
                    break;
                }
            }
        }

        /// <summary>
        /// Occurs when the active panel is changed.
        /// </summary>
        public event EventHandler<DockablePanelEventArgs> ActivePanelChanged;

        #region OnActivePanelChanged
        /// <summary>
        /// Triggers the ActivePanelChanged event.
        /// </summary>
        public virtual void OnActivePanelChanged(DockablePanelEventArgs ea)
        {
            if (ActivePanelChanged != null)
                ActivePanelChanged(null/*this*/, ea);
        }
        #endregion


        /// <summary>
        /// Selects the panel.
        /// </summary>
        /// <param name="key">The key.</param>
        public void SelectPanel(string key)
        {

        }

        /// <summary>
        /// Adds the specified panel.
        /// </summary>
        /// <param name="panel"></param>
        public void Add(DockablePanel panel)
        {
            Add(panel.Key, panel.Caption, panel.InnerControl, panel.Dock);
        }


        /// <summary>
        /// Resets the layout of the dock panels to a developer specified location.
        /// </summary>
        public void ResetLayout()
        {

        }

        /// <summary>
        /// PanelClosed
        /// </summary>
        public event EventHandler<DockablePanelEventArgs> PanelClosed;
        #region OnPanelClosed
        /// <summary>
        /// Triggers the PanelClosed event.
        /// </summary>
        public virtual void OnPanelClosed(DockablePanelEventArgs ea)
        {
            if (PanelClosed != null)
                PanelClosed(null/*this*/, ea);
        }
        #endregion

        /// <summary>
        /// PanelAdded
        /// </summary>
        public event EventHandler<DockablePanelEventArgs> PanelAdded;

        #region OnPanelAdded
        /// <summary>
        /// Triggers the PanelAdded event.
        /// </summary>
        public virtual void OnPanelAdded(DockablePanelEventArgs ea)
        {
            if (PanelAdded != null)
                PanelAdded(null/*this*/, ea);
        }
        #endregion

        /// <summary>
        /// PanelRemoved
        /// </summary>
        public event EventHandler<DockablePanelEventArgs> PanelRemoved;
        #region OnPanelRemoved
        /// <summary>
        /// Triggers the PanelRemoved event.
        /// </summary>
        public virtual void OnPanelRemoved(DockablePanelEventArgs ea)
        {
            if (PanelRemoved != null)
                PanelRemoved(null/*this*/, ea);
        }
        #endregion

        /// <summary>
        /// Empty implementation of HidePanel
        /// </summary>
        /// <param name="key"></param>
        public void HidePanel(string key)
        {

        }
    }
}