using System.Web.Mvc;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction(nameof(EmployeesController.Login), Employees);
        }
    }
}