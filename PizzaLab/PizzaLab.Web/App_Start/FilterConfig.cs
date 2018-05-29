using System.Web.Mvc;
using PizzaLab.Web.Filters;

namespace PizzaLab.Web
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
