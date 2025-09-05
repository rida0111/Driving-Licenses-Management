namespace DVLD.Applications.Local_License
{
    partial class frmListLocalDrivingLicenseApp
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLocalLicenseApplication = new System.Windows.Forms.DataGridView();
            this.cmsApplication = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.EditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.SechduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.VisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.WrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.StreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.IssueDriverLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRecordsNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplication)).BeginInit();
            this.cmsApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLocalLicenseApplication
            // 
            this.dgvLocalLicenseApplication.AllowUserToAddRows = false;
            this.dgvLocalLicenseApplication.AllowUserToDeleteRows = false;
            this.dgvLocalLicenseApplication.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicenseApplication.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicenseApplication.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocalLicenseApplication.ColumnHeadersHeight = 35;
            this.dgvLocalLicenseApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLocalLicenseApplication.ContextMenuStrip = this.cmsApplication;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocalLicenseApplication.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocalLicenseApplication.Location = new System.Drawing.Point(15, 283);
            this.dgvLocalLicenseApplication.MultiSelect = false;
            this.dgvLocalLicenseApplication.Name = "dgvLocalLicenseApplication";
            this.dgvLocalLicenseApplication.ReadOnly = true;
            this.dgvLocalLicenseApplication.RowHeadersWidth = 30;
            this.dgvLocalLicenseApplication.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvLocalLicenseApplication.RowTemplate.Height = 25;
            this.dgvLocalLicenseApplication.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLocalLicenseApplication.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicenseApplication.ShowEditingIcon = false;
            this.dgvLocalLicenseApplication.ShowRowErrors = false;
            this.dgvLocalLicenseApplication.Size = new System.Drawing.Size(1223, 331);
            this.dgvLocalLicenseApplication.TabIndex = 0;
            // 
            // cmsApplication
            // 
            this.cmsApplication.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowApplicationDetailsToolStripMenuItem,
            this.toolStripMenuItem12,
            this.EditApplication,
            this.DeleteApplication,
            this.toolStripMenuItem11,
            this.cancelApplication,
            this.toolStripMenuItem13,
            this.SechduleTest,
            this.toolStripMenuItem14,
            this.IssueDriverLicense,
            this.toolStripMenuItem15,
            this.ShowLicense,
            this.toolStripMenuItem16,
            this.toolStripMenuItem7});
            this.cmsApplication.Name = "cmsLocalDrivingLicenseApp";
            this.cmsApplication.Size = new System.Drawing.Size(259, 344);
            this.cmsApplication.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLocalDrivingLicenseApp_Opening);
            // 
            // ShowApplicationDetailsToolStripMenuItem
            // 
            this.ShowApplicationDetailsToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.ShowApplicationDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowApplicationDetailsToolStripMenuItem.Name = "ShowApplicationDetailsToolStripMenuItem";
            this.ShowApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(258, 38);
            this.ShowApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.ShowApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.ShowApplicationDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(255, 6);
            // 
            // EditApplication
            // 
            this.EditApplication.Image = global::DVLD.Properties.Resources.edit_32;
            this.EditApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditApplication.Name = "EditApplication";
            this.EditApplication.Size = new System.Drawing.Size(258, 38);
            this.EditApplication.Text = "Edit Application";
            this.EditApplication.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // DeleteApplication
            // 
            this.DeleteApplication.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.DeleteApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteApplication.Name = "DeleteApplication";
            this.DeleteApplication.Size = new System.Drawing.Size(258, 38);
            this.DeleteApplication.Text = "Delete Application";
            this.DeleteApplication.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(255, 6);
            // 
            // cancelApplication
            // 
            this.cancelApplication.Image = global::DVLD.Properties.Resources.Delete_32;
            this.cancelApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cancelApplication.Name = "cancelApplication";
            this.cancelApplication.Size = new System.Drawing.Size(258, 38);
            this.cancelApplication.Text = "Cancel Application";
            this.cancelApplication.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(255, 6);
            // 
            // SechduleTest
            // 
            this.SechduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VisionTest,
            this.WrittenTest,
            this.StreetTest});
            this.SechduleTest.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.SechduleTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SechduleTest.Name = "SechduleTest";
            this.SechduleTest.Size = new System.Drawing.Size(258, 38);
            this.SechduleTest.Text = "Sechdule Tests";
            // 
            // VisionTest
            // 
            this.VisionTest.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.VisionTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.VisionTest.Name = "VisionTest";
            this.VisionTest.Size = new System.Drawing.Size(201, 38);
            this.VisionTest.Text = "Schedule Vision Test";
            this.VisionTest.Click += new System.EventHandler(this.VisionTest_Click);
            // 
            // WrittenTest
            // 
            this.WrittenTest.Image = global::DVLD.Properties.Resources.Written_Test_32;
            this.WrittenTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.WrittenTest.Name = "WrittenTest";
            this.WrittenTest.Size = new System.Drawing.Size(201, 38);
            this.WrittenTest.Text = "Schedule written Test";
            this.WrittenTest.Click += new System.EventHandler(this.writtenTest_Click);
            // 
            // StreetTest
            // 
            this.StreetTest.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.StreetTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StreetTest.Name = "StreetTest";
            this.StreetTest.Size = new System.Drawing.Size(201, 38);
            this.StreetTest.Text = "Schedule Street Test";
            this.StreetTest.Click += new System.EventHandler(this.StreetTest_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(255, 6);
            // 
            // IssueDriverLicense
            // 
            this.IssueDriverLicense.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.IssueDriverLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IssueDriverLicense.Name = "IssueDriverLicense";
            this.IssueDriverLicense.Size = new System.Drawing.Size(258, 38);
            this.IssueDriverLicense.Text = "Issue Driving License(First Time)";
            this.IssueDriverLicense.Click += new System.EventHandler(this.issueDrivingLicenseFirstTimeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(255, 6);
            // 
            // ShowLicense
            // 
            this.ShowLicense.Image = global::DVLD.Properties.Resources.LocalDriving_License;
            this.ShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicense.Name = "ShowLicense";
            this.ShowLicense.Size = new System.Drawing.Size(258, 38);
            this.ShowLicense.Text = "Show License";
            this.ShowLicense.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(255, 6);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.toolStripMenuItem7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(258, 38);
            this.toolStripMenuItem7.Text = "Show Person License History";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 24F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(354, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 38);
            this.label1.TabIndex = 5;
            this.label1.Text = "Local Driving License Application";
            // 
            // lbRecordsNumber
            // 
            this.lbRecordsNumber.AutoSize = true;
            this.lbRecordsNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbRecordsNumber.Location = new System.Drawing.Point(109, 636);
            this.lbRecordsNumber.Name = "lbRecordsNumber";
            this.lbRecordsNumber.Size = new System.Drawing.Size(31, 21);
            this.lbRecordsNumber.TabIndex = 7;
            this.lbRecordsNumber.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 636);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "# Records:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(15, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Filter By:";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No.",
            "FullName",
            "Status"});
            this.cbFilter.Location = new System.Drawing.Point(88, 239);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(141, 26);
            this.cbFilter.TabIndex = 9;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtFilter.Location = new System.Drawing.Point(238, 239);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(168, 26);
            this.txtFilter.TabIndex = 13;
            this.txtFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilter_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnAdd.Image = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAdd.Location = new System.Drawing.Point(1167, 200);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 65);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1126, 630);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(671, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 31);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(536, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // frmListLocalDrivingLicenseApp
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1253, 677);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbRecordsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvLocalLicenseApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListLocalDrivingLicenseApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving License Application";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicenseApp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplication)).EndInit();
            this.cmsApplication.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalLicenseApplication;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRecordsNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ContextMenuStrip cmsApplication;
        private System.Windows.Forms.ToolStripMenuItem ShowApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditApplication;
        private System.Windows.Forms.ToolStripMenuItem DeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem cancelApplication;
        private System.Windows.Forms.ToolStripMenuItem SechduleTest;
        private System.Windows.Forms.ToolStripMenuItem IssueDriverLicense;
        private System.Windows.Forms.ToolStripMenuItem ShowLicense;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem VisionTest;
        private System.Windows.Forms.ToolStripMenuItem WrittenTest;
        private System.Windows.Forms.ToolStripMenuItem StreetTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
    }
}