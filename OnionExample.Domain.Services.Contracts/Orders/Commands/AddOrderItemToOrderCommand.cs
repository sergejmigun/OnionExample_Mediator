using MediatR;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class AddOrderItemToOrderCommand : IRequest
    {
        public AddOrderItemToOrderCommand(int orderId, int productId, int quantity)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public int OrderId { get; private set; }

        public int ProductId { get; private set; }

        public int Quantity { get; private set; }
    }
}
