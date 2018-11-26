using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Domain.Services.Products
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public CreateProductCommandHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.productsDataProvider.Create(new Product
            {
                 Description = request.Description,
                 Price = request.Price,
                 Title = request.Title
            }));
        }
    }
}
