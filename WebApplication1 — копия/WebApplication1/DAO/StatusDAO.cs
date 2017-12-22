using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;
using WebApplication1.Models;


namespace WebApplication1.DAO
{
    public class StatusDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Status> GetAllStatus()
        {
            return dbentities.Status.Select(n => n);
        }

    }
}