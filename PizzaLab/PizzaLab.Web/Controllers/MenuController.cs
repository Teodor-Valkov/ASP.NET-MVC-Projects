using System.Collections.Generic;
using System.Web.Mvc;
using PizzaLab.Models.ViewModels.Pizzas;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.Managers;

namespace PizzaLab.Server.Controllers
{
    public class MenuController : Controller
    {
        private IPizzaManager pizzaManager;

        public MenuController()
            : this(new PizzaManager())
        {
        }

        public MenuController(IPizzaManager pizzaManager)
        {
            this.pizzaManager = pizzaManager;
        }

        public ActionResult Index()
        {
            ICollection<PizzaDetailsViewModel> pizzas = this.pizzaManager.GetAllPizzas();
            return this.View(pizzas);
        }

        public ActionResult Details(int id)
        {
            PizzaDetailsViewModel pizza = this.pizzaManager.GetPizzaDetailsById(id);
            return this.View(pizza);
        }

        [ChildActionOnly]
        public ActionResult MostWantedPizza()
        {
            MostWantedPizzaViewModel pizza = this.pizzaManager.GetMostWantedPizza();
            return this.PartialView(pizza);
        }
    }
}