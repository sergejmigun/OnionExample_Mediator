using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MediatR;
using OnionExample.Areas.Orders.ApiModels;
using OnionExample.Areas.Orders.Mappers;
using OnionExample.Domain.Services.Contracts.Orders.Commands;

namespace OnionExample.Areas.Orders.ApiControllers
{
    public class OrdersApiController : ApiController
    {
        private readonly IMediator mediator;

        public OrdersApiController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<OrderModel> GetOrders()
        {
            return this.mediator.Send(new GetAllOrdersRequest()).Result.Select(x => x.ToOrderModel());
        }

        [HttpGet]
        public OrderModel GetOrder(int id)
        {
            return this.mediator.Send(new GetOrderByIdRequest(id)).Result.ToOrderModel();
        }

        [HttpPut]
        public void CompleteOrder(int id)
        {
            this.mediator.Send(new CompleteOrderCommand(id)).Wait();
        }

        [HttpDelete]
        public void DeleteOrder(int id)
        {
            this.mediator.Send(new DeleteOrderCommand(id)).Wait();
        }

        [HttpPut]
        public void AddProduct(int orderId, int productId, int quantity)
        {
            this.mediator.Send(new AddOrderItemToOrderCommand(orderId, productId, quantity)).Wait();
        }

        [HttpPut]
        public void UpdateProduct(int orderId, int productId, int quantity)
        {
            this.mediator.Send(new AddOrderItemToOrderCommand(orderId, productId, quantity)).Wait();
        }

        [HttpDelete]
        public void DeleteProduct(int orderId, int productId)
        {
            this.mediator.Send(new DeleteOrderItemFromOrderCommand(orderId, productId)).Wait();
        }
    }
}