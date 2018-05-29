using System.Web.Optimization;

namespace BirthdaySystem.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            BundleTable.Bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome", "https://use.fontawesome.com/2ede8d50aa.js"));
        }
    }
}