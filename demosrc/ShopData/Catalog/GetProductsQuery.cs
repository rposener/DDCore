using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Catalog
{
    public class GetProductsQuery
    {
        public string Search { get; private set; }

        public GetProductsQuery(string search)
        {
            Search = search;
        }

        public async Task<IReadOnlyList<Product>> ExecuteAsync(ShopContext context)
        {
            var query = from p in context.Products.Include("_reviews")
                        where p.Name.Contains(Search)
                        select p;
            return await query.ToListAsync();
        }
    }
}
