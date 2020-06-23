using OrderingManagementSystem.DAL;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingManagementSystem.Controllers
{
    public class EmployeeMasterController : Controller
    {
        // GET: EmployeeMaster
        public ActionResult EmployeeList()
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var empList = db.Employees.ToList();
                var elvmList = new List<EmployeeListViewModel>();
                foreach (var item in empList)
                {
                    var e = new EmployeeListViewModel(item);
                    elvmList.Add(e);
                }
                return View(elvmList);
            }
        }

        public ActionResult EmployeeAddInput()
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                return View();
            }
        }

        public ActionResult EmployeeAddConfirm(EmployeeInputViewModel evm)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("EmployeeAddInput");
                }
                Employee e = new Employee();
                e.EmpNo = evm.EmpNo;
                e.EmpName = evm.EmpName;
                e.Password = evm.Password;
                return View(e);
            }
        }
        public ActionResult EmployeeAdd(Employee emp)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                ViewBag.Add = 1;

                var empList = db.Employees.ToList();
                var elvmList = new List<EmployeeListViewModel>();
                foreach (var item in empList)
                {
                    var e = new EmployeeListViewModel(item);
                    elvmList.Add(e);
                }

                return View("EmployeeList", elvmList);
            }
        }

        public ActionResult EmployeeUpdateInput(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var emp = db.Employees.Find(id);
                var evm = new EmployeeUpdateInputViewModel();
                evm.EmpNo = emp.EmpNo;
                evm.EmpName = emp.EmpName;
                evm.Password = evm.Password;
                return View(evm);
            }
        }

        public ActionResult EmployeeUpdateConfirm(EmployeeUpdateInputViewModel evm)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("EmployeeUpdateInput", evm);
                }
                Employee e = new Employee();
                e.EmpNo = evm.EmpNo;
                e.EmpName = evm.EmpName;
                e.Password = evm.Password;
                return View(e);
            }
        }

        public ActionResult EmployeeUpdate(Employee emp)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                Employee e = db.Employees.Find(emp.EmpNo);
                e.EmpNo = emp.EmpNo;
                e.EmpName = emp.EmpName;
                e.Password = emp.Password;
                db.SaveChanges();

                ViewBag.Update = 1;

                var empList = db.Employees.ToList();
                var elvmList = new List<EmployeeListViewModel>();
                foreach (var item in empList)
                {
                    var ei = new EmployeeListViewModel(item);
                    elvmList.Add(ei);
                }

                return View("EmployeeList", elvmList);
            }
        }

        public ActionResult EmployeeDeleteConfirm(List<EmployeeListViewModel> elvmList)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                int countCheck = 0;
                foreach (var item in elvmList)
                {
                    if (item.IsChecked == true)
                    {
                        countCheck++;
                    }
                }

                if (countCheck == 0)
                {
                    ViewBag.NoChecked = 1;

                    var empList = db.Employees.ToList();
                    var elvmListDisplay = new List<EmployeeListViewModel>();
                    foreach (var item in empList)
                    {
                        var ei = new EmployeeListViewModel(item);
                        elvmListDisplay.Add(ei);
                    }
                    return View("EmployeeList", elvmListDisplay);
                }
                else
                {
                    return View(elvmList);
                }
            }
        }

        public ActionResult EmployeeDelete(List<EmployeeListViewModel> eivmList)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                foreach (var item in eivmList)
                {
                    if (item.IsChecked == true)
                    {
                        var emp = db.Employees.Find(item.EmpNo);
                        db.Employees.Remove(emp);
                    }
                }
                db.SaveChanges();

                ViewBag.Delete = 1;

                var empList = db.Employees.ToList();
                var elvmList = new List<EmployeeListViewModel>();
                foreach (var item in empList)
                {
                    var e = new EmployeeListViewModel(item);
                    elvmList.Add(e);
                }

                return View("EmployeeList", elvmList);
            }
        }
    }
}