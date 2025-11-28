using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IValidator<DeleteBasketCommand> _validator;

        public DeleteBasketHandler(IBasketRepository basketRepository,
                                   IValidator<DeleteBasketCommand> validator) 
        {
            _basketRepository = basketRepository;
            _validator = validator;
        }

        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            if (errors.Any())
            {
                var exceptionMessage = string.Join("; ", errors);
                throw new ValidationException(exceptionMessage);
            }

            var result = await _basketRepository.DeleteBasket(command.UserName, cancellationToken);

            return new DeleteBasketResult(result);
        }
    }
}
