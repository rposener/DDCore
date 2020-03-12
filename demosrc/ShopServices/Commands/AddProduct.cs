using DDCore.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Commands
{
    public sealed class AddProduct : ICommand
    {
        public AddProduct(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get;  }

        public string Description { get; }

        public decimal Price { get; }
    }
}
