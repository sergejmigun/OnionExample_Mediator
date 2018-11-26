using System.Collections.Generic;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Contracts.Orders.Models
{
    public class OrderUpdatingData
    {
        public int OrderId { get; set; }

        public short Status { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }
    }
}