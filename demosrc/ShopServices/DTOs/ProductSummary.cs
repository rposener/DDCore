using ShopData.ViewTypes;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.DTOs
{
    public class ProductSummary
    {
        internal ProductSummary(ProductResult product)
        {
            ProductId = product.ProductId;
            Rating = product.Rating;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

        public decimal? Rating { get; private set; }

        public long ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
    }
}
