using Shop.Cart;
using ShopData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore.Events
{
    public class ReserveProductOnAddToCart : IDomainEventHandler<ProductAddedToCart>
    {
        private readonly ShopContext shopContext;

        public ReserveProductOnAddToCart(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task HandleEventAsync(ProductAddedToCart domainEvent)
        {
            var product = await shopContext.Products.FindAsync(domainEvent.ProductId);
            // Do something like 
            //product.ReduceAvailableQuantity(domainEvent.Quantity);
        }
    }
}
