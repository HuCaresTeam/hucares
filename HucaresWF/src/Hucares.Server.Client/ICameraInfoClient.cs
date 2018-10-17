using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;

namespace Hucares.Server.Client
{
    public interface ICameraInfoClient
    {
        Uri HostUri { get; }

        Task<IEnumerable<CameraInfo>> GetActiveCameras(bool? isTrustedSource = null);
        Task<IEnumerable<CameraInfo>> GetAllCameras(bool? isTrustedSource = null);
        Task<IEnumerable<CameraInfo>> GetInactiveCameras();
        Task<CameraInfo> InsertCamera(CameraInfo cameraInfo);
        Task<CameraInfo> UpdateCameraActivity(CameraInfo cameraInfo);
        Task<CameraInfo> UpdateCameraSource(CameraInfo cameraInfo);
    }
}