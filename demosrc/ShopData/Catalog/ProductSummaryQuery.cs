using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Catalog
{
    public class ProductSummaryQuery
    {
        public enum OrderBy { NameAsc, NameDesc, RatingAsc, RatingDesc, PriceAsc, PriceDesc };

        public readonly int PageSize;
        public readonly OrderBy Order;

        public ProductSummaryQuery(int pageSize = 20, OrderBy orderBy = OrderBy.RatingDesc)
        {
            PageSize = pageSize;
            Order = orderBy;           
        }

        private IOrderedQueryable<ProductSummary> ApplyOrdering(IQueryable<ProductSummary> source)
        {
            switch (Order)
            {
                case OrderBy.NameAsc:
                    return source.OrderBy(p => p.Name);
                case OrderBy.NameDesc:
                    return source.OrderByDescending(p => p.Name);
                case OrderBy.RatingAsc:
                    return source.OrderBy(p => p.Rating);
                case OrderBy.RatingDesc:
                    return source.OrderByDescending(p => p.Rating);
                case OrderBy.PriceAsc:
                    return source.OrderBy(p => p.Price);
                case OrderBy.PriceDesc:
                    return source.OrderByDescending(p => p.Price);
            }
            return source.OrderBy(p => p.Rating);
        }

        public async Task<IReadOnlyList<ProductSummary>> ExecuteAsync(ShopContext context, int PageNumber = 1)
        {
            var skip = PageSize * (PageNumber - 1);
            var query = ApplyOrdering(context.ProductSummaries)
                .Skip(skip)
                .Take(PageSize);

            return await query.ToListAsync();
        }

    }
}
