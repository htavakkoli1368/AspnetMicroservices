using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
          Task<ShoppingCart> GetBasket(string username);
          Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
          Task DeleteBasket(string username);
    }
}
