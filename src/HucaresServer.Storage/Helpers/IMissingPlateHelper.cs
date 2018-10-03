using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    public interface IMissingPlateHelper
    {
        /// <summary>
        /// Insert new record
        /// </summary>
        /// <param name="plateNumber">The string representation of the searched license plate number.</param>
        /// <param name="searchStartDatetime">Optional parameter, from what datetime to start querying detected license plates</param>
        /// <returns> The stored MissingLicensePlate instance </returns>
        MissingLicensePlate InsertPlateRecord(string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// Get all records from DB
        /// </summary>
        /// <returns> The stored MissingLicensePlate instance </returns>
        IEnumerable<MissingLicensePlate> GetAllPlateRecords();

        /// <summary>
        /// Get all information about plate by passing plate number
        /// </summary>
        /// <param name="plateNumber">The string representation of the searched license plate number.</param>
        /// <returns>The stored MissingLicensePlate instance</returns>
        IEnumerable<MissingLicensePlate> GetPlateRecordByPlateNumber(string plateNumber);
        
        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="plateId">Plate id in the DB</param>
        /// <param name="plateNumber">The string representation of the searched license plate number.</param>
        /// <param name="searchStartDatetime">Optional parameter, from what datetime to start querying detected license plates</param>
        /// <returns> The stored MissingLicensePlate instance </returns>
        MissingLicensePlate UpdatePlateRecord(int plateId, string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// If plate was found, mark as found plate
        /// </summary>
        /// <param name="plateId">Plate id in the DB</param>
        /// <param name="requestDateTime">Optional parameter, from what datetime to start querying detected license plates</param>
        /// <param name="isFound">True/false parameter of missing license plate</param>
        /// <returns> The stored MissingLicensePlate instance </returns>
        MissingLicensePlate MarkFoundPlate(int plateId, DateTime requestDateTime, bool isFound);

        /// <summary>
        /// Delete plate from DB by ID
        /// </summary>
        /// <param name="plateId">Plate id in the DB</param>
        /// <returns> The stored MissingLicensePlate instance </returns>
        MissingLicensePlate DeletePlateById(int plateId);

        /// <summary>
        /// Delete plate from DB by Number
        /// </summary>
        /// <param name="plateNumber">Plate number in the DB</param>
        /// <returns> The stored MissingLicensePlate instance </returns>
        MissingLicensePlate DeletePlateByNumber(string plateNumber);
    }
}
