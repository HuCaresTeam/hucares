using Hangfire;
using HucaresServer.DataAcquisition;
using HucaresServer.Storage;
using HucaresServer.TimedProcess;
using System.Web;

namespace HucaresServer
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            using (var ctx = new HucaresContext())
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(ctx.Database.Connection.ConnectionString);
            }

            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            var dlpTimedProcess = new DlpCollectionProcess();
            RecurringJob.AddOrUpdate(() => dlpTimedProcess.StartProccess(), Cron.Minutely);
        }
    }
}
