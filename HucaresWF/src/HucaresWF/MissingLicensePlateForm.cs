﻿using Hucares.Server.Client;
using Hucares.Server.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HucaresWF
{
    public partial class MissingLicensePlateForm : Form
    {
        private ICameraInfoClient cameraClient;

        public MissingLicensePlateForm(ICameraInfoClient cameraClient)
        {
            this.cameraClient = cameraClient;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MissingLicensePlateForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private async void submitCam_Click(object sender, EventArgs e)
        {
            var camObj = new CameraInfo()
            {
                HostUrl = hostUrlField.Text,
                Latitude = (double) latField.Value,
                Longitude = (double) longField.Value,
                IsTrustedSource = isTrustedBox.Checked
            };

            await cameraClient.InsertCamera(camObj);
        }

        private void showMlpButton_Click(object sender, EventArgs e)
        {
            MissingLicensePlateList mlpList = new MissingLicensePlateList();
            mlpList.Show();
        }
    }
}
