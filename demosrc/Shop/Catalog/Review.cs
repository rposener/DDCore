using DDCore;
using DDCore.Domain;
using ShopDomain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShopDomain.Catalog
{
    public class Review : ValueObject
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
            if (String.IsNullOrWhiteSpace(reviewer))
                return Result.Failure<Review>("Reviewer is Required.");
            if (String.IsNullOrWhiteSpace(reviewText))
                return Result.Failure<Review>("Review must have some text.");
            if (rating < 1 || rating > 5)
                return Result.Failure<Review>("Rating is invalid (must be betwen 1 and 5.");
            return Result.Success(new Review(reviewer, reviewText, rating));
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

    }
}
