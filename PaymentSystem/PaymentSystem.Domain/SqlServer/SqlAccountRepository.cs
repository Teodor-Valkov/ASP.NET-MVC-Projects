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

            IDictionary<int, AccountViewModel> accounts = new Dictionary<int, AccountViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string iban = reader.GetString(1);
                    decimal amount = reader.GetDecimal(2);
                    AccountViewModel account = new AccountViewModel(id, iban, amount);

                    accounts[id] = account;
                }
            }

            return accounts.Values;
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

            IDictionary<int, AccountDescriptionViewModel> accounts = new Dictionary<int, AccountDescriptionViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string iban = reader.GetString(1);
                    decimal amount = reader.GetDecimal(2);
                    string name = string.Format("{0} ({1})", iban, amount);
                    AccountDescriptionViewModel account = new AccountDescriptionViewModel(id, name);

                    accounts[id] = account;
                }
            }

            return accounts.Values;
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

            int? existingAccountId = null;

            using (reader)
            {
                while (reader.Read())
                {
                    existingAccountId = reader.GetInt32(0);
                }
            }

            return existingAccountId.HasValue;
        }
    }
}
