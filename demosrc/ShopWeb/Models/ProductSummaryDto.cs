using ShopServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Models
{

    /// <summary>
    /// A Product in the Catalog
    /// </summary>
    public class ProductSummaryDto
    {
        /// <summary>
        /// Average Rating
        /// </summary>
        public decimal? Rating { get; set; }

        /// <summary>
        /// Id of the Product
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Name of the Product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the Product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price of the Product
        /// </summary>
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Example AutoMapper Profile for <seealso cref="ProductSummaryDto"/>
    /// </summary>
    public class ProductSummaryProfile : AutoMapper.Profile
    {
        public ProductSummaryProfile()
        {
            // Map only from Query Result to DTO
            CreateMap<ProductSummary, ProductSummaryDto>()
                .ForMember(dto => dto.Price, opt => opt.Ignore());
        }
    }
}
