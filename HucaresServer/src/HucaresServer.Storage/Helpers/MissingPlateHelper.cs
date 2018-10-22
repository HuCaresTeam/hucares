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

        public MissingLicensePlate InsertPlateRecord(string plateNumber, DateTime searchStartDatetime)
        {
            var missingPlateObj = new MissingLicensePlate()
            {
                PlateNumber = plateNumber,
                SearchStartDateTime = searchStartDatetime,
                Status = LicensePlateFoundStatus.Searching
               
            };

            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                ctx.MissingLicensePlates.Add(missingPlateObj);
                ctx.SaveChanges();
            }

            return missingPlateObj;
        }

        public IEnumerable<MissingLicensePlate> GetAllPlateRecords()
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var query = ctx.MissingLicensePlates.Select(c => c);
                
                return query.ToList();
            }
        }

        public IEnumerable<MissingLicensePlate> GetPlateRecordByPlateNumber(string plateNumber)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                return ctx.MissingLicensePlates.Where(c => c.PlateNumber == plateNumber).ToList();
            }
        }

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

        public MissingLicensePlate MarkFoundPlate(int plateId, DateTime requestDateTime, LicensePlateFoundStatus status)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToUpdate = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                recordToUpdate.Status = status;
                recordToUpdate.SearchEndDateTime = requestDateTime;
                ctx.SaveChanges();

                return recordToUpdate;
            }
        }
        
        public MissingLicensePlate DeletePlateById(int plateId)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToDelete = ctx.MissingLicensePlates.FirstOrDefault(c => c.Id == plateId) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateId));

                ctx.MissingLicensePlates.Remove(recordToDelete);
                ctx.SaveChanges();

                return recordToDelete;
            }
        }

        public MissingLicensePlate DeletePlateByNumber(string plateNumber)
        {
            using (var ctx = _dbContextFactory.BuildHucaresContext())
            {
                var recordToDelete = ctx.MissingLicensePlates.FirstOrDefault(c => c.PlateNumber == plateNumber) ?? 
                                     throw new ArgumentException(string.Format(Resources.Error_BadIdProvided, plateNumber));

                ctx.MissingLicensePlates.Remove(recordToDelete);
                ctx.SaveChanges();

                return recordToDelete;
            }
        }
    }
}
