using System.Collections.Generic;
using LocalPub.Models.ViewModels;

namespace LocalPub.Domain.Interfaces
{
    public interface IMenuManager
    {
        ICollection<MostOrderedMealViewModel> GetMostOrderedMeals();

        ICollection<MealDetailsViewModel> GetMealsByDate();
    }
}