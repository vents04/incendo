using System;

namespace ExtensionMethods
{
    public static class DateTimeExtensionMethods
    {
        public static long ToUnixMilliseconds(this DateTime dateTime)
        {
            TimeSpan span = dateTime - DateTime.UnixEpoch;
            return (long)span.TotalMilliseconds;
        }

        public static long ToUnixSeconds(this DateTime dateTime)
        {
            return ToUnixMilliseconds(dateTime) / 1000;
        }

        public static DateTime FromUnixMilliseconds(this DateTime dateTime, long milliseconds)
        {
            TimeSpan span = TimeSpan.FromMilliseconds(milliseconds);
            return DateTime.UnixEpoch + span;
        }

        public static DateTime FromUnixSeconds(this DateTime dateTime, long seconds)
        {
            return FromUnixMilliseconds(dateTime, seconds * 1000);
        }
    }
}