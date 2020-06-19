using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("社員番号")]
        public int EmpNo { get; set; }

        [DisplayName("担当者氏名")]
        public string EmpName { get; set; }

        [DisplayName("パスワード")]
        public string Password { get; set; }

    }
}