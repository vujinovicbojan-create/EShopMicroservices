using Carter;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpointcs : ICarterModule
    {
        public record DeleteProductResponse(bool IsSuccess);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
