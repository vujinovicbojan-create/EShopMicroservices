using Basket.API.Exceptions;
using Basket.API.Models;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDocumentSession _documentSession;

        public BasketRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            _documentSession.Delete<ShoppingCart>(username);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            var basket = await _documentSession.LoadAsync<ShoppingCart>(username, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(username) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            _documentSession.Store(basket);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return basket;
        }
    }
}
