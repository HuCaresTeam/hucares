using Hucares.Server.Client;
using System;
using System.Windows.Forms;
using System.Net.Http;
using HucaresWF.Properties;

namespace HucaresWF
{
    public partial class HucaresForm : Form, IExceptionEventObservable
    {
        public event EventHandler<ExceptionEventArgs> ExceptionEventHandler;

        private ICameraInfoClient cameraClient;
        private IMissingPlateClient mlpClient;
        private IDetectedPlateClient dlpClient;

        public HucaresForm(ICameraInfoClient cameraClient, IMissingPlateClient mlpClient, IDetectedPlateClient dlpClient)
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
            catch (HttpRequestException ex)
            {
                var eventArgs = new ExceptionEventArgs { ExceptionMessage = String.Format(Resources.Error_Connection, ex.Message) };
                InvokeHandler(eventArgs);
            }
        }

        public void InvokeHandler(ExceptionEventArgs eventArgs)
        {
            ExceptionEventHandler?.Invoke(this, eventArgs);
        }
    }
}
