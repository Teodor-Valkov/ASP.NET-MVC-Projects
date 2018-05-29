using System.Web.Mvc;
using PizzaLab.Models;
using PizzaLab.Models.BindingModels;
using PizzaLab.Models.ViewModels;
using PizzaLab.Models.ViewModels.Users;
using PizzaLab.Models.ViewModels.Pizzas;
using PizzaLab.Server.Controllers;
using PizzaLab.Server.Filters;
using PizzaLab.Server.ModelBinders;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.Managers;
using PizzaLab.Web.Extensions;
using static PizzaLab.Common.Messages;
using static PizzaLab.Web.WebConstants;

namespace PizzaLab.Web.Controllers
{
    [AuthorizeUser]
    public class OrdersController : Controller
    {
        private IPizzaManager pizzaManager;

        public OrdersController()
            : this(new PizzaManager())
        {
        }

        public OrdersController(IPizzaManager pizzaManager)
        {
            this.pizzaManager = pizzaManager;
        }

        [HttpGet]
        public ActionResult OrderPizza(int id)
        {
            PizzaOrderViewModel pizza = this.pizzaManager.GetPizzaToOrder(id);
            ViewBag.DoughTypes = new SelectList(pizza.DoughTypes, "Id", "Name", "-- none --");
            ViewBag.Sizes = new SelectList(pizza.Sizes, "Id", "Name", "-- none --");

            return this.View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPizza([ModelBinder(typeof(PizzaIngredientsModelBinder))]PizzaOrderBindingModel pizza)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(OrderPizza), pizza.PizzaId);
            }

            ShoppingCart shoppingCart = this.Session.GetShoppingCart<ShoppingCart>(UserShoppingCartSessionKey);
            shoppingCart.Pizzas.Add(pizza);

            this.Session.SetShoppingCart(UserShoppingCartSessionKey, shoppingCart);
            TempData.AddSuccessMessage(PizzaAddedToShoppingCart);

            return this.RedirectToAction(nameof(MenuController.Index), Home);
        }

        public ActionResult Checkout()
        {
            ShoppingCart shoppingCart = this.Session.GetShoppingCart<ShoppingCart>(UserShoppingCartSessionKey);

            ShoppingCartViewModel shoppingCartViewModel = this.pizzaManager.GetShoppingCart(shoppingCart);

            return this.View(shoppingCartViewModel);
        }

        [HttpPost]
        public ActionResult FinishOrder(decimal totalPrice)
        {
            ShoppingCart shoppingCart = HttpContext.Session.GetShoppingCart<ShoppingCart>(UserShoppingCartSessionKey);

            if (shoppingCart.Pizzas.Count == 0)
            {
                return this.RedirectToAction(nameof(HomeController.Index), Home);
            }

            UserModel user = this.Session[UserSessionKey] as UserModel;

            bool finishOrderResult = this.pizzaManager.FinishOrder(user.Id, totalPrice, shoppingCart);
            if (finishOrderResult)
            {
                HttpContext.Session.Remove(UserShoppingCartSessionKey);
                this.TempData.AddSuccessMessage(OrderSuccessful);
            }

            return this.RedirectToAction(nameof(HomeController.Index), Home);
        }
    }
}