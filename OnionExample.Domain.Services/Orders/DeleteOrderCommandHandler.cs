using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Domain.Services.Orders
{
    internal class DeleteOrderCommandHandler : RequestHandler<DeleteOrderCommand>
    {
        private IOrdersDataProvider ordersDataProvider;

        public DeleteOrderCommandHandler(
            IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        protected override void Handle(DeleteOrderCommand request)
        {
            this.ordersDataProvider.Delete(request.OrderId);
        }
    }
}
