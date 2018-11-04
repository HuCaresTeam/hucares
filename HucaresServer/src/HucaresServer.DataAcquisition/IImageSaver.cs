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
    }
}