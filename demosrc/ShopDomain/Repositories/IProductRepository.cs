using ShopDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();


        Task<Product> GetProductAsync(int productId);


        Task<IEnumerable<Product>> FindProductsAsync(string search);


        Task SaveProductAsync(Product toSave);
    }
}
