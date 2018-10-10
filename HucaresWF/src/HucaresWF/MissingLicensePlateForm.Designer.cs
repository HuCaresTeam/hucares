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
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateImage)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabPage1.Text = "MLP";
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(804, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
    }
}

