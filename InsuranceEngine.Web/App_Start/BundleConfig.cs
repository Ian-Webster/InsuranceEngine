using System.Web.Optimization;

namespace InsuranceEngine.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/bootswatch/flatly/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/datepicker3.css",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/cssAdmin").Include(
                //"~/Content/bootstrap.css",
                      "~/Content/bootswatch/cerulean/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/siteadmin.css",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/Telerik/css").Include(
                "~/Content/kendo/2014.1.415/kendo.common.min.css",
                "~/Content/kendo/2014.1.415/kendo.dataviz.min.css",
                "~/Content/kendo/2014.1.415/kendo.bootstrap.min.css",
                "~/Content/kendo/2014.1.415/kendo.dataviz.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/AceEditor").Include(
                      "~/Scripts/Ace/ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/datePicker").Include(
                      "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootBox").Include(
                      "~/Scripts/bootbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/questionnaire").Include(
                      "~/Scripts/Questionnaire.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsTree_js").Include(
                      "~/Scripts/jsTree3/jstree.js"));

            bundles.Add(new StyleBundle("~/bundles/jsTree_css").Include(
                      "~/Scripts/jsTree3/themes/default/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/telerik.jquery").Include(
                "~/Scripts/kendo/2014.2.716/jquery.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.all.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.modernizr.custom.js"));



        }
    }
}
