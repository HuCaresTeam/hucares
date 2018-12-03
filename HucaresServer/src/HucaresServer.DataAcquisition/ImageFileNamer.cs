using System;
using System.Text.RegularExpressions;

namespace HucaresServer.DataAcquisition
{
    public class ImageFileNamer : IImageFileNamer
    {
        /// <summary>
        /// Method generates a filename to be used for an image
        /// </summary>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="captureDateTime"> Optional parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> The generated filename for the image.</returns>
        public string GenerateFileName(int cameraId, DateTime captureDateTime)
        {
            return $"{cameraId}_{captureDateTime.ToString("yyyy-MM-dd-HH-mm-ss")}_{Guid.NewGuid().ToString()}";
        }

        public int ExtractCameraId(string fileName)
        {
            var fileCamIdMatch = Regex.Match(fileName, @"^(\d+)_.+");
            if (!fileCamIdMatch.Success)
            {
                throw new Exception("Bad file name");
            }

            int.TryParse(fileCamIdMatch.Groups[1].Value, out int camId);
            return camId;
        }
    }
}
