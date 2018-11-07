using System;
using System.Threading.Tasks;

namespace HucaresServer.DataAcquisition
{
    public interface ICameraImageDownloading
    {
        /// <summary>
        /// Downloads image from every source available in CameraInfo table. Optionally can be filtered by isTrusted value.
        /// </summary>
        /// <param name="isTrusted"> Optional parameter indicating if the source of image if verified.</param>
        /// <param name="downloadDateTime"> Optional parameter for the datetime of when the pictures are downloaded. If
        /// not specified the current datetime will be used.</param>
        /// <returns> Number of images downloaded.</returns>
        Task<int> DownloadImagesFromCameraInfoSources(bool? isTrusted = null, DateTime? downloadDateTime = null);
    }
}