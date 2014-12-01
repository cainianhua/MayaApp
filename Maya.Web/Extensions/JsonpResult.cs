using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace System.Web.Mvc
{
	public class JsonpResult : JsonResult
	{
		private static readonly string JSONPCALLBACKNAME = "callback";
		private static readonly string CALLBACKAPPLICATIONTYPE = "application/json";


		/// <summary>
		/// Enables processing of the result of an action method by a custom type that
		/// inherits from the System.Web.Mvc.ActionResult class.</summary>
		/// <param name="context">The context within which the result is executed.</param>
		/// <exception cref="ArgumentNullException">The context parameter is null</exception>
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if ((JsonRequestBehavior == JsonRequestBehavior.DenyGet) &&
				  String.Equals(context.HttpContext.Request.HttpMethod, "GET"))
			{
				throw new InvalidOperationException();
			}

			var response = context.HttpContext.Response;
			if (!String.IsNullOrEmpty(ContentType))
				response.ContentType = ContentType;
			else
				response.ContentType = CALLBACKAPPLICATIONTYPE;
			if (ContentEncoding != null)
				response.ContentEncoding = this.ContentEncoding;
			if (Data != null)
			{
				String buffer;
				var request = context.HttpContext.Request;
				var serializer = new JavaScriptSerializer();
				if (!string.IsNullOrEmpty(request[JSONPCALLBACKNAME]))
					buffer = String.Format("{0}({1})", request[JSONPCALLBACKNAME], serializer.Serialize(Data));
				else
					buffer = serializer.Serialize(Data);
				response.Write(buffer);
			}
		}
	}
}