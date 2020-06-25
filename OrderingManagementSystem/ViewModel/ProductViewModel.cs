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
    public class ProductViewModel
    {

        public Boolean IsChecked { get; set; }
        public string Category { get; set; }

        public static ValidationResult ItemNoCheck(int itemNo)
        {
            using (var db = new ModelContext())
            {
                var query = (from e in db.Products
                             where e.ItemNo == itemNo
                             select e).ToList();
                if (query.Count() > 0)
                {
                    return new ValidationResult("入力された商品番号は既に登録されています");
                }
                return ValidationResult.Success;
            }
        }
        [Required(ErrorMessage = "商品番号を入力してください。")]
        [Range(1, 9999, ErrorMessage = "商品番号は4桁以内の数値で入力してください。")]
        [CustomValidation(typeof(ProductViewModel), "ItemNoCheck")]
        [DisplayFormat(DataFormatString = "{0:d4}")]
        public int ItemNo { get; set; }

        [Required(ErrorMessage = "商品写真を入力してください。")]
        [Url(ErrorMessage = "商品写真はURL形式で入力して下さい。")]
        public string PhotoUrl { get; set; }

        public static ValidationResult ItemNameCheck(string itemName)
        {
            using (var db = new ModelContext())
            {
                var query = (from e in db.Products
                             where e.ItemName == itemName
                             select e).ToList();
                if (query.Count() > 0)
                {
                    return new ValidationResult("入力された商品名は既に登録されています");
                }
                return ValidationResult.Success;
            }
        }
        [Required(ErrorMessage = "商品名を入力してください。")]
        [CustomValidation(typeof(ProductViewModel), "ItemNameCheck")]
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