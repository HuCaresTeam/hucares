using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HucaresServer.Storage.Helpers;
using HucaresServer.Utils;

namespace HucaresServer.DataAcquisition
{
    public class CameraImageDownloading : ICameraImageDownloading
    {
        private readonly string TemporaryStorageUrl = Config.TemporaryStorageUrl;

        private ICameraInfoHelper _cameraInfoHelper;
        private IImageSaver _imageSaver;
        private IWebClientFactory _webClientFactory;

        public CameraImageDownloading(ICameraInfoHelper cameraInfoHelper = null, IImageSaver imageSaver = null,
            IWebClientFactory webClientFactory = null)
        {
            _cameraInfoHelper = cameraInfoHelper ?? new CameraInfoHelper();
            _imageSaver = imageSaver ?? new LocalImageSaver(TemporaryStorageUrl);
            _webClientFactory = webClientFactory ?? new CustomWebClientFactory();
        }

        public async Task<int> DownloadImagesFromCameraInfoSources(bool? isTrusted = null, DateTime? downloadDateTime = null)
        {
            var cameraDataToDownload = _cameraInfoHelper.GetActiveCameras(isTrusted).ToList();
            var imageSavingTasks = new List<Task>();

            var datetime = downloadDateTime ?? DateTime.Now;

            foreach (var cameraData in cameraDataToDownload)
            {
                imageSavingTasks.Add(Task.Factory.StartNew(
                    () => DownloadAndSaveImage(cameraData.HostUrl, cameraData.Id, datetime)));
            }

            await Task.WhenAll(imageSavingTasks.ToArray());

            return cameraDataToDownload.Count;
        }

        private void DownloadAndSaveImage(string imageUrl, int cameraId, DateTime captureDateTime)
        {
            using (var webClient = _webClientFactory.BuildWebClient())
            {
                var imageData = webClient.DownloadData(imageUrl);

                using (var memoryStream = new MemoryStream(imageData))
                {
                    _imageSaver.SaveImage(cameraId, captureDateTime, new Bitmap(memoryStream));
                }
            }
        }
    }
}