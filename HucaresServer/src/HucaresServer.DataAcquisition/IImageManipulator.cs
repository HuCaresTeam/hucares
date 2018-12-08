using System;
using System.Collections.Generic;
using System.IO;

namespace HucaresServer.DataAcquisition
{
    public interface IImageManipulator
    {
        /// <summary>
        /// Method saves the specified image
        /// </summary>
        /// <param name="cameraId"> Id of a camera</param>
        /// <param name="captureDateTime">DateTime when the photo was taken</param>
        /// <param name="imageToSave"> Bitmap of an image to save</param>
        string SaveImage(int cameraId, DateTime captureDateTime, byte[] imgToSaveBytes);

        /// <summary>
        /// Moves passed file to permanent storage directory
        /// </summary>
        /// <param name="file">File to move</param>
        /// <returns>New file location</returns>
        string MoveFileToPerm(FileSystemInfo file, DateTime captureDateTime);

        /// <summary>
        /// Returns all files in temp sotrage
        /// </summary>
        /// <returns>Returns all files in temp sotrage</returns>
        IEnumerable<FileSystemInfo> GetTempFiles();

        /// <summary>
        /// Deletes all temp folder files
        /// </summary>
        void DeleteTempFiles();

        string GenerateFolderLocationPath(DateTime captureDateTime);
    }
}