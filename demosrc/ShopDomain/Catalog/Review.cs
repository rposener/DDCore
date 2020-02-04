using DDCore.Domain;
using ShopDomain.Common;
using System;
using System.Collections.Generic;

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

        internal Review(string reviewer, string reviewText, int rating)
        {
            this._reviewer = reviewer;
            this._reviewText = reviewText;
            _reviewDate = Date.TodayLocal;
            this._rating = rating;
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
