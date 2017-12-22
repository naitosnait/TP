using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class CoefDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Coef> GetAllCoef()
        {
            return dbentities.Coef.Select(n => n);
        }

        public Coef GetCoef(int id)
        {
            return dbentities.Coef.Where(n => n.id == id).First();
        }
    }
}