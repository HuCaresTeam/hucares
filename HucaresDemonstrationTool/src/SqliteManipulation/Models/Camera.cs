using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManipulation.Models
{
    public class Camera
    {
        public long Id { get; set; }
        public string HostUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsTrustedSource { get; set; } = true;
    }
}
