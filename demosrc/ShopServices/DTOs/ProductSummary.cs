using ShopData.ViewTypes;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.DTOs
{
    public sealed class ProductSummary
    {
        internal ProductSummary(ProductResult product)
        {
            ProductId = product.ProductId;
            Rating = product.Rating;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

        public decimal? Rating { get; }

        public long ProductId { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }
    }
}
