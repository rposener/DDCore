using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Catalog
{
    public class ProductsQuery
    {
        public enum OrderBy { NameAsc, NameDesc, PriceAsc, PriceDesc };

        public readonly string Search;

        public readonly OrderBy Order;

        public ProductsQuery(string search, OrderBy order)
        {
            Search = search;
            Order = order;
        }
        private IOrderedQueryable<Product> ApplyOrdering(IQueryable<Product> source)
        {
            switch (Order)
            {
                case OrderBy.NameAsc:
                    return source.OrderBy(p => p.Name);
                case OrderBy.NameDesc:
                    return source.OrderByDescending(p => p.Name);
                case OrderBy.PriceAsc:
                    return source.OrderBy(p => p.Price);
                case OrderBy.PriceDesc:
                    return source.OrderByDescending(p => p.Price);
            }
            return source.OrderBy(p => p.Name);
        }

        public async Task<IReadOnlyList<Product>> ExecuteAsync(ShopContext context)
        {
            var query = from p in context.Products.Include("_reviews")
                        where p.Name.Contains(Search)
                        select p;
            return await ApplyOrdering(query).ToListAsync();
        }
    }
}
