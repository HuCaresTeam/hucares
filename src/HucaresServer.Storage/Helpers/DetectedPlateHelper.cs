using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HucaresServer.Storage.Models;

namespace HucaresServer.Storage.Helpers
{
    public class DetectedPlateHelper : IDetectedPlateHelper
    {
        public IEnumerable<DetectedLicensePlate> DeletePlatesOlderThanDatetime(DateTime olderThanDatetime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlates()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByCamera(int cameraId, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByPlateNumber(string plateNumber, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            throw new NotImplementedException();
        }

        public DetectedLicensePlate InsertNewDetectedPlate(DetectedLicensePlate licensePlateToInsert)
        {
            throw new NotImplementedException();
        }
    }
}
