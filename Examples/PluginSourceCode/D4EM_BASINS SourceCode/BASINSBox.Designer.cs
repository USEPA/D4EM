namespace D4EM_BASINS
{
    partial class BASINSBox
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
            this.btnRemoveBasins = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.listHuc8Basins = new System.Windows.Forms.ListBox();
            this.btnAddBasins = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHUC8Basins = new System.Windows.Forms.TextBox();
            this.txtProjectFolderBasins = new System.Windows.Forms.TextBox();
            this.boxBasinsDataType = new System.Windows.Forms.CheckedListBox();
            this.btnBrowseProjectFolderBasins = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnRunBasins = new System.Windows.Forms.Button();
            this.labelBasins = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCacheFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseCache = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBasinsLoadDataToMap = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemoveBasins
            // 
            this.btnRemoveBasins.Location = new System.Drawing.Point(88, 88);
            this.btnRemoveBasins.Name = "btnRemoveBasins";
            this.btnRemoveBasins.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveBasins.TabIndex = 78;
            this.btnRemoveBasins.Text = "Remove Selected";
            this.btnRemoveBasins.UseVisualStyleBackColor = true;
            this.btnRemoveBasins.Click += new System.EventHandler(this.btnRemoveBasins_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(55, 114);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(130, 13);
            this.linkLabel3.TabIndex = 77;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Click here to find a HUC-8";
            this.linkLabel3.Click += new System.EventHandler(this.btnHUCfind_Click);
            // 
            // listHuc8Basins
            // 
            this.listHuc8Basins.FormattingEnabled = true;
            this.listHuc8Basins.Location = new System.Drawing.Point(218, 35);
            this.listHuc8Basins.Name = "listHuc8Basins";
            this.listHuc8Basins.Size = new System.Drawing.Size(91, 95);
            this.listHuc8Basins.TabIndex = 76;
            // 
            // btnAddBasins
            // 
            this.btnAddBasins.Location = new System.Drawing.Point(88, 58);
            this.btnAddBasins.Name = "btnAddBasins";
            this.btnAddBasins.Size = new System.Drawing.Size(100, 23);
            this.btnAddBasins.TabIndex = 75;
            this.btnAddBasins.Text = "Add HUC-8 to List";
            this.btnAddBasins.UseVisualStyleBackColor = true;
            this.btnAddBasins.Click += new System.EventHandler(this.btnAddBasins_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Enter a HUC-8:";
            // 
            // txtHUC8Basins
            // 
            this.txtHUC8Basins.Location = new System.Drawing.Point(88, 32);
            this.txtHUC8Basins.Name = "txtHUC8Basins";
            this.txtHUC8Basins.Size = new System.Drawing.Size(100, 20);
            this.txtHUC8Basins.TabIndex = 73;
            // 
            // txtProjectFolderBasins
            // 
            this.txtProjectFolderBasins.Location = new System.Drawing.Point(101, 19);
            this.txtProjectFolderBasins.Name = "txtProjectFolderBasins";
            this.txtProjectFolderBasins.Size = new System.Drawing.Size(259, 20);
            this.txtProjectFolderBasins.TabIndex = 69;
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
            this.boxBasinsDataType.Location = new System.Drawing.Point(23, 19);
            this.boxBasinsDataType.Name = "boxBasinsDataType";
            this.boxBasinsDataType.Size = new System.Drawing.Size(120, 94);
            this.boxBasinsDataType.TabIndex = 72;
            // 
            // btnBrowseProjectFolderBasins
            // 
            this.btnBrowseProjectFolderBasins.Location = new System.Drawing.Point(365, 19);
            this.btnBrowseProjectFolderBasins.Name = "btnBrowseProjectFolderBasins";
            this.btnBrowseProjectFolderBasins.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectFolderBasins.TabIndex = 71;
            this.btnBrowseProjectFolderBasins.Text = "Browse";
            this.btnBrowseProjectFolderBasins.UseVisualStyleBackColor = true;
            this.btnBrowseProjectFolderBasins.Click += new System.EventHandler(this.btnBrowseProjectFolderBasins_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 70;
            this.label13.Text = "Project Folder";
            // 
            // btnRunBasins
            // 
            this.btnRunBasins.Location = new System.Drawing.Point(182, 263);
            this.btnRunBasins.Name = "btnRunBasins";
            this.btnRunBasins.Size = new System.Drawing.Size(190, 23);
            this.btnRunBasins.TabIndex = 68;
            this.btnRunBasins.Text = "Download";
            this.btnRunBasins.UseVisualStyleBackColor = true;
            this.btnRunBasins.Click += new System.EventHandler(this.btnRunBasins_Click);
            // 
            // labelBasins
            // 
            this.labelBasins.AutoSize = true;
            this.labelBasins.Location = new System.Drawing.Point(144, 293);
            this.labelBasins.Name = "labelBasins";
            this.labelBasins.Size = new System.Drawing.Size(35, 13);
            this.labelBasins.TabIndex = 79;
            this.labelBasins.Text = "label1";
            this.labelBasins.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxBasinsDataType);
            this.groupBox1.Location = new System.Drawing.Point(383, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 130);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BASINS Data Types";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCacheFolder);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnBrowseCache);
            this.groupBox2.Controls.Add(this.txtProjectFolderBasins);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnBrowseProjectFolderBasins);
            this.groupBox2.Location = new System.Drawing.Point(34, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 80);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            // 
            // txtCacheFolder
            // 
            this.txtCacheFolder.Location = new System.Drawing.Point(101, 46);
            this.txtCacheFolder.Name = "txtCacheFolder";
            this.txtCacheFolder.Size = new System.Drawing.Size(259, 20);
            this.txtCacheFolder.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Cache Folder";
            // 
            // btnBrowseCache
            // 
            this.btnBrowseCache.Location = new System.Drawing.Point(365, 46);
            this.btnBrowseCache.Name = "btnBrowseCache";
            this.btnBrowseCache.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCache.TabIndex = 74;
            this.btnBrowseCache.Text = "Browse";
            this.btnBrowseCache.UseVisualStyleBackColor = true;
            this.btnBrowseCache.Click += new System.EventHandler(this.btnBrowseCache_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtHUC8Basins);
            this.groupBox3.Controls.Add(this.btnAddBasins);
            this.groupBox3.Controls.Add(this.listHuc8Basins);
            this.groupBox3.Controls.Add(this.btnRemoveBasins);
            this.groupBox3.Controls.Add(this.linkLabel3);
            this.groupBox3.Location = new System.Drawing.Point(34, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 164);
            this.groupBox3.TabIndex = 82;
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Your HUC-8 List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "<--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "-->";
            // 
            // btnBasinsLoadDataToMap
            // 
            this.btnBasinsLoadDataToMap.Location = new System.Drawing.Point(182, 315);
            this.btnBasinsLoadDataToMap.Name = "btnBasinsLoadDataToMap";
            this.btnBasinsLoadDataToMap.Size = new System.Drawing.Size(171, 23);
            this.btnBasinsLoadDataToMap.TabIndex = 83;
            this.btnBasinsLoadDataToMap.Text = "Load Data to Map";
            this.btnBasinsLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnBasinsLoadDataToMap.Visible = false;
            this.btnBasinsLoadDataToMap.Click += new System.EventHandler(this.btnBasinsLoadDataToMap_Click);
            // 
            // BASINSBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 350);
            this.Controls.Add(this.btnBasinsLoadDataToMap);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelBasins);
            this.Controls.Add(this.btnRunBasins);
            this.Name = "BASINSBox";
            this.Text = "BASINSBox";
            this.Load += new System.EventHandler(this.BASINSBox_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Label labelBasins;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseCache;
        private System.Windows.Forms.Button btnBasinsLoadDataToMap;
    }
}