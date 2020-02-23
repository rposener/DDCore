using DDCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Commands;
using ShopAppServices;
using ShopServices.Commands;
using ShopServices.DTOs;
using ShopServices.Queries;
using ShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        private readonly Messages mesages;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(Messages mesages, ILogger<CatalogController> logger)
        {
            this.mesages = mesages ?? throw new ArgumentNullException(nameof(mesages));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSummary>>> Get([FromQuery] int pageSize = 50, [FromQuery] int page = 1, [FromQuery] GetProducts.OrderBy orderBy = GetProducts.OrderBy.RatingDesc)
        {
            var result = await mesages.DispatchAsync(new GetProducts(pageSize, orderBy, page));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductSummary>> CreateProduct([FromBody] NewProductDto product)
        {
            var result = await mesages.DispatchAsync(new AddProduct(product.Name, product.Description, product.Price));
            if (result)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSummary>> Get(long id)
        {
            var result = await mesages.DispatchAsync(new GetProduct(id));
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost("{id}/review")]
        public async Task<ActionResult> Post(long id, NewReviewDto review)
        {
            var reviewer = User.Identity.Name ?? "Unknown Reviewer";
            var result = await mesages.DispatchAsync(new AddProductReview(id, reviewer, review.Stars, review.ReviewText));
            if (result)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        //[HttpPut("{id}/name")]
        //public async Task UpdateName(int id, [FromBody]string value)
        //{
        //    var product = await productRepository.GetProductAsync(id);
        //    product.ChangeName(value);
        //    await productRepository.SaveChangesAsync();
        //}

        //[HttpPut("{id}/description")]
        //public async Task UpdateDescription(int id, [FromBody]string value)
        //{
        //    var product = await productRepository.GetProductAsync(id);
        //    product.UpdateDescription(value);
        //    await productRepository.SaveChangesAsync();
        //}
    }
}
