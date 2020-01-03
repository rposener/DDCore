using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShopDomain.Common
{
    /// <summary>
    /// Represents a Date without any Time Component
    /// </summary>
    public class Date : ValueObject
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        public DateTime Value { get => new DateTime(_year, _month, _day, 0, 0, 0, 0, new GregorianCalendar(), DateTimeKind.Unspecified); }

        /// <summary>
        /// Returns the Date is IsoFormat
        /// </summary>
        public string IsoFormat => _year.ToString("0000")+"-"+_month.ToString("00")+"-"+_day.ToString("00");

        /// <summary>
        /// Today per Local DateTime
        /// </summary>
        public static Date TodayLocal => new Date(DateTime.Now);

        /// <summary>
        /// Today per UTC DateTime
        /// </summary>
        public static Date TodayUTC => new Date(DateTime.UtcNow);

        /// <summary>
        /// Today in a Specific TimeZone
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Date TodayInTimezone(TimeZoneInfo info) => new Date(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, info));

        /// <summary>
        /// Creates a new Date value
        /// </summary>
        /// <param name="date"></param>
        public Date(DateTime date) : this(date.Year, date.Month, date.Day)
        {
        }

        /// <summary>
        /// Creates a new Date value
        /// </summary>
        /// <param name="isoFormat">ISO string date value</param>
        public Date(string isoFormat) : this(DateTime.Parse(isoFormat, DateTimeFormatInfo.InvariantInfo))
        {
        }

        /// <summary>
        /// Creates a new Date value
        /// </summary>
        /// <param name="dateTimeOffset">A DateTimeOffset value</param>
        public Date(DateTimeOffset dateTimeOffset) : this(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day)
        {
        }

        /// <summary>
        /// Creates a new Date value
        /// </summary>
        /// <param name="date"></param>
        public Date(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        /// <summary>
        /// Returns set of Values that are used to compare dates
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return _year;
            yield return _month;
            yield return _day;
        }
    }
}
