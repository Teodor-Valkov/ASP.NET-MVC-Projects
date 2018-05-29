using System.Web.Mvc;

namespace PaymentSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction(nameof(UsersController.Login), WebConstants.UsersController);
        }
    }
}