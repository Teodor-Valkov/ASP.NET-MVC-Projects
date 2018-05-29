using LocalPub.Domain.Interfaces;
using LocalPub.Models;
using LocalPub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LocalPub.Domain.SqlServer
{
    public class SqlMenuRepository : DbRepository, IMenuRepository
    {
        public SqlMenuRepository()
        {
        }

        public SqlMenuRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<MostOrderedMealViewModel> GetMostOrderedMeals()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT TOP 10
                          m.Name,
                          COUNT(om.OrderId) as OrdersCount
                     FROM Meals as m
                     JOIN OrderMeals AS om
                       ON m.Id = om.MealId
                    GROUP BY m.Name
                    ORDER BY OrdersCount DESC");

            IList<MostOrderedMealViewModel> mostOrderedMeals = new List<MostOrderedMealViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    string mealName = reader.GetString(0);
                    int ordersCount = reader.GetInt32(1);

                    mostOrderedMeals.Add(new MostOrderedMealViewModel(mealName, ordersCount));
                }
            }

            return mostOrderedMeals;
        }

        public ICollection<MealDetailsViewModel> GetMealsByDate()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT mealCounts.OrderDate,
                          mealCounts.MealName,
                          mealCounts.MealsCount,
                          c.Username,
                          c.Name AS ClientName
                     FROM
                          (SELECT o.OrderDate,
                     		      o.ClientId,
                     		      m.Name AS MealName,
                     		      COUNT(o.ClientId) AS MealsCount
                     	     FROM Meals AS m
                     	     JOIN OrderMeals AS om
                     	       ON om.MealId = m.Id
                     	     JOIN Orders AS o
                     	       ON om.OrderId = o.Id
                     	    WHERE o.IsCancelled = 0
                     	    GROUP BY o.OrderDate, m.Name, o.ClientId
                          ) AS mealCounts
                     JOIN Clients AS c
                       ON mealCounts.ClientId = c.Id
                    ORDER BY mealCounts.OrderDate, mealCounts.MealName, mealCounts.ClientId");

            IDictionary<Tuple<DateTime, string>, MealDetailsViewModel> mealsByDate = new Dictionary<Tuple<DateTime, string>, MealDetailsViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    DateTime orderDate = reader.GetDateTime(0);
                    string mealName = reader.GetString(1);
                    int mealsCount = reader.GetInt32(2);
                    string clientUsername = reader.GetString(3);
                    string clientName = reader.GetString(4);

                    string clientNameRepresentation = this.CombineUsernameAndName(clientUsername, clientName);

                    Tuple<DateTime, string> key = new Tuple<DateTime, string>(orderDate, mealName);

                    if (!mealsByDate.ContainsKey(key))
                    {
                        mealsByDate[key] = new MealDetailsViewModel(orderDate, mealName);
                    }

                    mealsByDate[key].MealsCount += mealsCount;
                    mealsByDate[key].ClientNames.Add(clientNameRepresentation);
                }
            }

            return mealsByDate.Values;
        }

        public OrderMenuViewModel GetOrderMenu()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT mt.Name AS MealType,
                          m.Id as MealId,
                          m.Name AS MealName
                     FROM Meals AS m
                     JOIN MealTypes AS mt
                       ON m.MealTypeId = mt.Id");

            ICollection<MealDescription> appetizers = new List<MealDescription>();
            ICollection<MealDescription> mainCourses = new List<MealDescription>();
            ICollection<MealDescription> desserts = new List<MealDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    string mealTypeName = reader.GetString(0);
                    int mealId = reader.GetInt32(1);
                    string mealName = reader.GetString(2);

                    MealDescription meal = new MealDescription(mealId, mealName);

                    switch (mealTypeName)
                    {
                        case "Предястие":
                            appetizers.Add(meal);
                            break;

                        case "Основно ястие":
                            mainCourses.Add(meal);
                            break;

                        case "Десерт":
                            desserts.Add(meal);
                            break;

                        default:
                            throw new InvalidOperationException("No such meal type: " + mealTypeName);
                    }
                }
            }

            OrderMenuViewModel menu = new OrderMenuViewModel(appetizers, mainCourses, desserts);
            return menu;
        }

        private string CombineUsernameAndName(string username, string name)
        {
            string resultName = string.Format("{0} ({1})", string.IsNullOrEmpty(username) ? "[no name]" : name, username);
            return resultName;
        }
    }
}