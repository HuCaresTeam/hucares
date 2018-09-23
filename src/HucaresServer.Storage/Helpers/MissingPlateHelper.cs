using System;
using System.Collections.Generic;
using System.Linq;
using HucaresServer.Storage.Models;
using HucaresServer.Storage.Properties;

namespace HucaresServer.Storage.Helpers
{
    public class MissingPlateHelper : IMissingPlateHelper
    {
        private IDbContextFactory _dbContextFactory;

        public MissingPlateHelper(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory ?? new DbContextFactory();
        }

        /// <inheritdoc />
        public MissingLicensePlate InsertPlateRecord(string plateNumber, DateTime searchStartDatetime)
        {
            var missingPlateObj = new MissingLicensePlate()
            {
                PlateNumber = plateNumber,
                SearchStartDateTime = searchStartDatetime,
               
            };

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                ctx.MissingLicensePlates.Add(missingPlateObj);
                ctx.SaveChanges();
            }

            return missingPlateObj;
        }

        /// <inheritdoc />
        public IEnumerable<MissingLicensePlate> GetAllPlateRecords()
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var query = ctx.MissingLicensePlates.Select(c => c);
                
                return query.ToList();
            }
        }

        /// <inheritdoc />
        public MissingLicensePlate UpdatePlateRecord(int plateId, string plateNumber, DateTime searchStartDatetime)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                recordToUpdate.PlateNumber = plateNumber;
                recordToUpdate.SearchStartDateTime = searchStartDatetime;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }

        /// <inheritdoc />
        public MissingLicensePlate MarkFoundPlate(int plateId, DateTime requestDateTime)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                recordToUpdate.LicensePlateFound = true;
                recordToUpdate.SearchStartDateTime = requestDateTime;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }

        /// <inheritdoc />
        public MissingLicensePlate MarkNotFoundPlate(int plateId, DateTime requestDateTime)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                recordToUpdate.LicensePlateFound = false;
                recordToUpdate.SearchStartDateTime = requestDateTime;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }

        /// <inheritdoc />
        public MissingLicensePlate DeletePlateById(int plateId)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToDelete = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                ctx.MissingLicensePlates.Attach(recordToDelete);
                ctx.MissingLicensePlates.Remove(recordToDelete);
                ctx.SaveChanges();

                return recordToDelete;
            }
        }

        /// <inheritdoc />
        public MissingLicensePlate DeletePlateByNumber(string plateNumber)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToDelete = ctx.MissingLicensePlates.FirstOrDefault(c => c.PlateNumber == plateNumber) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateNumber));

                ctx.MissingLicensePlates.Attach(recordToDelete);
                ctx.MissingLicensePlates.Remove(recordToDelete);
                ctx.SaveChanges();

                return recordToDelete;
            }
        }
    }
}
