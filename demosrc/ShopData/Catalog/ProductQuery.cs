using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Catalog
{
    public class ProductQuery
    {
        public readonly int ProductId;

        public ProductQuery(int productId)
        {
            ProductId = productId;
        }
        public async Task<Product> ExecuteAsync(ShopContext context)
        {
            var query = from p in context.Products.Include("_reviews")
                        where p.ProductId == ProductId
                        select p;
            return await query.FirstOrDefaultAsync();
        }
    }
}
