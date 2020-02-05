using AutoMapper;
using Dapper;
using DDCore.Data;
using Microsoft.Extensions.Logging;
using ShopDapperData.Extensions;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDapperData
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ILogger<ProductRepository> logger;
        private readonly IDbConnection connection;
        private readonly IMapper mapper;

        public ProductRepository(ILogger<ProductRepository> logger, IDbConnection connection, IMapper mapper)
        {
            this.logger = logger;
            this.connection = connection;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns a <seealso cref="Product"/> from the Repository or null if none are found
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductAsync(int productId)
        {
            logger.LogInformation("Loading Product {0}", productId);
            var productData = await connection.QueryFirstAsync<Data.Product>("SELECT * FROM [Product].[Details] WHERE [ProductId] = @productId", new { productId }, commandType:CommandType.Text);
            var reviewData = await connection.QueryAsync<Data.Review>("SELECT * FROM [Product].[Reviews] WHERE [ProductId] = @productId", new { productId }, commandType: CommandType.Text);
            var entity = mapper.Map<Product>(productData);
            entity.SetField("_reviews", reviewData.AsList());
            return entity;
        }

        /// <summary>
        /// Saves a Product and all associated entities to the Repository
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task SaveProductAsync(Product product)
        {
            var productData = mapper.Map<Data.Product>(product);
            var reviewData = mapper.Map<IEnumerable<Data.Review>>(product.GetField<List<Review>>("_reviews"));

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    productData.ProductId = await connection.ExecuteScalarAsync<long>("UPSERT SP that returns ProductId", 
                        productData, 
                        transaction: transaction);

                    foreach (var review in reviewData)
                    {
                        var parameters = new DynamicParameters(reviewData);
                        parameters.Add("ProductId", productData.ProductId);
                        review.ReviewId = await connection.ExecuteScalarAsync<long>("UPSERT SP that returns ReviewId", 
                            parameters, 
                            transaction: transaction);
                    }

                    await connection.ExecuteScalarAsync("SP to remove reviews for @ProductId not in @ReviewIds",
                    new
                    {
                        productData.ProductId,
                        ReivewIds = String.Join(",",reviewData.Select(r => r.ReviewId))
                    }, transaction: transaction);
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    logger.LogError("Failed Updating Entity: {0} - {1}", productData.ProductId, ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
            // Set Id Field
            product.SetField("_productId", productData.ProductId);
            // Set Owned Aggregate Field
            product.SetField("_reviews", mapper.Map<List<Review>>(reviewData));
        }
    }
}
