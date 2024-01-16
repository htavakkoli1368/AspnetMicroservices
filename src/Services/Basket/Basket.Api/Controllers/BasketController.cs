using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        public readonly IBasketRepository _repository;

        public BasketController(IBasketRepository basketRepository)
        {
            _repository = basketRepository;
        }
        [HttpGet("{username}",Name = "GetBasket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>>  GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return new OkObjectResult(basket?? new ShoppingCart(username));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {             
            return new OkObjectResult(_repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name ="DeleteBasket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return new OkObjectResult("successfully delete");
        }
    }
}
