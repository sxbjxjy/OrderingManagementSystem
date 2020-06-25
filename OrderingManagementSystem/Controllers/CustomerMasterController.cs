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
    public class CustomerMasterController : Controller
    {
        // GET: CustomerMaster
        public ActionResult CustomerList()
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                return View(db.Customers.ToList());

            }
        }

        public ActionResult CustomerUpdateInput(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                Customer ctm = db.Customers.Find(id);
                var civm = new CustomerInputViewModel(ctm);
                return View(civm);
            }
        }


        public ActionResult CustomerUpdateInputClear(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var civm = new CustomerInputViewModel();
                civm.CustomerId = id;
                return View("CustomerUpdateInput", civm);
            }
        }

        public ActionResult CustomerUpdateConfirm(CustomerInputViewModel civm)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("CustomerUpdateInput", civm);
                }
                Customer ctm = new Customer();
                ctm.CustomerId = civm.CustomerId;
                ctm.CompanyName = civm.CompanyName;
                ctm.Address = civm.Address;
                ctm.Telno = civm.Telno;
                ctm.CustomerName = civm.CustomerName;
                ctm.CustomerKana = civm.CustomerKana;
                ctm.Dept = civm.Dept;
                ctm.Email = civm.Email;
                ctm.Password = civm.Password;
                return View(ctm);
            }
        }

        public ActionResult CustomerUpdate(Customer ctm)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                Customer c = db.Customers.Find(ctm.CustomerId);
                c.CompanyName = ctm.CompanyName;
                c.Address = ctm.Address;
                c.Telno = ctm.Telno;
                c.CustomerName = ctm.CustomerName;
                c.CustomerKana = ctm.CustomerKana;
                c.Dept = ctm.Dept;
                c.Email = ctm.Email;
                c.Password = ctm.Password;
                db.SaveChanges();

                ViewBag.Update = 1;

                var ctmList = db.Customers.ToList();
                return View("CustomerList",ctmList);

            }
        }

        public ActionResult CustomerDeleteConfirm(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                Customer ctm = db.Customers.Find(id);
                return View(ctm);
            }
        }

        public ActionResult CustomerDelete(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                Customer ctm = db.Customers.Find(id);
                db.Customers.Remove(ctm);
                db.SaveChanges();

                ViewBag.Delete = 1;

                var ctmList = db.Customers.ToList();
                return View("CustomerList", ctmList);

            }
        }
    }
}