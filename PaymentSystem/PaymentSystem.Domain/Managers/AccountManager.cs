using System.Collections.Generic;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.SqlServer;
using PaymentSystem.Models.ViewModels.Accounts;

namespace PaymentSystem.Domain.Managers
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository accountRepository;

        public AccountManager()
            : this(new SqlAccountRepository())
        {
        }

        public AccountManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public ICollection<AccountViewModel> GetAllAccountsByUserId(int userId)
        {
            using (this.accountRepository)
            {
                ICollection<AccountViewModel> accounts = this.accountRepository.GetAllAccountsByUserId(userId);
                return accounts;
            }
        }

        public ICollection<AccountDescriptionViewModel> GetAllAccountsForPaymentByUserId(int userId)
        {
            using (this.accountRepository)
            {
                ICollection<AccountDescriptionViewModel> accounts = this.accountRepository.GetAllAccountsForPaymentByUserId(userId);
                return accounts;
            }
        }
    }
}