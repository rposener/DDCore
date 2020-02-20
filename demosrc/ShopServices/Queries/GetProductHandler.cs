using DDCore;
using DDCore.Data;
using ShopData;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopServices.Queries
{
    public class GetProductHandler : IQueryHandler<GetProduct, Product>
    {
        private readonly ShopContext context;

        public GetProductHandler(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> ExecuteAsync(GetProduct query)
        {
            return await context.Products.FindAsync(query.ProductId);
        }
    }
}
