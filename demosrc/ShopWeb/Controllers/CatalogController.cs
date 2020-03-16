using AutoMapper;
using DDCore.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Commands;
using ShopAppServices;
using ShopServices.Commands;
using ShopServices.Queries;
using ShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CatalogController : Controller
    {
        private readonly ICommandDispatcher cmdDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<CatalogController> logger;
        private readonly IMapper mapper;

        public CatalogController(ICommandDispatcher cmdDispatcher, IQueryDispatcher queryDispatcher, ILogger<CatalogController> logger, IMapper mapper)
        {
            this.cmdDispatcher = cmdDispatcher ?? throw new ArgumentNullException(nameof(cmdDispatcher));
            this.queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Queries the Catalog for a set of <see cref="ProductSummaryDto"/>
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSummaryDto>>> Get([FromQuery] int pageSize = 50, [FromQuery] int page = 1, [FromQuery]OrderProductsBy orderBy = OrderProductsBy.RatingDesc)
        {
            var ordering = mapper.Map<GetProducts.OrderBy>(orderBy);
            var results = await queryDispatcher.DispatchAsync(new GetProducts(pageSize, ordering, page));
            var dtos = mapper.Map<ProductSummaryDto>(results);
            return Ok(dtos);
        }

        /// <summary>
        /// Creates a New Product
        /// </summary>
        /// <param name="product">Details of new Product to Create</param>
        /// <returns>New Product ID</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(int))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IDictionary<string, ICollection<string>>))]
        public async Task<ActionResult<int>> CreateProduct([FromBody] NewProductDto product)
        {
            var result = await cmdDispatcher.DispatchAsync(new AddProduct(product.Name, product.Description, product.Price));
            if (result)
            {
                return Ok(result.Value);
            }
            ModelState.AddModelError("", result.Error);
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Gets a Specific Product
        /// </summary>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductSummaryDto>> Get(long productId)
        {
            var result = await queryDispatcher.DispatchAsync(new GetProduct(productId));
            var dto = mapper.Map<ProductSummaryDto>(result);
            return Ok(dto);
        }

        /// <summary>
        /// Adds a new review to a Product
        /// </summary>
        /// <param name="productId">Id of the Product</param>
        /// <param name="review">New Review to Add</param>
        /// <returns></returns>
        [HttpPost("{productId}/review")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IDictionary<string, ICollection<string>>))]
        public async Task<ActionResult> Post(long productId, NewReviewDto review)
        {
            var reviewer = User.Identity.Name ?? "Unknown Reviewer";
            var result = await cmdDispatcher.DispatchAsync(new AddProductReview(productId, reviewer, review.Stars, review.ReviewText));
            if (result)
            {
                return NoContent();
            }
            ModelState.AddModelError("", result.Error);
            return BadRequest(ModelState);
        }
    }
}
