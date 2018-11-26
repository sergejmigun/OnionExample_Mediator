using MediatR;
using OnionExample.Domain.Models.Common.Products;

namespace OnionExample.Domain.Services.Contracts.Products.Commands
{
    public class GetProductByIdRequest: IRequest<Product>
    {
        public GetProductByIdRequest(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; private set; }
    }
}