using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class ProductCatalogViewModel
    {
        [DisplayFormat(DataFormatString = "{0:d4}")]
        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public string PhotoUrl { get; set; }

        public string UnitPrice { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Overview { get; set; }

        public string Size { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public int Stock { get; set; }

        public ProductCatalogViewModel() { }

        public ProductCatalogViewModel(Product prd)
        {
            this.ItemNo = prd.ItemNo;
            this.ItemName = prd.ItemName;
            this.PhotoUrl = prd.PhotoUrl;
            this.UnitPrice = prd.UnitPrice.ToString("C");
            this.Author = prd.Author;
            this.Publisher = prd.Publisher;
            this.Overview = prd.Overview;
            this.Size = prd.Size;
            this.Type = prd.Type;
            this.Category = prd.Category;
            this.Stock = prd.Stock;
        }
    }
}