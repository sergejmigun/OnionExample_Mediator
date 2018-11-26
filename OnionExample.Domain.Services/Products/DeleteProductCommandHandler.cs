using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Domain.Services.Products
{
    internal class DeleteProductCommandHandler : RequestHandler<DeleteProductCommand>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public DeleteProductCommandHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        protected override void Handle(DeleteProductCommand request)
        {
            this.productsDataProvider.Delete(request.ProductId);
        }
    }
}
