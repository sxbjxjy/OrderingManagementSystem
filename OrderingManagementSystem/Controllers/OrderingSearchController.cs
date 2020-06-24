using OrderingManagementSystem.DAL;
using OrderingManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingManagementSystem.Controllers
{
    public class OrderingSearchController : Controller
    {
        public ActionResult Search()
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            return View();
        }

        public ActionResult Searchresult(int? customerId, int? orderNo, DateTime? deliveryFrom, DateTime? deliveryTo, DateTime? orderFrom, DateTime? orderTo, int status)
        {
            if (Session["Employee"] == null)
            {
                return Redirect("/EmployeeLogin/Login");
            }
            using (var db = new ModelContext())
            {
                if (status == 1)
                {
                    ViewBag.status = "未出荷";
                }
                else if (status == 2)
                {
                    ViewBag.status = "出荷済";
                }
                else if (status == 3)
                {
                    ViewBag.status = "キャンセル";
                }
                else
                {
                    ViewBag.status = "入荷待ち";
                }

                //モデルのインスタンスを生成。
                var OrderingSearchResultViewModelList = new List<OrderingSearchResultViewModel>();

                var customerIdList = (from e in db.OrderDetails
                                      select new { e.DetailNo }).ToList();
                if (customerId != null)
                {
                    ViewBag.customerId = customerId;
                    //入力されたCustomerIdに該当するOrderNoのカラム名付きリスト
                    var customerIdPreList = (from e in db.Orders
                                             where e.CustomerId.ToString().Contains(customerId.ToString())
                                             select new { e.OrderNo }).ToList();
                    //入力されたCustomerIdに該当するOrderNoの数値のみのリスト（この時点では空）
                    List<int> colist = new List<int>();
                    for (int i = 0; i < customerIdPreList.Count(); i++)
                    {
                        colist.Add(customerIdPreList[i].OrderNo);//リストにOrderNoの数値を追加。
                        int co = colist[i];
                        customerIdList = (from e in db.OrderDetails
                                          where e.OrderNo == co
                                          select new { e.DetailNo }).ToList();
                    }
                }

                var orderNoList = (from e in db.OrderDetails
                                   select new { e.DetailNo }).ToList();
                if (orderNo != null)
                {
                    ViewBag.orderNo = orderNo;
                    orderNoList = (from e in db.OrderDetails
                                   where e.OrderNo.ToString().Contains(orderNo.ToString())
                                   select new { e.DetailNo }).ToList();
                }

                var deliveryFromList = (from e in db.OrderDetails
                                        select new { e.DetailNo }).ToList();
                if (deliveryFrom != null)
                {
                    ViewBag.deliveryFrom = deliveryFrom + "～";
                    deliveryFromList = (from e in db.OrderDetails
                                        where deliveryFrom <= e.DeliveryDate
                                        select new { e.DetailNo }).ToList();
                }

                var deliveryToList = (from e in db.OrderDetails
                                      select new { e.DetailNo }).ToList();
                if (deliveryTo != null)
                {
                    ViewBag.deliveryTo = deliveryTo;
                    if (deliveryFrom == null)
                    {
                        ViewBag.deliveryTo = "～" + deliveryTo;
                    }
                    deliveryFromList = (from e in db.OrderDetails
                                        where e.DeliveryDate <= deliveryTo
                                        select new { e.DetailNo }).ToList();
                }

                var orderFromList = (from e in db.OrderDetails
                                     select new { e.DetailNo }).ToList();
                if (orderFrom != null)
                {
                    ViewBag.orderFrom = orderFrom + "～";
                    var orderFromPreList = (from e in db.Orders
                                            where orderFrom <= e.OrderDate
                                            select new { e.OrderNo }).ToList();
                    //入力されたorderFrom・OrderToに該当するOrderNoの数値のみのリスト（この時点では空）
                    List<int> oolist = new List<int>();
                    for (int i = 0; i < orderFromPreList.Count(); i++)
                    {
                        oolist.Add(orderFromPreList[i].OrderNo);//リストにOrderNoの数値を追加。
                        int oo = oolist[i];
                        orderFromList = (from e in db.OrderDetails
                                         where e.OrderNo == oo
                                         select new { e.DetailNo }).ToList();
                    }
                }

                var orderToList = (from e in db.OrderDetails
                                   select new { e.DetailNo }).ToList();
                if (orderTo != null)
                {
                    ViewBag.orderTo = orderTo;
                    if (orderFrom == null)
                    {
                        ViewBag.orderTo = "～" + orderTo;
                    }
                    var orderToPreList = (from e in db.Orders
                                          where e.OrderDate <= orderTo
                                          select new { e.OrderNo }).ToList();
                    //入力されたorderFrom・OrderToに該当するOrderNoの数値のみのリスト（この時点では空）
                    List<int> oolist = new List<int>();
                    for (int i = 0; i < orderToPreList.Count(); i++)
                    {
                        oolist.Add(orderToPreList[i].OrderNo);//リストにOrderNoの数値を追加。
                        int oo = oolist[i];
                        orderToList = (from e in db.OrderDetails
                                       where e.OrderNo == oo
                                       select new { e.DetailNo }).ToList();
                    }

                }

                var statusList = (from e in db.OrderDetails
                                  where e.Status == status
                                  select new { e.DetailNo }).ToList();
                if (statusList.Count == 0)//該当DetailNoゼロ
                {
                    ViewBag.element = 0;
                    return View("Search");
                }
                else
                {
                    List<int> resultList = new List<int>();
                    for (int i = 0; i < statusList.Count(); i++)
                    {
                        for (int j = 0; j < customerIdList.Count(); j++)
                        {
                            for (int k = 0; k < orderNoList.Count(); k++)
                            {
                                for (int l = 0; l < deliveryFromList.Count(); l++)
                                {
                                    for (int m = 0; m < deliveryToList.Count(); m++)
                                    {
                                        for (int n = 0; n < orderFromList.Count(); n++)
                                        {
                                            for (int o = 0; o < orderToList.Count(); o++)
                                            {
                                                if (statusList[i].DetailNo == customerIdList[j].DetailNo
                                                && statusList[i].DetailNo == orderNoList[k].DetailNo
                                                && statusList[i].DetailNo == deliveryFromList[l].DetailNo
                                                && statusList[i].DetailNo == deliveryToList[m].DetailNo
                                                && statusList[i].DetailNo == orderFromList[n].DetailNo
                                                && statusList[i].DetailNo == orderToList[o].DetailNo)
                                                {
                                                    //各検索項目に該当するDetailNoのリストの中から、全てのリストに含まれるDetailNoを抽出ししてresultListに追加する。
                                                    resultList.Add(statusList[i].DetailNo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (resultList.Count() == 0)//該当DetailNoゼロ
                    {
                        ViewBag.element = 0;
                        return View("Search");
                    }
                    else
                    {
                        for (int i = 0; i < resultList.Count(); i++)
                        {
                            //抽出したDetailNoに該当する列のデータをSearchResultListにリストとして格納。
                            int resultDetailNo = resultList[i];
                            var SearchResultList = (from e in db.OrderDetails
                                                    where e.DetailNo == resultDetailNo
                                                    select e).ToList();

                            for (int j = 0; j < SearchResultList.Count(); j++)
                            {
                                //SearchResultListの中身をモデルに格納。
                                var osrViewModel = new OrderingSearchResultViewModel(SearchResultList[j]);
                                OrderingSearchResultViewModelList.Add(osrViewModel);
                            }
                        }
                    }
                }
                return View(OrderingSearchResultViewModelList);
            }
        }
    }
}
