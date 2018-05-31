using LocalPub.Domain.Interfaces;
using LocalPub.Domain.Managers;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Web.Filters;
using LocalPub.Web.Models.Clients;
using System.Web.Mvc;
using static LocalPub.Common.AuthConstants;
using static LocalPub.Common.WebConstants;
using static LocalPub.Common.MessageConstants;
using System.Collections.Generic;

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
        public ActionResult Register()
        {
            ClientRegisterBindingModel clientModel = PrepareClientRegisterBindingModel();
            return this.View(clientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectLoggedInUser]
        public ActionResult Register(ClientRegisterBindingModel clientModel)
        {
            if (!this.ModelState.IsValid)
            {
                clientModel = this.PrepareClientRegisterBindingModel();
                return this.View(clientModel);
            }

            ClientCreateModel client = new ClientCreateModel(clientModel.Username, clientModel.Name, clientModel.Password, clientModel.ClientTypeId);

            bool registerResult = this.clientManager.CreateClient(client);
            if (!registerResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, UserExistingUsername);
                return this.RedirectToAction(nameof(ClientsController.Register), Clients);
            }

            this.TempData.Add(TempDataSuccessMessageKey, RegistrationSuccessful);
            return RedirectToAction(nameof(ClientsController.Login), Clients);
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

        private ClientRegisterBindingModel PrepareClientRegisterBindingModel()
        {
            ClientRegisterBindingModel clientModel = new ClientRegisterBindingModel();
            ICollection<ClientTypeDescription> clientTypes = this.clientManager.GetAllClientTypes();
            clientModel.ClientTypesSelectList = new SelectList(clientTypes, "Id", "Name");
            return clientModel;
        }
    }
}