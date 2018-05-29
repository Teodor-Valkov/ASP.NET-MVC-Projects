using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PaymentSystem.Common;
using PaymentSystem.Models.ValidationAttributes;

namespace PaymentSystem.Models.Payments
{
    public class MakePaymentBindingModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        public int AccountId { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.PaymentIBANLength, ErrorMessage = MessageConstants.PaymentIBANLengthError, MinimumLength = DataConstants.PaymentIBANLength)]
        [Display(Name = DisplayConstants.PaymentIBANName)]
        [PaymentIBAN]
        public string PaymentIBAN { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [Display(Name = DisplayConstants.PaymentAmountName)]
        [PaymentAmount]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.PaymentReasonMaxLength, ErrorMessage = MessageConstants.PaymentReasonLengthError, MinimumLength = DataConstants.PaymentReasonMinLength)]
        [Display(Name = DisplayConstants.PaymentReasonName)]
        [PaymentReason]
        public string PaymentReason { get; set; }

        public SelectList AccountsSelectList { get; set; }
    }
}