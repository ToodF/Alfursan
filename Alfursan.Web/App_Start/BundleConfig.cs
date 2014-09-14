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
                        "~/Plugins/Datatable/js/jquery.dataTables.min.js",
                        "~/Plugins/Datatable/js/dataTables.bootstrap.js",
                        "~/Plugins/Datatable/js/dataTables.responsive.js",
                        "~/Plugins/Datatable/js/jquery-ui.min.js",
                        "~/Plugins/Datatable/js/jquery.dataTables.columnFilter.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                //,"~/Scripts/Alfursan.Ajax.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                     "~/Plugins/JQueryFileUpload/js/vendor/jquery.ui.widget.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.iframe-transport.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload-process.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload-image.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload-audio.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload-video.js",
                     "~/Plugins/JQueryFileUpload/js/jquery.fileupload-validate.js"
                     ));

            bundles.Add(new StyleBundle("~/Style/en").Include(
                      "~/Content/en/bootstrap.css",
                      "~/Content/en/site.css",
                      "~/Plugins/Datatable/css/style.css",
                      "~/Plugins/Datatable/css/jquery.fileupload.css"
                      ));
            bundles.Add(new StyleBundle("~/Style/ar").Include(
                     "~/Content/ar/bootstrap.css",
                     "~/Content/ar/site.css"));

            bundles.Add(new StyleBundle("~/Style/Datatable").Include(
                     "~/Plugins/Datatable/css/jquery.dataTables.min.css",
                      "~/Plugins/Datatable/css/jquery.dataTables_themeroller.css",
                      "~/Plugins/Datatable/css/dataTables.bootstrap.css",
                      "~/Plugins/Datatable/css/dataTables.responsive.css",
                      "~/Plugins/Datatable/css/jquery-ui.min.css",
                      "~/Plugins/Datatable/css/dataTables.bootstrap.css"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
