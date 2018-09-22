using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
