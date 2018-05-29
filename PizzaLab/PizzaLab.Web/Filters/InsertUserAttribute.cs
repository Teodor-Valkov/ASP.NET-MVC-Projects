using System.Web.Mvc;
using System.Web.Mvc.Filters;
using PizzaLab.Models.ViewModels.Users;
using static PizzaLab.Web.WebConstants;

namespace PizzaLab.Web.Filters
{
    public class InsertUserAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            UserModel user = filterContext.HttpContext.Session[UserSessionKey] as UserModel;

            if (user != null)
            {
                filterContext.Principal = user;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // This is not needed, we can just skip it
        }
    }
}