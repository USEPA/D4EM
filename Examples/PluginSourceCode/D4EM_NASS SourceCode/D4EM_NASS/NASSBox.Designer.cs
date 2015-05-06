namespace D4EM_NASS
{
    partial class NASSBox
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
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.listYearsNASS = new System.Windows.Forms.CheckedListBox();
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
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCache = new System.Windows.Forms.TextBox();
            this.btnBrowseCache = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.txtProjectFolderNASS = new System.Windows.Forms.TextBox();
            this.btnBrowseNASS = new System.Windows.Forms.Button();
            this.labelNASS = new System.Windows.Forms.Label();
            this.btnNASSloadDataToMap = new System.Windows.Forms.Button();
            this.groupBox34.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.listYearsNASS);
            this.groupBox34.Location = new System.Drawing.Point(267, 133);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(104, 78);
            this.groupBox34.TabIndex = 41;
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
            this.groupBox32.Location = new System.Drawing.Point(42, 124);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(177, 131);
            this.groupBox32.TabIndex = 40;
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
            this.btnNASSDownload.Location = new System.Drawing.Point(416, 147);
            this.btnNASSDownload.Name = "btnNASSDownload";
            this.btnNASSDownload.Size = new System.Drawing.Size(172, 64);
            this.btnNASSDownload.TabIndex = 39;
            this.btnNASSDownload.Text = "Download ";
            this.btnNASSDownload.UseVisualStyleBackColor = true;
            this.btnNASSDownload.Click += new System.EventHandler(this.btnNASSDownload_Click);
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.label1);
            this.groupBox33.Controls.Add(this.txtCache);
            this.groupBox33.Controls.Add(this.btnBrowseCache);
            this.groupBox33.Controls.Add(this.label55);
            this.groupBox33.Controls.Add(this.txtProjectFolderNASS);
            this.groupBox33.Controls.Add(this.btnBrowseNASS);
            this.groupBox33.Location = new System.Drawing.Point(42, 4);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(446, 92);
            this.groupBox33.TabIndex = 42;
            this.groupBox33.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Cache Folder";
            // 
            // txtCache
            // 
            this.txtCache.Location = new System.Drawing.Point(87, 54);
            this.txtCache.Name = "txtCache";
            this.txtCache.Size = new System.Drawing.Size(259, 20);
            this.txtCache.TabIndex = 32;
            // 
            // btnBrowseCache
            // 
            this.btnBrowseCache.Location = new System.Drawing.Point(352, 54);
            this.btnBrowseCache.Name = "btnBrowseCache";
            this.btnBrowseCache.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCache.TabIndex = 34;
            this.btnBrowseCache.Text = "Browse";
            this.btnBrowseCache.UseVisualStyleBackColor = true;
            this.btnBrowseCache.Click += new System.EventHandler(this.btnBrowseCache_Click);
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
            // labelNASS
            // 
            this.labelNASS.AutoSize = true;
            this.labelNASS.Location = new System.Drawing.Point(161, 273);
            this.labelNASS.Name = "labelNASS";
            this.labelNASS.Size = new System.Drawing.Size(58, 13);
            this.labelNASS.TabIndex = 43;
            this.labelNASS.Text = "labelNASS";
            this.labelNASS.Visible = false;
            // 
            // btnNASSloadDataToMap
            // 
            this.btnNASSloadDataToMap.Location = new System.Drawing.Point(160, 289);
            this.btnNASSloadDataToMap.Name = "btnNASSloadDataToMap";
            this.btnNASSloadDataToMap.Size = new System.Drawing.Size(228, 23);
            this.btnNASSloadDataToMap.TabIndex = 44;
            this.btnNASSloadDataToMap.Text = "Load Data to Map";
            this.btnNASSloadDataToMap.UseVisualStyleBackColor = true;
            this.btnNASSloadDataToMap.Visible = false;
            this.btnNASSloadDataToMap.Click += new System.EventHandler(this.btnNASSloadDataToMap_Click);
            // 
            // NASSBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 324);
            this.Controls.Add(this.btnNASSloadDataToMap);
            this.Controls.Add(this.labelNASS);
            this.Controls.Add(this.groupBox33);
            this.Controls.Add(this.groupBox34);
            this.Controls.Add(this.groupBox32);
            this.Controls.Add(this.btnNASSDownload);
            this.Name = "NASSBox";
            this.Text = "NASSBox";
            this.Load += new System.EventHandler(this.NASSBox_Load);
            this.groupBox34.ResumeLayout(false);
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.CheckedListBox listYearsNASS;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txtNorthNASS;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtSouthNASS;
        private System.Windows.Forms.TextBox txtEastNASS;
        private System.Windows.Forms.TextBox txtWestNASS;
        private System.Windows.Forms.Button btnNASSDownload;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtProjectFolderNASS;
        private System.Windows.Forms.Button btnBrowseNASS;
        private System.Windows.Forms.Label labelNASS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCache;
        private System.Windows.Forms.Button btnBrowseCache;
        private System.Windows.Forms.Button btnNASSloadDataToMap;
    }
}