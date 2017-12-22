using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class InsuranceCaseDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<InsuranceCase> GetAllCase()
        {
            return dbentities.InsuranceCase.Select(n => n);
        }

        public IEnumerable<InsuranceCase> GetInsurance(int id)
        {
            return dbentities.InsuranceCase.Where(n => n.insurant == id);
        }

        public InsuranceCase GetCase(int id)
        {
            return dbentities.InsuranceCase.Where(n => n.id == id).First();
        }

        public bool AddCase(InsuranceCase insurance)
        {
            try
            {
                dbentities.InsuranceCase.Add(insurance);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateCase(InsuranceCase insurance)
        {
            try
            {
                var Entity = dbentities.InsuranceCase.FirstOrDefault(x => x.id == insurance.id);
                Entity.contract = insurance.contract;
                Entity.description = insurance.description;
                Entity.insurant = insurance.insurant;
                Entity.phone = insurance.phone;
                Entity.status = insurance.status;
                Entity.datestart = insurance.datestart;
                Entity.datestop = insurance.datestop;

                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateCaseStatus(InsuranceCase insurance)
        {
            try
            {
                var Entity = dbentities.InsuranceCase.FirstOrDefault(x => x.id == insurance.id);
                Entity.status = insurance.status;
                Entity.datestop = insurance.datestop;

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