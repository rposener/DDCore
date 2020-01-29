using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Users
{
    public class Customer : EntityObject, IAggregateRoot
    {
        // EF Backing Fields
        private long _customerId;
        private string _displayName;
        private DateTime _lastSeen;
        private DateTime _addedOn;
        private string _email;
        private string _passwordHash;

        /// <summary>
        /// Publicly Visible Properties
        /// </summary>
        public long CustomerId { get => _customerId; }
        public string DisplayName { get => _displayName; }
        public DateTime LastSeen { get => _lastSeen; }
        public DateTime AddedOn { get => _addedOn; }
        public string Email { get => _email; }

        /// <summary>
        /// Added for EF Construction
        /// </summary>
        private Customer()
        {

        }

        private Customer(string email, string passwordHash, string displayName, DateTime addedOn)
        {
            _email = email;
            _passwordHash = passwordHash;
            _displayName = displayName;
            _addedOn = addedOn;
            _lastSeen = addedOn;
        }

        public static Result<Customer> Register(string email, string passwordHash, string displayName)
        {
            return Result<Customer>.Success(new Customer(email, passwordHash, displayName, DateTime.UtcNow));
        }

        public void LoggedIn()
        {
            _lastSeen = DateTime.UtcNow;
        }

        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return _email;
        }
    }
}
