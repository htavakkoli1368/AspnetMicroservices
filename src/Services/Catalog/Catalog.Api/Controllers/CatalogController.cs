using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Products))]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            //get productsa
            var products = await _repository.GetProducts();
            return new OkObjectResult(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Products))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Products))]
        public async Task<ActionResult<Products>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return new NotFoundResult();
            }
            return new OkObjectResult(product);
        }
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Products>))]        
        public async Task<ActionResult<IEnumerable<Products>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);
            return new OkObjectResult(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Products))]        
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products product)
        {
            await _repository.CreateProduct(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Products))]
        public async Task<IActionResult> UpdateProduct([FromBody] Products product)
        {
            return new OkObjectResult(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Products))]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return new OkObjectResult(await _repository.DeleteProduct(id));
        }
    }
}
