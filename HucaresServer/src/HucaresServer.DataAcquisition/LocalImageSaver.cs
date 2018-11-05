using System;
using System.Drawing;

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
            throw new NotImplementedException();
        }

        public string GenerateStorageLocationAndFilename(int cameraId, DateTime? dateTime = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method generates a filename to be used for an image
        /// </summary>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="captureDateTime"> Optional parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> The generated filename for the image.</returns>
        private string GenerateFileName(int cameraId, DateTime? captureDateTime = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a path to the location at which image, based on it's capture time, should be saved.
        /// </summary>
        /// <param name="captureDateTime"> Optional parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> Path to the folder, where image should be saved.</returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GenerateFolderLocationPath(DateTime? captureDateTime = null)
        {
            throw new NotImplementedException();
        }
    }
}