using OrderingManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderingManagementSystem.DAL
{
    //public class Initializer : DropCreateDatabaseIfModelChanges<ModelContext>
    //public class Initializer : DropCreateDatabaseAlways<ModelContext>
    public class Initializer : CreateDatabaseIfNotExists<ModelContext>
    {
        protected override void Seed(ModelContext model)
        {
            var employees = new List<Employee>
            {
                new Employee { EmpNo = 1001, EmpName = "田中　一郎", Password = "1111" },
                new Employee { EmpNo = 1002, EmpName = "山田　花子", Password = "2222" },
                new Employee { EmpNo = 1003, EmpName = "佐藤　三郎", Password = "3333" },
            };
            employees.ForEach(emp => model.Employees.Add(emp));
            model.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1, CompanyName = "株式会社フルネス", Address = "東京都新宿区",
                    Telno = "03-1111-1111", CustomerName = "竹野口　豊", CustomerKana = "たけのくち　ゆたか",
                    Dept = "第一事業部", Email = "takenokuchi.yutaka@fullness.co.jp", Password = "111aaa"
                },
                new Customer
                {
                    CustomerId = 2, CompanyName = "有限会社10＠2", Address = "兵庫県神戸市",
                    Telno = "03-2222-2222", CustomerName = "織田　裕三", CustomerKana = "おりた　ゆうぞう",
                    Dept = "マーケティング本部", Email = "orita.yuzou@gmail.com", Password = "222bbb"
                },
                new Customer
                {
                    CustomerId = 3, CompanyName = "株式会社神田葬儀社", Address = "東京都千代田区",
                    Telno = "03-3333-3333", CustomerName = "常盤　貴代", CustomerKana = "ときわ　たかよ",
                    Dept = "営業部", Email = "tokiwa.takayo@ksg.co.jp", Password = "333ccc"
                }
            };
            customers.ForEach(ctm => model.Customers.Add(ctm));
            model.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    ItemNo = 1, ItemName = "夜は短し歩けよ乙女", Author = "森見　登美彦", Publisher = "角川文庫",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1kbSC6SiguremiDFHTm1zZ8Rs7o_ij70r", Overview = null,
                    UnitPrice = 616, Category = "SF・ホラー・ファンタジー", Size = "15x11x2cm",
                    Type = "文庫", Stock = 15, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 2, ItemName = "君の膵臓をたべたい", Author = "住野　よる", Publisher = "双葉社",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1kbSC6SiguremiDFHTm1zZ8Rs7o_ij70r", Overview = null,
                    UnitPrice = 734, Category = "日本文学", Size = "15x11x2cm",
                    Type = "文庫", Stock = 20, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 3, ItemName = "三体", Author = "劉　慈欣", Publisher = "早川書房",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1kbSC6SiguremiDFHTm1zZ8Rs7o_ij70r", Overview = null,
                    UnitPrice = 2090, Category = "SF・ホラー・ファンタジー", Size = "14x3x20cm",
                    Type = "ハードカバー", Stock = 10, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 4, ItemName = "バーティミアス1 サマルカンドの秘宝 上", Author = "ジョナサン ストラウド", Publisher = "静山社",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1kbSC6SiguremiDFHTm1zZ8Rs7o_ij70r", Overview = null,
                    UnitPrice = 836, Category = "こどものSF・ファンタジー", Size = "18x11x1cm",
                    Type = "新書", Stock = 0, ReceiptDate = DateTime.Parse("2020/07/01")
                },
                new Product
                {
                    ItemNo = 5, ItemName = "斜め屋敷の犯罪", Author = "島田　荘司", Publisher = "講談社",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1kbSC6SiguremiDFHTm1zZ8Rs7o_ij70r", Overview = null,
                    UnitPrice = 880, Category = "ミステリー・サスペンス・ハードボイルド", Size = "15x11x2cm",
                    Type = "文庫", Stock = 20, ReceiptDate = null
                }
            };
            products.ForEach(prod => model.Products.Add(prod));
            model.SaveChanges();

            var cartDetails = new List<CartDetail>
            {
                new CartDetail
                {
                    CartNo = 1, CustomerId = 1, ItemNo = 2, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/07/05")
                },
                new CartDetail
                {
                    CartNo = 2, CustomerId = 3, ItemNo = 3, Quantity = 2,
                    DeliveryDate = DateTime.Parse("2020/07/07")
                },
                new CartDetail
                {
                    CartNo = 3, CustomerId = 2, ItemNo = 5, Quantity = 3,
                    DeliveryDate = DateTime.Parse("2020/08/01")
                },
                new CartDetail
                {
                    CartNo = 4, CustomerId = 1, ItemNo = 1, Quantity = 5,
                    DeliveryDate = DateTime.Parse("2020/07/10")
                }
            };
            cartDetails.ForEach(cd => model.CartDetails.Add(cd));
            model.SaveChanges();

            var orders = new List<Order>
            {
                new Order { OrderNo = 1, CustomerId = 1, OrderDate = DateTime.Parse("2020/05/01") },
                new Order { OrderNo = 2, CustomerId = 1, OrderDate = DateTime.Parse("2020/06/06") },
                //new Order { OrderNo = 3, CustomerId = 2, OrderDate = DateTime.Parse("2020/06/15") },
            };
            orders.ForEach(od => model.Orders.Add(od));
            model.SaveChanges();

            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    DetailNo = 1, OrderNo = 1, MeisaiNo = 1, /*CustomerId = 1,*/ ItemNo = 2, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/05/05"), Status = 2
                },
                new OrderDetail
                {
                    DetailNo = 2, OrderNo = 1, MeisaiNo = 2, /*CustomerId = 1,*/ ItemNo = 5, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/05/05"), Status = 2
                },
                new OrderDetail
                {
                    DetailNo = 3, OrderNo = 2, MeisaiNo = 1, /*CustomerId = 1,*/ ItemNo = 1, Quantity = 5,
                    DeliveryDate = DateTime.Parse("2020/06/14"), Status = 1
                }
            };
            orderDetails.ForEach(odd => model.OrderDetails.Add(odd));
            model.SaveChanges();




            //using (var md = new ModelContext())
            //{
            //    md.Database.CreateIfNotExists();
            //}
        }

    }
}