using MediatR;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class DeleteOrderItemFromOrderCommand : IRequest
    {
        public DeleteOrderItemFromOrderCommand(int orderId, int productId)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
        }

        public int OrderId { get; private set; }

        public int ProductId { get; private set; }
    }
}
