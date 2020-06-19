using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.Models
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            CartDetails = new HashSet<CartDetail>();
            Orders = new HashSet<Order>();
            //OrderDetails = new HashSet<OrderDetail>();
        }


        [Key]
        [DisplayFormat(DataFormatString = "{0:d6}")]
        public int CustomerId { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Telno { get; set; }

        public string CustomerName { get; set; }

        public string CustomerKana { get; set; }

        public string Dept { get; set; }

        public string Email { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}