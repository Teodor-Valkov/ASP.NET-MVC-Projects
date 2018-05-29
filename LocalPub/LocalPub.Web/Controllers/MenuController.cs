using LocalPub.Domain.Interfaces;
using LocalPub.Domain.Managers;
using LocalPub.Models.ViewModels;
using LocalPub.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using static LocalPub.Common.WebConstants;

namespace LocalPub.Web.Controllers
{
    [AuthorizeClient]
    public class MenuController : Controller
    {
        private IMenuManager menuManager;

        public MenuController()
            : this(new MenuManager())
        {
        }

        public MenuController(IMenuManager menuManager)
        {
            this.menuManager = menuManager;
        }

        [HttpGet]
        public ActionResult MostOrderedMeals()
        {
            ICollection<MostOrderedMealViewModel> mostOrderedMeals = this.menuManager.GetMostOrderedMeals();
            return this.View(mostOrderedMeals);
        }

        [HttpGet]
        [AuthorizeClient(Roles = PriviligedClient)]
        public ActionResult MealsByDate()
        {
            ICollection<MealDetailsViewModel> mealsByDate = this.menuManager.GetMealsByDate();
            return this.View(mealsByDate);
        }
    }
}