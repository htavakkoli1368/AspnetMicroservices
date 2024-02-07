using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.GrpcServices;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        public readonly IBasketRepository _repository;
        public readonly DiscountGrpcServices _discountGrpcServices;

        

        public BasketController(IBasketRepository basketRepository, DiscountGrpcServices discountGrpcServices)
        {
            _repository = basketRepository;
            _discountGrpcServices = discountGrpcServices;
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
            foreach (var item in basket.Items)
            {
               var coupon = await  _discountGrpcServices.Getdiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return new OkObjectResult( await _repository.UpdateBasket(basket));
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
