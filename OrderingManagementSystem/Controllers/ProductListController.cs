using OrderingManagementSystem.Models;
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

        public ActionResult ProductCatalog()
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