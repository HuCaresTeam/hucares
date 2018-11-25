using HucaresServer.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace HucaresServer.DataAcquisition
{
    //TODO seperate "Normal" and "Temp" image saver logic
    public class LocalImageSaver : IImageSaver
    {
        private readonly string _pathToStorageLocation = Config.TemporaryStorageUrl;

        //TODO will be deprecated due to config file
        public LocalImageSaver(string pathToStorageLocation = null)
        {
            _pathToStorageLocation = pathToStorageLocation ?? _pathToStorageLocation;
        }

        public string MoveFileToPerm(FileInfo file)
        {
            var newFileLocation = Path.Combine(Config.FullPermStoragePath, file.Name);
            Directory.Move(file.FullName, newFileLocation);

            return newFileLocation;
        }

        public string SaveImage(int cameraId, DateTime captureDateTime, Image imageToSave)
        {
            var folderLocationPath = GenerateFolderLocationPath(captureDateTime);
            var fileName = GenerateFileName(cameraId, captureDateTime);
            var fullImageLocation = Path.Combine(folderLocationPath, fileName, ".jpg");

            imageToSave.Save(fullImageLocation, ImageFormat.Jpeg);
            return fullImageLocation;
        }

        /// <summary>
        /// Method generates a filename to be used for an image
        /// </summary>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="captureDateTime"> Optional parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> The generated filename for the image.</returns>
        private string GenerateFileName(int cameraId, DateTime captureDateTime)
        {
            return $"{cameraId}_{captureDateTime.ToString("yyyy-MM-dd")}";
        }

        /// <summary>
        /// Generates a path to the location at which image, based on it's capture time, should be saved.
        /// </summary>
        /// <param name="captureDateTime"> Required parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> Creates directory name based on capture date.</returns>
        private string GenerateFolderLocationPath(DateTime captureDateTime)
        {
            var year = captureDateTime.Year.ToString();
            var month = captureDateTime.Month.ToString();
            var day = captureDateTime.Day.ToString();

            return Path.Combine(_pathToStorageLocation, year, month, day);
        }

        public int ExtractCameraId(FileInfo file)
        {
            var fileCamIdMatch = Regex.Match(file.Name, @"^(\d+)_.+");
            if (!fileCamIdMatch.Success)
            {
                throw new Exception("Bad file name");
            }

            int.TryParse(fileCamIdMatch.Groups[1].Value, out int camId);
            return camId;
        }

        public FileInfo[] GetTempFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(Config.FullTemporaryStoragePath);
            return dir.GetFiles();
        }
    }
}