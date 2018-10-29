using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HucaresWF
{
    public static class PlateNumberValidation
    {
        public static bool ValidatePlateNumber(this string plateNumber)
        {
            var regex = new Regex(@"^([A-Z]){3}\d{3}$");
            return regex.IsMatch(plateNumber);
        }
    }
}
