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
        
        private IDbContextFactory _dbContextFactory;

        public DetectedPlateHelper(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
        }
        
        /// <summary>
        /// Inserts DetectedLicensePlate object into the database.
        /// </summary>
        /// <param name="licensePlateToInsert"> DetectedLicensePlate object to insert. </param>
        /// <returns> Inserted object. </returns>
        public DetectedLicensePlate InsertNewDetectedPlate(DetectedLicensePlate licensePlateToInsert)
        {
            
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                ctx.DetectedLicensePlates.Add(licensePlateToInsert);
                ctx.SaveChanges();
            }

            return licensePlateToInsert;    
        }


        public IEnumerable<DetectedLicensePlate> DeletePlatesOlderThanDatetime(DateTime olderThanDatetime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlates()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByPlateNumber(String plateNumber,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByCamera(int cameraId,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            throw new NotImplementedException();
        }
    }
}
