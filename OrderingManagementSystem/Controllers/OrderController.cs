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
        public ActionResult OrderResult(List<ShoppingCartViewModel> x)//int ctmId
        {
            using (var db = new ModelContext())
            {
                foreach (var item in x)
                {
                    if (item.IsChecked == true)
                    {
                        int id = item.CustomerId;
                        return RedirectToAction("ShoppingCartError", "Estimate", new { id });
                    }

                }


                var cartList = (from cl in db.CartDetails
                                where cl.CustomerId == ctmId
                                select cl).ToList();

                var orderNew = new Order();
                orderNew.CustomerId = ctmId;
                //OderDate 追加
                //orderNew.OrderDate = DateTime.Now;
                db.Orders.Add(orderNew);
                db.SaveChanges();

                for (int i = 0; i < cartList.Count(); i++)
                {
                    var orderDetailNew = new OrderDetail();
                    orderDetailNew.OrderNo = orderNew.OrderNo;
                    orderDetailNew.ItemNo = cartList[i].ItemNo;
                    orderDetailNew.Quantity = cartList[i].Quantity;
                    orderDetailNew.DeliveryDate = cartList[i].DeliveryDate;
                    //orderDetailNew.CustomerId = ctmId;
                    orderDetailNew.Status = "未発送";
                    orderDetailNew.Product = cartList[i].Product;
                    orderDetailNew.Customer = cartList[i].Customer;
                    orderDetailNew.Order = orderNew;
                    db.OrderDetails.Add(orderDetailNew);
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
                }*/

                //return View(orderDetailResultList);
                return View(x);
            }
        }


        /*
        // GET: Order
        Database1Entities1 db = new Database1Entities1();
        public ActionResult OrderResult(int CustomerId)//ユーザIDの取得
        {
           　//注文番号とユーザIDのペアで追加
            var u = new Orderss
            {
                CustomerId = CustomerId
            };
            var v = db.Ordersses.Add(u);
            db.SaveChanges();

            //直前の注文番号とカートの中身をセットで追加していく
            /*var  = db.CartDetails.; 
            foreach(var y in )
            OrderDetail d = new OrderDetail
             {
                
             }
            db.OrderDetails.Add(d);

            ViewBag.a = db.OrderDetails.Where(x => x.CustomerId == CustomerId && x.OrderNo == 1);

            //注文明細と商品を結合、現在のログインユーザIDと直前の注文番号で検索
            var list = (from x in db.OrderDetails
                        join y in db.Products
                        on x.ItemNo equals y.ItemNo
                        select new
                        {     
                            OrderNo = x.OrderNo,
                            ItemName = y.ItemName,
                            UnitPrice = y.UnitPrice,
                            Quantity = x.Quantity,
                            CustomerId = x.CustomerId
                        })
                        .Where(x => x.CustomerId == CustomerId && x.OrderNo == 1);

            //オーダー内の小計を計算
            decimal p = 0;
            foreach (var z in list)
            {
                p = p + z.UnitPrice * z.Quantity;
            }
            ViewBag.P = String.Format("{0:#,0}", p);

            //消費税を計算
            double p2 = Decimal.ToDouble(p);
            double t = 0;
            t = p2 * 0.1;
            ViewBag.T = String.Format("{0:#,0}", t);

            //オーダー内の合計金額を計算
            double s = 0;
            s = p2 + t;
            ViewBag.S = String.Format("{0:#,0}", s);
            return View();   */
        //}
    }
}