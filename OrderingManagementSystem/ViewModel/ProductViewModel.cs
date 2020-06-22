using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.DAL;
using System.Web.Mvc;


namespace OrderingManagementSystem.ViewModels
{
    [CustomValidation(typeof(ProductViewModel),"ItemNoCheck")]
    public class ProductViewModel
    {

        public Boolean IsChecked { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "商品番号を入力してください。")]
        [Range(1, 9999, ErrorMessage = "商品番号は4桁以内の数値で入力してください。")]
        [DisplayFormat(DataFormatString = "{0:d4}")]
        public int ItemNo { get; set; }

        public static ValidationResult ItemNoCheck(ProductViewModel pvm)
        {
            using (var db = new ModelContext())
            {
                var ul = int.Parse(db.Products.Select(e => e.ItemNo).ToString());
                if (pvm.ItemNo == ul)
                {
                    return new ValidationResult("入力された社員IDはすでに登録されています。");
                }
                return ValidationResult.Success;
            }
        }

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
            , ErrorMessage ="#x#x#cmの形式で数値を入力してください。")]
        public string Size { get; set; }

        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:#}")]
        public int Stock { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public ProductViewModel() { }
        public ProductViewModel(Product prd)
        {
            ItemName = prd.ItemName;
            Category = prd.Category;
            ItemNo = prd.ItemNo;
            PhotoUrl = prd.PhotoUrl;
            UnitPrice = prd.UnitPrice;
            Author = prd.Author;
            Publisher = prd.Publisher;
            Overview = prd.Overview;
            Size = prd.Size;
            Type = prd.Type;
            Stock = prd.Stock;
            ReceiptDate = prd.ReceiptDate;
        }
    }

}