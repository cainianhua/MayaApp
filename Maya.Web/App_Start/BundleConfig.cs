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
                    "~/Scripts/plugins/jquery.cokie.min.js",
                    "~/Scripts/plugins/uniform/jquery.uniform.min.js",
                    "~/Scripts/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/Scripts/metronic.js",
                    "~/Scripts/admin/layout/layout.js",
                    "~/Scripts/admin/layout/quick-sidebar.js",
                    "~/Scripts/admin/layout/demo.js" ) );

			/* DASHBOARD PAGE LEVEL PLUGINS */
			bundles.Add( new ScriptBundle( "~/bundles/dashboard-plugins" ).Include(
					"~/Scripts/plugins/jqvmap/jqvmap/jquery.vmap.js",
					"~/Scripts/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
					"~/Scripts/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
					"~/Scripts/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
					"~/Scripts/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
					"~/Scripts/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
					"~/Scripts/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js",
					"~/Scripts/plugins/flot/jquery.flot.min.js",
					"~/Scripts/plugins/flot/jquery.flot.resize.min.js",
					"~/Scripts/plugins/flot/jquery.flot.categories.min.js",
					"~/Scripts/plugins/jquery.pulsate.min.js",
					"~/Scripts/plugins/bootstrap-daterangepicker/moment.min.js",
					"~/Scripts/plugins/bootstrap-daterangepicker/daterangepicker.js",
					"~/Scripts/plugins/fullcalendar/fullcalendar/fullcalendar.min.js",
					"~/Scripts/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
					"~/Scripts/plugins/jquery.sparkline.min.js",
					"~/Scripts/plugins/gritter/js/jquery.gritter.js" ) );

			/* DASHBOARD PAGE LEVEL SCRIPTS */
			bundles.Add( new ScriptBundle( "~/bundles/dashboard-scripts" ).Include(
					"~/Scripts/admin/pages/index.js",
					"~/Scripts/admin/pages/tasks.js" ) );

            /* FORM PAGE LEVEL PLUGINS */
            bundles.Add( new ScriptBundle( "~/bundles/form-plugins" ).Include(
                    "~/Scripts/plugins/jquery-validation/js/jquery.validate.min.js",
                    "~/Scripts/plugins/jquery-validation/js/additional-methods.min.js",
                    "~/Scripts/plugins/select2/select2.min.js",
                    "~/Scripts/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/Scripts/plugins/bootstrap-wysihtml5/wysihtml5-0.3.0.js",
                    "~/Scripts/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.js",
                    //"~/Scripts/plugins/bootstrap-markdown/js/bootstrap-markdown.js",
                    //"~/Scripts/plugins/bootstrap-markdown/lib/markdown.js",
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/jquery.validate.unobtrusive.bootstrap.js" ) );

            /* FORM PAGE LEVEL SCRIPTS  */
            bundles.Add( new ScriptBundle( "~/bundles/form-pages" ).Include(
                    /* PAGE LEVEL SCRIPTS PUBLIC */
                    
                    "~/Scripts/admin/pages/form-validation.js" ) );

            /* jQuery validation js */
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/jquery.validate.unobtrusive.bootstrap.js"));

			/***********************************+ CSS +************************************/

			/* GLOBAL MANDATORY STYLES */
			bundles.Add( new StyleBundle( "~/Content/global" )
				.Include( "~/Content/google-fonts.css", new IItemTransform[0] )
				.Include( "~/Scripts/plugins/font-awesome/css/font-awesome.min.css", new IItemTransform[0] )
				.Include( "~/Scripts/plugins/simple-line-icons/simple-line-icons.min.css", new IItemTransform[0] )
				.Include( "~/Scripts/plugins/bootstrap/css/bootstrap.min.css", new IItemTransform[0] )
				.Include( "~/Scripts/plugins/uniform/css/uniform.default.css", new IItemTransform[0] )
				.Include( "~/Scripts/plugins/bootstrap-switch/css/bootstrap-switch.min.css", new IItemTransform[0] ) );

            /* Admin */
            bundles.Add(new StyleBundle("~/Content/Admin/skin3/stylesheets/main.css").Include(
                        "~/Content/Admin/skin3/stylesheets/site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/skin3/stylesheets/datetimepicker.css").Include(
                        "~/Scripts/datetimepicker/css/bootstrap-datetimepicker.min.css"));
        }
    }
}