using System.Web;
using System.Web.Optimization;

namespace EprocurementWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                        "~/Content/Scripts/multiLanguajeDemo.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.js",
                      "~/Content/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Styles/bootstrap.css",
                      "~/Content/Styles/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/libs/runtime*",
                 "~/Scripts/libs/polyfills*",
                 "~/Scripts/libs/vendor*",
                 "~/Scripts/libs/main*"));
        }
    }
}
