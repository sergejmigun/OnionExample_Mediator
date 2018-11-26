﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.DataProviders.Helpers;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Orders
{
    internal class MsSqlOrdersDataProvider : IOrdersDataProvider
    {
        static MsSqlOrdersDataProvider()
        {
            if (!MsSqlHelper.CheckDatabaseExists("OnionExample"))
            {
                MsSqlHelper.DeployDbInstance("OnionExample");
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string ordersQuery = @"SELECT * FROM [dbo].[Orders]";

                var dbOrders = db.Query<dynamic>(ordersQuery);

                return dbOrders.Select(x => GetOrder(db, x)).Cast<Order>().ToList();
            }
        }

        public Order GetById(int orderId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string ordersQuery = @"SELECT * FROM [dbo].[Orders] WHERE [Id] = @orderId";

                var dbOrder = db.Query<dynamic>(ordersQuery, new
                {
                    orderId = orderId
                }).First();

                return GetOrder(db, dbOrder);
            }
        }

        public int Create(OrderCreationData order)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string insertOrderQuery = @"INSERT INTO [dbo].[Orders]([CreationDate], [CustomerName], [CustomerPhone], [Status]) VALUES (@CreationDate, @CustomerName, @CustomerPhone, @Status); SELECT CAST(SCOPE_IDENTITY() as int);";

                int orderId = db.Query<int>(insertOrderQuery, new
                {
                    CreationDate = order.CreationDate,
                    CustomerName = order.Customer.Name,
                    CustomerPhone = order.Customer.Phone,
                    Status = order.Status
                }).First();

                InsertOrderItems(db, order.Items, orderId);

                return orderId;
            }
        }

        public void Update(OrderUpdatingData order)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string updateOrderQuery = @"UPDATE [dbo].[Orders] SET [Status] = @Status WHERE [Id] = @OrderId";

                db.Execute(updateOrderQuery, new
                {
                    Status = order.Status,
                    OrderId = order.OrderId
                });

                string deleteItemsQuery = "DELETE FROM [dbo].[OrderItems] WHERE [OrderId] = @OrderId";

                db.Execute(deleteItemsQuery, new
                {
                    OrderId = order.OrderId
                });

                InsertOrderItems(db, order.Items, order.OrderId);
            }
        }

        public void Delete(int orderId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = "DELETE FROM [dbo].[Orders] WHERE [Id] = @OrderId";

                db.Execute(query, new
                {
                    OrderId = orderId
                });
            }
        }

        private static Order GetOrder(IDbConnection db, dynamic dbOrder)
        {
            return new Order
            {
                Id = dbOrder.Id,
                CreationDate = dbOrder.CreationDate,
                Status = dbOrder.Status,
                Customer = new OrderCustomer
                {
                    Name = dbOrder.CustomerName,
                    Phone = dbOrder.CustomerPhone
                },
                Items = GetOrderItems(db, dbOrder.Id)
            };
        }

        private static ICollection<OrderItem> GetOrderItems(IDbConnection db, int orderId)
        {
            string orderItemsQuery = @"SELECT * FROM [dbo].[OrderItems] WHERE [OrderId] = @orderId";

            return db.Query<OrderItem>(orderItemsQuery, new
            {
                orderId = orderId
            }).ToList();
        }

        private static void InsertOrderItems(IDbConnection db, IEnumerable<OrderItemManagementData> items, int orderId)
        {
            string insertOrderItemQuery = @"INSERT INTO [dbo].[OrderItems]([OrderId], [ProductId], [ProductTitle], [Price], [Quantity]) VALUES (@OrderId, @ProductId, @ProductTitle, @Price, @Quantity)";

            foreach (OrderItemManagementData item in items)
            {
                item.OrderId = orderId;

                db.Execute(insertOrderItemQuery, item);
            }
        }
    }
}
