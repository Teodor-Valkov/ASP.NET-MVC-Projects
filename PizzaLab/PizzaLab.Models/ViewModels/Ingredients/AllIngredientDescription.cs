namespace PizzaLab.Models.ViewModels.Ingredients

{
    public class AllIngredientDescription : IngredientDescription
    {
        public AllIngredientDescription(int id, string name, bool isSelected)
            : base(id, name)
        {
            this.IsSelected = isSelected;
        }

        public bool IsSelected { get; set; }
    }
}
