using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Common
{
    public class Address : ValueObject
    {
        // EF Backing Fields
        private string _street;
        private string _city;
        private string _state;
        private string _postCode;

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
