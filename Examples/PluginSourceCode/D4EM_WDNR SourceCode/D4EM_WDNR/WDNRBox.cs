using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D4EM.Data;
using D4EM.Data.Source;
using System.IO;
using DotSpatial.Data;

namespace D4EM_WDNR
{
    public partial class WDNRBox : Form
    {
        string aProjectFolderWDNR;
        string aCacheFolderWDNR;
        List<string> hucnums = null;
        double _north;
        double _south;
        double _east;
        double _west;
        bool useBox = false;

        public WDNRBox(List<string> huc8nums, double north, double south, double east, double west)
        {
            hucnums = null;
            InitializeComponent();
            hucnums = huc8nums;

             if ((north > 0.0) && (south > 0.0) && (east < 0.0) && (west < 0.0))
            {
                useBox = true;
            }

            _north = north;
            _south = south;
            _east = east;
            _west = west;
        }

        private void WDNRBox_Load(object sender, EventArgs e)
        {
            listHucWDNR.Items.Clear();
            foreach (string huc8 in hucnums)
            {
                listHucWDNR.Items.Add(huc8);
                listHUC8huc12.Items.Add(huc8);
            }
            File.Delete(@"C:\Temp\DownloadedFilePathWDNR");
            txtProjectFolderWDNR.Text = @"C:\Temp\ProjectFolderWDNR";
            txtCache.Text = System.IO.Path.GetFullPath("..\\Bin\\Cache");
            if (useBox == true)
            {
                txtNorthWDNR.Text = _north.ToString();
                txtSouthWDNR.Text = _south.ToString();
                txtEastWDNR.Text = _east.ToString();
                txtWestWDNR.Text = _west.ToString();
            }
            else
            {
                txtNorthWDNR.Text = "44.543505";
                txtSouthWDNR.Text = "42.405047";
                txtEastWDNR.Text = "-88.703613";
                txtWestWDNR.Text = "-90.681152";                
            }
            txtHucWDNR.Text = "07070005";
            txtHuc8Huc12WDNR.Text = "07070005";
        }

