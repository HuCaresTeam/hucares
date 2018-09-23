using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    interface IMissingPlateHelper
    {
        /// <summary>
        /// Insert new record
        /// </summary>
        /// <returns> void </returns>
        /// <param name="plateNumber">The string representation of the searched license plate number.</param>
        /// <param name="searchStartDatetime">Optional parameter, from what datetime to start querying detected license plates</param>
        MissingLicensePlate InsertPlateRecord(string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// Get all records from DB
        /// </summary>
        /// <returns> All records from DB. </returns>
        IEnumerable<MissingLicensePlate> GetAllPlateRecords();

        /// <summary>
        /// Update record
        /// </summary>
        /// <returns> MissingLicensePlate </returns>
        /// <param name="plateId">Plate id in the DB</param>
        /// <param name="plateNumber">The string representation of the searched license plate number.</param>
        /// <param name="searchStartDatetime">Optional parameter, from what datetime to start querying detected license plates</param>
        MissingLicensePlate UpdatePlateRecord(int plateId, string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// If plate was found, mark as found plate
        /// </summary>
        /// <returns> MissingLicensePlate </returns>
        /// <param name="plateId">Plate id in the DB</param>
        /// <param name="requestDateTime">Optional parameter, from what datetime to start querying detected license plates</param>
        MissingLicensePlate MarkFoundPlate(int plateId, DateTime requestDateTime);

        /// <summary>
        /// If plate was not found, mark as not found plate
        /// </summary>
        /// <returns> MissingLicensePlate </returns>
        /// <param name="plateId">Plate id in the DB</param>
        /// <param name="requestDateTime">Optional parameter, from what datetime to start querying detected license plates</param>
        MissingLicensePlate MarkNotFoundPlate(int plateId, DateTime requestDateTime);

        /// <summary>
        /// Delete plate from DB by ID
        /// </summary>
        /// <returns> MissingLicensePlate </returns>
        /// <param name="plateId">Plate id in the DB</param>
        MissingLicensePlate DeletePlateById(int plateId);

        /// <summary>
        /// Delete plate from DB by Number
        /// </summary>
        /// <returns> MissingLicensePlate </returns>
        /// <param name="plateNumber">Plate number in the DB</param>
        MissingLicensePlate DeletePlateByNumber(string plateNumber);
    }
}
