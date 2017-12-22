using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;
using WebApplication1.Models.Agency;

namespace WebApplication1.Controllers.DAOControllers
{
    public class TaxController : Controller
    {
        private TaxDAO taxDAO = new TaxDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();

        // GET: Tax
        [HttpGet]
        public ActionResult Tax()
        {
            return View(taxDAO.GetAllTax());
        }

        // GET: Tax/CreateTax
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateTax()
        {
            return View("CreateTax");
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateTax(Tax tax)
        {
            if (taxDAO.AddTax(tax))
            {
                return RedirectToAction("Tax");
            }
            else
            {
                return View("CreateTax");
            }
        }

        protected bool ViewDataSelectList(int id)
        {
            var category = categoryDAO.GetAllCategory();
            ViewData["category"] = new SelectList(category, "id", "title", id);
            return category.Count() > 0;
        }

        // GET: Tax/EditTax
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditTax(int id)
        {
            Tax tax = taxDAO.GetTax(id);
            if (!ViewDataSelectList(tax.Category1.id))
                return RedirectToAction("Tax");
            return View(tax);

        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Tax tax)
        {
            if (id > 0 && tax != null && ModelState.IsValid)
            {
                taxDAO.UpdateTax(tax);
                return RedirectToAction("Tax");
            }
            else
            {
                ViewDataSelectList(-1);
                return View("EditTax", taxDAO.GetTax(id));
            }
        }
    }
}