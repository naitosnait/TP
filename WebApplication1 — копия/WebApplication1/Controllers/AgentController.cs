using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;
using Microsoft.AspNet.Identity;
using WebApplication1.Models.Agency;

namespace WebApplication1.Controllers
{
    public class AgentController : Controller
    {
        private AgentDAO agentDAO = new AgentDAO();

        // GET: Agent
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult Agent()
        {
            return View(agentDAO.GetAllAgent());
        }

        // GET: Agent/CreateAgent
        [HttpGet]
        [Authorize(Roles = "Admin, Agent")]
        public ActionResult CreateAgent()
        {
            if (User.IsInRole("Agent"))
            {
                Agent agent = new Agent();
                string id = User.Identity.GetUserId();
                agent.account = id;
                return View("CreateAgent", agent);
            }
            return View("CreateAgent");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Agent")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateAgent(Agent agent)
        {
            if (agentDAO.AddAgent(agent))
            {
                return RedirectToAction("Agent");
            }
            else
            {
                return View("CreateAgent");
            }
        }

        // GET: Agent/EditAgent
        [HttpGet]
        public ActionResult EditAgent(int id)
        {
            Agent agent = agentDAO.GetAgent(id);
            return View(agent);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAgent(int id, Agent agent)
        {
            if (id > 0 && agent != null && ModelState.IsValid)
            {
                agentDAO.UpdateAgent(agent);
                return RedirectToAction("Agent");
            }
            else
            {
                return View("EditAgent", agentDAO.GetAgent(id));
            }
        }
    }
}