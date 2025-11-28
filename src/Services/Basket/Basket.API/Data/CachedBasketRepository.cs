using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository : IBasketRepository
    {
        private readonly IBasketRepository _basketRepository;
        private IDistributedCache _distributedCache;

        public CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) 
        {
            _basketRepository = basketRepository;
            _distributedCache = distributedCache;
        }

        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RefreshAsync(username, cancellationToken);
            await _basketRepository.DeleteBasket(username, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await _distributedCache.GetStringAsync(username);
            if (!String.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }

            var basket = await _basketRepository.GetBasket(username, cancellationToken);
            await _distributedCache.SetStringAsync(username, JsonSerializer.Serialize<ShoppingCart>(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await _basketRepository.StoreBasket(basket, cancellationToken);
            await _distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
