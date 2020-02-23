using DDCore.Data;
using ShopDomain.Catalog;
using ShopServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.Queries
{
    public sealed class GetProduct: IQuery<ProductDetails>
    {
        public GetProduct(long productId)
        {
            ProductId = productId;
        }

        public long ProductId { get; }
    }
}
