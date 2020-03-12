using DDCore.Queries;
using ShopData;
using ShopServices.DTOs;
using System;
using System.Threading.Tasks;

namespace ShopServices.Queries
{
    public class GetProductHandler : IQueryHandler<GetProduct, ProductDetails>
    {
        private readonly ShopContext context;

        public GetProductHandler(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductDetails> ExecuteAsync(GetProduct query)
        {
            var product = await context.Products.FindAsync(query.ProductId);
            return new ProductDetails(product);
        }
    }
}
