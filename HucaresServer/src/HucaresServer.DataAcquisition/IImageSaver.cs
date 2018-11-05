using System;
using System.Drawing;

namespace HucaresServer.DataAcquisition
{
    public interface IImageSaver
    {
        /// <summary>
        /// Method saves the specified image
        /// </summary>
        /// <param name="imageToSave"> Bitmap of an image to save</param>
        void SaveImage(Bitmap imageToSave);

        /// <summary>
        /// Method generates path and filename where image should be saved
        /// </summary>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="dateTime"> Optional parameter of when the picture was taken. If not provided current datetime
        /// is used.</param>
        /// <returns> String of file location uri and filename.</returns>
        string GenerateStorageLocationAndFilename(int cameraId, DateTime? dateTime = null);
    }
}