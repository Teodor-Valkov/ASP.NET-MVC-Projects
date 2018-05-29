using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PaymentSystem.Common;

namespace PaymentSystem.Models.ValidationAttributes
{
    public class PaymentIBANAttribute : ValidationAttribute
    {
        private const string ValidIBANCharactersPattern = "[A-Za-z0-9]{22}";

        public PaymentIBANAttribute()
        {
            this.ErrorMessage = MessageConstants.PaymentIBANError;
        }

        public override bool IsValid(object value)
        {
            string paymentIBAN = value as string;

            if (paymentIBAN == null)
            {
                return true;
            }

            Regex validPaymentIBANSymbols = new Regex(ValidIBANCharactersPattern);
            if (!validPaymentIBANSymbols.IsMatch(paymentIBAN))
            {
                return false;
            }

            return true;
        }
    }
}
