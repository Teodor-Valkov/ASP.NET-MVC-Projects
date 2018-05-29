using System.Web.Mvc;
using System.Web.Routing;
using PizzaLab.Web;
using PizzaLab.Web.Controllers;

namespace PizzaLab.Server.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult userLoginRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = WebConstants.Users, action = nameof(UsersController.Login) }));
            filterContext.Result = userLoginRoute;
        }
    }
}