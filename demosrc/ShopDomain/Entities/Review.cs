using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Entities
{
    public class Review : EntityObject
    {
        private int _reviewId;
        private string _reviewer;
        private string _reviewText;
        private DateTime _reviewDate;
        private int _rating;

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

        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return _reviewer;
            yield return _reviewText;
        }
    }
}
