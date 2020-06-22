using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingManagementSystem.ViewModel;
using OrderingManagementSystem.DAL;

namespace OrderingManegimentSystem.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult OrderResult(List<ShoppingCartViewModel> x)
        {
            using (var db = new ModelContext())
            {
                //削除チェックが入っている
                foreach (var item in x)
                {
                    if (item.IsChecked == true)
                    {
                        int id = item.CustomerId;
                        return RedirectToAction("ShoppingCartError", "Estimate", new { id });
                    }
                }
                //数量、納期が変更されている
                foreach(var item in x)
                {
                    var list = db.CartDetails.Find(item.CartNo);
                    if(list.Quantity != item.Quantity)
                    {
                        int id = item.CustomerId;
                        return RedirectToAction("ShoppingCartError", "Estimate", new { id });
                    }
                    if(list.DeliveryDate != item.DeliveryDate)
                    {
                        int id = item.CustomerId;
                        return RedirectToAction("ShoppingCartError", "Estimate", new { id });
                    }
                }

                //在庫不足チェック
                int ctmId = x.First().CustomerId;
             
                var s = from t in db.CartDetails
                        where t.CustomerId == ctmId
                        select t;
                var u = (from v in s
                        group v by v.ItemNo).ToList();

                for (int i = 0; i < u.Count(); i++)
                {
                    var w = (from q in u[i]
                            select q).ToList();
                    //注文しようとしている数量
                    var o = (from q in u[i]
                            select q.Quantity).Sum();

                    //在庫から未発送を引く(error)
                    var stock = db.Products.Find(w[0].ItemNo);
                               
                    var y = (from a in db.OrderDetails
                            where a.ItemNo == stock.ItemNo && a.Status == 1
                            select (int?)a.Quantity).Sum()?? 0;
                   
                    int n = stock.Stock - y;
                    if(n < o)
                    {
                        return RedirectToAction("ShoppingCartError2", "Estimate", new { ctmId });
                    }
                }
               
                //注文追加
                var cartList = (from cl in db.CartDetails
                                where cl.CustomerId == ctmId
                                select cl).ToList();

                var orderNew = new Order();
                orderNew.CustomerId = ctmId;
                orderNew.OrderDate = DateTime.Now;
                db.Orders.Add(orderNew);
                db.SaveChanges();

                int j = 1;
                for (int i = 0; i < cartList.Count(); i++)
                {
                    var orderDetailNew = new OrderDetail();
                    orderDetailNew.OrderNo = orderNew.OrderNo;
                    orderDetailNew.MeisaiNo = j;
                    orderDetailNew.ItemNo = cartList[i].ItemNo;
                    orderDetailNew.Quantity = cartList[i].Quantity;
                    orderDetailNew.DeliveryDate = cartList[i].DeliveryDate;
                    orderDetailNew.Status = 1;
                    orderDetailNew.Product = cartList[i].Product;
                    orderDetailNew.Order = orderNew;
                    db.OrderDetails.Add(orderDetailNew);

                    j += 1;
                }
                db.SaveChanges();

                for (int i = 0; i < cartList.Count(); i++)
                {
                    int cn = cartList[i].CartNo;
                    var cart = db.CartDetails.Find(cn);
                    db.CartDetails.Remove(cart);
                }
                db.SaveChanges();

                var orderDetailResultList = new List<OrderResultViewModel>();

                int odn = orderNew.OrderNo;
                var orderDetailList = (from od in db.OrderDetails
                                       where od.OrderNo == odn
                                       select od).ToList();
                for (int i = 0; i < orderDetailList.Count(); i++)
                {
                    var orderDetailResult = new OrderResultViewModel(orderDetailList[i]);
                    orderDetailResultList.Add(orderDetailResult);
                }

                return View(orderDetailResultList);
            }
        }
    }
}