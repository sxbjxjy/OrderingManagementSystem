using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.DAL;
using OrderingManagementSystem.ViewModel;

namespace Test.Controllers
{
    public class CustomerLoginController : Controller
    {
        // GET: CustomerLogin
        public ActionResult CustomerLoginIndex()
        {
            using (var db = new ModelContext())
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult CustomerMainMenu()
        {
            using (var db = new ModelContext())
            {
                return View("CustomerLoginIndex");
            }
        }
        [HttpPost]
        public ActionResult CustomerMainMenu(CustomerSignUpViewModel csvm)
        {
            if(csvm.Password == null || csvm.CustomerId == 0)
            {
                return View("CustomerLoginIndex");
            }            
            Customer customer = new Customer();
            customer.CustomerId = csvm.CustomerId;
            customer.Password = csvm.Password;
            using (var db = new ModelContext())
            {
                var ul = db.Customers.Find(customer.CustomerId);
                if(ul == null)
                {
                    ViewBag.IsAuth = false;
                    return View("CustomerLoginIndex");
                }
                else if (customer.CustomerId == ul.CustomerId && customer.Password == ul.Password)
                {
                    Session["Customer"] = ul.CustomerName;
                    return View();
                }
                else
                {
                    ViewBag.IsAuth = false;
                    return View("CustomerLoginIndex");
                }
            }
        }
    }
}