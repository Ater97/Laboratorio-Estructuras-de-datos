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
            return View(Singleton.Instance.ProductsBinaryTree);
        }

        public ActionResult UploadProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProduct(HttpPostedFileBase file)
        {
            try
            {
               if (file == null) return View("UploadProduct");

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
                        ProductCount = long.Parse(Result[i + 3].Trim())

                    });
                    Singleton.Instance.ProductsBinaryTree.Add(newProduct);
                }

                return RedirectToAction("Index");
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
                ProductModel newProduct = (new ProductModel
                {
                    ProductID = int.Parse(collection["ProductID"]),
                    ProductDescription = collection["ProductDescription"],
                    ProductPrize = double.Parse(collection["ProductPrize"]),
                    ProductCount = long.Parse(collection["ProductCount"])

                });
                Singleton.Instance.ProductsBinaryTree.Add(newProduct);
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
            return View(SearchElement(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ProductModel item = new ProductModel();
                item.ProductID = id;
                item.ProductDescription = collection["ProductDescription"];
                item.ProductPrize = double.Parse(collection["ProductPrize"]);
                item.ProductCount = long.Parse(collection["ProductCount"]);

                if(Singleton.Instance.ProductsBinaryTree.Edit<int>(Comparar, id, item))
                {
                    ViewBag.Message = "Se ha editado el elemento correctamente.";
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error, no se han realizado los cambios en el elemento deseado.";
                }
                 
                return RedirectToAction("Index");

            }
            catch(Exception e)
            {
                ViewBag.Message = "ERROR: " + e.Message;
                return RedirectToAction("Index");
            }
        }


        public ProductModel SearchElement(int id)
        {
            TreeNode<ProductModel> temp = Singleton.Instance.ProductsBinaryTree.Search<int>(Comparar, id);
            return temp.Value;
        }

        public static int Comparar<E>(ProductModel product, E elementoBuscar)
        {
            return product.ProductID.CompareTo(elementoBuscar);
        }

        //public void Edit(int id, ProductModel newModel)
        //{
        //    for (int i = 0; i < Singleton.Instance.ProductsBinaryTree.GetCount(); i++)
        //    {
        //        //Buscar ID, igualar nodo con newModel
        //    }
        //}
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
