using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace HucaresServer.DataAcquisition
{
    public class LocalImageSaver : IImageSaver
    {
        private string _pathToStorageLocation;
        
        public LocalImageSaver(string pathToStorageLocation = null)
        {
            _pathToStorageLocation = pathToStorageLocation ?? "";
        }

        public void SaveImage(Bitmap imageToSave)
        {
            var folderLocationPath = GenerateFolderLocationPath();
            var fileName = GenerateFileName();
            var fullImageLocation = folderLocationPath + fileName + ".jpeg";
                        
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
            return string.Concat(cameraId, captureDateTime.ToString("yy-MM-dd"));
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
            
            return _pathToStorageLocation + "\\" + year + "\\" + month + "\\" + day + "\\";
        }
    }
}