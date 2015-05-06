namespace D4EM_NatureServe
{
    partial class NatureServeBox
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
            this.btnBrowseNatureServe = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtProjectFolderNatureServe = new System.Windows.Forms.TextBox();
            this.listPollinator = new System.Windows.Forms.CheckedListBox();
            this.btnDownloadNatureServe = new System.Windows.Forms.Button();
            this.labelNatureServe = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCacheFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseCache = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnNatureServeLoadDatatoMap = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelBirds = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBirds = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectFolderBirds = new System.Windows.Forms.TextBox();
            this.btnBrowseBirds = new System.Windows.Forms.Button();
            this.btnDownloadBirds = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridViewNatureServe = new System.Windows.Forms.DataGridView();
            this.btnPopulateNativeSpeciesTable = new System.Windows.Forms.Button();
            this.txtHUC8natureServe = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNatureServe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowseNatureServe
            // 
            this.btnBrowseNatureServe.Location = new System.Drawing.Point(360, 16);
            this.btnBrowseNatureServe.Name = "btnBrowseNatureServe";
            this.btnBrowseNatureServe.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNatureServe.TabIndex = 63;
            this.btnBrowseNatureServe.Text = "Browse";
            this.btnBrowseNatureServe.UseVisualStyleBackColor = true;
            this.btnBrowseNatureServe.Click += new System.EventHandler(this.btnBrowseNatureServe_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(19, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 13);
            this.label20.TabIndex = 62;
            this.label20.Text = "Project Folder";
            // 
            // txtProjectFolderNatureServe
            // 
            this.txtProjectFolderNatureServe.Location = new System.Drawing.Point(95, 16);
            this.txtProjectFolderNatureServe.Name = "txtProjectFolderNatureServe";
            this.txtProjectFolderNatureServe.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderNatureServe.TabIndex = 61;
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
            this.listPollinator.Location = new System.Drawing.Point(37, 19);
            this.listPollinator.Name = "listPollinator";
            this.listPollinator.Size = new System.Drawing.Size(268, 79);
            this.listPollinator.TabIndex = 60;
            // 
            // btnDownloadNatureServe
            // 
            this.btnDownloadNatureServe.Location = new System.Drawing.Point(112, 239);
            this.btnDownloadNatureServe.Name = "btnDownloadNatureServe";
            this.btnDownloadNatureServe.Size = new System.Drawing.Size(184, 29);
            this.btnDownloadNatureServe.TabIndex = 59;
            this.btnDownloadNatureServe.Text = "Download";
            this.btnDownloadNatureServe.UseVisualStyleBackColor = true;
            this.btnDownloadNatureServe.Click += new System.EventHandler(this.btnDownloadNatureServe_Click);
            // 
            // labelNatureServe
            // 
            this.labelNatureServe.AutoSize = true;
            this.labelNatureServe.Location = new System.Drawing.Point(109, 271);
            this.labelNatureServe.Name = "labelNatureServe";
            this.labelNatureServe.Size = new System.Drawing.Size(35, 13);
            this.labelNatureServe.TabIndex = 64;
            this.labelNatureServe.Text = "label1";
            this.labelNatureServe.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listPollinator);
            this.groupBox1.Location = new System.Drawing.Point(48, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 117);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pollinators";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtCacheFolder);
            this.groupBox2.Controls.Add(this.btnBrowseCache);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtProjectFolderNatureServe);
            this.groupBox2.Controls.Add(this.btnBrowseNatureServe);
            this.groupBox2.Location = new System.Drawing.Point(-3, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 87);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Cache Folder";
            // 
            // txtCacheFolder
            // 
            this.txtCacheFolder.Location = new System.Drawing.Point(95, 47);
            this.txtCacheFolder.Name = "txtCacheFolder";
            this.txtCacheFolder.Size = new System.Drawing.Size(259, 20);
            this.txtCacheFolder.TabIndex = 64;
            // 
            // btnBrowseCache
            // 
            this.btnBrowseCache.Location = new System.Drawing.Point(360, 47);
            this.btnBrowseCache.Name = "btnBrowseCache";
            this.btnBrowseCache.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCache.TabIndex = 66;
            this.btnBrowseCache.Text = "Browse";
            this.btnBrowseCache.UseVisualStyleBackColor = true;
            this.btnBrowseCache.Click += new System.EventHandler(this.btnBrowseCache_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(588, 356);
            this.tabControl1.TabIndex = 67;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnNatureServeLoadDatatoMap);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnDownloadNatureServe);
            this.tabPage1.Controls.Add(this.labelNatureServe);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(580, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pollinators";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnNatureServeLoadDatatoMap
            // 
            this.btnNatureServeLoadDatatoMap.Location = new System.Drawing.Point(112, 288);
            this.btnNatureServeLoadDatatoMap.Name = "btnNatureServeLoadDatatoMap";
            this.btnNatureServeLoadDatatoMap.Size = new System.Drawing.Size(184, 23);
            this.btnNatureServeLoadDatatoMap.TabIndex = 67;
            this.btnNatureServeLoadDatatoMap.Text = "Load Data to Map";
            this.btnNatureServeLoadDatatoMap.UseVisualStyleBackColor = true;
            this.btnNatureServeLoadDatatoMap.Visible = false;
            this.btnNatureServeLoadDatatoMap.Click += new System.EventHandler(this.btnNatureServeLoadDatatoMap_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelBirds);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.btnDownloadBirds);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(580, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Birds";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelBirds
            // 
            this.labelBirds.AutoSize = true;
            this.labelBirds.Location = new System.Drawing.Point(122, 294);
            this.labelBirds.Name = "labelBirds";
            this.labelBirds.Size = new System.Drawing.Size(35, 13);
            this.labelBirds.TabIndex = 70;
            this.labelBirds.Text = "label2";
            this.labelBirds.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBirds);
            this.groupBox3.Location = new System.Drawing.Point(11, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(546, 220);
            this.groupBox3.TabIndex = 68;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Birds";
            // 
            // listBirds
            // 
            this.listBirds.CheckOnClick = true;
            this.listBirds.FormattingEnabled = true;
            this.listBirds.Items.AddRange(new object[] {
            "Select All",
            "Accipitridae",
            "Aegithalidae",
            "Alaudidae",
            "Alcedinidae",
            "Alcidae",
            "Anatidae",
            "Anhimidae",
            "Anhingidae",
            "Apodidae",
            "Aramidae",
            "Ardeidae",
            "Bombycillidae",
            "Bucconidae",
            "Burhinidae",
            "Capitonidae",
            "Caprimulgidae",
            "Cardinalidae",
            "Cariamidae",
            "Cathartidae",
            "Certhiidae",
            "Charadriidae",
            "Chionidae",
            "Ciconiidae",
            "Cinclidae",
            "Columbidae",
            "Conopophagidae",
            "Corvidae",
            "Cotingidae",
            "Cracidae",
            "Cuculidae",
            "Diomedeidae",
            "Dulidae",
            "Emberizidae",
            "Estrildidae",
            "Eurypygidae",
            "Falconidae",
            "Formicariidae",
            "Fregatidae",
            "Fringillidae",
            "Furnariidae",
            "Galbulidae",
            "Gaviidae",
            "Glareolidae",
            "Gruidae",
            "Haematopodidae",
            "Heliornithidae",
            "Hirundinidae",
            "Hydrobatidae",
            "Icteridae",
            "Incertae_sedis",
            "Incertae_sedis_(near_Cinclidae)",
            "Incertae_sedis_(near_Parulidae)",
            "Incertae_sedis_(near_Pipridae)",
            "Incertae_sedis_(near_Thraupidae)",
            "Jacanidae"});
            this.listBirds.Location = new System.Drawing.Point(37, 19);
            this.listBirds.MultiColumn = true;
            this.listBirds.Name = "listBirds";
            this.listBirds.Size = new System.Drawing.Size(494, 184);
            this.listBirds.TabIndex = 60;
            this.listBirds.SelectedIndexChanged += new System.EventHandler(this.listBirds_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtProjectFolderBirds);
            this.groupBox4.Controls.Add(this.btnBrowseBirds);
            this.groupBox4.Location = new System.Drawing.Point(3, 232);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(454, 55);
            this.groupBox4.TabIndex = 69;
            this.groupBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Project Folder";
            // 
            // txtProjectFolderBirds
            // 
            this.txtProjectFolderBirds.Location = new System.Drawing.Point(95, 16);
            this.txtProjectFolderBirds.Name = "txtProjectFolderBirds";
            this.txtProjectFolderBirds.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderBirds.TabIndex = 61;
            // 
            // btnBrowseBirds
            // 
            this.btnBrowseBirds.Location = new System.Drawing.Point(360, 16);
            this.btnBrowseBirds.Name = "btnBrowseBirds";
            this.btnBrowseBirds.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBirds.TabIndex = 63;
            this.btnBrowseBirds.Text = "Browse";
            this.btnBrowseBirds.UseVisualStyleBackColor = true;
            // 
            // btnDownloadBirds
            // 
            this.btnDownloadBirds.Location = new System.Drawing.Point(463, 245);
            this.btnDownloadBirds.Name = "btnDownloadBirds";
            this.btnDownloadBirds.Size = new System.Drawing.Size(114, 29);
            this.btnDownloadBirds.TabIndex = 67;
            this.btnDownloadBirds.Text = "Download";
            this.btnDownloadBirds.UseVisualStyleBackColor = true;
            this.btnDownloadBirds.Click += new System.EventHandler(this.btnDownloadBirds_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(580, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mammals";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridViewNatureServe);
            this.tabPage4.Controls.Add(this.btnPopulateNativeSpeciesTable);
            this.tabPage4.Controls.Add(this.txtHUC8natureServe);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(580, 330);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Native Species";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridViewNatureServe
            // 
            this.dataGridViewNatureServe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNatureServe.Location = new System.Drawing.Point(110, 112);
            this.dataGridViewNatureServe.Name = "dataGridViewNatureServe";
            this.dataGridViewNatureServe.Size = new System.Drawing.Size(399, 150);
            this.dataGridViewNatureServe.TabIndex = 67;
            // 
            // btnPopulateNativeSpeciesTable
            // 
            this.btnPopulateNativeSpeciesTable.Location = new System.Drawing.Point(199, 68);
            this.btnPopulateNativeSpeciesTable.Name = "btnPopulateNativeSpeciesTable";
            this.btnPopulateNativeSpeciesTable.Size = new System.Drawing.Size(310, 23);
            this.btnPopulateNativeSpeciesTable.TabIndex = 65;
            this.btnPopulateNativeSpeciesTable.Text = "List Native Species in HUC-8";
            this.btnPopulateNativeSpeciesTable.UseVisualStyleBackColor = true;
            this.btnPopulateNativeSpeciesTable.Click += new System.EventHandler(this.btnPopulateNativeSpeciesTable_Click);
            // 
            // txtHUC8natureServe
            // 
            this.txtHUC8natureServe.Location = new System.Drawing.Point(71, 68);
            this.txtHUC8natureServe.Name = "txtHUC8natureServe";
            this.txtHUC8natureServe.Size = new System.Drawing.Size(101, 20);
            this.txtHUC8natureServe.TabIndex = 66;
            // 
            // NatureServeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 356);
            this.Controls.Add(this.tabControl1);
            this.Name = "NatureServeBox";
            this.Text = "NatureServeBox";
            this.Load += new System.EventHandler(this.NatureServeBox_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNatureServe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseNatureServe;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtProjectFolderNatureServe;
        private System.Windows.Forms.CheckedListBox listPollinator;
        private System.Windows.Forms.Button btnDownloadNatureServe;
        private System.Windows.Forms.Label labelNatureServe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox listBirds;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectFolderBirds;
        private System.Windows.Forms.Button btnBrowseBirds;
        private System.Windows.Forms.Button btnDownloadBirds;
        private System.Windows.Forms.Label labelBirds;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridViewNatureServe;
        private System.Windows.Forms.Button btnPopulateNativeSpeciesTable;
        private System.Windows.Forms.TextBox txtHUC8natureServe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCacheFolder;
        private System.Windows.Forms.Button btnBrowseCache;
        private System.Windows.Forms.Button btnNatureServeLoadDatatoMap;
    }
}