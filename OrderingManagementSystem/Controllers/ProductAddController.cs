﻿using System;
using OrderingManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.ViewModel;
using OrderingManagementSystem.DAL;
using System.Diagnostics.Eventing.Reader;

namespace OrderingManegimentSystem.Controllers
{
    public class ProductAddController : Controller
    {
        public ActionResult List(int ItemNo)
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                var item = db.Products.Find(ItemNo);

                var a = new ProductAddViewModel(item);
                return View(a);
            }
        }
        public ActionResult ProductAddResult(ProductAddViewModel pro)
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                //空欄、形式チェック
                if (!ModelState.IsValid)
                {
                    return View("List", pro);
                }
                //数量ゼロチェック
                if (pro.Quantity <= 0)
                {
                    ViewBag.N = false;
                    return View("List", pro);
                }
                //在庫チェック
                int x = pro.ItemNo;
                var stock = db.Products.Find(x);
                var y = (from a in db.OrderDetails
                        where a.ItemNo == x && a.Status == 1
                        select (int?)a.Quantity).Sum() ?? 0;
               
                int s = stock.Stock - y;
                    if (s < pro.Quantity)
                    {
                        ViewBag.E = false;
                        return View("List", pro);
                    }
                    
                //希望納期チェック(過去or90日以上未来)
                DateTime dfrom = DateTime.Now;
                DateTime dto = pro.DeliveryDate;
                double interval = (dto - dfrom).TotalDays;
                if (interval < 0 || interval > 90)
                {
                    ViewBag.D = false;
                    return View("List", pro);
                }

                //カートへ追加
                var u = new CartDetail
                {
                    ItemNo = pro.ItemNo,
                    Quantity = pro.Quantity,
                    DeliveryDate = pro.DeliveryDate,
                    CustomerId = (int)Session["Customer"]               
                };

                //時刻除外）
                string result = pro.DeliveryDate.ToString();
                ViewBag.Date = result.Substring(0, 10);
                //単価を三桁区切り
                ViewBag.UnitP = String.Format("{0:#,0}", pro.UnitPrice);
                //合計金額を三桁区切り
                var price = pro.UnitPrice * pro.Quantity;
                ViewBag.Price = String.Format("{0:#,0}", price);

                ViewBag.ItemN = pro.ItemName;
                ViewBag.Result = u;
                ViewBag.Url = stock.PhotoUrl;

                db.CartDetails.Add(u);
                db.SaveChanges();

                return View();
            }
        }
    }
}