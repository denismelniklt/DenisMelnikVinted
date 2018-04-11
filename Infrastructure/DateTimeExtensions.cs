using System;

namespace Infrastructure
{
    public static class DateTimeExtensions
    {
        public static string GetYearAndMonthString(this DateTime dateTime)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;

            var result = string.Concat(year, month);

            return result;
        }
    }
}
