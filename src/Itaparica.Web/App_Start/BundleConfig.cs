using System.Web;
using System.Web.Optimization;
using Itaparica.Web.Bundles;

namespace Itaparica.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-{version}.js",
                "~/Scripts/jquery/plugins/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular/angular.min.js",
                "~/Scripts/angular/plugins/datetime.js",
                "~/Scripts/angular/plugins/angular-validate.min.js",
                "~/Scripts/angular/plugins/angular-ladda.min.js",
                //"~/Scripts/angular/plugins/angular-animate.min.js",
                "~/Scripts/angular/plugins/angular-toastr/angular-toastr.min.js",
                "~/Scripts/angular/plugins/angular-toastr/angular-toastr.tpls.min.js",
                "~/Scripts/angular/plugins/dataTables/datatables.min.js",
                "~/Scripts/angular/plugins/dataTables/angular-datatables.min.js",
                "~/Scripts/angular/plugins/dataTables/buttons/angular-datatables.buttons.min.js",
                "~/Scripts/angular/plugins/dataTables/angular-datatables.bootstrap.min.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/config.js",
                "~/Scripts/app/directives.js",
                "~/Scripts/app/filters.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Scripts/angular/plugins/angular-toastr/angular-toastr.css",
                //    "~/Content/ladda-themeless.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            RegisterApp(bundles);
        }

        private static void RegisterApp(BundleCollection bundles)
        {
            CadastrosBundles.RegisterBundles(bundles);
        }
    }
}
