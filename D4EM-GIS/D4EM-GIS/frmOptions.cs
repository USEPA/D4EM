using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D4EMProjectBuilder
{
    public partial class frmOptions : Form
    {
        public string CachePath {get; set;}
        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Specify the location of the download data cache.";
            fbd.ShowDialog();
            txtCacheFolder.Text = fbd.SelectedPath;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            CachePath = txtCacheFolder.Text;            
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            txtCacheFolder.Text = CachePath;
        }
    }
}
