using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class StringDateTimeInterchanger
    {
        private const string ISO_Format = "yyyy-MM-dd HH:mm:ss";

        public static DateTime ToIsoDateTime(this string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString.ToString(),
                                       ISO_Format,
                                       CultureInfo.InvariantCulture);
        }

        public static string ToIsoDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(ISO_Format);
        }
    }
}
