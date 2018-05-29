﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using PizzaLab.Models;
using PizzaLab.Models.BindingModels;
using PizzaLab.Models.ViewModels.Ingredients;
using PizzaLab.Models.ViewModels.Pizzas;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.Repositories;

namespace PizzaLab.Services.SqlServer
{
    public class PizzaRepository : DbRepository, IPizzaRepository
    {
        public PizzaRepository()
        {
        }

        public PizzaRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<PizzaDetailsViewModel> GetAllPizzas()
        {
            SqlDataReader reader = this.ExecuteReader(
                @"SELECT p.Id AS PizzaId,
                         p.Name AS PizzaName,
                         p.Description,
                         p.PicturePath,
                         i.Id AS IngredientId,
                         i.Name AS IngredientName
                    FROM Pizzas AS p
                    JOIN Pizzas_Ingredients AS [pi]
                      ON [pi].PizzaId = p.Id
                    JOIN Ingredients AS i
                      ON pi.IngredientId = i.Id");

            IDictionary<int, PizzaDetailsViewModel> pizzas = new Dictionary<int, PizzaDetailsViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int pizzaId = reader.GetInt32(0);
                    string pizzaName = reader.GetString(1);
                    string description = reader.GetString(2);
                    string picturePath = reader.GetString(3);
                    int ingredientId = reader.GetInt32(4);
                    string indgredientName = reader.GetString(5);

                    if (!pizzas.ContainsKey(pizzaId))
                    {
                        pizzas[pizzaId] = new PizzaDetailsViewModel(pizzaId, pizzaName, description, picturePath);
                    }

                    pizzas[pizzaId].Ingredients.Add(new IngredientDescription(ingredientId, indgredientName));
                }
            }

            return pizzas.Values;
        }

