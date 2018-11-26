using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Commands;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Orders
{
    internal class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequest, IEnumerable<Order>>
    {
        private IOrdersDataProvider ordersDataProvider;

        public GetAllOrdersRequestHandler(
           IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        public Task<IEnumerable<Order>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.ordersDataProvider.GetAll().Select(x => x.ToOrder()).ToList() as IEnumerable<Order>);
        }
    }
}
