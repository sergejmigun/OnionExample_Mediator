using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Commands;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Orders
{
    internal class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, Order>
    {
        private IOrdersDataProvider ordersDataProvider;

        public GetOrderByIdRequestHandler(
           IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        public Task<Order> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.ordersDataProvider.GetById(request.OrderId).ToOrder());
        }
    }
}
