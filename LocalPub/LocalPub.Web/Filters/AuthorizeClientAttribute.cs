using System.Web.Mvc;
using System.Web.Routing;
using LocalPub.Web.Controllers;
using static LocalPub.Common.WebConstants;

namespace LocalPub.Web.Filters
{
    public class AuthorizeClientAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult clientsLoginRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = Clients, action = nameof(ClientsController.Login) }));
            filterContext.Result = clientsLoginRoute;
        }
    }
}