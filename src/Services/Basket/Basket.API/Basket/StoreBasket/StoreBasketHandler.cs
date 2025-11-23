using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IValidator<StoreBasketCommand> _validator;

        public StoreBasketHandler(IBasketRepository basketRepository, 
                                  IValidator<StoreBasketCommand> validator)
        {
            _basketRepository = basketRepository;
            _validator = validator;
        }

        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            if (errors.Any())
            {
                var exceptionMessage = string.Join("; ", errors);
                throw new ValidationException(exceptionMessage);
            }

            await _basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
