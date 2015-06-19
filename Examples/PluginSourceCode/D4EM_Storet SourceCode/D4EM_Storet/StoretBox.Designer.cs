namespace D4EM_Storet
{
    partial class StoretBox
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
            this.labelStoret = new System.Windows.Forms.Label();
            this.btnStoretLoadDataToMap = new System.Windows.Forms.Button();
            this.listStoretDataTypes = new System.Windows.Forms.CheckedListBox();
            this.groupBox18.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label32);
            this.groupBox18.Controls.Add(this.txtProjectFolderStoret);
            this.groupBox18.Controls.Add(this.btnBrowseStoret);
            this.groupBox18.Location = new System.Drawing.Point(112, 182);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(446, 58);
            this.groupBox18.TabIndex = 39;
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
            this.groupBox16.Location = new System.Drawing.Point(101, 35);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(177, 131);
            this.groupBox16.TabIndex = 38;
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
            this.btnDownloadStoret.Location = new System.Drawing.Point(252, 255);
            this.btnDownloadStoret.Name = "btnDownloadStoret";
            this.btnDownloadStoret.Size = new System.Drawing.Size(158, 23);
            this.btnDownloadStoret.TabIndex = 37;
            this.btnDownloadStoret.Text = "Download";
            this.btnDownloadStoret.UseVisualStyleBackColor = true;
            this.btnDownloadStoret.Click += new System.EventHandler(this.btnDownloadStoret_Click);
            // 
            // labelStoret
            // 
            this.labelStoret.AutoSize = true;
            this.labelStoret.Location = new System.Drawing.Point(160, 396);
            this.labelStoret.Name = "labelStoret";
            this.labelStoret.Size = new System.Drawing.Size(35, 13);
            this.labelStoret.TabIndex = 40;
            this.labelStoret.Text = "label1";
            this.labelStoret.Visible = false;
            // 
            // btnStoretLoadDataToMap
            // 
            this.btnStoretLoadDataToMap.Location = new System.Drawing.Point(199, 429);
            this.btnStoretLoadDataToMap.Name = "btnStoretLoadDataToMap";
            this.btnStoretLoadDataToMap.Size = new System.Drawing.Size(166, 23);
            this.btnStoretLoadDataToMap.TabIndex = 41;
            this.btnStoretLoadDataToMap.Text = "Load Data to Map";
            this.btnStoretLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnStoretLoadDataToMap.Visible = false;
            this.btnStoretLoadDataToMap.Click += new System.EventHandler(this.btnStoretLoadDataToMap_Click);
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
            this.listStoretDataTypes.Location = new System.Drawing.Point(143, 305);
            this.listStoretDataTypes.Name = "listStoretDataTypes";
            this.listStoretDataTypes.Size = new System.Drawing.Size(371, 79);
            this.listStoretDataTypes.TabIndex = 67;
            // 
            // StoretBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 464);
            this.Controls.Add(this.listStoretDataTypes);
            this.Controls.Add(this.btnStoretLoadDataToMap);
            this.Controls.Add(this.labelStoret);
            this.Controls.Add(this.groupBox18);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.btnDownloadStoret);
            this.Name = "StoretBox";
            this.Text = "StoretBox";
            this.Load += new System.EventHandler(this.StoretBox_Load);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtProjectFolderStoret;
        private System.Windows.Forms.Button btnBrowseStoret;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtNorthStoret;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtSouthStoret;
        private System.Windows.Forms.TextBox txtEastStoret;
        private System.Windows.Forms.TextBox txtWestStoret;
        private System.Windows.Forms.Button btnDownloadStoret;
        private System.Windows.Forms.Label labelStoret;
        private System.Windows.Forms.Button btnStoretLoadDataToMap;
        private System.Windows.Forms.CheckedListBox listStoretDataTypes;
    }
}