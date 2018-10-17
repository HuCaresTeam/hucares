using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;

namespace Hucares.Server.Client
{
    public interface IDetectedPlateClient
    {
        Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedMissingPlates();
        Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByPlateNumber(string plateNumber,
            DateTime? startDateTime = null, DateTime? endDateTime = null);
        Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByCamera(int cameraId, DateTime? startDateTime = null, 
            DateTime? endDateTime = null);
    }
}