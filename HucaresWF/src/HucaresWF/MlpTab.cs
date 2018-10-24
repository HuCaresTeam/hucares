using HucaresWF.Properties;
using System;
using System.Windows.Forms;

namespace HucaresWF
{
    public partial class HucaresForm : Form
    {
        private void showMlpList_Click(object sender, EventArgs e)
        {
            MissingLicensePlateList mlpList = MissingLicensePlateList.CreateIfNotExistsAndGetInstance(mlpClient, this);
            mlpList.Show();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            var plateNumber = lettersBox.Text + digitsBox.Text;
            if (plateNumber.ValidatePlateNumber())
            {
                var eventArgs = new ExceptionEventArgs { ExceptionMessage = Resources.Error_BadUri };
                ExceptionEventHandler?.Invoke(this, eventArgs);
                return;
            }

            await mlpClient.InsertPlateRecord(plateNumber, DateTime.Now);
            await MissingLicensePlateList.CreateIfNotExistsAndGetInstance(mlpClient, this).UpdateDatasource();
            await UpdateDlpDataSource();
        }
    }
}
