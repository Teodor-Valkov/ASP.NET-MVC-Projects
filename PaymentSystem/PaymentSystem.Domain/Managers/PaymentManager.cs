using System.Collections.Generic;
using PaymentSystem.Common;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.SqlServer;
using PaymentSystem.Models.Models.Payments;
using PaymentSystem.Models.ViewModels.Payments;

namespace PaymentSystem.Domain.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private IPaymentRepository paymentRepository;
        private IAccountRepository accountRepository;

        public PaymentManager()
            : this(new SqlPaymentRepository(), new SqlAccountRepository())
        {
        }

        public PaymentManager(IPaymentRepository paymentRepository, IAccountRepository accountRepository)
        {
            this.paymentRepository = paymentRepository;
            this.accountRepository = accountRepository;
        }

        public ICollection<PaymentViewModel> GetAllPaymentsByUserId(int userId, string order)
        {
            using (this.paymentRepository)
            {
                ICollection<PaymentViewModel> payments = this.paymentRepository.GetAllPaymentsByUserId(userId, order);
                return payments;
            }
        }

        public string MakePayment(MakePaymentModel makePayment)
        {
            using (this.paymentRepository)
            {
                using (this.accountRepository)
                {
                    bool isAccountExisting = this.accountRepository.IsAccountExisting(makePayment.AccountId);
                    if (!isAccountExisting)
                    {
                        return MessageConstants.AccountNotFoundError;
                    }
                }

                bool makePaymentResult = this.paymentRepository.MakePayment(makePayment);
                if (!makePaymentResult)
                {
                    return MessageConstants.MakePaymentError;
                }

                return string.Empty;
            }
        }

        public string ProcessPayment(int paymentId, int userId)
        {
            using (this.paymentRepository)
            {
                bool isPaymentExisting = this.paymentRepository.IsPaymentExisting(paymentId);
                if (!isPaymentExisting)
                {
                    return MessageConstants.PaymentNotFoundError;
                }

                bool isCurrentUserSameAsPaymentUser = this.paymentRepository.IsCurrentUserSameAsPaymentUser(paymentId, userId);
                if (!isCurrentUserSameAsPaymentUser)
                {
                    return MessageConstants.PaymentNotFoundError;
                }

                bool isAccountAmountEnoughForProcessing = this.paymentRepository.IsAccountAmountEnoughForProcessing(paymentId);
                if (!isAccountAmountEnoughForProcessing)
                {
                    return MessageConstants.AccountNotEnoughAmountError;
                }

                bool processPaymentResult = this.paymentRepository.ProcessPayment(paymentId);
                if (!processPaymentResult)
                {
                    return MessageConstants.ProcessPaymentError;
                }

                return string.Empty;
            }
        }

        public string CancelPayment(int paymentId, int userId)
        {
            using (this.paymentRepository)
            {
                bool isPaymentExisting = this.paymentRepository.IsPaymentExisting(paymentId);
                if (!isPaymentExisting)
                {
                    return MessageConstants.PaymentNotFoundError;
                }

                bool isCurrentUserSameAsPaymentUser = this.paymentRepository.IsCurrentUserSameAsPaymentUser(paymentId, userId);
                if (!isCurrentUserSameAsPaymentUser)
                {
                    return MessageConstants.PaymentNotFoundError;
                }

                bool cancelPaymentResult = this.paymentRepository.CancelPayment(paymentId);
                if (!cancelPaymentResult)
                {
                    return MessageConstants.CancelPaymentError;
                }

                return null;
            }
        }
    }
}