using DDCore.Queries;
using Microsoft.EntityFrameworkCore;
using ShopData;
using ShopData.ViewTypes;
using ShopServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopAppServices.GetProducts;

namespace ShopAppServices.Queries
{

    /// <summary>
    /// Handler to exectue a Query
    /// </summary>
    public class GetProductsHandler : IQueryHandler<GetProducts, IEnumerable<ProductSummary>>
    {
        private readonly ShopContext context;

        public GetProductsHandler(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ProductSummary>> ExecuteAsync(GetProducts query)
        {
            var skip = query.PageSize * (query.PageNumber - 1);
            var results = await ApplyOrdering(context.ProductSummaries, query.Order)
                .Skip(skip).Take(query.PageSize).AsNoTracking().ToArrayAsync();

            return results.Select(r => new ProductSummary(r));
        }

        /// <summary>
        /// Method to Apply Ordering to the Query based on the <see cref="Order"/> property
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private IOrderedQueryable<ProductResult> ApplyOrdering(IQueryable<ProductResult> source, OrderBy order)
        {
            switch (order)
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
    }
}
