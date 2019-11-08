using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Catalog
{
    public class ProductSummary : ProductBase, IAggregateRoot
    {

        private decimal? _rating;
        
        public decimal? Rating
        {
            get { return _rating; }
        }

        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return ProductId;
        }
    }
}
