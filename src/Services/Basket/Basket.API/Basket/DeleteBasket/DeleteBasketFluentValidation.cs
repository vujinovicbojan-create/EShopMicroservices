using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketFluentValidation : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketFluentValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
}
