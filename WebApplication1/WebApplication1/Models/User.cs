using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        public List<User> UserList { get; set; }

        public User() { }

        string login { get; set; }
        string pass { get; set; }
        string fio { get; set; }
        int phone { get; set; }

        public void LogIn()
        {

        }

        public void LogOut()
        {

        }

        public void Registration()
        {

        }
    }
}