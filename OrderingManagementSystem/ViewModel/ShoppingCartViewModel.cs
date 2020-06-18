using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OrderingManagementSystem.Models;


namespace OrderingManagementSystem.ViewModel
{
    public class ShoppingCartViewModel
    {
        public int CartNo { get; set; }

        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public string UnitPrice { get; set; }

        [DisplayName("数量（半角）")]
        [Required(ErrorMessage = "数量を入力してください")]
        [RegularExpression("[0-9]+", ErrorMessage = "数量は数値で入力してください")]
        public int Quantity { get; set; }

        [DisplayName("希望納期 yyyy/mm/dd（半角）")]
        [Required(ErrorMessage = "希望納期を入力してください")]
        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"\d{4}/\d{1,2}/\d{1,2}", ErrorMessage = "希望納期はyyyy/mm/ddの形式で入力してください")]
        public DateTime DeliveryDate { get; set; }

        public string Total { get; set; }

        public int CustomerId { get; set; }

        public bool IsChecked { get; set; }

        public ShoppingCartViewModel() { }
        public ShoppingCartViewModel(CartDetail cd)
        {
            this.CartNo = cd.CartNo;
            this.ItemNo = cd.ItemNo;
            this.ItemName = cd.Product.ItemName;
            this.UnitPrice = cd.Product.UnitPrice.ToString("C");
            this.Quantity = Convert.ToInt32(cd.Quantity);
            this.DeliveryDate = cd.DeliveryDate;
            this.Total = (cd.Quantity * cd.Product.UnitPrice).ToString("C");
            this.CustomerId = cd.CustomerId;
        }

    }
}