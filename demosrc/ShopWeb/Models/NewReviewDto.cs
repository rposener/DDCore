using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Models
{
    public class NewReviewDto
    {
        [Range(1,5, ErrorMessage ="Review must be between 1 and 5.")]
        public int Stars { get; set; }

        [Required]
        [StringLength(250, MinimumLength =5, ErrorMessage ="A Review must be between 5 and 250 characters.")]
        public string ReviewText { get; set; }
    }
}
