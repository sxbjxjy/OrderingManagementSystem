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
        [DisplayFormat(DataFormatString = "{0:d6}")]
        public int OrderNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:d4}")]
        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        public int Status { get; set; }

        public string StatusDisplay { get; set; }

        public OrderStatusViewModel() { }

        public OrderStatusViewModel(OrderDetail od)
        {
            this.OrderNo = od.OrderNo;
            this.ItemNo = od.ItemNo;
            this.ItemName = od.Product.ItemName;
            this.Quantity = od.Quantity;
            this.DeliveryDate = od.DeliveryDate;
            this.Status = od.Status;

            if (this.Status == 1)
            {
                this.StatusDisplay = "未発送";
            }
            else if (this.Status == 2)
            {
                this.StatusDisplay = "発送済";
            }
            else if (this.Status == 3)
            {
                this.StatusDisplay = "キャンセル";
            }
            else if (this.Status == 4)
            {
                this.StatusDisplay = "入荷待ち";
            }

        }

    }
}