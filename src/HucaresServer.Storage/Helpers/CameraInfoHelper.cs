﻿using System;
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
        private IDetectedPlateHelper _detectedPlateHelper;

        public CameraInfoHelper(IDbContextFactory dbContextFactory = null, IDetectedPlateHelper detectedPlateHelper = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
            _detectedPlateHelper = detectedPlateHelper ?? new DetectedPlateHelper();
        }

        public CameraInfo DeleteCameraById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all active camera records from the DB CameraInfoTable. Optionally, this request may be filtered by TrustedSource field.
        /// </summary>
        /// <param name="isTrustedSource">Optional paramater to filter by the TrustedSource field. Not filtered if null.</param>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        public IEnumerable<CameraInfo> GetActiveCameras(bool? isTrustedSource = null)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var query = ctx.CameraInfo.Where(c => c.IsActive);
                if (null != isTrustedSource)
                {
                    query = query.Where(c => c.IsTrustedSource == isTrustedSource);
                }
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets all camera records from the DB CameraInfoTable. Optionally, this request may be filtered by TrustedSource field.
        /// </summary>
        /// <param name="isTrustedSource">Optional paramater to filter by the TrustedSource field. Not filtered if null.</param>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        public IEnumerable<CameraInfo> GetAllCameras(bool? isTrustedSource = null)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var query = ctx.CameraInfo.Select(c => c);
                if (null != isTrustedSource)
                {
                    query = query.Where(c => c.IsTrustedSource == isTrustedSource);
                }
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets a camera instance by id from DB CameraInfo table.
        /// </summary>
        /// <param name="id">Id of the record in the DB CameraInfo table</param>
        /// <returns>CameraInfo instance matching the queried record</returns>
        public CameraInfo GetCameraById(int id)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                return ctx.CameraInfo.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all inactive camera records from the DB CameraInfoTable.
        /// </summary>
        /// <returns>IEnumerable of CameraInfo from the query result.</returns>
        public IEnumerable<CameraInfo> GetInactiveCameras()
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                return ctx.CameraInfo.Where(c => !c.IsActive).ToList();
            }
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

        /// <summary>
        /// Updates the camera record in the DB CameraInfo table. 
        /// </summary>
        /// <param name="id"> Id of the record in the DB CameraInfo table </param>
        /// <param name="newHostUrl">New host url for the camera</param>
        /// <param name="newIsTrustedSource">New trusted source value for the camera</param>
        /// <returns> The updated CameraInfo instance</returns>
        public CameraInfo UpdateCameraSource(int id, string newHostUrl, bool newIsTrustedSource)
        {
            if (!Uri.IsWellFormedUriString(newHostUrl, UriKind.Absolute))
            {
                throw new UriFormatException(string.Format(Resources.Error_AbsoluteUriInvalid, nameof(newHostUrl)));
            }

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.CameraInfo.Where(c => c.Id == id).FirstOrDefault() ??
                    throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, id));

                recordToUpdate.HostUrl = newHostUrl;
                recordToUpdate.IsTrustedSource = newIsTrustedSource;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }
    }
}
