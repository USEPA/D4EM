namespace D4EM_WDNR
{
    partial class WDNRBox
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
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.checkedListAnimals = new System.Windows.Forms.CheckedListBox();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCache = new System.Windows.Forms.TextBox();
            this.btnBrowseCache = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectFolderWDNR = new System.Windows.Forms.TextBox();
            this.btnBrowseWDNR = new System.Windows.Forms.Button();
            this.btnGetStatewideData = new System.Windows.Forms.Button();
            this.btnGetDataWithinBoxWDNR = new System.Windows.Forms.Button();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.labelWDNRBB = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWestWDNR = new System.Windows.Forms.TextBox();
            this.txtEastWDNR = new System.Windows.Forms.TextBox();
            this.txtSouthWDNR = new System.Windows.Forms.TextBox();
            this.txtNorthWDNR = new System.Windows.Forms.TextBox();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.labelWDNRHUC8 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.btnGetDataWithinHuc = new System.Windows.Forms.Button();
            this.txtHucWDNR = new System.Windows.Forms.TextBox();
            this.btnAddHucWDNR = new System.Windows.Forms.Button();
            this.btnRemoveHucWDNR = new System.Windows.Forms.Button();
            this.listHucWDNR = new System.Windows.Forms.ListBox();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
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
            this.labelWDNRStatewide = new System.Windows.Forms.Label();
            this.btnWDNRLoadDataToMap = new System.Windows.Forms.Button();
            this.groupBox28.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.checkedListAnimals);
            this.groupBox28.Location = new System.Drawing.Point(493, 12);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(139, 100);
            this.groupBox28.TabIndex = 12;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Animal";
            // 
            // checkedListAnimals
            // 
            this.checkedListAnimals.CheckOnClick = true;
            this.checkedListAnimals.FormattingEnabled = true;
            this.checkedListAnimals.Items.AddRange(new object[] {
            "Beef",
            "Chickens",
            "Dairy",
            "Swine",
            "Turkeys"});
            this.checkedListAnimals.Location = new System.Drawing.Point(6, 15);
            this.checkedListAnimals.Name = "checkedListAnimals";
            this.checkedListAnimals.Size = new System.Drawing.Size(120, 79);
            this.checkedListAnimals.TabIndex = 1;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.label2);
            this.groupBox27.Controls.Add(this.txtCache);
            this.groupBox27.Controls.Add(this.btnBrowseCache);
            this.groupBox27.Controls.Add(this.label1);
            this.groupBox27.Controls.Add(this.txtProjectFolderWDNR);
            this.groupBox27.Controls.Add(this.btnBrowseWDNR);
            this.groupBox27.Location = new System.Drawing.Point(26, 12);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(429, 81);
            this.groupBox27.TabIndex = 11;
            this.groupBox27.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cache Folder";
            // 
            // txtCache
            // 
            this.txtCache.Location = new System.Drawing.Point(94, 46);
            this.txtCache.Name = "txtCache";
            this.txtCache.Size = new System.Drawing.Size(222, 20);
            this.txtCache.TabIndex = 5;
            // 
            // btnBrowseCache
            // 
            this.btnBrowseCache.Location = new System.Drawing.Point(330, 45);
            this.btnBrowseCache.Name = "btnBrowseCache";
            this.btnBrowseCache.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCache.TabIndex = 6;
            this.btnBrowseCache.Text = "Browse";
            this.btnBrowseCache.UseVisualStyleBackColor = true;
            this.btnBrowseCache.Click += new System.EventHandler(this.btnBrowseCache_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Project Folder";
            // 
            // txtProjectFolderWDNR
            // 
            this.txtProjectFolderWDNR.Location = new System.Drawing.Point(93, 16);
            this.txtProjectFolderWDNR.Name = "txtProjectFolderWDNR";
            this.txtProjectFolderWDNR.Size = new System.Drawing.Size(222, 20);
            this.txtProjectFolderWDNR.TabIndex = 2;
            // 
            // btnBrowseWDNR
            // 
            this.btnBrowseWDNR.Location = new System.Drawing.Point(329, 15);
            this.btnBrowseWDNR.Name = "btnBrowseWDNR";
            this.btnBrowseWDNR.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseWDNR.TabIndex = 3;
            this.btnBrowseWDNR.Text = "Browse";
            this.btnBrowseWDNR.UseVisualStyleBackColor = true;
            this.btnBrowseWDNR.Click += new System.EventHandler(this.btnBrowseWDNR_Click_1);
            // 
            // btnGetStatewideData
            // 
            this.btnGetStatewideData.Location = new System.Drawing.Point(638, 28);
            this.btnGetStatewideData.Name = "btnGetStatewideData";
            this.btnGetStatewideData.Size = new System.Drawing.Size(123, 78);
            this.btnGetStatewideData.TabIndex = 10;
            this.btnGetStatewideData.Text = "Get Statewide Data";
            this.btnGetStatewideData.UseVisualStyleBackColor = true;
            this.btnGetStatewideData.Click += new System.EventHandler(this.btnGetStatewideData_Click);
            // 
            // btnGetDataWithinBoxWDNR
            // 
            this.btnGetDataWithinBoxWDNR.Location = new System.Drawing.Point(7, 125);
            this.btnGetDataWithinBoxWDNR.Name = "btnGetDataWithinBoxWDNR";
            this.btnGetDataWithinBoxWDNR.Size = new System.Drawing.Size(194, 23);
            this.btnGetDataWithinBoxWDNR.TabIndex = 14;
            this.btnGetDataWithinBoxWDNR.Text = "Get Data Within Bounding Box";
            this.btnGetDataWithinBoxWDNR.UseVisualStyleBackColor = true;
            this.btnGetDataWithinBoxWDNR.Click += new System.EventHandler(this.btnGetDataWithinBoxWDNR_Click);
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
            this.groupBox29.Location = new System.Drawing.Point(26, 99);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(207, 177);
            this.groupBox29.TabIndex = 13;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Bounding Box";
            // 
            // labelWDNRBB
            // 
            this.labelWDNRBB.AutoSize = true;
            this.labelWDNRBB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelWDNRBB.Location = new System.Drawing.Point(3, 161);
            this.labelWDNRBB.Name = "labelWDNRBB";
            this.labelWDNRBB.Size = new System.Drawing.Size(35, 13);
            this.labelWDNRBB.TabIndex = 15;
            this.labelWDNRBB.Text = "label1";
            this.labelWDNRBB.Visible = false;
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
            this.groupBox31.Location = new System.Drawing.Point(295, 130);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(337, 174);
            this.groupBox31.TabIndex = 67;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "HUC-8";
            // 
            // labelWDNRHUC8
            // 
            this.labelWDNRHUC8.AutoSize = true;
            this.labelWDNRHUC8.Location = new System.Drawing.Point(11, 158);
            this.labelWDNRHUC8.Name = "labelWDNRHUC8";
            this.labelWDNRHUC8.Size = new System.Drawing.Size(35, 13);
            this.labelWDNRHUC8.TabIndex = 62;
            this.labelWDNRHUC8.Text = "label1";
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
            this.btnGetDataWithinHuc.Location = new System.Drawing.Point(26, 123);
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
            this.linkLabel5.Click += new System.EventHandler(this.clickHUC8_Click);
            // 
            // groupBox30
            // 
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
            this.groupBox30.Location = new System.Drawing.Point(32, 310);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(617, 143);
            this.groupBox30.TabIndex = 68;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "HUC-12";
            // 
            // labelWDNRHUC12
            // 
            this.labelWDNRHUC12.AutoSize = true;
            this.labelWDNRHUC12.Location = new System.Drawing.Point(160, 127);
            this.labelWDNRHUC12.Name = "labelWDNRHUC12";
            this.labelWDNRHUC12.Size = new System.Drawing.Size(35, 13);
            this.labelWDNRHUC12.TabIndex = 65;
            this.labelWDNRHUC12.Text = "label1";
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
            this.btnRemoveHuc8Huc12.Click += new System.EventHandler(this.btnRemoveHuc8Huc12_Click_1);
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
            this.btnGetDataWithinHuc12.Click += new System.EventHandler(this.btnGetDataWithinHuc12_Click_1);
            // 
            // listHuc12WDNR
            // 
            this.listHuc12WDNR.FormattingEnabled = true;
            this.listHuc12WDNR.Location = new System.Drawing.Point(396, 19);
            this.listHuc12WDNR.Name = "listHuc12WDNR";
            this.listHuc12WDNR.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listHuc12WDNR.Size = new System.Drawing.Size(205, 56);
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
            this.btnGetHuc12WithinHuc8.Click += new System.EventHandler(this.btnGetHuc12WithinHuc8_Click_1);
            // 
            // labelWDNRStatewide
            // 
            this.labelWDNRStatewide.AutoSize = true;
            this.labelWDNRStatewide.Location = new System.Drawing.Point(266, 99);
            this.labelWDNRStatewide.Name = "labelWDNRStatewide";
            this.labelWDNRStatewide.Size = new System.Drawing.Size(35, 13);
            this.labelWDNRStatewide.TabIndex = 69;
            this.labelWDNRStatewide.Text = "label1";
            this.labelWDNRStatewide.Visible = false;
            // 
            // btnWDNRLoadDataToMap
            // 
            this.btnWDNRLoadDataToMap.Location = new System.Drawing.Point(230, 476);
            this.btnWDNRLoadDataToMap.Name = "btnWDNRLoadDataToMap";
            this.btnWDNRLoadDataToMap.Size = new System.Drawing.Size(253, 23);
            this.btnWDNRLoadDataToMap.TabIndex = 70;
            this.btnWDNRLoadDataToMap.Text = "Load Data to Map";
            this.btnWDNRLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnWDNRLoadDataToMap.Visible = false;
            this.btnWDNRLoadDataToMap.Click += new System.EventHandler(this.btnWDNRLoadDataToMap_Click);
            // 
            // WDNRBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 511);
            this.Controls.Add(this.btnWDNRLoadDataToMap);
            this.Controls.Add(this.labelWDNRStatewide);
            this.Controls.Add(this.groupBox30);
            this.Controls.Add(this.groupBox31);
            this.Controls.Add(this.groupBox29);
            this.Controls.Add(this.groupBox28);
            this.Controls.Add(this.groupBox27);
            this.Controls.Add(this.btnGetStatewideData);
            this.Name = "WDNRBox";
            this.Text = "WDNRBox";
            this.Load += new System.EventHandler(this.WDNRBox_Load);
            this.groupBox28.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.CheckedListBox checkedListAnimals;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.TextBox txtProjectFolderWDNR;
        private System.Windows.Forms.Button btnBrowseWDNR;
        private System.Windows.Forms.Button btnGetStatewideData;
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
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnGetDataWithinHuc;
        private System.Windows.Forms.TextBox txtHucWDNR;
        private System.Windows.Forms.Button btnAddHucWDNR;
        private System.Windows.Forms.Button btnRemoveHucWDNR;
        private System.Windows.Forms.ListBox listHucWDNR;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Button btnAddHUC8Huc12;
        private System.Windows.Forms.Button btnRemoveHuc8Huc12;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.ListBox listHUC8huc12;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtHuc8Huc12WDNR;
        private System.Windows.Forms.Button btnGetDataWithinHuc12;
        private System.Windows.Forms.ListBox listHuc12WDNR;
        private System.Windows.Forms.Button btnGetHuc12WithinHuc8;
        private System.Windows.Forms.Label labelWDNRBB;
        private System.Windows.Forms.Label labelWDNRHUC8;
        private System.Windows.Forms.Label labelWDNRHUC12;
        private System.Windows.Forms.Label labelWDNRStatewide;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCache;
        private System.Windows.Forms.Button btnBrowseCache;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWDNRLoadDataToMap;

    }
}