using System.Linq;
using System.Web;
using MediatR;
using OnionExample.Areas.Cart.ApiModels;
using OnionExample.Areas.Orders.ApiModels;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Orders.Commands;
using OnionExample.Domain.Services.Contracts.Orders.Models;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Core.Cart.Services
{
    internal class CartService : ICartService
    {
        private readonly IMediator mediator;

        static CartService()
        {
            HttpContext.Current.Session["cart"] = new CartModel();
        }

        public CartService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public CartModel GetCart()
        {
            return HttpContext.Current.Session["cart"] as CartModel;
        }

        public void AddToCart(int productId, int? quantity)
        {
            CartModel cart = this.GetCart();
            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (productInCart == null)
            {
                Product product = this.mediator.Send(new GetProductByIdRequest(productId)).Result;

                productInCart = new CartItemModel
                {
                    Price = product.Price,
                    ProductId = product.Id,
                    ProductName = product.Title
                };

                cart.Items.Add(productInCart);
            }

            if (quantity.HasValue)
            {
                productInCart.Quantity = quantity.Value;
            }
            else
            {
                productInCart.Quantity++;
            }
        }

        public void DeleteFromCart(int productId)
        {
            CartModel cart = this.GetCart();
            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            cart.Items.Remove(productInCart);
        }

        public void ProcessOrder(OrderSubmitModel submitModel)
        {
            CartModel cart = this.GetCart();

            this.mediator.Send(new CreateOrderCommand(new OrderCustomer
            {
                Name = submitModel.Customer.Name,
                Phone = submitModel.Customer.Phone
            }, submitModel.Items.Select(x => new OrderItemManagementData
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }))).Wait();

            cart.Items.Clear();
        }
    }
}