using System.Web;
using System.Web.Optimization;

namespace Alfursan.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Plugins/Datatable/js/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css/en").Include(
                      "~/Content/en/bootstrap.css",
                      "~/Content/en/site.css",
                      "~/Plugins/Datatable/style/jquery.dataTables.css",
                      "~/Plugins/Datatable/style/jquery.dataTables_themeroller.css"));
            bundles.Add(new StyleBundle("~/Content/css/ar").Include(
                     "~/Content/ar/bootstrap.css",
                     "~/Content/ar/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
