using System.Web;
using System.Web.Optimization;

namespace TestProj
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Employee").Include(
                "~/Scripts/Employee/Employee.js"));
            bundles.Add(new ScriptBundle("~/bundles/EmployeeDataTable").Include(
                "~/Scripts/Employee/EmployeeDataTable.js"));
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                "~/Scripts/dataTables/jquery.dataTables.js",
                "~/Scripts/dataTables/dataTables.responsive.js"));
            bundles.Add(new StyleBundle("~/bundles/datatables-css")
                .Include("~/Content/dataTables/jquery.dataTables.css", new CssRewriteUrlTransform())
                .Include("~/Content/dataTables/dataTables.responsive.css", new CssRewriteUrlTransform()));
        }
    }
}
