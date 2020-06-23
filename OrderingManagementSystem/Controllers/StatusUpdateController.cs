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
            if (ViewBag.model.Status == 1)
            {
                ViewBag.Status = "未出荷";
            }
            else if(ViewBag.model.Status == 2)
            {
                ViewBag.Status = "出荷済";
            }
            else if(ViewBag.model.Status == 3)
            {
                ViewBag.Status = "キャンセル";
            }
            else if(ViewBag.model.Status == 4)
            {
                ViewBag.Status = "入荷待ち";
            }
            return View();
        }
    }
}