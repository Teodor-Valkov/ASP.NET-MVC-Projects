using System.Web.Mvc;
using static LocalPub.Common.WebConstants;

namespace PizzaLab.Web.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this TempDataDictionary tempData, string message)
        {
            tempData[TempDataSuccessMessageKey] = message;
        }

        public static void AddWarningMessage(this TempDataDictionary tempData, string message)
        {
            tempData[TempDataWarningMessageKey] = message;
        }

        public static void AddErrorMessage(this TempDataDictionary tempData, string message)
        {
            tempData[TempDataErrorMessageKey] = message;
        }
    }
}