using System.Web.Mvc;
using System.Web.Routing;
using PizzaLab.Web.Controllers;
using static PizzaLab.Web.WebConstants;

namespace PizzaLab.Web.Filters
{
    public class RejectLoggedInUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RouteValueDictionary homeRoute = new RouteValueDictionary(new { controller = Home, action = nameof(HomeController.Index) });
                filterContext.Result = new RedirectToRouteResult(homeRoute);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}