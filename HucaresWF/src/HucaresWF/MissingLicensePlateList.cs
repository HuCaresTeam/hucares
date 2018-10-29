using Hucares.Server.Client;
using HucaresWF.Properties;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HucaresWF
{
    public partial class MissingLicensePlateList : Form
    {
        private IMissingPlateClient mlpClient;
        private IExceptionEventObservable parent;
        private static object mlpLock = new object();
        private static MissingLicensePlateList mlpList;

        private MissingLicensePlateList(IMissingPlateClient mlpClient, IExceptionEventObservable parent)
        {
            this.mlpClient = mlpClient;
            this.parent = parent;
            InitializeComponent();
        }

        public static MissingLicensePlateList CreateIfNotExistsAndGetInstance(IMissingPlateClient mlpClient, IExceptionEventObservable parent)
        {
            lock (mlpLock)
            {
                if (null == mlpList || mlpList.IsDisposed)
                {
                    mlpList = new MissingLicensePlateList(mlpClient, parent);
                }
                return mlpList;
            }
        }

        private async void MissingLicensePlateList_Load(object sender, EventArgs e)
        {
            try
            {
                await UpdateDatasource();
            }

            catch (HttpRequestException ex)
            {
                var eventArgs = new ExceptionEventArgs { ExceptionMessage = String.Format(Resources.Error_Connection, ex.Message) };
                parent.InvokeHandler(eventArgs);
            }
        }

        public async Task UpdateDatasource()
        {
            var datasource = (await mlpClient.GetAllPlateRecords())
                .Select(mlp => new { mlp.Id, mlp.PlateNumber, mlp.SearchStartDateTime });
            mlpTable.DataSource = datasource.ToList();
        }
    }
}
