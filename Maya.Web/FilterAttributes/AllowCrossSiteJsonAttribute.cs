using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Maya.Web.FilterAttributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
	public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting( ActionExecutingContext filterContext ) {
			filterContext.RequestContext.HttpContext.Response.AddHeader( "Access-Control-Allow-Origin", "*" );
			filterContext.RequestContext.HttpContext.Response.AddHeader( "Access-Control-Allow-Methods", "POST, GET, OPTIONS" );
			base.OnActionExecuting( filterContext );
		}
	}
}