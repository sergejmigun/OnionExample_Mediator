using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Domain.Services.Products
{
    internal class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, Product>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public GetProductByIdRequestHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        public Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.productsDataProvider.GetById(request.ProductId));
        }
    }
}
