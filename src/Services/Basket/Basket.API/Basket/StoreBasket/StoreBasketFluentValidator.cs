using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketFluentValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketFluentValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
}