using LocalPub.Domain.Interfaces;
using LocalPub.Domain.Managers;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Web.Filters;
using System.Web.Mvc;
using static LocalPub.Common.AuthConstants;
using static LocalPub.Common.WebConstants;
using static LocalPub.Common.MessageConstants;

namespace LocalPub.Web.Controllers
{
    public class ClientsController : Controller
    {
        private IClientManager clientManager;

        public ClientsController()
            : this(new ClientManager())
        {
        }

        public ClientsController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
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
        public ActionResult Login(ClientLoginBindingModel clientModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(clientModel);
            }

            ClientModel client = this.clientManager.GetClient(clientModel);
            if (client == null)
            {
                this.TempData.Add(TempDataErrorMessageKey, InvalidCredentials);
                return this.View(clientModel);
            }

            this.Session[SessionUserKey] = client;
            this.TempData.Add(TempDataSuccessMessageKey, LoginSuccessful);

            return this.RedirectToAction(nameof(OrdersController.Index), Orders);
        }

        [AuthorizeClient]
        public ActionResult Logout()
        {
            this.Session[SessionUserKey] = null;
            this.TempData.Add(TempDataSuccessMessageKey, LogoutSuccessful);

            return this.RedirectToAction(nameof(HomeController.Index), Home);
        }
    }
}