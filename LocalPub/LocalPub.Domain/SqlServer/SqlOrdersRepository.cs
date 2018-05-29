using LocalPub.Domain.Interfaces;
using LocalPub.Models.BindingModels;
using LocalPub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LocalPub.Domain.SqlServer
{
    public class SqlOrdersRepository : DbRepository, IOrdersRepository
    {
        public SqlOrdersRepository()
        {
        }

        public SqlOrdersRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<OrderViewModel> GetAllOrdersForClient(int clientId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT o.Id AS OrderId,
                          o.OrderDate,
                          o.IsCancelled,
                          m.Name AS MealName
                     FROM Orders AS o
                     JOIN OrderMeals AS om
                       ON om.OrderId = o.Id
                     JOIN Meals AS m
                       ON om.MealId = m.Id
                    WHERE ClientId = @clientId
                    ORDER BY o.OrderDate",
                      new Dictionary<string, object>
                      {
                          { "@clientId", clientId }
                      });

            IDictionary<int, OrderViewModel> orders = new Dictionary<int, OrderViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int orderId = reader.GetInt32(0);
                    DateTime orderDate = reader.GetDateTime(1);
                    bool isCancelled = reader.GetBoolean(2);
                    string mealName = reader.GetString(3);

                    if (!orders.ContainsKey(orderId))
                    {
                        orders[orderId] = new OrderViewModel(orderId, orderDate, isCancelled);
                    }

                    orders[orderId].Meals.Add(mealName);
                }
            }

            return orders.Values;
        }

        public bool IsCurrentClientSameAsOrderClient(int orderId, int clientId)
        {
            int orderClientId = (int)this.ExecuteScalar(
                     @"SELECT ClientId
                         FROM Orders
                        WHERE Id = @orderId",
                          new Dictionary<string, object>()
                          {
                              { "@orderId", orderId }
                          });

            return orderClientId == clientId;
        }

        public bool IsClientAlreadyMadeOrder(int clientId, DateTime date)
        {
            int ordersCount = (int)this.ExecuteScalar(
                   @"SELECT count(*) as OrdersCount
                       FROM Orders
                      WHERE OrderDate = @date
                        AND ClientId = @clientId
                        AND Orders.IsCancelled = 0",
                        new Dictionary<string, object>()
                        {
                            {"@date", date },
                            {"@clientId", clientId }
                        });

            return ordersCount > 0;
        }

        public bool CancelOrder(int orderId)
        {
            int recordsUpdated = this.ExecuteNonQuery(
                      @"UPDATE Orders
                           SET IsCancelled = 1
                         WHERE Id = @orderId",
                           new Dictionary<string, object>()
                           {
                               {"@orderId", orderId },
                           });

            return recordsUpdated > 0;
        }

        public bool SaveOrder(OrderBindingModel order)
        {
            if (!order.AppetizerId.HasValue && !order.MainCourseId.HasValue && !order.DessertId.HasValue)
            {
                if (order is PrivilegedUserOrderBindingModel)
                {
                    PrivilegedUserOrderBindingModel privilegedOrder = order as PrivilegedUserOrderBindingModel;

                    if (!privilegedOrder.AdditionalDessertId.HasValue)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            int orderId = (int)this.ExecuteScalar(
               @"INSERT INTO Orders (ClientId, OrderDate, IsCancelled)
                 OUTPUT INSERTED.ID
                 VALUES (@clientId, @date, 0)",
                    new Dictionary<string, object>()
                    {
                        {"@date", order.OrderDate },
                        {"@clientId", order.ClientId }
                    });

            StringBuilder query = new StringBuilder();

            IDictionary<string, object> queryParams = new Dictionary<string, object>()
            {
                { "@orderId", orderId }
            };

            if (order.AppetizerId.HasValue)
            {
                query.AppendLine(@"INSERT INTO OrderMeals (OrderId, MealId)
                                   VALUES (@orderId, @appetizerId)");

                queryParams.Add("@appetizerId", order.AppetizerId.Value);
            }

            if (order.MainCourseId.HasValue)
            {
                query.AppendLine(@"INSERT INTO OrderMeals (OrderId, MealId)
                                   VALUES (@orderId, @mainCourseId)");

                queryParams.Add("@mainCourseId", order.MainCourseId.Value);
            }

            if (order.DessertId.HasValue)
            {
                query.AppendLine(@"INSERT INTO OrderMeals (OrderId, MealId)
                                   VALUES (@orderId, @dessertId)");

                queryParams.Add("@dessertId", order.DessertId.Value);
            }

            if (order is PrivilegedUserOrderBindingModel)
            {
                PrivilegedUserOrderBindingModel privilegedOrder = order as PrivilegedUserOrderBindingModel;

                if (privilegedOrder.AdditionalDessertId.HasValue)
                {
                    query.AppendLine(@"INSERT INTO OrderMeals (OrderId, MealId)
                                       VALUES (@orderId, @additionalDessertId)");

                    queryParams.Add("@additionalDessertId", privilegedOrder.AdditionalDessertId.Value);
                }
            }

            int recordsInserted = this.ExecuteNonQuery(query.ToString(), queryParams);
            return recordsInserted > 0;
        }
    }
}