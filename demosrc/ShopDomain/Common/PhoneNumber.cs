using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Common
{
    public class PhoneNumber : ValueObject
    {
        private readonly string _countryCode;
        private readonly string _number;
        private readonly string _extension;

        // Property 
        public string CountryCode { get => _countryCode; }

        public string Number { get => _number; }

        public string Extension { get => _extension; }

        public PhoneNumber(string number, string countryCode = "", string extension = "")
        {
            _number = number;
            _countryCode = countryCode;
            _extension = extension;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return CountryCode;
            yield return Number;
            yield return Extension;
        }
    }
}
