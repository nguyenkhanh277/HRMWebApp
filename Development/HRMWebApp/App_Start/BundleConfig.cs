using System.Web;
using System.Web.Optimization;

namespace HRMWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/lite").Include(
                        "~/Content/lib/jquery-ui/jquery-ui.min.js",
                        "~/Content/lib/jquery-pjax/jquery.pjax.js",
                        "~/Content/lib/bootstrap-sass/assets/javascripts/bootstrap.min.js",
                        "~/Content/lib/widgster/widgster.js",
                        "~/Content/lib/underscore/underscore.js",
                        "~/Content/js/app.js",
                        "~/Content/js/settings.js",
                        "~/Content/js/Globals.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainscripts").Include(
                        "~/Content/lib/slimScroll/jquery.slimscroll.min.js",
                        "~/Content/lib/jquery.sparkline/index.js",
                        "~/Content/lib/backbone/backbone.js",
                        "~/Content/lib/backbone.localStorage/backbone.localStorage-min.js",
                        "~/Content/lib/d3/d3.min.js",
                        "~/Content/lib/nvd3/build/nv.d3.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
