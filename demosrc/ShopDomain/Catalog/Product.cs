using DDCore.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ShopDomain.Catalog
{
    public class Product : EntityObject, IAggregateRoot
    {
        public const int MAX_REVIEWS = 50;
        public const int NAME_LENGTH = 128;
        public const int DESCRIPTION_LENGTH = 512;

        // EF Backing Field for Value Object
        private long _productId;
        private string _name;
        private string _description;
        private decimal _price;

        private List<Review> _reviews;

        public IReadOnlyList<Review> Reviews
        {
            get 
            {
                return _reviews;
            }
        }

        public long ProductId
        {
            get { return _productId; }
        }

        public virtual string Name
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

        private Product()
        {
            _reviews = new List<Review>();
        }

        /// <summary>
        /// Private Constructor use <seealso cref="Create(string, string, decimal)"/> Factory Method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        private Product(string name, string description, decimal price):this()
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _description = description ?? string.Empty;
            _price = price;
        }

        #region Validation Methods
        public static Result Validate(string name, string description, decimal price)
        {
            List<Result> results = new List<Result>();
            results.Add(ValidateName(name));
            results.Add(ValidateDescription(description));
            results.Add(ValidatePrice(price));

            return Result.Combine(results);
        }

        public static Result ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return Result.Failure("Product name cannot be empty.");
            if (name.Length > NAME_LENGTH)
                return Result.Failure($"Product name must not be longer than {NAME_LENGTH} characters.");

            return Result.Success();
        }

        public static Result ValidateDescription(string description)
        {
            if (description.Length > NAME_LENGTH)
                return Result.Failure($"Product description must not be longer than {DESCRIPTION_LENGTH} characters.");

            return Result.Success();
        }


        public static Result ValidatePrice(decimal price)
        {
            if (price < 0)
                return Result.Failure("Product price cannot be negative.");

            return Result.Success();
        }

        #endregion Validation Methods

        #region Factory Methods

        /// <summary>
        /// Creates a new <see cref="Product"></see>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static Result<Product> Create(string name, string description, decimal price)
        {
            name = (name ?? String.Empty).Trim();
            description = (description ?? String.Empty).Trim();

            // Validate on Create
            var validationResult = Validate(name, description, price);
            if (validationResult.IsFailure)
                return Result.Failure<Product>(validationResult.Error);

            return Result.Success(new Product(name, description, price));
        }

        #endregion Factory Methods

        #region Member Methods

        public Result ChangeName(string name)
        {
            var validationResult = ValidateName(name);
            if (validationResult.IsFailure)
                return validationResult;
            this._name = name;
            return Result.Success();
        }

        public Result UpdateDescription(string description)
        {
            var validationResult = ValidateDescription(description);
            if (validationResult.IsFailure)
                return validationResult;
            this._description = description;
            return Result.Success();
        }

        public Result AddReview(string reviewer, string reviewText, int rating)
        {
            var reviewResult = Review.Create(reviewer, reviewText, rating);
            if (reviewResult.IsFailure)
                return reviewResult;
            var newReview = reviewResult.Value;
            if (_reviews.Any(r => r == newReview))
            {
                // Already Exists, do not add
                return Result.Success();
            }

            // Add the new Review
            _reviews.Add(newReview);
            // Ensure we only keep the last MAX_REVIEWS for this product
            while (_reviews.Count > MAX_REVIEWS)
            {
                _reviews.RemoveAt(0);
            }
            return Result.Success();
        }

        #endregion Member Methods

        /// <summary>
        /// Fields that Determine this Entities Identity
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return ProductId;
        }
    }
}
