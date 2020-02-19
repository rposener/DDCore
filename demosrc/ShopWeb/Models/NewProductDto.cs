using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Models
{
    public class NewProductDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}
