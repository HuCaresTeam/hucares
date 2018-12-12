using System;
using System.Threading.Tasks;
using SqliteManipulation.Models;

namespace Hucares.Server.Client
{
    public interface ICameraInfoClient
    {
        Uri HostUri { get; }

        Task DeleteAllCameras(bool? isTrustedSource = null);
        Task<Camera> InsertCamera(Camera cameraInfo);
    }
}