using LocalPub.Models.BindingModels;
using LocalPub.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace LocalPub.Domain.Interfaces
{
    public interface IOrdersRepository : IDbRepository
    {
        ICollection<OrderViewModel> GetAllOrdersForClient(int clientId);

        bool IsCurrentClientSameAsOrderClient(int orderId, int clientId);

        bool IsClientAlreadyMadeOrder(int clientId, DateTime date);

        bool IsOrderDateInThePast(int orderId, DateTime today);

        bool CancelOrder(int orderId);

        bool SaveOrder(OrderBindingModel order);
    }
}