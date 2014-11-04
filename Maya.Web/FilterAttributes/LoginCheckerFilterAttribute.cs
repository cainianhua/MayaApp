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
    public class LoginCheckerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ForceToLogin { get; set; }

        public LoginCheckerAttribute() : this(true) { }

        public LoginCheckerAttribute(bool forceToLogin)
            : base()
        {
            this.ForceToLogin = forceToLogin;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            HttpCookie cookie = filterContext.HttpContext.Request.Cookies["MAYA_USER_TOKEN"];
            if (cookie == null)
            {
                if (ForceToLogin)
                {
                    RouteValueDictionary routeValues = new RouteValueDictionary();
                    routeValues.Add("controller", "Account");
                    routeValues.Add("action", "Login");
                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}