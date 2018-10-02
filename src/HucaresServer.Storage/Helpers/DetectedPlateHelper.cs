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
            throw new NotImplementedException();
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
                .First(s => !(s.LicensePlateFound ?? false));

            if (missingPlateInfo == null)
            {
                return new List<DetectedLicensePlate>();
            }

            var searchStartDateTime = startDateTime ?? missingPlateInfo.SearchStartDateTime;
            
            if (searchStartDateTime < missingPlateInfo.SearchStartDateTime)
            {
                searchStartDateTime = missingPlateInfo.SearchStartDateTime;
            }

            if (endDateTime != null && endDateTime < searchStartDateTime)
            {
                // Need to move to Resources, but don't know if possible on Rider
                throw new ArgumentException("Search endDateTime cannot be before startDateTime");
            }

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                List<DetectedLicensePlate> results;
                
                if (endDateTime == null)
                {
                    results = ctx.DetectedLicensePlates
                        .Where(s => s.PlateNumber == missingPlateInfo.PlateNumber
                                    && s.DetectedDateTime >= searchStartDateTime)
                        .Select(s => s)
                        .ToList();

                }
                else
                {
                    results = ctx.DetectedLicensePlates
                        .Where(s => s.PlateNumber == missingPlateInfo.PlateNumber
                                    && s.DetectedDateTime >= searchStartDateTime
                                    && s.DetectedDateTime <= endDateTime)
                        .Select(s => s)
                        .ToList();  
                }

                return results;
            }
            
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlatesByCamera(int cameraId,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            throw new NotImplementedException();
        }
    }
}
