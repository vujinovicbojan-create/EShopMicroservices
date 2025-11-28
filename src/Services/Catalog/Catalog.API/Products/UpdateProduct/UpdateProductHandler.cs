using BuildingBlocks.CQRS;
using Marten;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IDocumentSession _session;

        public UpdateProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
