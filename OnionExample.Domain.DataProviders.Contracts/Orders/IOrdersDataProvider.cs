using System.Collections.Generic;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Contracts.Orders
{
    public interface IOrdersDataProvider
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(OrderCreationData order);

        void Update(OrderUpdatingData order);

        void Delete(int orderId);
    }
}