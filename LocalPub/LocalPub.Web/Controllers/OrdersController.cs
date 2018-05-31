using LocalPub.Domain.Interfaces;
using LocalPub.Domain.Managers;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Models.ViewModels;
using LocalPub.Web.Filters;
using LocalPub.Web.ModelBinders;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static LocalPub.Common.WebConstants;
using static LocalPub.Common.MessageConstants;

namespace LocalPub.Web.Controllers
{
    [AuthorizeClient]
    public class OrdersController : Controller
    {
        private IOrderManager orderManager;

        public OrdersController()
            : this(new OrderManager())
        {
        }

        public OrdersController(IOrderManager clientManager)
        {
            this.orderManager = clientManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int userId = this.User.GetUserId();
            ICollection<OrderViewModel> orders = this.orderManager.GetAllOrdersForClient(userId);

            return this.View(orders);
        }

        [HttpGet]
        public ActionResult MakeOrder()
        {
            OrderMenuViewModel menu = this.orderManager.GetOrderMenu();

            ViewBag.AppetizersList = new SelectList(menu.Appetizers, "Id", "Name", "-- none --");
            ViewBag.MainCoursesList = new SelectList(menu.MainCourses, "Id", "Name", "-- none --");
            ViewBag.DessertsList = new SelectList(menu.Desserts, "Id", "Name", "-- none --");

            if (this.User.IsInRole(PriviligedClient))
            {
                return this.View(nameof(OrdersController.MakeOrderPrivileged));
            }

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeOrder([ModelBinder(typeof(DateModelBinder))]OrderBindingModel order)
        {
            return this.TrySaveOrder(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = PriviligedClient)]
        public ActionResult MakeOrderPrivileged([ModelBinder(typeof(DateModelBinder))]PrivilegedUserOrderBindingModel order)
        {
            return this.TrySaveOrder(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            int userId = this.User.GetUserId();

            bool cancelResult = this.orderManager.CancelOrder(id, userId, DateTime.Now);
            if (!cancelResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, CancelError);
            }
            else
            {
                this.TempData.Add(TempDataSuccessMessageKey, CancelSuccessful);
            }

            return this.RedirectToAction(nameof(OrdersController.Index), Orders);
        }

        private ActionResult TrySaveOrder(OrderBindingModel order)
        {
            int userId = this.User.GetUserId();
            order.ClientId = userId;

            bool saveResult = this.orderManager.SaveOrder(order);
            if (!saveResult)
            {
                this.TempData.Add(TempDataErrorMessageKey, OrderError);
                return this.MakeOrder();
            }

            this.TempData.Add(TempDataSuccessMessageKey, OrderSuccessful);
            return this.RedirectToAction(nameof(OrdersController.Index), Orders);
        }
    }
}