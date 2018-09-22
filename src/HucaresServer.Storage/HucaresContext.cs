using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage
{
    public class HucaresContext : DbContext
    {
        public HucaresContext(): base()
        {

        }

        public DbSet<MissingLicensePlate> MissingLicensePlates { get; set; }
        public DbSet<DetectedLicensePlate> DetectedLicensePlates { get; set; }
        public DbSet<CameraInfo> CameraInfo { get; set; }
    }
}
