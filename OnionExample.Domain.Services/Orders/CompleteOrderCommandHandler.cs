using System.Linq;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Domain.Services.Orders
{
    internal class CompleteOrderCommandHandler : RequestHandler<CompleteOrderCommand>
    {
        private readonly IOrdersDataProvider ordersDataProvider;

        public CompleteOrderCommandHandler(
            IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        protected override void Handle(CompleteOrderCommand request)
        {
            Order order = this.ordersDataProvider.GetById(request.OrderId);

            this.ordersDataProvider.Update(new OrderUpdatingData
            {
                OrderId = request.OrderId,
                Status = (short)OrderStatus.Processed,
                Items = order.Items.Select(x => x.ToDalOrderItemManagementData(request.OrderId))
            });
        }
    }
}
