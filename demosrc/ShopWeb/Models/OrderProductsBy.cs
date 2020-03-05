using ShopAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopWeb.Models
{
    /// <summary>
    /// Specifies the Order a Product Query is Sorted
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderProductsBy
    {
        NameAsc,
        NameDesc,
        RatingAsc,
        RatingDesc,
        PriceAsc,
        PriceDesc
    }

    public class OrderProductsByProfile : AutoMapper.Profile
    {
        public OrderProductsByProfile()
        {
            // Only maps to Command
            CreateMap<OrderProductsBy, GetProducts.OrderBy>();
        }
    }
}
