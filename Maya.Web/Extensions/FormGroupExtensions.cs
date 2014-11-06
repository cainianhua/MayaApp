using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web.Extensions
{
    public static class FormGroupExtensions
    {
        public static MvcHtmlString FormGroupFor<TModel, TValue>(this HtmlHelper htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            TagBuilder tag = new TagBuilder("div");

            throw new NotImplementedException();
        }
    }
}