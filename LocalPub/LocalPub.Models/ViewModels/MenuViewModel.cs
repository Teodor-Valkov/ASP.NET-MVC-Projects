using System.Collections.Generic;

namespace LocalPub.Models
{
    public class OrderMenuViewModel
    {
        public OrderMenuViewModel(ICollection<MealDescription> appetizers, ICollection<MealDescription> mainCourses, ICollection<MealDescription> desserts)
        {
            this.Appetizers = appetizers;
            this.MainCourses = mainCourses;
            this.Desserts = desserts;
        }

        public ICollection<MealDescription> Appetizers { get; private set; }

        public ICollection<MealDescription> MainCourses { get; private set; }

        public ICollection<MealDescription> Desserts { get; private set; }
    }
}