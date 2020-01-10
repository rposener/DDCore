using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDapperData.Profiles
{
    public class ValueTypesProfile : Profile
    {
        public ValueTypesProfile()
        {
            CreateMap<ShopDomain.Common.Date, DateTime>()
                .ConstructUsing(date => date.Value);
            CreateMap<DateTime, ShopDomain.Common.Date>()
                .ConstructUsing(dt => new ShopDomain.Common.Date(dt));
        }
    }
}
