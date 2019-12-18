using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Catalog
{
    public class Review : ValueObject
    {
        // EF Core Backing Fields
        private int _reviewId;
        private string _reviewer;
        private string _reviewText;
        private DateTime _reviewDate;
        private int _rating;

        /// <summary>
        /// Private Constructor for EF Core Materialization
        /// </summary>
        private Review()
        {

        }

        internal Review(string reviewer, string reviewText, int rating)
        {
            _reviewer = reviewer;
            _reviewText = reviewText;
            _reviewDate = DateTime.UtcNow;
            _rating = rating;
        }

        public int ReviewId
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

        public DateTime ReviewDate
        {
            get { return _reviewDate; }
        }

        public int Rating
        {
            get { return _rating; }
        }

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
