using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemNo { get; set; }

        public string ItemName { get; set; }

        public string PhotoUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Overview { get; set; }

        public string Size { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public int Stock { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}