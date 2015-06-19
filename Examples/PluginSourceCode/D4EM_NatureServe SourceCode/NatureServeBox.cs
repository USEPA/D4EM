using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using Ionic;
using D4EM.Data;


namespace D4EM_NatureServe
{
    public partial class NatureServeBox : Form
    {
        int countFiles = 0;
        string aProjectFolderNatureServe;
        string aProjectFolderBirds;
        string aCacheFolder;

        List<string> _huc8nums;

        public NatureServeBox(List<string> huc8nums)
        {
            InitializeComponent();
            _huc8nums = huc8nums;
        }

        private void btnDownloadNatureServe_Click(object sender, EventArgs e)
        {

            if (listPollinator.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select atleast one pollinator");
                return;
            }

            if (txtProjectFolderNatureServe.Text == "")
            {
                MessageBox.Show("Please enter a Project Folder directory");
                return;
            }
            int fileCount = 0;
            TextWriter fileShp = new StreamWriter(@"C:\Temp\DownloadedFilePathNatureServe");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();
                aCacheFolder = txtCacheFolder.Text.Trim();
                string fileLocationsText = "Downloaded NatureServe files are located in " + aProjectFolderNatureServe + Environment.NewLine + Environment.NewLine;
                aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();

                D4EM.Data.LayerSpecification pollinator_layer = new LayerSpecification();
                bool filesExist = false;
                foreach (object pollinator in listPollinator.CheckedItems)
                {
                    string _pollinator = pollinator.ToString();
                    string aSubFolder = "";
                    switch (_pollinator)
                    {
                        case "Anna's Hummingbird (Calypte anna)":
                            pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Calypte_anna;
                            filesExist = D4EM.Data.Source.NatureServe.getPollinatorDataForMap(aProjectFolderNatureServe, aCacheFolder, pollinator_layer, fileShp);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                            fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                            break;
                        case "Eastern Tiger Swallowtail (Papilio glaucus)":
                            pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Papilio_glaucus;
                            filesExist = D4EM.Data.Source.NatureServe.getPollinatorDataForMap(aProjectFolderNatureServe, aCacheFolder, pollinator_layer, fileShp);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                            fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                            break;
                        case "Hermit Sphinx (Lintneria eremitus)":
                            pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Lintneria_eremitus;
                            filesExist = D4EM.Data.Source.NatureServe.getPollinatorDataForMap(aProjectFolderNatureServe, aCacheFolder, pollinator_layer, fileShp);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                            fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                            break;
                        case "Rusty-patched Bumble Bee (Bombus affinis)":
                            pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Bombus_affinis;
                            filesExist = D4EM.Data.Source.NatureServe.getPollinatorDataForMap(aProjectFolderNatureServe, aCacheFolder, pollinator_layer, fileShp);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                            fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                            break;
                        case "Southeastern Blueberry Bee (Habropoda laboriosa)":
                            pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Habropoda_laboriosa;
                            filesExist = D4EM.Data.Source.NatureServe.getPollinatorDataForMap(aProjectFolderNatureServe, aCacheFolder, pollinator_layer, fileShp);
                            aSubFolder = System.IO.Path.Combine(aProjectFolderNatureServe, pollinator_layer.Tag);
                            fileLocationsText = fileLocationsText + addHtmFileLocations(aSubFolder, _pollinator, filesExist);
                            fileLocationsText = fileLocationsText + addShpFileLocations(aSubFolder, _pollinator, filesExist);
                            break;
                    }
                }
                fileShp.Close();
                fileCount = countFiles;
                labelNatureServe.Visible = true;
                labelNatureServe.Text = "Downloaded data is located in " + aProjectFolderNatureServe;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "NatureServe File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNatureServeLoadDatatoMap.Visible = true;
                }
                countFiles = 0;
            }
            catch (Exception ex)
            {
                fileShp.Close();
                MessageBox.Show(ex.ToString());
            }

