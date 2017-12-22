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
    public class PersAreaController : Controller
    {
        private TypeDAO typeDAO = new TypeDAO();
        private InsurantDAO insurantDAO = new InsurantDAO();
        private AgentDAO agentDAO = new AgentDAO();
        private ContractDAO contractDAO = new ContractDAO();
        private InsuranceCaseDAO caseDAO = new InsuranceCaseDAO();

        // GET: PersArea
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        // GET: PersArea
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult Agent()
        {
            return View();
        }

        // GET: PersArea
        [Authorize(Roles = "Admin, Insurant")]
        public ActionResult Insurant()
        {
            return View();
        }

        // GET: PersArea/ViewInsurant
        [Authorize(Roles = "Admin, Insurant")]
        public ActionResult ViewInsurant()
        {
            string id = User.Identity.GetUserId();
            Insurant insurant = insurantDAO.GetAcc(id);
            return View("ViewInsurant", insurant);
        }

        // GET: PersArea/ViewAgent
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult ViewAgent()
        {
            string id = User.Identity.GetUserId();
            Agent agent = agentDAO.GetAcc(id);
            return View("ViewAgent", agent);
        }

        // GET: PersArea/InsurantContract
        [Authorize(Roles = "Admin, Insurant")]
        public ActionResult InsurantContract()
        {
            string id = User.Identity.GetUserId();
            Insurant insurant = insurantDAO.GetAcc(id);
            return View("InsurantContract", contractDAO.GetInsurance(insurant.id));
        }

        // GET: PersArea/AgentContract
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult AgentContract()
        {
            string id = User.Identity.GetUserId();
            Agent agent = agentDAO.GetAcc(id);
            return View("AgentContract", contractDAO.GetAgent(agent.id));
        }

        // GET: PersArea/InsurantCase
        [Authorize(Roles = "Admin, Insurant")]
        public ActionResult InsurantCase()
        {
            string id = User.Identity.GetUserId();
            Insurant insurant = insurantDAO.GetAcc(id);
            return View("InsurantCase", caseDAO.GetInsurance(insurant.id));
        }
    }
}