        public PizzaDetailsViewModel GetPizzaDetailsById(int pizzaId)
        {
            SqlDataReader reader = this.ExecuteReader(
                @"SELECT p.Id AS PizzaId,
                         p.Name AS PizzaName,
                         p.Description,
                         p.PicturePath,
                         i.Id AS IngredientId,
                         i.Name AS IngredientName
                    FROM Pizzas AS p
                    JOIN Pizzas_Ingredients AS [pi]
                      ON [pi].PizzaId = p.Id
                    JOIN Ingredients AS i
                      ON pi.IngredientId = i.Id
                   WHERE p.Id = @pizzaId",
                     new Dictionary<string, object>()
                     {
                         { "@pizzaId", pizzaId }
                     });

            PizzaDetailsViewModel pizza = null;

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string picturePath = reader.GetString(3);
                    int ingredientId = reader.GetInt32(4);
                    string indgredientName = reader.GetString(5);

                    if (pizza == null)
                    {
                        pizza = new PizzaDetailsViewModel(id, name, description, picturePath);
                    }

                    pizza.Ingredients.Add(new IngredientDescription(ingredientId, indgredientName));
                }
            }

            return pizza;
        }

        public MostWantedPizzaViewModel GetMostWantedPizza()
        {
            SqlDataReader reader = this.ExecuteReader(
             @"SELECT TOP 1 p.Id,
                            p.Name,
                            p.Description,
                            p.PicturePath,
                            sales.Count
                     FROM (SELECT p.Id, COUNT(po.PizzaId) AS Count
		                     FROM Pizzas AS p
		                     JOIN PizzaOrders AS po
		                       ON po.PizzaId = p.Id
		                    GROUP BY p.Id
                          ) AS sales
                     JOIN Pizzas AS p
                       ON sales.Id = p.Id
                    ORDER BY sales.Count DESC");

            MostWantedPizzaViewModel pizza = null;

            using (reader)
            {
                while (reader.Read())
                {
                    int pizzaId = reader.GetInt32(0);
                    string pizzaName = reader.GetString(1);
                    string description = reader.GetString(2);
                    string picturePath = reader.GetString(3);
                    int salesCount = reader.GetInt32(4);

                    pizza = new MostWantedPizzaViewModel(pizzaId, pizzaName, description, picturePath, salesCount);
                }
            }

            return pizza;
        }

        public PizzaModel GetPizzaInfoById(int pizzaId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id,
                          Name,
                          Description,
                          PicturePath
                     FROM Pizzas
                    WHERE Id = @pizzaId",
                      new Dictionary<string, object>()
                      {
                          { "@pizzaId", pizzaId }
                      });

            PizzaModel pizza = null;

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string picturePath = reader.GetString(3);

                    if (pizza == null)
                    {
                        pizza = new PizzaModel(id, name, description, picturePath);
                    }
                }
            }

            return pizza;
        }

        public ICollection<DoughTypeDescription> GetAllDoughTypes()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Name
                     FROM DoughTypes");

            IDictionary<int, DoughTypeDescription> doughTypes = new Dictionary<int, DoughTypeDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int doughTypeId = reader.GetInt32(0);
                    string doughTypeName = reader.GetString(1);

                    doughTypes[doughTypeId] = new DoughTypeDescription(doughTypeId, doughTypeName);
                }
            }

            return doughTypes.Values;
        }

        public ICollection<SizeDescription> GetAllSizes()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Name
                     FROM Sizes");

            IDictionary<int, SizeDescription> sizes = new Dictionary<int, SizeDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int sizeId = reader.GetInt32(0);
                    string sizeName = reader.GetString(1);

                    sizes[sizeId] = new SizeDescription(sizeId, sizeName);
                }
            }

            return sizes.Values;
        }

        public ICollection<IngredientDescription> GetAllIngredients()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Name
                     FROM Ingredients");

            IDictionary<int, IngredientDescription> ingredients = new Dictionary<int, IngredientDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int ingredientId = reader.GetInt32(0);
                    string ingredientName = reader.GetString(1);

                    ingredients[ingredientId] = new IngredientDescription(ingredientId, ingredientName);
                }
            }

            return ingredients.Values;
        }

        public ICollection<IngredientDescription> GetAllPizzaIngredients(int pizzaId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT i.Id, i.Name
                     FROM Pizzas AS p
                     JOIN Pizzas_Ingredients AS pi
                       ON pi.PizzaId = p.Id
                     JOIN Ingredients AS i
                       ON pi.IngredientId = i.Id
                    WHERE p.Id = @pizzaId",
                      new Dictionary<string, object>()
                      {
                          { "@pizzaId", pizzaId }
                      });

            IDictionary<int, IngredientDescription> pizzaIngredients = new Dictionary<int, IngredientDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int ingredientId = reader.GetInt32(0);
                    string ingredientName = reader.GetString(1);

                    pizzaIngredients[ingredientId] = new IngredientDescription(ingredientId, ingredientName);
                }
            }

            return pizzaIngredients.Values;
        }

        public decimal GetDoughTypePrice(int doughTypeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Price
                     FROM DoughTypes
                    WHERE Id = @doughTypeId",
                      new Dictionary<string, object>()
                      {
                          { "@doughTypeId", doughTypeId }
                      });

            decimal doughTypePrice = default(decimal);

            using (reader)
            {
                while (reader.Read())
                {
                    doughTypePrice = reader.GetDecimal(0);
                }
            }

            return doughTypePrice;
        }

        public decimal GetSizePrice(int sizeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Price
                     FROM Sizes
                    WHERE Id = @sizeId",
                      new Dictionary<string, object>()
                      {
                          { "@sizeId", sizeId }
                      });

            decimal sizePrice = default(decimal);

            using (reader)
            {
                while (reader.Read())
                {
                    sizePrice = reader.GetDecimal(0);
                }
            }

            return sizePrice;
        }

        public decimal GetIngredientsPrice(ICollection<int> ingredientIds)
        {
            StringBuilder parameters = new StringBuilder();
            IDictionary<string, object> ingredients = new Dictionary<string, object>();
            int i = 1;

            foreach (int ingredientId in ingredientIds)
            {
                parameters.Append("@ingredientId" + i.ToString() + ",");
                ingredients["@ingredientId" + i.ToString()] = ingredientId;
                i++;
            }

            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Price
                     FROM Ingredients
                    WHERE Id IN (" + parameters.ToString().TrimEnd(',') + ")",
                      new Dictionary<string, object>(ingredients));

            decimal ingredientsPrice = default(decimal);

            using (reader)
            {
                while (reader.Read())
                {
                    ingredientsPrice += reader.GetDecimal(0);
                }
            }

            return ingredientsPrice;
        }

        public bool CreateOrder(int userId, decimal totalPrice, ShoppingCart shoppingCart)
        {
            int orderId = (int)this.ExecuteScalar(
                @"INSERT INTO Orders (UserId, TotalPrice)
                       OUTPUT INSERTED.ID
                       VALUES (@userId, @totalPrice)",
                          new Dictionary<string, object>()
                          {
                                  { "@userId", userId },
                                  { "@totalPrice", totalPrice },
                          });

            int recordsInserted = 0;

            foreach (PizzaOrderBindingModel pizza in shoppingCart.Pizzas)
            {
                recordsInserted += this.ExecuteNonQuery(
                  @"INSERT INTO PizzaOrders (PizzaId, OrderId, DoughTypeId, SizeId)
                         VALUES (@pizzaId, @orderId, @doughTypeId, @sizeId)",
                            new Dictionary<string, object>()
                            {
                               { "@pizzaId", pizza.PizzaId },
                               { "@orderId", orderId },
                               { "@doughTypeId", pizza.DoughTypeId },
                               { "@sizeId", pizza.SizeId },
                            });
            }

            return recordsInserted > 0;
        }
    }
}