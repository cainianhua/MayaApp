using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web
{
    public static class PanelExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="invalidCssClass"></param>
        /// <returns></returns>
        public static MvcPanel BeginPanelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string invalidCssClass = "has-error")
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (!htmlHelper.ViewData.ModelState.IsValidField(modelMetadata.PropertyName)){
                tagBuilder.AddCssClass(invalidCssClass);
            }

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            MvcPanel thePanel = new MvcPanel(htmlHelper.ViewContext);

            return thePanel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        public static void EndPanel(this HtmlHelper htmlHelper)
        {
            EndPanel(htmlHelper.ViewContext);
        }

        internal static void EndPanel(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
        }
    }
}