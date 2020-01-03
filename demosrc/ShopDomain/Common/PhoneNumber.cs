using DDCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Common
{
    [Owned]
    public class PhoneNumber : ValueObject
    {
        private string _countryCode;
        private string _number;
        private string _extension;

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
