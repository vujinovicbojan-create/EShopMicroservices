using Carter;
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductResponse(IEnumerable<Product> Products);

    internal class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsRequest>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
