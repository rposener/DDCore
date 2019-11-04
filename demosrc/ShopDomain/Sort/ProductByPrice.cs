using ShopDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Sort
{
    public class ProductByPrice : IComparer<Product>, IComparer<ProductSummary>
    {
        public int Compare(Product x, Product y)
        {
            return x.Price.CompareTo(y.Price);
        }

        public int Compare(ProductSummary x, ProductSummary y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
