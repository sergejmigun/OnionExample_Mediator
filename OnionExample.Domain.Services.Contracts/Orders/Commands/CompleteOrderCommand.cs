using MediatR;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class CompleteOrderCommand : IRequest
    {
        public CompleteOrderCommand(int orderId)
        {
            this.OrderId = orderId;
        }

        public int OrderId { get; private set; }
    }
}
