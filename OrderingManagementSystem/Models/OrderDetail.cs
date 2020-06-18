using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int OrderNo { get; set; }

        [Key]
        public int DetailNo { get; set; }

        public int MeisaiNo { get; set; }

        public int ItemNo { get; set; }

        public int Quantity { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Status { get; set; }

        //public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}