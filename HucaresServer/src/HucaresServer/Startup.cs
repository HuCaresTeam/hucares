using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HucaresServer.Startup))]

namespace HucaresServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
