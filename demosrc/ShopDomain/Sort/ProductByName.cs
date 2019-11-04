using ShopDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Sort
{
    public class ProductByName : IComparer<Product>, IComparer<ProductSummary>
    {
        public int Compare(Product x, Product y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int Compare(ProductSummary x, ProductSummary y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
