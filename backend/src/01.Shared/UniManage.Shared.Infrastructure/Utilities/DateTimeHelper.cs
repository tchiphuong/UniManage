using System.Globalization;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Helper functions for DateTime operations and formatting.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets current UTC time.
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets current local time.
        /// </summary>
        public static DateTime Now => DateTime.Now;

        /// <summary>
        /// Converts UTC time to Vietnam timezone (UTC+7).
        /// </summary>
        /// <param name="utcDateTime">UTC DateTime.</param>
        /// <returns>Vietnam local time.</returns>
        public static DateTime ToVietnamTime(DateTime utcDateTime)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vietnamTimeZone);
        }

        /// <summary>
        /// Converts Vietnam time to UTC.
        /// </summary>
        /// <param name="vietnamDateTime">Vietnam local DateTime.</param>
        /// <returns>UTC time.</returns>
        public static DateTime ToUtcFromVietnam(DateTime vietnamDateTime)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(vietnamDateTime, vietnamTimeZone);
        }

        /// <summary>
        /// Formats date for Vietnamese display (dd/MM/yyyy).
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>Formatted date string.</returns>
        public static string ToVietnameseDateString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Formats datetime for Vietnamese display (dd/MM/yyyy HH:mm).
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>Formatted datetime string.</returns>
        public static string ToVietnameseDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Formats datetime with full Vietnamese format (dd/MM/yyyy HH:mm:ss).
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>Formatted datetime string.</returns>
        public static string ToVietnamseFullDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Formats date for database storage (yyyy-MM-dd).
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>Database-formatted date string.</returns>
        public static string ToDatabaseDateString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Formats datetime for database storage (yyyy-MM-dd HH:mm:ss).
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>Database-formatted datetime string.</returns>
        public static string ToDatabaseDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Formats datetime for ISO 8601 format.
        /// </summary>
        /// <param name="dateTime">DateTime to format.</param>
        /// <returns>ISO 8601 formatted string.</returns>
        public static string ToIso8601String(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        /// <summary>
        /// Parses Vietnamese date string (dd/MM/yyyy or dd/MM/yyyy HH:mm).
        /// </summary>
        /// <param name="dateString">Date string to parse.</param>
        /// <returns>Parsed DateTime or null if invalid.</returns>
        public static DateTime? ParseVietnameseDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            var formats = new[]
            {
                "dd/MM/yyyy",
                "dd/MM/yyyy HH:mm",
                "dd/MM/yyyy HH:mm:ss",
                "d/M/yyyy",
                "d/M/yyyy H:mm",
                "d/M/yyyy H:mm:ss"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets start of day (00:00:00).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>Start of day DateTime.</returns>
        public static DateTime StartOfDay(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets end of day (23:59:59.999).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>End of day DateTime.</returns>
        public static DateTime EndOfDay(DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Gets start of month (first day).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>Start of month DateTime.</returns>
        public static DateTime StartOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Gets end of month (last day).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>End of month DateTime.</returns>
        public static DateTime EndOfMonth(DateTime dateTime)
        {
            return StartOfMonth(dateTime).AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Gets start of year (January 1st).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>Start of year DateTime.</returns>
        public static DateTime StartOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// Gets end of year (December 31st).
        /// </summary>
        /// <param name="dateTime">Input DateTime.</param>
        /// <returns>End of year DateTime.</returns>
        public static DateTime EndOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// Calculates age in years based on birth date.
        /// </summary>
        /// <param name="birthDate">User's birth date.</param>
        /// <param name="asOfDate">Reference date (defaults to today).</param>
        /// <returns>Age in years.</returns>
        public static int CalculateAge(DateTime birthDate, DateTime? asOfDate = null)
        {
            var referenceDate = asOfDate ?? DateTime.Today;

            var age = referenceDate.Year - birthDate.Year;

            if (referenceDate < birthDate.AddYears(age))
                age--;

            return age;
        }

        /// <summary>
        /// Calculates business days between two dates (excludes weekends).
        /// </summary>
        /// <param name="startDate">Start reference date.</param>
        /// <param name="endDate">End reference date.</param>
        /// <returns>Total number of business days.</returns>
        public static int CalculateBusinessDays(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return 0;

            var totalDays = (endDate - startDate).Days + 1;
            var businessDays = 0;

            for (int i = 0; i < totalDays; i++)
            {
                var currentDate = startDate.AddDays(i);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays++;
                }
            }

            return businessDays;
        }

        /// <summary>
        /// Checks if a date falls on a weekend.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>True if the date is a Saturday or Sunday.</returns>
        public static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Checks if a date is today's date.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>True if the date is today.</returns>
        public static bool IsToday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        /// <summary>
        /// Checks if a date is within the current month.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>True if the date is in the current month.</returns>
        public static bool IsCurrentMonth(DateTime dateTime)
        {
            var now = DateTime.Now;
            return dateTime.Year == now.Year && dateTime.Month == now.Month;
        }

        /// <summary>
        /// Checks if a date is within the current year.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>True if the date is in the current year.</returns>
        public static bool IsCurrentYear(DateTime dateTime)
        {
            return dateTime.Year == DateTime.Now.Year;
        }

        /// <summary>
        /// Returns a relative time string (e.g., "just now", "2 hours ago").
        /// </summary>
        /// <param name="dateTime">DateTime to compare.</param>
        /// <param name="referenceDate">Optionally specify a reference date.</param>
        /// <returns>A localized relative time description.</returns>
        public static string GetRelativeTime(DateTime dateTime, DateTime? referenceDate = null)
        {
            var reference = referenceDate ?? DateTime.Now;
            var timeSpan = reference - dateTime;
            var totalMinutes = Math.Abs(timeSpan.TotalMinutes);
            var isFuture = timeSpan.TotalMinutes < 0;

            string result;

            if (totalMinutes < 1)
                result = "vừa xong";
            else if (totalMinutes < 60)
                result = $"{(int)totalMinutes} phút";
            else if (totalMinutes < 1440) // Less than 24 hours
                result = $"{(int)(totalMinutes / 60)} giờ";
            else if (totalMinutes < 43200) // Less than 30 days
                result = $"{(int)(totalMinutes / 1440)} ngày";
            else if (totalMinutes < 525600) // Less than 365 days
                result = $"{(int)(totalMinutes / 43200)} tháng";
            else
                result = $"{(int)(totalMinutes / 525600)} năm";

            if (result == "vừa xong")
                return result;

            return isFuture ? $"trong {result} nữa" : $"{result} trước";
        }

        /// <summary>
        /// Adds business days skipping weekends.
        /// </summary>
        /// <param name="startDate">Start reference date.</param>
        /// <param name="businessDays">Number of days to add.</param>
        /// <returns>Target date skipping weekends.</returns>
        public static DateTime AddBusinessDays(DateTime startDate, int businessDays)
        {
            var result = startDate;
            var daysAdded = 0;

            while (daysAdded < businessDays)
            {
                result = result.AddDays(1);
                if (!IsWeekend(result))
                {
                    daysAdded++;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the calendar quarter for a given date.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>Quarter number (1-4).</returns>
        public static int GetQuarter(DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Returns the localized Vietnamese quarter name.
        /// </summary>
        /// <param name="dateTime">DateTime to check.</param>
        /// <returns>Localized quarter name string.</returns>
        public static string GetQuarterName(DateTime dateTime)
        {
            var quarter = GetQuarter(dateTime);
            return $"Quý {quarter}";
        }

        /// <summary>
        /// Checks if a specific year is a leap year.
        /// </summary>
        /// <param name="year">Year to check.</param>
        /// <returns>True if it is a leap year.</returns>
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }
}
