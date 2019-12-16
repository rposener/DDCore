using DDCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopData
{
    /// <summary>
    /// Query to Return a Product Summary
    /// </summary>
    public class ProductSummaryQuery : IQuerySpecification<ProductSummaryResult>
    {
        private readonly ShopContext context;
        private readonly ILogger<ProductSummaryQuery> logger;

        /// <summary>
        /// Constructor that supports Injection for this <seealso cref="IQuerySpecification{T}"/>
        /// </summary>
        /// <param name="context"></param>
        public ProductSummaryQuery(ShopContext context, ILogger<ProductSummaryQuery> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        #region Parameters for this Query

        /// <summary>
        /// Ordering Options available to this Query
        /// </summary>
        public enum OrderBy { NameAsc, NameDesc, RatingAsc, RatingDesc, PriceAsc, PriceDesc };

        /// <summary>
        /// The number of results returned when <seealso cref="ExecuteAsync"/> is called
        /// </summary>
        public int PageSize { get; private set; } = 50;

        /// <summary>
        /// How Items should be Ordered when returned from <seealso cref="ExecuteAsync"/>
        /// defaults to <seealso cref="OrderBy.RatingDesc"/>
        /// </summary>
        public OrderBy Order { get; private set; } = OrderBy.RatingDesc;

        /// <summary>
        /// The PageNumer that will be returned when <seealso cref="ExecuteAsync"/> is called
        /// </summary>
        public int PageNumber { get; private set; } = 1;

        /// <summary>
        /// Methods to Set Query Paramters
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns>the <see cref="ProductSummaryQuery"/> to support chained calls</returns>
        public ProductSummaryQuery SetQueryParameters(int pageSize, OrderBy orderBy)
        {
            if (PageSize < 1)
                throw new ArgumentException("PageSize must be a positive number.", nameof(PageSize));
            PageSize = pageSize;
            logger.LogDebug("Page Size set to {0}", PageSize);
            Order = orderBy;
            logger.LogDebug("Ordering set to {0}", Order.ToString());
            return this;
        }

        /// <summary>
        /// Sets the Page to Return
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns>the <see cref="ProductSummaryQuery"/> to support chained calls</returns>
        public ProductSummaryQuery SetPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
            logger.LogDebug("Page set to {0}", PageNumber);
            return this;
        }

        #endregion Parameters for this Query
        
        /// <summary>
        /// Method to Apply Ordering to the Query based on the <see cref="Order"/> property
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Executes the Query returning a set of <seealso cref="ProductSummaryResult"/>
        /// </summary>
        /// <returns>readonly collection of <seealso cref="ProductSummaryResult"/></returns>
        public async Task<IEnumerable<ProductSummaryResult>> ExecuteAsync()
        {
            logger.LogInformation("Executing Request for {0} results orderd by {1} starting at page {2}", PageSize, Order.ToString(), PageNumber);
            var skip = PageSize * (PageNumber - 1);
            var query = ApplyOrdering(context.ProductSummaries)
                .Skip(skip)
                .Take(PageSize);

            return await query.AsNoTracking().ToArrayAsync();
        }

    }
}
