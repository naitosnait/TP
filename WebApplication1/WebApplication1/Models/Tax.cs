using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Tax
    {
        public List<Tax> TaxList { get; set; }

        public Tax() { }

        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public int category { get; set; }
        public string property { get; set; }
        public decimal price { get; set; }

        public Tax(string s)
        {
            name = s;
        }
    }
}