using System;
using System.Drawing;

namespace HucaresServer.DataAcquisition
{
    public interface IImageSaver
    {
        /// <summary>
        /// Method saves the specified image
        /// </summary>
        /// <param name="imageToSave"> Bitmap of an image to save.</param>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="captureDateTime"> Datetime when the picture was taken.</param>
        void SaveImage(Bitmap imageToSave, int cameraId, DateTime? captureDateTime);
    }
}