using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopDomain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomainTests
{
    [TestClass]
    public class ProductTests
    {

        [TestMethod]
        public void Create_valid()
        {

            var result = Product.Create("test", "test", 1.00M);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("test", result.Value.Name);
        }

        [TestMethod]
        public void Create_Invalid_Price()
        {

            var result = Product.Create("test", "test", -1.00M);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Product price cannot be negative.", result.Error);
        }

        [TestMethod]
        public void AddReview_Limits_Reviews()
        {

            var product = Product.Create("test", "test description", 1.00M).Value;

            for (int i = 0; i <= Product.MAX_REVIEWS; i++)
            {
                product.AddReview($"review {i}", "test review text", 3);
            }

            Assert.AreEqual(Product.MAX_REVIEWS, product.Reviews.Count);
        }
    }
}
