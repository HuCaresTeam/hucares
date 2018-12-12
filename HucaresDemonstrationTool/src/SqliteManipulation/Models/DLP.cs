using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManipulation.Models
{
    public class DLP
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public string DetectedDateTime { get; set; }
        public long CamId { get; set; }
        public byte[] Img { get; set; }
        public double Confidence { get; set; }
    }
}
