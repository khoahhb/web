using TimeZoneNames;

namespace Web.Application.Helpers.DateTimeHandlers
{
    public static class TimezoneHelper
    {
        public static string ConvertTimeZone(DateTime fromDateTime, string fromTimeZoneId, string toTimeZoneId,
            string format = "dd/MM/yyyy hh:mm tt", bool includeTimeZone = false)
        {
            try
            {
                var fromZone = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
                var toZone = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

                var convertedDateTime = TimeZoneInfo.ConvertTime(fromDateTime, fromZone, toZone);
                var formattedDateTime = convertedDateTime.ToString(format);

                return includeTimeZone ? $"{formattedDateTime} {GetShortTimeZoneFormat(toZone)}" : formattedDateTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public static DateTime ConvertToUtc(DateTime dateTime, string timeZoneId)
        {
            var zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, zoneInfo);
        }

        public static DateTime ConvertFromUtc(DateTime dateTime, string timeZoneId)
        {
            var zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, zoneInfo);
        }

        private static string GetShortTimeZoneFormat(TimeZoneInfo timeZone)
        {
            try
            {
                var abbreviations = TZNames.GetAbbreviationsForTimeZone(timeZone.Id, "en-US");
                return !string.IsNullOrWhiteSpace(abbreviations.Standard) ? abbreviations.Standard : CreateShortName(timeZone.StandardName);
            }
            catch
            {
                return CreateShortName(timeZone.StandardName);
            }
        }

        private static string CreateShortName(string name)
        {
            return string.Concat(name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(word => word[0]));
        }
    }
}
