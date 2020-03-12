using DDCore;
using DDCore.Commands;
using ShopData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopServices.Commands
{
    public class AddProductReviewHandler : ICommandHandler<AddProductReview>
    {
        private readonly ShopContext context;

        public AddProductReviewHandler(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> HandleAsync(AddProductReview command)
        {
            var product = await context.Products.FindAsync(command.ProductId);
            var result = product.AddReview(command.Reviewer, command.ReviewText, command.Stars);
            if (result.IsSuccess)
            {
                await context.SaveChangesAsync();
            }
            return result;
        }
    }
}
