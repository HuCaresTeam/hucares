using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HucaresServer.Storage.Models
{
    public class MissingLicensePlate
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SearchStartDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SearchEndDateTime { get; set; }

        public bool LicensePlateFound { get; set; }
    }
}
