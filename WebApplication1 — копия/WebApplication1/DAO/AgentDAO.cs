using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;
using System.Web.Mvc;

namespace WebApplication1.DAO
{
    public class AgentDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Agent> GetAllAgent()
        {
                return dbentities.Agent.Select(n => n);
        }

        public Agent GetAgent(int id)
        {
            return dbentities.Agent.Where(n => n.id == id).First();
        }

        public Agent GetAgentFio(string fio)
        {
            return dbentities.Agent.Where(n => n.fio == fio).First();
        }

        public Agent GetAcc(string id)
        {
            return dbentities.Agent.Where(n => n.account == id).First();
        }

        public bool AddAgent(Agent agent)
        {
            try
            {
                dbentities.Agent.Add(agent);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateAgent(Agent agent)
        {
            try
            {
                dbentities.Agent.FirstOrDefault(x => x.id == agent.id);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }
    }
}
