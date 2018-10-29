namespace HucaresWF
{
    partial class HucaresForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HucaresForm));
            this.licensePlateImage = new System.Windows.Forms.PictureBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.mlpLabel = new System.Windows.Forms.Label();
            this.lettersBox = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.mlpTab = new System.Windows.Forms.TabPage();
            this.showMlpListButton = new System.Windows.Forms.Button();
            this.digitsBox = new System.Windows.Forms.TextBox();
            this.dlpTab = new System.Windows.Forms.TabPage();
            this.dlpTable = new System.Windows.Forms.DataGridView();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.longField = new System.Windows.Forms.NumericUpDown();
            this.latField = new System.Windows.Forms.NumericUpDown();
            this.cameraTable = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Long = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTrustedSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submitCam = new System.Windows.Forms.Button();
            this.isTrustedBox = new System.Windows.Forms.CheckBox();
            this.isTrustedLabel = new System.Windows.Forms.Label();
            this.hostUrlField = new System.Windows.Forms.TextBox();
            this.longLabel = new System.Windows.Forms.Label();
            this.latLabel = new System.Windows.Forms.Label();
            this.hostUrlLabel = new System.Windows.Forms.Label();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Confidence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gMapUri = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateImage)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.mlpTab.SuspendLayout();
            this.dlpTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dlpTable)).BeginInit();
            this.cameraTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.latField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraTable)).BeginInit();
            this.SuspendLayout();
            // 
            // licensePlateImage
            // 
            this.licensePlateImage.Image = ((System.Drawing.Image)(resources.GetObject("licensePlateImage.Image")));
            this.licensePlateImage.Location = new System.Drawing.Point(21, 43);
            this.licensePlateImage.Name = "licensePlateImage";
            this.licensePlateImage.Size = new System.Drawing.Size(741, 167);
            this.licensePlateImage.TabIndex = 0;
            this.licensePlateImage.TabStop = false;
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(285, 235);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(340, 59);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "My car has been stolen!";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // mlpLabel
            // 
            this.mlpLabel.AutoSize = true;
            this.mlpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlpLabel.Location = new System.Drawing.Point(328, -1);
            this.mlpLabel.Name = "mlpLabel";
            this.mlpLabel.Size = new System.Drawing.Size(251, 31);
            this.mlpLabel.TabIndex = 2;
            this.mlpLabel.Text = "Input MLP number";
            // 
            // lettersBox
            // 
            this.lettersBox.BackColor = System.Drawing.SystemColors.Window;
            this.lettersBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 93F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lettersBox.Location = new System.Drawing.Point(93, 52);
            this.lettersBox.MaxLength = 3;
            this.lettersBox.Name = "lettersBox";
            this.lettersBox.Size = new System.Drawing.Size(325, 148);
            this.lettersBox.TabIndex = 3;
            this.lettersBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.mlpTab);
            this.tabControlMain.Controls.Add(this.dlpTab);
            this.tabControlMain.Controls.Add(this.cameraTab);
            this.tabControlMain.Location = new System.Drawing.Point(-2, 9);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(812, 332);
            this.tabControlMain.TabIndex = 5;
            // 
            // mlpTab
            // 
            this.mlpTab.Controls.Add(this.showMlpListButton);
            this.mlpTab.Controls.Add(this.digitsBox);
            this.mlpTab.Controls.Add(this.submitButton);
            this.mlpTab.Controls.Add(this.mlpLabel);
            this.mlpTab.Controls.Add(this.lettersBox);
            this.mlpTab.Controls.Add(this.licensePlateImage);
            this.mlpTab.Location = new System.Drawing.Point(4, 22);
            this.mlpTab.Name = "mlpTab";
            this.mlpTab.Padding = new System.Windows.Forms.Padding(3);
            this.mlpTab.Size = new System.Drawing.Size(804, 306);
            this.mlpTab.TabIndex = 0;
            this.mlpTab.Text = "MLP Insert";
            this.mlpTab.UseVisualStyleBackColor = true;
            // 
            // showMlpListButton
            // 
            this.showMlpListButton.Location = new System.Drawing.Point(21, 248);
            this.showMlpListButton.Name = "showMlpListButton";
            this.showMlpListButton.Size = new System.Drawing.Size(102, 44);
            this.showMlpListButton.TabIndex = 5;
            this.showMlpListButton.Text = "Show MLP List";
            this.showMlpListButton.UseVisualStyleBackColor = true;
            this.showMlpListButton.Click += new System.EventHandler(this.showMlpList_Click);
            // 
            // digitsBox
            // 
            this.digitsBox.BackColor = System.Drawing.SystemColors.Window;
            this.digitsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 93F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digitsBox.Location = new System.Drawing.Point(467, 52);
            this.digitsBox.MaxLength = 3;
            this.digitsBox.Name = "digitsBox";
            this.digitsBox.Size = new System.Drawing.Size(285, 148);
            this.digitsBox.TabIndex = 4;
            this.digitsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dlpTab
            // 
            this.dlpTab.Controls.Add(this.dlpTable);
            this.dlpTab.Location = new System.Drawing.Point(4, 22);
            this.dlpTab.Name = "dlpTab";
            this.dlpTab.Padding = new System.Windows.Forms.Padding(3);
            this.dlpTab.Size = new System.Drawing.Size(804, 306);
            this.dlpTab.TabIndex = 1;
            this.dlpTab.Text = "DLP";
            this.dlpTab.UseVisualStyleBackColor = true;
            // 
            // dlpTable
            // 
            this.dlpTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dlpTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.LicenseNumber,
            this.Date,
            this.ImageUrl,
            this.Confidence,
            this.gMapUri});
            this.dlpTable.Location = new System.Drawing.Point(29, 19);
            this.dlpTable.Name = "dlpTable";
            this.dlpTable.ReadOnly = true;
            this.dlpTable.Size = new System.Drawing.Size(744, 252);
            this.dlpTable.TabIndex = 0;
            this.dlpTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dlpTable_CellContentClick);
            // 
            // camTab
            // 
            this.cameraTab.Controls.Add(this.longField);
            this.cameraTab.Controls.Add(this.latField);
            this.cameraTab.Controls.Add(this.cameraTable);
            this.cameraTab.Controls.Add(this.submitCam);
            this.cameraTab.Controls.Add(this.isTrustedBox);
            this.cameraTab.Controls.Add(this.isTrustedLabel);
            this.cameraTab.Controls.Add(this.hostUrlField);
            this.cameraTab.Controls.Add(this.longLabel);
            this.cameraTab.Controls.Add(this.latLabel);
            this.cameraTab.Controls.Add(this.hostUrlLabel);
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Padding = new System.Windows.Forms.Padding(3);
            this.cameraTab.Size = new System.Drawing.Size(804, 306);
            this.cameraTab.TabIndex = 2;
            this.cameraTab.Text = "Camera";
            this.cameraTab.UseVisualStyleBackColor = true;
            // 
            // longField
            // 
            this.longField.DecimalPlaces = 6;
            this.longField.Location = new System.Drawing.Point(68, 67);
            this.longField.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.longField.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.longField.Name = "longField";
            this.longField.Size = new System.Drawing.Size(98, 20);
            this.longField.TabIndex = 21;
            // 
            // latField
            // 
            this.latField.DecimalPlaces = 6;
            this.latField.Location = new System.Drawing.Point(68, 41);
            this.latField.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.latField.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.latField.Name = "latField";
            this.latField.Size = new System.Drawing.Size(98, 20);
            this.latField.TabIndex = 20;
            // 
            // cameraTable
            // 
            this.cameraTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cameraTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.HostUrl,
            this.Lat,
            this.Long,
            this.IsTrustedSource,
            this.IsActive});
            this.cameraTable.Location = new System.Drawing.Point(185, 15);
            this.cameraTable.Name = "cameraTable";
            this.cameraTable.ReadOnly = true;
            this.cameraTable.Size = new System.Drawing.Size(601, 257);
            this.cameraTable.TabIndex = 18;
            // 
            // idColumn
            // 
            this.idColumn.DataPropertyName = "Id";
            this.idColumn.HeaderText = "Id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 50;
            // 
            // HostUrl
            // 
            this.HostUrl.DataPropertyName = "HostUrl";
            this.HostUrl.HeaderText = "Host Url";
            this.HostUrl.Name = "HostUrl";
            this.HostUrl.ReadOnly = true;
            // 
            // Lat
            // 
            this.Lat.DataPropertyName = "Latitude";
            this.Lat.HeaderText = "Latitude";
            this.Lat.Name = "Lat";
            this.Lat.ReadOnly = true;
            // 
            // Long
            // 
            this.Long.DataPropertyName = "Longitude";
            this.Long.HeaderText = "Longitude";
            this.Long.Name = "Long";
            this.Long.ReadOnly = true;
            // 
            // IsTrustedSource
            // 
            this.IsTrustedSource.DataPropertyName = "IsTrustedSource";
            this.IsTrustedSource.HeaderText = "Is Trusted";
            this.IsTrustedSource.Name = "IsTrustedSource";
            this.IsTrustedSource.ReadOnly = true;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Is Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            // 
            // submitCam
            // 
            this.submitCam.Location = new System.Drawing.Point(65, 127);
            this.submitCam.Name = "submitCam";
            this.submitCam.Size = new System.Drawing.Size(75, 23);
            this.submitCam.TabIndex = 17;
            this.submitCam.Text = "Submit";
            this.submitCam.UseVisualStyleBackColor = true;
            this.submitCam.Click += new System.EventHandler(this.submitCam_Click);
            // 
            // isTrustedBox
            // 
            this.isTrustedBox.AutoSize = true;
            this.isTrustedBox.Location = new System.Drawing.Point(65, 95);
            this.isTrustedBox.Name = "isTrustedBox";
            this.isTrustedBox.Size = new System.Drawing.Size(15, 14);
            this.isTrustedBox.TabIndex = 16;
            this.isTrustedBox.UseVisualStyleBackColor = true;
            // 
            // isTrustedLabel
            // 
            this.isTrustedLabel.AutoSize = true;
            this.isTrustedLabel.Location = new System.Drawing.Point(8, 95);
            this.isTrustedLabel.Name = "isTrustedLabel";
            this.isTrustedLabel.Size = new System.Drawing.Size(51, 13);
            this.isTrustedLabel.TabIndex = 15;
            this.isTrustedLabel.Text = "IsTrusted";
            // 
            // hostUrlField
            // 
            this.hostUrlField.Location = new System.Drawing.Point(66, 15);
            this.hostUrlField.Name = "hostUrlField";
            this.hostUrlField.Size = new System.Drawing.Size(100, 20);
            this.hostUrlField.TabIndex = 19;
            // 
            // longLabel
            // 
            this.longLabel.AutoSize = true;
            this.longLabel.Location = new System.Drawing.Point(28, 69);
            this.longLabel.Name = "longLabel";
            this.longLabel.Size = new System.Drawing.Size(31, 13);
            this.longLabel.TabIndex = 13;
            this.longLabel.Text = "Long";
            // 
            // latLabel
            // 
            this.latLabel.AutoSize = true;
            this.latLabel.Location = new System.Drawing.Point(37, 43);
            this.latLabel.Name = "latLabel";
            this.latLabel.Size = new System.Drawing.Size(22, 13);
            this.latLabel.TabIndex = 11;
            this.latLabel.Text = "Lat";
            // 
            // hostUrlLabel
            // 
            this.hostUrlLabel.AutoSize = true;
            this.hostUrlLabel.Location = new System.Drawing.Point(17, 18);
            this.hostUrlLabel.Name = "hostUrlLabel";
            this.hostUrlLabel.Size = new System.Drawing.Size(42, 13);
            this.hostUrlLabel.TabIndex = 9;
            this.hostUrlLabel.Text = "HostUrl";
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 50;
            // 
            // LicenseNumber
            // 
            this.LicenseNumber.DataPropertyName = "PlateNumber";
            this.LicenseNumber.HeaderText = "License Number";
            this.LicenseNumber.Name = "LicenseNumber";
            this.LicenseNumber.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "DetectedDateTime";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 150;
            // 
            // ImageUrl
            // 
            this.ImageUrl.DataPropertyName = "ImgUrl";
            this.ImageUrl.HeaderText = "Image URL";
            this.ImageUrl.Name = "ImageUrl";
            this.ImageUrl.ReadOnly = true;
            this.ImageUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ImageUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ImageUrl.Width = 150;
            // 
            // Confidence
            // 
            this.Confidence.DataPropertyName = "Confidence";
            this.Confidence.HeaderText = "Confidence";
            this.Confidence.Name = "Confidence";
            this.Confidence.ReadOnly = true;
            // 
            // gMapUri
            // 
            this.gMapUri.DataPropertyName = "GMapsUri";
            this.gMapUri.HeaderText = "GMapsUri";
            this.gMapUri.Name = "gMapUri";
            this.gMapUri.ReadOnly = true;
            this.gMapUri.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gMapUri.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.gMapUri.Width = 150;
            // 
            // MissingLicensePlateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MissingLicensePlateForm";
            this.Text = "HUCARES";
            this.Load += new System.EventHandler(this.MissingLicensePlateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateImage)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.mlpTab.ResumeLayout(false);
            this.mlpTab.PerformLayout();
            this.dlpTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dlpTable)).EndInit();
            this.cameraTab.ResumeLayout(false);
            this.cameraTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.latField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox licensePlateImage;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label mlpLabel;
        private System.Windows.Forms.TextBox lettersBox;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage mlpTab;
        private System.Windows.Forms.TabPage dlpTab;
        private System.Windows.Forms.TextBox digitsBox;
        private System.Windows.Forms.DataGridView dlpTable;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.DataGridView cameraTable;
        private System.Windows.Forms.Button submitCam;
        private System.Windows.Forms.CheckBox isTrustedBox;
        private System.Windows.Forms.Label isTrustedLabel;
        private System.Windows.Forms.TextBox hostUrlField;
        private System.Windows.Forms.Label longLabel;
        private System.Windows.Forms.Label latLabel;
        private System.Windows.Forms.Label hostUrlLabel;
        private System.Windows.Forms.NumericUpDown longField;
        private System.Windows.Forms.NumericUpDown latField;
        private System.Windows.Forms.Button showMlpListButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Long;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTrustedSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewLinkColumn ImageUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confidence;
        private System.Windows.Forms.DataGridViewLinkColumn gMapUri;
    }
}

