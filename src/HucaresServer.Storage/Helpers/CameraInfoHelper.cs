using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HucaresServer.Storage.Models;
using HucaresServer.Storage.Properties;

namespace HucaresServer.Storage.Helpers
{
    public class CameraInfoHelper : ICameraInfoHelper
    {
        private IDbContextFactory _dbContextFactory;

        public CameraInfoHelper(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
        }

        public CameraInfo DeleteCameraById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CameraInfo> GetActiveCameras(bool? isTrustedSource = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CameraInfo> GetAllCameras(bool? isTrustedSource = null)
        {
            throw new NotImplementedException();
        }

        public CameraInfo GetCameraById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CameraInfo> GetInactiveCameras()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts a camera record into the DB CameraInfo table.
        /// </summary>
        /// <param name="hostUrl"> Url for reaching the camera's footage. Must be valid Uri. </param>
        /// <param name="latitude"> Geographical latitude of the camera</param>
        /// <param name="longitude">Geographical longitude of the camera</param>
        /// <param name="isTrustedSource">Determines if the source is trust worthy. Default is false. </param>
        /// <returns> The stored CameraInfo instance </returns>
        public CameraInfo InsertCamera(string hostUrl, double latitude, double longitude, bool isTrustedSource = false)
        {
            if (!Uri.IsWellFormedUriString(hostUrl, UriKind.Absolute))
            {
                throw new UriFormatException(string.Format(Resources.Error_AbsoluteUriInvalid, nameof(hostUrl)));
            }

            var camInfoObj = new CameraInfo()
            {
                HostUrl = hostUrl,
                Latitude = latitude,
                Longitude = longitude,
                IsTrustedSource = isTrustedSource
            };

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                ctx.CameraInfo.Add(camInfoObj);
                ctx.SaveChanges();
            }

            return camInfoObj;
        }

        /// <summary>
        /// Sets whether the camera is still used for processing (IsActive) or not.
        /// This is done because DLP records depend on camera locations.
        /// </summary>
        /// <param name="id"> Id of the record in the DB CameraInfo table </param>
        /// <param name="isActive"> Sets whether the camera is active or not </param>
        /// <returns> The updated CameraInfo instance </returns>
        public CameraInfo UpdateCameraActivity(int id, bool isActive)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.CameraInfo.Where(c => c.Id == id).FirstOrDefault() ?? 
                    throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, id));

                recordToUpdate.IsActive = isActive;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }

        public CameraInfo UpdateCameraSource(int id, string newHostUrl, bool newIsTrustedSource)
        {
            throw new NotImplementedException();
        }
    }
}
