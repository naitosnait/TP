using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;
using WebApplication1.Models.Agency;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class InsurantController : Controller
    {
        private InsurantDAO insurantDAO = new InsurantDAO();
        private TypeDAO typeDAO = new TypeDAO();

        // GET: Insurant
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult Insurant()
        {
            return View(insurantDAO.GetAllInsurant());
        }

        // GET: Insurant/Create
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult CreateInsurant()
        {
            SelectList type = new SelectList(typeDAO.GetAllType(), "id", "title");
            ViewBag.Type = type;
            return View("CreateInsurant");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateInsurant(Insurant insurant)
        {
            if (insurantDAO.AddInsurant(insurant) && ModelState.IsValid)
            { 
                return RedirectToAction("Insurant");
            }
            else
            {
                return View("CreateInsurant");
            }
        }

        // GET: Insurant/EditInsurant
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult EditInsurant(int id)
        {
            Insurant insurant = insurantDAO.GetInsurant(id);

            if (insurant != null)
            {
                SelectList type = new SelectList(typeDAO.GetAllType(), "id", "title");
                ViewBag.Type = type;

                return View(insurant);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditInsurant(int id, Insurant insurant)
        {
            if (id > 0 && insurant != null && ModelState.IsValid)
            {
                insurantDAO.UpdateInsurant(insurant);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditInsurant", insurantDAO.GetInsurant(id));
            }
        }
    }
}
