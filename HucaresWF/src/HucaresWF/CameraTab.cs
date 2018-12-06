using Hucares.Server.Client.Models;
using HucaresWF.Properties;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HucaresWF
{
    public partial class HucaresForm : Form
    {
        private async Task UpdateCameraDataSource()
        {
            cameraTable.DataSource = await cameraClient.GetAllCameras();
        }

        private async void submitCam_Click(object sender, EventArgs e)
        {
            if (!Uri.IsWellFormedUriString(hostUrlField.Text, UriKind.Absolute))
            {
                var eventArgs = new ExceptionEventArgs { ExceptionMessage = Resources.Error_BadUri };
                ExceptionEventHandler?.Invoke(this, eventArgs);
                return;
            };
            var camObj = new CameraInfo()
            {
                HostUrl = hostUrlField.Text,
                Latitude = (double)latField.Value,
                Longitude = (double)longField.Value,
                IsTrustedSource = isTrustedBox.Checked
            };

            try
            {
                await cameraClient.InsertCamera(camObj);
            }
            catch (Exception ex)
            {
                var eventArgs = new ExceptionEventArgs { ExceptionMessage = ex.Message};
                ExceptionEventHandler?.Invoke(this, eventArgs);
            }
            await UpdateCameraDataSource();
        }
    }
}
