using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

     

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
             

        }
        public Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            throw new NotImplementedException();
        }
        public Task DeleteBasket(string username)
        {
            throw new NotImplementedException();
        }
    }
}
