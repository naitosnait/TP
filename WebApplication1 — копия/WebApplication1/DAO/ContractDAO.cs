using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class ContractDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Contract> GetAllContract()
        {
            return dbentities.Contract.Select(n => n);
        }


        public Contract GetContract(int id)
        {
            return dbentities.Contract.Where(n => n.id == id).First();
        }

        public IEnumerable<Contract> GetInsurance(int id)
        {
            return dbentities.Contract.Where(n => n.insurant == id);
        }

        public IEnumerable<Contract> GetAgent(int id)
        {
            return dbentities.Contract.Where(n => n.agent == id);
        }

        public bool AddContract(Contract contract)
        {
            try
            {
                dbentities.Contract.Add(contract);
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateContract(Contract contract)
        {
            try
            {
                var Entity = dbentities.Contract.FirstOrDefault(x => x.id == contract.id);
                Entity.series = contract.series;
                Entity.number = contract.number;
                Entity.agent = contract.agent;
                dbentities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Ошибка: ", ex);
                return false;
            }
            return true;
        }

        public bool UpdateContractStatus(Contract contract)
        {
            try
            {
            var Entity = dbentities.Contract.FirstOrDefault(x => x.id == contract.id);
            Entity.status = contract.status;
            dbentities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}