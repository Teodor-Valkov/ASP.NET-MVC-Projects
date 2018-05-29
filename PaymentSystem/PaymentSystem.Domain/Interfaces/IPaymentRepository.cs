using System.Collections.Generic;
using PaymentSystem.Models.Models.Payments;
using PaymentSystem.Models.ViewModels.Payments;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IPaymentRepository : IDbRepository
    {
        ICollection<PaymentViewModel> GetAllPaymentsByUserId(int userId, string order);

        bool IsPaymentExisting(int paymentId);

        bool IsCurrentUserSameAsPaymentUser(int paymentId, int userId);

        bool IsAccountAmountEnoughForProcessing(int paymentId);

        bool ProcessPayment(int paymentId);

        bool CancelPayment(int paymentId);

        bool MakePayment(MakePaymentModel makePayment);
    }
}