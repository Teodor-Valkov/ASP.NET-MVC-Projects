using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace PizzaLab.Server.ModelBinders
{
    public class PizzaIngredientsModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "Ingredients")
            {
                NameValueCollection form = controllerContext.HttpContext.Request.Form;

                string ingredients = form.Get("Ingredients") as string;

                IList<int> value = ingredients.Split(',').Select(ingredient => int.Parse(ingredient)).ToList();

                propertyDescriptor.SetValue(bindingContext.Model, value);
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }
    }
}