using OrderingManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OrderingManagementSystem.ViewModel
{
    public class EmployeeLoginViewModel
    {

        [Required(ErrorMessage = "{0}が入力されていません")]
        [DisplayName("社員番号")]
        public int EmpNo { get; set; }     
        public string EmpName { get; set; }

        [Required(ErrorMessage = "{0}が入力されていません")]
        [DisplayName("パスワード")]
        public string Password { get; set; }
        public string Password_verify { get; set; }     

    }
}