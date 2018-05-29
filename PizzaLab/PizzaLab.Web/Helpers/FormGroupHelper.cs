using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PizzaLab.Web.Helpers
{
    public static class FormGroupHelper
    {
        public static MvcHtmlString FormGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TProperty>> expression)
        {
            MvcHtmlString textBox = helper.TextBoxFor(expression, htmlAttributes: new { @class = "form-control" });
            return GenerateFormGroup(helper, expression, textBox);
        }

        public static MvcHtmlString FormGroupForPassword<TModel, TProperty>(
            this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TProperty>> expression)
        {
            MvcHtmlString passwordBox = helper.PasswordFor(expression, htmlAttributes: new { @class = "form-control" });
            return GenerateFormGroup(helper, expression, passwordBox);
        }

        private static MvcHtmlString GenerateFormGroup<TModel, TProperty>(
            this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TProperty>> expression,
            MvcHtmlString editor)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("form-group row");

            MvcHtmlString label = helper.LabelFor(expression, htmlAttributes: new { @class = "col-sm-2 col-form-label" });

            MvcHtmlString validationMessage = helper.ValidationMessageFor(expression, null, htmlAttributes: new { @class = "text-danger" });

            TagBuilder fieldContainer = new TagBuilder("div");
            fieldContainer.AddCssClass("col-sm-10");
            fieldContainer.InnerHtml = editor.ToHtmlString() + validationMessage.ToHtmlString();
            tagBuilder.InnerHtml = label.ToHtmlString() + fieldContainer.ToString();

            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}