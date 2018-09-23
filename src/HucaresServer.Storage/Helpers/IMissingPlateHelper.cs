using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Helpers
{
    interface IMissingPlateHelper
    {
        /// <summary>
        /// Insert new record
        /// </summary>
        /// <returns> bool </returns>
        bool InsertRecord()
        {

        }

        /// <summary>
        /// Get all records from DB
        /// </summary>
        /// <returns> void </returns>
        void GetAllRecords()
        {

        }

        /// <summary>
        /// Update record
        /// </summary>
        /// <returns> bool </returns>
        bool UpdateRecord()
        {

        }

        /// <summary>
        /// If plate was found, mark as found plate
        /// </summary>
        /// <returns> bool </returns>
        bool MarkFoundPlate()
        {

        }

        /// <summary>
        /// Delete plate from DB by ID
        /// </summary>
        /// <returns> void </returns>
        void DeleteById()
        {

        }
    }
}
