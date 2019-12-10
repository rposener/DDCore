using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Catalog
{
    public class ProductSummary : ProductBase, IAggregateRoot
    {

        private decimal? _rating;

        /// <summary>
        /// Private Constructor for EF Core
        /// </summary>
        private ProductSummary()
        {

        }
        
        /// <summary>
        /// Field Unique to ProductSummary
        /// </summary>
        public decimal? Rating
        {
            get { return _rating; }
        }

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
