using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Domain.Managers;
using BirthdaySystem.Models.BindingModels.Employees;
using BirthdaySystem.Models.Models.Employees;
using BirthdaySystem.Web.Filters;
using System.Web.Mvc;
using static BirthdaySystem.Common.AuthConstants;
using static BirthdaySystem.Common.MessageConstants;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeManager employeeManager;

        public EmployeesController()
            : this(new EmployeeManager())
        {
        }

        public EmployeesController(IEmployeeManager employeeManager)
        {
            this.employeeManager = employeeManager;
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
        public ActionResult Register(EmployeeRegisterBindingModel employeeModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(employeeModel);
            }

            bool registerResult = this.employeeManager.CreateEmployee(employeeModel);
            if (!registerResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, ExistingUsernameError);
                return this.View(employeeModel);
            }

            this.TempData.Add(TempDataSuccessMessageKey, RegistrationSuccessful);
            return RedirectToAction(nameof(EmployeesController.Login), Employees);
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
        public ActionResult Login(EmployeeLoginBindingModel employeeModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(employeeModel);
            }

            EmployeeModel employee = this.employeeManager.GetEmployee(employeeModel);
            if (employee == null)
            {
                this.TempData.Add(TempDataErrorMessageKey, InvalidCredentials);
                return this.View(employeeModel);
            }

            this.Session[SessionUserKey] = employee;
            this.TempData.Add(TempDataSuccessMessageKey, LoginSuccessful);

            return RedirectToAction(nameof(VotingsController.Index), Votings);
        }

        [AuthorizeEmployee]
        public ActionResult Logout()
        {
            this.Session[SessionUserKey] = null;
            this.TempData.Add(TempDataSuccessMessageKey, LogoutSuccessful);

            return RedirectToAction(nameof(HomeController.Index), Home);
        }
    }
}