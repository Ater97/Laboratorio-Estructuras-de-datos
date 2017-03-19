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
            ViewBag.Message = "Cantidad de elementos: " + Singleton.Instance.ProductsBinaryTree.GetCount();
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
               if (file == null) { ViewBag.Message = "Ocurrió un error. Por favor seleccione un archivo"; return View("UploadProduct"); }

                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(file.ContentLength);

                string Data = System.Text.Encoding.UTF8.GetString(binData);
                Data = Data.Replace("\",\"","*");
                Data = Data.Replace("\r\n", "*");
                Data = Data.Replace('"', ' ');
                string[] Result = Data.Split('*');
                for (int i = 0; i < VerverifyLenght(Result.Length); i = i + 4)
                {
                    ProductModel newProduct = (new ProductModel
                    {
                        ProductID = Result[i].Trim(),
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
                ViewBag.Message = "Ocurrió un error al subir el archivo, por favor revise el formato.";
                return View();
            }

        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            return View(SearchElement(id));
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
                    ProductID = collection["ProductID"],
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
        public ActionResult Edit(String id)
        {
            return View(SearchElement(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                ProductModel item = new ProductModel();
                item.ProductID = id;
                item.ProductDescription = collection["ProductDescription"];
                item.ProductPrize = double.Parse(collection["ProductPrize"]);
                item.ProductCount = long.Parse(collection["ProductCount"]);

                if(Singleton.Instance.ProductsBinaryTree.Edit<string>(Comparar, id, item))
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

        public ProductModel SearchElement(string id)
        {
            TreeNode<ProductModel> temp = Singleton.Instance.ProductsBinaryTree.Search<string>(Comparar, id);
            return temp.Value;
        }

        public static int Comparar<E>(ProductModel product, E elementoBuscar)
        {
            return product.ProductID.CompareTo(elementoBuscar);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            return View(SearchElement(id));
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private bool DeleteProduct(string id)
        {
            TreeNode<ProductModel> Product = Singleton.Instance.ProductsBinaryTree.Search<string>(Comparar, id);
            Singleton.Instance.ProductsBinaryTree.Eliminate(Product);
            return false;
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
