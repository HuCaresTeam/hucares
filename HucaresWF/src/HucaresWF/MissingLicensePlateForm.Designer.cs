namespace HucaresWF
{
    partial class MissingLicensePlateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissingLicensePlateForm));
            this.licensePlateImage = new System.Windows.Forms.PictureBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.mlpLabel = new System.Windows.Forms.Label();
            this.lettersBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.digitsBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confidence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gMapUri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.longField = new System.Windows.Forms.NumericUpDown();
            this.latField = new System.Windows.Forms.NumericUpDown();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Long = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTrustedSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submitCam = new System.Windows.Forms.Button();
            this.isTrustedBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.hostUrlField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateImage)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.latField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
            this.submitButton.Click += new System.EventHandler(this.button1_Click);
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
            this.mlpLabel.Click += new System.EventHandler(this.label1_Click);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-2, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(812, 332);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.digitsBox);
            this.tabPage1.Controls.Add(this.submitButton);
            this.tabPage1.Controls.Add(this.mlpLabel);
            this.tabPage1.Controls.Add(this.lettersBox);
            this.tabPage1.Controls.Add(this.licensePlateImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(804, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MLP Insert";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(804, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DLP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.LicenseNumber,
            this.Date,
            this.ImageUrl,
            this.Confidence,
            this.gMapUri});
            this.dataGridView1.Location = new System.Drawing.Point(82, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(643, 230);
            this.dataGridView1.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            // 
            // LicenseNumber
            // 
            this.LicenseNumber.HeaderText = "License Number";
            this.LicenseNumber.Name = "LicenseNumber";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // ImageUrl
            // 
            this.ImageUrl.HeaderText = "Image URL";
            this.ImageUrl.Name = "ImageUrl";
            // 
            // Confidence
            // 
            this.Confidence.HeaderText = "Confidence";
            this.Confidence.Name = "Confidence";
            // 
            // gMapUri
            // 
            this.gMapUri.HeaderText = "GMapsUri";
            this.gMapUri.Name = "gMapUri";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.longField);
            this.tabPage3.Controls.Add(this.latField);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.submitCam);
            this.tabPage3.Controls.Add(this.isTrustedBox);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.hostUrlField);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(804, 306);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Camera";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
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
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.HostUrl,
            this.Lat,
            this.Long,
            this.IsTrustedSource});
            this.dataGridView2.Location = new System.Drawing.Point(198, 15);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(544, 261);
            this.dataGridView2.TabIndex = 18;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // HostUrl
            // 
            this.HostUrl.HeaderText = "Host Url";
            this.HostUrl.Name = "HostUrl";
            // 
            // Lat
            // 
            this.Lat.HeaderText = "Latitude";
            this.Lat.Name = "Lat";
            // 
            // Long
            // 
            this.Long.HeaderText = "Longitude";
            this.Long.Name = "Long";
            // 
            // IsTrustedSource
            // 
            this.IsTrustedSource.HeaderText = "Is Trusted";
            this.IsTrustedSource.Name = "IsTrustedSource";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "IsTrusted";
            // 
            // hostUrlField
            // 
            this.hostUrlField.Location = new System.Drawing.Point(66, 15);
            this.hostUrlField.Name = "hostUrlField";
            this.hostUrlField.Size = new System.Drawing.Size(100, 20);
            this.hostUrlField.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Long";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lat";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "HostUrl";
            // 
            // MissingLicensePlateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.tabControl1);
            this.Name = "MissingLicensePlateForm";
            this.Text = "HUCARES";
            this.Load += new System.EventHandler(this.MissingLicensePlateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateImage)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.longField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.latField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox licensePlateImage;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label mlpLabel;
        private System.Windows.Forms.TextBox lettersBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox digitsBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confidence;
        private System.Windows.Forms.DataGridViewTextBoxColumn gMapUri;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button submitCam;
        private System.Windows.Forms.CheckBox isTrustedBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox hostUrlField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Long;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTrustedSource;
        private System.Windows.Forms.NumericUpDown longField;
        private System.Windows.Forms.NumericUpDown latField;
    }
}

