using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HucaresWF
{
    public partial class HucaresForm : Form
    {
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

        private void dlpTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dlpTable.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex != -1)
            {
                var value = dlpTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                System.Diagnostics.Process.Start(value);
            }
        }

        public async void refresh_Click(object sender, EventArgs e)
        {
            await UpdateDlpDataSource();
        }
    }
}
