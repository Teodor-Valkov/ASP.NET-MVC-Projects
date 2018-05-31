using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PaymentSystem.Common;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Models.Models.Payments;
using PaymentSystem.Models.ViewModels.Payments;

namespace PaymentSystem.Domain.SqlServer
{
    public class SqlPaymentRepository : DbRepository, IPaymentRepository
    {
        public ICollection<PaymentViewModel> GetAllPaymentsByUserId(int userId, string order)
        {
            string getAllPaymentsByUserIdCommand = @"SELECT p.Id, a.IBAN, p.PaymentIBAN, p.PaymentAmount, p.PaymentReason, p.PaymentDate, s.Status
                     FROM Payments AS p
                     JOIN Accounts AS a
                       ON p.AccountId = a.Id
					 JOIN Statuses AS s
					   ON p.StatusId = s.Id
                    WHERE p.UserId = @userId
                    ORDER BY ";

            if (order == DataConstants.Status)
            {
                getAllPaymentsByUserIdCommand += string.Format("p.{0}", DataConstants.StatusOrder);
            }
            else
            {
                getAllPaymentsByUserIdCommand += string.Format("p.{0} DESC", DataConstants.DateOrder);
            }

            SqlDataReader reader = this.ExecuteReader(
                      getAllPaymentsByUserIdCommand,
                      new Dictionary<string, object>
                      {
                          { "@userId", userId }
                      });

            ICollection<PaymentViewModel> payments = new List<PaymentViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string accountIban = reader.GetString(1);
                    string paymentIban = reader.GetString(2);
                    decimal paymentAmount = reader.GetDecimal(3);
                    string paymentReason = reader.GetString(4);
                    DateTime paymenDate = reader.GetDateTime(5);
                    string status = reader.GetString(6);
                    PaymentViewModel payment = new PaymentViewModel(id, accountIban, paymentIban, paymentAmount, paymentReason, paymenDate, status);

                    payments.Add(payment);
                }
            }

            return payments;
        }

        public bool MakePayment(MakePaymentModel makePayment)
        {
            int recordsInserted = this.ExecuteNonQuery(
                     @"INSERT INTO Payments (AccountId, UserId, PaymentIBAN, PaymentAmount, PaymentReason, StatusId, PaymentDate)
                            VALUES (@accountId, @userId, @paymentIban, @paymentAmount, @paymentReason, 1, @paymentDate)",
                            new Dictionary<string, object>
                            {
                               { "@accountId", makePayment.AccountId },
                               { "@userId", makePayment.UserId },
                               { "@paymentIban", makePayment.PaymentIBAN },
                               { "@paymentAmount", makePayment.PaymentAmount },
                               { "@paymentReason", makePayment.PaymentReason },
                               { "@paymentDate", makePayment.PaymentDate }
                            });

            return recordsInserted > 0;
        }

        public bool IsPaymentExisting(int paymentId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id
                     FROM Payments
                    WHERE Id = @paymentId",
                      new Dictionary<string, object>()
                      {
                          { "@paymentId", paymentId }
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

        public bool IsCurrentUserSameAsPaymentUser(int paymentId, int userId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT UserId
                     FROM Payments
                    WHERE Id = @paymentId",
                      new Dictionary<string, object>
                      {
                          { "@paymentId", paymentId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    int paymentUserId = reader.GetInt32(0);
                    return paymentUserId == userId;
                }

                return false;
            }
        }

        public bool IsAccountAmountEnoughForProcessing(int paymentId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT a.Amount, p.PaymentAmount
                     FROM Payments AS p
                     JOIN Accounts AS a
                       ON p.AccountId = a.Id
                    WHERE p.Id = @paymentId",
                      new Dictionary<string, object>()
                      {
                          { "@paymentId", paymentId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    decimal accountAmount = reader.GetDecimal(0);
                    decimal paymentAmount = reader.GetDecimal(1);
                    return accountAmount >= paymentAmount;
                }

                return false;
            }
        }

        public bool ProcessPayment(int paymentId)
        {
            int rowsUpdated = this.ExecuteNonQuery(
                  @"UPDATE Payments
                        SET StatusId = 2
                      WHERE Id = @paymentId
                        AND StatusId = 1",
                       new Dictionary<string, object>()
                       {
                            { "@paymentId", paymentId }
                       });

            if (rowsUpdated > 0)
            {
                SqlDataReader reader = this.ExecuteReader(
                     @"SELECT AccountId, PaymentAmount
                         FROM Payments
                        WHERE Id = @paymentId",
                          new Dictionary<string, object>()
                          {
                              { "@paymentId", paymentId }
                          });

                int accountId = 0;
                decimal paymentAmount = 0;

                using (reader)
                {
                    if (reader.Read())
                    {
                        accountId = reader.GetInt32(0);
                        paymentAmount = reader.GetDecimal(1);
                    }
                }

                rowsUpdated = this.ExecuteNonQuery(
                   @"UPDATE Accounts
                       SET Amount -= @paymentAmount
                     WHERE Id = @accountId",
                        new Dictionary<string, object>()
                        {
                           { "@paymentAmount", paymentAmount },
                           { "@accountId", accountId }
                        });
            }

            return rowsUpdated > 0;
        }

        public bool CancelPayment(int paymentId)
        {
            int rowsUpdated = this.ExecuteNonQuery(
                   @"UPDATE Payments
                        SET StatusId = 3
                      WHERE Id = @paymentId
                        AND StatusId = 1",
                        new Dictionary<string, object>()
                        {
                            { "@paymentId", paymentId }
                        });

            return rowsUpdated > 0;
        }
    }
}