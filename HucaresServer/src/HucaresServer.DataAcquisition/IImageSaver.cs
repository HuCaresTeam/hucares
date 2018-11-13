using System;
using System.Drawing;

namespace HucaresServer.DataAcquisition
{
    public interface IImageSaver
    {
        /// <summary>
        /// Method saves the specified image
        /// </summary>
        /// <param name="cameraId"> Id of a camera</param>
        /// <param name="captureDateTime">DateTime when the photo was taken</param>
        /// <param name="imageToSave"> Bitmap of an image to save</param>
        string SaveImage(int cameraId, DateTime captureDateTime, Image imageToSave);
    }
}