using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HucaresServer.Storage.Models
{
    public class MissingLicensePlate
    {
        /// <summary>
        /// The primary key of MissingLicensePlates table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The string representation of the missing license plate number.
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// The approximate DateTime of when the license plate went missing.
        /// This will be used to filter out appropriate records.
        /// </summary>
        /// <remarks>
        /// Saved as "datetime2" in SQL
        /// </remarks>
        [Column(TypeName = "datetime2")]
        public DateTime SearchStartDateTime { get; set; }

        /// <summary>
        /// The DateTime of when the search concluded.
        /// </summary>
        /// /// <remarks>
        /// Saved as "datetime2" in SQL
        /// </remarks>
        [Column(TypeName = "datetime2")]
        public DateTime? SearchEndDateTime { get; set; }

        /// <summary>
        /// Indicates wheter the license plate was found once the search is complete.
        /// </summary>
        public bool? LicensePlateFound { get; set; }
    }
}
