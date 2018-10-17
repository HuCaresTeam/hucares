using Hucares.Server.Client;
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

        private void submitButton_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void MissingLicensePlateForm_Load(object sender, EventArgs e)
        {
            cameraTable.DataSource = await cameraClient.GetAllCameras();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
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
            cameraTable.DataSource = await cameraClient.GetAllCameras();
        }

        private void showMlpList_Click(object sender, EventArgs e)
        {
            MissingLicensePlateList mlpList = new MissingLicensePlateList();
            mlpList.Show();
        }
    }
}
