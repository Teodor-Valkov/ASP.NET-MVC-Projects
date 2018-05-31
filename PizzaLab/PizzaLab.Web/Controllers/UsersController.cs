using System.Web.Mvc;
using PizzaLab.Models.BindingModels;
using PizzaLab.Models.Models.Users;
using PizzaLab.Server.Filters;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.Managers;
using PizzaLab.Web.Filters;
using static PizzaLab.Common.Messages;
using static PizzaLab.Web.WebConstants;

namespace PizzaLab.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserManager userManager;

        public UsersController()
            : this(new UserManager())
        {
        }

        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [RejectLoggedInUser]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectLoggedInUser]
        public ActionResult Register(RegisterUserBindingModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            bool registerResult = this.userManager.CreateUser(userModel);
            if (!registerResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, UserExistingUsername);
                return this.View(userModel);
            }

            this.TempData.Add(TempDataSuccessMessageKey, RegistrationSuccessful);
            return this.RedirectToAction(nameof(UsersController.Login), Users);
        }

        [HttpGet]
        [RejectLoggedInUser]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectLoggedInUser]
        public ActionResult Login(LoginUserBindingModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            UserModel user = this.userManager.GetUser(userModel);
            if (user == null)
            {
                this.TempData.Add(TempDataErrorMessageKey, InvalidCredentials);
                return this.View(userModel);
            }

            this.Session[UserSessionKey] = user;
            this.TempData.Add(TempDataSuccessMessageKey, LoginSuccessful);
            return this.RedirectToAction(nameof(HomeController.Index), Home);
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Logout()
        {
            this.Session[UserSessionKey] = null;

            if (this.Session[UserShoppingCartSessionKey] != null)
            {
                HttpContext.Session.Remove(UserShoppingCartSessionKey);
            }

            this.TempData.Add(TempDataSuccessMessageKey, LogoutSuccessful);
            return this.RedirectToAction(nameof(HomeController.Index), Home);
        }
    }
}