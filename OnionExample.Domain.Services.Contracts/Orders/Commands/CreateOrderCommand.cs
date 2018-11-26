using System.Collections.Generic;
using MediatR;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderCommand(OrderCustomer customer, IEnumerable<OrderItemManagementData> items)
        {
            this.Customer = customer;
            this.Items = items;
        }

        public OrderCustomer Customer { get; private set; }

        public IEnumerable<OrderItemManagementData> Items { get; private set; }
    }
}
