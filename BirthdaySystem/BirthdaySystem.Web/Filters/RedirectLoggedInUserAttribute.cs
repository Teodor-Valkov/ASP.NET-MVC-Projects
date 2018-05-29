using BirthdaySystem.Web.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Filters
{
    public class RedirectLoggedInUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToRouteResult votingIndexRoute = new RedirectToRouteResult(new RouteValueDictionary(new { controller = Votings, action = nameof(VotingsController.Index) }));
                filterContext.Result = votingIndexRoute;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}