using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        TaxDAO taxDAO = new TaxDAO();
        ContractDAO contractDAO = new ContractDAO();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Tax
        public ActionResult Tax()
        {
            return View(taxDAO.GetAllTax());
        }

        // GET: Home/Contract
        public ActionResult Contract()
        {
            return View(contractDAO.GetAllContract());
        }

        //
        // GET: /Home/Create
        public ActionResult ContractCreate()
        {
            return View();
        }
        //
        // POST: /Home/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContractCreate([Bind(Exclude = "id")] Contract contract)
        {
            try
            {
                if (contractDAO.Add(contract))
                    return RedirectToAction("Contract");
                else
                    return View("ContractCreate");
            }
            catch
            {
                return View("ContractCreate");
            }
        }

    }
}