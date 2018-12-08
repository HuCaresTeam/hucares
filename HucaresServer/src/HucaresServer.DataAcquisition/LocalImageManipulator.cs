using HucaresServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HucaresServer.DataAcquisition
{
    //TODO seperate "Normal" and "Temp" image saver logic
    public class LocalImageManipulator : IImageManipulator
    {
        private IImageFileNamer _fileNamer;

        public LocalImageManipulator(IImageFileNamer fileNamer = null)
        {
            _fileNamer = fileNamer ?? new ImageFileNamer();
        }

        public string MoveFileToPerm(FileSystemInfo file, DateTime captureDateTime)
        {
            var newFilePath = GenerateFolderLocationPath(captureDateTime);
            EnsureFolder(newFilePath);
            var newFileLocation = Path.Combine(newFilePath, file.Name);
            File.Move(file.FullName, newFileLocation);

            return newFileLocation;
        }

        public string SaveImage(int cameraId, DateTime captureDateTime, byte[] imgToSaveBytes)
        {
            var fileName = _fileNamer.GenerateFileName(cameraId, captureDateTime);
            var fullImageLocation = Path.Combine(Config.TemporaryStorage, fileName + ".jpg");

            EnsureFolder(Config.TemporaryStorage);
            using (FileStream fs = new FileStream(fullImageLocation, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(imgToSaveBytes, 0, imgToSaveBytes.Length);
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
        /// Generates a path to the location at which image, based on it's capture time, should be saved.
        /// </summary>
        /// <param name="captureDateTime"> Required parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> Creates directory name based on capture date.</returns>
        public string GenerateFolderLocationPath(DateTime captureDateTime)
        {
            var year = captureDateTime.Year.ToString();
            var month = captureDateTime.Month.ToString();
            var day = captureDateTime.Day.ToString();

            return Path.Combine(Config.PermStorage, year, month, day);
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