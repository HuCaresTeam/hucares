using Hucares.Server.Client;
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
    public partial class MissingLicensePlateList : Form
    {
        private IMissingPlateClient mlpClient;
        private static object mlpLock = new object();
        private static MissingLicensePlateList mlpList;

        private MissingLicensePlateList(IMissingPlateClient mlpClient)
        {
            this.mlpClient = mlpClient;
            InitializeComponent();
        }

        public static MissingLicensePlateList CreateIfNotExistsAndGetInstance(IMissingPlateClient mlpClient)
        {
            lock (mlpLock)
            {
                if (null == mlpList || mlpList.IsDisposed)
                {
                    mlpList = new MissingLicensePlateList(mlpClient);
                }
                return mlpList;
            }
        }

        private async void MissingLicensePlateList_Load(object sender, EventArgs e)
        {
            await UpdateDatasource();
        }

        public async Task UpdateDatasource()
        {
            var datasource = (await mlpClient.GetAllPlateRecords())
                .Select(mlp => new { mlp.Id, mlp.PlateNumber, mlp.SearchStartDateTime });
            mlpTable.DataSource = datasource.ToList();
        }
    }
}
