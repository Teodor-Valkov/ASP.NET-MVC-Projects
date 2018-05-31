using System.Web.Mvc;
using PaymentSystem.Common;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.Managers;
using PaymentSystem.Extensions;
using PaymentSystem.Filters;
using PaymentSystem.Models.BindingModels.Users;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Controllers
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
        [RedirectLoggedInUser]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectLoggedInUser]
        public ActionResult Register(UserRegisterBindingModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            bool registerResult = this.userManager.CreateUser(userModel);
            if (!registerResult)
            {
                this.TempData.AddErrorMessage(MessageConstants.ExistingUsernameError);
                return this.View(userModel);
            }

            this.TempData.AddSuccessMessage(MessageConstants.RegistrationSuccessful);
            return RedirectToAction(nameof(UsersController.Login), WebConstants.UsersController);
        }

        [HttpGet]
        [RedirectLoggedInUser]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectLoggedInUser]
        public ActionResult Login(UserLoginBindingModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            UserModel user = this.userManager.GetUser(userModel);
            if (user == null)
            {
                this.TempData.AddErrorMessage(MessageConstants.InvalidCredentials);
                return this.View(userModel);
            }

            this.Session[AuthConstants.SessionUserKey] = user;
            this.TempData.AddSuccessMessage(MessageConstants.LoginSuccessful);

            return this.RedirectToAction(nameof(AccountsController.Index), WebConstants.AccountsController);
        }

        [AuthorizeUser]
        public ActionResult Logout()
        {
            this.Session[AuthConstants.SessionUserKey] = null;
            this.TempData.AddSuccessMessage(MessageConstants.LogoutSuccessful);

            return this.RedirectToAction(nameof(HomeController.Index), WebConstants.HomeController);
        }
    }
}