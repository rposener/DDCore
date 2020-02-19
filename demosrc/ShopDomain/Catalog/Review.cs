using DDCore.Domain;
using ShopDomain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShopDomain.Catalog
{
    public class Review : ValueObject, IValidatableObject
    {
        // EF Core Backing Fields
        private long _reviewId;
        private string _reviewer;
        private string _reviewText;
        private Date _reviewDate;
        private int _rating;

        /// <summary>
        /// Private Constructor for EF Core Materialization
        /// </summary>
        private Review()
        {

        }

        private Review(string reviewer, string reviewText, int rating)
        {
            _reviewer = reviewer;
            _reviewText = reviewText;
            _reviewDate = Date.TodayLocal;
            _rating = rating;
        }

        public long ReviewId
        {
            get { return _reviewId; }
        }

        public string Reviewer 
        { 
            get { return _reviewer; }
        }

        [StringLength(200, ErrorMessage = "Review Text must be at least 5 characters.", MinimumLength =5)]
        public string ReviewText
        {
            get { return _reviewText; }
        }

        public Date ReviewDate
        {
            get { return _reviewDate; }
        }

        public int Rating
        {
            get { return _rating; }
        }

        #region Factory Methods

        public static Result<Review> Create(string reviewer, string reviewText, int rating)
        {
            return Result.Validate(new Review(reviewer, reviewText, rating));
        }

        #endregion Factory Methods

        /// <summary>
        /// Returns the fields that Identify this Values Object
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return _reviewer;
            yield return _reviewText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (_rating < 1 || _rating > 5)
                yield return new ValidationResult("Rating must be between 1 and 5.", new[] { nameof(Rating) });
            if (String.IsNullOrWhiteSpace(_reviewer))
                yield return new ValidationResult("Reviewer must be a valid username.", new[] { nameof(Reviewer) });
        }
    }
}
