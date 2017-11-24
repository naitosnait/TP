using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Contract
    {
        public List<Contract> ContractList { get; set; }

        public Contract() { }

        public int id { get; set; }
        public string series { get; set; }
        public int number { get; set; }
        public string insurant_f { get; set; }
        public string insurant_i { get; set; }
        public string insurant_o { get; set; }
        public int tax { get; set; }
        public DateTime date { get; set; }
        public int coef { get; set; }
        public decimal cost { get; set; }

        public Contract(string s)
        {
            series = s;
        }
    }
}