namespace D4EM_NDBC
{
    partial class NDBCBox
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
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtRadiusNDBC = new System.Windows.Forms.TextBox();
            this.txtLongitudeNDBC = new System.Windows.Forms.TextBox();
            this.txtLatitudeNDBC = new System.Windows.Forms.TextBox();
            this.btnRunNDBC = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.btnBrowseNDBC = new System.Windows.Forms.Button();
            this.txtProjectFolderNDBC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNDBCLoadDataToMap = new System.Windows.Forms.Button();
            this.groupNDBChistorical = new System.Windows.Forms.GroupBox();
            this.listStationIDs = new System.Windows.Forms.CheckedListBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNDBCyear = new System.Windows.Forms.TextBox();
            this.btnNDBChistoricalData = new System.Windows.Forms.Button();
            this.groupNDBChistorical.SuspendLayout();
            this.SuspendLayout();
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(73, 62);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(40, 13);
            this.label58.TabIndex = 16;
            this.label58.Text = "Radius";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(73, 36);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(54, 13);
            this.label57.TabIndex = 15;
            this.label57.Text = "Longitude";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(73, 9);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(45, 13);
            this.label56.TabIndex = 14;
            this.label56.Text = "Latitude";
            // 
            // txtRadiusNDBC
            // 
            this.txtRadiusNDBC.Location = new System.Drawing.Point(134, 62);
            this.txtRadiusNDBC.Name = "txtRadiusNDBC";
            this.txtRadiusNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtRadiusNDBC.TabIndex = 13;
            // 
            // txtLongitudeNDBC
            // 
            this.txtLongitudeNDBC.Location = new System.Drawing.Point(134, 36);
            this.txtLongitudeNDBC.Name = "txtLongitudeNDBC";
            this.txtLongitudeNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtLongitudeNDBC.TabIndex = 12;
            // 
            // txtLatitudeNDBC
            // 
            this.txtLatitudeNDBC.Location = new System.Drawing.Point(134, 9);
            this.txtLatitudeNDBC.Name = "txtLatitudeNDBC";
            this.txtLatitudeNDBC.Size = new System.Drawing.Size(100, 20);
            this.txtLatitudeNDBC.TabIndex = 11;
            // 
            // btnRunNDBC
            // 
            this.btnRunNDBC.Location = new System.Drawing.Point(388, 12);
            this.btnRunNDBC.Name = "btnRunNDBC";
            this.btnRunNDBC.Size = new System.Drawing.Size(162, 66);
            this.btnRunNDBC.TabIndex = 9;
            this.btnRunNDBC.Text = "Download";
            this.btnRunNDBC.UseVisualStyleBackColor = true;
            this.btnRunNDBC.Click += new System.EventHandler(this.btnRunNDBC_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(43, 154);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(75, 13);
            this.label59.TabIndex = 19;
            this.label59.Text = "Project Folder:";
            // 
            // btnBrowseNDBC
            // 
            this.btnBrowseNDBC.Location = new System.Drawing.Point(402, 152);
            this.btnBrowseNDBC.Name = "btnBrowseNDBC";
            this.btnBrowseNDBC.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNDBC.TabIndex = 18;
            this.btnBrowseNDBC.Text = "Browse";
            this.btnBrowseNDBC.UseVisualStyleBackColor = true;
            this.btnBrowseNDBC.Click += new System.EventHandler(this.btnBrowseNDBC_Click);
            // 
            // txtProjectFolderNDBC
            // 
            this.txtProjectFolderNDBC.Location = new System.Drawing.Point(124, 154);
            this.txtProjectFolderNDBC.Name = "txtProjectFolderNDBC";
            this.txtProjectFolderNDBC.Size = new System.Drawing.Size(264, 20);
            this.txtProjectFolderNDBC.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "(nautical miles)";
            // 
            // btnNDBCLoadDataToMap
            // 
            this.btnNDBCLoadDataToMap.Location = new System.Drawing.Point(168, 225);
            this.btnNDBCLoadDataToMap.Name = "btnNDBCLoadDataToMap";
            this.btnNDBCLoadDataToMap.Size = new System.Drawing.Size(178, 23);
            this.btnNDBCLoadDataToMap.TabIndex = 22;
            this.btnNDBCLoadDataToMap.Text = "Load Data to Map";
            this.btnNDBCLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnNDBCLoadDataToMap.Visible = false;
            this.btnNDBCLoadDataToMap.Click += new System.EventHandler(this.btnNDBCLoadDataToMap_Click);
            // 
            // groupNDBChistorical
            // 
            this.groupNDBChistorical.Controls.Add(this.listStationIDs);
            this.groupNDBChistorical.Controls.Add(this.label24);
            this.groupNDBChistorical.Controls.Add(this.label23);
            this.groupNDBChistorical.Controls.Add(this.txtNDBCyear);
            this.groupNDBChistorical.Controls.Add(this.btnNDBChistoricalData);
            this.groupNDBChistorical.Location = new System.Drawing.Point(46, 264);
            this.groupNDBChistorical.Name = "groupNDBChistorical";
            this.groupNDBChistorical.Size = new System.Drawing.Size(493, 171);
            this.groupNDBChistorical.TabIndex = 23;
            this.groupNDBChistorical.TabStop = false;
            this.groupNDBChistorical.Text = "Historical Data";
            this.groupNDBChistorical.Visible = false;
            // 
            // listStationIDs
            // 
            this.listStationIDs.CheckOnClick = true;
            this.listStationIDs.FormattingEnabled = true;
            this.listStationIDs.Location = new System.Drawing.Point(118, 14);
            this.listStationIDs.Name = "listStationIDs";
            this.listStationIDs.Size = new System.Drawing.Size(342, 94);
            this.listStationIDs.TabIndex = 5;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(83, 133);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Year";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(67, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(45, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Stations";
            // 
            // txtNDBCyear
            // 
            this.txtNDBCyear.Location = new System.Drawing.Point(118, 126);
            this.txtNDBCyear.Name = "txtNDBCyear";
            this.txtNDBCyear.Size = new System.Drawing.Size(100, 20);
            this.txtNDBCyear.TabIndex = 2;
            // 
            // btnNDBChistoricalData
            // 
            this.btnNDBChistoricalData.Location = new System.Drawing.Point(271, 124);
            this.btnNDBChistoricalData.Name = "btnNDBChistoricalData";
            this.btnNDBChistoricalData.Size = new System.Drawing.Size(145, 23);
            this.btnNDBChistoricalData.TabIndex = 0;
            this.btnNDBChistoricalData.Text = "Get Historical Data";
            this.btnNDBChistoricalData.UseVisualStyleBackColor = true;
            this.btnNDBChistoricalData.Click += new System.EventHandler(this.btnNDBChistoricalData_Click);
            // 
            // NDBCBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 489);
            this.Controls.Add(this.groupNDBChistorical);
            this.Controls.Add(this.btnNDBCLoadDataToMap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.btnBrowseNDBC);
            this.Controls.Add(this.txtProjectFolderNDBC);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.txtRadiusNDBC);
            this.Controls.Add(this.txtLongitudeNDBC);
            this.Controls.Add(this.txtLatitudeNDBC);
            this.Controls.Add(this.btnRunNDBC);
            this.Name = "NDBCBox";
            this.Text = "NDBCBox";
            this.Load += new System.EventHandler(this.NDBCBox_Load);
            this.groupNDBChistorical.ResumeLayout(false);
            this.groupNDBChistorical.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtRadiusNDBC;
        private System.Windows.Forms.TextBox txtLongitudeNDBC;
        private System.Windows.Forms.TextBox txtLatitudeNDBC;
        private System.Windows.Forms.Button btnRunNDBC;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button btnBrowseNDBC;
        private System.Windows.Forms.TextBox txtProjectFolderNDBC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNDBCLoadDataToMap;
        private System.Windows.Forms.GroupBox groupNDBChistorical;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNDBCyear;
        private System.Windows.Forms.Button btnNDBChistoricalData;
        private System.Windows.Forms.CheckedListBox listStationIDs;
    }
}