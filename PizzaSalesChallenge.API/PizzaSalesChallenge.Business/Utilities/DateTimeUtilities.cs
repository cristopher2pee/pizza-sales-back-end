using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Utilities
{
    public static class DateTimeUtilities
    {
        public static DateTime ConvertRecordToDateTime(DateTime date, DateTime time)
        {

            DateTime combinedDateTime = new DateTime
                    (date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            return combinedDateTime;
        }
    }
}
