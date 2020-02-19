using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopData;
using ShopDomain.Catalog;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopDataTests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        // Injected Items
        ILogger<ProductRepository> logger;

        [TestInitialize]
        public void Init_Tests()
        {
            logger = Mock.Of<ILogger<ProductRepository>>();
        }

        [TestMethod]
        public async Task FindAsync_GetProductAsync_Invalid_Id()
        {
            // Setup
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "Get_Invalid")
                .Options;

            //Test
            Product prod;
            using (var testContext = new ShopContext(options))
            {
                var repository = new ProductRepository(testContext, logger);
                prod = await repository.GetProductAsync(999);
            }

            // Assert
            Assert.IsNull(prod, "Product did not return null");
        }

        [TestMethod]
        public async Task FindAsync_GetProductAsync_Valid_Id()
        {
            // Setup
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "Get_Valid")
                .Options;

            using (var context1 = new ShopContext(options))
            {
                context1.Add(Product.Create("Test", "Description", 15.84M).Value);
                context1.SaveChanges();
            }

            //Test
            Product prod;
            using (var testContext = new ShopContext(options))
            {
                var repository = new ProductRepository(testContext, logger);
                prod = await repository.GetProductAsync(1);
            }

            // Assert
            Assert.IsNotNull(prod, "Product was null");
            Assert.AreEqual("Test", prod.Name, "Name was not saved");
            Assert.AreEqual("Description", prod.Description, "Description was not saved");
            Assert.AreEqual(15.84M, prod.Price, "Price was not saved");
        }

        [TestMethod]
        public async Task Product_With_Review_Saved()
        {
            // Setup
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "Validate_Reviews")
                .Options;

            using (var context1 = new ShopContext(options))
            {
                var product = Product.Create("Test", "Description", 15.84M).Value;
                product.AddReview("test", "some data", 2);
                context1.Add(product);
                context1.SaveChanges();
            }

            //Test
            Product prod;
            using (var testContext = new ShopContext(options))
            {
                var repository = new ProductRepository(testContext, logger);
                prod = await repository.GetProductAsync(1);

                // Assert
                Assert.IsNotNull(prod, "Product was null");
                Assert.IsNotNull(prod.Reviews, "Reviews were null");
                Assert.AreEqual(1, prod.Reviews.Count, "Review was not saved");
            }
        }

        [TestMethod]
        public async Task Product_Saves_And_Deletes_Reviews()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Setup
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "Validate_Review_Deletes")
                .Options;

            using (var context1 = new ShopContext(options))
            {
                var product = Product.Create("Test", "Description", 15.84M).Value;
                product.AddReview("ORIGINAL", "some data", 2);  /// this should get deleted before Assert
                context1.Add(product);
                context1.SaveChanges();
            }

            //Test
            using (var testContext = new ShopContext(options))
            {
                var repository = new ProductRepository(testContext, logger);
                var product = await repository.GetProductAsync(1);

                // Add 1 more than MAX_REVIEWS
                for (int i = 0; i <= Product.MAX_REVIEWS; i++)
                {
                    product.AddReview($"review {i}", "test review notes", 3);
                }
                await repository.SaveChangesAsync();
            }

            // Assert
            using (var testContext = new ShopContext(options))
            {
                var repository = new ProductRepository(testContext, logger);
                var product = await repository.GetProductAsync(1);
                Assert.AreEqual(Product.MAX_REVIEWS, product.Reviews.Count);
                Assert.IsFalse(product.Reviews.Any(r => r.Reviewer == "ORIGINAL"));
            }

            connection.Close();
        }
    }
}
