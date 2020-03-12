using DDCore.Queries;
using ShopServices.DTOs;

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
