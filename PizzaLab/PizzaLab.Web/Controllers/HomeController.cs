using System.Web.Mvc;

namespace PizzaLab.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}