using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderCheckViewModel
    {
        public int ItemNo { get; set; }
        public int Quantity { get; set; }

        public OrderCheckViewModel() { }
        public OrderCheckViewModel(CartDetail cart)
        {
            this.ItemNo = cart.ItemNo;
            this.Quantity = cart.Quantity;
        }
    }
}