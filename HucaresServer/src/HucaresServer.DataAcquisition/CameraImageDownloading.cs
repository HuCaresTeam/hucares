using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HucaresServer.Storage.Helpers;

namespace HucaresServer.DataAcquisition
{
    public class CameraImageDownloading : ICameraImageDownloading
    {
        private ICameraInfoHelper _cameraInfoHelper;
        private IImageManipulator _imageSaver;
        private IWebClientFactory _webClientFactory;

        public CameraImageDownloading(ICameraInfoHelper cameraInfoHelper = null, 
            IImageManipulator imageSaver = null,
            IWebClientFactory webClientFactory = null)
        {
            _cameraInfoHelper = cameraInfoHelper ?? new CameraInfoHelper();
            _imageSaver = imageSaver ?? new LocalImageManipulator();
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
                _imageSaver.SaveImage(cameraId, captureDateTime, imageData);
            }
        }
    }
}