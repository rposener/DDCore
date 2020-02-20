using DDCore;
using DDCore.Domain;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopDomain.Cart
{
    public class Cart : EntityObject, IAggregateRoot
    {
        private long _cartId;
        private DateTime _createdUTC;
        private string _cartIdentifier;
        private IList<CartItem> _contents;

        public long CartId { get { return _cartId; } }

        public DateTime CreatedUTC { get { return _createdUTC; } }

        public string CartIdentifier {  get { return _cartIdentifier; } }

        /// <summary>
        /// List of Products in the Cart
        /// </summary>
        public IReadOnlyList<CartItem> Contents
        {
            get
            {
                return _contents.ToArray();
            }
        }

        private Cart()
        {
        }

        private Cart(string cartIdentifier)
        {
            _contents = new List<CartItem>();
            _createdUTC = DateTime.UtcNow;
            _cartIdentifier = cartIdentifier;
        }

        #region Member Methods

        public Result AddProduct(Product product, int quantity)
        {
            return AddProduct(product.ProductId, quantity);
        }

        public Result AddProduct(long productId, int quantity)
        {
            var cartResult = CartItem.Create(productId, quantity);
            if (cartResult.IsFailure)
                return cartResult;
            if (_contents.Contains(cartResult.Value))
            {
                return _contents[_contents.IndexOf(cartResult.Value)].AdjustQuantity(quantity);
            }
            _contents.Add(cartResult.Value);
            return Result.Success();
        }

        #endregion Member Methods

        protected override IEnumerable<IComparable> GetIdentityComponents()
        {
            yield return CartIdentifier;
            yield return CartId;
        }
    }
}
