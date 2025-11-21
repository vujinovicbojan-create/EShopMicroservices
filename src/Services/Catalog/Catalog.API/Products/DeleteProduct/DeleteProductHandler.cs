using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResponse>;
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            documentSession.Delete<Product>(command.id);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(true);
        }
    }
}
