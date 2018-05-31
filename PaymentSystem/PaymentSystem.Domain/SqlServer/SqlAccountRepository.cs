using System.Collections.Generic;
using System.Data.SqlClient;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Models.ViewModels.Accounts;

namespace PaymentSystem.Domain.SqlServer
{
    public class SqlAccountRepository : DbRepository, IAccountRepository
    {
        public ICollection<AccountViewModel> GetAllAccountsByUserId(int userId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT a.IBAN,
	                      a.Amount
                     FROM Accounts AS a
                     JOIN AccountUsers AS au
                       ON a.Id = au.AccountId
                    WHERE au.UserId = @userId",
                      new Dictionary<string, object>
                      {
                          { "@userId", userId }
                      });

            ICollection<AccountViewModel> accounts = new List<AccountViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    string iban = reader.GetString(0);
                    decimal amount = reader.GetDecimal(1);
                    AccountViewModel account = new AccountViewModel(iban, amount);

                    accounts.Add(account);
                }
            }

            return accounts;
        }

        public ICollection<AccountDescriptionViewModel> GetAllAccountsForPaymentByUserId(int userId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT au.AccountId,
                          a.IBAN,
	                      a.Amount
                     FROM Accounts AS a
                     JOIN AccountUsers AS au
                       ON a.Id = au.AccountId
                    WHERE au.UserId = @userId",
                      new Dictionary<string, object>
                      {
                          { "@userId", userId }
                      });

            ICollection<AccountDescriptionViewModel> accounts = new List<AccountDescriptionViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string iban = reader.GetString(1);
                    decimal amount = reader.GetDecimal(2);
                    string name = string.Format("{0} ({1})", iban, amount);
                    AccountDescriptionViewModel account = new AccountDescriptionViewModel(id, name);

                    accounts.Add(account);
                }
            }

            return accounts;
        }

        public bool IsAccountExisting(int accountId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id
                     FROM Accounts
                    WHERE Id = @accountId",
                      new Dictionary<string, object>()
                      {
                          { "@accountId", accountId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
        }
    }
}