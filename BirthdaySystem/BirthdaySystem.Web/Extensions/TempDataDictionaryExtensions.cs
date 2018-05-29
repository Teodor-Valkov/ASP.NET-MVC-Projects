using System.Web.Mvc;
using static BirthdaySystem.Common.WebConstants;

namespace BirthdaySystem.Web.Extensions
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