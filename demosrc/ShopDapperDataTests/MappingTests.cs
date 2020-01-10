using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopDapperData.Profiles;
using System;

namespace ShopDapperDataTests
{
    [TestClass]
    public class MappingTests
    {
        IMapper mapper;

        [TestInitialize]
        public void CreateMapper()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ValueTypesProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<DomainProfile>();
            }).CreateMapper();
        }

        [TestMethod]
        public void Verify_Profiles_AreValid()
        {
            // Test + Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid<ValueTypesProfile>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid<ProductProfile>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid<DomainProfile>();
        }

        [TestMethod]
        public void ProductProfile_Review_Test()
        {
            // Setup
            var reviewData = new ShopDapperData.Data.Review
            {
                ReviewId = 323,
                ReviewDate = new DateTime(2010, 3, 5),
                Rating = 5,
                Reviewer = "Me",
                ReviewText = "Some Comment"
            };

            // Test
            var result = mapper.Map<ShopDomain.Catalog.Review>(reviewData);

            // Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(323, result.ReviewId);
            Assert.AreEqual(new ShopDomain.Common.Date(2010,3,5), result.ReviewDate);
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual("Me", result.Reviewer);
            Assert.AreEqual("Some Comment", result.ReviewText);
        }
    }
}
