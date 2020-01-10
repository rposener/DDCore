using DDCore.Domain;
using ShopDomain.Common;
using System;
using System.Collections.Generic;

namespace ShopDomain.Catalog
{
    public class Review : ValueObject
    {
        // EF Core Backing Fields
        private int review_id;
        private string reviewer;
        private string reviewText;
        private Date reviewDate;
        private int rating;

        /// <summary>
        /// Private Constructor for EF Core Materialization
        /// </summary>
        private Review()
        {

        }

        internal Review(string reviewer, string reviewText, int rating)
        {
            this.reviewer = reviewer;
            this.reviewText = reviewText;
            reviewDate = Date.TodayLocal;
            this.rating = rating;
        }

        public int ReviewId
        {
            get { return review_id; }
        }

        public string Reviewer 
        { 
            get { return reviewer; }
        }

        public string ReviewText
        {
            get { return reviewText; }
        }

        public Date ReviewDate
        {
            get { return reviewDate; }
        }

        public int Rating
        {
            get { return rating; }
        }

        /// <summary>
        /// Returns the fields that Identify this Values Object
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return reviewer;
            yield return reviewText;
        }
    }
}
