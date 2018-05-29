namespace PizzaLab.Models.ViewModels.Pizzas
{
    public class PizzaWithPrice : PizzaModel
    {
        public PizzaWithPrice(int id, string name, string description, string pictureUrl, decimal price)
            :base(id, name, description, pictureUrl)
        {
            this.Price = price;
        }

        public decimal Price { get; set; }
    }
}
