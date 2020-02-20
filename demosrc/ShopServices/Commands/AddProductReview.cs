using DDCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServices.Commands
{
    public class AddProductReview: ICommand
    {
        public AddProductReview(long productId, string reviewer, int stars, string reviewText)
        {
            ProductId = productId;
            Reviewer = reviewer;
            Stars = stars;
            ReviewText = reviewText;
        }

        public long ProductId { get; }

        public string Reviewer { get; }

        public int Stars { get; }

        public string ReviewText { get; }
    }
}
