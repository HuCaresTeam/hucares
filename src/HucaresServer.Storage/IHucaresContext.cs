using System.Data.Entity;
using HucaresServer.Storage.Models;

namespace HucaresServer.Storage
{
    public interface IHucaresContext
    {
        DbSet<CameraInfo> CameraInfo { get; set; }
        DbSet<DetectedLicensePlate> DetectedLicensePlates { get; set; }
        DbSet<MissingLicensePlate> MissingLicensePlates { get; set; }
    }
}