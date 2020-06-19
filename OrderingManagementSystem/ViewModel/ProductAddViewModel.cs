using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class ProductAddViewModel
    {
        public int ItemNo { get; set; }
        public string ItemName { get; set; }
        public string PhotoUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public int UnitPrice { get; set; }

        [DisplayName("数量(半角)")]
        [Required(ErrorMessage = "数量を入力してください")]
        [RegularExpression("[0-9]+", ErrorMessage = "数量は数値で入力してください")]
        public int Quantity { get; set; }

        [DisplayName("希望納期 yyyy/mm/dd(半角)")]
        [Required(ErrorMessage = "希望納期を入力してください")]
        //[RegularExpression(@"[0-9]{4}/[0-9]{1,2}/[0-9]{1,2}", ErrorMessage ="希望納期はyyyy/mm/ddの形式で入力してください")]
        public DateTime DeliveryDate { get; set; }

        public ProductAddViewModel() { }
        public ProductAddViewModel(Product pro)
        {
            this.ItemName = pro.ItemName;
            this.ItemNo = pro.ItemNo;
            this.PhotoUrl = pro.PhotoUrl;
            this.UnitPrice = Convert.ToInt32(pro.UnitPrice);
            this.Quantity = 1;
            this.DeliveryDate = (DateTime.Now.Date).AddDays(3);
        }
    }
}