using Hucares.Server.Client;
using Hucares.Server.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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

        private async void MissingLicensePlateForm_Load(object sender, EventArgs e)
        {
            try
            {
                await UpdateCameraDataSource();
                await UpdateDlpDataSource();
            }
            catch
            {
                MessageBox.Show("Check your connection!");
            }
        }

        private async Task UpdateDlpDataSource()
        {
            var cameraList = await cameraClient.GetActiveCameras();
            var dlpList = await dlpClient.GetAllDetectedMissingPlates();

            var dlpListWithCam = dlpList.Join
                 (
                 cameraList,
                 detectedLicensePlates => detectedLicensePlates.CamId,
                 camera => camera.Id,
                 (detectedLicensePlates, camera) => new { detectedLicensePlates, camera }
                 );

            dlpTable.DataSource = dlpListWithCam
                .Select(dnc => new
                {
                    dnc.detectedLicensePlates.Id,
                    dnc.detectedLicensePlates.PlateNumber,
                    dnc.detectedLicensePlates.DetectedDateTime,
                    dnc.detectedLicensePlates.ImgUrl,
                    dnc.detectedLicensePlates.Confidence,
                    GMapsUri = $"https://www.google.com/maps/search/?api=1&query={dnc.camera.Latitude},{dnc.camera.Longitude}"
                })
                .ToList();
        }

        private async Task UpdateCameraDataSource()
        {
            cameraTable.DataSource = await cameraClient.GetAllCameras();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            var plateNumber = lettersBox.Text + digitsBox.Text;
            
            if(!validatePlateNumber(plateNumber))
            {
                MessageBox.Show("You have entered wrong plate number. Check it!");
                return;
            }

            await mlpClient.InsertPlateRecord(plateNumber, DateTime.Now);
            await MissingLicensePlateList.CreateIfNotExistsAndGetInstance(mlpClient).UpdateDatasource();
            await UpdateDlpDataSource();
        }

        private void mlpTab_Click(object sender, EventArgs e)
        {

        }

        private void dlpTab_Click(object sender, EventArgs e)
        {

        }

        private void cameraTab_Click(object sender, EventArgs e)
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

        private bool validatePlateNumber(string plateNumber)
        {
            var regex = new Regex(@"^([A-Z]){3}\d{3}$");
            return regex.IsMatch(plateNumber);
        }
    }
}
