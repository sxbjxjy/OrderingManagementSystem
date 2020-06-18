using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderResultViewModel
    {
        public int OrderNo { get; set; }

        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public DateTime DeliveryDate { get; set; }

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