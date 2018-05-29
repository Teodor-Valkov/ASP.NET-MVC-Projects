using System;

namespace PaymentSystem.Models.Models.Payments
{
    public class MakePaymentModel
    {
        public MakePaymentModel(int accountId, int userId, string paymentIban, decimal paymentAmount, string paymentReason, DateTime paymentDate)
        {
            this.AccountId = accountId;
            this.UserId = userId;
            this.PaymentIBAN = paymentIban;
            this.PaymentAmount = paymentAmount;
            this.PaymentReason = paymentReason;
            this.PaymentDate = paymentDate;
        }

        public int AccountId { get; private set; }

        public int UserId { get; private set; }

        public string PaymentIBAN { get; private set; }

        public decimal PaymentAmount { get; private set; }

        public string PaymentReason { get; private set; }

        public DateTime PaymentDate { get; set; }
    }
}