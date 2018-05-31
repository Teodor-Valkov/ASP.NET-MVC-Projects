using System;
using System.Collections.Generic;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Models.ViewModels;

namespace LocalPub.Domain.Interfaces
{
    public interface IOrderManager
    {
        ICollection<OrderViewModel> GetAllOrdersForClient(int clientId);

        OrderMenuViewModel GetOrderMenu();

        bool SaveOrder(OrderBindingModel order);

        bool CancelOrder(int orderId, int clientId, DateTime today);
    }
}