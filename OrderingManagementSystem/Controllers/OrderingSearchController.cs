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
            return View();
        }

        public ActionResult Searchresult(OrderingSearchResultViewModel osrvm, int? CustomerId, int? OrderNo, DateTime? deliveryFrom, DateTime? deliveryTo, DateTime? orderFrom, DateTime? orderTo, int status)
        {
            /*if (ModelState.IsValid)
            {
                return View("Search", osrvm);
            }*/
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
                if (CustomerId != null && !ModelState.IsValid)
                {
                    ViewBag.customerId = CustomerId;
                    //入力されたCustomerIdに該当するOrderNoのカラム名付きリスト
                    var customerIdPreList = (from e in db.Orders
                                             where e.CustomerId.ToString().Contains(CustomerId.ToString())
                                             select new { e.OrderNo }).ToList();
                    //入力されたCustomerIdに該当するOrderNoの数値のみのリスト（この時点では空）
                    List<int> colist = new List<int>();
                    for (int i = 0; i < customerIdPreList.Count(); i++)
                    {
                        colist.Add(customerIdPreList[i].OrderNo);//リストにOrderNoの数値を追加。
                        customerIdList = (from e in db.OrderDetails
                                          where e.OrderNo == colist[i]
                                          select new { e.DetailNo }).ToList();
                    }
                }
                else
                {
                    return View("Search", osrvm);
                }

                var orderNoList = (from e in db.OrderDetails
                                   select new { e.DetailNo }).ToList();
                if (OrderNo != null && !ModelState.IsValid)
                {
                    ViewBag.orderNo = OrderNo;
                    orderNoList = (from e in db.OrderDetails
                                   where e.OrderNo.ToString().Contains(OrderNo.ToString())
                                   select new { e.DetailNo }).ToList();
                }
                else {
                    return View("Search", osrvm);
                }

                var deliveryDateList = (from e in db.OrderDetails
                                        select new { e.DetailNo }).ToList();
                if (deliveryFrom != null && deliveryTo != null && ModelState.IsValid)
                {
                    ViewBag.deliveryPeriod = deliveryFrom + "～" + deliveryTo;
                    deliveryDateList = (from e in db.OrderDetails
                                        where deliveryFrom <= e.DeliveryDate
                                        & e.DeliveryDate <= deliveryTo
                                        select new { e.DetailNo }).ToList();
                }
                else
                {
                    return View("Search", osrvm);
                }

                var orderDateList = (from e in db.OrderDetails
                                     select new { e.DetailNo }).ToList();
                if (orderFrom != null && orderTo != null && ModelState.IsValid)
                {
                    ViewBag.orderPeriod = orderFrom + "～" + orderTo;
                    var orderDatePreList = (from e in db.Orders
                                            where orderFrom <= e.OrderDate
                                            & e.OrderDate <= orderTo
                                            select new { e.OrderNo }).ToList();
                    //入力されたorderFrom・OrderToに該当するOrderNoの数値のみのリスト（この時点では空）
                    List<int> oolist = new List<int>();
                    for (int i = 0; i < orderDatePreList.Count(); i++)
                    {
                        oolist.Add(orderDatePreList[i].OrderNo);//リストにOrderNoの数値を追加。
                        orderDateList = (from e in db.OrderDetails
                                         where e.OrderNo == oolist[i]
                                         select new { e.DetailNo }).ToList();
                    }

                }
                else
                {
                    return View("Search", osrvm);
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
                                for (int l = 0; l < deliveryDateList.Count(); l++)
                                {
                                    for (int m = 0; m < orderDateList.Count(); m++)
                                    {
                                        if (statusList[i].DetailNo == customerIdList[j].DetailNo
                                        && statusList[i].DetailNo == orderNoList[k].DetailNo
                                        && statusList[i].DetailNo == deliveryDateList[l].DetailNo
                                        && statusList[i].DetailNo == orderDateList[m].DetailNo)
                                        {
                                            //各検索項目に該当するDetailNoのリストの中から、全てのリストに含まれるDetailNoを抽出ししてresultListに追加する。
                                            resultList.Add(statusList[i].DetailNo);
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
