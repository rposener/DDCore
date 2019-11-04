using Microsoft.EntityFrameworkCore;
using ShopDomain.Entities;
using ShopDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopData
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext context;

        public ProductRepository(ShopContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> FindProductsAsync(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveProductAsync(Product toSave)
        {
            throw new NotImplementedException();
        }
    }
}
