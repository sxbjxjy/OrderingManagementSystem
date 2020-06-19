using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderingSearchResultViewModel
    {
        [DisplayName("注文番号")]
        public int OrderNo { get; set; }
        public int DetailNo { get; set; }
        public int MeisaiNo { get; set; }
        [DisplayName("注文番号-明細")]
        public string OrderDetail { get; set; }
        public int ItemNo { get; set; }
        [DisplayName("商品名")]
        public string ItemName { get; set; }
        [DisplayName("数量")]
        public decimal Quantity { get; set; }
        [DisplayName("希望納期")]
        public System.DateTime DeliveryDate { get; set; }
        [DisplayName("受注日時")]
        public System.DateTime OrderDate { get; set; }

        [DisplayName("ステータス")]
        public string Status { get; set; }
        [DisplayName("顧客ID")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        public OrderingSearchResultViewModel() { }
        public OrderingSearchResultViewModel(OrderDetail osr)
        {
            this.DetailNo = osr.DetailNo;
            this.OrderDetail = osr.OrderNo + "-" + osr.MeisaiNo;
            this.ItemNo = osr.ItemNo;
            this.ItemName = osr.Product.ItemName;
            this.Quantity = osr.Quantity;
            this.DeliveryDate = osr.DeliveryDate;
            this.Status = osr.Status;
        }
    }
}