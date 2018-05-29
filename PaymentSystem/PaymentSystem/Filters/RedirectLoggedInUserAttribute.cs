using System.Web.Mvc;
using System.Web.Routing;
using PaymentSystem.Controllers;

namespace PaymentSystem.Filters
{
    public class RedirectLoggedInUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToRouteResult accountsIndexRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = WebConstants.AccountsController, action = nameof(AccountsController.Index) }));
                filterContext.Result = accountsIndexRoute;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}