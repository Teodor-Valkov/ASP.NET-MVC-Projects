namespace LocalPub.Models.ViewModels
{
    public class MostOrderedMealViewModel
    {
        public MostOrderedMealViewModel(string mealName, int ordersCount)
        {
            this.MealName = mealName;
            this.OrdersCount = ordersCount;
        }

        public string MealName { get; private set; }

        public int OrdersCount { get; private set; }
    }
}