using System.Collections.Generic;
using PaymentSystem.Models.Models.Payments;
using PaymentSystem.Models.ViewModels.Payments;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IPaymentManager
    {
        ICollection<PaymentViewModel> GetAllPaymentsByUserId(int userId, string order);

        string MakePayment(MakePaymentModel makePayment);

        string ProcessPayment(int paymentId, int userId);

        string CancelPayment(int paymentId, int userId);
    }
}