namespace D4EM_NAWQA
{
    partial class NAWQABox
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
            this.groupNAWQAchoice = new System.Windows.Forms.GroupBox();
            this.radioUseCountiesFromMap = new System.Windows.Forms.RadioButton();
            this.radioUseLatLong = new System.Windows.Forms.RadioButton();
            this.radioUseStatesCounties = new System.Windows.Forms.RadioButton();
            this.groupNAWQAstatesCounties = new System.Windows.Forms.GroupBox();
            this.groupNAWQAstates = new System.Windows.Forms.GroupBox();
            this.listNAWQAstates = new System.Windows.Forms.ListBox();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.listCounties = new System.Windows.Forms.CheckedListBox();
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
            this.btnGetNAWQAaverageStdDev = new System.Windows.Forms.Button();
            this.gridShowNAWQAaverages = new System.Windows.Forms.DataGridView();
            this.groupNAWQAlatLong = new System.Windows.Forms.GroupBox();
            this.btnDetermineCounty = new System.Windows.Forms.Button();
            this.labelNAWQAlatLongCounty = new System.Windows.Forms.Label();
            this.txtNAWQAlng = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtNAWQAlat = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.labelNAWQA = new System.Windows.Forms.Label();
            this.btnRunNAWQA = new System.Windows.Forms.Button();
            this.groupNAWQAcountiesFromMap = new System.Windows.Forms.GroupBox();
            this.listCountiesFromMap = new System.Windows.Forms.CheckedListBox();
            this.groupNAWQAchoice.SuspendLayout();
            this.groupNAWQAstatesCounties.SuspendLayout();
            this.groupNAWQAstates.SuspendLayout();
            this.groupBox35.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.groupNAQWAdates.SuspendLayout();
            this.groupNAWQAstart.SuspendLayout();
            this.groupNAQWAend.SuspendLayout();
            this.groupNAWQAqueryTypes.SuspendLayout();
            this.groupNAWQAfileTypes.SuspendLayout();
            this.groupNAWQAwaterType.SuspendLayout();
            this.groupNAWQAparameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShowNAWQAaverages)).BeginInit();
            this.groupNAWQAlatLong.SuspendLayout();
            this.groupNAWQAcountiesFromMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupNAWQAchoice
            // 
            this.groupNAWQAchoice.Controls.Add(this.radioUseCountiesFromMap);
            this.groupNAWQAchoice.Controls.Add(this.radioUseLatLong);
            this.groupNAWQAchoice.Controls.Add(this.radioUseStatesCounties);
            this.groupNAWQAchoice.Location = new System.Drawing.Point(30, 358);
            this.groupNAWQAchoice.Name = "groupNAWQAchoice";
            this.groupNAWQAchoice.Size = new System.Drawing.Size(346, 85);
            this.groupNAWQAchoice.TabIndex = 104;
            this.groupNAWQAchoice.TabStop = false;
            // 
            // radioUseCountiesFromMap
            // 
            this.radioUseCountiesFromMap.AutoSize = true;
            this.radioUseCountiesFromMap.Checked = true;
            this.radioUseCountiesFromMap.Location = new System.Drawing.Point(13, 58);
            this.radioUseCountiesFromMap.Name = "radioUseCountiesFromMap";
            this.radioUseCountiesFromMap.Size = new System.Drawing.Size(297, 17);
            this.radioUseCountiesFromMap.TabIndex = 111;
            this.radioUseCountiesFromMap.TabStop = true;
            this.radioUseCountiesFromMap.Text = "Download NAWQA using Counties selected from the Map";
            this.radioUseCountiesFromMap.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.radioUseCountiesFromMap.UseVisualStyleBackColor = true;
            this.radioUseCountiesFromMap.CheckedChanged += new System.EventHandler(this.radioUseCountiesFromMap_CheckedChanged);
            // 
            // radioUseLatLong
            // 
            this.radioUseLatLong.AutoSize = true;
            this.radioUseLatLong.Location = new System.Drawing.Point(13, 15);
            this.radioUseLatLong.Name = "radioUseLatLong";
            this.radioUseLatLong.Size = new System.Drawing.Size(192, 17);
            this.radioUseLatLong.TabIndex = 1;
            this.radioUseLatLong.Text = "Download NAWQA using Lat/Long";
            this.radioUseLatLong.UseVisualStyleBackColor = true;
            this.radioUseLatLong.CheckedChanged += new System.EventHandler(this.radioUseLatLong_CheckedChanged_1);
            // 
            // radioUseStatesCounties
            // 
            this.radioUseStatesCounties.AutoSize = true;
            this.radioUseStatesCounties.Location = new System.Drawing.Point(13, 35);
            this.radioUseStatesCounties.Name = "radioUseStatesCounties";
            this.radioUseStatesCounties.Size = new System.Drawing.Size(311, 17);
            this.radioUseStatesCounties.TabIndex = 0;
            this.radioUseStatesCounties.Text = "Download NAWQA using States/Counties selected manually";
            this.radioUseStatesCounties.UseVisualStyleBackColor = true;
            this.radioUseStatesCounties.CheckedChanged += new System.EventHandler(this.radioUseStatesCounties_CheckedChanged_1);
            // 
            // groupNAWQAstatesCounties
            // 
            this.groupNAWQAstatesCounties.Controls.Add(this.groupNAWQAstates);
            this.groupNAWQAstatesCounties.Controls.Add(this.groupBox35);
            this.groupNAWQAstatesCounties.Location = new System.Drawing.Point(404, 356);
            this.groupNAWQAstatesCounties.Name = "groupNAWQAstatesCounties";
            this.groupNAWQAstatesCounties.Size = new System.Drawing.Size(453, 148);
            this.groupNAWQAstatesCounties.TabIndex = 103;
            this.groupNAWQAstatesCounties.TabStop = false;
            this.groupNAWQAstatesCounties.Text = "Download NAWQA using States/Counties";
            this.groupNAWQAstatesCounties.Visible = false;
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
            this.listNAWQAstates.SelectedIndexChanged += new System.EventHandler(this.listNAWQAstates_SelectedIndexChanged);
            // 
            // groupBox35
            // 
            this.groupBox35.Controls.Add(this.listCounties);
            this.groupBox35.Location = new System.Drawing.Point(230, 17);
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.Size = new System.Drawing.Size(217, 120);
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
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.label1);
            this.groupBox36.Controls.Add(this.txtCacheFolderNAWQA);
            this.groupBox36.Controls.Add(this.btnBrowseCacheFolderNAWQA);
            this.groupBox36.Controls.Add(this.label14);
            this.groupBox36.Controls.Add(this.txtProjectFolderNAWQA);
            this.groupBox36.Controls.Add(this.btnBrowseProjectFolderNAWQA);
            this.groupBox36.Location = new System.Drawing.Point(30, 256);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Size = new System.Drawing.Size(457, 94);
            this.groupBox36.TabIndex = 102;
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
            // 
            // groupNAQWAdates
            // 
            this.groupNAQWAdates.Controls.Add(this.chkBoxWater);
            this.groupNAQWAdates.Controls.Add(this.groupNAWQAstart);
            this.groupNAQWAdates.Controls.Add(this.groupNAQWAend);
            this.groupNAQWAdates.Location = new System.Drawing.Point(590, 12);
            this.groupNAQWAdates.Name = "groupNAQWAdates";
            this.groupNAQWAdates.Size = new System.Drawing.Size(208, 186);
            this.groupNAQWAdates.TabIndex = 101;
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
            this.groupNAWQAqueryTypes.Location = new System.Drawing.Point(398, 20);
            this.groupNAWQAqueryTypes.Name = "groupNAWQAqueryTypes";
            this.groupNAWQAqueryTypes.Size = new System.Drawing.Size(150, 111);
            this.groupNAWQAqueryTypes.TabIndex = 100;
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
            this.groupNAWQAfileTypes.Location = new System.Drawing.Point(277, 20);
            this.groupNAWQAfileTypes.Name = "groupNAWQAfileTypes";
            this.groupNAWQAfileTypes.Size = new System.Drawing.Size(108, 105);
            this.groupNAWQAfileTypes.TabIndex = 99;
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
            this.groupNAWQAwaterType.Location = new System.Drawing.Point(16, 20);
            this.groupNAWQAwaterType.Name = "groupNAWQAwaterType";
            this.groupNAWQAwaterType.Size = new System.Drawing.Size(244, 72);
            this.groupNAWQAwaterType.TabIndex = 98;
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
            this.groupNAWQAparameters.Location = new System.Drawing.Point(30, 137);
            this.groupNAWQAparameters.Name = "groupNAWQAparameters";
            this.groupNAWQAparameters.Size = new System.Drawing.Size(444, 113);
            this.groupNAWQAparameters.TabIndex = 97;
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
            // btnGetNAWQAaverageStdDev
            // 
            this.btnGetNAWQAaverageStdDev.Location = new System.Drawing.Point(63, 566);
            this.btnGetNAWQAaverageStdDev.Name = "btnGetNAWQAaverageStdDev";
            this.btnGetNAWQAaverageStdDev.Size = new System.Drawing.Size(322, 23);
            this.btnGetNAWQAaverageStdDev.TabIndex = 109;
            this.btnGetNAWQAaverageStdDev.Text = "Get Average And Standard Deviation";
            this.btnGetNAWQAaverageStdDev.UseVisualStyleBackColor = true;
            this.btnGetNAWQAaverageStdDev.Click += new System.EventHandler(this.btnGetNAWQAaverageStdDev_Click);
            // 
            // gridShowNAWQAaverages
            // 
            this.gridShowNAWQAaverages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShowNAWQAaverages.Location = new System.Drawing.Point(12, 606);
            this.gridShowNAWQAaverages.Name = "gridShowNAWQAaverages";
            this.gridShowNAWQAaverages.Size = new System.Drawing.Size(856, 124);
            this.gridShowNAWQAaverages.TabIndex = 108;
            this.gridShowNAWQAaverages.Visible = false;
            // 
            // groupNAWQAlatLong
            // 
            this.groupNAWQAlatLong.Controls.Add(this.btnDetermineCounty);
            this.groupNAWQAlatLong.Controls.Add(this.labelNAWQAlatLongCounty);
            this.groupNAWQAlatLong.Controls.Add(this.txtNAWQAlng);
            this.groupNAWQAlatLong.Controls.Add(this.label22);
            this.groupNAWQAlatLong.Controls.Add(this.txtNAWQAlat);
            this.groupNAWQAlatLong.Controls.Add(this.label21);
            this.groupNAWQAlatLong.Location = new System.Drawing.Point(39, 452);
            this.groupNAWQAlatLong.Name = "groupNAWQAlatLong";
            this.groupNAWQAlatLong.Size = new System.Drawing.Size(337, 79);
            this.groupNAWQAlatLong.TabIndex = 107;
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
            this.btnDetermineCounty.Click += new System.EventHandler(this.btnDetermineCounty_Click_1);
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
            this.label22.Location = new System.Drawing.Point(127, 17);
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
            this.labelNAWQA.Location = new System.Drawing.Point(391, 542);
            this.labelNAWQA.Name = "labelNAWQA";
            this.labelNAWQA.Size = new System.Drawing.Size(41, 13);
            this.labelNAWQA.TabIndex = 106;
            this.labelNAWQA.Text = "label21";
            this.labelNAWQA.Visible = false;
            // 
            // btnRunNAWQA
            // 
            this.btnRunNAWQA.Location = new System.Drawing.Point(63, 537);
            this.btnRunNAWQA.Name = "btnRunNAWQA";
            this.btnRunNAWQA.Size = new System.Drawing.Size(322, 23);
            this.btnRunNAWQA.TabIndex = 105;
            this.btnRunNAWQA.Text = "Get All Data";
            this.btnRunNAWQA.UseVisualStyleBackColor = true;
            this.btnRunNAWQA.Click += new System.EventHandler(this.btnRunNAWQA_Click);
            // 
            // groupNAWQAcountiesFromMap
            // 
            this.groupNAWQAcountiesFromMap.Controls.Add(this.listCountiesFromMap);
            this.groupNAWQAcountiesFromMap.Location = new System.Drawing.Point(562, 204);
            this.groupNAWQAcountiesFromMap.Name = "groupNAWQAcountiesFromMap";
            this.groupNAWQAcountiesFromMap.Size = new System.Drawing.Size(236, 139);
            this.groupNAWQAcountiesFromMap.TabIndex = 112;
            this.groupNAWQAcountiesFromMap.TabStop = false;
            this.groupNAWQAcountiesFromMap.Text = "Download NAWQA using Counties Selected From the Map";
            // 
            // listCountiesFromMap
            // 
            this.listCountiesFromMap.CheckOnClick = true;
            this.listCountiesFromMap.FormattingEnabled = true;
            this.listCountiesFromMap.Location = new System.Drawing.Point(17, 34);
            this.listCountiesFromMap.Name = "listCountiesFromMap";
            this.listCountiesFromMap.Size = new System.Drawing.Size(198, 94);
            this.listCountiesFromMap.TabIndex = 0;
            this.listCountiesFromMap.SelectedIndexChanged += new System.EventHandler(this.listCountiesFromMap_SelectedIndexChanged);
            // 
            // NAWQABox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 742);
            this.Controls.Add(this.groupNAWQAcountiesFromMap);
            this.Controls.Add(this.btnGetNAWQAaverageStdDev);
            this.Controls.Add(this.gridShowNAWQAaverages);
            this.Controls.Add(this.groupNAWQAlatLong);
            this.Controls.Add(this.labelNAWQA);
            this.Controls.Add(this.btnRunNAWQA);
            this.Controls.Add(this.groupNAWQAchoice);
            this.Controls.Add(this.groupNAWQAstatesCounties);
            this.Controls.Add(this.groupBox36);
            this.Controls.Add(this.groupNAQWAdates);
            this.Controls.Add(this.groupNAWQAqueryTypes);
            this.Controls.Add(this.groupNAWQAfileTypes);
            this.Controls.Add(this.groupNAWQAwaterType);
            this.Controls.Add(this.groupNAWQAparameters);
            this.Name = "NAWQABox";
            this.Text = "NAWQABox";
            this.Load += new System.EventHandler(this.NAWQABox_Load);
            this.groupNAWQAchoice.ResumeLayout(false);
            this.groupNAWQAchoice.PerformLayout();
            this.groupNAWQAstatesCounties.ResumeLayout(false);
            this.groupNAWQAstates.ResumeLayout(false);
            this.groupBox35.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.gridShowNAWQAaverages)).EndInit();
            this.groupNAWQAlatLong.ResumeLayout(false);
            this.groupNAWQAlatLong.PerformLayout();
            this.groupNAWQAcountiesFromMap.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupNAWQAchoice;
        private System.Windows.Forms.RadioButton radioUseLatLong;
        private System.Windows.Forms.RadioButton radioUseStatesCounties;
        private System.Windows.Forms.GroupBox groupNAWQAstatesCounties;
        private System.Windows.Forms.GroupBox groupNAWQAstates;
        private System.Windows.Forms.ListBox listNAWQAstates;
        private System.Windows.Forms.GroupBox groupBox35;
        private System.Windows.Forms.CheckedListBox listCounties;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheFolderNAWQA;
        private System.Windows.Forms.Button btnBrowseCacheFolderNAWQA;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProjectFolderNAWQA;
        private System.Windows.Forms.Button btnBrowseProjectFolderNAWQA;
        private System.Windows.Forms.GroupBox groupNAQWAdates;
        private System.Windows.Forms.CheckBox chkBoxWater;
        private System.Windows.Forms.GroupBox groupNAWQAstart;
        private System.Windows.Forms.TextBox txtStartYearNAQWA;
        private System.Windows.Forms.GroupBox groupNAQWAend;
        private System.Windows.Forms.TextBox txtEndYearNAQWA;
        private System.Windows.Forms.GroupBox groupNAWQAqueryTypes;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.GroupBox groupNAWQAfileTypes;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupNAWQAwaterType;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupNAWQAparameters;
        private System.Windows.Forms.CheckedListBox listNAWQAparameters;
        private System.Windows.Forms.Button btnGetNAWQAaverageStdDev;
        private System.Windows.Forms.DataGridView gridShowNAWQAaverages;
        private System.Windows.Forms.GroupBox groupNAWQAlatLong;
        private System.Windows.Forms.Button btnDetermineCounty;
        private System.Windows.Forms.Label labelNAWQAlatLongCounty;
        private System.Windows.Forms.TextBox txtNAWQAlng;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtNAWQAlat;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labelNAWQA;
        private System.Windows.Forms.Button btnRunNAWQA;
        private System.Windows.Forms.RadioButton radioUseCountiesFromMap;
        private System.Windows.Forms.GroupBox groupNAWQAcountiesFromMap;
        private System.Windows.Forms.CheckedListBox listCountiesFromMap;
    }
}