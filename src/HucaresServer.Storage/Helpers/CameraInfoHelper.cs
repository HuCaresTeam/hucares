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
            if (!Uri.TryCreate(hostUrl, UriKind.Absolute, out Uri parsedUri))
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

        public CameraInfo UpdateCameraActivity(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public CameraInfo UpdateCameraSource(int id, string newHostUrl, bool newIsTrustedSource)
        {
            throw new NotImplementedException();
        }
    }
}
