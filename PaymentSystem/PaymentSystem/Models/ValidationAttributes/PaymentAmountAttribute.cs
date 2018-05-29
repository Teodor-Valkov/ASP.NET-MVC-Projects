using System.ComponentModel.DataAnnotations;
using PaymentSystem.Common;

namespace PaymentSystem.Models.ValidationAttributes
{
    public class PaymentAmountAttribute : ValidationAttribute
    {
        private const int ValidDigitsAfterTheDecimalSign = 2;

        public PaymentAmountAttribute()
        {
            this.ErrorMessage = MessageConstants.PaymentAmountError;
        }

        public override bool IsValid(object value)
        {
            decimal? paymentAmount = value as decimal?;

            if (paymentAmount == null)
            {
                return true;
            }

            int indexOfTheDecimalSign = paymentAmount.ToString().IndexOf('.');
            int digitsAfterTheDecimalSign = paymentAmount.ToString().Length - indexOfTheDecimalSign - 1;
            if (indexOfTheDecimalSign == -1 || digitsAfterTheDecimalSign != ValidDigitsAfterTheDecimalSign)
            {
                return false;
            }

            if (paymentAmount.Value <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
