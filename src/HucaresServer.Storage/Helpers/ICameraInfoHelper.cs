using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    public interface ICameraInfoHelper
    {
        CameraInfo InsertCamera(string hostUrl, double latitude, double longtitude, bool trustedSource = false);
        CameraInfo UpdateCameraSource(int id, string hostUrl, bool trustedSource);
        CameraInfo UpdateCameraActivity(int id, bool inactive);
        CameraInfo DeleteCameraById(int id);
        IEnumerable<CameraInfo> GetAllCameras(bool? trustedSource = null);
        IEnumerable<CameraInfo> GetActiveCameras(bool? trustedSource = null);
        IEnumerable<CameraInfo> GetInactiveCameras();
    }
}
