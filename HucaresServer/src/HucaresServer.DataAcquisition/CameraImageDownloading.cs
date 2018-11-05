using System;
using System.Linq;
using System.Net;
using HucaresServer.Storage.Helpers;

namespace HucaresServer.DataAcquisition
{
    public class CameraImageDownloading : ICameraImageDownloading
    {
        // TODO: Take this value from a configuration file or somewhere else
        private const string TemporaryStorageUrl = "/temporaryImages";

        private ICameraInfoHelper _cameraInfoHelper;
        private IImageSaver _imageSaver;

        public CameraImageDownloading(ICameraInfoHelper cameraInfoHelper = null, IImageSaver imageSaver = null)
        {
            _cameraInfoHelper = cameraInfoHelper ?? new CameraInfoHelper();
            _imageSaver = imageSaver ?? new LocalImageSaver();
        }

        public int DownloadImagesFromCameraInfoSources(bool? isTrusted = null, DateTime? downloadDateTime = null)
        {
            var cameraDataToDownload = _cameraInfoHelper.GetActiveCameras(isTrusted = isTrusted).ToList();

            using (var webClient = new WebClient())
            {
                foreach (var cameraData in cameraDataToDownload)
                {
                    webClient.DownloadFileAsync(new Uri(cameraData.HostUrl),
                        _imageSaver.GenerateStorageLocationAndFilename(cameraData.Id, downloadDateTime));
                }
            }

            return cameraDataToDownload.Count;
        }
    }
}