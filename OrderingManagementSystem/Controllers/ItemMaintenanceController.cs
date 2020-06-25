using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.ViewModel;
using OrderingManagementSystem.DAL;

namespace OrderingManagementSystem.Controllers
{
    public class ItemMaintenanceController : Controller
    {
        // GET: ItemMaintenance
        public ActionResult InventoryDisplay()
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var ul = db.Products.ToList();
                var dlist = new List<ProductViewModel>();
                foreach (var item in ul)
                {
                    var e = new ProductViewModel(item);
                    dlist.Add(e);
                }
                
                return View(dlist);
            }
        }
        public ActionResult InventoryInformationUpdate(int id)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                ViewBag.model = db.Products.Find(id);
                Session["list"] = db.Products.Find(id);
                return View();
            }
        }
        public ActionResult ArraivalRedirect(int itemNo, int stock)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var model = db.Products.Find(itemNo);
                model.Stock = stock + model.Stock;
                
                var pdList = db.Products.ToList();
                var List = new List<ProductViewModel>();
                foreach (var item in pdList)
                {
                    var e = new ProductViewModel(item);
                    List.Add(e);
                }

                db.SaveChanges();
                ViewBag.Arraival = "在庫を入荷しました";
                return View("InventoryDisplay",List);
            }
        }
        public ActionResult DecreaseRedirect(int itemNo, int stock)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var model = db.Products.Find(itemNo);

                if(model.Stock == 0)
                {
                    var a = (Product)Session["list"];
                    ViewBag.model = a;
                    ViewBag.Zero = "在庫数量の減少に失敗しました。";
                    return View("InventoryInformationUpdate");
                }

                model.Stock = model.Stock - stock;
                               
                if(model.Stock < 0)
                {
                    model.Stock = model.Stock + stock;
                    var a = (Product)Session["list"];
                    ViewBag.model = a;
                    ViewBag.Stock = "在庫数量が不足しています。";
                    return View("InventoryInformationUpdate");
                }
                else
                {
                    var pdList = db.Products.ToList();
                    var List = new List<ProductViewModel>();
                    foreach (var item in pdList)
                    {
                        var e = new ProductViewModel(item);
                        List.Add(e);
                    }
                    db.SaveChanges();
                    ViewBag.Decrease = "在庫を減少しました。";
                    return View("InventoryDisplay", List);
                }              
            }
        }
        public ActionResult UpdateRedirect(int itemNo, DateTime receiptDate)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                var model = db.Products.Find(itemNo);
                model.ReceiptDate = receiptDate;
                db.SaveChanges();
                ViewBag.Update = "入荷予定日を更新しました。";
                var pdList = db.Products.ToList();
                var List = new List<ProductViewModel>();
                foreach (var item in pdList)
                {
                    var e = new ProductViewModel(item);
                    List.Add(e);
                }
                return View("InventoryDisplay", List);
            }
        }
    }
}