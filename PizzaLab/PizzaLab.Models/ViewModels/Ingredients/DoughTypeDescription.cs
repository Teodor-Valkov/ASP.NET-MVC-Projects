namespace PizzaLab.Models.ViewModels.Ingredients
{
    public class DoughTypeDescription : IdentifiedObject
    {
        public DoughTypeDescription(int id, string name)
            : base(id)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
