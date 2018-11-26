using System.Collections.Generic;
using MediatR;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Contracts.Orders.Commands
{
    public class GetAllOrdersRequest: IRequest<IEnumerable<Order>>
    {
    }
}
