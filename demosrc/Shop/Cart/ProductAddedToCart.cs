using DDCore.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Cart
{
    public class ProductAddedToCart: IDomainEvent
    {
        public ProductAddedToCart(string cartId, long productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }

        public string CartId { get; }
        public long ProductId { get; }
        public int Quantity { get; }
    }
}
