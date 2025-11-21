using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductHandler(IDocumentSession documentSession) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
