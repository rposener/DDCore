using DDCore.Data;

namespace ShopDomain.Catalog
{
    public class ProductSummaryResult : IQueryResult
    {
        public ProductSummaryResult(long productId, decimal? rating, string name, string description, decimal price)
        {
            ProductId = productId;
            Rating = rating;
            Name = name;
            Description = description;
            Price = price;
        }

        public decimal? Rating { get; private set; }

        public long ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

    }
}
