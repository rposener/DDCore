using AutoMapper;
using DDCore.Data;
using Microsoft.Extensions.Logging;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
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



        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
