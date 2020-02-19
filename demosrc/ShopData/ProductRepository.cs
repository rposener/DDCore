using DDCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopDomain.Catalog;
using System.Linq;
using System.Threading.Tasks;

namespace ShopData
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShopContext context;
        private readonly ILogger<ProductRepository> logger;

        /// <summary>
        /// Base Query Should include any Includes necessary to materialize <seealso cref="Product"/> which should not be loaded Lazily
        /// </summary>
        private IQueryable<Product> BaseQuery => context.Products;

        /// <summary>
        /// Repository Constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductRepository(ShopContext context, ILogger<ProductRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a Product to the Repository
        /// </summary>
        /// <param name="newProduct"></param>
        public void AddProduct(Product newProduct)
        {
            context.Add(newProduct);
        }

        /// <summary>
        /// Returns a <seealso cref="Product"/> from the Repository or null if none are found
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductAsync(int productId)
        {
            logger.LogDebug("Loading Product {0}", productId);
            logger.LogInformation("Loading Product");
            return await BaseQuery.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        /// <summary>
        /// Saves all Changes tracked by the Repository
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            logger.LogInformation("Saving Changes");
            await context.SaveChangesAsync();
        }
    }
}
