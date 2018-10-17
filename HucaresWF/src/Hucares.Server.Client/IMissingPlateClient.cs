using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;

namespace Hucares.Server.Client
{
    public interface IMissingPlateClient
    {
        Task<MissingLicensePlate> InsertPlateRecord(string plateNumber, DateTime searchStartDatetime);
        Task<IEnumerable<MissingLicensePlate>> GetAllPlateRecords();
        Task<IEnumerable<MissingLicensePlate>> GetPlateRecordByPlateNumber(string plateNumber);
        Task<MissingLicensePlate> UpdatePlateRecord(int plateId, string plateNumber, DateTime searchStartDatetime);
        Task<MissingLicensePlate> MarkFoundPlate(int plateId, DateTime requestDateTime, bool isFound);
        Task<MissingLicensePlate> DeletePlateById(int plateId);
        Task<MissingLicensePlate> DeletePlateByNumber(string plateNumber);
    }
}