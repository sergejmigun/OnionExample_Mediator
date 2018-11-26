using MediatR;

namespace OnionExample.Domain.Services.Contracts.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; private set; }
    }
}