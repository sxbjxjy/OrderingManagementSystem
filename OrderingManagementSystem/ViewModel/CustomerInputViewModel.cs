using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    public class CustomerInputViewModel
    {
        [DisplayName("顧客ID")]
        [DisplayFormat(DataFormatString = "{0:d6}")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "{0}が入力されていません")]
        [DisplayName("パスワード")]
        [RegularExpression("[a-zA-Z0-9]*", ErrorMessage = "認証用パスワードは半角英数字のみで入力してください")]
        public string Password { get; set; }

        [DisplayName("会社名")]
        [Required(ErrorMessage = "{0}を入力してください")]
        public string CompanyName { get; set; }

        [DisplayName("住所")]
        [Required(ErrorMessage = "{0}を入力してください")]
        public string Address { get; set; }

        [DisplayName("電話番号")]
        [Required(ErrorMessage = "{0}を入力してください")]
        [RegularExpression("[0-9]{2,4}-[0-9]{4}-[0-9]{4}", ErrorMessage = "{0}は、xxxx-yyyy-zzzzの形式で入力してください")]
        public string Telno { get; set; }

        [DisplayName("氏名（漢字）")]
        [Required(ErrorMessage = "{0}を入力してください")]
        public string CustomerName { get; set; }

        [DisplayName("氏名（かな）")]
        [Required(ErrorMessage = "氏名（かな）を入力してください")]
        [RegularExpression("[あ-ん|　]*", ErrorMessage = "{0}は、全角ひらがなと空白で入力してください")]
        public string CustomerKana { get; set; }

        [DisplayName("部署")]
        public string Dept { get; set; }

        [DisplayName("メールアドレス")]
        [RegularExpression("[a-zA-Z0-9!-/:-@¥[-`{-~]+@[a-zA-Z0-9!-/:-@¥[-`{-~]+", ErrorMessage = "メールアドレスは、半角数字と記号のみで、xxxx@yyyyの形式で入力してください")]
        public string Email { get; set; }

        [DisplayName("パスワード（確認）")]
        [Compare("Password", ErrorMessage = "パスワードとパスワード(確認)が一致しない")]
        public string Password_verify { get; set; }

        public CustomerInputViewModel() { }

        public CustomerInputViewModel(Customer ctm)
        {
            this.CustomerId = ctm.CustomerId;
            this.CompanyName = ctm.CompanyName;
            this.Address = ctm.Address;
            this.Telno = ctm.Telno;
            this.CustomerName = ctm.CustomerName;
            this.CustomerKana = ctm.CustomerKana;
            this.Dept = ctm.Dept;
            this.Email = ctm.Email;
            this.Password = ctm.Password;
        }
    }
}