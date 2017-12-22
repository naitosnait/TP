using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;

namespace WebApplication1.DAO
{
    public class TypeDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Models.Agency.Type> GetAllType()
        {
            return dbentities.Type.Select(n => n);
        }
    }
}