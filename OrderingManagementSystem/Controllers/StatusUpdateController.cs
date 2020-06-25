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

            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            return View();
        }

        //在庫数が少ないときに飛ばすアクション。
        public ActionResult OrderStatusUpdate2(int detailNo)
        {
            ModelContext db = new ModelContext();
            ViewBag.model = db.OrderDetails.Find(detailNo);
            ViewBag.stockerror = 1;
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            return View("OrderStatusUpdate");
        }

        public ActionResult OrderStatusChange(int detailNo, int status, int itemNo,  int quantity, int oldstatus)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            ModelContext db = new ModelContext();
            //ステータス変更処理。
            var od = db.OrderDetails.Find(detailNo);
            od.Status = status;
            db.SaveChanges();

            //Product在庫増減処理
            if (oldstatus == 1 && status == 2)//未出荷→出荷済
            {
                var pd = db.Products.Find(itemNo);
                if(pd.Stock < quantity)
                {
                    return RedirectToAction("OrderStatusUpdate2", "StatusUpdate", new {detailNo});
                }
                else
                {
                    pd.Stock -= quantity;
                    db.SaveChanges();
                }
            }
            else if (oldstatus == 2 && status == 1)//出荷済→未出荷
            {
                var pd = db.Products.Find(itemNo);
                pd.Stock += quantity;
                db.SaveChanges();
            }

            //Viewに値を渡す準備。
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