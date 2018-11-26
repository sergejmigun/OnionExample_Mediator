using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Domain.Services.Orders
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private IOrdersDataProvider ordersDataProvider;
        private IProductsDataProvider productsDataProvider;

        public CreateOrderCommandHandler(
            IOrdersDataProvider ordersDataProvider,
            IProductsDataProvider productsDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
            this.productsDataProvider = productsDataProvider;
        }

        public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            OrderCreationData dalOrderData = request.ToDalOrderCreationData();

            dalOrderData.CreationDate = DateTime.Now;
            dalOrderData.Status = (short)OrderStatus.Pending;

            foreach (OrderItemManagementData item in dalOrderData.Items)
            {
                Product product = this.productsDataProvider.GetById(item.ProductId);

                item.ProductTitle = product.Title;
                item.Price = product.Price;
            }

            return Task.FromResult(this.ordersDataProvider.Create(dalOrderData));
        }
    }
}
