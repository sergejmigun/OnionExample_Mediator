using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Domain.Services.Products
{
    internal class UpdateProductCommandHandler : RequestHandler<UpdateProductCommand>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public UpdateProductCommandHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        protected override void Handle(UpdateProductCommand request)
        {
            this.productsDataProvider.Update(new Product
            {
                 Id = request.Id,
                 Description = request.Description,
                 Price = request.Price,
                 Title = request.Title
            });
        }
    }
}
