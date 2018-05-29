using LocalPub.Web.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using static LocalPub.Common.WebConstants;

namespace LocalPub.Web.Filters
{
    public class RedirectLoggedInUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToRouteResult ordersIndexRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = Orders, action = nameof(OrdersController.Index) }));
                filterContext.Result = ordersIndexRoute;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}