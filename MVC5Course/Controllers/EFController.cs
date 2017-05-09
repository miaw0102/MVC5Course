using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();//查看 FabricsEntities


        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();//把AsQueryable轉成AsQueryable()只是個查詢物件，不會真的把物件取回來

            var data = all
                //.Where(p => p.Active == true && p.ProductName.Contains("Black"))
                .Where(p => p.IsDeleted== false && p.Active == true && p.ProductName.Contains("Black"))
                .OrderByDescending(p => p.ProductId);

            //var data1 = all.Where(p => p.ProductId == 1);
            //var data2 = all.FirstOrDefault(p => p.ProductId == 1);
            //var data3 = db.Product.Find(1);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);//注意product的型別
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id,Product product)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock = product.Stock;
                item.Active = product.Active;
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(product);
        }

        public ActionResult Details(int id)
        {
            

            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0", id).FirstOrDefault();


            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            //方法一
            //foreach (var item in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //    //db.SaveChanges();不要再這邊寫，不然會一直有髒資料
            //}

            //方法二等於方法一的表示用一行表示
            //db.OrderLine.RemoveRange(product.OrderLine);

            product.IsDeleted= true;
            //db.Product.Remove(product);

          
            //db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                throw ex;
            }

            return RedirectToAction("Index");

            

        }

    }


}