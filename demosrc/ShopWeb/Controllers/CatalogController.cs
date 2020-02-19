using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopData;
using ShopDomain.Catalog;
using ShopWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        private readonly ProductRepository productRepository;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(ProductRepository productRepository, ILogger<CatalogController> logger)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<ProductSummaryResult>> Get([FromServices] ProductSummaryQuery query, [FromQuery] int page = 1)
        {
            query.SetPageNumber(page);
            return await query.ExecuteAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] NewProductDto product)
        {
            logger.LogInformation("Creating a new Product");
            var result = Product.Create(product.Name, product.Description, product.Price);
            if (result.IsSuccess)
            {
                productRepository.AddProduct(result.Value);
                await productRepository.SaveChangesAsync();
                return Ok(result.Value);
            }
            return BadRequest(result.ValidationResults);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await productRepository.GetProductAsync(id);
        }

        // POST api/<controller>
        [HttpPost("{id}/review")]
        public async Task<ActionResult> Post(int id, NewReviewDto review)
        {
            var product = await productRepository.GetProductAsync(id);
            var result = product.AddReview(User.Identity.Name ?? "Unknown User", review.ReviewText, review.Stars);
            if (result.IsSuccess)
            {
                await productRepository.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(result.ValidationResults);
        }

        [HttpPut("{id}/name")]
        public async Task UpdateName(int id, [FromBody]string value)
        {
            var product = await productRepository.GetProductAsync(id);
            product.ChangeName(value);
            await productRepository.SaveChangesAsync();
        }

        [HttpPut("{id}/description")]
        public async Task UpdateDescription(int id, [FromBody]string value)
        {
            var product = await productRepository.GetProductAsync(id);
            product.UpdateDescription(value);
            await productRepository.SaveChangesAsync();
        }
    }
}
