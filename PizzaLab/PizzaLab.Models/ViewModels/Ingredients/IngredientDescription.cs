namespace PizzaLab.Models.ViewModels.Ingredients
{
    public class IngredientDescription : IdentifiedObject
    {
        public IngredientDescription(int id, string name)
            : base(id)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
