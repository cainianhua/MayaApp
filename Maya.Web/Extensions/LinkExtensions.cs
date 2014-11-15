using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Maya.Web
{
	public static class LinkExtensions
	{
		public static MvcHtmlString DeleteLink( this HtmlHelper helper, string imageUrlPath, string actionName, object routeValues ) {
			return DeleteLink( helper, imageUrlPath, actionName, null /* Controller */, routeValues, null );
		}

		public static MvcHtmlString DeleteLink( this HtmlHelper helper, string imageUrlPath, string actionName, string controllerName, object routeValues ) {
			return DeleteLink( helper, imageUrlPath, actionName, controllerName,  routeValues, null );
		}

		public static MvcHtmlString DeleteLink( this HtmlHelper helper, string imageUrlPath, string actionName, object routeValues, object htmlAttributes ) {
			return _DeletePutLink( HttpVerbs.Delete, helper, imageUrlPath, actionName, null /* Controller */, routeValues, htmlAttributes );
		}

		public static MvcHtmlString DeleteLink( this HtmlHelper helper, string imageUrlPath, string actionName, string controllerName, object routeValues, object htmlAttributes ) {
			return _DeletePutLink( HttpVerbs.Delete, helper, imageUrlPath, actionName, controllerName,  routeValues, htmlAttributes );
		}

		public static MvcHtmlString PutLink( this HtmlHelper helper, string imageUrlPath, string actionName, object routeValues ) {
			return PutLink( helper, imageUrlPath, actionName, null, routeValues, null );
		}

		public static MvcHtmlString PutLink( this HtmlHelper helper, string imageUrlPath, string actionName, string controllerName, object routeValues ) {
			return PutLink( helper, imageUrlPath, actionName, controllerName,  routeValues, null );
		}

		public static MvcHtmlString PutLink( this HtmlHelper helper, string imageUrlPath, string actionName, object routeValues, object htmlAttributes ) {
			return _DeletePutLink( HttpVerbs.Put, helper, imageUrlPath, actionName, null /* Controller */, routeValues, htmlAttributes );
		}

		public static MvcHtmlString PutLink( this HtmlHelper helper, string imageUrlPath, string actionName, string controllerName, object routeValues, object htmlAttributes ) {
			return _DeletePutLink( HttpVerbs.Put, helper, imageUrlPath, actionName, controllerName,  routeValues, htmlAttributes );
		}

		private static MvcHtmlString _DeletePutLink( HttpVerbs verb, HtmlHelper helper, string imageUrlPath, string actionName, string controllerName,  object routeValues, object htmlAttributes ) {
			// Get the url to the controller method
			var urlHelper = new UrlHelper( helper.ViewContext.RequestContext );
			var url = urlHelper.Action( actionName, controllerName, routeValues );

			// Create the form
			var form = new TagBuilder( "form" );
			form.Attributes["action"] = url;
			form.Attributes["method"] = "post";

			// Add X-HTTP-Method-Override to the form
			string formHtml = helper.HttpMethodOverride( verb ).ToHtmlString();

			// Create the clickable image
			var inputImage = new TagBuilder( "input" );
			inputImage.Attributes["type"] = "image";
			inputImage.Attributes["src"] = imageUrlPath;
			inputImage.Attributes["alt"] = verb.ToString();
			if (htmlAttributes != null) {
				inputImage.MergeAttributes<string, object>( new RouteValueDictionary( htmlAttributes ) );
			}

			// Return the html form
			formHtml += inputImage.ToString( TagRenderMode.SelfClosing );
			form.InnerHtml = formHtml;
			return MvcHtmlString.Create( form.ToString() );
		}
	}
}