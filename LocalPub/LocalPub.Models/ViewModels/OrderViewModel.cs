using System;
using System.Collections.Generic;

namespace LocalPub.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(int id, DateTime orderDate, bool isCancelled)
        {
            this.Id = id;
            this.OrderDate = orderDate;
            this.IsCancelled = isCancelled;
            this.Meals = new List<string>();
        }

        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsCancelled { get; set; }

        public ICollection<string> Meals { get; set; }
    }
}