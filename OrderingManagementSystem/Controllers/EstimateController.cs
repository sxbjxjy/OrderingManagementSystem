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
        //見積表示
        public ActionResult ShoppingCart()
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                int CustomerId = (int)Session["Customer"];
                var cdList = new List<ShoppingCartViewModel>();

                var cartList = (from e in db.CartDetails
                                where e.CustomerId == CustomerId
                                select e).ToList();

                int stp = 0;

                for (int i = 0; i < cartList.Count(); i++)
                {
                    var a = new ShoppingCartViewModel(cartList[i]);
                    cdList.Add(a);
                    stp += (int)a.Total;
                }

                ViewBag.G = stp;

                return View(cdList);
            }
        }

        //再見積
        public ActionResult ReShoppingCart(List<ShoppingCartViewModel> cdList)
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                foreach (var item in cdList)
                {
                    if (!ModelState.IsValid)
                    {
                        return View("ShoppingCart");
                    }
                    //数量ゼロチェック
                    if (item.Quantity <= 0)
                    {
                        ViewBag.N = false;
                        return View("ShoppingCart");
                    }

                    //在庫不足チェック
                    int ctmId = item.CustomerId;

                    var s = (from v in cdList
                             group v by v.ItemNo).ToList();

                    for (int i = 0; i < s.Count(); i++)
                    {
                        var w = (from q in s[i]
                                 select q).ToList();
                        //注文しようとしている数量
                        var o = (from q in s[i]
                                 select q.Quantity).Sum();

                        //在庫から未発送を引く
                        var stock = db.Products.Find(w[0].ItemNo);

                        var y = (from a in db.OrderDetails
                                 where a.ItemNo == stock.ItemNo && a.Status == 1
                                 select (int?)a.Quantity).Sum() ?? 0;

                        int n = stock.Stock - y;
                        if (n < o)
                        {
                            ViewBag.Z = false;
                            return View("ShoppingCart");
                        }
                    }


                    //希望納期チェック(過去or90日未来)
                    DateTime dfrom = DateTime.Now;
                    DateTime dto = item.DeliveryDate;
                    double interval = (dto - dfrom).TotalDays;
                    if (interval < 0 || interval > 90)
                    {
                        ViewBag.X = false;
                        return View("ShoppingCart");
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
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
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

        //在庫不足注文
        public ActionResult ShoppingCartError2(int ctmId)
        {
            if (Session["Customer"] == null)
            {
                return Redirect("/CustomerLogin/CustomerLoginIndex");
            }
            using (var db = new ModelContext())
            {
                ViewBag.Y = false;
                int CustomerId = ctmId;

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
