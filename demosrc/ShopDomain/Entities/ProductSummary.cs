using DDCore.Domain;
using System;
using System.Collections.Generic;

namespace ShopDomain.Entities
{
    public class ProductSummary : EntityObject, IAggregateRoot
    {

        private int _productId;
        private string _name;
        private string _description;
        private decimal _price;
        private decimal? _rating;

        public int ProductId
        {
            get { return _productId; }
        }

        public string Name
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
