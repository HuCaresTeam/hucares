﻿using System;
using System.Collections.Generic;
using System.Linq;
using HucaresServer.Storage.Models;
using HucaresServer.Storage.Properties;
using HucaresServer.Utils;

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
            if (!plateNumber.IsValidPlateNumber())
                throw new ArgumentException(Resources.Error_PlateNumberFomatInvalid);

            if (confidence < 0 || confidence > 100)
            {
                // It seems like I can't edit resource file on Rider
                throw new ArgumentException(string.Format($"The confidence parameter must be between 0 and 100, is: {confidence}"));
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
        public IEnumerable<DetectedLicensePlate> GetAllDlps()
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                    .Select(s => s);

                return results.ToList();
            }
        }

        public IEnumerable<DetectedLicensePlate> GetAllDetectedMissingPlates(int? page = null)
        {
            var missingPlateNumbers = _missingPlateHelper.GetAllPlateRecords()
                .Select(s => s.PlateNumber);

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var orderedResults = ctx.DetectedLicensePlates
                    .Select(s => s)
                    .Where(s => missingPlateNumbers.Contains(s.PlateNumber))
                    .OrderByDescending(s => s.DetectedDateTime);

                if (null != page && 0 != page)
                {
                    var itemsPerPage = Config.ItemsPerPage;
                    var skipItemCount = (page.Value - 1) * itemsPerPage;
                    var pagedResults = orderedResults
                        .Skip(skipItemCount)
                        .Take(itemsPerPage);

                    return pagedResults.ToList();
                }

                return orderedResults.ToList();
            }
        }

        public IEnumerable<DetectedLicensePlate> GetAllActiveDetectedPlatesByPlateNumber(string plateNumber,
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            if (!plateNumber.IsValidPlateNumber())
                throw new ArgumentException(Resources.Error_PlateNumberFomatInvalid);

            var missingPlateInfo = _missingPlateHelper.GetPlateRecordByPlateNumber(plateNumber)
                .FirstOrDefault(delegate (MissingLicensePlate plate)
                {
                    return plate.Status == LicensePlateFoundStatus.Searching;
                });

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

        public IEnumerable<DetectedLicensePlate> GetAllActiveDetectedPlatesByPlateNumberAndCameraId(string plateNumber, int cameraId)
        {
            if (!plateNumber.IsValidPlateNumber())
                throw new ArgumentException(Resources.Error_PlateNumberFomatInvalid);

            var missingPlateInfo = _missingPlateHelper.GetPlateRecordByPlateNumber(plateNumber)
                .FirstOrDefault(delegate (MissingLicensePlate plate)
                {
                    return plate.Status == LicensePlateFoundStatus.Searching;
                });

            if (missingPlateInfo == null)
            {
                return new List<DetectedLicensePlate>();
            }

            var searchStartDateTime = missingPlateInfo.SearchStartDateTime;
            var searchEndDateTime = missingPlateInfo.SearchEndDateTime;

            if (searchEndDateTime != null && searchEndDateTime < searchStartDateTime)
            {
                // Need to move to Resources, but don't know if possible on Rider
                throw new ArgumentException("Search endDateTime cannot be before startDateTime");
            }

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                     .Where(s => s.PlateNumber == missingPlateInfo.PlateNumber)
                     .Where(s => s.DetectedDateTime >= searchStartDateTime)
                     .Where(s => s.CamId == cameraId);

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

        public IEnumerable<DetectedLicensePlate> GetDetectedPlatesByImgUrl(string imgUrl)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var results = ctx.DetectedLicensePlates
                    .Where(s => s.ImgUrl == imgUrl);

                return results.ToList();
            }
        }

        public void DeleteAll()
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordsToDelete = ctx.DetectedLicensePlates.Select(c => c);
                ctx.DetectedLicensePlates.RemoveRange(recordsToDelete);
                ctx.SaveChanges();
            }
        }
    }
}
