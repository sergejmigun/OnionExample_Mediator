using MediatR;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public DeleteOrderCommand(int orderId)
        {
            this.OrderId = orderId;
        }

        public int OrderId { get; private set; }
    }
}
