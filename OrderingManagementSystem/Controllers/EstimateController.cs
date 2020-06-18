using OrderingManagementSystem.Models;
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
        // GET: Estimate
        public ActionResult ShoppingCart(int CustomerId)//ユーザのCustomerId取得
        {
            using (var db = new ModelContext())
            {
                var cdList = new List<ShoppingCartViewModel>();

                var cartList = (from e in db.CartDetails
                                where e.CustomerId == CustomerId
                                select e).ToList();

                for (int i = 0; i < cartList.Count(); i++)
                {
                    var a = new ShoppingCartViewModel(cartList[i]);
                    cdList.Add(a);
                }

                var list = (from x in db.CartDetails
                            join y in db.Products
                            on x.ItemNo equals y.ItemNo
                            select new
                            {
                                ItemNo = x.ItemNo,
                                ItemName = y.ItemName,
                                UnitPrice = y.UnitPrice,
                                Quantity = x.Quantity,
                                DeliveryDate = x.DeliveryDate,
                                Total = x.Quantity * y.UnitPrice,
                                CustomerId = x.CustomerId,

                            })
                            .Where(x => x.CustomerId == CustomerId);

                //カート内の小計
                decimal p = 0;
                foreach (var z in list)
                {
                    p += z.UnitPrice * z.Quantity;
                }
                ViewBag.P = String.Format("{0:#,0}", p);

                //消費税
                double p2 = Decimal.ToDouble(p);
                double t = 0;
                t = p2 * 0.1;
                ViewBag.T = String.Format("{0:#,0}", t);

                //カート内の合計
                double s = 0;
                s = p2 + t;
                ViewBag.S = String.Format("{0:#,0}", s);

                return View(cdList);
            }
        }

        //再見積
        public ActionResult ReShoppingCart(List<ShoppingCartViewModel> cdList)
        {
            using (var db = new ModelContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("ShoppingCart", cdList);
                }

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

                var list = (from x in db.CartDetails
                            join y in db.Products
                            on x.ItemNo equals y.ItemNo
                            select new
                            {
                                ItemNo = x.ItemNo,
                                ItemName = y.ItemName,
                                UnitPrice = y.UnitPrice,
                                Quantity = x.Quantity,
                                DeliveryDate = x.DeliveryDate,
                                Total = x.Quantity * y.UnitPrice,
                                CustomerId = x.CustomerId,

                            })
                            .Where(x => x.CustomerId == CustomerId);

                //カート内の小計を計算
                decimal p = 0;
                foreach (var z in list)
                {
                    p += z.UnitPrice * z.Quantity;
                }
                ViewBag.P = String.Format("{0:#,0}", p);

                //消費税を計算
                double p2 = Decimal.ToDouble(p);
                double t = 0;
                t = p2 * 0.1;
                ViewBag.T = String.Format("{0:#,0}", t);

                //カート内の合計金額を計算
                double s = 0;
                s = p2 + t;
                ViewBag.S = String.Format("{0:#,0}", s);

                return View("ShoppingCart", cdList);
            }
        }

    }
}