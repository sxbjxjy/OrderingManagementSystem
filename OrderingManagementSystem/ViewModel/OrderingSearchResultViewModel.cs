using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderingManagementSystem.ViewModel
{
    public class OrderingSearchResultViewModel
    {
        [DisplayName("顧客ID")]
        [DisplayFormat(DataFormatString = "0:d6")]
        [StringLength(6, ErrorMessage = "{0}は{1}桁以内で入力してください")]
        [Range(0, 999999, ErrorMessage = "{0}は数値で入力してください")]
        public int CustomerId { get; set; }
        [DisplayName("注文番号")]
        [DisplayFormat(DataFormatString = "0:d6")]
        [StringLength(6, ErrorMessage = "{0}は{1}桁以内で入力してください")]
        [Range(0, 999999, ErrorMessage = "{0}は数値で入力してください")]
        public int OrderNo { get; set; }
        public int DetailNo { get; set; }
        [DisplayFormat(DataFormatString = "0:d3")]
        public int MeisaiNo { get; set; }
        [DisplayName("注文番号-明細")]
        public string OrderDetail { get; set; }
        public int ItemNo { get; set; }
        [DisplayName("商品名")]
        public string ItemName { get; set; }
        [DisplayName("数量")]
        [DisplayFormat(DataFormatString = "n*")]
        public decimal Quantity { get; set; }
        [DisplayName("希望納期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime DeliveryDate { get; set; }
        [DisplayName("受注日時")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime OrderDate { get; set; }

        [DisplayName("ステータス")]
        public string Status { get; set; }

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