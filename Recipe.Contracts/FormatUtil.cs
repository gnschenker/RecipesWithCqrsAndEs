using System;
using System.Globalization;

namespace Recipes.Contracts
{
    /// <summary>
    /// Pretty format utils
    /// </summary>
    public static class FormatUtil
    {
        static readonly string[] ByteOrders = new[] { "EB", "PB", "TB", "GB", "MB", "KB", "Bytes" };
        static readonly long MaxScale;

        static FormatUtil()
        {
            MaxScale = (long)Math.Pow(1024, ByteOrders.Length - 1);
        }

        /// <summary>
        /// Formats the size in bytes to a pretty string.
        /// </summary>
        /// <param name="sizeInBytes">The size in bytes.</param>
        /// <returns></returns>
        public static string SizeInBytes(long sizeInBytes)
        {
            var max = MaxScale;

            foreach (var order in ByteOrders)
            {
                if (sizeInBytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(sizeInBytes, max), order);

                max /= 1024;
            }
            return "0 Bytes";
        }

        static string PositiveTimeSpan(TimeSpan timeSpan)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            double delta = timeSpan.TotalSeconds;

            if (delta < 1) return timeSpan.Milliseconds + " ms";
            if (delta < 1 * minute) return timeSpan.Seconds == 1 ? "one second" : timeSpan.Seconds + " seconds";
            if (delta < 2 * minute) return "a minute";
            if (delta < 50 * minute) return timeSpan.Minutes + " minutes";
            if (delta < 70 * minute) return "an hour";
            if (delta < 2 * hour) return (int)timeSpan.TotalMinutes + " minutes";
            if (delta < 24 * hour) return timeSpan.Hours + " hours";
            if (delta < 48 * hour) return (int)timeSpan.TotalHours + " hours";
            if (delta < 30 * day) return timeSpan.Days + " days";

            if (delta < 12 * month)
            {
                var months = (int)Math.Floor(timeSpan.Days / 30.0);
                return months <= 1 ? "one month" : months + " months";
            }

            var years = (int)Math.Floor(timeSpan.Days / 365.0);
            return years <= 1 ? "one year" : years + " years";
        }

        /// <summary>
        /// Displays time passed since (or remaining before) some event expressed in UTC, displaying it as '<em>5 days ago</em>'.
        /// </summary>
        /// <param name="dateInUtc">The date in UTC.</param>
        /// <returns>formatted event</returns>
        public static string TimeOffsetUtc(DateTime dateInUtc)
        {
            if (dateInUtc == DateTime.MinValue)
                return "";
            var now = DateTime.UtcNow;
            var offset = now - dateInUtc;

            if (offset > new TimeSpan(7, 0, 0, 0))
            {
                return dateInUtc.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
            }

            return TimeSpan(offset);
        }

        /// <summary>
        /// Displays time passed since some event expressed, displaying it as '<em>5 days ago</em>'.
        /// </summary>
        /// <param name="localTime">The local time.</param>
        /// <returns></returns>
        public static string TimeOffset(DateTime localTime)
        {
            var now = DateTime.Now;
            var offset = now - localTime;

            if (offset > new TimeSpan(7, 0, 0, 0))
            {
                return localTime.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
            }

            return TimeSpan(offset);
        }

        /// <summary>
        /// Formats time span nicely
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>formatted string</returns>
        public static string TimeSpan(TimeSpan offset)
        {
            if (offset > System.TimeSpan.Zero)
            {
                return PositiveTimeSpan(offset) + " ago";
            }
            return PositiveTimeSpan(-offset) + " from now";
        }
    }
}