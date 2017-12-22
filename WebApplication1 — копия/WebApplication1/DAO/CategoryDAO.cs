using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Agency;

namespace WebApplication1.DAO
{
    public class CategoryDAO
    {
        private AgencyEntities dbentities = new AgencyEntities();

        public IEnumerable<Category> GetAllCategory()
        {
            return dbentities.Category.Select(n => n);
        }
    }
}