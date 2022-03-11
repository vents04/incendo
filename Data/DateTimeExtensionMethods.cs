using System;

namespace ExtensionMethods
{
    public static class DateTimeExtensionMethods
    {
        private static readonly DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        public static long ToUnixMilliseconds(this DateTime dateTime)
        {
            var span = TimeZoneInfo.ConvertTimeToUtc(dateTime) - unixStart;
            return (long)span.TotalMilliseconds;
        }

        public static long ToUnixSeconds(this DateTime dateTime)
        {
            return ToUnixMilliseconds(dateTime) / 1000;
        }

        public static DateTime FromUnixMilliseconds(this DateTime dateTime, long milliseconds)
        {
            var span = TimeSpan.FromMilliseconds(milliseconds);
            return unixStart + span;
        }

        public static DateTime FromUnixSeconds(this DateTime dateTime, long seconds)
        {
            return FromUnixMilliseconds(dateTime, seconds * 1000);
        }
    }
}