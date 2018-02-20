namespace PPLDatabaseManager
{
    partial class frmPPLDatabaseForm1
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
            this.lblDbStatus = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolbarOption = new System.Windows.Forms.ToolStripMenuItem();
            this.importPPLDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolbarOption = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDbOutput = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxActivePPLparts = new System.Windows.Forms.ListBox();
            this.btnImportPplData = new System.Windows.Forms.Button();
            this.lblActiveDoc = new System.Windows.Forms.Label();
            this.btnLoadPplData = new System.Windows.Forms.Button();
            this.btnRunActiveCommand = new System.Windows.Forms.Button();
            this.lblDatabaseTitle = new System.Windows.Forms.Label();
            this.txtSqlTestString = new System.Windows.Forms.TextBox();
            this.btnRunTestCommand = new System.Windows.Forms.Button();
            this.btnSaveCommand = new System.Windows.Forms.Button();
            this.cbxSavedCommands = new System.Windows.Forms.ComboBox();
            this.lblSavedDBCommands = new System.Windows.Forms.Label();
            this.btnEditCommand = new System.Windows.Forms.Button();
            this.quitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbOutput)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDbStatus
            // 
            this.lblDbStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDbStatus.Location = new System.Drawing.Point(0, 24);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblDbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDbStatus.Size = new System.Drawing.Size(1096, 13);
            this.lblDbStatus.TabIndex = 1;
            this.lblDbStatus.Text = "Database Status: ...";
            this.lblDbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolbarOption,
            this.MenuToolbarOption});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1096, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolbarOption
            // 
            this.FileToolbarOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importPPLDataToolStripMenuItem,
            this.quitApplicationToolStripMenuItem});
            this.FileToolbarOption.Name = "FileToolbarOption";
            this.FileToolbarOption.Size = new System.Drawing.Size(37, 20);
            this.FileToolbarOption.Text = "File";
            // 
            // importPPLDataToolStripMenuItem
            // 
            this.importPPLDataToolStripMenuItem.Name = "importPPLDataToolStripMenuItem";
            this.importPPLDataToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.importPPLDataToolStripMenuItem.Text = "Import PPL Data";
            this.importPPLDataToolStripMenuItem.Click += new System.EventHandler(this.importPPLDataToolStripMenuItem_Click);
            // 
            // MenuToolbarOption
            // 
            this.MenuToolbarOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkDatabaseToolStripMenuItem});
            this.MenuToolbarOption.Name = "MenuToolbarOption";
            this.MenuToolbarOption.Size = new System.Drawing.Size(50, 20);
            this.MenuToolbarOption.Text = "Menu";
            // 
            // checkDatabaseToolStripMenuItem
            // 
            this.checkDatabaseToolStripMenuItem.Name = "checkDatabaseToolStripMenuItem";
            this.checkDatabaseToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.checkDatabaseToolStripMenuItem.Text = "Check Database Connection";
            this.checkDatabaseToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // dgvDbOutput
            // 
            this.dgvDbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDbOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDbOutput.Location = new System.Drawing.Point(13, 44);
            this.dgvDbOutput.Name = "dgvDbOutput";
            this.dgvDbOutput.Size = new System.Drawing.Size(825, 443);
            this.dgvDbOutput.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbxActivePPLparts);
            this.groupBox1.Controls.Add(this.btnImportPplData);
            this.groupBox1.Controls.Add(this.lblActiveDoc);
            this.groupBox1.Controls.Add(this.btnLoadPplData);
            this.groupBox1.Location = new System.Drawing.Point(844, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 446);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Parts Listing:";
            // 
            // lbxActivePPLparts
            // 
            this.lbxActivePPLparts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxActivePPLparts.FormattingEnabled = true;
            this.lbxActivePPLparts.Location = new System.Drawing.Point(8, 116);
            this.lbxActivePPLparts.Name = "lbxActivePPLparts";
            this.lbxActivePPLparts.Size = new System.Drawing.Size(228, 277);
            this.lbxActivePPLparts.TabIndex = 8;
            // 
            // btnImportPplData
            // 
            this.btnImportPplData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportPplData.Enabled = false;
            this.btnImportPplData.Location = new System.Drawing.Point(8, 65);
            this.btnImportPplData.Name = "btnImportPplData";
            this.btnImportPplData.Size = new System.Drawing.Size(227, 29);
            this.btnImportPplData.TabIndex = 7;
            this.btnImportPplData.Text = "&Import PPL Data";
            this.btnImportPplData.UseVisualStyleBackColor = true;
            this.btnImportPplData.Click += new System.EventHandler(this.btnImportPplData_Click_1);
            // 
            // lblActiveDoc
            // 
            this.lblActiveDoc.AutoSize = true;
            this.lblActiveDoc.Location = new System.Drawing.Point(6, 16);
            this.lblActiveDoc.Name = "lblActiveDoc";
            this.lblActiveDoc.Size = new System.Drawing.Size(121, 13);
            this.lblActiveDoc.TabIndex = 0;
            this.lblActiveDoc.Text = "Active Document: None";
            // 
            // btnLoadPplData
            // 
            this.btnLoadPplData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadPplData.Location = new System.Drawing.Point(7, 399);
            this.btnLoadPplData.Name = "btnLoadPplData";
            this.btnLoadPplData.Size = new System.Drawing.Size(229, 38);
            this.btnLoadPplData.TabIndex = 5;
            this.btnLoadPplData.Text = "&Load PPL Data";
            this.btnLoadPplData.UseVisualStyleBackColor = true;
            this.btnLoadPplData.Click += new System.EventHandler(this.btnImportPplData_Click);
            // 
            // btnRunActiveCommand
            // 
            this.btnRunActiveCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRunActiveCommand.Location = new System.Drawing.Point(13, 492);
            this.btnRunActiveCommand.Name = "btnRunActiveCommand";
            this.btnRunActiveCommand.Size = new System.Drawing.Size(253, 38);
            this.btnRunActiveCommand.TabIndex = 6;
            this.btnRunActiveCommand.Text = "...";
            this.btnRunActiveCommand.UseVisualStyleBackColor = true;
            this.btnRunActiveCommand.Click += new System.EventHandler(this.btnShowAllRecords_Click);
            // 
            // lblDatabaseTitle
            // 
            this.lblDatabaseTitle.AutoSize = true;
            this.lblDatabaseTitle.Location = new System.Drawing.Point(10, 28);
            this.lblDatabaseTitle.Name = "lblDatabaseTitle";
            this.lblDatabaseTitle.Size = new System.Drawing.Size(88, 13);
            this.lblDatabaseTitle.TabIndex = 9;
            this.lblDatabaseTitle.Text = "Database Output";
            // 
            // txtSqlTestString
            // 
            this.txtSqlTestString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlTestString.Location = new System.Drawing.Point(272, 493);
            this.txtSqlTestString.Multiline = true;
            this.txtSqlTestString.Name = "txtSqlTestString";
            this.txtSqlTestString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlTestString.Size = new System.Drawing.Size(566, 85);
            this.txtSqlTestString.TabIndex = 10;
            this.txtSqlTestString.Text = "Write/test SQL commands here...";
            // 
            // btnRunTestCommand
            // 
            this.btnRunTestCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunTestCommand.Location = new System.Drawing.Point(844, 493);
            this.btnRunTestCommand.Name = "btnRunTestCommand";
            this.btnRunTestCommand.Size = new System.Drawing.Size(243, 41);
            this.btnRunTestCommand.TabIndex = 11;
            this.btnRunTestCommand.Text = "◀   Run SQL Command";
            this.btnRunTestCommand.UseVisualStyleBackColor = true;
            this.btnRunTestCommand.Click += new System.EventHandler(this.btnRunTestCommand_Click);
            // 
            // btnSaveCommand
            // 
            this.btnSaveCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCommand.Location = new System.Drawing.Point(844, 537);
            this.btnSaveCommand.Name = "btnSaveCommand";
            this.btnSaveCommand.Size = new System.Drawing.Size(120, 41);
            this.btnSaveCommand.TabIndex = 12;
            this.btnSaveCommand.Text = "Save Command";
            this.btnSaveCommand.UseVisualStyleBackColor = true;
            this.btnSaveCommand.Click += new System.EventHandler(this.btnSaveCommand_Click);
            // 
            // cbxSavedCommands
            // 
            this.cbxSavedCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxSavedCommands.FormattingEnabled = true;
            this.cbxSavedCommands.Location = new System.Drawing.Point(13, 557);
            this.cbxSavedCommands.Name = "cbxSavedCommands";
            this.cbxSavedCommands.Size = new System.Drawing.Size(253, 21);
            this.cbxSavedCommands.TabIndex = 13;
            this.cbxSavedCommands.SelectedIndexChanged += new System.EventHandler(this.cbxSavedCommands_SelectedIndexChanged);
            // 
            // lblSavedDBCommands
            // 
            this.lblSavedDBCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSavedDBCommands.AutoSize = true;
            this.lblSavedDBCommands.Location = new System.Drawing.Point(12, 539);
            this.lblSavedDBCommands.Name = "lblSavedDBCommands";
            this.lblSavedDBCommands.Size = new System.Drawing.Size(142, 13);
            this.lblSavedDBCommands.TabIndex = 14;
            this.lblSavedDBCommands.Text = "Saved Database Commands";
            // 
            // btnEditCommand
            // 
            this.btnEditCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCommand.Location = new System.Drawing.Point(967, 537);
            this.btnEditCommand.Name = "btnEditCommand";
            this.btnEditCommand.Size = new System.Drawing.Size(120, 41);
            this.btnEditCommand.TabIndex = 15;
            this.btnEditCommand.Text = "Edit Command";
            this.btnEditCommand.UseVisualStyleBackColor = true;
            // 
            // quitApplicationToolStripMenuItem
            // 
            this.quitApplicationToolStripMenuItem.Name = "quitApplicationToolStripMenuItem";
            this.quitApplicationToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.quitApplicationToolStripMenuItem.Text = "Quit Application";
            this.quitApplicationToolStripMenuItem.Click += new System.EventHandler(this.quitApplicationToolStripMenuItem_Click);
            // 
            // frmPPLDatabaseForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 590);
            this.Controls.Add(this.btnEditCommand);
            this.Controls.Add(this.lblSavedDBCommands);
            this.Controls.Add(this.cbxSavedCommands);
            this.Controls.Add(this.btnSaveCommand);
            this.Controls.Add(this.btnRunTestCommand);
            this.Controls.Add(this.txtSqlTestString);
            this.Controls.Add(this.lblDatabaseTitle);
            this.Controls.Add(this.btnRunActiveCommand);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDbOutput);
            this.Controls.Add(this.lblDbStatus);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPPLDatabaseForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PPL Database Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbOutput)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDbStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuToolbarOption;
        private System.Windows.Forms.ToolStripMenuItem checkDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileToolbarOption;
        private System.Windows.Forms.ToolStripMenuItem importPPLDataToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvDbOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoadPplData;
        private System.Windows.Forms.Button btnRunActiveCommand;
        private System.Windows.Forms.Label lblActiveDoc;
        private System.Windows.Forms.ListBox lbxActivePPLparts;
        private System.Windows.Forms.Button btnImportPplData;
        private System.Windows.Forms.Label lblDatabaseTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSqlTestString;
        private System.Windows.Forms.Button btnRunTestCommand;
        private System.Windows.Forms.Button btnSaveCommand;
        private System.Windows.Forms.ComboBox cbxSavedCommands;
        private System.Windows.Forms.Label lblSavedDBCommands;
        private System.Windows.Forms.Button btnEditCommand;
        private System.Windows.Forms.ToolStripMenuItem quitApplicationToolStripMenuItem;
    }
}

