using System.ComponentModel.DataAnnotations;
using PaymentSystem.Common;

namespace PaymentSystem.Models.ValidationAttributes
{
    public class PaymentReasonAttribute : ValidationAttribute
    {
        public PaymentReasonAttribute()
        {
            this.ErrorMessage = MessageConstants.PaymentReasonLengthError;
        }

        public override bool IsValid(object value)
        {
            string paymentReason = value as string;

            if (paymentReason == null)
            {
                return true;
            }

            if (paymentReason.Length < 3 || paymentReason.Length > 32)
            {
                return false;
            }

            return true;
        }
    }
}
