using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResponse>;
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {

        private readonly IDocumentSession _session;

        public DeleteProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _session.Delete<Product>(command.id);
            await _session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(true);
        }
    }
}
