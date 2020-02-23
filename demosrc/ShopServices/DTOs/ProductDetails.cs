using ShopData.ViewTypes;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.DTOs
{
    public class ProductDetails
    {
        internal ProductDetails(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

        public long ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
    }
}
