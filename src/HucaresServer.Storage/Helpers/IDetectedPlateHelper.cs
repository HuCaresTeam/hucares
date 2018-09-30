using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    public interface IDetectedPlateHelper
    {
        
        /// <summary>
        /// Creates and inserts DetectedLicensePlate object into DetectedLicensePlate table in DB.
        /// </summary>
        /// <param name="PlateNumber"> The number plate that was detected. </param>
        /// <param name="DetectedDateTime"> The datetime of when the picture was taken, from which the plate was detected. </param>
        /// <param name="CamId"> The id of the camera, that took the picture. </param>
        /// <param name="ImgUrl"> Url to the image, from which number was detected. </param>
        /// <param name="Confidence"> Number from 0 to 1 (100%) that defines how confident the algorithm was when detecting the numberplate. </param>
        /// <returns> The inserted object instance. </returns>
        DetectedLicensePlate InsertNewDetectedPlate(string PlateNumber, DateTime DetectedDateTime, int CamId, 
            string ImgUrl, double Confidence);

        /// <summary>
        /// Deletes all detected plates from DB that are older than provided datetime.
        /// </summary>
        /// <param name="olderThanDatetime"> The DateTime before which all DetectedLicensePlates will be deleted </param>
        /// <returns> All deleted license plates. </returns>
        IEnumerable<DetectedLicensePlate> DeletePlatesOlderThanDatetime(DateTime olderThanDatetime);

        /// <summary>
        /// Gets all detected license plates from DB that exist in MissingLicensePlates table.
        /// </summary>
        /// <returns> All detected license plates. </returns>
        IEnumerable<DetectedLicensePlate> GetAllDetectedMissingPlates();

        /// <summary>
        /// Gets all detected plates that have the specified PlateNumber predicted
        /// </summary>
        /// <param name="plateNumber"> The string representation of the searched license plate number. </param>
        /// <param name="startDateTime"> Optional parameter, from what datetime to start querying detected license plates. </param>
        /// <param name="endDateTime"> Optional parameter, until what datetime to query detected license plates. </param>
        /// <returns> All license plates that meet the parameters. </returns>
        IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByPlateNumber(String plateNumber, 
            DateTime? startDateTime = null, DateTime? endDateTime = null);

        /// <summary>
        /// Gets all license plates that were detected by a specific camera.
        /// </summary>
        /// <param name="cameraId"> Id of the camera, which data is needed. </param>
        /// <param name="startDateTime"> Optional parameter, from what datetime to start querying detected license plates. </param>
        /// <param name="endDateTime"> Optional parameter, until what datetime to query detected license plates. </param>
        /// <returns> All detected license plates that were detected by that camera. </returns>
        IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByCamera(int cameraId, 
            DateTime? startDateTime = null, DateTime? endDateTime = null);

    }
}
