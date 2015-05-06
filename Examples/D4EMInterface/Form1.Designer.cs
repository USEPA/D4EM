namespace testingNLCD
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabNWIS = new System.Windows.Forms.TabPage();
            this.groupNWISspecific = new System.Windows.Forms.GroupBox();
            this.listNWISDataTypesSpecific = new System.Windows.Forms.CheckedListBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtProjectNWIS = new System.Windows.Forms.TextBox();
            this.btnBrowseProjectNWIS = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listNWIS = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnDowloadNWISUsingStationIds = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.listNWISStations = new System.Windows.Forms.ListBox();
            this.btnAddStations = new System.Windows.Forms.Button();
            this.txtNWISStation = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNorthNWIS = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSouthNWIS = new System.Windows.Forms.TextBox();
            this.btnRunNWIS = new System.Windows.Forms.Button();
            this.txtEastNWIS = new System.Windows.Forms.TextBox();
            this.txtWestNWIS = new System.Windows.Forms.TextBox();
            this.labelNWIS = new System.Windows.Forms.Label();
            this.NHDPlus = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label62 = new System.Windows.Forms.Label();
            this.txtCacheNHDPlus = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNHDPlus = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtProjectFolderNHDPlus = new System.Windows.Forms.TextBox();
            this.btnBrowseProjectNHDPlus = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.checkedListNHDPlus = new System.Windows.Forms.CheckedListBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtHUC8NHDPlus = new System.Windows.Forms.TextBox();
            this.btnAddHuc8NHDPlus = new System.Windows.Forms.Button();
            this.btnRemoveNHDPlus = new System.Windows.Forms.Button();
            this.listHuc8NHDPlus = new System.Windows.Forms.ListBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.labelNHDPlus = new System.Windows.Forms.Label();
            this.btnRunNHDplus = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label61 = new System.Windows.Forms.Label();
            this.btnBrowseCacheBasins = new System.Windows.Forms.Button();
            this.txtCacheBasins = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnBrowseProjectFolderBasins = new System.Windows.Forms.Button();
            this.txtProjectFolderBasins = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.boxBasinsDataType = new System.Windows.Forms.CheckedListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHUC8Basins = new System.Windows.Forms.TextBox();
            this.btnRemoveBasins = new System.Windows.Forms.Button();
            this.btnAddBasins = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.listHuc8Basins = new System.Windows.Forms.ListBox();
            this.labelBasins = new System.Windows.Forms.Label();
            this.btnRunBasins = new System.Windows.Forms.Button();
            this.tabUSGS_Seamless = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtCacheFolderUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNLCD = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProjectFolderUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.btnBrowseProjectNLCD = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.boxLayer = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNorthUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSouthUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.txtEastUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.txtWestUSGS_Seamless = new System.Windows.Forms.TextBox();
            this.labelUSGS_Seamless = new System.Windows.Forms.Label();
            this.btnRunNLCD = new System.Windows.Forms.Button();
            this.D4EMInterface = new System.Windows.Forms.TabControl();
            this.tabNRCSSOIL = new System.Windows.Forms.TabPage();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.txtNorth = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.txtSouth = new System.Windows.Forms.TextBox();
            this.txtEast = new System.Windows.Forms.TextBox();
            this.txtWest = new System.Windows.Forms.TextBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txtCacheNRCSSoil = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNRCSSoil = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.txtProjectFolderSoils = new System.Windows.Forms.TextBox();
            this.btnBrowseSoils = new System.Windows.Forms.Button();
            this.btnRunNRCSSoil = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataset = new System.Windows.Forms.GroupBox();
            this.datasetType = new System.Windows.Forms.CheckedListBox();
            this.groupBoxNCDCButtons = new System.Windows.Forms.GroupBox();
            this.btnDownloadNCDC = new System.Windows.Forms.Button();
            this.btnDownloadforSelectedStation = new System.Windows.Forms.Button();
            this.label47 = new System.Windows.Forms.Label();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.dataVariablesNCDC = new System.Windows.Forms.DataGridView();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.dataStationsNCDC = new System.Windows.Forms.DataGridView();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txtCacheNCDC = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNCDC = new System.Windows.Forms.Button();
            this.label65 = new System.Windows.Forms.Label();
            this.txtProjectFolderNCDC = new System.Windows.Forms.TextBox();
            this.btnBrowseNCDC = new System.Windows.Forms.Button();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.outputType = new System.Windows.Forms.CheckedListBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.listStatesNCDC = new System.Windows.Forms.CheckedListBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.labelNCDC = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txtEndDay = new System.Windows.Forms.TextBox();
            this.txtEndMonth = new System.Windows.Forms.TextBox();
            this.txtEndYear = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtStartDay = new System.Windows.Forms.TextBox();
            this.txtStartMonth = new System.Windows.Forms.TextBox();
            this.txtStartYear = new System.Windows.Forms.TextBox();
            this.tabNatureServe = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.listPollinator = new System.Windows.Forms.CheckedListBox();
            this.btnDownloadNatureServe = new System.Windows.Forms.Button();
            this.labelNatureServe = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label67 = new System.Windows.Forms.Label();
            this.txtCacheNatureServe = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNatureServe = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtProjectFolderNatureServe = new System.Windows.Forms.TextBox();
            this.btnBrowseNatureServe = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnCreateHUCNativeFishSpeciesFile = new System.Windows.Forms.Button();
            this.dataGridViewNatureServe = new System.Windows.Forms.DataGridView();
            this.btnPopulateNativeSpeciesTable = new System.Windows.Forms.Button();
            this.txtHUC8natureServe = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listStoretDataTypes = new System.Windows.Forms.CheckedListBox();
            this.labelStoret = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtProjectFolderStoret = new System.Windows.Forms.TextBox();
            this.btnBrowseStoret = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtNorthStoret = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtSouthStoret = new System.Windows.Forms.TextBox();
            this.txtEastStoret = new System.Windows.Forms.TextBox();
            this.txtWestStoret = new System.Windows.Forms.TextBox();
            this.btnDownloadStoret = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.labelWDNRStatewide = new System.Windows.Forms.Label();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.btnSelectAllHuc12 = new System.Windows.Forms.Button();
            this.labelWDNRHUC12 = new System.Windows.Forms.Label();
            this.btnAddHUC8Huc12 = new System.Windows.Forms.Button();
            this.btnRemoveHuc8Huc12 = new System.Windows.Forms.Button();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.listHUC8huc12 = new System.Windows.Forms.ListBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtHuc8Huc12WDNR = new System.Windows.Forms.TextBox();
            this.btnGetDataWithinHuc12 = new System.Windows.Forms.Button();
            this.listHuc12WDNR = new System.Windows.Forms.ListBox();
            this.btnGetHuc12WithinHuc8 = new System.Windows.Forms.Button();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.labelWDNRHUC8 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.btnGetDataWithinHuc = new System.Windows.Forms.Button();
            this.txtHucWDNR = new System.Windows.Forms.TextBox();
            this.btnAddHucWDNR = new System.Windows.Forms.Button();
            this.btnRemoveHucWDNR = new System.Windows.Forms.Button();
            this.listHucWDNR = new System.Windows.Forms.ListBox();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.labelWDNRBB = new System.Windows.Forms.Label();
            this.btnGetDataWithinBoxWDNR = new System.Windows.Forms.Button();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWestWDNR = new System.Windows.Forms.TextBox();
            this.txtEastWDNR = new System.Windows.Forms.TextBox();
            this.txtSouthWDNR = new System.Windows.Forms.TextBox();
            this.txtNorthWDNR = new System.Windows.Forms.TextBox();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.checkedListAnimals = new System.Windows.Forms.CheckedListBox();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.label70 = new System.Windows.Forms.Label();
            this.txtCacheWDNR = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheWDNR = new System.Windows.Forms.Button();
            this.label69 = new System.Windows.Forms.Label();
            this.txtProjectFolderWDNR = new System.Windows.Forms.TextBox();
            this.btnBrowseWDNR = new System.Windows.Forms.Button();
            this.btnGetSpreadsheet = new System.Windows.Forms.Button();
            this.btnDownloadNASS = new System.Windows.Forms.TabPage();
            this.labelNASS = new System.Windows.Forms.Label();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.listYearsNASS = new System.Windows.Forms.CheckedListBox();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.label71 = new System.Windows.Forms.Label();
            this.txtCacheNASS = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheNASS = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.txtProjectFolderNASS = new System.Windows.Forms.TextBox();
            this.btnBrowseNASS = new System.Windows.Forms.Button();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.txtNorthNASS = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txtSouthNASS = new System.Windows.Forms.TextBox();
            this.txtEastNASS = new System.Windows.Forms.TextBox();
            this.txtWestNASS = new System.Windows.Forms.TextBox();
            this.btnNASSDownload = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNDBCyear = new System.Windows.Forms.TextBox();
            this.txtNDBCStationID = new System.Windows.Forms.TextBox();
            this.btnNDBChistoricalData = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.btnBrowseNDBC = new System.Windows.Forms.Button();
            this.txtProjectFolderNDBC = new System.Windows.Forms.TextBox();
            this.dataGridViewNDBC = new System.Windows.Forms.DataGridView();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtRadiusNDBC = new System.Windows.Forms.TextBox();
            this.txtLongitudeNDBC = new System.Windows.Forms.TextBox();
            this.txtLatitudeNDBC = new System.Windows.Forms.TextBox();
            this.labelNDBC = new System.Windows.Forms.Label();
            this.btnRunNDBC = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label81 = new System.Windows.Forms.Label();
            this.txtHucNLDAS = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.txtCacheFolderNLDAS = new System.Windows.Forms.TextBox();
            this.txtProjectFolderNLDAS = new System.Windows.Forms.TextBox();
            this.lblNLDAS = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtLongitudeNLDAS = new System.Windows.Forms.TextBox();
            this.txtLatitudeNLDAS = new System.Windows.Forms.TextBox();
            this.btnRunNLDAS = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.btnGetNAWQAaverageStdDev = new System.Windows.Forms.Button();
            this.groupNAWQAchoice = new System.Windows.Forms.GroupBox();
            this.radioUseLatLong = new System.Windows.Forms.RadioButton();
            this.radioUseStatesCounties = new System.Windows.Forms.RadioButton();
            this.gridShowNAWQAaverages = new System.Windows.Forms.DataGridView();
            this.groupNAWQAstatesCounties = new System.Windows.Forms.GroupBox();
            this.groupNAWQAstates = new System.Windows.Forms.GroupBox();
            this.listNAWQAstates = new System.Windows.Forms.ListBox();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.listCounties = new System.Windows.Forms.CheckedListBox();
            this.groupNAWQAlatLong = new System.Windows.Forms.GroupBox();
            this.btnDetermineCounty = new System.Windows.Forms.Button();
            this.labelNAWQAlatLongCounty = new System.Windows.Forms.Label();
            this.txtNAWQAlng = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtNAWQAlat = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.labelNAWQA = new System.Windows.Forms.Label();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCacheFolderNAWQA = new System.Windows.Forms.TextBox();
            this.btnBrowseCacheFolderNAWQA = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtProjectFolderNAWQA = new System.Windows.Forms.TextBox();
            this.btnBrowseProjectFolderNAWQA = new System.Windows.Forms.Button();
            this.groupNAQWAdates = new System.Windows.Forms.GroupBox();
            this.chkBoxWater = new System.Windows.Forms.CheckBox();
            this.groupNAWQAstart = new System.Windows.Forms.GroupBox();
            this.txtStartYearNAQWA = new System.Windows.Forms.TextBox();
            this.groupNAQWAend = new System.Windows.Forms.GroupBox();
            this.txtEndYearNAQWA = new System.Windows.Forms.TextBox();
            this.groupNAWQAqueryTypes = new System.Windows.Forms.GroupBox();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.groupNAWQAfileTypes = new System.Windows.Forms.GroupBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupNAWQAwaterType = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupNAWQAparameters = new System.Windows.Forms.GroupBox();
            this.listNAWQAparameters = new System.Windows.Forms.CheckedListBox();
            this.btnRunNAWQA = new System.Windows.Forms.Button();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.lblHSPFComplete = new System.Windows.Forms.Label();
            this.btnBrowseHuc = new System.Windows.Forms.Button();
            this.txtHucNumHSPF = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.btnBrowseCacheHSPF = new System.Windows.Forms.Button();
            this.label78 = new System.Windows.Forms.Label();
            this.txtCacheFolderHSPF = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.txtSimEndYearHSPF = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txtSimStartYearHSPF = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.txtLanduseIgnoreBelowFraction = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtMinFlowlineKM = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txtMinCatchKM2 = new System.Windows.Forms.TextBox();
            this.btnBrowseProjHSPF = new System.Windows.Forms.Button();
            this.txtProjectFolderHSPF = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.btnRunHSPF = new System.Windows.Forms.Button();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.btnRunSWAT = new System.Windows.Forms.Button();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.btnRunDelineation = new System.Windows.Forms.Button();
            this.appManager = new DotSpatial.Controls.AppManager();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabNWIS.SuspendLayout();
            this.groupNWISspecific.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NHDPlus.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabUSGS_Seamless.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.D4EMInterface.SuspendLayout();
            this.tabNRCSSOIL.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.dataset.SuspendLayout();
            this.groupBoxNCDCButtons.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataVariablesNCDC)).BeginInit();
            this.groupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataStationsNCDC)).BeginInit();
            this.groupBox24.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tabNatureServe.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNatureServe)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.btnDownloadNASS.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox37.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNDBC)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.groupNAWQAchoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShowNAWQAaverages)).BeginInit();
            this.groupNAWQAstatesCounties.SuspendLayout();
            this.groupNAWQAstates.SuspendLayout();
            this.groupBox35.SuspendLayout();
            this.groupNAWQAlatLong.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.groupNAQWAdates.SuspendLayout();
            this.groupNAWQAstart.SuspendLayout();
            this.groupNAQWAend.SuspendLayout();
            this.groupNAWQAqueryTypes.SuspendLayout();
            this.groupNAWQAfileTypes.SuspendLayout();
            this.groupNAWQAwaterType.SuspendLayout();
            this.groupNAWQAparameters.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabNWIS
            // 
            this.tabNWIS.Controls.Add(this.groupNWISspecific);
            this.tabNWIS.Controls.Add(this.groupBox13);
            this.tabNWIS.Controls.Add(this.groupBox3);
            this.tabNWIS.Controls.Add(this.groupBox2);
            this.tabNWIS.Controls.Add(this.groupBox1);
            this.tabNWIS.Controls.Add(this.labelNWIS);
            this.tabNWIS.Location = new System.Drawing.Point(4, 22);
            this.tabNWIS.Name = "tabNWIS";
            this.tabNWIS.Padding = new System.Windows.Forms.Padding(3);
            this.tabNWIS.Size = new System.Drawing.Size(892, 655);
            this.tabNWIS.TabIndex = 4;
            this.tabNWIS.Text = "NWIS";
            this.tabNWIS.UseVisualStyleBackColor = true;
            // 
            // groupNWISspecific
            // 
            this.groupNWISspecific.Controls.Add(this.listNWISDataTypesSpecific);
            this.groupNWISspecific.Location = new System.Drawing.Point(50, 326);
            this.groupNWISspecific.Name = "groupNWISspecific";
            this.groupNWISspecific.Size = new System.Drawing.Size(444, 113);
            this.groupNWISspecific.TabIndex = 67;
            this.groupNWISspecific.TabStop = false;
            this.groupNWISspecific.Visible = false;
            // 
            // listNWISDataTypesSpecific
            // 
            this.listNWISDataTypesSpecific.CheckOnClick = true;
            this.listNWISDataTypesSpecific.FormattingEnabled = true;
            this.listNWISDataTypesSpecific.Items.AddRange(new object[] {
            "00010  - Temperature, water, degrees Celsius",
            "00025  - Barometric pressure, millimeters of mercury",
            "00400  - pH, water, unfiltered, field, standard units",
            "00680  - Organic carbon, water, unfiltered, milligrams per liter"});
            this.listNWISDataTypesSpecific.Location = new System.Drawing.Point(23, 19);
            this.listNWISDataTypesSpecific.Name = "listNWISDataTypesSpecific";
            this.listNWISDataTypesSpecific.Size = new System.Drawing.Size(371, 79);
            this.listNWISDataTypesSpecific.TabIndex = 65;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label15);
            this.groupBox13.Controls.Add(this.txtProjectNWIS);
            this.groupBox13.Controls.Add(this.btnBrowseProjectNWIS);
            this.groupBox13.Location = new System.Drawing.Point(26, 230);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(491, 65);
            this.groupBox13.TabIndex = 64;
            this.groupBox13.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "Project Folder";
            // 
            // txtProjectNWIS
            // 
            this.txtProjectNWIS.Location = new System.Drawing.Point(108, 23);
            this.txtProjectNWIS.Name = "txtProjectNWIS";
            this.txtProjectNWIS.Size = new System.Drawing.Size(259, 20);
            this.txtProjectNWIS.TabIndex = 55;
            // 
            // btnBrowseProjectNWIS
            // 
            this.btnBrowseProjectNWIS.Location = new System.Drawing.Point(393, 23);
            this.btnBrowseProjectNWIS.Name = "btnBrowseProjectNWIS";
            this.btnBrowseProjectNWIS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectNWIS.TabIndex = 58;
            this.btnBrowseProjectNWIS.Text = "Browse";
            this.btnBrowseProjectNWIS.UseVisualStyleBackColor = true;
            this.btnBrowseProjectNWIS.Click += new System.EventHandler(this.btnBrowseProjectNWIS_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listNWIS);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 100);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NWIS Data Types";
            // 
            // listNWIS
            // 
            this.listNWIS.CheckOnClick = true;
            this.listNWIS.FormattingEnabled = true;
            this.listNWIS.Items.AddRange(new object[] {
            "Daily Discharge",
            "IDA Discharge",
            "Measurement",
            "Water Quality"});
            this.listNWIS.Location = new System.Drawing.Point(6, 22);
            this.listNWIS.Name = "listNWIS";
            this.listNWIS.Size = new System.Drawing.Size(120, 64);
            this.listNWIS.TabIndex = 59;
            this.listNWIS.SelectedIndexChanged += new System.EventHandler(this.listNWIS_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.btnDowloadNWISUsingStationIds);
            this.groupBox2.Controls.Add(this.btnRemoveSelected);
            this.groupBox2.Controls.Add(this.listNWISStations);
            this.groupBox2.Controls.Add(this.btnAddStations);
            this.groupBox2.Controls.Add(this.txtNWISStation);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(357, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 205);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download NWIS using Station IDs";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(11, 35);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(87, 13);
            this.linkLabel1.TabIndex = 64;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Look up Stations";
            this.linkLabel1.Click += new System.EventHandler(this.btnLookUpStations_Click);
            // 
            // btnDowloadNWISUsingStationIds
            // 
            this.btnDowloadNWISUsingStationIds.Location = new System.Drawing.Point(47, 167);
            this.btnDowloadNWISUsingStationIds.Name = "btnDowloadNWISUsingStationIds";
            this.btnDowloadNWISUsingStationIds.Size = new System.Drawing.Size(142, 23);
            this.btnDowloadNWISUsingStationIds.TabIndex = 68;
            this.btnDowloadNWISUsingStationIds.Text = "Download";
            this.btnDowloadNWISUsingStationIds.UseVisualStyleBackColor = true;
            this.btnDowloadNWISUsingStationIds.Click += new System.EventHandler(this.btnDowloadNWISUsingStationIds_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(10, 127);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveSelected.TabIndex = 67;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // listNWISStations
            // 
            this.listNWISStations.FormattingEnabled = true;
            this.listNWISStations.Location = new System.Drawing.Point(126, 60);
            this.listNWISStations.Name = "listNWISStations";
            this.listNWISStations.Size = new System.Drawing.Size(91, 95);
            this.listNWISStations.TabIndex = 65;
            // 
            // btnAddStations
            // 
            this.btnAddStations.Location = new System.Drawing.Point(10, 60);
            this.btnAddStations.Name = "btnAddStations";
            this.btnAddStations.Size = new System.Drawing.Size(100, 39);
            this.btnAddStations.TabIndex = 64;
            this.btnAddStations.Text = "Add Station ID to List";
            this.btnAddStations.UseVisualStyleBackColor = true;
            this.btnAddStations.Click += new System.EventHandler(this.btnAddStations_Click);
            // 
            // txtNWISStation
            // 
            this.txtNWISStation.Location = new System.Drawing.Point(126, 19);
            this.txtNWISStation.Name = "txtNWISStation";
            this.txtNWISStation.Size = new System.Drawing.Size(100, 20);
            this.txtNWISStation.TabIndex = 62;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 63;
            this.label17.Text = "Enter a Station ID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNorthNWIS);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSouthNWIS);
            this.groupBox1.Controls.Add(this.btnRunNWIS);
            this.groupBox1.Controls.Add(this.txtEastNWIS);
            this.groupBox1.Controls.Add(this.txtWestNWIS);
            this.groupBox1.Location = new System.Drawing.Point(151, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 180);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download NWIS using Coordinates";
            // 
            // txtNorthNWIS
            // 
            this.txtNorthNWIS.Location = new System.Drawing.Point(82, 19);
            this.txtNorthNWIS.Name = "txtNorthNWIS";
            this.txtNorthNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtNorthNWIS.TabIndex = 45;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(44, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "North";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "South";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "West";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "East";
            // 
            // txtSouthNWIS
            // 
            this.txtSouthNWIS.Location = new System.Drawing.Point(82, 42);
            this.txtSouthNWIS.Name = "txtSouthNWIS";
            this.txtSouthNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtSouthNWIS.TabIndex = 47;
            // 
            // btnRunNWIS
            // 
            this.btnRunNWIS.Location = new System.Drawing.Point(10, 128);
            this.btnRunNWIS.Name = "btnRunNWIS";
            this.btnRunNWIS.Size = new System.Drawing.Size(171, 23);
            this.btnRunNWIS.TabIndex = 0;
            this.btnRunNWIS.Text = "Download";
            this.btnRunNWIS.UseVisualStyleBackColor = true;
            this.btnRunNWIS.Click += new System.EventHandler(this.btnRunNWIS_Click);
            // 
            // txtEastNWIS
            // 
            this.txtEastNWIS.Location = new System.Drawing.Point(82, 67);
            this.txtEastNWIS.Name = "txtEastNWIS";
            this.txtEastNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtEastNWIS.TabIndex = 51;
            // 
            // txtWestNWIS
            // 
            this.txtWestNWIS.Location = new System.Drawing.Point(82, 90);
            this.txtWestNWIS.Name = "txtWestNWIS";
            this.txtWestNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtWestNWIS.TabIndex = 49;
            // 
            // labelNWIS
            // 
            this.labelNWIS.AutoSize = true;
            this.labelNWIS.Location = new System.Drawing.Point(47, 481);
            this.labelNWIS.Name = "labelNWIS";
            this.labelNWIS.Size = new System.Drawing.Size(35, 13);
            this.labelNWIS.TabIndex = 60;
            this.labelNWIS.Text = "label3";
            this.labelNWIS.Visible = false;
            // 
            // NHDPlus
            // 
            this.NHDPlus.Controls.Add(this.groupBox12);
            this.NHDPlus.Controls.Add(this.groupBox11);
            this.NHDPlus.Controls.Add(this.groupBox10);
            this.NHDPlus.Controls.Add(this.labelNHDPlus);
            this.NHDPlus.Controls.Add(this.btnRunNHDplus);
            this.NHDPlus.Location = new System.Drawing.Point(4, 22);
            this.NHDPlus.Name = "NHDPlus";
            this.NHDPlus.Padding = new System.Windows.Forms.Padding(3);
            this.NHDPlus.Size = new System.Drawing.Size(892, 655);
            this.NHDPlus.TabIndex = 2;
            this.NHDPlus.Text = "NHDPlus";
            this.NHDPlus.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label62);
            this.groupBox12.Controls.Add(this.txtCacheNHDPlus);
            this.groupBox12.Controls.Add(this.btnBrowseCacheNHDPlus);
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Controls.Add(this.txtProjectFolderNHDPlus);
            this.groupBox12.Controls.Add(this.btnBrowseProjectNHDPlus);
            this.groupBox12.Location = new System.Drawing.Point(32, 156);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(457, 94);
            this.groupBox12.TabIndex = 67;
            this.groupBox12.TabStop = false;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(6, 53);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(70, 13);
            this.label62.TabIndex = 57;
            this.label62.Text = "Cache Folder";
            // 
            // txtCacheNHDPlus
            // 
            this.txtCacheNHDPlus.Location = new System.Drawing.Point(82, 53);
            this.txtCacheNHDPlus.Name = "txtCacheNHDPlus";
            this.txtCacheNHDPlus.Size = new System.Drawing.Size(259, 20);
            this.txtCacheNHDPlus.TabIndex = 56;
            // 
            // btnBrowseCacheNHDPlus
            // 
            this.btnBrowseCacheNHDPlus.Location = new System.Drawing.Point(347, 53);
            this.btnBrowseCacheNHDPlus.Name = "btnBrowseCacheNHDPlus";
            this.btnBrowseCacheNHDPlus.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNHDPlus.TabIndex = 58;
            this.btnBrowseCacheNHDPlus.Text = "Browse";
            this.btnBrowseCacheNHDPlus.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNHDPlus.Click += new System.EventHandler(this.btnBrowseCacheNHDPlus_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 53;
            this.label19.Text = "Project Folder";
            // 
            // txtProjectFolderNHDPlus
            // 
            this.txtProjectFolderNHDPlus.Location = new System.Drawing.Point(82, 16);
            this.txtProjectFolderNHDPlus.Name = "txtProjectFolderNHDPlus";
            this.txtProjectFolderNHDPlus.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNHDPlus.TabIndex = 52;
            // 
            // btnBrowseProjectNHDPlus
            // 
            this.btnBrowseProjectNHDPlus.Location = new System.Drawing.Point(347, 16);
            this.btnBrowseProjectNHDPlus.Name = "btnBrowseProjectNHDPlus";
            this.btnBrowseProjectNHDPlus.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectNHDPlus.TabIndex = 55;
            this.btnBrowseProjectNHDPlus.Text = "Browse";
            this.btnBrowseProjectNHDPlus.UseVisualStyleBackColor = true;
            this.btnBrowseProjectNHDPlus.Click += new System.EventHandler(this.btnBrowseProjectNHDPlus_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.checkedListNHDPlus);
            this.groupBox11.Location = new System.Drawing.Point(349, 15);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(235, 129);
            this.groupBox11.TabIndex = 66;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "NHDPLus Data Types";
            // 
            // checkedListNHDPlus
            // 
            this.checkedListNHDPlus.CheckOnClick = true;
            this.checkedListNHDPlus.FormattingEnabled = true;
            this.checkedListNHDPlus.Items.AddRange(new object[] {
            "CatchmentGrid",
            "CatchmentPolygons",
            "ElevationGrid",
            "FlowAccumulationGrid",
            "FlowDirectionGrid",
            "SlopeGrid",
            "Hydrography.Area",
            "Hydrography.Flowline",
            "Hydrography.Line",
            "Hydrography.Point",
            "Hydrography.Waterbody",
            "HydrologicUnits.RegionPolygons",
            "HydrologicUnits.SubBasinPolygons",
            "HydrologicUnits.SubWatershedPolygons",
            "HydrologicUnits.WatershedPolygons"});
            this.checkedListNHDPlus.Location = new System.Drawing.Point(21, 19);
            this.checkedListNHDPlus.Name = "checkedListNHDPlus";
            this.checkedListNHDPlus.Size = new System.Drawing.Size(189, 94);
            this.checkedListNHDPlus.TabIndex = 63;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.txtHUC8NHDPlus);
            this.groupBox10.Controls.Add(this.btnAddHuc8NHDPlus);
            this.groupBox10.Controls.Add(this.btnRemoveNHDPlus);
            this.groupBox10.Controls.Add(this.listHuc8NHDPlus);
            this.groupBox10.Controls.Add(this.linkLabel2);
            this.groupBox10.Location = new System.Drawing.Point(6, 15);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(337, 129);
            this.groupBox10.TabIndex = 65;
            this.groupBox10.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 13);
            this.label18.TabIndex = 49;
            this.label18.Text = "Enter a HUC-8";
            // 
            // txtHUC8NHDPlus
            // 
            this.txtHUC8NHDPlus.Location = new System.Drawing.Point(105, 13);
            this.txtHUC8NHDPlus.Name = "txtHUC8NHDPlus";
            this.txtHUC8NHDPlus.Size = new System.Drawing.Size(100, 20);
            this.txtHUC8NHDPlus.TabIndex = 48;
            // 
            // btnAddHuc8NHDPlus
            // 
            this.btnAddHuc8NHDPlus.Location = new System.Drawing.Point(105, 39);
            this.btnAddHuc8NHDPlus.Name = "btnAddHuc8NHDPlus";
            this.btnAddHuc8NHDPlus.Size = new System.Drawing.Size(100, 23);
            this.btnAddHuc8NHDPlus.TabIndex = 58;
            this.btnAddHuc8NHDPlus.Text = "Add HUC-8 to List";
            this.btnAddHuc8NHDPlus.UseVisualStyleBackColor = true;
            this.btnAddHuc8NHDPlus.Click += new System.EventHandler(this.btnAddHuc8NHDPlus_Click);
            // 
            // btnRemoveNHDPlus
            // 
            this.btnRemoveNHDPlus.Location = new System.Drawing.Point(105, 69);
            this.btnRemoveNHDPlus.Name = "btnRemoveNHDPlus";
            this.btnRemoveNHDPlus.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveNHDPlus.TabIndex = 61;
            this.btnRemoveNHDPlus.Text = "Remove Selected";
            this.btnRemoveNHDPlus.UseVisualStyleBackColor = true;
            this.btnRemoveNHDPlus.Click += new System.EventHandler(this.btnRemoveNHDPlus_Click);
            // 
            // listHuc8NHDPlus
            // 
            this.listHuc8NHDPlus.FormattingEnabled = true;
            this.listHuc8NHDPlus.Location = new System.Drawing.Point(232, 13);
            this.listHuc8NHDPlus.Name = "listHuc8NHDPlus";
            this.listHuc8NHDPlus.Size = new System.Drawing.Size(91, 95);
            this.listHuc8NHDPlus.TabIndex = 59;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(72, 95);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(130, 13);
            this.linkLabel2.TabIndex = 60;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Click here to find a HUC-8";
            this.linkLabel2.Click += new System.EventHandler(this.btnHUCfind_Click);
            // 
            // labelNHDPlus
            // 
            this.labelNHDPlus.AutoSize = true;
            this.labelNHDPlus.Location = new System.Drawing.Point(38, 310);
            this.labelNHDPlus.Name = "labelNHDPlus";
            this.labelNHDPlus.Size = new System.Drawing.Size(35, 13);
            this.labelNHDPlus.TabIndex = 64;
            this.labelNHDPlus.Text = "label1";
            this.labelNHDPlus.Visible = false;
            // 
            // btnRunNHDplus
            // 
            this.btnRunNHDplus.Location = new System.Drawing.Point(185, 256);
            this.btnRunNHDplus.Name = "btnRunNHDplus";
            this.btnRunNHDplus.Size = new System.Drawing.Size(131, 23);
            this.btnRunNHDplus.TabIndex = 0;
            this.btnRunNHDplus.Text = "Download";
            this.btnRunNHDplus.UseVisualStyleBackColor = true;
            this.btnRunNHDplus.Click += new System.EventHandler(this.btnRunNHDplus_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.labelBasins);
            this.tabPage2.Controls.Add(this.btnRunBasins);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(892, 655);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "BASINS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label61);
            this.groupBox9.Controls.Add(this.btnBrowseCacheBasins);
            this.groupBox9.Controls.Add(this.txtCacheBasins);
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Controls.Add(this.btnBrowseProjectFolderBasins);
            this.groupBox9.Controls.Add(this.txtProjectFolderBasins);
            this.groupBox9.Location = new System.Drawing.Point(60, 168);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(459, 106);
            this.groupBox9.TabIndex = 71;
            this.groupBox9.TabStop = false;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(10, 62);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(70, 13);
            this.label61.TabIndex = 46;
            this.label61.Text = "Cache Folder";
            // 
            // btnBrowseCacheBasins
            // 
            this.btnBrowseCacheBasins.Location = new System.Drawing.Point(352, 59);
            this.btnBrowseCacheBasins.Name = "btnBrowseCacheBasins";
            this.btnBrowseCacheBasins.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheBasins.TabIndex = 47;
            this.btnBrowseCacheBasins.Text = "Browse";
            this.btnBrowseCacheBasins.UseVisualStyleBackColor = true;
            this.btnBrowseCacheBasins.Click += new System.EventHandler(this.btnBrowseCacheBasins_Click);
            // 
            // txtCacheBasins
            // 
            this.txtCacheBasins.Location = new System.Drawing.Point(88, 59);
            this.txtCacheBasins.Name = "txtCacheBasins";
            this.txtCacheBasins.Size = new System.Drawing.Size(259, 20);
            this.txtCacheBasins.TabIndex = 45;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Project Folder";
            // 
            // btnBrowseProjectFolderBasins
            // 
            this.btnBrowseProjectFolderBasins.Location = new System.Drawing.Point(352, 25);
            this.btnBrowseProjectFolderBasins.Name = "btnBrowseProjectFolderBasins";
            this.btnBrowseProjectFolderBasins.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectFolderBasins.TabIndex = 44;
            this.btnBrowseProjectFolderBasins.Text = "Browse";
            this.btnBrowseProjectFolderBasins.UseVisualStyleBackColor = true;
            this.btnBrowseProjectFolderBasins.Click += new System.EventHandler(this.btnBrowseProjectFolderBasins_Click);
            // 
            // txtProjectFolderBasins
            // 
            this.txtProjectFolderBasins.Location = new System.Drawing.Point(88, 25);
            this.txtProjectFolderBasins.Name = "txtProjectFolderBasins";
            this.txtProjectFolderBasins.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderBasins.TabIndex = 39;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.boxBasinsDataType);
            this.groupBox8.Location = new System.Drawing.Point(408, 21);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(151, 128);
            this.groupBox8.TabIndex = 70;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "BASINS Data Types";
            // 
            // boxBasinsDataType
            // 
            this.boxBasinsDataType.CheckOnClick = true;
            this.boxBasinsDataType.FormattingEnabled = true;
            this.boxBasinsDataType.Items.AddRange(new object[] {
            "census",
            "core31",
            "dem",
            "DEMG",
            "giras",
            "huc12",
            "lstoret",
            "NED",
            "nhd",
            "pcs3"});
            this.boxBasinsDataType.Location = new System.Drawing.Point(6, 19);
            this.boxBasinsDataType.Name = "boxBasinsDataType";
            this.boxBasinsDataType.Size = new System.Drawing.Size(120, 94);
            this.boxBasinsDataType.TabIndex = 45;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.txtHUC8Basins);
            this.groupBox7.Controls.Add(this.btnRemoveBasins);
            this.groupBox7.Controls.Add(this.btnAddBasins);
            this.groupBox7.Controls.Add(this.linkLabel3);
            this.groupBox7.Controls.Add(this.listHuc8Basins);
            this.groupBox7.Location = new System.Drawing.Point(21, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(348, 130);
            this.groupBox7.TabIndex = 69;
            this.groupBox7.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Enter a HUC-8";
            // 
            // txtHUC8Basins
            // 
            this.txtHUC8Basins.Location = new System.Drawing.Point(103, 13);
            this.txtHUC8Basins.Name = "txtHUC8Basins";
            this.txtHUC8Basins.Size = new System.Drawing.Size(100, 20);
            this.txtHUC8Basins.TabIndex = 62;
            // 
            // btnRemoveBasins
            // 
            this.btnRemoveBasins.Location = new System.Drawing.Point(103, 69);
            this.btnRemoveBasins.Name = "btnRemoveBasins";
            this.btnRemoveBasins.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveBasins.TabIndex = 67;
            this.btnRemoveBasins.Text = "Remove Selected";
            this.btnRemoveBasins.UseVisualStyleBackColor = true;
            this.btnRemoveBasins.Click += new System.EventHandler(this.btnRemoveBasins_Click);
            // 
            // btnAddBasins
            // 
            this.btnAddBasins.Location = new System.Drawing.Point(103, 39);
            this.btnAddBasins.Name = "btnAddBasins";
            this.btnAddBasins.Size = new System.Drawing.Size(100, 23);
            this.btnAddBasins.TabIndex = 64;
            this.btnAddBasins.Text = "Add HUC-8 to List";
            this.btnAddBasins.UseVisualStyleBackColor = true;
            this.btnAddBasins.Click += new System.EventHandler(this.btnAddBasins_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(70, 95);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(130, 13);
            this.linkLabel3.TabIndex = 66;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Click here to find a HUC-8";
            this.linkLabel3.Click += new System.EventHandler(this.btnHUCfind_Click);
            // 
            // listHuc8Basins
            // 
            this.listHuc8Basins.FormattingEnabled = true;
            this.listHuc8Basins.Location = new System.Drawing.Point(230, 13);
            this.listHuc8Basins.Name = "listHuc8Basins";
            this.listHuc8Basins.Size = new System.Drawing.Size(91, 95);
            this.listHuc8Basins.TabIndex = 65;
            // 
            // labelBasins
            // 
            this.labelBasins.AutoSize = true;
            this.labelBasins.Location = new System.Drawing.Point(70, 322);
            this.labelBasins.Name = "labelBasins";
            this.labelBasins.Size = new System.Drawing.Size(35, 13);
            this.labelBasins.TabIndex = 68;
            this.labelBasins.Text = "label1";
            this.labelBasins.Visible = false;
            // 
            // btnRunBasins
            // 
            this.btnRunBasins.Location = new System.Drawing.Point(203, 280);
            this.btnRunBasins.Name = "btnRunBasins";
            this.btnRunBasins.Size = new System.Drawing.Size(139, 23);
            this.btnRunBasins.TabIndex = 0;
            this.btnRunBasins.Text = "Download";
            this.btnRunBasins.UseVisualStyleBackColor = true;
            this.btnRunBasins.Click += new System.EventHandler(this.btnRunBasins_Click);
            // 
            // tabUSGS_Seamless
            // 
            this.tabUSGS_Seamless.AllowDrop = true;
            this.tabUSGS_Seamless.Controls.Add(this.groupBox6);
            this.tabUSGS_Seamless.Controls.Add(this.groupBox5);
            this.tabUSGS_Seamless.Controls.Add(this.groupBox4);
            this.tabUSGS_Seamless.Controls.Add(this.labelUSGS_Seamless);
            this.tabUSGS_Seamless.Controls.Add(this.btnRunNLCD);
            this.tabUSGS_Seamless.Location = new System.Drawing.Point(4, 22);
            this.tabUSGS_Seamless.Name = "tabUSGS_Seamless";
            this.tabUSGS_Seamless.Padding = new System.Windows.Forms.Padding(3);
            this.tabUSGS_Seamless.Size = new System.Drawing.Size(892, 655);
            this.tabUSGS_Seamless.TabIndex = 0;
            this.tabUSGS_Seamless.Text = "USGS-Seamless";
            this.tabUSGS_Seamless.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label60);
            this.groupBox6.Controls.Add(this.txtCacheFolderUSGS_Seamless);
            this.groupBox6.Controls.Add(this.btnBrowseCacheNLCD);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.txtProjectFolderUSGS_Seamless);
            this.groupBox6.Controls.Add(this.btnBrowseProjectNLCD);
            this.groupBox6.Location = new System.Drawing.Point(74, 151);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(446, 97);
            this.groupBox6.TabIndex = 35;
            this.groupBox6.TabStop = false;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(9, 57);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(70, 13);
            this.label60.TabIndex = 33;
            this.label60.Text = "Cache Folder";
            // 
            // txtCacheFolderUSGS_Seamless
            // 
            this.txtCacheFolderUSGS_Seamless.Location = new System.Drawing.Point(87, 54);
            this.txtCacheFolderUSGS_Seamless.Name = "txtCacheFolderUSGS_Seamless";
            this.txtCacheFolderUSGS_Seamless.Size = new System.Drawing.Size(259, 20);
            this.txtCacheFolderUSGS_Seamless.TabIndex = 32;
            // 
            // btnBrowseCacheNLCD
            // 
            this.btnBrowseCacheNLCD.Location = new System.Drawing.Point(352, 54);
            this.btnBrowseCacheNLCD.Name = "btnBrowseCacheNLCD";
            this.btnBrowseCacheNLCD.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNLCD.TabIndex = 34;
            this.btnBrowseCacheNLCD.Text = "Browse";
            this.btnBrowseCacheNLCD.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNLCD.Click += new System.EventHandler(this.btnBrowseCacheNLCD_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Project Folder";
            // 
            // txtProjectFolderUSGS_Seamless
            // 
            this.txtProjectFolderUSGS_Seamless.Location = new System.Drawing.Point(87, 19);
            this.txtProjectFolderUSGS_Seamless.Name = "txtProjectFolderUSGS_Seamless";
            this.txtProjectFolderUSGS_Seamless.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderUSGS_Seamless.TabIndex = 17;
            // 
            // btnBrowseProjectNLCD
            // 
            this.btnBrowseProjectNLCD.Location = new System.Drawing.Point(352, 19);
            this.btnBrowseProjectNLCD.Name = "btnBrowseProjectNLCD";
            this.btnBrowseProjectNLCD.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectNLCD.TabIndex = 31;
            this.btnBrowseProjectNLCD.Text = "Browse";
            this.btnBrowseProjectNLCD.UseVisualStyleBackColor = true;
            this.btnBrowseProjectNLCD.Click += new System.EventHandler(this.btnBrowseProject_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.boxLayer);
            this.groupBox5.Location = new System.Drawing.Point(335, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(160, 131);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data Types";
            // 
            // boxLayer
            // 
            this.boxLayer.CheckOnClick = true;
            this.boxLayer.FormattingEnabled = true;
            this.boxLayer.Items.AddRange(new object[] {
            "Select All",
            "1992 LandCover",
            "2001 LandCover",
            "2001 Canopy",
            "2001 Impervious",
            "2006 LandCover",
            "2006 Impervious",
            "NED 1 ArcSecond",
            "NED 1/3 ArcSecond"});
            this.boxLayer.Location = new System.Drawing.Point(16, 19);
            this.boxLayer.Name = "boxLayer";
            this.boxLayer.Size = new System.Drawing.Size(120, 94);
            this.boxLayer.TabIndex = 29;
            this.boxLayer.SelectedIndexChanged += new System.EventHandler(this.boxLayer_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtNorthUSGS_Seamless);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtSouthUSGS_Seamless);
            this.groupBox4.Controls.Add(this.txtEastUSGS_Seamless);
            this.groupBox4.Controls.Add(this.txtWestUSGS_Seamless);
            this.groupBox4.Location = new System.Drawing.Point(55, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 131);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bounding Box";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "North";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "South";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "West";
            // 
            // txtNorthUSGS_Seamless
            // 
            this.txtNorthUSGS_Seamless.Location = new System.Drawing.Point(56, 23);
            this.txtNorthUSGS_Seamless.Name = "txtNorthUSGS_Seamless";
            this.txtNorthUSGS_Seamless.Size = new System.Drawing.Size(100, 20);
            this.txtNorthUSGS_Seamless.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "East";
            // 
            // txtSouthUSGS_Seamless
            // 
            this.txtSouthUSGS_Seamless.Location = new System.Drawing.Point(56, 46);
            this.txtSouthUSGS_Seamless.Name = "txtSouthUSGS_Seamless";
            this.txtSouthUSGS_Seamless.Size = new System.Drawing.Size(100, 20);
            this.txtSouthUSGS_Seamless.TabIndex = 23;
            // 
            // txtEastUSGS_Seamless
            // 
            this.txtEastUSGS_Seamless.Location = new System.Drawing.Point(56, 71);
            this.txtEastUSGS_Seamless.Name = "txtEastUSGS_Seamless";
            this.txtEastUSGS_Seamless.Size = new System.Drawing.Size(100, 20);
            this.txtEastUSGS_Seamless.TabIndex = 27;
            // 
            // txtWestUSGS_Seamless
            // 
            this.txtWestUSGS_Seamless.Location = new System.Drawing.Point(56, 94);
            this.txtWestUSGS_Seamless.Name = "txtWestUSGS_Seamless";
            this.txtWestUSGS_Seamless.Size = new System.Drawing.Size(100, 20);
            this.txtWestUSGS_Seamless.TabIndex = 25;
            // 
            // labelUSGS_Seamless
            // 
            this.labelUSGS_Seamless.AutoSize = true;
            this.labelUSGS_Seamless.Location = new System.Drawing.Point(70, 293);
            this.labelUSGS_Seamless.Name = "labelUSGS_Seamless";
            this.labelUSGS_Seamless.Size = new System.Drawing.Size(35, 13);
            this.labelUSGS_Seamless.TabIndex = 32;
            this.labelUSGS_Seamless.Text = "label1";
            this.labelUSGS_Seamless.Visible = false;
            // 
            // btnRunNLCD
            // 
            this.btnRunNLCD.Location = new System.Drawing.Point(161, 254);
            this.btnRunNLCD.Name = "btnRunNLCD";
            this.btnRunNLCD.Size = new System.Drawing.Size(157, 23);
            this.btnRunNLCD.TabIndex = 14;
            this.btnRunNLCD.Text = "Download";
            this.btnRunNLCD.UseVisualStyleBackColor = true;
            this.btnRunNLCD.Click += new System.EventHandler(this.btnRun_Click_1);
            // 
            // D4EMInterface
            // 
            this.D4EMInterface.Controls.Add(this.tabUSGS_Seamless);
            this.D4EMInterface.Controls.Add(this.tabPage2);
            this.D4EMInterface.Controls.Add(this.NHDPlus);
            this.D4EMInterface.Controls.Add(this.tabNWIS);
            this.D4EMInterface.Controls.Add(this.tabNRCSSOIL);
            this.D4EMInterface.Controls.Add(this.tabPage3);
            this.D4EMInterface.Controls.Add(this.tabNatureServe);
            this.D4EMInterface.Controls.Add(this.tabPage4);
            this.D4EMInterface.Controls.Add(this.tabPage7);
            this.D4EMInterface.Controls.Add(this.btnDownloadNASS);
            this.D4EMInterface.Controls.Add(this.tabPage8);
            this.D4EMInterface.Controls.Add(this.tabPage1);
            this.D4EMInterface.Controls.Add(this.tabPage9);
            this.D4EMInterface.Controls.Add(this.tabPage10);
            this.D4EMInterface.Controls.Add(this.tabPage11);
            this.D4EMInterface.Controls.Add(this.tabPage12);
            this.D4EMInterface.Location = new System.Drawing.Point(42, -2);
            this.D4EMInterface.Name = "D4EMInterface";
            this.D4EMInterface.SelectedIndex = 0;
            this.D4EMInterface.Size = new System.Drawing.Size(900, 681);
            this.D4EMInterface.TabIndex = 15;
            this.D4EMInterface.Tag = "";
            // 
            // tabNRCSSOIL
            // 
            this.tabNRCSSOIL.Controls.Add(this.groupBox38);
            this.tabNRCSSOIL.Controls.Add(this.groupBox19);
            this.tabNRCSSOIL.Controls.Add(this.btnRunNRCSSoil);
            this.tabNRCSSOIL.Location = new System.Drawing.Point(4, 22);
            this.tabNRCSSOIL.Name = "tabNRCSSOIL";
            this.tabNRCSSOIL.Padding = new System.Windows.Forms.Padding(3);
            this.tabNRCSSOIL.Size = new System.Drawing.Size(892, 655);
            this.tabNRCSSOIL.TabIndex = 5;
            this.tabNRCSSOIL.Text = "NRCS-SOIL";
            this.tabNRCSSOIL.UseVisualStyleBackColor = true;
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.label25);
            this.groupBox38.Controls.Add(this.label26);
            this.groupBox38.Controls.Add(this.label63);
            this.groupBox38.Controls.Add(this.txtNorth);
            this.groupBox38.Controls.Add(this.label68);
            this.groupBox38.Controls.Add(this.txtSouth);
            this.groupBox38.Controls.Add(this.txtEast);
            this.groupBox38.Controls.Add(this.txtWest);
            this.groupBox38.Location = new System.Drawing.Point(97, 18);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(177, 131);
            this.groupBox38.TabIndex = 73;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "Area of Interest Bounding Box";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 22;
            this.label25.Text = "North";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 49);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(35, 13);
            this.label26.TabIndex = 24;
            this.label26.Text = "South";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(18, 94);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(32, 13);
            this.label63.TabIndex = 26;
            this.label63.Text = "West";
            // 
            // txtNorth
            // 
            this.txtNorth.Location = new System.Drawing.Point(56, 23);
            this.txtNorth.Name = "txtNorth";
            this.txtNorth.Size = new System.Drawing.Size(100, 20);
            this.txtNorth.TabIndex = 21;
            this.txtNorth.MouseHover += new System.EventHandler(this.txtNorth_MouseHover);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(18, 74);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(28, 13);
            this.label68.TabIndex = 28;
            this.label68.Text = "East";
            // 
            // txtSouth
            // 
            this.txtSouth.Location = new System.Drawing.Point(56, 46);
            this.txtSouth.Name = "txtSouth";
            this.txtSouth.Size = new System.Drawing.Size(100, 20);
            this.txtSouth.TabIndex = 23;
            // 
            // txtEast
            // 
            this.txtEast.Location = new System.Drawing.Point(56, 71);
            this.txtEast.Name = "txtEast";
            this.txtEast.Size = new System.Drawing.Size(100, 20);
            this.txtEast.TabIndex = 27;
            // 
            // txtWest
            // 
            this.txtWest.Location = new System.Drawing.Point(56, 94);
            this.txtWest.Name = "txtWest";
            this.txtWest.Size = new System.Drawing.Size(100, 20);
            this.txtWest.TabIndex = 25;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.label64);
            this.groupBox19.Controls.Add(this.txtCacheNRCSSoil);
            this.groupBox19.Controls.Add(this.btnBrowseCacheNRCSSoil);
            this.groupBox19.Controls.Add(this.label33);
            this.groupBox19.Controls.Add(this.txtProjectFolderSoils);
            this.groupBox19.Controls.Add(this.btnBrowseSoils);
            this.groupBox19.Location = new System.Drawing.Point(97, 155);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(471, 90);
            this.groupBox19.TabIndex = 65;
            this.groupBox19.TabStop = false;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 54);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(70, 13);
            this.label64.TabIndex = 60;
            this.label64.Text = "Cache Folder";
            // 
            // txtCacheNRCSSoil
            // 
            this.txtCacheNRCSSoil.Location = new System.Drawing.Point(93, 54);
            this.txtCacheNRCSSoil.Name = "txtCacheNRCSSoil";
            this.txtCacheNRCSSoil.Size = new System.Drawing.Size(259, 20);
            this.txtCacheNRCSSoil.TabIndex = 59;
            // 
            // btnBrowseCacheNRCSSoil
            // 
            this.btnBrowseCacheNRCSSoil.Location = new System.Drawing.Point(378, 54);
            this.btnBrowseCacheNRCSSoil.Name = "btnBrowseCacheNRCSSoil";
            this.btnBrowseCacheNRCSSoil.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNRCSSoil.TabIndex = 61;
            this.btnBrowseCacheNRCSSoil.Text = "Browse";
            this.btnBrowseCacheNRCSSoil.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNRCSSoil.Click += new System.EventHandler(this.btnBrowseCacheNRCSSoil_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(72, 13);
            this.label33.TabIndex = 56;
            this.label33.Text = "Project Folder";
            // 
            // txtProjectFolderSoils
            // 
            this.txtProjectFolderSoils.Location = new System.Drawing.Point(93, 23);
            this.txtProjectFolderSoils.Name = "txtProjectFolderSoils";
            this.txtProjectFolderSoils.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderSoils.TabIndex = 55;
            // 
            // btnBrowseSoils
            // 
            this.btnBrowseSoils.Location = new System.Drawing.Point(378, 23);
            this.btnBrowseSoils.Name = "btnBrowseSoils";
            this.btnBrowseSoils.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSoils.TabIndex = 58;
            this.btnBrowseSoils.Text = "Browse";
            this.btnBrowseSoils.UseVisualStyleBackColor = true;
            this.btnBrowseSoils.Click += new System.EventHandler(this.btnBrowseSoils_Click);
            // 
            // btnRunNRCSSoil
            // 
            this.btnRunNRCSSoil.Location = new System.Drawing.Point(445, 126);
            this.btnRunNRCSSoil.Name = "btnRunNRCSSoil";
            this.btnRunNRCSSoil.Size = new System.Drawing.Size(123, 23);
            this.btnRunNRCSSoil.TabIndex = 0;
            this.btnRunNRCSSoil.Text = "Download";
            this.btnRunNRCSSoil.UseVisualStyleBackColor = true;
            this.btnRunNRCSSoil.Click += new System.EventHandler(this.btnRunNRCSSoil_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataset);
            this.tabPage3.Controls.Add(this.groupBoxNCDCButtons);
            this.tabPage3.Controls.Add(this.groupBox26);
            this.tabPage3.Controls.Add(this.groupBox25);
            this.tabPage3.Controls.Add(this.groupBox24);
            this.tabPage3.Controls.Add(this.groupBox23);
            this.tabPage3.Controls.Add(this.groupBox22);
            this.tabPage3.Controls.Add(this.groupBox21);
            this.tabPage3.Controls.Add(this.labelNCDC);
            this.tabPage3.Controls.Add(this.groupBox20);
            this.tabPage3.Controls.Add(this.groupBox17);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(892, 655);
            this.tabPage3.TabIndex = 7;
            this.tabPage3.Text = "NCDC";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataset
            // 
            this.dataset.Controls.Add(this.datasetType);
            this.dataset.Location = new System.Drawing.Point(534, 46);
            this.dataset.Name = "dataset";
            this.dataset.Size = new System.Drawing.Size(103, 82);
            this.dataset.TabIndex = 92;
            this.dataset.TabStop = false;
            this.dataset.Text = "Dataset Types";
            // 
            // datasetType
            // 
            this.datasetType.CheckOnClick = true;
            this.datasetType.FormattingEnabled = true;
            this.datasetType.Items.AddRange(new object[] {
            "daily",
            "ish",
            "isd"});
            this.datasetType.Location = new System.Drawing.Point(6, 19);
            this.datasetType.Name = "datasetType";
            this.datasetType.Size = new System.Drawing.Size(73, 49);
            this.datasetType.TabIndex = 63;
            // 
            // groupBoxNCDCButtons
            // 
            this.groupBoxNCDCButtons.Controls.Add(this.btnDownloadNCDC);
            this.groupBoxNCDCButtons.Controls.Add(this.btnDownloadforSelectedStation);
            this.groupBoxNCDCButtons.Controls.Add(this.label47);
            this.groupBoxNCDCButtons.Location = new System.Drawing.Point(37, 372);
            this.groupBoxNCDCButtons.Name = "groupBoxNCDCButtons";
            this.groupBoxNCDCButtons.Size = new System.Drawing.Size(575, 104);
            this.groupBoxNCDCButtons.TabIndex = 78;
            this.groupBoxNCDCButtons.TabStop = false;
            // 
            // btnDownloadNCDC
            // 
            this.btnDownloadNCDC.Location = new System.Drawing.Point(15, 19);
            this.btnDownloadNCDC.Name = "btnDownloadNCDC";
            this.btnDownloadNCDC.Size = new System.Drawing.Size(536, 23);
            this.btnDownloadNCDC.TabIndex = 54;
            this.btnDownloadNCDC.Text = "Download NCDC Stations and Load Variables";
            this.btnDownloadNCDC.UseVisualStyleBackColor = true;
            this.btnDownloadNCDC.Click += new System.EventHandler(this.btnDownloadNCDC_Click);
            // 
            // btnDownloadforSelectedStation
            // 
            this.btnDownloadforSelectedStation.Location = new System.Drawing.Point(15, 48);
            this.btnDownloadforSelectedStation.Name = "btnDownloadforSelectedStation";
            this.btnDownloadforSelectedStation.Size = new System.Drawing.Size(536, 23);
            this.btnDownloadforSelectedStation.TabIndex = 56;
            this.btnDownloadforSelectedStation.Text = "Download Data for Selected Station/Variable";
            this.btnDownloadforSelectedStation.UseVisualStyleBackColor = true;
            this.btnDownloadforSelectedStation.Click += new System.EventHandler(this.btnDownloadforSelectedStation_Click);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ForeColor = System.Drawing.Color.Red;
            this.label47.Location = new System.Drawing.Point(139, 74);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(261, 13);
            this.label47.TabIndex = 71;
            this.label47.Text = "Note:  You must wait 60 seconds between downloads";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.dataVariablesNCDC);
            this.groupBox26.Location = new System.Drawing.Point(311, 226);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(246, 124);
            this.groupBox26.TabIndex = 56;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Variables";
            // 
            // dataVariablesNCDC
            // 
            this.dataVariablesNCDC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataVariablesNCDC.Location = new System.Drawing.Point(6, 14);
            this.dataVariablesNCDC.Name = "dataVariablesNCDC";
            this.dataVariablesNCDC.Size = new System.Drawing.Size(223, 104);
            this.dataVariablesNCDC.TabIndex = 57;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.dataStationsNCDC);
            this.groupBox25.Location = new System.Drawing.Point(38, 226);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(253, 124);
            this.groupBox25.TabIndex = 77;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Stations";
            // 
            // dataStationsNCDC
            // 
            this.dataStationsNCDC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataStationsNCDC.Location = new System.Drawing.Point(16, 14);
            this.dataStationsNCDC.Name = "dataStationsNCDC";
            this.dataStationsNCDC.Size = new System.Drawing.Size(220, 104);
            this.dataStationsNCDC.TabIndex = 55;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.label66);
            this.groupBox24.Controls.Add(this.txtCacheNCDC);
            this.groupBox24.Controls.Add(this.btnBrowseCacheNCDC);
            this.groupBox24.Controls.Add(this.label65);
            this.groupBox24.Controls.Add(this.txtProjectFolderNCDC);
            this.groupBox24.Controls.Add(this.btnBrowseNCDC);
            this.groupBox24.Location = new System.Drawing.Point(56, 135);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(581, 72);
            this.groupBox24.TabIndex = 76;
            this.groupBox24.TabStop = false;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(3, 46);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(70, 13);
            this.label66.TabIndex = 74;
            this.label66.Text = "Cache Folder";
            // 
            // txtCacheNCDC
            // 
            this.txtCacheNCDC.Location = new System.Drawing.Point(108, 44);
            this.txtCacheNCDC.Name = "txtCacheNCDC";
            this.txtCacheNCDC.Size = new System.Drawing.Size(304, 20);
            this.txtCacheNCDC.TabIndex = 72;
            // 
            // btnBrowseCacheNCDC
            // 
            this.btnBrowseCacheNCDC.Location = new System.Drawing.Point(426, 41);
            this.btnBrowseCacheNCDC.Name = "btnBrowseCacheNCDC";
            this.btnBrowseCacheNCDC.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNCDC.TabIndex = 73;
            this.btnBrowseCacheNCDC.Text = "Browse";
            this.btnBrowseCacheNCDC.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNCDC.Click += new System.EventHandler(this.btnBrowseCacheNCDC_Click);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(3, 16);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(72, 13);
            this.label65.TabIndex = 71;
            this.label65.Text = "Project Folder";
            // 
            // txtProjectFolderNCDC
            // 
            this.txtProjectFolderNCDC.Location = new System.Drawing.Point(108, 14);
            this.txtProjectFolderNCDC.Name = "txtProjectFolderNCDC";
            this.txtProjectFolderNCDC.Size = new System.Drawing.Size(304, 20);
            this.txtProjectFolderNCDC.TabIndex = 66;
            // 
            // btnBrowseNCDC
            // 
            this.btnBrowseNCDC.Location = new System.Drawing.Point(426, 11);
            this.btnBrowseNCDC.Name = "btnBrowseNCDC";
            this.btnBrowseNCDC.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNCDC.TabIndex = 70;
            this.btnBrowseNCDC.Text = "Browse";
            this.btnBrowseNCDC.UseVisualStyleBackColor = true;
            this.btnBrowseNCDC.Click += new System.EventHandler(this.btnBrowseNCDC_Click);
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.outputType);
            this.groupBox23.Location = new System.Drawing.Point(416, 47);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(103, 82);
            this.groupBox23.TabIndex = 75;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Output Types";
            // 
            // outputType
            // 
            this.outputType.CheckOnClick = true;
            this.outputType.FormattingEnabled = true;
            this.outputType.Items.AddRange(new object[] {
            "csv",
            "waterml",
            "xml"});
            this.outputType.Location = new System.Drawing.Point(6, 19);
            this.outputType.Name = "outputType";
            this.outputType.Size = new System.Drawing.Size(73, 49);
            this.outputType.TabIndex = 63;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.listStatesNCDC);
            this.groupBox22.Location = new System.Drawing.Point(292, 42);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(89, 87);
            this.groupBox22.TabIndex = 74;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "States";
            // 
            // listStatesNCDC
            // 
            this.listStatesNCDC.CheckOnClick = true;
            this.listStatesNCDC.FormattingEnabled = true;
            this.listStatesNCDC.Items.AddRange(new object[] {
            "AK",
            "AL",
            "AR",
            "AZ",
            "CA",
            "CO",
            "CT",
            "DC",
            "DE",
            "FL",
            "GA",
            "HI",
            "IA",
            "ID",
            "IL",
            "IN",
            "KS",
            "KY",
            "LA",
            "MA",
            "MD",
            "ME",
            "MI",
            "MN",
            "MO",
            "MS",
            "MT",
            "NC",
            "ND",
            "NE",
            "NH",
            "NJ",
            "NM",
            "NV",
            "NY",
            "OH",
            "OK",
            "OR",
            "PA",
            "PR",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VA",
            "VT",
            "WA",
            "WI",
            "WV",
            "WY"});
            this.listStatesNCDC.Location = new System.Drawing.Point(6, 19);
            this.listStatesNCDC.Name = "listStatesNCDC";
            this.listStatesNCDC.Size = new System.Drawing.Size(76, 64);
            this.listStatesNCDC.TabIndex = 68;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.txtToken);
            this.groupBox21.Controls.Add(this.linkLabel4);
            this.groupBox21.Location = new System.Drawing.Point(283, 3);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(307, 37);
            this.groupBox21.TabIndex = 73;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Token";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(53, 13);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(176, 20);
            this.txtToken.TabIndex = 59;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(235, 16);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(67, 13);
            this.linkLabel4.TabIndex = 69;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Get a Token";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            this.linkLabel4.Click += new System.EventHandler(this.btnFindToken);
            // 
            // labelNCDC
            // 
            this.labelNCDC.AutoSize = true;
            this.labelNCDC.Location = new System.Drawing.Point(53, 356);
            this.labelNCDC.Name = "labelNCDC";
            this.labelNCDC.Size = new System.Drawing.Size(41, 13);
            this.labelNCDC.TabIndex = 72;
            this.labelNCDC.Text = "label48";
            this.labelNCDC.Visible = false;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label42);
            this.groupBox20.Controls.Add(this.label43);
            this.groupBox20.Controls.Add(this.label44);
            this.groupBox20.Controls.Add(this.label45);
            this.groupBox20.Controls.Add(this.txtEndDay);
            this.groupBox20.Controls.Add(this.txtEndMonth);
            this.groupBox20.Controls.Add(this.txtEndYear);
            this.groupBox20.Location = new System.Drawing.Point(164, 6);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(104, 102);
            this.groupBox20.TabIndex = 65;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "End Date";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 70);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(15, 13);
            this.label42.TabIndex = 6;
            this.label42.Text = "D";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(-20, 67);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(15, 13);
            this.label43.TabIndex = 5;
            this.label43.Text = "D";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 44);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(16, 13);
            this.label44.TabIndex = 4;
            this.label44.Text = "M";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 18);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(14, 13);
            this.label45.TabIndex = 3;
            this.label45.Text = "Y";
            // 
            // txtEndDay
            // 
            this.txtEndDay.Location = new System.Drawing.Point(26, 67);
            this.txtEndDay.Name = "txtEndDay";
            this.txtEndDay.Size = new System.Drawing.Size(62, 20);
            this.txtEndDay.TabIndex = 2;
            // 
            // txtEndMonth
            // 
            this.txtEndMonth.Location = new System.Drawing.Point(26, 41);
            this.txtEndMonth.Name = "txtEndMonth";
            this.txtEndMonth.Size = new System.Drawing.Size(62, 20);
            this.txtEndMonth.TabIndex = 1;
            // 
            // txtEndYear
            // 
            this.txtEndYear.Location = new System.Drawing.Point(26, 14);
            this.txtEndYear.Name = "txtEndYear";
            this.txtEndYear.Size = new System.Drawing.Size(62, 20);
            this.txtEndYear.TabIndex = 0;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label41);
            this.groupBox17.Controls.Add(this.label40);
            this.groupBox17.Controls.Add(this.label39);
            this.groupBox17.Controls.Add(this.label38);
            this.groupBox17.Controls.Add(this.txtStartDay);
            this.groupBox17.Controls.Add(this.txtStartMonth);
            this.groupBox17.Controls.Add(this.txtStartYear);
            this.groupBox17.Location = new System.Drawing.Point(28, 6);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(104, 102);
            this.groupBox17.TabIndex = 64;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Start Date";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(7, 70);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(15, 13);
            this.label41.TabIndex = 6;
            this.label41.Text = "D";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(-20, 67);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(15, 13);
            this.label40.TabIndex = 5;
            this.label40.Text = "D";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 44);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(16, 13);
            this.label39.TabIndex = 4;
            this.label39.Text = "M";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 18);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(14, 13);
            this.label38.TabIndex = 3;
            this.label38.Text = "Y";
            // 
            // txtStartDay
            // 
            this.txtStartDay.Location = new System.Drawing.Point(26, 67);
            this.txtStartDay.Name = "txtStartDay";
            this.txtStartDay.Size = new System.Drawing.Size(62, 20);
            this.txtStartDay.TabIndex = 2;
            // 
            // txtStartMonth
            // 
            this.txtStartMonth.Location = new System.Drawing.Point(26, 41);
            this.txtStartMonth.Name = "txtStartMonth";
            this.txtStartMonth.Size = new System.Drawing.Size(62, 20);
            this.txtStartMonth.TabIndex = 1;
            // 
            // txtStartYear
            // 
            this.txtStartYear.Location = new System.Drawing.Point(26, 14);
            this.txtStartYear.Name = "txtStartYear";
            this.txtStartYear.Size = new System.Drawing.Size(62, 20);
            this.txtStartYear.TabIndex = 0;
            // 
            // tabNatureServe
            // 
            this.tabNatureServe.Controls.Add(this.tabControl1);
            this.tabNatureServe.Location = new System.Drawing.Point(4, 22);
            this.tabNatureServe.Name = "tabNatureServe";
            this.tabNatureServe.Padding = new System.Windows.Forms.Padding(3);
            this.tabNatureServe.Size = new System.Drawing.Size(892, 655);
            this.tabNatureServe.TabIndex = 8;
            this.tabNatureServe.Text = "NatureServe";
            this.tabNatureServe.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(886, 649);
            this.tabControl1.TabIndex = 64;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox14);
            this.tabPage5.Controls.Add(this.btnDownloadNatureServe);
            this.tabPage5.Controls.Add(this.labelNatureServe);
            this.tabPage5.Controls.Add(this.groupBox15);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(878, 623);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Pollinators";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.listPollinator);
            this.groupBox14.Location = new System.Drawing.Point(137, 16);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(297, 109);
            this.groupBox14.TabIndex = 60;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "NatureServe Data Types";
            // 
            // listPollinator
            // 
            this.listPollinator.CheckOnClick = true;
            this.listPollinator.FormattingEnabled = true;
            this.listPollinator.Items.AddRange(new object[] {
            "Anna\'s Hummingbird (Calypte anna)",
            "Eastern Tiger Swallowtail (Papilio glaucus)",
            "Hermit Sphinx (Lintneria eremitus)",
            "Rusty-patched Bumble Bee (Bombus affinis)",
            "Southeastern Blueberry Bee (Habropoda laboriosa)"});
            this.listPollinator.Location = new System.Drawing.Point(10, 24);
            this.listPollinator.Name = "listPollinator";
            this.listPollinator.Size = new System.Drawing.Size(268, 79);
            this.listPollinator.TabIndex = 1;
            // 
            // btnDownloadNatureServe
            // 
            this.btnDownloadNatureServe.Location = new System.Drawing.Point(177, 239);
            this.btnDownloadNatureServe.Name = "btnDownloadNatureServe";
            this.btnDownloadNatureServe.Size = new System.Drawing.Size(184, 28);
            this.btnDownloadNatureServe.TabIndex = 0;
            this.btnDownloadNatureServe.Text = "Download";
            this.btnDownloadNatureServe.UseVisualStyleBackColor = true;
            this.btnDownloadNatureServe.Click += new System.EventHandler(this.btnDownloadNatureServe_Click);
            // 
            // labelNatureServe
            // 
            this.labelNatureServe.AutoSize = true;
            this.labelNatureServe.Location = new System.Drawing.Point(163, 281);
            this.labelNatureServe.Name = "labelNatureServe";
            this.labelNatureServe.Size = new System.Drawing.Size(41, 13);
            this.labelNatureServe.TabIndex = 59;
            this.labelNatureServe.Text = "label26";
            this.labelNatureServe.Visible = false;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label67);
            this.groupBox15.Controls.Add(this.txtCacheNatureServe);
            this.groupBox15.Controls.Add(this.btnBrowseCacheNatureServe);
            this.groupBox15.Controls.Add(this.label20);
            this.groupBox15.Controls.Add(this.txtProjectFolderNatureServe);
            this.groupBox15.Controls.Add(this.btnBrowseNatureServe);
            this.groupBox15.Location = new System.Drawing.Point(39, 141);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(510, 92);
            this.groupBox15.TabIndex = 61;
            this.groupBox15.TabStop = false;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(32, 51);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(70, 13);
            this.label67.TabIndex = 60;
            this.label67.Text = "Cache Folder";
            // 
            // txtCacheNatureServe
            // 
            this.txtCacheNatureServe.Location = new System.Drawing.Point(108, 51);
            this.txtCacheNatureServe.Name = "txtCacheNatureServe";
            this.txtCacheNatureServe.Size = new System.Drawing.Size(259, 20);
            this.txtCacheNatureServe.TabIndex = 59;
            // 
            // btnBrowseCacheNatureServe
            // 
            this.btnBrowseCacheNatureServe.Location = new System.Drawing.Point(373, 51);
            this.btnBrowseCacheNatureServe.Name = "btnBrowseCacheNatureServe";
            this.btnBrowseCacheNatureServe.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNatureServe.TabIndex = 61;
            this.btnBrowseCacheNatureServe.Text = "Browse";
            this.btnBrowseCacheNatureServe.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNatureServe.Click += new System.EventHandler(this.btnBrowseCacheNatureServe_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(32, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 13);
            this.label20.TabIndex = 57;
            this.label20.Text = "Project Folder";
            // 
            // txtProjectFolderNatureServe
            // 
            this.txtProjectFolderNatureServe.Location = new System.Drawing.Point(108, 16);
            this.txtProjectFolderNatureServe.Name = "txtProjectFolderNatureServe";
            this.txtProjectFolderNatureServe.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNatureServe.TabIndex = 56;
            // 
            // btnBrowseNatureServe
            // 
            this.btnBrowseNatureServe.Location = new System.Drawing.Point(373, 16);
            this.btnBrowseNatureServe.Name = "btnBrowseNatureServe";
            this.btnBrowseNatureServe.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNatureServe.TabIndex = 58;
            this.btnBrowseNatureServe.Text = "Browse";
            this.btnBrowseNatureServe.UseVisualStyleBackColor = true;
            this.btnBrowseNatureServe.Click += new System.EventHandler(this.btnBrowseNatureServe_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnCreateHUCNativeFishSpeciesFile);
            this.tabPage6.Controls.Add(this.dataGridViewNatureServe);
            this.tabPage6.Controls.Add(this.btnPopulateNativeSpeciesTable);
            this.tabPage6.Controls.Add(this.txtHUC8natureServe);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(878, 623);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Native Species";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnCreateHUCNativeFishSpeciesFile
            // 
            this.btnCreateHUCNativeFishSpeciesFile.Location = new System.Drawing.Point(354, 270);
            this.btnCreateHUCNativeFishSpeciesFile.Name = "btnCreateHUCNativeFishSpeciesFile";
            this.btnCreateHUCNativeFishSpeciesFile.Size = new System.Drawing.Size(194, 23);
            this.btnCreateHUCNativeFishSpeciesFile.TabIndex = 65;
            this.btnCreateHUCNativeFishSpeciesFile.Text = "button1";
            this.btnCreateHUCNativeFishSpeciesFile.UseVisualStyleBackColor = true;
            this.btnCreateHUCNativeFishSpeciesFile.Click += new System.EventHandler(this.btnCreateHUCNativeFishSpeciesFile_Click);
            // 
            // dataGridViewNatureServe
            // 
            this.dataGridViewNatureServe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNatureServe.Location = new System.Drawing.Point(88, 81);
            this.dataGridViewNatureServe.Name = "dataGridViewNatureServe";
            this.dataGridViewNatureServe.Size = new System.Drawing.Size(399, 150);
            this.dataGridViewNatureServe.TabIndex = 64;
            // 
            // btnPopulateNativeSpeciesTable
            // 
            this.btnPopulateNativeSpeciesTable.Location = new System.Drawing.Point(177, 37);
            this.btnPopulateNativeSpeciesTable.Name = "btnPopulateNativeSpeciesTable";
            this.btnPopulateNativeSpeciesTable.Size = new System.Drawing.Size(310, 23);
            this.btnPopulateNativeSpeciesTable.TabIndex = 62;
            this.btnPopulateNativeSpeciesTable.Text = "List Native Species in HUC-8";
            this.btnPopulateNativeSpeciesTable.UseVisualStyleBackColor = true;
            this.btnPopulateNativeSpeciesTable.Click += new System.EventHandler(this.btnDownloadFishByHUC_Click);
            // 
            // txtHUC8natureServe
            // 
            this.txtHUC8natureServe.Location = new System.Drawing.Point(49, 37);
            this.txtHUC8natureServe.Name = "txtHUC8natureServe";
            this.txtHUC8natureServe.Size = new System.Drawing.Size(101, 20);
            this.txtHUC8natureServe.TabIndex = 63;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listStoretDataTypes);
            this.tabPage4.Controls.Add(this.labelStoret);
            this.tabPage4.Controls.Add(this.groupBox18);
            this.tabPage4.Controls.Add(this.groupBox16);
            this.tabPage4.Controls.Add(this.btnDownloadStoret);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(892, 655);
            this.tabPage4.TabIndex = 10;
            this.tabPage4.Text = "Storet";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listStoretDataTypes
            // 
            this.listStoretDataTypes.CheckOnClick = true;
            this.listStoretDataTypes.FormattingEnabled = true;
            this.listStoretDataTypes.Items.AddRange(new object[] {
            "Temperature, water",
            "pH",
            "Organic carbon",
            "Dissolved oxygen (DO)"});
            this.listStoretDataTypes.Location = new System.Drawing.Point(111, 374);
            this.listStoretDataTypes.Name = "listStoretDataTypes";
            this.listStoretDataTypes.Size = new System.Drawing.Size(371, 79);
            this.listStoretDataTypes.TabIndex = 66;
            // 
            // labelStoret
            // 
            this.labelStoret.AutoSize = true;
            this.labelStoret.Location = new System.Drawing.Point(122, 306);
            this.labelStoret.Name = "labelStoret";
            this.labelStoret.Size = new System.Drawing.Size(41, 13);
            this.labelStoret.TabIndex = 37;
            this.labelStoret.Text = "label60";
            this.labelStoret.Visible = false;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label32);
            this.groupBox18.Controls.Add(this.txtProjectFolderStoret);
            this.groupBox18.Controls.Add(this.btnBrowseStoret);
            this.groupBox18.Location = new System.Drawing.Point(66, 176);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(446, 66);
            this.groupBox18.TabIndex = 36;
            this.groupBox18.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(11, 19);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(72, 13);
            this.label32.TabIndex = 18;
            this.label32.Text = "Project Folder";
            // 
            // txtProjectFolderStoret
            // 
            this.txtProjectFolderStoret.Location = new System.Drawing.Point(87, 19);
            this.txtProjectFolderStoret.Name = "txtProjectFolderStoret";
            this.txtProjectFolderStoret.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderStoret.TabIndex = 17;
            // 
            // btnBrowseStoret
            // 
            this.btnBrowseStoret.Location = new System.Drawing.Point(352, 19);
            this.btnBrowseStoret.Name = "btnBrowseStoret";
            this.btnBrowseStoret.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseStoret.TabIndex = 31;
            this.btnBrowseStoret.Text = "Browse";
            this.btnBrowseStoret.UseVisualStyleBackColor = true;
            this.btnBrowseStoret.Click += new System.EventHandler(this.btnBrowseStoret_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label27);
            this.groupBox16.Controls.Add(this.label28);
            this.groupBox16.Controls.Add(this.label29);
            this.groupBox16.Controls.Add(this.txtNorthStoret);
            this.groupBox16.Controls.Add(this.label30);
            this.groupBox16.Controls.Add(this.txtSouthStoret);
            this.groupBox16.Controls.Add(this.txtEastStoret);
            this.groupBox16.Controls.Add(this.txtWestStoret);
            this.groupBox16.Location = new System.Drawing.Point(55, 29);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(177, 131);
            this.groupBox16.TabIndex = 34;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Storet Bounding Box";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(33, 13);
            this.label27.TabIndex = 22;
            this.label27.Text = "North";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(18, 49);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 13);
            this.label28.TabIndex = 24;
            this.label28.Text = "South";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(18, 94);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(32, 13);
            this.label29.TabIndex = 26;
            this.label29.Text = "West";
            // 
            // txtNorthStoret
            // 
            this.txtNorthStoret.Location = new System.Drawing.Point(56, 23);
            this.txtNorthStoret.Name = "txtNorthStoret";
            this.txtNorthStoret.Size = new System.Drawing.Size(100, 20);
            this.txtNorthStoret.TabIndex = 21;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(18, 74);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(28, 13);
            this.label30.TabIndex = 28;
            this.label30.Text = "East";
            // 
            // txtSouthStoret
            // 
            this.txtSouthStoret.Location = new System.Drawing.Point(56, 46);
            this.txtSouthStoret.Name = "txtSouthStoret";
            this.txtSouthStoret.Size = new System.Drawing.Size(100, 20);
            this.txtSouthStoret.TabIndex = 23;
            // 
            // txtEastStoret
            // 
            this.txtEastStoret.Location = new System.Drawing.Point(56, 71);
            this.txtEastStoret.Name = "txtEastStoret";
            this.txtEastStoret.Size = new System.Drawing.Size(100, 20);
            this.txtEastStoret.TabIndex = 27;
            // 
            // txtWestStoret
            // 
            this.txtWestStoret.Location = new System.Drawing.Point(56, 94);
            this.txtWestStoret.Name = "txtWestStoret";
            this.txtWestStoret.Size = new System.Drawing.Size(100, 20);
            this.txtWestStoret.TabIndex = 25;
            // 
            // btnDownloadStoret
            // 
            this.btnDownloadStoret.Location = new System.Drawing.Point(205, 276);
            this.btnDownloadStoret.Name = "btnDownloadStoret";
            this.btnDownloadStoret.Size = new System.Drawing.Size(158, 23);
            this.btnDownloadStoret.TabIndex = 0;
            this.btnDownloadStoret.Text = "Download";
            this.btnDownloadStoret.UseVisualStyleBackColor = true;
            this.btnDownloadStoret.Click += new System.EventHandler(this.btnDownloadStoret_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.labelWDNRStatewide);
            this.tabPage7.Controls.Add(this.groupBox30);
            this.tabPage7.Controls.Add(this.groupBox31);
            this.tabPage7.Controls.Add(this.groupBox29);
            this.tabPage7.Controls.Add(this.groupBox28);
            this.tabPage7.Controls.Add(this.groupBox27);
            this.tabPage7.Controls.Add(this.btnGetSpreadsheet);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(892, 655);
            this.tabPage7.TabIndex = 11;
            this.tabPage7.Text = "WDNR";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // labelWDNRStatewide
            // 
            this.labelWDNRStatewide.AutoSize = true;
            this.labelWDNRStatewide.Location = new System.Drawing.Point(590, 67);
            this.labelWDNRStatewide.Name = "labelWDNRStatewide";
            this.labelWDNRStatewide.Size = new System.Drawing.Size(41, 13);
            this.labelWDNRStatewide.TabIndex = 68;
            this.labelWDNRStatewide.Text = "label56";
            this.labelWDNRStatewide.Visible = false;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.btnSelectAllHuc12);
            this.groupBox30.Controls.Add(this.labelWDNRHUC12);
            this.groupBox30.Controls.Add(this.btnAddHUC8Huc12);
            this.groupBox30.Controls.Add(this.btnRemoveHuc8Huc12);
            this.groupBox30.Controls.Add(this.linkLabel6);
            this.groupBox30.Controls.Add(this.listHUC8huc12);
            this.groupBox30.Controls.Add(this.label50);
            this.groupBox30.Controls.Add(this.txtHuc8Huc12WDNR);
            this.groupBox30.Controls.Add(this.btnGetDataWithinHuc12);
            this.groupBox30.Controls.Add(this.listHuc12WDNR);
            this.groupBox30.Controls.Add(this.btnGetHuc12WithinHuc8);
            this.groupBox30.Location = new System.Drawing.Point(23, 273);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(617, 141);
            this.groupBox30.TabIndex = 67;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "HUC-12";
            // 
            // btnSelectAllHuc12
            // 
            this.btnSelectAllHuc12.Location = new System.Drawing.Point(552, 19);
            this.btnSelectAllHuc12.Name = "btnSelectAllHuc12";
            this.btnSelectAllHuc12.Size = new System.Drawing.Size(49, 56);
            this.btnSelectAllHuc12.TabIndex = 67;
            this.btnSelectAllHuc12.Text = "Select All";
            this.btnSelectAllHuc12.UseVisualStyleBackColor = true;
            this.btnSelectAllHuc12.Click += new System.EventHandler(this.btnSelectAllHuc12_Click);
            // 
            // labelWDNRHUC12
            // 
            this.labelWDNRHUC12.AutoSize = true;
            this.labelWDNRHUC12.Location = new System.Drawing.Point(6, 125);
            this.labelWDNRHUC12.Name = "labelWDNRHUC12";
            this.labelWDNRHUC12.Size = new System.Drawing.Size(41, 13);
            this.labelWDNRHUC12.TabIndex = 65;
            this.labelWDNRHUC12.Text = "label56";
            this.labelWDNRHUC12.Visible = false;
            // 
            // btnAddHUC8Huc12
            // 
            this.btnAddHUC8Huc12.Location = new System.Drawing.Point(77, 40);
            this.btnAddHUC8Huc12.Name = "btnAddHUC8Huc12";
            this.btnAddHUC8Huc12.Size = new System.Drawing.Size(100, 23);
            this.btnAddHUC8Huc12.TabIndex = 62;
            this.btnAddHUC8Huc12.Text = "Add HUC-8 to List";
            this.btnAddHUC8Huc12.UseVisualStyleBackColor = true;
            this.btnAddHUC8Huc12.Click += new System.EventHandler(this.btnAddHUC8Huc12_Click);
            // 
            // btnRemoveHuc8Huc12
            // 
            this.btnRemoveHuc8Huc12.Location = new System.Drawing.Point(77, 69);
            this.btnRemoveHuc8Huc12.Name = "btnRemoveHuc8Huc12";
            this.btnRemoveHuc8Huc12.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveHuc8Huc12.TabIndex = 64;
            this.btnRemoveHuc8Huc12.Text = "Remove Selected";
            this.btnRemoveHuc8Huc12.UseVisualStyleBackColor = true;
            this.btnRemoveHuc8Huc12.Click += new System.EventHandler(this.btnRemoveHuc8Huc12_Click);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(45, 95);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(130, 13);
            this.linkLabel6.TabIndex = 63;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Click here to find a HUC-8";
            this.linkLabel6.Click += new System.EventHandler(this.btnFindHuc8_Click);
            // 
            // listHUC8huc12
            // 
            this.listHUC8huc12.FormattingEnabled = true;
            this.listHUC8huc12.Location = new System.Drawing.Point(183, 13);
            this.listHUC8huc12.Name = "listHUC8huc12";
            this.listHUC8huc12.Size = new System.Drawing.Size(86, 95);
            this.listHUC8huc12.TabIndex = 60;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 16);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(76, 13);
            this.label50.TabIndex = 51;
            this.label50.Text = "Enter a HUC-8";
            // 
            // txtHuc8Huc12WDNR
            // 
            this.txtHuc8Huc12WDNR.Location = new System.Drawing.Point(83, 13);
            this.txtHuc8Huc12WDNR.Name = "txtHuc8Huc12WDNR";
            this.txtHuc8Huc12WDNR.Size = new System.Drawing.Size(94, 20);
            this.txtHuc8Huc12WDNR.TabIndex = 50;
            // 
            // btnGetDataWithinHuc12
            // 
            this.btnGetDataWithinHuc12.Location = new System.Drawing.Point(396, 81);
            this.btnGetDataWithinHuc12.Name = "btnGetDataWithinHuc12";
            this.btnGetDataWithinHuc12.Size = new System.Drawing.Size(205, 23);
            this.btnGetDataWithinHuc12.TabIndex = 2;
            this.btnGetDataWithinHuc12.Text = "Get Data Within Selected HUC-12\'s";
            this.btnGetDataWithinHuc12.UseVisualStyleBackColor = true;
            this.btnGetDataWithinHuc12.Click += new System.EventHandler(this.btnGetDataWithinHuc12_Click);
            // 
            // listHuc12WDNR
            // 
            this.listHuc12WDNR.FormattingEnabled = true;
            this.listHuc12WDNR.Location = new System.Drawing.Point(396, 19);
            this.listHuc12WDNR.Name = "listHuc12WDNR";
            this.listHuc12WDNR.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listHuc12WDNR.Size = new System.Drawing.Size(151, 56);
            this.listHuc12WDNR.TabIndex = 1;
            // 
            // btnGetHuc12WithinHuc8
            // 
            this.btnGetHuc12WithinHuc8.Location = new System.Drawing.Point(299, 19);
            this.btnGetHuc12WithinHuc8.Name = "btnGetHuc12WithinHuc8";
            this.btnGetHuc12WithinHuc8.Size = new System.Drawing.Size(63, 89);
            this.btnGetHuc12WithinHuc8.TabIndex = 0;
            this.btnGetHuc12WithinHuc8.Text = "Get HUC-12\'s within HUC-8";
            this.btnGetHuc12WithinHuc8.UseVisualStyleBackColor = true;
            this.btnGetHuc12WithinHuc8.Click += new System.EventHandler(this.btnGetHuc12WithinHuc8_Click);
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.labelWDNRHUC8);
            this.groupBox31.Controls.Add(this.label49);
            this.groupBox31.Controls.Add(this.btnGetDataWithinHuc);
            this.groupBox31.Controls.Add(this.txtHucWDNR);
            this.groupBox31.Controls.Add(this.btnAddHucWDNR);
            this.groupBox31.Controls.Add(this.btnRemoveHucWDNR);
            this.groupBox31.Controls.Add(this.listHucWDNR);
            this.groupBox31.Controls.Add(this.linkLabel5);
            this.groupBox31.Location = new System.Drawing.Point(247, 106);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(337, 161);
            this.groupBox31.TabIndex = 66;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "HUC-8";
            // 
            // labelWDNRHUC8
            // 
            this.labelWDNRHUC8.AutoSize = true;
            this.labelWDNRHUC8.Location = new System.Drawing.Point(0, 142);
            this.labelWDNRHUC8.Name = "labelWDNRHUC8";
            this.labelWDNRHUC8.Size = new System.Drawing.Size(41, 13);
            this.labelWDNRHUC8.TabIndex = 62;
            this.labelWDNRHUC8.Text = "label56";
            this.labelWDNRHUC8.Visible = false;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(23, 16);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(76, 13);
            this.label49.TabIndex = 49;
            this.label49.Text = "Enter a HUC-8";
            // 
            // btnGetDataWithinHuc
            // 
            this.btnGetDataWithinHuc.Location = new System.Drawing.Point(26, 114);
            this.btnGetDataWithinHuc.Name = "btnGetDataWithinHuc";
            this.btnGetDataWithinHuc.Size = new System.Drawing.Size(297, 23);
            this.btnGetDataWithinHuc.TabIndex = 1;
            this.btnGetDataWithinHuc.Text = "Get Data Within Huc-8";
            this.btnGetDataWithinHuc.UseVisualStyleBackColor = true;
            this.btnGetDataWithinHuc.Click += new System.EventHandler(this.btnGetDataWithinHuc_Click);
            // 
            // txtHucWDNR
            // 
            this.txtHucWDNR.Location = new System.Drawing.Point(105, 13);
            this.txtHucWDNR.Name = "txtHucWDNR";
            this.txtHucWDNR.Size = new System.Drawing.Size(100, 20);
            this.txtHucWDNR.TabIndex = 48;
            // 
            // btnAddHucWDNR
            // 
            this.btnAddHucWDNR.Location = new System.Drawing.Point(105, 39);
            this.btnAddHucWDNR.Name = "btnAddHucWDNR";
            this.btnAddHucWDNR.Size = new System.Drawing.Size(100, 23);
            this.btnAddHucWDNR.TabIndex = 58;
            this.btnAddHucWDNR.Text = "Add HUC-8 to List";
            this.btnAddHucWDNR.UseVisualStyleBackColor = true;
            this.btnAddHucWDNR.Click += new System.EventHandler(this.btnAddHucWDNR_Click);
            // 
            // btnRemoveHucWDNR
            // 
            this.btnRemoveHucWDNR.Location = new System.Drawing.Point(105, 69);
            this.btnRemoveHucWDNR.Name = "btnRemoveHucWDNR";
            this.btnRemoveHucWDNR.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveHucWDNR.TabIndex = 61;
            this.btnRemoveHucWDNR.Text = "Remove Selected";
            this.btnRemoveHucWDNR.UseVisualStyleBackColor = true;
            this.btnRemoveHucWDNR.Click += new System.EventHandler(this.btnRemoveHucWDNR_Click);
            // 
            // listHucWDNR
            // 
            this.listHucWDNR.FormattingEnabled = true;
            this.listHucWDNR.Location = new System.Drawing.Point(232, 13);
            this.listHucWDNR.Name = "listHucWDNR";
            this.listHucWDNR.Size = new System.Drawing.Size(91, 95);
            this.listHucWDNR.TabIndex = 59;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(72, 95);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(130, 13);
            this.linkLabel5.TabIndex = 60;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Click here to find a HUC-8";
            this.linkLabel5.Click += new System.EventHandler(this.btnHUCfind_Click);
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.labelWDNRBB);
            this.groupBox29.Controls.Add(this.btnGetDataWithinBoxWDNR);
            this.groupBox29.Controls.Add(this.label48);
            this.groupBox29.Controls.Add(this.label46);
            this.groupBox29.Controls.Add(this.label37);
            this.groupBox29.Controls.Add(this.label16);
            this.groupBox29.Controls.Add(this.txtWestWDNR);
            this.groupBox29.Controls.Add(this.txtEastWDNR);
            this.groupBox29.Controls.Add(this.txtSouthWDNR);
            this.groupBox29.Controls.Add(this.txtNorthWDNR);
            this.groupBox29.Location = new System.Drawing.Point(23, 93);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(196, 174);
            this.groupBox29.TabIndex = 7;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Bounding Box";
            // 
            // labelWDNRBB
            // 
            this.labelWDNRBB.AutoSize = true;
            this.labelWDNRBB.Location = new System.Drawing.Point(1, 151);
            this.labelWDNRBB.Name = "labelWDNRBB";
            this.labelWDNRBB.Size = new System.Drawing.Size(41, 13);
            this.labelWDNRBB.TabIndex = 9;
            this.labelWDNRBB.Text = "label56";
            this.labelWDNRBB.Visible = false;
            // 
            // btnGetDataWithinBoxWDNR
            // 
            this.btnGetDataWithinBoxWDNR.Location = new System.Drawing.Point(12, 125);
            this.btnGetDataWithinBoxWDNR.Name = "btnGetDataWithinBoxWDNR";
            this.btnGetDataWithinBoxWDNR.Size = new System.Drawing.Size(171, 23);
            this.btnGetDataWithinBoxWDNR.TabIndex = 8;
            this.btnGetDataWithinBoxWDNR.Text = "Get Data Within Bounding Box";
            this.btnGetDataWithinBoxWDNR.UseVisualStyleBackColor = true;
            this.btnGetDataWithinBoxWDNR.Click += new System.EventHandler(this.btnGetDataWithinBoxWDNR_Click);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(11, 99);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(32, 13);
            this.label48.TabIndex = 7;
            this.label48.Text = "West";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(11, 73);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(28, 13);
            this.label46.TabIndex = 6;
            this.label46.Text = "East";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(9, 47);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(35, 13);
            this.label37.TabIndex = 5;
            this.label37.Text = "South";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "North";
            // 
            // txtWestWDNR
            // 
            this.txtWestWDNR.Location = new System.Drawing.Point(48, 99);
            this.txtWestWDNR.Name = "txtWestWDNR";
            this.txtWestWDNR.Size = new System.Drawing.Size(100, 20);
            this.txtWestWDNR.TabIndex = 3;
            // 
            // txtEastWDNR
            // 
            this.txtEastWDNR.Location = new System.Drawing.Point(48, 73);
            this.txtEastWDNR.Name = "txtEastWDNR";
            this.txtEastWDNR.Size = new System.Drawing.Size(100, 20);
            this.txtEastWDNR.TabIndex = 2;
            // 
            // txtSouthWDNR
            // 
            this.txtSouthWDNR.Location = new System.Drawing.Point(48, 47);
            this.txtSouthWDNR.Name = "txtSouthWDNR";
            this.txtSouthWDNR.Size = new System.Drawing.Size(100, 20);
            this.txtSouthWDNR.TabIndex = 1;
            // 
            // txtNorthWDNR
            // 
            this.txtNorthWDNR.Location = new System.Drawing.Point(48, 19);
            this.txtNorthWDNR.Name = "txtNorthWDNR";
            this.txtNorthWDNR.Size = new System.Drawing.Size(100, 20);
            this.txtNorthWDNR.TabIndex = 0;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.checkedListAnimals);
            this.groupBox28.Location = new System.Drawing.Point(445, 6);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(139, 100);
            this.groupBox28.TabIndex = 6;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Animal";
            // 
            // checkedListAnimals
            // 
            this.checkedListAnimals.CheckOnClick = true;
            this.checkedListAnimals.FormattingEnabled = true;
            this.checkedListAnimals.Items.AddRange(new object[] {
            "Select All",
            "Beef",
            "Chickens",
            "Dairy",
            "Swine",
            "Turkeys"});
            this.checkedListAnimals.Location = new System.Drawing.Point(6, 15);
            this.checkedListAnimals.Name = "checkedListAnimals";
            this.checkedListAnimals.Size = new System.Drawing.Size(120, 79);
            this.checkedListAnimals.TabIndex = 1;
            this.checkedListAnimals.SelectedIndexChanged += new System.EventHandler(this.checkedListAnimals_SelectedIndexChanged);
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.label70);
            this.groupBox27.Controls.Add(this.txtCacheWDNR);
            this.groupBox27.Controls.Add(this.btnBrowseCacheWDNR);
            this.groupBox27.Controls.Add(this.label69);
            this.groupBox27.Controls.Add(this.txtProjectFolderWDNR);
            this.groupBox27.Controls.Add(this.btnBrowseWDNR);
            this.groupBox27.Location = new System.Drawing.Point(23, 6);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(416, 74);
            this.groupBox27.TabIndex = 5;
            this.groupBox27.TabStop = false;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(29, 45);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(70, 13);
            this.label70.TabIndex = 7;
            this.label70.Text = "Cache Folder";
            // 
            // txtCacheWDNR
            // 
            this.txtCacheWDNR.Location = new System.Drawing.Point(107, 45);
            this.txtCacheWDNR.Name = "txtCacheWDNR";
            this.txtCacheWDNR.Size = new System.Drawing.Size(171, 20);
            this.txtCacheWDNR.TabIndex = 5;
            // 
            // btnBrowseCacheWDNR
            // 
            this.btnBrowseCacheWDNR.Location = new System.Drawing.Point(299, 43);
            this.btnBrowseCacheWDNR.Name = "btnBrowseCacheWDNR";
            this.btnBrowseCacheWDNR.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheWDNR.TabIndex = 6;
            this.btnBrowseCacheWDNR.Text = "Browse";
            this.btnBrowseCacheWDNR.UseVisualStyleBackColor = true;
            this.btnBrowseCacheWDNR.Click += new System.EventHandler(this.btnBrowseCacheWDNR_Click);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(29, 12);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(72, 13);
            this.label69.TabIndex = 4;
            this.label69.Text = "Project Folder";
            // 
            // txtProjectFolderWDNR
            // 
            this.txtProjectFolderWDNR.Location = new System.Drawing.Point(107, 12);
            this.txtProjectFolderWDNR.Name = "txtProjectFolderWDNR";
            this.txtProjectFolderWDNR.Size = new System.Drawing.Size(171, 20);
            this.txtProjectFolderWDNR.TabIndex = 2;
            // 
            // btnBrowseWDNR
            // 
            this.btnBrowseWDNR.Location = new System.Drawing.Point(299, 10);
            this.btnBrowseWDNR.Name = "btnBrowseWDNR";
            this.btnBrowseWDNR.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseWDNR.TabIndex = 3;
            this.btnBrowseWDNR.Text = "Browse";
            this.btnBrowseWDNR.UseVisualStyleBackColor = true;
            this.btnBrowseWDNR.Click += new System.EventHandler(this.btnBrowseWDNR_Click);
            // 
            // btnGetSpreadsheet
            // 
            this.btnGetSpreadsheet.Location = new System.Drawing.Point(609, 18);
            this.btnGetSpreadsheet.Name = "btnGetSpreadsheet";
            this.btnGetSpreadsheet.Size = new System.Drawing.Size(95, 46);
            this.btnGetSpreadsheet.TabIndex = 0;
            this.btnGetSpreadsheet.Text = "Get Statewide Data";
            this.btnGetSpreadsheet.UseVisualStyleBackColor = true;
            this.btnGetSpreadsheet.Click += new System.EventHandler(this.btnGetSpreadsheet_Click);
            // 
            // btnDownloadNASS
            // 
            this.btnDownloadNASS.Controls.Add(this.labelNASS);
            this.btnDownloadNASS.Controls.Add(this.groupBox34);
            this.btnDownloadNASS.Controls.Add(this.groupBox33);
            this.btnDownloadNASS.Controls.Add(this.groupBox32);
            this.btnDownloadNASS.Controls.Add(this.btnNASSDownload);
            this.btnDownloadNASS.Location = new System.Drawing.Point(4, 22);
            this.btnDownloadNASS.Name = "btnDownloadNASS";
            this.btnDownloadNASS.Padding = new System.Windows.Forms.Padding(3);
            this.btnDownloadNASS.Size = new System.Drawing.Size(892, 655);
            this.btnDownloadNASS.TabIndex = 12;
            this.btnDownloadNASS.Text = "NASS";
            this.btnDownloadNASS.UseVisualStyleBackColor = true;
            this.btnDownloadNASS.Click += new System.EventHandler(this.btnDownloadNASS_Click);
            // 
            // labelNASS
            // 
            this.labelNASS.AutoSize = true;
            this.labelNASS.Location = new System.Drawing.Point(142, 271);
            this.labelNASS.Name = "labelNASS";
            this.labelNASS.Size = new System.Drawing.Size(41, 13);
            this.labelNASS.TabIndex = 39;
            this.labelNASS.Text = "label56";
            this.labelNASS.Visible = false;
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.listYearsNASS);
            this.groupBox34.Location = new System.Drawing.Point(252, 136);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(104, 78);
            this.groupBox34.TabIndex = 38;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "Year";
            // 
            // listYearsNASS
            // 
            this.listYearsNASS.CheckOnClick = true;
            this.listYearsNASS.FormattingEnabled = true;
            this.listYearsNASS.Items.AddRange(new object[] {
            "2008",
            "2009",
            "2010"});
            this.listYearsNASS.Location = new System.Drawing.Point(13, 19);
            this.listYearsNASS.Name = "listYearsNASS";
            this.listYearsNASS.Size = new System.Drawing.Size(85, 49);
            this.listYearsNASS.TabIndex = 37;
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.label71);
            this.groupBox33.Controls.Add(this.txtCacheNASS);
            this.groupBox33.Controls.Add(this.btnBrowseCacheNASS);
            this.groupBox33.Controls.Add(this.label55);
            this.groupBox33.Controls.Add(this.txtProjectFolderNASS);
            this.groupBox33.Controls.Add(this.btnBrowseNASS);
            this.groupBox33.Location = new System.Drawing.Point(27, 6);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(446, 95);
            this.groupBox33.TabIndex = 36;
            this.groupBox33.TabStop = false;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(11, 57);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(70, 13);
            this.label71.TabIndex = 33;
            this.label71.Text = "Cache Folder";
            // 
            // txtCacheNASS
            // 
            this.txtCacheNASS.Location = new System.Drawing.Point(87, 57);
            this.txtCacheNASS.Name = "txtCacheNASS";
            this.txtCacheNASS.Size = new System.Drawing.Size(259, 20);
            this.txtCacheNASS.TabIndex = 32;
            // 
            // btnBrowseCacheNASS
            // 
            this.btnBrowseCacheNASS.Location = new System.Drawing.Point(352, 57);
            this.btnBrowseCacheNASS.Name = "btnBrowseCacheNASS";
            this.btnBrowseCacheNASS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheNASS.TabIndex = 34;
            this.btnBrowseCacheNASS.Text = "Browse";
            this.btnBrowseCacheNASS.UseVisualStyleBackColor = true;
            this.btnBrowseCacheNASS.Click += new System.EventHandler(this.btnBrowseCacheNASS_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(11, 19);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(72, 13);
            this.label55.TabIndex = 18;
            this.label55.Text = "Project Folder";
            // 
            // txtProjectFolderNASS
            // 
            this.txtProjectFolderNASS.Location = new System.Drawing.Point(87, 19);
            this.txtProjectFolderNASS.Name = "txtProjectFolderNASS";
            this.txtProjectFolderNASS.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNASS.TabIndex = 17;
            // 
            // btnBrowseNASS
            // 
            this.btnBrowseNASS.Location = new System.Drawing.Point(352, 19);
            this.btnBrowseNASS.Name = "btnBrowseNASS";
            this.btnBrowseNASS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNASS.TabIndex = 31;
            this.btnBrowseNASS.Text = "Browse";
            this.btnBrowseNASS.UseVisualStyleBackColor = true;
            this.btnBrowseNASS.Click += new System.EventHandler(this.btnBrowseNASS_Click);
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.label51);
            this.groupBox32.Controls.Add(this.label52);
            this.groupBox32.Controls.Add(this.label53);
            this.groupBox32.Controls.Add(this.txtNorthNASS);
            this.groupBox32.Controls.Add(this.label54);
            this.groupBox32.Controls.Add(this.txtSouthNASS);
            this.groupBox32.Controls.Add(this.txtEastNASS);
            this.groupBox32.Controls.Add(this.txtWestNASS);
            this.groupBox32.Location = new System.Drawing.Point(27, 127);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(177, 131);
            this.groupBox32.TabIndex = 34;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "NASS Bounding Box";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(18, 26);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(33, 13);
            this.label51.TabIndex = 22;
            this.label51.Text = "North";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(18, 49);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(35, 13);
            this.label52.TabIndex = 24;
            this.label52.Text = "South";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(18, 94);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(32, 13);
            this.label53.TabIndex = 26;
            this.label53.Text = "West";
            // 
            // txtNorthNASS
            // 
            this.txtNorthNASS.Location = new System.Drawing.Point(56, 23);
            this.txtNorthNASS.Name = "txtNorthNASS";
            this.txtNorthNASS.Size = new System.Drawing.Size(100, 20);
            this.txtNorthNASS.TabIndex = 21;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(18, 74);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(28, 13);
            this.label54.TabIndex = 28;
            this.label54.Text = "East";
            // 
            // txtSouthNASS
            // 
            this.txtSouthNASS.Location = new System.Drawing.Point(56, 46);
            this.txtSouthNASS.Name = "txtSouthNASS";
            this.txtSouthNASS.Size = new System.Drawing.Size(100, 20);
            this.txtSouthNASS.TabIndex = 23;
            // 
            // txtEastNASS
            // 
            this.txtEastNASS.Location = new System.Drawing.Point(56, 71);
            this.txtEastNASS.Name = "txtEastNASS";
            this.txtEastNASS.Size = new System.Drawing.Size(100, 20);
            this.txtEastNASS.TabIndex = 27;
            // 
            // txtWestNASS
            // 
            this.txtWestNASS.Location = new System.Drawing.Point(56, 94);
            this.txtWestNASS.Name = "txtWestNASS";
            this.txtWestNASS.Size = new System.Drawing.Size(100, 20);
            this.txtWestNASS.TabIndex = 25;
            // 
            // btnNASSDownload
            // 
            this.btnNASSDownload.Location = new System.Drawing.Point(401, 150);
            this.btnNASSDownload.Name = "btnNASSDownload";
            this.btnNASSDownload.Size = new System.Drawing.Size(172, 64);
            this.btnNASSDownload.TabIndex = 0;
            this.btnNASSDownload.Text = "Download ";
            this.btnNASSDownload.UseVisualStyleBackColor = true;
            this.btnNASSDownload.Click += new System.EventHandler(this.btnNASSDownload_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.groupBox37);
            this.tabPage8.Controls.Add(this.label59);
            this.tabPage8.Controls.Add(this.btnBrowseNDBC);
            this.tabPage8.Controls.Add(this.txtProjectFolderNDBC);
            this.tabPage8.Controls.Add(this.dataGridViewNDBC);
            this.tabPage8.Controls.Add(this.label58);
            this.tabPage8.Controls.Add(this.label57);
            this.tabPage8.Controls.Add(this.label56);
            this.tabPage8.Controls.Add(this.txtRadiusNDBC);
            this.tabPage8.Controls.Add(this.txtLongitudeNDBC);
            this.tabPage8.Controls.Add(this.txtLatitudeNDBC);
            this.tabPage8.Controls.Add(this.labelNDBC);
            this.tabPage8.Controls.Add(this.btnRunNDBC);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(892, 655);
            this.tabPage8.TabIndex = 13;
            this.tabPage8.Text = "NDBC";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.label24);
            this.groupBox37.Controls.Add(this.label23);
            this.groupBox37.Controls.Add(this.txtNDBCyear);
            this.groupBox37.Controls.Add(this.txtNDBCStationID);
            this.groupBox37.Controls.Add(this.btnNDBChistoricalData);
            this.groupBox37.Location = new System.Drawing.Point(162, 453);
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.Size = new System.Drawing.Size(252, 114);
            this.groupBox37.TabIndex = 18;
            this.groupBox37.TabStop = false;
            this.groupBox37.Text = "Historical Data";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(57, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Year";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(44, 19);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "StationID";
            // 
            // txtNDBCyear
            // 
            this.txtNDBCyear.Location = new System.Drawing.Point(101, 46);
            this.txtNDBCyear.Name = "txtNDBCyear";
            this.txtNDBCyear.Size = new System.Drawing.Size(100, 20);
            this.txtNDBCyear.TabIndex = 2;
            // 
            // txtNDBCStationID
            // 
            this.txtNDBCStationID.Location = new System.Drawing.Point(101, 19);
            this.txtNDBCStationID.Name = "txtNDBCStationID";
            this.txtNDBCStationID.Size = new System.Drawing.Size(100, 20);
            this.txtNDBCStationID.TabIndex = 1;
            // 
            // btnNDBChistoricalData
            // 
            this.btnNDBChistoricalData.Location = new System.Drawing.Point(47, 85);
            this.btnNDBChistoricalData.Name = "btnNDBChistoricalData";
            this.btnNDBChistoricalData.Size = new System.Drawing.Size(145, 23);
            this.btnNDBChistoricalData.TabIndex = 0;
            this.btnNDBChistoricalData.Text = "Get Historical Data";
            this.btnNDBChistoricalData.UseVisualStyleBackColor = true;
            this.btnNDBChistoricalData.Click += new System.EventHandler(this.btnNDBChistoricalData_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(182, 30);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(75, 13);
            this.label59.TabIndex = 17;
            this.label59.Text = "Project Folder:";
            // 
            // btnBrowseNDBC
            // 
            this.btnBrowseNDBC.Location = new System.Drawing.Point(548, 28);
            this.btnBrowseNDBC.Name = "btnBrowseNDBC";
            this.btnBrowseNDBC.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNDBC.TabIndex = 16;
            this.btnBrowseNDBC.Text = "Browse";
            this.btnBrowseNDBC.UseVisualStyleBackColor = true;
            this.btnBrowseNDBC.Click += new System.EventHandler(this.btnBrowseNDBC_Click_1);
            // 
            // txtProjectFolderNDBC
            // 
            this.txtProjectFolderNDBC.Location = new System.Drawing.Point(263, 30);
            this.txtProjectFolderNDBC.Name = "txtProjectFolderNDBC";
            this.txtProjectFolderNDBC.Size = new System.Drawing.Size(264, 20);
            this.txtProjectFolderNDBC.TabIndex = 15;
            // 
            // dataGridViewNDBC
            // 
            this.dataGridViewNDBC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNDBC.Location = new System.Drawing.Point(201, 91);
            this.dataGridViewNDBC.Name = "dataGridViewNDBC";
            this.dataGridViewNDBC.Size = new System.Drawing.Size(438, 284);
            this.dataGridViewNDBC.TabIndex = 8;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 56);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(40, 13);
            this.label58.TabIndex = 7;
            this.label58.Text = "Radius";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 30);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(54, 13);
            this.label57.TabIndex = 6;
            this.label57.Text = "Longitude";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 6);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(45, 13);
            this.label56.TabIndex = 5;
            this.label56.Text = "Latitude";
            // 
            // txtRadiusNDBC
            // 
            this.txtRadiusNDBC.Location = new System.Drawing.Point(67, 56);
            this.txtRadiusNDBC.Name = "txtRadiusNDBC";
            this.txtRadiusNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtRadiusNDBC.TabIndex = 4;
            // 
            // txtLongitudeNDBC
            // 
            this.txtLongitudeNDBC.Location = new System.Drawing.Point(67, 30);
            this.txtLongitudeNDBC.Name = "txtLongitudeNDBC";
            this.txtLongitudeNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtLongitudeNDBC.TabIndex = 3;
            // 
            // txtLatitudeNDBC
            // 
            this.txtLatitudeNDBC.Location = new System.Drawing.Point(67, 3);
            this.txtLatitudeNDBC.Name = "txtLatitudeNDBC";
            this.txtLatitudeNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtLatitudeNDBC.TabIndex = 2;
            // 
            // labelNDBC
            // 
            this.labelNDBC.AutoSize = true;
            this.labelNDBC.Location = new System.Drawing.Point(41, 392);
            this.labelNDBC.Name = "labelNDBC";
            this.labelNDBC.Size = new System.Drawing.Size(59, 13);
            this.labelNDBC.TabIndex = 1;
            this.labelNDBC.Text = "labelNDBC";
            this.labelNDBC.Visible = false;
            // 
            // btnRunNDBC
            // 
            this.btnRunNDBC.Location = new System.Drawing.Point(6, 119);
            this.btnRunNDBC.Name = "btnRunNDBC";
            this.btnRunNDBC.Size = new System.Drawing.Size(162, 104);
            this.btnRunNDBC.TabIndex = 0;
            this.btnRunNDBC.Text = "Download";
            this.btnRunNDBC.UseVisualStyleBackColor = true;
            this.btnRunNDBC.Click += new System.EventHandler(this.btnRunNDBC_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label81);
            this.tabPage1.Controls.Add(this.txtHucNLDAS);
            this.tabPage1.Controls.Add(this.label80);
            this.tabPage1.Controls.Add(this.label77);
            this.tabPage1.Controls.Add(this.txtCacheFolderNLDAS);
            this.tabPage1.Controls.Add(this.txtProjectFolderNLDAS);
            this.tabPage1.Controls.Add(this.lblNLDAS);
            this.tabPage1.Controls.Add(this.label36);
            this.tabPage1.Controls.Add(this.label35);
            this.tabPage1.Controls.Add(this.txtLongitudeNLDAS);
            this.tabPage1.Controls.Add(this.txtLatitudeNLDAS);
            this.tabPage1.Controls.Add(this.btnRunNLDAS);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(892, 655);
            this.tabPage1.TabIndex = 14;
            this.tabPage1.Text = "NLDAS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(507, 145);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(70, 13);
            this.label81.TabIndex = 11;
            this.label81.Text = "HUC Number";
            // 
            // txtHucNLDAS
            // 
            this.txtHucNLDAS.Location = new System.Drawing.Point(510, 163);
            this.txtHucNLDAS.Name = "txtHucNLDAS";
            this.txtHucNLDAS.Size = new System.Drawing.Size(100, 20);
            this.txtHucNLDAS.TabIndex = 10;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(405, 77);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(38, 13);
            this.label80.TabIndex = 9;
            this.label80.Text = "Cache";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(405, 36);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(40, 13);
            this.label77.TabIndex = 8;
            this.label77.Text = "Project";
            // 
            // txtCacheFolderNLDAS
            // 
            this.txtCacheFolderNLDAS.Location = new System.Drawing.Point(408, 93);
            this.txtCacheFolderNLDAS.Name = "txtCacheFolderNLDAS";
            this.txtCacheFolderNLDAS.Size = new System.Drawing.Size(398, 20);
            this.txtCacheFolderNLDAS.TabIndex = 7;
            this.txtCacheFolderNLDAS.Text = "C:\\D4EMFromGoogleCodeSVN\\D4EM_05-30-13_NLDAS\\Externals\\cache\\";
            // 
            // txtProjectFolderNLDAS
            // 
            this.txtProjectFolderNLDAS.Location = new System.Drawing.Point(408, 52);
            this.txtProjectFolderNLDAS.Name = "txtProjectFolderNLDAS";
            this.txtProjectFolderNLDAS.Size = new System.Drawing.Size(398, 20);
            this.txtProjectFolderNLDAS.TabIndex = 6;
            this.txtProjectFolderNLDAS.Text = "C:\\D4EMFromGoogleCodeSVN\\D4EM_05-30-13_NLDAS\\Externals\\data\\";
            // 
            // lblNLDAS
            // 
            this.lblNLDAS.AutoSize = true;
            this.lblNLDAS.Location = new System.Drawing.Point(405, 186);
            this.lblNLDAS.Name = "lblNLDAS";
            this.lblNLDAS.Size = new System.Drawing.Size(41, 13);
            this.lblNLDAS.TabIndex = 5;
            this.lblNLDAS.Text = "label77";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(265, 79);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(31, 13);
            this.label36.TabIndex = 4;
            this.label36.Text = "Long";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(266, 36);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(22, 13);
            this.label35.TabIndex = 3;
            this.label35.Text = "Lat";
            // 
            // txtLongitudeNLDAS
            // 
            this.txtLongitudeNLDAS.Location = new System.Drawing.Point(269, 93);
            this.txtLongitudeNLDAS.Name = "txtLongitudeNLDAS";
            this.txtLongitudeNLDAS.Size = new System.Drawing.Size(100, 20);
            this.txtLongitudeNLDAS.TabIndex = 2;
            // 
            // txtLatitudeNLDAS
            // 
            this.txtLatitudeNLDAS.Location = new System.Drawing.Point(269, 52);
            this.txtLatitudeNLDAS.Name = "txtLatitudeNLDAS";
            this.txtLatitudeNLDAS.Size = new System.Drawing.Size(100, 20);
            this.txtLatitudeNLDAS.TabIndex = 1;
            // 
            // btnRunNLDAS
            // 
            this.btnRunNLDAS.Location = new System.Drawing.Point(408, 160);
            this.btnRunNLDAS.Name = "btnRunNLDAS";
            this.btnRunNLDAS.Size = new System.Drawing.Size(75, 23);
            this.btnRunNLDAS.TabIndex = 0;
            this.btnRunNLDAS.Text = "Run";
            this.btnRunNLDAS.UseVisualStyleBackColor = true;
            this.btnRunNLDAS.Click += new System.EventHandler(this.btnRunNLDAS_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.btnGetNAWQAaverageStdDev);
            this.tabPage9.Controls.Add(this.groupNAWQAchoice);
            this.tabPage9.Controls.Add(this.gridShowNAWQAaverages);
            this.tabPage9.Controls.Add(this.groupNAWQAstatesCounties);
            this.tabPage9.Controls.Add(this.groupNAWQAlatLong);
            this.tabPage9.Controls.Add(this.labelNAWQA);
            this.tabPage9.Controls.Add(this.groupBox36);
            this.tabPage9.Controls.Add(this.groupNAQWAdates);
            this.tabPage9.Controls.Add(this.groupNAWQAqueryTypes);
            this.tabPage9.Controls.Add(this.groupNAWQAfileTypes);
            this.tabPage9.Controls.Add(this.groupNAWQAwaterType);
            this.tabPage9.Controls.Add(this.groupNAWQAparameters);
            this.tabPage9.Controls.Add(this.btnRunNAWQA);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(892, 655);
            this.tabPage9.TabIndex = 15;
            this.tabPage9.Text = "NAWQA";
            this.tabPage9.UseVisualStyleBackColor = true;
            this.tabPage9.Click += new System.EventHandler(this.tabPage9_Click);
            // 
            // btnGetNAWQAaverageStdDev
            // 
            this.btnGetNAWQAaverageStdDev.Location = new System.Drawing.Point(75, 490);
            this.btnGetNAWQAaverageStdDev.Name = "btnGetNAWQAaverageStdDev";
            this.btnGetNAWQAaverageStdDev.Size = new System.Drawing.Size(322, 23);
            this.btnGetNAWQAaverageStdDev.TabIndex = 97;
            this.btnGetNAWQAaverageStdDev.Text = "Get Average And Standard Deviation";
            this.btnGetNAWQAaverageStdDev.UseVisualStyleBackColor = true;
            this.btnGetNAWQAaverageStdDev.Click += new System.EventHandler(this.btnGetNAWQAaverageStdDev_Click);
            // 
            // groupNAWQAchoice
            // 
            this.groupNAWQAchoice.Controls.Add(this.radioUseLatLong);
            this.groupNAWQAchoice.Controls.Add(this.radioUseStatesCounties);
            this.groupNAWQAchoice.Location = new System.Drawing.Point(3, 79);
            this.groupNAWQAchoice.Name = "groupNAWQAchoice";
            this.groupNAWQAchoice.Size = new System.Drawing.Size(257, 72);
            this.groupNAWQAchoice.TabIndex = 96;
            this.groupNAWQAchoice.TabStop = false;
            // 
            // radioUseLatLong
            // 
            this.radioUseLatLong.AutoSize = true;
            this.radioUseLatLong.Location = new System.Drawing.Point(13, 39);
            this.radioUseLatLong.Name = "radioUseLatLong";
            this.radioUseLatLong.Size = new System.Drawing.Size(192, 17);
            this.radioUseLatLong.TabIndex = 1;
            this.radioUseLatLong.Text = "Download NAWQA using Lat/Long";
            this.radioUseLatLong.UseVisualStyleBackColor = true;
            this.radioUseLatLong.CheckedChanged += new System.EventHandler(this.radioUseLatLong_CheckedChanged);
            // 
            // radioUseStatesCounties
            // 
            this.radioUseStatesCounties.AutoSize = true;
            this.radioUseStatesCounties.Checked = true;
            this.radioUseStatesCounties.Location = new System.Drawing.Point(13, 16);
            this.radioUseStatesCounties.Name = "radioUseStatesCounties";
            this.radioUseStatesCounties.Size = new System.Drawing.Size(224, 17);
            this.radioUseStatesCounties.TabIndex = 0;
            this.radioUseStatesCounties.TabStop = true;
            this.radioUseStatesCounties.Text = "Download NAWQA using States/Counties";
            this.radioUseStatesCounties.UseVisualStyleBackColor = true;
            this.radioUseStatesCounties.CheckedChanged += new System.EventHandler(this.radioUseStatesCounties_CheckedChanged);
            // 
            // gridShowNAWQAaverages
            // 
            this.gridShowNAWQAaverages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShowNAWQAaverages.Location = new System.Drawing.Point(18, 525);
            this.gridShowNAWQAaverages.Name = "gridShowNAWQAaverages";
            this.gridShowNAWQAaverages.Size = new System.Drawing.Size(856, 124);
            this.gridShowNAWQAaverages.TabIndex = 95;
            this.gridShowNAWQAaverages.Visible = false;
            // 
            // groupNAWQAstatesCounties
            // 
            this.groupNAWQAstatesCounties.Controls.Add(this.groupNAWQAstates);
            this.groupNAWQAstatesCounties.Controls.Add(this.groupBox35);
            this.groupNAWQAstatesCounties.Location = new System.Drawing.Point(590, 198);
            this.groupNAWQAstatesCounties.Name = "groupNAWQAstatesCounties";
            this.groupNAWQAstatesCounties.Size = new System.Drawing.Size(275, 257);
            this.groupNAWQAstatesCounties.TabIndex = 94;
            this.groupNAWQAstatesCounties.TabStop = false;
            this.groupNAWQAstatesCounties.Text = "Download NAWQA using States/Counties";
            // 
            // groupNAWQAstates
            // 
            this.groupNAWQAstates.Controls.Add(this.listNAWQAstates);
            this.groupNAWQAstates.Location = new System.Drawing.Point(31, 19);
            this.groupNAWQAstates.Name = "groupNAWQAstates";
            this.groupNAWQAstates.Size = new System.Drawing.Size(193, 105);
            this.groupNAWQAstates.TabIndex = 75;
            this.groupNAWQAstates.TabStop = false;
            this.groupNAWQAstates.Text = "States/Provinces";
            // 
            // listNAWQAstates
            // 
            this.listNAWQAstates.FormattingEnabled = true;
            this.listNAWQAstates.Items.AddRange(new object[] {
            "ALABAMA",
            "ALASKA",
            "ARIZONA",
            "ARKANSAS",
            "BRITISH COLUMBIA",
            "CALIFORNIA",
            "COLORADO",
            "CONNECTICUT",
            "DELAWARE",
            "DISTRICT OF COLUMBIA",
            "FLORIDA",
            "GEORGIA",
            "HAWAII",
            "IDAHO",
            "ILLINOIS",
            "INDIANA",
            "IOWA",
            "KANSAS",
            "KENTUCKY",
            "LOUISIANA",
            "MAINE",
            "MANITOBA",
            "MARYLAND",
            "MASSACHUSETTS",
            "MICHIGAN",
            "MINNESOTA",
            "MISSISSIPPI",
            "MISSOURI",
            "MONTANA",
            "NEBRASKA",
            "NEVADA",
            "NEW HAMPSHIRE",
            "NEW JERSEY",
            "NEW MEXICO",
            "NEW YORK",
            "NORTH CAROLINA",
            "NORTH DAKOTA",
            "OHIO",
            "OKLAHOMA",
            "OREGON",
            "PENNSYLVANIA",
            "RHODE ISLAND",
            "SOUTH CAROLINA",
            "SOUTH DAKOTA",
            "TENNESSEE",
            "TEXAS",
            "UTAH",
            "VERMONT",
            "VIRGINIA",
            "WASHINGTON",
            "WEST VIRGINIA",
            "WISCONSIN",
            "WYOMING"});
            this.listNAWQAstates.Location = new System.Drawing.Point(6, 21);
            this.listNAWQAstates.Name = "listNAWQAstates";
            this.listNAWQAstates.Size = new System.Drawing.Size(167, 69);
            this.listNAWQAstates.TabIndex = 90;
            this.listNAWQAstates.SelectedIndexChanged += new System.EventHandler(this.listNAWQAstates_SelectedIndexChanged_1);
            // 
            // groupBox35
            // 
            this.groupBox35.Controls.Add(this.listCounties);
            this.groupBox35.Location = new System.Drawing.Point(15, 130);
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.Size = new System.Drawing.Size(233, 120);
            this.groupBox35.TabIndex = 89;
            this.groupBox35.TabStop = false;
            this.groupBox35.Text = "Counties";
            // 
            // listCounties
            // 
            this.listCounties.CheckOnClick = true;
            this.listCounties.FormattingEnabled = true;
            this.listCounties.Location = new System.Drawing.Point(8, 14);
            this.listCounties.Name = "listCounties";
            this.listCounties.Size = new System.Drawing.Size(195, 94);
            this.listCounties.TabIndex = 88;
            this.listCounties.SelectedIndexChanged += new System.EventHandler(this.listCounties_SelectedIndexChanged);
            // 
            // groupNAWQAlatLong
            // 
            this.groupNAWQAlatLong.Controls.Add(this.btnDetermineCounty);
            this.groupNAWQAlatLong.Controls.Add(this.labelNAWQAlatLongCounty);
            this.groupNAWQAlatLong.Controls.Add(this.txtNAWQAlng);
            this.groupNAWQAlatLong.Controls.Add(this.label22);
            this.groupNAWQAlatLong.Controls.Add(this.txtNAWQAlat);
            this.groupNAWQAlatLong.Controls.Add(this.label21);
            this.groupNAWQAlatLong.Location = new System.Drawing.Point(75, 376);
            this.groupNAWQAlatLong.Name = "groupNAWQAlatLong";
            this.groupNAWQAlatLong.Size = new System.Drawing.Size(337, 79);
            this.groupNAWQAlatLong.TabIndex = 92;
            this.groupNAWQAlatLong.TabStop = false;
            this.groupNAWQAlatLong.Text = "Download NAWQA using Lat/Long";
            this.groupNAWQAlatLong.Visible = false;
            // 
            // btnDetermineCounty
            // 
            this.btnDetermineCounty.Location = new System.Drawing.Point(236, 14);
            this.btnDetermineCounty.Name = "btnDetermineCounty";
            this.btnDetermineCounty.Size = new System.Drawing.Size(75, 38);
            this.btnDetermineCounty.TabIndex = 89;
            this.btnDetermineCounty.Text = "Determine County";
            this.btnDetermineCounty.UseVisualStyleBackColor = true;
            this.btnDetermineCounty.Click += new System.EventHandler(this.btnDetermineCounty_Click);
            // 
            // labelNAWQAlatLongCounty
            // 
            this.labelNAWQAlatLongCounty.AutoSize = true;
            this.labelNAWQAlatLongCounty.Location = new System.Drawing.Point(46, 62);
            this.labelNAWQAlatLongCounty.Name = "labelNAWQAlatLongCounty";
            this.labelNAWQAlatLongCounty.Size = new System.Drawing.Size(41, 13);
            this.labelNAWQAlatLongCounty.TabIndex = 88;
            this.labelNAWQAlatLongCounty.Text = "label23";
            this.labelNAWQAlatLongCounty.Visible = false;
            // 
            // txtNAWQAlng
            // 
            this.txtNAWQAlng.Location = new System.Drawing.Point(130, 32);
            this.txtNAWQAlng.Name = "txtNAWQAlng";
            this.txtNAWQAlng.Size = new System.Drawing.Size(100, 20);
            this.txtNAWQAlng.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(127, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Longitude";
            // 
            // txtNAWQAlat
            // 
            this.txtNAWQAlat.Location = new System.Drawing.Point(19, 32);
            this.txtNAWQAlat.Name = "txtNAWQAlat";
            this.txtNAWQAlat.Size = new System.Drawing.Size(100, 20);
            this.txtNAWQAlat.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(16, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(45, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Latitude";
            // 
            // labelNAWQA
            // 
            this.labelNAWQA.AutoSize = true;
            this.labelNAWQA.Location = new System.Drawing.Point(403, 481);
            this.labelNAWQA.Name = "labelNAWQA";
            this.labelNAWQA.Size = new System.Drawing.Size(41, 13);
            this.labelNAWQA.TabIndex = 91;
            this.labelNAWQA.Text = "label21";
            this.labelNAWQA.Visible = false;
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.label1);
            this.groupBox36.Controls.Add(this.txtCacheFolderNAWQA);
            this.groupBox36.Controls.Add(this.btnBrowseCacheFolderNAWQA);
            this.groupBox36.Controls.Add(this.label14);
            this.groupBox36.Controls.Add(this.txtProjectFolderNAWQA);
            this.groupBox36.Controls.Add(this.btnBrowseProjectFolderNAWQA);
            this.groupBox36.Location = new System.Drawing.Point(71, 276);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Size = new System.Drawing.Size(457, 94);
            this.groupBox36.TabIndex = 90;
            this.groupBox36.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Cache Folder";
            // 
            // txtCacheFolderNAWQA
            // 
            this.txtCacheFolderNAWQA.Location = new System.Drawing.Point(82, 53);
            this.txtCacheFolderNAWQA.Name = "txtCacheFolderNAWQA";
            this.txtCacheFolderNAWQA.Size = new System.Drawing.Size(259, 20);
            this.txtCacheFolderNAWQA.TabIndex = 56;
            // 
            // btnBrowseCacheFolderNAWQA
            // 
            this.btnBrowseCacheFolderNAWQA.Location = new System.Drawing.Point(347, 53);
            this.btnBrowseCacheFolderNAWQA.Name = "btnBrowseCacheFolderNAWQA";
            this.btnBrowseCacheFolderNAWQA.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheFolderNAWQA.TabIndex = 58;
            this.btnBrowseCacheFolderNAWQA.Text = "Browse";
            this.btnBrowseCacheFolderNAWQA.UseVisualStyleBackColor = true;
            this.btnBrowseCacheFolderNAWQA.Click += new System.EventHandler(this.btnBrowseCacheFolderNAWQA_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "Project Folder";
            // 
            // txtProjectFolderNAWQA
            // 
            this.txtProjectFolderNAWQA.Location = new System.Drawing.Point(82, 16);
            this.txtProjectFolderNAWQA.Name = "txtProjectFolderNAWQA";
            this.txtProjectFolderNAWQA.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNAWQA.TabIndex = 52;
            // 
            // btnBrowseProjectFolderNAWQA
            // 
            this.btnBrowseProjectFolderNAWQA.Location = new System.Drawing.Point(347, 16);
            this.btnBrowseProjectFolderNAWQA.Name = "btnBrowseProjectFolderNAWQA";
            this.btnBrowseProjectFolderNAWQA.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectFolderNAWQA.TabIndex = 55;
            this.btnBrowseProjectFolderNAWQA.Text = "Browse";
            this.btnBrowseProjectFolderNAWQA.UseVisualStyleBackColor = true;
            this.btnBrowseProjectFolderNAWQA.Click += new System.EventHandler(this.btnBrowseProjectFolderNAWQA_Click);
            // 
            // groupNAQWAdates
            // 
            this.groupNAQWAdates.Controls.Add(this.chkBoxWater);
            this.groupNAQWAdates.Controls.Add(this.groupNAWQAstart);
            this.groupNAQWAdates.Controls.Add(this.groupNAQWAend);
            this.groupNAQWAdates.Location = new System.Drawing.Point(590, 6);
            this.groupNAQWAdates.Name = "groupNAQWAdates";
            this.groupNAQWAdates.Size = new System.Drawing.Size(208, 186);
            this.groupNAQWAdates.TabIndex = 87;
            this.groupNAQWAdates.TabStop = false;
            this.groupNAQWAdates.Text = "Water Year Dates";
            // 
            // chkBoxWater
            // 
            this.chkBoxWater.AutoSize = true;
            this.chkBoxWater.Location = new System.Drawing.Point(15, 19);
            this.chkBoxWater.Name = "chkBoxWater";
            this.chkBoxWater.Size = new System.Drawing.Size(133, 17);
            this.chkBoxWater.TabIndex = 86;
            this.chkBoxWater.Text = "Use Water Year Dates";
            this.chkBoxWater.UseVisualStyleBackColor = true;
            // 
            // groupNAWQAstart
            // 
            this.groupNAWQAstart.Controls.Add(this.txtStartYearNAQWA);
            this.groupNAWQAstart.Location = new System.Drawing.Point(15, 42);
            this.groupNAWQAstart.Name = "groupNAWQAstart";
            this.groupNAWQAstart.Size = new System.Drawing.Size(139, 49);
            this.groupNAWQAstart.TabIndex = 84;
            this.groupNAWQAstart.TabStop = false;
            this.groupNAWQAstart.Text = "Start Year";
            // 
            // txtStartYearNAQWA
            // 
            this.txtStartYearNAQWA.Location = new System.Drawing.Point(16, 17);
            this.txtStartYearNAQWA.Name = "txtStartYearNAQWA";
            this.txtStartYearNAQWA.Size = new System.Drawing.Size(100, 20);
            this.txtStartYearNAQWA.TabIndex = 0;
            // 
            // groupNAQWAend
            // 
            this.groupNAQWAend.Controls.Add(this.txtEndYearNAQWA);
            this.groupNAQWAend.Location = new System.Drawing.Point(15, 108);
            this.groupNAQWAend.Name = "groupNAQWAend";
            this.groupNAQWAend.Size = new System.Drawing.Size(139, 50);
            this.groupNAQWAend.TabIndex = 85;
            this.groupNAQWAend.TabStop = false;
            this.groupNAQWAend.Text = "End Year";
            // 
            // txtEndYearNAQWA
            // 
            this.txtEndYearNAQWA.Location = new System.Drawing.Point(16, 17);
            this.txtEndYearNAQWA.Name = "txtEndYearNAQWA";
            this.txtEndYearNAQWA.Size = new System.Drawing.Size(100, 20);
            this.txtEndYearNAQWA.TabIndex = 1;
            // 
            // groupNAWQAqueryTypes
            // 
            this.groupNAWQAqueryTypes.Controls.Add(this.radioButton11);
            this.groupNAWQAqueryTypes.Controls.Add(this.radioButton10);
            this.groupNAWQAqueryTypes.Controls.Add(this.radioButton9);
            this.groupNAWQAqueryTypes.Controls.Add(this.radioButton8);
            this.groupNAWQAqueryTypes.Location = new System.Drawing.Point(398, 6);
            this.groupNAWQAqueryTypes.Name = "groupNAWQAqueryTypes";
            this.groupNAWQAqueryTypes.Size = new System.Drawing.Size(150, 111);
            this.groupNAWQAqueryTypes.TabIndex = 81;
            this.groupNAWQAqueryTypes.TabStop = false;
            this.groupNAWQAqueryTypes.Text = "Query Types";
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(6, 64);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(91, 17);
            this.radioButton11.TabIndex = 3;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "crosstabBasic";
            this.radioButton11.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(6, 87);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(113, 17);
            this.radioButton10.TabIndex = 2;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "crosstabExpanded";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(6, 41);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(98, 17);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "crosstabCounts";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(6, 18);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(49, 17);
            this.radioButton8.TabIndex = 0;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "serial";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // groupNAWQAfileTypes
            // 
            this.groupNAWQAfileTypes.Controls.Add(this.radioButton7);
            this.groupNAWQAfileTypes.Controls.Add(this.radioButton6);
            this.groupNAWQAfileTypes.Controls.Add(this.radioButton5);
            this.groupNAWQAfileTypes.Controls.Add(this.radioButton4);
            this.groupNAWQAfileTypes.Controls.Add(this.radioButton3);
            this.groupNAWQAfileTypes.Location = new System.Drawing.Point(277, 6);
            this.groupNAWQAfileTypes.Name = "groupNAWQAfileTypes";
            this.groupNAWQAfileTypes.Size = new System.Drawing.Size(108, 105);
            this.groupNAWQAfileTypes.TabIndex = 79;
            this.groupNAWQAfileTypes.TabStop = false;
            this.groupNAWQAfileTypes.Text = "File Types";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(52, 25);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(40, 17);
            this.radioButton7.TabIndex = 4;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "xml";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(52, 48);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(44, 17);
            this.radioButton6.TabIndex = 3;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "json";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 71);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(50, 17);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "excel";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 48);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(40, 17);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "tab";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 24);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(42, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "csv";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupNAWQAwaterType
            // 
            this.groupNAWQAwaterType.Controls.Add(this.radioButton2);
            this.groupNAWQAwaterType.Controls.Add(this.radioButton1);
            this.groupNAWQAwaterType.Location = new System.Drawing.Point(16, 6);
            this.groupNAWQAwaterType.Name = "groupNAWQAwaterType";
            this.groupNAWQAwaterType.Size = new System.Drawing.Size(244, 72);
            this.groupNAWQAwaterType.TabIndex = 77;
            this.groupNAWQAwaterType.TabStop = false;
            this.groupNAWQAwaterType.Text = "Water Type";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(14, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(173, 17);
            this.radioButton2.TabIndex = 78;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Surfacewater and Groundwater";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(14, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 17);
            this.radioButton1.TabIndex = 77;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Groundwater";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupNAWQAparameters
            // 
            this.groupNAWQAparameters.Controls.Add(this.listNAWQAparameters);
            this.groupNAWQAparameters.Location = new System.Drawing.Point(71, 157);
            this.groupNAWQAparameters.Name = "groupNAWQAparameters";
            this.groupNAWQAparameters.Size = new System.Drawing.Size(444, 113);
            this.groupNAWQAparameters.TabIndex = 68;
            this.groupNAWQAparameters.TabStop = false;
            this.groupNAWQAparameters.Text = "Parameters";
            // 
            // listNAWQAparameters
            // 
            this.listNAWQAparameters.CheckOnClick = true;
            this.listNAWQAparameters.FormattingEnabled = true;
            this.listNAWQAparameters.Items.AddRange(new object[] {
            "00010  - Temperature_ water_ degrees Celsius",
            "90400 - pH_ water_ area weighted average_ standard units",
            "00400 - pH_ water_ unfiltered_ field_ standard units",
            "00403 - pH_ water_ unfiltered_ laboratory_ standard units",
            "00680 - Organic carbon_ water_ unfiltered_ milligrams per liter",
            "00681 - Organic carbon_ water_ filtered_ milligrams per liter",
            "01047 - Iron(II)_ water_ filtered_ micrograms per liter",
            "99032 - Iron(II)_ water_ unfiltered_ micrograms per liter",
            "99114 - Iron(II)_ water_ filtered_ field_ milligrams per liter",
            "99128 - Iron(II)_ water_ unfiltered_ field_ milligrams per liter"});
            this.listNAWQAparameters.Location = new System.Drawing.Point(23, 19);
            this.listNAWQAparameters.Name = "listNAWQAparameters";
            this.listNAWQAparameters.Size = new System.Drawing.Size(371, 79);
            this.listNAWQAparameters.TabIndex = 65;
            // 
            // btnRunNAWQA
            // 
            this.btnRunNAWQA.Location = new System.Drawing.Point(75, 461);
            this.btnRunNAWQA.Name = "btnRunNAWQA";
            this.btnRunNAWQA.Size = new System.Drawing.Size(322, 23);
            this.btnRunNAWQA.TabIndex = 0;
            this.btnRunNAWQA.Text = "Get All Data";
            this.btnRunNAWQA.UseVisualStyleBackColor = true;
            this.btnRunNAWQA.Click += new System.EventHandler(this.btnRunNAWQA_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.lblHSPFComplete);
            this.tabPage10.Controls.Add(this.btnBrowseHuc);
            this.tabPage10.Controls.Add(this.txtHucNumHSPF);
            this.tabPage10.Controls.Add(this.label79);
            this.tabPage10.Controls.Add(this.btnBrowseCacheHSPF);
            this.tabPage10.Controls.Add(this.label78);
            this.tabPage10.Controls.Add(this.txtCacheFolderHSPF);
            this.tabPage10.Controls.Add(this.btnCancel);
            this.tabPage10.Controls.Add(this.label76);
            this.tabPage10.Controls.Add(this.txtSimEndYearHSPF);
            this.tabPage10.Controls.Add(this.label75);
            this.tabPage10.Controls.Add(this.txtSimStartYearHSPF);
            this.tabPage10.Controls.Add(this.label74);
            this.tabPage10.Controls.Add(this.txtLanduseIgnoreBelowFraction);
            this.tabPage10.Controls.Add(this.label73);
            this.tabPage10.Controls.Add(this.txtMinFlowlineKM);
            this.tabPage10.Controls.Add(this.label72);
            this.tabPage10.Controls.Add(this.txtMinCatchKM2);
            this.tabPage10.Controls.Add(this.btnBrowseProjHSPF);
            this.tabPage10.Controls.Add(this.txtProjectFolderHSPF);
            this.tabPage10.Controls.Add(this.label34);
            this.tabPage10.Controls.Add(this.btnRunHSPF);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(892, 655);
            this.tabPage10.TabIndex = 16;
            this.tabPage10.Text = "HSPF";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // lblHSPFComplete
            // 
            this.lblHSPFComplete.AutoSize = true;
            this.lblHSPFComplete.Location = new System.Drawing.Point(219, 296);
            this.lblHSPFComplete.Name = "lblHSPFComplete";
            this.lblHSPFComplete.Size = new System.Drawing.Size(41, 13);
            this.lblHSPFComplete.TabIndex = 24;
            this.lblHSPFComplete.Text = "label82";
            this.lblHSPFComplete.Visible = false;
            this.lblHSPFComplete.Click += new System.EventHandler(this.lblHSPFComplete_Click);
            // 
            // btnBrowseHuc
            // 
            this.btnBrowseHuc.Location = new System.Drawing.Point(312, 123);
            this.btnBrowseHuc.Name = "btnBrowseHuc";
            this.btnBrowseHuc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseHuc.TabIndex = 23;
            this.btnBrowseHuc.Text = "browse..";
            this.btnBrowseHuc.UseVisualStyleBackColor = true;
            this.btnBrowseHuc.Click += new System.EventHandler(this.btnBrowseHSPF_Click);
            // 
            // txtHucNumHSPF
            // 
            this.txtHucNumHSPF.Location = new System.Drawing.Point(222, 126);
            this.txtHucNumHSPF.Name = "txtHucNumHSPF";
            this.txtHucNumHSPF.Size = new System.Drawing.Size(83, 20);
            this.txtHucNumHSPF.TabIndex = 22;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(219, 110);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(73, 13);
            this.label79.TabIndex = 21;
            this.label79.Text = "HUC Number:";
            // 
            // btnBrowseCacheHSPF
            // 
            this.btnBrowseCacheHSPF.Location = new System.Drawing.Point(686, 73);
            this.btnBrowseCacheHSPF.Name = "btnBrowseCacheHSPF";
            this.btnBrowseCacheHSPF.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheHSPF.TabIndex = 20;
            this.btnBrowseCacheHSPF.Text = "Browse";
            this.btnBrowseCacheHSPF.UseVisualStyleBackColor = true;
            this.btnBrowseCacheHSPF.Click += new System.EventHandler(this.btnBrowseCacheHSPF_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(219, 60);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(86, 13);
            this.label78.TabIndex = 18;
            this.label78.Text = "Cache Directory:";
            // 
            // txtCacheFolderHSPF
            // 
            this.txtCacheFolderHSPF.Location = new System.Drawing.Point(222, 76);
            this.txtCacheFolderHSPF.Name = "txtCacheFolderHSPF";
            this.txtCacheFolderHSPF.Size = new System.Drawing.Size(458, 20);
            this.txtCacheFolderHSPF.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(605, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(502, 207);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(102, 13);
            this.label76.TabIndex = 15;
            this.label76.Text = "Simulation End Year";
            // 
            // txtSimEndYearHSPF
            // 
            this.txtSimEndYearHSPF.Enabled = false;
            this.txtSimEndYearHSPF.Location = new System.Drawing.Point(505, 223);
            this.txtSimEndYearHSPF.Name = "txtSimEndYearHSPF";
            this.txtSimEndYearHSPF.Size = new System.Drawing.Size(54, 20);
            this.txtSimEndYearHSPF.TabIndex = 14;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(502, 167);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(105, 13);
            this.label75.TabIndex = 13;
            this.label75.Text = "Simulation Start Year";
            // 
            // txtSimStartYearHSPF
            // 
            this.txtSimStartYearHSPF.Enabled = false;
            this.txtSimStartYearHSPF.Location = new System.Drawing.Point(505, 183);
            this.txtSimStartYearHSPF.Name = "txtSimStartYearHSPF";
            this.txtSimStartYearHSPF.Size = new System.Drawing.Size(54, 20);
            this.txtSimStartYearHSPF.TabIndex = 12;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(219, 247);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(184, 13);
            this.label74.TabIndex = 11;
            this.label74.Text = "Ignore Landuse Areas Below Fraction";
            // 
            // txtLanduseIgnoreBelowFraction
            // 
            this.txtLanduseIgnoreBelowFraction.Enabled = false;
            this.txtLanduseIgnoreBelowFraction.Location = new System.Drawing.Point(222, 263);
            this.txtLanduseIgnoreBelowFraction.Name = "txtLanduseIgnoreBelowFraction";
            this.txtLanduseIgnoreBelowFraction.Size = new System.Drawing.Size(54, 20);
            this.txtLanduseIgnoreBelowFraction.TabIndex = 10;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(219, 207);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(181, 13);
            this.label73.TabIndex = 9;
            this.label73.Text = "Minimum Flowline Length (kilometers)";
            // 
            // txtMinFlowlineKM
            // 
            this.txtMinFlowlineKM.Enabled = false;
            this.txtMinFlowlineKM.Location = new System.Drawing.Point(222, 223);
            this.txtMinFlowlineKM.Name = "txtMinFlowlineKM";
            this.txtMinFlowlineKM.Size = new System.Drawing.Size(54, 20);
            this.txtMinFlowlineKM.TabIndex = 8;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(219, 167);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(216, 13);
            this.label72.TabIndex = 7;
            this.label72.Text = "Minimum Catchment Size (square kilometers)";
            // 
            // txtMinCatchKM2
            // 
            this.txtMinCatchKM2.Enabled = false;
            this.txtMinCatchKM2.Location = new System.Drawing.Point(222, 183);
            this.txtMinCatchKM2.Name = "txtMinCatchKM2";
            this.txtMinCatchKM2.Size = new System.Drawing.Size(54, 20);
            this.txtMinCatchKM2.TabIndex = 6;
            // 
            // btnBrowseProjHSPF
            // 
            this.btnBrowseProjHSPF.Location = new System.Drawing.Point(686, 27);
            this.btnBrowseProjHSPF.Name = "btnBrowseProjHSPF";
            this.btnBrowseProjHSPF.Size = new System.Drawing.Size(74, 23);
            this.btnBrowseProjHSPF.TabIndex = 3;
            this.btnBrowseProjHSPF.Text = "Browse";
            this.btnBrowseProjHSPF.UseVisualStyleBackColor = true;
            this.btnBrowseProjHSPF.Click += new System.EventHandler(this.btnBrowseProjHSPF_Click);
            // 
            // txtProjectFolderHSPF
            // 
            this.txtProjectFolderHSPF.Location = new System.Drawing.Point(222, 28);
            this.txtProjectFolderHSPF.Name = "txtProjectFolderHSPF";
            this.txtProjectFolderHSPF.Size = new System.Drawing.Size(458, 20);
            this.txtProjectFolderHSPF.TabIndex = 2;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(219, 12);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(125, 13);
            this.label34.TabIndex = 1;
            this.label34.Text = "Current Project Directory:";
            // 
            // btnRunHSPF
            // 
            this.btnRunHSPF.Location = new System.Drawing.Point(505, 124);
            this.btnRunHSPF.Name = "btnRunHSPF";
            this.btnRunHSPF.Size = new System.Drawing.Size(75, 23);
            this.btnRunHSPF.TabIndex = 0;
            this.btnRunHSPF.Text = "Run";
            this.btnRunHSPF.UseVisualStyleBackColor = true;
            this.btnRunHSPF.Click += new System.EventHandler(this.btnRunHSPF_Click);
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.btnRunSWAT);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(892, 655);
            this.tabPage11.TabIndex = 17;
            this.tabPage11.Text = "SWAT";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // btnRunSWAT
            // 
            this.btnRunSWAT.Location = new System.Drawing.Point(402, 88);
            this.btnRunSWAT.Name = "btnRunSWAT";
            this.btnRunSWAT.Size = new System.Drawing.Size(75, 23);
            this.btnRunSWAT.TabIndex = 0;
            this.btnRunSWAT.Text = "Run";
            this.btnRunSWAT.UseVisualStyleBackColor = true;
            this.btnRunSWAT.Click += new System.EventHandler(this.btnRunSWAT_Click);
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.btnRunDelineation);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(892, 655);
            this.tabPage12.TabIndex = 18;
            this.tabPage12.Text = "Delineation";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // btnRunDelineation
            // 
            this.btnRunDelineation.Location = new System.Drawing.Point(548, 62);
            this.btnRunDelineation.Name = "btnRunDelineation";
            this.btnRunDelineation.Size = new System.Drawing.Size(75, 23);
            this.btnRunDelineation.TabIndex = 0;
            this.btnRunDelineation.Text = "Run";
            this.btnRunDelineation.UseVisualStyleBackColor = true;
            this.btnRunDelineation.Click += new System.EventHandler(this.btnRunDelineation_Click);
            // 
            // appManager
            // 
            this.appManager.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager.Directories")));
            this.appManager.DockManager = null;
            this.appManager.HeaderControl = null;
            this.appManager.Legend = null;
            this.appManager.Map = null;
            this.appManager.ProgressHandler = null;
            this.appManager.ShowExtensionsDialog = DotSpatial.Controls.ShowExtensionsDialog.Default;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 741);
            this.Controls.Add(this.D4EMInterface);
            this.Name = "Form1";
            this.Text = "D4EM Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabNWIS.ResumeLayout(false);
            this.tabNWIS.PerformLayout();
            this.groupNWISspecific.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NHDPlus.ResumeLayout(false);
            this.NHDPlus.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabUSGS_Seamless.ResumeLayout(false);
            this.tabUSGS_Seamless.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.D4EMInterface.ResumeLayout(false);
            this.tabNRCSSOIL.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.groupBox38.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.dataset.ResumeLayout(false);
            this.groupBoxNCDCButtons.ResumeLayout(false);
            this.groupBoxNCDCButtons.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataVariablesNCDC)).EndInit();
            this.groupBox25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataStationsNCDC)).EndInit();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.tabNatureServe.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNatureServe)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox28.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.btnDownloadNASS.ResumeLayout(false);
            this.btnDownloadNASS.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.groupBox37.ResumeLayout(false);
            this.groupBox37.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNDBC)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.groupNAWQAchoice.ResumeLayout(false);
            this.groupNAWQAchoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShowNAWQAaverages)).EndInit();
            this.groupNAWQAstatesCounties.ResumeLayout(false);
            this.groupNAWQAstates.ResumeLayout(false);
            this.groupBox35.ResumeLayout(false);
            this.groupNAWQAlatLong.ResumeLayout(false);
            this.groupNAWQAlatLong.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.groupNAQWAdates.ResumeLayout(false);
            this.groupNAQWAdates.PerformLayout();
            this.groupNAWQAstart.ResumeLayout(false);
            this.groupNAWQAstart.PerformLayout();
            this.groupNAQWAend.ResumeLayout(false);
            this.groupNAQWAend.PerformLayout();
            this.groupNAWQAqueryTypes.ResumeLayout(false);
            this.groupNAWQAqueryTypes.PerformLayout();
            this.groupNAWQAfileTypes.ResumeLayout(false);
            this.groupNAWQAfileTypes.PerformLayout();
            this.groupNAWQAwaterType.ResumeLayout(false);
            this.groupNAWQAwaterType.PerformLayout();
            this.groupNAWQAparameters.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tabNWIS;
        private System.Windows.Forms.CheckedListBox listNWIS;
        private System.Windows.Forms.Button btnBrowseProjectNWIS;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtProjectNWIS;
        private System.Windows.Forms.TextBox txtEastNWIS;
        private System.Windows.Forms.TextBox txtWestNWIS;
        private System.Windows.Forms.TextBox txtSouthNWIS;
        private System.Windows.Forms.TextBox txtNorthNWIS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnRunNWIS;
        private System.Windows.Forms.TabPage NHDPlus;
        private System.Windows.Forms.Button btnRemoveNHDPlus;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ListBox listHuc8NHDPlus;
        private System.Windows.Forms.Button btnAddHuc8NHDPlus;
        private System.Windows.Forms.Button btnBrowseProjectNHDPlus;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtProjectFolderNHDPlus;
        private System.Windows.Forms.TextBox txtHUC8NHDPlus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnRunNHDplus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRemoveBasins;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.ListBox listHuc8Basins;
        private System.Windows.Forms.Button btnAddBasins;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHUC8Basins;
        private System.Windows.Forms.TextBox txtProjectFolderBasins;
        private System.Windows.Forms.CheckedListBox boxBasinsDataType;
        private System.Windows.Forms.Button btnBrowseProjectFolderBasins;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnRunBasins;
        private System.Windows.Forms.TabPage tabUSGS_Seamless;
        private System.Windows.Forms.Button btnBrowseProjectNLCD;
        private System.Windows.Forms.CheckedListBox boxLayer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEastUSGS_Seamless;
        private System.Windows.Forms.TextBox txtWestUSGS_Seamless;
        private System.Windows.Forms.TextBox txtSouthUSGS_Seamless;
        private System.Windows.Forms.TextBox txtNorthUSGS_Seamless;
        private System.Windows.Forms.TextBox txtProjectFolderUSGS_Seamless;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunNLCD;
        private System.Windows.Forms.TabControl D4EMInterface;
        private System.Windows.Forms.TabPage tabNRCSSOIL;
        private System.Windows.Forms.Button btnRunNRCSSoil;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox checkedListNHDPlus;
        private System.Windows.Forms.Label labelUSGS_Seamless;
        private System.Windows.Forms.Label labelBasins;
        private System.Windows.Forms.Label labelNHDPlus;
        private System.Windows.Forms.Label labelNWIS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDowloadNWISUsingStationIds;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.ListBox listNWISStations;
        private System.Windows.Forms.Button btnAddStations;
        private System.Windows.Forms.TextBox txtNWISStation;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDownloadNCDC;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabPage tabNatureServe;
        private System.Windows.Forms.Button btnDownloadNatureServe;
        private System.Windows.Forms.CheckedListBox listPollinator;
        private System.Windows.Forms.Button btnBrowseNatureServe;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtProjectFolderNatureServe;
        private System.Windows.Forms.Label labelNatureServe;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnDownloadStoret;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtNorthStoret;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtSouthStoret;
        private System.Windows.Forms.TextBox txtEastStoret;
        private System.Windows.Forms.TextBox txtWestStoret;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtProjectFolderStoret;
        private System.Windows.Forms.Button btnBrowseStoret;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtProjectFolderSoils;
        private System.Windows.Forms.Button btnBrowseSoils;
        private System.Windows.Forms.Button btnPopulateNativeSpeciesTable;
        private System.Windows.Forms.TextBox txtHUC8natureServe;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridViewNatureServe;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btnGetSpreadsheet;
        private System.Windows.Forms.CheckedListBox checkedListAnimals;
        private System.Windows.Forms.Button btnBrowseWDNR;
        private System.Windows.Forms.TextBox txtProjectFolderWDNR;
        private System.Windows.Forms.DataGridView dataStationsNCDC;
        private System.Windows.Forms.Button btnDownloadforSelectedStation;
        private System.Windows.Forms.DataGridView dataVariablesNCDC;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.CheckedListBox outputType;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtEndDay;
        private System.Windows.Forms.TextBox txtEndMonth;
        private System.Windows.Forms.TextBox txtEndYear;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtStartDay;
        private System.Windows.Forms.TextBox txtStartMonth;
        private System.Windows.Forms.TextBox txtStartYear;
        private System.Windows.Forms.TextBox txtProjectFolderNCDC;
        private System.Windows.Forms.CheckedListBox listStatesNCDC;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Button btnBrowseNCDC;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label labelNCDC;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.Button btnGetDataWithinBoxWDNR;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtWestWDNR;
        private System.Windows.Forms.TextBox txtEastWDNR;
        private System.Windows.Forms.TextBox txtSouthWDNR;
        private System.Windows.Forms.TextBox txtNorthWDNR;
        private System.Windows.Forms.Button btnGetDataWithinHuc;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtHucWDNR;
        private System.Windows.Forms.Button btnAddHucWDNR;
        private System.Windows.Forms.Button btnRemoveHucWDNR;
        private System.Windows.Forms.ListBox listHucWDNR;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Button btnGetHuc12WithinHuc8;
        private System.Windows.Forms.ListBox listHuc12WDNR;
        private System.Windows.Forms.Button btnGetDataWithinHuc12;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtHuc8Huc12WDNR;
        private System.Windows.Forms.Button btnAddHUC8Huc12;
        private System.Windows.Forms.Button btnRemoveHuc8Huc12;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.ListBox listHUC8huc12;
        private System.Windows.Forms.TabPage btnDownloadNASS;
        private System.Windows.Forms.Button btnNASSDownload;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txtNorthNASS;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtSouthNASS;
        private System.Windows.Forms.TextBox txtEastNASS;
        private System.Windows.Forms.TextBox txtWestNASS;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtProjectFolderNASS;
        private System.Windows.Forms.Button btnBrowseNASS;
        private System.Windows.Forms.CheckedListBox listYearsNASS;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.Label labelNASS;
        private System.Windows.Forms.Label labelWDNRStatewide;
        private System.Windows.Forms.Label labelWDNRHUC12;
        private System.Windows.Forms.Label labelWDNRHUC8;
        private System.Windows.Forms.Label labelWDNRBB;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button btnRunNDBC;
        private System.Windows.Forms.Label labelNDBC;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtRadiusNDBC;
        private System.Windows.Forms.TextBox txtLongitudeNDBC;
        private System.Windows.Forms.TextBox txtLatitudeNDBC;
        private System.Windows.Forms.DataGridView dataGridViewNDBC;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button btnBrowseNDBC;
        private System.Windows.Forms.TextBox txtProjectFolderNDBC;
        private System.Windows.Forms.Label labelStoret;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txtCacheFolderUSGS_Seamless;
        private System.Windows.Forms.Button btnBrowseCacheNLCD;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtCacheNHDPlus;
        private System.Windows.Forms.Button btnBrowseCacheNHDPlus;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Button btnBrowseCacheBasins;
        private System.Windows.Forms.TextBox txtCacheBasins;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txtCacheNRCSSoil;
        private System.Windows.Forms.Button btnBrowseCacheNRCSSoil;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txtCacheNCDC;
        private System.Windows.Forms.Button btnBrowseCacheNCDC;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox txtCacheNatureServe;
        private System.Windows.Forms.Button btnBrowseCacheNatureServe;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox txtCacheWDNR;
        private System.Windows.Forms.Button btnBrowseCacheWDNR;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txtCacheNASS;
        private System.Windows.Forms.Button btnBrowseCacheNASS;
        private System.Windows.Forms.GroupBox groupBoxNCDCButtons;
        private System.Windows.Forms.CheckedListBox listNWISDataTypesSpecific;
        private System.Windows.Forms.CheckedListBox listStoretDataTypes;
        private System.Windows.Forms.GroupBox groupNWISspecific;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnRunNLDAS;
        private System.Windows.Forms.GroupBox dataset;
        private System.Windows.Forms.CheckedListBox datasetType;
        private System.Windows.Forms.Button btnSelectAllHuc12;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.Button btnRunNAWQA;
        private System.Windows.Forms.GroupBox groupNAWQAparameters;
        private System.Windows.Forms.CheckedListBox listNAWQAparameters;
        private System.Windows.Forms.GroupBox groupNAWQAstates;
        private System.Windows.Forms.GroupBox groupNAWQAwaterType;
        private System.Windows.Forms.GroupBox groupNAWQAfileTypes;
        private System.Windows.Forms.GroupBox groupNAWQAqueryTypes;
        private System.Windows.Forms.GroupBox groupNAQWAdates;
        private System.Windows.Forms.CheckBox chkBoxWater;
        private System.Windows.Forms.GroupBox groupNAWQAstart;
        private System.Windows.Forms.GroupBox groupNAQWAend;
        private System.Windows.Forms.TextBox txtStartYearNAQWA;
        private System.Windows.Forms.TextBox txtEndYearNAQWA;
        private System.Windows.Forms.CheckedListBox listCounties;
        private System.Windows.Forms.GroupBox groupBox35;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.ListBox listNAWQAstates;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheFolderNAWQA;
        private System.Windows.Forms.Button btnBrowseCacheFolderNAWQA;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProjectFolderNAWQA;
        private System.Windows.Forms.Button btnBrowseProjectFolderNAWQA;
        private System.Windows.Forms.Label labelNAWQA;
        private System.Windows.Forms.GroupBox groupNAWQAlatLong;
        private System.Windows.Forms.TextBox txtNAWQAlng;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtNAWQAlat;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupNAWQAstatesCounties;
        private System.Windows.Forms.Label labelNAWQAlatLongCounty;
        private System.Windows.Forms.Button btnDetermineCounty;
        private System.Windows.Forms.DataGridView gridShowNAWQAaverages;
        private System.Windows.Forms.GroupBox groupNAWQAchoice;
        private System.Windows.Forms.RadioButton radioUseLatLong;
        private System.Windows.Forms.RadioButton radioUseStatesCounties;
        private System.Windows.Forms.Button btnGetNAWQAaverageStdDev;
        private System.Windows.Forms.Button btnCreateHUCNativeFishSpeciesFile;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Button btnRunHSPF;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.Button btnRunSWAT;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.Button btnRunDelineation;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNDBCyear;
        private System.Windows.Forms.TextBox txtNDBCStationID;
        private System.Windows.Forms.Button btnNDBChistoricalData;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txtNorth;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox txtSouth;
        private System.Windows.Forms.TextBox txtEast;
        private System.Windows.Forms.TextBox txtWest;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtProjectFolderHSPF;
        private System.Windows.Forms.Button btnBrowseProjHSPF;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox txtSimEndYearHSPF;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txtSimStartYearHSPF;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txtLanduseIgnoreBelowFraction;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txtMinFlowlineKM;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txtMinCatchKM2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowseCacheHSPF;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox txtCacheFolderHSPF;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox txtHucNumHSPF;
        private DotSpatial.Controls.AppManager appManager;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtLongitudeNLDAS;
        private System.Windows.Forms.TextBox txtLatitudeNLDAS;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txtCacheFolderNLDAS;
        private System.Windows.Forms.TextBox txtProjectFolderNLDAS;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txtHucNLDAS;
        private System.Windows.Forms.Label lblNLDAS;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowseHuc;
        private System.Windows.Forms.Label lblHSPFComplete;
    }
}

