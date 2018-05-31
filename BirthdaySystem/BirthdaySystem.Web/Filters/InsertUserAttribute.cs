using BirthdaySystem.Models.Models.Employees;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using static BirthdaySystem.Common.AuthConstants;

namespace BirthdaySystem.Web.Filters
{
    public class InsertUserAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            EmployeeModel employee = filterContext.HttpContext.Session[SessionUserKey] as EmployeeModel;
            if (employee != null)
            {
                filterContext.Principal = employee;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // This is not needed, we can just skip it
        }
    }
}