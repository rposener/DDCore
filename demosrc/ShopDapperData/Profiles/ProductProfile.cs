using AutoMapper;
using ShopDapperData.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShopDapperData.Profiles
{
    public class ProductProfile : Profile
    {
        /// <summary>
        /// Contains all Data to Domain Mappings
        /// </summary>
        public ProductProfile()
        {
            DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            // don't map any fields
            ShouldMapField = fi => true;

            // map properties with a public or private getter
            ShouldMapProperty = pi =>
                pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);

            ShouldUseConstructor = ci => ci.IsPrivate && ci.GetParameters().Length == 0;
            // Data -> Domain Maps
            CreateMap<Product, ShopDomain.Catalog.Product>()
                .ForMember("reviews", opt => opt.Ignore());

            CreateMap<Review, ShopDomain.Catalog.Review>();
                //.ForMember("review_id", opts => opts.MapFrom(src => src.ReviewId))
                //.ForMember("reviewer", opts => opts.MapFrom(src => src.Reviewer))
                //.ForMember("reviewText", opts => opts.MapFrom(src => src.ReviewText))
                //.ForMember("reviewDate", opts => opts.MapFrom(src => src.ReviewDate))
                //.ForMember("rating", opt => opt.MapFrom(src => src.Rating));
        }
    }
}
