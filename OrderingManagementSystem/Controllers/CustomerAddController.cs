using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.ViewModel;
using OrderingManagementSystem.DAL;

namespace Test.Controllers
{
    public class CustomerAddController : Controller
    {
        // GET: CustomerAdd
        public ActionResult CustomerSignUp()
        {            
            using (var db = new ModelContext())
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult CustomerSignUpComfirmation()
        {
            return Redirect("/CustomerLogin/CustomerLoginIndex");
        }
        [HttpPost]
        public ActionResult CustomerSignUpComfirmation(CustomerSignUpViewModel csvm)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerSignUp");
            }
            using (var db = new ModelContext())
            {
                Customer e = new Customer();
                e.CompanyName = csvm.CompanyName;
                e.Address = csvm.Address;
                e.CustomerKana = csvm.CustomerKana;
                e.CustomerName = csvm.CustomerName;
                e.Dept = csvm.Dept;
                e.Email = csvm.Email;
                e.Telno = csvm.Telno;
                e.Password = csvm.Password;

                return View(e);
            }
        }
        [HttpGet]
        public ActionResult CustomerSignUpDone()
        {
            return Redirect("/CustomerLogin/CustomerLoginIndex");
        }
        [HttpPost]
        public ActionResult CustomerSignUpDone(Customer customer)
        {
            using (var db = new ModelContext())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                ViewBag.Customer = customer;
                return View();
            }
        }
    }
}
