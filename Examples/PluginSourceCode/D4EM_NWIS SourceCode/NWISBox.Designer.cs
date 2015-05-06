namespace D4EM_NWIS
{
    partial class NWISBox
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
            this.listNWIS = new System.Windows.Forms.CheckedListBox();
            this.btnBrowseProjectNWIS = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtProjectNWIS = new System.Windows.Forms.TextBox();
            this.txtEastNWIS = new System.Windows.Forms.TextBox();
            this.txtWestNWIS = new System.Windows.Forms.TextBox();
            this.txtSouthNWIS = new System.Windows.Forms.TextBox();
            this.txtNorthNWIS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnRunNWIS = new System.Windows.Forms.Button();
            this.labelNWIS = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDowloadNWISUsingStationIds = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.listNWISStations = new System.Windows.Forms.ListBox();
            this.btnAddStations = new System.Windows.Forms.Button();
            this.txtNWISStation = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnNWISLoadDataToMap = new System.Windows.Forms.Button();
            this.groupNWISspecific = new System.Windows.Forms.GroupBox();
            this.listNWISDataTypesSpecific = new System.Windows.Forms.CheckedListBox();
            this.groupNWIShistorical = new System.Windows.Forms.GroupBox();
            this.listStationIDs = new System.Windows.Forms.CheckedListBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnNWIShistoricalData = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupNWISspecific.SuspendLayout();
            this.groupNWIShistorical.SuspendLayout();
            this.SuspendLayout();
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
            this.listNWIS.Location = new System.Drawing.Point(18, 19);
            this.listNWIS.Name = "listNWIS";
            this.listNWIS.Size = new System.Drawing.Size(120, 64);
            this.listNWIS.TabIndex = 72;
            this.listNWIS.SelectedIndexChanged += new System.EventHandler(this.listNWIS_SelectedIndexChanged);
            // 
            // btnBrowseProjectNWIS
            // 
            this.btnBrowseProjectNWIS.Location = new System.Drawing.Point(390, 19);
            this.btnBrowseProjectNWIS.Name = "btnBrowseProjectNWIS";
            this.btnBrowseProjectNWIS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectNWIS.TabIndex = 71;
            this.btnBrowseProjectNWIS.Text = "Browse";
            this.btnBrowseProjectNWIS.UseVisualStyleBackColor = true;
            this.btnBrowseProjectNWIS.Click += new System.EventHandler(this.btnBrowseProjectNWIS_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(37, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 70;
            this.label15.Text = "Project Folder";
            // 
            // txtProjectNWIS
            // 
            this.txtProjectNWIS.Location = new System.Drawing.Point(115, 19);
            this.txtProjectNWIS.Name = "txtProjectNWIS";
            this.txtProjectNWIS.Size = new System.Drawing.Size(259, 20);
            this.txtProjectNWIS.TabIndex = 69;
            // 
            // txtEastNWIS
            // 
            this.txtEastNWIS.Location = new System.Drawing.Point(63, 78);
            this.txtEastNWIS.Name = "txtEastNWIS";
            this.txtEastNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtEastNWIS.TabIndex = 67;
            // 
            // txtWestNWIS
            // 
            this.txtWestNWIS.Location = new System.Drawing.Point(63, 101);
            this.txtWestNWIS.Name = "txtWestNWIS";
            this.txtWestNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtWestNWIS.TabIndex = 65;
            // 
            // txtSouthNWIS
            // 
            this.txtSouthNWIS.Location = new System.Drawing.Point(63, 53);
            this.txtSouthNWIS.Name = "txtSouthNWIS";
            this.txtSouthNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtSouthNWIS.TabIndex = 63;
            // 
            // txtNorthNWIS
            // 
            this.txtNorthNWIS.Location = new System.Drawing.Point(63, 30);
            this.txtNorthNWIS.Name = "txtNorthNWIS";
            this.txtNorthNWIS.Size = new System.Drawing.Size(100, 20);
            this.txtNorthNWIS.TabIndex = 61;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 68;
            this.label9.Text = "East";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "West";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 64;
            this.label11.Text = "South";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "North";
            // 
            // btnRunNWIS
            // 
            this.btnRunNWIS.Location = new System.Drawing.Point(23, 136);
            this.btnRunNWIS.Name = "btnRunNWIS";
            this.btnRunNWIS.Size = new System.Drawing.Size(171, 23);
            this.btnRunNWIS.TabIndex = 60;
            this.btnRunNWIS.Text = "Download";
            this.btnRunNWIS.UseVisualStyleBackColor = true;
            this.btnRunNWIS.Click += new System.EventHandler(this.btnRunNWIS_Click);
            // 
            // labelNWIS
            // 
            this.labelNWIS.AutoSize = true;
            this.labelNWIS.Location = new System.Drawing.Point(124, 439);
            this.labelNWIS.Name = "labelNWIS";
            this.labelNWIS.Size = new System.Drawing.Size(35, 13);
            this.labelNWIS.TabIndex = 73;
            this.labelNWIS.Text = "label1";
            this.labelNWIS.Visible = false;
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
            this.groupBox1.Location = new System.Drawing.Point(186, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 180);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download NWIS using Coordinates";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listNWIS);
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 100);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NWIS Data Types";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDowloadNWISUsingStationIds);
            this.groupBox3.Controls.Add(this.btnRemoveSelected);
            this.groupBox3.Controls.Add(this.listNWISStations);
            this.groupBox3.Controls.Add(this.btnAddStations);
            this.groupBox3.Controls.Add(this.txtNWISStation);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(402, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(246, 175);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Download NWIS using Station IDs";
            this.groupBox3.Visible = false;
            // 
            // btnDowloadNWISUsingStationIds
            // 
            this.btnDowloadNWISUsingStationIds.Location = new System.Drawing.Point(115, 107);
            this.btnDowloadNWISUsingStationIds.Name = "btnDowloadNWISUsingStationIds";
            this.btnDowloadNWISUsingStationIds.Size = new System.Drawing.Size(102, 32);
            this.btnDowloadNWISUsingStationIds.TabIndex = 68;
            this.btnDowloadNWISUsingStationIds.Text = "Download";
            this.btnDowloadNWISUsingStationIds.UseVisualStyleBackColor = true;
            this.btnDowloadNWISUsingStationIds.Click += new System.EventHandler(this.btnDowloadNWISUsingStationIds_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(9, 118);
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
            this.listNWISStations.Location = new System.Drawing.Point(122, 19);
            this.listNWISStations.Name = "listNWISStations";
            this.listNWISStations.Size = new System.Drawing.Size(91, 82);
            this.listNWISStations.TabIndex = 65;
            // 
            // btnAddStations
            // 
            this.btnAddStations.Location = new System.Drawing.Point(9, 65);
            this.btnAddStations.Name = "btnAddStations";
            this.btnAddStations.Size = new System.Drawing.Size(100, 39);
            this.btnAddStations.TabIndex = 64;
            this.btnAddStations.Text = "Add Station ID to List";
            this.btnAddStations.UseVisualStyleBackColor = true;
            this.btnAddStations.Click += new System.EventHandler(this.btnAddStations_Click);
            // 
            // txtNWISStation
            // 
            this.txtNWISStation.Location = new System.Drawing.Point(9, 39);
            this.txtNWISStation.Name = "txtNWISStation";
            this.txtNWISStation.Size = new System.Drawing.Size(100, 20);
            this.txtNWISStation.TabIndex = 62;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 63;
            this.label17.Text = "Enter a Station ID";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtProjectNWIS);
            this.groupBox4.Controls.Add(this.btnBrowseProjectNWIS);
            this.groupBox4.Location = new System.Drawing.Point(12, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(534, 63);
            this.groupBox4.TabIndex = 77;
            this.groupBox4.TabStop = false;
            // 
            // btnNWISLoadDataToMap
            // 
            this.btnNWISLoadDataToMap.Location = new System.Drawing.Point(127, 455);
            this.btnNWISLoadDataToMap.Name = "btnNWISLoadDataToMap";
            this.btnNWISLoadDataToMap.Size = new System.Drawing.Size(278, 23);
            this.btnNWISLoadDataToMap.TabIndex = 78;
            this.btnNWISLoadDataToMap.Text = "Load Data to Map";
            this.btnNWISLoadDataToMap.UseVisualStyleBackColor = true;
            this.btnNWISLoadDataToMap.Visible = false;
            this.btnNWISLoadDataToMap.Click += new System.EventHandler(this.btnNWISLoadDataToMap_Click);
            // 
            // groupNWISspecific
            // 
            this.groupNWISspecific.Controls.Add(this.listNWISDataTypesSpecific);
            this.groupNWISspecific.Location = new System.Drawing.Point(102, 323);
            this.groupNWISspecific.Name = "groupNWISspecific";
            this.groupNWISspecific.Size = new System.Drawing.Size(444, 113);
            this.groupNWISspecific.TabIndex = 79;
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
            // groupNWIShistorical
            // 
            this.groupNWIShistorical.Controls.Add(this.listStationIDs);
            this.groupNWIShistorical.Controls.Add(this.label23);
            this.groupNWIShistorical.Controls.Add(this.btnNWIShistoricalData);
            this.groupNWIShistorical.Location = new System.Drawing.Point(108, 524);
            this.groupNWIShistorical.Name = "groupNWIShistorical";
            this.groupNWIShistorical.Size = new System.Drawing.Size(369, 139);
            this.groupNWIShistorical.TabIndex = 80;
            this.groupNWIShistorical.TabStop = false;
            this.groupNWIShistorical.Text = "Time-Series Data";
            this.groupNWIShistorical.Visible = false;
            // 
            // listStationIDs
            // 
            this.listStationIDs.CheckOnClick = true;
            this.listStationIDs.FormattingEnabled = true;
            this.listStationIDs.Location = new System.Drawing.Point(118, 14);
            this.listStationIDs.Name = "listStationIDs";
            this.listStationIDs.Size = new System.Drawing.Size(112, 94);
            this.listStationIDs.TabIndex = 5;
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
            // btnNWIShistoricalData
            // 
            this.btnNWIShistoricalData.Location = new System.Drawing.Point(249, 14);
            this.btnNWIShistoricalData.Name = "btnNWIShistoricalData";
            this.btnNWIShistoricalData.Size = new System.Drawing.Size(100, 100);
            this.btnNWIShistoricalData.TabIndex = 0;
            this.btnNWIShistoricalData.Text = "Show Discharge Time-Series Data";
            this.btnNWIShistoricalData.UseVisualStyleBackColor = true;
            this.btnNWIShistoricalData.Click += new System.EventHandler(this.btnNWIShistoricalData_Click);
            // 
            // NWISBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 714);
            this.Controls.Add(this.groupNWIShistorical);
            this.Controls.Add(this.groupNWISspecific);
            this.Controls.Add(this.btnNWISLoadDataToMap);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelNWIS);
            this.Name = "NWISBox";
            this.Text = "NWISBox";
            this.Load += new System.EventHandler(this.NWISBox_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupNWISspecific.ResumeLayout(false);
            this.groupNWIShistorical.ResumeLayout(false);
            this.groupNWIShistorical.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Label labelNWIS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDowloadNWISUsingStationIds;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.ListBox listNWISStations;
        private System.Windows.Forms.Button btnAddStations;
        private System.Windows.Forms.TextBox txtNWISStation;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnNWISLoadDataToMap;
        private System.Windows.Forms.GroupBox groupNWISspecific;
        private System.Windows.Forms.CheckedListBox listNWISDataTypesSpecific;
        private System.Windows.Forms.GroupBox groupNWIShistorical;
        private System.Windows.Forms.CheckedListBox listStationIDs;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnNWIShistoricalData;
    }
}