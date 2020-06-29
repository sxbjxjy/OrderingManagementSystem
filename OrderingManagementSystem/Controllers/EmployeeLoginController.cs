using OrderingManagementSystem.DAL;
using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.ViewModel;

namespace OrderingManagementSystem.Controllers
{
    public class EmployeeLoginController : Controller
    {
        // GET: EmployeeLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmployeeMain()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult EmployeeMain(EmployeeLoginViewModel eivm)
        {
            if (eivm.Password == null || eivm.EmpNo == 0)
            {
                return View("Login");
            }
            Employee employee = new Employee();
            employee.EmpNo = eivm.EmpNo;
            employee.Password = eivm.Password;
            using (var db = new ModelContext())
            {
                var ul = db.Employees.Find(employee.EmpNo);
                if (ul == null)
                {
                    ViewBag.IsAuth = false;
                    return View("Login");
                }
                else if (employee.EmpNo == ul.EmpNo && employee.Password == ul.Password)
                {
                    Session["Employee"] = ul.EmpNo;
                    Session["EmployeeName"] = ul.EmpName;
                    return View();
                }
                else
                {
                    ViewBag.IsAuth = false;
                    return View("Login");
                }
            }
        }
    }
}