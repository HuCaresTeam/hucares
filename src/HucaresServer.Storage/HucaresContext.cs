using HucaresServer.Storage.Models;
using System.Data.Entity;

namespace HucaresServer.Storage
{
    /// <summary>
    /// Creates a database called "HucaresServer.Storage.HucaresContext.
    /// Generates Tables according to their names from properties of type DbSet<T>.
    /// Used to access DB information.
    /// </summary>
    public class HucaresContext : DbContext
    {
        public HucaresContext(): base()
        {

        }

        /// <summary>
        /// Stores information about past and present Missing License Plates.
        /// </summary>
        public DbSet<MissingLicensePlate> MissingLicensePlates { get; set; }

        /// <summary>
        /// Stores information about all parsed license plate numbers from images.
        /// </summary>
        public DbSet<DetectedLicensePlate> DetectedLicensePlates { get; set; }

        /// <summary>
        /// Stores information about all of the cameras used to retrieve images for processing.
        /// </summary>
        public DbSet<CameraInfo> CameraInfo { get; set; }
    }
}
