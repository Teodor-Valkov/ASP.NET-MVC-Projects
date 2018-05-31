using LocalPub.Domain.Interfaces;
using LocalPub.Domain.SqlServer;
using LocalPub.Models;
using LocalPub.Models.BindingModels;
using LocalPub.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace LocalPub.Domain.Managers
{
    public class OrderManager : IOrderManager
    {
        private IMenuRepository menuRepository;
        private IOrdersRepository ordersRepository;

        public OrderManager()
            : this(new SqlMenuRepository(), new SqlOrdersRepository())
        {
        }

        public OrderManager(IMenuRepository menuRepository, IOrdersRepository ordersRepository)
        {
            this.menuRepository = menuRepository;
            this.ordersRepository = ordersRepository;
        }

        public ICollection<OrderViewModel> GetAllOrdersForClient(int clientId)
        {
            using (this.ordersRepository)
            {
                return this.ordersRepository.GetAllOrdersForClient(clientId);
            }
        }

        public OrderMenuViewModel GetOrderMenu()
        {
            using (this.menuRepository)
            {
                return this.menuRepository.GetOrderMenu();
            }
        }

        public bool CancelOrder(int orderId, int clientId, DateTime today)
        {
            using (this.ordersRepository)
            {
                bool isCurrentClientSameAsOrderClient = this.ordersRepository.IsCurrentClientSameAsOrderClient(orderId, clientId);
                if (!isCurrentClientSameAsOrderClient)
                {
                    return false;
                }

                bool isOrderDateInThePast = this.ordersRepository.IsOrderDateInThePast(orderId, today);
                if (isOrderDateInThePast)
                {
                    return false;
                }

                bool cancelResult = this.ordersRepository.CancelOrder(orderId);
                return cancelResult;
            }
        }

        public bool SaveOrder(OrderBindingModel order)
        {
            using (this.ordersRepository)
            {
                bool isClientAlreadyMadeOrder = this.ordersRepository.IsClientAlreadyMadeOrder(order.ClientId, order.OrderDate);
                if (isClientAlreadyMadeOrder)
                {
                    return false;
                }

                bool saveResult = this.ordersRepository.SaveOrder(order);
                return saveResult;
            }
        }
    }
}