namespace KOAR_Big_Builder
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ofdProject = new System.Windows.Forms.OpenFileDialog();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenBig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProjectBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPackageLoadTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPackageLoadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.lsvProject = new System.Windows.Forms.ListView();
            this.chFileID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFlags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSymbolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ofdAddFiles = new System.Windows.Forms.OpenFileDialog();
            this.ofdBig = new System.Windows.Forms.OpenFileDialog();
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPackage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSymbolName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnFileApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPackageIncludeMods = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPackageDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKOARPath = new System.Windows.Forms.TextBox();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.sfdBig = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabListView = new System.Windows.Forms.TabPage();
            this.tabPackage = new System.Windows.Forms.TabPage();
            this.chkFindNormalize = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFindFileID = new System.Windows.Forms.TextBox();
            this.lsvPackage = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHashGenerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHashSource = new System.Windows.Forms.TextBox();
            this.sfdProject = new System.Windows.Forms.SaveFileDialog();
            this.mnsMain.SuspendLayout();
            this.grpFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabListView.SuspendLayout();
            this.tabPackage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdProject
            // 
            this.ofdProject.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.ofdProject.RestoreDirectory = true;
            // 
            // mnsMain
            // 
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuProject,
            this.mnuPackage});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(1287, 24);
            this.mnsMain.TabIndex = 0;
            this.mnsMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenProject,
            this.mnuSaveProject,
            this.toolStripMenuItem1,
            this.mnuOpenBig,
            this.toolStripMenuItem2,
            this.mnuAddFiles});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpenProject
            // 
            this.mnuOpenProject.Name = "mnuOpenProject";
            this.mnuOpenProject.Size = new System.Drawing.Size(166, 22);
            this.mnuOpenProject.Text = "Open Project";
            this.mnuOpenProject.Click += new System.EventHandler(this.mnuOpenProject_Click);
            // 
            // mnuSaveProject
            // 
            this.mnuSaveProject.Name = "mnuSaveProject";
            this.mnuSaveProject.Size = new System.Drawing.Size(166, 22);
            this.mnuSaveProject.Text = "Save Project";
            this.mnuSaveProject.Click += new System.EventHandler(this.mnuSaveProject_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // mnuOpenBig
            // 
            this.mnuOpenBig.Name = "mnuOpenBig";
            this.mnuOpenBig.Size = new System.Drawing.Size(166, 22);
            this.mnuOpenBig.Text = "Open Big Archive";
            this.mnuOpenBig.Click += new System.EventHandler(this.mnuOpenBig_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // mnuAddFiles
            // 
            this.mnuAddFiles.Name = "mnuAddFiles";
            this.mnuAddFiles.Size = new System.Drawing.Size(166, 22);
            this.mnuAddFiles.Text = "Add Files";
            this.mnuAddFiles.Click += new System.EventHandler(this.mnuAddFiles_Click);
            // 
            // mnuProject
            // 
            this.mnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProjectBuild});
            this.mnuProject.Name = "mnuProject";
            this.mnuProject.Size = new System.Drawing.Size(56, 20);
            this.mnuProject.Text = "Project";
            // 
            // mnuProjectBuild
            // 
            this.mnuProjectBuild.Name = "mnuProjectBuild";
            this.mnuProjectBuild.Size = new System.Drawing.Size(101, 22);
            this.mnuProjectBuild.Text = "Build";
            this.mnuProjectBuild.Click += new System.EventHandler(this.mnuProjectBuild_Click);
            // 
            // mnuPackage
            // 
            this.mnuPackage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPackageLoadTarget,
            this.mnuPackageLoadAll});
            this.mnuPackage.Name = "mnuPackage";
            this.mnuPackage.Size = new System.Drawing.Size(63, 20);
            this.mnuPackage.Text = "Package";
            // 
            // mnuPackageLoadTarget
            // 
            this.mnuPackageLoadTarget.Name = "mnuPackageLoadTarget";
            this.mnuPackageLoadTarget.Size = new System.Drawing.Size(184, 22);
            this.mnuPackageLoadTarget.Text = "Load Target Package";
            this.mnuPackageLoadTarget.Click += new System.EventHandler(this.mnuPackageLoadTarget_Click);
            // 
            // mnuPackageLoadAll
            // 
            this.mnuPackageLoadAll.Name = "mnuPackageLoadAll";
            this.mnuPackageLoadAll.Size = new System.Drawing.Size(184, 22);
            this.mnuPackageLoadAll.Text = "Load All";
            this.mnuPackageLoadAll.Click += new System.EventHandler(this.mnuPackageLoadAll_Click);
            // 
            // lsvProject
            // 
            this.lsvProject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileID,
            this.chFilename,
            this.chFlags,
            this.chType,
            this.chSymbolName});
            this.lsvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvProject.FullRowSelect = true;
            this.lsvProject.GridLines = true;
            this.lsvProject.HideSelection = false;
            this.lsvProject.Location = new System.Drawing.Point(3, 3);
            this.lsvProject.Name = "lsvProject";
            this.lsvProject.Size = new System.Drawing.Size(886, 494);
            this.lsvProject.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvProject.TabIndex = 1;
            this.lsvProject.UseCompatibleStateImageBehavior = false;
            this.lsvProject.View = System.Windows.Forms.View.Details;
            this.lsvProject.SelectedIndexChanged += new System.EventHandler(this.lsvProject_SelectedIndexChanged);
            // 
            // chFileID
            // 
            this.chFileID.Tag = "";
            this.chFileID.Text = "FileID";
            this.chFileID.Width = 80;
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 400;
            // 
            // chFlags
            // 
            this.chFlags.Text = "Flags";
            this.chFlags.Width = 40;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 100;
            // 
            // chSymbolName
            // 
            this.chSymbolName.Text = "Symbol name";
            this.chSymbolName.Width = 300;
            // 
            // ofdAddFiles
            // 
            this.ofdAddFiles.Filter = "All files (*.*)|*.*";
            this.ofdAddFiles.Multiselect = true;
            this.ofdAddFiles.RestoreDirectory = true;
            // 
            // ofdBig
            // 
            this.ofdBig.FileName = "test.big";
            this.ofdBig.Filter = "big files (*.big)|*.big|All files (*.*)|*.*";
            this.ofdBig.RestoreDirectory = true;
            // 
            // grpFile
            // 
            this.grpFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFile.Controls.Add(this.btnCreate);
            this.grpFile.Controls.Add(this.label11);
            this.grpFile.Controls.Add(this.txtPackage);
            this.grpFile.Controls.Add(this.label10);
            this.grpFile.Controls.Add(this.txtSymbolName);
            this.grpFile.Controls.Add(this.label9);
            this.grpFile.Controls.Add(this.txtType);
            this.grpFile.Controls.Add(this.btnSaveFile);
            this.grpFile.Controls.Add(this.btnDelete);
            this.grpFile.Controls.Add(this.btnFileApply);
            this.grpFile.Controls.Add(this.label3);
            this.grpFile.Controls.Add(this.txtFlags);
            this.grpFile.Controls.Add(this.label2);
            this.grpFile.Controls.Add(this.txtFileID);
            this.grpFile.Controls.Add(this.label1);
            this.grpFile.Controls.Add(this.txtFilename);
            this.grpFile.Location = new System.Drawing.Point(918, 150);
            this.grpFile.Name = "grpFile";
            this.grpFile.Size = new System.Drawing.Size(357, 184);
            this.grpFile.TabIndex = 2;
            this.grpFile.TabStop = false;
            this.grpFile.Text = "File details";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(87, 153);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 15;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(263, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Package";
            // 
            // txtPackage
            // 
            this.txtPackage.Location = new System.Drawing.Point(266, 81);
            this.txtPackage.Name = "txtPackage";
            this.txtPackage.Size = new System.Drawing.Size(82, 20);
            this.txtPackage.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Symbol name or hash";
            // 
            // txtSymbolName
            // 
            this.txtSymbolName.Location = new System.Drawing.Point(6, 127);
            this.txtSymbolName.Name = "txtSymbolName";
            this.txtSymbolName.Size = new System.Drawing.Size(345, 20);
            this.txtSymbolName.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(180, 81);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(82, 20);
            this.txtType.TabIndex = 9;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(192, 153);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 8;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Visible = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(273, 153);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFileApply
            // 
            this.btnFileApply.Location = new System.Drawing.Point(6, 153);
            this.btnFileApply.Name = "btnFileApply";
            this.btnFileApply.Size = new System.Drawing.Size(75, 23);
            this.btnFileApply.TabIndex = 6;
            this.btnFileApply.Text = "Apply";
            this.btnFileApply.UseVisualStyleBackColor = true;
            this.btnFileApply.Click += new System.EventHandler(this.btnFileApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Flags";
            // 
            // txtFlags
            // 
            this.txtFlags.Location = new System.Drawing.Point(94, 81);
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(82, 20);
            this.txtFlags.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "FileID";
            // 
            // txtFileID
            // 
            this.txtFileID.Location = new System.Drawing.Point(8, 81);
            this.txtFileID.Name = "txtFileID";
            this.txtFileID.Size = new System.Drawing.Size(82, 20);
            this.txtFileID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filename";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(6, 38);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(345, 20);
            this.txtFilename.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkPackageIncludeMods);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPackageDir);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtKOARPath);
            this.groupBox1.Location = new System.Drawing.Point(918, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 117);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package";
            // 
            // chkPackageIncludeMods
            // 
            this.chkPackageIncludeMods.AutoSize = true;
            this.chkPackageIncludeMods.Location = new System.Drawing.Point(138, 84);
            this.chkPackageIncludeMods.Name = "chkPackageIncludeMods";
            this.chkPackageIncludeMods.Size = new System.Drawing.Size(89, 17);
            this.chkPackageIncludeMods.TabIndex = 4;
            this.chkPackageIncludeMods.Text = "Include mods";
            this.chkPackageIncludeMods.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Target package directory";
            // 
            // txtPackageDir
            // 
            this.txtPackageDir.Location = new System.Drawing.Point(6, 81);
            this.txtPackageDir.Name = "txtPackageDir";
            this.txtPackageDir.Size = new System.Drawing.Size(126, 20);
            this.txtPackageDir.TabIndex = 2;
            this.txtPackageDir.Text = "001";
            this.txtPackageDir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "KOAR path";
            // 
            // txtKOARPath
            // 
            this.txtKOARPath.Location = new System.Drawing.Point(6, 38);
            this.txtKOARPath.Name = "txtKOARPath";
            this.txtKOARPath.Size = new System.Drawing.Size(345, 20);
            this.txtKOARPath.TabIndex = 0;
            this.txtKOARPath.TextChanged += new System.EventHandler(this.txtKOARPath_TextChanged);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.Filter = "All files (*.*)|*.*";
            // 
            // sfdBig
            // 
            this.sfdBig.FileName = "test.big";
            this.sfdBig.Filter = "big files (*.big)|*.big|All files (*.*)|*.*";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabListView);
            this.tabControl1.Controls.Add(this.tabPackage);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 526);
            this.tabControl1.TabIndex = 4;
            // 
            // tabListView
            // 
            this.tabListView.Controls.Add(this.lsvProject);
            this.tabListView.Location = new System.Drawing.Point(4, 22);
            this.tabListView.Name = "tabListView";
            this.tabListView.Padding = new System.Windows.Forms.Padding(3);
            this.tabListView.Size = new System.Drawing.Size(892, 500);
            this.tabListView.TabIndex = 0;
            this.tabListView.Text = "File list";
            this.tabListView.UseVisualStyleBackColor = true;
            // 
            // tabPackage
            // 
            this.tabPackage.Controls.Add(this.chkFindNormalize);
            this.tabPackage.Controls.Add(this.btnFind);
            this.tabPackage.Controls.Add(this.label4);
            this.tabPackage.Controls.Add(this.txtFindFileID);
            this.tabPackage.Controls.Add(this.lsvPackage);
            this.tabPackage.Location = new System.Drawing.Point(4, 22);
            this.tabPackage.Name = "tabPackage";
            this.tabPackage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPackage.Size = new System.Drawing.Size(892, 500);
            this.tabPackage.TabIndex = 1;
            this.tabPackage.Text = "Package";
            this.tabPackage.UseVisualStyleBackColor = true;
            // 
            // chkFindNormalize
            // 
            this.chkFindNormalize.AutoSize = true;
            this.chkFindNormalize.Location = new System.Drawing.Point(660, 41);
            this.chkFindNormalize.Name = "chkFindNormalize";
            this.chkFindNormalize.Size = new System.Drawing.Size(72, 17);
            this.chkFindNormalize.TabIndex = 8;
            this.chkFindNormalize.Text = "Normalize";
            this.chkFindNormalize.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(792, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(617, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "FileID:";
            // 
            // txtFindFileID
            // 
            this.txtFindFileID.Location = new System.Drawing.Point(660, 15);
            this.txtFindFileID.Name = "txtFindFileID";
            this.txtFindFileID.Size = new System.Drawing.Size(126, 20);
            this.txtFindFileID.TabIndex = 3;
            // 
            // lsvPackage
            // 
            this.lsvPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvPackage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lsvPackage.FullRowSelect = true;
            this.lsvPackage.GridLines = true;
            this.lsvPackage.HideSelection = false;
            this.lsvPackage.Location = new System.Drawing.Point(3, 3);
            this.lsvPackage.MinimumSize = new System.Drawing.Size(320, 494);
            this.lsvPackage.Name = "lsvPackage";
            this.lsvPackage.Size = new System.Drawing.Size(373, 494);
            this.lsvPackage.TabIndex = 2;
            this.lsvPackage.UseCompatibleStateImageBehavior = false;
            this.lsvPackage.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Filename";
            this.columnHeader2.Width = 350;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnHashGenerate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtHash);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtHashSource);
            this.groupBox2.Location = new System.Drawing.Point(918, 340);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 117);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hash";
            // 
            // btnHashGenerate
            // 
            this.btnHashGenerate.Location = new System.Drawing.Point(145, 81);
            this.btnHashGenerate.Name = "btnHashGenerate";
            this.btnHashGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnHashGenerate.TabIndex = 9;
            this.btnHashGenerate.Text = "Generate";
            this.btnHashGenerate.UseVisualStyleBackColor = true;
            this.btnHashGenerate.Click += new System.EventHandler(this.btnHashGenerate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Hash";
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(6, 81);
            this.txtHash.Name = "txtHash";
            this.txtHash.ReadOnly = true;
            this.txtHash.Size = new System.Drawing.Size(126, 20);
            this.txtHash.TabIndex = 2;
            this.txtHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Source string";
            // 
            // txtHashSource
            // 
            this.txtHashSource.Location = new System.Drawing.Point(6, 38);
            this.txtHashSource.Name = "txtHashSource";
            this.txtHashSource.Size = new System.Drawing.Size(345, 20);
            this.txtHashSource.TabIndex = 0;
            // 
            // sfdProject
            // 
            this.sfdProject.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 566);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFile);
            this.Controls.Add(this.mnsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KOAR Big Builder";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabListView.ResumeLayout(false);
            this.tabPackage.ResumeLayout(false);
            this.tabPackage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdProject;
        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenProject;
        private System.Windows.Forms.ListView lsvProject;
        private System.Windows.Forms.ColumnHeader chFileID;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chFlags;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFiles;
        private System.Windows.Forms.OpenFileDialog ofdAddFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem mnuProjectBuild;
        private System.Windows.Forms.OpenFileDialog ofdBig;
        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileID;
        private System.Windows.Forms.Button btnFileApply;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenBig;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPackageDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKOARPath;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.SaveFileDialog sfdBig;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabListView;
        private System.Windows.Forms.TabPage tabPackage;
        private System.Windows.Forms.ToolStripMenuItem mnuPackage;
        private System.Windows.Forms.ToolStripMenuItem mnuPackageLoadTarget;
        private System.Windows.Forms.ListView lsvPackage;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFindFileID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHashSource;
        private System.Windows.Forms.Button btnHashGenerate;
        private System.Windows.Forms.CheckBox chkPackageIncludeMods;
        private System.Windows.Forms.ToolStripMenuItem mnuPackageLoadAll;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.CheckBox chkFindNormalize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSymbolName;
        private System.Windows.Forms.ColumnHeader chSymbolName;
        private System.Windows.Forms.SaveFileDialog sfdProject;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPackage;
        private System.Windows.Forms.Button btnCreate;
    }
}

