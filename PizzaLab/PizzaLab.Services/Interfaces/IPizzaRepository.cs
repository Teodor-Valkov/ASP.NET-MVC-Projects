using System.Collections.Generic;
using PizzaLab.Models;
using PizzaLab.Models.ViewModels.Ingredients;
using PizzaLab.Models.ViewModels.Pizzas;

namespace PizzaLab.Services.Interfaces
{
    public interface IPizzaRepository : IDbRepository
    {
        ICollection<PizzaDetailsViewModel> GetAllPizzas();

        PizzaDetailsViewModel GetPizzaDetailsById(int pizzaId);

        MostWantedPizzaViewModel GetMostWantedPizza();

        PizzaModel GetPizzaInfoById(int pizzaId);

        ICollection<DoughTypeDescription> GetAllDoughTypes();

        ICollection<SizeDescription> GetAllSizes();

        ICollection<IngredientDescription> GetAllIngredients();

        ICollection<IngredientDescription> GetAllPizzaIngredients(int pizzaId);

        decimal GetSizePrice(int sizeId);

        decimal GetDoughTypePrice(int doughTypeId);

        decimal GetIngredientsPrice(ICollection<int> ingredients);

        bool CreateOrder(int userId, decimal totalPrice, ShoppingCart shoppingCart);
    }
}