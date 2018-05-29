using System.Collections.Generic;
using PizzaLab.Models.ViewModels.Pizzas;

namespace PizzaLab.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.Pizzas = new List<PizzaWithPrice>();
        }

        public ICollection<PizzaWithPrice> Pizzas { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (PizzaWithPrice pizza in this.Pizzas)
                {
                    totalPrice += pizza.Price;
                }

                return totalPrice;
            }
        }
    }
}
