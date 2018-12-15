using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HucaresServer.Models
{
    public class DemonstrationDlpInput
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        public DateTime DetectedDateTime { get; set; }

        public int CamId { get; set; }

        public byte[] Img { get; set; }

        public double Confidence { get; set; }
    }
}