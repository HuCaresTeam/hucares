using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    public interface ICameraInfoHelper
    {
        /// <summary>
        /// Inserts a camera record into the DB CameraInfo table.
        /// </summary>
        /// <param name="hostUrl"> Url for reaching the camera's footage </param>
        /// <param name="latitude"> Geographical latitude of the camera</param>
        /// <param name="longitude">Geographical longitude of the camera</param>
        /// <param name="isTrustedSource">Determines if the source is trust worthy. Default is false. </param>
        /// <returns> The stored CameraInfo instance </returns>
        CameraInfo InsertCamera(string hostUrl, double latitude, double longitude, bool isTrustedSource = false);

        /// <summary>
        /// Updates the camera record in the DB CameraInfo table. 
        /// </summary>
        /// <param name="id"> Id of the record in the DB CameraInfo table </param>
        /// <param name="newHostUrl">New host url for the camera</param>
        /// <param name="newIsTrustedSource">New trusted source value for the camera</param>
        /// <returns> The updated CameraInfo instance</returns>
        CameraInfo UpdateCameraSource(int id, string newHostUrl, bool newIsTrustedSource);

        /// <summary>
        /// Sets whether the camera is still used for processing (IsActive) or not.
        /// This is done because DLP records depend on camera locations.
        /// </summary>
        /// <param name="id"> Id of the record in the DB CameraInfo table </param>
        /// <param name="isActive"> Sets whether the camera is active or not </param>
        /// <returns> The updated CameraInfo instance </returns>
        CameraInfo UpdateCameraActivity(int id, bool isActive);

        /// <summary>
        /// Deletes the camera record from the DB CameraInfo table. 
        /// This should only succeed if no dependencies exist in DetectedLicensePlates DB table.
        /// </summary>
        /// <param name="id"> Id of the record in the DB CameraInfo table </param>
        /// <returns> The deleted CameraInfo instance </returns>
        CameraInfo DeleteCameraById(int id);

        /// <summary>
        /// Gets all camera records from the DB CameraInfoTable. Optionally, this request may be filtered by TrustedSource field.
        /// </summary>
        /// <param name="isTrustedSource">Optional paramater to filter by the TrustedSource field. Not filtered if null.</param>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        IEnumerable<CameraInfo> GetAllCameras(bool? isTrustedSource = null);

        /// <summary>
        /// Gets all active camera records from the DB CameraInfoTable. Optionally, this request may be filtered by TrustedSource field.
        /// </summary>
        /// <param name="isTrustedSource">Optional paramater to filter by the TrustedSource field. Not filtered if null.</param>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        IEnumerable<CameraInfo> GetActiveCameras(bool? isTrustedSource = null);

        /// <summary>
        /// Get all inactive camera records from the DB CameraInfoTable.
        /// </summary>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        IEnumerable<CameraInfo> GetInactiveCameras();
    }
}
