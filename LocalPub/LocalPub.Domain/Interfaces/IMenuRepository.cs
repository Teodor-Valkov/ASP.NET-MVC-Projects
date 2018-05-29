using LocalPub.Models;
using LocalPub.Models.ViewModels;
using System.Collections.Generic;

namespace LocalPub.Domain.Interfaces
{
    public interface IMenuRepository : IDbRepository
    {
        ICollection<MostOrderedMealViewModel> GetMostOrderedMeals();

        ICollection<MealDetailsViewModel> GetMealsByDate();

        OrderMenuViewModel GetOrderMenu();
    }
}