namespace Rise.Contact.API.Utils
{
    public static class SystemDateTime
    {
        public static DateTime GetDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public static DateTime? GetDateTime(DateTime? date, TimeSpan? time)
        {
            if (date != null && time != null)
            {
                return GetDateTime((DateTime)date, (TimeSpan)time);
            }
            return default;
        }

        public static DateTime SetDate(DateTime datetime)
        {
            return datetime.ToUniversalTime().Date;
        }

        public static TimeSpan SetTime(DateTime datetime)
        {
            return datetime.ToUniversalTime().AddHours(3).TimeOfDay;
        }

        public static DateTime SetDate(string datetime)
        {
            return SetDate(Convert.ToDateTime(datetime));
        }

        public static TimeSpan SetTime(string datetime)
        {
            return SetTime(Convert.ToDateTime(datetime));
        }

        public static TimeSpan NowTime()
        {
            return NowDateTime().TimeOfDay;
        }

        public static DateTime NowDate()
        {
            return NowDateTime().Date;
        }

        public static DateTime NowDateTime()
        {
            return DateTime.UtcNow.AddHours(3);
        }
    }

    public static class SystemDateTimeExtension
    {
        public static DateTime ToSetDate(this DateTime datetime)
        {
            return SystemDateTime.SetDate(datetime);
        }

        public static TimeSpan ToSetTime(this DateTime datetime)
        {
            return SystemDateTime.SetTime(datetime);
        }

        public static TimeSpan ToNowTime(this DateTime datetime)
        {
            return SystemDateTime.NowTime();
        }

        public static DateTime ToNowDate(this DateTime datetime)
        {
            return SystemDateTime.NowDate();
        }
    }
}