using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Marten;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, 
                                       List<string> Category, 
                                       string Description, 
                                       string ImageFile, 
                                       decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    //session needed for marten library
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IDocumentSession _session;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        {
            _session = session;
            _validator = validator;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            if (errors.Any())
            {
                var exceptionMessage = string.Join("; ", errors);
                throw new ValidationException(exceptionMessage);
            }

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save to database
            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
