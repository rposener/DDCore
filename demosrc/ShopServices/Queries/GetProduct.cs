using DDCore.Data;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.Queries
{
    public sealed class GetProduct: IQuery<Product>
    {
        public GetProduct(long productId)
        {
            ProductId = productId;
        }

        public long ProductId { get; }
    }
}
