using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.DAL;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.ViewModel;

namespace OrderingManegimentSystem.Controllers
{
    public class ProductMaintenanceController : Controller
    {
        // GET: ProductMaintenance
        public ActionResult List()
        {
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
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddCheck(ProductViewModel pvm)
        {
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("Add");
                }
                Product e = new Product();
                e.Category = pvm.Category;
                e.ItemNo = pvm.ItemNo;
                e.PhotoUrl = pvm.PhotoUrl;
                e.ItemName = pvm.ItemName;
                e.UnitPrice = pvm.UnitPrice;
                e.Author = pvm.Author;
                e.Publisher = pvm.Publisher;
                e.Overview = pvm.Overview;
                e.Size = pvm.Size;
                e.Type = pvm.Type;
                e.Stock = 0;
                return View(e);
            }
        }
        public ActionResult AddRedirect(Product prd)
        {
            using (var db = new ModelContext())
            {
                db.Products.Add(prd);
                db.SaveChanges();
                ViewBag.Add = 1;
                return Redirect("List");
                
            }
        }

        public ActionResult Update(int id)
        {
            /*using (var db = new ModelContext())
            {
                ViewBag.model3 = db.Products.Find(id);
                return View();
            }*/

            using (var db = new ModelContext())
            {
                var pvm = db.Products.Find(id);
                var e = new ProductViewModel2();
                e.Category = pvm.Category;
                e.ItemNo = pvm.ItemNo;
                e.PhotoUrl = pvm.PhotoUrl;
                e.ItemName = pvm.ItemName;
                e.UnitPrice = pvm.UnitPrice;
                e.Author = pvm.Author;
                e.Publisher = pvm.Publisher;
                e.Overview = pvm.Overview;
                e.Size = pvm.Size;
                e.Type = pvm.Type;
                e.Stock = pvm.Stock;
                ViewBag.model3 = e;
                return View(e);
            }
        }

        public ActionResult UpdateCheck(ProductViewModel2 pvm)
        {
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("UpDate");
                }
                Product e = new Product();
                e.Category = pvm.Category;
                e.ItemNo = pvm.ItemNo;
                e.PhotoUrl = pvm.PhotoUrl;
                e.ItemName = pvm.ItemName;
                e.UnitPrice = pvm.UnitPrice;
                e.Author = pvm.Author;
                e.Publisher = pvm.Publisher;
                e.Overview = pvm.Overview;
                e.Size = pvm.Size;
                e.Type = pvm.Type;
                e.Stock = pvm.Stock;
                ViewBag.model4 = e;
                return View(e);
            }
        }
        public ActionResult UpdateRedirect(string category, int itemNo, string photoUrl, string itemName, int unitPrice,
            string author, string publisher, string overview, string size, string type, int stock)
        {
            using (var db = new ModelContext())
            {
                var model5 = db.Products.Find(itemNo);
                model5.Category = category;
                model5.ItemNo = itemNo;
                model5.PhotoUrl = photoUrl;
                model5.ItemName = itemName;
                model5.UnitPrice = unitPrice;
                model5.Author = author;
                model5.Publisher = publisher;
                model5.Overview = overview;
                model5.Size = size;
                model5.Type = type;
                model5.Stock = stock;
                db.SaveChanges();
                ViewBag.Update = 1;
                return Redirect("/ProductMaintenance/List");
            }
        }
        public ActionResult Delete(List<ProductViewModel> dlist)
        {
            using (var db = new ModelContext())
            {
                int countCheck = 0;
                foreach (var item in dlist)
                {
                    if (item.IsChecked == true)
                    {
                        countCheck++;
                    }
                }
                if (countCheck == 0)
                {
                    ViewBag.NoChecked = 1;

                    var pdList = db.Products.ToList();
                    var elvmListDisplay = new List<ProductViewModel>();
                    foreach (var item in pdList)
                    {
                        var ei = new ProductViewModel(item);
                        elvmListDisplay.Add(ei);
                    }
                    return View("List", elvmListDisplay);
                }
                else
                {
                    return View(dlist);
                }
                    
            }
        }

        public ActionResult DeleteRedirect(List<ProductViewModel> dlist)
        {
            using (var db = new ModelContext())
            {

                foreach (var item in dlist)
                {
                    if (item.IsChecked == true)
                    {
                        var model7 = db.Products.Find(item.ItemNo);
                        db.Products.Remove(model7);
                    }

                }
                db.SaveChanges();
                ViewBag.Delete = 1;
                return Redirect("/ProductMaintenance/List");
            }
        }

    }
}