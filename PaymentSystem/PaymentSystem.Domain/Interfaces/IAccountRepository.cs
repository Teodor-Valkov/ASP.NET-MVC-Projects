using System.Collections.Generic;
using PaymentSystem.Models.ViewModels.Accounts;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IAccountRepository : IDbRepository
    {
        ICollection<AccountViewModel> GetAllAccountsByUserId(int userId);

        ICollection<AccountDescriptionViewModel> GetAllAccountsForPaymentByUserId(int userId);

        bool IsAccountExisting(int paymentId);
    }
}
