using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Maya.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.UseCdn = true;
			/* CORE PLUGINS */
			bundles.Add( new ScriptBundle( "~/bundles/cores" ).Include(
					"~/Scripts/plugins/jquery-1.11.0.min.js",
					"~/Scripts/plugins/jquery-migrate-1.2.1.min.js",
					"~/Scripts/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
					"~/Scripts/plugins/bootstrap/js/bootstrap.min.js",
					"~/Scripts/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
					"~/Scripts/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
					"~/Scripts/plugins/jquery.blockui.min.js",
					"~/Scripts/jquery.cokie.min.js",
					"~/Scripts/uniform/jquery.uniform.min.js",
					"~/Scripts/bootstrap-switch/js/bootstrap-switch.min.js" ) );

			/* PAGE LEVEL PLUGINS */
			bundles.Add( new ScriptBundle( "~/bundles/PagePlugins" ).Include(
					"~/Scripts/plugins/jqvmap/jqvmap/jquery.vmap.js" ) );

			/* PAGE LEVEL SCRIPTS */
			bundles.Add( new ScriptBundle( "" ).Include( "" ) );

			

            /* Datetime picker js */
            bundles.Add(new ScriptBundle("~/Scripts/datetimepicker.js").Include(
                        "~/Scripts/datetimepicker/js/bootstrap-datetimepicker.min.js",
                        "~/Scripts/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"));

            /* jQuery validation js */
            bundles.Add(new ScriptBundle("~/Scripts/jqueryval.js").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.unobtrusive.bootstrap.js"));

            /***********************************+ CSS +************************************/

            /* BootStrap v3.2 */
            bundles.Add(new StyleBundle("~/Content/Admin/skin3/stylesheets/cores.css").Include(
                        "~/Content/Admin/skin3/stylesheets/application-cf0a60d5.css",
                        "~/Content/Admin/skin3/stylesheets/font-awesome.min.css"
                        ));
            /* Admin */
            bundles.Add(new StyleBundle("~/Content/Admin/skin3/stylesheets/main.css").Include(
                        "~/Content/Admin/skin3/stylesheets/site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/skin3/stylesheets/datetimepicker.css").Include(
                        "~/Scripts/datetimepicker/css/bootstrap-datetimepicker.min.css"));
        }
    }
}