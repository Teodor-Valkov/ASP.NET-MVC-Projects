using System.Collections.Generic;
using PizzaLab.Models.BindingModels;

namespace PizzaLab.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Pizzas = new List<PizzaOrderBindingModel>();
        }

        public IList<PizzaOrderBindingModel> Pizzas { get; set; }
    }
}
