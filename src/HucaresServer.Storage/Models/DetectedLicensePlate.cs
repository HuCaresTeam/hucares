using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Models
{
    public class DetectedLicensePlate
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DetectedDateTime { get; set; }

        public int CamId { get; set; }

        public string ImgUrl { get; set; }

        public double Confidence { get; set; }
    }
}
