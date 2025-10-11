using System.Globalization;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for DateTime operations and formatting
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets current UTC time
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets current local time
        /// </summary>
        public static DateTime Now => DateTime.Now;

        /// <summary>
        /// Converts UTC time to Vietnam timezone (UTC+7)
        /// </summary>
        /// <param name="utcDateTime">UTC DateTime</param>
        /// <returns>Vietnam local time</returns>
        public static DateTime ToVietnamTime(DateTime utcDateTime)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vietnamTimeZone);
        }

        /// <summary>
        /// Converts Vietnam time to UTC
        /// </summary>
        /// <param name="vietnamDateTime">Vietnam local DateTime</param>
        /// <returns>UTC time</returns>
        public static DateTime ToUtcFromVietnam(DateTime vietnamDateTime)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(vietnamDateTime, vietnamTimeZone);
        }

        /// <summary>
        /// Formats date for Vietnamese display (dd/MM/yyyy)
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Formatted date string</returns>
        public static string ToVietnameseDateString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Formats datetime for Vietnamese display (dd/MM/yyyy HH:mm)
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Formatted datetime string</returns>
        public static string ToVietnameseDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Formats datetime with full Vietnamese format (dd/MM/yyyy HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Formatted datetime string</returns>
        public static string ToVietnamseFullDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Formats date for database storage (yyyy-MM-dd)
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Database-formatted date string</returns>
        public static string ToDatabaseDateString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Formats datetime for database storage (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Database-formatted datetime string</returns>
        public static string ToDatabaseDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Formats datetime for ISO 8601 format
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>ISO 8601 formatted string</returns>
        public static string ToIso8601String(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        /// <summary>
        /// Parses Vietnamese date string (dd/MM/yyyy or dd/MM/yyyy HH:mm)
        /// </summary>
        /// <param name="dateString">Date string to parse</param>
        /// <returns>Parsed DateTime or null if invalid</returns>
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
        /// Gets start of day (00:00:00)
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>Start of day</returns>
        public static DateTime StartOfDay(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets end of day (23:59:59.999)
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>End of day</returns>
        public static DateTime EndOfDay(DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Gets start of month
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>Start of month</returns>
        public static DateTime StartOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Gets end of month
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>End of month</returns>
        public static DateTime EndOfMonth(DateTime dateTime)
        {
            return StartOfMonth(dateTime).AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Gets start of year
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>Start of year</returns>
        public static DateTime StartOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// Gets end of year
        /// </summary>
        /// <param name="dateTime">Input DateTime</param>
        /// <returns>End of year</returns>
        public static DateTime EndOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// Calculates age in years
        /// </summary>
        /// <param name="birthDate">Birth date</param>
        /// <param name="asOfDate">Date to calculate age as of (default: today)</param>
        /// <returns>Age in years</returns>
        public static int CalculateAge(DateTime birthDate, DateTime? asOfDate = null)
        {
            var referenceDate = asOfDate ?? DateTime.Today;

            var age = referenceDate.Year - birthDate.Year;

            if (referenceDate < birthDate.AddYears(age))
                age--;

            return age;
        }

        /// <summary>
        /// Calculates business days between two dates (excludes weekends)
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Number of business days</returns>
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
        /// Checks if date is weekend
        /// </summary>
        /// <param name="dateTime">Date to check</param>
        /// <returns>True if weekend</returns>
        public static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Checks if date is today
        /// </summary>
        /// <param name="dateTime">Date to check</param>
        /// <returns>True if today</returns>
        public static bool IsToday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        /// <summary>
        /// Checks if date is in current month
        /// </summary>
        /// <param name="dateTime">Date to check</param>
        /// <returns>True if in current month</returns>
        public static bool IsCurrentMonth(DateTime dateTime)
        {
            var now = DateTime.Now;
            return dateTime.Year == now.Year && dateTime.Month == now.Month;
        }

        /// <summary>
        /// Checks if date is in current year
        /// </summary>
        /// <param name="dateTime">Date to check</param>
        /// <returns>True if in current year</returns>
        public static bool IsCurrentYear(DateTime dateTime)
        {
            return dateTime.Year == DateTime.Now.Year;
        }

        /// <summary>
        /// Gets relative time description (e.g., "2 hours ago", "in 3 days")
        /// </summary>
        /// <param name="dateTime">DateTime to compare</param>
        /// <param name="referenceDate">Reference date (default: now)</param>
        /// <returns>Relative time description</returns>
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
        /// Adds business days to date (skips weekends)
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="businessDays">Number of business days to add</param>
        /// <returns>Date after adding business days</returns>
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
        /// Gets quarter number (1-4) for given date
        /// </summary>
        /// <param name="dateTime">Date to get quarter for</param>
        /// <returns>Quarter number (1-4)</returns>
        public static int GetQuarter(DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Gets quarter name in Vietnamese
        /// </summary>
        /// <param name="dateTime">Date to get quarter for</param>
        /// <returns>Quarter name</returns>
        public static string GetQuarterName(DateTime dateTime)
        {
            var quarter = GetQuarter(dateTime);
            return $"Quý {quarter}";
        }

        /// <summary>
        /// Checks if year is leap year
        /// </summary>
        /// <param name="year">Year to check</param>
        /// <returns>True if leap year</returns>
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }
}