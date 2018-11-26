using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MediatR;
using OnionExample.Areas.Products.ApiModels;
using OnionExample.Areas.Products.Mappers;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Areas.Products.ApiControllers
{
    public class ProductsApiController : ApiController
    {
        private readonly IMediator mediator;

        public ProductsApiController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            return this.mediator.Send(new GetAllProductsCommand()).Result.Select(x => x.ToProductModel());
        }

        [HttpGet]
        public ProductModel GetProduct(int id)
        {
            return this.mediator.Send(new GetProductByIdRequest(id)).Result.ToProductModel();
        }

        [HttpPost]
        public int CreateProduct(ProductSubmitModel submitModel)
        {
            return this.mediator.Send(submitModel.ToCreateProductCommand()).Result;
        }

        [HttpPut]
        public void EditProduct(ProductSubmitModel submitModel)
        {
            this.mediator.Send(submitModel.ToUpdateProductCommand()).Wait();
        }

        [HttpDelete]
        public void DeleteProduct(int id)
        {
            this.mediator.Send(new DeleteProductCommand(id)).Wait();
        }
    }
}