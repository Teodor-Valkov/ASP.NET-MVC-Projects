using System.Web.Mvc;
using System.Web.Mvc.Filters;
using PaymentSystem.Common;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Filters
{
    public class InsertUserAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            UserModel user = filterContext.HttpContext.Session[AuthConstants.SessionUserKey] as UserModel;
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