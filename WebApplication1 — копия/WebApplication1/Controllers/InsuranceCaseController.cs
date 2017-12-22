using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;
using WebApplication1.Models.Agency;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class InsuranceCaseController : Controller
    {
        private ContractDAO contractDAO = new ContractDAO();
        private InsurantDAO insurantDAO = new InsurantDAO();
        private AgentDAO agentDAO = new AgentDAO();
        private TaxDAO taxDAO = new TaxDAO();
        private StatusDAO statusDAO = new StatusDAO();
        private CoefDAO coefDAO = new CoefDAO();
        private InsuranceCaseDAO caseDAO = new InsuranceCaseDAO();

        // GET: InsuranceCase
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult InsuranceCase()
        {
            return View(caseDAO.GetAllCase());
        }

        // GET: InsuranceCase/CreateCase
        [HttpGet]
        [Authorize(Roles = "Admin, Agent, Insurant")]
        public ActionResult CreateCase()
        {
            SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "title");
            ViewBag.Status = status;

            if (User.IsInRole("Agent") || User.IsInRole("Admin"))
            {
                SelectList insurants = new SelectList(insurantDAO.GetAllInsurant(), "id", "name");
                ViewBag.Insurant = insurants;
            }

            if (User.IsInRole("Insurant"))
            {
                string id = User.Identity.GetUserId();
                Insurant insurant = insurantDAO.GetAcc(id);
                InsuranceCase insurance = new InsuranceCase();
                insurance.insurant = insurant.id;
                return View("CreateCase", insurance);
            }

            return View("CreateCase");

        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent, Insurant")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCase(InsuranceCase insurance)
        {
            insurance.datestart = DateTime.Now;
            insurance.datestop = null;

            if (caseDAO.AddCase(insurance))
            {
                return View("Index");
            }
            else
            {
                return View("CreateCase");
            }
        }

        // GET: InsuranceCase/EditCase
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult EditCase(int id)
        {
            InsuranceCase insurance = caseDAO.GetCase(id);

            if (insurance != null)
            {
                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "title", id);
                ViewBag.Status = status;

                SelectList insurants = new SelectList(insurantDAO.GetAllInsurant(), "id", "name");
                ViewBag.Insurant = insurants;

                return View(insurance);
            }
            return RedirectToAction("InsuranceCase");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditCase(int id, InsuranceCase insurance)
        {
            if (id > 0 && insurance != null && ModelState.IsValid)
            {
                caseDAO.UpdateCase(insurance);
                return RedirectToAction("InsuranceCase");
            }
            else
            {
                return View("EditCase", caseDAO.GetCase(id));
            }
        }

        // GET: InsuranceCase/StatusEdit
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult StatusEdit(int id)
        {
            InsuranceCase insurance = caseDAO.GetCase(id);

            if (insurance != null)
            {
                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "title", id);
                ViewBag.Status = status;

                return View(insurance);
            }
            return RedirectToAction("InsuranceCase");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StatusEdit(int id, InsuranceCase insurance)
        {
            if (id > 0 && insurance != null && ModelState.IsValid)
            {
                caseDAO.UpdateCase(insurance);
                return RedirectToAction("InsuranceCase");
            }
            else
            {
                return View("StatusEdit", caseDAO.GetCase(id));
            }
        }
    }
}