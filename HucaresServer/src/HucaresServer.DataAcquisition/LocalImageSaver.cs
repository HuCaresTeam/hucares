using HucaresServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HucaresServer.DataAcquisition
{
    //TODO seperate "Normal" and "Temp" image saver logic
    public class LocalImageSaver : IImageSaver
    {
        public string MoveFileToPerm(FileSystemInfo file, DateTime captureDateTime)
        {
            var newFilePath = GenerateFolderLocationPath(captureDateTime);
            EnsureFolder(newFilePath);
            var newFileLocation = Path.Combine(newFilePath, file.Name);
            File.Move(file.FullName, newFileLocation);

            return newFilePath;
        }

        public string SaveImage(int cameraId, DateTime captureDateTime, MemoryStream imageToSave)
        {
            var fileName = GenerateFileName(cameraId, captureDateTime);
            var fullImageLocation = Path.Combine(Config.TemporaryStorage, fileName + ".jpg");

            EnsureFolder(Config.TemporaryStorage);
            using (FileStream fs = new FileStream(fullImageLocation, FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] bytes = imageToSave.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
            return fullImageLocation;
        }

        private void EnsureFolder(string path)
        {
            if ((path.Length > 0) && (!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
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
            return $"{cameraId}_{captureDateTime.ToString("yyyy-MM-dd-HH-mm-ss")}_{Guid.NewGuid().ToString()}";
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

            return Path.Combine(Config.PermStorage, year, month, day);
        }

        public int ExtractCameraId(FileSystemInfo file)
        {
            var fileCamIdMatch = Regex.Match(file.Name, @"^(\d+)_.+");
            if (!fileCamIdMatch.Success)
            {
                throw new Exception("Bad file name");
            }

            int.TryParse(fileCamIdMatch.Groups[1].Value, out int camId);
            return camId;
        }

        public IEnumerable<FileSystemInfo> GetTempFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(Config.TemporaryStorage);
            return dir.GetFiles("*");
        }

        public void DeleteTempFiles()
        {
            foreach (var file in GetTempFiles())
            {
                File.Delete(file.FullName);
            }
        }
    }
}