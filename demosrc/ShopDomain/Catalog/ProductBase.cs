using DDCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDomain.Catalog
{
    public abstract class ProductBase : EntityObject
    {
        // EF Core Backing Fields
        protected int _productId;
        protected string _name;
        protected string _description;
        protected decimal _price;


        public int ProductId
        {
            get { return _productId; }
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }
    }
}
