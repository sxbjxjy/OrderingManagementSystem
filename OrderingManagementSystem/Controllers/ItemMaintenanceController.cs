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
                db.SaveChanges();
                return Redirect("/ItemMaintenance/InventoryDisplay");
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
                if (!ModelState.IsValid)
                {
                    return View("InventoryInformationUpdate");
                }
                var model = db.Products.Find(itemNo);
                model.Stock = model.Stock - stock;
                db.SaveChanges();
                return Redirect("/ItemMaintenance/InventoryDisplay");              
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
                return Redirect("/ItemMaintenance/InventoryDisplay");
            }
        }
    }
}