using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingManagementSystem.Controllers
{
    public class CustomerLogoutController : Controller
    {
        public ActionResult Index()
        {
            Session.Abandon();
            return Redirect("/CustomerLogin/CustomerLoginIndex");
        }
    }
}