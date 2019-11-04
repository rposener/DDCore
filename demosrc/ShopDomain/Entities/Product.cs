using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShopDomain.Entities
{
    public class Product : EntityObject, IAggregateRoot
    {
        const int MAX_REVIEWS = 50;

        private int _productId;
        [StringLength(128)]
        private string _name;
        [StringLength(512)]
        private string _description;
        private decimal _price;
        private List<Review> _reviews;

        private Product()
        {

        }

        public Product(string name, string description, decimal price)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _description = description ?? String.Empty;
            if (price < 0)
                throw new ArgumentOutOfRangeException("Product price cannot be negative.");
            _price = price;
            _reviews = new List<Review>();
        }

        public int ProductId
        {
            get { return _productId; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public IReadOnlyCollection<Review> Reviews => _reviews;

        public void ChangeName(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Product must have a name.");
            _name = name;
        }

        public void UpdateDescription(string description)
        {
            _description = description;
        }

        public void AddReview(string reviewer, string reviewText, int rating)
        {
            var newReview = new Review(reviewer, reviewText, rating);
            if (_reviews.Any(r => r == newReview))
            {
                // Already Exists, do not add
                return;
            }

            // Add the new Review
            _reviews.Add(newReview);

            // Ensure we only keep the last MAX_REVIEWS for this product
            while (_reviews.Count > MAX_REVIEWS)
            {
                _reviews.RemoveAt(0);
            }
        }

        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return ProductId;
        }
    }
}
