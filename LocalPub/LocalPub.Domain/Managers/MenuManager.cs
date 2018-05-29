using LocalPub.Domain.Interfaces;
using LocalPub.Domain.SqlServer;
using LocalPub.Models.ViewModels;
using System.Collections.Generic;

namespace LocalPub.Domain.Managers
{
    public class MenuManager : IMenuManager
    {
        private IMenuRepository menuRepository;

        public MenuManager()
            : this(new SqlMenuRepository())
        {
        }

        public MenuManager(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }

        public ICollection<MostOrderedMealViewModel> GetMostOrderedMeals()
        {
            using (this.menuRepository)
            {
                ICollection<MostOrderedMealViewModel> mostOrderedMeals = this.menuRepository.GetMostOrderedMeals();
                return mostOrderedMeals;
            }
        }

        public ICollection<MealDetailsViewModel> GetMealsByDate()
        {
            using (this.menuRepository)
            {
                ICollection<MealDetailsViewModel> mealsByDate = this.menuRepository.GetMealsByDate();
                return mealsByDate;
            }
        }
    }
}