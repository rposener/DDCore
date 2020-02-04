using AutoMapper;
using ShopDapperData.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDapperData.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            SourceMemberNamingConvention = new UnderscoreCamelCaseNamingConvention();

            // map all fields
            ShouldMapField = fi => false;
            // don't map any properties
            ShouldMapProperty = pi => true;

            // Domain -> Data Maps
            CreateMap<ShopDomain.Catalog.Product, Product>();
            CreateMap<ShopDomain.Catalog.Review, Review>();

        }
    }
}
