using BirthdaySystem.Web.Filters;
using System.Web.Mvc;

namespace BirthdaySystem.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InsertUserAttribute());
        }
    }
}