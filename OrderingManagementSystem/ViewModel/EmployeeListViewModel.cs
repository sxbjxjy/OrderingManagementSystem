using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.ViewModel
{
    [CustomValidation(typeof(EmployeeListViewModel), "NoChecked")]
    public class EmployeeListViewModel
    {
        [DisplayName("社員番号")]
        public int EmpNo { get; set; }

        [DisplayName("担当者氏名")]

        public string EmpName { get; set; }

        public Boolean IsChecked { get; set; }


        public EmployeeListViewModel() { }

        public EmployeeListViewModel(Employee emp)
        {
            EmpNo = emp.EmpNo;
            EmpName = emp.EmpName;
        }


    }
}