            this.Cursor = StoredCursor;
        }

        private void NatureServeBox_Load(object sender, EventArgs e)
        {
            
            txtProjectFolderNatureServe.Text = "C:\\Temp\\ProjectFolderNatureServe";
            txtCacheFolder.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            File.Delete(@"C:\Temp\DownloadedFilePathNatureServe");
            File.Delete(@"C:\Temp\DownloadedFilePathNatureServeBirds");

            txtProjectFolderBirds.Text = "C:\\Temp\\ProjectFolderBirds";
            if (_huc8nums.Count > 0)
            {
                txtHUC8natureServe.Text = _huc8nums[0];
            }
        }

        private void btnBrowseNatureServe_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderNatureServe.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderNatureServe = this.txtProjectFolderNatureServe.Text;
            }
        }

        private void btnDownloadBirds_Click(object sender, EventArgs e)
        {
            if (listBirds.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select atleast one bird family");
                return;
            }

            if (txtProjectFolderBirds.Text == "")
            {
                MessageBox.Show("Please enter a Project Folder directory");
                return;
            }

            TextWriter fileShp = new StreamWriter(@"C:\Temp\DownloadedFilePathNatureServeBirds");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            aProjectFolderBirds = txtProjectFolderBirds.Text.Trim();
            aCacheFolder = txtCacheFolder.Text.Trim();

            D4EM.Data.LayerSpecification bird_layer = new LayerSpecification();

            foreach (object bird in listBirds.CheckedItems)
            {
                string _bird = bird.ToString();
                switch (_bird)
                {
                    case "Accipitridae":
                        bird_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Birds.Accipitridae;
                        D4EM.Data.Source.NatureServe.getBirdDataForMap(aProjectFolderBirds, aCacheFolder, bird_layer, fileShp);
                        break;
                    
                }
            }
            fileShp.Close();
            this.Cursor = StoredCursor;
            labelBirds.Visible = true;
            labelBirds.Text = "Downloaded data is located in " + aProjectFolderBirds;
        }

        private void btnPopulateNativeSpeciesTable_Click(object sender, EventArgs e)
        {
            if (txtHUC8natureServe.Text == "")
            {
                MessageBox.Show("Please enter a HUC-8");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            aProjectFolderNatureServe = txtProjectFolderNatureServe.Text.Trim();
            string aHuc = txtHUC8natureServe.Text.Trim();
            Directory.CreateDirectory(aProjectFolderNatureServe);
            D4EM.Data.Source.NativeSpecies ns = new D4EM.Data.Source.NativeSpecies(aProjectFolderNatureServe, aHuc);
            string tableFile = ns.csvFile;

            if (System.IO.File.Exists(tableFile))
            {
                System.IO.StreamReader fileReader = new StreamReader(tableFile);

                //Checking the end of file's content
                if (fileReader.Peek() != -1)
                {
                    string fileRow = fileReader.ReadLine();
                    string[] fileDataField = fileRow.Split(',');
                    int counts = fileDataField.Count();
                    //Adding Column Header to DataGridView
                    for (int i = 0; i < counts; i++)
                    {
                        DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
                        columnDataGridTextBox.Name = fileDataField[i];
                        columnDataGridTextBox.HeaderText = fileDataField[i];
                        columnDataGridTextBox.Width = 120;
                        dataGridViewNatureServe.Columns.Add(columnDataGridTextBox);
                    }

                    //Adding Data to DataGridView
                    while (fileReader.Peek() != -1)
                    {
                        fileRow = fileReader.ReadLine();
                        fileDataField = fileRow.Split(',');
                        dataGridViewNatureServe.Rows.Add(fileDataField);
                    }
                    fileReader.Close();
                }
            }

            this.Cursor = StoredCursor;
        }
        private string addShpFileLocations(string aSubFolder, string dataType, bool filesExist)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {

                string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
                if (filesExist == false)
                {
                    foreach (string file in shapefiles)
                    {
                        DateTime creationTime = File.GetLastAccessTime(file);
                        TimeSpan timeSpan = DateTime.Now - creationTime;
                        if (timeSpan.Minutes < 2)
                        {
                            text = text + dataType + " shapefile: " + file + Environment.NewLine;
                            countFiles++;
                        }
                    }
                }
                else
                {
                    foreach (string file in shapefiles)
                    {
                        text = text + dataType + " shapefile: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }
        private string addHtmFileLocations(string aSubFolder, string dataType, bool filesExist)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] htmfiles = Directory.GetFiles(aSubFolder, "*.htm", SearchOption.AllDirectories);
                if (filesExist == false)
                {
                    foreach (string file in htmfiles)
                    {
                        DateTime creationTime = File.GetLastAccessTime(file);
                        TimeSpan timeSpan = DateTime.Now - creationTime;
                        if (timeSpan.Minutes < 2)
                        {
                            text = text + dataType + " htm file: " + file + Environment.NewLine;
                            countFiles++;
                        }
                    }
                }
                else
                {
                    foreach (string file in htmfiles)
                    {
                        text = text + dataType + " htm file: " + file + Environment.NewLine;
                        countFiles++;
                    }
                }
            }
            return text;
        }

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCacheFolder.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolder = this.txtCacheFolder.Text;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnNatureServeLoadDatatoMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBirds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBirds.SelectedIndex == 0)
            {
                for (int i = 1; i < listBirds.Items.Count; i++)
                {
                    listBirds.SetItemChecked(i, listBirds.GetItemChecked(0));
                }
            }
            else
            {
                if (!listBirds.GetItemChecked(listBirds.SelectedIndex))
                {
                    listBirds.SetItemChecked(0, false);
                }
            }
        }
 

      
    }
}
