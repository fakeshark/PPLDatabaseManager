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
            this.btnImportPplData = new System.Windows.Forms.Button();
            this.btnShowAllRecords = new System.Windows.Forms.Button();
            this.lblActiveDoc = new System.Windows.Forms.Label();
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
            this.importPPLDataToolStripMenuItem});
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
            this.dgvDbOutput.Location = new System.Drawing.Point(13, 47);
            this.dgvDbOutput.Name = "dgvDbOutput";
            this.dgvDbOutput.Size = new System.Drawing.Size(889, 409);
            this.dgvDbOutput.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblActiveDoc);
            this.groupBox1.Location = new System.Drawing.Point(911, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 415);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options:";
            // 
            // btnImportPplData
            // 
            this.btnImportPplData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportPplData.Location = new System.Drawing.Point(911, 462);
            this.btnImportPplData.Name = "btnImportPplData";
            this.btnImportPplData.Size = new System.Drawing.Size(176, 38);
            this.btnImportPplData.TabIndex = 5;
            this.btnImportPplData.Text = "&Import PPL Data";
            this.btnImportPplData.UseVisualStyleBackColor = true;
            this.btnImportPplData.Click += new System.EventHandler(this.btnImportPplData_Click);
            // 
            // btnShowAllRecords
            // 
            this.btnShowAllRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowAllRecords.Location = new System.Drawing.Point(13, 462);
            this.btnShowAllRecords.Name = "btnShowAllRecords";
            this.btnShowAllRecords.Size = new System.Drawing.Size(253, 38);
            this.btnShowAllRecords.TabIndex = 6;
            this.btnShowAllRecords.Text = "Show All PPL Database Records";
            this.btnShowAllRecords.UseVisualStyleBackColor = true;
            this.btnShowAllRecords.Click += new System.EventHandler(this.btnShowAllRecords_Click);
            // 
            // lblActiveDoc
            // 
            this.lblActiveDoc.AutoSize = true;
            this.lblActiveDoc.Location = new System.Drawing.Point(12, 23);
            this.lblActiveDoc.Name = "lblActiveDoc";
            this.lblActiveDoc.Size = new System.Drawing.Size(121, 13);
            this.lblActiveDoc.TabIndex = 0;
            this.lblActiveDoc.Text = "Active Document: None";
            // 
            // frmPPLDatabaseForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 512);
            this.Controls.Add(this.btnShowAllRecords);
            this.Controls.Add(this.btnImportPplData);
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
        private System.Windows.Forms.Button btnImportPplData;
        private System.Windows.Forms.Button btnShowAllRecords;
        private System.Windows.Forms.Label lblActiveDoc;
    }
}

