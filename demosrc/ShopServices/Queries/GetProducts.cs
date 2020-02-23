using DDCore.Data;
using ShopServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopAppServices
{

    public class GetProducts : IQuery<IEnumerable<ProductSummary>>
    {
        public GetProducts(int pageSize, OrderBy order, int pageNumber)
        {
            PageSize = pageSize;
            Order = order;
            PageNumber = pageNumber;
        }

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

    }
}
