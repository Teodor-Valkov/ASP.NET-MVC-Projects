using System.Collections.Generic;
using System.Web.Mvc;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.Managers;
using PaymentSystem.Extensions;
using PaymentSystem.Filters;
using PaymentSystem.Models.ViewModels.Accounts;

namespace PaymentSystem.Controllers
{
    [AuthorizeUser]
    public class AccountsController : Controller
    {
        private IAccountManager accountManager;

        public AccountsController()
            : this(new AccountManager())
        {
        }

        public AccountsController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public ActionResult Index()
        {
            int userId = this.User.GetUserId();
            ICollection<AccountViewModel> accounts = this.accountManager.GetAllAccountsByUserId(userId);

            return this.View(accounts);
        }
    }
}