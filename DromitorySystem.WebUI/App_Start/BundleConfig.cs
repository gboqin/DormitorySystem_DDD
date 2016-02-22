using System.Web;
using System.Web.Optimization;

namespace DromitorySystem.WebUI
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyuijs").Include(
                      "~/Scripts/jquery.easyui-{version}.js",
                      "~/Scripts/jquery.easyui-{version}.min.js",
                      "~/Scripts/locale/easyui-lang-zh_CN.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/easyuicss").Include(
                      "~/Content/themes/icon.css",
                      "~/Content/themes/default.css",
                      "~/Content/themes/default/icon.css",
                      "~/Content/themes/default/easyui.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region 捆绑自定义css
            bundles.Add(new StyleBundle("~/bundles/mydefinedcss").Include("~/Content/mydefined.css"));
            #endregion

            #region 捆绑自定义js
            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include("~/Scripts/sys_js/common.js"));
            bundles.Add(new ScriptBundle("~/bundles/userjs").Include("~/Scripts/sys_js/usergrid.js"));
            bundles.Add(new ScriptBundle("~/bundles/depttreejs").Include("~/Scripts/sys_js/depttree.js"));
            bundles.Add(new ScriptBundle("~/bundles/goodsjs").Include("~/Scripts/sys_js/goodsgrid.js"));
            bundles.Add(new ScriptBundle("~/bundles/dotgridjs").Include("~/Scripts/sys_js/dotgrid.js"));
            bundles.Add(new ScriptBundle("~/bundles/dsettinggridjs").Include("~/Scripts/sys_js/dsettinggrid.js"));
            bundles.Add(new ScriptBundle("~/dundles/dormitorytreegridjs").Include("~/Scripts/sys_js/dormitorytreegrid.js"));
            bundles.Add(new ScriptBundle("~/bundles/dormsetrelationjs").Include("~/Scripts/sys_js/dormsetrelation.js"));
            #endregion
        }
    }
}
