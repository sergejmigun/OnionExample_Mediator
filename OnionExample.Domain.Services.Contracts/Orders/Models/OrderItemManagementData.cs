﻿namespace OnionExample.Domain.Services.Contracts.Orders.Models
{
    public class OrderItemManagementData
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
