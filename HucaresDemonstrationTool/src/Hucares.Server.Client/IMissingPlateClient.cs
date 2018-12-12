using System;
using System.Threading.Tasks;
using SqliteManipulation.Models;

namespace Hucares.Server.Client
{
    public interface IMissingPlateClient
    {
        Task<MLP> InsertPlateRecord(string plateNumber, string searchStartDatetime);
        Task DeleteAllMLPs();
    }
}