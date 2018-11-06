using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            _imageSaver = imageSaver ?? new LocalImageSaver(TemporaryStorageUrl);
        }

        public int DownloadImagesFromCameraInfoSources(bool? isTrusted = null, DateTime? downloadDateTime = null)
        {
            var cameraDataToDownload = _cameraInfoHelper.GetActiveCameras(isTrusted = isTrusted).ToList();
            var imageSavingTasks = new List<Task>();

            foreach (var cameraData in cameraDataToDownload)
            {
                imageSavingTasks.Add(Task.Factory.StartNew(() => DownloadAndSaveImage(cameraData.HostUrl)));
            }

            Task.WaitAll(imageSavingTasks.ToArray());

            return cameraDataToDownload.Count;
        }

        private void DownloadAndSaveImage(string imageUrl)
        {
            using (var webClient = new WebClient())
            {
                var imageData = webClient.DownloadData(imageUrl);

                using (var memoryStream = new MemoryStream(imageData))
                {
                    _imageSaver.SaveImage(new Bitmap(memoryStream));
                }
            }
        }
    }
}