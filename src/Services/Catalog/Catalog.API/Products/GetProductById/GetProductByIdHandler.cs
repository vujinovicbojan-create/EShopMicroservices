using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);

    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _session;

        public GetProductByIdHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(query.id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(query.id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
