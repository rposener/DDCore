using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShopDomain.Common
{
    public class Date : ValueObject
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        public DateTime Value { get => new DateTime(_year, _month, _day, 0, 0, 0, 0, new GregorianCalendar(), DateTimeKind.Unspecified); }

        public string IsoFormat { get => Value.ToString("YYYY-dd-mm"); }

        public Date(DateTime date)
        {
            _year = date.Year;
            _month = date.Month;
            _day = date.Day;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return _year;
            yield return _month;
            yield return _day;
        }
    }
}
