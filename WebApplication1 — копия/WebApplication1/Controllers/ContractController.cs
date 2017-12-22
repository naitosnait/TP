using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Agency;
using Microsoft.AspNet.Identity;
using WebApplication1.DAO;

namespace WebApplication1.Controllers
{
    public class ContractController : Controller
    {
        private ContractDAO contractDAO = new ContractDAO();
        private InsurantDAO insurantDAO = new InsurantDAO();
        private AgentDAO agentDAO = new AgentDAO();
        private TaxDAO taxDAO = new TaxDAO();
        private StatusDAO statusDAO = new StatusDAO();
        private CoefDAO coefDAO = new CoefDAO();
        private AgencyEntities dbentities = new AgencyEntities();

        // GET: Contract
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult Contract(string searchString)
        {
            var contract = contractDAO.GetAllContract();
            if (!String.IsNullOrEmpty(searchString))
            {
                contract = contract.Where(s => s.series.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(contract);
        }

        // GET: Contract/CreateContract
        [HttpGet]
        [Authorize(Roles = "Admin, Agent, Insurant")]
        public ActionResult CreateContract()
        {
            SelectList agent = new SelectList(agentDAO.GetAllAgent(), "id", "fio");
            ViewBag.Agent = agent;

            SelectList tax = new SelectList(taxDAO.GetAllTax(), "id", "name");
            ViewBag.Tax = tax;

            SelectList coef = new SelectList(coefDAO.GetAllCoef(), "id", "value");
            ViewBag.Coef = coef;

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
                Contract contract = new Contract();
                contract.insurant = insurant.id;
                return View("CreateContract", contract);
            }

            return View("CreateContract");

        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent, Insurant")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateContract(Contract contract)
        {
            contract.date = DateTime.Now;

            Tax tax = taxDAO.GetTax(contract.tax);
            Coef coef = coefDAO.GetCoef(contract.coef);

            var a = (float)tax.price * coef.value;
            contract.cost = (decimal)a;

            if (contractDAO.AddContract(contract))
            {
                return View("Index");
            }
            else
            {
                return View("CreateContract");
            }
        }

        // GET: Contract/EditContract
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult EditContract(int id)
        {
            Contract contract = contractDAO.GetContract(id);

            if (contract != null)
            {
                SelectList insurants = new SelectList(insurantDAO.GetAllInsurant(), "id", "name", id);
                ViewBag.Insurant = insurants;

                SelectList agent = new SelectList(agentDAO.GetAllAgent(), "id", "fio", id);
                ViewBag.Agent = agent;

                SelectList tax = new SelectList(taxDAO.GetAllTax(), "id", "name", id);
                ViewBag.Tax = tax;

                SelectList coef = new SelectList(coefDAO.GetAllCoef(), "id", "value", id);
                ViewBag.Coef = coef;

                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "title", id);
                ViewBag.Status = status;

                return View(contract);
            }
            return RedirectToAction("Contract");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContract(int id, Contract contract)
        {
            if (id > 0 && contract != null && ModelState.IsValid)
            {
                contractDAO.UpdateContract(contract);
                return RedirectToAction("Contract");
            }
            else
            {
                return View("EditContract", contractDAO.GetContract(id));
            }
        }

        // GET: Contract/EditStatus
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult EditStatus(int id)
        {
            Contract contract = contractDAO.GetContract(id);

            if (contract != null)
            {
                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "title", id);
                ViewBag.Status = status;
                return View(contract);
            }
            return RedirectToAction("Contract");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditStatus(int id, Contract contract)
        {
            if (id > 0 && contract != null && ModelState.IsValid)
            {
                contractDAO.UpdateContractStatus(contract);
                return RedirectToAction("Contract");
            }
            else
            {
                return View("EditStatus", contractDAO.GetContract(id));
            }
        }
    }
}