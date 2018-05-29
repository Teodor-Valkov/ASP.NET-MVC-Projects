using System.Collections.Generic;
using PaymentSystem.Models.ViewModels.Accounts;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IAccountManager
    {
        ICollection<AccountViewModel> GetAllAccountsByUserId(int userId);

        ICollection<AccountDescriptionViewModel> GetAllAccountsForPaymentByUserId(int userId);
    }
}