        private void clickHUC8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://cfpub.epa.gov/surf/locate/index.cfm");
            System.Diagnostics.Process.Start(sInfo);
        }
        private string addShpFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] shapefiles = Directory.GetFiles(aSubFolder, "*.shp", SearchOption.AllDirectories);
                foreach (string file in shapefiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " shapefile: " + file + Environment.NewLine;
                    }
                }
            }
            return text;
        }
        private string addCSVFileLocations(string aSubFolder, string dataType)
        {
            string text = "";
            if (Directory.Exists(aSubFolder))
            {
                string[] csvfiles = Directory.GetFiles(aSubFolder, "*.csv", SearchOption.AllDirectories);
                foreach (string file in csvfiles)
                {
                    DateTime creationTime = File.GetLastWriteTime(file);
                    TimeSpan timeSpan = DateTime.Now - creationTime;
                    if (timeSpan.Minutes < 2)
                    {
                        text = text + dataType + " csvfile: " + file + Environment.NewLine;
                    }
                }
            }
            return text;
        }

        private void btnGetStatewideData_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathWDNR");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string fileLocationsText = "Downloaded WDNR (STATEWIDE) files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;

                aCacheFolderWDNR = System.IO.Path.Combine(txtCache.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                foreach (object an in checkedListAnimals.CheckedItems)
                {
                    string animal = an.ToString();
                    D4EM.Data.LayerSpecification _animal = new D4EM.Data.LayerSpecification();
                    switch (animal)
                    {
                        case "Beef":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                            break;
                        case "Chickens":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                            break;
                        case "Dairy":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                            break;
                        case "Swine":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                            break;
                        case "Turkeys":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                            break;
                    }
                    animals.Add(_animal);
                    D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
                    wdnr.getData(_animal, aProjectFolderWDNR, aCacheFolderWDNR);
                    List<string> fileNames = new List<string>();
                    fileNames.Clear();
                    fileNames = wdnr.FileNames;
                    fileCount = fileNames.Count;
                    fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                    foreach (string file in fileNames)
                    {
                        if (Path.GetExtension(file) == ".shp")
                        {
                            fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                            fileShpTif.WriteLine(file);
                        }
                        if (Path.GetExtension(file) == ".csv")
                        {
                            fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                        }
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;

                }
                fileShpTif.Close();
                labelWDNRStatewide.Visible = true;
                labelWDNRStatewide.Text = "Downloaded data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnWDNRLoadDataToMap.Visible = true;
                }
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            
        }

        

        private void btnBrowseWDNR_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtProjectFolderWDNR.Text = folderbrowserdialog1.SelectedPath;
                aProjectFolderWDNR = this.txtProjectFolderWDNR.Text;
            }
        }

        private void btnGetDataWithinBoxWDNR_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathWDNR");
            try
            {
                int fileCount = 0;
                double aNorth = Convert.ToDouble(txtNorthWDNR.Text.Trim());
                double aSouth = Convert.ToDouble(txtSouthWDNR.Text.Trim());
                double aEast = Convert.ToDouble(txtEastWDNR.Text.Trim());
                double aWest = Convert.ToDouble(txtWestWDNR.Text.Trim());
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                string fileLocationsText = "Downloaded WDNR files (North = " + aNorth + ", South = " + aSouth + ", East = " + aEast + ", West = " + aWest + ")  are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;
                                
                aCacheFolderWDNR = System.IO.Path.Combine(txtCache.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();


                
                foreach (object an in checkedListAnimals.CheckedItems)
                {
                    string animal = an.ToString();
                    D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                    switch (animal)
                    {
                        case "Beef":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                            break;
                        case "Chickens":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                            break;
                        case "Dairy":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                            break;
                        case "Swine":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                            break;
                        case "Turkeys":
                            _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                            break;
                    }
                    animals.Add(_animal);
                    D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
                    wdnr.getDataWithinBoundingBox(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aNorth, aSouth, aEast, aWest);
                    List<string> fileNames = new List<string>();
                    fileNames.Clear();
                    fileNames = wdnr.FileNames;
                    fileCount = fileNames.Count;
                    fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                    foreach (string file in fileNames)
                    {
                        if (Path.GetExtension(file) == ".shp")
                        {
                            fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                            fileShpTif.WriteLine(file);
                        }
                        if (Path.GetExtension(file) == ".csv")
                        {
                            fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                        }
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                fileShpTif.Close();
                labelWDNRBB.Visible = true;
                labelWDNRBB.Text = "Downloaded data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnWDNRLoadDataToMap.Visible = true;
                }  
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;            
        }

        private void btnAddHucWDNR_Click(object sender, EventArgs e)
        {
            listHucWDNR.Items.Add(txtHucWDNR.Text.Trim());
        }

        private void btnRemoveHucWDNR_Click(object sender, EventArgs e)
        {
            while (listHucWDNR.SelectedItems.Count > 0)
            {
                listHucWDNR.Items.Remove(listHucWDNR.SelectedItems[0]);
            }
        }

        private void btnGetDataWithinHuc_Click(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathWDNR");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                aCacheFolderWDNR = System.IO.Path.Combine(txtCache.Text.Trim(), "WDNR");
                string fileLocationsText = "Downloaded WDNR files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;                

                foreach (object huc8 in listHucWDNR.Items)
                {
                    string aHuc8 = huc8.ToString();

                    fileLocationsText = fileLocationsText + "WDNR FILE LOCATIONS for " + aHuc8 + Environment.NewLine;
                    List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                    foreach (object an in checkedListAnimals.CheckedItems)
                    {
                        string animal = an.ToString();
                        D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                        switch (animal)
                        {
                            case "Beef":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                break;
                            case "Chickens":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                break;
                            case "Dairy":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                break;
                            case "Swine":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                break;
                            case "Turkeys":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                break;
                        }
                        animals.Add(_animal);
                        D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
                        wdnr.getDataWithinHuc8(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc8);
                        List<string> fileNames = new List<string>();
                        fileNames.Clear();
                        fileNames = wdnr.FileNames;
                        fileCount = fileNames.Count;
                        fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                        foreach (string file in fileNames)
                        {
                            if (Path.GetExtension(file) == ".shp")
                            {
                                fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                                fileShpTif.WriteLine(file);
                            }
                            if (Path.GetExtension(file) == ".csv")
                            {
                                fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                            }
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                fileShpTif.Close();
                labelWDNRHUC8.Visible = true;
                labelWDNRHUC8.Text = "Downloaded data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnWDNRLoadDataToMap.Visible = true;
                }
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
           
        }

        private void writeDownloadFilePaths(string animal, string aFolderNameExt, TextWriter fileShpTif, string aProjectFolderWDNR)
        {
             string subFolder = System.IO.Path.Combine(aProjectFolderWDNR, aFolderNameExt);
                    if (Directory.Exists(subFolder))
                    {
                        string[] shp = Directory.GetFiles(subFolder, "*.shp", SearchOption.AllDirectories);
                        string[] add = Directory.GetFiles(subFolder, "*.csv", SearchOption.AllDirectories);
                        int i = 0;
                        while (i < shp.Length)
                        {
                            if (File.Exists(shp[i]))
                            {
                                long count = 0;
                                using (StreamReader r = new StreamReader(add[0]))
                                {
                                    string line;
                                    while ((line = r.ReadLine()) != null)
                                    {
                                        count++;
                                    }
                                }
                                if (count > 1)
                                {
                                    fileShpTif.WriteLine(shp[i]);
                                }
                            }
                            i++;
                        }
                    }
                }
               
            
            
        

        List<string> Hucs = new List<string>();
        string aTempFolder = @"C:\Temp\TempHuc12";

        private void btnAddHUC8Huc12_Click(object sender, EventArgs e)
        {
            listHUC8huc12.Items.Add(txtHuc8Huc12WDNR.Text.Trim());
        }
        
        private void btnGetHuc12WithinHuc8_Click_1(object sender, EventArgs e)
        {
            if (listHUC8huc12.Items.Count == 0)
            {
                MessageBox.Show("Please enter at least 1 HUC-8");
                return;
            }
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (object huc in listHUC8huc12.Items)
                {
                    string aHUC8 = huc.ToString();
                    Hucs.Add(aHUC8);
                    string aSaveFolder = aHUC8;

                    DotSpatial.Projections.ProjectionInfo aDesiredProjection = D4EM.Data.Globals.GeographicProjection();
                    D4EM.Data.Region aRegion = new D4EM.Data.Region(0, 0, 0, 0, aDesiredProjection);

                    D4EM.Data.Project aProject = new D4EM.Data.Project(aDesiredProjection, aTempFolder, aTempFolder, aRegion, false, true);
                    D4EM.Data.Source.BASINS.GetBASINS(aProject, aSaveFolder, aHUC8, BASINS.LayerSpecifications.huc12);

                    string huc12ShapeFile = System.IO.Path.Combine(aTempFolder, aHUC8, "huc12.shp");
                    if (File.Exists(huc12ShapeFile))
                    {
                        IFeatureSet fs = FeatureSet.OpenFile(huc12ShapeFile);
                        int count = fs.Features.Count;

                        for (int i = 0; i < count; i++)
                        {
                            IFeature hucFeature = fs.GetFeature(i);
                            string huc12 = hucFeature.DataRow[2].ToString();
                            listHuc12WDNR.Items.Add(huc12);

                        }
                        listHuc12WDNR.Sorted = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            this.Cursor = StoredCursor;
        }

        private void btnRemoveHuc8Huc12_Click_1(object sender, EventArgs e)
        {
            while (listHUC8huc12.SelectedItems.Count > 0)
            {
                listHUC8huc12.Items.Remove(listHUC8huc12.SelectedItems[0]);
            }
        }

        private void btnGetDataWithinHuc12_Click_1(object sender, EventArgs e)
        {
            if (checkedListAnimals.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 animal");
                return;
            }

            if (listHuc12WDNR.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 HUC-12");
                return;
            }
            TextWriter fileShpTif = new StreamWriter(@"C:\Temp\DownloadedFilePathWDNR");
            Cursor StoredCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int fileCount = 0;
                string aProjectFolderWDNR = txtProjectFolderWDNR.Text.Trim();
                aCacheFolderWDNR = System.IO.Path.Combine(txtCache.Text.Trim(), "WDNR");
                List<D4EM.Data.LayerSpecification> animals = new List<LayerSpecification>();
                string fileLocationsText = "Downloaded WDNR files are located in " + aProjectFolderWDNR + Environment.NewLine + Environment.NewLine;
                
                foreach (object huc12 in listHuc12WDNR.SelectedItems)
                {
                    string aHuc12 = huc12.ToString();

                    fileLocationsText = fileLocationsText + "WDNR FILE LOCATIONS for " + aHuc12 + Environment.NewLine;

                    foreach (object an in checkedListAnimals.CheckedItems)
                    {
                        string animal = an.ToString();
                        D4EM.Data.LayerSpecification _animal = new LayerSpecification();
                        switch (animal)
                        {
                            case "Beef":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Beef;
                                break;
                            case "Chickens":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Chickens;
                                break;
                            case "Dairy":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Dairy;
                                break;
                            case "Swine":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Swine;
                                break;
                            case "Turkeys":
                                _animal = D4EM.Data.Source.WDNR.LayerSpecifications.Turkeys;
                                break;
                        }
                        animals.Add(_animal);
                        D4EM.Data.Source.WDNR wdnr = new D4EM.Data.Source.WDNR();
                        wdnr.getDataWithinHuc12(_animal, aProjectFolderWDNR, aCacheFolderWDNR, aHuc12);
                        List<string> fileNames = new List<string>();
                        fileNames.Clear();
                        fileNames = wdnr.FileNames;
                        fileCount = fileNames.Count;
                        fileLocationsText = fileLocationsText + animal + Environment.NewLine;
                        foreach (string file in fileNames)
                        {
                            if (Path.GetExtension(file) == ".shp")
                            {
                                fileLocationsText = fileLocationsText + "Shapefile: " + file + Environment.NewLine;
                                fileShpTif.WriteLine(file);
                            }
                            if (Path.GetExtension(file) == ".csv")
                            {
                                fileLocationsText = fileLocationsText + "CSV file: " + file + Environment.NewLine;
                            }
                        }
                        fileLocationsText = fileLocationsText + Environment.NewLine;
                    }
                    fileLocationsText = fileLocationsText + Environment.NewLine;
                }
                fileShpTif.Close();
                /*
                foreach (string huc in Hucs)
                {
                    string aHuc8 = huc;
                    string tempFolder = System.IO.Path.Combine(aTempFolder, aHuc8);
                    if (Directory.Exists(tempFolder))
                    {
                        string[] files = Directory.GetFileSystemEntries(tempFolder);
                        int count = files.Length;
                        for (int i = 0; i < count; i++)
                        {
                            File.Delete(files[i]);
                        }
                        Directory.Delete(tempFolder);
                    }
                    if (Directory.Exists(aTempFolder + "\\clsBASINS"))
                    {
                        string[] files = Directory.GetFileSystemEntries(aTempFolder + "\\clsBASINS");
                        int count = files.Length;
                        for (int i = 0; i < count; i++)
                        {
                            File.Delete(files[i]);
                        }
                        Directory.Delete(aTempFolder + "\\clsBASINS");
                    }
                }
                Directory.Delete(aTempFolder);
                */
                labelWDNRHUC12.Visible = true;
                labelWDNRHUC12.Text = "Downloaded data is located in " + aProjectFolderWDNR;
                if (fileCount == 0)
                {
                    MessageBox.Show("No files were downloaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(fileLocationsText, "WDNR File Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnWDNRLoadDataToMap.Visible = true;
                }
            }
            catch (Exception ex)
            {
                fileShpTif.Close();
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = StoredCursor;
            
        }

        private void btnBrowseCache_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog1 = new FolderBrowserDialog();
            if (folderbrowserdialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtCache.Text = folderbrowserdialog1.SelectedPath;
                aCacheFolderWDNR = this.txtCache.Text;
            }
        }

        private void btnWDNRLoadDataToMap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
