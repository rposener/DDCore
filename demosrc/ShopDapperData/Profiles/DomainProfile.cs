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

            // Domain -> Data Maps
            CreateMap<ShopDomain.Catalog.Product, Product>();
            CreateMap<ShopDomain.Catalog.Review, Review>();

        }
    }
}
