using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Common
{
    public class Address : ValueObject
    {
        // EF Backing Fields
        private readonly string _street;
        private readonly string _city;
        private readonly string _state;
        private readonly string _postCode;

        // Properties
        public string Street { get => _street; }
        public string City { get => _city; }
        public string State { get => _state; }
        public string PostCode { get => _postCode; }

        public Address(string street, string city, string state, string postCode)
        {
            _street = street;
            _city = city;
            _state = state;
            _postCode = postCode;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return PostCode;
        }
    }
}
