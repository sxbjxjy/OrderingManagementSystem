using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class EmployeeUpdateInputViewModel
    {
        [Required(ErrorMessage = "{0}を入力してください")]
        [DisplayName("社員番号")]
        [Range(1000, 9999, ErrorMessage = "{0}は1000から始まる4桁の数値で入力してください")]
        public int EmpNo { get; set; }

        [Required(ErrorMessage = "{0}を入力してください")]
        [DisplayName("担当者氏名")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "{0}を入力してください")]
        [DisplayName("パスワード")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "{0}は半角英数字で入力してください")]
        public string Password { get; set; }

        [DisplayName("パスワード（確認）")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "パスワードとパスワード(確認)が一致しない")]
        public string Password_verify { get; set; }
    }
}