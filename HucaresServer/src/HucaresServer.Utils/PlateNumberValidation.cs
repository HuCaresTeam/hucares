using System.Text.RegularExpressions;

namespace HucaresServer.Utils
{
    public static class PlateNumberValidation
    {
        public static bool IsValidPlateNumber(this string str)
        {
            var rx = new Regex(@"^([A-Z]){3}\d{3}$");
            return rx.IsMatch(str);
        }
    }
}
