namespace SDPProjectBuilderPlugin
{
    partial class frmSDPProjectBuilderProject
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
            this.tabRun = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRunStatus = new System.Windows.Forms.TextBox();
            this.lbScienceModules = new System.Windows.Forms.ListBox();
            this.lbSourceTypes = new System.Windows.Forms.ListBox();
            this.chkForceFullDependency = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tabDatabaseConnection = new System.Windows.Forms.TabPage();
            this.btnGetDatabases = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.cboDatabaseName = new System.Windows.Forms.ComboBox();
            this.txtNumberOfRetries = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtLengthOfTimeout = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabCreateSource = new System.Windows.Forms.TabPage();
            this.groupAOIBuffer = new System.Windows.Forms.GroupBox();
            this.btnClearDrawingAOIBuffer = new System.Windows.Forms.Button();
            this.txtBuffer = new System.Windows.Forms.TextBox();
            this.lblRadiusBuffer = new System.Windows.Forms.Label();
            this.btnSaveBufferAOIBuffer = new System.Windows.Forms.Button();
            this.groupAOICircle = new System.Windows.Forms.GroupBox();
            this.btnCancelAOICircle = new System.Windows.Forms.Button();
            this.btnClearDrawingAOICircle = new System.Windows.Forms.Button();
            this.btnUndoPointAOICircle = new System.Windows.Forms.Button();
            this.btnEditAOICircle = new System.Windows.Forms.Button();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.lblRadius = new System.Windows.Forms.Label();
            this.btnSaveCircleAOICircle = new System.Windows.Forms.Button();
            this.groupAOIShape = new System.Windows.Forms.GroupBox();
            this.btnClearDrawingAOIShape = new System.Windows.Forms.Button();
            this.btnCancelAOIShape = new System.Windows.Forms.Button();
            this.btnEditAOIShape = new System.Windows.Forms.Button();
            this.btnSavePolygonAOIShape = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.groupAOI = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbShape = new System.Windows.Forms.RadioButton();
            this.rbBuffer = new System.Windows.Forms.RadioButton();
            this.rbCircle = new System.Windows.Forms.RadioButton();
            this.rbPolygon = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.groupAOIPolygon = new System.Windows.Forms.GroupBox();
            this.btnCancelAOIPolygon = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnClearDrawingAOIPolygon = new System.Windows.Forms.Button();
            this.btnUndoPointAOIPolygon = new System.Windows.Forms.Button();
            this.btnSavePolygonAOIPolygon = new System.Windows.Forms.Button();
            this.btnEditAOIPolygon = new System.Windows.Forms.Button();
            this.groupSource = new System.Windows.Forms.GroupBox();
            this.btnCancelSource = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btnClearDrawingSource = new System.Windows.Forms.Button();
            this.btnUndoPointSource = new System.Windows.Forms.Button();
            this.btnSavePolygonSource = new System.Windows.Forms.Button();
            this.btnEditSource = new System.Windows.Forms.Button();
            this.tabNavigation = new System.Windows.Forms.TabPage();
            this.btnZoomToCoordinates = new System.Windows.Forms.Button();
            this.btnZoomToCounty = new System.Windows.Forms.Button();
            this.btnZoomToHUC = new System.Windows.Forms.Button();
            this.btnZoomToState = new System.Windows.Forms.Button();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.cboHUC = new System.Windows.Forms.ComboBox();
            this.cboCounty = new System.Windows.Forms.ComboBox();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabSourceDataLocation = new System.Windows.Forms.TabPage();
            this.chkDisregardCacheSoils = new System.Windows.Forms.CheckBox();
            this.chkDisregardCacheNLCD = new System.Windows.Forms.CheckBox();
            this.chkDisregardCacheNHDPlus = new System.Windows.Forms.CheckBox();
            this.btnBrowseSoilsLocation = new System.Windows.Forms.Button();
            this.btnBrowseNLCDLocation = new System.Windows.Forms.Button();
            this.btnBrowseNHDPlusLocation = new System.Windows.Forms.Button();
            this.txtSoilsLocation = new System.Windows.Forms.TextBox();
            this.txtNLCDLocation = new System.Windows.Forms.TextBox();
            this.txtNHDPlusLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabProjectFolders = new System.Windows.Forms.TabPage();
            this.btnCreateProjectFolders = new System.Windows.Forms.Button();
            this.btnBrowseIntermediateFolder = new System.Windows.Forms.Button();
            this.btnBrowseCacheFolder = new System.Windows.Forms.Button();
            this.btnBrowseProjectFolder = new System.Windows.Forms.Button();
            this.txtIntermediateFolder = new System.Windows.Forms.TextBox();
            this.txtCacheFolder = new System.Windows.Forms.TextBox();
            this.txtProjectFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnDone = new System.Windows.Forms.Button();
            this.tabRun.SuspendLayout();
            this.tabDatabaseConnection.SuspendLayout();
            this.tabCreateSource.SuspendLayout();
            this.groupAOIBuffer.SuspendLayout();
            this.groupAOICircle.SuspendLayout();
            this.groupAOIShape.SuspendLayout();
            this.groupAOI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupAOIPolygon.SuspendLayout();
            this.groupSource.SuspendLayout();
            this.tabNavigation.SuspendLayout();
            this.tabSourceDataLocation.SuspendLayout();
            this.tabProjectFolders.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRun
            // 
            this.tabRun.Controls.Add(this.label15);
            this.tabRun.Controls.Add(this.txtRunStatus);
            this.tabRun.Controls.Add(this.lbScienceModules);
            this.tabRun.Controls.Add(this.lbSourceTypes);
            this.tabRun.Controls.Add(this.chkForceFullDependency);
            this.tabRun.Controls.Add(this.btnRun);
            this.tabRun.Controls.Add(this.txtSourceName);
            this.tabRun.Controls.Add(this.label27);
            this.tabRun.Controls.Add(this.label28);
            this.tabRun.Controls.Add(this.label29);
            this.tabRun.Location = new System.Drawing.Point(4, 22);
            this.tabRun.Name = "tabRun";
            this.tabRun.Size = new System.Drawing.Size(608, 422);
            this.tabRun.TabIndex = 6;
            this.tabRun.Text = "Run";
            this.tabRun.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 249);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Status:";
            // 
            // txtRunStatus
            // 
            this.txtRunStatus.Location = new System.Drawing.Point(23, 275);
            this.txtRunStatus.Multiline = true;
            this.txtRunStatus.Name = "txtRunStatus";
            this.txtRunStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRunStatus.Size = new System.Drawing.Size(563, 124);
            this.txtRunStatus.TabIndex = 27;
            // 
            // lbScienceModules
            // 
            this.lbScienceModules.FormattingEnabled = true;
            this.lbScienceModules.Location = new System.Drawing.Point(418, 57);
            this.lbScienceModules.Name = "lbScienceModules";
            this.lbScienceModules.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbScienceModules.Size = new System.Drawing.Size(168, 199);
            this.lbScienceModules.TabIndex = 26;
            // 
            // lbSourceTypes
            // 
            this.lbSourceTypes.FormattingEnabled = true;
            this.lbSourceTypes.Location = new System.Drawing.Point(113, 57);
            this.lbSourceTypes.Name = "lbSourceTypes";
            this.lbSourceTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSourceTypes.Size = new System.Drawing.Size(168, 95);
            this.lbSourceTypes.TabIndex = 25;
            // 
            // chkForceFullDependency
            // 
            this.chkForceFullDependency.AutoSize = true;
            this.chkForceFullDependency.Location = new System.Drawing.Point(313, 19);
            this.chkForceFullDependency.Name = "chkForceFullDependency";
            this.chkForceFullDependency.Size = new System.Drawing.Size(136, 17);
            this.chkForceFullDependency.TabIndex = 24;
            this.chkForceFullDependency.Text = "Force Full Dependency";
            this.chkForceFullDependency.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(113, 187);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(62, 23);
            this.btnRun.TabIndex = 23;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtSourceName
            // 
            this.txtSourceName.Location = new System.Drawing.Point(113, 19);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(168, 20);
            this.txtSourceName.TabIndex = 20;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(310, 57);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(92, 13);
            this.label27.TabIndex = 19;
            this.label27.Text = "Science Modules:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(20, 57);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(76, 13);
            this.label28.TabIndex = 18;
            this.label28.Text = "Source Types:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(20, 19);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(75, 13);
            this.label29.TabIndex = 17;
            this.label29.Text = "Source Name:";
            // 
            // tabDatabaseConnection
            // 
            this.tabDatabaseConnection.Controls.Add(this.btnGetDatabases);
            this.tabDatabaseConnection.Controls.Add(this.label30);
            this.tabDatabaseConnection.Controls.Add(this.cboDatabaseName);
            this.tabDatabaseConnection.Controls.Add(this.txtNumberOfRetries);
            this.tabDatabaseConnection.Controls.Add(this.label26);
            this.tabDatabaseConnection.Controls.Add(this.txtLengthOfTimeout);
            this.tabDatabaseConnection.Controls.Add(this.txtPort);
            this.tabDatabaseConnection.Controls.Add(this.label23);
            this.tabDatabaseConnection.Controls.Add(this.label24);
            this.tabDatabaseConnection.Controls.Add(this.label25);
            this.tabDatabaseConnection.Controls.Add(this.btnTestConnection);
            this.tabDatabaseConnection.Controls.Add(this.txtPassword);
            this.tabDatabaseConnection.Controls.Add(this.txtUsername);
            this.tabDatabaseConnection.Controls.Add(this.txtServer);
            this.tabDatabaseConnection.Controls.Add(this.label20);
            this.tabDatabaseConnection.Controls.Add(this.label21);
            this.tabDatabaseConnection.Controls.Add(this.label22);
            this.tabDatabaseConnection.Location = new System.Drawing.Point(4, 22);
            this.tabDatabaseConnection.Name = "tabDatabaseConnection";
            this.tabDatabaseConnection.Size = new System.Drawing.Size(608, 422);
            this.tabDatabaseConnection.TabIndex = 5;
            this.tabDatabaseConnection.Text = "Database Connection";
            this.tabDatabaseConnection.UseVisualStyleBackColor = true;
            // 
            // btnGetDatabases
            // 
            this.btnGetDatabases.Location = new System.Drawing.Point(378, 202);
            this.btnGetDatabases.Name = "btnGetDatabases";
            this.btnGetDatabases.Size = new System.Drawing.Size(119, 23);
            this.btnGetDatabases.TabIndex = 27;
            this.btnGetDatabases.Text = "Get Databases";
            this.btnGetDatabases.UseVisualStyleBackColor = true;
            this.btnGetDatabases.Click += new System.EventHandler(this.btnGetDatabases_Click);
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(248, 163);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(249, 43);
            this.label30.TabIndex = 26;
            this.label30.Text = "Enter all values except Database Name, then click \"Get Databases\".   Select Datab" +
                "ase Name from drop-down list.";
            // 
            // cboDatabaseName
            // 
            this.cboDatabaseName.FormattingEnabled = true;
            this.cboDatabaseName.Location = new System.Drawing.Point(179, 239);
            this.cboDatabaseName.Name = "cboDatabaseName";
            this.cboDatabaseName.Size = new System.Drawing.Size(238, 21);
            this.cboDatabaseName.TabIndex = 25;
            // 
            // txtNumberOfRetries
            // 
            this.txtNumberOfRetries.Location = new System.Drawing.Point(179, 200);
            this.txtNumberOfRetries.Name = "txtNumberOfRetries";
            this.txtNumberOfRetries.Size = new System.Drawing.Size(54, 20);
            this.txtNumberOfRetries.TabIndex = 24;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(48, 202);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 13);
            this.label26.TabIndex = 23;
            this.label26.Text = "Number of Retries:";
            // 
            // txtLengthOfTimeout
            // 
            this.txtLengthOfTimeout.Location = new System.Drawing.Point(179, 163);
            this.txtLengthOfTimeout.Name = "txtLengthOfTimeout";
            this.txtLengthOfTimeout.Size = new System.Drawing.Size(54, 20);
            this.txtLengthOfTimeout.TabIndex = 22;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(179, 127);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(318, 20);
            this.txtPort.TabIndex = 20;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(48, 165);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(122, 13);
            this.label23.TabIndex = 19;
            this.label23.Text = "Length of Timeout (sec):";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(48, 239);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(87, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "Database Name:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(48, 127);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "Port Number:";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(179, 276);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(119, 23);
            this.btnTestConnection.TabIndex = 16;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(179, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(318, 20);
            this.txtPassword.TabIndex = 15;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(179, 50);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(318, 20);
            this.txtUsername.TabIndex = 14;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(179, 14);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(318, 20);
            this.txtServer.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(48, 90);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "Password:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(48, 52);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "User ID:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(48, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 10;
            this.label22.Text = "Server:";
            // 
            // tabCreateSource
            // 
            this.tabCreateSource.Controls.Add(this.groupAOIBuffer);
            this.tabCreateSource.Controls.Add(this.groupAOICircle);
            this.tabCreateSource.Controls.Add(this.groupAOIShape);
            this.tabCreateSource.Controls.Add(this.groupAOI);
            this.tabCreateSource.Controls.Add(this.groupSource);
            this.tabCreateSource.Location = new System.Drawing.Point(4, 22);
            this.tabCreateSource.Name = "tabCreateSource";
            this.tabCreateSource.Size = new System.Drawing.Size(608, 422);
            this.tabCreateSource.TabIndex = 3;
            this.tabCreateSource.Text = "Source / Area Of Interest";
            this.tabCreateSource.UseVisualStyleBackColor = true;
            // 
            // groupAOIBuffer
            // 
            this.groupAOIBuffer.Controls.Add(this.btnClearDrawingAOIBuffer);
            this.groupAOIBuffer.Controls.Add(this.txtBuffer);
            this.groupAOIBuffer.Controls.Add(this.lblRadiusBuffer);
            this.groupAOIBuffer.Controls.Add(this.btnSaveBufferAOIBuffer);
            this.groupAOIBuffer.Location = new System.Drawing.Point(37, 258);
            this.groupAOIBuffer.Name = "groupAOIBuffer";
            this.groupAOIBuffer.Size = new System.Drawing.Size(345, 78);
            this.groupAOIBuffer.TabIndex = 40;
            this.groupAOIBuffer.TabStop = false;
            // 
            // btnClearDrawingAOIBuffer
            // 
            this.btnClearDrawingAOIBuffer.Location = new System.Drawing.Point(112, 43);
            this.btnClearDrawingAOIBuffer.Name = "btnClearDrawingAOIBuffer";
            this.btnClearDrawingAOIBuffer.Size = new System.Drawing.Size(121, 23);
            this.btnClearDrawingAOIBuffer.TabIndex = 43;
            this.btnClearDrawingAOIBuffer.Text = "Clear Drawing";
            this.btnClearDrawingAOIBuffer.UseVisualStyleBackColor = true;
            this.btnClearDrawingAOIBuffer.Click += new System.EventHandler(this.btnClearDrawingAOIBuffer_Click);
            // 
            // txtBuffer
            // 
            this.txtBuffer.Location = new System.Drawing.Point(124, 15);
            this.txtBuffer.Name = "txtBuffer";
            this.txtBuffer.Size = new System.Drawing.Size(64, 20);
            this.txtBuffer.TabIndex = 39;
            this.txtBuffer.Text = "1000";
            // 
            // lblRadiusBuffer
            // 
            this.lblRadiusBuffer.Location = new System.Drawing.Point(9, 16);
            this.lblRadiusBuffer.Name = "lblRadiusBuffer";
            this.lblRadiusBuffer.Size = new System.Drawing.Size(111, 16);
            this.lblRadiusBuffer.TabIndex = 38;
            this.lblRadiusBuffer.Text = "Enter buffer (meters):";
            // 
            // btnSaveBufferAOIBuffer
            // 
            this.btnSaveBufferAOIBuffer.Location = new System.Drawing.Point(239, 43);
            this.btnSaveBufferAOIBuffer.Name = "btnSaveBufferAOIBuffer";
            this.btnSaveBufferAOIBuffer.Size = new System.Drawing.Size(100, 23);
            this.btnSaveBufferAOIBuffer.TabIndex = 37;
            this.btnSaveBufferAOIBuffer.Text = "Save Buffer";
            this.btnSaveBufferAOIBuffer.UseVisualStyleBackColor = true;
            this.btnSaveBufferAOIBuffer.Click += new System.EventHandler(this.btnSaveBufferAOIBuffer_Click);
            // 
            // groupAOICircle
            // 
            this.groupAOICircle.Controls.Add(this.btnCancelAOICircle);
            this.groupAOICircle.Controls.Add(this.btnClearDrawingAOICircle);
            this.groupAOICircle.Controls.Add(this.btnUndoPointAOICircle);
            this.groupAOICircle.Controls.Add(this.btnEditAOICircle);
            this.groupAOICircle.Controls.Add(this.txtRadius);
            this.groupAOICircle.Controls.Add(this.lblRadius);
            this.groupAOICircle.Controls.Add(this.btnSaveCircleAOICircle);
            this.groupAOICircle.Location = new System.Drawing.Point(38, 257);
            this.groupAOICircle.Name = "groupAOICircle";
            this.groupAOICircle.Size = new System.Drawing.Size(345, 113);
            this.groupAOICircle.TabIndex = 95;
            this.groupAOICircle.TabStop = false;
            // 
            // btnCancelAOICircle
            // 
            this.btnCancelAOICircle.Location = new System.Drawing.Point(281, 42);
            this.btnCancelAOICircle.Name = "btnCancelAOICircle";
            this.btnCancelAOICircle.Size = new System.Drawing.Size(57, 23);
            this.btnCancelAOICircle.TabIndex = 43;
            this.btnCancelAOICircle.Text = "Cancel";
            this.btnCancelAOICircle.UseVisualStyleBackColor = true;
            this.btnCancelAOICircle.Click += new System.EventHandler(this.btnCancelAOICircle_Click);
            // 
            // btnClearDrawingAOICircle
            // 
            this.btnClearDrawingAOICircle.Location = new System.Drawing.Point(10, 78);
            this.btnClearDrawingAOICircle.Name = "btnClearDrawingAOICircle";
            this.btnClearDrawingAOICircle.Size = new System.Drawing.Size(121, 23);
            this.btnClearDrawingAOICircle.TabIndex = 42;
            this.btnClearDrawingAOICircle.Text = "Clear Drawing";
            this.btnClearDrawingAOICircle.UseVisualStyleBackColor = true;
            this.btnClearDrawingAOICircle.Click += new System.EventHandler(this.btnClearDrawingAOICircle_Click);
            // 
            // btnUndoPointAOICircle
            // 
            this.btnUndoPointAOICircle.Location = new System.Drawing.Point(149, 78);
            this.btnUndoPointAOICircle.Name = "btnUndoPointAOICircle";
            this.btnUndoPointAOICircle.Size = new System.Drawing.Size(75, 23);
            this.btnUndoPointAOICircle.TabIndex = 41;
            this.btnUndoPointAOICircle.Text = "Undo Point";
            this.btnUndoPointAOICircle.UseVisualStyleBackColor = true;
            this.btnUndoPointAOICircle.Click += new System.EventHandler(this.btnUndoPointAOICircle_Click);
            // 
            // btnEditAOICircle
            // 
            this.btnEditAOICircle.Location = new System.Drawing.Point(280, 13);
            this.btnEditAOICircle.Name = "btnEditAOICircle";
            this.btnEditAOICircle.Size = new System.Drawing.Size(57, 23);
            this.btnEditAOICircle.TabIndex = 40;
            this.btnEditAOICircle.Text = "Edit";
            this.btnEditAOICircle.UseVisualStyleBackColor = true;
            this.btnEditAOICircle.Click += new System.EventHandler(this.btnEditAOICircle_Click);
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(124, 32);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(64, 20);
            this.txtRadius.TabIndex = 39;
            this.txtRadius.Text = "1000";
            // 
            // lblRadius
            // 
            this.lblRadius.Location = new System.Drawing.Point(9, 33);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(111, 16);
            this.lblRadius.TabIndex = 38;
            this.lblRadius.Text = "Enter radius (meters):";
            // 
            // btnSaveCircleAOICircle
            // 
            this.btnSaveCircleAOICircle.Location = new System.Drawing.Point(237, 78);
            this.btnSaveCircleAOICircle.Name = "btnSaveCircleAOICircle";
            this.btnSaveCircleAOICircle.Size = new System.Drawing.Size(100, 23);
            this.btnSaveCircleAOICircle.TabIndex = 37;
            this.btnSaveCircleAOICircle.Text = "Save Circle";
            this.btnSaveCircleAOICircle.UseVisualStyleBackColor = true;
            this.btnSaveCircleAOICircle.Click += new System.EventHandler(this.btnSaveCircleAOICircle_Click);
            // 
            // groupAOIShape
            // 
            this.groupAOIShape.Controls.Add(this.btnClearDrawingAOIShape);
            this.groupAOIShape.Controls.Add(this.btnCancelAOIShape);
            this.groupAOIShape.Controls.Add(this.btnEditAOIShape);
            this.groupAOIShape.Controls.Add(this.btnSavePolygonAOIShape);
            this.groupAOIShape.Controls.Add(this.label18);
            this.groupAOIShape.Location = new System.Drawing.Point(37, 255);
            this.groupAOIShape.Name = "groupAOIShape";
            this.groupAOIShape.Size = new System.Drawing.Size(345, 79);
            this.groupAOIShape.TabIndex = 37;
            this.groupAOIShape.TabStop = false;
            // 
            // btnClearDrawingAOIShape
            // 
            this.btnClearDrawingAOIShape.Location = new System.Drawing.Point(12, 49);
            this.btnClearDrawingAOIShape.Name = "btnClearDrawingAOIShape";
            this.btnClearDrawingAOIShape.Size = new System.Drawing.Size(121, 23);
            this.btnClearDrawingAOIShape.TabIndex = 46;
            this.btnClearDrawingAOIShape.Text = "Clear Drawing";
            this.btnClearDrawingAOIShape.UseVisualStyleBackColor = true;
            this.btnClearDrawingAOIShape.Click += new System.EventHandler(this.btnClearDrawingAOIShape_Click);
            // 
            // btnCancelAOIShape
            // 
            this.btnCancelAOIShape.Location = new System.Drawing.Point(279, 42);
            this.btnCancelAOIShape.Name = "btnCancelAOIShape";
            this.btnCancelAOIShape.Size = new System.Drawing.Size(57, 23);
            this.btnCancelAOIShape.TabIndex = 45;
            this.btnCancelAOIShape.Text = "Cancel";
            this.btnCancelAOIShape.UseVisualStyleBackColor = true;
            this.btnCancelAOIShape.Click += new System.EventHandler(this.btnCancelAOIShape_Click);
            // 
            // btnEditAOIShape
            // 
            this.btnEditAOIShape.Location = new System.Drawing.Point(279, 13);
            this.btnEditAOIShape.Name = "btnEditAOIShape";
            this.btnEditAOIShape.Size = new System.Drawing.Size(57, 23);
            this.btnEditAOIShape.TabIndex = 44;
            this.btnEditAOIShape.Text = "Edit";
            this.btnEditAOIShape.UseVisualStyleBackColor = true;
            this.btnEditAOIShape.Click += new System.EventHandler(this.btnEditAOIShape_Click);
            // 
            // btnSavePolygonAOIShape
            // 
            this.btnSavePolygonAOIShape.Location = new System.Drawing.Point(148, 50);
            this.btnSavePolygonAOIShape.Name = "btnSavePolygonAOIShape";
            this.btnSavePolygonAOIShape.Size = new System.Drawing.Size(100, 23);
            this.btnSavePolygonAOIShape.TabIndex = 35;
            this.btnSavePolygonAOIShape.Text = "Save Polygon";
            this.btnSavePolygonAOIShape.UseVisualStyleBackColor = true;
            this.btnSavePolygonAOIShape.Click += new System.EventHandler(this.btnSavePolygonAOIShape_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(9, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(268, 33);
            this.label18.TabIndex = 34;
            this.label18.Text = "Select a layer on the map, then click the map to select the layer.  Use shift to " +
                "select multiple shapes.";
            // 
            // groupAOI
            // 
            this.groupAOI.Controls.Add(this.groupBox1);
            this.groupAOI.Controls.Add(this.label19);
            this.groupAOI.Controls.Add(this.groupAOIPolygon);
            this.groupAOI.Location = new System.Drawing.Point(24, 172);
            this.groupAOI.Name = "groupAOI";
            this.groupAOI.Size = new System.Drawing.Size(370, 217);
            this.groupAOI.TabIndex = 29;
            this.groupAOI.TabStop = false;
            this.groupAOI.Text = "Area Of Interest";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbShape);
            this.groupBox1.Controls.Add(this.rbBuffer);
            this.groupBox1.Controls.Add(this.rbCircle);
            this.groupBox1.Controls.Add(this.rbPolygon);
            this.groupBox1.Location = new System.Drawing.Point(22, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 40);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // rbShape
            // 
            this.rbShape.AutoSize = true;
            this.rbShape.Location = new System.Drawing.Point(261, 14);
            this.rbShape.Name = "rbShape";
            this.rbShape.Size = new System.Drawing.Size(56, 17);
            this.rbShape.TabIndex = 41;
            this.rbShape.TabStop = true;
            this.rbShape.Text = "Shape";
            this.rbShape.UseVisualStyleBackColor = true;
            this.rbShape.CheckedChanged += new System.EventHandler(this.rbShape_CheckedChanged);
            // 
            // rbBuffer
            // 
            this.rbBuffer.AutoSize = true;
            this.rbBuffer.Location = new System.Drawing.Point(180, 14);
            this.rbBuffer.Name = "rbBuffer";
            this.rbBuffer.Size = new System.Drawing.Size(53, 17);
            this.rbBuffer.TabIndex = 40;
            this.rbBuffer.TabStop = true;
            this.rbBuffer.Text = "Buffer";
            this.rbBuffer.UseVisualStyleBackColor = true;
            this.rbBuffer.CheckedChanged += new System.EventHandler(this.rbBuffer_CheckedChanged);
            // 
            // rbCircle
            // 
            this.rbCircle.AutoSize = true;
            this.rbCircle.Location = new System.Drawing.Point(99, 14);
            this.rbCircle.Name = "rbCircle";
            this.rbCircle.Size = new System.Drawing.Size(51, 17);
            this.rbCircle.TabIndex = 39;
            this.rbCircle.TabStop = true;
            this.rbCircle.Text = "Circle";
            this.rbCircle.UseVisualStyleBackColor = true;
            this.rbCircle.CheckedChanged += new System.EventHandler(this.rbCircle_CheckedChanged);
            // 
            // rbPolygon
            // 
            this.rbPolygon.AutoSize = true;
            this.rbPolygon.Location = new System.Drawing.Point(14, 14);
            this.rbPolygon.Name = "rbPolygon";
            this.rbPolygon.Size = new System.Drawing.Size(63, 17);
            this.rbPolygon.TabIndex = 38;
            this.rbPolygon.TabStop = true;
            this.rbPolygon.Text = "Polygon";
            this.rbPolygon.UseVisualStyleBackColor = true;
            this.rbPolygon.CheckedChanged += new System.EventHandler(this.rbPolygon_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 13);
            this.label19.TabIndex = 32;
            this.label19.Text = "Choose the type of AOI:";
            // 
            // groupAOIPolygon
            // 
            this.groupAOIPolygon.Controls.Add(this.btnCancelAOIPolygon);
            this.groupAOIPolygon.Controls.Add(this.label16);
            this.groupAOIPolygon.Controls.Add(this.btnClearDrawingAOIPolygon);
            this.groupAOIPolygon.Controls.Add(this.btnUndoPointAOIPolygon);
            this.groupAOIPolygon.Controls.Add(this.btnSavePolygonAOIPolygon);
            this.groupAOIPolygon.Controls.Add(this.btnEditAOIPolygon);
            this.groupAOIPolygon.Location = new System.Drawing.Point(12, 83);
            this.groupAOIPolygon.Name = "groupAOIPolygon";
            this.groupAOIPolygon.Size = new System.Drawing.Size(345, 113);
            this.groupAOIPolygon.TabIndex = 30;
            this.groupAOIPolygon.TabStop = false;
            // 
            // btnCancelAOIPolygon
            // 
            this.btnCancelAOIPolygon.Location = new System.Drawing.Point(282, 44);
            this.btnCancelAOIPolygon.Name = "btnCancelAOIPolygon";
            this.btnCancelAOIPolygon.Size = new System.Drawing.Size(57, 23);
            this.btnCancelAOIPolygon.TabIndex = 37;
            this.btnCancelAOIPolygon.Text = "Cancel";
            this.btnCancelAOIPolygon.UseVisualStyleBackColor = true;
            this.btnCancelAOIPolygon.Click += new System.EventHandler(this.btnCancelAOIPolygon_Click);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(268, 47);
            this.label16.TabIndex = 36;
            this.label16.Text = "Click \"Edit\", then click on the map to add polygon vertices.  You must have at le" +
                "ast 3 points.  When you are finished, click \"Save Polygon.\"";
            // 
            // btnClearDrawingAOIPolygon
            // 
            this.btnClearDrawingAOIPolygon.Location = new System.Drawing.Point(11, 78);
            this.btnClearDrawingAOIPolygon.Name = "btnClearDrawingAOIPolygon";
            this.btnClearDrawingAOIPolygon.Size = new System.Drawing.Size(121, 23);
            this.btnClearDrawingAOIPolygon.TabIndex = 35;
            this.btnClearDrawingAOIPolygon.Text = "Clear Drawing";
            this.btnClearDrawingAOIPolygon.UseVisualStyleBackColor = true;
            this.btnClearDrawingAOIPolygon.Click += new System.EventHandler(this.btnClearDrawingAOIPolygon_Click);
            // 
            // btnUndoPointAOIPolygon
            // 
            this.btnUndoPointAOIPolygon.Location = new System.Drawing.Point(149, 78);
            this.btnUndoPointAOIPolygon.Name = "btnUndoPointAOIPolygon";
            this.btnUndoPointAOIPolygon.Size = new System.Drawing.Size(75, 23);
            this.btnUndoPointAOIPolygon.TabIndex = 34;
            this.btnUndoPointAOIPolygon.Text = "Undo Point";
            this.btnUndoPointAOIPolygon.UseVisualStyleBackColor = true;
            this.btnUndoPointAOIPolygon.Click += new System.EventHandler(this.btnUndoPointAOIPolygon_Click);
            // 
            // btnSavePolygonAOIPolygon
            // 
            this.btnSavePolygonAOIPolygon.Location = new System.Drawing.Point(240, 78);
            this.btnSavePolygonAOIPolygon.Name = "btnSavePolygonAOIPolygon";
            this.btnSavePolygonAOIPolygon.Size = new System.Drawing.Size(100, 23);
            this.btnSavePolygonAOIPolygon.TabIndex = 33;
            this.btnSavePolygonAOIPolygon.Text = "Save Polygon";
            this.btnSavePolygonAOIPolygon.UseVisualStyleBackColor = true;
            this.btnSavePolygonAOIPolygon.Click += new System.EventHandler(this.btnSavePolygonAOIPolygon_Click);
            // 
            // btnEditAOIPolygon
            // 
            this.btnEditAOIPolygon.Location = new System.Drawing.Point(283, 15);
            this.btnEditAOIPolygon.Name = "btnEditAOIPolygon";
            this.btnEditAOIPolygon.Size = new System.Drawing.Size(57, 23);
            this.btnEditAOIPolygon.TabIndex = 32;
            this.btnEditAOIPolygon.Text = "Edit";
            this.btnEditAOIPolygon.UseVisualStyleBackColor = true;
            this.btnEditAOIPolygon.Click += new System.EventHandler(this.btnEditAOIPolygon_Click);
            // 
            // groupSource
            // 
            this.groupSource.Controls.Add(this.btnCancelSource);
            this.groupSource.Controls.Add(this.label17);
            this.groupSource.Controls.Add(this.btnClearDrawingSource);
            this.groupSource.Controls.Add(this.btnUndoPointSource);
            this.groupSource.Controls.Add(this.btnSavePolygonSource);
            this.groupSource.Controls.Add(this.btnEditSource);
            this.groupSource.Location = new System.Drawing.Point(24, 18);
            this.groupSource.Name = "groupSource";
            this.groupSource.Size = new System.Drawing.Size(370, 142);
            this.groupSource.TabIndex = 28;
            this.groupSource.TabStop = false;
            this.groupSource.Text = "Source";
            // 
            // btnCancelSource
            // 
            this.btnCancelSource.Enabled = false;
            this.btnCancelSource.Location = new System.Drawing.Point(294, 56);
            this.btnCancelSource.Name = "btnCancelSource";
            this.btnCancelSource.Size = new System.Drawing.Size(57, 23);
            this.btnCancelSource.TabIndex = 13;
            this.btnCancelSource.Text = "Cancel";
            this.btnCancelSource.UseVisualStyleBackColor = true;
            this.btnCancelSource.Click += new System.EventHandler(this.btnCancelSource_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(20, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(268, 47);
            this.label17.TabIndex = 12;
            this.label17.Text = "Click \"Edit\", then click on the map to add polygon vertices.  You must have at le" +
                "ast 3 points.  When you are finished, click \"Save Polygon.\"";
            // 
            // btnClearDrawingSource
            // 
            this.btnClearDrawingSource.Location = new System.Drawing.Point(22, 90);
            this.btnClearDrawingSource.Name = "btnClearDrawingSource";
            this.btnClearDrawingSource.Size = new System.Drawing.Size(121, 23);
            this.btnClearDrawingSource.TabIndex = 11;
            this.btnClearDrawingSource.Text = "Clear Drawing";
            this.btnClearDrawingSource.UseVisualStyleBackColor = true;
            this.btnClearDrawingSource.Click += new System.EventHandler(this.btnClearDrawingSource_Click);
            // 
            // btnUndoPointSource
            // 
            this.btnUndoPointSource.Enabled = false;
            this.btnUndoPointSource.Location = new System.Drawing.Point(160, 90);
            this.btnUndoPointSource.Name = "btnUndoPointSource";
            this.btnUndoPointSource.Size = new System.Drawing.Size(75, 23);
            this.btnUndoPointSource.TabIndex = 10;
            this.btnUndoPointSource.Text = "Undo Point";
            this.btnUndoPointSource.UseVisualStyleBackColor = true;
            this.btnUndoPointSource.Click += new System.EventHandler(this.btnUndoPointSource_Click);
            // 
            // btnSavePolygonSource
            // 
            this.btnSavePolygonSource.Enabled = false;
            this.btnSavePolygonSource.Location = new System.Drawing.Point(251, 90);
            this.btnSavePolygonSource.Name = "btnSavePolygonSource";
            this.btnSavePolygonSource.Size = new System.Drawing.Size(100, 23);
            this.btnSavePolygonSource.TabIndex = 9;
            this.btnSavePolygonSource.Text = "Save Polygon";
            this.btnSavePolygonSource.UseVisualStyleBackColor = true;
            this.btnSavePolygonSource.Click += new System.EventHandler(this.btnSavePolygonSource_Click);
            // 
            // btnEditSource
            // 
            this.btnEditSource.Location = new System.Drawing.Point(294, 26);
            this.btnEditSource.Name = "btnEditSource";
            this.btnEditSource.Size = new System.Drawing.Size(57, 23);
            this.btnEditSource.TabIndex = 8;
            this.btnEditSource.Text = "Edit";
            this.btnEditSource.UseVisualStyleBackColor = true;
            this.btnEditSource.Click += new System.EventHandler(this.btnEditSource_Click);
            // 
            // tabNavigation
            // 
            this.tabNavigation.Controls.Add(this.btnZoomToCoordinates);
            this.tabNavigation.Controls.Add(this.btnZoomToCounty);
            this.tabNavigation.Controls.Add(this.btnZoomToHUC);
            this.tabNavigation.Controls.Add(this.btnZoomToState);
            this.tabNavigation.Controls.Add(this.txtLongitude);
            this.tabNavigation.Controls.Add(this.txtLatitude);
            this.tabNavigation.Controls.Add(this.cboHUC);
            this.tabNavigation.Controls.Add(this.cboCounty);
            this.tabNavigation.Controls.Add(this.cboState);
            this.tabNavigation.Controls.Add(this.label12);
            this.tabNavigation.Controls.Add(this.label13);
            this.tabNavigation.Controls.Add(this.label14);
            this.tabNavigation.Controls.Add(this.label11);
            this.tabNavigation.Controls.Add(this.label10);
            this.tabNavigation.Controls.Add(this.label9);
            this.tabNavigation.Controls.Add(this.label8);
            this.tabNavigation.Controls.Add(this.label7);
            this.tabNavigation.Location = new System.Drawing.Point(4, 22);
            this.tabNavigation.Name = "tabNavigation";
            this.tabNavigation.Size = new System.Drawing.Size(608, 422);
            this.tabNavigation.TabIndex = 2;
            this.tabNavigation.Text = "Map Navigation";
            this.tabNavigation.UseVisualStyleBackColor = true;
            // 
            // btnZoomToCoordinates
            // 
            this.btnZoomToCoordinates.Location = new System.Drawing.Point(280, 210);
            this.btnZoomToCoordinates.Name = "btnZoomToCoordinates";
            this.btnZoomToCoordinates.Size = new System.Drawing.Size(43, 23);
            this.btnZoomToCoordinates.TabIndex = 16;
            this.btnZoomToCoordinates.Text = "GO";
            this.btnZoomToCoordinates.UseVisualStyleBackColor = true;
            this.btnZoomToCoordinates.Click += new System.EventHandler(this.btnZoomToCoordinates_Click);
            // 
            // btnZoomToCounty
            // 
            this.btnZoomToCounty.Location = new System.Drawing.Point(280, 69);
            this.btnZoomToCounty.Name = "btnZoomToCounty";
            this.btnZoomToCounty.Size = new System.Drawing.Size(43, 23);
            this.btnZoomToCounty.TabIndex = 15;
            this.btnZoomToCounty.Text = "GO";
            this.btnZoomToCounty.UseVisualStyleBackColor = true;
            this.btnZoomToCounty.Click += new System.EventHandler(this.btnZoomToCounty_Click);
            // 
            // btnZoomToHUC
            // 
            this.btnZoomToHUC.Location = new System.Drawing.Point(280, 125);
            this.btnZoomToHUC.Name = "btnZoomToHUC";
            this.btnZoomToHUC.Size = new System.Drawing.Size(43, 23);
            this.btnZoomToHUC.TabIndex = 14;
            this.btnZoomToHUC.Text = "GO";
            this.btnZoomToHUC.UseVisualStyleBackColor = true;
            this.btnZoomToHUC.Click += new System.EventHandler(this.btnZoomToHUC_Click);
            // 
            // btnZoomToState
            // 
            this.btnZoomToState.Location = new System.Drawing.Point(280, 40);
            this.btnZoomToState.Name = "btnZoomToState";
            this.btnZoomToState.Size = new System.Drawing.Size(43, 23);
            this.btnZoomToState.TabIndex = 13;
            this.btnZoomToState.Text = "GO";
            this.btnZoomToState.UseVisualStyleBackColor = true;
            this.btnZoomToState.Click += new System.EventHandler(this.btnZoomToState_Click);
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(151, 210);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(119, 20);
            this.txtLongitude.TabIndex = 12;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(151, 181);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(119, 20);
            this.txtLatitude.TabIndex = 11;
            // 
            // cboHUC
            // 
            this.cboHUC.FormattingEnabled = true;
            this.cboHUC.Location = new System.Drawing.Point(120, 125);
            this.cboHUC.Name = "cboHUC";
            this.cboHUC.Size = new System.Drawing.Size(150, 21);
            this.cboHUC.TabIndex = 10;
            // 
            // cboCounty
            // 
            this.cboCounty.FormattingEnabled = true;
            this.cboCounty.Location = new System.Drawing.Point(120, 69);
            this.cboCounty.Name = "cboCounty";
            this.cboCounty.Size = new System.Drawing.Size(150, 21);
            this.cboCounty.TabIndex = 9;
            // 
            // cboState
            // 
            this.cboState.FormattingEnabled = true;
            this.cboState.Location = new System.Drawing.Point(120, 40);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(150, 21);
            this.cboState.TabIndex = 8;
            this.cboState.TextChanged += new System.EventHandler(this.cboState_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(70, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Longitude (X):";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 181);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Latitude (Y):";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(41, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "3) Zoom to Coordinates:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(70, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "HUC:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "2) Zoom to NHD HUC8:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "County:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "State:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "1) Zoom to State or County:";
            // 
            // tabSourceDataLocation
            // 
            this.tabSourceDataLocation.Controls.Add(this.chkDisregardCacheSoils);
            this.tabSourceDataLocation.Controls.Add(this.chkDisregardCacheNLCD);
            this.tabSourceDataLocation.Controls.Add(this.chkDisregardCacheNHDPlus);
            this.tabSourceDataLocation.Controls.Add(this.btnBrowseSoilsLocation);
            this.tabSourceDataLocation.Controls.Add(this.btnBrowseNLCDLocation);
            this.tabSourceDataLocation.Controls.Add(this.btnBrowseNHDPlusLocation);
            this.tabSourceDataLocation.Controls.Add(this.txtSoilsLocation);
            this.tabSourceDataLocation.Controls.Add(this.txtNLCDLocation);
            this.tabSourceDataLocation.Controls.Add(this.txtNHDPlusLocation);
            this.tabSourceDataLocation.Controls.Add(this.label4);
            this.tabSourceDataLocation.Controls.Add(this.label5);
            this.tabSourceDataLocation.Controls.Add(this.label6);
            this.tabSourceDataLocation.Location = new System.Drawing.Point(4, 22);
            this.tabSourceDataLocation.Name = "tabSourceDataLocation";
            this.tabSourceDataLocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourceDataLocation.Size = new System.Drawing.Size(608, 422);
            this.tabSourceDataLocation.TabIndex = 1;
            this.tabSourceDataLocation.Text = "Source Data Location";
            this.tabSourceDataLocation.UseVisualStyleBackColor = true;
            // 
            // chkDisregardCacheSoils
            // 
            this.chkDisregardCacheSoils.AutoSize = true;
            this.chkDisregardCacheSoils.Location = new System.Drawing.Point(492, 107);
            this.chkDisregardCacheSoils.Name = "chkDisregardCacheSoils";
            this.chkDisregardCacheSoils.Size = new System.Drawing.Size(105, 17);
            this.chkDisregardCacheSoils.TabIndex = 20;
            this.chkDisregardCacheSoils.Text = "Disregard Cache";
            this.chkDisregardCacheSoils.UseVisualStyleBackColor = true;
            // 
            // chkDisregardCacheNLCD
            // 
            this.chkDisregardCacheNLCD.AutoSize = true;
            this.chkDisregardCacheNLCD.Location = new System.Drawing.Point(491, 69);
            this.chkDisregardCacheNLCD.Name = "chkDisregardCacheNLCD";
            this.chkDisregardCacheNLCD.Size = new System.Drawing.Size(105, 17);
            this.chkDisregardCacheNLCD.TabIndex = 19;
            this.chkDisregardCacheNLCD.Text = "Disregard Cache";
            this.chkDisregardCacheNLCD.UseVisualStyleBackColor = true;
            // 
            // chkDisregardCacheNHDPlus
            // 
            this.chkDisregardCacheNHDPlus.AutoSize = true;
            this.chkDisregardCacheNHDPlus.Location = new System.Drawing.Point(491, 31);
            this.chkDisregardCacheNHDPlus.Name = "chkDisregardCacheNHDPlus";
            this.chkDisregardCacheNHDPlus.Size = new System.Drawing.Size(105, 17);
            this.chkDisregardCacheNHDPlus.TabIndex = 18;
            this.chkDisregardCacheNHDPlus.Text = "Disregard Cache";
            this.chkDisregardCacheNHDPlus.UseVisualStyleBackColor = true;
            // 
            // btnBrowseSoilsLocation
            // 
            this.btnBrowseSoilsLocation.Location = new System.Drawing.Point(402, 107);
            this.btnBrowseSoilsLocation.Name = "btnBrowseSoilsLocation";
            this.btnBrowseSoilsLocation.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSoilsLocation.TabIndex = 17;
            this.btnBrowseSoilsLocation.Text = "Browse...";
            this.btnBrowseSoilsLocation.UseVisualStyleBackColor = true;
            this.btnBrowseSoilsLocation.Click += new System.EventHandler(this.btnBrowseSoilsLocation_Click);
            // 
            // btnBrowseNLCDLocation
            // 
            this.btnBrowseNLCDLocation.Location = new System.Drawing.Point(402, 69);
            this.btnBrowseNLCDLocation.Name = "btnBrowseNLCDLocation";
            this.btnBrowseNLCDLocation.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNLCDLocation.TabIndex = 16;
            this.btnBrowseNLCDLocation.Text = "Browse...";
            this.btnBrowseNLCDLocation.UseVisualStyleBackColor = true;
            this.btnBrowseNLCDLocation.Click += new System.EventHandler(this.btnBrowseNLCDLocation_Click);
            // 
            // btnBrowseNHDPlusLocation
            // 
            this.btnBrowseNHDPlusLocation.Location = new System.Drawing.Point(402, 31);
            this.btnBrowseNHDPlusLocation.Name = "btnBrowseNHDPlusLocation";
            this.btnBrowseNHDPlusLocation.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseNHDPlusLocation.TabIndex = 15;
            this.btnBrowseNHDPlusLocation.Text = "Browse...";
            this.btnBrowseNHDPlusLocation.UseVisualStyleBackColor = true;
            this.btnBrowseNHDPlusLocation.Click += new System.EventHandler(this.btnBrowseNHDPlusLocation_Click);
            // 
            // txtSoilsLocation
            // 
            this.txtSoilsLocation.Location = new System.Drawing.Point(78, 107);
            this.txtSoilsLocation.Name = "txtSoilsLocation";
            this.txtSoilsLocation.Size = new System.Drawing.Size(318, 20);
            this.txtSoilsLocation.TabIndex = 14;
            // 
            // txtNLCDLocation
            // 
            this.txtNLCDLocation.Location = new System.Drawing.Point(78, 69);
            this.txtNLCDLocation.Name = "txtNLCDLocation";
            this.txtNLCDLocation.Size = new System.Drawing.Size(318, 20);
            this.txtNLCDLocation.TabIndex = 13;
            // 
            // txtNHDPlusLocation
            // 
            this.txtNHDPlusLocation.Location = new System.Drawing.Point(78, 33);
            this.txtNHDPlusLocation.Name = "txtNHDPlusLocation";
            this.txtNHDPlusLocation.Size = new System.Drawing.Size(318, 20);
            this.txtNHDPlusLocation.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Soils:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "NLCD:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "NHD Plus:";
            // 
            // tabProjectFolders
            // 
            this.tabProjectFolders.Controls.Add(this.btnCreateProjectFolders);
            this.tabProjectFolders.Controls.Add(this.btnBrowseIntermediateFolder);
            this.tabProjectFolders.Controls.Add(this.btnBrowseCacheFolder);
            this.tabProjectFolders.Controls.Add(this.btnBrowseProjectFolder);
            this.tabProjectFolders.Controls.Add(this.txtIntermediateFolder);
            this.tabProjectFolders.Controls.Add(this.txtCacheFolder);
            this.tabProjectFolders.Controls.Add(this.txtProjectFolder);
            this.tabProjectFolders.Controls.Add(this.label3);
            this.tabProjectFolders.Controls.Add(this.label2);
            this.tabProjectFolders.Controls.Add(this.label1);
            this.tabProjectFolders.Location = new System.Drawing.Point(4, 22);
            this.tabProjectFolders.Name = "tabProjectFolders";
            this.tabProjectFolders.Padding = new System.Windows.Forms.Padding(3);
            this.tabProjectFolders.Size = new System.Drawing.Size(608, 422);
            this.tabProjectFolders.TabIndex = 0;
            this.tabProjectFolders.Text = "Project Folders";
            this.tabProjectFolders.UseVisualStyleBackColor = true;
            // 
            // btnCreateProjectFolders
            // 
            this.btnCreateProjectFolders.Location = new System.Drawing.Point(162, 141);
            this.btnCreateProjectFolders.Name = "btnCreateProjectFolders";
            this.btnCreateProjectFolders.Size = new System.Drawing.Size(140, 23);
            this.btnCreateProjectFolders.TabIndex = 9;
            this.btnCreateProjectFolders.Text = "Create Project Folders";
            this.btnCreateProjectFolders.UseVisualStyleBackColor = true;
            // 
            // btnBrowseIntermediateFolder
            // 
            this.btnBrowseIntermediateFolder.Location = new System.Drawing.Point(486, 100);
            this.btnBrowseIntermediateFolder.Name = "btnBrowseIntermediateFolder";
            this.btnBrowseIntermediateFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseIntermediateFolder.TabIndex = 8;
            this.btnBrowseIntermediateFolder.Text = "Browse...";
            this.btnBrowseIntermediateFolder.UseVisualStyleBackColor = true;
            this.btnBrowseIntermediateFolder.Click += new System.EventHandler(this.btnBrowseIntermediateFolder_Click);
            // 
            // btnBrowseCacheFolder
            // 
            this.btnBrowseCacheFolder.Location = new System.Drawing.Point(486, 62);
            this.btnBrowseCacheFolder.Name = "btnBrowseCacheFolder";
            this.btnBrowseCacheFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCacheFolder.TabIndex = 7;
            this.btnBrowseCacheFolder.Text = "Browse...";
            this.btnBrowseCacheFolder.UseVisualStyleBackColor = true;
            this.btnBrowseCacheFolder.Click += new System.EventHandler(this.btnBrowseCacheFolder_Click);
            // 
            // btnBrowseProjectFolder
            // 
            this.btnBrowseProjectFolder.Location = new System.Drawing.Point(486, 24);
            this.btnBrowseProjectFolder.Name = "btnBrowseProjectFolder";
            this.btnBrowseProjectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProjectFolder.TabIndex = 6;
            this.btnBrowseProjectFolder.Text = "Browse...";
            this.btnBrowseProjectFolder.UseVisualStyleBackColor = true;
            this.btnBrowseProjectFolder.Click += new System.EventHandler(this.btnBrowseProjectFolder_Click);
            // 
            // txtIntermediateFolder
            // 
            this.txtIntermediateFolder.Location = new System.Drawing.Point(162, 100);
            this.txtIntermediateFolder.Name = "txtIntermediateFolder";
            this.txtIntermediateFolder.Size = new System.Drawing.Size(318, 20);
            this.txtIntermediateFolder.TabIndex = 5;
            // 
            // txtCacheFolder
            // 
            this.txtCacheFolder.Location = new System.Drawing.Point(162, 62);
            this.txtCacheFolder.Name = "txtCacheFolder";
            this.txtCacheFolder.Size = new System.Drawing.Size(318, 20);
            this.txtCacheFolder.TabIndex = 4;
            // 
            // txtProjectFolder
            // 
            this.txtProjectFolder.Location = new System.Drawing.Point(162, 26);
            this.txtProjectFolder.Name = "txtProjectFolder";
            this.txtProjectFolder.Size = new System.Drawing.Size(318, 20);
            this.txtProjectFolder.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Intermediate Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cache Folder:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Folder:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabNavigation);
            this.tabControl1.Controls.Add(this.tabCreateSource);
            this.tabControl1.Controls.Add(this.tabProjectFolders);
            this.tabControl1.Controls.Add(this.tabSourceDataLocation);
            this.tabControl1.Controls.Add(this.tabDatabaseConnection);
            this.tabControl1.Controls.Add(this.tabRun);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(550, 758);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // frmSDPProjectBuilderProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 470);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "frmSDPProjectBuilderProject";
            this.Text = "SDP Project Builder Project";
            this.Load += new System.EventHandler(this.frmHE2RMESProject_Load);
            this.tabRun.ResumeLayout(false);
            this.tabRun.PerformLayout();
            this.tabDatabaseConnection.ResumeLayout(false);
            this.tabDatabaseConnection.PerformLayout();
            this.tabCreateSource.ResumeLayout(false);
            this.groupAOIBuffer.ResumeLayout(false);
            this.groupAOIBuffer.PerformLayout();
            this.groupAOICircle.ResumeLayout(false);
            this.groupAOICircle.PerformLayout();
            this.groupAOIShape.ResumeLayout(false);
            this.groupAOI.ResumeLayout(false);
            this.groupAOI.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupAOIPolygon.ResumeLayout(false);
            this.groupSource.ResumeLayout(false);
            this.tabNavigation.ResumeLayout(false);
            this.tabNavigation.PerformLayout();
            this.tabSourceDataLocation.ResumeLayout(false);
            this.tabSourceDataLocation.PerformLayout();
            this.tabProjectFolders.ResumeLayout(false);
            this.tabProjectFolders.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabRun;
        private System.Windows.Forms.TabPage tabDatabaseConnection;
        private System.Windows.Forms.TabPage tabCreateSource;
        private System.Windows.Forms.TabPage tabNavigation;
        private System.Windows.Forms.TabPage tabSourceDataLocation;
        private System.Windows.Forms.TabPage tabProjectFolders;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnCreateProjectFolders;
        private System.Windows.Forms.Button btnBrowseIntermediateFolder;
        private System.Windows.Forms.Button btnBrowseCacheFolder;
        private System.Windows.Forms.Button btnBrowseProjectFolder;
        private System.Windows.Forms.TextBox txtIntermediateFolder;
        private System.Windows.Forms.TextBox txtCacheFolder;
        private System.Windows.Forms.TextBox txtProjectFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseSoilsLocation;
        private System.Windows.Forms.Button btnBrowseNLCDLocation;
        private System.Windows.Forms.Button btnBrowseNHDPlusLocation;
        private System.Windows.Forms.TextBox txtSoilsLocation;
        private System.Windows.Forms.TextBox txtNLCDLocation;
        private System.Windows.Forms.TextBox txtNHDPlusLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDisregardCacheSoils;
        private System.Windows.Forms.CheckBox chkDisregardCacheNLCD;
        private System.Windows.Forms.CheckBox chkDisregardCacheNHDPlus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.ComboBox cboHUC;
        private System.Windows.Forms.ComboBox cboCounty;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.Button btnZoomToCoordinates;
        private System.Windows.Forms.Button btnZoomToCounty;
        private System.Windows.Forms.Button btnZoomToHUC;
        private System.Windows.Forms.Button btnZoomToState;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtLengthOfTimeout;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtNumberOfRetries;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ListBox lbScienceModules;
        private System.Windows.Forms.ListBox lbSourceTypes;
        private System.Windows.Forms.CheckBox chkForceFullDependency;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cboDatabaseName;
        private System.Windows.Forms.Button btnGetDatabases;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRunStatus;
        private System.Windows.Forms.GroupBox groupSource;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnClearDrawingSource;
        private System.Windows.Forms.Button btnUndoPointSource;
        private System.Windows.Forms.Button btnSavePolygonSource;
        private System.Windows.Forms.Button btnEditSource;
        private System.Windows.Forms.GroupBox groupAOI;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbShape;
        private System.Windows.Forms.RadioButton rbBuffer;
        private System.Windows.Forms.RadioButton rbCircle;
        private System.Windows.Forms.RadioButton rbPolygon;
        private System.Windows.Forms.GroupBox groupAOIPolygon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnClearDrawingAOIPolygon;
        private System.Windows.Forms.Button btnUndoPointAOIPolygon;
        private System.Windows.Forms.Button btnSavePolygonAOIPolygon;
        private System.Windows.Forms.Button btnEditAOIPolygon;
        private System.Windows.Forms.GroupBox groupAOIShape;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnSavePolygonAOIShape;
        private System.Windows.Forms.GroupBox groupAOICircle;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Button btnSaveCircleAOICircle;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.GroupBox groupAOIBuffer;
        private System.Windows.Forms.TextBox txtBuffer;
        private System.Windows.Forms.Label lblRadiusBuffer;
        private System.Windows.Forms.Button btnSaveBufferAOIBuffer;
        private System.Windows.Forms.Button btnClearDrawingAOICircle;
        private System.Windows.Forms.Button btnUndoPointAOICircle;
        private System.Windows.Forms.Button btnEditAOICircle;
        private System.Windows.Forms.Button btnCancelSource;
        private System.Windows.Forms.Button btnCancelAOIPolygon;
        private System.Windows.Forms.Button btnCancelAOICircle;
        private System.Windows.Forms.Button btnClearDrawingAOIBuffer;
        private System.Windows.Forms.Button btnClearDrawingAOIShape;
        private System.Windows.Forms.Button btnCancelAOIShape;
        private System.Windows.Forms.Button btnEditAOIShape;
    }
}