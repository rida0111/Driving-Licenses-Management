namespace DVLD.Applications.Release_Application
{
    partial class frmListDetainedLicenses
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
            this.dgvDetainedLicenses = new System.Windows.Forms.DataGridView();
            this.cmsListDetainedLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsReleaseDLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterby = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.btnDetain = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.lbtext = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbReleased = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).BeginInit();
            this.cmsListDetainedLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetainedLicenses
            // 
            this.dgvDetainedLicenses.AllowUserToAddRows = false;
            this.dgvDetainedLicenses.AllowUserToDeleteRows = false;
            this.dgvDetainedLicenses.AllowUserToResizeColumns = false;
            this.dgvDetainedLicenses.AllowUserToResizeRows = false;
            this.dgvDetainedLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetainedLicenses.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvDetainedLicenses.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetainedLicenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetainedLicenses.ColumnHeadersHeight = 35;
            this.dgvDetainedLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetainedLicenses.ContextMenuStrip = this.cmsListDetainedLicense;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetainedLicenses.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetainedLicenses.Location = new System.Drawing.Point(11, 285);
            this.dgvDetainedLicenses.MultiSelect = false;
            this.dgvDetainedLicenses.Name = "dgvDetainedLicenses";
            this.dgvDetainedLicenses.ReadOnly = true;
            this.dgvDetainedLicenses.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDetainedLicenses.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDetainedLicenses.RowTemplate.Height = 25;
            this.dgvDetainedLicenses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDetainedLicenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetainedLicenses.ShowCellErrors = false;
            this.dgvDetainedLicenses.ShowCellToolTips = false;
            this.dgvDetainedLicenses.ShowEditingIcon = false;
            this.dgvDetainedLicenses.ShowRowErrors = false;
            this.dgvDetainedLicenses.Size = new System.Drawing.Size(1286, 349);
            this.dgvDetainedLicenses.TabIndex = 0;
            // 
            // cmsListDetainedLicense
            // 
            this.cmsListDetainedLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetails,
            this.showLicenseDetails,
            this.showPersonLicenseHistory,
            this.toolStripMenuItem1,
            this.cmsReleaseDLicense});
            this.cmsListDetainedLicense.Name = "cmsListDetainedLicense";
            this.cmsListDetainedLicense.Size = new System.Drawing.Size(242, 162);
            this.cmsListDetainedLicense.Opening += new System.ComponentModel.CancelEventHandler(this.cmsDetainedLicense_Opening);
            // 
            // showPersonDetails
            // 
            this.showPersonDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.showPersonDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonDetails.Name = "showPersonDetails";
            this.showPersonDetails.Size = new System.Drawing.Size(241, 38);
            this.showPersonDetails.Text = "Show Person Details";
            this.showPersonDetails.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // showLicenseDetails
            // 
            this.showLicenseDetails.Image = global::DVLD.Properties.Resources.LocalDriving_License;
            this.showLicenseDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseDetails.Name = "showLicenseDetails";
            this.showLicenseDetails.Size = new System.Drawing.Size(241, 38);
            this.showLicenseDetails.Text = "Show License Details";
            this.showLicenseDetails.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistory
            // 
            this.showPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHistory.Name = "showPersonLicenseHistory";
            this.showPersonLicenseHistory.Size = new System.Drawing.Size(241, 38);
            this.showPersonLicenseHistory.Text = "Show Person License History";
            this.showPersonLicenseHistory.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
            // 
            // cmsReleaseDLicense
            // 
            this.cmsReleaseDLicense.Image = global::DVLD.Properties.Resources.Release_Detained_License_32;
            this.cmsReleaseDLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsReleaseDLicense.Name = "cmsReleaseDLicense";
            this.cmsReleaseDLicense.Size = new System.Drawing.Size(241, 38);
            this.cmsReleaseDLicense.Text = "Release Detained License";
            this.cmsReleaseDLicense.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter By:";
            // 
            // cbFilterby
            // 
            this.cbFilterby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterby.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cbFilterby.FormattingEnabled = true;
            this.cbFilterby.Items.AddRange(new object[] {
            "None",
            "Detain.ID",
            "License.ID",
            "IsReleased",
            "National.No",
            "Full Name",
            "Release Application ID"});
            this.cbFilterby.Location = new System.Drawing.Point(82, 246);
            this.cbFilterby.Name = "cbFilterby";
            this.cbFilterby.Size = new System.Drawing.Size(168, 26);
            this.cbFilterby.TabIndex = 3;
            this.cbFilterby.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(11, 656);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "#Records:";
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecordsNumber.Location = new System.Drawing.Point(100, 656);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(31, 21);
            this.lblRecordsNumber.TabIndex = 5;
            this.lblRecordsNumber.Text = "???";
            // 
            // btnDetain
            // 
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetain.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.Image = global::DVLD.Properties.Resources.Detain_64;
            this.btnDetain.Location = new System.Drawing.Point(1225, 205);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(72, 68);
            this.btnDetain.TabIndex = 6;
            this.btnDetain.UseVisualStyleBackColor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRelease.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.Image = global::DVLD.Properties.Resources.Release_Detained_License_64;
            this.btnRelease.Location = new System.Drawing.Point(1134, 206);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(72, 67);
            this.btnRelease.TabIndex = 7;
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // lbtext
            // 
            this.lbtext.AutoSize = true;
            this.lbtext.Font = new System.Drawing.Font("Verdana", 24F);
            this.lbtext.ForeColor = System.Drawing.Color.Red;
            this.lbtext.Location = new System.Drawing.Point(466, 143);
            this.lbtext.Name = "lbtext";
            this.lbtext.Size = new System.Drawing.Size(376, 38);
            this.lbtext.TabIndex = 100;
            this.lbtext.Text = "List Detained Licenses";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1185, 650);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbReleased
            // 
            this.cbReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReleased.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cbReleased.FormattingEnabled = true;
            this.cbReleased.Items.AddRange(new object[] {
            "ALL",
            "Yes",
            "No"});
            this.cbReleased.Location = new System.Drawing.Point(256, 247);
            this.cbReleased.Name = "cbReleased";
            this.cbReleased.Size = new System.Drawing.Size(92, 26);
            this.cbReleased.TabIndex = 104;
            this.cbReleased.SelectedIndexChanged += new System.EventHandler(this.cbReleased_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtFilter.Location = new System.Drawing.Point(256, 247);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(167, 26);
            this.txtFilter.TabIndex = 105;
            this.txtFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilter_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(569, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 103;
            this.pictureBox1.TabStop = false;
            // 
            // frmListDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1308, 699);
            this.Controls.Add(this.cbReleased);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbtext);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFilterby);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDetainedLicenses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListDetainedLicenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List Detained Licenses";
            this.Load += new System.EventHandler(this.frmListDetainedLicenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).EndInit();
            this.cmsListDetainedLicense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetainedLicenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbFilterby;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Label lbtext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbReleased;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ContextMenuStrip cmsListDetainedLicense;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetails;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem cmsReleaseDLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}