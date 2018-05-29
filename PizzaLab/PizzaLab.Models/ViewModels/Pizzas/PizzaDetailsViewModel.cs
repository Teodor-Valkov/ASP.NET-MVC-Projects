using System.Collections.Generic;
using PizzaLab.Models.ViewModels.Ingredients;

namespace PizzaLab.Models.ViewModels.Pizzas
{
    public class PizzaDetailsViewModel : PizzaModel
    {
        public PizzaDetailsViewModel(int id, string name, string description, string pictureUrl)
            : base(id, name, description, pictureUrl)
        {
            this.Ingredients = new List<IngredientDescription>();
        }

        public ICollection<IngredientDescription> Ingredients { get; private set; }
    }
}
