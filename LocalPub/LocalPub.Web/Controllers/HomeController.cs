using System.Web.Mvc;
using static LocalPub.Common.WebConstants;

namespace LocalPub.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction(nameof(ClientsController.Login), Clients);
        }
    }
}