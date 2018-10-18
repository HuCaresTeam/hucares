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
        private IMissingPlateClient mlpClient;
        private IDetectedPlateClient dlpClient;

        public MissingLicensePlateForm(ICameraInfoClient cameraClient, IMissingPlateClient mlpClient, IDetectedPlateClient dlpClient)
        {
            this.cameraClient = cameraClient;
            this.mlpClient = mlpClient;
            this.dlpClient = dlpClient;
            InitializeComponent();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            var plateNumber = lettersBox.Text + digitsBox.Text;
            await mlpClient.InsertPlateRecord(plateNumber, DateTime.Now);
            await MissingLicensePlateList.CreateIfNotExistsAndGetInstance(mlpClient).UpdateDatasource();
            await UpdateDlpDataSource();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void MissingLicensePlateForm_Load(object sender, EventArgs e)
        {
            await UpdateCameraDataSource();
            await UpdateDlpDataSource();
        }

        private async Task UpdateCameraDataSource()
        {
            cameraTable.DataSource = await cameraClient.GetAllCameras();
        }

        private async Task UpdateDlpDataSource()
        {
            var cameraList = await cameraClient.GetActiveCameras();
            var dlpList = await dlpClient.GetAllDetectedMissingPlates();

           var dlpListWithCam = dlpList.Join
                (
                cameraList,
                dlp => dlp.CamId,
                cam => cam.Id,
                (dlp, cam) => new { dlp, cam }
                );

            dlpTable.DataSource = dlpListWithCam
                .Select(dnc => new
                    {
                    dnc.dlp.Id,
                    dnc.dlp.PlateNumber,
                    dnc.dlp.DetectedDateTime,
                    dnc.dlp.ImgUrl,
                    dnc.dlp.Confidence,
                    GMapsUri = $"https://www.google.com/maps/search/?api=1&query={dnc.cam.Latitude},{dnc.cam.Longitude}"
                })
                .ToList();
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
            await UpdateCameraDataSource();
        }

        private void showMlpList_Click(object sender, EventArgs e)
        {
            MissingLicensePlateList mlpList = MissingLicensePlateList.CreateIfNotExistsAndGetInstance(mlpClient);
            mlpList.Show();
        }

        private void dlpTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dlpTable.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex != -1)
            {
                var value = dlpTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                System.Diagnostics.Process.Start(value);
            }
        }
    }
}
