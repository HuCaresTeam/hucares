using System;

namespace Hucares.Server.Client.Models
{
    public class MissingLicensePlate : IEquatable<MissingLicensePlate>
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
        public DateTime SearchStartDateTime { get; set; }

        /// <summary>
        /// The DateTime of when the search concluded.
        /// </summary>
        /// /// <remarks>
        /// Saved as "datetime2" in SQL
        /// </remarks>
        public DateTime? SearchEndDateTime { get; set; }

        /// <summary>
        /// Indicates wheter the license plate was found once the search is complete.
        /// </summary>
        public LicensePlateFoundStatus Status { get; set; }
        
        public bool Equals(MissingLicensePlate other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (ReferenceEquals(null, other))
                return false;
            return (Id == other.Id
                    && PlateNumber == other.PlateNumber
                    && SearchStartDateTime == other.SearchStartDateTime
                    && SearchEndDateTime== other.SearchEndDateTime
                    && Status == other.Status);
        }
    }
}
