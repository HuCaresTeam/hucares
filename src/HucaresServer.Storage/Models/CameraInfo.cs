using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.Models
{
    public class CameraInfo
    {
        public int Id { get; set; }

        public string HostUrl { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool TrustedSource { get; set; }
    }
}
