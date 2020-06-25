using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.DAL;
using System.Web.Mvc;


namespace OrderingManagementSystem.ViewModel
{
    public class ProductViewModel2
    {
        public string Category { get; set; }

        [Required(ErrorMessage = "商品番号を入力してください。")]
        [Range(1, 9999, ErrorMessage = "商品番号は4桁以内の数値で入力してください。")]
        [DisplayFormat(DataFormatString = "{0:d4}")]
        public int ItemNo { get; set; }

        [Required(ErrorMessage = "商品写真を入力してください。")]
        [Url(ErrorMessage = "商品写真はURL形式で入力して下さい。")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "商品名を入力してください。")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "単価を入力してください。")]
        [Range(1, 99999999, ErrorMessage = "単価は数値で入力してください。")]
        [DisplayFormat(DataFormatString = "{0:c}-")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "著者を入力してください。")]
        public string Author { get; set; }

        [Required(ErrorMessage = "出版社を入力してください。")]
        public string Publisher { get; set; }

        public string Overview { get; set; }

        [Required(ErrorMessage = "寸法を入力してください。")]
        [RegularExpression("[0-9]{1,}x[0-9]{1,}x[0-9]{1,}cm"
            , ErrorMessage = "#x#x#cmの形式で数値を入力してください。")]
        public string Size { get; set; }

        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:#}")]
        public int Stock { get; set; }

        public DateTime? ReceiptDate { get; set; }

    }
}