using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web
{
	public static class SideBarExtensions
	{
		public static MvcHtmlString SideBarLink(this HtmlHelper htmlHelper, string name, string iconClass, string controller, bool hasArrow = true ) {
			string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

			if (string.IsNullOrEmpty( name )) name = "[没有名称]";
			if (string.IsNullOrEmpty( iconClass )) iconClass = "icon-home";
			if (string.IsNullOrEmpty( controller )) controller = currentController;

			var linkHtml = "<a href=\"javascript:;\">"
						 + "	<i class=\"{0}\"></i>"
						 + "	<span class=\"title\">{1}</span>{2}{3}"
						 + "</a>";
			var arrowHtml = "<span class=\"arrow{0}\"></span>";

			linkHtml = string.Format( linkHtml, iconClass, name,
				currentController.Equals( controller, StringComparison.OrdinalIgnoreCase ) ? "<span class=\"selected\"></span>" : "",
				hasArrow ? ( string.Format( arrowHtml, currentController.Equals( controller, StringComparison.OrdinalIgnoreCase ) ? " open" : "" ) ) : "" );

			return MvcHtmlString.Create( linkHtml );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="html"></param>
		/// <param name="controller"></param>
		/// <param name="action"></param>
		/// <param name="className"></param>
		/// <param name="outputFormatter"></param>
		/// <returns></returns>
		public static MvcHtmlString ActiveCss( this HtmlHelper html, string controller, string action, string className = "active", string outputFormatter = "class=\"{0}\"" ) {
			string currentController = html.ViewContext.RouteData.GetRequiredString( "controller" );
			string currentAction = html.ViewContext.RouteData.GetRequiredString( "action" );

			if (string.IsNullOrEmpty( controller ))
				controller = currentController;

			if (string.IsNullOrEmpty( action ))
				action = currentAction;

			className = string.IsNullOrEmpty( className ) ? "active" : className;
			outputFormatter = string.IsNullOrEmpty( outputFormatter ) ? "class=\"{0}\"" : outputFormatter;
			outputFormatter = string.Format( outputFormatter, className );

			return MvcHtmlString.Create( controller.Equals( currentController, StringComparison.OrdinalIgnoreCase ) && action.Equals( currentAction, StringComparison.OrdinalIgnoreCase ) ?
				outputFormatter : String.Empty );
		}
	}
}