using PizzaLab.Models.ViewModels.Ingredients;
using System.Collections.Generic;

namespace PizzaLab.Models.ViewModels.Pizzas
{
    public class PizzaOrderViewModel : PizzaModel
    {
        public PizzaOrderViewModel(
            int id,
            string name,
            string description,
            string pictureUrl,
            ICollection<DoughTypeDescription> doughTypes,
            ICollection<SizeDescription> sizes,
            ICollection<AllIngredientDescription> allIngredients)
            : base(id, name, description, pictureUrl)
        {
            this.DoughTypes = doughTypes;
            this.Sizes = sizes;
            this.AllIngredients = new List<AllIngredientDescription>(allIngredients);
        }

        public ICollection<DoughTypeDescription> DoughTypes { get; set; }

        public ICollection<SizeDescription> Sizes { get; set; }

        public ICollection<AllIngredientDescription> AllIngredients { get; set; }
    }
}
