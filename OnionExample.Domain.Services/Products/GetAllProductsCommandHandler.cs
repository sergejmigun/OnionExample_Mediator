using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Domain.Services.Products
{
    internal class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<Product>>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public GetAllProductsCommandHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        public Task<IEnumerable<Product>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.productsDataProvider.GetAll());
        }
    }
}
