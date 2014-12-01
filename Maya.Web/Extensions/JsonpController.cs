using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class JsonpController : Controller
    {
		protected internal virtual JsonpResult Jsonp(object data)
		{
			JsonpResult result = new JsonpResult()
			{
				Data = data,
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};

			return result;
		}
    }
}
