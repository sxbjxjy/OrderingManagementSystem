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
                new Employee { EmpNo = 1004, EmpName = "小西　優子", Password = "4444" },
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

                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1auBOSGdPlXxzUKL4XrCR89tsQ1TqeQlZ",
                    UnitPrice = 616, Category = "SF・ホラー・ファンタジー", Size = "15x11x2cm",


                    Overview = "「黒髪の乙女」にひそかに想いを寄せる「先輩」は、京都のいたるところで彼女の姿を追い求めた。二人を待ち受ける珍事件の数々、そして運命の大転回とは? 山本周五郎賞受賞、本屋大賞2位の傑作、待望の文庫化!",

                    Type = "文庫", Stock = 15, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 2, ItemName = "君の膵臓をたべたい", Author = "住野　よる", Publisher = "双葉社",

                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1S-vOoldWxJxZBEuYsxfVrcy7KuMoxlBZ", Overview = null,
                    UnitPrice = 734, Category = "日本文学", Size = "15x11x2cm",

                    Type = "文庫", Stock = 20, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 3, ItemName = "三体", Author = "劉　慈欣", Publisher = "早川書房",

                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1FBdd5tKT4AGUDk5Qydt-PZ2KBl2oXqmX", Overview = null,
                    UnitPrice = 2090, Category = "SF・ホラー・ファンタジー", Size = "14x3x20cm",


                    Type = "ハードカバー", Stock = 10, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 4, ItemName = "バーティミアス1 サマルカンドの秘宝 上", Author = "ジョナサン ストラウド", Publisher = "静山社",

                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1ss4421fcFV80XEQolFTdT_mngAAqPE9M", Overview = null,
                    UnitPrice = 836, Category = "こどものSF・ファンタジー", Size = "18x11x1cm",

                    Type = "新書", Stock = 0, ReceiptDate = DateTime.Parse("2020/07/01")
                },
                new Product
                {
                    ItemNo = 5, ItemName = "斜め屋敷の犯罪", Author = "島田　荘司", Publisher = "講談社",

                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1QhGlNrMSS44JNdT7tN_sEZk35v4bA6r2", Overview = null,
                    UnitPrice = 880, Category = "ミステリー・サスペンス・ハードボイルド", Size = "15x11x2cm",


                    Type = "文庫", Stock = 20, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 6, ItemName = "柚莉愛とかくれんぼ", Author = "真下　みこと", Publisher = "講談社",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=18XDCyaimLj_8EdSXTVymH8kWkc1fKQG4", Overview = null,
                    UnitPrice = 1430, Category = "ミステリー・サスペンス・ハードボイルド", Size = "18.8 x 13 x 2.4 cm",
                    Type = "単行本", Stock = 4, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 7, ItemName = "リアルフェイス", Author = "知念　実希人", Publisher = "実業之日本社",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1KKtNxWwEQ-6mqzXG180HecqYChEZJPak",
                    Overview = null,
                    UnitPrice = 734, Category = "ミステリー・サスペンス・ハードボイルド", Size = "15 x 12.2 x 1.8 cm",
                    Type = "文庫", Stock = 0, ReceiptDate = DateTime.Parse("2020/07/05")
                },
                new Product
                {
                    ItemNo = 8, ItemName = "黒い家", Author = "貴志　祐介", Publisher = "角川書店",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1pEQ-fQSEf67SFFTEnFIuInmsidxO0IJk",
                    Overview = "顧客の家に呼ばれ、子供の首吊り死体の発見者になってしまった保険会社社員・若槻は、顧客の不審な態度から独自の調査を始める。それが悪夢の始まりだった。第4回日本ホラー小説大賞受賞。",
                    UnitPrice = 748, Category = "SF・ホラー・ファンタジー", Size = "14.8 x 10.8 x 2 cm",
                    Type = "文庫", Stock = 30, ReceiptDate = null
                },
                new Product
                {
                    ItemNo = 9, ItemName = "てふてふ荘へようこそ", Author = "乾　ルカ", Publisher = "角川書店",
                    PhotoUrl = "https://drive.google.com/uc?export=view&id=1Fg7Gb92gA0eX2H_Mwi0Bw5gZGasfPonl",
                    Overview = "家賃13,000円、間取りは2K、敷金・礼金なし、管理費なし、身元保証人不要、最初の1か月は家賃不要、トイレ・風呂・玄関は共同、そんな破格の条件が揃った古びたアパート「てふてふ荘」にあるもう一つの条件は、どの部屋にも幽霊がおり、同居しなければならないことだった。切なくもほっこり心が温かくなる、おんぼろアパート物語。",
                    UnitPrice = 649, Category = "SF・ホラー・ファンタジー", Size = "14.8 x 10.8 x 2 cm",
                    Type = "文庫", Stock = 5, ReceiptDate = null
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
                new Order { OrderNo = 3, CustomerId = 2, OrderDate = DateTime.Parse("2020/06/07") },
                new Order { OrderNo = 4, CustomerId = 1, OrderDate = DateTime.Parse("2020/06/08") },
                new Order { OrderNo = 5, CustomerId = 3, OrderDate = DateTime.Parse("2020/06/08") }
            };
            orders.ForEach(od => model.Orders.Add(od));
            model.SaveChanges();

            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    DetailNo = 1, OrderNo = 1, MeisaiNo = 1, ItemNo = 2, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/05/05"), Status = 2
                },
                new OrderDetail
                {
                    DetailNo = 2, OrderNo = 1, MeisaiNo = 2, ItemNo = 5, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/05/05"), Status = 2
                },
                new OrderDetail
                {
                    DetailNo = 3, OrderNo = 2, MeisaiNo = 1, ItemNo = 1, Quantity = 5,
                    DeliveryDate = DateTime.Parse("2020/06/14"), Status = 1
                },
                new OrderDetail
                {
                    DetailNo = 4, OrderNo = 2, MeisaiNo = 2, ItemNo = 3, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/06/14"), Status = 1
                },
                new OrderDetail
                {
                    DetailNo = 5, OrderNo = 2, MeisaiNo = 3, ItemNo = 5, Quantity = 2,
                    DeliveryDate = DateTime.Parse("2020/06/14"), Status = 1
                },
                new OrderDetail
                {
                    DetailNo = 6, OrderNo = 3, MeisaiNo = 1, ItemNo = 4, Quantity = 2,
                    DeliveryDate = DateTime.Parse("2020/07/01"), Status = 4
                },
                new OrderDetail
                {
                    DetailNo = 7, OrderNo = 4, MeisaiNo = 1, ItemNo = 1, Quantity = 2,
                    DeliveryDate = DateTime.Parse("2020/07/02"), Status = 2
                },
                new OrderDetail
                {
                    DetailNo = 8, OrderNo = 4, MeisaiNo = 2, ItemNo = 7, Quantity = 1,
                    DeliveryDate = DateTime.Parse("2020/07/10"), Status = 4
                },
                new OrderDetail
                {
                    DetailNo = 9, OrderNo = 5, MeisaiNo = 1, ItemNo = 9, Quantity = 20,
                    DeliveryDate = DateTime.Parse("2020/07/10"), Status = 1
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