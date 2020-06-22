﻿using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;
using OrderingManagementSystem.ViewModel;
using System.Security.Cryptography.X509Certificates;
using OrderingManagementSystem.DAL;

namespace OrderingManegimentSystem.Controllers
{
    public class EstimateController : Controller
    {
        //見積表示
        public ActionResult ShoppingCart()// int CustomerId
        {
            using (var db = new ModelContext())
            {
                int CustomerId = 3;//ユーザID
                var cdList = new List<ShoppingCartViewModel>();

                var cartList = (from e in db.CartDetails
                                where e.CustomerId == CustomerId//ユーザID
                                select e).ToList();

                for (int i = 0; i < cartList.Count(); i++)
                {
                    var a = new ShoppingCartViewModel(cartList[i]);
                    cdList.Add(a);
                }
                
                return View(cdList);
            }
        }

        //再見積
        public ActionResult ReShoppingCart(List<ShoppingCartViewModel> cdList)
        {
            using (var db = new ModelContext())
            {    
                foreach (var item in cdList)
                    {          
                      if(!ModelState.IsValid)
                      {
                        return View("ShoppingCart", cdList);
                      }
                        //数量ゼロチェック
                      if (item.Quantity <= 0)
                      {
                         ViewBag.N = false;
                         return View("ShoppingCart", cdList);
                      }

                        //在庫チェック



                        //希望納期チェック(過去or90日未来)
                        DateTime dfrom = DateTime.Now;
                        DateTime dto = item.DeliveryDate;
                        double interval = (dto - dfrom).TotalDays;
                        if (interval < 0 || interval > 90)
                        {
                            ViewBag.X = false;
                            return View("ShoppingCart", cdList);
                        }                     
                    }

                    //再見積
                    foreach (var item in cdList)
                    {
                        if (item.IsChecked == true)
                        {
                            var x = db.CartDetails.Find(item.CartNo);
                            db.CartDetails.Remove(x);
                            db.SaveChanges();
                        }
                        else
                        {
                            CartDetail y = db.CartDetails.Find(item.CartNo);
                            y.Quantity = item.Quantity;
                            y.DeliveryDate = item.DeliveryDate;
                            db.SaveChanges();
                        }
                    }

                    return Redirect("ShoppingCart");
                }
        }

        //再見積を押さずに注文
        public ActionResult ShoppingCartError(int id)
        {
            using (var db = new ModelContext())
            {
                ViewBag.D = false;
                int CustomerId = id;

                //以下、見積表示機能と同じ
                var cdList = new List<ShoppingCartViewModel>();

                var cartList = (from e in db.CartDetails
                                where e.CustomerId == CustomerId
                                select e).ToList();

                for (int i = 0; i < cartList.Count(); i++)
                {
                    var a = new ShoppingCartViewModel(cartList[i]);
                    cdList.Add(a);
                }             
                
                return View("ShoppingCart", cdList);
            }
        }

    }
}
 