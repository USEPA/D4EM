namespace D4EM_NHDPlus
{
    partial class NHDPlusBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.labelNHDPlus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCacheFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseCache = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtProjectFolderNHDPlus = new System.Windows.Forms.TextBox();
            this.btnBrowseProjectNHDPlus = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListNHDPlus = new System.Windows.Forms.CheckedListBox();
            this.btnNHDPlusLoadDataToMap = new System.Windows.Forms.Button();
            this.btnRunNHDplus = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listHuc8NHDPlus = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtHUC8NHDPlus = new System.Windows.Forms.TextBox();
            this.btnAddHuc8NHDPlus = new System.Windows.Forms.Button();
            this.btnRemoveNHDPlus = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupStreamIDs = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.listStreamIDs = new System.Windows.Forms.CheckedListBox();
            this.btnManitowoc = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCache = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGetWatershed = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupStreamIDs.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNHDPlus
            // 
            this.labelNHDPlus.AutoSize = true;
            this.labelNHDPlus.Location = new System.Drawing.Point(127, 296);
            this.labelNHDPlus.Name = "labelNHDPlus";
            this.labelNHDPlus.Size = new System.Drawing.Size(35, 13);
            this.labelNHDPlus.TabIndex = 76;
            this.labelNHDPlus.Text = "label1";
            this.labelNHDPlus.Visible = false;
            this.labelNHDPlus.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 344);
            this.tabControl1.TabIndex = 81;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnNHDPlusLoadDataToMap);
            this.tabPage1.Controls.Add(this.btnRunNHDplus);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(641, 318);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NHDPlus";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCacheFolder);
            this.groupBox3.Controls.Add(this.btnBrowseCache);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txtProjectFolderNHDPlus);
            this.groupBox3.Controls.Add(this.btnBrowseProjectNHDPlus);
            this.groupBox3.Location = new System.Drawing.Point(60, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(498, 90);
            this.groupBox3.TabIndex = 84;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Cache Folder";
            // 
            // txtCacheFolder
            // 
            this.txtCacheFolder.Location = new System.Drawing.Point(105, 54);
            this.txtCacheFolder.Name = "txtCacheFolder";
            this.txtCacheFolder.Size = new System.Drawing.Size(259, 20);
            this.txtCacheFolder.TabIndex = 71;
            // 
            // btnBrowseCache
            // 
            this.btnBrowseCache.Location = new System.Drawing.Point(370, 54);
            this.btnBrowseCache.Name = "btnBrowseCache";
            this.btnBrowseCache.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCache.TabIndex = 73;
            this.btnBrowseCache.Text = "Browse";
            this.btnBrowseCache.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(29, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 69;
            this.label19.Text = "Project Folder";
            // 
            // txtProjectFolderNHDPlus
            // 
            this.txtProjectFolderNHDPlus.Location = new System.Drawing.Point(105, 27);
            this.txtProjectFolderNHDPlus.Name = "txtProjectFolderNHDPlus";
            this.txtProjectFolderNHDPlus.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNHDPlus.TabIndex = 68;
            // 
            // btnBrowseProjectNHDPlus
            // 
            this.btnBrowseProjectNHDPlus.Location = new System.Drawing.Point(370, 27);
            this.btnBrowseProjectNHDPlus.Name = "btnBrowseProjectNHDPlus";
            this.btnBrowseProjectNHDPlus.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectNHDPlus.TabIndex = 70;
            this.btnBrowseProjectNHDPlus.Text = "Browse";
            this.btnBrowseProjectNHDPlus.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListNHDPlus);
            this.groupBox1.Location = new System.Drawing.Point(351, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 129);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NHDPlus Data Types";
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
            this.checkedListNHDPlus.Location = new System.Drawing.Point(26, 19);
            this.checkedListNHDPlus.Name = "checkedListNHDPlus";
            this.checkedListNHDPlus.Size = new System.Drawing.Size(245, 94);
            this.checkedListNHDPlus.TabIndex = 75;
            // 
            // btnNHDPlusLoadDataToMap
            // 
            this.btnNHDPlusLoadDataToMap.Location = new System.Drawing.Point(161, 289);
            this.btnNHDPlusLoadDataToMap.Name = "btnNHDPlusLoadDataToMap";
            this.btnNHDPlusLoadDataToMap.Size = new System.Drawing.Size(163, 23);
            this.btnNHDPlusLoadDataToMap.TabIndex = 82;
            this.btnNHDPlusLoadDataToMap.Text = "Load Data to Map";
            this.btnNHDPlusLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnNHDPlusLoadDataToMap.Visible = false;
            this.btnNHDPlusLoadDataToMap.Click += new System.EventHandler(this.btnNHDPlusLoadDataToMap_Click_1);
            // 
            // btnRunNHDplus
            // 
            this.btnRunNHDplus.Location = new System.Drawing.Point(161, 260);
            this.btnRunNHDplus.Name = "btnRunNHDplus";
            this.btnRunNHDplus.Size = new System.Drawing.Size(131, 23);
            this.btnRunNHDplus.TabIndex = 81;
            this.btnRunNHDplus.Text = "Download";
            this.btnRunNHDplus.UseVisualStyleBackColor = true;
            this.btnRunNHDplus.Click += new System.EventHandler(this.btnRunNHDplus_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtHUC8NHDPlus);
            this.groupBox2.Controls.Add(this.btnAddHuc8NHDPlus);
            this.groupBox2.Controls.Add(this.btnRemoveNHDPlus);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 147);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listHuc8NHDPlus);
            this.groupBox4.Location = new System.Drawing.Point(216, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(108, 125);
            this.groupBox4.TabIndex = 80;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Your HUC-8 List";
            // 
            // listHuc8NHDPlus
            // 
            this.listHuc8NHDPlus.FormattingEnabled = true;
            this.listHuc8NHDPlus.Location = new System.Drawing.Point(6, 19);
            this.listHuc8NHDPlus.Name = "listHuc8NHDPlus";
            this.listHuc8NHDPlus.Size = new System.Drawing.Size(91, 95);
            this.listHuc8NHDPlus.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "<--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 76;
            this.label2.Text = "-->";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 26);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 13);
            this.label18.TabIndex = 67;
            this.label18.Text = "Enter a HUC-8:";
            // 
            // txtHUC8NHDPlus
            // 
            this.txtHUC8NHDPlus.Location = new System.Drawing.Point(94, 23);
            this.txtHUC8NHDPlus.Name = "txtHUC8NHDPlus";
            this.txtHUC8NHDPlus.Size = new System.Drawing.Size(100, 20);
            this.txtHUC8NHDPlus.TabIndex = 66;
            // 
            // btnAddHuc8NHDPlus
            // 
            this.btnAddHuc8NHDPlus.Location = new System.Drawing.Point(94, 49);
            this.btnAddHuc8NHDPlus.Name = "btnAddHuc8NHDPlus";
            this.btnAddHuc8NHDPlus.Size = new System.Drawing.Size(100, 23);
            this.btnAddHuc8NHDPlus.TabIndex = 71;
            this.btnAddHuc8NHDPlus.Text = "Add HUC-8 to List";
            this.btnAddHuc8NHDPlus.UseVisualStyleBackColor = true;
            // 
            // btnRemoveNHDPlus
            // 
            this.btnRemoveNHDPlus.Location = new System.Drawing.Point(94, 79);
            this.btnRemoveNHDPlus.Name = "btnRemoveNHDPlus";
            this.btnRemoveNHDPlus.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveNHDPlus.TabIndex = 74;
            this.btnRemoveNHDPlus.Text = "Remove Selected";
            this.btnRemoveNHDPlus.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(61, 105);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(130, 13);
            this.linkLabel2.TabIndex = 73;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Click here to find a HUC-8";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupStreamIDs);
            this.tabPage2.Controls.Add(this.btnManitowoc);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(641, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Delineation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupStreamIDs
            // 
            this.groupStreamIDs.Controls.Add(this.button3);
            this.groupStreamIDs.Controls.Add(this.listStreamIDs);
            this.groupStreamIDs.Location = new System.Drawing.Point(384, 6);
            this.groupStreamIDs.Name = "groupStreamIDs";
            this.groupStreamIDs.Size = new System.Drawing.Size(200, 111);
            this.groupStreamIDs.TabIndex = 87;
            this.groupStreamIDs.TabStop = false;
            this.groupStreamIDs.Text = "Stream IDs";
            this.groupStreamIDs.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 34);
            this.button3.TabIndex = 7;
            this.button3.Text = "Get Watershed and Flowlines";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listStreamIDs
            // 
            this.listStreamIDs.CheckOnClick = true;
            this.listStreamIDs.FormattingEnabled = true;
            this.listStreamIDs.Location = new System.Drawing.Point(6, 18);
            this.listStreamIDs.Name = "listStreamIDs";
            this.listStreamIDs.Size = new System.Drawing.Size(188, 49);
            this.listStreamIDs.TabIndex = 6;
            // 
            // btnManitowoc
            // 
            this.btnManitowoc.Location = new System.Drawing.Point(283, 17);
            this.btnManitowoc.Name = "btnManitowoc";
            this.btnManitowoc.Size = new System.Drawing.Size(67, 23);
            this.btnManitowoc.TabIndex = 86;
            this.btnManitowoc.Text = "Manitowoc";
            this.btnManitowoc.UseVisualStyleBackColor = true;
            this.btnManitowoc.Click += new System.EventHandler(this.btnManitowoc_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtCache);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtProject);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Location = new System.Drawing.Point(59, 137);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(498, 90);
            this.groupBox5.TabIndex = 85;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Cache Folder";
            // 
            // txtCache
            // 
            this.txtCache.Location = new System.Drawing.Point(105, 54);
            this.txtCache.Name = "txtCache";
            this.txtCache.Size = new System.Drawing.Size(259, 20);
            this.txtCache.TabIndex = 71;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 73;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Project Folder";
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(105, 27);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(259, 20);
            this.txtProject.TabIndex = 68;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(370, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 70;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnGetWatershed
            // 
            this.btnGetWatershed.Location = new System.Drawing.Point(10, 73);
            this.btnGetWatershed.Name = "btnGetWatershed";
            this.btnGetWatershed.Size = new System.Drawing.Size(170, 23);
            this.btnGetWatershed.TabIndex = 4;
            this.btnGetWatershed.Text = "Get Watershed and Flowlines";
            this.btnGetWatershed.UseVisualStyleBackColor = true;
            this.btnGetWatershed.Click += new System.EventHandler(this.btnGetWatershed_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Longitude:";
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(69, 45);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(100, 20);
            this.txtLongitude.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Latitude:";
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(69, 18);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(100, 20);
            this.txtLatitude.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.txtLatitude);
            this.groupBox6.Controls.Add(this.txtLongitude);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.btnGetWatershed);
            this.groupBox6.Location = new System.Drawing.Point(77, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 119);
            this.groupBox6.TabIndex = 88;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Coordinates";
            // 
            // NHDPlusBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelNHDPlus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NHDPlusBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NHDPlus";
            this.Load += new System.EventHandler(this.NHDPlus_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupStreamIDs.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNHDPlus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheFolder;
        private System.Windows.Forms.Button btnBrowseCache;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtProjectFolderNHDPlus;
        private System.Windows.Forms.Button btnBrowseProjectNHDPlus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListNHDPlus;
        private System.Windows.Forms.Button btnNHDPlusLoadDataToMap;
        private System.Windows.Forms.Button btnRunNHDplus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listHuc8NHDPlus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtHUC8NHDPlus;
        private System.Windows.Forms.Button btnAddHuc8NHDPlus;
        private System.Windows.Forms.Button btnRemoveNHDPlus;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGetWatershed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCache;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnManitowoc;
        private System.Windows.Forms.GroupBox groupStreamIDs;
        private System.Windows.Forms.CheckedListBox listStreamIDs;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox6;


    }
}
