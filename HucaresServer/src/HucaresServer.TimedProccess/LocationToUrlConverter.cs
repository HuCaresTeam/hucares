using HucaresServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.TimedProccess
{
    public class LocationToUrlConverter : ILocationToUrlConverter
    {
        public string ConvertPathToUrl(string fileName, DateTime currentTime)
        {
            var hostUri = new Uri(Config.HostAddress);
            var relativeString = $"api/images/{currentTime.Date}/{fileName}";
            return new Uri(hostUri, relativeString).ToString();
        }
    }
}
