using System.Web.Mvc;
using System.Web.Mvc.Filters;
using LocalPub.Models;
using static LocalPub.Common.AuthConstants;

namespace LocalPub.Web.Filters
{
    public class InsertUserAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            ClientModel client = filterContext.HttpContext.Session[SessionUserKey] as ClientModel;

            if (client != null)
            {
                filterContext.Principal = client;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // This is not needed, we can just skip it
        }
    }
}