using HucaresServer.Storage.Models;
using System.Data.Entity;

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
