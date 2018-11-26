using System.Linq;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Domain.Services.Orders
{
    internal class DeleteOrderItemFromOrderCommandHandler : RequestHandler<DeleteOrderItemFromOrderCommand>
    {
        private IOrdersDataProvider ordersDataProvider;

        public DeleteOrderItemFromOrderCommandHandler(
            IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        protected override void Handle(DeleteOrderItemFromOrderCommand request)
        {
            Order order = this.ordersDataProvider.GetById(request.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == request.ProductId).FirstOrDefault();

            if (item != null)
            {
                order.Items.Remove(item);
                this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
            }
        }
    }
}
