using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratory_2.UtilitiesClass;
using Laboratory_2.Models;

namespace Laboratory_2.Controllers
{
    public class BillsController : Controller
    {
        // GET: Bills
        public ActionResult Index()
        {
            return View(Singleton.Instance.BillsBinaryTree);
        }

        // GET: Bills/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                BillsModel newBillModel = (new BillsModel
                {
                    Serie = char.Parse(collection["Serie"]),
                    Correlative = int.Parse(collection["Correlative"]),
                    Name = collection["Name"],
                    NIT = collection["NIT"],
                    SaleDate = collection["SaleDate"],
                    BillDescription =collection["BillDescription"],
                    Total = double.Parse(collection["Total"]),
                });
                Singleton.Instance.BillsBinaryTree.Add(newBillModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bills/Edit/5
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

        // GET: Bills/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bills/Delete/5
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
    }
}
