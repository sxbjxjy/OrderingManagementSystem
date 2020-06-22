using OrderingManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingManagementSystem.Controllers
{
    public class StatusUpdateController : Controller
    {
        // GET: StatusUpdate
        public ActionResult OrderStatusUpdate(int detailNo)
        {
            ModelContext db = new ModelContext();
            ViewBag.model = db.OrderDetails.Find(detailNo);

            return View();
        }

        public ActionResult OrderStatusChange(int detailNo, int status)
        {
            ModelContext db = new ModelContext();
            var od = db.OrderDetails.Find(detailNo);
            od.Status = status;
            db.SaveChanges();
            ViewBag.model = db.OrderDetails.Find(detailNo);
            return View();
        }
    }
}