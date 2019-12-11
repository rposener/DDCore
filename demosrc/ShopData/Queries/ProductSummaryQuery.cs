using DDCore.Data;
using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopData
{
    public class ProductSummaryQuery : IQuerySpecification<ProductSummaryResult>
    {
        public enum OrderBy { NameAsc, NameDesc, RatingAsc, RatingDesc, PriceAsc, PriceDesc };

        private readonly ShopContext context;

        public readonly int PageSize;
        public readonly OrderBy Order;

        public int PageNumber { get; set; } = 1;

        public ProductSummaryQuery(ShopContext context, int pageSize = 20, OrderBy orderBy = OrderBy.RatingDesc)
        {
            this.context = context;
            PageSize = pageSize;
            Order = orderBy;           
        }

        private IOrderedQueryable<ProductSummaryResult> ApplyOrdering(IQueryable<ProductSummaryResult> source)
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

        public async Task<IReadOnlyList<ProductSummaryResult>> ExecuteAsync()
        {
            var skip = PageSize * (PageNumber - 1);
            var query = ApplyOrdering(context.ProductSummaries)
                .Skip(skip)
                .Take(PageSize);

            return await query.AsNoTracking().ToListAsync();
        }

    }
}
