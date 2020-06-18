using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        [Key]
        public int CartNo { get; set; }

        public int ItemNo { get; set; }

        public int Quantity { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

    }
}