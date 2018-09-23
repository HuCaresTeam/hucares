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
        void InsertRecord(string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// Get all records from DB
        /// </summary>
        /// <returns> void </returns>
        IEnumerable<MissingLicensePlate> GetAllRecords();

        /// <summary>
        /// Update record
        /// </summary>
        /// <returns> void </returns>
        void UpdateRecord(int plateId, string plateNumber, DateTime searchStartDatetime);

        /// <summary>
        /// If plate was found, mark as found plate
        /// </summary>
        /// <returns> bool </returns>
        bool MarkFoundPlate(int plateId, DateTime requestDateTime);

        /// <summary>
        /// If plate was not found, mark as not found plate
        /// </summary>
        /// <returns> bool </returns>
        bool MarkNotFoundPlate(int plateId, DateTime requestDateTime);

        /// <summary>
        /// Delete plate from DB by ID
        /// </summary>
        /// <returns> void </returns>
        void DeleteById(int plateId);
    }
}
