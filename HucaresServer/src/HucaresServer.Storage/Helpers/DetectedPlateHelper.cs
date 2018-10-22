using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HucaresServer.Storage.Models;
using HucaresServer.Storage.Properties;

namespace HucaresServer.Storage.Helpers
{
    public class DetectedPlateHelper : IDetectedPlateHelper
    {
        
        private IDbContextFactory _dbContextFactory;
        private IMissingPlateHelper _missingPlateHelper;

        public DetectedPlateHelper(IDbContextFactory dbContextFactory = null, IMissingPlateHelper missingPlateHelper = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
            _missingPlateHelper = missingPlateHelper ?? new MissingPlateHelper(dbContextFactory);
        }
        
        ///<inheritdoc/>
        public DetectedLicensePlate InsertNewDetectedPlate(string plateNumber, DateTime detectedDateTime, int camId, 
            string imgUrl, double confidence)
        {
            
            if (!Uri.IsWellFormedUriString(imgUrl, UriKind.Absolute))
            {
                throw new UriFormatException(string.Format(Resources.Error_AbsoluteUriInvalid, nameof(imgUrl)));
            }

            if (confidence < 0 || confidence > 1)
            {
                // It seems like I can't edit resource file on Rider
                throw new ArgumentException(string.Format($"The confidence parameter must be between 0 and 1, is: {confidence}"));
            }
            
            var detectedPlateToInsert = new DetectedLicensePlate()
            {
                PlateNumber = plateNumber,
                DetectedDateTime = detectedDateTime,
                CamId = camId,
                ImgUrl = imgUrl,
                Confidence = confidence
            };
            
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                ctx.DetectedLicensePlates.Add(detectedPlateToInsert);
                ctx.SaveChanges();
            }

            return detectedPlateToInsert;    
        }


        public IEnumerable<DetectedLicensePlate> DeletePlatesOlderThanDatetime(DateTime olderThanDatetime)
        {
            if (olderThanDatetime > DateTime.Today)
            {
                throw new ArgumentException(string.Format($"Provided datetime cannot be in the future ({olderThanDatetime})"));
            }

            var missingPlates = _missingPlateHelper.GetAllPlateRecords()
                .Select(s => s.PlateNumber);
            
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var platesToDelete = ctx.DetectedLicensePlates.Where(s => s.DetectedDateTime < olderThanDatetime &&
                                                                          !missingPlates.Contains(s.PlateNumber)).ToList();
                ctx.DetectedLicensePlates.RemoveRange(platesToDelete);
                ctx.SaveChanges();

                return platesToDelete;
            }
        }
        
        ///<inheritdoc/>
        public IEnumerable<DetectedLicensePlate> GetAllDetectedMissingPlates()
        {

            var missingPlateNumbers = _missingPlateHelper.GetAllPlateRecords()
                .Select(s => s.PlateNumber);
            
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                    .Select(s => s)
                    .Where(s => missingPlateNumbers.Contains(s.PlateNumber));

                return results.ToList();
            }
        }

        public IEnumerable<DetectedLicensePlate> GetAllActiveDetectedPlatesByPlateNumber(String plateNumber,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var missingPlateInfo = _missingPlateHelper.GetPlateRecordByPlateNumber(plateNumber)
                .FirstOrDefault(s => s.Status == LicensePlateFoundStatus.Searching);

            if (missingPlateInfo == null)
            {
                return new List<DetectedLicensePlate>();
            }

            var searchStartDateTime = startDateTime ?? missingPlateInfo.SearchStartDateTime;
            
            if (searchStartDateTime < missingPlateInfo.SearchStartDateTime)
            {
                searchStartDateTime = missingPlateInfo.SearchStartDateTime;
            }

            var searchEndDateTime = endDateTime ?? missingPlateInfo.SearchEndDateTime;

            if (searchEndDateTime > missingPlateInfo.SearchEndDateTime)
            {
                searchEndDateTime = missingPlateInfo.SearchEndDateTime;
            }

            if (searchEndDateTime != null && searchEndDateTime < searchStartDateTime)
            {
                // Need to move to Resources, but don't know if possible on Rider
                throw new ArgumentException("Search endDateTime cannot be before startDateTime");
            }

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                     .Where(s => s.PlateNumber == missingPlateInfo.PlateNumber 
                                 && s.DetectedDateTime >= searchStartDateTime);

                if (searchEndDateTime != null)
                {
                    results = results.Where(s => s.DetectedDateTime <= searchEndDateTime);
                }

                return results.ToList();
            }
            
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByCamera(int cameraId,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            if (startDateTime != null && endDateTime != null && endDateTime < startDateTime)
            {
                // Need to move to Resources, but don't know if possible on Rider
                throw new ArgumentException("Search endDateTime cannot be before startDateTime");
            }

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                    .Where(s => s.CamId == cameraId);

                if (startDateTime != null)
                {
                    results = results.Where(s => s.DetectedDateTime >= startDateTime);
                }

                if (endDateTime != null)
                {
                    results = results.Where(s => s.DetectedDateTime <= endDateTime);
                }

                return results.ToList();
            }
        }
    }
}
