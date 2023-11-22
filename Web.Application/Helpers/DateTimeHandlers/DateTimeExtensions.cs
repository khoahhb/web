using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;

namespace Web.Application.Helpers.DateTimeHandlers
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime dateTime, string timeZoneId)
        {
            return TimezoneHelper.ConvertToUtc(dateTime, timeZoneId);
        }

        public static DateTime ToUtc(this string dateString, string timeZoneId, string format)
        {
            DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return TimezoneHelper.ConvertToUtc(dateTime, timeZoneId);
        }

        public static DateTime FromUtc(this DateTime dateTime, string timeZoneId)
        {
            return TimezoneHelper.ConvertFromUtc(dateTime, timeZoneId);
        }

        public static string ToFormattedString(this DateTime dateTime, string toTimeZoneId, bool includeTimeZone = false, string format = "dd/MM/yyyy h:mm:ss tt")
        {
            return TimezoneHelper.ConvertTimeZone(dateTime, TimeZoneInfo.Utc.Id, toTimeZoneId, format, includeTimeZone);
        }

        public static TimeSpan To24HourFormat(string time)
        {
            var parts = time.Split(':');
            var hour = int.Parse(parts[0]);
            var minute = int.Parse(parts[1].Substring(0, 2));
            var amPm = parts[1].Substring(3);

            if (amPm.ToUpper() == "PM" && hour < 12)
                hour += 12;
            else if (amPm.ToUpper() == "AM" && hour == 12)
                hour = 0;

            return new TimeSpan(hour, minute, 0);
        }
    }
}
