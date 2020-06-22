using OrderingManagementSystem.DAL;
using OrderingManagementSystem.Models;
using OrderingManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingManagementSystem.Controllers
{
    public class OrderStatusController : Controller
    {
        // GET: OrderStatus
        public ActionResult OrderList(int ctmId = 1)
        {
            using (var db = new ModelContext())
            {
                var osList = new List<List<OrderStatusViewModel>>();

                var orderNoList = (from odn in db.Orders
                                   where odn.CustomerId == ctmId
                                   select new { odn.OrderNo }).ToList();

                for (int i = 0; i < orderNoList.Count(); i++)
                {
                    int odn = orderNoList[i].OrderNo;
                    var orderDetailList = (from e in db.OrderDetails
                                           where e.OrderNo == odn
                                           select e).ToList();

                    var osvmList = new List<OrderStatusViewModel>();
                    for (int j = 0; j < orderDetailList.Count(); j++)
                    {
                        var osvm = new OrderStatusViewModel(orderDetailList[j]);
                        osvmList.Add(osvm);
                    }
                    osList.Add(osvmList);
                }

                return View(osList);
            }
        }

        public ActionResult OrderCancelConfirm(int odNo)
        {
            using (var db = new ModelContext())
            {
                var osvmList = new List<OrderStatusViewModel>();

                var orderDetailList = (from e in db.OrderDetails
                                       where e.OrderNo == odNo
                                       select e).ToList();
                for (int i = 0; i < orderDetailList.Count(); i++)
                {
                    var osvm = new OrderStatusViewModel(orderDetailList[i]);
                    osvmList.Add(osvm);
                }
                return View(osvmList);
            }
        }

        public ActionResult OrderCancel(int odNo)
        {
            using (var db = new ModelContext())
            {
                var orderDetailList = (from e in db.OrderDetails
                                       where e.OrderNo == odNo
                                       select e).ToList();
                for (int i = 0; i < orderDetailList.Count(); i++)
                {
                    int odi = orderDetailList[i].DetailNo;
                    OrderDetail od = db.OrderDetails.Find(odi);
                    od.Status = 3;
                }
                db.SaveChanges();
                return Redirect("OrderList");
            }
        }
    }
}