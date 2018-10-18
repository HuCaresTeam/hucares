namespace HucaresWF
{
    partial class MissingLicensePlateList
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
            lock (mlpLock)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mlpTable = new System.Windows.Forms.DataGridView();
            this.PlateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plateNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mlpTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mlpTable
            // 
            this.mlpTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mlpTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlateId,
            this.plateNumber,
            this.Date});
            this.mlpTable.Location = new System.Drawing.Point(12, 34);
            this.mlpTable.Name = "mlpTable";
            this.mlpTable.ReadOnly = true;
            this.mlpTable.Size = new System.Drawing.Size(453, 410);
            this.mlpTable.TabIndex = 0;
            // 
            // PlateId
            // 
            this.PlateId.DataPropertyName = "Id";
            this.PlateId.HeaderText = "Id";
            this.PlateId.Name = "PlateId";
            this.PlateId.ReadOnly = true;
            // 
            // plateNumber
            // 
            this.plateNumber.DataPropertyName = "PlateNumber";
            this.plateNumber.HeaderText = "Plate number";
            this.plateNumber.Name = "plateNumber";
            this.plateNumber.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "SearchStartDateTime";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 150;
            // 
            // MissingLicensePlateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 456);
            this.Controls.Add(this.mlpTable);
            this.Name = "MissingLicensePlateList";
            this.Text = "MLP List";
            this.Load += new System.EventHandler(this.MissingLicensePlateList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mlpTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mlpTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn plateNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
    }
}