using System.Web.Mvc;
using System.Web.Routing;
using PaymentSystem.Controllers;

namespace PaymentSystem.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult userLoginRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = WebConstants.UsersController, action = nameof(UsersController.Login) }));
            filterContext.Result = userLoginRoute;
        }
    }
}