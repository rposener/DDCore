using DDCore;
using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShopDomain.Catalog
{
    public class Product : ProductBase, IAggregateRoot
    {
        const int MAX_REVIEWS = 50;
        const int NAME_LENGTH = 128;
        const int DESCRIPTION_LENGTH = 512;
        
        private List<Review> _reviews;

        private Product()
        {

        }

        private Product(string name, string description, decimal price)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _description = description ?? String.Empty;
            _price = price;
            _reviews = new List<Review>();
        }

        public Result Validate(string name, string description, decimal price)
        {
            List<Result> results = new List<Result>();
            results.Add(ValidateName(name));
            results.Add(ValidateDescription(description));
            results.Add(ValidatePrice(price));
            
            return Result.Combine(results);
        }

        public Result ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return Result.Failure("Product name cannot be empty.");
            if (name.Length > NAME_LENGTH)
                return Result.Failure($"Product name must not be longer than {NAME_LENGTH} characters.");

            return Result.Success();
        }

        public Result ValidateDescription(string description)
        { 
            if (description.Length > NAME_LENGTH)
                return Result.Failure($"Product description must not be longer than {DESCRIPTION_LENGTH} characters.");

            return Result.Success();
        }


        public Result ValidatePrice(decimal price)
        {
            if (price < 0)
                return Result.Failure("Product price cannot be negative.");

            return Result.Success();
        }

        public Result<Product> Create(string name, string description, decimal price)
        {
            name = (name ?? String.Empty).Trim();
            description = (description ?? String.Empty).Trim();

            // Validate on Create
            var validationResult = Validate(name, description, price);
            if (validationResult.IsFailure)
                return Result<Product>.Failure(validationResult.Error);

            return Result<Product>.Success(new Product(name, description, price));
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
