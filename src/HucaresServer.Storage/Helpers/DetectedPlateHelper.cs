using System;
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

        public DetectedPlateHelper(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
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
        public IEnumerable<DetectedLicensePlate> GetAllDetectedPlates()
        {
            
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates.Select(s => s);

                return results.ToList();
            }
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
