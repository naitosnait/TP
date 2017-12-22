using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class TaxDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Tax> GetAllTax()
        {
            return dbentities.Tax.Select(n => n);

        }

        public Tax GetTax(int id)
        {
            return dbentities.Tax.Where(n => n.id == id).First();
        }

        public bool AddTax(Tax tax)
        {
            try
            {
                dbentities.Tax.Add(tax);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateTax(Tax tax)
        {
            try
            {
                dbentities.Tax.FirstOrDefault(x => x.id == tax.id);
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