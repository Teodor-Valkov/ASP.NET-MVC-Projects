using System.Collections.Generic;
using PizzaLab.Models;
using PizzaLab.Models.ViewModels;
using PizzaLab.Models.ViewModels.Pizzas;

namespace PizzaLab.Services.Interfaces
{
    public interface IPizzaManager
    {
        ICollection<PizzaDetailsViewModel> GetAllPizzas();

        MostWantedPizzaViewModel GetMostWantedPizza();

        PizzaDetailsViewModel GetPizzaDetailsById(int pizzaId);

        PizzaOrderViewModel GetPizzaToOrder(int id);

        ShoppingCartViewModel GetShoppingCart(ShoppingCart shoppingCart);

        bool FinishOrder(int userId, decimal totalPrice, ShoppingCart shoppingCart);
    }
}