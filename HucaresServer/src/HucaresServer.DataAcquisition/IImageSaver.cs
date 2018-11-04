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
        /// Method generates a filename to be used for an image
        /// </summary>
        /// <param name="cameraId"> Id of the camera that took the picture.</param>
        /// <param name="captureDateTime"> Optional parameter for the datetime of when the picture was taken. If not
        /// specified the current datetime will be used.</param>
        /// <returns> The generated filename for the image.</returns>
        string GenerateFileName(int cameraId, DateTime? captureDateTime = null);
    }
}