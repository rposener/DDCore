using DDCore;
using DDCore.Data;
using ShopData;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Commands
{
    public class AddProductHandler : ICommandHandler<AddProduct>
    {
        private readonly ShopContext context;

        public AddProductHandler(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> HandleAsync(AddProduct command)
        {
            var result = Product.Create(command.Name, command.Description, command.Price);
            if (result.IsSuccess)
            {
                context.Add(result.Value);
                await context.SaveChangesAsync();
            }
            return result;
        }
    }
}
