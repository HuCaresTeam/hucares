using HucaresServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
            var fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            var relativeString = $"api/images/{currentTime.Date.ToString("yyyy-MM-dd")}/{fileNameNoExtension}";
            return new Uri(hostUri, relativeString).ToString();
        }
    }
}
