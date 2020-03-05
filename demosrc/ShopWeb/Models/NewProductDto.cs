using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Models
{
    /// <summary>
    /// DTO Used to Create a New Product
    /// </summary>
    public class NewProductDto
    {
        /// <summary>
        /// Name of the New Product
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the new Product
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Price for the new Product
        /// </summary>
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}
