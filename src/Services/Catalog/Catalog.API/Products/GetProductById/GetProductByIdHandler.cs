using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);

    internal class GetProductByIdHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(query.id, cancellationToken);

            if (product is null)
            {
            }

            return new GetProductByIdResult(product);
        }
    }
}
