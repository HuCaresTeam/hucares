using System;
using System.Threading.Tasks;
using SqliteManipulation.Models;

namespace Hucares.Server.Client
{
    public interface ICameraInfoClient
    {
        Task DeleteAllCameras(bool? isTrustedSource = null);
        Task<Camera> InsertCamera(Camera cameraInfo);
    }
}