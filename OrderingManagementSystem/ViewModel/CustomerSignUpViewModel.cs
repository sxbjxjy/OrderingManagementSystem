using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OrderingManagementSystem.Models;


namespace OrderingManagementSystem.ViewModel
{
    public class CustomerSignUpViewModel
    {
        [Required(ErrorMessage = "{0}が入力されていません")]
        [DisplayName("顧客ID")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "{0}が入力されていません")]
        [DisplayName("パスワード")]
        [RegularExpression
           ("[a-zA-Z0-9]*", ErrorMessage = "認証用パスワードは半角英数字のみで入力してください")]
        public string Password { get; set; }
        [Required(ErrorMessage = "パスワード(確認)が入力されていません")]
        [Compare("Password", ErrorMessage = "パスワードとパスワード(確認)が一致しない。")]
        public string Password_verify { get; set; }

        [Required(ErrorMessage = "会社名を入力してください")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "電話番号を入力してください")]
        [RegularExpression("[0-9]{2,4}-[0-9]{4}-[0-9]{4}", ErrorMessage = "電話番号は、xxxx-yyyy-zzzzの形式で入力してください")]
        public string Telno { get; set; }
        [Required(ErrorMessage = "氏名（漢字）を入力してください")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "氏名（かな）を入力してください")]
        [RegularExpression("[あ-ん]",ErrorMessage = "氏名（かな）は、全角ひらがなと空白で入力してください")]
        public string CustomerKana { get; set; }
        public string Dept { get; set; }
        [RegularExpression
           ("[a-zA-Z0-9!-/:-@¥[-`{-~]+@[a-zA-Z0-9!-/:-@¥[-`{-~]+", ErrorMessage = "メールアドレスは、半角数字と記号のみで、xxxx@yyyyの形式で入力してください")]
        public string Email { get; set; }
        [Required(ErrorMessage = "住所を入力してください。")]
        public string Address { get; set; }
        public CustomerSignUpViewModel() { }

        public CustomerSignUpViewModel(Customer ctm)
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