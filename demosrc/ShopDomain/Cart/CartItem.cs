using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Cart
{
    public class CartItem : ValueObject
    {
        private long _productId;
        private int _quantity;

        public long ProductId
        {
            get { return _productId; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }
               
        private CartItem()
        {

        }

        private CartItem(long productId, int quantity)
        {
            _productId = productId;
            _quantity = quantity;
        }

        #region Validation Methods

        public static Result ValidateQuantity(int quantity)
        {
            if (quantity<1)
                return Result.Failure("Quantity cannot be less than 1.");
            return Result.Success();
        }

        #endregion Validation Methods

        #region Member Methods

        public Result AdjustQuantity(int quantity)
        {
            var result = ValidateQuantity(quantity);
            if (result.IsFailure)
                return result;
            _quantity += quantity;
            return Result.Success();
        }

        #endregion Member Methods 

        #region Factory Methods

        static internal Result<CartItem> Create(long productId, int quantity)
        {
            var result = ValidateQuantity(quantity);
            if (result.IsFailure)
                return Result<CartItem>.Failure(result.Error);
            return Result<CartItem>.Success(new CartItem(productId, quantity));
        }

        #endregion Factory Methods

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return ProductId;
        }
    }
}
