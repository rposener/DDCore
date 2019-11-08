using System.Collections.Generic;

namespace ShopDomain.Catalog
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
