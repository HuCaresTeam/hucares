using System;
using HucaresServer.Storage.Helpers;

namespace HucaresServer.DataAcquisition
{
    public class CameraImageDownloading : ICameraImageDownloading
    {
        private  ICameraInfoHelper _cameraInfoHelper;

        public CameraImageDownloading(ICameraInfoHelper cameraInfoHelper = null)
        {
            _cameraInfoHelper = cameraInfoHelper ?? new CameraInfoHelper();
        }
        
        public int DownloadImagesFromCameraInfoSources(bool? isTrusted = null, DateTime? downloadDateTime = null)
        {
            throw new System.NotImplementedException();
        }
    }
}