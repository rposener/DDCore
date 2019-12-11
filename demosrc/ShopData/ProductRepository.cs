using DDCore.Domain;
using Microsoft.EntityFrameworkCore;
using ShopDomain.Catalog;
using System.Linq;
using System.Threading.Tasks;

namespace ShopData
{
    public class ProductRepository : RepositoryBase<Product>
    {
        private readonly ShopContext context;

        /// <summary>
        /// Base Query Should include all Includes, etc necessary to materialize <seealso cref="Product"/>
        /// </summary>
        private IQueryable<Product> BaseQuery => context.Products.Include("_reviews");

        /// <summary>
        /// Repository Constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductRepository(ShopContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns a Product from the Repository
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductAsync(int productId)
        {
            var query = from p in BaseQuery
                        where p.ProductId == productId
                        select p;
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves all Changes tracked by the Repository
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
