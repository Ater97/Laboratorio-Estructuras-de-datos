using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratory_2.Models;
using Laboratory_2.UtilitiesClass;

namespace Laboratory_2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(file.ContentLength);

                string Data = System.Text.Encoding.UTF8.GetString(binData);
                Data = Data.Replace('"', ' ');
                Data = Data.Replace("\r\n", ",");
                string[] Result = Data.Split(',');
                for (int i = 0; i < VerverifyLenght(Result.Length); i = i + 4)
                {
                    ProductModel newProduct = (new ProductModel
                    {
                        ProductID = int.Parse(Result[i].Trim()),
                        ProductDescription = Result[i + 1].Trim(),
                        ProductPrize = double.Parse(Result[i + 2].Trim()),
                        ProductCount = int.Parse(Result[i + 3].Trim())

                    });
                    Singleton.Instance.ProductsBinaryTree.Add(newProduct);
                }

                return View("Index");
            }
            catch
            {
                return View();
            }

        }
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private double VerverifyLenght(double number)
        {
            if (number % 4 == 0)
            {
                return number;
            }
            else
            {
                return (VerverifyLenght(number - 1));
            }
        }
    }
}
