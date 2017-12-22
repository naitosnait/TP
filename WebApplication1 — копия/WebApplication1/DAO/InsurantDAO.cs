using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class InsurantDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Insurant> GetAllInsurant()
        {
            return dbentities.Insurant.Select(n => n);
        }

        public Insurant GetInsurant(int id)
        {
            return dbentities.Insurant.Where(n => n.id == id).First();
        }

        public Insurant GetAcc(string id)
        {
            return  dbentities.Insurant.Where(n => n.account == id).First();
        }

        public bool AddInsurant(Insurant insurant)
        {
            try
            {
                dbentities.Insurant.Add(insurant);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateInsurant(Insurant insurant)
        {
            try
            {
                dbentities.Insurant.FirstOrDefault(x => x.id == insurant.id);
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
