using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using System.Web.Mvc.Html;

namespace Maya.Web
{
	public static class TagLinkExtensions
	{
		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName)
		{
			return TagLink(htmlHelper, linkText, actionName, null /* controllerName */, new RouteValueDictionary(), new RouteValueDictionary());
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
		{
			return TagLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, null /* htmlAttributes */);
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
		{
			return TagLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
		{
			return TagLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, new RouteValueDictionary());
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return TagLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
		{
			return TagLink(htmlHelper, linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
		{
			return TagLink(htmlHelper, linkText, actionName, controllerName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return TagLink(htmlHelper, linkText, actionName, controllerName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			var htmlString = htmlHelper.ActionLink("[[-link-text-]]", actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes).ToString();
			return MvcHtmlString.Create(htmlString.Replace("[[-link-text-]]", linkText));
		}

		public static MvcHtmlString TagLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			var htmlString = htmlHelper.ActionLink("[[-link-text-]]", actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes).ToString();
			return MvcHtmlString.Create(htmlString.Replace("[[-link-text-]]", linkText));
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, object routeValues)
		{
			return RouteTagLink(htmlHelper, linkText, routeValues, null /* htmlAttributes */);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues)
		{
			return RouteTagLink(htmlHelper, linkText, routeValues, new RouteValueDictionary());
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName)
		{
			return RouteTagLink(htmlHelper, linkText, routeName, (object)null /* routeValues */);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
		{
			return RouteTagLink(htmlHelper, linkText, routeName, routeValues, null /* htmlAttributes */);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
		{
			return RouteTagLink(htmlHelper, linkText, routeName, routeValues, new RouteValueDictionary());
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes)
		{
			return RouteTagLink(htmlHelper, linkText, routeValues, htmlAttributes);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return RouteTagLink(htmlHelper, linkText, null /* routeName */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
		{
			return RouteTagLink(htmlHelper, linkText, routeName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return RouteTagLink(htmlHelper, linkText, routeName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			var htmlString = htmlHelper.RouteLink("[[-link-text-]]", routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToString();
			return MvcHtmlString.Create(htmlString.Replace("[[-link-text-]]", linkText));
		}

		public static MvcHtmlString RouteTagLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			var htmlString = htmlHelper.RouteLink("[[-link-text-]]", routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToString();
			return MvcHtmlString.Create(htmlString.Replace("[[-link-text-]]", linkText));
		}
	}
}