using LocalPub.Web.Filters;
using System.Web.Mvc;

namespace LocalPub.Web
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