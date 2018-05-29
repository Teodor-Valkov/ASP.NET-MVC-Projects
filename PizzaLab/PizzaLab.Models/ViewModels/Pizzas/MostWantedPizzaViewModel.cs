namespace PizzaLab.Models.ViewModels.Pizzas
{
    public class MostWantedPizzaViewModel : PizzaModel
    {
        public MostWantedPizzaViewModel(int id, string name, string description, string pictureUrl, int sales)
            : base(id, name, description, pictureUrl)
        {
            this.Sales = sales;
        }

        public int Sales { get; private set; }
    }
}
