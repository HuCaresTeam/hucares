using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HucaresServer.Utils;

namespace HucaresServer.Utils
{
    public static class PlateNumberValidation
    {
        public static bool IsValidPlateNumber(this string str)
        {
            var rx = new Regex(@"^\w{3}\d{3}$");
            return rx.IsMatch(str);
        }
    }
}
