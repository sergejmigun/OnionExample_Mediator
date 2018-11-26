using System.Collections.Generic;
using MediatR;
using OnionExample.Domain.Models.Common.Products;

namespace OnionExample.Domain.Services.Contracts.Products.Commands
{
    public class GetAllProductsCommand: IRequest<IEnumerable<Product>>
    {
    }
}