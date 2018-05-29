using System;
using System.Collections.Generic;

namespace LocalPub.Models.ViewModels
{
    public class MealDetailsViewModel
    {
        public MealDetailsViewModel(DateTime orderDate, string mealName)
        {
            this.OrderDate = orderDate;
            this.MealName = mealName;
            this.ClientNames = new List<string>();
        }

        public int MealsCount { get; set; }

        public DateTime OrderDate { get; private set; }

        public string MealName { get; private set; }

        public ICollection<string> ClientNames { get; private set; }
    }
}