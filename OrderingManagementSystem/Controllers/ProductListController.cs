﻿using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.ViewModel;
using OrderingManagementSystem.DAL;

namespace OrderingManagementSystem.Controllers
{
    public class ProductListController : Controller
    {
        // GET: ProductList
        public ActionResult ProductCatalog()
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                var ul = db.Products.ToList();
                ViewBag.model = ul;

                for(int i=0; i < ul.Count(); i++)
                {
                    var stock = db.Products.Find(ul[i].ItemNo);
                    var od = (from a in db.OrderDetails
                              where a.ItemNo == stock.ItemNo && a.Status == 1
                              select (int?)a.Quantity).Sum() ?? 0;
                    stock.Stock = stock.Stock - od;
                }                               
            }
            return View();
        }
        public ActionResult ProductCatalog2()
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                var ul = db.Products
                    .OrderBy(e => e.ItemNo)
                    .Skip(0)
                    .Take(2);
                var dlist = new List<ProductViewModel>();
                foreach (var item in ul)
                {
                    var e = new ProductViewModel(item);
                    dlist.Add(e);
                }
                return View(dlist);
            }
        }

        public ActionResult ProductCatalog3()
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                var pList = db.Products.ToList();
                var pcList = new List<ProductCatalogViewModel>();

                for (int i = 0; i < pList.Count(); i++)
                {
                    var pc = new ProductCatalogViewModel(pList[i]);
                    int pNo = pList[i].ItemNo;
                    var odq = (from x in db.OrderDetails
                               where x.ItemNo == pNo && x.Status == 1
                               select (int?)x.Quantity).Sum() ?? 0;
                    pc.Stock -= odq;
                    pcList.Add(pc);
                }
                return View(pcList);
            }
        }
    }
}