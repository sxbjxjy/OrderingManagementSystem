using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderStatusViewModel
    {
        public int OrderNo { get; set; }

        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        public int Status { get; set; }


        public OrderStatusViewModel() { }

        public OrderStatusViewModel(OrderDetail od)
        {
            this.OrderNo = od.OrderNo;
            this.ItemNo = od.ItemNo;
            this.ItemName = od.Product.ItemName;
            this.Quantity = od.Quantity;
            this.DeliveryDate = od.DeliveryDate;
            this.Status = od.Status;
        }

    }
}