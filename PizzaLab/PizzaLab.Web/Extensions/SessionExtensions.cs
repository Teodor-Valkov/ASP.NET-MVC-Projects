using Newtonsoft.Json;
using System;
using System.Web;

namespace PizzaLab.Web.Extensions
{
    public static class SessionExtensions
    {
        public static T GetShoppingCart<T>(this HttpSessionStateBase session, string key)
            where T : class
        {
            string value = null; 

            if (session[key] != null)
            {
                value = session[key].ToString();
            }

            return value == null
                ? Activator.CreateInstance<T>()
                : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetShoppingCart<T>(this HttpSessionStateBase session, string key, T value)
            where T : class
        {
            session[key] = JsonConvert.SerializeObject(value);
        }
    }
}
