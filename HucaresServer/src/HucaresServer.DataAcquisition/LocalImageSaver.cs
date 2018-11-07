using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HucaresServer.DataAcquisition
{
    //TODO seperate "Normal" and "Temp" image saver logic
    public class LocalImageSaver : IImageSaver
    {
        private readonly string _pathToStorageLocation;
        
        //TODO will be deprecated due to config file
        public LocalImageSaver(string pathToStorageLocation)
        {
            _pathToStorageLocation = pathToStorageLocation;
        }

        public void SaveImage(int cameraId, DateTime captureDateTime, Bitmap imageToSave)
        {
            var folderLocationPath = GenerateFolderLocationPath(captureDateTime);
            var fileName = GenerateFileName(cameraId, captureDateTime);
            var fullImageLocation = Path.Combine(folderLocationPath, fileName, ".jpg");
                        
            imageToSave.Save(fullImageLocation, ImageFormat.Jpeg);
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
            return string.Concat(cameraId, captureDateTime.ToString("yyyy-MM-dd"));
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
    }
}