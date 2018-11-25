using System;
using System.Drawing;
using System.IO;

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

        /// <summary>
        /// Moves passed file to permanent storage directory
        /// </summary>
        /// <param name="file">File to move</param>
        /// <returns>New file location</returns>
        string MoveFileToPerm(FileInfo file);

        /// <summary>
        /// Extracts Camera ID from passed fileinfo name 
        /// </summary>
        /// <param name="file">Fileinfo to parse</param>
        /// <returns>Camera id</returns>
        int ExtractCameraId(FileInfo file);

        /// <summary>
        /// Returns all files in temp sotrage
        /// </summary>
        /// <returns>Returns all files in temp sotrage</returns>
        FileInfo[] GetTempFiles();
    }
}