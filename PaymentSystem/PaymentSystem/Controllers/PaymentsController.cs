using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PaymentSystem.Common;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.Managers;
using PaymentSystem.Extensions;
using PaymentSystem.Filters;
using PaymentSystem.Models.Models.Payments;
using PaymentSystem.Models.Payments;
using PaymentSystem.Models.ViewModels.Accounts;
using PaymentSystem.Models.ViewModels.Payments;

namespace PaymentSystem.Controllers
{
    [AuthorizeUser]
    public class PaymentsController : Controller
    {
        private IPaymentManager paymentManager;
        private IAccountManager accountManager;

        public PaymentsController()
            : this(new PaymentManager(), new AccountManager())
        {
        }

        public PaymentsController(IPaymentManager paymentManager, IAccountManager accountManager)
        {
            this.paymentManager = paymentManager;
            this.accountManager = accountManager;
        }

        public ActionResult Index(string order = DataConstants.Date)
        {
            int userId = this.User.GetUserId();
            ICollection<PaymentViewModel> payments = this.paymentManager.GetAllPaymentsByUserId(userId, order);

            return this.View(payments);
        }

        public ActionResult MakePayment()
        {
            int userId = this.User.GetUserId();
            MakePaymentBindingModel makePaymentModel = new MakePaymentBindingModel();
            makePaymentModel.AccountsSelectList = this.PrepareAccountsSelectList(userId);

            return this.View(makePaymentModel);
        }

        [HttpPost]
        public ActionResult MakePayment(MakePaymentBindingModel makePaymentModel)
        {
            int userId = this.User.GetUserId();

            if (!this.ModelState.IsValid)
            {
                makePaymentModel.AccountsSelectList = this.PrepareAccountsSelectList(userId);
                return this.View(makePaymentModel);
            }

            MakePaymentModel makePayment = new MakePaymentModel(makePaymentModel.AccountId, userId, makePaymentModel.PaymentIBAN, makePaymentModel.PaymentAmount, makePaymentModel.PaymentReason, DateTime.Now);

            string makePaymentResult = this.paymentManager.MakePayment(makePayment);
            if (string.IsNullOrEmpty(makePaymentResult))
            {
                this.TempData.AddSuccessMessage(MessageConstants.MakePaymentSuccess);
            }
            else
            {
                this.TempData.AddErrorMessage(makePaymentResult);
            }

            return this.RedirectToAction(nameof(PaymentsController.Index));
        }

        public ActionResult ProcessPayment(int id)
        {
            int userId = this.User.GetUserId();

            string processPaymentResult = this.paymentManager.ProcessPayment(id, userId);
            if (string.IsNullOrEmpty(processPaymentResult))
            {
                this.TempData.AddSuccessMessage(MessageConstants.ProcessPaymentSuccess);
            }
            else
            {
                this.TempData.AddErrorMessage(processPaymentResult);
            }

            return this.RedirectToAction(nameof(PaymentsController.Index));
        }

        public ActionResult CancelPayment(int id)
        {
            int userId = this.User.GetUserId();

            string cancelPaymentResult = this.paymentManager.CancelPayment(id, userId);
            if (string.IsNullOrEmpty(cancelPaymentResult))
            {
                this.TempData.AddSuccessMessage(MessageConstants.CancelPaymentSuccess);
            }
            else
            {
                this.TempData.AddErrorMessage(cancelPaymentResult);
            }

            return this.RedirectToAction(nameof(PaymentsController.Index));
        }

        private SelectList PrepareAccountsSelectList(int userId)
        {
            ICollection<AccountDescriptionViewModel> accounts = this.accountManager.GetAllAccountsForPaymentByUserId(userId);

            SelectList accountsSelectList = new SelectList(accounts, "Id", "Name");
            return accountsSelectList;
        }
    }
}