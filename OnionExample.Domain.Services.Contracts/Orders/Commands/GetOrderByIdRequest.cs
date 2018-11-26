using MediatR;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class GetOrderByIdRequest: IRequest<Order>
    {
        public GetOrderByIdRequest(int orderId)
        {
            this.OrderId = orderId;
        }

        public int OrderId { get; private set; }
    }
}
