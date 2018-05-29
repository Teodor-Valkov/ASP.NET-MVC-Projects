using BirthdaySystem.Web.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Filters
{
    public class AuthorizeEmployeeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult employeesLoginRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = Employees, action = nameof(EmployeesController.Login) }));
            filterContext.Result = employeesLoginRoute;
        }
    }
}