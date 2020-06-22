using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }


        [Key]
        [DisplayFormat(DataFormatString = "{0:d6}")]
        [DisplayName("顧客ID")]
        public int CustomerId { get; set; }

        [DisplayName("パスワード")]
        public string Password { get; set; }

        [DisplayName("会社名")]
        public string CompanyName { get; set; }

        [DisplayName("住所")]
        public string Address { get; set; }

        [DisplayName("電話番号")]
        public string Telno { get; set; }

        [DisplayName("氏名（漢字）")]
        public string CustomerName { get; set; }

        [DisplayName("氏名（かな）")]
        public string CustomerKana { get; set; }

        [DisplayName("部署")]
        public string Dept { get; set; }

        [DisplayName("メールアドレス")]
        public string Email { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}