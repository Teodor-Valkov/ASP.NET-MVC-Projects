using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace LocalPub.Web.ModelBinders
{
    public class DateModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "OrderDate")
            {
                NameValueCollection form = controllerContext.HttpContext.Request.Form;
                string dateTimeValue = form.Get("OrderDate") as string;
                IList<int> value = dateTimeValue.Split('-').Select(part => int.Parse(part)).ToList();

                // If we are using bootstrap datepicker, the year/month/date come reversed
                if (value[2].ToString().Length == 4)
                {
                    value = value.Reverse().ToList();
                }

                propertyDescriptor.SetValue(bindingContext.Model, new DateTime(value[0], value[1], value[2]));
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }
    }
}