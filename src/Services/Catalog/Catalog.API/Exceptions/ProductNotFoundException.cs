using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product", id)
        {
        }
    }
}
