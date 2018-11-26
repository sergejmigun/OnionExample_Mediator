using System.Linq;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Domain.Services.Orders
{
    internal class AddOrderItemToOrderCommandHandler : RequestHandler<AddOrderItemToOrderCommand>
    {
        private IOrdersDataProvider ordersDataProvider;
        private IProductsDataProvider productsDataProvider;

        public AddOrderItemToOrderCommandHandler(
            IOrdersDataProvider ordersDataProvider,
            IProductsDataProvider productsDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
            this.productsDataProvider = productsDataProvider;
        }

        protected override void Handle(AddOrderItemToOrderCommand request)
        {
            Order order = this.ordersDataProvider.GetById(request.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == request.ProductId).FirstOrDefault();

            if (item == null)
            {
                Product product = this.productsDataProvider.GetById(request.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductId = request.ProductId,
                    Price = product.Price,
                    ProductTitle = product.Title,
                    Quantity = request.Quantity
                });
            }
            else
            {
                item.Quantity = request.Quantity;
            }

            this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
        }
    }
}
