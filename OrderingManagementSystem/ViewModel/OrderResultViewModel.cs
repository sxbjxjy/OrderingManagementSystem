using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderResultViewModel
    {
        [DisplayFormat(DataFormatString ="{0:D6}")]
        public int OrderNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:D4}")]
        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        public OrderResultViewModel() { }

        public OrderResultViewModel(OrderDetail od)
        {
            this.OrderNo = od.OrderNo;
            this.ItemNo = od.ItemNo;
            this.ItemName = od.Product.ItemName;
            this.UnitPrice = od.Product.UnitPrice;
            this.Quantity = od.Quantity;
            this.DeliveryDate = od.DeliveryDate;
            this.Amount = od.Product.UnitPrice * od.Quantity;
        }

    }
}