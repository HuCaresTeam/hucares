using System;

namespace HucaresServer.DataAcquisition
{
    public interface IImageFileNamer
    {
        int ExtractCameraId(string fileName);
        string GenerateFileName(int cameraId, DateTime captureDateTime);
    }
}